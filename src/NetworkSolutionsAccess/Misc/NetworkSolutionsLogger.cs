using Netco.Logging;
using NetworkSolutionsAccess.NetworkSolutionsService;
using ServiceStack;

namespace NetworkSolutionsAccess.Misc
{
	public static class NetworkSolutionsLogger
	{
		public static ILogger Log{ get; private set; }

		static NetworkSolutionsLogger()
		{
			Log = NetcoLogger.GetLogger( "NetworkSolutionsLogger" );
		}

		//public static string logstr = "";

		public static void TraceRequest( string methodName, SecurityCredentialType credentials, object obj )
		{
			//logstr += string.Format( "{0} request for {1}. Data: {2}", methodName, credentials.Application, obj.ToJson() );
			Log.Trace( "{0} - request for {1}. Data: {2}", methodName, credentials.Application, obj.ToJson() );
		}

		public static void TraceResponse( string methodName, SecurityCredentialType credentials, object obj )
		{
			//logstr += string.Format( "{0} response for {1}. Data: {2}", methodName, credentials.Application, obj.ToJson() );
			Log.Trace( "{0} - response for {1}. Data: {2}", methodName, credentials.Application, obj.ToJson() );
		}
	}
}