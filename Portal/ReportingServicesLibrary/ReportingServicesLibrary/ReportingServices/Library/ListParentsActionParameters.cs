using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000C3 RID: 195
	internal sealed class ListParentsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00022067 File Offset: 0x00020267
		// (set) Token: 0x06000873 RID: 2163 RVA: 0x0002206F File Offset: 0x0002026F
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

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00022078 File Offset: 0x00020278
		// (set) Token: 0x06000875 RID: 2165 RVA: 0x00022080 File Offset: 0x00020280
		public CatalogItemList Parents
		{
			get
			{
				return this.m_parents;
			}
			set
			{
				this.m_parents = value;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00022089 File Offset: 0x00020289
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}", this.ItemPath);
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x000220A0 File Offset: 0x000202A0
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x0400042C RID: 1068
		private string m_itemPath;

		// Token: 0x0400042D RID: 1069
		private CatalogItemList m_parents;
	}
}
