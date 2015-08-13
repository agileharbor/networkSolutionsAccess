using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NetworkSolutionsAccess;
using NetworkSolutionsAccess.Models.Configuration;
using NUnit.Framework;

namespace NetworkSolutionsAccessTests.Orders
{
	public class OrderTests
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
		public void GetOrders()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var result = service.GetOrders().ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetOrdersAsync() ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void GetOrdersByDateRange()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var result = service.GetOrders( DateTime.UtcNow.AddHours( -2 ), DateTime.UtcNow ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsyncByDateRange()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var result = ( await service.GetOrdersAsync( new DateTime( 2014, 9, 16, 12, 7, 0 ), DateTime.UtcNow ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public void IsOrdersReceived()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var result = service.IsOrdersReceived();

			result.Should().BeTrue();
		}

		[ Test ]
		public async Task IsOrdersReceivedAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var result = await service.IsOrdersReceivedAsync();

			result.Should().BeTrue();
		}

		[ Test ]
		public void GetOrdersByIds()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var ids = new List< string > { "1", "2", "10" };
			var result = service.GetOrdersExceptReceived( ids ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}

		[ Test ]
		public async Task GetOrdersAsyncByIds()
		{
			var service = this.NetworkSolutionsFactory.CreateOrdersService( this.Config );
			var ids = new List< string > { "1", "2", "10" };
			var result = ( await service.GetOrdersExceptReceivedAsync( ids ) ).ToList();

			result.Should().NotBeNull();
			result.Count().Should().BeGreaterThan( 0 );
		}
	}
}