using System;

namespace Kata
{
	public class Checkout
	{

		decimal total = 0m;

		public decimal Total()
		{
			return total;
		}

		public void Scan(Item item)
		{
			total += item.UnitPrice;
		}

	}
	public class Item
	{
		public string SKU { get; set; }
		public decimal UnitPrice { get; set; }
		public Item(string sku, decimal unitPrice)
		{
			this.SKU = sku;
			this.UnitPrice = unitPrice;
		}
		public Item(string sku)
		{
			this.SKU = sku;
			this.UnitPrice = 0;
		}
	}

	public class SpecialOffer
	{
		public string SKU { get; set; }
		public uint Quantity { get; set; }
		public decimal OfferPrice { get; set; }
		public SpecialOffer(string sku, uint quantity, decimal offerPrice)
		{
			this.SKU = sku;
			this.Quantity = quantity;
			this.OfferPrice = offerPrice;
		}
	}

}
