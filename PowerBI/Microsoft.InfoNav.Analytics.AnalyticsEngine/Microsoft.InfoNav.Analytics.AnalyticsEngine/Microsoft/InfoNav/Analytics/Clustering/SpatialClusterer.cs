using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.In4.AutoInsight.ComputationLibraries.Internal;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Clustering
{
	// Token: 0x0200003E RID: 62
	internal sealed class SpatialClusterer : DataTransform
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00006E48 File Offset: 0x00005048
		internal SpatialClusterer(ServiceRuntimeContext context)
			: base(context)
		{
			this._tracer = context.Tracer;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00006E60 File Offset: 0x00005060
		public override SchemaTransformResult GetSchema(SchemaTransformContext context)
		{
			return this.ServiceRuntimeContext.TelemetryService.RunInActivity<SchemaTransformResult>("SpatialClusteringGetSchema", () => this.GetSchemaCore(context));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00006EA4 File Offset: 0x000050A4
		public override Task<DataTransformResult> ExecuteAsync(DataTransformExecutionContext context)
		{
			return Task.FromResult<DataTransformResult>(this.ServiceRuntimeContext.TelemetryService.RunInActivity<DataTransformResult>("SpatialClusteringExecute", () => this.ExecuteCore(context)));
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00006EEC File Offset: 0x000050EC
		private SchemaTransformResult GetSchemaCore(SchemaTransformContext context)
		{
			int[] array;
			int[] array2;
			SpatialClusterer.ValidateInputSchema(context.Schema, this._tracer, out array, out array2);
			IColumn column = context.Schema.CreateColumn(context.Schema.MakeUniqueName("ClusterId", null), DataType.Int64, "ClusterId", null);
			return new SchemaTransformResult(context.Schema.AddColumns(column.ArrayWrap<IColumn>()));
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00006F48 File Offset: 0x00005148
		private DataTransformResult ExecuteCore(DataTransformExecutionContext context)
		{
			int[] array;
			int[] array2;
			SpatialClusterer.ValidateInputSchema(context.InputSchema, this._tracer, out array, out array2);
			this._tracer.Trace(TraceLevel.Info, "SpatialClusteringTransform: Doing clustering on {0} {1} columns and {2} {3} columns", array2.Length, "Item", array.Length, "Attribute");
			Stopwatch stopwatch = Stopwatch.StartNew();
			int num = 0;
			List<ClusteringDataRow> list = new List<ClusteringDataRow>();
			foreach (IDataRow dataRow in context.InputRows)
			{
				double[] array3;
				bool flag = dataRow.TryGetAsDouble(array, out array3);
				if (flag)
				{
					flag = !dataRow.AreAllBlanks(array2);
				}
				ClusteringDataRow clusteringDataRow = (flag ? new ClusteringDataRow(dataRow, true, array3) : new ClusteringDataRow(dataRow, false, null));
				list.Add(clusteringDataRow);
				if (clusteringDataRow.UseForClustering)
				{
					num++;
				}
			}
			stopwatch.Stop();
			this._tracer.Trace(TraceLevel.Info, "SpatialClusteringTransform: Finished buffering {0} valid rows (out of {1} original rows) in {2}ms", num, list.Count, stopwatch.ElapsedMilliseconds);
			stopwatch.Restart();
			double[][] array4;
			SpatialClusterer.GenerateClusteringInputArrays(list, num, out array4);
			MinMaxNormalizer.Normalize(array4);
			DBScanSingleDensityClusterer dbscanSingleDensityClusterer = new DBScanSingleDensityClusterer();
			IReadOnlyList<int> readOnlyList;
			try
			{
				readOnlyList = dbscanSingleDensityClusterer.GenerateLabels(array4, CancellationToken.None);
				stopwatch.Stop();
				this._tracer.Trace(TraceLevel.Info, "SpatialClusteringTransform: Found {0} clusters for {1} data points in {2}ms", dbscanSingleDensityClusterer.NumberOfClusters, num, stopwatch.ElapsedMilliseconds);
			}
			catch (Exception ex)
			{
				throw new TransformException(StringUtil.FormatInvariant("Error occured while trying to cluster {0} data points using algorithm {1}", array4.Length, dbscanSingleDensityClusterer.ClustererAlgorithmName), ex);
			}
			return new DataTransformResult(SpatialClusterer.GenerateRows(list, readOnlyList));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007108 File Offset: 0x00005308
		private static void ValidateInputSchema(ISchemaRow inputSchema, ITracer tracer, out int[] attributeIndexes, out int[] itemIndexes)
		{
			inputSchema.ValidateRequiredRole("Attribute", out attributeIndexes, (DataType t) => t.IsNumeric(), true);
			if (attributeIndexes.Length < 2)
			{
				tracer.Trace(TraceLevel.Error, "SpatialClusteringTransform: InputSchema is missing role {0}", new string[] { "Attribute" });
				throw new TransformException(StringUtil.FormatInvariant("InputSchema is missing role {0}", "Attribute"));
			}
			inputSchema.ValidateRequiredRole("Item", out itemIndexes, null, true);
			if (itemIndexes.Length < 1)
			{
				throw new TransformException(StringUtil.FormatInvariant("InputSchema is missing role {0}", "Item"));
			}
			inputSchema.ValidateAbsentRole("ClusterId");
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000071AC File Offset: 0x000053AC
		private static void GenerateClusteringInputArrays(IReadOnlyList<ClusteringDataRow> bufferedRows, int clusteringRowCount, out double[][] dataPoints)
		{
			int count = bufferedRows.Count;
			dataPoints = new double[clusteringRowCount][];
			int i = 0;
			int num = 0;
			while (i < count)
			{
				ClusteringDataRow clusteringDataRow = bufferedRows[i];
				if (clusteringDataRow.UseForClustering)
				{
					dataPoints[num] = clusteringDataRow.ClusteringAttributes;
					num++;
				}
				i++;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000071F4 File Offset: 0x000053F4
		private static IEnumerable<IDataRow> GenerateRows(IReadOnlyList<ClusteringDataRow> bufferedRows, IReadOnlyList<int> clusterLabels)
		{
			int rowCount = bufferedRows.Count;
			int labelCount = clusterLabels.Count;
			int rowIndex = 0;
			int clusteringRowIndex = 0;
			while (rowIndex < rowCount)
			{
				ClusteringDataRow clusteringDataRow = bufferedRows[rowIndex];
				bool isClusteringRow = clusteringDataRow.UseForClustering;
				object[] array;
				if (!isClusteringRow || clusteringRowIndex >= labelCount)
				{
					array = SpatialClusterer.BlankClusterId;
				}
				else
				{
					(array = new object[1])[0] = (long)clusterLabels[clusteringRowIndex];
				}
				object[] array2 = array;
				yield return clusteringDataRow.SourceDataRow.AddColumns(array2);
				int num;
				if (isClusteringRow)
				{
					num = clusteringRowIndex;
					clusteringRowIndex = num + 1;
				}
				num = rowIndex;
				rowIndex = num + 1;
			}
			yield break;
		}

		// Token: 0x04000154 RID: 340
		private const int MinimumAttributeColumns = 2;

		// Token: 0x04000155 RID: 341
		private const int MinimumItemColumns = 1;

		// Token: 0x04000156 RID: 342
		private static readonly object[] BlankClusterId = new object[1];

		// Token: 0x04000157 RID: 343
		private readonly ITracer _tracer;
	}
}
