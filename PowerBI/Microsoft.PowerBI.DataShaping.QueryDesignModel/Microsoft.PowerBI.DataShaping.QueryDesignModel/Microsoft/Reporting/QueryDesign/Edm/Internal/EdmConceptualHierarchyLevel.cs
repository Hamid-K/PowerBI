using System;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E4 RID: 484
	internal sealed class EdmConceptualHierarchyLevel : IConceptualHierarchyLevel, IConceptualDisplayItem, IEquatable<IConceptualHierarchyLevel>
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x0003F71C File Offset: 0x0003D91C
		internal EdmConceptualHierarchyLevel(EdmHierarchyLevel edmHierarchyLevel, IConceptualProperty source)
		{
			this._edmHierarchyLevel = edmHierarchyLevel;
			this._source = source;
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0003F732 File Offset: 0x0003D932
		public string Name
		{
			get
			{
				return this._edmHierarchyLevel.ReferenceName;
			}
		}

		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x0003F73F File Offset: 0x0003D93F
		public string EdmName
		{
			get
			{
				return this._edmHierarchyLevel.Name;
			}
		}

		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0003F74C File Offset: 0x0003D94C
		public string DisplayName
		{
			get
			{
				return this._edmHierarchyLevel.Caption;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001712 RID: 5906 RVA: 0x0003F759 File Offset: 0x0003D959
		public string Description
		{
			get
			{
				return this._source.Description;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x06001713 RID: 5907 RVA: 0x0003F766 File Offset: 0x0003D966
		public IConceptualProperty Source
		{
			get
			{
				return this._source;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x06001714 RID: 5908 RVA: 0x0003F76E File Offset: 0x0003D96E
		public IConceptualHierarchy Hierarchy
		{
			get
			{
				return this._hierarchy;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x06001715 RID: 5909 RVA: 0x0003F776 File Offset: 0x0003D976
		public string StableName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001716 RID: 5910 RVA: 0x0003F779 File Offset: 0x0003D979
		public bool Equals(IConceptualHierarchyLevel other)
		{
			return this == other;
		}

		// Token: 0x06001717 RID: 5911 RVA: 0x0003F77F File Offset: 0x0003D97F
		internal void CompleteInitialization(IConceptualHierarchy hierarchy)
		{
			this._hierarchy = hierarchy;
		}

		// Token: 0x04000C52 RID: 3154
		private readonly EdmHierarchyLevel _edmHierarchyLevel;

		// Token: 0x04000C53 RID: 3155
		private readonly IConceptualProperty _source;

		// Token: 0x04000C54 RID: 3156
		private IConceptualHierarchy _hierarchy;
	}
}
