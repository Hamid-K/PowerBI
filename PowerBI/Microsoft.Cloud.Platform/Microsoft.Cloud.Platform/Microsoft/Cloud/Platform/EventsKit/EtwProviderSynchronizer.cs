using System;
using System.Linq;
using System.Threading;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Eventing.Etw;
using Microsoft.Cloud.Platform.Utils;
using Microsoft.Diagnostics.Tracing;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000333 RID: 819
	public static class EtwProviderSynchronizer
	{
		// Token: 0x06001821 RID: 6177 RVA: 0x00058697 File Offset: 0x00056897
		public static void EnableEventSource(EventSource eventSource, EventLevel level, EtwSession newEventsSession, IEtwSessionsManagerEventsKit eventsKit)
		{
			EtwProviderSynchronizer.EnableEventSource(eventSource, level, null, newEventsSession, eventsKit);
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x000586A4 File Offset: 0x000568A4
		public static void EnableEventSource(EventSource eventSource, EventLevel level, EtwSession manifestsSession, EtwSession newEventsSession, IEtwSessionsManagerEventsKit eventsKit)
		{
			TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Enabling Event Source {0}:{1}", new object[] { eventSource.Name, eventSource.Guid });
			if (EtwProviderSynchronizer.IsEventSourceEnabled(eventSource, newEventsSession.Handle))
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Event Source {0}:{1} is already enabled on Events Session, skipping manifest session", new object[] { eventSource.Name, eventSource.Guid });
				return;
			}
			if (manifestsSession != null)
			{
				EtwProviderSynchronizer.TryEnableEventSourceOnSession(manifestsSession, eventSource, level, eventsKit);
				EtwProviderSynchronizer.TryDisableEventSourceOnSession(newEventsSession, manifestsSession, eventSource, eventsKit);
			}
			EtwProviderSynchronizer.TryEnableEventSourceOnSession(newEventsSession, eventSource, level, eventsKit);
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x0005873C File Offset: 0x0005693C
		private static bool TryWaitForEventSourceOperation(string operationName, string sessionName, Func<bool> shouldContinuePolling, EventSource eventSource, int millisecondsTimeout, IEtwSessionsManagerEventsKit eventsKit)
		{
			bool timeout = false;
			bool flag = false;
			using (WatchdogTimer.Start(millisecondsTimeout, delegate
			{
				timeout = true;
			}))
			{
				while (shouldContinuePolling() && !timeout && !flag)
				{
					if (eventSource.ConstructionException != null)
					{
						EtwProviderSynchronizerException ex = new EtwProviderSynchronizerException(operationName, eventSource.Name, eventSource.Guid, sessionName, string.Empty, eventSource.ConstructionException);
						eventsKit.NotifyEventSourceException(operationName, eventSource.Name, eventSource.Guid, sessionName, ex);
						flag = true;
						break;
					}
					Thread.Sleep(50);
				}
			}
			if (timeout)
			{
				EtwProviderSynchronizerException ex2 = new EtwProviderSynchronizerException(operationName, eventSource.Name, eventSource.Guid, sessionName);
				eventsKit.NotifyEventSourceOperationTimeout(operationName, eventSource.Name, eventSource.Guid, sessionName, ex2);
			}
			return !timeout && !flag;
		}

		// Token: 0x06001824 RID: 6180 RVA: 0x00058824 File Offset: 0x00056A24
		private static bool TryDisableEventSourceOnSession(EtwSession newSession, EtwSession sessionToDisableFrom, EventSource eventSource, IEtwSessionsManagerEventsKit eventsKit)
		{
			bool flag = false;
			new EtwProvider(eventSource.Guid, null).Disable(sessionToDisableFrom);
			if (!string.IsNullOrEmpty(sessionToDisableFrom.Properties.Name))
			{
				flag = EtwProviderSynchronizer.TryWaitForEventSourceOperation("Disable on ETW session '{0}'".FormatWithInvariantCulture(new object[] { sessionToDisableFrom.Properties.Name }), newSession.Properties.Name, delegate
				{
					if (EtwProviderSynchronizer.IsEventSourceEnabled(eventSource, newSession.Handle))
					{
						TraceSourceBase<EventingTrace>.Tracer.TraceWarning("Event Source {0}:{1} is already enabled on new session '{2}', skipping '{3}'", new object[]
						{
							eventSource.Name,
							eventSource.Guid,
							newSession.Properties.Name,
							sessionToDisableFrom.Properties.Name
						});
						return false;
					}
					return EtwProviderSynchronizer.IsEventSourceEnabled(eventSource, sessionToDisableFrom.Handle) || eventSource.IsEnabled();
				}, eventSource, 120000, eventsKit);
			}
			if (flag)
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Event Source {0}:{1} successfully disabled on ETW session: '{2}'", new object[]
				{
					eventSource.Name,
					eventSource.Guid,
					sessionToDisableFrom.Properties.Name
				});
			}
			return flag;
		}

		// Token: 0x06001825 RID: 6181 RVA: 0x00058920 File Offset: 0x00056B20
		private static bool TryEnableEventSourceOnSession(EtwSession session, EventSource eventSource, EventLevel level, IEtwSessionsManagerEventsKit eventsKit)
		{
			bool flag = false;
			new EtwProvider(eventSource.Guid, null).Enable(session, level.ToEtwTraceLevel(), 0);
			if (!string.IsNullOrEmpty(session.Properties.Name))
			{
				flag = EtwProviderSynchronizer.TryWaitForEventSourceOperation("Enable on ETW session '{0}'".FormatWithInvariantCulture(new object[] { session.Properties.Name }), session.Properties.Name, () => !EtwProviderSynchronizer.IsEventSourceEnabled(eventSource, session.Handle), eventSource, 120000, eventsKit);
			}
			if (flag)
			{
				TraceSourceBase<EventingTrace>.Tracer.TraceInformation("Event Source {0}:{1} successfully enabled on ETW session: '{2}'", new object[]
				{
					eventSource.Name,
					eventSource.Guid,
					session.Properties.Name
				});
			}
			return flag;
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x00058A1C File Offset: 0x00056C1C
		private static bool IsEventSourceEnabled(EventSource eventSource, long loggerId)
		{
			ProviderInfo etwProviderInfo = EtwProviderSynchronizer.GetEtwProviderInfo(eventSource);
			return etwProviderInfo != null && etwProviderInfo.IsEnabled && (long)etwProviderInfo.LoggerId == loggerId && eventSource.IsEnabled();
		}

		// Token: 0x06001827 RID: 6183 RVA: 0x00058A50 File Offset: 0x00056C50
		private static ProviderInfo GetEtwProviderInfo(EventSource eventSource)
		{
			return EtwProvider.GetRegisteredProviders().FirstOrDefault((ProviderInfo p) => p.Guid.Equals(eventSource.Guid));
		}

		// Token: 0x0400086E RID: 2158
		private const int c_millisecondsTimeoutForEnablingProvider = 120000;

		// Token: 0x0400086F RID: 2159
		private const int c_millisecondsPollingInterval = 50;
	}
}
