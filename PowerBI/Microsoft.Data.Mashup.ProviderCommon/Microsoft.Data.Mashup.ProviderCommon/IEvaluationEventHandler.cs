using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x0200000E RID: 14
	internal interface IEvaluationEventHandler
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000045 RID: 69
		IEnumerable<IResource> TracedResources { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000046 RID: 70
		IEnumerable<string> SubscribedChannels { get; }

		// Token: 0x06000047 RID: 71
		ResourceCredentialCollection GetCredentials(ConnectionContext context, IResource resource);

		// Token: 0x06000048 RID: 72
		ResourceCredentialCollection RefreshCredential(ConnectionContext context, ResourceCredentialCollection oldCredential);

		// Token: 0x06000049 RID: 73
		void TraceRequest(RequestTraceData traceData);

		// Token: 0x0600004A RID: 74
		bool IsQueryExecutionPermitted(ConnectionContext context, IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames);

		// Token: 0x0600004B RID: 75
		bool TryUpdateFirewallGroup(ConnectionContext context, IResource resource, FirewallGroup2 originalGroup, IValue traits, out FirewallGroup2 newGroup);

		// Token: 0x0600004C RID: 76
		void DiagnosticEvent(ConnectionContext context, string channelName, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> parameters);
	}
}
