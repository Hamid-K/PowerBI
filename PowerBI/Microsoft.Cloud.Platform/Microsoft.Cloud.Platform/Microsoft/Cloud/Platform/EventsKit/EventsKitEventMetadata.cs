using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Eventing.Base;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.EventsKit
{
	// Token: 0x02000361 RID: 865
	public class EventsKitEventMetadata : IEventMetadata
	{
		// Token: 0x060019A3 RID: 6563 RVA: 0x0005F1F4 File Offset: 0x0005D3F4
		internal EventsKitEventMetadata([NotNull] MethodInfo method, [NotNull] EventsKitMetadata eventsKit)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<MethodInfo>(method, "method");
			ExtendedDiagnostics.EnsureArgumentNotNull<EventsKitMetadata>(eventsKit, "eventskit");
			this.m_eventAttr = Auxiliary.GetCustomAttribute<EventAttribute>(method);
			if (this.m_eventAttr == null)
			{
				throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "{0}.{1} must have the [Event] attribute", new object[] { eventsKit.Name, method.Name }));
			}
			this.m_traceAttr = Auxiliary.GetCustomAttribute<TraceAttribute>(method);
			this.m_visibleAttr = Auxiliary.GetCustomAttribute<VisibleAttribute>(method);
			this.m_publishedEventAttr = Auxiliary.GetCustomAttribute<PublishedEventAttribute>(method);
			this.m_auditProperty = Auxiliary.GetCustomAttribute<AuditAttribute>(method);
			this.m_activityMonitoringAttr = Auxiliary.GetCustomAttribute<ActivityMonitoringAttribute>(method);
			this.m_filteredWindowsEventLogAttr = Auxiliary.GetCustomAttribute<FilteredWindowsEventLogAttribute>(method);
			this.m_windowsEventLogAttr = Auxiliary.GetCustomAttribute<WindowsEventLogAttribute>(method);
			this.m_reportingAttribute = Auxiliary.GetCustomAttribute<ReportingAttribute>(method);
			this.m_errorInitiationPointAttr = Auxiliary.GetCustomAttribute<ErrorInitiationPointAttribute>(method);
			this.m_eventsKit = eventsKit;
			this.EventMethod = method;
			this.Id = new EventsKitIdentifiers(this.m_eventsKit.Id.EventsKitId, this.m_eventAttr.Id);
			eventsKit.IsWindowsEventLog = this.IsWindowsEventLog;
			this.m_parameters = new List<ParameterMetadata>();
			int num = method.GetParameters().Sum(delegate(ParameterInfo p)
			{
				if (!(p.ParameterType.GetInterface(typeof(IMonitoredError).FullName) != null) && !p.ParameterType.FullName.Equals(typeof(IMonitoredError).FullName, StringComparison.Ordinal))
				{
					return 0;
				}
				return 1;
			});
			if (num > 1)
			{
				throw new InvalidOperationException("Events support a single IMonitoredError parameter in " + method.Name);
			}
			if (this.m_filteredWindowsEventLogAttr != null && num == 0)
			{
				throw new InvalidOperationException("[FilteredWindowsEventLog] attribute requires one IMonitoredError parameter in " + method.Name + ". Common IMonitoredError implementations are MonitoredException(s) and MessageDeliveryStatus");
			}
			foreach (ParameterInfo parameterInfo in method.GetParameters())
			{
				this.m_parameters.Add(ParameterMetadataFactory.Create(parameterInfo));
			}
			this.m_perfCounters = new SortedList<string, PerformanceCounterMetadata>();
			PerformanceCounterAttribute[] customAttributes = Auxiliary.GetCustomAttributes<PerformanceCounterAttribute>(method);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				PerformanceCounterMetadata performanceCounterMetadata = new PerformanceCounterMetadata(customAttributes[i], this);
				this.m_perfCounters.Add(performanceCounterMetadata.Name, performanceCounterMetadata);
			}
		}

		// Token: 0x17000389 RID: 905
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x0005F3EC File Offset: 0x0005D5EC
		public string Name
		{
			get
			{
				return this.EventMethod.Name;
			}
		}

		// Token: 0x1700038A RID: 906
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x0000E568 File Offset: 0x0000C768
		public int Priority
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x0005F3F9 File Offset: 0x0005D5F9
		public EventsKitMetadata EventsKit
		{
			get
			{
				return this.m_eventsKit;
			}
		}

		// Token: 0x1700038C RID: 908
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x0005F401 File Offset: 0x0005D601
		public int Order
		{
			get
			{
				return this.m_eventAttr.Order;
			}
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x0005F40E File Offset: 0x0005D60E
		public int Version
		{
			get
			{
				return this.m_eventAttr.Version;
			}
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x060019A9 RID: 6569 RVA: 0x0005F41B File Offset: 0x0005D61B
		public ReadOnlyCollection<ParameterMetadata> Parameters
		{
			get
			{
				return new ReadOnlyCollection<ParameterMetadata>(this.m_parameters);
			}
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x0005F428 File Offset: 0x0005D628
		public ReadOnlyCollection<WireFieldMetadata> GetWireFields()
		{
			List<WireFieldMetadata> list = new List<WireFieldMetadata>();
			foreach (ParameterMetadata parameterMetadata in this.m_parameters)
			{
				list.AddRange(parameterMetadata.WireFields);
			}
			return new ReadOnlyCollection<WireFieldMetadata>(list);
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0005F48C File Offset: 0x0005D68C
		public bool HasPromotedParameters
		{
			get
			{
				return (from f in this.GetWireFields()
					where f.ParameterAttribute != null
					select f).Any<WireFieldMetadata>();
			}
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x0005F4C0 File Offset: 0x0005D6C0
		[CanBeNull]
		public ParameterInfo TryGetMonitoredErrorParameter()
		{
			IEnumerable<ParameterInfo> enumerable = from p in this.EventMethod.GetParameters()
				where p.ParameterType.GetInterface(typeof(IMonitoredError).FullName) != null || p.ParameterType.FullName.Equals(typeof(IMonitoredError).FullName, StringComparison.Ordinal)
				select p;
			int num = enumerable.Count<ParameterInfo>();
			if (num > 1)
			{
				throw new InvalidOperationException("Events support a single IMonitoredError parameter in " + this.GetFriendlyName());
			}
			if (num == 1)
			{
				return enumerable.First<ParameterInfo>();
			}
			return null;
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0005F528 File Offset: 0x0005D728
		public bool IsPublishedToEventingServer
		{
			get
			{
				if (this.m_publishedEventAttr == null)
				{
					return this.m_eventsKit.IsPublishedToEventingServer;
				}
				return (this.m_publishedEventAttr.PublishTo & PublishEventTo.PublishToEventingServer) > PublishEventTo.None;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x0005F54E File Offset: 0x0005D74E
		public bool IsPublishedToEtw
		{
			get
			{
				if (this.m_publishedEventAttr == null)
				{
					return this.m_eventsKit.IsPublishedToEtw;
				}
				return (this.m_publishedEventAttr.PublishTo & PublishEventTo.PublishToEtw) > PublishEventTo.None;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x0005F574 File Offset: 0x0005D774
		public bool IsAlwaysEnabled
		{
			get
			{
				if (this.m_publishedEventAttr == null)
				{
					return this.m_eventsKit.IsAlwaysEnabled;
				}
				return this.m_publishedEventAttr.AlwaysEnabled;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x0005F595 File Offset: 0x0005D795
		public bool IsTrace
		{
			get
			{
				if (this.m_traceAttr == null)
				{
					return this.m_eventsKit.IsTrace;
				}
				return this.m_traceAttr.Enable;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x0005F5B6 File Offset: 0x0005D7B6
		public bool IsVisible
		{
			get
			{
				if (this.m_visibleAttr == null)
				{
					return this.m_eventsKit.IsVisible;
				}
				return this.m_visibleAttr.Enable;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x060019B2 RID: 6578 RVA: 0x0005F5D8 File Offset: 0x0005D7D8
		[CanBeNull]
		public string TraceVerbosityLevel
		{
			get
			{
				switch (this.Level)
				{
				case EventLevel.Verbose:
					return "TraceVerbosity.Verbose";
				case EventLevel.Informational:
					return "TraceVerbosity.Info";
				case EventLevel.Warning:
					return "TraceVerbosity.Warning";
				case EventLevel.Error:
					return "TraceVerbosity.Error";
				case EventLevel.Critical:
					return "TraceVerbosity.Fatal";
				default:
					return null;
				}
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x0005F62C File Offset: 0x0005D82C
		public string TraceProvider
		{
			get
			{
				string text = this.EventsKit.TraceProvider;
				if (this.m_traceAttr != null && this.m_traceAttr.TraceProvider != null)
				{
					text = this.m_traceAttr.TraceProvider.FullName;
				}
				return text;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x0005F672 File Offset: 0x0005D872
		public string Format
		{
			get
			{
				return this.m_eventAttr.Format;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x0005F67F File Offset: 0x0005D87F
		public bool IsActivityMonitoring
		{
			get
			{
				return this.m_activityMonitoringAttr != null;
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x0005F68A File Offset: 0x0005D88A
		public bool IsErrorInitiationPoint
		{
			get
			{
				return this.m_errorInitiationPointAttr != null;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x0005F695 File Offset: 0x0005D895
		public bool IsWindowsEventLog
		{
			get
			{
				return this.m_windowsEventLogAttr != null;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0005F6A0 File Offset: 0x0005D8A0
		public bool HasPerformanceCounters
		{
			get
			{
				return this.m_perfCounters != null && this.m_perfCounters.Any<KeyValuePair<string, PerformanceCounterMetadata>>();
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0005F6B7 File Offset: 0x0005D8B7
		public bool IsReportingEvent
		{
			get
			{
				return this.m_reportingAttribute != null;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x0005F6C4 File Offset: 0x0005D8C4
		public EventPurpose EventTypes
		{
			get
			{
				EventPurpose eventPurpose = EventPurpose.Testability;
				if (this.IsActivityMonitoring || this.IsErrorInitiationPoint || this.IsWindowsEventLog || this.IsFilteredWindowsEventLog)
				{
					eventPurpose |= EventPurpose.Monitoring;
				}
				if (this.IsReportingEvent)
				{
					eventPurpose |= EventPurpose.Reporting;
				}
				if (this.IsAuditEvent)
				{
					eventPurpose |= EventPurpose.Audit;
				}
				return eventPurpose;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0005F718 File Offset: 0x0005D918
		public bool IsFilteredWindowsEventLog
		{
			get
			{
				return this.m_filteredWindowsEventLogAttr != null;
			}
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x0005F723 File Offset: 0x0005D923
		public EventLogEntryType GetWindowsEventLogEntrySeverity()
		{
			if (this.m_windowsEventLogAttr != null)
			{
				return this.m_windowsEventLogAttr.Severity;
			}
			throw new InvalidOperationException("Cannot find [WindowsEventLog] attribute");
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x0005F743 File Offset: 0x0005D943
		public int GetWindowsEventLogId()
		{
			if (this.m_windowsEventLogAttr != null)
			{
				return this.m_windowsEventLogAttr.WindowsEventLogId;
			}
			if (this.m_filteredWindowsEventLogAttr != null)
			{
				return this.m_filteredWindowsEventLogAttr.WindowsEventLogId;
			}
			throw new InvalidOperationException("Cannot find [WindowsEventLog] attribute");
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060019BE RID: 6590 RVA: 0x0005F777 File Offset: 0x0005D977
		// (set) Token: 0x060019BF RID: 6591 RVA: 0x0005F77F File Offset: 0x0005D97F
		public EventsKitIdentifiers Id { get; private set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0005F788 File Offset: 0x0005D988
		public EventLevel Level
		{
			get
			{
				if (this.m_eventAttr.Level.Equals(EventLevel.Inherit))
				{
					return this.m_eventsKit.Level;
				}
				return this.m_eventAttr.Level;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060019C1 RID: 6593 RVA: 0x0005F7CD File Offset: 0x0005D9CD
		// (set) Token: 0x060019C2 RID: 6594 RVA: 0x0005F7D5 File Offset: 0x0005D9D5
		public MethodInfo EventMethod { get; private set; }

		// Token: 0x060019C3 RID: 6595 RVA: 0x0005F7DE File Offset: 0x0005D9DE
		public string GetFriendlyName()
		{
			if (!string.IsNullOrEmpty(this.m_eventAttr.Name))
			{
				return this.m_eventAttr.Name;
			}
			return this.Name;
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0005F804 File Offset: 0x0005DA04
		public bool IsPublishedEvent
		{
			get
			{
				bool flag;
				if (this.m_publishedEventAttr != null)
				{
					flag = this.m_publishedEventAttr.Enable;
				}
				else
				{
					flag = this.EventsKit.IsPublishedEvent;
				}
				return flag;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x0005F83B File Offset: 0x0005DA3B
		public bool IsAuditEvent
		{
			get
			{
				return this.m_auditProperty != null || this.EventsKit.IsAuditEvent;
			}
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x0005F854 File Offset: 0x0005DA54
		public string GetSchemaAsString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.IsWindowsEventLog)
			{
				stringBuilder.AppendLine("{0}[{1}({2}, {3})]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(WindowsEventLogAttribute).Name,
					this.GetWindowsEventLogEntrySeverity(),
					this.GetWindowsEventLogId()
				}));
			}
			if (this.IsFilteredWindowsEventLog)
			{
				stringBuilder.AppendLine("{0}[{1}({2})]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(FilteredWindowsEventLogAttribute).Name,
					this.GetWindowsEventLogId()
				}));
			}
			if (Auxiliary.GetCustomAttribute<AnalysisAttribute>(this.EventMethod) != null)
			{
				stringBuilder.AppendLine("{0}[{1}]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(AnalysisAttribute).Name
				}));
			}
			if (Auxiliary.GetCustomAttribute<ReportingAttribute>(this.EventMethod) != null)
			{
				stringBuilder.AppendLine("{0}[{1}]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(ReportingAttribute).Name
				}));
			}
			if (Auxiliary.GetCustomAttribute<LoggingAttribute>(this.EventMethod) != null)
			{
				stringBuilder.AppendLine("{0}[{1}]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(LoggingAttribute).Name
				}));
			}
			if (Auxiliary.GetCustomAttribute<PerformanceAnalysisAttribute>(this.EventMethod) != null)
			{
				stringBuilder.AppendLine("{0}[{1}]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(PerformanceAnalysisAttribute).Name
				}));
			}
			if (Auxiliary.GetCustomAttribute<SupportAttribute>(this.EventMethod) != null)
			{
				stringBuilder.AppendLine("{0}[{1}]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(SupportAttribute).Name
				}));
			}
			if (Auxiliary.GetCustomAttribute<AuditAttribute>(this.EventMethod) != null)
			{
				stringBuilder.AppendLine("{0}[{1}]".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(AuditAttribute).Name
				}));
			}
			foreach (PerformanceCounterMetadata performanceCounterMetadata in this.PerformanceCounters)
			{
				stringBuilder.AppendLine("{0}[{1}(\"{2}\", {3}, \"{4}\", {5})".FormatWithInvariantCulture(new object[]
				{
					"    ",
					typeof(PerformanceCounterAttribute).Name,
					performanceCounterMetadata.CounterName,
					performanceCounterMetadata.Modifier,
					performanceCounterMetadata.ModifierExpression,
					performanceCounterMetadata.CounterType
				}));
			}
			stringBuilder.AppendLine("{0}[{1}({2}, Level = {3}, Format = \"{4}\")]".FormatWithInvariantCulture(new object[]
			{
				"    ",
				typeof(EventAttribute).Name,
				this.Id.EventId,
				this.Level,
				this.m_eventAttr.Format
			}));
			stringBuilder.AppendLine("{0}{1}".FormatWithInvariantCulture(new object[]
			{
				"    ",
				EventsKitEventMetadata.EmitFullSignature(this.EventMethod)
			}));
			return stringBuilder.ToString();
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x0005FBA4 File Offset: 0x0005DDA4
		public IEnumerable<PerformanceCounterMetadata> PerformanceCounters
		{
			get
			{
				return this.m_perfCounters.Values;
			}
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x0005FBB4 File Offset: 0x0005DDB4
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, "{0}.{1} ({2})", new object[]
			{
				this.EventMethod.DeclaringType.Name,
				this.EventMethod.Name,
				this.Level
			});
		}

		// Token: 0x060019C9 RID: 6601 RVA: 0x0005FC08 File Offset: 0x0005DE08
		private static string EmitFullSignature(MethodInfo methodInfo)
		{
			ExtendedDiagnostics.EnsureArgument("methodInfo", !methodInfo.IsGenericMethod, "EventsKit does not support generic methods");
			IEnumerable<string> enumerable = from param in methodInfo.GetParameters()
				select string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					param.ParameterType.Name,
					param.Name
				});
			return "{0} {1}({2})".FormatWithInvariantCulture(new object[]
			{
				methodInfo.ReturnType.Name,
				methodInfo.Name,
				enumerable.StringJoin(",")
			});
		}

		// Token: 0x040008DD RID: 2269
		private const string c_tabString = "    ";

		// Token: 0x040008DE RID: 2270
		private readonly EventsKitMetadata m_eventsKit;

		// Token: 0x040008DF RID: 2271
		private readonly EventAttribute m_eventAttr;

		// Token: 0x040008E0 RID: 2272
		private readonly PublishedEventAttribute m_publishedEventAttr;

		// Token: 0x040008E1 RID: 2273
		private readonly AuditAttribute m_auditProperty;

		// Token: 0x040008E2 RID: 2274
		private readonly TraceAttribute m_traceAttr;

		// Token: 0x040008E3 RID: 2275
		private readonly VisibleAttribute m_visibleAttr;

		// Token: 0x040008E4 RID: 2276
		private readonly WindowsEventLogAttribute m_windowsEventLogAttr;

		// Token: 0x040008E5 RID: 2277
		private readonly FilteredWindowsEventLogAttribute m_filteredWindowsEventLogAttr;

		// Token: 0x040008E6 RID: 2278
		private readonly ActivityMonitoringAttribute m_activityMonitoringAttr;

		// Token: 0x040008E7 RID: 2279
		private readonly ErrorInitiationPointAttribute m_errorInitiationPointAttr;

		// Token: 0x040008E8 RID: 2280
		private readonly ReportingAttribute m_reportingAttribute;

		// Token: 0x040008E9 RID: 2281
		private readonly List<ParameterMetadata> m_parameters;

		// Token: 0x040008EA RID: 2282
		private readonly SortedList<string, PerformanceCounterMetadata> m_perfCounters;
	}
}
