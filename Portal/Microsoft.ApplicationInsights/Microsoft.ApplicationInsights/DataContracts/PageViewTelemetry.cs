using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D7 RID: 215
	public sealed class PageViewTelemetry : ITelemetry, ISupportProperties, ISupportSampling, ISupportMetrics, IAiSerializableTelemetry
	{
		// Token: 0x0600078F RID: 1935 RVA: 0x00019838 File Offset: 0x00017A38
		public PageViewTelemetry()
		{
			this.Data = new PageViewData();
			this.context = new TelemetryContext(this.Data.properties);
		}

		// Token: 0x06000790 RID: 1936 RVA: 0x00019861 File Offset: 0x00017A61
		public PageViewTelemetry(string pageName)
			: this()
		{
			this.Name = pageName;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x00019870 File Offset: 0x00017A70
		private PageViewTelemetry(PageViewTelemetry source)
		{
			this.Data = source.Data.DeepClone();
			this.context = source.context.DeepClone(this.Data.properties);
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
			this.Timestamp = source.Timestamp;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x000198D4 File Offset: 0x00017AD4
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "PageView";
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000793 RID: 1939 RVA: 0x000198DB File Offset: 0x00017ADB
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "PageViewData";
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000794 RID: 1940 RVA: 0x000198E2 File Offset: 0x00017AE2
		// (set) Token: 0x06000795 RID: 1941 RVA: 0x000198EA File Offset: 0x00017AEA
		public DateTimeOffset Timestamp { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x000198F3 File Offset: 0x00017AF3
		// (set) Token: 0x06000797 RID: 1943 RVA: 0x000198FB File Offset: 0x00017AFB
		public string Sequence { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x00019904 File Offset: 0x00017B04
		public TelemetryContext Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000799 RID: 1945 RVA: 0x0001990C File Offset: 0x00017B0C
		// (set) Token: 0x0600079A RID: 1946 RVA: 0x00019914 File Offset: 0x00017B14
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

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600079B RID: 1947 RVA: 0x0001991D File Offset: 0x00017B1D
		// (set) Token: 0x0600079C RID: 1948 RVA: 0x0001992A File Offset: 0x00017B2A
		public string Id
		{
			get
			{
				return this.Data.id;
			}
			set
			{
				this.Data.id = value;
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600079D RID: 1949 RVA: 0x00019938 File Offset: 0x00017B38
		// (set) Token: 0x0600079E RID: 1950 RVA: 0x00019945 File Offset: 0x00017B45
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

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00019953 File Offset: 0x00017B53
		// (set) Token: 0x060007A0 RID: 1952 RVA: 0x0001997A File Offset: 0x00017B7A
		public Uri Url
		{
			get
			{
				if (this.Data.url.IsNullOrWhiteSpace())
				{
					return null;
				}
				return new Uri(this.Data.url, UriKind.RelativeOrAbsolute);
			}
			set
			{
				if (value == null)
				{
					this.Data.url = null;
					return;
				}
				this.Data.url = value.ToString();
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x000199A3 File Offset: 0x00017BA3
		// (set) Token: 0x060007A2 RID: 1954 RVA: 0x000199B5 File Offset: 0x00017BB5
		public TimeSpan Duration
		{
			get
			{
				return Utils.ValidateDuration(this.Data.duration);
			}
			set
			{
				this.Data.duration = value.ToString();
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x000199CF File Offset: 0x00017BCF
		public IDictionary<string, double> Metrics
		{
			get
			{
				return this.Data.measurements;
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x000199DC File Offset: 0x00017BDC
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.properties;
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x000199E9 File Offset: 0x00017BE9
		// (set) Token: 0x060007A6 RID: 1958 RVA: 0x000199F1 File Offset: 0x00017BF1
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

		// Token: 0x060007A7 RID: 1959 RVA: 0x000199FA File Offset: 0x00017BFA
		public ITelemetry DeepClone()
		{
			return new PageViewTelemetry(this);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00019A02 File Offset: 0x00017C02
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty(this.Data);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00019A10 File Offset: 0x00017C10
		void ITelemetry.Sanitize()
		{
			this.Name = this.Name.SanitizeName();
			this.Name = Utils.PopulateRequiredStringValue(this.Name, "name", typeof(PageViewTelemetry).FullName);
			this.Properties.SanitizeProperties();
			this.Metrics.SanitizeMeasurements();
			this.Url = this.Url.SanitizeUri();
			this.Id.SanitizeName();
		}

		// Token: 0x040002E2 RID: 738
		internal const string TelemetryName = "PageView";

		// Token: 0x040002E3 RID: 739
		internal readonly PageViewData Data;

		// Token: 0x040002E4 RID: 740
		private readonly TelemetryContext context;

		// Token: 0x040002E5 RID: 741
		private IExtension extension;

		// Token: 0x040002E6 RID: 742
		private double? samplingPercentage;
	}
}
