using System;

namespace Microsoft.OData.Client.Materialization
{
	// Token: 0x02000111 RID: 273
	internal class MaterializerNavigationLink
	{
		// Token: 0x06000BA0 RID: 2976 RVA: 0x0002BC1B File Offset: 0x00029E1B
		private MaterializerNavigationLink(ODataNestedResourceInfo link, object materializedFeedOrEntry)
		{
			this.link = link;
			this.feedOrEntry = materializedFeedOrEntry;
		}

		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06000BA1 RID: 2977 RVA: 0x0002BC31 File Offset: 0x00029E31
		public ODataNestedResourceInfo Link
		{
			get
			{
				return this.link;
			}
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x0002BC39 File Offset: 0x00029E39
		public MaterializerEntry Entry
		{
			get
			{
				return this.feedOrEntry as MaterializerEntry;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000BA3 RID: 2979 RVA: 0x0002BC46 File Offset: 0x00029E46
		public ODataResourceSet Feed
		{
			get
			{
				return this.feedOrEntry as ODataResourceSet;
			}
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0002BC54 File Offset: 0x00029E54
		public static MaterializerNavigationLink CreateLink(ODataNestedResourceInfo link, MaterializerEntry entry)
		{
			MaterializerNavigationLink materializerNavigationLink = new MaterializerNavigationLink(link, entry);
			link.SetAnnotation(materializerNavigationLink);
			return materializerNavigationLink;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0002BC74 File Offset: 0x00029E74
		public static MaterializerNavigationLink CreateLink(ODataNestedResourceInfo link, ODataResourceSet resourceSet)
		{
			MaterializerNavigationLink materializerNavigationLink = new MaterializerNavigationLink(link, resourceSet);
			link.SetAnnotation(materializerNavigationLink);
			return materializerNavigationLink;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0002BC91 File Offset: 0x00029E91
		public static MaterializerNavigationLink GetLink(ODataNestedResourceInfo link)
		{
			return link.GetAnnotation<MaterializerNavigationLink>();
		}

		// Token: 0x0400064C RID: 1612
		private readonly ODataNestedResourceInfo link;

		// Token: 0x0400064D RID: 1613
		private readonly object feedOrEntry;
	}
}
