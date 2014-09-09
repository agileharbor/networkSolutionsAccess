using System.Collections.Generic;
using System.Threading.Tasks;
using CuttingEdge.Conditions;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.Models.Product;
using NetworkSolutionsAccess.NetworkSolutionsService;
using NetworkSolutionsAccess.Services;

namespace NetworkSolutionsAccess
{
	public class NetworkSolutionsProductsService: INetworkSolutionsProductsService
	{
		protected readonly SecurityCredentialType Credentials;
		protected readonly NetSolEcomServiceSoapClient Client;
		internal readonly WebRequestServices WebRequestServices;

		public NetworkSolutionsProductsService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();
			Condition.Requires( config, "config" ).IsNotNull();

			this.Credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate, UserToken = config.UserToken };
			this.Client = new NetSolEcomServiceSoapClient();
			this.WebRequestServices = new WebRequestServices();
		}

		public IEnumerable< ProductType > GetProducts()
		{
			var result = new List< ProductType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadProductRequestType { PageRequest = new PaginationType { Page = i } };
				var response = this.WebRequestServices.GetPage( this.Client.ReadProduct, this.Credentials, request );
				if( response.ProductList != null )
					result.AddRange( response.ProductList );
				if( !response.PageResponse.HasMore )
					break;
			}

			return result;
		}

		public async Task< IEnumerable< ProductType > > GetProductsAsync()
		{
			var result = new List< ProductType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadProductRequestType { PageRequest = new PaginationType { Page = i } };
				var response = await this.WebRequestServices.GetPageAsync( this.Client.ReadProductAsync, this.Credentials, request );
				if( response.ReadProductResponse1.ProductList != null )
					result.AddRange( response.ReadProductResponse1.ProductList );
				if( !response.ReadProductResponse1.PageResponse.HasMore )
					break;
			}

			return result;
		}

		public NetworkSolutionsInventory UpdateInventory( NetworkSolutionsInventory inventory )
		{
			var request = new UpdateInventoryRequestType
			{
				Inventory = new InventoryType { ProductId = inventory.ProductId, QtyInStock = new ProductQuantityType { Value = inventory.QtyInStock, Adjustment = inventory.Adjustment } }
			};

			var response = this.WebRequestServices.Submit( this.Client.UpdateInventory, this.Credentials, request );
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

			var response = await this.WebRequestServices.SubmitAsync( this.Client.UpdateInventoryAsync, this.Credentials, request );
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