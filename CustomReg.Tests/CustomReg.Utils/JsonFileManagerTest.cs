
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CustomReg.Domain;
using CustomReg.Utils.InputOutput;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace CustomReg.Tests.CustomReg.Utils
{
    [TestClass]
    public class JsonFileManagerTest
    {
        private const string FilePath = "test.json";
        private readonly List<FieldConfig> _listTest = new List<FieldConfig>
            {
                new FieldConfig
                {
                    Name = "Joe tester",
                    IsVisible = false,
                    Required = true
                }
            };

        //this will create a dummy file that we need for our tests
       
        void RunBeforeTests(object testObj)
        {
            using (var file = File.CreateText(FilePath))
            {
                var serializer = new JsonSerializer();
                serializer.Serialize(file, testObj);
            }
        }

        /// <summary>
        /// unit test checks that given a json representation of an object, we are able to read it
        /// through our filemanager
        /// </summary>
        [TestMethod]
        public void LoadContentAsTest()
        {
            RunBeforeTests(_listTest);
            var fileManager = new JsonFileManager();
            var resultingObject = fileManager.LoadContentAs<IEnumerable<FieldConfig>>(FilePath);
            var fieldConfigs = resultingObject.ToList();
            var top1 = fieldConfigs.FirstOrDefault();

            Assert.IsNotNull(top1);
            Assert.IsTrue(top1.Required);
        }

        /// <summary>
        /// Unit test checks that we are able to persist object into file
        /// </summary>
        [TestMethod]
        public void PersistContentTest()
        {
            _listTest.Add(new FieldConfig
            {
                Name = "Test 2",
                IsVisible = false,
                Required = false
            });

            RunBeforeTests(_listTest);

            var fileManager = new JsonFileManager();
            var resultingObject = fileManager.LoadContentAs<IEnumerable<FieldConfig>>(FilePath);
            var fieldConfigs = resultingObject.ToList();
            
            Assert.IsTrue(fieldConfigs.Count == 2);
        }

        /// <summary>
        /// cleanup our test files
        /// </summary>
        [TearDown]
        private void RunAfterAnyTests()
        {
            File.Delete(FilePath);
        }
    }
}
