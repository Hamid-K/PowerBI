using System;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200021C RID: 540
	internal static class ODataJsonLightValidationUtils
	{
		// Token: 0x060015EB RID: 5611 RVA: 0x000431AC File Offset: 0x000413AC
		internal static void ValidateMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			string text = propertyName;
			if (propertyName.get_Chars(0) == '#')
			{
				text = UriUtils.UriToString(metadataDocumentUri) + UriUtils.EnsureEscapedFragment(propertyName);
			}
			if (!Uri.IsWellFormedUriString(text, 1) || !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) || propertyName.get_Chars(propertyName.Length - 1) == '#')
			{
				throw new ODataException(Strings.ValidationUtils_InvalidMetadataReferenceProperty(propertyName));
			}
			if (ODataJsonLightValidationUtils.IsOpenMetadataReferencePropertyName(metadataDocumentUri, propertyName))
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(propertyName, UriUtils.UriToString(metadataDocumentUri)));
			}
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00043224 File Offset: 0x00041424
		internal static void ValidateOperation(Uri metadataDocumentUri, ODataOperation operation)
		{
			ValidationUtils.ValidateOperationMetadataNotNull(operation);
			string text = UriUtils.UriToString(operation.Metadata);
			if (metadataDocumentUri != null)
			{
				ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(metadataDocumentUri, text);
			}
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x00043253 File Offset: 0x00041453
		internal static bool IsOpenMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) && propertyName.get_Chars(0) != '#' && !propertyName.StartsWith(UriUtils.UriToString(metadataDocumentUri), 5);
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0004327A File Offset: 0x0004147A
		internal static void ValidateOperationPropertyValueIsNotNull(object propertyValue, string propertyName, string metadata)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(propertyName, metadata));
			}
		}
	}
}
