using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x0200011B RID: 283
	public static class SegmentedStringExtensions
	{
		// Token: 0x060004C6 RID: 1222 RVA: 0x00006EDC File Offset: 0x000050DC
		public static int CompareOrdinal(this SegmentedString str1, int offset1, SegmentedString str2, int offset2, int length)
		{
			if (str1.Length == 0)
			{
				return (str2.Length != 0) ? 1 : 0;
			}
			if (str2.Length == 0)
			{
				if (str1.Length != 0)
				{
					return -1;
				}
				return 0;
			}
			else
			{
				if (offset1 + length > str1.Length || offset2 + length > str2.Length)
				{
					throw new IndexOutOfRangeException();
				}
				int num = offset1 >> 15;
				int num2 = offset2 >> 15;
				int num3 = offset1 & 32767;
				int num4 = offset2 & 32767;
				int i = length;
				while (i > 0)
				{
					StringSegment segment = str1.GetSegment(num);
					StringSegment segment2 = str2.GetSegment(num2);
					int num5 = Math.Min(i, Math.Min(segment.Length - num3, segment2.Length - num4));
					int num6 = string.CompareOrdinal(segment.String, segment.Offset + num3, segment2.String, segment2.Offset + num4, num5);
					if (num6 != 0)
					{
						return num6;
					}
					i -= num5;
					num3 += num5;
					num4 += num5;
					if (num3 == segment.Length)
					{
						num++;
						num3 = 0;
					}
					if (num4 == segment2.Length)
					{
						num2++;
						num4 = 0;
					}
				}
				return 0;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x00006FF4 File Offset: 0x000051F4
		public static StringSegment GetSubstringSegment(this SegmentedString segmentedString, int offset, int length)
		{
			StringSegment stringSegment;
			using (IEnumerator<StringSegment> enumerator = segmentedString.GetSubstringSegments(offset, length).GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					stringSegment = new StringSegment(string.Empty);
				}
				else
				{
					StringSegment stringSegment2 = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						stringSegment = new StringSegment(stringSegment2.String, stringSegment2.Offset, stringSegment2.Length);
					}
					else
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append(stringSegment2.String, stringSegment2.Offset, stringSegment2.Length);
						do
						{
							StringBuilder stringBuilder2 = stringBuilder;
							StringSegment stringSegment3 = enumerator.Current;
							string @string = stringSegment3.String;
							stringSegment3 = enumerator.Current;
							int offset2 = stringSegment3.Offset;
							stringSegment3 = enumerator.Current;
							stringBuilder2.Append(@string, offset2, stringSegment3.Length);
						}
						while (enumerator.MoveNext());
						stringSegment = new StringSegment(stringBuilder.ToString());
					}
				}
			}
			return stringSegment;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000070DC File Offset: 0x000052DC
		public static IEnumerable<StringSegment> GetSubstringSegments(this SegmentedString segmentedString, int offset, int length)
		{
			if (length == 0)
			{
				yield break;
			}
			int segmentIndex = offset >> 15;
			int num = offset & 32767;
			int lastSegmentIndex = offset + length >> 15;
			StringSegment segment = segmentedString.GetSegment(segmentIndex);
			if (lastSegmentIndex == segmentIndex)
			{
				yield return new StringSegment(segment.String, segment.Offset + num, length);
				yield break;
			}
			yield return new StringSegment(segment.String, segment.Offset + num, segment.Length - num);
			int num2 = segmentIndex + 1;
			segmentIndex = num2;
			while (segmentIndex < lastSegmentIndex)
			{
				num2 = segmentIndex;
				segmentIndex = num2 + 1;
				StringSegment segment2 = segmentedString.GetSegment(num2);
				yield return new StringSegment(segment2.String, segment2.Offset, segment2.Length);
			}
			int num3 = (offset + length) & 32767;
			if (num3 > 0)
			{
				StringSegment segment3 = segmentedString.GetSegment(segmentIndex);
				yield return new StringSegment(segment3.String, segment3.Offset, num3);
			}
			yield break;
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x000070FC File Offset: 0x000052FC
		public static int OrdinalIndexOf(this SegmentedString segmentedString, string value, int startIndex)
		{
			for (int i = startIndex; i < segmentedString.Length; i++)
			{
				bool flag = true;
				for (int j = 0; j < value.Length; j++)
				{
					if (segmentedString[i + j] != value[j])
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0000714C File Offset: 0x0000534C
		public static SegmentedString Remove(this SegmentedString segmentedString, int startOffset, int length)
		{
			SegmentedStringBuilder segmentedStringBuilder = SegmentedStringBuilder.New();
			segmentedStringBuilder.Append(segmentedString, 0, startOffset);
			segmentedStringBuilder.Append(segmentedString, startOffset + length);
			return segmentedStringBuilder.ToSegmentedString();
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x00007180 File Offset: 0x00005380
		public static SegmentedString Substring(this SegmentedString segmentedString, int offset)
		{
			SegmentedStringBuilder segmentedStringBuilder = SegmentedStringBuilder.New();
			segmentedStringBuilder.Append(segmentedString, offset);
			return segmentedStringBuilder.ToSegmentedString();
		}

		// Token: 0x060004CC RID: 1228 RVA: 0x000071A4 File Offset: 0x000053A4
		public static SegmentedString Substring(this SegmentedString segmentedString, int offset, int length)
		{
			SegmentedStringBuilder segmentedStringBuilder = SegmentedStringBuilder.New();
			segmentedStringBuilder.Append(segmentedString, offset, length);
			return segmentedStringBuilder.ToSegmentedString();
		}
	}
}
