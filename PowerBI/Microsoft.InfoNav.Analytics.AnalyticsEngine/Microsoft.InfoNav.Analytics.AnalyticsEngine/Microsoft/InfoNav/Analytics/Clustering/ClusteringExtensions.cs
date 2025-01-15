using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.AnalysisServices.Dax.ExtensionInterfaces;
using Microsoft.In4.AutoInsight.ComputationLibraries.Internal;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics.Clustering
{
	// Token: 0x0200003C RID: 60
	internal class ClusteringExtensions
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x000065B4 File Offset: 0x000047B4
		public IReadOnlyRowSchema PrepareClustering(IReadOnlyRowSchema inputTableSchema, IEnumerable<IReadOnlyRow> inputRoles, IEnumerable<IReadOnlyRow> outputRoles)
		{
			ITracer threadLocalTracer = TraceFactory.ThreadLocalTracer;
			if (this._isInitialized)
			{
				threadLocalTracer.TraceError("SpatialCluteringExtension: PrepareSpatialClustering was called more than once");
				Utils.ThrowExtensionException(ClusteringErrorType.PrepareSpatialClusteringCalledTwice);
			}
			this._isInitialized = true;
			this._inputTableSchema = inputTableSchema;
			Dictionary<string, List<int>> columnIndexesByRoles = inputTableSchema.GetColumnIndexesByRoles(inputRoles, threadLocalTracer, false);
			List<int> list;
			if (!columnIndexesByRoles.TryGetValue("Item", out list))
			{
				Utils.ThrowExtensionException(ClusteringErrorType.MissingRequiredItemColumn);
			}
			this._itemIndexes = list.ToArray();
			if (!columnIndexesByRoles.TryGetValue("Attribute", out list))
			{
				threadLocalTracer.TraceError("SpatialCluteringExtension: Missing {0} role column", new object[] { "Attribute" });
				Utils.ThrowExtensionException(ClusteringErrorType.MissingRequiredAttributeColumn);
			}
			this._attributeIndexes = list.ToArray();
			if (!inputTableSchema.AreAllNumeric(this._attributeIndexes))
			{
				threadLocalTracer.TraceError("SpatialCluteringExtension: One of the {0} columns is not of numeric type", new object[] { "Attribute" });
			}
			Dictionary<string, List<string>> columnNamesByRoles = outputRoles.GetColumnNamesByRoles(threadLocalTracer, true);
			List<string> list2;
			if (!columnNamesByRoles.TryGetValue("ClusterId", out list2) || list2.Count != 1)
			{
				threadLocalTracer.TraceError("SpatialCluteringExtension: Missing {0} column", new object[] { "ClusterId" });
				Utils.ThrowExtensionException(ClusteringErrorType.MissingOutputRole);
			}
			string text = list2[0];
			IExtensionService instance = ExtensionService.Instance;
			IRowSchemaFactory rowSchemaFactory = instance.RowSchemaFactory;
			IColumnFactory columnFactory = instance.ColumnFactory;
			this._outputRowSchema = rowSchemaFactory.Create();
			list = new List<int>();
			if (columnNamesByRoles.TryGetValue("NULL", out list2))
			{
				foreach (string text2 in list2)
				{
					int num = inputTableSchema.IndexOf(text2);
					if (num == -1)
					{
						threadLocalTracer.TraceError("SpatialCluteringExtension: Column {0} does not exist in the input table schema", new object[] { text2 });
						Utils.ThrowExtensionException(ClusteringErrorType.NonExistingColumn);
					}
					list.Add(num);
					IReadOnlyColumn readOnlyColumn = inputTableSchema[num];
					this._outputRowSchema.Add(readOnlyColumn);
				}
			}
			this._outputColumnIndexes = list.ToArray();
			IColumn column = columnFactory.Create(text, DbType.Int64);
			this._outputRowSchema.Add(column);
			this._clusterColumnIndex = this._outputRowSchema.IndexOf(text);
			return this._outputRowSchema;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000067C0 File Offset: 0x000049C0
		[ExtensionFunction(Name = "SpatialClustering", PrepareFunction = "PrepareClustering")]
		public IEnumerable<IReadOnlyRow> SpatialClustering([PassRowSchemaToPrepare] IEnumerable<IReadOnlyRow> inputTable, [PassValueToPrepare] IEnumerable<IReadOnlyRow> inputRoleMapping, [PassValueToPrepare] IEnumerable<IReadOnlyRow> outputRoleMapping, CancellationToken cancelToken)
		{
			YadingMultiDensityClusterer yadingMultiDensityClusterer = new YadingMultiDensityClusterer();
			return this.RunClustering(inputTable, cancelToken, yadingMultiDensityClusterer, null);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000067E0 File Offset: 0x000049E0
		[ExtensionFunction(Name = "KMeansClustering", PrepareFunction = "PrepareClustering")]
		public IEnumerable<IReadOnlyRow> KMeansClustering([PassRowSchemaToPrepare] IEnumerable<IReadOnlyRow> inputTable, [PassValueToPrepare] IEnumerable<IReadOnlyRow> inputRoleMapping, [PassValueToPrepare] IEnumerable<IReadOnlyRow> outputRoleMapping, long? requestedNumberOfClusters, CancellationToken cancelToken)
		{
			KMeansClusterer kmeansClusterer = new KMeansClusterer((requestedNumberOfClusters != null) ? ((int)requestedNumberOfClusters.Value) : (-1));
			return this.RunClustering(inputTable, cancelToken, kmeansClusterer, delegate(int numberOfDataPoints, IClusterer kMeansClusterer)
			{
				int numberOfClusters = kMeansClusterer.NumberOfClusters;
				if (numberOfClusters != -1 && numberOfClusters > numberOfDataPoints)
				{
					Utils.ThrowExtensionException(ClusteringErrorType.LessThanKDataPoints);
				}
			});
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00006830 File Offset: 0x00004A30
		private IEnumerable<IReadOnlyRow> RunClustering(IEnumerable<IReadOnlyRow> inputTable, CancellationToken cancelToken, IClusterer clusterer, Action<int, IClusterer> validator = null)
		{
			cancelToken.ThrowIfCancellationRequested();
			ITracer threadLocalTracer = TraceFactory.ThreadLocalTracer;
			string clustererAlgorithmName = clusterer.ClustererAlgorithmName;
			if (!this._isInitialized)
			{
				threadLocalTracer.TraceError("{0}: PrepareClustering function was not called", new object[] { clustererAlgorithmName });
				Utils.ThrowExtensionException(ClusteringErrorType.PrepareSpatialClusteringNotCalled);
			}
			List<IRow> list2;
			List<double[]> list = this.BufferDataPoints(inputTable, cancelToken, threadLocalTracer, out list2);
			cancelToken.ThrowIfCancellationRequested();
			MinMaxNormalizer.Normalize(list);
			ClusteringStatistics clusteringStatistics = new ClusteringStatistics
			{
				ClusteringAlgorithm = clustererAlgorithmName,
				RequestedNumberOfClusters = new int?(clusterer.NumberOfClusters),
				NumberOfDataPoints = new int?(list.Count),
				Dimension = new int?((list.Count > 0) ? list[0].Length : 0),
				Exception = null
			};
			if (validator != null)
			{
				validator(list.Count, clusterer);
			}
			IReadOnlyList<int> readOnlyList;
			try
			{
				Stopwatch stopwatch = Stopwatch.StartNew();
				readOnlyList = clusterer.GenerateLabels(list, cancelToken);
				stopwatch.Stop();
				clusteringStatistics.NumberOfOutliers = new int?(0);
				clusteringStatistics.NumberOfClusters = new int?(clusterer.NumberOfClusters);
				clusteringStatistics.EllapsedTimeInMs = new long?(stopwatch.ElapsedMilliseconds);
			}
			catch (Exception ex)
			{
				clusteringStatistics.Exception = ex;
				if (KMeansClusterer.IsKnownTlcException(ex))
				{
					Utils.ThrowExtensionException(ClusteringErrorType.LessThanKDataPoints);
				}
				if (!(ex is TransformException))
				{
					Utils.ThrowExtensionException(ClusteringErrorType.ClusteringAlgorithm, ex);
				}
				throw;
			}
			finally
			{
				threadLocalTracer.TraceInformation("ClusteringStatistcs:{0}", new object[] { clusteringStatistics.ToString() });
			}
			cancelToken.ThrowIfCancellationRequested();
			return this.GenerateOutputRows(list2, readOnlyList);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000069BC File Offset: 0x00004BBC
		private List<double[]> BufferDataPoints(IEnumerable<IReadOnlyRow> inputTable, CancellationToken cancelToken, ITracer tracer, out List<IRow> outputRows)
		{
			cancelToken.ThrowIfCancellationRequested();
			List<double[]> list = new List<double[]>();
			outputRows = new List<IRow>();
			int num = 0;
			tracer.TraceInformation("SpatialCluteringExtension: Doing clustering on {0} {1} columns and {2} {3} columns", new object[]
			{
				this._itemIndexes.Length,
				"Item",
				this._attributeIndexes.Length,
				"Attribute"
			});
			Stopwatch stopwatch = Stopwatch.StartNew();
			foreach (IReadOnlyRow readOnlyRow in inputTable)
			{
				num++;
				double[] array;
				if (!readOnlyRow.AreAllBlanks(this._itemIndexes) && this.TryGetDoubleValues(readOnlyRow, this._attributeIndexes, out array))
				{
					IRow row = this._outputRowSchema.CopyColumns(readOnlyRow, this._outputColumnIndexes);
					readOnlyRow.Pinned = false;
					outputRows.Add(row);
					list.Add(array);
				}
			}
			stopwatch.Stop();
			tracer.TraceInformation("CluteringExtensions: Finished buffering {0} valid rows (out of {1} original rows) in {2}ms", new object[] { outputRows.Count, num, stopwatch.ElapsedMilliseconds });
			if (list.Count > 1000000)
			{
				tracer.TraceInformation("CluteringExtensions: Not running clustering on {0} data points", new object[] { list.Count });
				Utils.ThrowExtensionException(ClusteringErrorType.TooManyDataPoints);
			}
			return list;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00006B24 File Offset: 0x00004D24
		private IEnumerable<IRow> GenerateOutputRows(IReadOnlyList<IRow> outputRows, IReadOnlyList<int> labels)
		{
			return outputRows.Zip(labels, delegate(IRow row, int label)
			{
				row[this._clusterColumnIndex] = label;
				return row;
			});
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00006B3C File Offset: 0x00004D3C
		private bool TryGetDoubleValues(IReadOnlyRow dataRow, int[] columnIndexes, out double[] point)
		{
			int num = columnIndexes.Length;
			point = new double[num];
			int num2 = 0;
			foreach (int num3 in columnIndexes)
			{
				DbType dataType = this._inputTableSchema[num3].DataType;
				double num4;
				if (!dataRow[num3].TryConvertNumericToDouble(dataType, out num4) || !num4.IsStrictlyFinite())
				{
					return false;
				}
				point[num2++] = num4;
			}
			return true;
		}

		// Token: 0x0400013C RID: 316
		private int[] _itemIndexes;

		// Token: 0x0400013D RID: 317
		private int[] _attributeIndexes;

		// Token: 0x0400013E RID: 318
		private int[] _outputColumnIndexes;

		// Token: 0x0400013F RID: 319
		private int _clusterColumnIndex;

		// Token: 0x04000140 RID: 320
		private bool _isInitialized;

		// Token: 0x04000141 RID: 321
		private IReadOnlyRowSchema _inputTableSchema;

		// Token: 0x04000142 RID: 322
		private IRowSchema _outputRowSchema;
	}
}
