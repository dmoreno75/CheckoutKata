using Kata;
using NUnit.Framework;
using System.Collections.Generic;

namespace Kata.Tests
{
	public class CheckoutTests
	{
		List<Item> testProductCatalogue = new List<Item>
		{
			new Item("A99", 0.50m),
			new Item("B15", 0.30m),
			new Item("C40", 0.60m),
		};

		List<SpecialOffer> testSpecialOffers = new List<SpecialOffer>
		{
			new SpecialOffer("A99", 3, 1.30m),
			new SpecialOffer("B15", 2, 0.45m),
		};


		[Test]
		public void WhenEmptyBasket_ExpectedTotalZero()
		{
			var checkout = new Checkout(testProductCatalogue, testSpecialOffers);

			Assert.AreEqual(checkout.Total(), 0);
		}

		[Test]
		public void WhenItemNotInCatalog_ExpectedTotalZero()
		{
			var checkout = new Checkout(testProductCatalogue, testSpecialOffers);

			checkout.Scan("A99XXXX");

			Assert.AreEqual(checkout.Total(), 0);
		}

		[Test]
		public void WhenScanSingleItem_ExpectedTotalCorrect()
		{
			var checkout = new Checkout(testProductCatalogue, testSpecialOffers);

			checkout.Scan("A99");

			Assert.AreEqual(checkout.Total(), 0.50m);
		}

		[Test]
		public void WhenScanMultipleItemsNoSpecialOffers_ExpectedTotalCorrect()
		{
			var checkout = new Checkout(testProductCatalogue, new List<SpecialOffer>());

			checkout.Scan("B15");
			checkout.Scan("A99");
			checkout.Scan("B15");
			
			Assert.AreEqual(checkout.Total(), 1.10m);
		}

		[Test]
		public void WhenScanMultipleItemsWithSimpleSpecialOffers_ExpectedTotalCorrect()
		{
			var checkout = new Checkout(testProductCatalogue, testSpecialOffers);

			checkout.Scan("B15");
			checkout.Scan("A99");
			checkout.Scan("B15");

			Assert.AreEqual(checkout.Total(), 0.95m);
		}

		[Test]
		public void WhenScanMultipleItemsWithComplexSpecialOffers_ExpectedTotalCorrect()
		{
			var checkout = new Checkout(testProductCatalogue, testSpecialOffers);
		
			checkout.Scan("B15");
			checkout.Scan("B15");
			checkout.Scan("B15");
			checkout.Scan("A99");
			checkout.Scan("A99");
			checkout.Scan("A99");
			checkout.Scan("A99");
			checkout.Scan("B15");
			Assert.AreEqual(checkout.Total(), 2.7m);
		}
	}
}