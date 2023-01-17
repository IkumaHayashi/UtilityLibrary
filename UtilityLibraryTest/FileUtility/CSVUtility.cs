using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using UtilityLibrary;

namespace UtilityLibraryTest
{

    /// <summary>
    /// GetSettingsObject ÇÃäTóvÇÃê‡ñæ
    /// </summary>
    [TestClass]
    public class CSVUtilityTestClass
    {

        const string SHIFT_JIS_TEST_DATA_PATH = @"TestData\Shift-Jis_TestData.txt";
        const string UTF_8_TEST_DATA_PATH = @"TestData\UTF-8_TestData.txt";


        [TestMethod]
        public void DetectEncodeName()
        {

            PrivateType privateType = new PrivateType(typeof(CSVUtility));

            string encodename;
            string testDataPath;

            testDataPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,
                                                        SHIFT_JIS_TEST_DATA_PATH);
            encodename = (string)privateType.InvokeStatic("DetectEncodeName", testDataPath);
            Assert.AreEqual("Shift_JIS", encodename);

            testDataPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,
                                                        UTF_8_TEST_DATA_PATH);
            encodename = (string)privateType.InvokeStatic("DetectEncodeName", testDataPath);
            Assert.AreEqual("utf-8", encodename);

        }

        public class TestCSVClass
        {
            public int test0 { get; set; }
            public int test1 { get; set; }
            public int test2 { get; set; }
            public string test3 { get; set; }
            public string test4 { get; set; }
            public string test5 { get; set; }
        }

        public class TestCSVClassDataMap
        : CsvHelper.Configuration.ClassMap<TestCSVClass>
        {
            public TestCSVClassDataMap()
            {
                Map(m => m.test0).Index(0);
                Map(m => m.test1).Index(1);
                Map(m => m.test2).Index(2);
                Map(m => m.test3).Index(3);
                Map(m => m.test4).Index(4);
                Map(m => m.test5).Index(5);

            }
        }

        [TestMethod]
        public void ConvertCSVtoList()
        {
            const string SHIFT_JIS_NOHEADER_NOQUOTES_TEST_DATA_PATH = @"TestData\Shift-Jis_NoHeader_NoQuotes.csv";
            const string UTF8_NOHEADER_NOQUOTES_TEST_DATA_PATH = @"TestData\Shift-Jis_NoHeader_NoQuotes.csv";

            List<TestCSVClass> expectedList = new List<TestCSVClass>()
            {
                new TestCSVClass() {test0 = 0, test1 = 1, test2 = 2, test3 = "test3", test4 = "test4", test5 = "test5"}
                ,new TestCSVClass() {test0 = 0, test1 = 1, test2 = 2, test3 = "test3", test4 = "test4", test5 = "test5"}
                ,new TestCSVClass() {test0 = 0, test1 = 1, test2 = 2, test3 = "test3", test4 = "test4", test5 = "test5"}
                ,new TestCSVClass() {test0 = 0, test1 = 1, test2 = 2, test3 = "test3", test4 = "test4", test5 = "test5"}
                ,new TestCSVClass() {test0 = 0, test1 = 1, test2 = 2, test3 = "test3", test4 = "test4", test5 = "test5"}
            };


            string testDataPath;

            testDataPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,
                                                        SHIFT_JIS_NOHEADER_NOQUOTES_TEST_DATA_PATH);

            List<TestCSVClass> resultList = CSVUtility.ConvertCSVtoList<TestCSVClass, TestCSVClassDataMap>(testDataPath, false, false);
            var properties = typeof(TestCSVClass).GetProperties();

            Assert.AreEqual(expectedList.Count, resultList.Count);

            for(int i = 0; i < resultList.Count; i ++)
            {
                TestCSVClass result = resultList[i];
                TestCSVClass expect = expectedList[i];

                for (int j = 0; j < properties.Length; j++)
                {
                    var resultValue = properties[j].GetValue(result);
                    var expectedValue = properties[j].GetValue(expect);
                    Assert.AreEqual(expectedValue, resultValue);
                }
            }



            testDataPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory,
                                                        UTF8_NOHEADER_NOQUOTES_TEST_DATA_PATH);

            resultList = CSVUtility.ConvertCSVtoList<TestCSVClass, TestCSVClassDataMap>(testDataPath, false, false);


            Assert.AreEqual(expectedList.Count, resultList.Count);

            for (int i = 0; i < resultList.Count; i++)
            {
                TestCSVClass result = resultList[i];
                TestCSVClass expect = expectedList[i];

                for (int j = 0; j < properties.Length; j++)
                {
                    var resultValue = properties[j].GetValue(result);
                    var expectedValue = properties[j].GetValue(expect);
                    Assert.AreEqual(expectedValue, resultValue);
                }
            }

        }
    }
}
