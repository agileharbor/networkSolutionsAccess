using System.Linq;
using FluentAssertions;
using LINQtoCSV;
using NetworkSolutionsAccess;
using NetworkSolutionsAccess.Models.Configuration;
using NUnit.Framework;

namespace NetworkSolutionsAccessTests.Orders
{
	public class OrderTests
	{
		private readonly INetworkSolutionsFactory NetworkSolutionsFactory = new NetworkSolutionsFactory();
		private NetworkSolutionsConfig Config;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\NetworkSolutionsCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this.Config = new NetworkSolutionsConfig( testConfig.ApplicationName, testConfig.Certificate, testConfig.UserToken );
		}

		[ Test ]
		public void GetOrders()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var orders = service.GetOrders();

			orders.Should().NotBeNull();
			orders.Count().Should().BeGreaterThan( 0 );
		}
	}
}