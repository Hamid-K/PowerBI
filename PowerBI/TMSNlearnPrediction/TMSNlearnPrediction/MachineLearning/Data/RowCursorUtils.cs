using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x02000231 RID: 561
	public static class RowCursorUtils
	{
		// Token: 0x06000C99 RID: 3225 RVA: 0x000447BC File Offset: 0x000429BC
		public static Delegate GetGetterAsDelegate(IRow row, int col)
		{
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckParam(0 <= col && col < row.Schema.ColumnCount, "col");
			Contracts.CheckParam(row.IsColumnActive(col), "col", "column was not active");
			Func<IRow, int, Delegate> func = new Func<IRow, int, Delegate>(RowCursorUtils.GetGetterAsDelegateCore<int>);
			return Utils.MarshalInvoke<IRow, int, Delegate>(func, row.Schema.GetColumnType(col).RawType, row, col);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0004482F File Offset: 0x00042A2F
		private static Delegate GetGetterAsDelegateCore<TValue>(IRow row, int col)
		{
			return row.GetGetter<TValue>(col);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00044838 File Offset: 0x00042A38
		public static Delegate GetGetterAs(ColumnType typeDst, IRow row, int col)
		{
			Contracts.CheckValue<ColumnType>(typeDst, "typeDst");
			Contracts.CheckParam(typeDst.IsPrimitive, "typeDst");
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckParam(0 <= col && col < row.Schema.ColumnCount, "col");
			Contracts.CheckParam(row.IsColumnActive(col), "col", "column was not active");
			ColumnType columnType = row.Schema.GetColumnType(col);
			Contracts.Check(columnType.IsPrimitive, "Source column type must be primitive");
			Func<ColumnType, ColumnType, IRow, int, ValueGetter<int>> func = new Func<ColumnType, ColumnType, IRow, int, ValueGetter<int>>(RowCursorUtils.GetGetterAsCore<int, int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[] { columnType.RawType, typeDst.RawType });
			return (Delegate)methodInfo.Invoke(null, new object[] { columnType, typeDst, row, col });
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00044924 File Offset: 0x00042B24
		public static ValueGetter<TDst> GetGetterAs<TDst>(ColumnType typeDst, IRow row, int col)
		{
			Contracts.CheckValue<ColumnType>(typeDst, "typeDst");
			Contracts.CheckParam(typeDst.IsPrimitive, "typeDst");
			Contracts.CheckParam(typeDst.RawType == typeof(TDst), "typeDst");
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckParam(0 <= col && col < row.Schema.ColumnCount, "col");
			Contracts.CheckParam(row.IsColumnActive(col), "col", "column was not active");
			ColumnType columnType = row.Schema.GetColumnType(col);
			Contracts.Check(columnType.IsPrimitive, "Source column type must be primitive");
			Func<ColumnType, ColumnType, IRow, int, ValueGetter<TDst>> func = new Func<ColumnType, ColumnType, IRow, int, ValueGetter<TDst>>(RowCursorUtils.GetGetterAsCore<int, TDst>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
			{
				columnType.RawType,
				typeof(TDst)
			});
			return (ValueGetter<TDst>)methodInfo.Invoke(null, new object[] { columnType, typeDst, row, col });
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x00044A60 File Offset: 0x00042C60
		private static ValueGetter<TDst> GetGetterAsCore<TSrc, TDst>(ColumnType typeSrc, ColumnType typeDst, IRow row, int col)
		{
			ValueGetter<TSrc> getter = row.GetGetter<TSrc>(col);
			bool flag;
			ValueMapper<TSrc, TDst> conv = Conversions.Instance.GetStandardConversion<TSrc, TDst>(typeSrc, typeDst, out flag);
			if (flag)
			{
				return (ValueGetter<TDst>)getter;
			}
			TSrc src = default(TSrc);
			return delegate(ref TDst dst)
			{
				getter.Invoke(ref src);
				conv.Invoke(ref src, ref dst);
			};
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00044ABC File Offset: 0x00042CBC
		public static ValueGetter<StringBuilder> GetGetterAsStringBuilder(IRow row, int col)
		{
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckParam(0 <= col && col < row.Schema.ColumnCount, "col");
			Contracts.CheckParam(row.IsColumnActive(col), "col", "column was not active");
			ColumnType columnType = row.Schema.GetColumnType(col);
			Contracts.Check(columnType.IsPrimitive, "Source column type must be primitive");
			return Utils.MarshalInvoke<ColumnType, IRow, int, ValueGetter<StringBuilder>>(new Func<ColumnType, IRow, int, ValueGetter<StringBuilder>>(RowCursorUtils.GetGetterAsStringBuilderCore<int>), columnType.RawType, columnType, row, col);
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00044B70 File Offset: 0x00042D70
		private static ValueGetter<StringBuilder> GetGetterAsStringBuilderCore<TSrc>(ColumnType typeSrc, IRow row, int col)
		{
			ValueGetter<TSrc> getter = row.GetGetter<TSrc>(col);
			ValueMapper<TSrc, StringBuilder> conv = Conversions.Instance.GetStringConversion<TSrc>(typeSrc);
			TSrc src = default(TSrc);
			return delegate(ref StringBuilder dst)
			{
				getter.Invoke(ref src);
				conv.Invoke(ref src, ref dst);
			};
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00044BBC File Offset: 0x00042DBC
		public static Delegate GetVecGetterAs(PrimitiveType typeDst, IRow row, int col)
		{
			Contracts.CheckValue<PrimitiveType>(typeDst, "typeDst");
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckParam(0 <= col && col < row.Schema.ColumnCount, "col");
			Contracts.CheckParam(row.IsColumnActive(col), "col", "column was not active");
			ColumnType columnType = row.Schema.GetColumnType(col);
			Contracts.Check(columnType.IsVector, "Source column type must be vector");
			Func<VectorType, PrimitiveType, RowCursorUtils.GetterFactory, ValueGetter<VBuffer<int>>> func = new Func<VectorType, PrimitiveType, RowCursorUtils.GetterFactory, ValueGetter<VBuffer<int>>>(RowCursorUtils.GetVecGetterAsCore<int, int>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
			{
				columnType.ItemType.RawType,
				typeDst.RawType
			});
			return (Delegate)methodInfo.Invoke(null, new object[]
			{
				columnType,
				typeDst,
				RowCursorUtils.GetterFactory.Create(row, col)
			});
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x00044C9C File Offset: 0x00042E9C
		public static ValueGetter<VBuffer<TDst>> GetVecGetterAs<TDst>(PrimitiveType typeDst, IRow row, int col)
		{
			Contracts.CheckValue<PrimitiveType>(typeDst, "typeDst");
			Contracts.CheckParam(typeDst.RawType == typeof(TDst), "typeDst");
			Contracts.CheckValue<IRow>(row, "row");
			Contracts.CheckParam(0 <= col && col < row.Schema.ColumnCount, "col");
			Contracts.CheckParam(row.IsColumnActive(col), "col", "column was not active");
			ColumnType columnType = row.Schema.GetColumnType(col);
			Contracts.Check(columnType.IsVector, "Source column type must be vector");
			Func<VectorType, PrimitiveType, RowCursorUtils.GetterFactory, ValueGetter<VBuffer<TDst>>> func = new Func<VectorType, PrimitiveType, RowCursorUtils.GetterFactory, ValueGetter<VBuffer<TDst>>>(RowCursorUtils.GetVecGetterAsCore<int, TDst>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
			{
				columnType.ItemType.RawType,
				typeof(TDst)
			});
			return (ValueGetter<VBuffer<TDst>>)methodInfo.Invoke(null, new object[]
			{
				columnType,
				typeDst,
				RowCursorUtils.GetterFactory.Create(row, col)
			});
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x00044D9C File Offset: 0x00042F9C
		public static ValueGetter<VBuffer<TDst>> GetVecGetterAs<TDst>(PrimitiveType typeDst, ISlotCursor cursor)
		{
			Contracts.CheckValue<PrimitiveType>(typeDst, "typeDst");
			Contracts.CheckParam(typeDst.RawType == typeof(TDst), "typeDst");
			VectorType slotType = cursor.GetSlotType();
			Func<VectorType, PrimitiveType, RowCursorUtils.GetterFactory, ValueGetter<VBuffer<TDst>>> func = new Func<VectorType, PrimitiveType, RowCursorUtils.GetterFactory, ValueGetter<VBuffer<TDst>>>(RowCursorUtils.GetVecGetterAsCore<int, TDst>);
			MethodInfo methodInfo = func.GetMethodInfo().GetGenericMethodDefinition().MakeGenericMethod(new Type[]
			{
				slotType.ItemType.RawType,
				typeof(TDst)
			});
			return (ValueGetter<VBuffer<TDst>>)methodInfo.Invoke(null, new object[]
			{
				slotType,
				typeDst,
				RowCursorUtils.GetterFactory.Create(cursor)
			});
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x00044F34 File Offset: 0x00043134
		private static ValueGetter<VBuffer<TDst>> GetVecGetterAsCore<TSrc, TDst>(VectorType typeSrc, PrimitiveType typeDst, RowCursorUtils.GetterFactory getterFact)
		{
			ValueGetter<VBuffer<TSrc>> getter = getterFact.GetGetter<VBuffer<TSrc>>();
			bool flag;
			ValueMapper<TSrc, TDst> conv = Conversions.Instance.GetStandardConversion<TSrc, TDst>(typeSrc.ItemType, typeDst, out flag);
			if (flag)
			{
				return (ValueGetter<VBuffer<TDst>>)getter;
			}
			int size = typeSrc.VectorSize;
			VBuffer<TSrc> src = default(VBuffer<TSrc>);
			return delegate(ref VBuffer<TDst> dst)
			{
				getter.Invoke(ref src);
				if (size > 0)
				{
					Contracts.Check(src.Length == size);
				}
				TDst[] array = dst.Values;
				int[] array2 = dst.Indices;
				int count = src.Count;
				if (count > 0)
				{
					if (Utils.Size<TDst>(array) < count)
					{
						array = new TDst[count];
					}
					for (int i = 0; i < count; i++)
					{
						conv.Invoke(ref src.Values[i], ref array[i]);
					}
					if (!src.IsDense)
					{
						if (Utils.Size<int>(array2) < count)
						{
							array2 = new int[count];
						}
						Array.Copy(src.Indices, array2, count);
					}
				}
				dst = new VBuffer<TDst>(src.Length, count, array, array2);
			};
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x00044FA0 File Offset: 0x000431A0
		public static Func<bool> GetIsNewGroupDelegate(IRow cursor, int col)
		{
			Contracts.CheckValue<IRow>(cursor, "cursor");
			Contracts.Check(0 <= col && col < cursor.Schema.ColumnCount);
			ColumnType columnType = cursor.Schema.GetColumnType(col);
			Contracts.Check(columnType.IsKey);
			return Utils.MarshalInvoke<IRow, int, Func<bool>>(new Func<IRow, int, Func<bool>>(RowCursorUtils.GetIsNewGroupDelegateCore<int>), columnType.RawType, cursor, col);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x00045070 File Offset: 0x00043270
		private static Func<bool> GetIsNewGroupDelegateCore<T>(IRow cursor, int col)
		{
			ValueGetter<T> getter = cursor.GetGetter<T>(col);
			bool first = true;
			T old = default(T);
			T val = default(T);
			EqualityComparer<T> compare = EqualityComparer<T>.Default;
			return delegate
			{
				getter.Invoke(ref val);
				if (first)
				{
					first = false;
					old = val;
					return true;
				}
				if (compare.Equals(val, old))
				{
					return false;
				}
				old = val;
				return true;
			};
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000450C6 File Offset: 0x000432C6
		public static string TestGetLabelGetter(ColumnType type)
		{
			return RowCursorUtils.TestGetLabelGetter(type, true);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000450CF File Offset: 0x000432CF
		public static string TestGetLabelGetter(ColumnType type, bool allowKeys)
		{
			if (type == NumberType.R4 || type == NumberType.R8 || type.IsBool)
			{
				return null;
			}
			if (allowKeys && type.IsKey)
			{
				return null;
			}
			if (!allowKeys)
			{
				return "Expected R4, R8 or Bool type";
			}
			return "Expected R4, R8, Bool or Key type";
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0004513C File Offset: 0x0004333C
		public static ValueGetter<float> GetLabelGetter(IRow cursor, int labelIndex)
		{
			ColumnType columnType = cursor.Schema.GetColumnType(labelIndex);
			if (columnType == NumberType.R4)
			{
				return cursor.GetGetter<float>(labelIndex);
			}
			if (columnType == NumberType.R8)
			{
				ValueGetter<double> getSingleSrc = cursor.GetGetter<double>(labelIndex);
				return delegate(ref float dst)
				{
					double naN = double.NaN;
					getSingleSrc.Invoke(ref naN);
					dst = Convert.ToSingle(naN);
				};
			}
			return RowCursorUtils.GetLabelGetterNotFloat(cursor, labelIndex);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x00045210 File Offset: 0x00043410
		private static ValueGetter<float> GetLabelGetterNotFloat(IRow cursor, int labelIndex)
		{
			ColumnType columnType = cursor.Schema.GetColumnType(labelIndex);
			if (columnType.IsBool)
			{
				ValueGetter<DvBool> getBoolSrc = cursor.GetGetter<DvBool>(labelIndex);
				return delegate(ref float dst)
				{
					DvBool na = DvBool.NA;
					getBoolSrc.Invoke(ref na);
					dst = (float)na;
				};
			}
			Contracts.Check(columnType.IsKey, "Only floating point number, boolean, and key type values can be used as label.");
			ulong keyMax = (ulong)((long)columnType.KeyCount);
			if (keyMax == 0UL)
			{
				keyMax = ulong.MaxValue;
			}
			ValueGetter<ulong> getSrc = RowCursorUtils.GetGetterAs<ulong>(NumberType.U8, cursor, labelIndex);
			return delegate(ref float dst)
			{
				ulong num = 0UL;
				getSrc.Invoke(ref num);
				if (0UL < num && num <= keyMax)
				{
					dst = num - 1UL;
					return;
				}
				dst = float.NaN;
			};
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00045388 File Offset: 0x00043588
		public static ValueGetter<VBuffer<float>> GetLabelGetter(ISlotCursor cursor)
		{
			PrimitiveType itemType = cursor.GetSlotType().ItemType;
			if (itemType == NumberType.R4)
			{
				return cursor.GetGetter<float>();
			}
			if (itemType == NumberType.R8 || itemType.IsBool)
			{
				return RowCursorUtils.GetVecGetterAs<float>(NumberType.R4, cursor);
			}
			Contracts.Check(itemType.IsKey, "Only floating point number, boolean, and key type values can be used as label.");
			ulong keyMax = (ulong)((long)itemType.KeyCount);
			if (keyMax == 0UL)
			{
				keyMax = ulong.MaxValue;
			}
			ValueGetter<VBuffer<ulong>> getSrc = RowCursorUtils.GetVecGetterAs<ulong>(NumberType.U8, cursor);
			VBuffer<ulong> src = default(VBuffer<ulong>);
			return delegate(ref VBuffer<float> dst)
			{
				getSrc.Invoke(ref src);
				float[] values = dst.Values;
				Utils.EnsureSize<float>(ref values, src.Length, true);
				foreach (KeyValuePair<int, ulong> keyValuePair in src.Items(true))
				{
					if (0UL < keyValuePair.Value && keyValuePair.Value <= keyMax)
					{
						values[keyValuePair.Key] = keyValuePair.Value - 1UL;
					}
					else
					{
						values[keyValuePair.Key] = float.NaN;
					}
				}
				dst = new VBuffer<float>(src.Length, values, dst.Indices);
			};
		}

		// Token: 0x02000232 RID: 562
		private abstract class GetterFactory
		{
			// Token: 0x06000CAB RID: 3243 RVA: 0x0004542A File Offset: 0x0004362A
			public static RowCursorUtils.GetterFactory Create(IRow row, int col)
			{
				return new RowCursorUtils.GetterFactory.RowImpl(row, col);
			}

			// Token: 0x06000CAC RID: 3244 RVA: 0x00045433 File Offset: 0x00043633
			public static RowCursorUtils.GetterFactory Create(ISlotCursor cursor)
			{
				return new RowCursorUtils.GetterFactory.SlotImpl(cursor);
			}

			// Token: 0x06000CAD RID: 3245
			public abstract ValueGetter<TValue> GetGetter<TValue>();

			// Token: 0x02000233 RID: 563
			private sealed class RowImpl : RowCursorUtils.GetterFactory
			{
				// Token: 0x06000CAF RID: 3247 RVA: 0x00045443 File Offset: 0x00043643
				public RowImpl(IRow row, int col)
				{
					this._row = row;
					this._col = col;
				}

				// Token: 0x06000CB0 RID: 3248 RVA: 0x00045459 File Offset: 0x00043659
				public override ValueGetter<TValue> GetGetter<TValue>()
				{
					return this._row.GetGetter<TValue>(this._col);
				}

				// Token: 0x040006ED RID: 1773
				private readonly IRow _row;

				// Token: 0x040006EE RID: 1774
				private readonly int _col;
			}

			// Token: 0x02000234 RID: 564
			private sealed class SlotImpl : RowCursorUtils.GetterFactory
			{
				// Token: 0x06000CB1 RID: 3249 RVA: 0x0004546C File Offset: 0x0004366C
				public SlotImpl(ISlotCursor cursor)
				{
					this._cursor = cursor;
				}

				// Token: 0x06000CB2 RID: 3250 RVA: 0x0004547B File Offset: 0x0004367B
				public override ValueGetter<TValue> GetGetter<TValue>()
				{
					return this._cursor.GetGetterWithVectorType(null);
				}

				// Token: 0x040006EF RID: 1775
				private readonly ISlotCursor _cursor;
			}
		}
	}
}
