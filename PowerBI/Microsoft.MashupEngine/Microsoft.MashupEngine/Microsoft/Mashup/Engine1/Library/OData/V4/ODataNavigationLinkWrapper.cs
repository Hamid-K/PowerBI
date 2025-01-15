using System;
using Microsoft.OData.Core;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000876 RID: 2166
	internal class ODataNavigationLinkWrapper : IODataNavigationLinkWrapper
	{
		// Token: 0x06003E4F RID: 15951 RVA: 0x000CB923 File Offset: 0x000C9B23
		public ODataNavigationLinkWrapper(ODataNavigationLink link)
		{
			this.link = link;
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06003E50 RID: 15952 RVA: 0x000CB932 File Offset: 0x000C9B32
		public Uri AssociationLinkUrl
		{
			get
			{
				return this.link.AssociationLinkUrl;
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06003E51 RID: 15953 RVA: 0x000CB93F File Offset: 0x000C9B3F
		public bool? IsCollection
		{
			get
			{
				return this.link.IsCollection;
			}
		}

		// Token: 0x17001476 RID: 5238
		// (get) Token: 0x06003E52 RID: 15954 RVA: 0x000CB94C File Offset: 0x000C9B4C
		public string Name
		{
			get
			{
				return this.link.Name;
			}
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06003E53 RID: 15955 RVA: 0x000CB959 File Offset: 0x000C9B59
		public Uri Url
		{
			get
			{
				return this.link.Url;
			}
		}

		// Token: 0x06003E54 RID: 15956 RVA: 0x000CB968 File Offset: 0x000C9B68
		public override bool Equals(object obj)
		{
			ODataNavigationLinkWrapper odataNavigationLinkWrapper = obj as ODataNavigationLinkWrapper;
			return odataNavigationLinkWrapper != null && (this.AssociationLinkUrl.Equals(odataNavigationLinkWrapper.AssociationLinkUrl) && this.IsCollection != null && this.IsCollection.Equals(odataNavigationLinkWrapper.IsCollection) && this.Name.Equals(odataNavigationLinkWrapper.Name)) && this.Url.Equals(odataNavigationLinkWrapper.Url);
		}

		// Token: 0x06003E55 RID: 15957 RVA: 0x000CB9EB File Offset: 0x000C9BEB
		public override int GetHashCode()
		{
			return this.link.Name.GetHashCode();
		}

		// Token: 0x040020C5 RID: 8389
		private readonly ODataNavigationLink link;
	}
}
