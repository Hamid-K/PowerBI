using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Pdf.GeometryUtilities
{
	// Token: 0x02000C40 RID: 3136
	[NullableContext(1)]
	[Nullable(0)]
	public class QuadTree<[Nullable(0)] TElement, [Nullable(0)] TUnit> : ICollection<TElement>, IEnumerable<TElement>, IEnumerable where TElement : IBounded<TUnit> where TUnit : BoundsUnit
	{
		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x060050D9 RID: 20697 RVA: 0x000FD9A3 File Offset: 0x000FBBA3
		[Nullable(new byte[] { 0, 1 })]
		public Bounds<TUnit> Bounds
		{
			[return: Nullable(new byte[] { 0, 1 })]
			get;
		}

		// Token: 0x060050DA RID: 20698 RVA: 0x000FD9AB File Offset: 0x000FBBAB
		public QuadTree([Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds)
		{
			this.Bounds = bounds;
			this._storedElements = new HashSet<TElement>();
			this._allElements = new HashSet<TElement>();
		}

		// Token: 0x060050DB RID: 20699 RVA: 0x000FD9D0 File Offset: 0x000FBBD0
		public QuadTree([Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds, IEnumerable<TElement> elements)
			: this(bounds)
		{
			this.AddRange(elements);
		}

		// Token: 0x060050DC RID: 20700 RVA: 0x000FD9E0 File Offset: 0x000FBBE0
		public QuadTree(IReadOnlyList<TElement> elements)
			: this(Bounds<TUnit>.Join(elements.Select((TElement element) => element.Bounds)), elements)
		{
		}

		// Token: 0x060050DD RID: 20701 RVA: 0x000FDA14 File Offset: 0x000FBC14
		public void Add(TElement element)
		{
			this._allElements.Add(element);
			this.SubdivideIfNeeded();
			if (this._children != null)
			{
				foreach (QuadTree<TElement, TUnit> quadTree in this._children)
				{
					if (quadTree.Bounds.Contains(element.Bounds))
					{
						quadTree.Add(element);
						return;
					}
				}
			}
			this._storedElements.Add(element);
		}

		// Token: 0x060050DE RID: 20702 RVA: 0x000FDA87 File Offset: 0x000FBC87
		public void Clear()
		{
			this._children = null;
			this._storedElements.Clear();
			this._allElements.Clear();
		}

		// Token: 0x060050DF RID: 20703 RVA: 0x000FDAA8 File Offset: 0x000FBCA8
		public void AddRange(IEnumerable<TElement> elements)
		{
			foreach (TElement telement in elements)
			{
				this.Add(telement);
			}
		}

		// Token: 0x060050E0 RID: 20704 RVA: 0x000FDAF0 File Offset: 0x000FBCF0
		public void CopyTo(TElement[] array, int arrayIndex)
		{
			this._allElements.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060050E1 RID: 20705 RVA: 0x000FDAFF File Offset: 0x000FBCFF
		public int Count
		{
			get
			{
				return this._allElements.Count;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060050E2 RID: 20706 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060050E3 RID: 20707 RVA: 0x000FDB0C File Offset: 0x000FBD0C
		public bool Remove(TElement element)
		{
			if (element == null)
			{
				return false;
			}
			if (!this._allElements.Remove(element))
			{
				return false;
			}
			if (this._children != null)
			{
				foreach (QuadTree<TElement, TUnit> quadTree in this._children)
				{
					if (quadTree.Bounds.Contains(element.Bounds))
					{
						quadTree.Remove(element);
						return true;
					}
				}
			}
			this._storedElements.Remove(element);
			return true;
		}

		// Token: 0x060050E4 RID: 20708 RVA: 0x000FDB8C File Offset: 0x000FBD8C
		public void RemoveRange(IEnumerable<TElement> elements)
		{
			foreach (TElement telement in elements)
			{
				this.Remove(telement);
			}
		}

		// Token: 0x060050E5 RID: 20709 RVA: 0x000FDBD8 File Offset: 0x000FBDD8
		public bool Contains(TElement element)
		{
			return this._allElements.Contains(element);
		}

		// Token: 0x060050E6 RID: 20710 RVA: 0x000FDBE6 File Offset: 0x000FBDE6
		public bool IsEmpty()
		{
			return this._allElements.Count == 0;
		}

		// Token: 0x060050E7 RID: 20711 RVA: 0x000FDBF6 File Offset: 0x000FBDF6
		public IEnumerable<TElement> ContainedElements([Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds)
		{
			if (this._children != null)
			{
				foreach (QuadTree<TElement, TUnit> quadTree in this._children)
				{
					if (bounds.Overlaps(quadTree.Bounds))
					{
						if (bounds.Contains(quadTree.Bounds))
						{
							foreach (TElement telement in quadTree)
							{
								yield return telement;
							}
							IEnumerator<TElement> enumerator = null;
						}
						else
						{
							foreach (TElement telement2 in quadTree.ContainedElements(bounds))
							{
								yield return telement2;
							}
							IEnumerator<TElement> enumerator = null;
						}
					}
				}
				QuadTree<TElement, TUnit>[] array = null;
			}
			foreach (TElement telement3 in this._storedElements)
			{
				if (bounds.Contains(telement3.Bounds))
				{
					yield return telement3;
				}
			}
			HashSet<TElement>.Enumerator enumerator2 = default(HashSet<TElement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060050E8 RID: 20712 RVA: 0x000FDC0D File Offset: 0x000FBE0D
		public IEnumerable<TElement> ElementsThatContain([Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds)
		{
			if (this._children != null)
			{
				foreach (QuadTree<TElement, TUnit> quadTree in this._children)
				{
					if (quadTree.Bounds.Contains(bounds))
					{
						foreach (TElement telement in quadTree.ElementsThatContain(bounds))
						{
							yield return telement;
						}
						IEnumerator<TElement> enumerator = null;
					}
				}
				QuadTree<TElement, TUnit>[] array = null;
			}
			foreach (TElement telement2 in this._storedElements)
			{
				if (telement2.Bounds.Contains(bounds))
				{
					yield return telement2;
				}
			}
			HashSet<TElement>.Enumerator enumerator2 = default(HashSet<TElement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060050E9 RID: 20713 RVA: 0x000FDC24 File Offset: 0x000FBE24
		public IEnumerable<TElement> OverlappingElements([Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds)
		{
			if (this._children != null)
			{
				foreach (QuadTree<TElement, TUnit> quadTree in this._children)
				{
					if (bounds.Overlaps(quadTree.Bounds))
					{
						foreach (TElement telement in quadTree.OverlappingElements(bounds))
						{
							yield return telement;
						}
						IEnumerator<TElement> enumerator = null;
					}
				}
				QuadTree<TElement, TUnit>[] array = null;
			}
			foreach (TElement telement2 in this._storedElements)
			{
				if (bounds.Overlaps(telement2.Bounds))
				{
					yield return telement2;
				}
			}
			HashSet<TElement>.Enumerator enumerator2 = default(HashSet<TElement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060050EA RID: 20714 RVA: 0x000FDC3B File Offset: 0x000FBE3B
		public IEnumerable<TElement> OverlappingElements([Nullable(new byte[] { 0, 1 })] Range<TUnit> range, Axis axis)
		{
			if (this._children != null)
			{
				foreach (QuadTree<TElement, TUnit> quadTree in this._children)
				{
					if (range.Overlaps(quadTree.Bounds[axis]))
					{
						foreach (TElement telement in quadTree.OverlappingElements(range, axis))
						{
							yield return telement;
						}
						IEnumerator<TElement> enumerator = null;
					}
				}
				QuadTree<TElement, TUnit>[] array = null;
			}
			foreach (TElement telement2 in this._storedElements)
			{
				if (range.Overlaps(telement2.Bounds[axis]))
				{
					yield return telement2;
				}
			}
			HashSet<TElement>.Enumerator enumerator2 = default(HashSet<TElement>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060050EB RID: 20715 RVA: 0x000FDC5C File Offset: 0x000FBE5C
		public bool IsCovered([Nullable(new byte[] { 0, 1 })] Bounds<TUnit> bounds)
		{
			if (!this.Bounds.Contains(bounds))
			{
				return false;
			}
			List<Bounds<TUnit>> list = new List<Bounds<TUnit>> { bounds };
			using (HashSet<TElement>.Enumerator enumerator = this._storedElements.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TElement storedElement = enumerator.Current;
					if (storedElement.Overlaps(bounds))
					{
						list = list.SelectMany((Bounds<TUnit> b) => b.Subtract(storedElement.Bounds)).ToList<Bounds<TUnit>>();
					}
				}
			}
			if (this._children == null)
			{
				return list.Count == 0;
			}
			foreach (Bounds<TUnit> bounds2 in list)
			{
				QuadTree<TElement, TUnit>[] children = this._children;
				int i = 0;
				while (i < children.Length)
				{
					QuadTree<TElement, TUnit> quadTree = children[i];
					if (quadTree.Bounds.Overlaps(bounds2))
					{
						if (!quadTree.IsCovered(bounds2))
						{
							return false;
						}
						break;
					}
					else
					{
						i++;
					}
				}
			}
			return true;
		}

		// Token: 0x060050EC RID: 20716 RVA: 0x000FDD94 File Offset: 0x000FBF94
		private void SubdivideIfNeeded()
		{
			if (this._atMinSize)
			{
				return;
			}
			if (this._children == null)
			{
				if (Math.Min(this.Bounds.Width(), this.Bounds.Height()) > 5)
				{
					this._children = new QuadTree<TElement, TUnit>[]
					{
						new QuadTree<TElement, TUnit>(new Bounds<TUnit>(this.Bounds.Left, (int)this.Bounds.CenterAlongAxis(Axis.Horizontal) - 1, this.Bounds.Top, (int)this.Bounds.CenterAlongAxis(Axis.Vertical) - 1)),
						new QuadTree<TElement, TUnit>(new Bounds<TUnit>((int)this.Bounds.CenterAlongAxis(Axis.Horizontal), this.Bounds.Right, this.Bounds.Top, (int)this.Bounds.CenterAlongAxis(Axis.Vertical) - 1)),
						new QuadTree<TElement, TUnit>(new Bounds<TUnit>(this.Bounds.Left, (int)this.Bounds.CenterAlongAxis(Axis.Horizontal) - 1, (int)this.Bounds.CenterAlongAxis(Axis.Vertical), this.Bounds.Bottom)),
						new QuadTree<TElement, TUnit>(new Bounds<TUnit>((int)this.Bounds.CenterAlongAxis(Axis.Horizontal), this.Bounds.Right, (int)this.Bounds.CenterAlongAxis(Axis.Vertical), this.Bounds.Bottom))
					};
					return;
				}
				this._atMinSize = true;
			}
		}

		// Token: 0x060050ED RID: 20717 RVA: 0x000FDF1C File Offset: 0x000FC11C
		public IEnumerator<TElement> GetEnumerator()
		{
			return this._allElements.GetEnumerator();
		}

		// Token: 0x060050EE RID: 20718 RVA: 0x000FDF2E File Offset: 0x000FC12E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400239D RID: 9117
		[Nullable(new byte[] { 2, 1, 1, 1 })]
		private QuadTree<TElement, TUnit>[] _children;

		// Token: 0x0400239E RID: 9118
		private bool _atMinSize;

		// Token: 0x0400239F RID: 9119
		private readonly HashSet<TElement> _storedElements;

		// Token: 0x040023A0 RID: 9120
		private readonly HashSet<TElement> _allElements;

		// Token: 0x040023A1 RID: 9121
		private const int MinimumSize = 5;
	}
}
