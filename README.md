# FdjUnited Exchange API

A .NET 8 Web API for currency exchange rate conversion using live data from exchangerate-api.com.

## Prerequisites

- **.NET 8.0 SDK** or later
- **Visual Studio Code** or **Visual Studio 2022** (recommended)
- **Git** (for cloning the repository)

## Project Structure

```
FdjUnited/
├── FdjUnited.Api.Exchange/           # Main Web API project
├── FdjUnited.Api.Contracts/          # DTOs and Commands
├── FdjUnited.Api.Infrastructure/     # Services and External API integration
├── FdjUnited.Common/                 # Shared utilities and base classes
└── TestExchangeService/              # Test console application
```

## Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/foramshah1210/FdjUnitedTest.git
cd FdjUnited
```

### 2. Restore Dependencies

```bash
cd FdjUnited.Api.Exchange
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the Application

```bash
cd FdjUnited.Api.Exchange
dotnet run --urls "http://localhost:5002"
```

The application will start and be available at:
- **API Base URL**: http://localhost:5002
- **Swagger UI**: http://localhost:5002/swagger

## API Endpoints

### POST /api/ExchangeService

Converts currency amounts using live exchange rates.

**Request Body:**
```json
{
  "amount": 100.00,
  "inputCurrency": "USD",
  "outputCurrency": "EUR"
}
```

**Response:**
```json
{
  "data": {
    "amount": 100.00,
    "inputCurrency": "USD",
    "outputCurrency": "EUR",
    "value": 84.63
  },
  "statusCode": 200,
  "errors": []
}
```

## Features Included
- Dependency Injection (DI) setup
- MediatR pattern implementation
- Robust error handling within handlers
- Swagger configuration added
- Clean and well-structured project architecture

## Features Not Inlduded / Improvements / Enhancements / Production Ready
- Configure and bind the ExchangeService API in startup and inject it into the service
- Complete unit test coverage, including handlers, services, and controllers
- Add Refit instead of HttpClient
- Implement a retry mechanism using Polly (or similar) with exponential backoff
- ExchangerateServiceSettings configuration 
- Improve error handling within the service layer
- Add pipeline configuration to the project
- Implement secure key management using a secrets manager (e.g., Vault)
- Validator class implementation for Input Data
- Handle all ProducesResponseTypes for Controller methods


## Troubleshooting

### Port Already in Use
If you encounter "address already in use" errors, try running on a different port:

```bash
dotnet run --urls "http://localhost:5003"
```

