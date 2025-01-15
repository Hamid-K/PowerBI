using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000CB RID: 203
	public sealed class NAIndicatorTransform : OneToOneTransformBase
	{
		// Token: 0x06000440 RID: 1088 RVA: 0x00017A3C File Offset: 0x00015C3C
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("NAIND TF", 65537U, 65537U, 65537U, "NaIndicatorTransform", null);
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x00017A60 File Offset: 0x00015C60
		private static string TestType(ColumnType type)
		{
			Delegate @delegate;
			if (Conversions.Instance.TryGetIsNAPredicate(type.ItemType, out @delegate))
			{
				return null;
			}
			return string.Format("Type '{0}' is not supported by {1} since it doesn't have an NA value", type, "NaIndicatorTransform");
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00017A93 File Offset: 0x00015C93
		public NAIndicatorTransform(NAIndicatorTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "NaIndicator", Contracts.CheckRef<NAIndicatorTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(NAIndicatorTransform.TestType))
		{
			this._types = this.GetTypesAndMetadata();
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x00017ACA File Offset: 0x00015CCA
		private NAIndicatorTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(NAIndicatorTransform.TestType))
		{
			this._types = this.GetTypesAndMetadata();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00017B10 File Offset: 0x00015D10
		public static NAIndicatorTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("NaIndicator");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(NAIndicatorTransform.GetVersionInfo());
			return HostExtensions.Apply<NAIndicatorTransform>(h, "Loading Model", (IChannel ch) => new NAIndicatorTransform(ctx, h, input));
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00017BA5 File Offset: 0x00015DA5
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(NAIndicatorTransform.GetVersionInfo());
			base.SaveBase(ctx);
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x00017BD0 File Offset: 0x00015DD0
		private ColumnType[] GetTypesAndMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			ColumnType[] array = new ColumnType[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ColumnType typeSrc = this.Infos[i].TypeSrc;
				if (!typeSrc.IsVector)
				{
					array[i] = NumberType.Float;
				}
				else
				{
					array[i] = new VectorType(NumberType.Float, typeSrc.AsVector);
				}
				using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source, "SlotNames"))
				{
					builder.AddPrimitive<DvBool>("IsNormalized", BoolType.Instance, DvBool.True);
				}
			}
			metadata.Seal();
			return array;
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x00017CA0 File Offset: 0x00015EA0
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._types[iinfo];
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x00017CAA File Offset: 0x00015EAA
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			if (!this.Infos[iinfo].TypeSrc.IsVector)
			{
				return this.ComposeGetterOne(input, iinfo);
			}
			return this.ComposeGetterVec(input, iinfo);
		}

		// Token: 0x06000449 RID: 1097 RVA: 0x00017CD8 File Offset: 0x00015ED8
		private ValueGetter<float> ComposeGetterOne(IRow input, int iinfo)
		{
			Func<IRow, int, ValueGetter<float>> func = new Func<IRow, int, ValueGetter<float>>(this.ComposeGetterOne<int>);
			return Utils.MarshalInvoke<IRow, int, ValueGetter<float>>(func, this.Infos[iinfo].TypeSrc.RawType, input, iinfo);
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00017D48 File Offset: 0x00015F48
		private ValueGetter<float> ComposeGetterOne<T>(IRow input, int iinfo)
		{
			ValueGetter<T> getSrc = base.GetSrcGetter<T>(input, iinfo);
			RefPredicate<T> isNA = Conversions.Instance.GetIsNAPredicate<T>(input.Schema.GetColumnType(this.Infos[iinfo].Source));
			T src = default(T);
			return delegate(ref float dst)
			{
				getSrc.Invoke(ref src);
				dst = (isNA.Invoke(ref src) ? 1f : 0f);
			};
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00017DAC File Offset: 0x00015FAC
		private ValueGetter<VBuffer<float>> ComposeGetterVec(IRow input, int iinfo)
		{
			Func<IRow, int, ValueGetter<VBuffer<float>>> func = new Func<IRow, int, ValueGetter<VBuffer<float>>>(this.ComposeGetterVec<int>);
			return Utils.MarshalInvoke<IRow, int, ValueGetter<VBuffer<float>>>(func, this.Infos[iinfo].TypeSrc.ItemType.RawType, input, iinfo);
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00017E54 File Offset: 0x00016054
		private ValueGetter<VBuffer<float>> ComposeGetterVec<T>(IRow input, int iinfo)
		{
			ValueGetter<VBuffer<T>> getSrc = base.GetSrcGetter<VBuffer<T>>(input, iinfo);
			RefPredicate<T> isNA = Conversions.Instance.GetIsNAPredicate<T>(input.Schema.GetColumnType(this.Infos[iinfo].Source).ItemType);
			T t = default(T);
			bool defaultIsNA = isNA.Invoke(ref t);
			VBuffer<T> src = default(VBuffer<T>);
			List<int> indices = new List<int>();
			return delegate(ref VBuffer<float> dst)
			{
				getSrc.Invoke(ref src);
				bool flag;
				this.FindNAs<T>(ref src, isNA, defaultIsNA, indices, out flag);
				this.FillValues(src.Length, ref dst, indices, flag);
			};
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00017EE8 File Offset: 0x000160E8
		private void FindNAs<T>(ref VBuffer<T> src, RefPredicate<T> isNA, bool defaultIsNA, List<int> indices, out bool sense)
		{
			indices.Clear();
			T[] values = src.Values;
			int count = src.Count;
			if (src.IsDense)
			{
				for (int i = 0; i < count; i++)
				{
					if (isNA.Invoke(ref values[i]))
					{
						indices.Add(i);
					}
				}
				sense = true;
				return;
			}
			if (!defaultIsNA)
			{
				int[] indices2 = src.Indices;
				for (int j = 0; j < count; j++)
				{
					if (isNA.Invoke(ref values[j]))
					{
						indices.Add(indices2[j]);
					}
				}
				sense = true;
				return;
			}
			int[] indices3 = src.Indices;
			for (int k = 0; k < count; k++)
			{
				if (!isNA.Invoke(ref values[k]))
				{
					indices.Add(indices3[k]);
				}
			}
			sense = false;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00017FAC File Offset: 0x000161AC
		private void FillValues(int srcLength, ref VBuffer<float> dst, List<int> indices, bool sense)
		{
			float[] values = dst.Values;
			int[] indices2 = dst.Indices;
			if (indices.Count == 0)
			{
				if (sense)
				{
					dst = new VBuffer<float>(srcLength, 0, values, indices2);
					return;
				}
				Utils.EnsureSize<float>(ref values, srcLength, false);
				for (int i = 0; i < srcLength; i++)
				{
					values[i] = 1f;
				}
				dst = new VBuffer<float>(srcLength, values, indices2);
				return;
			}
			else
			{
				if (sense && indices.Count < srcLength / 2)
				{
					int count = indices.Count;
					Utils.EnsureSize<float>(ref values, count, false);
					Utils.EnsureSize<int>(ref indices2, count, false);
					indices.CopyTo(indices2);
					for (int j = 0; j < count; j++)
					{
						values[j] = 1f;
					}
					dst = new VBuffer<float>(srcLength, count, values, indices2);
					return;
				}
				if (!sense && srcLength - indices.Count < srcLength / 2)
				{
					int num = srcLength - indices.Count;
					Utils.EnsureSize<float>(ref values, num, false);
					Utils.EnsureSize<int>(ref indices2, num, false);
					indices.Add(srcLength);
					int num2 = 0;
					int num3 = 0;
					int num4 = indices[num3];
					for (int k = 0; k < srcLength; k++)
					{
						if (k < num4)
						{
							values[num2] = 1f;
							indices2[num2++] = k;
						}
						else
						{
							num4 = indices[++num3];
						}
					}
					dst = new VBuffer<float>(srcLength, num, values, indices2);
					return;
				}
				Utils.EnsureSize<float>(ref values, srcLength, false);
				indices.Add(srcLength);
				int num5 = 0;
				float num6 = (sense ? 1f : 0f);
				float num7 = (sense ? 0f : 1f);
				for (int l = 0; l < srcLength; l++)
				{
					if (l == indices[num5])
					{
						values[l] = num6;
						num5++;
					}
					else
					{
						values[l] = num7;
					}
				}
				dst = new VBuffer<float>(srcLength, values, indices2);
				return;
			}
		}

		// Token: 0x040001C8 RID: 456
		public const string LoaderSignature = "NaIndicatorTransform";

		// Token: 0x040001C9 RID: 457
		private const string RegistrationName = "NaIndicator";

		// Token: 0x040001CA RID: 458
		private readonly ColumnType[] _types;

		// Token: 0x020000CC RID: 204
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x0600044F RID: 1103 RVA: 0x0001817C File Offset: 0x0001637C
			public static NAIndicatorTransform.Column Parse(string str)
			{
				NAIndicatorTransform.Column column = new NAIndicatorTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000450 RID: 1104 RVA: 0x0001819B File Offset: 0x0001639B
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x020000CD RID: 205
		public sealed class Arguments
		{
			// Token: 0x040001CB RID: 459
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public NAIndicatorTransform.Column[] column;
		}
	}
}
