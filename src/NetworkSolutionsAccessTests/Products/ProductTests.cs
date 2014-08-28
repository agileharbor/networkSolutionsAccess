using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NetworkSolutionsAccess;
using NetworkSolutionsAccess.Models.Configuration;
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
				this.Config = new NetworkSolutionsConfig( testConfig.UserToken );
			}
		}

		[ Test ]
		public void GetProducts()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var products = service.GetProducts();

			products.Should().NotBeNull();
			products.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetProductsAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateProductsService( this.Config );
			var products = await service.GetProductsAsync();

			products.Should().NotBeNull();
			products.Count().Should().BeGreaterThan( 0 );
		}
	}
}