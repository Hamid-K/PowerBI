using System;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000FE RID: 254
	internal static class ODataJsonLightValidationUtils
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x000232C4 File Offset: 0x000214C4
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

		// Token: 0x0600099E RID: 2462 RVA: 0x0002333C File Offset: 0x0002153C
		internal static void ValidateOperation(Uri metadataDocumentUri, ODataOperation operation)
		{
			ValidationUtils.ValidateOperationMetadataNotNull(operation);
			string text = UriUtils.UriToString(operation.Metadata);
			if (metadataDocumentUri != null)
			{
				ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(metadataDocumentUri, text);
			}
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0002336B File Offset: 0x0002156B
		internal static bool IsOpenMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) && propertyName.get_Chars(0) != '#' && !propertyName.StartsWith(UriUtils.UriToString(metadataDocumentUri), 5);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00023392 File Offset: 0x00021592
		internal static void ValidateOperationPropertyValueIsNotNull(object propertyValue, string propertyName, string metadata)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(propertyName, metadata));
			}
		}
	}
}
