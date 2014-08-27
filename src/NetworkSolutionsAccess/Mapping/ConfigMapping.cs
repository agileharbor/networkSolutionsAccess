using NetworkSolutionsAccess.Models.Configuration;
using NetworkSolutionsAccess.NetworkSolutionsService;

namespace NetworkSolutionsAccess.Mapping
{
	internal static class ConfigMapping
	{
		public static SecurityCredentialType ToSecurityCredentialType( this NetworkSolutionsConfig config )
		{
			return new SecurityCredentialType
			{
				Application = config.ApplicationName,
				Certificate = config.Certificate,
				UserToken = config.UserToken
			};
		}
	}
}