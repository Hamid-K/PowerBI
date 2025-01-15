using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000216 RID: 534
	internal sealed class ODataJsonLightReaderNestedResourceInfo
	{
		// Token: 0x060015BA RID: 5562 RVA: 0x00042745 File Offset: 0x00040945
		private ODataJsonLightReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, bool isExpanded)
		{
			this.nestedResourceInfo = nestedResourceInfo;
			this.nestedProperty = nestedProperty;
			this.hasValue = isExpanded;
		}

		// Token: 0x060015BB RID: 5563 RVA: 0x00042762 File Offset: 0x00040962
		private ODataJsonLightReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, IEdmStructuredType nestedResourceType, bool isExpanded)
		{
			this.nestedProperty = nestedProperty;
			this.nestedResourceInfo = nestedResourceInfo;
			this.nestedResourceType = nestedResourceType;
			this.hasValue = isExpanded;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x060015BC RID: 5564 RVA: 0x00042787 File Offset: 0x00040987
		internal ODataNestedResourceInfo NestedResourceInfo
		{
			get
			{
				return this.nestedResourceInfo;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x060015BD RID: 5565 RVA: 0x0004278F File Offset: 0x0004098F
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return this.nestedProperty as IEdmNavigationProperty;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x0004279C File Offset: 0x0004099C
		internal IEdmStructuralProperty StructuralProperty
		{
			get
			{
				return this.nestedProperty as IEdmStructuralProperty;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x060015BF RID: 5567 RVA: 0x000427A9 File Offset: 0x000409A9
		internal IEdmProperty NestedProperty
		{
			get
			{
				return this.nestedProperty;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x060015C0 RID: 5568 RVA: 0x000427B1 File Offset: 0x000409B1
		internal bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x000427B9 File Offset: 0x000409B9
		internal ODataResourceSet NestedResourceSet
		{
			get
			{
				return this.resourceSet;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x000427C1 File Offset: 0x000409C1
		internal IEdmStructuredType NestedResourceType
		{
			get
			{
				if (this.nestedResourceType == null && this.nestedProperty != null)
				{
					this.nestedResourceType = this.nestedProperty.Type.ToStructuredType();
				}
				return this.nestedResourceType;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x060015C3 RID: 5571 RVA: 0x000427EF File Offset: 0x000409EF
		internal bool HasEntityReferenceLink
		{
			get
			{
				return this.entityReferenceLinks != null && this.entityReferenceLinks.First != null;
			}
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x00042809 File Offset: 0x00040A09
		internal static ODataJsonLightReaderNestedResourceInfo CreateDeferredLinkInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, navigationProperty, false);
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00042814 File Offset: 0x00040A14
		internal static ODataJsonLightReaderNestedResourceInfo CreateResourceReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, IEdmStructuredType nestedResourceType)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, nestedProperty, nestedResourceType, true);
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x0004282C File Offset: 0x00040A2C
		internal static ODataJsonLightReaderNestedResourceInfo CreateResourceSetReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, IEdmStructuredType nestedResourceType, ODataResourceSet resourceSet)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, nestedProperty, nestedResourceType, true)
			{
				resourceSet = resourceSet
			};
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x0004284C File Offset: 0x00040A4C
		internal static ODataJsonLightReaderNestedResourceInfo CreateSingletonEntityReferenceLinkInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmNavigationProperty navigationProperty, ODataEntityReferenceLink entityReferenceLink, bool isExpanded)
		{
			ODataJsonLightReaderNestedResourceInfo odataJsonLightReaderNestedResourceInfo = new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, navigationProperty, isExpanded);
			if (entityReferenceLink != null)
			{
				odataJsonLightReaderNestedResourceInfo.entityReferenceLinks = new LinkedList<ODataEntityReferenceLink>();
				odataJsonLightReaderNestedResourceInfo.entityReferenceLinks.AddFirst(entityReferenceLink);
			}
			return odataJsonLightReaderNestedResourceInfo;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00042880 File Offset: 0x00040A80
		internal static ODataJsonLightReaderNestedResourceInfo CreateCollectionEntityReferenceLinksInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmNavigationProperty navigationProperty, LinkedList<ODataEntityReferenceLink> entityReferenceLinks, bool isExpanded)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, navigationProperty, isExpanded)
			{
				entityReferenceLinks = entityReferenceLinks
			};
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x000428A0 File Offset: 0x00040AA0
		internal static ODataJsonLightReaderNestedResourceInfo CreateProjectedNestedResourceInfo(IEdmNavigationProperty navigationProperty)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(navigationProperty.Type.IsCollection())
			};
			return new ODataJsonLightReaderNestedResourceInfo(odataNestedResourceInfo, navigationProperty, false);
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x000428E0 File Offset: 0x00040AE0
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

		// Token: 0x04000A38 RID: 2616
		private readonly ODataNestedResourceInfo nestedResourceInfo;

		// Token: 0x04000A39 RID: 2617
		private readonly IEdmProperty nestedProperty;

		// Token: 0x04000A3A RID: 2618
		private readonly bool hasValue;

		// Token: 0x04000A3B RID: 2619
		private ODataResourceSet resourceSet;

		// Token: 0x04000A3C RID: 2620
		private LinkedList<ODataEntityReferenceLink> entityReferenceLinks;

		// Token: 0x04000A3D RID: 2621
		private IEdmStructuredType nestedResourceType;
	}
}
