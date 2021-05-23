using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace MitoCodeStorexUnitTest
{
    public class ClientexUnitTest : DbContextUnitTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Customer_ShouldUpdateRecordTest(int id)
        {
            // Act
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);
            if (customer == null)
            {
                return;
            }
            customer.Name = "Tony Stark";

            _context.Customers.Attach(customer);
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();

            // Assert.
            Assert.Equal("Tony Stark", customer.Name);
        }
    }
}
