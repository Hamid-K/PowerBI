using System;
using System.Linq;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002D3 RID: 723
	public sealed class MultiClassPerInstanceEvaluator : PerInstanceEvaluatorBase
	{
		// Token: 0x0600108D RID: 4237 RVA: 0x0005BBB2 File Offset: 0x00059DB2
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("MLTIINST", 65537U, 65537U, 65537U, "MulticlassPerInstance", null);
		}

		// Token: 0x0600108E RID: 4238 RVA: 0x0005BBD4 File Offset: 0x00059DD4
		public MultiClassPerInstanceEvaluator(IHostEnvironment env, ISchema schema, string scoreCol, string labelCol, int numClasses)
			: base(env, schema, scoreCol, labelCol)
		{
			this.CheckInputColumnTypes(schema);
			this._numClasses = numClasses;
			this._types = new ColumnType[4];
			KeyType keyType = new KeyType(6, 0UL, this._numClasses, true);
			this._types[0] = keyType;
			this._types[1] = NumberType.R8;
			this._types[2] = new VectorType(NumberType.R4, this._numClasses);
			this._types[3] = new VectorType(keyType, this._numClasses);
		}

		// Token: 0x0600108F RID: 4239 RVA: 0x0005BC5C File Offset: 0x00059E5C
		private MultiClassPerInstanceEvaluator(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
			: base(ctx, env, schema)
		{
			this.CheckInputColumnTypes(schema);
			this._numClasses = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(this._host, this._numClasses > 0);
			this._types = new ColumnType[4];
			KeyType keyType = new KeyType(6, 0UL, this._numClasses, true);
			this._types[0] = keyType;
			this._types[1] = NumberType.R8;
			this._types[2] = new VectorType(NumberType.R4, this._numClasses);
			this._types[3] = new VectorType(keyType, this._numClasses);
		}

		// Token: 0x06001090 RID: 4240 RVA: 0x0005BCFC File Offset: 0x00059EFC
		public static MultiClassPerInstanceEvaluator Create(ModelLoadContext ctx, IHostEnvironment env, ISchema schema)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(MultiClassPerInstanceEvaluator.GetVersionInfo());
			return new MultiClassPerInstanceEvaluator(ctx, env, schema);
		}

		// Token: 0x06001091 RID: 4241 RVA: 0x0005BD28 File Offset: 0x00059F28
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(MultiClassPerInstanceEvaluator.GetVersionInfo());
			base.Save(ctx);
			ctx.Writer.Write(this._numClasses);
		}

		// Token: 0x06001092 RID: 4242 RVA: 0x0005BDE0 File Offset: 0x00059FE0
		public override Func<int, bool> GetDependencies(Func<int, bool> activeOutput)
		{
			return (int col) => (col == this._labelIndex && activeOutput(1)) || (col == this._scoreIndex && (activeOutput(0) || activeOutput(2) || activeOutput(3) || activeOutput(1)));
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0005C09C File Offset: 0x0005A29C
		public override Delegate[] CreateGetters(IRowCursor input, Func<int, bool> activeOutput)
		{
			MultiClassPerInstanceEvaluator.<>c__DisplayClassf CS$<>8__locals1 = new MultiClassPerInstanceEvaluator.<>c__DisplayClassf();
			CS$<>8__locals1.input = input;
			CS$<>8__locals1.<>4__this = this;
			Delegate[] array = new Delegate[4];
			if (!activeOutput(0) && !activeOutput(3) && !activeOutput(2) && !activeOutput(1))
			{
				return array;
			}
			CS$<>8__locals1.cachedPosition = -1L;
			CS$<>8__locals1.scores = default(VBuffer<float>);
			CS$<>8__locals1.label = 0f;
			CS$<>8__locals1.scoresArr = new float[this._numClasses];
			CS$<>8__locals1.sortedIndices = new int[this._numClasses];
			MultiClassPerInstanceEvaluator.<>c__DisplayClassf CS$<>8__locals2 = CS$<>8__locals1;
			ValueGetter<float> valueGetter;
			if (!activeOutput(1))
			{
				valueGetter = delegate(ref float dst)
				{
					dst = float.NaN;
				};
			}
			else
			{
				valueGetter = RowCursorUtils.GetLabelGetter(CS$<>8__locals1.input, this._labelIndex);
			}
			CS$<>8__locals2.labelGetter = valueGetter;
			CS$<>8__locals1.scoreGetter = CS$<>8__locals1.input.GetGetter<VBuffer<float>>(this._scoreIndex);
			CS$<>8__locals1.updateCacheIfNeeded = delegate
			{
				if (CS$<>8__locals1.cachedPosition != CS$<>8__locals1.input.Position)
				{
					CS$<>8__locals1.labelGetter.Invoke(ref CS$<>8__locals1.label);
					CS$<>8__locals1.scoreGetter.Invoke(ref CS$<>8__locals1.scores);
					CS$<>8__locals1.scores.CopyTo(CS$<>8__locals1.scoresArr);
					int num = 0;
					foreach (int num2 in from i in Enumerable.Range(0, CS$<>8__locals1.scoresArr.Length)
						orderby CS$<>8__locals1.scoresArr[i] descending
						select i)
					{
						CS$<>8__locals1.sortedIndices[num++] = num2;
					}
					CS$<>8__locals1.cachedPosition = CS$<>8__locals1.input.Position;
				}
			};
			if (activeOutput(0))
			{
				ValueGetter<uint> valueGetter2 = delegate(ref uint dst)
				{
					CS$<>8__locals1.updateCacheIfNeeded();
					dst = (uint)(CS$<>8__locals1.sortedIndices[0] + 1);
				};
				array[0] = valueGetter2;
			}
			if (activeOutput(2))
			{
				ValueGetter<VBuffer<float>> valueGetter3 = delegate(ref VBuffer<float> dst)
				{
					CS$<>8__locals1.updateCacheIfNeeded();
					float[] array2 = dst.Values;
					if (Utils.Size<float>(array2) < CS$<>8__locals1.<>4__this._numClasses)
					{
						array2 = new float[CS$<>8__locals1.<>4__this._numClasses];
					}
					for (int i = 0; i < CS$<>8__locals1.<>4__this._numClasses; i++)
					{
						array2[i] = CS$<>8__locals1.scores.GetItemOrDefault(CS$<>8__locals1.sortedIndices[i]);
					}
					dst = new VBuffer<float>(CS$<>8__locals1.<>4__this._numClasses, array2, null);
				};
				array[2] = valueGetter3;
			}
			if (activeOutput(3))
			{
				ValueGetter<VBuffer<uint>> valueGetter4 = delegate(ref VBuffer<uint> dst)
				{
					CS$<>8__locals1.updateCacheIfNeeded();
					uint[] array3 = dst.Values;
					if (Utils.Size<uint>(array3) < CS$<>8__locals1.<>4__this._numClasses)
					{
						array3 = new uint[CS$<>8__locals1.<>4__this._numClasses];
					}
					for (int j = 0; j < CS$<>8__locals1.<>4__this._numClasses; j++)
					{
						array3[j] = (uint)(CS$<>8__locals1.sortedIndices[j] + 1);
					}
					dst = new VBuffer<uint>(CS$<>8__locals1.<>4__this._numClasses, array3, null);
				};
				array[3] = valueGetter4;
			}
			if (activeOutput(1))
			{
				ValueGetter<double> valueGetter5 = delegate(ref double dst)
				{
					CS$<>8__locals1.updateCacheIfNeeded();
					if (float.IsNaN(CS$<>8__locals1.label))
					{
						dst = double.NaN;
						return;
					}
					int num3 = (int)CS$<>8__locals1.label;
					if (num3 < CS$<>8__locals1.<>4__this._numClasses)
					{
						float num4 = Math.Min(1f, Math.Max(1E-15f, CS$<>8__locals1.scoresArr[num3]));
						dst = -Math.Log((double)num4);
						return;
					}
					dst = -Math.Log(1.0000000036274937E-15);
				};
				array[1] = valueGetter5;
			}
			return array;
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0005C23C File Offset: 0x0005A43C
		public override RowMapperColumnInfo[] GetOutputColumns()
		{
			RowMapperColumnInfo[] array = new RowMapperColumnInfo[4];
			array[0] = new RowMapperColumnInfo("Assigned", this._types[0], null);
			array[1] = new RowMapperColumnInfo("Log-loss", this._types[1], null);
			VectorType vectorType = new VectorType(TextType.Instance, this._numClasses);
			ColumnMetadataInfo columnMetadataInfo = new ColumnMetadataInfo("SortedScores");
			columnMetadataInfo.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter(this._numClasses, "Score")));
			ColumnMetadataInfo columnMetadataInfo2 = new ColumnMetadataInfo("SortedClasses");
			columnMetadataInfo2.Add("SlotNames", new MetadataInfo<VBuffer<DvText>>(vectorType, this.CreateSlotNamesGetter(this._numClasses, "Class")));
			array[2] = new RowMapperColumnInfo("SortedScores", this._types[2], columnMetadataInfo);
			array[3] = new RowMapperColumnInfo("SortedClasses", this._types[3], columnMetadataInfo2);
			return array;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0005C398 File Offset: 0x0005A598
		private MetadataUtils.MetadataGetter<VBuffer<DvText>> CreateSlotNamesGetter(int numTopClasses, string suffix)
		{
			return delegate(int col, ref VBuffer<DvText> dst)
			{
				DvText[] array = dst.Values;
				if (Utils.Size<DvText>(array) < numTopClasses)
				{
					array = new DvText[numTopClasses];
				}
				for (int i = 1; i <= numTopClasses; i++)
				{
					array[i - 1] = new DvText(string.Format("#{0} {1}", i, suffix));
				}
				dst = new VBuffer<DvText>(numTopClasses, array, null);
			};
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0005C3C8 File Offset: 0x0005A5C8
		private void CheckInputColumnTypes(ISchema schema)
		{
			ColumnType columnType = schema.GetColumnType(this._scoreIndex);
			if (columnType.VectorSize < 2 || columnType.ItemType != NumberType.Float)
			{
				throw Contracts.Except(this._host, "Score column '{0}' has type '{1}' but must be a vector of two or more items of type R4", new object[] { this._scoreCol, columnType });
			}
			columnType = schema.GetColumnType(this._labelIndex);
			if (columnType != NumberType.Float && columnType.KeyCount <= 0)
			{
				throw Contracts.Except(this._host, "Label column '{0}' has type '{1}' but must be a float or a known-cardinality key", new object[] { this._labelCol, columnType });
			}
		}

		// Token: 0x04000945 RID: 2373
		public const string LoaderSignature = "MulticlassPerInstance";

		// Token: 0x04000946 RID: 2374
		private const int AssignedCol = 0;

		// Token: 0x04000947 RID: 2375
		private const int LogLossCol = 1;

		// Token: 0x04000948 RID: 2376
		private const int SortedScoresCol = 2;

		// Token: 0x04000949 RID: 2377
		private const int SortedClassesCol = 3;

		// Token: 0x0400094A RID: 2378
		public const string Assigned = "Assigned";

		// Token: 0x0400094B RID: 2379
		public const string LogLoss = "Log-loss";

		// Token: 0x0400094C RID: 2380
		public const string SortedScores = "SortedScores";

		// Token: 0x0400094D RID: 2381
		public const string SortedClasses = "SortedClasses";

		// Token: 0x0400094E RID: 2382
		private const float Epsilon = 1E-15f;

		// Token: 0x0400094F RID: 2383
		private readonly int _numClasses;

		// Token: 0x04000950 RID: 2384
		private readonly ColumnType[] _types;
	}
}
