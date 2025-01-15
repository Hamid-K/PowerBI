using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000006 RID: 6
	internal class DiagnosticsService : IDiagnosticsService
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000025A6 File Offset: 0x000007A6
		public DiagnosticsService(ConnectionContext connectionContext)
		{
			this.connectionContext = connectionContext;
			this.subscribedChannels = new HashSet<string>(connectionContext.SubscribedChannels);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000025C6 File Offset: 0x000007C6
		public HashSet<string> EnabledChannels
		{
			get
			{
				return this.subscribedChannels;
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025CE File Offset: 0x000007CE
		public void Emit(string channelName, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> properties)
		{
			this.connectionContext.DiagnosticEvent(channelName, eventName, eventTime, resource, properties);
		}

		// Token: 0x0400000A RID: 10
		private readonly ConnectionContext connectionContext;

		// Token: 0x0400000B RID: 11
		private readonly HashSet<string> subscribedChannels;
	}
}
