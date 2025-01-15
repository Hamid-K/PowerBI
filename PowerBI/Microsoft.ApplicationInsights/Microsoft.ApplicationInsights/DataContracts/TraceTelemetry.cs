using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000DF RID: 223
	public sealed class TraceTelemetry : ITelemetry, ISupportProperties, ISupportSampling, IAiSerializableTelemetry
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x0001A8D8 File Offset: 0x00018AD8
		public TraceTelemetry()
		{
			this.Data = new MessageData();
			this.context = new TelemetryContext(this.Data.properties);
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001A901 File Offset: 0x00018B01
		public TraceTelemetry(string message)
			: this()
		{
			this.Message = message;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001A910 File Offset: 0x00018B10
		public TraceTelemetry(string message, SeverityLevel severityLevel)
			: this(message)
		{
			this.SeverityLevel = new SeverityLevel?(severityLevel);
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x0001A928 File Offset: 0x00018B28
		private TraceTelemetry(TraceTelemetry source)
		{
			this.Data = source.Data.DeepClone();
			this.context = source.context.DeepClone(this.Data.properties);
			this.Sequence = source.Sequence;
			this.Timestamp = source.Timestamp;
			this.samplingPercentage = source.samplingPercentage;
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "Message";
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001A9AB File Offset: 0x00018BAB
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "MessageData";
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x0001A9B2 File Offset: 0x00018BB2
		// (set) Token: 0x06000821 RID: 2081 RVA: 0x0001A9BA File Offset: 0x00018BBA
		public DateTimeOffset Timestamp { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001A9C3 File Offset: 0x00018BC3
		// (set) Token: 0x06000823 RID: 2083 RVA: 0x0001A9CB File Offset: 0x00018BCB
		public string Sequence { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000824 RID: 2084 RVA: 0x0001A9D4 File Offset: 0x00018BD4
		public TelemetryContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x0001A9DC File Offset: 0x00018BDC
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x0001A9E4 File Offset: 0x00018BE4
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

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000827 RID: 2087 RVA: 0x0001A9ED File Offset: 0x00018BED
		// (set) Token: 0x06000828 RID: 2088 RVA: 0x0001A9FA File Offset: 0x00018BFA
		public string Message
		{
			get
			{
				return this.Data.message;
			}
			set
			{
				this.Data.message = value;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000829 RID: 2089 RVA: 0x0001AA08 File Offset: 0x00018C08
		// (set) Token: 0x0600082A RID: 2090 RVA: 0x0001AA1A File Offset: 0x00018C1A
		public SeverityLevel? SeverityLevel
		{
			get
			{
				return this.Data.severityLevel.TranslateSeverityLevel();
			}
			set
			{
				this.Data.severityLevel = value.TranslateSeverityLevel();
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x0001AA2D File Offset: 0x00018C2D
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.properties;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0001AA3A File Offset: 0x00018C3A
		// (set) Token: 0x0600082D RID: 2093 RVA: 0x0001AA42 File Offset: 0x00018C42
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

		// Token: 0x0600082E RID: 2094 RVA: 0x0001AA4B File Offset: 0x00018C4B
		public ITelemetry DeepClone()
		{
			return new TraceTelemetry(this);
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001AA53 File Offset: 0x00018C53
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty(this.Data);
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0001AA64 File Offset: 0x00018C64
		void ITelemetry.Sanitize()
		{
			this.Data.message = this.Data.message.SanitizeMessage();
			this.Data.message = Utils.PopulateRequiredStringValue(this.Data.message, "message", typeof(TraceTelemetry).FullName);
			this.Data.properties.SanitizeProperties();
		}

		// Token: 0x04000319 RID: 793
		internal const string TelemetryName = "Message";

		// Token: 0x0400031A RID: 794
		internal readonly MessageData Data;

		// Token: 0x0400031B RID: 795
		private readonly TelemetryContext context;

		// Token: 0x0400031C RID: 796
		private IExtension extension;

		// Token: 0x0400031D RID: 797
		private double? samplingPercentage;
	}
}
