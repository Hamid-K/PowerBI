using System;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DB0 RID: 3504
	internal class MetricGranularityTimeGranularity : ITimeGranularity, IEquatable<ITimeGranularity>, IJsonSerializable
	{
		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x06005F39 RID: 24377 RVA: 0x0014839D File Offset: 0x0014659D
		// (set) Token: 0x06005F3A RID: 24378 RVA: 0x001483A5 File Offset: 0x001465A5
		public string Granularity { get; set; }

		// Token: 0x06005F3B RID: 24379 RVA: 0x001483AE File Offset: 0x001465AE
		public override int GetHashCode()
		{
			return this.Granularity.GetHashCode();
		}

		// Token: 0x06005F3C RID: 24380 RVA: 0x001483BB File Offset: 0x001465BB
		public override bool Equals(object other)
		{
			return this.Equals(other as MetricGranularityTimeGranularity);
		}

		// Token: 0x06005F3D RID: 24381 RVA: 0x001483BB File Offset: 0x001465BB
		public bool Equals(ITimeGranularity other)
		{
			return this.Equals(other as MetricGranularityTimeGranularity);
		}

		// Token: 0x06005F3E RID: 24382 RVA: 0x001483C9 File Offset: 0x001465C9
		public bool Equals(MetricGranularityTimeGranularity other)
		{
			return other != null && this.Granularity == other.Granularity;
		}

		// Token: 0x06005F3F RID: 24383 RVA: 0x001483E1 File Offset: 0x001465E1
		public override string ToString()
		{
			return this.Granularity;
		}

		// Token: 0x06005F40 RID: 24384 RVA: 0x001483E9 File Offset: 0x001465E9
		public string ToJson()
		{
			return JsonFormatter.FormatString(this.Granularity);
		}

		// Token: 0x0400345F RID: 13407
		public static readonly MetricGranularityTimeGranularity PT1S = new MetricGranularityTimeGranularity
		{
			Granularity = "PT1S"
		};

		// Token: 0x04003460 RID: 13408
		public static readonly MetricGranularityTimeGranularity PT1M = new MetricGranularityTimeGranularity
		{
			Granularity = "PT1M"
		};

		// Token: 0x04003461 RID: 13409
		public static readonly MetricGranularityTimeGranularity PT5M = new MetricGranularityTimeGranularity
		{
			Granularity = "PT5M"
		};

		// Token: 0x04003462 RID: 13410
		public static readonly MetricGranularityTimeGranularity PT1H = new MetricGranularityTimeGranularity
		{
			Granularity = "PT1H"
		};

		// Token: 0x04003463 RID: 13411
		public static readonly MetricGranularityTimeGranularity P1D = new MetricGranularityTimeGranularity
		{
			Granularity = "P1D"
		};

		// Token: 0x04003464 RID: 13412
		public static readonly MetricGranularityTimeGranularity P7D = new MetricGranularityTimeGranularity
		{
			Granularity = "P7D"
		};

		// Token: 0x04003465 RID: 13413
		public static readonly MetricGranularityTimeGranularity P1M = new MetricGranularityTimeGranularity
		{
			Granularity = "P1M"
		};

		// Token: 0x04003466 RID: 13414
		public static readonly MetricGranularityTimeGranularity P1Y = new MetricGranularityTimeGranularity
		{
			Granularity = "P1Y"
		};

		// Token: 0x04003467 RID: 13415
		public static readonly MetricGranularityTimeGranularity All = new MetricGranularityTimeGranularity
		{
			Granularity = "ALL"
		};
	}
}
