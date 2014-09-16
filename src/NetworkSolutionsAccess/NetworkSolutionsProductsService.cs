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
		private readonly SecurityCredentialType _credentials;
		private readonly NetSolEcomServiceSoapClient _client;
		private readonly WebRequestServices _webRequestServices;

		public NetworkSolutionsProductsService( NetworkSolutionsAppConfig appConfig, NetworkSolutionsConfig config )
		{
			Condition.Requires( appConfig, "appConfig" ).IsNotNull();
			Condition.Requires( config, "config" ).IsNotNull();

			this._credentials = new SecurityCredentialType { Application = appConfig.ApplicationName, Certificate = appConfig.Certificate, UserToken = config.UserToken };
			this._client = new NetSolEcomServiceSoapClient();
			this._webRequestServices = new WebRequestServices();
		}

		public IEnumerable< ProductType > GetProducts()
		{
			var filter = this.GetProductsFilter();
			var result = new List< ProductType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadProductRequestType { PageRequest = new PaginationType { Page = i }, FilterList = filter };
				var response = this._webRequestServices.GetPage( this._client.ReadProduct, this._credentials, request );
				if( response.ProductList != null )
					result.AddRange( response.ProductList );
				if( !response.PageResponse.HasMore )
					break;
			}

			return result;
		}

		public async Task< IEnumerable< ProductType > > GetProductsAsync()
		{
			var filter = this.GetProductsFilter();
			var result = new List< ProductType >();
			for( var i = 1; i < 99999; i++ )
			{
				var request = new ReadProductRequestType { PageRequest = new PaginationType { Page = i }, FilterList = filter };
				var response = await this._webRequestServices.GetPageAsync( this._client.ReadProductAsync, this._credentials, request );
				if( response.ReadProductResponse1.ProductList != null )
					result.AddRange( response.ReadProductResponse1.ProductList );
				if( !response.ReadProductResponse1.PageResponse.HasMore )
					break;
			}

			return result;
		}

		public NetworkSolutionsInventory UpdateInventory( NetworkSolutionsInventory inventory )
		{
			var request = this.GetUpdateInventoryRequest( inventory );
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
			var request = this.GetUpdateInventoryRequest( inventory );
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

		private FilterType[] GetProductsFilter()
		{
			return new[]
			{
				new FilterType
				{
					Field = "PartNumber",
					Operator = OperatorCodeType.NotEqual,
					OperatorSpecified = true,
					ValueList = new[] { string.Empty }
				}
			};
		}

		private UpdateInventoryRequestType GetUpdateInventoryRequest( NetworkSolutionsInventory inventory )
		{
			return new UpdateInventoryRequestType
			{
				Inventory = new InventoryType
				{
					ProductId = inventory.ProductId,
					ProductIdSpecified = true,
					QtyInStock = new ProductQuantityType
					{
						Value = inventory.QtyInStock,
						Adjustment = inventory.Adjustment,
						AdjustmentSpecified = true
					}
				}
			};
		}
	}
}