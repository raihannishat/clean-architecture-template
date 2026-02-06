// System namespaces
global using System.Net;
global using System.Text.Json;
global using System.Threading.RateLimiting;

// Third-party libraries
global using FluentValidation;
global using MediatR;
global using Scalar.AspNetCore;
global using Serilog;

// ASP.NET Core
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.RateLimiting;

// Application layer
global using WeatherForecastApp.Application.Common;
global using WeatherForecastApp.Application.Contracts.Services;
global using WeatherForecastApp.Application.DTOs.Samples;
global using WeatherForecastApp.Application.Features.Sample.Commands.CreateSample;

// Extensions
global using WeatherForecastApp.API.Constants;
global using WeatherForecastApp.API.Extensions;
global using WeatherForecastApp.Application.Extensions;
global using WeatherForecastApp.Infrastructure.Extensions;

// Project namespaces
global using WeatherForecastApp.API.Middleware;
global using WeatherForecastApp.API.Models;
