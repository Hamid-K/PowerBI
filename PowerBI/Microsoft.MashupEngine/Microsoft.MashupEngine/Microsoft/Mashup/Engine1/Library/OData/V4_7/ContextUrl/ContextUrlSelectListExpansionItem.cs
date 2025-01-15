using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl
{
	// Token: 0x0200081B RID: 2075
	internal sealed class ContextUrlSelectListExpansionItem : ContextUrlSelectListItem
	{
		// Token: 0x06003BEE RID: 15342 RVA: 0x000C28F3 File Offset: 0x000C0AF3
		public ContextUrlSelectListExpansionItem(EdmPathExpression pathToNavigationProperty, IEnumerable<ContextUrlSelectListItem> selectList, bool recursive)
		{
			this.pathToNavigationProperty = pathToNavigationProperty;
			this.selectList = selectList;
			this.recursive = recursive;
		}

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x0000240C File Offset: 0x0000060C
		public override SelectListItemKind Kind
		{
			get
			{
				return SelectListItemKind.Expansion;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000C2910 File Offset: 0x000C0B10
		public EdmPathExpression PathToNavigationProperty
		{
			get
			{
				return this.pathToNavigationProperty;
			}
		}

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x000C2918 File Offset: 0x000C0B18
		public IEnumerable<ContextUrlSelectListItem> SelectList
		{
			get
			{
				return this.selectList;
			}
		}

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x06003BF2 RID: 15346 RVA: 0x000C2920 File Offset: 0x000C0B20
		public bool Recursive
		{
			get
			{
				return this.recursive;
			}
		}

		// Token: 0x04001F37 RID: 7991
		private readonly EdmPathExpression pathToNavigationProperty;

		// Token: 0x04001F38 RID: 7992
		private readonly IEnumerable<ContextUrlSelectListItem> selectList;

		// Token: 0x04001F39 RID: 7993
		private readonly bool recursive;
	}
}
