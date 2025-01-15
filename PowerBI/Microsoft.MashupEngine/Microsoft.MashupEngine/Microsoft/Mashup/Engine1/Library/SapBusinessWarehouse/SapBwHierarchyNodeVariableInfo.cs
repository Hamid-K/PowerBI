using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Mdx;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x020004B0 RID: 1200
	internal class SapBwHierarchyNodeVariableInfo
	{
		// Token: 0x0600277E RID: 10110 RVA: 0x00073FFB File Offset: 0x000721FB
		private SapBwHierarchyNodeVariableInfo(bool foundDefaultHierarchy, string firstHierarchyUniqueName, MdxHierarchy valueProviderHierarchy, List<Tuple<string, MdxHierarchy>> hierarchyNames)
		{
			this.foundDefaultHierarchy = foundDefaultHierarchy;
			this.firstHierarchyUniqueName = firstHierarchyUniqueName;
			this.valueProviderHierarchy = valueProviderHierarchy;
			this.hierarchyNames = hierarchyNames;
		}

		// Token: 0x17000F80 RID: 3968
		// (get) Token: 0x0600277F RID: 10111 RVA: 0x00074020 File Offset: 0x00072220
		public bool HasMultipleHierarchies
		{
			get
			{
				return (this.FoundDefaultHierarchy && this.HierarchyNames.Count > 0) || this.HierarchyNames.Count > 1;
			}
		}

		// Token: 0x17000F81 RID: 3969
		// (get) Token: 0x06002780 RID: 10112 RVA: 0x00074048 File Offset: 0x00072248
		public bool FoundDefaultHierarchy
		{
			get
			{
				return this.foundDefaultHierarchy;
			}
		}

		// Token: 0x17000F82 RID: 3970
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x00074050 File Offset: 0x00072250
		public string FirstHierarchyUniqueName
		{
			get
			{
				return this.firstHierarchyUniqueName;
			}
		}

		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x00074058 File Offset: 0x00072258
		public MdxHierarchy ValueProviderHierarchy
		{
			get
			{
				return this.valueProviderHierarchy;
			}
		}

		// Token: 0x17000F84 RID: 3972
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x00074060 File Offset: 0x00072260
		public List<Tuple<string, MdxHierarchy>> HierarchyNames
		{
			get
			{
				return this.hierarchyNames;
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00074068 File Offset: 0x00072268
		public List<Tuple<string, MdxHierarchy>> AddHierarchyName(MdxHierarchy hierarchy)
		{
			this.hierarchyNames.Add(new Tuple<string, MdxHierarchy>(hierarchy.UniqueIdentifier, hierarchy));
			return this.hierarchyNames;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00074088 File Offset: 0x00072288
		public static SapBwHierarchyNodeVariableInfo New(IEnumerable<MdxHierarchy> hierarchies, string defaultHierarchyUniqueName)
		{
			SapBwHierarchyNodeVariableInfo sapBwHierarchyNodeVariableInfo = new SapBwHierarchyNodeVariableInfo(false, null, null, new List<Tuple<string, MdxHierarchy>>());
			return hierarchies.Aggregate(sapBwHierarchyNodeVariableInfo, delegate(SapBwHierarchyNodeVariableInfo accumulator, MdxHierarchy hierarchy)
			{
				if (string.IsNullOrEmpty(hierarchy.UniqueIdentifier))
				{
					return accumulator;
				}
				if (!accumulator.FoundDefaultHierarchy && defaultHierarchyUniqueName != null && hierarchy.MdxIdentifier == defaultHierarchyUniqueName)
				{
					return new SapBwHierarchyNodeVariableInfo(true, accumulator.FirstHierarchyUniqueName, hierarchy, accumulator.HierarchyNames);
				}
				if (accumulator.FirstHierarchyUniqueName == null)
				{
					return new SapBwHierarchyNodeVariableInfo(accumulator.FoundDefaultHierarchy, hierarchy.UniqueIdentifier, accumulator.ValueProviderHierarchy, accumulator.AddHierarchyName(hierarchy));
				}
				return new SapBwHierarchyNodeVariableInfo(accumulator.FoundDefaultHierarchy, accumulator.FirstHierarchyUniqueName, accumulator.ValueProviderHierarchy, accumulator.AddHierarchyName(hierarchy));
			});
		}

		// Token: 0x0400109B RID: 4251
		private readonly bool foundDefaultHierarchy;

		// Token: 0x0400109C RID: 4252
		private readonly string firstHierarchyUniqueName;

		// Token: 0x0400109D RID: 4253
		private readonly MdxHierarchy valueProviderHierarchy;

		// Token: 0x0400109E RID: 4254
		private readonly List<Tuple<string, MdxHierarchy>> hierarchyNames;
	}
}
