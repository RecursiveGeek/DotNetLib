﻿using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Daikin.DotNetLib.Application
{
    public static class Appsettings
    {
        public static IConfigurationRoot GetConfiguration(string location, string userSecrets = null, params string[] environments)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true)
                .SetBasePath(location)
                .AddEnvironmentVariables();
            if (!string.IsNullOrEmpty(userSecrets)) configurationBuilder.AddUserSecrets(userSecrets);
            if (environments != null)
            {
                foreach (var environment in environments)
                {
                    configurationBuilder.AddJsonFile($"appsettings.{environment}.json", true);
                }
            }
            return configurationBuilder.Build();
        }

        public static IConfigurationRoot GetConfiguration(Type type, string userSecrets = null, params string[] environments)
        {
            var location = Path.GetDirectoryName(type.GetTypeInfo().Assembly.Location);
            return GetConfiguration(location, userSecrets, environments);
        }
    }
}