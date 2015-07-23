using System;
using System.Collections.Generic;
using NUnit.Framework;
using com.devjoy.black;

namespace black.core.tests
{
    public class QueryTableTests
    {
        [Test]
        public void CanConvert_AListOfObjects_ToSlimQueryTableFormat()
        {
            var objects = AListOfAnyClass();

            var actual = new QueryTable<AnyClass>().For(objects);

            var expected = new List<object>
                               {
                                   new List<object>
                                       {
                                           new List<object> {"Any Property", "A1"},
                                           new List<object> {"Any Other Property", "B1"},
                                           new List<object> {"Yet Another Property", "C1"},
                                           new List<object> {"A Date Time Property", "01-Jan-2012 13:30:55"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"Any Property", "A2"},
                                           new List<object> {"Any Other Property", "B2"},
                                           new List<object> {"Yet Another Property", "C2"},
                                           new List<object> {"A Date Time Property", "02-Feb-2013 13:30:55"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"Any Property", "A3"},
                                           new List<object> {"Any Other Property", "B3"},
                                           new List<object> {"Yet Another Property", null},
                                           new List<object> {"A Date Time Property", "03-Mar-2014 13:30:55"},
                                       }
                               };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanRename_Columns_Individually_WhenConverting_ObjectsToTable()
        {
            var objects = AListOfAnyClass();

            var table = new QueryTable<AnyClass>();
            table.OverrideColumnName("Any Property", "New Name");
            table.OverrideColumnName("Any Other Property", "Another New Name");

            var actual = table.For(objects);

            var expected = new List<object>
                               {
                                   new List<object>
                                       {
                                           new List<object> {"New Name", "A1"},
                                           new List<object> {"Another New Name", "B1"},
                                           new List<object> {"Yet Another Property", "C1"},
                                           new List<object> {"A Date Time Property", "01-Jan-2012 13:30:55"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"New Name", "A2"},
                                           new List<object> {"Another New Name", "B2"},
                                           new List<object> {"Yet Another Property", "C2"},
                                           new List<object> {"A Date Time Property", "02-Feb-2013 13:30:55"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"New Name", "A3"},
                                           new List<object> {"Another New Name", "B3"},
                                           new List<object> {"Yet Another Property", null},
                                           new List<object> {"A Date Time Property", "03-Mar-2014 13:30:55"},
                                       }
                               };

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CanRename_MultipleColumns_AtTheSameTime_WhenConverting_ObjectsToTable()
        {
            var objects = AListOfAnyClass();

            var table = new QueryTable<AnyClass>();
            table.OverrideColumnNames("Any Property|New Name,Any Other Property|Another New Name");

            var expected = new List<object>
                               {
                                   new List<object>
                                       {
                                           new List<object> {"New Name", "A1"},
                                           new List<object> {"Another New Name", "B1"},
                                           new List<object> {"Yet Another Property", "C1"},
                                           new List<object> {"A Date Time Property", "01-Jan-2012 13:30:55"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"New Name", "A2"},
                                           new List<object> {"Another New Name", "B2"},
                                           new List<object> {"Yet Another Property", "C2"},
                                           new List<object> {"A Date Time Property", "02-Feb-2013 13:30:55"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"New Name", "A3"},
                                           new List<object> {"Another New Name", "B3"},
                                           new List<object> {"Yet Another Property", null},
                                           new List<object> {"A Date Time Property", "03-Mar-2014 13:30:55"},
                                       }
                               };

            Assert.AreEqual(expected, table.For(objects));
        }

        [Test]
        public void CanHide_Columns_WhenConverting_ObjectsToTable()
        {
            var objects = AListOfAnyClass();

            var table = new QueryTable<AnyClass>();
            table.HideColumns("Any Property,Yet Another Property,A Date Time Property");

            var expected = new List<object>
                               {
                                   new List<object>
                                       {
                                           new List<object> {"Any Other Property", "B1"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"Any Other Property", "B2"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"Any Other Property", "B3"},
                                       }
                               };

            Assert.AreEqual(expected, table.For(objects));
        }

        [Test]
        public void CanShow_SpecificColumns_WhenConverting_ObjectsToTable()
        {
            var objects = AListOfAnyClass();

            var table = new QueryTable<AnyClass>();
            table.OnlyShowColumns("Any Other Property,Yet Another Property");

            var expected = new List<object>
                               {
                                   new List<object>
                                       {
                                           new List<object> {"Any Other Property", "B1"},
                                           new List<object> {"Yet Another Property", "C1"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"Any Other Property", "B2"},
                                           new List<object> {"Yet Another Property", "C2"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"Any Other Property", "B3"},
                                           new List<object> {"Yet Another Property", null},
                                       }
                               };

            Assert.AreEqual(expected, table.For(objects));
        }

        [Test]
        public void CanChangeFormat_OfDateTimes_WhenConverting_ObjectsToTable()
        {
            var objects = AListOfAnyClass();

            var table = new QueryTable<AnyClass>();
            table.OnlyShowColumns("A Date Time Property");
            table.UseDateTimeFormat("dd-MMM-yyyy");

            var expected = new List<object>
                               {
                                   new List<object>
                                       {
                                           new List<object> {"A Date Time Property", "01-Jan-2012"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"A Date Time Property", "02-Feb-2013"},
                                       },

                                   new List<object>
                                       {
                                           new List<object> {"A Date Time Property", "03-Mar-2014"},
                                       }
                               };

            Assert.AreEqual(expected, table.For(objects));
        }
       
        private static IEnumerable<AnyClass> AListOfAnyClass()
        {
            var list = new List<AnyClass>
                           {
                               new AnyClass {AnyProperty = "A1", AnyOtherProperty = "B1", YetAnotherProperty = "C1", ADateTimeProperty = new DateTime(2012, 1, 1, 13, 30, 55)},
                               new AnyClass {AnyProperty = "A2", AnyOtherProperty = "B2", YetAnotherProperty = "C2", ADateTimeProperty = new DateTime(2013, 2, 2, 13, 30, 55)},
                               new AnyClass {AnyProperty = "A3", AnyOtherProperty = "B3", YetAnotherProperty = null, ADateTimeProperty = new DateTime(2014, 3, 3, 13, 30, 55)},
                           };
            return list;
        }
    }
}
