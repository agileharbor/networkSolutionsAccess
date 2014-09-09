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
		private string UserKey;

		[ SetUp ]
		public void Init()
		{
			const string credentialsFilePath = @"..\..\Files\NetworkSolutionsCredentials.csv";

			var cc = new CsvContext();
			var testConfig = cc.Read< TestConfig >( credentialsFilePath, new CsvFileDescription { FirstLineHasColumnNames = true } ).FirstOrDefault();

			if( testConfig != null )
			{
				this.UserKey = testConfig.UserKey;
				this.NetworkSolutionsFactory = new NetworkSolutionsFactory( testConfig.ApplicationName, testConfig.Certificate );
			}
		}

		[ Test ]
		public void GetUserKey()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var result = service.GetUserKey();

			result.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetUserKeyAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var result = await service.GetUserKeyAsync();

			result.Should().NotBeNull();
		}

		[ Test ]
		public void GetUserToken()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var result = service.GetUserToken( this.UserKey );

			result.Should().NotBeNull();
		}

		[ Test ]
		public async Task GetUserTokenAsync()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService();
			var result = await service.GetUserTokenAsync( this.UserKey );

			result.Should().NotBeNull();
		}
	}
}