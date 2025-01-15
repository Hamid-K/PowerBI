using System;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000034 RID: 52
	public sealed class NADropTransform : OneToOneTransformBase
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00008BD6 File Offset: 0x00006DD6
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("NADROPXF", 65537U, 65537U, 65537U, "NADropTransform", null);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00008BF8 File Offset: 0x00006DF8
		public NADropTransform(NADropTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(Contracts.CheckRef<IHostEnvironment>(env, "env"), "DropNAs", Contracts.CheckRef<NADropTransform.Arguments>(env, args, "args").column, input, new Func<ColumnType, string>(NADropTransform.TestType))
		{
			Contracts.CheckNonEmpty<NADropTransform.Column>(this._host, args.column, "column");
			this._isNAs = this.InitIsNAAndMetadata();
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00008C5C File Offset: 0x00006E5C
		private Delegate[] InitIsNAAndMetadata()
		{
			MetadataDispatcher metadata = base.Metadata;
			Delegate[] array = new Delegate[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ColumnType typeSrc = this.Infos[i].TypeSrc;
				array[i] = this.GetIsNADelegate(typeSrc);
				using (MetadataDispatcher.Builder builder = metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source, new string[] { "IsNormalized", "KeyValues" }))
				{
					builder.AddPrimitive<DvBool>("HasMissingValues", BoolType.Instance, DvBool.False);
				}
			}
			metadata.Seal();
			return array;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x00008D24 File Offset: 0x00006F24
		private Delegate GetIsNADelegate(ColumnType type)
		{
			Func<ColumnType, Delegate> func = new Func<ColumnType, Delegate>(this.GetIsNADelegate<int>);
			return Utils.MarshalInvoke<ColumnType, Delegate>(func, type.ItemType.RawType, type);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00008D50 File Offset: 0x00006F50
		private Delegate GetIsNADelegate<T>(ColumnType type)
		{
			return Conversions.Instance.GetIsNAPredicate<T>(type.ItemType);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00008D64 File Offset: 0x00006F64
		private static string TestType(ColumnType type)
		{
			if (!type.IsVector)
			{
				return string.Format("Type '{0}' is not supported by {1} since it is not a vector", type, "NADropTransform");
			}
			Func<ColumnType, string> func = new Func<ColumnType, string>(NADropTransform.TestType<int>);
			return Utils.MarshalInvoke<ColumnType, string>(func, type.ItemType.RawType, type.ItemType);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00008DB0 File Offset: 0x00006FB0
		private static string TestType<T>(ColumnType type)
		{
			RefPredicate<T> refPredicate;
			if (!Conversions.Instance.TryGetIsNAPredicate<T>(type.ItemType, out refPredicate))
			{
				return string.Format("Type '{0}' is not supported by {1} since it doesn't have an NA value", type, "NADropTransform");
			}
			return null;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x00008E04 File Offset: 0x00007004
		public static NADropTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("DropNAs");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(NADropTransform.GetVersionInfo());
			return HostExtensions.Apply<NADropTransform>(h, "Loading Model", (IChannel ch) => new NADropTransform(ctx, h, input));
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00008E99 File Offset: 0x00007099
		private NADropTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(NADropTransform.TestType))
		{
			this._isNAs = this.InitIsNAAndMetadata();
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00008EBC File Offset: 0x000070BC
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(NADropTransform.GetVersionInfo());
			base.SaveBase(ctx);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00008EE7 File Offset: 0x000070E7
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return new VectorType(this.Infos[iinfo].TypeSrc.ItemType.AsPrimitive, 0);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00008F08 File Offset: 0x00007108
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			Func<IRow, int, ValueGetter<VBuffer<int>>> func = new Func<IRow, int, ValueGetter<VBuffer<int>>>(this.MakeVecGetter<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this.Infos[iinfo].TypeSrc.ItemType.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[] { input, iinfo });
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00008FD8 File Offset: 0x000071D8
		private ValueGetter<VBuffer<TDst>> MakeVecGetter<TDst>(IRow input, int iinfo)
		{
			ValueGetter<VBuffer<TDst>> srcGetter = base.GetSrcGetter<VBuffer<TDst>>(input, iinfo);
			VBuffer<TDst> buffer = default(VBuffer<TDst>);
			RefPredicate<TDst> isNA = (RefPredicate<TDst>)this._isNAs[iinfo];
			TDst tdst = default(TDst);
			if (isNA.Invoke(ref tdst))
			{
				return delegate(ref VBuffer<TDst> value)
				{
					srcGetter.Invoke(ref buffer);
					this.DropNAsAndDefaults<TDst>(ref buffer, ref value, isNA);
				};
			}
			return delegate(ref VBuffer<TDst> value)
			{
				srcGetter.Invoke(ref buffer);
				this.DropNAs<TDst>(ref buffer, ref value, isNA);
			};
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009058 File Offset: 0x00007258
		private void DropNAsAndDefaults<TDst>(ref VBuffer<TDst> src, ref VBuffer<TDst> dst, RefPredicate<TDst> isNA)
		{
			int num = 0;
			for (int i = 0; i < src.Count; i++)
			{
				if (!isNA.Invoke(ref src.Values[i]))
				{
					num++;
				}
			}
			if (num == 0)
			{
				dst = new VBuffer<TDst>(0, dst.Values, dst.Indices);
				return;
			}
			if (num == src.Count)
			{
				Utils.Swap<VBuffer<TDst>>(ref src, ref dst);
				if (!dst.IsDense)
				{
					dst = new VBuffer<TDst>(dst.Count, dst.Values, dst.Indices);
				}
				return;
			}
			int num2 = 0;
			TDst[] array = dst.Values;
			if (Utils.Size<TDst>(array) < num)
			{
				array = new TDst[num];
			}
			for (int j = 0; j < src.Count; j++)
			{
				if (!isNA.Invoke(ref src.Values[j]))
				{
					array[num2++] = src.Values[j];
				}
			}
			dst = new VBuffer<TDst>(num, array, dst.Indices);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009150 File Offset: 0x00007350
		private void DropNAs<TDst>(ref VBuffer<TDst> src, ref VBuffer<TDst> dst, RefPredicate<TDst> isNA)
		{
			int num = 0;
			for (int i = 0; i < src.Count; i++)
			{
				if (!isNA.Invoke(ref src.Values[i]))
				{
					num++;
				}
			}
			if (num == 0)
			{
				dst = new VBuffer<TDst>(src.Length - src.Count, 0, dst.Values, dst.Indices);
				return;
			}
			if (num == src.Count)
			{
				Utils.Swap<VBuffer<TDst>>(ref src, ref dst);
				return;
			}
			TDst[] array = dst.Values;
			if (Utils.Size<TDst>(array) < num)
			{
				array = new TDst[num];
			}
			int num2 = 0;
			if (src.IsDense)
			{
				for (int j = 0; j < src.Count; j++)
				{
					if (!isNA.Invoke(ref src.Values[j]))
					{
						array[num2] = src.Values[j];
						num2++;
					}
				}
				dst = new VBuffer<TDst>(num, array, dst.Indices);
				return;
			}
			int[] array2 = dst.Indices;
			if (Utils.Size<int>(array2) < num)
			{
				array2 = new int[num];
			}
			int num3 = 0;
			for (int k = 0; k < src.Count; k++)
			{
				if (!isNA.Invoke(ref src.Values[k]))
				{
					array[num2] = src.Values[k];
					array2[num2] = src.Indices[k] - num3;
					num2++;
				}
				else
				{
					num3++;
				}
			}
			dst = new VBuffer<TDst>(src.Length - num3, num, array, array2);
		}

		// Token: 0x04000086 RID: 134
		internal const string Summary = "Removes NAs from vector columns.";

		// Token: 0x04000087 RID: 135
		public const string LoaderSignature = "NADropTransform";

		// Token: 0x04000088 RID: 136
		private const string RegistrationName = "DropNAs";

		// Token: 0x04000089 RID: 137
		private readonly Delegate[] _isNAs;

		// Token: 0x02000035 RID: 53
		public sealed class Arguments
		{
			// Token: 0x0400008A RID: 138
			[Argument(4, HelpText = "Columns to drop the NAs for", ShortName = "col", SortOrder = 1)]
			public NADropTransform.Column[] column;
		}

		// Token: 0x02000036 RID: 54
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000134 RID: 308 RVA: 0x000092D0 File Offset: 0x000074D0
			public static NADropTransform.Column Parse(string str)
			{
				NADropTransform.Column column = new NADropTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000135 RID: 309 RVA: 0x000092EF File Offset: 0x000074EF
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}
	}
}
