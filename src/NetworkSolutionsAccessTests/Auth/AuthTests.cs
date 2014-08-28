using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using LINQtoCSV;
using NetworkSolutionsAccess;
using NUnit.Framework;

namespace NetworkSolutionsAccessTests.Auth
{
	public class AuthTests
	{
		private INetworkSolutionsFactory NetworkSolutionsFactory;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\NetworkSolutionsCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
				this.NetworkSolutionsFactory = new NetworkSolutionsFactory( testConfig.ApplicationName, testConfig.Certificate );
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
			var orders = service.GetUserToken( "" );

			orders.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetUserTokenAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var orders = await service.GetUserTokenAsync( "" );

			orders.Should().NotBeNull();
		}
	}
}