using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C1 RID: 193
	internal sealed class ListChildrenActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000866 RID: 2150 RVA: 0x00021E6B File Offset: 0x0002006B
		// (set) Token: 0x06000867 RID: 2151 RVA: 0x00021E73 File Offset: 0x00020073
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00021E7C File Offset: 0x0002007C
		// (set) Token: 0x06000869 RID: 2153 RVA: 0x00021E84 File Offset: 0x00020084
		public bool Recursive
		{
			get
			{
				return this.m_recursive;
			}
			set
			{
				this.m_recursive = value;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x00021E8D File Offset: 0x0002008D
		// (set) Token: 0x0600086B RID: 2155 RVA: 0x00021E95 File Offset: 0x00020095
		public CatalogItemList Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x0600086C RID: 2156 RVA: 0x00021E9E File Offset: 0x0002009E
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ItemPath, this.Recursive);
			}
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x00021EC0 File Offset: 0x000200C0
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x04000428 RID: 1064
		private string m_itemPath;

		// Token: 0x04000429 RID: 1065
		private bool m_recursive;

		// Token: 0x0400042A RID: 1066
		private CatalogItemList m_children;
	}
}
