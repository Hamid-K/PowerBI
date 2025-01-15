using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace Microsoft.ApplicationInsights.DataContracts
{
	// Token: 0x020000D8 RID: 216
	[Obsolete("Use MetricTelemetry instead.")]
	public sealed class PerformanceCounterTelemetry : ITelemetry, ISupportProperties, IAiSerializableTelemetry
	{
		// Token: 0x060007AA RID: 1962 RVA: 0x00019A86 File Offset: 0x00017C86
		public PerformanceCounterTelemetry()
		{
			this.Data = new MetricTelemetry();
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x00019AAF File Offset: 0x00017CAF
		public PerformanceCounterTelemetry(string categoryName, string counterName, string instanceName, double value)
			: this()
		{
			this.CategoryName = categoryName;
			this.CounterName = counterName;
			this.InstanceName = instanceName;
			this.Value = value;
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x00019AD4 File Offset: 0x00017CD4
		private PerformanceCounterTelemetry(PerformanceCounterTelemetry source)
		{
			this.Data = (MetricTelemetry)source.Data.DeepClone();
			this.categoryName = source.categoryName;
			this.counterName = source.counterName;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x00019B2B File Offset: 0x00017D2B
		string IAiSerializableTelemetry.TelemetryName
		{
			get
			{
				return ((IAiSerializableTelemetry)this.Data).TelemetryName;
			}
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x00019B38 File Offset: 0x00017D38
		string IAiSerializableTelemetry.BaseType
		{
			get
			{
				return ((IAiSerializableTelemetry)this.Data).BaseType;
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x00019B45 File Offset: 0x00017D45
		// (set) Token: 0x060007B0 RID: 1968 RVA: 0x00019B52 File Offset: 0x00017D52
		public DateTimeOffset Timestamp
		{
			get
			{
				return this.Data.Timestamp;
			}
			set
			{
				this.Data.Timestamp = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x060007B1 RID: 1969 RVA: 0x00019B60 File Offset: 0x00017D60
		// (set) Token: 0x060007B2 RID: 1970 RVA: 0x00019B6D File Offset: 0x00017D6D
		public string Sequence
		{
			get
			{
				return this.Data.Sequence;
			}
			set
			{
				this.Data.Sequence = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x00019B7B File Offset: 0x00017D7B
		public TelemetryContext Context
		{
			get
			{
				return this.Data.Context;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x00019B88 File Offset: 0x00017D88
		// (set) Token: 0x060007B5 RID: 1973 RVA: 0x00019B95 File Offset: 0x00017D95
		public IExtension Extension
		{
			get
			{
				return this.Data.Extension;
			}
			set
			{
				this.Data.Extension = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x00019BA3 File Offset: 0x00017DA3
		// (set) Token: 0x060007B7 RID: 1975 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public double Value
		{
			get
			{
				return this.Data.Value;
			}
			set
			{
				this.Data.Value = value;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x00019BBE File Offset: 0x00017DBE
		// (set) Token: 0x060007B9 RID: 1977 RVA: 0x00019BC6 File Offset: 0x00017DC6
		public string CategoryName
		{
			get
			{
				return this.categoryName;
			}
			set
			{
				this.categoryName = value;
				this.UpdateName();
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x00019BD5 File Offset: 0x00017DD5
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00019BDD File Offset: 0x00017DDD
		public string CounterName
		{
			get
			{
				return this.counterName;
			}
			set
			{
				this.counterName = value;
				this.UpdateName();
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x00019BEC File Offset: 0x00017DEC
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00019C16 File Offset: 0x00017E16
		public string InstanceName
		{
			get
			{
				if (this.Properties.ContainsKey("CounterInstanceName"))
				{
					return this.Properties["CounterInstanceName"];
				}
				return string.Empty;
			}
			set
			{
				this.Properties["CounterInstanceName"] = value;
				this.UpdateName();
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00019C2F File Offset: 0x00017E2F
		public IDictionary<string, string> Properties
		{
			get
			{
				return this.Data.Properties;
			}
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x00019C3C File Offset: 0x00017E3C
		public ITelemetry DeepClone()
		{
			return new PerformanceCounterTelemetry(this);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x00019C44 File Offset: 0x00017E44
		public void SerializeData(ISerializationWriter serializationWriter)
		{
			this.Data.SerializeData(serializationWriter);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00019C52 File Offset: 0x00017E52
		void ITelemetry.Sanitize()
		{
			((ITelemetry)this.Data).Sanitize();
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00019C60 File Offset: 0x00017E60
		private void UpdateName()
		{
			if (this.categoryName == "Processor")
			{
				this.Data.Name = "\\" + this.categoryName + "(_Total)\\" + this.counterName;
				return;
			}
			if (this.categoryName == "Process")
			{
				this.Data.Name = "\\" + this.categoryName + "(??APP_WIN32_PROC??)\\" + this.counterName;
				return;
			}
			if (this.categoryName == "ASP.NET Applications")
			{
				this.Data.Name = "\\" + this.categoryName + "(??APP_W3SVC_PROC??)\\" + this.counterName;
				return;
			}
			if (this.categoryName == ".NET CLR Exceptions")
			{
				this.Data.Name = "\\" + this.categoryName + "(??APP_CLR_PROC??)\\" + this.counterName;
				return;
			}
			this.Data.Name = (string.IsNullOrEmpty(this.InstanceName) ? (this.Data.Name = "\\" + this.categoryName + "\\" + this.counterName) : (this.Data.Name = string.Concat(new string[] { "\\", this.categoryName, "(", this.InstanceName, ")\\", this.counterName })));
		}

		// Token: 0x040002E9 RID: 745
		internal readonly MetricTelemetry Data;

		// Token: 0x040002EA RID: 746
		private string categoryName = string.Empty;

		// Token: 0x040002EB RID: 747
		private string counterName = string.Empty;
	}
}
