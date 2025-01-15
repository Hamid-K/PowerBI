using System;
using System.Reflection;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020000EC RID: 236
	public static class LambdaColumnMapper
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0001A558 File Offset: 0x00018758
		public static IDataView Create<TSrc, TDst>(IHostEnvironment env, string name, IDataView input, string src, string dst, ColumnType typeSrc, ColumnType typeDst, ValueMapper<TSrc, TDst> mapper, ValueGetter<VBuffer<DvText>> keyValueGetter = null)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckNonEmpty(env, name, "name");
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.CheckNonEmpty(env, src, "src");
			Contracts.CheckNonEmpty(env, dst, "dst");
			Contracts.CheckValue<ColumnType>(env, typeSrc, "typeSrc");
			Contracts.CheckValue<ColumnType>(env, typeDst, "typeDst");
			Contracts.CheckValue<ValueMapper<TSrc, TDst>>(env, mapper, "mapper");
			Contracts.Check(env, keyValueGetter == null || typeDst.IsKey);
			if (typeSrc.RawType != typeof(TSrc))
			{
				throw Contracts.ExceptParam(env, "mapper", "The source column type '{0}' doesn't match the input type of the mapper", new object[] { typeSrc });
			}
			if (typeDst.RawType != typeof(TDst))
			{
				throw Contracts.ExceptParam(env, "mapper", "The destination column type '{0}' doesn't match the output type of the mapper", new object[] { typeDst });
			}
			int num;
			if (!input.Schema.TryGetColumnIndex(src, ref num))
			{
				throw Contracts.ExceptParam(env, "src", "The input data doesn't have a column named '{0}'", new object[] { src });
			}
			ColumnType columnType = input.Schema.GetColumnType(num);
			bool flag;
			Delegate @delegate;
			if (columnType.SameSizeAndItemType(typeSrc))
			{
				flag = true;
				@delegate = null;
			}
			else if (!Conversions.Instance.TryGetStandardConversion(columnType, typeSrc, out @delegate, out flag))
			{
				throw Contracts.ExceptParam(env, "mapper", "The type of column '{0}', '{1}', cannot be converted to the input type of the mapper '{2}'", new object[] { src, columnType, typeSrc });
			}
			LambdaColumnMapper.Column column = new LambdaColumnMapper.Column(src, dst);
			IDataView dataView;
			if (flag)
			{
				dataView = new LambdaColumnMapper.Impl<TSrc, TDst, TDst>(env, name, input, column, typeDst, mapper, null, keyValueGetter);
			}
			else
			{
				Func<IHostEnvironment, string, IDataView, LambdaColumnMapper.Column, ColumnType, ValueMapper<int, int>, ValueMapper<int, int>, ValueGetter<VBuffer<DvText>>, LambdaColumnMapper.Impl<int, int, int>> func = new Func<IHostEnvironment, string, IDataView, LambdaColumnMapper.Column, ColumnType, ValueMapper<int, int>, ValueMapper<int, int>, ValueGetter<VBuffer<DvText>>, LambdaColumnMapper.Impl<int, int, int>>(LambdaColumnMapper.CreateImpl<int, int, int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
				{
					columnType.RawType,
					typeof(TSrc),
					typeof(TDst)
				});
				dataView = (IDataView)methodInfo.Invoke(null, new object[] { env, name, input, column, typeDst, @delegate, mapper, keyValueGetter });
			}
			return new OpaqueDataView(dataView);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001A7A2 File Offset: 0x000189A2
		private static LambdaColumnMapper.Impl<T1, T2, T3> CreateImpl<T1, T2, T3>(IHostEnvironment env, string name, IDataView input, LambdaColumnMapper.Column col, ColumnType typeDst, ValueMapper<T1, T2> map1, ValueMapper<T2, T3> map2, ValueGetter<VBuffer<DvText>> keyValueGetter)
		{
			return new LambdaColumnMapper.Impl<T1, T2, T3>(env, name, input, col, typeDst, map1, map2, keyValueGetter);
		}

		// Token: 0x020000ED RID: 237
		private sealed class Column : OneToOneColumn
		{
			// Token: 0x060004D9 RID: 1241 RVA: 0x0001A7B5 File Offset: 0x000189B5
			public Column(string src, string dst)
			{
				this.name = dst;
				this.source = src;
			}
		}

		// Token: 0x020000EE RID: 238
		private sealed class Impl<T1, T2, T3> : OneToOneTransformBase
		{
			// Token: 0x060004DA RID: 1242 RVA: 0x0001A7E4 File Offset: 0x000189E4
			public Impl(IHostEnvironment env, string name, IDataView input, OneToOneColumn col, ColumnType typeDst, ValueMapper<T1, T2> map1, ValueMapper<T2, T3> map2 = null, ValueGetter<VBuffer<DvText>> keyValueGetter = null)
				: base(env, name, new OneToOneColumn[] { col }, input, (ColumnType x) => null)
			{
				this._typeDst = typeDst;
				this._map1 = map1;
				this._map2 = map2;
				if (keyValueGetter != null)
				{
					using (MetadataDispatcher.Builder builder = base.Metadata.BuildMetadata(0))
					{
						MetadataUtils.MetadataGetter<VBuffer<DvText>> metadataGetter = delegate(int c, ref VBuffer<DvText> dst)
						{
							keyValueGetter.Invoke(ref dst);
						};
						builder.AddGetter<VBuffer<DvText>>("KeyValues", new VectorType(TextType.Instance, this._typeDst.KeyCount), metadataGetter);
					}
				}
				base.Metadata.Seal();
			}

			// Token: 0x060004DB RID: 1243 RVA: 0x0001A8C0 File Offset: 0x00018AC0
			public override void Save(ModelSaveContext ctx)
			{
				throw Contracts.ExceptNotSupp(this._host, "Shouldn't serialize this");
			}

			// Token: 0x060004DC RID: 1244 RVA: 0x0001A8D2 File Offset: 0x00018AD2
			protected override ColumnType GetColumnTypeCore(int iinfo)
			{
				return this._typeDst;
			}

			// Token: 0x060004DD RID: 1245 RVA: 0x0001A968 File Offset: 0x00018B68
			protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
			{
				disposer = null;
				T2 v2;
				if (this._map2 == null)
				{
					ValueGetter<T1> getSrc2 = base.GetSrcGetter<T1>(input, 0);
					T1 v4 = default(T1);
					return new ValueGetter<T2>(delegate(ref T2 v2)
					{
						getSrc2.Invoke(ref v4);
						this._map1.Invoke(ref v4, ref v2);
					});
				}
				ValueGetter<T1> getSrc = base.GetSrcGetter<T1>(input, 0);
				T1 v1 = default(T1);
				v2 = default(T2);
				return new ValueGetter<T3>(delegate(ref T3 v3)
				{
					getSrc.Invoke(ref v1);
					this._map1.Invoke(ref v1, ref v2);
					this._map2.Invoke(ref v2, ref v3);
				});
			}

			// Token: 0x0400024A RID: 586
			private readonly ColumnType _typeDst;

			// Token: 0x0400024B RID: 587
			private readonly ValueMapper<T1, T2> _map1;

			// Token: 0x0400024C RID: 588
			private readonly ValueMapper<T2, T3> _map2;
		}
	}
}
