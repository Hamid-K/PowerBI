using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x020017CA RID: 6090
	public static class ReadOnlySpanExtension
	{
		// Token: 0x0600C8F9 RID: 51449 RVA: 0x002B17B8 File Offset: 0x002AF9B8
		public unsafe static bool All<T>(this ReadOnlySpan<T> subject, Func<T, bool> predicate)
		{
			ReadOnlySpan<T> readOnlySpan = subject;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T t = *readOnlySpan[i];
				if (!predicate(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600C8FA RID: 51450 RVA: 0x002B17F3 File Offset: 0x002AF9F3
		public static bool AllDelimiters(this ReadOnlySpan<char> subject)
		{
			return subject.All((char c) => c.IsDelimiter());
		}

		// Token: 0x0600C8FB RID: 51451 RVA: 0x002B181C File Offset: 0x002AFA1C
		public static IEnumerable<int> AllIndexesOf(this ReadOnlySpan<char> subject, ReadOnlySpan<char> substring, StringComparison comparison = StringComparison.Ordinal)
		{
			if (substring.Length == 0)
			{
				return new int[0];
			}
			List<int> list = new List<int>();
			int num = 0;
			int num2;
			for (int i = 0; i < subject.Length; i += num2 + substring.Length + 1)
			{
				if (num++ > 1000)
				{
					throw new Exception(string.Format("Exceeded maximum substrings ({0:N0}).", 1000));
				}
				num2 = subject.Slice(i).IndexOf(substring, comparison);
				if (num2 == -1)
				{
					break;
				}
				list.Add(i + num2);
			}
			return list;
		}

		// Token: 0x0600C8FC RID: 51452 RVA: 0x002B18A1 File Offset: 0x002AFAA1
		public static bool Any<T>(this ReadOnlySpan<T> subject)
		{
			return subject.Length > 0;
		}

		// Token: 0x0600C8FD RID: 51453 RVA: 0x002B18B0 File Offset: 0x002AFAB0
		public unsafe static bool Any<T>(this ReadOnlySpan<T> subject, Func<T, bool> predicate)
		{
			ReadOnlySpan<T> readOnlySpan = subject;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T t = *readOnlySpan[i];
				if (predicate(t))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600C8FE RID: 51454 RVA: 0x002B18EB File Offset: 0x002AFAEB
		public static bool AnyDelimiter(this ReadOnlySpan<char> subject)
		{
			return subject.Any((char c) => c.IsDelimiter());
		}

		// Token: 0x0600C8FF RID: 51455 RVA: 0x002B1912 File Offset: 0x002AFB12
		public static int Count<T>(this ReadOnlySpan<T> subject)
		{
			return subject.Length;
		}

		// Token: 0x0600C900 RID: 51456 RVA: 0x002B191C File Offset: 0x002AFB1C
		public unsafe static int Count<T>(this ReadOnlySpan<T> subject, Func<T, bool> predicate)
		{
			int num = 0;
			ReadOnlySpan<T> readOnlySpan = subject;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T t = *readOnlySpan[i];
				if (predicate(t))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600C901 RID: 51457 RVA: 0x002B195B File Offset: 0x002AFB5B
		public unsafe static T First<T>(this ReadOnlySpan<T> subject)
		{
			return *subject[0];
		}

		// Token: 0x0600C902 RID: 51458 RVA: 0x00284B5E File Offset: 0x00282D5E
		public static IEnumerable<int> IndexRange<T>(this ReadOnlySpan<T> subject)
		{
			return Utils.Range(0, subject.Length - 1);
		}

		// Token: 0x0600C903 RID: 51459 RVA: 0x002B196A File Offset: 0x002AFB6A
		public static bool IsValidIndex<T>(this ReadOnlySpan<T> subject, int index)
		{
			return 0 <= index && index < subject.Length;
		}

		// Token: 0x0600C904 RID: 51460 RVA: 0x002B197C File Offset: 0x002AFB7C
		public static bool IsValidIndex(this string subject, int index)
		{
			return 0 <= index && index < subject.Length;
		}

		// Token: 0x0600C905 RID: 51461 RVA: 0x002B198D File Offset: 0x002AFB8D
		public unsafe static T Last<T>(this ReadOnlySpan<T> subject)
		{
			return *subject[subject.Length - 1];
		}

		// Token: 0x0600C906 RID: 51462 RVA: 0x002B19A4 File Offset: 0x002AFBA4
		public static int LastIndex<T>(this ReadOnlySpan<T> subject)
		{
			return subject.Length - 1;
		}

		// Token: 0x0600C907 RID: 51463 RVA: 0x002B19AF File Offset: 0x002AFBAF
		public static bool None<T>(this ReadOnlySpan<T> subject)
		{
			return subject.Length == 0;
		}

		// Token: 0x0600C908 RID: 51464 RVA: 0x002B19BB File Offset: 0x002AFBBB
		public static bool None<T>(this ReadOnlySpan<T> subject, Func<T, bool> predicate)
		{
			return !subject.Any(predicate);
		}

		// Token: 0x0600C909 RID: 51465 RVA: 0x002B19C7 File Offset: 0x002AFBC7
		public static ReadOnlySpan<T> Skip<T>(this ReadOnlySpan<T> subject, int length)
		{
			if (length == 0)
			{
				return subject;
			}
			if (length < subject.Length)
			{
				return subject.Slice(length);
			}
			return ReadOnlySpan<T>.Empty;
		}

		// Token: 0x0600C90A RID: 51466 RVA: 0x002B19E6 File Offset: 0x002AFBE6
		public static ReadOnlySpan<T> SkipTake<T>(this ReadOnlySpan<T> subject, int skip, int length)
		{
			return subject.Slice(skip, length);
		}

		// Token: 0x0600C90B RID: 51467 RVA: 0x002B19F1 File Offset: 0x002AFBF1
		public static ReadOnlySpan<T> SliceRange<T>(this ReadOnlySpan<T> subject, int startIndex, int stopIndex)
		{
			return subject.Slice(startIndex, stopIndex - startIndex);
		}

		// Token: 0x0600C90C RID: 51468 RVA: 0x002B19FE File Offset: 0x002AFBFE
		public static ReadOnlySpan<T> Take<T>(this ReadOnlySpan<T> subject, int length)
		{
			if (length == 0)
			{
				return ReadOnlySpan<T>.Empty;
			}
			if (length < subject.Length)
			{
				return subject.Slice(0, length);
			}
			return subject;
		}

		// Token: 0x0600C90D RID: 51469 RVA: 0x002B1A20 File Offset: 0x002AFC20
		public unsafe static ReadOnlySpan<T> TakeUntil<T>(this ReadOnlySpan<T> subject, Func<T, bool> predicate)
		{
			int num = 0;
			ReadOnlySpan<T> readOnlySpan = subject;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T t = *readOnlySpan[i];
				if (predicate(t))
				{
					break;
				}
				num++;
			}
			if (num != 0)
			{
				return subject.Slice(0, num);
			}
			return ReadOnlySpan<T>.Empty;
		}

		// Token: 0x0600C90E RID: 51470 RVA: 0x002B1A70 File Offset: 0x002AFC70
		public unsafe static ReadOnlySpan<T> TakeWhile<T>(this ReadOnlySpan<T> subject, Func<T, bool> predicate)
		{
			int num = 0;
			ReadOnlySpan<T> readOnlySpan = subject;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T t = *readOnlySpan[i];
				if (!predicate(t))
				{
					break;
				}
				num++;
			}
			if (num != 0)
			{
				return subject.Slice(0, num);
			}
			return ReadOnlySpan<T>.Empty;
		}

		// Token: 0x0600C90F RID: 51471 RVA: 0x002B1AC0 File Offset: 0x002AFCC0
		public static ReadOnlySpan<char> TakeWhileDelimiter(this ReadOnlySpan<char> subject)
		{
			return subject.TakeWhile((char c) => c.IsDelimiter());
		}

		// Token: 0x0600C910 RID: 51472 RVA: 0x002B1AE8 File Offset: 0x002AFCE8
		public static ReadOnlySpan<char> ToLowerInvariant(this ReadOnlySpan<char> subject)
		{
			Span<char> span = new Span<char>(new char[subject.Length]);
			subject.ToLowerInvariant(span);
			return span;
		}

		// Token: 0x0600C911 RID: 51473 RVA: 0x002B1B18 File Offset: 0x002AFD18
		public static ReadOnlySpan<char> ToUpperInvariant(this ReadOnlySpan<char> subject)
		{
			Span<char> span = new Span<char>(new char[subject.Length]);
			subject.ToUpperInvariant(span);
			return span;
		}

		// Token: 0x0600C912 RID: 51474 RVA: 0x002B1B48 File Offset: 0x002AFD48
		public unsafe static IReadOnlyList<T> Where<T>(this ReadOnlySpan<T> subject, Func<T, bool> predicate)
		{
			List<T> list = new List<T>();
			ReadOnlySpan<T> readOnlySpan = subject;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				T t = *readOnlySpan[i];
				if (predicate(t))
				{
					list.Add(t);
				}
			}
			return list;
		}
	}
}
