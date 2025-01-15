using System;
using Microsoft.OData;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x0200079B RID: 1947
	internal class ODataNestedResourceInfoWrapper : IODataNavigationLinkWrapper
	{
		// Token: 0x0600390D RID: 14605 RVA: 0x000B7AFB File Offset: 0x000B5CFB
		public ODataNestedResourceInfoWrapper(ODataNestedResourceInfo nestedResourceInfo)
		{
			this.nestedResourceInfo = nestedResourceInfo;
		}

		// Token: 0x17001358 RID: 4952
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x000B7B0A File Offset: 0x000B5D0A
		public Uri AssociationLinkUrl
		{
			get
			{
				return this.nestedResourceInfo.AssociationLinkUrl;
			}
		}

		// Token: 0x17001359 RID: 4953
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000B7B17 File Offset: 0x000B5D17
		public bool? IsCollection
		{
			get
			{
				return this.nestedResourceInfo.IsCollection;
			}
		}

		// Token: 0x1700135A RID: 4954
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000B7B24 File Offset: 0x000B5D24
		public string Name
		{
			get
			{
				return this.nestedResourceInfo.Name;
			}
		}

		// Token: 0x1700135B RID: 4955
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000B7B31 File Offset: 0x000B5D31
		public Uri Url
		{
			get
			{
				return this.nestedResourceInfo.Url;
			}
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000B7B40 File Offset: 0x000B5D40
		public override bool Equals(object obj)
		{
			ODataNestedResourceInfoWrapper odataNestedResourceInfoWrapper = obj as ODataNestedResourceInfoWrapper;
			return odataNestedResourceInfoWrapper != null && (this.AssociationLinkUrl.Equals(odataNestedResourceInfoWrapper.AssociationLinkUrl) && this.IsCollection != null && this.IsCollection.Equals(odataNestedResourceInfoWrapper.IsCollection) && this.Name.Equals(odataNestedResourceInfoWrapper.Name)) && this.Url == odataNestedResourceInfoWrapper.Url;
		}

		// Token: 0x06003913 RID: 14611 RVA: 0x000B7BC3 File Offset: 0x000B5DC3
		public override int GetHashCode()
		{
			return this.nestedResourceInfo.Name.GetHashCode();
		}

		// Token: 0x04001D6D RID: 7533
		private readonly ODataNestedResourceInfo nestedResourceInfo;
	}
}
