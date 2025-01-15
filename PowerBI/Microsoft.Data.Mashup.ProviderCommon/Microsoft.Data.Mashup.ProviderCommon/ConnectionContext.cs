using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.EngineHost.Services;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Data.Mashup.ProviderCommon
{
	// Token: 0x02000003 RID: 3
	internal abstract class ConnectionContext : IDisposable
	{
		// Token: 0x06000003 RID: 3 RVA: 0x000020AB File Offset: 0x000002AB
		protected ConnectionContext(string session, SafeHandle threadIdentity)
		{
			this.session = (string.IsNullOrEmpty(session) ? null : session);
			this.threadIdentity = threadIdentity;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000020CC File Offset: 0x000002CC
		public virtual bool IsDataSourceSettingUpdatable
		{
			get
			{
				return !string.IsNullOrEmpty(this.session);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020DC File Offset: 0x000002DC
		public bool IsRequestTracingEnabled
		{
			get
			{
				return !string.IsNullOrEmpty(this.session) && this.TracedResources.Any<IResource>();
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000006 RID: 6
		public abstract MashupConnectionPool2 ConnectionPool { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020F8 File Offset: 0x000002F8
		public string Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002100 File Offset: 0x00000300
		public SafeHandle ThreadIdentity
		{
			get
			{
				return this.threadIdentity;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x00002108 File Offset: 0x00000308
		public IEnumerable<IResource> TracedResources
		{
			get
			{
				return this.GetEvaluationHandlers().SelectMany((IEvaluationEventHandler h) => h.TracedResources);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002134 File Offset: 0x00000334
		public IEnumerable<string> SubscribedChannels
		{
			get
			{
				return this.GetEvaluationHandlers().SelectMany((IEvaluationEventHandler h) => h.SubscribedChannels);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002160 File Offset: 0x00000360
		public ResourceCredentialCollection GetCredentials(IResource resource)
		{
			foreach (IEvaluationEventHandler evaluationEventHandler in this.GetEvaluationHandlers())
			{
				ResourceCredentialCollection credentials = evaluationEventHandler.GetCredentials(this, resource);
				if (credentials != null)
				{
					return credentials;
				}
			}
			return null;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021B8 File Offset: 0x000003B8
		public ResourceCredentialCollection RefreshCredential(ResourceCredentialCollection oldCredential)
		{
			foreach (IEvaluationEventHandler evaluationEventHandler in this.GetEvaluationHandlers())
			{
				ResourceCredentialCollection resourceCredentialCollection = evaluationEventHandler.RefreshCredential(this, oldCredential);
				if (resourceCredentialCollection != null)
				{
					return resourceCredentialCollection;
				}
			}
			return oldCredential;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002210 File Offset: 0x00000410
		public void TraceRequest(RequestTraceData traceData)
		{
			foreach (IEvaluationEventHandler evaluationEventHandler in this.GetEvaluationHandlers())
			{
				evaluationEventHandler.TraceRequest(traceData);
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x0000225C File Offset: 0x0000045C
		public bool IsQueryExecutionPermitted(IResource resource, QueryPermissionChallengeType type, string query, int parameterCount, string[] parameterNames)
		{
			using (IEnumerator<IEvaluationEventHandler> enumerator = this.GetEvaluationHandlers().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.IsQueryExecutionPermitted(this, resource, type, query, parameterCount, parameterNames))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000022B8 File Offset: 0x000004B8
		public bool TryUpdateFirewallGroup(IResource resource, FirewallGroup2 originalGroup, IValue traits, out FirewallGroup2 newGroup)
		{
			using (IEnumerator<IEvaluationEventHandler> enumerator = this.GetEvaluationHandlers().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryUpdateFirewallGroup(this, resource, originalGroup, traits, out newGroup))
					{
						return true;
					}
				}
			}
			newGroup = null;
			return false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002318 File Offset: 0x00000518
		public void DiagnosticEvent(string channelName, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> parameters)
		{
			foreach (IEvaluationEventHandler evaluationEventHandler in this.GetEvaluationHandlers())
			{
				evaluationEventHandler.DiagnosticEvent(this, channelName, eventName, eventTime, resource, parameters);
			}
		}

		// Token: 0x06000011 RID: 17
		public abstract MashupResourceProperties CreateMashupResourceProperties();

		// Token: 0x06000012 RID: 18
		public abstract void Dispose();

		// Token: 0x06000013 RID: 19 RVA: 0x0000236C File Offset: 0x0000056C
		protected virtual IEnumerable<IEvaluationEventHandler> GetEvaluationHandlers()
		{
			return SessionHandle.GetEvaluationHandlers(this) ?? EmptyArray<IEvaluationEventHandler>.Instance;
		}

		// Token: 0x04000001 RID: 1
		private readonly string session;

		// Token: 0x04000002 RID: 2
		private readonly SafeHandle threadIdentity;
	}
}
