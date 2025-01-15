using System;
using System.Linq;
using Microsoft.Data.Edm;
using Microsoft.Data.OData.Metadata;

namespace Microsoft.Data.OData.JsonLight
{
	// Token: 0x0200013F RID: 319
	internal static class ODataJsonLightUtils
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x0001B138 File Offset: 0x00019338
		internal static bool IsMetadataReferenceProperty(string propertyName)
		{
			return propertyName.IndexOf('#') >= 0;
		}

		// Token: 0x06000860 RID: 2144 RVA: 0x0001B148 File Offset: 0x00019348
		internal static string GetFullyQualifiedFunctionImportName(Uri metadataDocumentUri, string metadataReferencePropertyName, out string firstParameterTypeName)
		{
			string text = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(metadataDocumentUri, metadataReferencePropertyName);
			firstParameterTypeName = null;
			int num = text.IndexOf('(');
			if (num > -1)
			{
				string text2 = text.Substring(num + 1);
				text = text.Substring(0, num);
				firstParameterTypeName = Enumerable.First<string>(text2.Split(ODataJsonLightUtils.ParameterSeparatorSplitCharacters)).Trim(ODataJsonLightUtils.CharactersToTrimFromParameters);
			}
			return text;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001B1A0 File Offset: 0x000193A0
		internal static string GetUriFragmentFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, propertyName).GetComponents(64, 2);
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001B1BE File Offset: 0x000193BE
		internal static Uri GetAbsoluteUriFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			if (propertyName.get_Chars(0) == '#')
			{
				propertyName = UriUtils.EnsureEscapedFragment(propertyName);
				return new Uri(metadataDocumentUri, propertyName);
			}
			return new Uri(propertyName, 1);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001B1E4 File Offset: 0x000193E4
		internal static string GetMetadataReferenceName(IEdmFunctionImport functionImport)
		{
			string text = functionImport.FullName();
			bool flag = Enumerable.Count<IEdmFunctionImport>(Enumerable.Take<IEdmFunctionImport>(functionImport.Container.FindFunctionImports(functionImport.Name), 2)) > 1;
			if (flag)
			{
				text = functionImport.FullNameWithParameters();
			}
			return text;
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001B224 File Offset: 0x00019424
		internal static ODataOperation CreateODataOperation(Uri metadataDocumentUri, string metadataReferencePropertyName, IEdmFunctionImport functionImport, out bool isAction)
		{
			isAction = functionImport.IsSideEffecting;
			ODataOperation odataOperation = (isAction ? new ODataAction() : new ODataFunction());
			odataOperation.Metadata = ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, metadataReferencePropertyName);
			return odataOperation;
		}

		// Token: 0x04000349 RID: 841
		private static readonly char[] ParameterSeparatorSplitCharacters = new char[] { ",".get_Chars(0) };

		// Token: 0x0400034A RID: 842
		private static readonly char[] CharactersToTrimFromParameters = new char[] { '(', ')' };
	}
}
