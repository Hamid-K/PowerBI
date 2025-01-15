using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D27 RID: 3367
	internal sealed class Dimensionality
	{
		// Token: 0x06005AB1 RID: 23217 RVA: 0x0013D207 File Offset: 0x0013B407
		public static Dimensionality Union(IEnumerable<Dimensionality> dimensionalities)
		{
			return new Dimensionality(dimensionalities.SelectMany((Dimensionality d) => d.Hierarchies.Select((ICubeHierarchy h) => d.GetLevelRange(h))));
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x0013D233 File Offset: 0x0013B433
		public Dimensionality(params CubeLevelRange[] levelRanges)
			: this(levelRanges)
		{
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x0013D23C File Offset: 0x0013B43C
		public Dimensionality(IEnumerable<CubeLevelRange> levelRanges)
		{
			ArrayBuilder<ICubeHierarchy> arrayBuilder = default(ArrayBuilder<ICubeHierarchy>);
			this.hierarchyLevelRanges = new Dictionary<ICubeHierarchy, CubeLevelRange>();
			foreach (CubeLevelRange cubeLevelRange in levelRanges)
			{
				CubeLevelRange cubeLevelRange2;
				if (this.hierarchyLevelRanges.TryGetValue(cubeLevelRange.Hierarchy, out cubeLevelRange2))
				{
					cubeLevelRange2 = cubeLevelRange.Union(cubeLevelRange2);
				}
				else
				{
					cubeLevelRange2 = cubeLevelRange;
					arrayBuilder.Add(cubeLevelRange.Hierarchy);
				}
				this.hierarchyLevelRanges[cubeLevelRange.Hierarchy] = cubeLevelRange2;
			}
			this.hierarchies = arrayBuilder.ToArray();
		}

		// Token: 0x17001AE4 RID: 6884
		// (get) Token: 0x06005AB4 RID: 23220 RVA: 0x0013D2E4 File Offset: 0x0013B4E4
		public bool IsEverything
		{
			get
			{
				return this.HierarchyCount == 0;
			}
		}

		// Token: 0x17001AE5 RID: 6885
		// (get) Token: 0x06005AB5 RID: 23221 RVA: 0x0013D2EF File Offset: 0x0013B4EF
		public int HierarchyCount
		{
			get
			{
				return this.hierarchies.Length;
			}
		}

		// Token: 0x17001AE6 RID: 6886
		// (get) Token: 0x06005AB6 RID: 23222 RVA: 0x0013D2F9 File Offset: 0x0013B4F9
		public IEnumerable<ICubeHierarchy> Hierarchies
		{
			get
			{
				return this.hierarchies;
			}
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x0013D301 File Offset: 0x0013B501
		public CubeLevelRange GetLevelRange(ICubeHierarchy hierarchy)
		{
			return this.hierarchyLevelRanges[hierarchy];
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x0013D30F File Offset: 0x0013B50F
		public bool TryGetLevelRange(ICubeHierarchy hierarchy, out CubeLevelRange level)
		{
			return this.hierarchyLevelRanges.TryGetValue(hierarchy, out level);
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x0013D320 File Offset: 0x0013B520
		public bool HasAllHierarchiesOf(Dimensionality other)
		{
			foreach (ICubeHierarchy cubeHierarchy in other.hierarchyLevelRanges.Keys)
			{
				if (!this.hierarchyLevelRanges.ContainsKey(cubeHierarchy))
				{
					return false;
				}
			}
			return !other.IsEverything;
		}

		// Token: 0x06005ABA RID: 23226 RVA: 0x0013D390 File Offset: 0x0013B590
		public bool IsDescendedTo(Dimensionality other)
		{
			foreach (ICubeHierarchy cubeHierarchy in this.hierarchyLevelRanges.Keys)
			{
				CubeLevelRange cubeLevelRange = this.hierarchyLevelRanges[cubeHierarchy];
				CubeLevelRange cubeLevelRange2;
				if (!other.hierarchyLevelRanges.TryGetValue(cubeHierarchy, out cubeLevelRange2) || cubeLevelRange.Fine.Number > cubeLevelRange2.Fine.Number)
				{
					return false;
				}
			}
			return !this.IsEverything || other.IsEverything;
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x0013D430 File Offset: 0x0013B630
		public bool IsSubsetOf(Dimensionality other)
		{
			foreach (ICubeHierarchy cubeHierarchy in this.hierarchyLevelRanges.Keys)
			{
				CubeLevelRange cubeLevelRange = this.hierarchyLevelRanges[cubeHierarchy];
				CubeLevelRange cubeLevelRange2;
				if (!other.hierarchyLevelRanges.TryGetValue(cubeHierarchy, out cubeLevelRange2) || cubeLevelRange.Fine.Number > cubeLevelRange2.Fine.Number || cubeLevelRange.Coarse.Number < cubeLevelRange2.Coarse.Number)
				{
					return false;
				}
			}
			return !this.IsEverything || other.IsEverything;
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x0013D4E8 File Offset: 0x0013B6E8
		public Dimensionality Union(Dimensionality other)
		{
			IEnumerable<CubeLevelRange> enumerable = this.hierarchies.Select((ICubeHierarchy h) => this.hierarchyLevelRanges[h]);
			IEnumerable<CubeLevelRange> enumerable2 = other.hierarchies.Select((ICubeHierarchy h) => other.hierarchyLevelRanges[h]);
			return new Dimensionality(enumerable.Concat(enumerable2));
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x0013D548 File Offset: 0x0013B748
		public Dimensionality IntersectHierarchies(Dimensionality other)
		{
			return new Dimensionality(from h in this.hierarchies
				where other.hierarchyLevelRanges.ContainsKey(h)
				select this.hierarchyLevelRanges[h]);
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x0013D598 File Offset: 0x0013B798
		public bool Equals(Dimensionality other)
		{
			bool flag = other != null && this.hierarchies.Length == other.hierarchies.Length;
			int num = 0;
			while (flag && num < this.hierarchies.Length)
			{
				ICubeHierarchy cubeHierarchy = this.hierarchies[num];
				CubeLevelRange cubeLevelRange = this.hierarchyLevelRanges[cubeHierarchy];
				CubeLevelRange cubeLevelRange2;
				flag = other.hierarchyLevelRanges.TryGetValue(cubeHierarchy, out cubeLevelRange2) && cubeLevelRange.Equals(cubeLevelRange2);
				num++;
			}
			return flag;
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x0013D607 File Offset: 0x0013B807
		public override bool Equals(object other)
		{
			return this.Equals(other as Dimensionality);
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x0013D618 File Offset: 0x0013B818
		public override int GetHashCode()
		{
			int num = this.hierarchies.Length;
			for (int i = 0; i < this.hierarchies.Length; i++)
			{
				num += this.hierarchies[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x040032C1 RID: 12993
		public static readonly Dimensionality Empty = new Dimensionality(EmptyArray<CubeLevelRange>.Instance);

		// Token: 0x040032C2 RID: 12994
		private readonly Dictionary<ICubeHierarchy, CubeLevelRange> hierarchyLevelRanges;

		// Token: 0x040032C3 RID: 12995
		private readonly ICubeHierarchy[] hierarchies;
	}
}
