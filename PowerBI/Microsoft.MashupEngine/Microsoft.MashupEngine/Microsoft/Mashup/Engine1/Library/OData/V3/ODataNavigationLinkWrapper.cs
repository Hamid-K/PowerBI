using System;
using Microsoft.Data.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V3
{
	// Token: 0x020008CE RID: 2254
	internal class ODataNavigationLinkWrapper : IODataNavigationLinkWrapper
	{
		// Token: 0x0600406B RID: 16491 RVA: 0x000D6F53 File Offset: 0x000D5153
		public ODataNavigationLinkWrapper(ODataNavigationLink link)
		{
			this.link = link;
		}

		// Token: 0x170014C1 RID: 5313
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x000D6F62 File Offset: 0x000D5162
		public Uri AssociationLinkUrl
		{
			get
			{
				return this.link.AssociationLinkUrl;
			}
		}

		// Token: 0x170014C2 RID: 5314
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x000D6F6F File Offset: 0x000D516F
		public bool? IsCollection
		{
			get
			{
				return this.link.IsCollection;
			}
		}

		// Token: 0x170014C3 RID: 5315
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x000D6F7C File Offset: 0x000D517C
		public string Name
		{
			get
			{
				return this.link.Name;
			}
		}

		// Token: 0x170014C4 RID: 5316
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x000D6F89 File Offset: 0x000D5189
		public Uri Url
		{
			get
			{
				return this.link.Url;
			}
		}

		// Token: 0x06004070 RID: 16496 RVA: 0x000D6F98 File Offset: 0x000D5198
		public override bool Equals(object obj)
		{
			ODataNavigationLinkWrapper odataNavigationLinkWrapper = obj as ODataNavigationLinkWrapper;
			return odataNavigationLinkWrapper != null && (this.AssociationLinkUrl.Equals(odataNavigationLinkWrapper.AssociationLinkUrl) && this.IsCollection != null && this.IsCollection.Equals(odataNavigationLinkWrapper.IsCollection) && this.Name.Equals(odataNavigationLinkWrapper.Name)) && this.Url.Equals(odataNavigationLinkWrapper.Url);
		}

		// Token: 0x06004071 RID: 16497 RVA: 0x000D701B File Offset: 0x000D521B
		public override int GetHashCode()
		{
			return this.link.Name.GetHashCode();
		}

		// Token: 0x040021D6 RID: 8662
		private readonly ODataNavigationLink link;
	}
}
