using Clean_Architecture_Soufiane.Domain.AggregatesModel.Sales;
using Clean_Architecture_Soufiane.Domain.Events;
using NUnit.Framework;
using Clean_Architecture_Soufiane.Domain.Exceptions;

namespace DeskTop.Unit.SaleAggregateTest
{
    public class SaleAggregateUnitTest
    {
        public SaleAggregateUnitTest()
        { }

        [Test]
        public void Create_order_item_success()
        {
            //Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 5;

            //Act 
            var fakeOrderItem = new SaleItem(productId, productName, unitPrice, discount, pictureUrl, units);

            //Assert
            Assert.NotNull(fakeOrderItem);
        }

        [Test]
        public void Invalid_number_of_units()
        {
            //Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = -1;

            //Act - Assert
            Assert.Throws<ValidationDomainException>(() => new SaleItem(productId, productName, unitPrice, discount, pictureUrl, units));
        }

        [Test]
        public void Invalid_total_of_order_item_lower_than_discount_applied()
        {
            //Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 1;

            //Act - Assert
            Assert.Throws<ValidationDomainException>(() => new SaleItem(productId, productName, unitPrice, discount, pictureUrl, units));
        }

        [Test]
        public void Invalid_discount_setting()
        {
            //Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 5;

            //Act 
            var fakeOrderItem = new SaleItem(productId, productName, unitPrice, discount, pictureUrl, units);

            //Assert
            Assert.Throws<ValidationDomainException>(() => fakeOrderItem.SetNewDiscount(-1));
        }

        [Test]
        public void Invalid_units_setting()
        {
            //Arrange    
            var productId = 1;
            var productName = "FakeProductName";
            var unitPrice = 12;
            var discount = 15;
            var pictureUrl = "FakeUrl";
            var units = 5;

            //Act 
            var fakeOrderItem = new SaleItem(productId, productName, unitPrice, discount, pictureUrl, units);

            //Assert
            Assert.Throws<ValidationDomainException>(() => fakeOrderItem.AddUnits(-1));
        }

          [Test]
          public void when_add_two_times_on_the_same_item_then_the_total_of_order_should_be_the_sum_of_the_two_items()
          {
              var order = new OrderBuilder()
                  .AddOne(1, "cup", 10.0m, 0, string.Empty)
                  .AddOne(1, "cup", 10.0m, 0, string.Empty)
                  .Build();

              Assert.AreEqual(20.0m, order.GetTotal());
          }
        [Test]
        public void Add_new_Order_raises_new_event()
        {
            //Arrange
            var expectedResult = 1;

            //Act 
            var fakeOrder = new Sale();

            //Assert
            Assert.AreEqual(fakeOrder.DomainEvents.Count, expectedResult);
        }

        [Test]
        public void Add_event_Order_explicitly_raises_new_event()
        {
            //Arrange   
            var expectedResult = 2;

            //Act 
            var fakeOrder = new Sale();
            fakeOrder.AddDomainEvent(new SaleStatusChangedToPaidCachDomainEvent(1));
            //Assert
            Assert.AreEqual(fakeOrder.DomainEvents.Count, expectedResult);
        }

        [Test]
        public void Remove_event_Order_explicitly()
        {
            //Arrange    
            var fakeOrder = new Sale();
            var @fakeEvent = new SaleStatusChangedToPaidCachDomainEvent(1);
            var expectedResult = 1;

            //Act         
            fakeOrder.AddDomainEvent(@fakeEvent);
            fakeOrder.RemoveDomainEvent(@fakeEvent);
            //Assert
            Assert.AreEqual(fakeOrder.DomainEvents.Count, expectedResult);
        }
    }
}
