using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020000CC RID: 204
	internal static class MemoryExtensions
	{
		// Token: 0x060006AA RID: 1706 RVA: 0x00019F74 File Offset: 0x00018174
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span)
		{
			return span.TrimStart().TrimEnd();
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00019F84 File Offset: 0x00018184
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span)
		{
			int num = 0;
			while (num < span.Length && char.IsWhiteSpace((char)(*span[num])))
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00019FC8 File Offset: 0x000181C8
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span)
		{
			int num = span.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*span[num])))
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x0001A010 File Offset: 0x00018210
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, char trimChar)
		{
			return span.TrimStart(trimChar).TrimEnd(trimChar);
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001A020 File Offset: 0x00018220
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = 0;
			while (num < span.Length && *span[num] == (ushort)trimChar)
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x0001A060 File Offset: 0x00018260
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = span.Length - 1;
			while (num >= 0 && *span[num] == (ushort)trimChar)
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001A0A4 File Offset: 0x000182A4
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			return span.TrimStart(trimChars).TrimEnd(trimChars);
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x0001A0B4 File Offset: 0x000182B4
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimStart();
			}
			int i = 0;
			IL_004F:
			while (i < span.Length)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i++;
						goto IL_004F;
					}
				}
				break;
			}
			return span.Slice(i);
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001A128 File Offset: 0x00018328
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimEnd();
			}
			int i = span.Length - 1;
			IL_0057:
			while (i >= 0)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i--;
						goto IL_0057;
					}
				}
				break;
			}
			return span.Slice(0, i + 1);
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x0001A1A0 File Offset: 0x000183A0
		public unsafe static bool IsWhiteSpace(this ReadOnlySpan<char> span)
		{
			for (int i = 0; i < span.Length; i++)
			{
				if (!char.IsWhiteSpace((char)(*span[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001A1DC File Offset: 0x000183DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this Span<T> span, T value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0001A27C File Offset: 0x0001847C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001A2F4 File Offset: 0x000184F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<T>(this Span<T> span, T value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060006B7 RID: 1719 RVA: 0x0001A394 File Offset: 0x00018594
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>(this Span<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001A40C File Offset: 0x0001860C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this Span<T> span, ReadOnlySpan<T> other) where T : object, IEquatable<T>
		{
			int length = span.Length;
			NUInt nuint;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out nuint))
			{
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (NUInt)length * nuint);
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001A49C File Offset: 0x0001869C
		public static int SequenceCompareTo<T>(this Span<T> span, ReadOnlySpan<T> other) where T : object, IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001A55C File Offset: 0x0001875C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<T>(this ReadOnlySpan<T> span, T value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001A5FC File Offset: 0x000187FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001A674 File Offset: 0x00018874
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<T>(this ReadOnlySpan<T> span, T value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0001A714 File Offset: 0x00018914
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x0001A78C File Offset: 0x0001898C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this Span<T> span, T value0, T value1) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0001A7F8 File Offset: 0x000189F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this Span<T> span, T value0, T value1, T value2) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x0001A86C File Offset: 0x00018A6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x0001A8E4 File Offset: 0x00018AE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x0001A950 File Offset: 0x00018B50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x0001A9C4 File Offset: 0x00018BC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x0001AA3C File Offset: 0x00018C3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this Span<T> span, T value0, T value1) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x0001AAA8 File Offset: 0x00018CA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this Span<T> span, T value0, T value1, T value2) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0001AB1C File Offset: 0x00018D1C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>(this Span<T> span, ReadOnlySpan<T> values) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x0001AB94 File Offset: 0x00018D94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x0001AC00 File Offset: 0x00018E00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0001AC74 File Offset: 0x00018E74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> values) where T : object, IEquatable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0001ACEC File Offset: 0x00018EEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : object, IEquatable<T>
		{
			int length = span.Length;
			NUInt nuint;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out nuint))
			{
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (NUInt)length * nuint);
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001AD7C File Offset: 0x00018F7C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SequenceCompareTo<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other) where T : object, IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001AE3C File Offset: 0x0001903C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this Span<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			int length = value.Length;
			NUInt nuint;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out nuint))
			{
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (NUInt)length * nuint);
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001AECC File Offset: 0x000190CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			int length = value.Length;
			NUInt nuint;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out nuint))
			{
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (NUInt)length * nuint);
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001AF5C File Offset: 0x0001915C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>(this Span<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			NUInt nuint;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out nuint))
			{
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (NUInt)length2 * nuint);
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001AFF8 File Offset: 0x000191F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> value) where T : object, IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			NUInt nuint;
			if (default(T) != null && MemoryExtensions.IsTypeComparableAsBytes<T>(out nuint))
			{
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (NUInt)length2 * nuint);
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001B094 File Offset: 0x00019294
		public unsafe static void Reverse<T>(this Span<T> span)
		{
			ref T reference = ref MemoryMarshal.GetReference<T>(span);
			int i = 0;
			int num = span.Length - 1;
			while (i < num)
			{
				T t = *Unsafe.Add<T>(ref reference, i);
				*Unsafe.Add<T>(ref reference, i) = *Unsafe.Add<T>(ref reference, num);
				*Unsafe.Add<T>(ref reference, num) = t;
				i++;
				num--;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001B0FC File Offset: 0x000192FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001B104 File Offset: 0x00019304
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this T[] array, int start, int length)
		{
			return new Span<T>(array, start, length);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001B110 File Offset: 0x00019310
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001B13C File Offset: 0x0001933C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, int start)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Span<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001B180 File Offset: 0x00019380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<T> AsSpan<T>(this ArraySegment<T> segment, int start, int length)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if ((ulong)length > (ulong)((long)(segment.Count - start)))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Span<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001B1D4 File Offset: 0x000193D4
		public static Memory<T> AsMemory<T>(this T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001B1DC File Offset: 0x000193DC
		public static Memory<T> AsMemory<T>(this T[] array, int start)
		{
			return new Memory<T>(array, start);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001B1E8 File Offset: 0x000193E8
		public static Memory<T> AsMemory<T>(this T[] array, int start, int length)
		{
			return new Memory<T>(array, start, length);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001B1F4 File Offset: 0x000193F4
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0001B220 File Offset: 0x00019420
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment, int start)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0001B264 File Offset: 0x00019464
		public static Memory<T> AsMemory<T>(this ArraySegment<T> segment, int start, int length)
		{
			if ((ulong)start > (ulong)((long)segment.Count))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if ((ulong)length > (ulong)((long)(segment.Count - start)))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0001B2B8 File Offset: 0x000194B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] source, Span<T> destination)
		{
			new ReadOnlySpan<T>(source).CopyTo(destination);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x0001B2D8 File Offset: 0x000194D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>(this T[] source, Memory<T> destination)
		{
			source.CopyTo(destination.Span);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0001B2E8 File Offset: 0x000194E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>(this Span<T> span, ReadOnlySpan<T> other)
		{
			return span.Overlaps(other);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0001B2F8 File Offset: 0x000194F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>(this Span<T> span, ReadOnlySpan<T> other, out int elementOffset)
		{
			return span.Overlaps(other, out elementOffset);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0001B308 File Offset: 0x00019508
		public static bool Overlaps<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				return false;
			}
			IntPtr intPtr = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				return (int)intPtr < span.Length * Unsafe.SizeOf<T>() || (int)intPtr > -(other.Length * Unsafe.SizeOf<T>());
			}
			return (long)intPtr < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)intPtr > -((long)other.Length * (long)Unsafe.SizeOf<T>());
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x0001B3B8 File Offset: 0x000195B8
		public static bool Overlaps<T>(this ReadOnlySpan<T> span, ReadOnlySpan<T> other, out int elementOffset)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				elementOffset = 0;
				return false;
			}
			IntPtr intPtr = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				if ((int)intPtr < span.Length * Unsafe.SizeOf<T>() || (int)intPtr > -(other.Length * Unsafe.SizeOf<T>()))
				{
					if ((int)intPtr % Unsafe.SizeOf<T>() != 0)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)intPtr / Unsafe.SizeOf<T>();
					return true;
				}
				elementOffset = 0;
				return false;
			}
			else
			{
				if ((long)intPtr < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)intPtr > -((long)other.Length * (long)Unsafe.SizeOf<T>()))
				{
					if ((long)intPtr % (long)Unsafe.SizeOf<T>() != 0L)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)((long)intPtr / (long)Unsafe.SizeOf<T>());
					return true;
				}
				elementOffset = 0;
				return false;
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x0001B4C4 File Offset: 0x000196C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T>(this Span<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x0001B4D0 File Offset: 0x000196D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this Span<T> span, TComparable comparable) where TComparable : object, IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x0001B4E0 File Offset: 0x000196E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparer>(this Span<T> span, T value, TComparer comparer) where TComparer : object, IComparer<T>
		{
			return span.BinarySearch(value, comparer);
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x0001B4F0 File Offset: 0x000196F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T>(this ReadOnlySpan<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x0001B4FC File Offset: 0x000196FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this ReadOnlySpan<T> span, TComparable comparable) where TComparable : object, IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x0001B508 File Offset: 0x00019708
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparer>(this ReadOnlySpan<T> span, T value, TComparer comparer) where TComparer : object, IComparer<T>
		{
			if (comparer == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparer);
			}
			SpanHelpers.ComparerComparable<T, TComparer> comparerComparable = new SpanHelpers.ComparerComparable<T, TComparer>(value, comparer);
			return span.BinarySearch(comparerComparable);
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x0001B53C File Offset: 0x0001973C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsTypeComparableAsBytes<T>(out NUInt size)
		{
			if (typeof(T) == typeof(byte) || typeof(T) == typeof(sbyte))
			{
				size = (NUInt)1;
				return true;
			}
			if (typeof(T) == typeof(char) || typeof(T) == typeof(short) || typeof(T) == typeof(ushort))
			{
				size = (NUInt)2;
				return true;
			}
			if (typeof(T) == typeof(int) || typeof(T) == typeof(uint))
			{
				size = (NUInt)4;
				return true;
			}
			if (typeof(T) == typeof(long) || typeof(T) == typeof(ulong))
			{
				size = (NUInt)8;
				return true;
			}
			size = default(NUInt);
			return false;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x0001B69C File Offset: 0x0001989C
		public static Span<T> AsSpan<T>(this T[] array, int start)
		{
			return Span<T>.Create(array, start);
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x0001B6A8 File Offset: 0x000198A8
		public static bool Contains(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			return span.IndexOf(value, comparisonType) >= 0;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x0001B6B8 File Offset: 0x000198B8
		public static bool Equals(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			if (comparisonType == StringComparison.Ordinal)
			{
				return span.SequenceEqual(other);
			}
			if (comparisonType == StringComparison.OrdinalIgnoreCase)
			{
				return span.Length == other.Length && MemoryExtensions.EqualsOrdinalIgnoreCase(span, other);
			}
			return span.ToString().Equals(other.ToString(), comparisonType);
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0001B71C File Offset: 0x0001991C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool EqualsOrdinalIgnoreCase(ReadOnlySpan<char> span, ReadOnlySpan<char> other)
		{
			return other.Length == 0 || MemoryExtensions.CompareToOrdinalIgnoreCase(span, other) == 0;
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0001B738 File Offset: 0x00019938
		public static int CompareTo(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			if (comparisonType == StringComparison.Ordinal)
			{
				return span.SequenceCompareTo(other);
			}
			if (comparisonType == StringComparison.OrdinalIgnoreCase)
			{
				return MemoryExtensions.CompareToOrdinalIgnoreCase(span, other);
			}
			return string.Compare(span.ToString(), other.ToString(), comparisonType);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0001B778 File Offset: 0x00019978
		private unsafe static int CompareToOrdinalIgnoreCase(ReadOnlySpan<char> strA, ReadOnlySpan<char> strB)
		{
			int num = Math.Min(strA.Length, strB.Length);
			int num2 = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(strA))
			{
				char* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(strB))
				{
					char* ptr2 = reference2;
					char* ptr3 = ptr;
					char* ptr4 = ptr2;
					while (num != 0 && *ptr3 <= '\u007f' && *ptr4 <= '\u007f')
					{
						int num3 = (int)(*ptr3);
						int num4 = (int)(*ptr4);
						if (num3 == num4)
						{
							ptr3++;
							ptr4++;
							num--;
						}
						else
						{
							if (num3 - 97 <= 25)
							{
								num3 -= 32;
							}
							if (num4 - 97 <= 25)
							{
								num4 -= 32;
							}
							if (num3 != num4)
							{
								return num3 - num4;
							}
							ptr3++;
							ptr4++;
							num--;
						}
					}
					if (num == 0)
					{
						return strA.Length - strB.Length;
					}
					num2 -= num;
					return string.Compare(strA.Slice(num2).ToString(), strB.Slice(num2).ToString(), StringComparison.OrdinalIgnoreCase);
				}
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0001B898 File Offset: 0x00019A98
		public static int IndexOf(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (comparisonType == StringComparison.Ordinal)
			{
				return span.IndexOf(value);
			}
			return span.ToString().IndexOf(value.ToString(), comparisonType);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0001B8CC File Offset: 0x00019ACC
		public static int ToLower(this ReadOnlySpan<char> source, Span<char> destination, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			string text = source.ToString();
			string text2 = text.ToLower(culture);
			text2.AsSpan().CopyTo(destination);
			return source.Length;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x0001B92C File Offset: 0x00019B2C
		public static int ToLowerInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			return source.ToLower(destination, CultureInfo.InvariantCulture);
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x0001B93C File Offset: 0x00019B3C
		public static int ToUpper(this ReadOnlySpan<char> source, Span<char> destination, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			string text = source.ToString();
			string text2 = text.ToUpper(culture);
			text2.AsSpan().CopyTo(destination);
			return source.Length;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x0001B99C File Offset: 0x00019B9C
		public static int ToUpperInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			return source.ToUpper(destination, CultureInfo.InvariantCulture);
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001B9AC File Offset: 0x00019BAC
		public static bool EndsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (comparisonType == StringComparison.Ordinal)
			{
				return span.EndsWith(value);
			}
			if (comparisonType == StringComparison.OrdinalIgnoreCase)
			{
				return value.Length <= span.Length && MemoryExtensions.EqualsOrdinalIgnoreCase(span.Slice(span.Length - value.Length), value);
			}
			string text = span.ToString();
			string text2 = value.ToString();
			return text.EndsWith(text2, comparisonType);
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001BA2C File Offset: 0x00019C2C
		public static bool StartsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			if (comparisonType == StringComparison.Ordinal)
			{
				return span.StartsWith(value);
			}
			if (comparisonType == StringComparison.OrdinalIgnoreCase)
			{
				return value.Length <= span.Length && MemoryExtensions.EqualsOrdinalIgnoreCase(span.Slice(0, value.Length), value);
			}
			string text = span.ToString();
			string text2 = value.ToString();
			return text.StartsWith(text2, comparisonType);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x0001BAA4 File Offset: 0x00019CA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text)
		{
			if (text == null)
			{
				return default(ReadOnlySpan<char>);
			}
			return new ReadOnlySpan<char>(Unsafe.As<Pinnable<char>>(text), MemoryExtensions.StringAdjustment, text.Length);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0001BADC File Offset: 0x00019CDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.As<Pinnable<char>>(text), MemoryExtensions.StringAdjustment + start * 2, text.Length - start);
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0001BB3C File Offset: 0x00019D3C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan(this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length || length > text.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.As<Pinnable<char>>(text), MemoryExtensions.StringAdjustment + start * 2, length);
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x0001BBA8 File Offset: 0x00019DA8
		public static ReadOnlyMemory<char> AsMemory(this string text)
		{
			if (text == null)
			{
				return default(ReadOnlyMemory<char>);
			}
			return new ReadOnlyMemory<char>(text, 0, text.Length);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001BBD8 File Offset: 0x00019DD8
		public static ReadOnlyMemory<char> AsMemory(this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, text.Length - start);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x0001BC28 File Offset: 0x00019E28
		public static ReadOnlyMemory<char> AsMemory(this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if (start > text.Length || length > text.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, length);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x0001BC84 File Offset: 0x00019E84
		private unsafe static IntPtr MeasureStringAdjustment()
		{
			string text = "a";
			fixed (string text2 = text)
			{
				char* ptr = text2;
				if (ptr != null)
				{
					ptr += RuntimeHelpers.OffsetToStringData / 2;
				}
				return Unsafe.ByteOffset<char>(ref Unsafe.As<Pinnable<char>>(text).Data, Unsafe.AsRef<char>((void*)ptr));
			}
		}

		// Token: 0x0400022B RID: 555
		internal static readonly IntPtr StringAdjustment = MemoryExtensions.MeasureStringAdjustment();
	}
}
