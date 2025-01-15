using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Web;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x020004CA RID: 1226
	public class ExternalCallContextInitializer : ICallContextInitializer
	{
		// Token: 0x0600255F RID: 9567 RVA: 0x00084CE8 File Offset: 0x00082EE8
		internal ExternalCallContextInitializer(ActivityType activityType, string rootActivityHeaderName, string clientActivityHeaderName, ClientActivityContextSource source, IActivityFactory activityFactory)
		{
			ExtendedDiagnostics.EnsureOperation(source.HasFlag(ClientActivityContextSource.CreateNew), "This behavior should not be added if CreateNew is not turned on");
			this.m_activityCreator = new ExternalRequestActivityCreator(activityType, rootActivityHeaderName, clientActivityHeaderName, source, activityFactory);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00084D20 File Offset: 0x00082F20
		public object BeforeInvoke(InstanceContext instanceContext, IClientChannel channel, Message message)
		{
			ExternalSyncActivity externalSyncActivity = null;
			if (message.Properties.ContainsKey(HttpRequestMessageProperty.Name))
			{
				HttpRequestMessageProperty httpRequestMessageProperty = (HttpRequestMessageProperty)message.Properties[HttpRequestMessageProperty.Name];
				externalSyncActivity = this.m_activityCreator.CreateActivity(httpRequestMessageProperty.Headers, HttpUtility.ParseQueryString(httpRequestMessageProperty.QueryString));
			}
			return externalSyncActivity;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00084D78 File Offset: 0x00082F78
		public void AfterInvoke(object activity)
		{
			ExternalSyncActivity externalSyncActivity = activity as ExternalSyncActivity;
			if (externalSyncActivity != null)
			{
				externalSyncActivity.Dispose();
			}
		}

		// Token: 0x04000D29 RID: 3369
		private readonly ExternalRequestActivityCreator m_activityCreator;
	}
}
