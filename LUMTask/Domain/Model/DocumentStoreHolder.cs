using Microsoft.Extensions.Configuration;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace LUMTask.Domain.Model
{
  
    public interface IDocumentStoreHolder 
    {
        IDocumentSession GetSession();
    }

    /// <summary>
    /// signlton pattern for 
    /// </summary>
    public class DocumentStoreHolder: IDocumentStoreHolder
    {

        private IDocumentSession _session;
        private readonly IConfiguration _configuration;
        public IDocumentSession GetSession()
        {
            if (_session == null)
            {
                IDocumentStore DocumentStore = new DocumentStore { Urls = new[] { Consts.URL }, Database = Consts.DataBaseName };
                DocumentStore.Initialize();
                _session = DocumentStore.OpenSession();
            }

            return _session;

        }

        public DocumentStoreHolder(IConfiguration configuration)
        {
            #region Validate Configuration
            _configuration = configuration;
            var RavenServerURL = _configuration.GetSection("RavenDB").GetValue<string>("URL");
            if (string.IsNullOrWhiteSpace(RavenServerURL))
            {
                throw new ArgumentException("Configuration must contain Raven Server URL");
            }

            var DataBaseName = _configuration.GetSection("RavenDB").GetValue<string>("DataBaseName");
            if (string.IsNullOrWhiteSpace(DataBaseName))
            {
                throw new ArgumentException("Configuration must contain DataBaseName");
            }

            #endregion

            Consts.URL = RavenServerURL;
            Consts.DataBaseName = DataBaseName;
            
        }
        
    }
}
