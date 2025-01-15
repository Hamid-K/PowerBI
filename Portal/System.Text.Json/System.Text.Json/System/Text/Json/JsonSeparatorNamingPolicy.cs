using System;
using System.Buffers;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Text.Json
{
	// Token: 0x02000033 RID: 51
	internal abstract class JsonSeparatorNamingPolicy : JsonNamingPolicy
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000613C File Offset: 0x0000433C
		internal JsonSeparatorNamingPolicy(bool lowercase, char separator)
		{
			this._lowercase = lowercase;
			this._separator = separator;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00006152 File Offset: 0x00004352
		public sealed override string ConvertName(string name)
		{
			if (name == null)
			{
				ThrowHelper.ThrowArgumentNullException("name");
			}
			return JsonSeparatorNamingPolicy.ConvertNameCore(this._separator, this._lowercase, name.AsSpan());
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00006178 File Offset: 0x00004378
		private unsafe static string ConvertNameCore(char separator, bool lowercase, ReadOnlySpan<char> chars)
		{
			JsonSeparatorNamingPolicy.<>c__DisplayClass4_0 CS$<>8__locals1;
			CS$<>8__locals1.rentedBuffer = null;
			int num = (int)(1.2 * (double)chars.Length);
			Span<char> span2;
			if (num <= 128)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
				span2 = span;
			}
			else
			{
				span2 = (CS$<>8__locals1.rentedBuffer = ArrayPool<char>.Shared.Rent(num));
			}
			Span<char> span3 = span2;
			JsonSeparatorNamingPolicy.SeparatorState separatorState = JsonSeparatorNamingPolicy.SeparatorState.NotStarted;
			CS$<>8__locals1.charsWritten = 0;
			int i = 0;
			while (i < chars.Length)
			{
				char c = (char)(*chars[i]);
				UnicodeCategory unicodeCategory = char.GetUnicodeCategory(c);
				if (unicodeCategory <= UnicodeCategory.LowercaseLetter)
				{
					if (unicodeCategory != UnicodeCategory.UppercaseLetter)
					{
						if (unicodeCategory != UnicodeCategory.LowercaseLetter)
						{
							goto IL_014D;
						}
						goto IL_0118;
					}
					else
					{
						switch (separatorState)
						{
						case JsonSeparatorNamingPolicy.SeparatorState.UppercaseLetter:
							if (i + 1 < chars.Length && char.IsLower((char)(*chars[i + 1])))
							{
								JsonSeparatorNamingPolicy.<ConvertNameCore>g__WriteChar|4_0(separator, ref span3, ref CS$<>8__locals1);
							}
							break;
						case JsonSeparatorNamingPolicy.SeparatorState.LowercaseLetterOrDigit:
						case JsonSeparatorNamingPolicy.SeparatorState.SpaceSeparator:
							JsonSeparatorNamingPolicy.<ConvertNameCore>g__WriteChar|4_0(separator, ref span3, ref CS$<>8__locals1);
							break;
						}
						if (lowercase)
						{
							c = char.ToLowerInvariant(c);
						}
						JsonSeparatorNamingPolicy.<ConvertNameCore>g__WriteChar|4_0(c, ref span3, ref CS$<>8__locals1);
						separatorState = JsonSeparatorNamingPolicy.SeparatorState.UppercaseLetter;
					}
				}
				else
				{
					if (unicodeCategory == UnicodeCategory.DecimalDigitNumber)
					{
						goto IL_0118;
					}
					if (unicodeCategory != UnicodeCategory.SpaceSeparator)
					{
						goto IL_014D;
					}
					if (separatorState != JsonSeparatorNamingPolicy.SeparatorState.NotStarted)
					{
						separatorState = JsonSeparatorNamingPolicy.SeparatorState.SpaceSeparator;
					}
				}
				IL_015A:
				i++;
				continue;
				IL_0118:
				if (separatorState == JsonSeparatorNamingPolicy.SeparatorState.SpaceSeparator)
				{
					JsonSeparatorNamingPolicy.<ConvertNameCore>g__WriteChar|4_0(separator, ref span3, ref CS$<>8__locals1);
				}
				if (!lowercase && unicodeCategory == UnicodeCategory.LowercaseLetter)
				{
					c = char.ToUpperInvariant(c);
				}
				JsonSeparatorNamingPolicy.<ConvertNameCore>g__WriteChar|4_0(c, ref span3, ref CS$<>8__locals1);
				separatorState = JsonSeparatorNamingPolicy.SeparatorState.LowercaseLetterOrDigit;
				goto IL_015A;
				IL_014D:
				JsonSeparatorNamingPolicy.<ConvertNameCore>g__WriteChar|4_0(c, ref span3, ref CS$<>8__locals1);
				separatorState = JsonSeparatorNamingPolicy.SeparatorState.NotStarted;
				goto IL_015A;
			}
			string text = span3.Slice(0, CS$<>8__locals1.charsWritten).ToString();
			if (CS$<>8__locals1.rentedBuffer != null)
			{
				span3.Slice(0, CS$<>8__locals1.charsWritten).Clear();
				ArrayPool<char>.Shared.Return(CS$<>8__locals1.rentedBuffer, false);
			}
			return text;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00006344 File Offset: 0x00004544
		[CompilerGenerated]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void <ConvertNameCore>g__WriteChar|4_0(char value, ref Span<char> destination, ref JsonSeparatorNamingPolicy.<>c__DisplayClass4_0 A_2)
		{
			if (A_2.charsWritten == destination.Length)
			{
				JsonSeparatorNamingPolicy.<ConvertNameCore>g__ExpandBuffer|4_1(ref destination, ref A_2);
			}
			int charsWritten = A_2.charsWritten;
			A_2.charsWritten = charsWritten + 1;
			*destination[charsWritten] = value;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00006380 File Offset: 0x00004580
		[CompilerGenerated]
		internal static void <ConvertNameCore>g__ExpandBuffer|4_1(ref Span<char> destination, ref JsonSeparatorNamingPolicy.<>c__DisplayClass4_0 A_1)
		{
			int num = checked(destination.Length * 2);
			char[] array = ArrayPool<char>.Shared.Rent(num);
			destination.CopyTo(array);
			if (A_1.rentedBuffer != null)
			{
				destination.Slice(0, A_1.charsWritten).Clear();
				ArrayPool<char>.Shared.Return(A_1.rentedBuffer, false);
			}
			A_1.rentedBuffer = array;
			destination = A_1.rentedBuffer;
		}

		// Token: 0x04000112 RID: 274
		private readonly bool _lowercase;

		// Token: 0x04000113 RID: 275
		private readonly char _separator;

		// Token: 0x0200010F RID: 271
		private enum SeparatorState
		{
			// Token: 0x0400043E RID: 1086
			NotStarted,
			// Token: 0x0400043F RID: 1087
			UppercaseLetter,
			// Token: 0x04000440 RID: 1088
			LowercaseLetterOrDigit,
			// Token: 0x04000441 RID: 1089
			SpaceSeparator
		}
	}
}
