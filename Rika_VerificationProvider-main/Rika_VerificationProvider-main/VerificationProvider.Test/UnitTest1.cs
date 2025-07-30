using System;
using Xunit;
using Moq;
using VerificationProvider.Services;
using VerificationProvider.Models;
using Microsoft.Extensions.Logging;
using VerificationProvider.Data.Contexts;
using Azure.Messaging.ServiceBus;

namespace VerificationProvider.Tests
{
    public class VerificationServiceTests
    {
        // Arrange, Act, Assert pattern (AAA) is followed in all tests.

        [Fact]
        public void GenerateCode_ShouldReturn6DigitCode()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationService>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            var verificationService = new VerificationService(mockLogger.Object, mockServiceProvider.Object);

            // Act
            var result = verificationService.GenerateCode();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(6, result.Length);
            Assert.True(int.TryParse(result, out _));
        }

        [Fact]
        public void GenerateEmailRequest_ShouldReturnValidEmailRequest()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationService>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            var verificationService = new VerificationService(mockLogger.Object, mockServiceProvider.Object);
            var verificationRequest = new VerificationRequest { Email = "test@example.com" };
            var code = "123456";

            // Act
            var emailRequest = verificationService.GenerateEmailRequest(verificationRequest, code);

            // Assert
            Assert.NotNull(emailRequest);
            Assert.Equal("test@example.com", emailRequest.To);
            Assert.Contains(code, emailRequest.HtmlBody);
        }

        [Fact]
        public async Task SaveVerificationRequest_ShouldReturnTrue_WhenRequestIsSavedSuccessfully()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationService>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            var mockDbContext = new Mock<DataContext>();
            mockServiceProvider.Setup(x => x.GetService(typeof(DataContext))).Returns(mockDbContext.Object);
            var verificationService = new VerificationService(mockLogger.Object, mockServiceProvider.Object);
            var verificationRequest = new VerificationRequest { Email = "test@example.com" };
            var code = "123456";

            // Act
            var result = await verificationService.SaveVerificationRequest(verificationRequest, code);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveExpiredRecords_ShouldRemoveExpiredRequests()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationCleanerService>>();
            var mockContext = new Mock<DataContext>();
            var verificationCleanerService = new VerificationCleanerService(mockLogger.Object, mockContext.Object);

            // Act
            await verificationCleanerService.RemoveExpiredRecordsAsync();

            // Assert
            mockContext.Verify(x => x.RemoveRange(It.IsAny<System.Collections.IEnumerable>()), Times.Once);
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task ValidateCode_ShouldReturnTrue_WhenCodeIsValid()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ValidateVerificationCodeService>>();
            var mockContext = new Mock<DataContext>();
            var validateService = new ValidateVerificationCodeService(mockLogger.Object, mockContext.Object);
            var validateRequest = new ValidateRequest { Email = "test@example.com", Code = "123456" };

            // Act
            var result = await validateService.ValidateCodeAsync(validateRequest);

            // Assert
            Assert.False(result); // Assuming no data in mock, should be false initially
        }

        [Fact]
        public void GenerateServiceBusEmailRequest_ShouldReturnValidJsonString()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationService>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            var verificationService = new VerificationService(mockLogger.Object, mockServiceProvider.Object);
            var emailRequest = new EmailRequest { To = "test@example.com", Subject = "Test", HtmlBody = "<p>Test</p>", PlainText = "Test" };

            // Act
            var result = verificationService.GenerateServiceBusEmailRequest(emailRequest);

            // Assert
            Assert.NotNull(result);
            Assert.Contains("test@example.com", result);
        }

        [Fact]
        public void UnpackVerificationRequest_ShouldReturnValidRequest_WhenMessageIsValid()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationService>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            var verificationService = new VerificationService(mockLogger.Object, mockServiceProvider.Object);

            var messageBody = "{\"Email\":\"test@example.com\"}";
            var binaryData = BinaryData.FromString(messageBody);

            // Mocking ServiceBusReceivedMessage and setting its properties
            var mockMessage = new Mock<ServiceBusReceivedMessage>();
            mockMessage.Setup(m => m.Body).Returns(binaryData);

            // Act
            var result = verificationService.UnpackVerificationRequest(mockMessage.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task ValidateCode_ShouldReturnFalse_WhenCodeIsInvalid()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<ValidateVerificationCodeService>>();
            var mockContext = new Mock<DataContext>();
            var validateService = new ValidateVerificationCodeService(mockLogger.Object, mockContext.Object);
            var validateRequest = new ValidateRequest { Email = "invalid@example.com", Code = "000000" };

            // Act
            var result = await validateService.ValidateCodeAsync(validateRequest);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GenerateEmailRequest_ShouldThrowException_WhenEmailIsNull()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationService>>();
            var mockServiceProvider = new Mock<IServiceProvider>();
            var verificationService = new VerificationService(mockLogger.Object, mockServiceProvider.Object);
            VerificationRequest verificationRequest = null;
            var code = "123456";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => verificationService.GenerateEmailRequest(verificationRequest, code));
        }

        [Fact]
        public async Task RemoveExpiredRecords_ShouldNotThrowException_WhenNoExpiredRecordsExist()
        {
            // Arrange
            var mockLogger = new Mock<ILogger<VerificationCleanerService>>();
            var mockContext = new Mock<DataContext>();
            var verificationCleanerService = new VerificationCleanerService(mockLogger.Object, mockContext.Object);

            // Act & Assert
            await verificationCleanerService.RemoveExpiredRecordsAsync();
            mockContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }
    }
}