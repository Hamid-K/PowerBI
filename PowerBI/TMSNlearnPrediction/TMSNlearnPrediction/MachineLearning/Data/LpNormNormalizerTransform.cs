using System;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.CpuMath;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002AD RID: 685
	public sealed class LpNormNormalizerTransform : OneToOneTransformBase
	{
		// Token: 0x06000FB7 RID: 4023 RVA: 0x00056B47 File Offset: 0x00054D47
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("GCNORMAF", 65538U, 65538U, 65537U, "GcnTransform", "GcnFunction");
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x00056B6C File Offset: 0x00054D6C
		public LpNormNormalizerTransform(LpNormNormalizerTransform.GcnArguments args, IHostEnvironment env, IDataView input)
			: base(env, "LpNormNormalizer", Contracts.CheckRef<LpNormNormalizerTransform.GcnArguments>(env, args, "args").column, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsFloatVector))
		{
			this._exes = new LpNormNormalizerTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new LpNormNormalizerTransform.ColInfoEx(args.column[i], args);
			}
			if (!args.subMean && args.useStdDev)
			{
				using (IChannel channel = this._host.Start("Argument validation"))
				{
					channel.Warning("subMean parameter is false while useStd is true. It is advisable to set subMean to true in case useStd is set to true.");
					channel.Done();
				}
			}
			this.SetMetadata();
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x00056C34 File Offset: 0x00054E34
		public LpNormNormalizerTransform(LpNormNormalizerTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "LpNormNormalizer", Contracts.CheckRef<LpNormNormalizerTransform.Arguments>(env, args, "args").column, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsFloatVector))
		{
			this._exes = new LpNormNormalizerTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new LpNormNormalizerTransform.ColInfoEx(args.column[i], args);
			}
			this.SetMetadata();
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x00056CB0 File Offset: 0x00054EB0
		private LpNormNormalizerTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsFloatItem))
		{
			this._exes = new LpNormNormalizerTransform.ColInfoEx[this.Infos.Length];
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i] = new LpNormNormalizerTransform.ColInfoEx(ctx, ctx.Header.ModelVerWritten >= 65538U);
			}
			this.SetMetadata();
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x00056D6C File Offset: 0x00054F6C
		public static LpNormNormalizerTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("LpNormNormalizer");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(LpNormNormalizerTransform.GetVersionInfo());
			return HostExtensions.Apply<LpNormNormalizerTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new LpNormNormalizerTransform(ctx, h, input);
			});
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x00056E04 File Offset: 0x00055004
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(LpNormNormalizerTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._exes.Length; i++)
			{
				this._exes[i].Save(ctx);
			}
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x00056E67 File Offset: 0x00055067
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			Contracts.Check(this._host, (0 <= iinfo) & (iinfo < this.Infos.Length));
			return this.Infos[iinfo].TypeSrc;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x00056E94 File Offset: 0x00055094
		private void SetMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source, "SlotNames"))
				{
					builder.AddPrimitive<DvBool>("IsNormalized", BoolType.Instance, DvBool.True);
				}
			}
			metadata.Seal();
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000572CC File Offset: 0x000554CC
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			OneToOneTransformBase.ColInfo colInfo = this.Infos[iinfo];
			LpNormNormalizerTransform.ColInfoEx colInfoEx = this._exes[iinfo];
			ValueGetter<VBuffer<float>> getSrc = base.GetSrcGetter<VBuffer<float>>(input, iinfo);
			VBuffer<float> src = default(VBuffer<float>);
			float scale = colInfoEx.Scale;
			if (colInfoEx.SubtractMean)
			{
				switch (colInfoEx.NormKind)
				{
				case LpNormNormalizerTransform.NormalizerKind.StdDev:
					return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
					{
						getSrc.Invoke(ref src);
						float num = LpNormNormalizerTransform.Mean(src.Values, src.Count, src.Length);
						float num2 = LpNormNormalizerTransform.StdDev(src.Values, src.Count, src.Length, num);
						LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num2, scale, num);
					});
				case LpNormNormalizerTransform.NormalizerKind.L1Norm:
					return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
					{
						getSrc.Invoke(ref src);
						float num3 = LpNormNormalizerTransform.Mean(src.Values, src.Count, src.Length);
						float num4 = LpNormNormalizerTransform.L1Norm(src.Values, src.Count, num3);
						LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num4, scale, num3);
					});
				case LpNormNormalizerTransform.NormalizerKind.LInf:
					return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
					{
						getSrc.Invoke(ref src);
						float num5 = LpNormNormalizerTransform.Mean(src.Values, src.Count, src.Length);
						float num6 = LpNormNormalizerTransform.LInfNorm(src.Values, src.Count, num5);
						LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num6, scale, num5);
					});
				}
				return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
				{
					getSrc.Invoke(ref src);
					float num7 = LpNormNormalizerTransform.Mean(src.Values, src.Count, src.Length);
					float num8 = LpNormNormalizerTransform.L2Norm(src.Values, src.Count, num7);
					LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num8, scale, num7);
				});
			}
			switch (colInfoEx.NormKind)
			{
			case LpNormNormalizerTransform.NormalizerKind.StdDev:
				return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
				{
					getSrc.Invoke(ref src);
					float num9 = LpNormNormalizerTransform.StdDev(src.Values, src.Count, src.Length);
					LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num9, scale, 0f);
				});
			case LpNormNormalizerTransform.NormalizerKind.L1Norm:
				return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
				{
					getSrc.Invoke(ref src);
					float num10 = LpNormNormalizerTransform.L1Norm(src.Values, src.Count, 0f);
					LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num10, scale, 0f);
				});
			case LpNormNormalizerTransform.NormalizerKind.LInf:
				return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
				{
					getSrc.Invoke(ref src);
					float num11 = LpNormNormalizerTransform.LInfNorm(src.Values, src.Count, 0f);
					LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num11, scale, 0f);
				});
			}
			return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
			{
				getSrc.Invoke(ref src);
				float num12 = LpNormNormalizerTransform.L2Norm(src.Values, src.Count, 0f);
				LpNormNormalizerTransform.FillValues(this._host, ref src, ref dst, num12, scale, 0f);
			});
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00057440 File Offset: 0x00055640
		private static void FillValues(IExceptionContext ectx, ref VBuffer<float> src, ref VBuffer<float> dst, float divisor, float scale, float offset = 0f)
		{
			int count = src.Count;
			int length = src.Length;
			if (count == 0)
			{
				dst = new VBuffer<float>(length, 0, dst.Values, dst.Indices);
				return;
			}
			float num = scale;
			if (divisor > 0f)
			{
				num /= divisor;
			}
			if (num < 1E-08f)
			{
				num = 1f;
			}
			if (offset == 0f)
			{
				float[] array = dst.Values;
				if (Utils.Size<float>(array) < count)
				{
					array = new float[count];
				}
				int[] array2 = dst.Indices;
				if (!src.IsDense)
				{
					if (Utils.Size<int>(array2) < count)
					{
						array2 = new int[count];
					}
					Array.Copy(src.Indices, array2, count);
				}
				SseUtils.Scale(num, src.Values, array, count);
				dst = new VBuffer<float>(length, count, array, array2);
				return;
			}
			src.CopyToDense(ref dst);
			if (num != 1f)
			{
				SseUtils.ScaleAdd(num, -offset, dst.Values, length);
				return;
			}
			SseUtils.Add(-offset, dst.Values, length);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x00057534 File Offset: 0x00055734
		private static float StdDev(float[] values, int count, int length)
		{
			if (count == 0)
			{
				return 0f;
			}
			float num = SseUtils.Sum(values, 0, count) / (float)length;
			float num2 = 0f;
			if (count != length && num != 0f)
			{
				float num3 = num * num;
				num2 = (float)(length - count) * num3;
			}
			num2 += SseUtils.SumSq(num, values, 0, count);
			return MathUtils.Sqrt(num2 / (float)length);
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x00057588 File Offset: 0x00055788
		private static float StdDev(float[] values, int count, int length, float mean)
		{
			if (count == 0)
			{
				return 0f;
			}
			float num = 0f;
			if (count != length && mean != 0f)
			{
				float num2 = mean * mean;
				num = (float)(length - count) * num2;
			}
			num += SseUtils.SumSq(mean, values, 0, count);
			return MathUtils.Sqrt(num / (float)length);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000575D0 File Offset: 0x000557D0
		private static float L2Norm(float[] values, int count, float mean = 0f)
		{
			if (count == 0)
			{
				return 0f;
			}
			return MathUtils.Sqrt(SseUtils.SumSq(mean, values, 0, count));
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000575E9 File Offset: 0x000557E9
		private static float L1Norm(float[] values, int count, float mean = 0f)
		{
			if (count == 0)
			{
				return 0f;
			}
			return SseUtils.SumAbs(mean, values, 0, count);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000575FD File Offset: 0x000557FD
		private static float LInfNorm(float[] values, int count, float mean = 0f)
		{
			if (count == 0)
			{
				return 0f;
			}
			return SseUtils.MaxAbsDiff(mean, values, count);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x00057610 File Offset: 0x00055810
		private static float Mean(float[] src, int count, int length)
		{
			if (length == 0 || count == 0)
			{
				return 0f;
			}
			return SseUtils.Sum(src, 0, count) / (float)length;
		}

		// Token: 0x040008B5 RID: 2229
		internal const string GcnSummary = "Performs a global contrast normalization on input values: Y = (s * X - M) / D, where s is a scale, M is mean and D is either L2 norm or standard deviation.";

		// Token: 0x040008B6 RID: 2230
		internal const string Summary = "Normalize vectors (rows) individually by rescaling them to unit norm (L2, L1 or LInf). Performs the following operation on a vector X: Y = (X - M) / D, where M is mean and D is either L2 norm, L1 norm or LInf norm.";

		// Token: 0x040008B7 RID: 2231
		private const uint verVectorNormalizerSupported = 65538U;

		// Token: 0x040008B8 RID: 2232
		public const string LoaderSignature = "GcnTransform";

		// Token: 0x040008B9 RID: 2233
		internal const string LoaderSignatureOld = "GcnFunction";

		// Token: 0x040008BA RID: 2234
		private const string RegistrationName = "LpNormNormalizer";

		// Token: 0x040008BB RID: 2235
		private const float MinScale = 1E-08f;

		// Token: 0x040008BC RID: 2236
		private readonly LpNormNormalizerTransform.ColInfoEx[] _exes;

		// Token: 0x020002AE RID: 686
		public enum NormalizerKind : byte
		{
			// Token: 0x040008BE RID: 2238
			L2Norm,
			// Token: 0x040008BF RID: 2239
			StdDev,
			// Token: 0x040008C0 RID: 2240
			L1Norm,
			// Token: 0x040008C1 RID: 2241
			LInf
		}

		// Token: 0x020002AF RID: 687
		public sealed class Arguments
		{
			// Token: 0x040008C2 RID: 2242
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public LpNormNormalizerTransform.Column[] column;

			// Token: 0x040008C3 RID: 2243
			[Argument(0, HelpText = "The norm to use to normalize each sample", ShortName = "norm", SortOrder = 1)]
			public LpNormNormalizerTransform.NormalizerKind normKind;

			// Token: 0x040008C4 RID: 2244
			[Argument(0, HelpText = "Subtract mean from each value before normalizing", SortOrder = 2)]
			public bool subMean;
		}

		// Token: 0x020002B0 RID: 688
		public sealed class GcnArguments
		{
			// Token: 0x040008C5 RID: 2245
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public LpNormNormalizerTransform.GcnColumn[] column;

			// Token: 0x040008C6 RID: 2246
			[Argument(0, HelpText = "Subtract mean from each value before normalizing", SortOrder = 1)]
			public bool subMean = true;

			// Token: 0x040008C7 RID: 2247
			[Argument(0, HelpText = "Normalize by standard deviation rather than L2 norm", ShortName = "useStd")]
			public bool useStdDev;

			// Token: 0x040008C8 RID: 2248
			[Argument(0, HelpText = "Scale features by this value")]
			public float scale = 1f;
		}

		// Token: 0x020002B1 RID: 689
		public abstract class ColumnBase : OneToOneColumn
		{
			// Token: 0x06000FC9 RID: 4041 RVA: 0x0005764B File Offset: 0x0005584B
			protected override bool TryUnparseCore(StringBuilder sb)
			{
				return this.subMean == null && base.TryUnparseCore(sb);
			}

			// Token: 0x040008C9 RID: 2249
			[Argument(0, HelpText = "Subtract mean from each value before normalizing")]
			public bool? subMean;
		}

		// Token: 0x020002B2 RID: 690
		public sealed class Column : LpNormNormalizerTransform.ColumnBase
		{
			// Token: 0x06000FCB RID: 4043 RVA: 0x0005766C File Offset: 0x0005586C
			public static LpNormNormalizerTransform.Column Parse(string str)
			{
				LpNormNormalizerTransform.Column column = new LpNormNormalizerTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000FCC RID: 4044 RVA: 0x0005768B File Offset: 0x0005588B
			public bool TryUnparse(StringBuilder sb)
			{
				return this.normKind == null && this.TryUnparseCore(sb);
			}

			// Token: 0x040008CA RID: 2250
			[Argument(0, HelpText = "The norm to use to normalize each sample", ShortName = "norm", SortOrder = 1)]
			public LpNormNormalizerTransform.NormalizerKind? normKind;
		}

		// Token: 0x020002B3 RID: 691
		public sealed class GcnColumn : LpNormNormalizerTransform.ColumnBase
		{
			// Token: 0x06000FCE RID: 4046 RVA: 0x000576AC File Offset: 0x000558AC
			public static LpNormNormalizerTransform.GcnColumn Parse(string str)
			{
				LpNormNormalizerTransform.GcnColumn gcnColumn = new LpNormNormalizerTransform.GcnColumn();
				if (gcnColumn.TryParse(str))
				{
					return gcnColumn;
				}
				return null;
			}

			// Token: 0x06000FCF RID: 4047 RVA: 0x000576CB File Offset: 0x000558CB
			public bool TryUnparse(StringBuilder sb)
			{
				return this.useStdDev == null && this.scale == null && this.TryUnparseCore(sb);
			}

			// Token: 0x040008CB RID: 2251
			[Argument(0, HelpText = "Normalize by standard deviation rather than L2 norm")]
			public bool? useStdDev;

			// Token: 0x040008CC RID: 2252
			[Argument(0, HelpText = "Scale features by this value")]
			public float? scale;
		}

		// Token: 0x020002B4 RID: 692
		private sealed class ColInfoEx
		{
			// Token: 0x06000FD1 RID: 4049 RVA: 0x000576F8 File Offset: 0x000558F8
			public ColInfoEx(LpNormNormalizerTransform.Column col, LpNormNormalizerTransform.Arguments args)
			{
				this.SubtractMean = col.subMean ?? args.subMean;
				this.NormKind = col.normKind ?? args.normKind;
				this.Scale = 1f;
			}

			// Token: 0x06000FD2 RID: 4050 RVA: 0x00057760 File Offset: 0x00055960
			public ColInfoEx(LpNormNormalizerTransform.GcnColumn col, LpNormNormalizerTransform.GcnArguments args)
			{
				this.SubtractMean = col.subMean ?? args.subMean;
				this.NormKind = ((col.useStdDev ?? args.useStdDev) ? LpNormNormalizerTransform.NormalizerKind.StdDev : LpNormNormalizerTransform.NormalizerKind.L2Norm);
				float? scale = col.scale;
				this.Scale = ((scale != null) ? scale.GetValueOrDefault() : args.scale);
				Contracts.CheckUserArg(0f < this.Scale && this.Scale < float.PositiveInfinity, "scale", "scale must be a positive finite value");
			}

			// Token: 0x06000FD3 RID: 4051 RVA: 0x00057818 File Offset: 0x00055A18
			public ColInfoEx(ModelLoadContext ctx, bool normKindSerialized)
			{
				this.SubtractMean = Utils.ReadBoolByte(ctx.Reader);
				byte b = ctx.Reader.ReadByte();
				Contracts.CheckDecode(Enum.IsDefined(typeof(LpNormNormalizerTransform.NormalizerKind), b));
				this.NormKind = (LpNormNormalizerTransform.NormalizerKind)b;
				Contracts.CheckDecode(normKindSerialized || this.NormKind == LpNormNormalizerTransform.NormalizerKind.L2Norm || this.NormKind == LpNormNormalizerTransform.NormalizerKind.StdDev);
				this.Scale = Utils.ReadFloat(ctx.Reader);
				Contracts.CheckDecode(0f < this.Scale && this.Scale < float.PositiveInfinity);
			}

			// Token: 0x06000FD4 RID: 4052 RVA: 0x000578BB File Offset: 0x00055ABB
			public void Save(ModelSaveContext ctx)
			{
				Utils.WriteBoolByte(ctx.Writer, this.SubtractMean);
				ctx.Writer.Write((byte)this.NormKind);
				ctx.Writer.Write(this.Scale);
			}

			// Token: 0x040008CD RID: 2253
			public readonly bool SubtractMean;

			// Token: 0x040008CE RID: 2254
			public readonly LpNormNormalizerTransform.NormalizerKind NormKind;

			// Token: 0x040008CF RID: 2255
			public readonly float Scale;
		}
	}
}
