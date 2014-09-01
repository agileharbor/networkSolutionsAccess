using System.Collections.Generic;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Misc;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.Models.Product;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsProductsService: NetworkSolutionsBaseService, INetworkSolutionsProductsService
	{
		public NetworkSolutionsProductsService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config, CacheManager cacheManager = null ): base( appConfig, config, cacheManager )
		{
		}

		public IEnumerable< ProductType > GetProducts()
		{
			var result = new List< ProductType >();
			for( var i = 0; i < 99999; i++ )
			{
				var request = new ReadProductRequestType { PageRequest = new PaginationType { Size = PageSize, Page = i }, Version = Version, DetailSize = SizeCodeType.Large };
				var response = this.WebRequestServices.Get( this.Client.ReadProduct, this.GetCredentials(), request );
				if( response.ProductList == null || response.ProductList.Length == 0 )
					break;
				result.AddRange( response.ProductList );
			}

			return result;
		}

		public async Task< IEnumerable< ProductType > > GetProductsAsync()
		{
			var result = new List< ProductType >();
			for( var i = 0; i < 99999; i++ )
			{
				var request = new ReadProductRequestType { PageRequest = new PaginationType { Size = PageSize, Page = i }, Version = Version, DetailSize = SizeCodeType.Large };
				var response = await this.WebRequestServices.GetAsync( this.Client.ReadProductAsync, this.GetCredentials(), request );
				if( response.ReadProductResponse1.ProductList == null || response.ReadProductResponse1.ProductList.Length == 0 )
					break;
				result.AddRange( response.ReadProductResponse1.ProductList );
			}

			return result;
		}

		public NetworkSolutionsInventory UpdateInventory( NetworkSolutionsInventory inventory )
		{
			var request = new UpdateInventoryRequestType
			{
				Version = Version,
				Inventory = new InventoryType { ProductId = inventory.ProductId, QtyInStock = new ProductQuantityType { Value = inventory.QtyInStock, Adjustment = inventory.Adjustment } }
			};

			var response = this.WebRequestServices.Submit( this.Client.UpdateInventory, this.GetCredentials(), request );
			if( response.Inventory == null )
				return null;

			return new NetworkSolutionsInventory
			{
				ProductId = response.Inventory.ProductId,
				QtyInStock = response.Inventory.QtyInStock.Value,
				Adjustment = response.Inventory.QtyInStock.Adjustment
			};
		}

		public async Task< NetworkSolutionsInventory > UpdateInventoryAsync( NetworkSolutionsInventory inventory )
		{
			var request = new UpdateInventoryRequestType
			{
				Version = Version,
				Inventory = new InventoryType { ProductId = inventory.ProductId, QtyInStock = new ProductQuantityType { Value = inventory.QtyInStock, Adjustment = inventory.Adjustment } }
			};

			var response = await this.WebRequestServices.SubmitAsync( this.Client.UpdateInventoryAsync, this.GetCredentials(), request );
			if( response.UpdateInventoryResponse1.Inventory == null )
				return null;

			return new NetworkSolutionsInventory
			{
				ProductId = response.UpdateInventoryResponse1.Inventory.ProductId,
				QtyInStock = response.UpdateInventoryResponse1.Inventory.QtyInStock.Value,
				Adjustment = response.UpdateInventoryResponse1.Inventory.QtyInStock.Adjustment
			};
		}

		public IEnumerable< NetworkSolutionsInventory > UpdateInventory( IEnumerable< NetworkSolutionsInventory > inventory )
		{
			var result = new List< NetworkSolutionsInventory >();
			foreach( var inv in inventory )
			{
				var response = this.UpdateInventory( inv );
				if( response != null )
					result.Add( response );
			}
			return result;
		}

		public async Task< IEnumerable< NetworkSolutionsInventory > > UpdateInventoryAsync( IEnumerable< NetworkSolutionsInventory > inventory )
		{
			var result = new List< NetworkSolutionsInventory >();
			foreach( var inv in inventory )
			{
				var response = await this.UpdateInventoryAsync( inv );
				if( response != null )
					result.Add( response );
			}
			return result;
		}
	}
}