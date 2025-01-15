using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.JsonLight;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000263 RID: 611
	internal abstract class ODataResourceMetadataBuilder
	{
		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06001B94 RID: 7060 RVA: 0x000552A7 File Offset: 0x000534A7
		internal static ODataResourceMetadataBuilder Null
		{
			get
			{
				return ODataResourceMetadataBuilder.NullResourceMetadataBuilder.Instance;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06001B95 RID: 7061 RVA: 0x000552AE File Offset: 0x000534AE
		// (set) Token: 0x06001B96 RID: 7062 RVA: 0x000552B6 File Offset: 0x000534B6
		internal ODataResourceMetadataBuilder ParentMetadataBuilder { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06001B97 RID: 7063 RVA: 0x000552BF File Offset: 0x000534BF
		// (set) Token: 0x06001B98 RID: 7064 RVA: 0x000552C7 File Offset: 0x000534C7
		internal bool IsFromCollection { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x000552D0 File Offset: 0x000534D0
		// (set) Token: 0x06001B9A RID: 7066 RVA: 0x000552D8 File Offset: 0x000534D8
		internal string NameAsProperty { get; set; }

		// Token: 0x06001B9B RID: 7067
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetEditLink();

		// Token: 0x06001B9C RID: 7068
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetReadLink();

		// Token: 0x06001B9D RID: 7069
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract Uri GetId();

		// Token: 0x06001B9E RID: 7070
		internal abstract bool TryGetIdForSerialization(out Uri id);

		// Token: 0x06001B9F RID: 7071
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal abstract string GetETag();

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0000360D File Offset: 0x0000180D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual ODataStreamReferenceValue GetMediaResource()
		{
			return null;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x000552E1 File Offset: 0x000534E1
		internal virtual IEnumerable<ODataProperty> GetProperties(IEnumerable<ODataProperty> nonComputedProperties)
		{
			if (nonComputedProperties != null)
			{
				return nonComputedProperties.Where(delegate(ODataProperty p)
				{
					if (p.ODataValue is ODataStreamReferenceValue)
					{
						return false;
					}
					if (p.ODataValue is ODataResourceValue)
					{
						throw new ODataException(Strings.ODataResource_PropertyValueCannotBeODataResourceValue(p.Name));
					}
					ODataCollectionValue odataCollectionValue = p.ODataValue as ODataCollectionValue;
					if (odataCollectionValue != null && odataCollectionValue.Items != null)
					{
						if (odataCollectionValue.Items.Any((object t) => t is ODataResourceValue))
						{
							throw new ODataException(Strings.ODataResource_PropertyValueCannotBeODataResourceValue(p.Name));
						}
					}
					return true;
				});
			}
			return null;
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0000360D File Offset: 0x0000180D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual IEnumerable<ODataAction> GetActions()
		{
			return null;
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0000360D File Offset: 0x0000180D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual IEnumerable<ODataFunction> GetFunctions()
		{
			return null;
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0000239D File Offset: 0x0000059D
		internal virtual void MarkNestedResourceInfoProcessed(string navigationPropertyName)
		{
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0000360D File Offset: 0x0000180D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual ODataProperty GetNextUnprocessedStreamProperty()
		{
			return null;
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0000239D File Offset: 0x0000059D
		internal virtual void MarkStreamPropertyProcessed(string streamPropertyName)
		{
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0000360D File Offset: 0x0000180D
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "A method for consistency with the rest of the API.")]
		internal virtual ODataJsonLightReaderNestedResourceInfo GetNextUnprocessedNavigationLink()
		{
			return null;
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0005530D File Offset: 0x0005350D
		internal virtual Uri GetStreamEditLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x0005530D File Offset: 0x0005350D
		internal virtual Uri GetStreamReadLink(string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			return null;
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x0005531B File Offset: 0x0005351B
		internal virtual Uri GetNavigationLinkUri(string navigationPropertyName, Uri navigationLinkUrl, bool hasNestedResourceInfoUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0005531B File Offset: 0x0005351B
		internal virtual Uri GetAssociationLinkUri(string navigationPropertyName, Uri associationLinkUrl, bool hasAssociationLinkUrl)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return null;
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00055329 File Offset: 0x00053529
		internal virtual Uri GetOperationTargetUri(string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x00055329 File Offset: 0x00053529
		internal virtual string GetOperationTitle(string operationName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			return null;
		}

		// Token: 0x02000454 RID: 1108
		private sealed class NullResourceMetadataBuilder : ODataResourceMetadataBuilder
		{
			// Token: 0x06002208 RID: 8712 RVA: 0x0005E97E File Offset: 0x0005CB7E
			private NullResourceMetadataBuilder()
			{
			}

			// Token: 0x06002209 RID: 8713 RVA: 0x0000360D File Offset: 0x0000180D
			internal override Uri GetEditLink()
			{
				return null;
			}

			// Token: 0x0600220A RID: 8714 RVA: 0x0000360D File Offset: 0x0000180D
			internal override Uri GetReadLink()
			{
				return null;
			}

			// Token: 0x0600220B RID: 8715 RVA: 0x0000360D File Offset: 0x0000180D
			internal override Uri GetId()
			{
				return null;
			}

			// Token: 0x0600220C RID: 8716 RVA: 0x0000360D File Offset: 0x0000180D
			internal override string GetETag()
			{
				return null;
			}

			// Token: 0x0600220D RID: 8717 RVA: 0x0005E7A8 File Offset: 0x0005C9A8
			internal override bool TryGetIdForSerialization(out Uri id)
			{
				id = null;
				return false;
			}

			// Token: 0x04001083 RID: 4227
			internal static readonly ODataResourceMetadataBuilder.NullResourceMetadataBuilder Instance = new ODataResourceMetadataBuilder.NullResourceMetadataBuilder();
		}
	}
}
