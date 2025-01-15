using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000228 RID: 552
	internal abstract class ODataResourceMetadataBuilder
	{
		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x000452EF File Offset: 0x000434EF
		internal static ODataResourceMetadataBuilder Null
		{
			get
			{
				return ODataResourceMetadataBuilder.NullResourceMetadataBuilder.Instance;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x000452F6 File Offset: 0x000434F6
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x000452FE File Offset: 0x000434FE
		internal ODataResourceMetadataBuilder ParentMetadataBuilder { get; set; }

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00045307 File Offset: 0x00043507
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x0004530F File Offset: 0x0004350F
		internal bool IsFromCollection { get; set; }

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00045318 File Offset: 0x00043518
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x00045320 File Offset: 0x00043520
		internal string NameAsProperty { get; set; }

		// Token: 0x06001673 RID: 5747
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetEditLink();

		// Token: 0x06001674 RID: 5748
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetReadLink();

		// Token: 0x06001675 RID: 5749
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetId();

		// Token: 0x06001676 RID: 5750
		internal abstract bool TryGetIdForSerialization(out Uri id);

		// Token: 0x06001677 RID: 5751
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract string GetETag();

		// Token: 0x06001678 RID: 5752 RVA: 0x0000B41B File Offset: 0x0000961B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual ODataStreamReferenceValue GetMediaResource()
		{
			return null;
		}

		// Token: 0x06001679 RID: 5753 RVA: 0x00045329 File Offset: 0x00043529
		internal virtual IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (nonComputedProperties != null)
			{
				return Enumerable.Where<ODataProperty>(nonComputedProperties, (ODataProperty p) => !(p.ODataValue is ODataStreamReferenceValue));
			}
			return null;
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0000B41B File Offset: 0x0000961B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual IEnumerable<ODataAction> GetActions()
		{
			return null;
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0000B41B File Offset: 0x0000961B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual IEnumerable<ODataFunction> GetFunctions()
		{
			return null;
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0000250D File Offset: 0x0000070D
		internal virtual void MarkNestedResourceInfoProcessed(string navigationPropertyName)
		{
		}

		// Token: 0x0600167D RID: 5757 RVA: 0x0000B41B File Offset: 0x0000961B
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual ODataJsonLightReaderNestedResourceInfo GetNextUnprocessedNavigationLink()
		{
			return null;
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x00045355 File Offset: 0x00043555
		internal virtual Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x0600167F RID: 5759 RVA: 0x00045355 File Offset: 0x00043555
		internal virtual Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06001680 RID: 5760 RVA: 0x00045363 File Offset: 0x00043563
		internal virtual Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06001681 RID: 5761 RVA: 0x00045363 File Offset: 0x00043563
		internal virtual Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06001682 RID: 5762 RVA: 0x00045371 File Offset: 0x00043571
		internal virtual Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06001683 RID: 5763 RVA: 0x00045371 File Offset: 0x00043571
		internal virtual string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x0200036D RID: 877
		private sealed class NullResourceMetadataBuilder : ODataResourceMetadataBuilder
		{
			// Token: 0x06001B60 RID: 7008 RVA: 0x0004D27A File Offset: 0x0004B47A
			private NullResourceMetadataBuilder()
			{
			}

			// Token: 0x06001B61 RID: 7009 RVA: 0x0000B41B File Offset: 0x0000961B
			internal override Uri GetEditLink()
			{
				return null;
			}

			// Token: 0x06001B62 RID: 7010 RVA: 0x0000B41B File Offset: 0x0000961B
			internal override Uri GetReadLink()
			{
				return null;
			}

			// Token: 0x06001B63 RID: 7011 RVA: 0x0000B41B File Offset: 0x0000961B
			internal override Uri GetId()
			{
				return null;
			}

			// Token: 0x06001B64 RID: 7012 RVA: 0x0000B41B File Offset: 0x0000961B
			internal override string GetETag()
			{
				return null;
			}

			// Token: 0x06001B65 RID: 7013 RVA: 0x0004D0A4 File Offset: 0x0004B2A4
			internal override bool TryGetIdForSerialization(out Uri id)
			{
				id = null;
				return false;
			}

			// Token: 0x04000DC6 RID: 3526
			internal static readonly ODataResourceMetadataBuilder.NullResourceMetadataBuilder Instance = new ODataResourceMetadataBuilder.NullResourceMetadataBuilder();
		}
	}
}
