using System;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000255 RID: 597
	internal static class ODataJsonLightValidationUtils
	{
		// Token: 0x06001AC8 RID: 6856 RVA: 0x0005170C File Offset: 0x0004F90C
		internal static void ValidateMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			string text = propertyName;
			if (propertyName[0] == '#')
			{
				text = UriUtils.UriToString(metadataDocumentUri) + UriUtils.EnsureEscapedFragment(propertyName);
			}
			if (!Uri.IsWellFormedUriString(text, UriKind.Absolute) || !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) || propertyName[propertyName.Length - 1] == '#')
			{
				throw new ODataException(Strings.ValidationUtils_InvalidMetadataReferenceProperty(propertyName));
			}
			if (ODataJsonLightValidationUtils.IsOpenMetadataReferencePropertyName(metadataDocumentUri, propertyName))
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(propertyName, UriUtils.UriToString(metadataDocumentUri)));
			}
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00051784 File Offset: 0x0004F984
		internal static void ValidateOperation(Uri metadataDocumentUri, ODataOperation operation)
		{
			ValidationUtils.ValidateOperationMetadataNotNull(operation);
			string text = UriUtils.UriToString(operation.Metadata);
			if (metadataDocumentUri != null)
			{
				ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(metadataDocumentUri, text);
			}
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x000517B3 File Offset: 0x0004F9B3
		internal static bool IsOpenMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) && propertyName[0] != '#' && !propertyName.StartsWith(UriUtils.UriToString(metadataDocumentUri), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000517DA File Offset: 0x0004F9DA
		internal static void ValidateOperationPropertyValueIsNotNull(object propertyValue, string propertyName, string metadata)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(propertyName, metadata));
			}
		}
	}
}
