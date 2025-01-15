using System;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.CpuMath;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;
using Microsoft.MachineLearning.Numeric;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020002CA RID: 714
	public sealed class RffTransform : OneToOneTransformBase
	{
		// Token: 0x0600105A RID: 4186 RVA: 0x00059C0F File Offset: 0x00057E0F
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("RFF FUNC", 65537U, 65537U, 65537U, "RffTransform", null);
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00059C30 File Offset: 0x00057E30
		private static string TestColumnType(ColumnType type)
		{
			if (type.ItemType == NumberType.Float && type.ValueCount > 0)
			{
				return null;
			}
			return "Expected R4 or vector of R4 with known size";
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00059C6C File Offset: 0x00057E6C
		public RffTransform(RffTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "Rff", Contracts.CheckRef<RffTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(RffTransform.TestColumnType))
		{
			IExceptionContext host = this._host;
			bool flag;
			if (!args.column.All((RffTransform.Column c) => c.seed != null))
			{
				if (!args.column.All((RffTransform.Column c) => c.seed == null))
				{
					flag = args.seed != null;
					goto IL_0091;
				}
			}
			flag = true;
			IL_0091:
			Contracts.CheckUserArg(host, flag, "seed", "If any column specific seeds are non-zero, the global transform seed must also be non-zero, to make results deterministic");
			this._transformInfos = new RffTransform.TransformInfo[args.column.Length];
			float[] array = RffTransform.Train(this._host, this.Infos, args, input);
			for (int i = 0; i < this._transformInfos.Length; i++)
			{
				this._transformInfos[i] = new RffTransform.TransformInfo(this._host.Fork(), args.column[i], args, this.Infos[i].TypeSrc.ValueCount, array[i]);
			}
			this._types = this.InitColumnTypes();
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00059D98 File Offset: 0x00057F98
		private RffTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(RffTransform.TestColumnType))
		{
			this._transformInfos = new RffTransform.TransformInfo[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				this._transformInfos[i] = new RffTransform.TransformInfo(ctx, this._host, this.Infos[i].TypeSrc.ValueCount, string.Format("MatrixGenerator{0}", i));
			}
			this._types = this.InitColumnTypes();
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x00059E70 File Offset: 0x00058070
		public static RffTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Rff");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(RffTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<RffTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(h, num == 4);
				return new RffTransform(ctx, h, input);
			});
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00059F08 File Offset: 0x00058108
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(RffTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._transformInfos.Length; i++)
			{
				this._transformInfos[i].Save(ctx, string.Format("MatrixGenerator{0}", i));
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x00059F7C File Offset: 0x0005817C
		private static int RoundUp(int cflt, int cfltAlign)
		{
			int num = (cflt + cfltAlign - 1) / cfltAlign;
			return num * cfltAlign;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00059FEC File Offset: 0x000581EC
		private static float[] Train(IHost host, OneToOneTransformBase.ColInfo[] infos, RffTransform.Arguments args, IDataView trainingData)
		{
			float[] array = new float[infos.Length];
			bool[] activeColumns = new bool[trainingData.Schema.ColumnCount];
			for (int i = 0; i < infos.Length; i++)
			{
				activeColumns[infos[i].Source] = true;
			}
			ReservoirSamplerWithReplacement<VBuffer<float>>[] array2 = new ReservoirSamplerWithReplacement<VBuffer<float>>[infos.Length];
			using (IRowCursor rowCursor = trainingData.GetRowCursor((int col) => activeColumns[col], null))
			{
				IRandom random = ((args.seed != null) ? RandomUtils.Create(args.seed) : host.Rand);
				for (int j = 0; j < infos.Length; j++)
				{
					if (infos[j].TypeSrc.IsVector)
					{
						ValueGetter<VBuffer<float>> getter = rowCursor.GetGetter<VBuffer<float>>(infos[j].Source);
						array2[j] = new ReservoirSamplerWithReplacement<VBuffer<float>>(random, 5000, getter);
					}
					else
					{
						ValueGetter<float> getOne = rowCursor.GetGetter<float>(infos[j].Source);
						float val = 0f;
						ValueGetter<VBuffer<float>> valueGetter = delegate(ref VBuffer<float> dst)
						{
							getOne.Invoke(ref val);
							dst = new VBuffer<float>(1, new float[] { val }, null);
						};
						array2[j] = new ReservoirSamplerWithReplacement<VBuffer<float>>(random, 5000, valueGetter);
					}
				}
				while (rowCursor.MoveNext())
				{
					for (int k = 0; k < infos.Length; k++)
					{
						array2[k].Sample();
					}
				}
				for (int l = 0; l < infos.Length; l++)
				{
					array2[l].Lock();
				}
			}
			for (int m = 0; m < infos.Length; m++)
			{
				long numSampled = array2[m].NumSampled;
				VBuffer<float>[] array3;
				int num;
				if (numSampled < 5000L && numSampled * (numSampled - 1L) <= 5000L)
				{
					array3 = array2[m].GetCache();
					num = array2[m].Size;
				}
				else
				{
					array3 = array2[m].GetSample().ToArray<VBuffer<float>>();
					num = array3.Length;
				}
				if (numSampled <= 1L)
				{
					array[m] = 1f;
				}
				else
				{
					SubComponent<IFourierDistributionSampler, SignatureFourierDistributionSampler> subComponent = args.column[m].matrixGenerator;
					if (!SubComponentExtensions.IsGood(subComponent))
					{
						subComponent = args.matrixGenerator;
					}
					ComponentCatalog.LoadableClassInfo loadableClassInfo = ComponentCatalog.GetLoadableClassInfo<IFourierDistributionSampler, SignatureFourierDistributionSampler>(subComponent);
					bool flag = loadableClassInfo != null && loadableClassInfo.Type == typeof(GaussianFourierSampler);
					float[] array4;
					if (num < 5000)
					{
						array4 = new float[numSampled * (numSampled - 1L) / 2L];
						int num2 = 0;
						int num3 = 0;
						while ((long)num3 < numSampled)
						{
							int num4 = num3 + 1;
							while ((long)num4 < numSampled)
							{
								array4[num2++] = (flag ? VectorUtils.L2DistSquared(ref array3[num3], ref array3[num4]) : VectorUtils.L1Distance(ref array3[num3], ref array3[num4]));
								num4++;
							}
							num3++;
						}
					}
					else
					{
						array4 = new float[2500];
						for (int n = 0; n < 4999; n += 2)
						{
							array4[n / 2] = (flag ? VectorUtils.L2DistSquared(ref array3[n], ref array3[n + 1]) : VectorUtils.L1Distance(ref array3[n], ref array3[n + 1]));
						}
					}
					float medianInPlace = MathUtils.GetMedianInPlace(array4, array4.Length);
					array[m] = ((medianInPlace == 0f) ? 1f : medianInPlace);
				}
			}
			return array;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0005A364 File Offset: 0x00058564
		private ColumnType[] InitColumnTypes()
		{
			ColumnType[] array = new ColumnType[this.Infos.Length];
			for (int i = 0; i < this._transformInfos.Length; i++)
			{
				array[i] = new VectorType(NumberType.Float, (this._transformInfos[i].RotationTerms == null) ? (this._transformInfos[i].NewDim * 2) : this._transformInfos[i].NewDim);
			}
			base.Metadata.Seal();
			return array;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0005A3D8 File Offset: 0x000585D8
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			Contracts.Check(this._host, (0 <= iinfo) & (iinfo < this.Infos.Length));
			return this._types[iinfo];
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0005A400 File Offset: 0x00058600
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			OneToOneTransformBase.ColInfo colInfo = this.Infos[iinfo];
			if (colInfo.TypeSrc.IsVector)
			{
				return this.GetterFromVectorType(input, iinfo);
			}
			return this.GetterFromFloatType(input, iinfo);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0005A494 File Offset: 0x00058694
		private ValueGetter<VBuffer<float>> GetterFromVectorType(IRow input, int iinfo)
		{
			ValueGetter<VBuffer<float>> getSrc = base.GetSrcGetter<VBuffer<float>>(input, iinfo);
			VBuffer<float> src = default(VBuffer<float>);
			AlignedArray featuresAligned = new AlignedArray(RffTransform.RoundUp(this.Infos[iinfo].TypeSrc.ValueCount, 4), 16);
			AlignedArray productAligned = new AlignedArray(RffTransform.RoundUp(this._transformInfos[iinfo].NewDim, 4), 16);
			return delegate(ref VBuffer<float> dst)
			{
				getSrc.Invoke(ref src);
				RffTransform.TransformFeatures(this._host, ref src, ref dst, this._transformInfos[iinfo], featuresAligned, productAligned);
			};
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0005A5A0 File Offset: 0x000587A0
		private ValueGetter<VBuffer<float>> GetterFromFloatType(IRow input, int iinfo)
		{
			RffTransform.<>c__DisplayClass12 CS$<>8__locals1 = new RffTransform.<>c__DisplayClass12();
			CS$<>8__locals1.iinfo = iinfo;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.getSrc = base.GetSrcGetter<float>(input, CS$<>8__locals1.iinfo);
			CS$<>8__locals1.src = 0f;
			CS$<>8__locals1.featuresAligned = new AlignedArray(RffTransform.RoundUp(1, 4), 16);
			CS$<>8__locals1.productAligned = new AlignedArray(RffTransform.RoundUp(this._transformInfos[CS$<>8__locals1.iinfo].NewDim, 4), 16);
			RffTransform.<>c__DisplayClass12 CS$<>8__locals2 = CS$<>8__locals1;
			int num = 1;
			float[] array = new float[1];
			CS$<>8__locals2.oneDimensionalVector = new VBuffer<float>(num, array, null);
			return delegate(ref VBuffer<float> dst)
			{
				CS$<>8__locals1.getSrc.Invoke(ref CS$<>8__locals1.src);
				CS$<>8__locals1.oneDimensionalVector.Values[0] = CS$<>8__locals1.src;
				RffTransform.TransformFeatures(CS$<>8__locals1.<>4__this._host, ref CS$<>8__locals1.oneDimensionalVector, ref dst, CS$<>8__locals1.<>4__this._transformInfos[CS$<>8__locals1.iinfo], CS$<>8__locals1.featuresAligned, CS$<>8__locals1.productAligned);
			};
		}

		// Token: 0x06001067 RID: 4199 RVA: 0x0005A640 File Offset: 0x00058840
		private static void TransformFeatures(IHost host, ref VBuffer<float> src, ref VBuffer<float> dst, RffTransform.TransformInfo transformInfo, AlignedArray featuresAligned, AlignedArray productAligned)
		{
			Contracts.Check(host, src.Length == transformInfo.SrcDim, "column does not have the expected dimensionality.");
			float[] array = dst.Values;
			float num;
			if (transformInfo.RotationTerms != null)
			{
				if (Utils.Size<float>(array) < transformInfo.NewDim)
				{
					array = new float[transformInfo.NewDim];
				}
				num = MathUtils.Sqrt(2f / (float)transformInfo.NewDim);
			}
			else
			{
				if (Utils.Size<float>(array) < 2 * transformInfo.NewDim)
				{
					array = new float[2 * transformInfo.NewDim];
				}
				num = MathUtils.Sqrt(1f / (float)transformInfo.NewDim);
			}
			if (src.IsDense)
			{
				featuresAligned.CopyFrom(src.Values, 0, src.Length);
				SseUtils.MatTimesSrc(false, false, transformInfo.RndFourierVectors, featuresAligned, productAligned, transformInfo.NewDim);
			}
			else
			{
				featuresAligned.CopyFrom(src.Indices, src.Values, 0, 0, src.Count, false);
				SseUtils.MatTimesSrc(false, false, transformInfo.RndFourierVectors, src.Indices, featuresAligned, 0, 0, src.Count, productAligned, transformInfo.NewDim);
			}
			for (int i = 0; i < transformInfo.NewDim; i++)
			{
				float num2 = productAligned[i];
				if (transformInfo.RotationTerms != null)
				{
					array[i] = (float)MathUtils.Cos((double)(num2 + transformInfo.RotationTerms[i])) * num;
				}
				else
				{
					array[2 * i] = (float)MathUtils.Cos((double)num2) * num;
					array[2 * i + 1] = (float)MathUtils.Sin((double)num2) * num;
				}
			}
			dst = new VBuffer<float>((transformInfo.RotationTerms == null) ? (2 * transformInfo.NewDim) : transformInfo.NewDim, array, dst.Indices);
		}

		// Token: 0x04000908 RID: 2312
		internal const string Summary = "This transform maps numeric vectors to a random low-dimensional feature space. It is useful when data has non-linear features, since the transform is designed so that the inner products of the transformed data are approximately equal to those in the feature space of a user specified shift-invariant kernel.";

		// Token: 0x04000909 RID: 2313
		public const string LoaderSignature = "RffTransform";

		// Token: 0x0400090A RID: 2314
		private const string RegistrationName = "Rff";

		// Token: 0x0400090B RID: 2315
		private const int CfltAlign = 4;

		// Token: 0x0400090C RID: 2316
		private readonly ColumnType[] _types;

		// Token: 0x0400090D RID: 2317
		private readonly RffTransform.TransformInfo[] _transformInfos;

		// Token: 0x020002CB RID: 715
		public sealed class Arguments
		{
			// Token: 0x04000910 RID: 2320
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public RffTransform.Column[] column;

			// Token: 0x04000911 RID: 2321
			[Argument(0, HelpText = "The number of random Fourier features to create", ShortName = "dim")]
			public int newDim = 1000;

			// Token: 0x04000912 RID: 2322
			[Argument(4, HelpText = "which kernel to use?", ShortName = "kernel")]
			public SubComponent<IFourierDistributionSampler, SignatureFourierDistributionSampler> matrixGenerator = new SubComponent<IFourierDistributionSampler, SignatureFourierDistributionSampler>("GaussianRandom");

			// Token: 0x04000913 RID: 2323
			[Argument(0, HelpText = "create two features for every random Fourier frequency? (one for cos and one for sin)", ShortName = "useSin")]
			public bool useSin;

			// Token: 0x04000914 RID: 2324
			[Argument(4, HelpText = "The seed of the random number generator for generating the new features (if unspecified, the global random is used)")]
			public int? seed;
		}

		// Token: 0x020002CC RID: 716
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x0600106B RID: 4203 RVA: 0x0005A7F8 File Offset: 0x000589F8
			public static RffTransform.Column Parse(string str)
			{
				RffTransform.Column column = new RffTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x0600106C RID: 4204 RVA: 0x0005A817 File Offset: 0x00058A17
			public bool TryUnparse(StringBuilder sb)
			{
				return this.newDim == null && !SubComponentExtensions.IsGood(this.matrixGenerator) && this.useSin == null && this.seed == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000915 RID: 2325
			[Argument(0, HelpText = "The number of random Fourier features to create", ShortName = "dim")]
			public int? newDim;

			// Token: 0x04000916 RID: 2326
			[Argument(4, HelpText = "which kernel to use?", ShortName = "kernel")]
			public SubComponent<IFourierDistributionSampler, SignatureFourierDistributionSampler> matrixGenerator;

			// Token: 0x04000917 RID: 2327
			[Argument(0, HelpText = "create two features for every random Fourier frequency? (one for cos and one for sin)", ShortName = "useSin")]
			public bool? useSin;

			// Token: 0x04000918 RID: 2328
			[Argument(4, HelpText = "The seed of the random number generator for generating the new features (if unspecified, the global random is used)")]
			public int? seed;
		}

		// Token: 0x020002CD RID: 717
		private sealed class TransformInfo
		{
			// Token: 0x0600106E RID: 4206 RVA: 0x0005A860 File Offset: 0x00058A60
			public TransformInfo(IHost host, RffTransform.Column item, RffTransform.Arguments args, int d, float avgDist)
			{
				this.SrcDim = d;
				this.NewDim = item.newDim ?? args.newDim;
				Contracts.CheckUserArg(host, this.NewDim > 0, "newDim");
				this._useSin = item.useSin ?? args.useSin;
				int? seed = item.seed;
				int? num = ((seed != null) ? new int?(seed.GetValueOrDefault()) : args.seed);
				this._rand = ((num != null) ? RandomUtils.Create(num) : RandomUtils.Create(host.Rand));
				this._state = this._rand.GetState();
				SubComponent<IFourierDistributionSampler, SignatureFourierDistributionSampler> subComponent = item.matrixGenerator;
				if (!SubComponentExtensions.IsGood(subComponent))
				{
					subComponent = args.matrixGenerator;
				}
				this._matrixGenerator = ComponentCatalog.CreateInstance<IFourierDistributionSampler, SignatureFourierDistributionSampler>(subComponent, new object[] { host, avgDist });
				int num2 = RffTransform.RoundUp(this.NewDim, 4);
				int num3 = RffTransform.RoundUp(this.SrcDim, 4);
				this.RndFourierVectors = new AlignedArray(num2 * num3, 16);
				this.RotationTerms = (this._useSin ? null : new AlignedArray(num2, 16));
				this.InitializeFourierCoefficients(num3, num2);
			}

			// Token: 0x0600106F RID: 4207 RVA: 0x0005A9BC File Offset: 0x00058BBC
			public TransformInfo(ModelLoadContext ctx, IHostEnvironment env, int colValueCount, string directoryName)
			{
				this.SrcDim = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(env, this.SrcDim == colValueCount);
				this.NewDim = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(env, this.NewDim > 0);
				this._useSin = Utils.ReadBoolByte(ctx.Reader);
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(env, num == 4);
				this._state = TauswortheHybrid.State.Load(ctx.Reader);
				this._rand = new TauswortheHybrid(this._state);
				Contracts.CheckDecode(env, ctx.Repository != null && ctx.LoadModelOrNull<IFourierDistributionSampler, SignatureLoadModel>(out this._matrixGenerator, directoryName, new object[] { env }));
				int num2 = RffTransform.RoundUp(this.NewDim, 4);
				int num3 = RffTransform.RoundUp(this.SrcDim, 4);
				this.RndFourierVectors = new AlignedArray(num2 * num3, 16);
				this.RotationTerms = (this._useSin ? null : new AlignedArray(num2, 16));
				this.InitializeFourierCoefficients(num3, num2);
			}

			// Token: 0x06001070 RID: 4208 RVA: 0x0005AAD0 File Offset: 0x00058CD0
			public void Save(ModelSaveContext ctx, string directoryName)
			{
				ctx.Writer.Write(this.SrcDim);
				ctx.Writer.Write(this.NewDim);
				Utils.WriteBoolByte(ctx.Writer, this._useSin);
				ctx.Writer.Write(4);
				this._state.Save(ctx.Writer);
				ctx.SaveModel<IFourierDistributionSampler>(this._matrixGenerator, directoryName);
			}

			// Token: 0x06001071 RID: 4209 RVA: 0x0005AB40 File Offset: 0x00058D40
			private void GetDDimensionalFeatureMapping(int rowSize)
			{
				for (int i = 0; i < this.NewDim; i++)
				{
					for (int j = 0; j < this.SrcDim; j++)
					{
						this.RndFourierVectors[i * rowSize + j] = this._matrixGenerator.Next(this._rand);
					}
				}
			}

			// Token: 0x06001072 RID: 4210 RVA: 0x0005AB90 File Offset: 0x00058D90
			private void GetDRotationTerms(int colSize)
			{
				for (int i = 0; i < colSize; i++)
				{
					this.RotationTerms[i] = (RandomUtils.NextFloat(this._rand) - 0.5f) * 3.1415927f;
				}
			}

			// Token: 0x06001073 RID: 4211 RVA: 0x0005ABCC File Offset: 0x00058DCC
			private void InitializeFourierCoefficients(int rowSize, int colSize)
			{
				this.GetDDimensionalFeatureMapping(rowSize);
				if (!this._useSin)
				{
					this.GetDRotationTerms(this.NewDim);
				}
			}

			// Token: 0x04000919 RID: 2329
			public readonly int NewDim;

			// Token: 0x0400091A RID: 2330
			public readonly int SrcDim;

			// Token: 0x0400091B RID: 2331
			public readonly AlignedArray RndFourierVectors;

			// Token: 0x0400091C RID: 2332
			public readonly AlignedArray RotationTerms;

			// Token: 0x0400091D RID: 2333
			private readonly IFourierDistributionSampler _matrixGenerator;

			// Token: 0x0400091E RID: 2334
			private readonly bool _useSin;

			// Token: 0x0400091F RID: 2335
			private readonly TauswortheHybrid _rand;

			// Token: 0x04000920 RID: 2336
			private readonly TauswortheHybrid.State _state;
		}
	}
}
