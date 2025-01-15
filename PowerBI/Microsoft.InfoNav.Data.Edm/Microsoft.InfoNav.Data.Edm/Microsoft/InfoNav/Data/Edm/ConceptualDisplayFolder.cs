using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Edm
{
	// Token: 0x0200000E RID: 14
	[ImmutableObject(true)]
	internal sealed class ConceptualDisplayFolder : IConceptualDisplayFolder, IConceptualDisplayItem
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00002BB0 File Offset: 0x00000DB0
		internal ConceptualDisplayFolder(string name, string displayName, string description, IReadOnlyList<IConceptualDisplayItem> displayItems)
		{
			this._name = name;
			this._description = description;
			this._displayName = displayName;
			this._displayItems = displayItems;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002BDD File Offset: 0x00000DDD
		public string DisplayName
		{
			get
			{
				return this._displayName;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002BE5 File Offset: 0x00000DE5
		public string Description
		{
			get
			{
				return this._description;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002BED File Offset: 0x00000DED
		public IReadOnlyList<IConceptualDisplayItem> DisplayItems
		{
			get
			{
				return this._displayItems;
			}
		}

		// Token: 0x04000046 RID: 70
		private readonly string _name;

		// Token: 0x04000047 RID: 71
		private readonly string _displayName;

		// Token: 0x04000048 RID: 72
		private readonly string _description;

		// Token: 0x04000049 RID: 73
		private readonly IReadOnlyList<IConceptualDisplayItem> _displayItems;
	}
}
