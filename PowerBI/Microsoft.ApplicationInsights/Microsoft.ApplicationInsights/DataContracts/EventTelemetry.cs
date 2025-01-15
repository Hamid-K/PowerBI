using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000CB RID: 203
	public sealed class EventTelemetry : ITelemetry, ISupportProperties, ISupportSampling, ISupportMetrics, IAiSerializableTelemetry
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x000185BF File Offset: 0x000167BF
		public EventTelemetry()
		{
			this.Data = new EventData();
			this.context = new TelemetryContext(this.Data.properties);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x000185E8 File Offset: 0x000167E8
		public EventTelemetry(string name)
			: this()
		{
			this.Name = name;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x000185F8 File Offset: 0x000167F8
		private EventTelemetry(EventTelemetry source)
		{
			this.Data = source.Data.DeepClone();
			this.context = source.context.DeepClone(this.Data.properties);
			this.Sequence = source.Sequence;
			this.Timestamp = source.Timestamp;
			this.samplingPercentage = source.samplingPercentage;
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x00018674 File Offset: 0x00016874
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "Event";
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0001867B File Offset: 0x0001687B
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "EventData";
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006E6 RID: 1766 RVA: 0x00018682 File Offset: 0x00016882
		// (set) Token: 0x060006E7 RID: 1767 RVA: 0x0001868A File Offset: 0x0001688A
		public DateTimeOffset Timestamp { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x00018693 File Offset: 0x00016893
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0001869B File Offset: 0x0001689B
		public string Sequence { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x000186A4 File Offset: 0x000168A4
		public TelemetryContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x000186AC File Offset: 0x000168AC
		// (set) Token: 0x060006EC RID: 1772 RVA: 0x000186B4 File Offset: 0x000168B4
		public IExtension Extension
		{
			get
			{
				return this.extension;
			}
			set
			{
				this.extension = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006ED RID: 1773 RVA: 0x000186BD File Offset: 0x000168BD
		// (set) Token: 0x060006EE RID: 1774 RVA: 0x000186CA File Offset: 0x000168CA
		public string Name
		{
			get
			{
				return this.Data.name;
			}
			set
			{
				this.Data.name = value;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006EF RID: 1775 RVA: 0x000186D8 File Offset: 0x000168D8
		public IDictionary<string, double> Metrics
		{
			get
			{
				return this.Data.measurements;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006F0 RID: 1776 RVA: 0x000186E5 File Offset: 0x000168E5
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.properties;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x000186F2 File Offset: 0x000168F2
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x000186FA File Offset: 0x000168FA
		double? ISupportSampling.SamplingPercentage
		{
			get
			{
				return this.samplingPercentage;
			}
			set
			{
				this.samplingPercentage = value;
			}
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00018703 File Offset: 0x00016903
		public ITelemetry DeepClone()
		{
			return new EventTelemetry(this);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001870C File Offset: 0x0001690C
		void ITelemetry.Sanitize()
		{
			this.Name = this.Name.SanitizeEventName();
			this.Name = Utils.PopulateRequiredStringValue(this.Name, "name", typeof(EventTelemetry).FullName);
			this.Properties.SanitizeProperties();
			this.Metrics.SanitizeMeasurements();
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00018765 File Offset: 0x00016965
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty(this.Data);
		}

		// Token: 0x040002BD RID: 701
		internal const string TelemetryName = "Event";

		// Token: 0x040002BE RID: 702
		internal readonly EventData Data;

		// Token: 0x040002BF RID: 703
		private readonly TelemetryContext context;

		// Token: 0x040002C0 RID: 704
		private IExtension extension;

		// Token: 0x040002C1 RID: 705
		private double? samplingPercentage;
	}
}
