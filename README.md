# 💰 FinanceApp

A fullstack financial application composed of a **.NET Core 5** backend and a **React 19 + Vite** frontend.

---

## 📁 Project Structure

```
FinanceApp/
├── backend/       # .NET 5 API with Entity Framework Core
│   └── FinanceApp/
├── frontend/      # React 19 + Vite frontend app
├── db/            # Database scripts
├── README.md
└── .gitignore
```

---

## 🚀 Main Technologies

### Backend:
- .NET 5 (ASP.NET Core)
- Entity Framework Core
- SQL Server

### Frontend:
- React 19
- Vite
- TypeScript
- Material UI (MUI)
- React Hook Form + Yup
- TanStack React Query
- Axios
- React Router v7

---

## ⚙️ Prerequisites

### General
- Git
- Visual Studio (recommended for backend) or VS Code

### Backend
- .NET SDK 5.0
- SQL Server or compatible DB
- EF Core CLI (optional):  
  ```bash
  dotnet tool install --global dotnet-ef
  ```

### Frontend
- Node.js (v18 or later)
- npm (v9+) or yarn

---

## ▶️ Running the Project

### 1. Clone the repository

```bash
git clone https://github.com/kcampos25/FinanceApp.git
cd FinanceApp
```

---

### 2. Run the Backend

```bash
cd backend/FinanceApp
dotnet restore
dotnet build
dotnet run
```

> ⚠️ Make sure to configure the connection string (`ConnectionStrings`) in `appsettings.json`.

---

### 3. Run the Frontend

```bash
cd frontend
npm install
npm run dev
```

> The app will start at [http://localhost:5173](http://localhost:5173)

---

## 🧪 Useful Scripts (Frontend)

| Command           | Description                  |
|-------------------|------------------------------|
| `npm run dev`     | Start development server     |
| `npm run build`   | Create production build      |
| `npm run preview` | Preview the production build |
| `npm run lint`    | Run ESLint                  |
| `npm run format`  | Format files with Prettier   |

---

## 🧰 Database

This project uses a **Database-First** approach, so the database already exists and the data model is generated from it.

SQL scripts for initial setup and updates are located in the `/db` folder.

To update your data model from the database, you can use the Entity Framework Core Scaffold command:

```bash
dotnet ef dbcontext scaffold "YourConnectionString" Microsoft.EntityFrameworkCore.SqlServer -o Models
```

> Replace `"YourConnectionString"` with your actual connection string.

---

**Note:** Migrations are not used to update the database, as it is managed directly on the server or through SQL scripts.

---

## 📦 Production Build

### Frontend

```bash
cd frontend
npm run build
```

This generates a production-ready build in the `dist/` folder.

### Backend

```bash
cd backend/FinanceApp
dotnet publish -c Release
```

This generates the build in `bin/Release/net5.0/publish/`.

---

## 🧱 Project Status

🚧 In development.

Feel free to contribute or open an issue if you find any bugs.

---

## 📄 License

MIT License  
See [LICENSE](LICENSE) for more information.

---

## ✉️ Contact

**Author**: kenneth campos 
📧 Email: kennethcampos25@gmail.com  
🔗 GitHub: [@kcampos25](https://github.com/kcampos25)
