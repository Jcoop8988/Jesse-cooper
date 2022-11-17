﻿using AngleSharp.Css;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using Xunit;

namespace Radzen.Blazor.Tests
{
    public class PropertyAccessTests
    {
        [Fact]
        public void Getter_Resolves_Property_On_Simple_Object()
        {
            var o = new SimpleObject() { Prop1 = "TestString" };
            var getter = PropertyAccess.Getter<SimpleObject, string>("Prop1");
            var value = getter(o);
            Assert.Equal("TestString", value);
        }

        [Fact]
        public void Getter_Resolves_Property_On_Simple_Object_QueryableType()
        {
            var _data = new List<SimpleObject>()
            {
                new SimpleObject() { Prop1 = "TestString" },
            };

            Func<object, object> getter = PropertyAccess.Getter<object, object>("Prop1");

            var value = getter(_data[0]);
            Assert.Equal("TestString", value);
        }

        [Fact]
        public void Getter_Resolves_Property_On_Nested_Object()
        {
            var o = new NestedObject() { Obj = new SimpleObject { Prop1 = "TestString" } };
            var getter = PropertyAccess.Getter<NestedObject, string>("Obj.Prop1");
            var value = getter(o);
            Assert.Equal("TestString", value);
        }

        [Fact]
        public void Getter_Resolves_Property_From_Array()
        {
            var o = new ArrayObject() { Values = new string[] { "1", "2", "3" } };
            var getter = PropertyAccess.Getter<ArrayObject, string>("Values[1]");
            var value = getter(o);
            Assert.Equal("2", value);
        }

        [Fact]
        public void Getter_Resolves_Property_From_Nested_Array()
        {
            var o = new NestedArrayObject() { Obj = new ArrayObject() { Values = new string[] { "1", "2", "3" } } };
            var getter = PropertyAccess.Getter<NestedArrayObject, string>("Obj.Values[2]");
            var value = getter(o);
            Assert.Equal("3", value);
        }

        [Fact]
        public void Getter_Resolves_Property_From_List()
        {
            var o = new ListObject() { Values = new List<string>() { "1", "2", "3" } };
            var getter = PropertyAccess.Getter<ListObject, string>("Values[1]");
            var value = getter(o);
            Assert.Equal("2", value);
        }

        public class SimpleObject
        {
            public string Prop1 { get; set; }
        }

        public class NestedObject
        {
            public SimpleObject Obj { get; set; }
        }

        public class ArrayObject
        {
            public string[] Values { get; set; }
        }

        public class NestedArrayObject
        {
            public ArrayObject Obj { get; set; }
        }

        public class ListObject
        {
            public List<string> Values { get; set; }
        }
    }
}
