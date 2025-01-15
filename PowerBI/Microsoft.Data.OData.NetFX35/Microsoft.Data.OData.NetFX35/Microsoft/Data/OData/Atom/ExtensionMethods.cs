using System;

namespace Microsoft.Data.OData.Atom
{
	// Token: 0x02000282 RID: 642
	public static class ExtensionMethods
	{
		// Token: 0x06001436 RID: 5174 RVA: 0x0004A424 File Offset: 0x00048624
		public static AtomEntryMetadata Atom(this ODataEntry entry)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntry>(entry, "entry");
			AtomEntryMetadata atomEntryMetadata = entry.GetAnnotation<AtomEntryMetadata>();
			if (atomEntryMetadata == null)
			{
				atomEntryMetadata = new AtomEntryMetadata();
				entry.SetAnnotation<AtomEntryMetadata>(atomEntryMetadata);
			}
			return atomEntryMetadata;
		}

		// Token: 0x06001437 RID: 5175 RVA: 0x0004A454 File Offset: 0x00048654
		public static AtomFeedMetadata Atom(this ODataFeed feed)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataFeed>(feed, "feed");
			AtomFeedMetadata atomFeedMetadata = feed.GetAnnotation<AtomFeedMetadata>();
			if (atomFeedMetadata == null)
			{
				atomFeedMetadata = new AtomFeedMetadata();
				feed.SetAnnotation<AtomFeedMetadata>(atomFeedMetadata);
			}
			return atomFeedMetadata;
		}

		// Token: 0x06001438 RID: 5176 RVA: 0x0004A484 File Offset: 0x00048684
		public static AtomLinkMetadata Atom(this ODataNavigationLink navigationLink)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataNavigationLink>(navigationLink, "navigationLink");
			AtomLinkMetadata atomLinkMetadata = navigationLink.GetAnnotation<AtomLinkMetadata>();
			if (atomLinkMetadata == null)
			{
				atomLinkMetadata = new AtomLinkMetadata();
				navigationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			return atomLinkMetadata;
		}

		// Token: 0x06001439 RID: 5177 RVA: 0x0004A4B4 File Offset: 0x000486B4
		public static AtomWorkspaceMetadata Atom(this ODataWorkspace workspace)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataWorkspace>(workspace, "workspace");
			AtomWorkspaceMetadata atomWorkspaceMetadata = workspace.GetAnnotation<AtomWorkspaceMetadata>();
			if (atomWorkspaceMetadata == null)
			{
				atomWorkspaceMetadata = new AtomWorkspaceMetadata();
				workspace.SetAnnotation<AtomWorkspaceMetadata>(atomWorkspaceMetadata);
			}
			return atomWorkspaceMetadata;
		}

		// Token: 0x0600143A RID: 5178 RVA: 0x0004A4E4 File Offset: 0x000486E4
		public static AtomResourceCollectionMetadata Atom(this ODataResourceCollectionInfo collection)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataResourceCollectionInfo>(collection, "collection");
			AtomResourceCollectionMetadata atomResourceCollectionMetadata = collection.GetAnnotation<AtomResourceCollectionMetadata>();
			if (atomResourceCollectionMetadata == null)
			{
				atomResourceCollectionMetadata = new AtomResourceCollectionMetadata();
				collection.SetAnnotation<AtomResourceCollectionMetadata>(atomResourceCollectionMetadata);
			}
			return atomResourceCollectionMetadata;
		}

		// Token: 0x0600143B RID: 5179 RVA: 0x0004A514 File Offset: 0x00048714
		public static AtomLinkMetadata Atom(this ODataAssociationLink associationLink)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataAssociationLink>(associationLink, "associationLink");
			AtomLinkMetadata atomLinkMetadata = associationLink.GetAnnotation<AtomLinkMetadata>();
			if (atomLinkMetadata == null)
			{
				atomLinkMetadata = new AtomLinkMetadata();
				associationLink.SetAnnotation<AtomLinkMetadata>(atomLinkMetadata);
			}
			return atomLinkMetadata;
		}
	}
}
