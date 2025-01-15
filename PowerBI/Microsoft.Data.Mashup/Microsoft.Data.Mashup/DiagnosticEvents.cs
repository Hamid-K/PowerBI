using System;
using System.Collections.Generic;
using Microsoft.Data.Mashup.ProviderCommon;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200001C RID: 28
	internal class DiagnosticEvents
	{
		// Token: 0x0600011E RID: 286 RVA: 0x000064C8 File Offset: 0x000046C8
		public void Emit(ConnectionContext context, string channelName, string eventName, DateTime eventTime, IResource resource, Dictionary<string, object> parameters)
		{
			DiagnosticEventArgs diagnosticEventArgs = new DiagnosticEventArgs(channelName, eventName, eventTime, resource, parameters);
			Dictionary<string, ContextAwareEvent<DiagnosticEventArgs>> handlers = this.Handlers;
			ContextAwareEvent<DiagnosticEventArgs> contextAwareEvent;
			lock (handlers)
			{
				if (!this.Handlers.TryGetValue(channelName, out contextAwareEvent))
				{
					return;
				}
			}
			try
			{
				contextAwareEvent.EventHandler(null, diagnosticEventArgs);
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00006558 File Offset: 0x00004758
		public void AddHandler(string channelName, Action<DiagnosticEventArgs> handler)
		{
			Dictionary<string, ContextAwareEvent<DiagnosticEventArgs>> handlers = this.Handlers;
			lock (handlers)
			{
				ContextAwareEvent<DiagnosticEventArgs> contextAwareEvent;
				if (!this.Handlers.TryGetValue(channelName, out contextAwareEvent))
				{
					contextAwareEvent = new ContextAwareEvent<DiagnosticEventArgs>();
					this.Handlers.Add(channelName, contextAwareEvent);
				}
				contextAwareEvent.AddHandler(delegate(object s, DiagnosticEventArgs e)
				{
					handler(e);
				});
			}
		}

		// Token: 0x040000A5 RID: 165
		public readonly Dictionary<string, ContextAwareEvent<DiagnosticEventArgs>> Handlers = new Dictionary<string, ContextAwareEvent<DiagnosticEventArgs>>();
	}
}
