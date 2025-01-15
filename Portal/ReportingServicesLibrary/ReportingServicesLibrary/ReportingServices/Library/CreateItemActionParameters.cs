using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000A7 RID: 167
	internal abstract class CreateItemActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700029C RID: 668
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0001FFE1 File Offset: 0x0001E1E1
		// (set) Token: 0x060007BE RID: 1982 RVA: 0x0001FFE9 File Offset: 0x0001E1E9
		public string ItemName
		{
			get
			{
				return this.m_itemName;
			}
			set
			{
				this.m_itemName = value;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0001FFF2 File Offset: 0x0001E1F2
		// (set) Token: 0x060007C0 RID: 1984 RVA: 0x0001FFFA File Offset: 0x0001E1FA
		public string ParentPath
		{
			get
			{
				return this.m_parentPath;
			}
			set
			{
				this.m_parentPath = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x00020003 File Offset: 0x0001E203
		// (set) Token: 0x060007C2 RID: 1986 RVA: 0x0002000B File Offset: 0x0001E20B
		public bool Overwrite
		{
			get
			{
				return this.m_overwrite;
			}
			set
			{
				this.m_overwrite = value;
			}
		}

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x00020014 File Offset: 0x0001E214
		// (set) Token: 0x060007C4 RID: 1988 RVA: 0x0002001C File Offset: 0x0001E21C
		public Property[] Properties
		{
			get
			{
				return this.m_properties;
			}
			set
			{
				this.m_properties = value;
			}
		}

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x00020025 File Offset: 0x0001E225
		// (set) Token: 0x060007C6 RID: 1990 RVA: 0x0002002D File Offset: 0x0001E22D
		public CatalogItem ItemInfo
		{
			get
			{
				return this.m_itemInfo;
			}
			set
			{
				this.m_itemInfo = value;
			}
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x060007C7 RID: 1991 RVA: 0x00020036 File Offset: 0x0001E236
		// (set) Token: 0x060007C8 RID: 1992 RVA: 0x0002003E File Offset: 0x0001E23E
		internal bool SkipSecurityCheck
		{
			get
			{
				return this.m_skipSecurityCheck;
			}
			set
			{
				this.m_skipSecurityCheck = value;
			}
		}

		// Token: 0x040003FC RID: 1020
		private string m_itemName;

		// Token: 0x040003FD RID: 1021
		private string m_parentPath;

		// Token: 0x040003FE RID: 1022
		private bool m_overwrite;

		// Token: 0x040003FF RID: 1023
		private Property[] m_properties;

		// Token: 0x04000400 RID: 1024
		private CatalogItem m_itemInfo;

		// Token: 0x04000401 RID: 1025
		private bool m_skipSecurityCheck;
	}
}
