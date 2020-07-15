using LUMTask.Domain.Model;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using Raven.TestDriver;
using System;
using System.Collections.Generic;
using System.Text;

namespace LUMTaskTest.Infrastructures
{
    public interface IDocumentStoreTest
    {
        public IDocumentSession GetSession();
    }
    public class DocumentStoreTest : RavenTestDriver, IDocumentStoreTest, IDocumentStoreHolder
    {
        private IDocumentSession _session;
        public IDocumentSession GetSession()
        {
            if (_session == null)
            {
                var store = GetDocumentStore();
                _session = store.OpenSession();
            }
            return _session;
        }


        protected override void PreInitialize(IDocumentStore documentStore)
        {
            documentStore.Conventions.MaxNumberOfRequestsPerSession = 50;
        }


    }
}
