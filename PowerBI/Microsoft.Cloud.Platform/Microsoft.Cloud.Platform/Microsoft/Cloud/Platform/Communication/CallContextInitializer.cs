using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004B9 RID: 1209
	public class CallContextInitializer : ICallContextInitializer
	{
		// Token: 0x0600250C RID: 9484 RVA: 0x00083D2B File Offset: 0x00081F2B
		internal CallContextInitializer(IActivityFactory activityFactory)
		{
			this.m_activityFactory = activityFactory;
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x00083D3C File Offset: 0x00081F3C
		public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
		{
			UniqueId messageId = OperationContext.Current.IncomingMessageHeaders.MessageId;
			MessageHeaders headers = message.Headers;
			Guid guid;
			TraceSourceBase<CommunicationFrameworkTrace>.Tracer.Trace(TraceVerbosity.Verbose, "Server received Message with ID '{0}' on operation '{1}'", new object[]
			{
				(messageId != null && messageId.TryGetGuid(out guid)) ? guid.ToString() : "empty",
				(headers.Action == null) ? "empty" : headers.Action
			});
			SyncActivity syncActivity = null;
			IDisposable disposable = null;
			int num = headers.FindHeader(AddContextToHeaderEndpointBehavior.FORCE_TRACES_HEADER_NAME, AddContextToHeaderEndpointBehavior.FORCE_TRACES_HEADER_NAMESPACE);
			bool flag = num >= 0 && headers.GetHeader<bool>(num);
			num = headers.FindHeader(AddContextToHeaderEndpointBehavior.ACTIVITY_HEADER_NAME, AddContextToHeaderEndpointBehavior.ACTIVITY_HEADER_NAMESPACE);
			if (num >= 0)
			{
				SerializedActivity header = headers.GetHeader<SerializedActivity>(num);
				if (!UtilsContext.Current.Activity.Equals(Activity.Empty))
				{
					UtilsContext.Current.ClearStack();
				}
				Activity activity = header.ToActivity();
				if (activity.Equals(Activity.Empty))
				{
					syncActivity = null;
				}
				else
				{
					syncActivity = new SyncActivity(activity.ActivityId, activity.ActivityType, activity.RootActivityId, activity.ClientActivityId);
				}
				if (flag)
				{
					disposable = UtilsContext.Current.CreateEnforcedTracingProxy().GetBeginAsyncScope(true);
				}
				return new CallContextInitializer.CorrelationState(syncActivity, disposable);
			}
			if (flag)
			{
				disposable = UtilsContext.Current.CreateEnforcedTracingProxy().GetBeginAsyncScope(true);
			}
			return new CallContextInitializer.CorrelationState(syncActivity, disposable);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x00083E9C File Offset: 0x0008209C
		public void AfterInvoke(object correlationState)
		{
			if (correlationState != null)
			{
				CallContextInitializer.CorrelationState correlationState2 = correlationState as CallContextInitializer.CorrelationState;
				if (correlationState2.TracingForced != null)
				{
					correlationState2.TracingForced.Dispose();
				}
				if (correlationState2.Activity != null)
				{
					correlationState2.Activity.Dispose();
				}
			}
		}

		// Token: 0x04000D12 RID: 3346
		private IActivityFactory m_activityFactory;

		// Token: 0x02000837 RID: 2103
		private class CorrelationState
		{
			// Token: 0x060032E7 RID: 13031 RVA: 0x000AA9DB File Offset: 0x000A8BDB
			public CorrelationState(SyncActivity activity, IDisposable tracingForced)
			{
				this.Activity = activity;
				this.TracingForced = tracingForced;
			}

			// Token: 0x17000781 RID: 1921
			// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000AA9F1 File Offset: 0x000A8BF1
			// (set) Token: 0x060032E9 RID: 13033 RVA: 0x000AA9F9 File Offset: 0x000A8BF9
			public SyncActivity Activity { get; private set; }

			// Token: 0x17000782 RID: 1922
			// (get) Token: 0x060032EA RID: 13034 RVA: 0x000AAA02 File Offset: 0x000A8C02
			// (set) Token: 0x060032EB RID: 13035 RVA: 0x000AAA0A File Offset: 0x000A8C0A
			public IDisposable TracingForced { get; set; }
		}
	}
}
