# 🧩 Rika — Modular Backoffice Platform (Team Project)

**Rika** is a modular backoffice system designed and developed as a team project.  
It consists of multiple independent providers (microservices) connected to a central Blazor WebApp.

This project was developed collaboratively by a group of developers as part of a practical training exercise.

---

## 👥 Team Collaboration

This repository showcases the collective work of a development team.  
Each member contributed to specific modules such as authentication, product handling, emailing, and reporting.

---

## 📦 Project Structure

The solution is organized into multiple independent projects:

```

Rika/
├── Rika\_WebApp/             # Main Blazor WebApp (central UI)
├── Rika\_Backoffice/         # Admin dashboard and routing
├── Rika\_AccountProvider/    # User authentication & account logic
├── Rika\_VerificationProvider/ # Email/Phone verification
├── Rika\_EmailProvider/      # Email service provider
├── Rika\_ProductProvider/    # Product management
├── Rika\_CategoryProvider/   # Product category handler
├── Rika\_OrderProvider/      # Order handling system
├── Rika\_ReportProvider/     # Report generation & logging
├── TestRepo2/               # Testing utilities or legacy modules

```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 7 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022+ with ASP.NET & Azure workload
- Azure Functions Core Tools (for Providers)

### Running the WebApp

```bash
cd Rika_WebApp
dotnet run
```

### Running a Provider (e.g. Product)

```bash
cd Rika_ProductProvider
func start
```

Repeat for other providers as needed.

---

## 🧰 Technologies Used

* Blazor Server (.NET 7)
* Azure Functions
* Entity Framework Core
* REST APIs
* C#
* HTML/CSS

---

## 📄 License

This project was created for training and demonstration purposes only.
Not intended for production use.

---

Made with ❤️ by the **Rika Team**
## 🔗 Original Team Repository

This project was originally developed as a group collaboration.  
You can find the original source and full contribution history here:  
➡️ [Project-Rika on GitHub](https://github.com/Project-Rika)
