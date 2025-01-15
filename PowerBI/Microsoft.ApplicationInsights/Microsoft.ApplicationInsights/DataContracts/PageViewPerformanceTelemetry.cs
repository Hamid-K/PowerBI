using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.ApplicationInsights.Extensibility.Implementation.External;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D6 RID: 214
	public sealed class PageViewPerformanceTelemetry : ITelemetry, ISupportProperties, ISupportSampling, IAiSerializableTelemetry
	{
		// Token: 0x06000769 RID: 1897 RVA: 0x0001950F File Offset: 0x0001770F
		public PageViewPerformanceTelemetry()
		{
			this.Data = new PageViewPerfData();
			this.Context = new TelemetryContext(this.Data.properties);
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x00019538 File Offset: 0x00017738
		public PageViewPerformanceTelemetry(string pageName)
			: this()
		{
			this.Name = pageName;
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x00019548 File Offset: 0x00017748
		private PageViewPerformanceTelemetry(PageViewPerformanceTelemetry source)
		{
			this.Data = source.Data.DeepClone();
			this.Context = source.Context.DeepClone(this.Data.properties);
			IExtension extension = source.extension;
			this.extension = ((extension != null) ? extension.DeepClone() : null);
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x000195A0 File Offset: 0x000177A0
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return "PageViewPerformance";
			}
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x000195A7 File Offset: 0x000177A7
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return "PageViewPerformanceData";
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x000195AE File Offset: 0x000177AE
		// (set) Token: 0x0600076F RID: 1903 RVA: 0x000195B6 File Offset: 0x000177B6
		public DateTimeOffset Timestamp { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x000195BF File Offset: 0x000177BF
		// (set) Token: 0x06000771 RID: 1905 RVA: 0x000195C7 File Offset: 0x000177C7
		public string Sequence { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x000195D0 File Offset: 0x000177D0
		// (set) Token: 0x06000773 RID: 1907 RVA: 0x000195D8 File Offset: 0x000177D8
		public TelemetryContext Context { get; private set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x000195E1 File Offset: 0x000177E1
		// (set) Token: 0x06000775 RID: 1909 RVA: 0x000195E9 File Offset: 0x000177E9
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

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x000195F2 File Offset: 0x000177F2
		// (set) Token: 0x06000777 RID: 1911 RVA: 0x000195FF File Offset: 0x000177FF
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

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0001960D File Offset: 0x0001780D
		// (set) Token: 0x06000779 RID: 1913 RVA: 0x0001961A File Offset: 0x0001781A
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

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x00019628 File Offset: 0x00017828
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0001964F File Offset: 0x0001784F
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

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600077C RID: 1916 RVA: 0x00019678 File Offset: 0x00017878
		// (set) Token: 0x0600077D RID: 1917 RVA: 0x0001968A File Offset: 0x0001788A
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

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600077E RID: 1918 RVA: 0x000196A4 File Offset: 0x000178A4
		// (set) Token: 0x0600077F RID: 1919 RVA: 0x000196B6 File Offset: 0x000178B6
		public TimeSpan DomProcessing
		{
			get
			{
				return Utils.ValidateDuration(this.Data.domProcessing);
			}
			set
			{
				this.Data.domProcessing = value.ToString();
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x000196D0 File Offset: 0x000178D0
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x000196E2 File Offset: 0x000178E2
		public TimeSpan PerfTotal
		{
			get
			{
				return Utils.ValidateDuration(this.Data.perfTotal);
			}
			set
			{
				this.Data.perfTotal = value.ToString();
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x000196FC File Offset: 0x000178FC
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0001970E File Offset: 0x0001790E
		public TimeSpan NetworkConnect
		{
			get
			{
				return Utils.ValidateDuration(this.Data.networkConnect);
			}
			set
			{
				this.Data.networkConnect = value.ToString();
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x00019728 File Offset: 0x00017928
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0001973A File Offset: 0x0001793A
		public TimeSpan SentRequest
		{
			get
			{
				return Utils.ValidateDuration(this.Data.sentRequest);
			}
			set
			{
				this.Data.sentRequest = value.ToString();
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x00019754 File Offset: 0x00017954
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x00019766 File Offset: 0x00017966
		public TimeSpan ReceivedResponse
		{
			get
			{
				return Utils.ValidateDuration(this.Data.receivedResponse);
			}
			set
			{
				this.Data.receivedResponse = value.ToString();
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000788 RID: 1928 RVA: 0x00019780 File Offset: 0x00017980
		public IDictionary<string, double> Metrics
		{
			get
			{
				return this.Data.measurements;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000789 RID: 1929 RVA: 0x0001978D File Offset: 0x0001798D
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.properties;
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600078A RID: 1930 RVA: 0x0001979A File Offset: 0x0001799A
		// (set) Token: 0x0600078B RID: 1931 RVA: 0x000197A2 File Offset: 0x000179A2
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

		// Token: 0x0600078C RID: 1932 RVA: 0x000197AB File Offset: 0x000179AB
		public ITelemetry DeepClone()
		{
			return new PageViewPerformanceTelemetry(this);
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x000197B4 File Offset: 0x000179B4
		void ITelemetry.Sanitize()
		{
			this.Name = this.Name.SanitizeName();
			this.Name = Utils.PopulateRequiredStringValue(this.Name, "name", typeof(PageViewTelemetry).FullName);
			this.Properties.SanitizeProperties();
			this.Metrics.SanitizeMeasurements();
			this.Url = this.Url.SanitizeUri();
			this.Id.SanitizeName();
		}

		// Token: 0x0600078E RID: 1934 RVA: 0x0001982A File Offset: 0x00017A2A
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			serializationWriter.WriteProperty(this.Data);
		}

		// Token: 0x040002DB RID: 731
		internal const string TelemetryName = "PageViewPerformance";

		// Token: 0x040002DC RID: 732
		internal readonly PageViewPerfData Data;

		// Token: 0x040002DD RID: 733
		private IExtension extension;

		// Token: 0x040002DE RID: 734
		private double? samplingPercentage;
	}
}
