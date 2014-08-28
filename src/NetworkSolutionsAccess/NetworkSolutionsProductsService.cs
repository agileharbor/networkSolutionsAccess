using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.Models.Product;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsProductsService: NetworkSolutionsBaseService, INetworkSolutionsProductsService
	{
		public NetworkSolutionsProductsService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config ): base( appConfig, config )
		{
		}

		public IEnumerable< ProductType > GetProducts()
		{
			var request = new ReadProductRequestType();
			var response = this._webRequestServices.Get( this._client.ReadProduct, this._credentials, request );
			return response.ProductList != null ? response.ProductList.ToList() : new List< ProductType >();
		}

		public async Task< IEnumerable< ProductType > > GetProductsAsync()
		{
			var request = new ReadProductRequestType();
			var response = await this._webRequestServices.GetAsync( this._client.ReadProductAsync, this._credentials, request );
			return response.ReadProductResponse1.ProductList != null ? response.ReadProductResponse1.ProductList.ToList() : new List< ProductType >();
		}

		public NetworkSolutionsInventory UpdateInventory( NetworkSolutionsInventory inventory )
		{
			var request = new UpdateInventoryRequestType
			{
				Inventory = new InventoryType { ProductId = inventory.ProductId, QtyInStock = new ProductQuantityType { Value = inventory.QtyInStock, Adjustment = inventory.Adjustment } }
			};

			var response = this._webRequestServices.Submit( this._client.UpdateInventory, this._credentials, request );
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
				Inventory = new InventoryType { ProductId = inventory.ProductId, QtyInStock = new ProductQuantityType { Value = inventory.QtyInStock, Adjustment = inventory.Adjustment } }
			};

			var response = await this._webRequestServices.SubmitAsync( this._client.UpdateInventoryAsync, this._credentials, request );
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