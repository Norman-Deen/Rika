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

The component archives are preserved in their existing directories:

- `Rika_WebApp-main/` — web application and business project.
- `Rika_Backoffice-main/` — Blazor backoffice.
- `Rika_AccountProvider-main/` — account provider.
- `Rika_VerificationProvider-main/` — verification provider.
- `Rika_EmailProvider-main/` — email provider.
- `Rika_ProductProvider-main/` — product provider.
- `Rika_CategoryProvider-master/` — category provider.
- `Rika_OrderProvider-main/` — order provider.
- `Rika_ReportProvider-master/` — reporting provider.
- `TestRepo-main/` and `TestRepo2-main/` — additional training snapshots.

Most archives contain a second directory with the same name. The commands below use the actual paths from the repository root.


---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- Visual Studio 2022+ with ASP.NET & Azure workload
- Azure Functions Core Tools (for Providers)

### Running the WebApp

```bash
cd Rika_WebApp-main/Rika_WebApp-main/Rika_WebApp
dotnet run
```

### Running a Provider (e.g. Product)

```bash
cd Rika_ProductProvider-main/Rika_ProductProvider-main/Rika_ProductProvider
func start
```

Repeat for other providers as needed.

---

## 🧰 Technologies Used

* Blazor Server (.NET 8)
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
