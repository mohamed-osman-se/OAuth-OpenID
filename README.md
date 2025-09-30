# 🔐 OAuth & OpenID Connect Demo in ASP.NET Core

A focused ASP.NET Core project demonstrating **secure authentication & authorization** using modern identity protocols. Learn how to integrate **OpenID Connect with Google** and **OAuth 2.0 with GitHub** to enable robust user identity and GitHub repo access.

---

## ✨ Features

-   **Google Sign-In with OpenID Connect**: Authenticate users using a secure, standards-based approach.
-   **GitHub OAuth Integration**: Authorize and fetch user repositories securely via GitHub's OAuth flow.
-   **Standard Authorization Code Flow**: Implements the **Authorization Code Flow** (without PKCE) suitable for **server-side** applications.
-   **Custom JWT Issuance**: After successful authentication via Google, the server generates its **own JWT** to manage sessions internally, enabling:
    -   Custom claims (e.g., roles, app-specific permissions)
    -   First-party token control and expiration
    -   Lightweight stateless session management
-   **High-Security Implementation**:
    -   Uses **anti-CSRF protection**
    -   
---

## 🛠️ Tech Stack

| Category       | Technology                              |
| -------------- | ---------------------------------------- |
| **Backend**    | `ASP.NET Core (.NET 9)`                |
| **Auth Protocols** | `OpenID Connect`, `OAuth 2.0`         |
| **Providers**  | `Google`, `GitHub`                      |
| **Security**   | `Anti-CSRF`, `Authorization Code Flow` |

---

## 🎬 Video Walkthrough

[![Watch the video](https://img.youtube.com/vi/0eGRF5HPSuk/0.jpg)](https://youtu.be/0eGRF5HPSuk?si=rGnNdc9Fy8yc3sBp)

---



### Run Locally  

```bash
git clone https://github.com/mohamed-osman-se/OAuth-OpenID.git
cd OAuth-OpenID
dotnet run
```

## Don't Forget To Configure Your Secrets In appsettings.json
