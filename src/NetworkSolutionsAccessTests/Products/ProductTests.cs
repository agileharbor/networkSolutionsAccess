using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NetworkSolutionsAccess;
using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.Models.Product;
using NUnit.Framework;

namespace NetworkSolutionsAccessTests.Products
{
	public class ProductTests
	{
		private INetworkSolutionsFactory NetworkSolutionsFactory;
		private NetworkSolutionsConfig Config;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\NetworkSolutionsCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.NetworkSolutionsFactory = new NetworkSolutionsFactory( testConfig.ApplicationName, testConfig.Certificate );
				this.Config = new NetworkSolutionsConfig( testConfig.UserKey );
			}
		}

		[ Test ]
		public void GetProducts()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var result = service.GetProducts().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetProductsAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var result = ( await service.GetProductsAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void UpdateInventory()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var inventory = new NetworkSolutionsInventory
			{
				ProductId = 1,
				QtyInStock = 10,
				Adjustment = false
			};
			var result = service.UpdateInventory( inventory );

			result.Should().NotBeNull();
		}

		[ Test ]
		public async Task UpdateInventoryAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var inventory = new NetworkSolutionsInventory
			{
				ProductId = 1,
				QtyInStock = 10,
				Adjustment = false
			};
			var result = await service.UpdateInventoryAsync( inventory );

			result.Should().NotBeNull();
		}

		[ Test ]
		public void UpdateInventoryList()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var inventory = new List< NetworkSolutionsInventory >
			{
				new NetworkSolutionsInventory
				{
					ProductId = 1,
					QtyInStock = 10,
					Adjustment = false
				}
			};
			var result = service.UpdateInventory( inventory ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task UpdateInventoryAsyncList()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var inventory = new List< NetworkSolutionsInventory >
			{
				new NetworkSolutionsInventory
				{
					ProductId = 1,
					QtyInStock = 10,
					Adjustment = false
				}
			};
			var result = ( await service.UpdateInventoryAsync( inventory ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}
	}
}