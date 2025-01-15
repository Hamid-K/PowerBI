using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;
using Microsoft.TMSN.TMSNlearn;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000330 RID: 816
	public sealed class NormalizeTransform : OneToOneTransformBase
	{
		// Token: 0x06001228 RID: 4648 RVA: 0x00065C00 File Offset: 0x00063E00
		public static NormalizeTransform Create(NormalizeTransform.MinMaxArguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Normalize(MinMax)");
			NormalizeTransform normalizeTransform;
			using (IChannel channel = host.Start("Training"))
			{
				normalizeTransform = NormalizeTransform.Create<NormalizeTransform.MinMaxArguments>(args, host, input, new Func<NormalizeTransform.MinMaxArguments, IHost, int, int, ColumnType, IRowCursor, IColumnFunctionBuilder>(NormalizeTransform.MinMaxUtils.CreateBuilder), new int[0]);
				channel.Done();
			}
			return normalizeTransform;
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00065C70 File Offset: 0x00063E70
		public static NormalizeTransform Create(NormalizeTransform.MeanVarArguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Normalize(MeanVar)");
			NormalizeTransform normalizeTransform;
			using (IChannel channel = host.Start("Training"))
			{
				normalizeTransform = NormalizeTransform.Create<NormalizeTransform.MeanVarArguments>(args, host, input, new Func<NormalizeTransform.MeanVarArguments, IHost, int, int, ColumnType, IRowCursor, IColumnFunctionBuilder>(NormalizeTransform.MeanVarUtils.CreateBuilder), new int[0]);
				channel.Done();
			}
			return normalizeTransform;
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00065CE0 File Offset: 0x00063EE0
		public static NormalizeTransform Create(NormalizeTransform.LogMeanVarArguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Normalize(LogMeanVar)");
			NormalizeTransform normalizeTransform;
			using (IChannel channel = host.Start("Training"))
			{
				normalizeTransform = NormalizeTransform.Create<NormalizeTransform.LogMeanVarArguments>(args, host, input, new Func<NormalizeTransform.LogMeanVarArguments, IHost, int, int, ColumnType, IRowCursor, IColumnFunctionBuilder>(NormalizeTransform.LogMeanVarUtils.CreateBuilder), new int[0]);
				channel.Done();
			}
			return normalizeTransform;
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00065D50 File Offset: 0x00063F50
		public static NormalizeTransform Create(NormalizeTransform.BinArguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Normalize(Bin)");
			NormalizeTransform normalizeTransform;
			using (IChannel channel = host.Start("Training"))
			{
				normalizeTransform = NormalizeTransform.Create<NormalizeTransform.BinArguments>(args, host, input, new Func<NormalizeTransform.BinArguments, IHost, int, int, ColumnType, IRowCursor, IColumnFunctionBuilder>(NormalizeTransform.BinUtils.CreateBuilder), new int[0]);
				channel.Done();
			}
			return normalizeTransform;
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00065DC0 File Offset: 0x00063FC0
		public static NormalizeTransform Create(NormalizeTransform.SupervisedBinArguments args, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost host = env.Register("Normalize(SupervisedBin)");
			NormalizeTransform normalizeTransform;
			using (IChannel channel = host.Start("Training"))
			{
				int labelColumnId = NormalizeTransform.SupervisedBinUtils.GetLabelColumnId(channel, input.Schema, args.labelColumn);
				normalizeTransform = NormalizeTransform.Create<NormalizeTransform.SupervisedBinArguments>(args, host, input, new Func<NormalizeTransform.SupervisedBinArguments, IHost, int, int, ColumnType, IRowCursor, IColumnFunctionBuilder>(NormalizeTransform.SupervisedBinUtils.CreateBuilder), new int[] { labelColumnId });
				channel.Done();
			}
			return normalizeTransform;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00065E4C File Offset: 0x0006404C
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("NORMFUNC", 65539U, 65539U, 65539U, "NormalizeTransform", "NormalizeFunction");
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00065E98 File Offset: 0x00064098
		private static NormalizeTransform Create<TArgs>(TArgs args, IHost host, IDataView input, Func<TArgs, IHost, int, int, ColumnType, IRowCursor, IColumnFunctionBuilder> fnCreate, params int[] extraTrainColumnIds) where TArgs : NormalizeTransform.ArgumentsBase
		{
			Contracts.CheckValue<TArgs>(host, args, "args");
			Contracts.CheckValue<IDataView>(host, input, "input");
			Func<int, int, ColumnType, IRowCursor, IColumnFunctionBuilder> func = (int iinfo, int colSrc, ColumnType typeSrc, IRowCursor curs) => fnCreate(args, host, iinfo, colSrc, typeSrc, curs);
			return new NormalizeTransform(args, host, input, func, extraTrainColumnIds);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00065F38 File Offset: 0x00064138
		private NormalizeTransform(NormalizeTransform.ArgumentsBase args, IHost host, IDataView input, Func<int, int, ColumnType, IRowCursor, IColumnFunctionBuilder> fnCreate, params int[] extraTrainColumnIds)
			: base(host, Contracts.CheckRef<NormalizeTransform.ArgumentsBase>(host, args, "args").GetColumns(), input, new Func<ColumnType, string>(args.TestType))
		{
			bool[] activeInput = new bool[this._input.Schema.ColumnCount];
			if (Utils.Size<int>(extraTrainColumnIds) > 0)
			{
				foreach (int num in extraTrainColumnIds)
				{
					activeInput[num] = true;
				}
			}
			foreach (OneToOneTransformBase.ColInfo colInfo in this.Infos)
			{
				activeInput[colInfo.Source] = true;
			}
			IColumnFunctionBuilder[] array = new IColumnFunctionBuilder[this.Infos.Length];
			bool[] array2 = new bool[this.Infos.Length];
			using (IProgressChannel progressChannel = this._host.StartProgressChannel("Normalize"))
			{
				long numRows = 0L;
				progressChannel.SetHeader(new ProgressHeader(new string[] { "examples" }), delegate(IProgressEntry e)
				{
					e.SetProgress(0, (double)numRows);
				});
				using (IRowCursor rowCursor = this._input.GetRowCursor((int col) => activeInput[col], null))
				{
					for (int k = 0; k < this.Infos.Length; k++)
					{
						array2[k] = true;
						OneToOneTransformBase.ColInfo colInfo2 = this.Infos[k];
						array[k] = fnCreate(k, colInfo2.Source, colInfo2.TypeSrc, rowCursor);
					}
					while (rowCursor.MoveNext())
					{
						bool flag = false;
						for (int l = 0; l < this.Infos.Length; l++)
						{
							if (array2[l])
							{
								OneToOneTransformBase.ColInfo colInfo3 = this.Infos[l];
								flag |= (array2[l] = array[l].ProcessValue());
							}
						}
						numRows += 1L;
						if (!flag)
						{
							break;
						}
					}
				}
				progressChannel.Checkpoint(new double?[]
				{
					new double?((double)numRows)
				});
				this._functions = new IColumnFunction[this.Infos.Length];
				for (int m = 0; m < this.Infos.Length; m++)
				{
					this._functions[m] = array[m].CreateColumnFunction();
				}
			}
			this.SetMetadata();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x000661E8 File Offset: 0x000643E8
		private NormalizeTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, null)
		{
			this._functions = new IColumnFunction[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ColumnType typeSrc = this.Infos[i].TypeSrc;
				if (typeSrc.ValueCount == 0)
				{
					throw Contracts.Except(this._host, "Column '{0}' is a vector of variable size, which is not supported for normalizers", new object[] { this.Infos[i].Name });
				}
				string text = string.Format("Normalizer_{0:000}", i);
				ctx.LoadModel<IColumnFunction, SignatureLoadColumnFunction>(out this._functions[i], text, new object[] { this._host, typeSrc });
			}
			this.SetMetadata();
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x000662F8 File Offset: 0x000644F8
		public static NormalizeTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("Normalize");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			ctx.CheckAtModel(NormalizeTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(h, input, "input");
			return HostExtensions.Apply<NormalizeTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new NormalizeTransform(ctx, h, input);
			});
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00066390 File Offset: 0x00064590
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(NormalizeTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
			for (int i = 0; i < this._functions.Length; i++)
			{
				string text = string.Format("Normalizer_{0:000}", i);
				ctx.SaveSubModel(text, new Action<ModelSaveContext>(this._functions[i].Save));
			}
		}

		// Token: 0x06001233 RID: 4659 RVA: 0x00066411 File Offset: 0x00064611
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this.Infos[iinfo].TypeSrc;
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00066420 File Offset: 0x00064620
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

		// Token: 0x06001235 RID: 4661 RVA: 0x000664A4 File Offset: 0x000646A4
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			return this._functions[iinfo].GetGetter(input, this.Infos[iinfo].Source);
		}

		// Token: 0x04000A94 RID: 2708
		internal const string MinMaxNormalizerSummary = "Normalizes the data based on the observed minimum and maximum values of the data.";

		// Token: 0x04000A95 RID: 2709
		internal const string MeanVarNormalizerSummary = "Normalizes the data based on the computed mean and variance of the data.";

		// Token: 0x04000A96 RID: 2710
		internal const string LogMeanVarNormalizerSummary = "Normalizes the data based on the computed mean and variance of the logarithm of the data.";

		// Token: 0x04000A97 RID: 2711
		internal const string BinNormalizerSummary = "The values are assigned into equidensity bins and a value is mapped to its bin_number/number_of_bins.";

		// Token: 0x04000A98 RID: 2712
		internal const string SupervisedBinNormalizerSummary = "Similar to BinNormalizer, but calculates bins based on correlation with the label column, not equi-density. The new value is bin_number / number_of_bins.";

		// Token: 0x04000A99 RID: 2713
		public const string LoaderSignature = "NormalizeTransform";

		// Token: 0x04000A9A RID: 2714
		internal const string LoaderSignatureOld = "NormalizeFunction";

		// Token: 0x04000A9B RID: 2715
		private readonly IColumnFunction[] _functions;

		// Token: 0x02000331 RID: 817
		public abstract class ColumnBase : OneToOneColumn
		{
			// Token: 0x06001236 RID: 4662 RVA: 0x000664C5 File Offset: 0x000646C5
			protected override bool TryUnparseCore(StringBuilder sb)
			{
				return this.maxTrainingExamples == null && base.TryUnparseCore(sb);
			}

			// Token: 0x04000A9C RID: 2716
			[Argument(0, HelpText = "Max number of examples used to train the normalizer", ShortName = "maxtrain")]
			public long? maxTrainingExamples;
		}

		// Token: 0x02000332 RID: 818
		public abstract class FixZeroColumnBase : NormalizeTransform.ColumnBase
		{
			// Token: 0x06001238 RID: 4664 RVA: 0x000664E5 File Offset: 0x000646E5
			protected override bool TryUnparseCore(StringBuilder sb)
			{
				return this.fixZero == null && base.TryUnparseCore(sb);
			}

			// Token: 0x04000A9D RID: 2717
			[Argument(0, HelpText = "Whether to map zero to zero, preserving sparsity", ShortName = "zero")]
			public bool? fixZero;
		}

		// Token: 0x02000333 RID: 819
		public sealed class AffineColumn : NormalizeTransform.FixZeroColumnBase
		{
			// Token: 0x0600123A RID: 4666 RVA: 0x00066508 File Offset: 0x00064708
			public static NormalizeTransform.AffineColumn Parse(string str)
			{
				NormalizeTransform.AffineColumn affineColumn = new NormalizeTransform.AffineColumn();
				if (affineColumn.TryParse(str))
				{
					return affineColumn;
				}
				return null;
			}

			// Token: 0x0600123B RID: 4667 RVA: 0x00066527 File Offset: 0x00064727
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x02000334 RID: 820
		public sealed class BinColumn : NormalizeTransform.FixZeroColumnBase
		{
			// Token: 0x0600123D RID: 4669 RVA: 0x00066538 File Offset: 0x00064738
			public static NormalizeTransform.BinColumn Parse(string str)
			{
				NormalizeTransform.BinColumn binColumn = new NormalizeTransform.BinColumn();
				if (binColumn.TryParse(str))
				{
					return binColumn;
				}
				return null;
			}

			// Token: 0x0600123E RID: 4670 RVA: 0x00066557 File Offset: 0x00064757
			public bool TryUnparse(StringBuilder sb)
			{
				return this.numBins == null && this.TryUnparseCore(sb);
			}

			// Token: 0x04000A9E RID: 2718
			[Argument(0, HelpText = "Max number of bins, power of 2 recommended", ShortName = "bins")]
			[TGUI(Label = "Max number of bins")]
			public int? numBins;
		}

		// Token: 0x02000335 RID: 821
		public sealed class LogNormalColumn : NormalizeTransform.ColumnBase
		{
			// Token: 0x06001240 RID: 4672 RVA: 0x00066578 File Offset: 0x00064778
			public static NormalizeTransform.LogNormalColumn Parse(string str)
			{
				NormalizeTransform.LogNormalColumn logNormalColumn = new NormalizeTransform.LogNormalColumn();
				if (logNormalColumn.TryParse(str))
				{
					return logNormalColumn;
				}
				return null;
			}

			// Token: 0x06001241 RID: 4673 RVA: 0x00066597 File Offset: 0x00064797
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x02000336 RID: 822
		public abstract class ArgumentsBase
		{
			// Token: 0x06001243 RID: 4675
			public abstract OneToOneColumn[] GetColumns();

			// Token: 0x06001244 RID: 4676 RVA: 0x000665A8 File Offset: 0x000647A8
			public string TestType(ColumnType type)
			{
				if (type.ItemType != NumberType.R4 && type.ItemType != NumberType.R8)
				{
					return "Expected R4 or R8 item type";
				}
				if (type.IsVector && !type.IsKnownSizeVector)
				{
					return "Expected known size vector";
				}
				return null;
			}

			// Token: 0x04000A9F RID: 2719
			[Argument(0, HelpText = "Max number of examples used to train the normalizer", ShortName = "maxtrain")]
			public long maxTrainingExamples = 1000000000L;
		}

		// Token: 0x02000337 RID: 823
		public abstract class FixZeroArgumentsBase : NormalizeTransform.ArgumentsBase
		{
			// Token: 0x04000AA0 RID: 2720
			[Argument(0, HelpText = "Whether to map zero to zero, preserving sparsity", ShortName = "zero")]
			public bool fixZero = true;
		}

		// Token: 0x02000338 RID: 824
		public abstract class AffineArgumentsBase : NormalizeTransform.FixZeroArgumentsBase
		{
			// Token: 0x06001247 RID: 4679 RVA: 0x00066604 File Offset: 0x00064804
			public override OneToOneColumn[] GetColumns()
			{
				return this.column;
			}

			// Token: 0x04000AA1 RID: 2721
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public NormalizeTransform.AffineColumn[] column;
		}

		// Token: 0x02000339 RID: 825
		public sealed class MinMaxArguments : NormalizeTransform.AffineArgumentsBase
		{
		}

		// Token: 0x0200033A RID: 826
		public sealed class MeanVarArguments : NormalizeTransform.AffineArgumentsBase
		{
			// Token: 0x04000AA2 RID: 2722
			[Argument(0, HelpText = "Whether to use CDF as the output", ShortName = "cdf")]
			public bool useCdf;
		}

		// Token: 0x0200033B RID: 827
		public sealed class LogMeanVarArguments : NormalizeTransform.ArgumentsBase
		{
			// Token: 0x0600124B RID: 4683 RVA: 0x00066624 File Offset: 0x00064824
			public override OneToOneColumn[] GetColumns()
			{
				return this.column;
			}

			// Token: 0x04000AA3 RID: 2723
			[Argument(0, HelpText = "Whether to use CDF as the output", ShortName = "cdf")]
			public bool useCdf = true;

			// Token: 0x04000AA4 RID: 2724
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public NormalizeTransform.LogNormalColumn[] column;
		}

		// Token: 0x0200033C RID: 828
		public abstract class BinArgumentsBase : NormalizeTransform.FixZeroArgumentsBase
		{
			// Token: 0x0600124D RID: 4685 RVA: 0x0006663B File Offset: 0x0006483B
			public override OneToOneColumn[] GetColumns()
			{
				return this.column;
			}

			// Token: 0x04000AA5 RID: 2725
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public NormalizeTransform.BinColumn[] column;

			// Token: 0x04000AA6 RID: 2726
			[Argument(0, HelpText = "Max number of bins, power of 2 recommended", ShortName = "bins")]
			[TGUI(Label = "Max number of bins")]
			public int numBins = 1024;
		}

		// Token: 0x0200033D RID: 829
		public sealed class BinArguments : NormalizeTransform.BinArgumentsBase
		{
		}

		// Token: 0x0200033E RID: 830
		public sealed class SupervisedBinArguments : NormalizeTransform.BinArgumentsBase
		{
			// Token: 0x04000AA7 RID: 2727
			[Argument(1, HelpText = "Label column for supervised binning", ShortName = "label,lab", Purpose = "ColumnName")]
			public string labelColumn;

			// Token: 0x04000AA8 RID: 2728
			[Argument(0, HelpText = "Minimum number of examples per bin")]
			public int minBinSize = 10;
		}

		// Token: 0x02000340 RID: 832
		public abstract class AffineColumnFunction : IColumnFunction, ICanSaveModel
		{
			// Token: 0x06001252 RID: 4690 RVA: 0x0006666E File Offset: 0x0006486E
			private AffineColumnFunction(IHost host)
			{
				this._host = host;
			}

			// Token: 0x06001253 RID: 4691
			public abstract void Save(ModelSaveContext ctx);

			// Token: 0x06001254 RID: 4692
			public abstract Delegate GetGetter(IRow input, int icol);

			// Token: 0x06001255 RID: 4693 RVA: 0x00066680 File Offset: 0x00064880
			public static NormalizeTransform.AffineColumnFunction Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
			{
				if (typeSrc.IsNumber)
				{
					if (typeSrc == NumberType.R4)
					{
						return NormalizeTransform.AffineColumnFunction.Sng.ImplOne.Create(ctx, host, typeSrc);
					}
					if (typeSrc == NumberType.R8)
					{
						return NormalizeTransform.AffineColumnFunction.Dbl.ImplOne.Create(ctx, host, typeSrc);
					}
				}
				else if (typeSrc.ItemType.IsNumber)
				{
					if (typeSrc.ItemType == NumberType.R4)
					{
						return NormalizeTransform.AffineColumnFunction.Sng.ImplVec.Create(ctx, host, typeSrc);
					}
					if (typeSrc.ItemType == NumberType.R8)
					{
						return NormalizeTransform.AffineColumnFunction.Dbl.ImplVec.Create(ctx, host, typeSrc);
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {0}.", new object[] { typeSrc.ToString() });
			}

			// Token: 0x06001256 RID: 4694 RVA: 0x00066711 File Offset: 0x00064911
			public static IColumnFunction Create(IHost host, double scale, double offset)
			{
				return new NormalizeTransform.AffineColumnFunction.Dbl.ImplOne(host, scale, offset);
			}

			// Token: 0x06001257 RID: 4695 RVA: 0x0006671B File Offset: 0x0006491B
			public static IColumnFunction Create(IHost host, double[] scale, double[] offset, int[] indicesNonZeroOffset)
			{
				return new NormalizeTransform.AffineColumnFunction.Dbl.ImplVec(host, scale, offset, indicesNonZeroOffset);
			}

			// Token: 0x06001258 RID: 4696 RVA: 0x00066726 File Offset: 0x00064926
			public static IColumnFunction Create(IHost host, float scale, float offset)
			{
				return new NormalizeTransform.AffineColumnFunction.Sng.ImplOne(host, scale, offset);
			}

			// Token: 0x06001259 RID: 4697 RVA: 0x00066730 File Offset: 0x00064930
			public static IColumnFunction Create(IHost host, float[] scale, float[] offset, int[] indicesNonZeroOffset)
			{
				return new NormalizeTransform.AffineColumnFunction.Sng.ImplVec(host, scale, offset, indicesNonZeroOffset);
			}

			// Token: 0x04000AA9 RID: 2729
			protected readonly IHost _host;

			// Token: 0x02000341 RID: 833
			private abstract class ImplOne<TFloat> : NormalizeTransform.AffineColumnFunction
			{
				// Token: 0x0600125A RID: 4698 RVA: 0x0006673B File Offset: 0x0006493B
				protected ImplOne(IHost host, TFloat scale, TFloat offset)
					: base(host)
				{
					this._scale = scale;
					this._offset = offset;
				}

				// Token: 0x04000AAA RID: 2730
				protected readonly TFloat _scale;

				// Token: 0x04000AAB RID: 2731
				protected readonly TFloat _offset;
			}

			// Token: 0x02000342 RID: 834
			private abstract class ImplVec<TFloat> : NormalizeTransform.AffineColumnFunction
			{
				// Token: 0x0600125B RID: 4699 RVA: 0x00066752 File Offset: 0x00064952
				protected ImplVec(IHost host, TFloat[] scale, TFloat[] offset, int[] indicesNonZeroOffset)
					: base(host)
				{
					this._scale = scale;
					this._offset = offset;
					this._indicesNonZeroOffset = indicesNonZeroOffset;
				}

				// Token: 0x04000AAC RID: 2732
				protected readonly TFloat[] _scale;

				// Token: 0x04000AAD RID: 2733
				protected readonly TFloat[] _offset;

				// Token: 0x04000AAE RID: 2734
				protected readonly int[] _indicesNonZeroOffset;
			}

			// Token: 0x02000343 RID: 835
			private static class Dbl
			{
				// Token: 0x02000344 RID: 836
				public sealed class ImplOne : NormalizeTransform.AffineColumnFunction.ImplOne<double>
				{
					// Token: 0x0600125C RID: 4700 RVA: 0x00066771 File Offset: 0x00064971
					public ImplOne(IHost host, double scale, double offset)
						: base(host, scale, offset)
					{
					}

					// Token: 0x0600125D RID: 4701 RVA: 0x0006677C File Offset: 0x0006497C
					public new static NormalizeTransform.AffineColumnFunction.Dbl.ImplOne Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.RawType == typeof(double), "The column type must be R8.");
						List<int> list = null;
						int num;
						double[] array;
						double[] array2;
						int[] array3;
						double[] array4;
						double[] array5;
						AffineNormSerializationUtils.LoadModel(ctx, ref list, out num, out array, out array2, out array3, out array4, out array5);
						if (num != 1)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has 1 slot.", new object[] { num });
						}
						return new NormalizeTransform.AffineColumnFunction.Dbl.ImplOne(host, array[0], (array2 != null) ? array2[0] : 0.0);
					}

					// Token: 0x0600125E RID: 4702 RVA: 0x000667FD File Offset: 0x000649FD
					private void GetResult(ref double input, ref double value)
					{
						value = (input - this._offset) * this._scale;
					}

					// Token: 0x0600125F RID: 4703 RVA: 0x00066814 File Offset: 0x00064A14
					public override void Save(ModelSaveContext ctx)
					{
						AffineNormSerializationUtils.SaveModel(ctx, 1, null, new double[] { this._scale }, new double[] { this._offset }, true);
					}

					// Token: 0x06001260 RID: 4704 RVA: 0x00066870 File Offset: 0x00064A70
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<double> getSrc = input.GetGetter<double>(icol);
						return new ValueGetter<double>(delegate(ref double dst)
						{
							getSrc.Invoke(ref dst);
							this.GetResult(ref dst, ref dst);
						});
					}
				}

				// Token: 0x02000345 RID: 837
				public sealed class ImplVec : NormalizeTransform.AffineColumnFunction.ImplVec<double>
				{
					// Token: 0x06001261 RID: 4705 RVA: 0x000668A5 File Offset: 0x00064AA5
					public ImplVec(IHost host, double[] scale, double[] offset, int[] indicesNonZeroOffset)
						: base(host, scale, offset, indicesNonZeroOffset)
					{
					}

					// Token: 0x06001262 RID: 4706 RVA: 0x000668B4 File Offset: 0x00064AB4
					public new static NormalizeTransform.AffineColumnFunction.Dbl.ImplVec Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.ItemType.RawType == typeof(double), "The column type must be vector of R8.");
						int num = Math.Max(1, typeSrc.VectorSize);
						List<int> list = null;
						int num2;
						double[] array;
						double[] array2;
						int[] array3;
						double[] array4;
						double[] array5;
						AffineNormSerializationUtils.LoadModel(ctx, ref list, out num2, out array, out array2, out array3, out array4, out array5);
						if (num2 != num)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has {1} slots.", new object[] { num2, num });
						}
						return new NormalizeTransform.AffineColumnFunction.Dbl.ImplVec(host, array, array2, (array2 != null && list.Count < num / 2) ? list.ToArray() : null);
					}

					// Token: 0x06001263 RID: 4707 RVA: 0x00066958 File Offset: 0x00064B58
					public override void Save(ModelSaveContext ctx)
					{
						AffineNormSerializationUtils.SaveModel(ctx, this._scale.Length, null, this._scale, this._offset, true);
					}

					// Token: 0x06001264 RID: 4708 RVA: 0x00066AA8 File Offset: 0x00064CA8
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<VBuffer<double>> getSrc = input.GetGetter<VBuffer<double>>(icol);
						VBufferBuilder<double> bldr = new VBufferBuilder<double>(R8Adder.Instance);
						ValueGetter<VBuffer<double>> valueGetter;
						if (this._offset == null)
						{
							valueGetter = delegate(ref VBuffer<double> dst)
							{
								getSrc.Invoke(ref dst);
								Contracts.Check(dst.Length == this._scale.Length);
								NormalizeTransform.AffineColumnFunction.Dbl.ImplVec.FillValues(ref dst, bldr, this._scale);
								bldr.GetResult(ref dst);
							};
						}
						else if (this._indicesNonZeroOffset == null)
						{
							valueGetter = delegate(ref VBuffer<double> dst)
							{
								getSrc.Invoke(ref dst);
								Contracts.Check(dst.Length == this._scale.Length);
								NormalizeTransform.AffineColumnFunction.Dbl.ImplVec.FillValues(ref dst, bldr, this._scale, this._offset);
								bldr.GetResult(ref dst);
							};
						}
						else
						{
							valueGetter = delegate(ref VBuffer<double> dst)
							{
								getSrc.Invoke(ref dst);
								Contracts.Check(dst.Length == this._scale.Length);
								NormalizeTransform.AffineColumnFunction.Dbl.ImplVec.FillValues(ref dst, bldr, this._scale, this._offset, this._indicesNonZeroOffset);
								bldr.GetResult(ref dst);
							};
						}
						return valueGetter;
					}

					// Token: 0x06001265 RID: 4709 RVA: 0x00066B38 File Offset: 0x00064D38
					private static void FillValues(ref VBuffer<double> input, VBufferBuilder<double> bldr, double[] scale)
					{
						int num = scale.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							return;
						}
						double[] values = input.Values;
						if (count >= num)
						{
							for (int i = 0; i < num; i++)
							{
								bldr.AddFeature(i, values[i] * scale[i]);
							}
							return;
						}
						int[] indices = input.Indices;
						for (int j = 0; j < count; j++)
						{
							int num2 = indices[j];
							bldr.AddFeature(num2, values[j] * scale[num2]);
						}
					}

					// Token: 0x06001266 RID: 4710 RVA: 0x00066BB4 File Offset: 0x00064DB4
					private static void FillValues(ref VBuffer<double> input, VBufferBuilder<double> bldr, double[] scale, double[] offset)
					{
						int num = scale.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							for (int i = 0; i < num; i++)
							{
								bldr.AddFeature(i, -offset[i] * scale[i]);
							}
							return;
						}
						double[] values = input.Values;
						if (count >= num)
						{
							for (int j = 0; j < num; j++)
							{
								bldr.AddFeature(j, (values[j] - offset[j]) * scale[j]);
							}
							return;
						}
						int[] indices = input.Indices;
						int num2 = 0;
						int num3 = indices[num2];
						for (int k = 0; k < num; k++)
						{
							if (k == num3)
							{
								bldr.AddFeature(k, (values[num2] - offset[k]) * scale[k]);
								num3 = ((++num2 < count) ? indices[num2] : num);
							}
							else
							{
								bldr.AddFeature(k, -offset[k] * scale[k]);
							}
						}
					}

					// Token: 0x06001267 RID: 4711 RVA: 0x00066C8C File Offset: 0x00064E8C
					private static void FillValues(ref VBuffer<double> input, VBufferBuilder<double> bldr, double[] scale, double[] offset, int[] nz)
					{
						int num = scale.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							foreach (int num2 in nz)
							{
								bldr.AddFeature(num2, -offset[num2] * scale[num2]);
							}
							return;
						}
						double[] values = input.Values;
						if (count >= num)
						{
							for (int j = 0; j < num; j++)
							{
								bldr.AddFeature(j, (values[j] - offset[j]) * scale[j]);
							}
							return;
						}
						int[] indices = input.Indices;
						int num3 = 0;
						int num4 = indices[num3];
						int num5 = 0;
						int num6 = nz[num5];
						for (;;)
						{
							int num7 = num4 - num6;
							if (num7 > 0)
							{
								bldr.AddFeature(num6, -offset[num6] * scale[num6]);
								num6 = ((++num5 < nz.Length) ? nz[num5] : num);
							}
							else if (num7 < 0)
							{
								bldr.AddFeature(num4, values[num3] * scale[num4]);
								num4 = ((++num3 < count) ? indices[num3] : num);
							}
							else
							{
								if (num6 >= num)
								{
									break;
								}
								bldr.AddFeature(num6, (values[num3] - offset[num6]) * scale[num6]);
								num4 = ((++num3 < count) ? indices[num3] : num);
								num6 = ((++num5 < nz.Length) ? nz[num5] : num);
							}
						}
					}
				}
			}

			// Token: 0x02000346 RID: 838
			private static class Sng
			{
				// Token: 0x02000347 RID: 839
				public sealed class ImplOne : NormalizeTransform.AffineColumnFunction.ImplOne<float>
				{
					// Token: 0x06001268 RID: 4712 RVA: 0x00066DD8 File Offset: 0x00064FD8
					public ImplOne(IHost host, float scale, float offset)
						: base(host, scale, offset)
					{
					}

					// Token: 0x06001269 RID: 4713 RVA: 0x00066DE4 File Offset: 0x00064FE4
					public new static NormalizeTransform.AffineColumnFunction.Sng.ImplOne Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.RawType == typeof(float), "The column type must be R4.");
						List<int> list = null;
						int num;
						float[] array;
						float[] array2;
						int[] array3;
						float[] array4;
						float[] array5;
						AffineNormSerializationUtils.LoadModel(ctx, ref list, out num, out array, out array2, out array3, out array4, out array5);
						if (num != 1)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has 1 slot.", new object[] { num });
						}
						return new NormalizeTransform.AffineColumnFunction.Sng.ImplOne(host, array[0], (array2 != null) ? array2[0] : 0f);
					}

					// Token: 0x0600126A RID: 4714 RVA: 0x00066E61 File Offset: 0x00065061
					private void GetResult(ref float input, ref float value)
					{
						value = (input - this._offset) * this._scale;
					}

					// Token: 0x0600126B RID: 4715 RVA: 0x00066E78 File Offset: 0x00065078
					public override void Save(ModelSaveContext ctx)
					{
						AffineNormSerializationUtils.SaveModel(ctx, 1, null, new float[] { this._scale }, new float[] { this._offset }, true);
					}

					// Token: 0x0600126C RID: 4716 RVA: 0x00066ED4 File Offset: 0x000650D4
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<float> getSrc = input.GetGetter<float>(icol);
						return new ValueGetter<float>(delegate(ref float dst)
						{
							getSrc.Invoke(ref dst);
							this.GetResult(ref dst, ref dst);
						});
					}
				}

				// Token: 0x02000348 RID: 840
				public sealed class ImplVec : NormalizeTransform.AffineColumnFunction.ImplVec<float>
				{
					// Token: 0x0600126D RID: 4717 RVA: 0x00066F09 File Offset: 0x00065109
					public ImplVec(IHost host, float[] scale, float[] offset, int[] indicesNonZeroOffset)
						: base(host, scale, offset, indicesNonZeroOffset)
					{
					}

					// Token: 0x0600126E RID: 4718 RVA: 0x00066F18 File Offset: 0x00065118
					public new static NormalizeTransform.AffineColumnFunction.Sng.ImplVec Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.ItemType.RawType == typeof(float), "The column type must be vector of R4.");
						int num = Math.Max(1, typeSrc.VectorSize);
						List<int> list = null;
						int num2;
						float[] array;
						float[] array2;
						int[] array3;
						float[] array4;
						float[] array5;
						AffineNormSerializationUtils.LoadModel(ctx, ref list, out num2, out array, out array2, out array3, out array4, out array5);
						if (num2 != num)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has {1} slots.", new object[] { num2, num });
						}
						return new NormalizeTransform.AffineColumnFunction.Sng.ImplVec(host, array, array2, (array2 != null && list.Count < num / 2) ? list.ToArray() : null);
					}

					// Token: 0x0600126F RID: 4719 RVA: 0x00066FBC File Offset: 0x000651BC
					public override void Save(ModelSaveContext ctx)
					{
						AffineNormSerializationUtils.SaveModel(ctx, this._scale.Length, null, this._scale, this._offset, true);
					}

					// Token: 0x06001270 RID: 4720 RVA: 0x0006710C File Offset: 0x0006530C
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<VBuffer<float>> getSrc = input.GetGetter<VBuffer<float>>(icol);
						VBufferBuilder<float> bldr = new VBufferBuilder<float>(R4Adder.Instance);
						ValueGetter<VBuffer<float>> valueGetter;
						if (this._offset == null)
						{
							valueGetter = delegate(ref VBuffer<float> dst)
							{
								getSrc.Invoke(ref dst);
								Contracts.Check(dst.Length == this._scale.Length);
								NormalizeTransform.AffineColumnFunction.Sng.ImplVec.FillValues(ref dst, bldr, this._scale);
								bldr.GetResult(ref dst);
							};
						}
						else if (this._indicesNonZeroOffset == null)
						{
							valueGetter = delegate(ref VBuffer<float> dst)
							{
								getSrc.Invoke(ref dst);
								Contracts.Check(dst.Length == this._scale.Length);
								NormalizeTransform.AffineColumnFunction.Sng.ImplVec.FillValues(ref dst, bldr, this._scale, this._offset);
								bldr.GetResult(ref dst);
							};
						}
						else
						{
							valueGetter = delegate(ref VBuffer<float> dst)
							{
								getSrc.Invoke(ref dst);
								Contracts.Check(dst.Length == this._scale.Length);
								NormalizeTransform.AffineColumnFunction.Sng.ImplVec.FillValues(ref dst, bldr, this._scale, this._offset, this._indicesNonZeroOffset);
								bldr.GetResult(ref dst);
							};
						}
						return valueGetter;
					}

					// Token: 0x06001271 RID: 4721 RVA: 0x0006719C File Offset: 0x0006539C
					private static void FillValues(ref VBuffer<float> input, VBufferBuilder<float> bldr, float[] scale)
					{
						int num = scale.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							return;
						}
						float[] values = input.Values;
						if (count >= num)
						{
							for (int i = 0; i < num; i++)
							{
								bldr.AddFeature(i, values[i] * scale[i]);
							}
							return;
						}
						int[] indices = input.Indices;
						for (int j = 0; j < count; j++)
						{
							int num2 = indices[j];
							bldr.AddFeature(num2, values[j] * scale[num2]);
						}
					}

					// Token: 0x06001272 RID: 4722 RVA: 0x00067218 File Offset: 0x00065418
					private static void FillValues(ref VBuffer<float> input, VBufferBuilder<float> bldr, float[] scale, float[] offset)
					{
						int num = scale.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							for (int i = 0; i < num; i++)
							{
								bldr.AddFeature(i, -offset[i] * scale[i]);
							}
							return;
						}
						float[] values = input.Values;
						if (count >= num)
						{
							for (int j = 0; j < num; j++)
							{
								bldr.AddFeature(j, (values[j] - offset[j]) * scale[j]);
							}
							return;
						}
						int[] indices = input.Indices;
						int num2 = 0;
						int num3 = indices[num2];
						for (int k = 0; k < num; k++)
						{
							if (k == num3)
							{
								bldr.AddFeature(k, (values[num2] - offset[k]) * scale[k]);
								num3 = ((++num2 < count) ? indices[num2] : num);
							}
							else
							{
								bldr.AddFeature(k, -offset[k] * scale[k]);
							}
						}
					}

					// Token: 0x06001273 RID: 4723 RVA: 0x000672F0 File Offset: 0x000654F0
					private static void FillValues(ref VBuffer<float> input, VBufferBuilder<float> bldr, float[] scale, float[] offset, int[] nz)
					{
						int num = scale.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							foreach (int num2 in nz)
							{
								bldr.AddFeature(num2, -offset[num2] * scale[num2]);
							}
							return;
						}
						float[] values = input.Values;
						if (count >= num)
						{
							for (int j = 0; j < num; j++)
							{
								bldr.AddFeature(j, (values[j] - offset[j]) * scale[j]);
							}
							return;
						}
						int[] indices = input.Indices;
						int num3 = 0;
						int num4 = indices[num3];
						int num5 = 0;
						int num6 = nz[num5];
						for (;;)
						{
							int num7 = num4 - num6;
							if (num7 > 0)
							{
								bldr.AddFeature(num6, -offset[num6] * scale[num6]);
								num6 = ((++num5 < nz.Length) ? nz[num5] : num);
							}
							else if (num7 < 0)
							{
								bldr.AddFeature(num4, values[num3] * scale[num4]);
								num4 = ((++num3 < count) ? indices[num3] : num);
							}
							else
							{
								if (num6 >= num)
								{
									break;
								}
								bldr.AddFeature(num6, (values[num3] - offset[num6]) * scale[num6]);
								num4 = ((++num3 < count) ? indices[num3] : num);
								num6 = ((++num5 < nz.Length) ? nz[num5] : num);
							}
						}
					}
				}
			}
		}

		// Token: 0x02000349 RID: 841
		public abstract class CdfColumnFunction : IColumnFunction, ICanSaveModel
		{
			// Token: 0x06001274 RID: 4724 RVA: 0x0006743C File Offset: 0x0006563C
			private CdfColumnFunction(IHost host)
			{
				this._host = host;
			}

			// Token: 0x06001275 RID: 4725
			public abstract void Save(ModelSaveContext ctx);

			// Token: 0x06001276 RID: 4726
			public abstract Delegate GetGetter(IRow input, int icol);

			// Token: 0x06001277 RID: 4727 RVA: 0x0006744C File Offset: 0x0006564C
			public static NormalizeTransform.CdfColumnFunction Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
			{
				if (typeSrc.IsNumber)
				{
					if (typeSrc == NumberType.R4)
					{
						return NormalizeTransform.CdfColumnFunction.Sng.ImplOne.Create(ctx, host, typeSrc);
					}
					if (typeSrc == NumberType.R8)
					{
						return NormalizeTransform.CdfColumnFunction.Dbl.ImplOne.Create(ctx, host, typeSrc);
					}
				}
				else if (typeSrc.ItemType.IsNumber)
				{
					if (typeSrc.ItemType == NumberType.R4)
					{
						return NormalizeTransform.CdfColumnFunction.Sng.ImplVec.Create(ctx, host, typeSrc);
					}
					if (typeSrc.ItemType == NumberType.R8)
					{
						return NormalizeTransform.CdfColumnFunction.Dbl.ImplVec.Create(ctx, host, typeSrc);
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {0}.", new object[] { typeSrc.ToString() });
			}

			// Token: 0x06001278 RID: 4728 RVA: 0x000674DD File Offset: 0x000656DD
			public static VersionInfo GetVersionInfo()
			{
				return new VersionInfo("CDFNORMF", 65537U, 65537U, 65537U, "CdfNormalizeFunction", null);
			}

			// Token: 0x06001279 RID: 4729 RVA: 0x000674FE File Offset: 0x000656FE
			public static IColumnFunction Create(IHost host, double mean, double stddev, bool useLog)
			{
				return new NormalizeTransform.CdfColumnFunction.Dbl.ImplOne(host, mean, stddev, useLog);
			}

			// Token: 0x0600127A RID: 4730 RVA: 0x00067509 File Offset: 0x00065709
			public static IColumnFunction Create(IHost host, double[] mean, double[] stddev, bool useLog)
			{
				return new NormalizeTransform.CdfColumnFunction.Dbl.ImplVec(host, mean, stddev, useLog);
			}

			// Token: 0x0600127B RID: 4731 RVA: 0x00067514 File Offset: 0x00065714
			public static IColumnFunction Create(IHost host, float mean, float stddev, bool useLog)
			{
				return new NormalizeTransform.CdfColumnFunction.Sng.ImplOne(host, mean, stddev, useLog);
			}

			// Token: 0x0600127C RID: 4732 RVA: 0x0006751F File Offset: 0x0006571F
			public static IColumnFunction Create(IHost host, float[] mean, float[] stddev, bool useLog)
			{
				return new NormalizeTransform.CdfColumnFunction.Sng.ImplVec(host, mean, stddev, useLog);
			}

			// Token: 0x04000AAF RID: 2735
			public const string LoaderSignature = "CdfNormalizeFunction";

			// Token: 0x04000AB0 RID: 2736
			protected readonly IHost _host;

			// Token: 0x0200034A RID: 842
			private abstract class ImplOne<TFloat> : NormalizeTransform.CdfColumnFunction
			{
				// Token: 0x0600127D RID: 4733 RVA: 0x0006752A File Offset: 0x0006572A
				protected ImplOne(IHost host, TFloat mean, TFloat stddev, bool useLog)
					: base(host)
				{
					this._mean = mean;
					this._stddev = stddev;
					this._useLog = useLog;
				}

				// Token: 0x04000AB1 RID: 2737
				protected readonly TFloat _mean;

				// Token: 0x04000AB2 RID: 2738
				protected readonly TFloat _stddev;

				// Token: 0x04000AB3 RID: 2739
				protected readonly bool _useLog;
			}

			// Token: 0x0200034B RID: 843
			private abstract class ImplVec<TFloat> : NormalizeTransform.CdfColumnFunction
			{
				// Token: 0x0600127E RID: 4734 RVA: 0x00067549 File Offset: 0x00065749
				protected ImplVec(IHost host, TFloat[] mean, TFloat[] stddev, bool useLog)
					: base(host)
				{
					this._mean = mean;
					this._stddev = stddev;
					this._useLog = useLog;
				}

				// Token: 0x04000AB4 RID: 2740
				protected readonly TFloat[] _mean;

				// Token: 0x04000AB5 RID: 2741
				protected readonly TFloat[] _stddev;

				// Token: 0x04000AB6 RID: 2742
				protected readonly bool _useLog;
			}

			// Token: 0x0200034C RID: 844
			private static class Dbl
			{
				// Token: 0x0200034D RID: 845
				public sealed class ImplOne : NormalizeTransform.CdfColumnFunction.ImplOne<double>
				{
					// Token: 0x0600127F RID: 4735 RVA: 0x00067568 File Offset: 0x00065768
					public ImplOne(IHost host, double mean, double stddev, bool useLog)
						: base(host, mean, stddev, useLog)
					{
					}

					// Token: 0x06001280 RID: 4736 RVA: 0x00067578 File Offset: 0x00065778
					public new static NormalizeTransform.CdfColumnFunction.Dbl.ImplOne Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.RawType == typeof(double), "The column type must be R8.");
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						bool flag;
						double[] array;
						double[] array2;
						CdfNormSerializationUtils.LoadModel(ctx, 1, out flag, out array, out array2);
						return new NormalizeTransform.CdfColumnFunction.Dbl.ImplOne(host, array[0], array2[0], flag);
					}

					// Token: 0x06001281 RID: 4737 RVA: 0x000675D8 File Offset: 0x000657D8
					private void GetResult(ref double input, ref double value)
					{
						double num = (this._useLog ? Math.Log(input) : input);
						if (!FloatUtils.IsFinite(num))
						{
							value = 0.0;
							return;
						}
						value = NormalizeTransform.CdfUtils.Cdf(num, this._mean, this._stddev);
					}

					// Token: 0x06001282 RID: 4738 RVA: 0x00067624 File Offset: 0x00065824
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						CdfNormSerializationUtils.SaveModel(ctx, this._useLog, new double[] { this._mean }, new double[] { this._stddev });
					}

					// Token: 0x06001283 RID: 4739 RVA: 0x000676A0 File Offset: 0x000658A0
					public override Delegate GetGetter(IRow input, int icol)
					{
						if (this._stddev <= 5E-324)
						{
							return new ValueGetter<double>(delegate(ref double dst)
							{
								dst = 0.0;
							});
						}
						ValueGetter<double> getSrc = input.GetGetter<double>(icol);
						return new ValueGetter<double>(delegate(ref double dst)
						{
							getSrc.Invoke(ref dst);
							this.GetResult(ref dst, ref dst);
						});
					}
				}

				// Token: 0x0200034E RID: 846
				public sealed class ImplVec : NormalizeTransform.CdfColumnFunction.ImplVec<double>
				{
					// Token: 0x06001285 RID: 4741 RVA: 0x00067706 File Offset: 0x00065906
					public ImplVec(IHost host, double[] mean, double[] stddev, bool useLog)
						: base(host, mean, stddev, useLog)
					{
					}

					// Token: 0x06001286 RID: 4742 RVA: 0x00067714 File Offset: 0x00065914
					public new static NormalizeTransform.CdfColumnFunction.Dbl.ImplVec Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.ItemType.RawType == typeof(double), "The column type must be vector of R8.");
						int num = Math.Max(1, typeSrc.VectorSize);
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						bool flag;
						double[] array;
						double[] array2;
						CdfNormSerializationUtils.LoadModel(ctx, num, out flag, out array, out array2);
						return new NormalizeTransform.CdfColumnFunction.Dbl.ImplVec(host, array, array2, flag);
					}

					// Token: 0x06001287 RID: 4743 RVA: 0x00067780 File Offset: 0x00065980
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						CdfNormSerializationUtils.SaveModel(ctx, this._useLog, this._mean, this._stddev);
					}

					// Token: 0x06001288 RID: 4744 RVA: 0x0006782C File Offset: 0x00065A2C
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<VBuffer<double>> getSrc = input.GetGetter<VBuffer<double>>(icol);
						VBufferBuilder<double> bldr = new VBufferBuilder<double>(R8Adder.Instance);
						return new ValueGetter<VBuffer<double>>(delegate(ref VBuffer<double> dst)
						{
							getSrc.Invoke(ref dst);
							Contracts.Check(this._host, dst.Length == this._mean.Length);
							NormalizeTransform.CdfColumnFunction.Dbl.ImplVec.FillValues(ref dst, bldr, this._mean, this._stddev, this._useLog);
							bldr.GetResult(ref dst);
						});
					}

					// Token: 0x06001289 RID: 4745 RVA: 0x00067874 File Offset: 0x00065A74
					private static void FillValues(ref VBuffer<double> input, VBufferBuilder<double> bldr, double[] mean, double[] stddev, bool useLog)
					{
						int num = mean.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							return;
						}
						double[] values = input.Values;
						if (count >= num)
						{
							for (int i = 0; i < num; i++)
							{
								double num2 = stddev[i];
								if (num2 > 5E-324)
								{
									double num3 = (useLog ? Math.Log(values[i]) : values[i]);
									if (FloatUtils.IsFinite(num3))
									{
										bldr.AddFeature(i, NormalizeTransform.CdfUtils.Cdf(num3, mean[i], num2));
									}
								}
							}
							return;
						}
						int[] indices = input.Indices;
						for (int j = 0; j < indices.Length; j++)
						{
							int num4 = indices[j];
							double num5 = stddev[num4];
							if (num5 > 5E-324)
							{
								double num6 = (useLog ? Math.Log(values[j]) : values[j]);
								if (FloatUtils.IsFinite(num6))
								{
									bldr.AddFeature(num4, NormalizeTransform.CdfUtils.Cdf(num6, mean[num4], num5));
								}
							}
						}
					}
				}
			}

			// Token: 0x0200034F RID: 847
			private static class Sng
			{
				// Token: 0x02000350 RID: 848
				public sealed class ImplOne : NormalizeTransform.CdfColumnFunction.ImplOne<float>
				{
					// Token: 0x0600128A RID: 4746 RVA: 0x0006795B File Offset: 0x00065B5B
					public ImplOne(IHost host, float mean, float stddev, bool useLog)
						: base(host, mean, stddev, useLog)
					{
					}

					// Token: 0x0600128B RID: 4747 RVA: 0x00067968 File Offset: 0x00065B68
					public new static NormalizeTransform.CdfColumnFunction.Sng.ImplOne Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.RawType == typeof(float), "The column type must be R4.");
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						bool flag;
						float[] array;
						float[] array2;
						CdfNormSerializationUtils.LoadModel(ctx, 1, out flag, out array, out array2);
						return new NormalizeTransform.CdfColumnFunction.Sng.ImplOne(host, array[0], array2[0], flag);
					}

					// Token: 0x0600128C RID: 4748 RVA: 0x000679C8 File Offset: 0x00065BC8
					private void GetResult(ref float input, ref float value)
					{
						float num = (this._useLog ? ((float)Math.Log((double)input)) : input);
						if (!FloatUtils.IsFinite(num))
						{
							value = 0f;
							return;
						}
						value = NormalizeTransform.CdfUtils.Cdf(num, this._mean, this._stddev);
					}

					// Token: 0x0600128D RID: 4749 RVA: 0x00067A10 File Offset: 0x00065C10
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						CdfNormSerializationUtils.SaveModel(ctx, this._useLog, new float[] { this._mean }, new float[] { this._stddev });
					}

					// Token: 0x0600128E RID: 4750 RVA: 0x00067A88 File Offset: 0x00065C88
					public override Delegate GetGetter(IRow input, int icol)
					{
						if (this._stddev <= 1E-45f)
						{
							return new ValueGetter<float>(delegate(ref float dst)
							{
								dst = 0f;
							});
						}
						ValueGetter<float> getSrc = input.GetGetter<float>(icol);
						return new ValueGetter<float>(delegate(ref float dst)
						{
							getSrc.Invoke(ref dst);
							this.GetResult(ref dst, ref dst);
						});
					}
				}

				// Token: 0x02000351 RID: 849
				public sealed class ImplVec : NormalizeTransform.CdfColumnFunction.ImplVec<float>
				{
					// Token: 0x06001290 RID: 4752 RVA: 0x00067AEA File Offset: 0x00065CEA
					public ImplVec(IHost host, float[] mean, float[] stddev, bool useLog)
						: base(host, mean, stddev, useLog)
					{
					}

					// Token: 0x06001291 RID: 4753 RVA: 0x00067AF8 File Offset: 0x00065CF8
					public new static NormalizeTransform.CdfColumnFunction.Sng.ImplVec Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.ItemType.RawType == typeof(float), "The column type must be vector of R4.");
						int num = Math.Max(1, typeSrc.VectorSize);
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						bool flag;
						float[] array;
						float[] array2;
						CdfNormSerializationUtils.LoadModel(ctx, num, out flag, out array, out array2);
						return new NormalizeTransform.CdfColumnFunction.Sng.ImplVec(host, array, array2, flag);
					}

					// Token: 0x06001292 RID: 4754 RVA: 0x00067B64 File Offset: 0x00065D64
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.CdfColumnFunction.GetVersionInfo());
						CdfNormSerializationUtils.SaveModel(ctx, this._useLog, this._mean, this._stddev);
					}

					// Token: 0x06001293 RID: 4755 RVA: 0x00067C10 File Offset: 0x00065E10
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<VBuffer<float>> getSrc = input.GetGetter<VBuffer<float>>(icol);
						VBufferBuilder<float> bldr = new VBufferBuilder<float>(R4Adder.Instance);
						return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
						{
							getSrc.Invoke(ref dst);
							Contracts.Check(this._host, dst.Length == this._mean.Length);
							NormalizeTransform.CdfColumnFunction.Sng.ImplVec.FillValues(ref dst, bldr, this._mean, this._stddev, this._useLog);
							bldr.GetResult(ref dst);
						});
					}

					// Token: 0x06001294 RID: 4756 RVA: 0x00067C58 File Offset: 0x00065E58
					private static void FillValues(ref VBuffer<float> input, VBufferBuilder<float> bldr, float[] mean, float[] stddev, bool useLog)
					{
						int num = mean.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							return;
						}
						float[] values = input.Values;
						if (count >= num)
						{
							for (int i = 0; i < num; i++)
							{
								float num2 = stddev[i];
								if (num2 > 1E-45f)
								{
									float num3 = (useLog ? ((float)Math.Log((double)values[i])) : values[i]);
									if (FloatUtils.IsFinite(num3))
									{
										bldr.AddFeature(i, NormalizeTransform.CdfUtils.Cdf(num3, mean[i], num2));
									}
								}
							}
							return;
						}
						int[] indices = input.Indices;
						for (int j = 0; j < indices.Length; j++)
						{
							int num4 = indices[j];
							float num5 = stddev[num4];
							if (num5 > 1E-45f)
							{
								float num6 = (useLog ? ((float)Math.Log((double)values[j])) : values[j]);
								if (FloatUtils.IsFinite(num6))
								{
									bldr.AddFeature(num4, NormalizeTransform.CdfUtils.Cdf(num6, mean[num4], num5));
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x02000352 RID: 850
		public abstract class BinColumnFunction : IColumnFunction, ICanSaveModel
		{
			// Token: 0x06001295 RID: 4757 RVA: 0x00067D39 File Offset: 0x00065F39
			protected BinColumnFunction(IHost host)
			{
				this._host = host;
			}

			// Token: 0x06001296 RID: 4758
			public abstract void Save(ModelSaveContext ctx);

			// Token: 0x06001297 RID: 4759
			public abstract Delegate GetGetter(IRow input, int icol);

			// Token: 0x06001298 RID: 4760 RVA: 0x00067D48 File Offset: 0x00065F48
			public static NormalizeTransform.BinColumnFunction Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
			{
				if (typeSrc.IsNumber)
				{
					if (typeSrc == NumberType.R4)
					{
						return NormalizeTransform.BinColumnFunction.Sng.ImplOne.Create(ctx, host, typeSrc);
					}
					if (typeSrc == NumberType.R8)
					{
						return NormalizeTransform.BinColumnFunction.Dbl.ImplOne.Create(ctx, host, typeSrc);
					}
				}
				if (typeSrc.IsVector && typeSrc.ItemType.IsNumber)
				{
					if (typeSrc.ItemType == NumberType.R4)
					{
						return NormalizeTransform.BinColumnFunction.Sng.ImplVec.Create(ctx, host, typeSrc);
					}
					if (typeSrc.ItemType == NumberType.R8)
					{
						return NormalizeTransform.BinColumnFunction.Dbl.ImplVec.Create(ctx, host, typeSrc);
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {0}.", new object[] { typeSrc.ToString() });
			}

			// Token: 0x06001299 RID: 4761 RVA: 0x00067DE1 File Offset: 0x00065FE1
			public static VersionInfo GetVersionInfo()
			{
				return new VersionInfo("BINNORMF", 65537U, 65537U, 65537U, "BinNormalizeFunction", null);
			}

			// Token: 0x0600129A RID: 4762 RVA: 0x00067E02 File Offset: 0x00066002
			public static IColumnFunction Create(IHost host, double[] binUpperBounds, bool fixZero)
			{
				return new NormalizeTransform.BinColumnFunction.Dbl.ImplOne(host, binUpperBounds, fixZero);
			}

			// Token: 0x0600129B RID: 4763 RVA: 0x00067E0C File Offset: 0x0006600C
			public static IColumnFunction Create(IHost host, double[][] binUpperBounds, bool fixZero)
			{
				return new NormalizeTransform.BinColumnFunction.Dbl.ImplVec(host, binUpperBounds, fixZero);
			}

			// Token: 0x0600129C RID: 4764 RVA: 0x00067E16 File Offset: 0x00066016
			public static IColumnFunction Create(IHost host, float[] binUpperBounds, bool fixZero)
			{
				return new NormalizeTransform.BinColumnFunction.Sng.ImplOne(host, binUpperBounds, fixZero);
			}

			// Token: 0x0600129D RID: 4765 RVA: 0x00067E20 File Offset: 0x00066020
			public static IColumnFunction Create(IHost host, float[][] binUpperBounds, bool fixZero)
			{
				return new NormalizeTransform.BinColumnFunction.Sng.ImplVec(host, binUpperBounds, fixZero);
			}

			// Token: 0x04000AB9 RID: 2745
			public const string LoaderSignature = "BinNormalizeFunction";

			// Token: 0x04000ABA RID: 2746
			protected readonly IHost _host;

			// Token: 0x02000353 RID: 851
			private static class Dbl
			{
				// Token: 0x02000354 RID: 852
				public sealed class ImplOne : NormalizeTransform.BinColumnFunction
				{
					// Token: 0x0600129E RID: 4766 RVA: 0x00067E2C File Offset: 0x0006602C
					public ImplOne(IHost host, double[] binUpperBounds, bool fixZero)
						: base(host)
					{
						this._binUpperBounds = binUpperBounds;
						this._den = (double)Math.Max(1, this._binUpperBounds.Length - 1);
						if (fixZero)
						{
							this._offset = (double)Utils.FindIndexSorted(this._binUpperBounds, 0.0) / this._den;
						}
					}

					// Token: 0x0600129F RID: 4767 RVA: 0x00067E9C File Offset: 0x0006609C
					public new static NormalizeTransform.BinColumnFunction.Dbl.ImplOne Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.RawType == typeof(double), "The column type must be R8.");
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						bool flag = Utils.ReadBoolByte(ctx.Reader);
						double[][] binUpperBounds = null;
						if (!ctx.TryProcessSubModel("BinNormalizer", delegate(ModelLoadContext c)
						{
							BinNormSerializationUtils.LoadModel(c, out binUpperBounds);
						}))
						{
							throw Contracts.ExceptDecode(host, "Missing BinNormalizer model");
						}
						if (binUpperBounds.Length != 1)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has 1 slot.", new object[] { binUpperBounds.Length });
						}
						return new NormalizeTransform.BinColumnFunction.Dbl.ImplOne(host, binUpperBounds[0], flag);
					}

					// Token: 0x060012A0 RID: 4768 RVA: 0x00067F84 File Offset: 0x00066184
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						Utils.WriteBoolByte(ctx.Writer, this._offset != 0.0);
						ctx.SaveSubModel("BinNormalizer", delegate(ModelSaveContext c)
						{
							BinNormSerializationUtils.SaveModel(c, new double[][] { this._binUpperBounds }, true);
						});
					}

					// Token: 0x060012A1 RID: 4769 RVA: 0x00067FFC File Offset: 0x000661FC
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<double> getSrc = input.GetGetter<double>(icol);
						return new ValueGetter<double>(delegate(ref double dst)
						{
							getSrc.Invoke(ref dst);
							this.GetResult(ref dst, ref dst);
						});
					}

					// Token: 0x060012A2 RID: 4770 RVA: 0x00068031 File Offset: 0x00066231
					private void GetResult(ref double input, ref double value)
					{
						value = NormalizeTransform.BinUtils.GetValue(ref input, this._binUpperBounds, this._den, this._offset);
					}

					// Token: 0x04000ABB RID: 2747
					private readonly double[] _binUpperBounds;

					// Token: 0x04000ABC RID: 2748
					private readonly double _den;

					// Token: 0x04000ABD RID: 2749
					private readonly double _offset;
				}

				// Token: 0x02000355 RID: 853
				public sealed class ImplVec : NormalizeTransform.BinColumnFunction
				{
					// Token: 0x060012A4 RID: 4772 RVA: 0x00068050 File Offset: 0x00066250
					public ImplVec(IHost host, double[][] binUpperBounds, bool fixZero)
						: base(host)
					{
						this._binUpperBounds = binUpperBounds;
						this._den = new double[this._binUpperBounds.Length];
						for (int i = 0; i < this._binUpperBounds.Length; i++)
						{
							this._den[i] = (double)Math.Max(1, this._binUpperBounds[i].Length - 1);
						}
						if (fixZero)
						{
							this._offset = new double[this._binUpperBounds.Length];
							bool flag = false;
							for (int j = 0; j < this._binUpperBounds.Length; j++)
							{
								this._offset[j] = (double)Utils.FindIndexSorted(this._binUpperBounds[j], 0.0) / this._den[j];
								flag |= this._offset[j] != 0.0;
							}
							if (!flag)
							{
								this._offset = null;
							}
						}
					}

					// Token: 0x060012A5 RID: 4773 RVA: 0x00068138 File Offset: 0x00066338
					public new static NormalizeTransform.BinColumnFunction.Dbl.ImplVec Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.ItemType.RawType == typeof(double), "The column type must be vector of R8.");
						int num = Math.Max(1, typeSrc.VectorSize);
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						bool flag = Utils.ReadBoolByte(ctx.Reader);
						double[][] binUpperBounds = null;
						if (!ctx.TryProcessSubModel("BinNormalizer", delegate(ModelLoadContext c)
						{
							BinNormSerializationUtils.LoadModel(c, out binUpperBounds);
						}))
						{
							throw Contracts.ExceptDecode(host, "Missing BinNormalizer model");
						}
						if (binUpperBounds.Length != num)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has {1} slots.", new object[] { binUpperBounds.Length, num });
						}
						return new NormalizeTransform.BinColumnFunction.Dbl.ImplVec(host, binUpperBounds, flag);
					}

					// Token: 0x060012A6 RID: 4774 RVA: 0x00068220 File Offset: 0x00066420
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						Utils.WriteBoolByte(ctx.Writer, this._offset != null);
						ctx.SaveSubModel("BinNormalizer", delegate(ModelSaveContext c)
						{
							BinNormSerializationUtils.SaveModel(c, this._binUpperBounds, true);
						});
					}

					// Token: 0x060012A7 RID: 4775 RVA: 0x000682C8 File Offset: 0x000664C8
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<VBuffer<double>> getSrc = input.GetGetter<VBuffer<double>>(icol);
						VBufferBuilder<double> bldr = new VBufferBuilder<double>(R8Adder.Instance);
						return new ValueGetter<VBuffer<double>>(delegate(ref VBuffer<double> dst)
						{
							getSrc.Invoke(ref dst);
							Contracts.Check(this._host, dst.Length == this._binUpperBounds.Length);
							this.GetResult(ref dst, ref dst, bldr);
						});
					}

					// Token: 0x060012A8 RID: 4776 RVA: 0x00068310 File Offset: 0x00066510
					private void GetResult(ref VBuffer<double> input, ref VBuffer<double> value, VBufferBuilder<double> bldr)
					{
						int num = this._binUpperBounds.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							bldr.GetResult(ref value);
							return;
						}
						double[] values = input.Values;
						if (count >= num)
						{
							if (this._offset != null)
							{
								for (int i = 0; i < num; i++)
								{
									bldr.AddFeature(i, NormalizeTransform.BinUtils.GetValue(ref values[i], this._binUpperBounds[i], this._den[i], this._offset[i]));
								}
							}
							else
							{
								for (int j = 0; j < num; j++)
								{
									bldr.AddFeature(j, NormalizeTransform.BinUtils.GetValue(ref values[j], this._binUpperBounds[j], this._den[j]));
								}
							}
							bldr.GetResult(ref value);
							return;
						}
						if (this._offset != null)
						{
							int[] indices = input.Indices;
							int num2 = 0;
							int num3 = indices[num2];
							double num4 = 0.0;
							for (int k = 0; k < num; k++)
							{
								if (k == num3)
								{
									bldr.AddFeature(k, NormalizeTransform.BinUtils.GetValue(ref values[num2], this._binUpperBounds[k], this._den[k], this._offset[k]));
									num3 = ((++num2 < count) ? indices[num2] : num);
								}
								else
								{
									bldr.AddFeature(k, NormalizeTransform.BinUtils.GetValue(ref num4, this._binUpperBounds[k], this._den[k], this._offset[k]));
								}
							}
						}
						else
						{
							int[] indices2 = input.Indices;
							for (int l = 0; l < count; l++)
							{
								int num5 = indices2[l];
								bldr.AddFeature(num5, NormalizeTransform.BinUtils.GetValue(ref values[l], this._binUpperBounds[num5], this._den[num5]));
							}
						}
						bldr.GetResult(ref value);
					}

					// Token: 0x04000ABE RID: 2750
					private readonly double[][] _binUpperBounds;

					// Token: 0x04000ABF RID: 2751
					private readonly double[] _den;

					// Token: 0x04000AC0 RID: 2752
					private readonly double[] _offset;
				}
			}

			// Token: 0x02000356 RID: 854
			private static class Sng
			{
				// Token: 0x02000357 RID: 855
				public sealed class ImplOne : NormalizeTransform.BinColumnFunction
				{
					// Token: 0x060012AA RID: 4778 RVA: 0x000684D0 File Offset: 0x000666D0
					public ImplOne(IHost host, float[] binUpperBounds, bool fixZero)
						: base(host)
					{
						this._binUpperBounds = binUpperBounds;
						this._den = (float)Math.Max(1, this._binUpperBounds.Length - 1);
						if (fixZero)
						{
							this._offset = (float)Utils.FindIndexSorted(this._binUpperBounds, 0f) / this._den;
						}
					}

					// Token: 0x060012AB RID: 4779 RVA: 0x0006853C File Offset: 0x0006673C
					public new static NormalizeTransform.BinColumnFunction.Sng.ImplOne Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.RawType == typeof(float), "The column type must be R4.");
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						bool flag = Utils.ReadBoolByte(ctx.Reader);
						float[][] binUpperBounds = null;
						if (!ctx.TryProcessSubModel("BinNormalizer", delegate(ModelLoadContext c)
						{
							BinNormSerializationUtils.LoadModel(c, out binUpperBounds);
						}))
						{
							throw Contracts.ExceptDecode(host, "Missing BinNormalizer model");
						}
						if (binUpperBounds.Length != 1)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has 1 slot.", new object[] { binUpperBounds.Length });
						}
						return new NormalizeTransform.BinColumnFunction.Sng.ImplOne(host, binUpperBounds[0], flag);
					}

					// Token: 0x060012AC RID: 4780 RVA: 0x00068624 File Offset: 0x00066824
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						Utils.WriteBoolByte(ctx.Writer, this._offset != 0f);
						ctx.SaveSubModel("BinNormalizer", delegate(ModelSaveContext c)
						{
							BinNormSerializationUtils.SaveModel(c, new float[][] { this._binUpperBounds }, true);
						});
					}

					// Token: 0x060012AD RID: 4781 RVA: 0x00068698 File Offset: 0x00066898
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<float> getSrc = input.GetGetter<float>(icol);
						return new ValueGetter<float>(delegate(ref float dst)
						{
							getSrc.Invoke(ref dst);
							this.GetResult(ref dst, ref dst);
						});
					}

					// Token: 0x060012AE RID: 4782 RVA: 0x000686CD File Offset: 0x000668CD
					private void GetResult(ref float input, ref float value)
					{
						value = NormalizeTransform.BinUtils.GetValue(ref input, this._binUpperBounds, this._den, this._offset);
					}

					// Token: 0x04000AC1 RID: 2753
					private readonly float[] _binUpperBounds;

					// Token: 0x04000AC2 RID: 2754
					private readonly float _den;

					// Token: 0x04000AC3 RID: 2755
					private readonly float _offset;
				}

				// Token: 0x02000358 RID: 856
				public sealed class ImplVec : NormalizeTransform.BinColumnFunction
				{
					// Token: 0x060012B0 RID: 4784 RVA: 0x000686EC File Offset: 0x000668EC
					public ImplVec(IHost host, float[][] binUpperBounds, bool fixZero)
						: base(host)
					{
						this._binUpperBounds = binUpperBounds;
						this._den = new float[this._binUpperBounds.Length];
						for (int i = 0; i < this._binUpperBounds.Length; i++)
						{
							this._den[i] = (float)Math.Max(1, this._binUpperBounds[i].Length - 1);
						}
						if (fixZero)
						{
							this._offset = new float[this._binUpperBounds.Length];
							bool flag = false;
							for (int j = 0; j < this._binUpperBounds.Length; j++)
							{
								this._offset[j] = (float)Utils.FindIndexSorted(this._binUpperBounds[j], 0f) / this._den[j];
								flag |= this._offset[j] != 0f;
							}
							if (!flag)
							{
								this._offset = null;
							}
						}
					}

					// Token: 0x060012B1 RID: 4785 RVA: 0x000687CC File Offset: 0x000669CC
					public new static NormalizeTransform.BinColumnFunction.Sng.ImplVec Create(ModelLoadContext ctx, IHost host, ColumnType typeSrc)
					{
						Contracts.Check(host, typeSrc.ItemType.RawType == typeof(float), "The column type must be vector of R4.");
						int num = Math.Max(1, typeSrc.VectorSize);
						Contracts.CheckValue<ModelLoadContext>(host, ctx, "ctx");
						ctx.CheckAtModel(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						bool flag = Utils.ReadBoolByte(ctx.Reader);
						float[][] binUpperBounds = null;
						if (!ctx.TryProcessSubModel("BinNormalizer", delegate(ModelLoadContext c)
						{
							BinNormSerializationUtils.LoadModel(c, out binUpperBounds);
						}))
						{
							throw Contracts.ExceptDecode(host, "Missing BinNormalizer model");
						}
						if (binUpperBounds.Length != num)
						{
							throw Contracts.Except(host, "Normalizer expected {0} slots, but the input data column has {1} slots.", new object[] { binUpperBounds.Length, num });
						}
						return new NormalizeTransform.BinColumnFunction.Sng.ImplVec(host, binUpperBounds, flag);
					}

					// Token: 0x060012B2 RID: 4786 RVA: 0x000688B4 File Offset: 0x00066AB4
					public override void Save(ModelSaveContext ctx)
					{
						ctx.CheckAtModel();
						ctx.SetVersionInfo(NormalizeTransform.BinColumnFunction.GetVersionInfo());
						Utils.WriteBoolByte(ctx.Writer, this._offset != null);
						ctx.SaveSubModel("BinNormalizer", delegate(ModelSaveContext c)
						{
							BinNormSerializationUtils.SaveModel(c, this._binUpperBounds, true);
						});
					}

					// Token: 0x060012B3 RID: 4787 RVA: 0x0006895C File Offset: 0x00066B5C
					public override Delegate GetGetter(IRow input, int icol)
					{
						ValueGetter<VBuffer<float>> getSrc = input.GetGetter<VBuffer<float>>(icol);
						VBufferBuilder<float> bldr = new VBufferBuilder<float>(R4Adder.Instance);
						return new ValueGetter<VBuffer<float>>(delegate(ref VBuffer<float> dst)
						{
							getSrc.Invoke(ref dst);
							Contracts.Check(this._host, dst.Length == this._binUpperBounds.Length);
							this.GetResult(ref dst, ref dst, bldr);
						});
					}

					// Token: 0x060012B4 RID: 4788 RVA: 0x000689A4 File Offset: 0x00066BA4
					private void GetResult(ref VBuffer<float> input, ref VBuffer<float> value, VBufferBuilder<float> bldr)
					{
						int num = this._binUpperBounds.Length;
						int count = input.Count;
						bldr.Reset(num, false);
						if (count == 0)
						{
							bldr.GetResult(ref value);
							return;
						}
						float[] values = input.Values;
						if (count >= num)
						{
							if (this._offset != null)
							{
								for (int i = 0; i < num; i++)
								{
									bldr.AddFeature(i, NormalizeTransform.BinUtils.GetValue(ref values[i], this._binUpperBounds[i], this._den[i], this._offset[i]));
								}
							}
							else
							{
								for (int j = 0; j < num; j++)
								{
									bldr.AddFeature(j, NormalizeTransform.BinUtils.GetValue(ref values[j], this._binUpperBounds[j], this._den[j]));
								}
							}
							bldr.GetResult(ref value);
							return;
						}
						if (this._offset != null)
						{
							int[] indices = input.Indices;
							int num2 = 0;
							int num3 = indices[num2];
							float num4 = 0f;
							for (int k = 0; k < num; k++)
							{
								if (k == num3)
								{
									bldr.AddFeature(k, NormalizeTransform.BinUtils.GetValue(ref values[num2], this._binUpperBounds[k], this._den[k], this._offset[k]));
									num3 = ((++num2 < count) ? indices[num2] : num);
								}
								else
								{
									bldr.AddFeature(k, NormalizeTransform.BinUtils.GetValue(ref num4, this._binUpperBounds[k], this._den[k], this._offset[k]));
								}
							}
						}
						else
						{
							int[] indices2 = input.Indices;
							for (int l = 0; l < count; l++)
							{
								int num5 = indices2[l];
								bldr.AddFeature(num5, NormalizeTransform.BinUtils.GetValue(ref values[l], this._binUpperBounds[num5], this._den[num5]));
							}
						}
						bldr.GetResult(ref value);
					}

					// Token: 0x04000AC4 RID: 2756
					private readonly float[][] _binUpperBounds;

					// Token: 0x04000AC5 RID: 2757
					private readonly float[] _den;

					// Token: 0x04000AC6 RID: 2758
					private readonly float[] _offset;
				}
			}
		}

		// Token: 0x0200035A RID: 858
		private abstract class OneColumnFunctionBuilderBase<TFloat> : IColumnFunctionBuilder
		{
			// Token: 0x060012B8 RID: 4792 RVA: 0x00068B5E File Offset: 0x00066D5E
			protected OneColumnFunctionBuilderBase(IHost host, long lim, ValueGetter<TFloat> getSrc)
			{
				this._host = host;
				this._rem = lim;
				this._lim = lim;
				this._getSrc = getSrc;
			}

			// Token: 0x060012B9 RID: 4793 RVA: 0x00068B84 File Offset: 0x00066D84
			public bool ProcessValue()
			{
				TFloat tfloat = default(TFloat);
				this._getSrc.Invoke(ref tfloat);
				return this.ProcessValue(ref tfloat);
			}

			// Token: 0x060012BA RID: 4794 RVA: 0x00068BAE File Offset: 0x00066DAE
			protected virtual bool ProcessValue(ref TFloat val)
			{
				if (this._rem == 0L)
				{
					return false;
				}
				this._rem -= 1L;
				return true;
			}

			// Token: 0x060012BB RID: 4795
			public abstract IColumnFunction CreateColumnFunction();

			// Token: 0x04000AC7 RID: 2759
			protected IHost _host;

			// Token: 0x04000AC8 RID: 2760
			protected readonly long _lim;

			// Token: 0x04000AC9 RID: 2761
			protected long _rem;

			// Token: 0x04000ACA RID: 2762
			private readonly ValueGetter<TFloat> _getSrc;
		}

		// Token: 0x0200035B RID: 859
		private abstract class VecColumnFunctionBuilderBase<TFloat> : IColumnFunctionBuilder
		{
			// Token: 0x060012BC RID: 4796 RVA: 0x00068BCC File Offset: 0x00066DCC
			protected VecColumnFunctionBuilderBase(IHost host, long lim, ValueGetter<VBuffer<TFloat>> getSrc)
			{
				this._host = host;
				this._rem = lim;
				this._lim = lim;
				this._getSrc = getSrc;
			}

			// Token: 0x060012BD RID: 4797 RVA: 0x00068BF0 File Offset: 0x00066DF0
			public bool ProcessValue()
			{
				this._getSrc.Invoke(ref this._buffer);
				return this.ProcessValue(ref this._buffer);
			}

			// Token: 0x060012BE RID: 4798 RVA: 0x00068C0F File Offset: 0x00066E0F
			protected virtual bool ProcessValue(ref VBuffer<TFloat> buffer)
			{
				if (this._rem == 0L)
				{
					return false;
				}
				this._rem -= 1L;
				return true;
			}

			// Token: 0x060012BF RID: 4799
			public abstract IColumnFunction CreateColumnFunction();

			// Token: 0x04000ACB RID: 2763
			protected IHost _host;

			// Token: 0x04000ACC RID: 2764
			protected readonly long _lim;

			// Token: 0x04000ACD RID: 2765
			protected long _rem;

			// Token: 0x04000ACE RID: 2766
			private readonly ValueGetter<VBuffer<TFloat>> _getSrc;

			// Token: 0x04000ACF RID: 2767
			private VBuffer<TFloat> _buffer;
		}

		// Token: 0x0200035C RID: 860
		private abstract class SupervisedBinFunctionBuilderBase : IColumnFunctionBuilder
		{
			// Token: 0x060012C0 RID: 4800 RVA: 0x00068C2D File Offset: 0x00066E2D
			protected SupervisedBinFunctionBuilderBase(IHost host, long lim, int labelColId, IRow dataRow)
			{
				this._host = host;
				this._rem = lim;
				this._lim = lim;
				this._labels = new List<int>();
				this._labelGetterSrc = this.GetLabelGetter(dataRow, labelColId, out this._labelCardinality);
			}

			// Token: 0x060012C1 RID: 4801 RVA: 0x00068CEC File Offset: 0x00066EEC
			private ValueGetter<int> GetLabelGetter(IRow row, int col, out int labelCardinality)
			{
				ColumnType columnType = row.Schema.GetColumnType(col);
				if (columnType.IsKey)
				{
					labelCardinality = columnType.KeyCount;
					int size = columnType.KeyCount;
					ulong src2 = 0UL;
					ValueGetter<ulong> getSrc2 = RowCursorUtils.GetGetterAs<ulong>(NumberType.U8, row, col);
					return delegate(ref int dst)
					{
						getSrc2.Invoke(ref src2);
						if (src2 <= (ulong)((long)size))
						{
							dst = (int)src2 - 1;
							return;
						}
						dst = -1;
					};
				}
				labelCardinality = 2;
				double src = 0.0;
				ValueGetter<double> getSrc = RowCursorUtils.GetGetterAs<double>(NumberType.R8, row, col);
				return delegate(ref int dst)
				{
					getSrc.Invoke(ref src);
					if (src > 0.0)
					{
						dst = 1;
						return;
					}
					if (src <= 0.0)
					{
						dst = 0;
						return;
					}
					dst = -1;
				};
			}

			// Token: 0x060012C2 RID: 4802 RVA: 0x00068D88 File Offset: 0x00066F88
			public virtual bool ProcessValue()
			{
				if (this._rem == 0L)
				{
					return false;
				}
				this._rem -= 1L;
				int num = 0;
				this._labelGetterSrc.Invoke(ref num);
				bool flag = num >= 0 && this.AcceptColumnValue();
				if (flag)
				{
					this._labels.Add(num);
				}
				return true;
			}

			// Token: 0x060012C3 RID: 4803
			public abstract IColumnFunction CreateColumnFunction();

			// Token: 0x060012C4 RID: 4804
			protected abstract bool AcceptColumnValue();

			// Token: 0x04000AD0 RID: 2768
			protected readonly IHost _host;

			// Token: 0x04000AD1 RID: 2769
			protected readonly long _lim;

			// Token: 0x04000AD2 RID: 2770
			protected long _rem;

			// Token: 0x04000AD3 RID: 2771
			protected readonly List<int> _labels;

			// Token: 0x04000AD4 RID: 2772
			protected readonly int _labelCardinality;

			// Token: 0x04000AD5 RID: 2773
			private readonly ValueGetter<int> _labelGetterSrc;
		}

		// Token: 0x0200035D RID: 861
		private abstract class OneColumnSupervisedBinFunctionBuilderBase<TFloat> : NormalizeTransform.SupervisedBinFunctionBuilderBase
		{
			// Token: 0x060012C5 RID: 4805 RVA: 0x00068DDD File Offset: 0x00066FDD
			protected OneColumnSupervisedBinFunctionBuilderBase(IHost host, long lim, int valueColId, int labelColId, IRow dataRow)
				: base(host, lim, labelColId, dataRow)
			{
				this._colGetterSrc = dataRow.GetGetter<TFloat>(valueColId);
				this._colValues = new List<TFloat>();
			}

			// Token: 0x060012C6 RID: 4806 RVA: 0x00068E04 File Offset: 0x00067004
			protected override bool AcceptColumnValue()
			{
				TFloat tfloat = default(TFloat);
				this._colGetterSrc.Invoke(ref tfloat);
				bool flag = this.AcceptColumnValue(ref tfloat);
				if (flag)
				{
					this._colValues.Add(tfloat);
				}
				return flag;
			}

			// Token: 0x060012C7 RID: 4807
			protected abstract bool AcceptColumnValue(ref TFloat colValue);

			// Token: 0x04000AD6 RID: 2774
			private readonly ValueGetter<TFloat> _colGetterSrc;

			// Token: 0x04000AD7 RID: 2775
			protected readonly List<TFloat> _colValues;
		}

		// Token: 0x0200035E RID: 862
		private abstract class VecColumnSupervisedBinFunctionBuilderBase<TFloat> : NormalizeTransform.SupervisedBinFunctionBuilderBase
		{
			// Token: 0x060012C8 RID: 4808 RVA: 0x00068E40 File Offset: 0x00067040
			protected VecColumnSupervisedBinFunctionBuilderBase(IHost host, long lim, int valueColId, int labelColId, IRow dataRow)
				: base(host, lim, labelColId, dataRow)
			{
				this._colValueGetter = dataRow.GetGetter<VBuffer<TFloat>>(valueColId);
				ColumnType columnType = dataRow.Schema.GetColumnType(valueColId);
				this._columnSlotCount = columnType.ValueCount;
				this._colValues = new List<TFloat>[this._columnSlotCount];
				for (int i = 0; i < this._columnSlotCount; i++)
				{
					this._colValues[i] = new List<TFloat>();
				}
				this._buffer = default(VBuffer<TFloat>);
			}

			// Token: 0x060012C9 RID: 4809 RVA: 0x00068EBC File Offset: 0x000670BC
			protected override bool AcceptColumnValue()
			{
				this._colValueGetter.Invoke(ref this._buffer);
				bool flag = this.AcceptColumnValue(ref this._buffer);
				if (flag)
				{
					if (this._buffer.IsDense)
					{
						TFloat[] values = this._buffer.Values;
						for (int i = 0; i < this._columnSlotCount; i++)
						{
							this._colValues[i].Add(values[i]);
						}
					}
					else
					{
						int[] indices = this._buffer.Indices;
						TFloat[] values2 = this._buffer.Values;
						int j = 0;
						for (int k = 0; k < this._buffer.Count; k++)
						{
							TFloat tfloat = values2[k];
							int num = indices[k];
							while (j < num)
							{
								this._colValues[j++].Add(default(TFloat));
							}
							this._colValues[j++].Add(tfloat);
						}
						while (j < this._columnSlotCount)
						{
							this._colValues[j++].Add(default(TFloat));
						}
					}
				}
				return flag;
			}

			// Token: 0x060012CA RID: 4810
			protected abstract bool AcceptColumnValue(ref VBuffer<TFloat> buffer);

			// Token: 0x04000AD8 RID: 2776
			private readonly ValueGetter<VBuffer<TFloat>> _colValueGetter;

			// Token: 0x04000AD9 RID: 2777
			private VBuffer<TFloat> _buffer;

			// Token: 0x04000ADA RID: 2778
			protected readonly List<TFloat>[] _colValues;

			// Token: 0x04000ADB RID: 2779
			protected readonly int _columnSlotCount;
		}

		// Token: 0x0200035F RID: 863
		private static class MinMaxUtils
		{
			// Token: 0x060012CB RID: 4811 RVA: 0x00068FDC File Offset: 0x000671DC
			public static IColumnFunctionBuilder CreateBuilder(NormalizeTransform.MinMaxArguments args, IHost host, int icol, int srcIndex, ColumnType srcType, IRowCursor cursor)
			{
				if (srcType.IsNumber)
				{
					if (srcType == NumberType.R4)
					{
						return NormalizeTransform.Sng.MinMaxOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<float>(srcIndex));
					}
					if (srcType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.MinMaxOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<double>(srcIndex));
					}
				}
				if (srcType.IsVector && srcType.ItemType.IsNumber)
				{
					if (srcType.ItemType == NumberType.R4)
					{
						return NormalizeTransform.Sng.MinMaxVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<float>>(srcIndex));
					}
					if (srcType.ItemType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.MinMaxVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<double>>(srcIndex));
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type for column {0}. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {1}.", new object[]
				{
					args.column[icol].source,
					srcType.ToString()
				});
			}

			// Token: 0x060012CC RID: 4812 RVA: 0x000690B5 File Offset: 0x000672B5
			public static void ComputeScaleAndOffset(bool fixZero, double max, double min, out double scale, out double offset)
			{
				if (fixZero)
				{
					NormalizeTransform.MinMaxUtils.ComputeScaleAndOffsetFixZero(max, min, out scale, out offset);
					return;
				}
				NormalizeTransform.MinMaxUtils.ComputeScaleAndOffset(max, min, out scale, out offset);
			}

			// Token: 0x060012CD RID: 4813 RVA: 0x000690D0 File Offset: 0x000672D0
			private static void ComputeScaleAndOffset(double max, double min, out double scale, out double offset)
			{
				if (max <= min)
				{
					scale = (offset = 0.0);
					return;
				}
				if ((scale = 1.0 / (max - min)) == 0.0)
				{
					offset = 0.0;
					return;
				}
				offset = min;
			}

			// Token: 0x060012CE RID: 4814 RVA: 0x0006911E File Offset: 0x0006731E
			private static void ComputeScaleAndOffsetFixZero(double max, double min, out double scale, out double offset)
			{
				offset = 0.0;
				if (max <= min)
				{
					scale = 0.0;
					return;
				}
				scale = 1.0 / Math.Max(Math.Abs(max), Math.Abs(min));
			}

			// Token: 0x060012CF RID: 4815 RVA: 0x00069158 File Offset: 0x00067358
			public static void ComputeScaleAndOffset(bool fixZero, float max, float min, out float scale, out float offset)
			{
				if (fixZero)
				{
					NormalizeTransform.MinMaxUtils.ComputeScaleAndOffsetFixZero(max, min, out scale, out offset);
					return;
				}
				NormalizeTransform.MinMaxUtils.ComputeScaleAndOffset(max, min, out scale, out offset);
			}

			// Token: 0x060012D0 RID: 4816 RVA: 0x00069174 File Offset: 0x00067374
			private static void ComputeScaleAndOffset(float max, float min, out float scale, out float offset)
			{
				if (max <= min)
				{
					scale = (offset = 0f);
					return;
				}
				if ((scale = 1f / (max - min)) == 0f)
				{
					offset = 0f;
					return;
				}
				offset = min;
			}

			// Token: 0x060012D1 RID: 4817 RVA: 0x000691B2 File Offset: 0x000673B2
			private static void ComputeScaleAndOffsetFixZero(float max, float min, out float scale, out float offset)
			{
				offset = 0f;
				if (max <= min)
				{
					scale = 0f;
					return;
				}
				scale = 1f / Math.Max(Math.Abs(max), Math.Abs(min));
			}
		}

		// Token: 0x02000360 RID: 864
		private static class MeanVarUtils
		{
			// Token: 0x060012D2 RID: 4818 RVA: 0x000691E0 File Offset: 0x000673E0
			public static IColumnFunctionBuilder CreateBuilder(NormalizeTransform.MeanVarArguments args, IHost host, int icol, int srcIndex, ColumnType srcType, IRowCursor cursor)
			{
				if (srcType.IsNumber)
				{
					if (srcType == NumberType.R4)
					{
						return NormalizeTransform.Sng.MeanVarOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<float>(srcIndex));
					}
					if (srcType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.MeanVarOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<double>(srcIndex));
					}
				}
				if (srcType.IsVector && srcType.ItemType.IsNumber)
				{
					if (srcType.ItemType == NumberType.R4)
					{
						return NormalizeTransform.Sng.MeanVarVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<float>>(srcIndex));
					}
					if (srcType.ItemType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.MeanVarVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<double>>(srcIndex));
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type for column {0}. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {1}.", new object[]
				{
					args.column[icol].source,
					srcType.ToString()
				});
			}

			// Token: 0x060012D3 RID: 4819 RVA: 0x000692BC File Offset: 0x000674BC
			public static void ComputeScaleAndOffset(double mean, double stddev, out double scale, out double offset)
			{
				if (stddev == 0.0)
				{
					scale = (offset = 0.0);
					return;
				}
				if ((scale = 1.0 / stddev) == 0.0)
				{
					offset = 0.0;
					return;
				}
				offset = mean;
			}

			// Token: 0x060012D4 RID: 4820 RVA: 0x00069312 File Offset: 0x00067512
			public static void ComputeScaleAndOffsetFixZero(double mean, double meanSquaredError, out double scale, out double offset)
			{
				offset = 0.0;
				if (meanSquaredError == 0.0)
				{
					scale = 0.0;
					return;
				}
				scale = 1.0 / Math.Sqrt(meanSquaredError + mean * mean);
			}

			// Token: 0x060012D5 RID: 4821 RVA: 0x00069350 File Offset: 0x00067550
			public static void ComputeScaleAndOffset(double mean, double stddev, out float scale, out float offset)
			{
				if (stddev == 0.0)
				{
					scale = (offset = 0f);
					return;
				}
				if ((scale = 1f / (float)stddev) == 0f)
				{
					offset = 0f;
					return;
				}
				offset = (float)mean;
			}

			// Token: 0x060012D6 RID: 4822 RVA: 0x00069396 File Offset: 0x00067596
			public static void ComputeScaleAndOffsetFixZero(double mean, double meanSquaredError, out float scale, out float offset)
			{
				offset = 0f;
				if (meanSquaredError == 0.0)
				{
					scale = 0f;
					return;
				}
				scale = 1f / (float)Math.Sqrt(meanSquaredError + mean * mean);
			}
		}

		// Token: 0x02000361 RID: 865
		private static class LogMeanVarUtils
		{
			// Token: 0x060012D7 RID: 4823 RVA: 0x000693C8 File Offset: 0x000675C8
			public static IColumnFunctionBuilder CreateBuilder(NormalizeTransform.LogMeanVarArguments args, IHost host, int icol, int srcIndex, ColumnType srcType, IRowCursor cursor)
			{
				if (srcType.IsNumber)
				{
					if (srcType == NumberType.R4)
					{
						return NormalizeTransform.Sng.MeanVarOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<float>(srcIndex));
					}
					if (srcType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.MeanVarOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<double>(srcIndex));
					}
				}
				if (srcType.IsVector && srcType.ItemType.IsNumber)
				{
					if (srcType.ItemType == NumberType.R4)
					{
						return NormalizeTransform.Sng.MeanVarVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<float>>(srcIndex));
					}
					if (srcType.ItemType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.MeanVarVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<double>>(srcIndex));
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type for column {0}. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {1}.", new object[]
				{
					args.column[icol].source,
					srcType.ToString()
				});
			}
		}

		// Token: 0x02000362 RID: 866
		private static class BinUtils
		{
			// Token: 0x060012D8 RID: 4824 RVA: 0x000694A4 File Offset: 0x000676A4
			public static IColumnFunctionBuilder CreateBuilder(NormalizeTransform.BinArguments args, IHost host, int icol, int srcIndex, ColumnType srcType, IRowCursor cursor)
			{
				if (srcType.IsNumber)
				{
					if (srcType == NumberType.R4)
					{
						return NormalizeTransform.Sng.BinOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<float>(srcIndex));
					}
					if (srcType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.BinOneColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<double>(srcIndex));
					}
				}
				if (srcType.IsVector && srcType.ItemType.IsNumber)
				{
					if (srcType.ItemType == NumberType.R4)
					{
						return NormalizeTransform.Sng.BinVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<float>>(srcIndex));
					}
					if (srcType.ItemType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.BinVecColumnFunctionBuilder.Create(args, host, icol, srcType, cursor.GetGetter<VBuffer<double>>(srcIndex));
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type for column {0}. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {1}.", new object[]
				{
					args.column[icol].source,
					srcType.ToString()
				});
			}

			// Token: 0x060012D9 RID: 4825 RVA: 0x00069580 File Offset: 0x00067780
			public static double GetValue(ref double input, double[] binUpperBounds, double den, double offset)
			{
				if (double.IsNaN(input))
				{
					return input;
				}
				int num = Utils.FindIndexSorted(binUpperBounds, 0, binUpperBounds.Length - 1, input);
				Contracts.Check(num < binUpperBounds.Length);
				return (double)num / den - offset;
			}

			// Token: 0x060012DA RID: 4826 RVA: 0x000695BC File Offset: 0x000677BC
			public static double GetValue(ref double input, double[] binUpperBounds, double den)
			{
				if (double.IsNaN(input))
				{
					return input;
				}
				int num = Utils.FindIndexSorted(binUpperBounds, 0, binUpperBounds.Length - 1, input);
				Contracts.Check(num < binUpperBounds.Length);
				return (double)num / den;
			}

			// Token: 0x060012DB RID: 4827 RVA: 0x000695F8 File Offset: 0x000677F8
			public static float GetValue(ref float input, float[] binUpperBounds, float den, float offset)
			{
				if (float.IsNaN(input))
				{
					return input;
				}
				int num = Utils.FindIndexSorted(binUpperBounds, 0, binUpperBounds.Length - 1, input);
				Contracts.Check(num < binUpperBounds.Length);
				return (float)num / den - offset;
			}

			// Token: 0x060012DC RID: 4828 RVA: 0x00069634 File Offset: 0x00067834
			public static float GetValue(ref float input, float[] binUpperBounds, float den)
			{
				if (float.IsNaN(input))
				{
					return input;
				}
				int num = Utils.FindIndexSorted(binUpperBounds, 0, binUpperBounds.Length - 1, input);
				Contracts.Check(num < binUpperBounds.Length);
				return (float)num / den;
			}
		}

		// Token: 0x02000363 RID: 867
		private static class SupervisedBinUtils
		{
			// Token: 0x060012DD RID: 4829 RVA: 0x00069670 File Offset: 0x00067870
			public static IColumnFunctionBuilder CreateBuilder(NormalizeTransform.SupervisedBinArguments args, IHost host, int icol, int srcIndex, ColumnType srcType, IRowCursor cursor)
			{
				Contracts.CheckUserArg(host, !string.IsNullOrWhiteSpace(args.labelColumn), "labelColumn", "Must specify the label column name");
				int labelColumnId = NormalizeTransform.SupervisedBinUtils.GetLabelColumnId(host, cursor.Schema, args.labelColumn);
				ColumnType columnType = cursor.Schema.GetColumnType(labelColumnId);
				if (columnType.IsKey)
				{
					Contracts.CheckUserArg(host, columnType.KeyCount > 0, "labelColumn", "Label column must have a known cardinality");
				}
				else
				{
					Contracts.CheckUserArg(host, columnType.IsNumber, "labelColumn", "Label column must be a number or a key type");
				}
				if (srcType.IsNumber)
				{
					if (srcType == NumberType.R4)
					{
						return NormalizeTransform.Sng.SupervisedBinOneColumnFunctionBuilder.Create(args, host, icol, srcIndex, labelColumnId, cursor);
					}
					if (srcType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.SupervisedBinOneColumnFunctionBuilder.Create(args, host, icol, srcIndex, labelColumnId, cursor);
					}
				}
				if (srcType.IsVector && srcType.ItemType.IsNumber)
				{
					if (srcType.ItemType == NumberType.R4)
					{
						return NormalizeTransform.Sng.SupervisedBinVecColumnFunctionBuilder.Create(args, host, icol, srcIndex, labelColumnId, cursor);
					}
					if (srcType.ItemType == NumberType.R8)
					{
						return NormalizeTransform.Dbl.SupervisedBinVecColumnFunctionBuilder.Create(args, host, icol, srcIndex, labelColumnId, cursor);
					}
				}
				throw Contracts.ExceptUserArg(host, "column", "Wrong column type for column {0}. Expected: R4, R8, Vec<R4, n> or Vec<R8, n>. Got: {1}.", new object[]
				{
					args.column[icol].source,
					srcType.ToString()
				});
			}

			// Token: 0x060012DE RID: 4830 RVA: 0x000697AC File Offset: 0x000679AC
			public static int GetLabelColumnId(IExceptionContext host, ISchema schema, string labelColumnName)
			{
				int num;
				if (!schema.TryGetColumnIndex(labelColumnName, ref num))
				{
					throw Contracts.ExceptUserArg(host, "labelColumn", "Label column '{0}' not found", new object[] { labelColumnName });
				}
				return num;
			}
		}

		// Token: 0x02000364 RID: 868
		private static class CdfUtils
		{
			// Token: 0x060012DF RID: 4831 RVA: 0x000697E4 File Offset: 0x000679E4
			public static double Cdf(double input, double mean, double stddev)
			{
				double num = (input - mean) / stddev;
				double num2 = num * num / 2.0;
				double num3 = 0.147 * num2;
				return 0.5 + 0.5 * (double)Math.Sign(num) * Math.Sqrt(1.0 - Math.Exp(-num2 * (1.2732395447351628 + num3) / (1.0 + num3)));
			}

			// Token: 0x060012E0 RID: 4832 RVA: 0x0006985C File Offset: 0x00067A5C
			public static float Cdf(float input, float mean, float stddev)
			{
				float num = (input - mean) / stddev;
				float num2 = num * num / 2f;
				float num3 = 0.147f * num2;
				return (float)(0.5 + 0.5 * (double)Math.Sign(num) * Math.Sqrt(1.0 - Math.Exp((double)(-(double)num2) * (1.2732395447351628 + (double)num3) / (double)(1f + num3))));
			}
		}

		// Token: 0x02000365 RID: 869
		private static class Dbl
		{
			// Token: 0x02000366 RID: 870
			public abstract class MinMaxOneColumnFunctionBuilderBase : NormalizeTransform.OneColumnFunctionBuilderBase<double>
			{
				// Token: 0x060012E1 RID: 4833 RVA: 0x000698CB File Offset: 0x00067ACB
				protected MinMaxOneColumnFunctionBuilderBase(IHost host, long lim, bool fix, ValueGetter<double> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._aggregator = new MinMaxDblAggregator(1);
					this._buffer = new VBuffer<double>(1, new double[1], null);
				}

				// Token: 0x060012E2 RID: 4834 RVA: 0x000698FD File Offset: 0x00067AFD
				protected override bool ProcessValue(ref double val)
				{
					if (!base.ProcessValue(ref val))
					{
						return false;
					}
					this._buffer.Values[0] = val;
					this._aggregator.ProcessValue(ref this._buffer);
					return true;
				}

				// Token: 0x04000ADC RID: 2780
				protected readonly bool _fix;

				// Token: 0x04000ADD RID: 2781
				protected readonly MinMaxDblAggregator _aggregator;

				// Token: 0x04000ADE RID: 2782
				private VBuffer<double> _buffer;
			}

			// Token: 0x02000367 RID: 871
			public sealed class MinMaxOneColumnFunctionBuilder : NormalizeTransform.Dbl.MinMaxOneColumnFunctionBuilderBase
			{
				// Token: 0x060012E3 RID: 4835 RVA: 0x0006992B File Offset: 0x00067B2B
				private MinMaxOneColumnFunctionBuilder(IHost host, long lim, bool fix, ValueGetter<double> getSrc)
					: base(host, lim, fix, getSrc)
				{
				}

				// Token: 0x060012E4 RID: 4836 RVA: 0x00069938 File Offset: 0x00067B38
				public static IColumnFunctionBuilder Create(NormalizeTransform.MinMaxArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<double> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					return new NormalizeTransform.Dbl.MinMaxOneColumnFunctionBuilder(host, num, flag, getter);
				}

				// Token: 0x060012E5 RID: 4837 RVA: 0x000699B4 File Offset: 0x00067BB4
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					double num;
					double num2;
					NormalizeTransform.MinMaxUtils.ComputeScaleAndOffset(this._fix, this._aggregator.Max[0], this._aggregator.Min[0], out num, out num2);
					return NormalizeTransform.AffineColumnFunction.Create(this._host, num, num2);
				}
			}

			// Token: 0x02000368 RID: 872
			public abstract class MinMaxVecColumnFunctionBuilderBase : NormalizeTransform.VecColumnFunctionBuilderBase<double>
			{
				// Token: 0x060012E6 RID: 4838 RVA: 0x00069A02 File Offset: 0x00067C02
				protected MinMaxVecColumnFunctionBuilderBase(IHost host, int cv, long lim, bool fix, ValueGetter<VBuffer<double>> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._aggregator = new MinMaxDblAggregator(cv);
				}

				// Token: 0x060012E7 RID: 4839 RVA: 0x00069A24 File Offset: 0x00067C24
				protected override bool ProcessValue(ref VBuffer<double> buffer)
				{
					if (!base.ProcessValue(ref buffer))
					{
						return false;
					}
					int num = this._aggregator.Min.Length;
					if (buffer.Length != num)
					{
						throw Contracts.Except(this._host, "Normalizer expected {0} slots but got {1}", new object[] { num, buffer.Length });
					}
					this._aggregator.ProcessValue(ref buffer);
					return true;
				}

				// Token: 0x04000ADF RID: 2783
				protected readonly MinMaxDblAggregator _aggregator;

				// Token: 0x04000AE0 RID: 2784
				protected readonly bool _fix;
			}

			// Token: 0x02000369 RID: 873
			public sealed class MinMaxVecColumnFunctionBuilder : NormalizeTransform.Dbl.MinMaxVecColumnFunctionBuilderBase
			{
				// Token: 0x060012E8 RID: 4840 RVA: 0x00069A90 File Offset: 0x00067C90
				private MinMaxVecColumnFunctionBuilder(IHost host, int cv, long lim, bool fix, ValueGetter<VBuffer<double>> getSrc)
					: base(host, cv, lim, fix, getSrc)
				{
				}

				// Token: 0x060012E9 RID: 4841 RVA: 0x00069AA0 File Offset: 0x00067CA0
				public static IColumnFunctionBuilder Create(NormalizeTransform.MinMaxArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<double>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Dbl.MinMaxVecColumnFunctionBuilder(host, valueCount, num, flag, getter);
				}

				// Token: 0x060012EA RID: 4842 RVA: 0x00069B24 File Offset: 0x00067D24
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					int num = this._aggregator.Min.Length;
					int num2 = num / 2;
					List<int> list = new List<int>();
					for (int i = 0; i < num; i++)
					{
						NormalizeTransform.MinMaxUtils.ComputeScaleAndOffset(this._fix, this._aggregator.Max[i], this._aggregator.Min[i], out this._aggregator.Max[i], out this._aggregator.Min[i]);
						if (this._aggregator.Min[i] != 0.0 && list.Count < num2)
						{
							list.Add(i);
						}
					}
					double[] array = this._aggregator.Min;
					int[] array2 = null;
					if (this._fix)
					{
						array = null;
					}
					else if (num == 1)
					{
						if (array[0] == 0.0)
						{
							array = null;
						}
					}
					else if (list.Count == 0)
					{
						array = null;
					}
					else if (list.Count < num2)
					{
						array2 = list.ToArray();
					}
					return NormalizeTransform.AffineColumnFunction.Create(this._host, this._aggregator.Max, array, array2);
				}
			}

			// Token: 0x0200036A RID: 874
			public sealed class MeanVarOneColumnFunctionBuilder : NormalizeTransform.OneColumnFunctionBuilderBase<double>
			{
				// Token: 0x060012EB RID: 4843 RVA: 0x00069C3C File Offset: 0x00067E3C
				private MeanVarOneColumnFunctionBuilder(IHost host, long lim, bool fix, ValueGetter<double> getSrc, bool useLog, bool useCdf)
					: base(host, lim, getSrc)
				{
					this._useLog = useLog;
					this._useCdf = useCdf;
					this._fix = fix;
					this._aggregator = new MeanVarDblAggregator(1, useLog);
					this._buffer = new VBuffer<double>(1, new double[1], null);
				}

				// Token: 0x060012EC RID: 4844 RVA: 0x00069C8C File Offset: 0x00067E8C
				public static IColumnFunctionBuilder Create(NormalizeTransform.MeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<double> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					return new NormalizeTransform.Dbl.MeanVarOneColumnFunctionBuilder(host, num, flag, getter, false, args.useCdf);
				}

				// Token: 0x060012ED RID: 4845 RVA: 0x00069D10 File Offset: 0x00067F10
				public static IColumnFunctionBuilder Create(NormalizeTransform.LogMeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<double> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					return new NormalizeTransform.Dbl.MeanVarOneColumnFunctionBuilder(host, num, false, getter, true, args.useCdf);
				}

				// Token: 0x060012EE RID: 4846 RVA: 0x00069D6A File Offset: 0x00067F6A
				protected override bool ProcessValue(ref double origVal)
				{
					if (!base.ProcessValue(ref origVal))
					{
						return false;
					}
					this._buffer.Values[0] = origVal;
					this._aggregator.ProcessValue(ref this._buffer);
					return true;
				}

				// Token: 0x060012EF RID: 4847 RVA: 0x00069D98 File Offset: 0x00067F98
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					if (this._useCdf)
					{
						return this.CreateCdfColumnFunction();
					}
					return this.CreateAffineColumnFunction();
				}

				// Token: 0x060012F0 RID: 4848 RVA: 0x00069DBC File Offset: 0x00067FBC
				private IColumnFunction CreateAffineColumnFunction()
				{
					if (this._aggregator.M2[0] == 0.0)
					{
						return NormalizeTransform.AffineColumnFunction.Create(this._host, 0.0, 0.0);
					}
					double num;
					double num2;
					if (this._fix)
					{
						NormalizeTransform.MeanVarUtils.ComputeScaleAndOffsetFixZero(this._aggregator.Mean[0], this._aggregator.MeanSquareError[0], out num, out num2);
					}
					else
					{
						NormalizeTransform.MeanVarUtils.ComputeScaleAndOffset(this._aggregator.Mean[0], this._aggregator.StdDev[0], out num, out num2);
					}
					return NormalizeTransform.AffineColumnFunction.Create(this._host, num, num2);
				}

				// Token: 0x060012F1 RID: 4849 RVA: 0x00069E5C File Offset: 0x0006805C
				private IColumnFunction CreateCdfColumnFunction()
				{
					if (this._aggregator.M2[0] == 0.0 || this._aggregator.Counts[0] == 0L)
					{
						return NormalizeTransform.CdfColumnFunction.Create(this._host, 0.0, 0.0, this._useLog);
					}
					return NormalizeTransform.CdfColumnFunction.Create(this._host, this._aggregator.Mean[0], this._aggregator.StdDev[0], this._useLog);
				}

				// Token: 0x04000AE1 RID: 2785
				private readonly bool _useLog;

				// Token: 0x04000AE2 RID: 2786
				private readonly bool _useCdf;

				// Token: 0x04000AE3 RID: 2787
				private readonly bool _fix;

				// Token: 0x04000AE4 RID: 2788
				private readonly MeanVarDblAggregator _aggregator;

				// Token: 0x04000AE5 RID: 2789
				private VBuffer<double> _buffer;
			}

			// Token: 0x0200036B RID: 875
			public sealed class MeanVarVecColumnFunctionBuilder : NormalizeTransform.VecColumnFunctionBuilderBase<double>
			{
				// Token: 0x060012F2 RID: 4850 RVA: 0x00069EE3 File Offset: 0x000680E3
				private MeanVarVecColumnFunctionBuilder(IHost host, int cv, long lim, bool fix, ValueGetter<VBuffer<double>> getSrc, bool useLog, bool useCdf)
					: base(host, lim, getSrc)
				{
					this._aggregator = new MeanVarDblAggregator(cv, useLog);
					this._fix = fix;
					this._useLog = useLog;
					this._useCdf = useCdf;
				}

				// Token: 0x060012F3 RID: 4851 RVA: 0x00069F18 File Offset: 0x00068118
				public static IColumnFunctionBuilder Create(NormalizeTransform.MeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<double>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Dbl.MeanVarVecColumnFunctionBuilder(host, valueCount, num, flag, getter, false, args.useCdf);
				}

				// Token: 0x060012F4 RID: 4852 RVA: 0x00069FA4 File Offset: 0x000681A4
				public static IColumnFunctionBuilder Create(NormalizeTransform.LogMeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<double>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Dbl.MeanVarVecColumnFunctionBuilder(host, valueCount, num, false, getter, true, args.useCdf);
				}

				// Token: 0x060012F5 RID: 4853 RVA: 0x0006A006 File Offset: 0x00068206
				protected override bool ProcessValue(ref VBuffer<double> buffer)
				{
					if (!base.ProcessValue(ref buffer))
					{
						return false;
					}
					this._aggregator.ProcessValue(ref buffer);
					return true;
				}

				// Token: 0x060012F6 RID: 4854 RVA: 0x0006A020 File Offset: 0x00068220
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					if (this._useCdf)
					{
						return this.CreateCdfColumnFunction();
					}
					return this.CreateAffineColumnFunction();
				}

				// Token: 0x060012F7 RID: 4855 RVA: 0x0006A044 File Offset: 0x00068244
				private IColumnFunction CreateAffineColumnFunction()
				{
					int num = this._aggregator.Mean.Length;
					int num2 = num / 2;
					List<int> list = new List<int>();
					double[] array = new double[num];
					double[] array2 = new double[num];
					for (int i = 0; i < num; i++)
					{
						if (this._aggregator.M2[i] == 0.0)
						{
							array[i] = (array2[i] = 0.0);
						}
						else
						{
							if (this._fix)
							{
								NormalizeTransform.MeanVarUtils.ComputeScaleAndOffsetFixZero(this._aggregator.Mean[i], this._aggregator.MeanSquareError[i], out array[i], out array2[i]);
							}
							else
							{
								NormalizeTransform.MeanVarUtils.ComputeScaleAndOffset(this._aggregator.Mean[i], this._aggregator.StdDev[i], out array[i], out array2[i]);
							}
							if (array2[i] != 0.0 && list.Count < num2)
							{
								list.Add(i);
							}
						}
					}
					int[] array3 = null;
					if (this._fix)
					{
						array2 = null;
					}
					else if (num == 1)
					{
						if (array2[0] == 0.0)
						{
							array2 = null;
						}
					}
					else if (list.Count == 0)
					{
						array2 = null;
					}
					else if (list.Count < num2)
					{
						array3 = list.ToArray();
					}
					return NormalizeTransform.AffineColumnFunction.Create(this._host, array, array2, array3);
				}

				// Token: 0x060012F8 RID: 4856 RVA: 0x0006A1A8 File Offset: 0x000683A8
				private IColumnFunction CreateCdfColumnFunction()
				{
					int num = this._aggregator.Mean.Length;
					double[] array = new double[num];
					double[] array2 = new double[num];
					for (int i = 0; i < num; i++)
					{
						if (this._aggregator.M2[i] == 0.0 || this._aggregator.Counts[i] == 0L)
						{
							array[i] = (array2[i] = 0.0);
						}
						else
						{
							array[i] = this._aggregator.Mean[i];
							array2[i] = this._aggregator.StdDev[i];
						}
					}
					return NormalizeTransform.CdfColumnFunction.Create(this._host, array, array2, this._useLog);
				}

				// Token: 0x04000AE6 RID: 2790
				private readonly bool _fix;

				// Token: 0x04000AE7 RID: 2791
				private readonly bool _useLog;

				// Token: 0x04000AE8 RID: 2792
				private readonly bool _useCdf;

				// Token: 0x04000AE9 RID: 2793
				private readonly MeanVarDblAggregator _aggregator;
			}

			// Token: 0x0200036C RID: 876
			public sealed class BinOneColumnFunctionBuilder : NormalizeTransform.OneColumnFunctionBuilderBase<double>
			{
				// Token: 0x060012F9 RID: 4857 RVA: 0x0006A251 File Offset: 0x00068451
				private BinOneColumnFunctionBuilder(IHost host, long lim, bool fix, int numBins, ValueGetter<double> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._values = new List<double>();
				}

				// Token: 0x060012FA RID: 4858 RVA: 0x0006A278 File Offset: 0x00068478
				public static IColumnFunctionBuilder Create(NormalizeTransform.BinArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<double> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int num2 = args.column[icol].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					return new NormalizeTransform.Dbl.BinOneColumnFunctionBuilder(host, num, flag, num2, getter);
				}

				// Token: 0x060012FB RID: 4859 RVA: 0x0006A330 File Offset: 0x00068530
				protected override bool ProcessValue(ref double val)
				{
					if (!base.ProcessValue(ref val))
					{
						return false;
					}
					if (val != 0.0)
					{
						this._values.Add(val);
					}
					return true;
				}

				// Token: 0x060012FC RID: 4860 RVA: 0x0006A358 File Offset: 0x00068558
				public override IColumnFunction CreateColumnFunction()
				{
					GreedyBinFinder greedyBinFinder = new GreedyBinFinder();
					checked
					{
						int num = (int)(this._lim - this._rem - unchecked((long)this._values.Count));
						this._values.RemoveAll(new Predicate<double>(double.IsNaN));
						double[] array = greedyBinFinder.FindBins(this._numBins, this._values, num);
						return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
					}
				}

				// Token: 0x04000AEA RID: 2794
				private readonly bool _fix;

				// Token: 0x04000AEB RID: 2795
				private readonly int _numBins;

				// Token: 0x04000AEC RID: 2796
				private List<double> _values;
			}

			// Token: 0x0200036D RID: 877
			public sealed class BinVecColumnFunctionBuilder : NormalizeTransform.VecColumnFunctionBuilderBase<double>
			{
				// Token: 0x060012FD RID: 4861 RVA: 0x0006A3C8 File Offset: 0x000685C8
				private BinVecColumnFunctionBuilder(IHost host, int cv, long lim, bool fix, int numBins, ValueGetter<VBuffer<double>> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._values = new List<double>[cv];
					for (int i = 0; i < cv; i++)
					{
						this._values[i] = new List<double>();
					}
				}

				// Token: 0x060012FE RID: 4862 RVA: 0x0006A414 File Offset: 0x00068614
				public static IColumnFunctionBuilder Create(NormalizeTransform.BinArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<double>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int num2 = args.column[icol].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Dbl.BinVecColumnFunctionBuilder(host, valueCount, num, flag, num2, getter);
				}

				// Token: 0x060012FF RID: 4863 RVA: 0x0006A4D8 File Offset: 0x000686D8
				protected override bool ProcessValue(ref VBuffer<double> buffer)
				{
					if (!base.ProcessValue(ref buffer))
					{
						return false;
					}
					int num = this._values.Length;
					Contracts.Check(this._host, buffer.Length == num);
					int count = buffer.Count;
					if (count == 0)
					{
						return true;
					}
					if (count == num)
					{
						double[] values = buffer.Values;
						for (int i = 0; i < count; i++)
						{
							this._values[i].Add(values[i]);
						}
					}
					else
					{
						int[] indices = buffer.Indices;
						double[] values2 = buffer.Values;
						for (int j = 0; j < count; j++)
						{
							double num2 = values2[j];
							int num3 = indices[j];
							this._values[num3].Add(num2);
						}
					}
					return true;
				}

				// Token: 0x06001300 RID: 4864 RVA: 0x0006A584 File Offset: 0x00068784
				public override IColumnFunction CreateColumnFunction()
				{
					GreedyBinFinder greedyBinFinder = new GreedyBinFinder();
					int num = this._values.Length;
					double[][] array = new double[num][];
					for (int i = 0; i < num; i++)
					{
						checked
						{
							int num2 = (int)(this._lim - this._rem - unchecked((long)this._values[i].Count));
							this._values[i].RemoveAll(new Predicate<double>(double.IsNaN));
							array[i] = greedyBinFinder.FindBins(this._numBins, this._values[i], num2);
						}
					}
					return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
				}

				// Token: 0x04000AED RID: 2797
				private readonly bool _fix;

				// Token: 0x04000AEE RID: 2798
				private readonly int _numBins;

				// Token: 0x04000AEF RID: 2799
				private List<double>[] _values;
			}

			// Token: 0x0200036E RID: 878
			public sealed class SupervisedBinOneColumnFunctionBuilder : NormalizeTransform.OneColumnSupervisedBinFunctionBuilderBase<double>
			{
				// Token: 0x06001301 RID: 4865 RVA: 0x0006A617 File Offset: 0x00068817
				private SupervisedBinOneColumnFunctionBuilder(IHost host, long lim, bool fix, int numBins, int minBinSize, int valueColumnId, int labelColumnId, IRow dataRow)
					: base(host, lim, valueColumnId, labelColumnId, dataRow)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._minBinSize = minBinSize;
				}

				// Token: 0x06001302 RID: 4866 RVA: 0x0006A63E File Offset: 0x0006883E
				protected override bool AcceptColumnValue(ref double colValue)
				{
					return !double.IsNaN(colValue);
				}

				// Token: 0x06001303 RID: 4867 RVA: 0x0006A64C File Offset: 0x0006884C
				public override IColumnFunction CreateColumnFunction()
				{
					SupervisedBinFinder supervisedBinFinder = new SupervisedBinFinder();
					double[] array = supervisedBinFinder.FindBins(this._numBins, this._minBinSize, this._labelCardinality, this._colValues, this._labels);
					return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
				}

				// Token: 0x06001304 RID: 4868 RVA: 0x0006A698 File Offset: 0x00068898
				public static IColumnFunctionBuilder Create(NormalizeTransform.SupervisedBinArguments args, IHost host, int argsColumnIndex, int valueColumnId, int labelColumnId, IRow dataRow)
				{
					long num = args.column[argsColumnIndex].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[argsColumnIndex].fixZero ?? args.fixZero;
					int num2 = args.column[argsColumnIndex].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					Contracts.CheckUserArg(host, args.minBinSize > 0, "minBinSize", "minBinSize must be positive");
					return new NormalizeTransform.Dbl.SupervisedBinOneColumnFunctionBuilder(host, num, flag, num2, args.minBinSize, valueColumnId, labelColumnId, dataRow);
				}

				// Token: 0x04000AF0 RID: 2800
				private readonly bool _fix;

				// Token: 0x04000AF1 RID: 2801
				private readonly int _numBins;

				// Token: 0x04000AF2 RID: 2802
				private readonly int _minBinSize;
			}

			// Token: 0x0200036F RID: 879
			public sealed class SupervisedBinVecColumnFunctionBuilder : NormalizeTransform.VecColumnSupervisedBinFunctionBuilderBase<double>
			{
				// Token: 0x06001305 RID: 4869 RVA: 0x0006A772 File Offset: 0x00068972
				private SupervisedBinVecColumnFunctionBuilder(IHost host, long lim, bool fix, int numBins, int minBinSize, int valueColumnId, int labelColumnId, IRow dataRow)
					: base(host, lim, valueColumnId, labelColumnId, dataRow)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._minBinSize = minBinSize;
				}

				// Token: 0x06001306 RID: 4870 RVA: 0x0006A799 File Offset: 0x00068999
				protected override bool AcceptColumnValue(ref VBuffer<double> colValuesBuffer)
				{
					return !colValuesBuffer.Values.Any(new Func<double, bool>(double.IsNaN));
				}

				// Token: 0x06001307 RID: 4871 RVA: 0x0006A7B8 File Offset: 0x000689B8
				public override IColumnFunction CreateColumnFunction()
				{
					SupervisedBinFinder supervisedBinFinder = new SupervisedBinFinder();
					double[][] array = new double[this._columnSlotCount][];
					for (int i = 0; i < this._columnSlotCount; i++)
					{
						array[i] = supervisedBinFinder.FindBins(this._numBins, this._minBinSize, this._labelCardinality, this._colValues[i], this._labels);
					}
					return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
				}

				// Token: 0x06001308 RID: 4872 RVA: 0x0006A824 File Offset: 0x00068A24
				public static IColumnFunctionBuilder Create(NormalizeTransform.SupervisedBinArguments args, IHost host, int argsColumnIndex, int valueColumnId, int labelColumnId, IRow dataRow)
				{
					long num = args.column[argsColumnIndex].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[argsColumnIndex].fixZero ?? args.fixZero;
					int num2 = args.column[argsColumnIndex].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					Contracts.CheckUserArg(host, args.minBinSize > 0, "minBinSize", "minBinSize must be positive");
					return new NormalizeTransform.Dbl.SupervisedBinVecColumnFunctionBuilder(host, num, flag, num2, args.minBinSize, valueColumnId, labelColumnId, dataRow);
				}

				// Token: 0x04000AF3 RID: 2803
				private readonly bool _fix;

				// Token: 0x04000AF4 RID: 2804
				private readonly int _numBins;

				// Token: 0x04000AF5 RID: 2805
				private readonly int _minBinSize;
			}
		}

		// Token: 0x02000370 RID: 880
		private static class Sng
		{
			// Token: 0x02000371 RID: 881
			public abstract class MinMaxOneColumnFunctionBuilderBase : NormalizeTransform.OneColumnFunctionBuilderBase<float>
			{
				// Token: 0x06001309 RID: 4873 RVA: 0x0006A8FE File Offset: 0x00068AFE
				protected MinMaxOneColumnFunctionBuilderBase(IHost host, long lim, bool fix, ValueGetter<float> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._aggregator = new MinMaxSngAggregator(1);
					this._buffer = new VBuffer<float>(1, new float[1], null);
				}

				// Token: 0x0600130A RID: 4874 RVA: 0x0006A930 File Offset: 0x00068B30
				protected override bool ProcessValue(ref float val)
				{
					if (!base.ProcessValue(ref val))
					{
						return false;
					}
					this._buffer.Values[0] = val;
					this._aggregator.ProcessValue(ref this._buffer);
					return true;
				}

				// Token: 0x04000AF6 RID: 2806
				protected readonly bool _fix;

				// Token: 0x04000AF7 RID: 2807
				protected readonly MinMaxSngAggregator _aggregator;

				// Token: 0x04000AF8 RID: 2808
				private VBuffer<float> _buffer;
			}

			// Token: 0x02000372 RID: 882
			public sealed class MinMaxOneColumnFunctionBuilder : NormalizeTransform.Sng.MinMaxOneColumnFunctionBuilderBase
			{
				// Token: 0x0600130B RID: 4875 RVA: 0x0006A95E File Offset: 0x00068B5E
				private MinMaxOneColumnFunctionBuilder(IHost host, long lim, bool fix, ValueGetter<float> getSrc)
					: base(host, lim, fix, getSrc)
				{
				}

				// Token: 0x0600130C RID: 4876 RVA: 0x0006A96C File Offset: 0x00068B6C
				public static IColumnFunctionBuilder Create(NormalizeTransform.MinMaxArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<float> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					return new NormalizeTransform.Sng.MinMaxOneColumnFunctionBuilder(host, num, flag, getter);
				}

				// Token: 0x0600130D RID: 4877 RVA: 0x0006A9E8 File Offset: 0x00068BE8
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					float num;
					float num2;
					NormalizeTransform.MinMaxUtils.ComputeScaleAndOffset(this._fix, this._aggregator.Max[0], this._aggregator.Min[0], out num, out num2);
					return NormalizeTransform.AffineColumnFunction.Create(this._host, num, num2);
				}
			}

			// Token: 0x02000373 RID: 883
			public abstract class MinMaxVecColumnFunctionBuilderBase : NormalizeTransform.VecColumnFunctionBuilderBase<float>
			{
				// Token: 0x0600130E RID: 4878 RVA: 0x0006AA36 File Offset: 0x00068C36
				protected MinMaxVecColumnFunctionBuilderBase(IHost host, int cv, long lim, bool fix, ValueGetter<VBuffer<float>> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._aggregator = new MinMaxSngAggregator(cv);
				}

				// Token: 0x0600130F RID: 4879 RVA: 0x0006AA58 File Offset: 0x00068C58
				protected override bool ProcessValue(ref VBuffer<float> buffer)
				{
					if (!base.ProcessValue(ref buffer))
					{
						return false;
					}
					int num = this._aggregator.Min.Length;
					if (buffer.Length != num)
					{
						throw Contracts.Except(this._host, "Normalizer expected {0} slots but got {1}", new object[] { num, buffer.Length });
					}
					this._aggregator.ProcessValue(ref buffer);
					return true;
				}

				// Token: 0x04000AF9 RID: 2809
				protected readonly MinMaxSngAggregator _aggregator;

				// Token: 0x04000AFA RID: 2810
				protected readonly bool _fix;
			}

			// Token: 0x02000374 RID: 884
			public sealed class MinMaxVecColumnFunctionBuilder : NormalizeTransform.Sng.MinMaxVecColumnFunctionBuilderBase
			{
				// Token: 0x06001310 RID: 4880 RVA: 0x0006AAC4 File Offset: 0x00068CC4
				private MinMaxVecColumnFunctionBuilder(IHost host, int cv, long lim, bool fix, ValueGetter<VBuffer<float>> getSrc)
					: base(host, cv, lim, fix, getSrc)
				{
				}

				// Token: 0x06001311 RID: 4881 RVA: 0x0006AAD4 File Offset: 0x00068CD4
				public static IColumnFunctionBuilder Create(NormalizeTransform.MinMaxArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<float>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Sng.MinMaxVecColumnFunctionBuilder(host, valueCount, num, flag, getter);
				}

				// Token: 0x06001312 RID: 4882 RVA: 0x0006AB58 File Offset: 0x00068D58
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					int num = this._aggregator.Min.Length;
					int num2 = num / 2;
					List<int> list = new List<int>();
					for (int i = 0; i < num; i++)
					{
						NormalizeTransform.MinMaxUtils.ComputeScaleAndOffset(this._fix, this._aggregator.Max[i], this._aggregator.Min[i], out this._aggregator.Max[i], out this._aggregator.Min[i]);
						if (this._aggregator.Min[i] != 0f && list.Count < num2)
						{
							list.Add(i);
						}
					}
					float[] array = this._aggregator.Min;
					int[] array2 = null;
					if (this._fix)
					{
						array = null;
					}
					else if (num == 1)
					{
						if (array[0] == 0f)
						{
							array = null;
						}
					}
					else if (list.Count == 0)
					{
						array = null;
					}
					else if (list.Count < num2)
					{
						array2 = list.ToArray();
					}
					return NormalizeTransform.AffineColumnFunction.Create(this._host, this._aggregator.Max, array, array2);
				}
			}

			// Token: 0x02000375 RID: 885
			public sealed class MeanVarOneColumnFunctionBuilder : NormalizeTransform.OneColumnFunctionBuilderBase<float>
			{
				// Token: 0x06001313 RID: 4883 RVA: 0x0006AC68 File Offset: 0x00068E68
				private MeanVarOneColumnFunctionBuilder(IHost host, long lim, bool fix, ValueGetter<float> getSrc, bool useLog, bool useCdf)
					: base(host, lim, getSrc)
				{
					this._useLog = useLog;
					this._useCdf = useCdf;
					this._fix = fix;
					this._aggregator = new MeanVarSngAggregator(1, useLog);
					this._buffer = new VBuffer<float>(1, new float[1], null);
				}

				// Token: 0x06001314 RID: 4884 RVA: 0x0006ACB8 File Offset: 0x00068EB8
				public static IColumnFunctionBuilder Create(NormalizeTransform.MeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<float> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					return new NormalizeTransform.Sng.MeanVarOneColumnFunctionBuilder(host, num, flag, getter, false, args.useCdf);
				}

				// Token: 0x06001315 RID: 4885 RVA: 0x0006AD3C File Offset: 0x00068F3C
				public static IColumnFunctionBuilder Create(NormalizeTransform.LogMeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<float> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					return new NormalizeTransform.Sng.MeanVarOneColumnFunctionBuilder(host, num, false, getter, true, args.useCdf);
				}

				// Token: 0x06001316 RID: 4886 RVA: 0x0006AD96 File Offset: 0x00068F96
				protected override bool ProcessValue(ref float origVal)
				{
					if (!base.ProcessValue(ref origVal))
					{
						return false;
					}
					this._buffer.Values[0] = origVal;
					this._aggregator.ProcessValue(ref this._buffer);
					return true;
				}

				// Token: 0x06001317 RID: 4887 RVA: 0x0006ADC4 File Offset: 0x00068FC4
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					if (this._useCdf)
					{
						return this.CreateCdfColumnFunction();
					}
					return this.CreateAffineColumnFunction();
				}

				// Token: 0x06001318 RID: 4888 RVA: 0x0006ADE8 File Offset: 0x00068FE8
				private IColumnFunction CreateAffineColumnFunction()
				{
					if (this._aggregator.M2[0] == 0.0)
					{
						return NormalizeTransform.AffineColumnFunction.Create(this._host, 0f, 0f);
					}
					float num;
					float num2;
					if (this._fix)
					{
						NormalizeTransform.MeanVarUtils.ComputeScaleAndOffsetFixZero(this._aggregator.Mean[0], this._aggregator.MeanSquareError[0], out num, out num2);
					}
					else
					{
						NormalizeTransform.MeanVarUtils.ComputeScaleAndOffset(this._aggregator.Mean[0], this._aggregator.StdDev[0], out num, out num2);
					}
					return NormalizeTransform.AffineColumnFunction.Create(this._host, num, num2);
				}

				// Token: 0x06001319 RID: 4889 RVA: 0x0006AE80 File Offset: 0x00069080
				private IColumnFunction CreateCdfColumnFunction()
				{
					if (this._aggregator.M2[0] == 0.0 || this._aggregator.Counts[0] == 0L)
					{
						return NormalizeTransform.CdfColumnFunction.Create(this._host, 0f, 0f, this._useLog);
					}
					return NormalizeTransform.CdfColumnFunction.Create(this._host, (float)this._aggregator.Mean[0], (float)this._aggregator.StdDev[0], this._useLog);
				}

				// Token: 0x04000AFB RID: 2811
				private readonly bool _useLog;

				// Token: 0x04000AFC RID: 2812
				private readonly bool _useCdf;

				// Token: 0x04000AFD RID: 2813
				private readonly bool _fix;

				// Token: 0x04000AFE RID: 2814
				private readonly MeanVarSngAggregator _aggregator;

				// Token: 0x04000AFF RID: 2815
				private VBuffer<float> _buffer;
			}

			// Token: 0x02000376 RID: 886
			public sealed class MeanVarVecColumnFunctionBuilder : NormalizeTransform.VecColumnFunctionBuilderBase<float>
			{
				// Token: 0x0600131A RID: 4890 RVA: 0x0006AEFF File Offset: 0x000690FF
				private MeanVarVecColumnFunctionBuilder(IHost host, int cv, long lim, bool fix, ValueGetter<VBuffer<float>> getSrc, bool useLog, bool useCdf)
					: base(host, lim, getSrc)
				{
					this._aggregator = new MeanVarSngAggregator(cv, useLog);
					this._fix = fix;
					this._useLog = useLog;
					this._useCdf = useCdf;
				}

				// Token: 0x0600131B RID: 4891 RVA: 0x0006AF34 File Offset: 0x00069134
				public static IColumnFunctionBuilder Create(NormalizeTransform.MeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<float>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Sng.MeanVarVecColumnFunctionBuilder(host, valueCount, num, flag, getter, false, args.useCdf);
				}

				// Token: 0x0600131C RID: 4892 RVA: 0x0006AFC0 File Offset: 0x000691C0
				public static IColumnFunctionBuilder Create(NormalizeTransform.LogMeanVarArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<float>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Sng.MeanVarVecColumnFunctionBuilder(host, valueCount, num, false, getter, true, args.useCdf);
				}

				// Token: 0x0600131D RID: 4893 RVA: 0x0006B022 File Offset: 0x00069222
				protected override bool ProcessValue(ref VBuffer<float> buffer)
				{
					if (!base.ProcessValue(ref buffer))
					{
						return false;
					}
					this._aggregator.ProcessValue(ref buffer);
					return true;
				}

				// Token: 0x0600131E RID: 4894 RVA: 0x0006B03C File Offset: 0x0006923C
				public override IColumnFunction CreateColumnFunction()
				{
					this._aggregator.Finish();
					if (this._useCdf)
					{
						return this.CreateCdfColumnFunction();
					}
					return this.CreateAffineColumnFunction();
				}

				// Token: 0x0600131F RID: 4895 RVA: 0x0006B060 File Offset: 0x00069260
				private IColumnFunction CreateAffineColumnFunction()
				{
					int num = this._aggregator.Mean.Length;
					int num2 = num / 2;
					List<int> list = new List<int>();
					float[] array = new float[num];
					float[] array2 = new float[num];
					for (int i = 0; i < num; i++)
					{
						if (this._aggregator.M2[i] == 0.0)
						{
							array[i] = (array2[i] = 0f);
						}
						else
						{
							if (this._fix)
							{
								NormalizeTransform.MeanVarUtils.ComputeScaleAndOffsetFixZero(this._aggregator.Mean[i], this._aggregator.MeanSquareError[i], out array[i], out array2[i]);
							}
							else
							{
								NormalizeTransform.MeanVarUtils.ComputeScaleAndOffset(this._aggregator.Mean[i], this._aggregator.StdDev[i], out array[i], out array2[i]);
							}
							if (array2[i] != 0f && list.Count < num2)
							{
								list.Add(i);
							}
						}
					}
					int[] array3 = null;
					if (this._fix)
					{
						array2 = null;
					}
					else if (num == 1)
					{
						if (array2[0] == 0f)
						{
							array2 = null;
						}
					}
					else if (list.Count == 0)
					{
						array2 = null;
					}
					else if (list.Count < num2)
					{
						array3 = list.ToArray();
					}
					return NormalizeTransform.AffineColumnFunction.Create(this._host, array, array2, array3);
				}

				// Token: 0x06001320 RID: 4896 RVA: 0x0006B1B8 File Offset: 0x000693B8
				private IColumnFunction CreateCdfColumnFunction()
				{
					int num = this._aggregator.Mean.Length;
					float[] array = new float[num];
					float[] array2 = new float[num];
					for (int i = 0; i < num; i++)
					{
						if (this._aggregator.M2[i] == 0.0 || this._aggregator.Counts[i] == 0L)
						{
							array[i] = (array2[i] = 0f);
						}
						else
						{
							array[i] = (float)this._aggregator.Mean[i];
							array2[i] = (float)this._aggregator.StdDev[i];
						}
					}
					return NormalizeTransform.CdfColumnFunction.Create(this._host, array, array2, this._useLog);
				}

				// Token: 0x04000B00 RID: 2816
				private readonly bool _fix;

				// Token: 0x04000B01 RID: 2817
				private readonly bool _useLog;

				// Token: 0x04000B02 RID: 2818
				private readonly bool _useCdf;

				// Token: 0x04000B03 RID: 2819
				private readonly MeanVarSngAggregator _aggregator;
			}

			// Token: 0x02000377 RID: 887
			public sealed class BinOneColumnFunctionBuilder : NormalizeTransform.OneColumnFunctionBuilderBase<float>
			{
				// Token: 0x06001321 RID: 4897 RVA: 0x0006B25D File Offset: 0x0006945D
				private BinOneColumnFunctionBuilder(IHost host, long lim, bool fix, int numBins, ValueGetter<float> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._values = new List<float>();
				}

				// Token: 0x06001322 RID: 4898 RVA: 0x0006B284 File Offset: 0x00069484
				public static IColumnFunctionBuilder Create(NormalizeTransform.BinArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<float> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int num2 = args.column[icol].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					return new NormalizeTransform.Sng.BinOneColumnFunctionBuilder(host, num, flag, num2, getter);
				}

				// Token: 0x06001323 RID: 4899 RVA: 0x0006B33C File Offset: 0x0006953C
				protected override bool ProcessValue(ref float val)
				{
					if (!base.ProcessValue(ref val))
					{
						return false;
					}
					if (val != 0f)
					{
						this._values.Add(val);
					}
					return true;
				}

				// Token: 0x06001324 RID: 4900 RVA: 0x0006B360 File Offset: 0x00069560
				public override IColumnFunction CreateColumnFunction()
				{
					GreedyBinFinder greedyBinFinder = new GreedyBinFinder();
					checked
					{
						int num = (int)(this._lim - this._rem - unchecked((long)this._values.Count));
						this._values.RemoveAll(new Predicate<float>(float.IsNaN));
						float[] array = greedyBinFinder.FindBins(this._numBins, this._values, num);
						return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
					}
				}

				// Token: 0x04000B04 RID: 2820
				private readonly bool _fix;

				// Token: 0x04000B05 RID: 2821
				private readonly int _numBins;

				// Token: 0x04000B06 RID: 2822
				private List<float> _values;
			}

			// Token: 0x02000378 RID: 888
			public sealed class BinVecColumnFunctionBuilder : NormalizeTransform.VecColumnFunctionBuilderBase<float>
			{
				// Token: 0x06001325 RID: 4901 RVA: 0x0006B3D0 File Offset: 0x000695D0
				private BinVecColumnFunctionBuilder(IHost host, int cv, long lim, bool fix, int numBins, ValueGetter<VBuffer<float>> getSrc)
					: base(host, lim, getSrc)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._values = new List<float>[cv];
					for (int i = 0; i < cv; i++)
					{
						this._values[i] = new List<float>();
					}
				}

				// Token: 0x06001326 RID: 4902 RVA: 0x0006B41C File Offset: 0x0006961C
				public static IColumnFunctionBuilder Create(NormalizeTransform.BinArguments args, IHost host, int icol, ColumnType srcType, ValueGetter<VBuffer<float>> getter)
				{
					long num = args.column[icol].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[icol].fixZero ?? args.fixZero;
					int num2 = args.column[icol].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					int valueCount = srcType.ValueCount;
					return new NormalizeTransform.Sng.BinVecColumnFunctionBuilder(host, valueCount, num, flag, num2, getter);
				}

				// Token: 0x06001327 RID: 4903 RVA: 0x0006B4E0 File Offset: 0x000696E0
				protected override bool ProcessValue(ref VBuffer<float> buffer)
				{
					if (!base.ProcessValue(ref buffer))
					{
						return false;
					}
					int num = this._values.Length;
					Contracts.Check(this._host, buffer.Length == num);
					int count = buffer.Count;
					if (count == 0)
					{
						return true;
					}
					if (count == num)
					{
						float[] values = buffer.Values;
						for (int i = 0; i < count; i++)
						{
							this._values[i].Add(values[i]);
						}
					}
					else
					{
						int[] indices = buffer.Indices;
						float[] values2 = buffer.Values;
						for (int j = 0; j < count; j++)
						{
							float num2 = values2[j];
							int num3 = indices[j];
							this._values[num3].Add(num2);
						}
					}
					return true;
				}

				// Token: 0x06001328 RID: 4904 RVA: 0x0006B58C File Offset: 0x0006978C
				public override IColumnFunction CreateColumnFunction()
				{
					GreedyBinFinder greedyBinFinder = new GreedyBinFinder();
					int num = this._values.Length;
					float[][] array = new float[num][];
					for (int i = 0; i < num; i++)
					{
						checked
						{
							int num2 = (int)(this._lim - this._rem - unchecked((long)this._values[i].Count));
							this._values[i].RemoveAll(new Predicate<float>(float.IsNaN));
							array[i] = greedyBinFinder.FindBins(this._numBins, this._values[i], num2);
						}
					}
					return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
				}

				// Token: 0x04000B07 RID: 2823
				private readonly bool _fix;

				// Token: 0x04000B08 RID: 2824
				private readonly int _numBins;

				// Token: 0x04000B09 RID: 2825
				private List<float>[] _values;
			}

			// Token: 0x02000379 RID: 889
			public sealed class SupervisedBinOneColumnFunctionBuilder : NormalizeTransform.OneColumnSupervisedBinFunctionBuilderBase<float>
			{
				// Token: 0x06001329 RID: 4905 RVA: 0x0006B61F File Offset: 0x0006981F
				private SupervisedBinOneColumnFunctionBuilder(IHost host, long lim, bool fix, int numBins, int minBinSize, int valueColumnId, int labelColumnId, IRow dataRow)
					: base(host, lim, valueColumnId, labelColumnId, dataRow)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._minBinSize = minBinSize;
				}

				// Token: 0x0600132A RID: 4906 RVA: 0x0006B646 File Offset: 0x00069846
				protected override bool AcceptColumnValue(ref float colValue)
				{
					return !float.IsNaN(colValue);
				}

				// Token: 0x0600132B RID: 4907 RVA: 0x0006B654 File Offset: 0x00069854
				public override IColumnFunction CreateColumnFunction()
				{
					SupervisedBinFinder supervisedBinFinder = new SupervisedBinFinder();
					float[] array = supervisedBinFinder.FindBins(this._numBins, this._minBinSize, this._labelCardinality, this._colValues, this._labels);
					return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
				}

				// Token: 0x0600132C RID: 4908 RVA: 0x0006B6A0 File Offset: 0x000698A0
				public static IColumnFunctionBuilder Create(NormalizeTransform.SupervisedBinArguments args, IHost host, int argsColumnIndex, int valueColumnId, int labelColumnId, IRow dataRow)
				{
					long num = args.column[argsColumnIndex].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[argsColumnIndex].fixZero ?? args.fixZero;
					int num2 = args.column[argsColumnIndex].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					Contracts.CheckUserArg(host, args.minBinSize > 0, "minBinSize", "minBinSize must be positive");
					return new NormalizeTransform.Sng.SupervisedBinOneColumnFunctionBuilder(host, num, flag, num2, args.minBinSize, valueColumnId, labelColumnId, dataRow);
				}

				// Token: 0x04000B0A RID: 2826
				private readonly bool _fix;

				// Token: 0x04000B0B RID: 2827
				private readonly int _numBins;

				// Token: 0x04000B0C RID: 2828
				private readonly int _minBinSize;
			}

			// Token: 0x0200037A RID: 890
			public sealed class SupervisedBinVecColumnFunctionBuilder : NormalizeTransform.VecColumnSupervisedBinFunctionBuilderBase<float>
			{
				// Token: 0x0600132D RID: 4909 RVA: 0x0006B77A File Offset: 0x0006997A
				private SupervisedBinVecColumnFunctionBuilder(IHost host, long lim, bool fix, int numBins, int minBinSize, int valueColumnId, int labelColumnId, IRow dataRow)
					: base(host, lim, valueColumnId, labelColumnId, dataRow)
				{
					this._fix = fix;
					this._numBins = numBins;
					this._minBinSize = minBinSize;
				}

				// Token: 0x0600132E RID: 4910 RVA: 0x0006B7A1 File Offset: 0x000699A1
				protected override bool AcceptColumnValue(ref VBuffer<float> colValuesBuffer)
				{
					return !colValuesBuffer.Values.Any(new Func<float, bool>(float.IsNaN));
				}

				// Token: 0x0600132F RID: 4911 RVA: 0x0006B7C0 File Offset: 0x000699C0
				public override IColumnFunction CreateColumnFunction()
				{
					SupervisedBinFinder supervisedBinFinder = new SupervisedBinFinder();
					float[][] array = new float[this._columnSlotCount][];
					for (int i = 0; i < this._columnSlotCount; i++)
					{
						array[i] = supervisedBinFinder.FindBins(this._numBins, this._minBinSize, this._labelCardinality, this._colValues[i], this._labels);
					}
					return NormalizeTransform.BinColumnFunction.Create(this._host, array, this._fix);
				}

				// Token: 0x06001330 RID: 4912 RVA: 0x0006B82C File Offset: 0x00069A2C
				public static IColumnFunctionBuilder Create(NormalizeTransform.SupervisedBinArguments args, IHost host, int argsColumnIndex, int valueColumnId, int labelColumnId, IRow dataRow)
				{
					long num = args.column[argsColumnIndex].maxTrainingExamples ?? args.maxTrainingExamples;
					Contracts.CheckUserArg(host, num > 1L, "maxTrainingExample", "maxTrainingExamples must be greater than 1");
					bool flag = args.column[argsColumnIndex].fixZero ?? args.fixZero;
					int num2 = args.column[argsColumnIndex].numBins ?? args.numBins;
					Contracts.CheckUserArg(host, num2 > 1, "numBins", "numBins must be greater than 1");
					Contracts.CheckUserArg(host, args.minBinSize > 0, "minBinSize", "minBinSize must be positive");
					return new NormalizeTransform.Sng.SupervisedBinVecColumnFunctionBuilder(host, num, flag, num2, args.minBinSize, valueColumnId, labelColumnId, dataRow);
				}

				// Token: 0x04000B0D RID: 2829
				private readonly bool _fix;

				// Token: 0x04000B0E RID: 2830
				private readonly int _numBins;

				// Token: 0x04000B0F RID: 2831
				private readonly int _minBinSize;
			}
		}
	}
}
