using LUMTask.Controllers;
using LUMTask.Domain.Model;
using LUMTask.Domain.Repositories;
using LUMTaskTest.Infrastructures;
using Raven.Client.Documents;
using Raven.Client.Documents.Indexes;
using Raven.TestDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LUMTaskTest.Controller
{
    public class MaterialControllerTest : RavenTestDriver
    {
        //This allows us to modify the conventions of the store we get from 'GetDocumentStore'


        [Fact]
        public void PostTest()
        {

            ConfigureServer(new TestServerOptions
            {
                DataDirectory = "C:\\RavenDBTestDir",
                FrameworkVersion = null
            });

            MaterialModel materialModel = new MaterialModel
            {

                MaterialName = "Test ",
                Author = "modrek",
                Visible = true,
                Type = TypeOfPhase.Continuou,
                Note = "newstring",
                MaterialFunction = new MaterialFunctionModel
                {
                    Min = 5,
                    Max = 70
                }
            };

            DocumentStoreTest documentStoreTest = new DocumentStoreTest();

            IMaterialRepository materialRepository = new MaterialRepository(documentStoreTest);
            MaterialsController materialController = new MaterialsController(materialRepository);
            materialController.Post(materialModel);

            var result = materialController.GetByName("Test");
            Assert.Equal(materialModel.MaterialName, result.MaterialName);


        }


    }

   


}
