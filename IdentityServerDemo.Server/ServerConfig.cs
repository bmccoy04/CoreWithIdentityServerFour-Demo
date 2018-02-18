using System;
using System.Collections.Generic;
using IdentityServer4.Models;

namespace IdentityServerDemo.Server
{
    public class ServerConfig
    {
        public IEnumerable<Client> GetClients()
        {
            return new List<Client> {

            };
        }

        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {

            };
        }
    }
}