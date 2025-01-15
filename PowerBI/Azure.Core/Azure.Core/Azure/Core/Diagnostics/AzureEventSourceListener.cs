using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Runtime.CompilerServices;
using Azure.Core.Shared;

namespace Azure.Core.Diagnostics
{
	// Token: 0x020000C9 RID: 201
	[NullableContext(1)]
	[Nullable(0)]
	public class AzureEventSourceListener : EventListener
	{
		// Token: 0x060006C4 RID: 1732 RVA: 0x00016E08 File Offset: 0x00015008
		public AzureEventSourceListener(Action<EventWrittenEventArgs, string> log, EventLevel level)
		{
			if (log == null)
			{
				throw new ArgumentNullException("log");
			}
			this._log = log;
			this._level = level;
			foreach (EventSource eventSource in this._eventSources)
			{
				this.OnEventSourceCreated(eventSource);
			}
			this._eventSources.Clear();
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00016E94 File Offset: 0x00015094
		protected sealed override void OnEventSourceCreated(EventSource eventSource)
		{
			base.OnEventSourceCreated(eventSource);
			if (this._log == null)
			{
				this._eventSources.Add(eventSource);
			}
			if (eventSource.GetTrait("AzureEventSource") == "true")
			{
				base.EnableEvents(eventSource, this._level);
			}
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00016EE0 File Offset: 0x000150E0
		protected sealed override void OnEventWritten(EventWrittenEventArgs eventData)
		{
			if (eventData.EventId == -1)
			{
				return;
			}
			Action<EventWrittenEventArgs, string> log = this._log;
			if (log == null)
			{
				return;
			}
			log(eventData, EventSourceEventFormatting.Format(eventData));
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00016F03 File Offset: 0x00015103
		public static AzureEventSourceListener CreateConsoleLogger(EventLevel level = EventLevel.Informational)
		{
			return new AzureEventSourceListener(delegate(EventWrittenEventArgs eventData, string text)
			{
				Console.WriteLine("[{1}] {0}: {2}", eventData.EventSource.Name, eventData.Level, text);
			}, level);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00016F2A File Offset: 0x0001512A
		public static AzureEventSourceListener CreateTraceLogger(EventLevel level = EventLevel.Informational)
		{
			return new AzureEventSourceListener(delegate(EventWrittenEventArgs eventData, string text)
			{
				Trace.WriteLine(string.Format(CultureInfo.InvariantCulture, "[{0}] {1}", eventData.Level, text), eventData.EventSource.Name);
			}, level);
		}

		// Token: 0x040002A3 RID: 675
		public const string TraitName = "AzureEventSource";

		// Token: 0x040002A4 RID: 676
		public const string TraitValue = "true";

		// Token: 0x040002A5 RID: 677
		private readonly List<EventSource> _eventSources = new List<EventSource>();

		// Token: 0x040002A6 RID: 678
		private readonly Action<EventWrittenEventArgs, string> _log;

		// Token: 0x040002A7 RID: 679
		private readonly EventLevel _level;
	}
}
