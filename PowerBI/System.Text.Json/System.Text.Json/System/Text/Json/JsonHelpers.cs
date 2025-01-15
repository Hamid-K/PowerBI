using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Encodings.Web;
using System.Text.Json.Serialization;

namespace System.Text.Json
{
	// Token: 0x02000030 RID: 48
	internal static class JsonHelpers
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00005470 File Offset: 0x00003670
		public static bool TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue value)
		{
			if (!dictionary.ContainsKey(key))
			{
				dictionary[key] = value;
				return true;
			}
			return false;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00005486 File Offset: 0x00003686
		public static bool TryDequeue<T>(this Queue<T> queue, [NotNullWhen(true)] out T result)
		{
			if (queue.Count > 0)
			{
				result = queue.Dequeue();
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000054A7 File Offset: 0x000036A7
		internal static bool RequiresSpecialNumberHandlingOnWrite(JsonNumberHandling? handling)
		{
			return handling != null && (handling.Value & (JsonNumberHandling.WriteAsString | JsonNumberHandling.AllowNamedFloatingPointLiterals)) > JsonNumberHandling.Strict;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000054C0 File Offset: 0x000036C0
		internal static void StableSortByKey<T, [IsUnmanaged] TKey>(this List<T> items, Func<T, TKey> keySelector) where TKey : struct, ValueType, IComparable<TKey>
		{
			T[] array = items.ToArray();
			global::System.ValueTuple<TKey, int>[] array2 = new global::System.ValueTuple<TKey, int>[array.Length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new global::System.ValueTuple<TKey, int>(keySelector(array[i]), i);
			}
			Array.Sort<global::System.ValueTuple<TKey, int>, T>(array2, array);
			items.Clear();
			items.AddRange(array);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000551C File Offset: 0x0000371C
		public static T[] TraverseGraphWithTopologicalSort<T>(T entryNode, Func<T, ICollection<T>> getChildren, IEqualityComparer<T> comparer = null)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			List<T> list = new List<T> { entryNode };
			Dictionary<T, int> dictionary = new Dictionary<T, int>(comparer);
			dictionary[entryNode] = 0;
			Dictionary<T, int> dictionary2 = dictionary;
			List<bool[]> list2 = new List<bool[]>();
			Queue<int> queue = new Queue<int>();
			for (int i = 0; i < list.Count; i++)
			{
				T t = list[i];
				ICollection<T> collection = getChildren(t);
				int count = collection.Count;
				if (count == 0)
				{
					list2.Add(null);
					queue.Enqueue(i);
				}
				else
				{
					bool[] array = new bool[Math.Max(list.Count, count)];
					foreach (T t2 in collection)
					{
						int count2;
						if (!dictionary2.TryGetValue(t2, out count2))
						{
							count2 = list.Count;
							dictionary2.Add(t2, count2);
							list.Add(t2);
						}
						if (count2 >= array.Length)
						{
							Array.Resize<bool>(ref array, count2 + 1);
						}
						array[count2] = true;
					}
					list2.Add(array);
				}
			}
			T[] array2 = new T[list.Count];
			int num = array2.Length;
			do
			{
				int num2 = queue.Dequeue();
				array2[--num] = list[num2];
				for (int j = 0; j < list2.Count; j++)
				{
					bool[] array3 = list2[j];
					if (array3 != null && num2 < array3.Length && array3[num2])
					{
						array3[num2] = false;
						if (array3.AsSpan<bool>().IndexOf(true) == -1)
						{
							queue.Enqueue(j);
						}
					}
				}
			}
			while (queue.Count > 0);
			return array2;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000056D4 File Offset: 0x000038D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<byte> GetSpan([ScopedRef] this Utf8JsonReader reader)
		{
			if (!reader.HasValueSequence)
			{
				return reader.ValueSpan;
			}
			ReadOnlySequence<byte> valueSequence = reader.ValueSequence;
			return (in valueSequence).ToArray<byte>();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00005703 File Offset: 0x00003903
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidUnicodeScalar(uint value)
		{
			return JsonHelpers.IsInRangeInclusive(value ^ 55296U, 2048U, 1114111U);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000571B File Offset: 0x0000391B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRangeInclusive(uint value, uint lowerBound, uint upperBound)
		{
			return value - lowerBound <= upperBound - lowerBound;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00005728 File Offset: 0x00003928
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRangeInclusive(int value, int lowerBound, int upperBound)
		{
			return value - lowerBound <= upperBound - lowerBound;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00005735 File Offset: 0x00003935
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRangeInclusive(long value, long lowerBound, long upperBound)
		{
			return value - lowerBound <= upperBound - lowerBound;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00005742 File Offset: 0x00003942
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRangeInclusive(JsonTokenType value, JsonTokenType lowerBound, JsonTokenType upperBound)
		{
			return value - lowerBound <= upperBound - lowerBound;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00005751 File Offset: 0x00003951
		public static bool IsDigit(byte value)
		{
			return value - 48 <= 9;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00005760 File Offset: 0x00003960
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ReadWithVerify(this Utf8JsonReader reader)
		{
			bool flag = reader.Read();
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00005774 File Offset: 0x00003974
		public static string Utf8GetString(ReadOnlySpan<byte> bytes)
		{
			return Encoding.UTF8.GetString(bytes.ToArray());
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00005788 File Offset: 0x00003988
		public static Dictionary<TKey, TValue> CreateDictionaryFromCollection<TKey, TValue>(IEnumerable<KeyValuePair<TKey, TValue>> collection, IEqualityComparer<TKey> comparer)
		{
			Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>(comparer);
			foreach (KeyValuePair<TKey, TValue> keyValuePair in collection)
			{
				dictionary.Add(keyValuePair.Key, keyValuePair.Value);
			}
			return dictionary;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000057E8 File Offset: 0x000039E8
		public static bool IsFinite(double value)
		{
			return !double.IsNaN(value) && !double.IsInfinity(value);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000057FD File Offset: 0x000039FD
		public static bool IsFinite(float value)
		{
			return !float.IsNaN(value) && !float.IsInfinity(value);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00005812 File Offset: 0x00003A12
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void ValidateInt32MaxArrayLength(uint length)
		{
			if (length > 2146435071U)
			{
				ThrowHelper.ThrowOutOfMemoryException(length);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00005824 File Offset: 0x00003A24
		public static bool HasAllSet(this BitArray bitArray)
		{
			for (int i = 0; i < bitArray.Count; i++)
			{
				if (!bitArray[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000584E File Offset: 0x00003A4E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidDateTimeOffsetParseLength(int length)
		{
			return JsonHelpers.IsInRangeInclusive(length, 10, 252);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000585D File Offset: 0x00003A5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidUnescapedDateTimeOffsetParseLength(int length)
		{
			return JsonHelpers.IsInRangeInclusive(length, 10, 42);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000586C File Offset: 0x00003A6C
		public static bool TryParseAsISO(ReadOnlySpan<byte> source, out DateTime value)
		{
			JsonHelpers.DateTimeParseData dateTimeParseData;
			if (!JsonHelpers.TryParseDateTimeOffset(source, out dateTimeParseData))
			{
				value = default(DateTime);
				return false;
			}
			if (dateTimeParseData.OffsetToken == 90)
			{
				return JsonHelpers.TryCreateDateTime(dateTimeParseData, DateTimeKind.Utc, out value);
			}
			if (dateTimeParseData.OffsetToken != 43 && dateTimeParseData.OffsetToken != 45)
			{
				return JsonHelpers.TryCreateDateTime(dateTimeParseData, DateTimeKind.Unspecified, out value);
			}
			DateTimeOffset dateTimeOffset;
			if (!JsonHelpers.TryCreateDateTimeOffset(ref dateTimeParseData, out dateTimeOffset))
			{
				value = default(DateTime);
				return false;
			}
			value = dateTimeOffset.LocalDateTime;
			return true;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000058E0 File Offset: 0x00003AE0
		public static bool TryParseAsISO(ReadOnlySpan<byte> source, out DateTimeOffset value)
		{
			JsonHelpers.DateTimeParseData dateTimeParseData;
			if (!JsonHelpers.TryParseDateTimeOffset(source, out dateTimeParseData))
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (dateTimeParseData.OffsetToken == 90 || dateTimeParseData.OffsetToken == 43 || dateTimeParseData.OffsetToken == 45)
			{
				return JsonHelpers.TryCreateDateTimeOffset(ref dateTimeParseData, out value);
			}
			return JsonHelpers.TryCreateDateTimeOffsetInterpretingDataAsLocalTime(dateTimeParseData, out value);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x00005930 File Offset: 0x00003B30
		private unsafe static bool TryParseDateTimeOffset(ReadOnlySpan<byte> source, out JsonHelpers.DateTimeParseData parseData)
		{
			parseData = default(JsonHelpers.DateTimeParseData);
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			uint num3 = (uint)(*source[2] - 48);
			uint num4 = (uint)(*source[3] - 48);
			if (num > 9U || num2 > 9U || num3 > 9U || num4 > 9U)
			{
				return false;
			}
			parseData.Year = (int)(num * 1000U + num2 * 100U + num3 * 10U + num4);
			if (*source[4] != 45 || !JsonHelpers.TryGetNextTwoDigits(source.Slice(5, 2), ref parseData.Month) || *source[7] != 45 || !JsonHelpers.TryGetNextTwoDigits(source.Slice(8, 2), ref parseData.Day))
			{
				return false;
			}
			if (source.Length == 10)
			{
				parseData.IsCalendarDateOnly = true;
				return true;
			}
			if (source.Length < 16)
			{
				return false;
			}
			if (*source[10] != 84 || *source[13] != 58 || !JsonHelpers.TryGetNextTwoDigits(source.Slice(11, 2), ref parseData.Hour) || !JsonHelpers.TryGetNextTwoDigits(source.Slice(14, 2), ref parseData.Minute))
			{
				return false;
			}
			if (source.Length == 16)
			{
				return true;
			}
			byte b = *source[16];
			int num5 = 17;
			if (b <= 45)
			{
				if (b == 43 || b == 45)
				{
					parseData.OffsetToken = b;
					return JsonHelpers.<TryParseDateTimeOffset>g__ParseOffset|24_0(ref parseData, source.Slice(num5));
				}
			}
			else if (b != 58)
			{
				if (b == 90)
				{
					parseData.OffsetToken = 90;
					return num5 == source.Length;
				}
			}
			else
			{
				if (source.Length < 19 || !JsonHelpers.TryGetNextTwoDigits(source.Slice(17, 2), ref parseData.Second))
				{
					return false;
				}
				if (source.Length == 19)
				{
					return true;
				}
				b = *source[19];
				num5 = 20;
				switch (b)
				{
				case 43:
				case 45:
					parseData.OffsetToken = b;
					return JsonHelpers.<TryParseDateTimeOffset>g__ParseOffset|24_0(ref parseData, source.Slice(num5));
				case 44:
					break;
				case 46:
				{
					if (source.Length < 21)
					{
						return false;
					}
					int i = 0;
					int num6 = Math.Min(num5 + 16, source.Length);
					while (num5 < num6 && JsonHelpers.IsDigit(b = *source[num5]))
					{
						if (i < 7)
						{
							parseData.Fraction = parseData.Fraction * 10 + (int)(b - 48);
							i++;
						}
						num5++;
					}
					if (parseData.Fraction != 0)
					{
						while (i < 7)
						{
							parseData.Fraction *= 10;
							i++;
						}
					}
					if (num5 == source.Length)
					{
						return true;
					}
					b = *source[num5++];
					if (b == 43 || b == 45)
					{
						parseData.OffsetToken = b;
						return JsonHelpers.<TryParseDateTimeOffset>g__ParseOffset|24_0(ref parseData, source.Slice(num5));
					}
					if (b == 90)
					{
						parseData.OffsetToken = 90;
						return num5 == source.Length;
					}
					return false;
				}
				default:
					if (b == 90)
					{
						parseData.OffsetToken = 90;
						return num5 == source.Length;
					}
					break;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00005C20 File Offset: 0x00003E20
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool TryGetNextTwoDigits(ReadOnlySpan<byte> source, ref int value)
		{
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			if (num > 9U || num2 > 9U)
			{
				value = 0;
				return false;
			}
			value = (int)(num * 10U + num2);
			return true;
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00005C60 File Offset: 0x00003E60
		private static bool TryCreateDateTimeOffset(DateTime dateTime, ref JsonHelpers.DateTimeParseData parseData, out DateTimeOffset value)
		{
			if (parseData.OffsetHours > 14)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (parseData.OffsetMinutes > 59)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (parseData.OffsetHours == 14 && parseData.OffsetMinutes != 0)
			{
				value = default(DateTimeOffset);
				return false;
			}
			long num = ((long)parseData.OffsetHours * 3600L + (long)parseData.OffsetMinutes * 60L) * 10000000L;
			if (parseData.OffsetNegative)
			{
				num = -num;
			}
			try
			{
				value = new DateTimeOffset(dateTime.Ticks, new TimeSpan(num));
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00005D18 File Offset: 0x00003F18
		private static bool TryCreateDateTimeOffset(ref JsonHelpers.DateTimeParseData parseData, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!JsonHelpers.TryCreateDateTime(parseData, DateTimeKind.Unspecified, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (!JsonHelpers.TryCreateDateTimeOffset(dateTime, ref parseData, out value))
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00005D54 File Offset: 0x00003F54
		private static bool TryCreateDateTimeOffsetInterpretingDataAsLocalTime(JsonHelpers.DateTimeParseData parseData, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!JsonHelpers.TryCreateDateTime(parseData, DateTimeKind.Local, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			try
			{
				value = new DateTimeOffset(dateTime);
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00005DA4 File Offset: 0x00003FA4
		private unsafe static bool TryCreateDateTime(JsonHelpers.DateTimeParseData parseData, DateTimeKind kind, out DateTime value)
		{
			if (parseData.Year == 0)
			{
				value = default(DateTime);
				return false;
			}
			if (parseData.Month - 1 >= 12)
			{
				value = default(DateTime);
				return false;
			}
			uint num = (uint)(parseData.Day - 1);
			if (num >= 28U && (ulong)num >= (ulong)((long)DateTime.DaysInMonth(parseData.Year, parseData.Month)))
			{
				value = default(DateTime);
				return false;
			}
			if (parseData.Hour > 23)
			{
				value = default(DateTime);
				return false;
			}
			if (parseData.Minute > 59)
			{
				value = default(DateTime);
				return false;
			}
			if (parseData.Second > 59)
			{
				value = default(DateTime);
				return false;
			}
			ReadOnlySpan<int> readOnlySpan = (DateTime.IsLeapYear(parseData.Year) ? JsonHelpers.DaysToMonth366 : JsonHelpers.DaysToMonth365);
			int num2 = parseData.Year - 1;
			int num3 = num2 * 365 + num2 / 4 - num2 / 100 + num2 / 400 + *readOnlySpan[parseData.Month - 1] + parseData.Day - 1;
			long num4 = (long)num3 * 864000000000L;
			int num5 = parseData.Hour * 3600 + parseData.Minute * 60 + parseData.Second;
			num4 += (long)num5 * 10000000L;
			num4 += (long)parseData.Fraction;
			value = new DateTime(num4, kind);
			return true;
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00005EEA File Offset: 0x000040EA
		private static ReadOnlySpan<int> DaysToMonth365
		{
			get
			{
				int[] array;
				if ((array = <PrivateImplementationDetails>.5857EE4CE98BFABBD62B385C1098507DD0052FF3951043AAD6A1DABD495F18AA_A6) == null)
				{
					array = (<PrivateImplementationDetails>.5857EE4CE98BFABBD62B385C1098507DD0052FF3951043AAD6A1DABD495F18AA_A6 = new int[]
					{
						0, 31, 59, 90, 120, 151, 181, 212, 243, 273,
						304, 334, 365
					});
				}
				return new ReadOnlySpan<int>(array);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00005F12 File Offset: 0x00004112
		private static ReadOnlySpan<int> DaysToMonth366
		{
			get
			{
				int[] array;
				if ((array = <PrivateImplementationDetails>.FADB218011E7702BB9575D0C32A685DA10B5C72EB809BD9A955DB1C76E4D8315_A6) == null)
				{
					array = (<PrivateImplementationDetails>.FADB218011E7702BB9575D0C32A685DA10B5C72EB809BD9A955DB1C76E4D8315_A6 = new int[]
					{
						0, 31, 60, 91, 121, 152, 182, 213, 244, 274,
						305, 335, 366
					});
				}
				return new ReadOnlySpan<int>(array);
			}
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00005F3C File Offset: 0x0000413C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte[] GetEscapedPropertyNameSection(ReadOnlySpan<byte> utf8Value, JavaScriptEncoder encoder)
		{
			int num = JsonWriterHelper.NeedsEscaping(utf8Value, encoder);
			if (num != -1)
			{
				return JsonHelpers.GetEscapedPropertyNameSection(utf8Value, num, encoder);
			}
			return JsonHelpers.GetPropertyNameSection(utf8Value);
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00005F64 File Offset: 0x00004164
		public unsafe static byte[] EscapeValue(ReadOnlySpan<byte> utf8Value, int firstEscapeIndexVal, JavaScriptEncoder encoder)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8Value.Length, firstEscapeIndexVal);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8Value, span3, firstEscapeIndexVal, encoder, out num);
			byte[] array2 = span3.Slice(0, num).ToArray();
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return array2;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00005FEC File Offset: 0x000041EC
		private unsafe static byte[] GetEscapedPropertyNameSection(ReadOnlySpan<byte> utf8Value, int firstEscapeIndexVal, JavaScriptEncoder encoder)
		{
			byte[] array = null;
			int maxEscapedLength = JsonWriterHelper.GetMaxEscapedLength(utf8Value.Length, firstEscapeIndexVal);
			Span<byte> span2;
			if (maxEscapedLength <= 256)
			{
				Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)256], 256);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<byte>.Shared.Rent(maxEscapedLength));
			}
			Span<byte> span3 = span2;
			int num;
			JsonWriterHelper.EscapeString(utf8Value, span3, firstEscapeIndexVal, encoder, out num);
			byte[] propertyNameSection = JsonHelpers.GetPropertyNameSection(span3.Slice(0, num));
			if (array != null)
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
			return propertyNameSection;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00006074 File Offset: 0x00004274
		private static byte[] GetPropertyNameSection(ReadOnlySpan<byte> utf8Value)
		{
			int num = utf8Value.Length;
			byte[] array = new byte[num + 3];
			array[0] = 34;
			utf8Value.CopyTo(array.AsSpan(1, num));
			array[++num] = 34;
			array[num + 1] = 58;
			return array;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000060BC File Offset: 0x000042BC
		[CompilerGenerated]
		internal unsafe static bool <TryParseDateTimeOffset>g__ParseOffset|24_0(ref JsonHelpers.DateTimeParseData parseData, ReadOnlySpan<byte> offsetData)
		{
			return offsetData.Length >= 2 && JsonHelpers.TryGetNextTwoDigits(offsetData.Slice(0, 2), ref parseData.OffsetHours) && (offsetData.Length == 2 || (offsetData.Length == 5 && *offsetData[2] == 58 && JsonHelpers.TryGetNextTwoDigits(offsetData.Slice(3), ref parseData.OffsetMinutes)));
		}

		// Token: 0x0200010E RID: 270
		[StructLayout(LayoutKind.Auto)]
		private struct DateTimeParseData
		{
			// Token: 0x170002CF RID: 719
			// (get) Token: 0x06000D2A RID: 3370 RVA: 0x000333AF File Offset: 0x000315AF
			public bool OffsetNegative
			{
				get
				{
					return this.OffsetToken == 45;
				}
			}

			// Token: 0x04000432 RID: 1074
			public int Year;

			// Token: 0x04000433 RID: 1075
			public int Month;

			// Token: 0x04000434 RID: 1076
			public int Day;

			// Token: 0x04000435 RID: 1077
			public bool IsCalendarDateOnly;

			// Token: 0x04000436 RID: 1078
			public int Hour;

			// Token: 0x04000437 RID: 1079
			public int Minute;

			// Token: 0x04000438 RID: 1080
			public int Second;

			// Token: 0x04000439 RID: 1081
			public int Fraction;

			// Token: 0x0400043A RID: 1082
			public int OffsetHours;

			// Token: 0x0400043B RID: 1083
			public int OffsetMinutes;

			// Token: 0x0400043C RID: 1084
			public byte OffsetToken;
		}
	}
}
