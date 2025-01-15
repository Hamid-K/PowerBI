using System;
using System.Collections.Specialized;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Web;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.MonitoredUtils;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004CD RID: 1229
	internal class ExternalMonitoredActivityOperationInvoker : IOperationInvoker
	{
		// Token: 0x0600256C RID: 9580 RVA: 0x0008513C File Offset: 0x0008333C
		public ExternalMonitoredActivityOperationInvoker([NotNull] IOperationInvoker operationInvoker, ClientActivityContextSource source, [NotNull] ActivityType activityType, string rootActivityHeaderName, string clientActivityHeaderName, [NotNull] IActivityFactory activityFactory, [NotNull] IMonitoredActivityCompletionModelFactory completionModelFactory)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<ActivityType>(activityType, "activityType");
			ExtendedDiagnostics.EnsureArgumentNotNull<IActivityFactory>(activityFactory, "activityFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IMonitoredActivityCompletionModelFactory>(completionModelFactory, "completionModelFactory");
			ExtendedDiagnostics.EnsureArgumentNotNull<IOperationInvoker>(operationInvoker, "operationInvoker");
			this.m_innerOperationInvoker = operationInvoker;
			this.m_source = source;
			this.m_activityType = activityType;
			this.m_activityFactory = activityFactory;
			this.m_rootActivityParamName = rootActivityHeaderName ?? string.Empty;
			this.m_clientActivityParamName = clientActivityHeaderName ?? string.Empty;
			this.m_completionModelFactory = completionModelFactory;
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000851C4 File Offset: 0x000833C4
		public object[] AllocateInputs()
		{
			return this.m_innerOperationInvoker.AllocateInputs();
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000851D4 File Offset: 0x000833D4
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering Invoke in {0}".FormatWithInvariantCulture(new object[] { typeof(ExternalMonitoredActivityOperationInvoker).Name }));
			Activity activity = this.CreateActivity();
			MonitoredActivityCompletionModel monitoredActivityCompletionModel = this.m_completionModelFactory.CreateMonitoredActivityCompletionModel(this.m_activityType);
			object obj2;
			using (this.m_activityFactory.CreateSyncActivity(activity.ActivityId, activity.ActivityType, activity.RootActivityId, activity.ClientActivityId))
			{
				monitoredActivityCompletionModel.FireActivityStartedEvent(true, null, null);
				try
				{
					object obj = this.m_innerOperationInvoker.Invoke(instance, inputs, out outputs);
					monitoredActivityCompletionModel.FireActivityCompletedSuccessfullyEvent(true, null, null);
					obj2 = obj;
				}
				catch (Exception ex)
				{
					ExternalMonitoredActivityOperationInvoker.HandleEcfException(monitoredActivityCompletionModel, ex);
					throw;
				}
			}
			return obj2;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x000852A0 File Offset: 0x000834A0
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering InvokeBegin in {0}".FormatWithInvariantCulture(new object[] { typeof(ExternalMonitoredActivityOperationInvoker).Name }));
			Activity activity = this.CreateActivity();
			AsyncActivity asyncActivity = this.m_activityFactory.CreateAsyncActivity(activity.ActivityId, activity.ActivityType, activity.RootActivityId, activity.ClientActivityId);
			MonitoredActivityCompletionModel monitoredActivityCompletionModel = this.m_completionModelFactory.CreateMonitoredActivityCompletionModel(this.m_activityType);
			ChainedAsyncResult<WorkTicket, Pair<AsyncActivity, MonitoredActivityCompletionModel>> chainedAsyncResult = new ChainedAsyncResult<WorkTicket, Pair<AsyncActivity, MonitoredActivityCompletionModel>>(callback, state, null, new Pair<AsyncActivity, MonitoredActivityCompletionModel>(asyncActivity, monitoredActivityCompletionModel));
			IAsyncResult asyncResult;
			using (asyncActivity.GetBeginAsyncScope(true))
			{
				monitoredActivityCompletionModel.FireActivityStartedEvent(true, null, null);
				try
				{
					chainedAsyncResult.InnerResult = this.m_innerOperationInvoker.InvokeBegin(instance, inputs, new AsyncCallback(chainedAsyncResult.BeginAsyncFunctionCallback), null);
					asyncResult = chainedAsyncResult;
				}
				catch (Exception ex)
				{
					ExternalMonitoredActivityOperationInvoker.HandleEcfException(monitoredActivityCompletionModel, ex);
					throw;
				}
			}
			return asyncResult;
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00085394 File Offset: 0x00083594
		private static void HandleEcfException(MonitoredActivityCompletionModel completionModel, Exception ex)
		{
			WebFaultException ex2 = ex as WebFaultException;
			if (ex2 != null && ex2.StatusCode == HttpStatusCode.ServiceUnavailable)
			{
				ex = new CommunicationFrameworkWebFaultWrapperException(string.Empty, ex);
			}
			AsyncUtils.HandleActivityException(ex, completionModel);
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x000853CC File Offset: 0x000835CC
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.TraceVerbose("Entering InvokeEnd in {0}".FormatWithInvariantCulture(new object[] { typeof(ExternalMonitoredActivityOperationInvoker).Name }));
			ChainedAsyncResult<WorkTicket, Pair<AsyncActivity, MonitoredActivityCompletionModel>> chainedAsyncResult = (ChainedAsyncResult<WorkTicket, Pair<AsyncActivity, MonitoredActivityCompletionModel>>)result;
			AsyncContextMemberProxy<Activity> first = chainedAsyncResult.Data.First;
			MonitoredActivityCompletionModel second = chainedAsyncResult.Data.Second;
			object obj2;
			using (first.GetEndAsyncScope(true))
			{
				try
				{
					chainedAsyncResult.End();
					object obj = this.m_innerOperationInvoker.InvokeEnd(instance, out outputs, chainedAsyncResult.InnerResult);
					second.FireActivityCompletedSuccessfullyEvent(true, null, null);
					obj2 = obj;
				}
				catch (Exception ex)
				{
					ExternalMonitoredActivityOperationInvoker.HandleEcfException(second, ex);
					throw;
				}
			}
			return obj2;
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06002572 RID: 9586 RVA: 0x00085484 File Offset: 0x00083684
		public bool IsSynchronous
		{
			get
			{
				return this.m_innerOperationInvoker.IsSynchronous;
			}
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00085494 File Offset: 0x00083694
		private Activity CreateActivity()
		{
			string text = string.Empty;
			string text2 = string.Empty;
			if (this.m_source.HasFlag(ClientActivityContextSource.Header) && WebOperationContext.Current != null && WebOperationContext.Current.IncomingRequest != null && WebOperationContext.Current.IncomingRequest.Headers != null)
			{
				WebHeaderCollection headers = WebOperationContext.Current.IncomingRequest.Headers;
				text = headers[this.m_clientActivityParamName] ?? string.Empty;
				text2 = headers[this.m_rootActivityParamName] ?? string.Empty;
			}
			if (string.IsNullOrEmpty(text) && string.IsNullOrEmpty(text2) && this.m_source.HasFlag(ClientActivityContextSource.QueryString))
			{
				if (OperationContext.Current != null && OperationContext.Current.IncomingMessageProperties != null)
				{
					NameValueCollection nameValueCollection = HttpUtility.ParseQueryString(((HttpRequestMessageProperty)OperationContext.Current.IncomingMessageProperties[HttpRequestMessageProperty.Name]).QueryString);
					text = nameValueCollection[this.m_clientActivityParamName] ?? string.Empty;
					text2 = nameValueCollection[this.m_rootActivityParamName] ?? string.Empty;
				}
				if (string.IsNullOrEmpty(text))
				{
					bool flag = !string.IsNullOrEmpty(text2);
				}
			}
			if (!UtilsContext.Current.Activity.Equals(Activity.Empty))
			{
				UtilsContext.Current.ClearStack();
			}
			Guid empty = Guid.Empty;
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "From headers: RootActivityId is '{0}' and ClientActivityId is '{1}'", new object[] { text2, text });
			Activity activity;
			if (!string.IsNullOrEmpty(text2) && Guid.TryParse(text2, out empty) && !empty.Equals(Guid.Empty))
			{
				activity = new Activity(Guid.NewGuid(), this.m_activityType, empty, text);
			}
			else
			{
				Guid guid = Guid.NewGuid();
				activity = new Activity(guid, this.m_activityType, guid, text);
			}
			return activity;
		}

		// Token: 0x04000D37 RID: 3383
		private IMonitoredActivityCompletionModelFactory m_completionModelFactory;

		// Token: 0x04000D38 RID: 3384
		private IOperationInvoker m_innerOperationInvoker;

		// Token: 0x04000D39 RID: 3385
		private IActivityFactory m_activityFactory;

		// Token: 0x04000D3A RID: 3386
		private string m_clientActivityParamName;

		// Token: 0x04000D3B RID: 3387
		private string m_rootActivityParamName;

		// Token: 0x04000D3C RID: 3388
		private ActivityType m_activityType;

		// Token: 0x04000D3D RID: 3389
		private ClientActivityContextSource m_source;
	}
}
