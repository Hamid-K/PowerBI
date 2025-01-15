using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Common.Utils
{
	// Token: 0x020005FC RID: 1532
	internal class Set<TElement> : InternalBase, IEnumerable<TElement>, IEnumerable
	{
		// Token: 0x06004AE9 RID: 19177 RVA: 0x0010979F File Offset: 0x0010799F
		internal Set(Set<TElement> other)
			: this(other._values, other.Comparer)
		{
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x001097B3 File Offset: 0x001079B3
		internal Set()
			: this(null, null)
		{
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x001097BD File Offset: 0x001079BD
		internal Set(IEnumerable<TElement> elements)
			: this(elements, null)
		{
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x001097C7 File Offset: 0x001079C7
		internal Set(IEqualityComparer<TElement> comparer)
			: this(null, comparer)
		{
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x001097D1 File Offset: 0x001079D1
		internal Set(IEnumerable<TElement> elements, IEqualityComparer<TElement> comparer)
		{
			this._values = new HashSet<TElement>(elements ?? Enumerable.Empty<TElement>(), comparer ?? EqualityComparer<TElement>.Default);
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06004AEE RID: 19182 RVA: 0x001097F8 File Offset: 0x001079F8
		internal int Count
		{
			get
			{
				return this._values.Count;
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06004AEF RID: 19183 RVA: 0x00109805 File Offset: 0x00107A05
		internal IEqualityComparer<TElement> Comparer
		{
			get
			{
				return this._values.Comparer;
			}
		}

		// Token: 0x06004AF0 RID: 19184 RVA: 0x00109812 File Offset: 0x00107A12
		internal bool Contains(TElement element)
		{
			return this._values.Contains(element);
		}

		// Token: 0x06004AF1 RID: 19185 RVA: 0x00109820 File Offset: 0x00107A20
		internal void Add(TElement element)
		{
			this._values.Add(element);
		}

		// Token: 0x06004AF2 RID: 19186 RVA: 0x00109830 File Offset: 0x00107A30
		internal void AddRange(IEnumerable<TElement> elements)
		{
			foreach (TElement telement in elements)
			{
				this.Add(telement);
			}
		}

		// Token: 0x06004AF3 RID: 19187 RVA: 0x00109878 File Offset: 0x00107A78
		internal void Remove(TElement element)
		{
			this._values.Remove(element);
		}

		// Token: 0x06004AF4 RID: 19188 RVA: 0x00109887 File Offset: 0x00107A87
		internal void Clear()
		{
			this._values.Clear();
		}

		// Token: 0x06004AF5 RID: 19189 RVA: 0x00109894 File Offset: 0x00107A94
		internal TElement[] ToArray()
		{
			return this._values.ToArray<TElement>();
		}

		// Token: 0x06004AF6 RID: 19190 RVA: 0x001098A1 File Offset: 0x00107AA1
		internal bool SetEquals(Set<TElement> other)
		{
			return this._values.Count == other._values.Count && this._values.IsSubsetOf(other._values);
		}

		// Token: 0x06004AF7 RID: 19191 RVA: 0x001098CE File Offset: 0x00107ACE
		internal bool IsSubsetOf(Set<TElement> other)
		{
			return this._values.IsSubsetOf(other._values);
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x001098E1 File Offset: 0x00107AE1
		internal bool Overlaps(Set<TElement> other)
		{
			return this._values.Overlaps(other._values);
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x001098F4 File Offset: 0x00107AF4
		internal void Subtract(IEnumerable<TElement> other)
		{
			this._values.ExceptWith(other);
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x00109902 File Offset: 0x00107B02
		internal Set<TElement> Difference(IEnumerable<TElement> other)
		{
			Set<TElement> set = new Set<TElement>(this);
			set.Subtract(other);
			return set;
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x00109911 File Offset: 0x00107B11
		internal void Unite(IEnumerable<TElement> other)
		{
			this._values.UnionWith(other);
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x0010991F File Offset: 0x00107B1F
		internal Set<TElement> Union(IEnumerable<TElement> other)
		{
			Set<TElement> set = new Set<TElement>(this);
			set.Unite(other);
			return set;
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0010992E File Offset: 0x00107B2E
		internal void Intersect(Set<TElement> other)
		{
			this._values.IntersectWith(other._values);
		}

		// Token: 0x06004AFE RID: 19198 RVA: 0x00109941 File Offset: 0x00107B41
		internal Set<TElement> AsReadOnly()
		{
			if (this._isReadOnly)
			{
				return this;
			}
			return new Set<TElement>(this)
			{
				_isReadOnly = true
			};
		}

		// Token: 0x06004AFF RID: 19199 RVA: 0x0010995A File Offset: 0x00107B5A
		internal Set<TElement> MakeReadOnly()
		{
			this._isReadOnly = true;
			return this;
		}

		// Token: 0x06004B00 RID: 19200 RVA: 0x00109964 File Offset: 0x00107B64
		internal int GetElementsHashCode()
		{
			int num = 0;
			foreach (TElement telement in this)
			{
				num ^= this.Comparer.GetHashCode(telement);
			}
			return num;
		}

		// Token: 0x06004B01 RID: 19201 RVA: 0x001099C0 File Offset: 0x00107BC0
		public HashSet<TElement>.Enumerator GetEnumerator()
		{
			return this._values.GetEnumerator();
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x001099CD File Offset: 0x00107BCD
		[Conditional("DEBUG")]
		private void AssertReadWrite()
		{
		}

		// Token: 0x06004B03 RID: 19203 RVA: 0x001099CF File Offset: 0x00107BCF
		[Conditional("DEBUG")]
		private void AssertSetCompatible(Set<TElement> other)
		{
		}

		// Token: 0x06004B04 RID: 19204 RVA: 0x001099D1 File Offset: 0x00107BD1
		IEnumerator<TElement> IEnumerable<TElement>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x001099DE File Offset: 0x00107BDE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x001099EB File Offset: 0x00107BEB
		internal override void ToCompactString(StringBuilder builder)
		{
			StringUtil.ToCommaSeparatedStringSorted(builder, this);
		}

		// Token: 0x04001A43 RID: 6723
		internal static readonly Set<TElement> Empty = new Set<TElement>().MakeReadOnly();

		// Token: 0x04001A44 RID: 6724
		private readonly HashSet<TElement> _values;

		// Token: 0x04001A45 RID: 6725
		private bool _isReadOnly;

		// Token: 0x02000C45 RID: 3141
		public class Enumerator : IEnumerator<TElement>, IDisposable, IEnumerator
		{
			// Token: 0x06006A3E RID: 27198 RVA: 0x0016B929 File Offset: 0x00169B29
			internal Enumerator(Dictionary<TElement, bool>.KeyCollection.Enumerator keys)
			{
				this.keys = keys;
			}

			// Token: 0x1700116B RID: 4459
			// (get) Token: 0x06006A3F RID: 27199 RVA: 0x0016B938 File Offset: 0x00169B38
			public TElement Current
			{
				get
				{
					return this.keys.Current;
				}
			}

			// Token: 0x06006A40 RID: 27200 RVA: 0x0016B945 File Offset: 0x00169B45
			public void Dispose()
			{
				this.keys.Dispose();
			}

			// Token: 0x1700116C RID: 4460
			// (get) Token: 0x06006A41 RID: 27201 RVA: 0x0016B952 File Offset: 0x00169B52
			object IEnumerator.Current
			{
				get
				{
					return ((IEnumerator)this.keys).Current;
				}
			}

			// Token: 0x06006A42 RID: 27202 RVA: 0x0016B964 File Offset: 0x00169B64
			public bool MoveNext()
			{
				return this.keys.MoveNext();
			}

			// Token: 0x06006A43 RID: 27203 RVA: 0x0016B971 File Offset: 0x00169B71
			void IEnumerator.Reset()
			{
				((IEnumerator)this.keys).Reset();
			}

			// Token: 0x040030AA RID: 12458
			private Dictionary<TElement, bool>.KeyCollection.Enumerator keys;
		}
	}
}
