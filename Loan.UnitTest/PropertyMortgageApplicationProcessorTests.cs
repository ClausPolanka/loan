﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Ploeh.Samples.Loan;
using Ploeh.Samples.Loan.DataCollection;

namespace Ploeh.Samples.Loan.UnitTest
{
    public class PropertyMortgageApplicationProcessorTests
    {
        [Fact]
        public void SutIsMortgageApplicationProcessor()
        {
            var sut = new PropertyMortgageApplicationProcessor();
            Assert.IsAssignableFrom<IMortgageApplicationProcessor>(sut);
        }

        [Theory]
        [InlineData("Main Street 7", "14873 Anywhere", "Norway", 184820, 285)]
        [InlineData("Side Street 5", "8888 Somewhere", "Norway", 992829, 567)]
        public void ProduceOfferReturnsCorrectResult(
            string street,
            string postalCode,
            string country,
            int price,
            int size)
        {
            var sut = new PropertyMortgageApplicationProcessor();
            var application = new MortgageApplication
            {
                Property = new Property
                {
                    Address = new Address
                    {
                        Street = street,
                        PostalCode = postalCode,
                        Country = country
                    },
                    Price = price,
                    Size = size
                }
            };

            var actual = sut.ProduceOffer(application);

            var expected =
                new PropertyProcessor
                {
                    PriceText = "Asking price"
                }
                .ProduceRenderings(application.Property);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SutEqualsOther()
        {
            var sut = new PropertyMortgageApplicationProcessor();
            var other = new PropertyMortgageApplicationProcessor();

            var actual = sut.Equals(other);

            Assert.True(actual);
        }

        [Fact]
        public void SutDoesNotEqualAnonymousObject()
        {
            var sut = new PropertyMortgageApplicationProcessor();
            var anonymous = new object();

            var actual = sut.Equals(anonymous);

            Assert.False(actual);
        }
    }
}
