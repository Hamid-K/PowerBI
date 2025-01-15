using System;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200008E RID: 142
	public sealed class ClusteringPerInstanceEvaluator : PerInstanceEvaluatorBase
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000F916 File Offset: 0x0000DB16
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("CLSTRINS", 65537U, 65537U, 65537U, "ClusteringPerInstance", null);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000F938 File Offset: 0x0000DB38
		public ClusteringPerInstanceEvaluator(IHostEnvironment env, ISchema schema, string scoreCol, int numClusters)
			: base(env, schema, scoreCol, null)
		{
			this.CheckInputColumnTypes(schema);
			this._numClusters = numClusters;
			this._types = new ColumnType[3];
			KeyType keyType = new KeyType(6, 0UL, this._numClusters, true);
			this._types[0] = keyType;
			this._types[1] = new VectorType(keyType, this._numClusters);
			this._types[2] = new VectorType(NumberType.R4, this._numClusters);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000F9B0 File Offset: 0x0000DBB0
		private ClusteringPerInstanceEvaluator(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
			: base(ctx, env, schema)
		{
			this.CheckInputColumnTypes(schema);
			this._numClusters = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._numClusters > 0);
			this._types = new ColumnType[3];
			KeyType keyType = new KeyType(6, 0UL, this._numClusters, true);
			this._types[0] = keyType;
			this._types[1] = new VectorType(keyType, this._numClusters);
			this._types[2] = new VectorType(NumberType.R4, this._numClusters);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000FA43 File Offset: 0x0000DC43
		public static ClusteringPerInstanceEvaluator Create(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(ClusteringPerInstanceEvaluator.GetVersionInfo());
			return new ClusteringPerInstanceEvaluator(ctx, env, schema);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000FA6F File Offset: 0x0000DC6F
		public override void Save(ModelSaveContext ctx)
		{
			base.Save(ctx);
			ctx.Writer.Write(this._numClusters);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000FAD0 File Offset: 0x0000DCD0
		public override Func<int, bool> GetDependencies(Func<int, bool> activeOutput)
		{
			return (int col) => col == this._scoreIndex && (activeOutput(0) || activeOutput(1) || activeOutput(2));
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000FCF4 File Offset: 0x0000DEF4
		public override Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeOutput)
		{
			Delegate[] array = new Delegate[3];
			if (!activeOutput(0) && !activeOutput(1) && !activeOutput(2))
			{
				return array;
			}
			long cachedPosition = -1L;
			VBuffer<float> scores = default(VBuffer<float>);
			float[] scoresArr = new float[this._numClusters];
			int[] sortedIndices = new int[this._numClusters];
			ValueGetter<VBuffer<float>> scoreGetter = input.GetGetter<VBuffer<float>>(this._scoreIndex);
			Action updateCacheIfNeeded = delegate
			{
				if (cachedPosition != input.Position)
				{
					scoreGetter.Invoke(ref scores);
					scores.CopyTo(scoresArr);
					int num = 0;
					foreach (int num2 in from i in Enumerable.Range(0, scoresArr.Length)
						orderby scoresArr[i]
						select i)
					{
						sortedIndices[num++] = num2;
					}
					cachedPosition = input.Position;
				}
			};
			if (activeOutput(0))
			{
				ValueGetter<uint> valueGetter = delegate(ref uint dst)
				{
					updateCacheIfNeeded();
					dst = (uint)(sortedIndices[0] + 1);
				};
				array[0] = valueGetter;
			}
			if (activeOutput(2))
			{
				ValueGetter<VBuffer<float>> valueGetter2 = delegate(ref VBuffer<float> dst)
				{
					updateCacheIfNeeded();
					float[] array2 = dst.Values;
					if (Utils.Size<float>(array2) < this._numClusters)
					{
						array2 = new float[this._numClusters];
					}
					for (int i = 0; i < this._numClusters; i++)
					{
						array2[i] = scores.GetItemOrDefault(sortedIndices[i]);
					}
					dst = new VBuffer<float>(this._numClusters, array2, null);
				};
				array[2] = valueGetter2;
			}
			if (activeOutput(1))
			{
				ValueGetter<VBuffer<uint>> valueGetter3 = delegate(ref VBuffer<uint> dst)
				{
					updateCacheIfNeeded();
					uint[] array3 = dst.Values;
					if (Utils.Size<uint>(array3) < this._numClusters)
					{
						array3 = new uint[this._numClusters];
					}
					for (int j = 0; j < this._numClusters; j++)
					{
						array3[j] = (uint)(sortedIndices[j] + 1);
					}
					dst = new VBuffer<uint>(this._numClusters, array3, null);
				};
				array[1] = valueGetter3;
			}
			return array;
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000FE18 File Offset: 0x0000E018
		public override RowMapperColumnInfo[] GetOutputColumns()
		{
			RowMapperColumnInfo[] array = new RowMapperColumnInfo[3];
			array[0] = new RowMapperColumnInfo("ClusterId", this._types[0], null);
			VectorType vectorType = new VectorType(TextType.Instance, this._numClusters);
			ColumnMetadataInfo columnMetadataInfo = new ColumnMetadataInfo("SortedClusters");
			columnMetadataInfo.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter(this._numClusters, "Cluster")));
			ColumnMetadataInfo columnMetadataInfo2 = new ColumnMetadataInfo("SortedScores");
			columnMetadataInfo2.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter(this._numClusters, "Score")));
			array[1] = new RowMapperColumnInfo("SortedClusters", this._types[1], columnMetadataInfo);
			array[2] = new RowMapperColumnInfo("SortedScores", this._types[2], columnMetadataInfo2);
			return array;
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000FF60 File Offset: 0x0000E160
		private MetadataUtils.MetadataGetter<VBuffer<DvText>> CreateSlotNamesGetter(int numTopClusters, string suffix)
		{
			return delegate(int col, ref VBuffer<DvText> dst)
			{
				DvText[] array = dst.Values;
				if (Utils.Size<DvText>(array) < numTopClusters)
				{
					array = new DvText[numTopClusters];
				}
				for (int i = 1; i <= numTopClusters; i++)
				{
					array[i - 1] = new DvText(string.Format("#{0} {1}", i, suffix));
				}
				dst = new VBuffer<DvText>(numTopClusters, array, null);
			};
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000FF90 File Offset: 0x0000E190
		private void CheckInputColumnTypes(ISchema schema)
		{
			ColumnType columnType = schema.GetColumnType(this._scoreIndex);
			if (!columnType.IsKnownSizeVector || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type {1}, but must be a float vector of known-size", new object[] { this._scoreCol, columnType });
			}
		}

		// Token: 0x0400010E RID: 270
		public const string LoaderSignature = "ClusteringPerInstance";

		// Token: 0x0400010F RID: 271
		private const int ClusterIdCol = 0;

		// Token: 0x04000110 RID: 272
		private const int SortedClusterCol = 1;

		// Token: 0x04000111 RID: 273
		private const int SortedClusterScoreCol = 2;

		// Token: 0x04000112 RID: 274
		public const string ClusterId = "ClusterId";

		// Token: 0x04000113 RID: 275
		public const string SortedClusters = "SortedClusters";

		// Token: 0x04000114 RID: 276
		public const string SortedClusterScores = "SortedScores";

		// Token: 0x04000115 RID: 277
		private readonly int _numClusters;

		// Token: 0x04000116 RID: 278
		private readonly ColumnType[] _types;
	}
}
