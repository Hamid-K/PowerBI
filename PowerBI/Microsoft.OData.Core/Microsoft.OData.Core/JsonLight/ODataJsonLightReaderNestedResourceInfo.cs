using System;
using System.Collections.Generic;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200024F RID: 591
	internal sealed class ODataJsonLightReaderNestedResourceInfo : ODataJsonLightReaderNestedInfo
	{
		// Token: 0x06001A96 RID: 6806 RVA: 0x00050BE4 File Offset: 0x0004EDE4
		private ODataJsonLightReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, bool isExpanded)
			: base(nestedProperty)
		{
			this.nestedResourceInfo = nestedResourceInfo;
			this.NestedResourceTypeReference = ((nestedProperty != null) ? nestedProperty.Type.Definition.AsElementType().ToTypeReference(nestedProperty.Type.IsNullable) : null);
			this.hasValue = isExpanded;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x00050C34 File Offset: 0x0004EE34
		private ODataJsonLightReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, IEdmType nestedResourceType, bool isExpanded)
			: base(nestedProperty)
		{
			this.nestedResourceInfo = nestedResourceInfo;
			bool flag = true;
			if (nestedProperty != null && nestedProperty.Type != null)
			{
				flag = nestedProperty.Type.IsNullable;
			}
			IEdmType edmType = nestedResourceType;
			if (nestedProperty != null && edmType == null)
			{
				edmType = nestedProperty.Type.Definition;
			}
			this.NestedResourceTypeReference = edmType.ToTypeReference(flag);
			this.hasValue = isExpanded;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x06001A98 RID: 6808 RVA: 0x00050C91 File Offset: 0x0004EE91
		internal ODataNestedResourceInfo NestedResourceInfo
		{
			get
			{
				return this.nestedResourceInfo;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x06001A99 RID: 6809 RVA: 0x00050C99 File Offset: 0x0004EE99
		internal IEdmNavigationProperty NavigationProperty
		{
			get
			{
				return base.NestedProperty as IEdmNavigationProperty;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x06001A9A RID: 6810 RVA: 0x00050CA6 File Offset: 0x0004EEA6
		internal IEdmStructuralProperty StructuralProperty
		{
			get
			{
				return base.NestedProperty as IEdmStructuralProperty;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x06001A9B RID: 6811 RVA: 0x00050CB3 File Offset: 0x0004EEB3
		internal bool HasValue
		{
			get
			{
				return this.hasValue;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x06001A9C RID: 6812 RVA: 0x00050CBB File Offset: 0x0004EEBB
		internal ODataResourceSetBase NestedResourceSet
		{
			get
			{
				return this.resourceSet;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x06001A9D RID: 6813 RVA: 0x00050CC3 File Offset: 0x0004EEC3
		// (set) Token: 0x06001A9E RID: 6814 RVA: 0x00050CCB File Offset: 0x0004EECB
		internal IEdmTypeReference NestedResourceTypeReference { get; private set; }

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06001A9F RID: 6815 RVA: 0x00050CD4 File Offset: 0x0004EED4
		internal bool HasEntityReferenceLink
		{
			get
			{
				return this.entityReferenceLinks != null && this.entityReferenceLinks.First != null;
			}
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x00050CEE File Offset: 0x0004EEEE
		internal static ODataJsonLightReaderNestedResourceInfo CreateDeferredLinkInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmNavigationProperty navigationProperty)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, navigationProperty, false);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x00050CF8 File Offset: 0x0004EEF8
		internal static ODataJsonLightReaderNestedResourceInfo CreateResourceReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, IEdmStructuredType nestedResourceType)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, nestedProperty, nestedResourceType, true);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x00050D10 File Offset: 0x0004EF10
		internal static ODataJsonLightReaderNestedResourceInfo CreateResourceSetReaderNestedResourceInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmProperty nestedProperty, IEdmType nestedResourceType, ODataResourceSetBase resourceSet)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, nestedProperty, nestedResourceType, true)
			{
				resourceSet = resourceSet
			};
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x00050D30 File Offset: 0x0004EF30
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

		// Token: 0x06001AA4 RID: 6820 RVA: 0x00050D64 File Offset: 0x0004EF64
		internal static ODataJsonLightReaderNestedResourceInfo CreateCollectionEntityReferenceLinksInfo(ODataNestedResourceInfo nestedResourceInfo, IEdmNavigationProperty navigationProperty, LinkedList<ODataEntityReferenceLink> entityReferenceLinks, bool isExpanded)
		{
			return new ODataJsonLightReaderNestedResourceInfo(nestedResourceInfo, navigationProperty, isExpanded)
			{
				entityReferenceLinks = entityReferenceLinks
			};
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x00050D84 File Offset: 0x0004EF84
		internal static ODataJsonLightReaderNestedResourceInfo CreateProjectedNestedResourceInfo(IEdmNavigationProperty navigationProperty)
		{
			ODataNestedResourceInfo odataNestedResourceInfo = new ODataNestedResourceInfo
			{
				Name = navigationProperty.Name,
				IsCollection = new bool?(navigationProperty.Type.IsCollection())
			};
			return new ODataJsonLightReaderNestedResourceInfo(odataNestedResourceInfo, navigationProperty, false);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x00050DC4 File Offset: 0x0004EFC4
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

		// Token: 0x04000B55 RID: 2901
		private readonly ODataNestedResourceInfo nestedResourceInfo;

		// Token: 0x04000B56 RID: 2902
		private readonly bool hasValue;

		// Token: 0x04000B57 RID: 2903
		private ODataResourceSetBase resourceSet;

		// Token: 0x04000B58 RID: 2904
		private LinkedList<ODataEntityReferenceLink> entityReferenceLinks;
	}
}
