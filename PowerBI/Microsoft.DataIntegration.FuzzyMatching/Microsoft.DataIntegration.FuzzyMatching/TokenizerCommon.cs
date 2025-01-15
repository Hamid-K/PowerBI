using System;
using System.Data;
using Microsoft.DataIntegration.FuzzyMatchingCommon;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x020000E9 RID: 233
	public static class TokenizerCommon
	{
		// Token: 0x06000926 RID: 2342 RVA: 0x0002AB60 File Offset: 0x00028D60
		public static void GetString(IDataRecord record, int columnIndex, bool isString, ISegmentAllocator<char> allocator, out ArraySegment<char> stringSegment, ref bool providerConvertsStringToChar)
		{
			if (record.IsDBNull(columnIndex))
			{
				stringSegment = default(ArraySegment<char>);
				return;
			}
			if (providerConvertsStringToChar)
			{
				try
				{
					long chars = record.GetChars(columnIndex, 0L, null, 0, 0);
					if (chars > 0L)
					{
						stringSegment = allocator.New((int)chars);
						record.GetChars(columnIndex, 0L, stringSegment.Array, stringSegment.Offset, (int)chars);
					}
				}
				catch (InvalidCastException)
				{
					providerConvertsStringToChar = false;
				}
			}
			object obj = record[columnIndex];
			if (obj is string)
			{
				string text = obj as string;
				stringSegment = allocator.New(text.Length);
				text.CopyToEx(0, stringSegment.Array, stringSegment.Offset, text.Length);
				return;
			}
			if (obj is ArraySegment<char>)
			{
				stringSegment = ((ArraySegment<char>)obj).Clone(allocator);
				return;
			}
			string text2 = record[columnIndex].ToString();
			stringSegment = allocator.New(text2.Length);
			text2.CopyToEx(0, stringSegment.Array, stringSegment.Offset, text2.Length);
		}
	}
}
