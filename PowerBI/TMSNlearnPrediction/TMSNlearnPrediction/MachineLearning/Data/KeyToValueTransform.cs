using System;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x0200002F RID: 47
	public sealed class KeyToValueTransform : OneToOneTransformBase
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000836A File Offset: 0x0000656A
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("KEY2VALT", 65537U, 65537U, 65537U, "KeyToValueTransform", null);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000838B File Offset: 0x0000658B
		public KeyToValueTransform(KeyToValueTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "KeyToValueTransform", Contracts.CheckRef<KeyToValueTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(KeyToValueTransform.TestIsKey))
		{
			this.ComputeKVMapsAndMetadata(out this._types, out this._kvMaps);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000083C8 File Offset: 0x000065C8
		private KeyToValueTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(KeyToValueTransform.TestIsKey))
		{
			this.ComputeKVMapsAndMetadata(out this._types, out this._kvMaps);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008414 File Offset: 0x00006614
		public static KeyToValueTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			Contracts.CheckValue<ModelLoadContext>(env, ctx, "ctx");
			ctx.CheckAtModel(KeyToValueTransform.GetVersionInfo());
			Contracts.CheckValue<IDataView>(env, input, "input");
			Contracts.CheckValue<IHostEnvironment>(env, env, "env");
			IHost h = env.Register("KeyToValueTransform");
			return HostExtensions.Apply<KeyToValueTransform>(h, "Loading Model", (IChannel ch) => new KeyToValueTransform(ctx, h, input));
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000084AB File Offset: 0x000066AB
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(KeyToValueTransform.GetVersionInfo());
			base.SaveBase(ctx);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000084D6 File Offset: 0x000066D6
		private static string TestIsKey(ColumnType type)
		{
			if (type.ItemType.KeyCount == 0)
			{
				return "Expected a key type with known cardinality";
			}
			return null;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000084EC File Offset: 0x000066EC
		private void ComputeKVMapsAndMetadata(out ColumnType[] types, out KeyToValueTransform.KeyToValueMap[] kvMaps)
		{
			MetadataDispatcher metadata = base.Metadata;
			types = new ColumnType[this.Infos.Length];
			kvMaps = new KeyToValueTransform.KeyToValueMap[this.Infos.Length];
			for (int i = 0; i < types.Length; i++)
			{
				using (metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source, "SlotNames"))
				{
				}
				ColumnType typeSrc = this.Infos[i].TypeSrc;
				ColumnType metadataTypeOrNull = this._input.Schema.GetMetadataTypeOrNull("KeyValues", this.Infos[i].Source);
				Contracts.Check(this._host, metadataTypeOrNull != null, "Metadata KeyValues does not exist");
				Contracts.Check(this._host, metadataTypeOrNull.VectorSize == typeSrc.ItemType.KeyCount, "KeyValues metadata size does not match column type key count");
				if (!typeSrc.IsVector)
				{
					types[i] = metadataTypeOrNull.ItemType;
				}
				else
				{
					types[i] = new VectorType(metadataTypeOrNull.ItemType.AsPrimitive, typeSrc.AsVector);
				}
				Func<int, ColumnType, ColumnType, KeyToValueTransform.KeyToValueMap> func = new Func<int, ColumnType, ColumnType, KeyToValueTransform.KeyToValueMap>(this.GetKeyMetadata<int, int>);
				MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
				{
					typeSrc.ItemType.RawType,
					types[i].ItemType.RawType
				});
				kvMaps[i] = (KeyToValueTransform.KeyToValueMap)methodInfo.Invoke(this, new object[] { i, typeSrc, metadataTypeOrNull });
			}
			metadata.Seal();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008694 File Offset: 0x00006894
		private KeyToValueTransform.KeyToValueMap GetKeyMetadata<K, V>(int iinfo, ColumnType typeKey, ColumnType typeVal)
		{
			VBuffer<V> vbuffer = default(VBuffer<V>);
			this._input.Schema.GetMetadata<VBuffer<V>>("KeyValues", this.Infos[iinfo].Source, ref vbuffer);
			Contracts.Check(this._host, vbuffer.Length == typeKey.ItemType.KeyCount);
			VBufferUtils.Densify<V>(ref vbuffer);
			return new KeyToValueTransform.KeyToValueMap<K, V>(this, typeKey.ItemType.AsKey, typeVal.ItemType.AsPrimitive, vbuffer.Values, iinfo);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008718 File Offset: 0x00006918
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._types[iinfo];
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000872F File Offset: 0x0000692F
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			return this._kvMaps[iinfo].GetMappingGetter(input);
		}

		// Token: 0x04000079 RID: 121
		public const string LoaderSignature = "KeyToValueTransform";

		// Token: 0x0400007A RID: 122
		private const string RegistrationName = "KeyToValueTransform";

		// Token: 0x0400007B RID: 123
		private readonly ColumnType[] _types;

		// Token: 0x0400007C RID: 124
		private KeyToValueTransform.KeyToValueMap[] _kvMaps;

		// Token: 0x02000030 RID: 48
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x0600011B RID: 283 RVA: 0x00008744 File Offset: 0x00006944
			public static KeyToValueTransform.Column Parse(string str)
			{
				KeyToValueTransform.Column column = new KeyToValueTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x0600011C RID: 284 RVA: 0x00008763 File Offset: 0x00006963
			public bool TryUnparse(StringBuilder sb)
			{
				return this.TryUnparseCore(sb);
			}
		}

		// Token: 0x02000031 RID: 49
		public sealed class Arguments
		{
			// Token: 0x0400007D RID: 125
			[Argument(4, HelpText = "New column definition(s) (optional form: name:src)", ShortName = "col", SortOrder = 1)]
			public KeyToValueTransform.Column[] column;
		}

		// Token: 0x02000032 RID: 50
		private abstract class KeyToValueMap
		{
			// Token: 0x0600011F RID: 287 RVA: 0x0000877C File Offset: 0x0000697C
			protected KeyToValueMap(KeyToValueTransform trans, PrimitiveType typeVal, int iinfo)
			{
				this._parent = trans;
				this._typeOutput = typeVal;
				this._iinfo = iinfo;
			}

			// Token: 0x06000120 RID: 288
			public abstract Delegate GetMappingGetter(IRow input);

			// Token: 0x0400007E RID: 126
			protected readonly PrimitiveType _typeOutput;

			// Token: 0x0400007F RID: 127
			protected readonly int _iinfo;

			// Token: 0x04000080 RID: 128
			protected readonly KeyToValueTransform _parent;
		}

		// Token: 0x02000033 RID: 51
		private class KeyToValueMap<K, V> : KeyToValueTransform.KeyToValueMap
		{
			// Token: 0x06000121 RID: 289 RVA: 0x0000879C File Offset: 0x0000699C
			public KeyToValueMap(KeyToValueTransform trans, KeyType typeKey, PrimitiveType typeVal, V[] values, int iinfo)
				: base(trans, typeVal, iinfo)
			{
				this._values = values;
				using (IChannel channel = this._parent._host.Start("Getting NA Predicate and Value"))
				{
					this._na = Conversions.Instance.GetNAOrDefault<V>(this._typeOutput.ItemType, out this._naMapsToDefault);
					if (this._naMapsToDefault)
					{
						this._isDefault = Conversions.Instance.GetIsDefaultPredicate<V>(this._typeOutput.ItemType);
						RefPredicate<V> refPredicate;
						if (!Conversions.Instance.TryGetIsNAPredicate<V>(this._typeOutput.ItemType, out refPredicate))
						{
							channel.Warning("There is no NA value for type '{0}'. The missing key value will be mapped to the default value of '{0}'", new object[] { this._typeOutput.ItemType });
						}
					}
				}
				bool flag;
				this._convertToUInt = Conversions.Instance.GetStandardConversion<K, uint>(typeKey, NumberType.U4, out flag);
			}

			// Token: 0x06000122 RID: 290 RVA: 0x00008884 File Offset: 0x00006A84
			private void MapKey(ref K src, ref V dst)
			{
				uint num = 0U;
				this._convertToUInt.Invoke(ref src, ref num);
				if (0U < num && (ulong)num <= (ulong)((long)this._values.Length))
				{
					dst = this._values[(int)((UIntPtr)(num - 1U))];
					return;
				}
				dst = this._na;
			}

			// Token: 0x06000123 RID: 291 RVA: 0x00008B00 File Offset: 0x00006D00
			public override Delegate GetMappingGetter(IRow input)
			{
				if (!this._parent._types[this._iinfo].IsVector)
				{
					K src2 = default(K);
					ValueGetter<K> getSrc2 = this._parent.GetSrcGetter<K>(input, this._iinfo);
					return new ValueGetter<V>(delegate(ref V dst)
					{
						getSrc2.Invoke(ref src2);
						this.MapKey(ref src2, ref dst);
					});
				}
				VBuffer<K> src = default(VBuffer<K>);
				V dstItem = default(V);
				int maxSize = (this._typeOutput.IsKnownSizeVector ? this._typeOutput.VectorSize : 2146435071);
				ValueGetter<VBuffer<K>> getSrc = this._parent.GetSrcGetter<VBuffer<K>>(input, this._iinfo);
				return new ValueGetter<VBuffer<V>>(delegate(ref VBuffer<V> dst)
				{
					getSrc.Invoke(ref src);
					int length = src.Length;
					int count = src.Count;
					K[] values = src.Values;
					V[] values2 = dst.Values;
					int[] indices = dst.Indices;
					int num = 0;
					if (src.IsDense)
					{
						Utils.EnsureSize<V>(ref values2, length, maxSize, false);
						for (int i = 0; i < length; i++)
						{
							this.MapKey(ref values[i], ref values2[i]);
						}
						num = length;
					}
					else if (!this._naMapsToDefault)
					{
						Utils.EnsureSize<V>(ref values2, length, maxSize, false);
						int[] indices2 = src.Indices;
						int num2 = ((src.Count == 0) ? length : indices2[0]);
						int num3 = 0;
						for (int j = 0; j < length; j++)
						{
							if (num2 == j)
							{
								this.MapKey(ref values[num3], ref values2[j]);
								num2 = ((++num3 == src.Count) ? length : indices2[num3]);
							}
							else
							{
								values2[j] = this._na;
							}
						}
						num = length;
					}
					else
					{
						Utils.EnsureSize<V>(ref values2, count, maxSize, false);
						Utils.EnsureSize<int>(ref indices, count, maxSize, false);
						int[] indices3 = src.Indices;
						for (int k = 0; k < count; k++)
						{
							this.MapKey(ref values[k], ref dstItem);
							if (!this._isDefault.Invoke(ref dstItem))
							{
								values2[num] = dstItem;
								indices[num++] = indices3[k];
							}
						}
					}
					dst = new VBuffer<V>(length, num, values2, indices);
				});
			}

			// Token: 0x04000081 RID: 129
			private readonly V[] _values;

			// Token: 0x04000082 RID: 130
			private readonly V _na;

			// Token: 0x04000083 RID: 131
			private readonly bool _naMapsToDefault;

			// Token: 0x04000084 RID: 132
			private readonly RefPredicate<V> _isDefault;

			// Token: 0x04000085 RID: 133
			private readonly ValueMapper<K, uint> _convertToUInt;
		}
	}
}
