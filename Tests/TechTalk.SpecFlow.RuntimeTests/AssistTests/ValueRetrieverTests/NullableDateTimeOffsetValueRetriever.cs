﻿using System;
using System.Globalization;
using System.Threading;
using Xunit;
using FluentAssertions;
using TechTalk.SpecFlow.Assist.ValueRetrievers;

namespace TechTalk.SpecFlow.RuntimeTests.AssistTests.ValueRetrieverTests
{
    public class NullableDateTimeOffsetValueRetrieverTests
    {
        public NullableDateTimeOffsetValueRetrieverTests()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
        }

        [Fact]
        public void Returns_the_value_from_the_DateTimeOffsetValueRetriever()
        {
            Func<string, DateTimeOffset> func = v =>
            {
                if (v == "one") return new DateTimeOffset(2011, 1, 2, 2, 0, 0, TimeSpan.Zero);
                if (v == "two") return new DateTimeOffset(2015, 12, 31, 2, 0, 0, TimeSpan.Zero);
                return DateTimeOffset.MinValue;
            };

            var retriever = new NullableDateTimeOffsetValueRetriever(func);
            retriever.GetValue("one").Should().Be(new DateTimeOffset(2011, 1, 2, 2, 0, 0, TimeSpan.Zero));
            retriever.GetValue("two").Should().Be(new DateTimeOffset(2015, 12, 31, 2, 0, 0, TimeSpan.Zero));
        }

        [Fact]
        public void Returns_null_when_value_is_null()
        {
            var retriever = new NullableDateTimeOffsetValueRetriever(v => DateTimeOffset.Parse("1/1/2016"));
            retriever.GetValue(null).Should().NotHaveValue();
        }

        [Fact]
        public void Returns_null_when_string_is_empty()
        {
            var retriever = new NullableDateTimeOffsetValueRetriever(v => DateTimeOffset.Parse("1/1/2017"));
            retriever.GetValue(string.Empty).Should().NotHaveValue();
        }
    }
}