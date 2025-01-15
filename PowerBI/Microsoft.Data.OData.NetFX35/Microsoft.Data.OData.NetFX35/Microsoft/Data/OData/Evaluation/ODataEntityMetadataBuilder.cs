using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.OData.JsonLight;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x02000102 RID: 258
	internal abstract class ODataEntityMetadataBuilder
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006C2 RID: 1730 RVA: 0x00017C3D File Offset: 0x00015E3D
		internal static ODataEntityMetadataBuilder Null
		{
			get
			{
				return ODataEntityMetadataBuilder.NullEntityMetadataBuilder.Instance;
			}
		}

		// Token: 0x060006C3 RID: 1731
		internal abstract Uri GetEditLink();

		// Token: 0x060006C4 RID: 1732
		internal abstract Uri GetReadLink();

		// Token: 0x060006C5 RID: 1733
		internal abstract string GetId();

		// Token: 0x060006C6 RID: 1734
		internal abstract string GetETag();

		// Token: 0x060006C7 RID: 1735 RVA: 0x00017C44 File Offset: 0x00015E44
		internal virtual ODataStreamReferenceValue GetMediaResource()
		{
			return null;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00017C5A File Offset: 0x00015E5A
		internal virtual IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (nonComputedProperties != null)
			{
				return Enumerable.Where<ODataProperty>(nonComputedProperties, (ODataProperty p) => !(p.ODataValue is ODataStreamReferenceValue));
			}
			return null;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00017C84 File Offset: 0x00015E84
		internal virtual IEnumerable<ODataAction> GetActions()
		{
			return null;
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00017C87 File Offset: 0x00015E87
		internal virtual IEnumerable<ODataFunction> GetFunctions()
		{
			return null;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00017C8A File Offset: 0x00015E8A
		internal virtual void MarkNavigationLinkProcessed(string navigationPropertyName)
		{
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00017C8C File Offset: 0x00015E8C
		internal virtual ODataJsonLightReaderNavigationLinkInfo GetNextUnprocessedNavigationLink()
		{
			return null;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00017C8F File Offset: 0x00015E8F
		internal virtual Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00017C9D File Offset: 0x00015E9D
		internal virtual Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00017CAB File Offset: 0x00015EAB
		internal virtual Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNavigationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00017CB9 File Offset: 0x00015EB9
		internal virtual Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00017CC7 File Offset: 0x00015EC7
		internal virtual Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00017CD5 File Offset: 0x00015ED5
		internal virtual string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x02000103 RID: 259
		private sealed class NullEntityMetadataBuilder : ODataEntityMetadataBuilder
		{
			// Token: 0x060006D5 RID: 1749 RVA: 0x00017CEB File Offset: 0x00015EEB
			private NullEntityMetadataBuilder()
			{
			}

			// Token: 0x060006D6 RID: 1750 RVA: 0x00017CF3 File Offset: 0x00015EF3
			internal override Uri GetEditLink()
			{
				return null;
			}

			// Token: 0x060006D7 RID: 1751 RVA: 0x00017CF6 File Offset: 0x00015EF6
			internal override Uri GetReadLink()
			{
				return null;
			}

			// Token: 0x060006D8 RID: 1752 RVA: 0x00017CF9 File Offset: 0x00015EF9
			internal override string GetId()
			{
				return null;
			}

			// Token: 0x060006D9 RID: 1753 RVA: 0x00017CFC File Offset: 0x00015EFC
			internal override string GetETag()
			{
				return null;
			}

			// Token: 0x040002A5 RID: 677
			internal static readonly ODataEntityMetadataBuilder.NullEntityMetadataBuilder Instance = new ODataEntityMetadataBuilder.NullEntityMetadataBuilder();
		}
	}
}
