using Microsoft.Extensions.Logging;
using MitoCodeStore.DataAccess;
using MitoCodeStore.Dto;
using MitoCodeStore.Entities;
using MitoCodeStore.Services;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using MitoCodeStore.DataAccess.Repositories;
using Xunit;

namespace MitoCodeStorexUnitTest
{
    public class ClientexUnitTest : DbContextUnitTest
    {
        [Fact]
        public async Task CustomerListShouldReturnPagesCount()
        {
            // Arrange
            var repository = new Mock<ICustomerRepository>();
            repository.Setup(x => x.GetCollectionAsync(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(CustomerResults());

            var logger = new Mock<ILogger<CustomerDto>>();

            var service = new CustomerService(repository.Object, logger.Object);


            // Act
            var actual = await service.GetCollectionAsync("", 1, 4);
            var expected = 11;

            // Assert
            Assert.Equal(expected, actual.TotalPages);
        }

        private Task<(ICollection<Customer> collection, int total)> CustomerResults()
        {
            ICollection<Customer> list = new List<Customer>();

            for (int i = 0; i < 41; i++)
            {
                list.Add(new Customer
                {
                    Id = i,
                    Name = $"Customer {i}",
                });
            }

            return Task.FromResult((list, list.Count));
        }


        [Theory]
        [InlineData("", 10, 10)]
        [InlineData("", 4, 25)]
        [InlineData("", 100, 1)]
        [InlineData("Tony Stark", 100, 0)]
        public async Task CustomerMustCheckPagination(string filter, int rows, int expected)
        {
            // Arrange
            var repository = new CustomerRepository(_context);
            //ICollection<Customer> list = new List<Customer>();

            //var repository = new Mock<ICustomerRepository>();
            //repository.Setup(x => x.GetCollectionAsync("", 1, rows))
            //    .ReturnsAsync((list, 0));

            var logger = new Mock<ILogger<CustomerDto>>();

            var service = new CustomerService(repository, logger.Object);

            // Act
            var actual = await service.GetCollectionAsync(filter, 1, rows);

            // Assert
            Assert.Equal(expected, actual.TotalPages);
        }

    }
}
