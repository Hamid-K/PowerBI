using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200016D RID: 365
	public abstract class ActivitySequencer : Sequencer
	{
		// Token: 0x06000982 RID: 2434 RVA: 0x000209B6 File Offset: 0x0001EBB6
		protected ActivitySequencer([NotNull] AsyncActivity asyncActivity)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<AsyncActivity>(asyncActivity, "asyncActivity");
			this.m_asyncActivity = asyncActivity;
			this.m_activityEndedWithErrorCalled = 0;
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x000209D8 File Offset: 0x0001EBD8
		public override IAsyncResult BeginExecute(AsyncCallback userCallback, object userContext)
		{
			IAsyncResult asyncResult;
			using (this.m_asyncActivity.GetBeginAsyncScope(this.FireVerboseEvents()))
			{
				this.CallOnActivityStarted();
				asyncResult = base.BeginExecute(userCallback, userContext);
			}
			return asyncResult;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00020A24 File Offset: 0x0001EC24
		public override void EndExecute(IAsyncResult asyncResult)
		{
			using (this.m_asyncActivity.GetEndAsyncScope(this.FireVerboseEvents()))
			{
				Exception exception = null;
				ExceptionFilters.TryFilterCatchFaultFinally(delegate
				{
					this.<>n__0(asyncResult);
				}, delegate(Exception ex)
				{
					ExtendedEnvironment.ApplyFailSlowOnFatalPolicy(this, ex);
					exception = ex;
					return ExceptionDisposition.ContinueSearch;
				}, null, delegate
				{
					this.CallOnActivityEndedWithError(exception);
				}, null);
				this.CallOnActivityEndedNormally();
			}
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00020AC0 File Offset: 0x0001ECC0
		protected override void Advance()
		{
			base.Advance();
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x000034FD File Offset: 0x000016FD
		protected virtual bool FireVerboseEvents()
		{
			return true;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000987 RID: 2439 RVA: 0x00020AC8 File Offset: 0x0001ECC8
		// (remove) Token: 0x06000988 RID: 2440 RVA: 0x00020B00 File Offset: 0x0001ED00
		protected event ActivitySequencer.ActivityStartedHandler OnActivityStarted;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000989 RID: 2441 RVA: 0x00020B38 File Offset: 0x0001ED38
		// (remove) Token: 0x0600098A RID: 2442 RVA: 0x00020B70 File Offset: 0x0001ED70
		protected event ActivitySequencer.ActivityEndedNormallyHandler OnActivityEndedNormally;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600098B RID: 2443 RVA: 0x00020BA8 File Offset: 0x0001EDA8
		// (remove) Token: 0x0600098C RID: 2444 RVA: 0x00020BE0 File Offset: 0x0001EDE0
		protected event ActivitySequencer.ActivityEndedWithErrorHandler OnActivityEndedWithError;

		// Token: 0x0600098D RID: 2445 RVA: 0x00020C18 File Offset: 0x0001EE18
		private void CallOnActivityStarted()
		{
			ActivitySequencer.ActivityStartedHandler onActivityStarted = this.OnActivityStarted;
			if (onActivityStarted != null)
			{
				onActivityStarted();
			}
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00020C38 File Offset: 0x0001EE38
		private void CallOnActivityEndedWithError(Exception ex)
		{
			IMonitoredError me = ex as IMonitoredError;
			if (me == null)
			{
				me = new ActivitySequencerFailureException(this, null, ex);
			}
			ActivitySequencer.ActivityEndedWithErrorHandler temp = this.OnActivityEndedWithError;
			if (temp != null)
			{
				TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNothing, delegate
				{
					temp(me);
				});
			}
		}

		// Token: 0x0600098F RID: 2447 RVA: 0x00020C98 File Offset: 0x0001EE98
		private void CallOnActivityEndedNormally()
		{
			ActivitySequencer.ActivityEndedNormallyHandler onActivityEndedNormally = this.OnActivityEndedNormally;
			if (onActivityEndedNormally != null)
			{
				onActivityEndedNormally();
			}
		}

		// Token: 0x0400039E RID: 926
		private AsyncActivity m_asyncActivity;

		// Token: 0x0400039F RID: 927
		private int m_activityEndedWithErrorCalled;

		// Token: 0x02000636 RID: 1590
		// (Invoke) Token: 0x06002CCC RID: 11468
		protected delegate void ActivityStartedHandler();

		// Token: 0x02000637 RID: 1591
		// (Invoke) Token: 0x06002CD0 RID: 11472
		protected delegate void ActivityEndedNormallyHandler();

		// Token: 0x02000638 RID: 1592
		// (Invoke) Token: 0x06002CD4 RID: 11476
		protected delegate void ActivityEndedWithErrorHandler(IMonitoredError error);
	}
}
