using System;
using System.Collections.Generic;
using Microsoft.Data.Edm;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000164 RID: 356
	internal sealed class ODataJsonLightReaderNavigationLinkInfo
	{
		// Token: 0x0600097A RID: 2426 RVA: 0x0001E4CE File Offset: 0x0001C6CE
		private ODataJsonLightReaderNavigationLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, bool isExpanded)
		{
			this.navigationLink = navigationLink;
			this.navigationProperty = navigationProperty;
			this.isExpanded = isExpanded;
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x0001E4EB File Offset: 0x0001C6EB
		internal ODataNavigationLink NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0001E4F3 File Offset: 0x0001C6F3
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x0001E4FB File Offset: 0x0001C6FB
		internal bool IsExpanded
		{
			get
			{
				return this.isExpanded;
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0001E503 File Offset: 0x0001C703
		internal ODataFeed ExpandedFeed
		{
			get
			{
				return this.expandedFeed;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0001E50B File Offset: 0x0001C70B
		internal bool HasEntityReferenceLink
		{
			get
			{
				return this.entityReferenceLinks != null && this.entityReferenceLinks.First != null;
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0001E528 File Offset: 0x0001C728
		internal static ODataJsonLightReaderNavigationLinkInfo CreateDeferredLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, false);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x0001E534 File Offset: 0x0001C734
		internal static ODataJsonLightReaderNavigationLinkInfo CreateExpandedEntryLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, true);
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x0001E54C File Offset: 0x0001C74C
		internal static ODataJsonLightReaderNavigationLinkInfo CreateExpandedFeedLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, ODataFeed expandedFeed)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, true)
			{
				expandedFeed = expandedFeed
			};
		}

		// Token: 0x06000983 RID: 2435 RVA: 0x0001E56C File Offset: 0x0001C76C
		internal static ODataJsonLightReaderNavigationLinkInfo CreateSingletonEntityReferenceLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, ODataEntityReferenceLink entityReferenceLink, bool isExpanded)
		{
			ODataJsonLightReaderNavigationLinkInfo odataJsonLightReaderNavigationLinkInfo = new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, isExpanded);
			if (entityReferenceLink != null)
			{
				odataJsonLightReaderNavigationLinkInfo.entityReferenceLinks = new LinkedList<ODataEntityReferenceLink>();
				odataJsonLightReaderNavigationLinkInfo.entityReferenceLinks.AddFirst(entityReferenceLink);
			}
			return odataJsonLightReaderNavigationLinkInfo;
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x0001E5A0 File Offset: 0x0001C7A0
		internal static ODataJsonLightReaderNavigationLinkInfo CreateCollectionEntityReferenceLinksInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, LinkedList<ODataEntityReferenceLink> entityReferenceLinks, bool isExpanded)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, isExpanded)
			{
				entityReferenceLinks = entityReferenceLinks
			};
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		internal static ODataJsonLightReaderNavigationLinkInfo CreateProjectedNavigationLinkInfo(IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(navigationProperty.Type.IsCollection())
			};
			return new ODataJsonLightReaderNavigationLinkInfo(odataNavigationLink, navigationProperty, false);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x0001E604 File Offset: 0x0001C804
		internal ODataEntityReferenceLink ReportEntityReferenceLink()
		{
			if (this.entityReferenceLinks != null && this.entityReferenceLinks.First != null)
			{
				ODataEntityReferenceLink value = this.entityReferenceLinks.First.Value;
				this.entityReferenceLinks.RemoveFirst();
				return value;
			}
			return null;
		}

		// Token: 0x04000399 RID: 921
		private readonly ODataNavigationLink navigationLink;

		// Token: 0x0400039A RID: 922
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x0400039B RID: 923
		private readonly bool isExpanded;

		// Token: 0x0400039C RID: 924
		private ODataFeed expandedFeed;

		// Token: 0x0400039D RID: 925
		private LinkedList<ODataEntityReferenceLink> entityReferenceLinks;
	}
}
