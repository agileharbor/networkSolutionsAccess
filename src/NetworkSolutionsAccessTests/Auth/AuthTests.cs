using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NetworkSolutionsAccess;
using NetworkSolutionsAccess.Models.Configuration;
using NUnit.Framework;

namespace NetworkSolutionsAccessTests.Auth
{
	public class AuthTests
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
		public void GetUserKey()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var orders = service.GetUserKey();

			orders.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetUserKeyAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var orders = await service.GetUserKeyAsync();

			orders.Should().NotBeNull();
		}

		[ Test ]
		public void GetUserToken()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var orders = service.GetUserToken( "t3T7Pwc2FWd5s6S8" );

			orders.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetUserTokenAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var orders = await service.GetUserTokenAsync( "t3T7Pwc2FWd5s6S8" );

			orders.Should().NotBeNull();
		}
	}
}