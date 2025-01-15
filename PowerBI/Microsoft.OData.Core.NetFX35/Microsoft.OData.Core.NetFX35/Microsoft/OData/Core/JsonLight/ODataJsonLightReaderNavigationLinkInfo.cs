using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000F8 RID: 248
	internal sealed class ODataJsonLightReaderNavigationLinkInfo
	{
		// Token: 0x06000979 RID: 2425 RVA: 0x0002264D File Offset: 0x0002084D
		private ODataJsonLightReaderNavigationLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, bool isExpanded)
		{
			this.navigationLink = navigationLink;
			this.navigationProperty = navigationProperty;
			this.isExpanded = isExpanded;
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600097A RID: 2426 RVA: 0x0002266A File Offset: 0x0002086A
		internal ODataNavigationLink NavigationLink
		{
			get
			{
				return this.navigationLink;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600097B RID: 2427 RVA: 0x00022672 File Offset: 0x00020872
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.navigationProperty;
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600097C RID: 2428 RVA: 0x0002267A File Offset: 0x0002087A
		internal bool IsExpanded
		{
			get
			{
				return this.isExpanded;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600097D RID: 2429 RVA: 0x00022682 File Offset: 0x00020882
		internal ODataFeed ExpandedFeed
		{
			get
			{
				return this.expandedFeed;
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600097E RID: 2430 RVA: 0x0002268A File Offset: 0x0002088A
		internal bool HasEntityReferenceLink
		{
			get
			{
				return this.entityReferenceLinks != null && this.entityReferenceLinks.First != null;
			}
		}

		// Token: 0x0600097F RID: 2431 RVA: 0x000226A7 File Offset: 0x000208A7
		internal static ODataJsonLightReaderNavigationLinkInfo CreateDeferredLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, false);
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x000226B4 File Offset: 0x000208B4
		internal static ODataJsonLightReaderNavigationLinkInfo CreateExpandedEntryLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, true);
		}

		// Token: 0x06000981 RID: 2433 RVA: 0x000226CC File Offset: 0x000208CC
		internal static ODataJsonLightReaderNavigationLinkInfo CreateExpandedFeedLinkInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, ODataFeed expandedFeed)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, true)
			{
				expandedFeed = expandedFeed
			};
		}

		// Token: 0x06000982 RID: 2434 RVA: 0x000226EC File Offset: 0x000208EC
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

		// Token: 0x06000983 RID: 2435 RVA: 0x00022720 File Offset: 0x00020920
		internal static ODataJsonLightReaderNavigationLinkInfo CreateCollectionEntityReferenceLinksInfo(ODataNavigationLink navigationLink, IEdmNavigationProperty navigationProperty, LinkedList<ODataEntityReferenceLink> entityReferenceLinks, bool isExpanded)
		{
			return new ODataJsonLightReaderNavigationLinkInfo(navigationLink, navigationProperty, isExpanded)
			{
				entityReferenceLinks = entityReferenceLinks
			};
		}

		// Token: 0x06000984 RID: 2436 RVA: 0x00022740 File Offset: 0x00020940
		internal static ODataJsonLightReaderNavigationLinkInfo CreateProjectedNavigationLinkInfo(IEdmNavigationProperty navigationProperty)
		{
			ODataNavigationLink odataNavigationLink = new ODataNavigationLink
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(navigationProperty.Type.IsCollection())
			};
			return new ODataJsonLightReaderNavigationLinkInfo(odataNavigationLink, navigationProperty, false);
		}

		// Token: 0x06000985 RID: 2437 RVA: 0x00022784 File Offset: 0x00020984
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

		// Token: 0x040003C1 RID: 961
		private readonly ODataNavigationLink navigationLink;

		// Token: 0x040003C2 RID: 962
		private readonly IEdmNavigationProperty navigationProperty;

		// Token: 0x040003C3 RID: 963
		private readonly bool isExpanded;

		// Token: 0x040003C4 RID: 964
		private ODataFeed expandedFeed;

		// Token: 0x040003C5 RID: 965
		private LinkedList<ODataEntityReferenceLink> entityReferenceLinks;
	}
}
