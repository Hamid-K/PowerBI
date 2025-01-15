using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Wrangling.AutoCompletion.SearchTree;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils
{
	// Token: 0x020001A7 RID: 423
	public class StringSubSequence : Substring, ISubSequence<char, string, StringSubSequence>, IEnumerable<char>, IEnumerable, IEquatable<StringSubSequence>
	{
		// Token: 0x06000943 RID: 2371 RVA: 0x0001B67D File Offset: 0x0001987D
		private StringSubSequence(string s, uint start, uint end)
			: base(s, start, end)
		{
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000944 RID: 2372 RVA: 0x0001B688 File Offset: 0x00019888
		public static StringSubSequence Empty { get; } = new StringSubSequence(string.Empty, 0U, 0U);

		// Token: 0x06000945 RID: 2373 RVA: 0x0001B68F File Offset: 0x0001988F
		public bool Equals(StringSubSequence other)
		{
			return other != null && (other == this || base.Value == other.Value);
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x0001B6AD File Offset: 0x000198AD
		public IEnumerator<char> GetEnumerator()
		{
			return this.AsEnumerable().GetEnumerator();
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x0001B6BA File Offset: 0x000198BA
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x0001B6C2 File Offset: 0x000198C2
		public string FullSequence
		{
			get
			{
				return base.Source;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001B6CA File Offset: 0x000198CA
		public uint FullLength
		{
			get
			{
				return (uint)base.Source.Length;
			}
		}

		// Token: 0x1700023C RID: 572
		public char this[int position]
		{
			get
			{
				return base.Source[(int)((ulong)base.Start + (ulong)((long)position))];
			}
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0001B6EF File Offset: 0x000198EF
		public StringSubSequence AbsoluteSlice(uint start, uint end)
		{
			if (start == base.Start && end == base.End)
			{
				return this;
			}
			return StringSubSequence.Create(base.Source, start, end);
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0001B712 File Offset: 0x00019912
		public bool StartsWith(string prefix)
		{
			return string.CompareOrdinal(base.Source, (int)base.Start, prefix, 0, prefix.Length) == 0;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0001B730 File Offset: 0x00019930
		public bool StartsWith(StringSubSequence prefix)
		{
			return string.CompareOrdinal(base.Source, (int)base.Start, prefix.Source, (int)prefix.Start, (int)prefix.Length) == 0;
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0001B758 File Offset: 0x00019958
		public uint FindFirstMismatchingIndex(string prefix)
		{
			return (uint)Enumerable.Range(0, (int)(Math.Min((long)((ulong)base.Length), (long)prefix.Length) + 1L)).First((int idx) => (long)idx == (long)((ulong)this.Length) || idx == prefix.Length || prefix[idx] != this[idx]);
		}

		// Token: 0x0600094F RID: 2383 RVA: 0x0001B7AC File Offset: 0x000199AC
		public uint FindFirstMismatchingIndex(StringSubSequence prefix)
		{
			return (uint)Enumerable.Range(0, (int)(Math.Min(base.Length, prefix.Length) + 1U)).First((int idx) => (long)idx == (long)((ulong)this.Length) || (long)idx == (long)((ulong)prefix.Length) || prefix[idx] != this[idx]);
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0001B7FC File Offset: 0x000199FC
		public StringSubSequence Concat(StringSubSequence other)
		{
			if (base.Length == 0U)
			{
				return other;
			}
			if (other.Length == 0U)
			{
				return this;
			}
			if (base.Source == other.Source && base.End == other.Start)
			{
				return new StringSubSequence(base.Source, base.Start, other.End);
			}
			return StringSubSequence.Create(base.Value + other.Value);
		}

		// Token: 0x06000951 RID: 2385 RVA: 0x0001B867 File Offset: 0x00019A67
		public StringSubSequence Concat(string other)
		{
			if (base.Length == 0U)
			{
				return StringSubSequence.Create(other);
			}
			if (other.Length == 0)
			{
				return this;
			}
			return StringSubSequence.Create(base.Value + other);
		}

		// Token: 0x06000952 RID: 2386 RVA: 0x0001B893 File Offset: 0x00019A93
		public static StringSubSequence Create(string s)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return new StringSubSequence(s, 0U, (uint)s.Length);
			}
			return StringSubSequence.Empty;
		}

		// Token: 0x06000953 RID: 2387 RVA: 0x0001B8B0 File Offset: 0x00019AB0
		public static StringSubSequence Create(string s, uint start)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return new StringSubSequence(s, start, (uint)s.Length);
			}
			if (start != 0U)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot have empty or null string with start != 0", Array.Empty<object>())));
			}
			return StringSubSequence.Empty;
		}

		// Token: 0x06000954 RID: 2388 RVA: 0x0001B8EA File Offset: 0x00019AEA
		public static StringSubSequence Create(string s, uint start, uint end)
		{
			if (!string.IsNullOrEmpty(s))
			{
				return new StringSubSequence(s, start, end);
			}
			if (start != 0U || end != 0U)
			{
				throw new ArgumentException(FormattableString.Invariant(FormattableStringFactory.Create("Cannot have empty or null string with start != 0 or end != 0", Array.Empty<object>())));
			}
			return StringSubSequence.Empty;
		}

		// Token: 0x06000955 RID: 2389 RVA: 0x0001B922 File Offset: 0x00019B22
		private IEnumerable<char> AsEnumerable()
		{
			uint num;
			for (uint i = base.Start; i < base.End; i = num)
			{
				yield return base.Source[(int)i];
				num = i + 1U;
			}
			yield break;
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x0001B932 File Offset: 0x00019B32
		public override bool Equals(object other)
		{
			return other != null && (other == this || (!(base.GetType() != other.GetType()) && this.Equals(other as StringSubSequence)));
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x0001B960 File Offset: 0x00019B60
		public override int GetHashCode()
		{
			return (((base.Value.GetHashCode() * 18979) ^ base.Start.GetHashCode()) * 20443) ^ base.End.GetHashCode();
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x0001B9A2 File Offset: 0x00019BA2
		public override string ToString()
		{
			return base.Value;
		}
	}
}
