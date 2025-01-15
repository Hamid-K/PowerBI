using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.CommandLine;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Data.IO;
using Microsoft.MachineLearning.Internal.CpuMath;
using Microsoft.MachineLearning.Internal.Utilities;
using Microsoft.MachineLearning.Model;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000037 RID: 55
	public sealed class NAReplaceTransform : OneToOneTransformBase
	{
		// Token: 0x06000137 RID: 311 RVA: 0x00009300 File Offset: 0x00007500
		private static VersionInfo GetVersionInfo()
		{
			return new VersionInfo("NAREP TF", 65538U, 65538U, 65537U, "NAReplaceTransform", null);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00009324 File Offset: 0x00007524
		private static string TestType(ColumnType type)
		{
			Func<ColumnType, string> func = new Func<ColumnType, string>(NAReplaceTransform.TestType<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.ItemType.RawType });
			return (string)methodInfo.Invoke(null, new object[] { type.ItemType });
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00009384 File Offset: 0x00007584
		private static string TestType<T>(ColumnType type)
		{
			RefPredicate<T> refPredicate;
			if (!Conversions.Instance.TryGetIsNAPredicate<T>(type.ItemType, out refPredicate))
			{
				return string.Format("Type '{0}' is not supported by {1} since it doesn't have an NA value", type, "NAReplaceTransform");
			}
			T t = default(T);
			if (refPredicate.Invoke(ref t))
			{
				return string.Format("Type '{0}' is not supported by {1} since its NA value is equivalent to its default value", type, "NAReplaceTransform");
			}
			return null;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000093DC File Offset: 0x000075DC
		public NAReplaceTransform(NAReplaceTransform.Arguments args, IHostEnvironment env, IDataView input)
			: base(env, "NAReplaceTransform", Contracts.CheckRef<NAReplaceTransform.Arguments>(args, "args").column, input, new Func<ColumnType, string>(NAReplaceTransform.TestType))
		{
			Contracts.CheckValue<NAReplaceTransform.Arguments>(this._host, args, "args");
			this.GetInfoAndMetadata(out this._types, out this._isNAs);
			this.GetReplacementValues(args, out this._repValues, out this._repIsDefault);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00009448 File Offset: 0x00007648
		private NAReplaceTransform(ModelLoadContext ctx, IHost host, IDataView input)
			: base(ctx, host, input, new Func<ColumnType, string>(NAReplaceTransform.TestType))
		{
			this.GetInfoAndMetadata(out this._types, out this._isNAs);
			this._repValues = new object[this.Infos.Length];
			this._repIsDefault = new BitArray[this.Infos.Length];
			BinarySaver binarySaver = new BinarySaver(new BinarySaver.Arguments(), this._host);
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ColumnType columnType;
				object obj;
				if (!binarySaver.TryLoadTypeAndValue(ctx.Reader.BaseStream, out columnType, out obj))
				{
					throw Contracts.ExceptDecode(this._host, "Unrecognized codec read");
				}
				if (!this._types[i].ItemType.Equals(columnType.ItemType))
				{
					throw Contracts.ExceptDecode(this._host, "Decoded serialization of type '{0}' does not match expected ColumnType of '{1}'", new object[]
					{
						columnType.ItemType,
						this._types[i].ItemType
					});
				}
				if (columnType.IsVector)
				{
					if (!this._types[i].IsVector)
					{
						throw Contracts.ExceptDecode(this._host, "Decoded serialization of type '{0}' cannot be a vector when Columntype is a scalar of type '{1}'", new object[]
						{
							columnType,
							this._types[i]
						});
					}
					if (!this._types[i].IsKnownSizeVector)
					{
						throw Contracts.ExceptDecode(this._host, "Decoded serialization for unknown size vector '{0}' must be a scalar instead of type '{1}'", new object[]
						{
							this._types[i],
							columnType
						});
					}
					if (this._types[i].VectorSize != columnType.VectorSize)
					{
						throw Contracts.ExceptDecode(this._host, "Decoded serialization of type '{0}' must be a scalar or a vector of the same size as Columntype '{1}'", new object[]
						{
							columnType,
							this._types[i]
						});
					}
					object[] array = new object[]
					{
						obj,
						this._types[i],
						i
					};
					Func<VBuffer<int>, ColumnType, int, int[]> func = new Func<VBuffer<int>, ColumnType, int, int[]>(this.GetValuesArray<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.ItemType.RawType });
					this._repValues[i] = methodInfo.Invoke(this, array);
				}
				else
				{
					this._repValues[i] = obj;
				}
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00009684 File Offset: 0x00007884
		private T[] GetValuesArray<T>(VBuffer<T> src, ColumnType srcType, int iinfo)
		{
			VBufferUtils.Densify<T>(ref src);
			RefPredicate<T> isDefaultPredicate = Conversions.Instance.GetIsDefaultPredicate<T>(srcType.ItemType);
			this._repIsDefault[iinfo] = new BitArray(srcType.VectorSize);
			for (int i = 0; i < src.Length; i++)
			{
				if (isDefaultPredicate.Invoke(ref src.Values[i]))
				{
					this._repIsDefault[iinfo][i] = true;
				}
			}
			T[] values = src.Values;
			Array.Resize<T>(ref values, srcType.VectorSize);
			return values;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000972C File Offset: 0x0000792C
		public static NAReplaceTransform Create(ModelLoadContext ctx, IHostEnvironment env, IDataView input)
		{
			Contracts.CheckValue<IHostEnvironment>(env, "env");
			IHost h = env.Register("NAReplaceTransform");
			Contracts.CheckValue<ModelLoadContext>(h, ctx, "ctx");
			Contracts.CheckValue<IDataView>(h, input, "input");
			ctx.CheckAtModel(NAReplaceTransform.GetVersionInfo());
			return HostExtensions.Apply<NAReplaceTransform>(h, "Loading Model", (IChannel ch) => new NAReplaceTransform(ctx, h, input));
		}

		// Token: 0x0600013E RID: 318 RVA: 0x000097C4 File Offset: 0x000079C4
		public override void Save(ModelSaveContext ctx)
		{
			Contracts.CheckValue<ModelSaveContext>(this._host, ctx, "ctx");
			ctx.CheckAtModel();
			ctx.SetVersionInfo(NAReplaceTransform.GetVersionInfo());
			base.SaveBase(ctx);
			BinarySaver binarySaver = new BinarySaver(new BinarySaver.Arguments(), this._host);
			for (int i = 0; i < this._types.Length; i++)
			{
				object obj = this._repValues[i];
				ColumnType columnType = this._types[i].ItemType;
				if (this._repIsDefault[i] != null)
				{
					Func<int[], VBuffer<int>> func = new Func<int[], VBuffer<int>>(this.CreateVBuffer<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this._types[i].ItemType.RawType });
					obj = methodInfo.Invoke(this, new object[] { this._repValues[i] });
					columnType = this._types[i];
				}
				object[] array = new object[]
				{
					ctx.Writer.BaseStream,
					binarySaver,
					columnType,
					obj
				};
				Action<Stream, BinarySaver, ColumnType, int> action = new Action<Stream, BinarySaver, ColumnType, int>(this.WriteTypeAndValue<int>);
				MethodInfo methodInfo2 = action.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { obj.GetType() });
				methodInfo2.Invoke(this, array);
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00009918 File Offset: 0x00007B18
		private VBuffer<T> CreateVBuffer<T>(T[] array)
		{
			return new VBuffer<T>(array.Length, array, null);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00009924 File Offset: 0x00007B24
		private void WriteTypeAndValue<T>(Stream stream, BinarySaver saver, ColumnType type, T rep)
		{
			int num;
			if (!saver.TryWriteTypeAndValue<T>(stream, type, ref rep, out num))
			{
				throw Contracts.Except(this._host, "We do not know how to serialize terms of type '{0}'", new object[] { type });
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000995C File Offset: 0x00007B5C
		private void GetReplacementValues(NAReplaceTransform.Arguments args, out object[] repValues, out BitArray[] slotIsDefault)
		{
			repValues = new object[this.Infos.Length];
			slotIsDefault = new BitArray[this.Infos.Length];
			NAReplaceTransform.ReplacementKind?[] array = new NAReplaceTransform.ReplacementKind?[this.Infos.Length];
			List<int> list = null;
			HashSet<int> hashSet = null;
			for (int i = 0; i < this.Infos.Length; i++)
			{
				NAReplaceTransform.ReplacementKind replacementKind = args.column[i].kind ?? args.kind;
				switch (replacementKind)
				{
				case NAReplaceTransform.ReplacementKind.SpecifiedValue:
					repValues[i] = this.GetSpecifiedValue(args.column[i].replacementString, this._types[i], this._isNAs[i]);
					break;
				case NAReplaceTransform.ReplacementKind.DefaultValue:
					repValues[i] = this.GetDefault(this._types[i]);
					break;
				case NAReplaceTransform.ReplacementKind.Mean:
				case NAReplaceTransform.ReplacementKind.Minimum:
				case NAReplaceTransform.ReplacementKind.Maximum:
					if (!this._types[i].ItemType.IsNumber && !this._types[i].ItemType.IsTimeSpan && !this._types[i].ItemType.IsDateTime)
					{
						throw Contracts.Except(this._host, "Cannot perform mean imputations on non-numeric '{0}'", new object[] { this._types[i].ItemType });
					}
					array[i] = new NAReplaceTransform.ReplacementKind?(replacementKind);
					Utils.Add<int>(ref list, i);
					Utils.Add<int>(ref hashSet, this.Infos[i].Source);
					break;
				default:
					throw Contracts.Except(this._host, "Internal error, undefined ReplacementKind '{0}' assigned in NAReplaceTransform.", new object[] { replacementKind });
				}
			}
			if (Utils.Size<int>(list) == 0)
			{
				return;
			}
			using (IChannel channel = this._host.Start("Computing Statistics"))
			{
				using (IRowCursor rowCursor = this._input.GetRowCursor(new Func<int, bool>(hashSet.Contains), null))
				{
					NAReplaceTransform.StatAggregator[] array2 = new NAReplaceTransform.StatAggregator[list.Count];
					for (int j = 0; j < list.Count; j++)
					{
						int num = list[j];
						bool flag = args.column[j].slot ?? args.slot ?? this._types[num].IsKnownSizeVector;
						array2[j] = NAReplaceTransform.CreateStatAggregator(channel, this._types[num], array[num], flag, rowCursor, this.Infos[num].Source);
					}
					while (rowCursor.MoveNext())
					{
						for (int k = 0; k < array2.Length; k++)
						{
							array2[k].ProcessRow();
						}
					}
					for (int l = 0; l < array2.Length; l++)
					{
						repValues[list[l]] = array2[l].GetStat();
					}
					channel.Done();
				}
			}
			for (int m = 0; m < list.Count; m++)
			{
				int num2 = list[m];
				if (repValues[num2] is Array)
				{
					Func<ColumnType, int[], BitArray> func = new Func<ColumnType, int[], BitArray>(this.ComputeDefaultSlots<int>);
					MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this._types[num2].ItemType.RawType });
					slotIsDefault[num2] = (BitArray)methodInfo.Invoke(this, new object[]
					{
						this._types[num2],
						repValues[num2]
					});
				}
			}
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00009D38 File Offset: 0x00007F38
		private BitArray ComputeDefaultSlots<T>(ColumnType type, T[] values)
		{
			BitArray bitArray = new BitArray(values.Length);
			RefPredicate<T> isDefaultPredicate = Conversions.Instance.GetIsDefaultPredicate<T>(type.ItemType);
			for (int i = 0; i < values.Length; i++)
			{
				if (isDefaultPredicate.Invoke(ref values[i]))
				{
					bitArray[i] = true;
				}
			}
			return bitArray;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00009D88 File Offset: 0x00007F88
		private void GetInfoAndMetadata(out ColumnType[] types, out Delegate[] isNAs)
		{
			MetadataDispatcher metadata = base.Metadata;
			types = new ColumnType[this.Infos.Length];
			isNAs = new Delegate[this.Infos.Length];
			for (int i = 0; i < this.Infos.Length; i++)
			{
				ColumnType typeSrc = this.Infos[i].TypeSrc;
				if (!typeSrc.IsVector)
				{
					types[i] = typeSrc;
				}
				else
				{
					types[i] = new VectorType(typeSrc.ItemType.AsPrimitive, typeSrc.AsVector);
				}
				isNAs[i] = this.GetIsNADelegate(typeSrc);
				using (metadata.BuildMetadata(i, this._input.Schema, this.Infos[i].Source, new string[] { "SlotNames", "IsNormalized" }))
				{
				}
			}
			metadata.Seal();
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00009E74 File Offset: 0x00008074
		private object GetDefault(ColumnType type)
		{
			Func<object> func = new Func<object>(this.GetDefault<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { type.ItemType.RawType });
			return methodInfo.Invoke(this, null);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00009EC0 File Offset: 0x000080C0
		private object GetDefault<T>()
		{
			return default(T);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00009EDC File Offset: 0x000080DC
		private Delegate GetIsNADelegate(ColumnType type)
		{
			Func<ColumnType, Delegate> func = new Func<ColumnType, Delegate>(this.GetIsNADelegate<int>);
			return Utils.MarshalInvoke<ColumnType, Delegate>(func, type.ItemType.RawType, type);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00009F08 File Offset: 0x00008108
		private Delegate GetIsNADelegate<T>(ColumnType type)
		{
			return Conversions.Instance.GetIsNAPredicate<T>(type.ItemType);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00009F1A File Offset: 0x0000811A
		protected override ColumnType GetColumnTypeCore(int iinfo)
		{
			return this._types[iinfo];
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00009F24 File Offset: 0x00008124
		private object GetSpecifiedValue(string srcStr, ColumnType dstType, Delegate isNA)
		{
			Func<string, ColumnType, RefPredicate<int>, object> func = new Func<string, ColumnType, RefPredicate<int>, object>(this.GetSpecifiedValue<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { dstType.ItemType.RawType });
			return methodInfo.Invoke(this, new object[] { srcStr, dstType, isNA });
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00009F80 File Offset: 0x00008180
		private object GetSpecifiedValue<T>(string srcStr, ColumnType dstType, RefPredicate<T> isNA)
		{
			T t = default(T);
			if (!string.IsNullOrEmpty(srcStr))
			{
				DvText dvText = new DvText(srcStr);
				bool flag;
				ValueMapper<DvText, T> standardConversion = Conversions.Instance.GetStandardConversion<DvText, T>(TextType.Instance, dstType.ItemType, out flag);
				standardConversion.Invoke(ref dvText, ref t);
				if (isNA.Invoke(ref t))
				{
					throw Contracts.Except("No conversion of '{0}' to '{1}'", new object[] { srcStr, dstType.ItemType });
				}
			}
			return t;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000A000 File Offset: 0x00008200
		protected override Delegate GetGetterCore(IChannel ch, IRow input, int iinfo, out Action disposer)
		{
			disposer = null;
			if (!this.Infos[iinfo].TypeSrc.IsVector)
			{
				return this.ComposeGetterOne(input, iinfo);
			}
			return this.ComposeGetterVec(input, iinfo);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000A02C File Offset: 0x0000822C
		private Delegate ComposeGetterOne(IRow input, int iinfo)
		{
			Func<IRow, int, ValueGetter<int>> func = new Func<IRow, int, ValueGetter<int>>(this.ComposeGetterOne<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this.Infos[iinfo].TypeSrc.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[] { input, iinfo });
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000A0D8 File Offset: 0x000082D8
		private ValueGetter<T> ComposeGetterOne<T>(IRow input, int iinfo)
		{
			ValueGetter<T> getSrc = base.GetSrcGetter<T>(input, iinfo);
			T src = default(T);
			RefPredicate<T> isNA = (RefPredicate<T>)this._isNAs[iinfo];
			T rep = (T)((object)this._repValues[iinfo]);
			return delegate(ref T dst)
			{
				getSrc.Invoke(ref src);
				dst = (isNA.Invoke(ref src) ? rep : src);
			};
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000A138 File Offset: 0x00008338
		private Delegate ComposeGetterVec(IRow input, int iinfo)
		{
			Func<IRow, int, ValueGetter<VBuffer<int>>> func = new Func<IRow, int, ValueGetter<VBuffer<int>>>(this.ComposeGetterVec<int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { this.Infos[iinfo].TypeSrc.ItemType.RawType });
			return (Delegate)methodInfo.Invoke(this, new object[] { input, iinfo });
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000A288 File Offset: 0x00008488
		private ValueGetter<VBuffer<T>> ComposeGetterVec<T>(IRow input, int iinfo)
		{
			ValueGetter<VBuffer<T>> getSrc = base.GetSrcGetter<VBuffer<T>>(input, iinfo);
			RefPredicate<T> isNA = (RefPredicate<T>)this._isNAs[iinfo];
			RefPredicate<T> isDefaultPredicate = Conversions.Instance.GetIsDefaultPredicate<T>(input.Schema.GetColumnType(this.Infos[iinfo].Source).ItemType);
			VBuffer<T> src = default(VBuffer<T>);
			if (this._repIsDefault[iinfo] == null)
			{
				T rep = (T)((object)this._repValues[iinfo]);
				bool repIsDefault = isDefaultPredicate.Invoke(ref rep);
				return delegate(ref VBuffer<T> dst)
				{
					getSrc.Invoke(ref src);
					this.FillValues<T>(ref src, ref dst, isNA, rep, repIsDefault);
				};
			}
			T[] repArray = (T[])this._repValues[iinfo];
			return delegate(ref VBuffer<T> dst)
			{
				getSrc.Invoke(ref src);
				Contracts.Check(this._host, src.Length == repArray.Length);
				this.FillValues<T>(ref src, ref dst, isNA, repArray, this._repIsDefault[iinfo]);
			};
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000A384 File Offset: 0x00008584
		private void FillValues<T>(ref VBuffer<T> src, ref VBuffer<T> dst, RefPredicate<T> isNA, T rep, bool repIsDefault)
		{
			int length = src.Length;
			int count = src.Count;
			T[] values = src.Values;
			int[] indices = src.Indices;
			T[] values2 = dst.Values;
			int[] indices2 = dst.Indices;
			Utils.EnsureSize<T>(ref values2, count, false);
			int num = 0;
			if (src.IsDense)
			{
				for (int i = 0; i < count; i++)
				{
					T t = values[i];
					values2[i] = (isNA.Invoke(ref t) ? rep : t);
				}
				num = count;
			}
			else
			{
				Utils.EnsureSize<int>(ref indices2, count, false);
				for (int j = 0; j < count; j++)
				{
					T t2 = values[j];
					int num2 = indices[j];
					if (!isNA.Invoke(ref t2))
					{
						values2[num] = t2;
						indices2[num++] = num2;
					}
					else if (!repIsDefault)
					{
						values2[num] = rep;
						indices2[num++] = num2;
					}
				}
			}
			dst = new VBuffer<T>(length, num, values2, indices2);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000A484 File Offset: 0x00008684
		private void FillValues<T>(ref VBuffer<T> src, ref VBuffer<T> dst, RefPredicate<T> isNA, T[] rep, BitArray repIsDefault)
		{
			int length = src.Length;
			int count = src.Count;
			T[] values = src.Values;
			int[] indices = src.Indices;
			T[] values2 = dst.Values;
			int[] indices2 = dst.Indices;
			Utils.EnsureSize<T>(ref values2, count, length, false);
			int num = 0;
			if (src.IsDense)
			{
				for (int i = 0; i < count; i++)
				{
					T t = values[i];
					values2[i] = (isNA.Invoke(ref t) ? rep[i] : t);
				}
				num = count;
			}
			else
			{
				Utils.EnsureSize<int>(ref indices2, count, length, false);
				for (int j = 0; j < count; j++)
				{
					T t2 = values[j];
					int num2 = indices[j];
					if (!isNA.Invoke(ref t2))
					{
						values2[num] = t2;
						indices2[num++] = num2;
					}
					else if (!repIsDefault[num2])
					{
						values2[num] = rep[num2];
						indices2[num++] = num2;
					}
				}
			}
			dst = new VBuffer<T>(length, num, values2, indices2);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000A59C File Offset: 0x0000879C
		private static NAReplaceTransform.StatAggregator CreateStatAggregator(IChannel ch, ColumnType type, NAReplaceTransform.ReplacementKind? kind, bool bySlot, IRowCursor cursor, int col)
		{
			if (!type.IsVector)
			{
				if (kind == NAReplaceTransform.ReplacementKind.Mean)
				{
					switch (type.RawKind)
					{
					case 1:
						return new NAReplaceTransform.I1.MeanAggregatorOne(ch, cursor, col);
					case 3:
						return new NAReplaceTransform.I2.MeanAggregatorOne(ch, cursor, col);
					case 5:
						return new NAReplaceTransform.I4.MeanAggregatorOne(ch, cursor, col);
					case 7:
						return new NAReplaceTransform.Long.MeanAggregatorOne<DvInt8>(ch, type, cursor, col);
					case 9:
						return new NAReplaceTransform.R4.MeanAggregatorOne(ch, cursor, col);
					case 10:
						return new NAReplaceTransform.R8.MeanAggregatorOne(ch, cursor, col);
					case 13:
						return new NAReplaceTransform.Long.MeanAggregatorOne<DvTimeSpan>(ch, type, cursor, col);
					case 14:
						return new NAReplaceTransform.Long.MeanAggregatorOne<DvDateTime>(ch, type, cursor, col);
					}
				}
				if (kind == NAReplaceTransform.ReplacementKind.Minimum || kind == NAReplaceTransform.ReplacementKind.Maximum)
				{
					switch (type.RawKind)
					{
					case 1:
						return new NAReplaceTransform.I1.MinMaxAggregatorOne(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 3:
						return new NAReplaceTransform.I2.MinMaxAggregatorOne(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 5:
						return new NAReplaceTransform.I4.MinMaxAggregatorOne(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 7:
						return new NAReplaceTransform.Long.MinMaxAggregatorOne<DvInt8>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 9:
						return new NAReplaceTransform.R4.MinMaxAggregatorOne(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 10:
						return new NAReplaceTransform.R8.MinMaxAggregatorOne(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 13:
						return new NAReplaceTransform.Long.MinMaxAggregatorOne<DvTimeSpan>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 14:
						return new NAReplaceTransform.Long.MinMaxAggregatorOne<DvDateTime>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					}
				}
			}
			else if (bySlot)
			{
				if (type.ValueCount == 0)
				{
					throw Contracts.Except(ch, "Imputation by slot is not allowed for vectors of unknown size.");
				}
				if (kind == NAReplaceTransform.ReplacementKind.Mean)
				{
					switch (type.ItemType.RawKind)
					{
					case 1:
						return new NAReplaceTransform.I1.MeanAggregatorBySlot(ch, type, cursor, col);
					case 3:
						return new NAReplaceTransform.I2.MeanAggregatorBySlot(ch, type, cursor, col);
					case 5:
						return new NAReplaceTransform.I4.MeanAggregatorBySlot(ch, type, cursor, col);
					case 7:
						return new NAReplaceTransform.Long.MeanAggregatorBySlot<DvInt8>(ch, type, cursor, col);
					case 9:
						return new NAReplaceTransform.R4.MeanAggregatorBySlot(ch, type, cursor, col);
					case 10:
						return new NAReplaceTransform.R8.MeanAggregatorBySlot(ch, type, cursor, col);
					case 13:
						return new NAReplaceTransform.Long.MeanAggregatorBySlot<DvTimeSpan>(ch, type, cursor, col);
					case 14:
						return new NAReplaceTransform.Long.MeanAggregatorBySlot<DvDateTime>(ch, type, cursor, col);
					}
				}
				else if (kind == NAReplaceTransform.ReplacementKind.Minimum || kind == NAReplaceTransform.ReplacementKind.Maximum)
				{
					switch (type.ItemType.RawKind)
					{
					case 1:
						return new NAReplaceTransform.I1.MinMaxAggregatorBySlot(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 3:
						return new NAReplaceTransform.I2.MinMaxAggregatorBySlot(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 5:
						return new NAReplaceTransform.I4.MinMaxAggregatorBySlot(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 7:
						return new NAReplaceTransform.Long.MinMaxAggregatorBySlot<DvInt8>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 9:
						return new NAReplaceTransform.R4.MinMaxAggregatorBySlot(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 10:
						return new NAReplaceTransform.R8.MinMaxAggregatorBySlot(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 13:
						return new NAReplaceTransform.Long.MinMaxAggregatorBySlot<DvTimeSpan>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					case 14:
						return new NAReplaceTransform.Long.MinMaxAggregatorBySlot<DvDateTime>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
					}
				}
			}
			else if (kind == NAReplaceTransform.ReplacementKind.Mean)
			{
				switch (type.ItemType.RawKind)
				{
				case 1:
					return new NAReplaceTransform.I1.MeanAggregatorAcrossSlots(ch, cursor, col);
				case 3:
					return new NAReplaceTransform.I2.MeanAggregatorAcrossSlots(ch, cursor, col);
				case 5:
					return new NAReplaceTransform.I4.MeanAggregatorAcrossSlots(ch, cursor, col);
				case 7:
					return new NAReplaceTransform.Long.MeanAggregatorAcrossSlots<DvInt8>(ch, type, cursor, col);
				case 9:
					return new NAReplaceTransform.R4.MeanAggregatorAcrossSlots(ch, cursor, col);
				case 10:
					return new NAReplaceTransform.R8.MeanAggregatorAcrossSlots(ch, cursor, col);
				case 13:
					return new NAReplaceTransform.Long.MeanAggregatorAcrossSlots<DvTimeSpan>(ch, type, cursor, col);
				case 14:
					return new NAReplaceTransform.Long.MeanAggregatorAcrossSlots<DvDateTime>(ch, type, cursor, col);
				}
			}
			else if (kind == NAReplaceTransform.ReplacementKind.Minimum || kind == NAReplaceTransform.ReplacementKind.Maximum)
			{
				switch (type.ItemType.RawKind)
				{
				case 1:
					return new NAReplaceTransform.I1.MinMaxAggregatorAcrossSlots(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				case 3:
					return new NAReplaceTransform.I2.MinMaxAggregatorAcrossSlots(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				case 5:
					return new NAReplaceTransform.I4.MinMaxAggregatorAcrossSlots(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				case 7:
					return new NAReplaceTransform.Long.MinMaxAggregatorAcrossSlots<DvInt8>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				case 9:
					return new NAReplaceTransform.R4.MinMaxAggregatorAcrossSlots(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				case 10:
					return new NAReplaceTransform.R8.MinMaxAggregatorAcrossSlots(ch, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				case 13:
					return new NAReplaceTransform.Long.MinMaxAggregatorAcrossSlots<DvTimeSpan>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				case 14:
					return new NAReplaceTransform.Long.MinMaxAggregatorAcrossSlots<DvDateTime>(ch, type, cursor, col, kind == NAReplaceTransform.ReplacementKind.Maximum);
				}
			}
			throw Contracts.Except(ch, "Internal error, unrecognized imputation method ReplacementKind '{0}' or unrecognized type '{1}' assigned in NAReplaceTransform.", new object[] { kind, type });
		}

		// Token: 0x0400008B RID: 139
		public const string LoadName = "NAReplaceTransform";

		// Token: 0x0400008C RID: 140
		private readonly ColumnType[] _types;

		// Token: 0x0400008D RID: 141
		private readonly object[] _repValues;

		// Token: 0x0400008E RID: 142
		private BitArray[] _repIsDefault;

		// Token: 0x0400008F RID: 143
		private readonly Delegate[] _isNAs;

		// Token: 0x02000038 RID: 56
		public enum ReplacementKind
		{
			// Token: 0x04000091 RID: 145
			SpecifiedValue,
			// Token: 0x04000092 RID: 146
			DefaultValue,
			// Token: 0x04000093 RID: 147
			Mean,
			// Token: 0x04000094 RID: 148
			Minimum,
			// Token: 0x04000095 RID: 149
			Maximum,
			// Token: 0x04000096 RID: 150
			[HideEnumValue]
			Val = 0,
			// Token: 0x04000097 RID: 151
			[HideEnumValue]
			Value = 0,
			// Token: 0x04000098 RID: 152
			[HideEnumValue]
			Def,
			// Token: 0x04000099 RID: 153
			[HideEnumValue]
			Default = 1,
			// Token: 0x0400009A RID: 154
			[HideEnumValue]
			Min = 3,
			// Token: 0x0400009B RID: 155
			[HideEnumValue]
			Max
		}

		// Token: 0x02000039 RID: 57
		public sealed class Column : OneToOneColumn
		{
			// Token: 0x06000153 RID: 339 RVA: 0x0000AD1C File Offset: 0x00008F1C
			public static NAReplaceTransform.Column Parse(string str)
			{
				NAReplaceTransform.Column column = new NAReplaceTransform.Column();
				if (column.TryParse(str))
				{
					return column;
				}
				return null;
			}

			// Token: 0x06000154 RID: 340 RVA: 0x0000AD3B File Offset: 0x00008F3B
			protected override bool TryParse(string str)
			{
				return base.TryParse(str, out this.replacementString);
			}

			// Token: 0x06000155 RID: 341 RVA: 0x0000AD4A File Offset: 0x00008F4A
			public bool TryUnparse(StringBuilder sb)
			{
				if (this.kind != null || this.slot != null)
				{
					return false;
				}
				if (this.replacementString == null)
				{
					return this.TryUnparseCore(sb);
				}
				return this.TryUnparseCore(sb, this.replacementString);
			}

			// Token: 0x0400009C RID: 156
			[Argument(0, HelpText = "Replacement value for NAs (uses default value if not given)", ShortName = "rep")]
			public string replacementString;

			// Token: 0x0400009D RID: 157
			[Argument(0, HelpText = "The replacement method to utilize")]
			public NAReplaceTransform.ReplacementKind? kind;

			// Token: 0x0400009E RID: 158
			[Argument(0, HelpText = "Whether to impute values by slot")]
			public bool? slot;
		}

		// Token: 0x0200003A RID: 58
		public sealed class Arguments
		{
			// Token: 0x0400009F RID: 159
			[Argument(4, HelpText = "New column definition(s) (optional form: name:rep:src)", ShortName = "col", SortOrder = 1)]
			public NAReplaceTransform.Column[] column;

			// Token: 0x040000A0 RID: 160
			[Argument(0, HelpText = "The replacement method to utilize")]
			public NAReplaceTransform.ReplacementKind kind;

			// Token: 0x040000A1 RID: 161
			[Argument(0, HelpText = "Whether to impute values by slot")]
			public bool? slot;
		}

		// Token: 0x0200003B RID: 59
		private abstract class StatAggregator
		{
			// Token: 0x06000158 RID: 344 RVA: 0x0000AD95 File Offset: 0x00008F95
			protected StatAggregator(IChannel ch)
			{
				this._ch = ch;
			}

			// Token: 0x06000159 RID: 345
			public abstract void ProcessRow();

			// Token: 0x0600015A RID: 346
			public abstract object GetStat();

			// Token: 0x040000A2 RID: 162
			protected readonly IChannel _ch;
		}

		// Token: 0x0200003C RID: 60
		private abstract class StatAggregator<TValue, TStat> : NAReplaceTransform.StatAggregator
		{
			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600015B RID: 347 RVA: 0x0000ADA4 File Offset: 0x00008FA4
			public long RowCount
			{
				get
				{
					return this._rowCount;
				}
			}

			// Token: 0x0600015C RID: 348 RVA: 0x0000ADAC File Offset: 0x00008FAC
			protected StatAggregator(IChannel ch, IRowCursor cursor, int col)
				: base(ch)
			{
				this._getter = cursor.GetGetter<TValue>(col);
			}

			// Token: 0x0600015D RID: 349 RVA: 0x0000ADC2 File Offset: 0x00008FC2
			public sealed override void ProcessRow()
			{
				this._rowCount += 1L;
				this._getter.Invoke(ref this._val);
				this.ProcessRow(ref this._val);
			}

			// Token: 0x0600015E RID: 350
			protected abstract void ProcessRow(ref TValue val);

			// Token: 0x040000A3 RID: 163
			private readonly ValueGetter<TValue> _getter;

			// Token: 0x040000A4 RID: 164
			private TValue _val;

			// Token: 0x040000A5 RID: 165
			private long _rowCount;

			// Token: 0x040000A6 RID: 166
			protected TStat _stat;
		}

		// Token: 0x0200003D RID: 61
		private abstract class StatAggregatorAcrossSlots<TItem, TStat> : NAReplaceTransform.StatAggregator<VBuffer<TItem>, TStat>
		{
			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600015F RID: 351 RVA: 0x0000ADF0 File Offset: 0x00008FF0
			public UInt128 ValueCount
			{
				get
				{
					return this._valueCount;
				}
			}

			// Token: 0x06000160 RID: 352 RVA: 0x0000ADF8 File Offset: 0x00008FF8
			protected StatAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col)
				: base(ch, cursor, col)
			{
			}

			// Token: 0x06000161 RID: 353 RVA: 0x0000AE04 File Offset: 0x00009004
			protected sealed override void ProcessRow(ref VBuffer<TItem> src)
			{
				int count = src.Count;
				TItem[] values = src.Values;
				for (int i = 0; i < count; i++)
				{
					this.ProcessValue(ref values[i]);
				}
				this._valueCount += (ulong)((long)src.Length);
			}

			// Token: 0x06000162 RID: 354
			protected abstract void ProcessValue(ref TItem val);

			// Token: 0x040000A7 RID: 167
			private UInt128 _valueCount;
		}

		// Token: 0x0200003E RID: 62
		private abstract class StatAggregatorBySlot<TItem, TStatItem> : NAReplaceTransform.StatAggregator<VBuffer<TItem>, TStatItem[]>
		{
			// Token: 0x06000163 RID: 355 RVA: 0x0000AE50 File Offset: 0x00009050
			protected StatAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col)
				: base(ch, cursor, col)
			{
				this._stat = new TStatItem[type.VectorSize];
			}

			// Token: 0x06000164 RID: 356 RVA: 0x0000AE70 File Offset: 0x00009070
			protected sealed override void ProcessRow(ref VBuffer<TItem> src)
			{
				int count = src.Count;
				TItem[] values = src.Values;
				if (src.IsDense)
				{
					for (int i = 0; i < count; i++)
					{
						this.ProcessValue(ref values[i], i);
					}
					return;
				}
				int[] indices = src.Indices;
				for (int j = 0; j < count; j++)
				{
					this.ProcessValue(ref values[j], indices[j]);
				}
			}

			// Token: 0x06000165 RID: 357
			protected abstract void ProcessValue(ref TItem val, int slot);
		}

		// Token: 0x0200003F RID: 63
		private abstract class MinMaxAggregatorOne<TValue, TStat> : NAReplaceTransform.StatAggregator<TValue, TStat>
		{
			// Token: 0x06000166 RID: 358 RVA: 0x0000AED8 File Offset: 0x000090D8
			protected MinMaxAggregatorOne(IChannel ch, IRowCursor cursor, int col, bool returnMax)
				: base(ch, cursor, col)
			{
				this._returnMax = returnMax;
				if (this._returnMax)
				{
					this._processValueDelegate = new NAReplaceTransform.MinMaxAggregatorOne<TValue, TStat>.ProcessValueDelegate(this.ProcessValueMax);
					return;
				}
				this._processValueDelegate = new NAReplaceTransform.MinMaxAggregatorOne<TValue, TStat>.ProcessValueDelegate(this.ProcessValueMin);
			}

			// Token: 0x06000167 RID: 359 RVA: 0x0000AF25 File Offset: 0x00009125
			protected override void ProcessRow(ref TValue val)
			{
				this._processValueDelegate(ref val);
			}

			// Token: 0x06000168 RID: 360 RVA: 0x0000AF33 File Offset: 0x00009133
			public override object GetStat()
			{
				return this._stat;
			}

			// Token: 0x06000169 RID: 361
			protected abstract void ProcessValueMin(ref TValue val);

			// Token: 0x0600016A RID: 362
			protected abstract void ProcessValueMax(ref TValue val);

			// Token: 0x040000A8 RID: 168
			protected readonly bool _returnMax;

			// Token: 0x040000A9 RID: 169
			private readonly NAReplaceTransform.MinMaxAggregatorOne<TValue, TStat>.ProcessValueDelegate _processValueDelegate;

			// Token: 0x02000040 RID: 64
			// (Invoke) Token: 0x0600016C RID: 364
			private delegate void ProcessValueDelegate(ref TValue val);
		}

		// Token: 0x02000041 RID: 65
		private abstract class MinMaxAggregatorAcrossSlots<TItem, TStat> : NAReplaceTransform.StatAggregatorAcrossSlots<TItem, TStat>
		{
			// Token: 0x17000018 RID: 24
			// (get) Token: 0x0600016F RID: 367 RVA: 0x0000AF40 File Offset: 0x00009140
			public long ValuesProcessed
			{
				get
				{
					return this._valuesProcessed;
				}
			}

			// Token: 0x06000170 RID: 368 RVA: 0x0000AF48 File Offset: 0x00009148
			protected MinMaxAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col, bool returnMax)
				: base(ch, cursor, col)
			{
				this._returnMax = returnMax;
				if (this._returnMax)
				{
					this._processValueDelegate = new NAReplaceTransform.MinMaxAggregatorAcrossSlots<TItem, TStat>.ProcessValueDelegate(this.ProcessValueMax);
					return;
				}
				this._processValueDelegate = new NAReplaceTransform.MinMaxAggregatorAcrossSlots<TItem, TStat>.ProcessValueDelegate(this.ProcessValueMin);
			}

			// Token: 0x06000171 RID: 369 RVA: 0x0000AF95 File Offset: 0x00009195
			protected override void ProcessValue(ref TItem val)
			{
				this._valuesProcessed += 1L;
				this._processValueDelegate(ref val);
			}

			// Token: 0x06000172 RID: 370
			protected abstract void ProcessValueMin(ref TItem val);

			// Token: 0x06000173 RID: 371
			protected abstract void ProcessValueMax(ref TItem val);

			// Token: 0x040000AA RID: 170
			protected readonly bool _returnMax;

			// Token: 0x040000AB RID: 171
			protected readonly NAReplaceTransform.MinMaxAggregatorAcrossSlots<TItem, TStat>.ProcessValueDelegate _processValueDelegate;

			// Token: 0x040000AC RID: 172
			private long _valuesProcessed;

			// Token: 0x02000042 RID: 66
			// (Invoke) Token: 0x06000175 RID: 373
			protected delegate void ProcessValueDelegate(ref TItem val);
		}

		// Token: 0x02000043 RID: 67
		private abstract class MinMaxAggregatorBySlot<TItem, TStatItem> : NAReplaceTransform.StatAggregatorBySlot<TItem, TStatItem>
		{
			// Token: 0x06000178 RID: 376 RVA: 0x0000AFB4 File Offset: 0x000091B4
			protected MinMaxAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
				: base(ch, type, cursor, col)
			{
				this._returnMax = returnMax;
				if (this._returnMax)
				{
					this._processValueDelegate = new NAReplaceTransform.MinMaxAggregatorBySlot<TItem, TStatItem>.ProcessValueDelegate(this.ProcessValueMax);
				}
				else
				{
					this._processValueDelegate = new NAReplaceTransform.MinMaxAggregatorBySlot<TItem, TStatItem>.ProcessValueDelegate(this.ProcessValueMin);
				}
				this._valuesProcessed = new long[type.VectorSize];
			}

			// Token: 0x06000179 RID: 377 RVA: 0x0000B015 File Offset: 0x00009215
			protected override void ProcessValue(ref TItem val, int slot)
			{
				this._valuesProcessed[slot] += 1L;
				this._processValueDelegate(ref val, slot);
			}

			// Token: 0x0600017A RID: 378 RVA: 0x0000B03E File Offset: 0x0000923E
			protected long GetValuesProcessed(int slot)
			{
				return this._valuesProcessed[slot];
			}

			// Token: 0x0600017B RID: 379
			protected abstract void ProcessValueMin(ref TItem val, int slot);

			// Token: 0x0600017C RID: 380
			protected abstract void ProcessValueMax(ref TItem val, int slot);

			// Token: 0x040000AD RID: 173
			protected readonly bool _returnMax;

			// Token: 0x040000AE RID: 174
			protected readonly NAReplaceTransform.MinMaxAggregatorBySlot<TItem, TStatItem>.ProcessValueDelegate _processValueDelegate;

			// Token: 0x040000AF RID: 175
			private readonly long[] _valuesProcessed;

			// Token: 0x02000044 RID: 68
			// (Invoke) Token: 0x0600017E RID: 382
			protected delegate void ProcessValueDelegate(ref TItem val, int slot);
		}

		// Token: 0x02000045 RID: 69
		private struct MeanStatDouble
		{
			// Token: 0x06000181 RID: 385 RVA: 0x0000B048 File Offset: 0x00009248
			public void Update(double val)
			{
				if (val == 0.0)
				{
					return;
				}
				if (!FloatUtils.IsFinite(val))
				{
					this._cna += 1L;
					return;
				}
				this._cnz += 1L;
				if ((this._cur > 0.0) ^ (val > 0.0))
				{
					this._cur = this._cur - this._cur / (double)this._cnz + val / (double)this._cnz;
					return;
				}
				this._cur += (val - this._cur) / (double)this._cnz;
			}

			// Token: 0x06000182 RID: 386 RVA: 0x0000B0EC File Offset: 0x000092EC
			public double GetCurrentValue(IChannel ch, long count)
			{
				if (count == this._cna)
				{
					ch.Warning("All values in this column are NAs, using default value for imputation");
					return 0.0;
				}
				return this._cur * ((double)this._cnz / (double)(count - this._cna));
			}

			// Token: 0x06000183 RID: 387 RVA: 0x0000B134 File Offset: 0x00009334
			public double GetCurrentValue(IChannel ch, UInt128 count)
			{
				if (count == (ulong)this._cna)
				{
					ch.Warning("All values in this column are NAs, using default value for imputation");
					return 0.0;
				}
				return this._cur * ((double)this._cnz / (double)(count - (ulong)this._cna));
			}

			// Token: 0x040000B0 RID: 176
			private long _cna;

			// Token: 0x040000B1 RID: 177
			private long _cnz;

			// Token: 0x040000B2 RID: 178
			private double _cur;
		}

		// Token: 0x02000046 RID: 70
		private struct MeanStatInt
		{
			// Token: 0x06000184 RID: 388 RVA: 0x0000B187 File Offset: 0x00009387
			[Conditional("DEBUG")]
			private void AssertValid(long valMax)
			{
			}

			// Token: 0x06000185 RID: 389 RVA: 0x0000B18C File Offset: 0x0000938C
			public void Update(long val, long valMax)
			{
				if (val >= 0L)
				{
					IntUtils.Add(ref this._sumHi, ref this._sumLo, (ulong)val);
					return;
				}
				if (val >= -valMax)
				{
					IntUtils.Sub(ref this._sumHi, ref this._sumLo, (ulong)(-(ulong)val));
					return;
				}
				this._cna += 1L;
			}

			// Token: 0x06000186 RID: 390 RVA: 0x0000B1DC File Offset: 0x000093DC
			public long GetCurrentValue(IChannel ch, long count, long valMax)
			{
				if ((this._sumHi | this._sumLo) == 0UL)
				{
					if (count == this._cna)
					{
						ch.Warning("All values in this column are NAs, using default value for imputation");
					}
					return 0L;
				}
				count -= this._cna;
				ulong num = this._sumHi;
				ulong num2 = this._sumLo;
				bool flag = num < 0UL;
				if (flag)
				{
					num = ~num;
					num2 = ~num2;
					IntUtils.Add(ref num, ref num2, 1UL);
				}
				ulong num3 = IntUtils.DivRound(num2, num, (ulong)count);
				if (!flag)
				{
					return (long)num3;
				}
				return (long)(-(long)num3);
			}

			// Token: 0x06000187 RID: 391 RVA: 0x0000B254 File Offset: 0x00009454
			public long GetCurrentValue(IChannel ch, UInt128 count, long valMax)
			{
				if ((this._sumHi | this._sumLo) == 0UL)
				{
					if (count == (ulong)this._cna)
					{
						ch.Warning("All values in this column are NAs, using default value for imputation");
					}
					return 0L;
				}
				count -= (ulong)this._cna;
				ulong num = this._sumHi;
				ulong num2 = this._sumLo;
				bool flag = num < 0UL;
				if (flag)
				{
					num = ~num;
					num2 = ~num2;
					IntUtils.Add(ref num, ref num2, 1UL);
				}
				ulong num3 = IntUtils.DivRound(num2, num, count.Lo, count.Hi);
				if (!flag)
				{
					return (long)num3;
				}
				return (long)(-(long)num3);
			}

			// Token: 0x040000B3 RID: 179
			private long _cna;

			// Token: 0x040000B4 RID: 180
			private ulong _sumLo;

			// Token: 0x040000B5 RID: 181
			private ulong _sumHi;
		}

		// Token: 0x02000047 RID: 71
		private static class R4
		{
			// Token: 0x02000048 RID: 72
			public sealed class MeanAggregatorOne : NAReplaceTransform.StatAggregator<float, NAReplaceTransform.MeanStatDouble>
			{
				// Token: 0x06000188 RID: 392 RVA: 0x0000B2E1 File Offset: 0x000094E1
				public MeanAggregatorOne(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x06000189 RID: 393 RVA: 0x0000B2EC File Offset: 0x000094EC
				protected override void ProcessRow(ref float val)
				{
					this._stat.Update((double)val);
				}

				// Token: 0x0600018A RID: 394 RVA: 0x0000B2FC File Offset: 0x000094FC
				public override object GetStat()
				{
					double currentValue = this._stat.GetCurrentValue(this._ch, base.RowCount);
					return (float)currentValue;
				}
			}

			// Token: 0x02000049 RID: 73
			public sealed class MeanAggregatorAcrossSlots : NAReplaceTransform.StatAggregatorAcrossSlots<float, NAReplaceTransform.MeanStatDouble>
			{
				// Token: 0x0600018B RID: 395 RVA: 0x0000B328 File Offset: 0x00009528
				public MeanAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x0600018C RID: 396 RVA: 0x0000B333 File Offset: 0x00009533
				protected override void ProcessValue(ref float val)
				{
					this._stat.Update((double)val);
				}

				// Token: 0x0600018D RID: 397 RVA: 0x0000B344 File Offset: 0x00009544
				public override object GetStat()
				{
					double currentValue = this._stat.GetCurrentValue(this._ch, base.ValueCount);
					return (float)currentValue;
				}
			}

			// Token: 0x0200004A RID: 74
			public sealed class MeanAggregatorBySlot : NAReplaceTransform.StatAggregatorBySlot<float, NAReplaceTransform.MeanStatDouble>
			{
				// Token: 0x0600018E RID: 398 RVA: 0x0000B370 File Offset: 0x00009570
				public MeanAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, type, cursor, col)
				{
				}

				// Token: 0x0600018F RID: 399 RVA: 0x0000B37D File Offset: 0x0000957D
				protected override void ProcessValue(ref float val, int slot)
				{
					this._stat[slot].Update((double)val);
				}

				// Token: 0x06000190 RID: 400 RVA: 0x0000B394 File Offset: 0x00009594
				public override object GetStat()
				{
					float[] array = new float[this._stat.Length];
					for (int i = 0; i < array.Length; i++)
					{
						double currentValue = this._stat[i].GetCurrentValue(this._ch, base.RowCount);
						array[i] = (float)currentValue;
					}
					return array;
				}
			}

			// Token: 0x0200004B RID: 75
			public sealed class MinMaxAggregatorOne : NAReplaceTransform.MinMaxAggregatorOne<float, float>
			{
				// Token: 0x06000191 RID: 401 RVA: 0x0000B3E1 File Offset: 0x000095E1
				public MinMaxAggregatorOne(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (this._returnMax ? float.NegativeInfinity : float.PositiveInfinity);
				}

				// Token: 0x06000192 RID: 402 RVA: 0x0000B408 File Offset: 0x00009608
				protected override void ProcessValueMin(ref float val)
				{
					if (val < this._stat)
					{
						this._stat = val;
					}
				}

				// Token: 0x06000193 RID: 403 RVA: 0x0000B41C File Offset: 0x0000961C
				protected override void ProcessValueMax(ref float val)
				{
					if (val > this._stat)
					{
						this._stat = val;
					}
				}
			}

			// Token: 0x0200004C RID: 76
			public sealed class MinMaxAggregatorAcrossSlots : NAReplaceTransform.MinMaxAggregatorAcrossSlots<float, float>
			{
				// Token: 0x06000194 RID: 404 RVA: 0x0000B430 File Offset: 0x00009630
				public MinMaxAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (this._returnMax ? float.NegativeInfinity : float.PositiveInfinity);
				}

				// Token: 0x06000195 RID: 405 RVA: 0x0000B457 File Offset: 0x00009657
				protected override void ProcessValueMin(ref float val)
				{
					if (val < this._stat)
					{
						this._stat = val;
					}
				}

				// Token: 0x06000196 RID: 406 RVA: 0x0000B46B File Offset: 0x0000966B
				protected override void ProcessValueMax(ref float val)
				{
					if (val > this._stat)
					{
						this._stat = val;
					}
				}

				// Token: 0x06000197 RID: 407 RVA: 0x0000B480 File Offset: 0x00009680
				public override object GetStat()
				{
					if (base.ValueCount > (ulong)base.ValuesProcessed)
					{
						float num = 0f;
						this._processValueDelegate(ref num);
					}
					return this._stat;
				}
			}

			// Token: 0x0200004D RID: 77
			public sealed class MinMaxAggregatorBySlot : NAReplaceTransform.MinMaxAggregatorBySlot<float, float>
			{
				// Token: 0x06000198 RID: 408 RVA: 0x0000B4C0 File Offset: 0x000096C0
				public MinMaxAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, type, cursor, col, returnMax)
				{
					float num = (this._returnMax ? float.NegativeInfinity : float.PositiveInfinity);
					for (int i = 0; i < this._stat.Length; i++)
					{
						this._stat[i] = num;
					}
				}

				// Token: 0x06000199 RID: 409 RVA: 0x0000B50B File Offset: 0x0000970B
				protected override void ProcessValueMin(ref float val, int slot)
				{
					if (val < this._stat[slot])
					{
						this._stat[slot] = val;
					}
				}

				// Token: 0x0600019A RID: 410 RVA: 0x0000B523 File Offset: 0x00009723
				protected override void ProcessValueMax(ref float val, int slot)
				{
					if (val > this._stat[slot])
					{
						this._stat[slot] = val;
					}
				}

				// Token: 0x0600019B RID: 411 RVA: 0x0000B53C File Offset: 0x0000973C
				public override object GetStat()
				{
					for (int i = 0; i < this._stat.Length; i++)
					{
						if (base.GetValuesProcessed(i) < base.RowCount)
						{
							float num = 0f;
							this._processValueDelegate(ref num, i);
						}
					}
					return this._stat;
				}
			}
		}

		// Token: 0x0200004E RID: 78
		private static class R8
		{
			// Token: 0x0200004F RID: 79
			public sealed class MeanAggregatorOne : NAReplaceTransform.StatAggregator<double, NAReplaceTransform.MeanStatDouble>
			{
				// Token: 0x0600019C RID: 412 RVA: 0x0000B585 File Offset: 0x00009785
				public MeanAggregatorOne(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x0600019D RID: 413 RVA: 0x0000B590 File Offset: 0x00009790
				protected override void ProcessRow(ref double val)
				{
					this._stat.Update(val);
				}

				// Token: 0x0600019E RID: 414 RVA: 0x0000B59F File Offset: 0x0000979F
				public override object GetStat()
				{
					return this._stat.GetCurrentValue(this._ch, base.RowCount);
				}
			}

			// Token: 0x02000050 RID: 80
			public sealed class MeanAggregatorAcrossSlots : NAReplaceTransform.StatAggregatorAcrossSlots<double, NAReplaceTransform.MeanStatDouble>
			{
				// Token: 0x0600019F RID: 415 RVA: 0x0000B5BD File Offset: 0x000097BD
				public MeanAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x060001A0 RID: 416 RVA: 0x0000B5C8 File Offset: 0x000097C8
				protected override void ProcessValue(ref double val)
				{
					this._stat.Update(val);
				}

				// Token: 0x060001A1 RID: 417 RVA: 0x0000B5D7 File Offset: 0x000097D7
				public override object GetStat()
				{
					return this._stat.GetCurrentValue(this._ch, base.ValueCount);
				}
			}

			// Token: 0x02000051 RID: 81
			public sealed class MeanAggregatorBySlot : NAReplaceTransform.StatAggregatorBySlot<double, NAReplaceTransform.MeanStatDouble>
			{
				// Token: 0x060001A2 RID: 418 RVA: 0x0000B5F5 File Offset: 0x000097F5
				public MeanAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, type, cursor, col)
				{
				}

				// Token: 0x060001A3 RID: 419 RVA: 0x0000B602 File Offset: 0x00009802
				protected override void ProcessValue(ref double val, int slot)
				{
					this._stat[slot].Update(val);
				}

				// Token: 0x060001A4 RID: 420 RVA: 0x0000B618 File Offset: 0x00009818
				public override object GetStat()
				{
					double[] array = new double[this._stat.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = this._stat[i].GetCurrentValue(this._ch, base.RowCount);
					}
					return array;
				}
			}

			// Token: 0x02000052 RID: 82
			public sealed class MinMaxAggregatorOne : NAReplaceTransform.MinMaxAggregatorOne<double, double>
			{
				// Token: 0x060001A5 RID: 421 RVA: 0x0000B662 File Offset: 0x00009862
				public MinMaxAggregatorOne(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (this._returnMax ? double.NegativeInfinity : double.PositiveInfinity);
				}

				// Token: 0x060001A6 RID: 422 RVA: 0x0000B691 File Offset: 0x00009891
				protected override void ProcessValueMin(ref double val)
				{
					if (val < this._stat)
					{
						this._stat = val;
					}
				}

				// Token: 0x060001A7 RID: 423 RVA: 0x0000B6A5 File Offset: 0x000098A5
				protected override void ProcessValueMax(ref double val)
				{
					if (val > this._stat)
					{
						this._stat = val;
					}
				}
			}

			// Token: 0x02000053 RID: 83
			public sealed class MinMaxAggregatorAcrossSlots : NAReplaceTransform.MinMaxAggregatorAcrossSlots<double, double>
			{
				// Token: 0x060001A8 RID: 424 RVA: 0x0000B6B9 File Offset: 0x000098B9
				public MinMaxAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (this._returnMax ? double.NegativeInfinity : double.PositiveInfinity);
				}

				// Token: 0x060001A9 RID: 425 RVA: 0x0000B6E8 File Offset: 0x000098E8
				protected override void ProcessValueMin(ref double val)
				{
					if (val < this._stat)
					{
						this._stat = val;
					}
				}

				// Token: 0x060001AA RID: 426 RVA: 0x0000B6FC File Offset: 0x000098FC
				protected override void ProcessValueMax(ref double val)
				{
					if (val > this._stat)
					{
						this._stat = val;
					}
				}

				// Token: 0x060001AB RID: 427 RVA: 0x0000B710 File Offset: 0x00009910
				public override object GetStat()
				{
					if (base.ValueCount > (ulong)base.ValuesProcessed)
					{
						double num = 0.0;
						this._processValueDelegate(ref num);
					}
					return this._stat;
				}
			}

			// Token: 0x02000054 RID: 84
			public sealed class MinMaxAggregatorBySlot : NAReplaceTransform.MinMaxAggregatorBySlot<double, double>
			{
				// Token: 0x060001AC RID: 428 RVA: 0x0000B754 File Offset: 0x00009954
				public MinMaxAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, type, cursor, col, returnMax)
				{
					double num = (this._returnMax ? double.MinValue : double.MaxValue);
					for (int i = 0; i < this._stat.Length; i++)
					{
						this._stat[i] = num;
					}
				}

				// Token: 0x060001AD RID: 429 RVA: 0x0000B7A7 File Offset: 0x000099A7
				protected override void ProcessValueMin(ref double val, int slot)
				{
					if (FloatUtils.IsFinite(val) && val < this._stat[slot])
					{
						this._stat[slot] = val;
					}
				}

				// Token: 0x060001AE RID: 430 RVA: 0x0000B7C8 File Offset: 0x000099C8
				protected override void ProcessValueMax(ref double val, int slot)
				{
					if (FloatUtils.IsFinite(val) && val > this._stat[slot])
					{
						this._stat[slot] = val;
					}
				}

				// Token: 0x060001AF RID: 431 RVA: 0x0000B7EC File Offset: 0x000099EC
				public override object GetStat()
				{
					for (int i = 0; i < this._stat.Length; i++)
					{
						if (base.GetValuesProcessed(i) < base.RowCount)
						{
							double num = 0.0;
							this._processValueDelegate(ref num, i);
						}
					}
					return this._stat;
				}
			}
		}

		// Token: 0x02000055 RID: 85
		private static class I1
		{
			// Token: 0x040000B6 RID: 182
			private const long MaxVal = 127L;

			// Token: 0x02000056 RID: 86
			public sealed class MeanAggregatorOne : NAReplaceTransform.StatAggregator<DvInt1, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001B0 RID: 432 RVA: 0x0000B839 File Offset: 0x00009A39
				public MeanAggregatorOne(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x060001B1 RID: 433 RVA: 0x0000B844 File Offset: 0x00009A44
				protected override void ProcessRow(ref DvInt1 val)
				{
					this._stat.Update((long)val.RawValue, 127L);
				}

				// Token: 0x060001B2 RID: 434 RVA: 0x0000B85C File Offset: 0x00009A5C
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.RowCount, 127L);
					return (sbyte)currentValue;
				}
			}

			// Token: 0x02000057 RID: 87
			public sealed class MeanAggregatorAcrossSlots : NAReplaceTransform.StatAggregatorAcrossSlots<DvInt1, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001B3 RID: 435 RVA: 0x0000B890 File Offset: 0x00009A90
				public MeanAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x060001B4 RID: 436 RVA: 0x0000B89B File Offset: 0x00009A9B
				protected override void ProcessValue(ref DvInt1 val)
				{
					this._stat.Update((long)val.RawValue, 127L);
				}

				// Token: 0x060001B5 RID: 437 RVA: 0x0000B8B4 File Offset: 0x00009AB4
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.ValueCount, 127L);
					return (sbyte)currentValue;
				}
			}

			// Token: 0x02000058 RID: 88
			public sealed class MeanAggregatorBySlot : NAReplaceTransform.StatAggregatorBySlot<DvInt1, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001B6 RID: 438 RVA: 0x0000B8E8 File Offset: 0x00009AE8
				public MeanAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, type, cursor, col)
				{
				}

				// Token: 0x060001B7 RID: 439 RVA: 0x0000B8F5 File Offset: 0x00009AF5
				protected override void ProcessValue(ref DvInt1 val, int slot)
				{
					this._stat[slot].Update((long)val.RawValue, 127L);
				}

				// Token: 0x060001B8 RID: 440 RVA: 0x0000B914 File Offset: 0x00009B14
				public override object GetStat()
				{
					DvInt1[] array = new DvInt1[this._stat.Length];
					for (int i = 0; i < array.Length; i++)
					{
						long currentValue = this._stat[i].GetCurrentValue(this._ch, base.RowCount, 127L);
						array[i] = (sbyte)currentValue;
					}
					return array;
				}
			}

			// Token: 0x02000059 RID: 89
			public sealed class MinMaxAggregatorOne : NAReplaceTransform.MinMaxAggregatorOne<DvInt1, sbyte>
			{
				// Token: 0x060001B9 RID: 441 RVA: 0x0000B972 File Offset: 0x00009B72
				public MinMaxAggregatorOne(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (sbyte)(this._returnMax ? (-127L) : 127L);
				}

				// Token: 0x060001BA RID: 442 RVA: 0x0000B998 File Offset: 0x00009B98
				protected override void ProcessValueMin(ref DvInt1 val)
				{
					sbyte rawValue = val.RawValue;
					if (rawValue < this._stat && rawValue != -128)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001BB RID: 443 RVA: 0x0000B9C4 File Offset: 0x00009BC4
				protected override void ProcessValueMax(ref DvInt1 val)
				{
					sbyte rawValue = val.RawValue;
					if (rawValue > this._stat)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001BC RID: 444 RVA: 0x0000B9E8 File Offset: 0x00009BE8
				public override object GetStat()
				{
					return this._stat;
				}
			}

			// Token: 0x0200005A RID: 90
			public sealed class MinMaxAggregatorAcrossSlots : NAReplaceTransform.MinMaxAggregatorAcrossSlots<DvInt1, sbyte>
			{
				// Token: 0x060001BD RID: 445 RVA: 0x0000B9FA File Offset: 0x00009BFA
				public MinMaxAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (sbyte)(this._returnMax ? (-127L) : 127L);
				}

				// Token: 0x060001BE RID: 446 RVA: 0x0000BA20 File Offset: 0x00009C20
				protected override void ProcessValueMin(ref DvInt1 val)
				{
					sbyte rawValue = val.RawValue;
					if (rawValue < this._stat && rawValue != -128)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001BF RID: 447 RVA: 0x0000BA4C File Offset: 0x00009C4C
				protected override void ProcessValueMax(ref DvInt1 val)
				{
					sbyte rawValue = val.RawValue;
					if (rawValue > this._stat)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001C0 RID: 448 RVA: 0x0000BA70 File Offset: 0x00009C70
				public override object GetStat()
				{
					if (base.ValueCount > (ulong)base.ValuesProcessed)
					{
						DvInt1 dvInt = default(DvInt1);
						this._processValueDelegate(ref dvInt);
					}
					return this._stat;
				}
			}

			// Token: 0x0200005B RID: 91
			public sealed class MinMaxAggregatorBySlot : NAReplaceTransform.MinMaxAggregatorBySlot<DvInt1, sbyte>
			{
				// Token: 0x060001C1 RID: 449 RVA: 0x0000BAB8 File Offset: 0x00009CB8
				public MinMaxAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, type, cursor, col, returnMax)
				{
					sbyte b = (sbyte)(this._returnMax ? (-127L) : 127L);
					for (int i = 0; i < this._stat.Length; i++)
					{
						this._stat[i] = b;
					}
				}

				// Token: 0x060001C2 RID: 450 RVA: 0x0000BB00 File Offset: 0x00009D00
				protected override void ProcessValueMin(ref DvInt1 val, int slot)
				{
					sbyte rawValue = val.RawValue;
					if (rawValue < this._stat[slot] && rawValue != -128)
					{
						this._stat[slot] = rawValue;
					}
				}

				// Token: 0x060001C3 RID: 451 RVA: 0x0000BB30 File Offset: 0x00009D30
				protected override void ProcessValueMax(ref DvInt1 val, int slot)
				{
					sbyte rawValue = val.RawValue;
					if (rawValue > this._stat[slot])
					{
						this._stat[slot] = rawValue;
					}
				}

				// Token: 0x060001C4 RID: 452 RVA: 0x0000BB58 File Offset: 0x00009D58
				public override object GetStat()
				{
					DvInt1[] array = new DvInt1[this._stat.Length];
					for (int i = 0; i < this._stat.Length; i++)
					{
						if (base.GetValuesProcessed(i) < base.RowCount)
						{
							DvInt1 dvInt = default(DvInt1);
							this._processValueDelegate(ref dvInt, i);
						}
						array[i] = this._stat[i];
					}
					return array;
				}
			}
		}

		// Token: 0x0200005C RID: 92
		private static class I2
		{
			// Token: 0x040000B7 RID: 183
			private const long MaxVal = 32767L;

			// Token: 0x0200005D RID: 93
			public sealed class MeanAggregatorOne : NAReplaceTransform.StatAggregator<DvInt2, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001C5 RID: 453 RVA: 0x0000BBC5 File Offset: 0x00009DC5
				public MeanAggregatorOne(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x060001C6 RID: 454 RVA: 0x0000BBD0 File Offset: 0x00009DD0
				protected override void ProcessRow(ref DvInt2 val)
				{
					this._stat.Update((long)val.RawValue, 32767L);
				}

				// Token: 0x060001C7 RID: 455 RVA: 0x0000BBEC File Offset: 0x00009DEC
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.RowCount, 32767L);
					return (short)currentValue;
				}
			}

			// Token: 0x0200005E RID: 94
			public sealed class MeanAggregatorAcrossSlots : NAReplaceTransform.StatAggregatorAcrossSlots<DvInt2, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001C8 RID: 456 RVA: 0x0000BC23 File Offset: 0x00009E23
				public MeanAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x060001C9 RID: 457 RVA: 0x0000BC2E File Offset: 0x00009E2E
				protected override void ProcessValue(ref DvInt2 val)
				{
					this._stat.Update((long)val.RawValue, 32767L);
				}

				// Token: 0x060001CA RID: 458 RVA: 0x0000BC48 File Offset: 0x00009E48
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.ValueCount, 32767L);
					return (short)currentValue;
				}
			}

			// Token: 0x0200005F RID: 95
			public sealed class MeanAggregatorBySlot : NAReplaceTransform.StatAggregatorBySlot<DvInt2, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001CB RID: 459 RVA: 0x0000BC7F File Offset: 0x00009E7F
				public MeanAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, type, cursor, col)
				{
				}

				// Token: 0x060001CC RID: 460 RVA: 0x0000BC8C File Offset: 0x00009E8C
				protected override void ProcessValue(ref DvInt2 val, int slot)
				{
					this._stat[slot].Update((long)val.RawValue, 32767L);
				}

				// Token: 0x060001CD RID: 461 RVA: 0x0000BCAC File Offset: 0x00009EAC
				public override object GetStat()
				{
					DvInt2[] array = new DvInt2[this._stat.Length];
					for (int i = 0; i < array.Length; i++)
					{
						long currentValue = this._stat[i].GetCurrentValue(this._ch, base.RowCount, 32767L);
						array[i] = (short)currentValue;
					}
					return array;
				}
			}

			// Token: 0x02000060 RID: 96
			public sealed class MinMaxAggregatorOne : NAReplaceTransform.MinMaxAggregatorOne<DvInt2, short>
			{
				// Token: 0x060001CE RID: 462 RVA: 0x0000BD0D File Offset: 0x00009F0D
				public MinMaxAggregatorOne(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (short)(this._returnMax ? (-32767L) : 32767L);
				}

				// Token: 0x060001CF RID: 463 RVA: 0x0000BD38 File Offset: 0x00009F38
				protected override void ProcessValueMin(ref DvInt2 val)
				{
					short rawValue = val.RawValue;
					if (rawValue < this._stat && rawValue != -32768)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001D0 RID: 464 RVA: 0x0000BD64 File Offset: 0x00009F64
				protected override void ProcessValueMax(ref DvInt2 val)
				{
					short rawValue = val.RawValue;
					if (rawValue > this._stat)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001D1 RID: 465 RVA: 0x0000BD88 File Offset: 0x00009F88
				public override object GetStat()
				{
					return this._stat;
				}
			}

			// Token: 0x02000061 RID: 97
			public sealed class MinMaxAggregatorAcrossSlots : NAReplaceTransform.MinMaxAggregatorAcrossSlots<DvInt2, short>
			{
				// Token: 0x060001D2 RID: 466 RVA: 0x0000BD9A File Offset: 0x00009F9A
				public MinMaxAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (short)(this._returnMax ? (-32767L) : 32767L);
				}

				// Token: 0x060001D3 RID: 467 RVA: 0x0000BDC4 File Offset: 0x00009FC4
				protected override void ProcessValueMin(ref DvInt2 val)
				{
					short rawValue = val.RawValue;
					if (rawValue < this._stat && rawValue != -32768)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001D4 RID: 468 RVA: 0x0000BDF0 File Offset: 0x00009FF0
				protected override void ProcessValueMax(ref DvInt2 val)
				{
					short rawValue = val.RawValue;
					if (rawValue > this._stat)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001D5 RID: 469 RVA: 0x0000BE14 File Offset: 0x0000A014
				public override object GetStat()
				{
					if (base.ValueCount > (ulong)base.ValuesProcessed)
					{
						DvInt2 dvInt = default(DvInt2);
						this._processValueDelegate(ref dvInt);
					}
					return this._stat;
				}
			}

			// Token: 0x02000062 RID: 98
			public sealed class MinMaxAggregatorBySlot : NAReplaceTransform.MinMaxAggregatorBySlot<DvInt2, short>
			{
				// Token: 0x060001D6 RID: 470 RVA: 0x0000BE5C File Offset: 0x0000A05C
				public MinMaxAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, type, cursor, col, returnMax)
				{
					short num = (short)(this._returnMax ? (-32767L) : 32767L);
					for (int i = 0; i < this._stat.Length; i++)
					{
						this._stat[i] = num;
					}
				}

				// Token: 0x060001D7 RID: 471 RVA: 0x0000BEAC File Offset: 0x0000A0AC
				protected override void ProcessValueMin(ref DvInt2 val, int slot)
				{
					short rawValue = val.RawValue;
					if (rawValue < this._stat[slot] && rawValue != -32768)
					{
						this._stat[slot] = rawValue;
					}
				}

				// Token: 0x060001D8 RID: 472 RVA: 0x0000BEDC File Offset: 0x0000A0DC
				protected override void ProcessValueMax(ref DvInt2 val, int slot)
				{
					short rawValue = val.RawValue;
					if (rawValue > this._stat[slot])
					{
						this._stat[slot] = rawValue;
					}
				}

				// Token: 0x060001D9 RID: 473 RVA: 0x0000BF04 File Offset: 0x0000A104
				public override object GetStat()
				{
					DvInt2[] array = new DvInt2[this._stat.Length];
					for (int i = 0; i < this._stat.Length; i++)
					{
						if (base.GetValuesProcessed(i) < base.RowCount)
						{
							DvInt2 dvInt = default(DvInt2);
							this._processValueDelegate(ref dvInt, i);
						}
						array[i] = this._stat[i];
					}
					return array;
				}
			}
		}

		// Token: 0x02000063 RID: 99
		private static class I4
		{
			// Token: 0x040000B8 RID: 184
			private const long MaxVal = 2147483647L;

			// Token: 0x02000064 RID: 100
			public sealed class MeanAggregatorOne : NAReplaceTransform.StatAggregator<DvInt4, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001DA RID: 474 RVA: 0x0000BF71 File Offset: 0x0000A171
				public MeanAggregatorOne(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x060001DB RID: 475 RVA: 0x0000BF7C File Offset: 0x0000A17C
				protected override void ProcessRow(ref DvInt4 val)
				{
					this._stat.Update((long)val.RawValue, 2147483647L);
				}

				// Token: 0x060001DC RID: 476 RVA: 0x0000BF98 File Offset: 0x0000A198
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.RowCount, 2147483647L);
					return (int)currentValue;
				}
			}

			// Token: 0x02000065 RID: 101
			public sealed class MeanAggregatorAcrossSlots : NAReplaceTransform.StatAggregatorAcrossSlots<DvInt4, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001DD RID: 477 RVA: 0x0000BFCF File Offset: 0x0000A1CF
				public MeanAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
				}

				// Token: 0x060001DE RID: 478 RVA: 0x0000BFDA File Offset: 0x0000A1DA
				protected override void ProcessValue(ref DvInt4 val)
				{
					this._stat.Update((long)val.RawValue, 2147483647L);
				}

				// Token: 0x060001DF RID: 479 RVA: 0x0000BFF4 File Offset: 0x0000A1F4
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.ValueCount, 2147483647L);
					return (int)currentValue;
				}
			}

			// Token: 0x02000066 RID: 102
			public sealed class MeanAggregatorBySlot : NAReplaceTransform.StatAggregatorBySlot<DvInt4, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001E0 RID: 480 RVA: 0x0000C02B File Offset: 0x0000A22B
				public MeanAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, type, cursor, col)
				{
				}

				// Token: 0x060001E1 RID: 481 RVA: 0x0000C038 File Offset: 0x0000A238
				protected override void ProcessValue(ref DvInt4 val, int slot)
				{
					this._stat[slot].Update((long)val.RawValue, 2147483647L);
				}

				// Token: 0x060001E2 RID: 482 RVA: 0x0000C058 File Offset: 0x0000A258
				public override object GetStat()
				{
					DvInt4[] array = new DvInt4[this._stat.Length];
					for (int i = 0; i < array.Length; i++)
					{
						long currentValue = this._stat[i].GetCurrentValue(this._ch, base.RowCount, 2147483647L);
						array[i] = (int)currentValue;
					}
					return array;
				}
			}

			// Token: 0x02000067 RID: 103
			public sealed class MinMaxAggregatorOne : NAReplaceTransform.MinMaxAggregatorOne<DvInt4, int>
			{
				// Token: 0x060001E3 RID: 483 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
				public MinMaxAggregatorOne(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (int)(this._returnMax ? (-2147483647L) : 2147483647L);
				}

				// Token: 0x060001E4 RID: 484 RVA: 0x0000C0E4 File Offset: 0x0000A2E4
				protected override void ProcessValueMin(ref DvInt4 val)
				{
					int rawValue = val.RawValue;
					if (rawValue < this._stat && rawValue != -2147483648)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001E5 RID: 485 RVA: 0x0000C110 File Offset: 0x0000A310
				protected override void ProcessValueMax(ref DvInt4 val)
				{
					int rawValue = val.RawValue;
					if (rawValue > this._stat)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001E6 RID: 486 RVA: 0x0000C134 File Offset: 0x0000A334
				public override object GetStat()
				{
					return this._stat;
				}
			}

			// Token: 0x02000068 RID: 104
			public sealed class MinMaxAggregatorAcrossSlots : NAReplaceTransform.MinMaxAggregatorAcrossSlots<DvInt4, int>
			{
				// Token: 0x060001E7 RID: 487 RVA: 0x0000C146 File Offset: 0x0000A346
				public MinMaxAggregatorAcrossSlots(IChannel ch, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (int)(this._returnMax ? (-2147483647L) : 2147483647L);
				}

				// Token: 0x060001E8 RID: 488 RVA: 0x0000C170 File Offset: 0x0000A370
				protected override void ProcessValueMin(ref DvInt4 val)
				{
					int rawValue = val.RawValue;
					if (rawValue < this._stat && rawValue != -2147483648)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001E9 RID: 489 RVA: 0x0000C19C File Offset: 0x0000A39C
				protected override void ProcessValueMax(ref DvInt4 val)
				{
					int rawValue = val.RawValue;
					if (rawValue > this._stat)
					{
						this._stat = rawValue;
					}
				}

				// Token: 0x060001EA RID: 490 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
				public override object GetStat()
				{
					if (base.ValueCount > (ulong)base.ValuesProcessed)
					{
						DvInt4 dvInt = default(DvInt4);
						this._processValueDelegate(ref dvInt);
					}
					return this._stat;
				}
			}

			// Token: 0x02000069 RID: 105
			public sealed class MinMaxAggregatorBySlot : NAReplaceTransform.MinMaxAggregatorBySlot<DvInt4, int>
			{
				// Token: 0x060001EB RID: 491 RVA: 0x0000C208 File Offset: 0x0000A408
				public MinMaxAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, type, cursor, col, returnMax)
				{
					int num = (int)(this._returnMax ? (-2147483647L) : 2147483647L);
					for (int i = 0; i < this._stat.Length; i++)
					{
						this._stat[i] = num;
					}
				}

				// Token: 0x060001EC RID: 492 RVA: 0x0000C258 File Offset: 0x0000A458
				protected override void ProcessValueMin(ref DvInt4 val, int slot)
				{
					int rawValue = val.RawValue;
					if (rawValue < this._stat[slot] && rawValue != -2147483648)
					{
						this._stat[slot] = rawValue;
					}
				}

				// Token: 0x060001ED RID: 493 RVA: 0x0000C288 File Offset: 0x0000A488
				protected override void ProcessValueMax(ref DvInt4 val, int slot)
				{
					int rawValue = val.RawValue;
					if (rawValue > this._stat[slot])
					{
						this._stat[slot] = rawValue;
					}
				}

				// Token: 0x060001EE RID: 494 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
				public override object GetStat()
				{
					DvInt4[] array = new DvInt4[this._stat.Length];
					for (int i = 0; i < this._stat.Length; i++)
					{
						if (base.GetValuesProcessed(i) < base.RowCount)
						{
							DvInt4 dvInt = default(DvInt4);
							this._processValueDelegate(ref dvInt, i);
						}
						array[i] = this._stat[i];
					}
					return array;
				}
			}
		}

		// Token: 0x0200006A RID: 106
		private static class Long
		{
			// Token: 0x060001EF RID: 495 RVA: 0x0000C320 File Offset: 0x0000A520
			private static NAReplaceTransform.Long.Converter<TItem> CreateConverter<TItem>(ColumnType type)
			{
				NAReplaceTransform.Long.Converter converter;
				if (type.ItemType.IsTimeSpan)
				{
					converter = new NAReplaceTransform.Long.TSConverter();
				}
				else if (type.ItemType.IsDateTime)
				{
					converter = new NAReplaceTransform.Long.DTConverter();
				}
				else
				{
					converter = new NAReplaceTransform.Long.I8Converter();
				}
				return (NAReplaceTransform.Long.Converter<TItem>)converter;
			}

			// Token: 0x040000B9 RID: 185
			private const long MaxVal = 9223372036854775807L;

			// Token: 0x0200006B RID: 107
			public sealed class MeanAggregatorOne<TItem> : NAReplaceTransform.StatAggregator<TItem, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001F0 RID: 496 RVA: 0x0000C363 File Offset: 0x0000A563
				public MeanAggregatorOne(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
					this._converter = NAReplaceTransform.Long.CreateConverter<TItem>(type);
				}

				// Token: 0x060001F1 RID: 497 RVA: 0x0000C37B File Offset: 0x0000A57B
				protected override void ProcessRow(ref TItem val)
				{
					this._stat.Update(this._converter.ToLong(val), long.MaxValue);
				}

				// Token: 0x060001F2 RID: 498 RVA: 0x0000C3A4 File Offset: 0x0000A5A4
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.RowCount, long.MaxValue);
					return this._converter.FromLong(currentValue);
				}

				// Token: 0x040000BA RID: 186
				private NAReplaceTransform.Long.Converter<TItem> _converter;
			}

			// Token: 0x0200006C RID: 108
			public sealed class MeanAggregatorAcrossSlots<TItem> : NAReplaceTransform.StatAggregatorAcrossSlots<TItem, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001F3 RID: 499 RVA: 0x0000C3E3 File Offset: 0x0000A5E3
				public MeanAggregatorAcrossSlots(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, cursor, col)
				{
					this._converter = NAReplaceTransform.Long.CreateConverter<TItem>(type);
				}

				// Token: 0x060001F4 RID: 500 RVA: 0x0000C3FB File Offset: 0x0000A5FB
				protected override void ProcessValue(ref TItem val)
				{
					this._stat.Update(this._converter.ToLong(val), long.MaxValue);
				}

				// Token: 0x060001F5 RID: 501 RVA: 0x0000C424 File Offset: 0x0000A624
				public override object GetStat()
				{
					long currentValue = this._stat.GetCurrentValue(this._ch, base.ValueCount, long.MaxValue);
					return this._converter.FromLong(currentValue);
				}

				// Token: 0x040000BB RID: 187
				private NAReplaceTransform.Long.Converter<TItem> _converter;
			}

			// Token: 0x0200006D RID: 109
			public sealed class MeanAggregatorBySlot<TItem> : NAReplaceTransform.StatAggregatorBySlot<TItem, NAReplaceTransform.MeanStatInt>
			{
				// Token: 0x060001F6 RID: 502 RVA: 0x0000C463 File Offset: 0x0000A663
				public MeanAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col)
					: base(ch, type, cursor, col)
				{
					this._converter = NAReplaceTransform.Long.CreateConverter<TItem>(type);
				}

				// Token: 0x060001F7 RID: 503 RVA: 0x0000C47C File Offset: 0x0000A67C
				protected override void ProcessValue(ref TItem val, int slot)
				{
					this._stat[slot].Update(this._converter.ToLong(val), long.MaxValue);
				}

				// Token: 0x060001F8 RID: 504 RVA: 0x0000C4AC File Offset: 0x0000A6AC
				public override object GetStat()
				{
					TItem[] array = new TItem[this._stat.Length];
					for (int i = 0; i < array.Length; i++)
					{
						long currentValue = this._stat[i].GetCurrentValue(this._ch, base.RowCount, long.MaxValue);
						array[i] = this._converter.FromLong(currentValue);
					}
					return array;
				}

				// Token: 0x040000BC RID: 188
				private NAReplaceTransform.Long.Converter<TItem> _converter;
			}

			// Token: 0x0200006E RID: 110
			public sealed class MinMaxAggregatorOne<TItem> : NAReplaceTransform.MinMaxAggregatorOne<TItem, long>
			{
				// Token: 0x060001F9 RID: 505 RVA: 0x0000C510 File Offset: 0x0000A710
				public MinMaxAggregatorOne(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (this._returnMax ? (-9223372036854775807L) : long.MaxValue);
					this._converter = NAReplaceTransform.Long.CreateConverter<TItem>(type);
				}

				// Token: 0x060001FA RID: 506 RVA: 0x0000C54C File Offset: 0x0000A74C
				protected override void ProcessValueMin(ref TItem val)
				{
					long num = this._converter.ToLong(val);
					if (num < this._stat && -9223372036854775807L <= num)
					{
						this._stat = num;
					}
				}

				// Token: 0x060001FB RID: 507 RVA: 0x0000C588 File Offset: 0x0000A788
				protected override void ProcessValueMax(ref TItem val)
				{
					long num = this._converter.ToLong(val);
					if (num > this._stat)
					{
						this._stat = num;
					}
				}

				// Token: 0x060001FC RID: 508 RVA: 0x0000C5B7 File Offset: 0x0000A7B7
				public override object GetStat()
				{
					return this._converter.FromLong(this._stat);
				}

				// Token: 0x040000BD RID: 189
				private NAReplaceTransform.Long.Converter<TItem> _converter;
			}

			// Token: 0x0200006F RID: 111
			public sealed class MinMaxAggregatorAcrossSlots<TItem> : NAReplaceTransform.MinMaxAggregatorAcrossSlots<TItem, long>
			{
				// Token: 0x060001FD RID: 509 RVA: 0x0000C5CF File Offset: 0x0000A7CF
				public MinMaxAggregatorAcrossSlots(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, cursor, col, returnMax)
				{
					this._stat = (this._returnMax ? (-9223372036854775807L) : long.MaxValue);
					this._converter = NAReplaceTransform.Long.CreateConverter<TItem>(type);
				}

				// Token: 0x060001FE RID: 510 RVA: 0x0000C60C File Offset: 0x0000A80C
				protected override void ProcessValueMin(ref TItem val)
				{
					long num = this._converter.ToLong(val);
					if (num < this._stat && -9223372036854775807L <= num)
					{
						this._stat = num;
					}
				}

				// Token: 0x060001FF RID: 511 RVA: 0x0000C648 File Offset: 0x0000A848
				protected override void ProcessValueMax(ref TItem val)
				{
					long num = this._converter.ToLong(val);
					if (num > this._stat)
					{
						this._stat = num;
					}
				}

				// Token: 0x06000200 RID: 512 RVA: 0x0000C678 File Offset: 0x0000A878
				public override object GetStat()
				{
					if (base.ValueCount > (ulong)base.ValuesProcessed)
					{
						TItem titem = default(TItem);
						this._processValueDelegate(ref titem);
					}
					return this._converter.FromLong(this._stat);
				}

				// Token: 0x040000BE RID: 190
				private NAReplaceTransform.Long.Converter<TItem> _converter;
			}

			// Token: 0x02000070 RID: 112
			public sealed class MinMaxAggregatorBySlot<TItem> : NAReplaceTransform.MinMaxAggregatorBySlot<TItem, long>
			{
				// Token: 0x06000201 RID: 513 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
				public MinMaxAggregatorBySlot(IChannel ch, ColumnType type, IRowCursor cursor, int col, bool returnMax)
					: base(ch, type, cursor, col, returnMax)
				{
					long num = (this._returnMax ? (-9223372036854775807L) : long.MaxValue);
					for (int i = 0; i < this._stat.Length; i++)
					{
						this._stat[i] = num;
					}
					this._converter = NAReplaceTransform.Long.CreateConverter<TItem>(type);
				}

				// Token: 0x06000202 RID: 514 RVA: 0x0000C724 File Offset: 0x0000A924
				protected override void ProcessValueMin(ref TItem val, int slot)
				{
					long num = this._converter.ToLong(val);
					if (num < this._stat[slot] && -9223372036854775807L <= num)
					{
						this._stat[slot] = num;
					}
				}

				// Token: 0x06000203 RID: 515 RVA: 0x0000C764 File Offset: 0x0000A964
				protected override void ProcessValueMax(ref TItem val, int slot)
				{
					long num = this._converter.ToLong(val);
					if (num > this._stat[slot])
					{
						this._stat[slot] = num;
					}
				}

				// Token: 0x06000204 RID: 516 RVA: 0x0000C798 File Offset: 0x0000A998
				public override object GetStat()
				{
					TItem[] array = new TItem[this._stat.Length];
					for (int i = 0; i < this._stat.Length; i++)
					{
						if (base.GetValuesProcessed(i) < base.RowCount)
						{
							TItem titem = default(TItem);
							this._processValueDelegate(ref titem, i);
						}
						array[i] = this._converter.FromLong(this._stat[i]);
					}
					return array;
				}

				// Token: 0x040000BF RID: 191
				private NAReplaceTransform.Long.Converter<TItem> _converter;
			}

			// Token: 0x02000071 RID: 113
			private abstract class Converter
			{
			}

			// Token: 0x02000072 RID: 114
			private abstract class Converter<T> : NAReplaceTransform.Long.Converter
			{
				// Token: 0x06000206 RID: 518
				public abstract long ToLong(T val);

				// Token: 0x06000207 RID: 519
				public abstract T FromLong(long val);
			}

			// Token: 0x02000073 RID: 115
			private sealed class I8Converter : NAReplaceTransform.Long.Converter<DvInt8>
			{
				// Token: 0x06000209 RID: 521 RVA: 0x0000C816 File Offset: 0x0000AA16
				public override long ToLong(DvInt8 val)
				{
					return val.RawValue;
				}

				// Token: 0x0600020A RID: 522 RVA: 0x0000C81F File Offset: 0x0000AA1F
				public override DvInt8 FromLong(long val)
				{
					return val;
				}
			}

			// Token: 0x02000074 RID: 116
			private sealed class TSConverter : NAReplaceTransform.Long.Converter<DvTimeSpan>
			{
				// Token: 0x0600020C RID: 524 RVA: 0x0000C830 File Offset: 0x0000AA30
				public override long ToLong(DvTimeSpan val)
				{
					return val.Ticks.RawValue;
				}

				// Token: 0x0600020D RID: 525 RVA: 0x0000C84C File Offset: 0x0000AA4C
				public override DvTimeSpan FromLong(long val)
				{
					return new DvTimeSpan(val);
				}
			}

			// Token: 0x02000075 RID: 117
			private sealed class DTConverter : NAReplaceTransform.Long.Converter<DvDateTime>
			{
				// Token: 0x0600020F RID: 527 RVA: 0x0000C864 File Offset: 0x0000AA64
				public override long ToLong(DvDateTime val)
				{
					return val.Ticks.RawValue;
				}

				// Token: 0x06000210 RID: 528 RVA: 0x0000C880 File Offset: 0x0000AA80
				public override DvDateTime FromLong(long val)
				{
					return new DvDateTime(val);
				}
			}
		}
	}
}
