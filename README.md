# Entity Images Management System

A modern full-stack application for managing customer files, built with enterprise-grade architecture patterns and cutting-edge technologies.

#### Planned Enhancements
I have designed the EntityImage table in the database to support storing images for multiple entity types (e.g., customers, leads, etc.). Currently, I’ve implemented APIs only for customer image uploads, but the same service can be reused to build endpoints for other entities as well.

## Architecture Overview

### Backend (.NET 8)

- **Clean Architecture** - Separation of concerns with distinct layers
- **SQL Server** - Robust relational database for data persistence
- **Domain-Driven Design** - Business logic encapsulation

### Frontend (Next.js 15)

- **Server-Side Rendering (SSR)** - Optimized performance and SEO
- **App Router** - Modern Next.js routing system
- **TypeScript** - Type-safe development
- **ShadCN UI** - Beautiful, accessible components
- **Zustand** - Lightweight state management

## 🚀 Features

- **File Management**: Upload, organize, and manage customer documents
- **Customer Management**: Comprehensive customer data handling
- **Image Optimization**: Advanced image processing and optimization
- **Real-time Updates**: Live synchronization across sessions
- **Responsive Design**: Mobile-first UI approach
- **Type Safety**: End-to-end TypeScript implementation

## 🛠️ Technology Stack

### Backend

- **.NET 8** - Latest framework with improved performance
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Enterprise database solution
# - **AutoMapper** - Object mapping
- **Image Processing** - Optimization and manipulation of uploaded images

### Frontend

- **Next.js 15** - React framework with SSR
- **TypeScript** - Static type checking
- **Tailwind CSS** - Utility-first CSS framework
- **ShadCN UI** - Component library
- **Zustand** - State management
- **React Hook Form** - Form handling
- **Zod** - Schema validation

## Getting Started

### Prerequisites

- .NET 8 SDK
- Node.js 18+
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or VS Code

### Backend Setup

1. **Clone the repository**

   ```bash
   git clone <repository-url>
   cd PrjectHejer
   ```

2. **Configure database connection**

   ```json
   // appsettings.json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=CustomerFilesDB;Trusted_Connection=true"
     }
   }
   ```

3. **Run database migrations**

   ```bash
   dotnet ef database update
   ```

4. **Start the API**

   ```bash
   dotnet watch run --project PrjectHejer.Server
   ```

### Frontend Setup

1. **Navigate to frontend directory**

   ```bash
   cd PrjectHejer.Client
   ```

2. **Install dependencies**

   ```bash
   npm install
   ```

3. **Configure environment variables**

   ```bash
   # .env.local
   NEXT_PUBLIC_API_URL=https://localhost:7271/api
   ```

4. **Start development server**

   ```bash
   npm run dev
   ```

## 📦 Deployment

### Backend Deployment

- Configure production connection strings
- Build and publish the application
- Deploy to IIS, Azure App Service, or Docker

### Frontend Deployment

- Build the Next.js application: `npm run build`
- Deploy to Vercel, Netlify, or any Node.js hosting platform
