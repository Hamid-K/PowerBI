using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D5 RID: 213
	public sealed class MetricTelemetry : ITelemetry, ISupportProperties, IAiSerializableTelemetry
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x000190B4 File Offset: 0x000172B4
		public MetricTelemetry()
		{
			this.Data = new MetricData();
			this.Metric = new DataPoint();
			this.Context = new TelemetryContext(this.Data.properties);
			this.Metric.kind = DataPointType.Aggregation;
			this.Data.metrics.Add(this.Metric);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x00019115 File Offset: 0x00017315
		public MetricTelemetry(string metricName, double metricValue)
			: this()
		{
			this.Name = metricName;
			this.Value = metricValue;
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001912C File Offset: 0x0001732C
		public MetricTelemetry(string name, int count, double sum, double min, double max, double standardDeviation)
			: this()
		{
			this.Name = name;
			this.Count = new int?(count);
			this.Sum = sum;
			this.Min = new double?(min);
			this.Max = new double?(max);
			this.StandardDeviation = new double?(standardDeviation);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x00019180 File Offset: 0x00017380
		public MetricTelemetry(string metricNamespace, string name, int count, double sum, double min, double max, double standardDeviation)
			: this()
		{
			this.MetricNamespace = metricNamespace;
			this.Name = name;
			this.Count = new int?(count);
			this.Sum = sum;
			this.Min = new double?(min);
			this.Max = new double?(max);
			this.StandardDeviation = new double?(standardDeviation);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x000191DC File Offset: 0x000173DC
		private MetricTelemetry(MetricTelemetry source)
		{
			this.Data = source.Data.DeepClone();
			this.Metric = source.Metric.DeepClone();
			this.Context = source.Context.DeepClone(this.Data.properties);
			this.Sequence = source.Sequence;
			this.Timestamp = source.Timestamp;
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x0001925D File Offset: 0x0001745D
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "Metric";
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00019264 File Offset: 0x00017464
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "MetricData";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x0001926B File Offset: 0x0001746B
		// (set) Token: 0x0600074F RID: 1871 RVA: 0x00019273 File Offset: 0x00017473
		public DateTimeOffset Timestamp { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000750 RID: 1872 RVA: 0x0001927C File Offset: 0x0001747C
		// (set) Token: 0x06000751 RID: 1873 RVA: 0x00019284 File Offset: 0x00017484
		public string Sequence { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000752 RID: 1874 RVA: 0x0001928D File Offset: 0x0001748D
		public TelemetryContext Context { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000753 RID: 1875 RVA: 0x00019295 File Offset: 0x00017495
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0001929D File Offset: 0x0001749D
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

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x000192A6 File Offset: 0x000174A6
		// (set) Token: 0x06000756 RID: 1878 RVA: 0x000192B3 File Offset: 0x000174B3
		public string MetricNamespace
		{
			get
			{
				return this.Metric.ns;
			}
			set
			{
				this.Metric.ns = value;
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x000192C1 File Offset: 0x000174C1
		// (set) Token: 0x06000758 RID: 1880 RVA: 0x000192CE File Offset: 0x000174CE
		public string Name
		{
			get
			{
				return this.Metric.name;
			}
			set
			{
				this.Metric.name = value;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x000192DC File Offset: 0x000174DC
		// (set) Token: 0x0600075A RID: 1882 RVA: 0x000192E9 File Offset: 0x000174E9
		[Obsolete("This property is obsolete. Use Sum property instead.")]
		public double Value
		{
			get
			{
				return this.Metric.value;
			}
			set
			{
				this.Metric.value = value;
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x000192F7 File Offset: 0x000174F7
		// (set) Token: 0x0600075C RID: 1884 RVA: 0x00019304 File Offset: 0x00017504
		public double Sum
		{
			get
			{
				return this.Metric.value;
			}
			set
			{
				this.Metric.value = value;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600075D RID: 1885 RVA: 0x00019314 File Offset: 0x00017514
		// (set) Token: 0x0600075E RID: 1886 RVA: 0x00019348 File Offset: 0x00017548
		public int? Count
		{
			get
			{
				if (this.Metric.count == null)
				{
					return new int?(1);
				}
				return this.Metric.count;
			}
			set
			{
				this.Metric.count = value;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600075F RID: 1887 RVA: 0x00019356 File Offset: 0x00017556
		// (set) Token: 0x06000760 RID: 1888 RVA: 0x00019363 File Offset: 0x00017563
		public double? Min
		{
			get
			{
				return this.Metric.min;
			}
			set
			{
				this.Metric.min = value;
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x00019371 File Offset: 0x00017571
		// (set) Token: 0x06000762 RID: 1890 RVA: 0x0001937E File Offset: 0x0001757E
		public double? Max
		{
			get
			{
				return this.Metric.max;
			}
			set
			{
				this.Metric.max = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001938C File Offset: 0x0001758C
		// (set) Token: 0x06000764 RID: 1892 RVA: 0x00019399 File Offset: 0x00017599
		public double? StandardDeviation
		{
			get
			{
				return this.Metric.stdDev;
			}
			set
			{
				this.Metric.stdDev = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x000193A7 File Offset: 0x000175A7
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.properties;
			}
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x000193B4 File Offset: 0x000175B4
		public ITelemetry DeepClone()
		{
			return new MetricTelemetry(this);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x000193BC File Offset: 0x000175BC
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty(this.Data);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x000193CC File Offset: 0x000175CC
		void ITelemetry.Sanitize()
		{
			this.MetricNamespace = Property.TrimAndTruncate(this.MetricNamespace, 256);
			this.Name = this.Name.SanitizeName();
			this.Name = Utils.PopulateRequiredStringValue(this.Name, "name", typeof(MetricTelemetry).FullName);
			this.Properties.SanitizeProperties();
			this.Sum = Utils.SanitizeNanAndInfinity(this.Sum);
			int? num2;
			if (this.Count != null)
			{
				int? count = this.Count;
				int num = 0;
				if (!((count.GetValueOrDefault() <= num) & (count != null)))
				{
					num2 = this.Count;
					goto IL_00A7;
				}
			}
			num2 = new int?(1);
			IL_00A7:
			this.Count = num2;
			if (this.Min != null)
			{
				this.Min = new double?(Utils.SanitizeNanAndInfinity(this.Min.Value));
			}
			if (this.Max != null)
			{
				this.Max = new double?(Utils.SanitizeNanAndInfinity(this.Max.Value));
			}
			if (this.StandardDeviation != null)
			{
				this.StandardDeviation = new double?(Utils.SanitizeNanAndInfinity(this.StandardDeviation.Value));
			}
		}

		// Token: 0x040002D4 RID: 724
		internal const string TelemetryName = "Metric";

		// Token: 0x040002D5 RID: 725
		internal readonly MetricData Data;

		// Token: 0x040002D6 RID: 726
		internal readonly DataPoint Metric;

		// Token: 0x040002D7 RID: 727
		private IExtension extension;
	}
}
