using System;
using System.Threading.Tasks;
using Netco.ActionPolicyServices;
using Netco.Utils;

namespace NetworkSolutionsAccess.Misc
{
	public static class ActionPolicies
	{
#if DEBUG
		private const int _retryCount = 1;
#else
		private const int _retryCount = 10;
#endif

		public static ActionPolicy Submit
		{
			get { return _volusionSumbitPolicy; }
		}

		private static readonly ActionPolicy _volusionSumbitPolicy = ActionPolicy.Handle< Exception >().Retry( _retryCount, ( ex, i ) =>
		{
			NetworkSolutionsLogger.Log.Trace( ex, "Retrying NetworkSolutions API submit call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync SubmitAsync
		{
			get { return _volusionSumbitAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _volusionSumbitAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( _retryCount, async ( ex, i ) =>
		{
			NetworkSolutionsLogger.Log.Trace( ex, "Retrying NetworkSolutions API submit call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicy Get
		{
			get { return _volusionGetPolicy; }
		}

		private static readonly ActionPolicy _volusionGetPolicy = ActionPolicy.Handle< Exception >().Retry( _retryCount, ( ex, i ) =>
		{
			NetworkSolutionsLogger.Log.Trace( ex, "Retrying NetworkSolutions API get call for the {0} time", i );
			SystemUtil.Sleep( TimeSpan.FromSeconds( 0.5 + i ) );
		} );

		public static ActionPolicyAsync GetAsync
		{
			get { return _volusionGetAsyncPolicy; }
		}

		private static readonly ActionPolicyAsync _volusionGetAsyncPolicy = ActionPolicyAsync.Handle< Exception >().RetryAsync( _retryCount, async ( ex, i ) =>
		{
			NetworkSolutionsLogger.Log.Trace( ex, "Retrying NetworkSolutions API get call for the {0} time", i );
			await Task.Delay( TimeSpan.FromSeconds( 0.5 + i ) );
		} );
	}
}