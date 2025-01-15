using System;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x02000141 RID: 321
	internal static class ODataJsonLightValidationUtils
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x0001B5F0 File Offset: 0x000197F0
		internal static void ValidateMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			string text = propertyName;
			if (propertyName.get_Chars(0) == '#')
			{
				text = UriUtilsCommon.UriToString(metadataDocumentUri) + UriUtils.EnsureEscapedFragment(propertyName);
			}
			if (!Uri.IsWellFormedUriString(text, 1) || !ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) || propertyName.get_Chars(propertyName.Length - 1) == '#')
			{
				throw new ODataException(Strings.ValidationUtils_InvalidMetadataReferenceProperty(propertyName));
			}
			if (ODataJsonLightValidationUtils.IsOpenMetadataReferencePropertyName(metadataDocumentUri, propertyName))
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OpenMetadataReferencePropertyNotSupported(propertyName, UriUtilsCommon.UriToString(metadataDocumentUri)));
			}
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0001B668 File Offset: 0x00019868
		internal static void ValidateOperation(Uri metadataDocumentUri, ODataOperation operation)
		{
			ValidationUtils.ValidateOperationMetadataNotNull(operation);
			string text = UriUtilsCommon.UriToString(operation.Metadata);
			if (metadataDocumentUri != null)
			{
				ODataJsonLightValidationUtils.ValidateMetadataReferencePropertyName(metadataDocumentUri, text);
			}
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0001B697 File Offset: 0x00019897
		internal static bool IsOpenMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.IsMetadataReferenceProperty(propertyName) && propertyName.get_Chars(0) != '#' && !propertyName.StartsWith(UriUtilsCommon.UriToString(metadataDocumentUri), 5);
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0001B6BE File Offset: 0x000198BE
		internal static void ValidateOperationPropertyValueIsNotNull(object propertyValue, string propertyName, string metadata)
		{
			if (propertyValue == null)
			{
				throw new ODataException(Strings.ODataJsonLightValidationUtils_OperationPropertyCannotBeNull(propertyName, metadata));
			}
		}
	}
}
