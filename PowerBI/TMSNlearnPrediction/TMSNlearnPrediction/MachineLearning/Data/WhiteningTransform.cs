using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.CpuMath;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020003E6 RID: 998
	public sealed class WhiteningTransform : OneToOneTransformBase
	{
		// Token: 0x06001533 RID: 5427 RVA: 0x0007B6A0 File Offset: 0x000798A0
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("WHITENTF", 65537U, 65537U, 65537U, "WhiteningTransform", "WhiteningFunction");
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x0007B6C8 File Offset: 0x000798C8
		public WhiteningTransform(WhiteningTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "Whitening", Contracts.CheckRef<WhiteningTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(WhiteningTransform.TestColumn))
		{
			this._exes = new WhiteningTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new WhiteningTransform.ColInfoEx(args.column[i], args, this.Infos[i]);
			}
			using (IChannel channel = this._host.Start("Training"))
			{
				this._models = new float[this.Infos.Length][];
				this._invModels = new float[this.Infos.Length][];
				int[] array2;
				float[][] array = this.LoadDataAsDense(channel, out array2);
				this.TrainModels(array, array2, channel);
				channel.Done();
			}
			base.Metadata.Seal();
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x0007B7C4 File Offset: 0x000799C4
		private float[][] LoadDataAsDense(IChannel ch, out int[] actualRowCounts)
		{
			long rowCount = this.GetRowCount();
			float[][] array = new float[this.Infos.Length][];
			actualRowCounts = new int[this.Infos.Length];
			int num = 0;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ColumnType typeSrc = this.Infos[i].TypeSrc;
				WhiteningTransform.ColInfoEx colInfoEx = this._exes[i];
				if (rowCount <= (long)colInfoEx.MaxRow)
				{
					actualRowCounts[i] = (int)rowCount;
				}
				else
				{
					ch.Info("Only {0:N0} rows of column '{1}' will be used for whitening transform.", new object[]
					{
						colInfoEx.MaxRow,
						this.Infos[i].Name
					});
					actualRowCounts[i] = colInfoEx.MaxRow;
				}
				int valueCount = typeSrc.ValueCount;
				if ((long)valueCount * (long)actualRowCounts[i] > 2147483647L)
				{
					actualRowCounts[i] = int.MaxValue / valueCount;
					ch.Info("Only {0:N0} rows of column '{1}' will be used for whitening transform.", new object[]
					{
						actualRowCounts[i],
						this.Infos[i].Name
					});
				}
				array[i] = new float[valueCount * actualRowCounts[i]];
				if (actualRowCounts[i] > num)
				{
					num = actualRowCounts[i];
				}
			}
			int[] array2 = new int[this.Infos.Length];
			HashSet<int> hashSet = new HashSet<int>(this.Infos.Select((OneToOneTransformBase.ColInfo info) => info.Source));
			using (IRowCursor rowCursor = this._input.GetRowCursor(new Func<int, bool>(hashSet.Contains), null))
			{
				ValueGetter<VBuffer<float>>[] array3 = new ValueGetter<VBuffer<float>>[this.Infos.Length];
				for (int j = 0; j < this.Infos.Length; j++)
				{
					array3[j] = rowCursor.GetGetter<VBuffer<float>>(this.Infos[j].Source);
				}
				VBuffer<float> vbuffer = default(VBuffer<float>);
				int num2 = 0;
				while (num2 < num && rowCursor.MoveNext())
				{
					for (int k = 0; k < this.Infos.Length; k++)
					{
						if (num2 < actualRowCounts[k] && array[k].Length != 0)
						{
							array3[k].Invoke(ref vbuffer);
							vbuffer.CopyTo(array[k], array2[k]);
							array2[k] += this.Infos[k].TypeSrc.ValueCount;
						}
					}
					num2++;
				}
			}
			return array;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x0007BA38 File Offset: 0x00079C38
		private void TrainModels(float[][] columnData, int[] rowCounts, IChannel ch)
		{
			for (int i = 0; i < this.Infos.Length; i++)
			{
				WhiteningTransform.ColInfoEx colInfoEx = this._exes[i];
				float[] array = columnData[i];
				int num = rowCounts[i];
				int valueCount = this.Infos[i].TypeSrc.ValueCount;
				float[] array2 = new float[valueCount * valueCount];
				ch.Info("Computing covariance matrix...");
				WhiteningTransform.Mkl.Gemm(WhiteningTransform.Mkl.Layout.RowMajor, WhiteningTransform.Mkl.Transpose.Trans, WhiteningTransform.Mkl.Transpose.NoTrans, valueCount, valueCount, num, 1f / (float)num, array, valueCount, array, valueCount, 0f, array2, valueCount);
				ch.Info("Computing SVD...");
				float[] array3 = new float[valueCount];
				float[] array4 = new float[valueCount];
				int num2 = WhiteningTransform.Mkl.Svd(WhiteningTransform.Mkl.Layout.RowMajor, WhiteningTransform.Mkl.SvdJob.MinOvr, WhiteningTransform.Mkl.SvdJob.None, valueCount, valueCount, array2, valueCount, array3, null, valueCount, null, valueCount, array4);
				if (num2 > 0)
				{
					throw Contracts.Except(ch, "SVD did not converge.");
				}
				if (num2 < 0)
				{
					throw Contracts.Except(ch, "Invalid arguments to LAPACK gesvd, error: {0}", new object[] { num2 });
				}
				ch.Info("Scaling eigenvectors...");
				for (int j = 0; j < array3.Length; j++)
				{
					array3[j] = MathUtils.Sqrt(Math.Max(0f, array3[j]) + colInfoEx.Epsilon);
				}
				float[] array5 = new float[array3.Length];
				for (int k = 0; k < array5.Length; k++)
				{
					array5[k] = 1f / array3[k];
				}
				float[] array6 = new float[array2.Length];
				float[] array7 = new float[array2.Length];
				int num3 = 0;
				for (int l = 0; l < valueCount; l++)
				{
					int num4 = l;
					for (int m = 0; m < valueCount; m++)
					{
						array6[num4] = array2[num3] * array5[m];
						array7[num4] = array2[num3] * array3[m];
						num3++;
						num4 += valueCount;
					}
				}
				if (colInfoEx.Kind == WhiteningKind.Pca)
				{
					this._models[i] = array6;
					if (colInfoEx.SaveInv)
					{
						this._invModels[i] = array7;
					}
				}
				else if (colInfoEx.Kind == WhiteningKind.Zca)
				{
					this._models[i] = new float[array2.Length];
					WhiteningTransform.Mkl.Gemm(WhiteningTransform.Mkl.Layout.RowMajor, WhiteningTransform.Mkl.Transpose.NoTrans, WhiteningTransform.Mkl.Transpose.NoTrans, valueCount, valueCount, valueCount, 1f, array2, valueCount, array6, valueCount, 0f, this._models[i], valueCount);
					if (colInfoEx.SaveInv)
					{
						this._invModels[i] = new float[array2.Length];
						WhiteningTransform.Mkl.Gemm(WhiteningTransform.Mkl.Layout.RowMajor, WhiteningTransform.Mkl.Transpose.NoTrans, WhiteningTransform.Mkl.Transpose.NoTrans, valueCount, valueCount, valueCount, 1f, array2, valueCount, array7, valueCount, 0f, this._invModels[i], valueCount);
					}
				}
			}
		}

		// Token: 0x06001537 RID: 5431 RVA: 0x0007BCC8 File Offset: 0x00079EC8
		private WhiteningTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(WhiteningTransform.TestColumn))
		{
			this._exes = new WhiteningTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new WhiteningTransform.ColInfoEx(ctx, this.Infos[i]);
			}
			this._models = new float[this.Infos.Length][];
			this._invModels = new float[this.Infos.Length][];
			for (int j = 0; j < this.Infos.Length; j++)
			{
				this._models[j] = Utils.ReadFloatArray(ctx.Reader);
				WhiteningTransform.ValidateModel(this._host, this._models[j], this.Infos[j].TypeSrc);
				if (this._exes[j].SaveInv)
				{
					this._invModels[j] = Utils.ReadFloatArray(ctx.Reader);
					WhiteningTransform.ValidateModel(this._host, this._invModels[j], this.Infos[j].TypeSrc);
				}
			}
			base.Metadata.Seal();
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x0007BE04 File Offset: 0x0007A004
		public static WhiteningTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Whitening");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(WhiteningTransform.GetVersionInfo());
			int num = ctx.Reader.ReadInt32();
			Contracts.CheckDecode(h, num == 4);
			return HostExtensions.Apply<WhiteningTransform>(h, "Loading Model", (IChannel ch) => new WhiteningTransform(ctx, h, input));
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x0007BEBC File Offset: 0x0007A0BC
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(WhiteningTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i].Save(ctx);
			}
			for (int j = 0; j < this._models.Length; j++)
			{
				Utils.WriteFloatArray(ctx.Writer, this._models[j]);
				if (this._exes[j].SaveInv)
				{
					Utils.WriteFloatArray(ctx.Writer, this._invModels[j]);
				}
			}
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x0007BF68 File Offset: 0x0007A168
		private static string TestColumn(ColumnType t)
		{
			string text = OneToOneTransformBase.TestIsKnownSizeFloatVector(t);
			if (text != null)
			{
				return text;
			}
			if ((long)t.ValueCount * (long)t.ValueCount > 2146435071L)
			{
				return "Vector size exceeds limit";
			}
			return null;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x0007BFA0 File Offset: 0x0007A1A0
		private static void ValidateModel(IExceptionContext ectx, float[] model, ColumnType col)
		{
			Contracts.CheckDecode(ectx, (long)Utils.Size<float>(model) == (long)col.ValueCount * (long)col.ValueCount, "Invalid model size.");
			for (int i = 0; i < model.Length; i++)
			{
				Contracts.CheckDecode(ectx, FloatUtils.IsFinite(model[i]), "Found NaN or infinity in the model.");
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x0007BFFC File Offset: 0x0007A1FC
		private long GetRowCount()
		{
			long? rowCount = this._input.GetRowCount(false);
			if (rowCount != null)
			{
				return rowCount.GetValueOrDefault();
			}
			int num = this._exes.Max((WhiteningTransform.ColInfoEx i) => i.MaxRow);
			long num2 = 0L;
			using (IRowCursor rowCursor = this._input.GetRowCursor((int col) => false, null))
			{
				while (num2 < (long)num && rowCursor.MoveNext())
				{
					num2 += 1L;
				}
			}
			return num2;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x0007C0B0 File Offset: 0x0007A2B0
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			Contracts.Check(this._host, (0 <= iinfo) & (iinfo < this.Infos.Length));
			return this._exes[iinfo].Type;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x0007C148 File Offset: 0x0007A348
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			WhiteningTransform.ColInfoEx colInfoEx = this._exes[iinfo];
			ValueGetter<VBuffer<float>> getSrc = base.GetSrcGetter<VBuffer<float>>(input, iinfo);
			VBuffer<float> src = default(VBuffer<float>);
			int cslotSrc = this.Infos[iinfo].TypeSrc.ValueCount;
			int cslotDst = ((colInfoEx.Kind == WhiteningKind.Pca && colInfoEx.PcaNum > 0) ? colInfoEx.PcaNum : this.Infos[iinfo].TypeSrc.ValueCount);
			float[] model = this._models[iinfo];
			return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
			{
				getSrc.Invoke(ref src);
				Contracts.Check(this._host, src.Length == cslotSrc, "Invalid column size.");
				WhiteningTransform.FillValues(model, ref src, ref dst, cslotDst);
			});
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x0007C1F0 File Offset: 0x0007A3F0
		private static void FillValues(float[] model, ref VBuffer<float> src, ref VBuffer<float> dst, int cdst)
		{
			int count = src.Count;
			int length = src.Length;
			float[] values = src.Values;
			int[] indices = src.Indices;
			float[] array = ((Utils.Size<float>(dst.Values) >= cdst) ? dst.Values : new float[cdst]);
			if (src.IsDense)
			{
				WhiteningTransform.Mkl.Gemv(WhiteningTransform.Mkl.Layout.RowMajor, WhiteningTransform.Mkl.Transpose.NoTrans, cdst, length, 1f, model, length, values, 1, 0f, array, 1);
			}
			else
			{
				int num = 0;
				for (int i = 0; i < cdst; i++)
				{
					array[i] = WhiteningTransform.DotProduct(model, num, values, indices, count);
					num += length;
				}
			}
			dst = new VBuffer<float>(cdst, array, dst.Indices);
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x0007C29A File Offset: 0x0007A49A
		private static float DotProduct(float[] a, int aOffset, float[] b, int[] indices, int count)
		{
			return SseUtils.DotProductSparse(a, aOffset, b, indices, count);
		}

		// Token: 0x04000CD5 RID: 3285
		private const WhiteningTransform.Mkl.Layout Layout = WhiteningTransform.Mkl.Layout.RowMajor;

		// Token: 0x04000CD6 RID: 3286
		internal const string Summary = "Applies PCA or ZCA whitening algorithm to the input.";

		// Token: 0x04000CD7 RID: 3287
		public const string LoaderSignature = "WhiteningTransform";

		// Token: 0x04000CD8 RID: 3288
		internal const string LoaderSignatureOld = "WhiteningFunction";

		// Token: 0x04000CD9 RID: 3289
		private const string RegistrationName = "Whitening";

		// Token: 0x04000CDA RID: 3290
		private readonly float[][] _models;

		// Token: 0x04000CDB RID: 3291
		internal readonly float[][] _invModels;

		// Token: 0x04000CDC RID: 3292
		private readonly WhiteningTransform.ColInfoEx[] _exes;

		// Token: 0x020003E7 RID: 999
		public sealed class Arguments
		{
			// Token: 0x04000CE0 RID: 3296
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public WhiteningTransform.Column[] column;

			// Token: 0x04000CE1 RID: 3297
			[Argument(0, HelpText = "Whitening kind (PCA/ZCA)")]
			public WhiteningKind kind = WhiteningKind.Zca;

			// Token: 0x04000CE2 RID: 3298
			[Argument(0, HelpText = "Scaling regularizer")]
			public float eps = 1E-05f;

			// Token: 0x04000CE3 RID: 3299
			[Argument(0, HelpText = "Max number of rows", ShortName = "rows")]
			public int maxRows = 100000;

			// Token: 0x04000CE4 RID: 3300
			[Argument(0, HelpText = "Whether to save inverse (recovery) matrix", ShortName = "saveInv")]
			public bool saveInverse;

			// Token: 0x04000CE5 RID: 3301
			[Argument(0, HelpText = "PCA components to retain")]
			public int pcaNum;
		}

		// Token: 0x020003E8 RID: 1000
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06001545 RID: 5445 RVA: 0x0007C2CC File Offset: 0x0007A4CC
			public static WhiteningTransform.Column Parse(string str)
			{
				WhiteningTransform.Column column = new WhiteningTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06001546 RID: 5446 RVA: 0x0007C2EC File Offset: 0x0007A4EC
			public bool TryUnparse(StringBuilder sb)
			{
				return this.kind == null && this.eps == null && this.maxRows == null && this.saveInverse == null && this.pcaNum == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000CE6 RID: 3302
			[Argument(0, HelpText = "Whitening kind (PCA/ZCA)")]
			public WhiteningKind? kind;

			// Token: 0x04000CE7 RID: 3303
			[Argument(0, HelpText = "Scaling regularizer")]
			public float? eps;

			// Token: 0x04000CE8 RID: 3304
			[Argument(0, HelpText = "Max number of rows", ShortName = "rows")]
			public int? maxRows;

			// Token: 0x04000CE9 RID: 3305
			[Argument(0, HelpText = "Whether to save inverse (recovery) matrix", ShortName = "saveInv")]
			public bool? saveInverse;

			// Token: 0x04000CEA RID: 3306
			[Argument(0, HelpText = "PCA components to keep/drop")]
			public int? pcaNum;
		}

		// Token: 0x020003E9 RID: 1001
		public sealed class ColInfoEx
		{
			// Token: 0x06001548 RID: 5448 RVA: 0x0007C34C File Offset: 0x0007A54C
			public ColInfoEx(WhiteningTransform.Column item, WhiteningTransform.Arguments args, OneToOneTransformBase.ColInfo info)
			{
				this.Kind = item.kind ?? args.kind;
				Contracts.CheckUserArg(this.Kind == WhiteningKind.Pca || this.Kind == WhiteningKind.Zca, "kind");
				float? eps = item.eps;
				this.Epsilon = ((eps != null) ? eps.GetValueOrDefault() : args.eps);
				Contracts.CheckUserArg(0f <= this.Epsilon && this.Epsilon < float.PositiveInfinity, "epsilon");
				this.MaxRow = item.maxRows ?? args.maxRows;
				Contracts.CheckUserArg(this.MaxRow > 0, "maxrow");
				this.SaveInv = item.saveInverse ?? args.saveInverse;
				this.PcaNum = item.pcaNum ?? args.pcaNum;
				Contracts.CheckUserArg(this.PcaNum >= 0, "pcaNum");
				if (this.Kind == WhiteningKind.Zca || this.PcaNum == 0)
				{
					this.Type = info.TypeSrc.AsVector;
					return;
				}
				this.Type = new VectorType(NumberType.Float, this.PcaNum);
			}

			// Token: 0x06001549 RID: 5449 RVA: 0x0007C4C4 File Offset: 0x0007A6C4
			public ColInfoEx(ModelLoadContext ctx, OneToOneTransformBase.ColInfo info)
			{
				this.Kind = (WhiteningKind)ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this.Kind == WhiteningKind.Pca || this.Kind == WhiteningKind.Zca);
				this.Epsilon = Utils.ReadFloat(ctx.Reader);
				Contracts.CheckDecode(0f <= this.Epsilon && this.Epsilon < float.PositiveInfinity);
				this.MaxRow = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this.MaxRow > 0);
				this.SaveInv = Utils.ReadBoolByte(ctx.Reader);
				this.PcaNum = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(this.PcaNum >= 0);
				if (this.Kind == WhiteningKind.Zca || this.PcaNum == 0)
				{
					this.Type = info.TypeSrc.AsVector;
					return;
				}
				this.Type = new VectorType(NumberType.Float, this.PcaNum);
			}

			// Token: 0x0600154A RID: 5450 RVA: 0x0007C5C0 File Offset: 0x0007A7C0
			public void Save(ModelSaveContext ctx)
			{
				ctx.Writer.Write((int)this.Kind);
				ctx.Writer.Write(this.Epsilon);
				ctx.Writer.Write(this.MaxRow);
				Utils.WriteBoolByte(ctx.Writer, this.SaveInv);
				ctx.Writer.Write(this.PcaNum);
			}

			// Token: 0x04000CEB RID: 3307
			public readonly WhiteningKind Kind;

			// Token: 0x04000CEC RID: 3308
			public readonly float Epsilon;

			// Token: 0x04000CED RID: 3309
			public readonly int MaxRow;

			// Token: 0x04000CEE RID: 3310
			public readonly bool SaveInv;

			// Token: 0x04000CEF RID: 3311
			public readonly int PcaNum;

			// Token: 0x04000CF0 RID: 3312
			public readonly VectorType Type;
		}

		// Token: 0x020003EA RID: 1002
		private static class Mkl
		{
			// Token: 0x0600154B RID: 5451
			[DllImport("Microsoft.MachineLearning.MklImports.dll", EntryPoint = "cblas_sgemv")]
			public static extern void Gemv(WhiteningTransform.Mkl.Layout layout, WhiteningTransform.Mkl.Transpose trans, int m, int n, float alpha, float[] a, int lda, float[] x, int incx, float beta, float[] y, int incy);

			// Token: 0x0600154C RID: 5452
			[DllImport("Microsoft.MachineLearning.MklImports.dll", EntryPoint = "cblas_sgemm")]
			public static extern void Gemm(WhiteningTransform.Mkl.Layout layout, WhiteningTransform.Mkl.Transpose transA, WhiteningTransform.Mkl.Transpose transB, int m, int n, int k, float alpha, float[] a, int lda, float[] b, int ldb, float beta, float[] c, int ldc);

			// Token: 0x0600154D RID: 5453
			[DllImport("Microsoft.MachineLearning.MklImports.dll", EntryPoint = "LAPACKE_sgesvd")]
			public static extern int Svd(WhiteningTransform.Mkl.Layout layout, WhiteningTransform.Mkl.SvdJob jobu, WhiteningTransform.Mkl.SvdJob jobvt, int m, int n, float[] a, int lda, float[] s, float[] u, int ldu, float[] vt, int ldvt, float[] superb);

			// Token: 0x04000CF1 RID: 3313
			private const string DllName = "Microsoft.MachineLearning.MklImports.dll";

			// Token: 0x020003EB RID: 1003
			public enum Layout
			{
				// Token: 0x04000CF3 RID: 3315
				RowMajor = 101,
				// Token: 0x04000CF4 RID: 3316
				ColMajor
			}

			// Token: 0x020003EC RID: 1004
			public enum Transpose
			{
				// Token: 0x04000CF6 RID: 3318
				NoTrans = 111,
				// Token: 0x04000CF7 RID: 3319
				Trans,
				// Token: 0x04000CF8 RID: 3320
				ConjTrans
			}

			// Token: 0x020003ED RID: 1005
			public enum SvdJob : byte
			{
				// Token: 0x04000CFA RID: 3322
				None = 78,
				// Token: 0x04000CFB RID: 3323
				All = 65,
				// Token: 0x04000CFC RID: 3324
				Min = 83,
				// Token: 0x04000CFD RID: 3325
				MinOvr = 79
			}
		}
	}
}
