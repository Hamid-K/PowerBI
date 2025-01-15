using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Core.Atom
{
	// Token: 0x02000026 RID: 38
	public static class ExtensionMethods
	{
		// Token: 0x06000158 RID: 344 RVA: 0x00004FA0 File Offset: 0x000031A0
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "ODataEntry subtype is intentional here since we want to return an AtomEntryMetadata.")]
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

		// Token: 0x06000159 RID: 345 RVA: 0x00004FD0 File Offset: 0x000031D0
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "ODataFeed subtype is intentional here since we want to return an AtomFeedMetadata.")]
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

		// Token: 0x0600015A RID: 346 RVA: 0x00005000 File Offset: 0x00003200
		[SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Justification = "ODataNavigationLink subtype is intentional here since we want to return an AtomLinkMetadata.")]
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

		// Token: 0x0600015B RID: 347 RVA: 0x00005030 File Offset: 0x00003230
		public static AtomWorkspaceMetadata Atom(this ODataServiceDocument serviceDocument)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataServiceDocument>(serviceDocument, "serviceDocument");
			AtomWorkspaceMetadata atomWorkspaceMetadata = serviceDocument.GetAnnotation<AtomWorkspaceMetadata>();
			if (atomWorkspaceMetadata == null)
			{
				atomWorkspaceMetadata = new AtomWorkspaceMetadata();
				serviceDocument.SetAnnotation<AtomWorkspaceMetadata>(atomWorkspaceMetadata);
			}
			return atomWorkspaceMetadata;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00005060 File Offset: 0x00003260
		public static AtomResourceCollectionMetadata Atom(this ODataEntitySetInfo entitySet)
		{
			ExceptionUtils.CheckArgumentNotNull<ODataEntitySetInfo>(entitySet, "entitySet");
			AtomResourceCollectionMetadata atomResourceCollectionMetadata = entitySet.GetAnnotation<AtomResourceCollectionMetadata>();
			if (atomResourceCollectionMetadata == null)
			{
				atomResourceCollectionMetadata = new AtomResourceCollectionMetadata();
				entitySet.SetAnnotation<AtomResourceCollectionMetadata>(atomResourceCollectionMetadata);
			}
			return atomResourceCollectionMetadata;
		}
	}
}
