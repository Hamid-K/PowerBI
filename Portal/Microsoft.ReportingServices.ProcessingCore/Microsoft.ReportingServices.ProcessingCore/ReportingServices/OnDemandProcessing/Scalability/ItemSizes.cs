using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace Microsoft.ReportingServices.OnDemandProcessing.Scalability
{
	// Token: 0x02000845 RID: 2117
	internal static class ItemSizes
	{
		// Token: 0x06007641 RID: 30273 RVA: 0x001EA1F8 File Offset: 0x001E83F8
		public static int PointerAlign(int size)
		{
			int num = size % ItemSizes.ReferenceSize;
			if (num > 0)
			{
				return size - num + ItemSizes.ReferenceSize;
			}
			return size;
		}

		// Token: 0x06007642 RID: 30274 RVA: 0x001EA21C File Offset: 0x001E841C
		public static int SizeOf(IStorable obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += ItemSizes.ObjectOverhead + obj.Size;
			}
			return num;
		}

		// Token: 0x06007643 RID: 30275 RVA: 0x001EA242 File Offset: 0x001E8442
		public static int SizeOf<T>(ScalableList<T> obj)
		{
			return ItemSizes.SizeOf(obj);
		}

		// Token: 0x06007644 RID: 30276 RVA: 0x001EA24C File Offset: 0x001E844C
		public static int SizeOf<T>(List<T> list) where T : IStorable
		{
			int num = ItemSizes.ReferenceSize;
			if (list != null)
			{
				num += 24;
				for (int i = 0; i < list.Count; i++)
				{
					num += ItemSizes.SizeOf(list[i]);
				}
			}
			return num;
		}

		// Token: 0x06007645 RID: 30277 RVA: 0x001EA28D File Offset: 0x001E848D
		public static int SizeOfEmptyObjectArray(int length)
		{
			return ItemSizes.ReferenceSize + 8 + ItemSizes.ReferenceSize * length;
		}

		// Token: 0x06007646 RID: 30278 RVA: 0x001EA2A0 File Offset: 0x001E84A0
		public static int SizeOf<T>(List<List<T>> listOfLists) where T : IStorable
		{
			int num = ItemSizes.ReferenceSize;
			if (listOfLists != null)
			{
				num += 24;
				for (int i = 0; i < listOfLists.Count; i++)
				{
					num += ItemSizes.SizeOf<T>(listOfLists[i]);
				}
			}
			return num;
		}

		// Token: 0x06007647 RID: 30279 RVA: 0x001EA2DC File Offset: 0x001E84DC
		public static int SizeOf(List<object> list)
		{
			int num = ItemSizes.ReferenceSize;
			if (list != null)
			{
				num += 24;
				for (int i = 0; i < list.Count; i++)
				{
					num += ItemSizes.SizeOf(list[i]);
				}
			}
			return num;
		}

		// Token: 0x06007648 RID: 30280 RVA: 0x001EA318 File Offset: 0x001E8518
		public static int SizeOf<T>(T[] array) where T : IStorable
		{
			int num = ItemSizes.ReferenceSize;
			if (array != null)
			{
				num += 8;
				for (int i = 0; i < array.Length; i++)
				{
					num += ItemSizes.SizeOf(array[i]);
				}
			}
			return num;
		}

		// Token: 0x06007649 RID: 30281 RVA: 0x001EA358 File Offset: 0x001E8558
		public static int SizeOf(object[] array)
		{
			int num = ItemSizes.ReferenceSize;
			if (array != null)
			{
				num += 8;
				for (int i = 0; i < array.Length; i++)
				{
					num += ItemSizes.SizeOf(array[i]);
				}
			}
			return num;
		}

		// Token: 0x0600764A RID: 30282 RVA: 0x001EA38C File Offset: 0x001E858C
		public static int SizeOf(int[] obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 8;
				num += obj.Length * 4;
			}
			return num;
		}

		// Token: 0x0600764B RID: 30283 RVA: 0x001EA3B0 File Offset: 0x001E85B0
		public static int SizeOf(long[] obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 8;
				num += obj.Length * 8;
			}
			return num;
		}

		// Token: 0x0600764C RID: 30284 RVA: 0x001EA3D4 File Offset: 0x001E85D4
		public static int SizeOf(double[] obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 8;
				num += obj.Length * 8;
			}
			return num;
		}

		// Token: 0x0600764D RID: 30285 RVA: 0x001EA3F8 File Offset: 0x001E85F8
		public static int SizeOf(bool[] obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 8;
				num += obj.Length;
			}
			return num;
		}

		// Token: 0x0600764E RID: 30286 RVA: 0x001EA41C File Offset: 0x001E861C
		public static int SizeOf(string[] obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 8;
				for (int i = 0; i < obj.Length; i++)
				{
					num += ItemSizes.SizeOf(obj[i]);
				}
			}
			return num;
		}

		// Token: 0x0600764F RID: 30287 RVA: 0x001EA450 File Offset: 0x001E8650
		public static int SizeOf(Array arr)
		{
			int num = ItemSizes.ReferenceSize;
			if (arr != null)
			{
				num += 8;
				int[] array = new int[arr.Rank];
				num += ItemSizes.TraverseArrayDim(arr, 0, array);
			}
			return num;
		}

		// Token: 0x06007650 RID: 30288 RVA: 0x001EA484 File Offset: 0x001E8684
		private static int TraverseArrayDim(Array arr, int dim, int[] indices)
		{
			int num = 0;
			bool flag = arr.Rank == dim + 1;
			int length = arr.GetLength(dim);
			for (int i = 0; i < length; i++)
			{
				indices[dim] = i;
				if (flag)
				{
					num += ItemSizes.SizeOf(arr.GetValue(indices));
				}
				else
				{
					num += ItemSizes.TraverseArrayDim(arr, dim + 1, indices);
				}
			}
			return num;
		}

		// Token: 0x06007651 RID: 30289 RVA: 0x001EA4DC File Offset: 0x001E86DC
		public static int SizeOf(List<string> obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 24;
				for (int i = 0; i < obj.Count; i++)
				{
					num += ItemSizes.SizeOf(obj[i]);
				}
			}
			return num;
		}

		// Token: 0x06007652 RID: 30290 RVA: 0x001EA518 File Offset: 0x001E8718
		public static int SizeOf(List<int> obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 24;
				num += obj.Count * 4;
			}
			return num;
		}

		// Token: 0x06007653 RID: 30291 RVA: 0x001EA540 File Offset: 0x001E8740
		public static int SizeOf(List<object>[] obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 8;
				for (int i = 0; i < obj.Length; i++)
				{
					num += ItemSizes.SizeOf(obj[i]);
				}
			}
			return num;
		}

		// Token: 0x06007654 RID: 30292 RVA: 0x001EA574 File Offset: 0x001E8774
		public static int SizeOf<T>(List<T>[] obj) where T : IStorable
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 8;
				for (int i = 0; i < obj.Length; i++)
				{
					num += ItemSizes.SizeOf<T>(obj[i]);
				}
			}
			return num;
		}

		// Token: 0x06007655 RID: 30293 RVA: 0x001EA5A8 File Offset: 0x001E87A8
		public static int SizeOf<T>(List<T[]> obj) where T : IStorable
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 24;
				for (int i = 0; i < obj.Count; i++)
				{
					num += ItemSizes.SizeOf<T>(obj[i]);
				}
			}
			return num;
		}

		// Token: 0x06007656 RID: 30294 RVA: 0x001EA5E4 File Offset: 0x001E87E4
		public static int SizeOf(Hashtable obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 56;
				foreach (object obj2 in obj)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj2;
					num += 4;
					num += ItemSizes.SizeOf(dictionaryEntry.Key);
					num += ItemSizes.SizeOf(dictionaryEntry.Value);
				}
			}
			return num;
		}

		// Token: 0x06007657 RID: 30295 RVA: 0x001EA664 File Offset: 0x001E8864
		public static int SizeOf<K, V>(Dictionary<K, V> obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 56;
				foreach (KeyValuePair<K, V> keyValuePair in obj)
				{
					num += 4;
					num += ItemSizes.SizeOf(keyValuePair.Key);
					num += ItemSizes.SizeOf(keyValuePair.Value);
				}
			}
			return num;
		}

		// Token: 0x06007658 RID: 30296 RVA: 0x001EA6E8 File Offset: 0x001E88E8
		public static int SizeOf(IList obj)
		{
			int num = ItemSizes.ReferenceSize;
			if (obj != null)
			{
				num += 24;
				for (int i = 0; i < obj.Count; i++)
				{
					num += ItemSizes.SizeOf(obj[i]);
				}
			}
			return num;
		}

		// Token: 0x06007659 RID: 30297 RVA: 0x001EA724 File Offset: 0x001E8924
		public static int SizeOf(string str)
		{
			int num = ItemSizes.ReferenceSize;
			if (str != null)
			{
				num += ItemSizes.ObjectOverhead + 4 + 4 + str.Length * 2;
			}
			return num;
		}

		// Token: 0x0600765A RID: 30298 RVA: 0x001EA750 File Offset: 0x001E8950
		public static int SizeOfInObjectArray(object obj)
		{
			return ItemSizes.SizeOf(obj) - ItemSizes.ReferenceSize;
		}

		// Token: 0x0600765B RID: 30299 RVA: 0x001EA760 File Offset: 0x001E8960
		public static int SizeOf(object obj)
		{
			if (obj == null)
			{
				return ItemSizes.ReferenceSize;
			}
			if (obj is IStorable)
			{
				return ItemSizes.SizeOf((IStorable)obj);
			}
			if (obj is IConvertible)
			{
				switch (((IConvertible)obj).GetTypeCode())
				{
				case TypeCode.Object:
					if (obj is TimeSpan)
					{
						return 8 + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is DateTimeOffset)
					{
						return 16 + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is Guid)
					{
						return 16 + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is Color)
					{
						return ItemSizes.GdiColorSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					return ItemSizes.ReferenceSize;
				case TypeCode.Boolean:
				case TypeCode.Char:
				case TypeCode.SByte:
				case TypeCode.Byte:
				case TypeCode.Int16:
				case TypeCode.UInt16:
				case TypeCode.Int32:
				case TypeCode.UInt32:
				case TypeCode.Single:
					return ItemSizes.ReferenceSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
				case TypeCode.Int64:
				case TypeCode.UInt64:
				case TypeCode.Double:
					return 8 + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
				case TypeCode.Decimal:
					return 16 + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
				case TypeCode.DateTime:
					return 8 + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
				case TypeCode.String:
					return ItemSizes.SizeOf((string)obj);
				}
			}
			else
			{
				if (obj is Array)
				{
					return ItemSizes.SizeOf((Array)obj);
				}
				if (obj is IList)
				{
					return ItemSizes.SizeOf((IList)obj);
				}
				if (Nullable.GetUnderlyingType(obj.GetType()) != null)
				{
					if (obj is bool?)
					{
						return ItemSizes.NullableBoolSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is byte?)
					{
						return ItemSizes.NullableByteSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is sbyte?)
					{
						return ItemSizes.NullableByteSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is short?)
					{
						return ItemSizes.NullableInt16Size + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is int?)
					{
						return ItemSizes.NullableInt32Size + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is long?)
					{
						return ItemSizes.NullableInt64Size + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is ushort?)
					{
						return ItemSizes.NullableInt16Size + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is uint?)
					{
						return ItemSizes.NullableInt32Size + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is ulong?)
					{
						return ItemSizes.NullableInt64Size + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is char?)
					{
						return ItemSizes.NullableCharSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is double?)
					{
						return ItemSizes.NullableDoubleSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is float?)
					{
						return ItemSizes.NullableSingleSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is DateTime?)
					{
						return ItemSizes.NullableDateTimeSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is Guid?)
					{
						return ItemSizes.NullableGuidSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
					if (obj is TimeSpan?)
					{
						return ItemSizes.NullableTimeSpanSize + ItemSizes.ObjectOverhead + ItemSizes.ReferenceSize;
					}
				}
			}
			return ItemSizes.ReferenceSize;
		}

		// Token: 0x04003BCA RID: 15306
		public const int BoolSize = 1;

		// Token: 0x04003BCB RID: 15307
		public const int ByteSize = 1;

		// Token: 0x04003BCC RID: 15308
		public const int SByteSize = 1;

		// Token: 0x04003BCD RID: 15309
		public const int Int16Size = 2;

		// Token: 0x04003BCE RID: 15310
		public const int UInt16Size = 2;

		// Token: 0x04003BCF RID: 15311
		public const int Int32Size = 4;

		// Token: 0x04003BD0 RID: 15312
		public const int UInt32Size = 4;

		// Token: 0x04003BD1 RID: 15313
		public const int Int64Size = 8;

		// Token: 0x04003BD2 RID: 15314
		public const int UInt64Size = 8;

		// Token: 0x04003BD3 RID: 15315
		public const int CharSize = 2;

		// Token: 0x04003BD4 RID: 15316
		public const int DoubleSize = 8;

		// Token: 0x04003BD5 RID: 15317
		public const int SingleSize = 4;

		// Token: 0x04003BD6 RID: 15318
		public const int DecimalSize = 16;

		// Token: 0x04003BD7 RID: 15319
		public const int DateTimeSize = 8;

		// Token: 0x04003BD8 RID: 15320
		public const int TimeSpanSize = 8;

		// Token: 0x04003BD9 RID: 15321
		public const int DateTimeOffsetSize = 16;

		// Token: 0x04003BDA RID: 15322
		public const int GuidSize = 16;

		// Token: 0x04003BDB RID: 15323
		public static readonly int ReferenceSize = IntPtr.Size;

		// Token: 0x04003BDC RID: 15324
		public static readonly int NullableBoolSize = ItemSizes.PointerAlign(2);

		// Token: 0x04003BDD RID: 15325
		public static readonly int NullableByteSize = ItemSizes.PointerAlign(2);

		// Token: 0x04003BDE RID: 15326
		public static readonly int NullableSByteSize = ItemSizes.PointerAlign(2);

		// Token: 0x04003BDF RID: 15327
		public static readonly int NullableInt16Size = ItemSizes.PointerAlign(3);

		// Token: 0x04003BE0 RID: 15328
		public static readonly int NullableUInt16Size = ItemSizes.PointerAlign(3);

		// Token: 0x04003BE1 RID: 15329
		public static readonly int NullableInt32Size = ItemSizes.PointerAlign(5);

		// Token: 0x04003BE2 RID: 15330
		public static readonly int NullableUInt32Size = ItemSizes.PointerAlign(5);

		// Token: 0x04003BE3 RID: 15331
		public static readonly int NullableInt64Size = ItemSizes.PointerAlign(9);

		// Token: 0x04003BE4 RID: 15332
		public static readonly int NullableUInt64Size = ItemSizes.PointerAlign(9);

		// Token: 0x04003BE5 RID: 15333
		public static readonly int NullableCharSize = ItemSizes.PointerAlign(3);

		// Token: 0x04003BE6 RID: 15334
		public static readonly int NullableDoubleSize = ItemSizes.PointerAlign(9);

		// Token: 0x04003BE7 RID: 15335
		public static readonly int NullableSingleSize = ItemSizes.PointerAlign(5);

		// Token: 0x04003BE8 RID: 15336
		public static readonly int NullableDecimalSize = ItemSizes.PointerAlign(17);

		// Token: 0x04003BE9 RID: 15337
		public static readonly int NullableDateTimeSize = ItemSizes.PointerAlign(9);

		// Token: 0x04003BEA RID: 15338
		public static readonly int NullableGuidSize = ItemSizes.PointerAlign(17);

		// Token: 0x04003BEB RID: 15339
		public static readonly int NullableTimeSpanSize = ItemSizes.PointerAlign(9);

		// Token: 0x04003BEC RID: 15340
		public static readonly int GdiColorSize = 12 + ItemSizes.ReferenceSize;

		// Token: 0x04003BED RID: 15341
		public const int Int32EnumSize = 4;

		// Token: 0x04003BEE RID: 15342
		public const int NullableOverhead = 1;

		// Token: 0x04003BEF RID: 15343
		public const int ListOverhead = 24;

		// Token: 0x04003BF0 RID: 15344
		public const int ArrayOverhead = 8;

		// Token: 0x04003BF1 RID: 15345
		public const int HashtableOverhead = 56;

		// Token: 0x04003BF2 RID: 15346
		public const int HashtableEntryOverhead = 4;

		// Token: 0x04003BF3 RID: 15347
		public static readonly int ObjectOverhead = ItemSizes.ReferenceSize * 2;

		// Token: 0x04003BF4 RID: 15348
		public static readonly int NonNullIStorableOverhead = ItemSizes.ReferenceSize + ItemSizes.ObjectOverhead;
	}
}
