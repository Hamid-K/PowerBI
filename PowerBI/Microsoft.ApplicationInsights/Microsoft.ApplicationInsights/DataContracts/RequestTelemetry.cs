using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D9 RID: 217
	public sealed class RequestTelemetry : OperationTelemetry, ITelemetry, ISupportProperties, ISupportMetrics, ISupportSampling, IAiSerializableTelemetry
	{
		// Token: 0x060007C3 RID: 1987 RVA: 0x00019DE0 File Offset: 0x00017FE0
		public RequestTelemetry()
		{
			this.context = new TelemetryContext();
			base.GenerateId();
			this.Source = string.Empty;
			this.Name = string.Empty;
			this.ResponseCode = string.Empty;
			this.Duration = TimeSpan.Zero;
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00019E37 File Offset: 0x00018037
		public RequestTelemetry(string name, DateTimeOffset startTime, TimeSpan duration, string responseCode, bool success)
			: this()
		{
			this.Name = name;
			this.Timestamp = startTime;
			this.Duration = duration;
			this.ResponseCode = responseCode;
			this.Success = new bool?(success);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00019E6C File Offset: 0x0001806C
		private RequestTelemetry(RequestTelemetry source)
		{
			this.Duration = source.Duration;
			this.Id = source.Id;
			if (source.measurementsValue != null)
			{
				Utils.CopyDictionary<double>(source.Metrics, this.Metrics);
			}
			this.Name = source.Name;
			this.context = source.context.DeepClone();
			this.ResponseCode = source.ResponseCode;
			this.Source = source.Source;
			this.Success = source.Success;
			this.Url = source.Url;
			this.Sequence = source.Sequence;
			this.Timestamp = source.Timestamp;
			this.successFieldSet = source.successFieldSet;
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00019F40 File Offset: 0x00018140
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "Request";
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00019F47 File Offset: 0x00018147
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "RequestData";
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007C8 RID: 1992 RVA: 0x00019F4E File Offset: 0x0001814E
		// (set) Token: 0x060007C9 RID: 1993 RVA: 0x00019F56 File Offset: 0x00018156
		public override DateTimeOffset Timestamp { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x00019F5F File Offset: 0x0001815F
		// (set) Token: 0x060007CB RID: 1995 RVA: 0x00019F67 File Offset: 0x00018167
		public override string Sequence { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x00019F70 File Offset: 0x00018170
		public override TelemetryContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x00019F78 File Offset: 0x00018178
		// (set) Token: 0x060007CE RID: 1998 RVA: 0x00019F80 File Offset: 0x00018180
		public override IExtension Extension
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

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x00019F89 File Offset: 0x00018189
		// (set) Token: 0x060007D0 RID: 2000 RVA: 0x00019F91 File Offset: 0x00018191
		public override string Id { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x00019F9A File Offset: 0x0001819A
		// (set) Token: 0x060007D2 RID: 2002 RVA: 0x00019FA2 File Offset: 0x000181A2
		public override string Name { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x00019FAB File Offset: 0x000181AB
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x00019FB3 File Offset: 0x000181B3
		public string ResponseCode { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x00019FBC File Offset: 0x000181BC
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x00019FE6 File Offset: 0x000181E6
		public override bool? Success
		{
			get
			{
				if (this.successFieldSet)
				{
					return new bool?(this.success);
				}
				return null;
			}
			set
			{
				if (value != null && value != null)
				{
					this.success = value.Value;
					this.successFieldSet = true;
					return;
				}
				this.success = true;
				this.successFieldSet = false;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0001A01D File Offset: 0x0001821D
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0001A025 File Offset: 0x00018225
		public override TimeSpan Duration { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0001A030 File Offset: 0x00018230
		public override IDictionary<string, string> Properties
		{
			get
			{
				if (!string.IsNullOrEmpty(this.MetricExtractorInfo) && !this.Context.Properties.ContainsKey("_MS.ProcessedByMetricExtractors"))
				{
					this.Context.Properties["_MS.ProcessedByMetricExtractors"] = this.MetricExtractorInfo;
				}
				return this.Context.Properties;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001A087 File Offset: 0x00018287
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001A08F File Offset: 0x0001828F
		public Uri Url { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x0001A098 File Offset: 0x00018298
		public override IDictionary<string, double> Metrics
		{
			get
			{
				return LazyInitializer.EnsureInitialized<IDictionary<string, double>>(ref this.measurementsValue, () => new ConcurrentDictionary<string, double>());
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x0001A0C4 File Offset: 0x000182C4
		// (set) Token: 0x060007DE RID: 2014 RVA: 0x0001A0D6 File Offset: 0x000182D6
		[Obsolete("Include http verb into request telemetry name and use custom properties to report http method as a dimension.")]
		public string HttpMethod
		{
			get
			{
				return this.Properties["httpMethod"];
			}
			set
			{
				this.Properties["httpMethod"] = value;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x0001A0E9 File Offset: 0x000182E9
		// (set) Token: 0x060007E0 RID: 2016 RVA: 0x0001A0F1 File Offset: 0x000182F1
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

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x0001A0FA File Offset: 0x000182FA
		// (set) Token: 0x060007E2 RID: 2018 RVA: 0x0001A102 File Offset: 0x00018302
		public string Source { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x0001A10B File Offset: 0x0001830B
		// (set) Token: 0x060007E4 RID: 2020 RVA: 0x0001A113 File Offset: 0x00018313
		internal string MetricExtractorInfo { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x0001A11C File Offset: 0x0001831C
		// (set) Token: 0x060007E6 RID: 2022 RVA: 0x0001A135 File Offset: 0x00018335
		internal RequestData Data
		{
			get
			{
				return LazyInitializer.EnsureInitialized<RequestData>(ref this.dataPrivate, delegate
				{
					RequestData requestData = new RequestData();
					requestData.duration = this.Duration;
					requestData.id = this.Id;
					requestData.measurements = this.measurementsValue;
					requestData.name = this.Name;
					requestData.properties = this.context.PropertiesValue;
					requestData.responseCode = this.ResponseCode;
					requestData.source = this.Source;
					requestData.success = this.success;
					Uri url = this.Url;
					requestData.url = ((url != null) ? url.ToString() : null);
					return requestData;
				});
			}
			private set
			{
				this.dataPrivate = value;
			}
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x0001A13E File Offset: 0x0001833E
		public override ITelemetry DeepClone()
		{
			return new RequestTelemetry(this);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x0001A146 File Offset: 0x00018346
		public override void SerializeData(ISerializationWriter serializationWriter)
		{
			this.dataPrivate = null;
			serializationWriter.WriteProperty(this.Data);
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0001A15C File Offset: 0x0001835C
		void ITelemetry.Sanitize()
		{
			this.Name = this.Name.SanitizeName();
			this.Properties.SanitizeProperties();
			this.Metrics.SanitizeMeasurements();
			this.Url = this.Url.SanitizeUri();
			this.Id = this.Id.SanitizeName();
			this.Id = Utils.PopulateRequiredStringValue(this.Id, "id", typeof(RequestTelemetry).FullName);
			if (this.Success == null)
			{
				this.Success = new bool?(true);
			}
			if (string.IsNullOrEmpty(this.ResponseCode))
			{
				this.ResponseCode = (this.Success.Value ? "200" : string.Empty);
			}
		}

		// Token: 0x040002EC RID: 748
		internal new const string TelemetryName = "Request";

		// Token: 0x040002ED RID: 749
		private readonly TelemetryContext context;

		// Token: 0x040002EE RID: 750
		private RequestData dataPrivate;

		// Token: 0x040002EF RID: 751
		private bool successFieldSet;

		// Token: 0x040002F0 RID: 752
		private IExtension extension;

		// Token: 0x040002F1 RID: 753
		private double? samplingPercentage;

		// Token: 0x040002F2 RID: 754
		private bool success = true;

		// Token: 0x040002F3 RID: 755
		private IDictionary<string, double> measurementsValue;
	}
}
