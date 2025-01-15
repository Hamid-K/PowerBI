using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200021B RID: 539
	public sealed class Set<T> : ICollection<T>, IEnumerable<T>, IEnumerable where T : IEquatable<T>
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x0003226A File Offset: 0x0003046A
		public Set()
		{
			this.m_elements = new List<T>();
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0003228F File Offset: 0x0003048F
		public Set(int capacity)
		{
			this.m_elements = new List<T>(capacity);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x000322B5 File Offset: 0x000304B5
		public Set(Set<T> other)
		{
			this.m_elements = new List<T>(other.m_elements);
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x000322E0 File Offset: 0x000304E0
		public Set(IEnumerable<T> range)
		{
			this.m_elements = new List<T>();
			this.AddRange(range);
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x0003230C File Offset: 0x0003050C
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x00032314 File Offset: 0x00030514
		public OnDuplicateElementHandler<T> OnDuplicateElement
		{
			get
			{
				return this.m_dupCallback;
			}
			set
			{
				this.m_dupCallback = value;
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0003231D File Offset: 0x0003051D
		public void Add(T item)
		{
			if (this.m_elements.Contains(item))
			{
				this.m_dupCallback(item);
				return;
			}
			this.m_elements.Add(item);
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x00032346 File Offset: 0x00030546
		public void Clear()
		{
			this.m_elements.Clear();
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00032353 File Offset: 0x00030553
		public bool Contains(T item)
		{
			return this.m_elements.Contains(item);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00032361 File Offset: 0x00030561
		public void CopyTo(T[] array, int arrayIndex)
		{
			this.m_elements.CopyTo(array, arrayIndex);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00032370 File Offset: 0x00030570
		public bool Remove(T item)
		{
			return this.m_elements.Remove(item);
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000E38 RID: 3640 RVA: 0x0003237E File Offset: 0x0003057E
		public int Count
		{
			get
			{
				return this.m_elements.Count;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0000E568 File Offset: 0x0000C768
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0003238C File Offset: 0x0003058C
		public void AddRange(IEnumerable<T> range)
		{
			foreach (T t in range)
			{
				this.Add(t);
			}
		}

		// Token: 0x1700020A RID: 522
		public T this[int index]
		{
			get
			{
				return this.m_elements[index];
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x000323E2 File Offset: 0x000305E2
		public ReadOnlyCollection<T> Elements
		{
			get
			{
				return new ReadOnlyCollection<T>(this.m_elements);
			}
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000323F0 File Offset: 0x000305F0
		public Set<T> Intersection(Set<T> other)
		{
			Set<T> result = new Set<T>();
			this.m_elements.ForEach(delegate(T e)
			{
				if (other.Contains(e))
				{
					result.Add(e);
				}
			});
			return result;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x00032434 File Offset: 0x00030634
		public Set<T> Difference(Set<T> other)
		{
			Set<T> result = new Set<T>();
			this.m_elements.ForEach(delegate(T e)
			{
				if (!other.Contains(e))
				{
					result.Add(e);
				}
			});
			return result;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x00032476 File Offset: 0x00030676
		public Set<T> Union(Set<T> other)
		{
			Set<T> set = new Set<T>(this);
			set.AddRange(other.Difference(this));
			return set;
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0003248C File Offset: 0x0003068C
		public bool Contains(Set<T> other)
		{
			if (other.Count > this.Count)
			{
				return false;
			}
			foreach (object obj in other)
			{
				T t = (T)((object)obj);
				if (!this.Contains(t))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000324FC File Offset: 0x000306FC
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.m_elements.GetEnumerator();
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000324FC File Offset: 0x000306FC
		public IEnumerator GetEnumerator()
		{
			return this.m_elements.GetEnumerator();
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0003250E File Offset: 0x0003070E
		private static void DefaultOnDuplicateElement(T item)
		{
			throw new DuplicateMemberException();
		}

		// Token: 0x0400058C RID: 1420
		private List<T> m_elements;

		// Token: 0x0400058D RID: 1421
		private OnDuplicateElementHandler<T> m_dupCallback = new OnDuplicateElementHandler<T>(Set<T>.DefaultOnDuplicateElement);
	}
}
