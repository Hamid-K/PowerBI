using System;
using System.Collections.Generic;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Clustering
{
	// Token: 0x0200003D RID: 61
	internal sealed class ClusteringStatistics
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00006BC6 File Offset: 0x00004DC6
		// (set) Token: 0x06000100 RID: 256 RVA: 0x00006BCE File Offset: 0x00004DCE
		internal int? NumberOfClusters { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00006BD7 File Offset: 0x00004DD7
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00006BDF File Offset: 0x00004DDF
		internal int? NumberOfDataPoints { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00006BE8 File Offset: 0x00004DE8
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00006BF0 File Offset: 0x00004DF0
		internal int? Dimension { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00006BF9 File Offset: 0x00004DF9
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00006C01 File Offset: 0x00004E01
		internal int? NumberOfOutliers { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00006C0A File Offset: 0x00004E0A
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00006C12 File Offset: 0x00004E12
		internal long? EllapsedTimeInMs { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00006C1B File Offset: 0x00004E1B
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00006C23 File Offset: 0x00004E23
		internal string ClusteringAlgorithm { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006C2C File Offset: 0x00004E2C
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00006C34 File Offset: 0x00004E34
		internal Exception Exception { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00006C3D File Offset: 0x00004E3D
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00006C45 File Offset: 0x00004E45
		internal int? RequestedNumberOfClusters { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00006C4E File Offset: 0x00004E4E
		internal int NumberOfThreads
		{
			get
			{
				return Environment.ProcessorCount;
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006C58 File Offset: 0x00004E58
		internal void FireTelemetryEvent(ITelemetryService telemetryService)
		{
			List<string> telemetryParameters = this.GetTelemetryParameters();
			string text = "ClusteringProcess";
			object[] array = telemetryParameters.ToArray();
			telemetryService.FireEvent(text, array);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006C80 File Offset: 0x00004E80
		private List<string> GetTelemetryParameters()
		{
			List<string> list = new List<string>();
			if (this.NumberOfClusters != null)
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("NumberOfClusters", this.NumberOfClusters));
			}
			if (this.NumberOfDataPoints != null)
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("NumberOfDataPoints", this.NumberOfDataPoints));
			}
			if (this.Dimension != null)
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("Dimension", this.Dimension));
			}
			if (this.NumberOfOutliers != null)
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("NumberOfOutliers", this.NumberOfOutliers));
			}
			if (!string.IsNullOrEmpty(this.ClusteringAlgorithm))
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("Algorithm", this.ClusteringAlgorithm));
			}
			if (this.RequestedNumberOfClusters != null)
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("RequestedNumberOfClusters", this.RequestedNumberOfClusters));
			}
			if (this.EllapsedTimeInMs != null)
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("EllapsedTimeInMs", this.EllapsedTimeInMs));
			}
			if (this.Exception != null)
			{
				list.Add(ClusteringStatistics.FormatPropDetailValue("Exception", this.Exception.ToString()));
			}
			list.Add(ClusteringStatistics.FormatPropDetailValue("NumberOfThreads", this.NumberOfThreads.ToStringInvariant()));
			return list;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public override string ToString()
		{
			List<string> telemetryParameters = this.GetTelemetryParameters();
			return string.Join("_", telemetryParameters);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006E17 File Offset: 0x00005017
		private static string FormatPropDetailValue(string propertyName, object value)
		{
			return string.Join(":", new string[]
			{
				propertyName,
				(value != null) ? value.ToString() : "null"
			});
		}

		// Token: 0x04000143 RID: 323
		private const string NumberOfClustersProp = "NumberOfClusters";

		// Token: 0x04000144 RID: 324
		private const string NumberOfDataPointsProp = "NumberOfDataPoints";

		// Token: 0x04000145 RID: 325
		private const string DimensionProp = "Dimension";

		// Token: 0x04000146 RID: 326
		private const string NumberOfOutliersProp = "NumberOfOutliers";

		// Token: 0x04000147 RID: 327
		private const string ExceptionProp = "Exception";

		// Token: 0x04000148 RID: 328
		private const string Algorithm = "Algorithm";

		// Token: 0x04000149 RID: 329
		private const string RequestedNumberOfClustersProp = "RequestedNumberOfClusters";

		// Token: 0x0400014A RID: 330
		private const string EllapsedTimeInMsProp = "EllapsedTimeInMs";

		// Token: 0x0400014B RID: 331
		private const string NumberOfThreadsProp = "NumberOfThreads";
	}
}
