using System;
using System.Collections.Generic;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E3 RID: 483
	internal sealed class EdmConceptualHierarchy : IConceptualHierarchy, IConceptualDisplayItem, IEquatable<IConceptualHierarchy>
	{
		// Token: 0x06001703 RID: 5891 RVA: 0x0003F61C File Offset: 0x0003D81C
		internal EdmConceptualHierarchy(EdmHierarchy edmHierarchy, IReadOnlyList<IConceptualHierarchyLevel> levels)
		{
			this._edmHierarchy = edmHierarchy;
			this._levels = levels;
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0003F632 File Offset: 0x0003D832
		public IReadOnlyList<IConceptualHierarchyLevel> Levels
		{
			get
			{
				return this._levels;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06001705 RID: 5893 RVA: 0x0003F63A File Offset: 0x0003D83A
		public string Name
		{
			get
			{
				return this._edmHierarchy.ReferenceName;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06001706 RID: 5894 RVA: 0x0003F647 File Offset: 0x0003D847
		public string EdmName
		{
			get
			{
				return this._edmHierarchy.Name;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06001707 RID: 5895 RVA: 0x0003F654 File Offset: 0x0003D854
		public string DisplayName
		{
			get
			{
				return this._edmHierarchy.Caption;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06001708 RID: 5896 RVA: 0x0003F661 File Offset: 0x0003D861
		public string Description
		{
			get
			{
				return this._edmHierarchy.Description;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06001709 RID: 5897 RVA: 0x0003F66E File Offset: 0x0003D86E
		public bool IsHidden
		{
			get
			{
				return this._edmHierarchy.IsHidden;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x0600170A RID: 5898 RVA: 0x0003F67B File Offset: 0x0003D87B
		public string StableName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600170B RID: 5899 RVA: 0x0003F67E File Offset: 0x0003D87E
		public bool Equals(IConceptualHierarchy other)
		{
			return this == other;
		}

		// Token: 0x0600170C RID: 5900 RVA: 0x0003F684 File Offset: 0x0003D884
		public bool TryGetLevel(string name, out IConceptualHierarchyLevel conceptualHierarchyLevel)
		{
			for (int i = 0; i < this._levels.Count; i++)
			{
				IConceptualHierarchyLevel conceptualHierarchyLevel2 = this._levels[i];
				if (EdmItem.ReferenceNameComparer.Equals(conceptualHierarchyLevel2.Name, name))
				{
					conceptualHierarchyLevel = conceptualHierarchyLevel2;
					return true;
				}
			}
			conceptualHierarchyLevel = null;
			return false;
		}

		// Token: 0x0600170D RID: 5901 RVA: 0x0003F6D0 File Offset: 0x0003D8D0
		public bool TryGetLevelByEdmName(string edmName, out IConceptualHierarchyLevel conceptualHierarchyLevel)
		{
			for (int i = 0; i < this._levels.Count; i++)
			{
				IConceptualHierarchyLevel conceptualHierarchyLevel2 = this._levels[i];
				if (EdmItem.IdentityComparer.Equals(conceptualHierarchyLevel2.EdmName, edmName))
				{
					conceptualHierarchyLevel = conceptualHierarchyLevel2;
					return true;
				}
			}
			conceptualHierarchyLevel = null;
			return false;
		}

		// Token: 0x04000C50 RID: 3152
		private readonly EdmHierarchy _edmHierarchy;

		// Token: 0x04000C51 RID: 3153
		private IReadOnlyList<IConceptualHierarchyLevel> _levels;
	}
}
