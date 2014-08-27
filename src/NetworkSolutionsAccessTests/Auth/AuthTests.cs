using System.Linq;
using FluentAssertions;
using LINQtoCSV;
using NetworkSolutionsAccess;
using NetworkSolutionsAccess.Models.Configuration;
using NUnit.Framework;

namespace NetworkSolutionsAccessTests.Auth
{
	public class AuthTests
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
		public void GetUserKey()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService( this.Config );
			var orders = service.GetUserKey();

			orders.Should().NotBeNull();
		}

		[ Test ]
		public void GetUserToken()
		{
			var service = this.NetworkSolutionsFactory.CreateAuthService( this.Config );
			var orders = service.GetUserToken();

			orders.Should().NotBeNull();
		}
	}
}