using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.MachineLearning.Data.Conversion;
using Microsoft.MachineLearning.Internal.Utilities;

namespace Microsoft.MachineLearning.Data
{
	// Token: 0x020001B9 RID: 441
	internal static class InvertHashUtils
	{
		// Token: 0x060009E1 RID: 2529 RVA: 0x00034B91 File Offset: 0x00032D91
		private static void ClearDst(ref StringBuilder dst)
		{
			if (dst == null)
			{
				dst = new StringBuilder();
				return;
			}
			dst.Clear();
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00034BF8 File Offset: 0x00032DF8
		public static ValueMapper<T, StringBuilder> GetSimpleMapper<T>(ISchema schema, int col)
		{
			ColumnType itemType = schema.GetColumnType(col).ItemType;
			Conversions instance = Conversions.Instance;
			if (!itemType.IsKey)
			{
				return instance.GetStringConversion<T>(itemType);
			}
			if (MetadataUtils.HasKeyNames(schema, col, itemType.KeyCount))
			{
				VBuffer<DvText> keyValues = default(VBuffer<DvText>);
				schema.GetMetadata<VBuffer<DvText>>("KeyValues", col, ref keyValues);
				DvText value = default(DvText);
				bool flag;
				ValueMapper<T, uint> keyMapper = instance.GetStandardConversion<T, uint>(itemType, NumberType.U4, out flag);
				return delegate(ref T src, ref StringBuilder dst)
				{
					InvertHashUtils.ClearDst(ref dst);
					uint num = 0U;
					keyMapper.Invoke(ref src, ref num);
					if (num == 0U)
					{
						return;
					}
					keyValues.GetItemOrDefault((int)(num - 1U), ref value);
					value.AddToStringBuilder(dst);
				};
			}
			return instance.GetKeyStringConversion<T>(itemType.AsKey);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00034CF4 File Offset: 0x00032EF4
		public static ValueMapper<KeyValuePair<int, T>, StringBuilder> GetPairMapper<T>(ValueMapper<T, StringBuilder> submap)
		{
			StringBuilder sb = null;
			char[] buffer = null;
			return delegate(ref KeyValuePair<int, T> pair, ref StringBuilder dst)
			{
				InvertHashUtils.ClearDst(ref dst);
				dst.Append(pair.Key);
				dst.Append(':');
				T value = pair.Value;
				submap.Invoke(ref value, ref sb);
				InvertHashUtils.AppendToEnd(sb, dst, ref buffer);
			};
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00034D28 File Offset: 0x00032F28
		public static void AppendToEnd(StringBuilder src, StringBuilder dst, ref char[] buffer)
		{
			if (Utils.Size(src) > 0)
			{
				Utils.EnsureSize<char>(ref buffer, src.Length, true);
				src.CopyTo(0, buffer, 0, src.Length);
				dst.Append(buffer, 0, src.Length);
			}
		}
	}
}
