using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200032C RID: 812
	public sealed class MissingValueIndicatorTransform : OneToOneTransformBase
	{
		// Token: 0x06001217 RID: 4631 RVA: 0x00065344 File Offset: 0x00063544
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("MISFEATF", 65538U, 65538U, 65538U, "MissingIndicatorFunction", "MissingFeatureFunction");
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x00065369 File Offset: 0x00063569
		public MissingValueIndicatorTransform(MissingValueIndicatorTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "MissingIndicator", Contracts.CheckRef<MissingValueIndicatorTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsFloatItem))
		{
			this._types = this.GetTypesAndMetadata();
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000653A0 File Offset: 0x000635A0
		private MissingValueIndicatorTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(OneToOneTransformBase.TestIsFloatItem))
		{
			this._types = this.GetTypesAndMetadata();
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0006540C File Offset: 0x0006360C
		public static MissingValueIndicatorTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("MissingIndicator");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(MissingValueIndicatorTransform.GetVersionInfo());
			return HostExtensions.Apply<MissingValueIndicatorTransform>(h, "Loading Model", delegate(IChannel ch)
			{
				int num = ctx.Reader.ReadInt32();
				Contracts.CheckDecode(ch, num == 4);
				return new MissingValueIndicatorTransform(ctx, h, input);
			});
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x000654A1 File Offset: 0x000636A1
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(MissingValueIndicatorTransform.GetVersionInfo());
			ctx.Writer.Write(4);
			base.SaveBase(ctx);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000654D8 File Offset: 0x000636D8
		private VectorType[] GetTypesAndMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			VectorType[] array = new VectorType[this.Infos.Length];
			int i = 0;
			while (i < this.Infos.Length)
			{
				ColumnType typeSrc = this.Infos[i].TypeSrc;
				Contracts.Check(this._host, typeSrc.ValueCount < 1073741823);
				if (!typeSrc.IsVector)
				{
					array[i] = new VectorType(NumberType.Float, 2);
					goto IL_00CB;
				}
				array[i] = new VectorType(NumberType.Float, typeSrc.AsVector, new int[] { 2 });
				ColumnType metadataTypeOrNull;
				if (typeSrc.IsKnownSizeVector && (metadataTypeOrNull = this._input.Schema.GetMetadataTypeOrNull("SlotNames", this.Infos[i].Source)) != null && metadataTypeOrNull.VectorSize == typeSrc.VectorSize && metadataTypeOrNull.ItemType.IsText)
				{
					goto IL_00CB;
				}
				IL_0107:
				i++;
				continue;
				IL_00CB:
				using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i))
				{
					builder.AddGetter<VBuffer<DvText>>("SlotNames", MetadataUtils.GetNamesType(array[i].VectorSize), new MetadataUtils.MetadataGetter<VBuffer<DvText>>(this.GetSlotNames));
				}
				goto IL_0107;
			}
			metadata.Seal();
			return array;
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00065618 File Offset: 0x00063818
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._types[iinfo];
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00065624 File Offset: 0x00063824
		private void GetSlotNames(int iinfo, ref VBuffer<DvText> dst)
		{
			int vectorSize = this._types[iinfo].VectorSize;
			if (vectorSize == 0)
			{
				throw MetadataUtils.ExceptGetMetadata();
			}
			DvText[] array = dst.Values;
			if (Utils.Size<DvText>(array) < vectorSize)
			{
				array = new DvText[vectorSize];
			}
			ColumnType typeSrc = this.Infos[iinfo].TypeSrc;
			if (!typeSrc.IsVector)
			{
				string columnName = this._input.Schema.GetColumnName(this.Infos[iinfo].Source);
				array[0] = new DvText(columnName);
				array[1] = new DvText(columnName + "_Indicator");
			}
			else
			{
				ColumnType metadataTypeOrNull = this._input.Schema.GetMetadataTypeOrNull("SlotNames", this.Infos[iinfo].Source);
				if (metadataTypeOrNull == null || metadataTypeOrNull.VectorSize != typeSrc.VectorSize || !metadataTypeOrNull.ItemType.IsText)
				{
					throw MetadataUtils.ExceptGetMetadata();
				}
				VBuffer<DvText> vbuffer = default(VBuffer<DvText>);
				this._input.Schema.GetMetadata<VBuffer<DvText>>("SlotNames", this.Infos[iinfo].Source, ref vbuffer);
				Contracts.Check(this._host, vbuffer.Length == typeSrc.VectorSize, "Unexpected slot name vector size");
				StringBuilder stringBuilder = new StringBuilder();
				int num = 0;
				foreach (KeyValuePair<int, DvText> keyValuePair in vbuffer.Items(true))
				{
					stringBuilder.Clear();
					if (!keyValuePair.Value.HasChars)
					{
						stringBuilder.Append('[').Append(num / 2).Append(']');
					}
					else
					{
						keyValuePair.Value.AddToStringBuilder(stringBuilder);
					}
					int length = stringBuilder.Length;
					stringBuilder.Append("_Indicator");
					string text = stringBuilder.ToString();
					array[num++] = new DvText(text, 0, length);
					array[num++] = new DvText(text);
				}
			}
			dst = new VBuffer<DvText>(vectorSize, array, dst.Indices);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000658B4 File Offset: 0x00063AB4
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			ValueGetter<VBuffer<float>> valueGetter;
			if (this.Infos[iinfo].TypeSrc.IsVector)
			{
				ValueGetter<VBuffer<float>> getSrc2 = base.GetSrcGetter<VBuffer<float>>(input, iinfo);
				valueGetter = delegate(ref VBuffer<float> dst)
				{
					getSrc2.Invoke(ref dst);
					MissingValueIndicatorTransform.FillValues(this._host, ref dst);
				};
			}
			else
			{
				ValueGetter<float> getSrc = base.GetSrcGetter<float>(input, iinfo);
				valueGetter = delegate(ref VBuffer<float> dst)
				{
					float num = 0f;
					getSrc.Invoke(ref num);
					MissingValueIndicatorTransform.FillValues(num, ref dst);
				};
			}
			return valueGetter;
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00065928 File Offset: 0x00063B28
		private static void FillValues(float input, ref VBuffer<float> result)
		{
			float[] array = result.Values;
			int[] array2 = result.Indices;
			if (input == 0f)
			{
				result = new VBuffer<float>(2, 0, array, array2);
				return;
			}
			if (Utils.Size<float>(array) < 1)
			{
				array = new float[1];
			}
			if (Utils.Size<int>(array2) < 1)
			{
				array2 = new int[1];
			}
			if (float.IsNaN(input))
			{
				array[0] = 1f;
				array2[0] = 1;
			}
			else
			{
				array[0] = input;
				array2[0] = 0;
			}
			result = new VBuffer<float>(2, 1, array, array2);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x000659A8 File Offset: 0x00063BA8
		private static void FillValues(IExceptionContext ectx, ref VBuffer<float> buffer)
		{
			int length = buffer.Length;
			Contracts.Check(ectx, (0 <= length) & (length < 1073741823));
			int count = buffer.Count;
			float[] values = buffer.Values;
			int[] array = buffer.Indices;
			int num = 0;
			if (count >= length)
			{
				if (Utils.Size<int>(array) < length)
				{
					array = new int[length];
				}
				for (int i = 0; i < count; i++)
				{
					float num2 = values[i];
					if (num2 != 0f)
					{
						if (float.IsNaN(num2))
						{
							values[num] = 1f;
							array[num] = 2 * i + 1;
						}
						else
						{
							values[num] = num2;
							array[num] = 2 * i;
						}
						num++;
					}
				}
			}
			else
			{
				for (int j = 0; j < count; j++)
				{
					float num3 = values[j];
					if (num3 != 0f)
					{
						int num4 = array[j];
						if (float.IsNaN(num3))
						{
							values[num] = 1f;
							array[num] = 2 * num4 + 1;
						}
						else
						{
							values[num] = num3;
							array[num] = 2 * num4;
						}
						num++;
					}
				}
			}
			buffer = new VBuffer<float>(length * 2, num, values, array);
		}

		// Token: 0x04000A8F RID: 2703
		public const string LoaderSignature = "MissingIndicatorFunction";

		// Token: 0x04000A90 RID: 2704
		private const string RegistrationName = "MissingIndicator";

		// Token: 0x04000A91 RID: 2705
		private const string IndicatorSuffix = "_Indicator";

		// Token: 0x04000A92 RID: 2706
		private readonly VectorType[] _types;

		// Token: 0x0200032D RID: 813
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06001222 RID: 4642 RVA: 0x00065ABC File Offset: 0x00063CBC
			public static MissingValueIndicatorTransform.Column Parse(string str)
			{
				MissingValueIndicatorTransform.Column column = new MissingValueIndicatorTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06001223 RID: 4643 RVA: 0x00065ADB File Offset: 0x00063CDB
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x0200032E RID: 814
		public sealed class Arguments
		{
			// Token: 0x04000A93 RID: 2707
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public MissingValueIndicatorTransform.Column[] column;
		}
	}
}
