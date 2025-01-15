using System;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x0200021B RID: 539
	internal static class ODataJsonLightUtils
	{
		// Token: 0x060015E4 RID: 5604 RVA: 0x00043063 File Offset: 0x00041263
		internal static bool IsMetadataReferenceProperty(string propertyName)
		{
			return propertyName.IndexOf('#') >= 0;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00043074 File Offset: 0x00041274
		internal static string GetFullyQualifiedOperationName(Uri metadataDocumentUri, string metadataReferencePropertyName, out string parameterNames)
		{
			string text = ODataJsonLightUtils.GetUriFragmentFromMetadataReferencePropertyName(metadataDocumentUri, metadataReferencePropertyName);
			parameterNames = null;
			int num = text.IndexOf('(');
			if (num > -1)
			{
				string text2 = text.Substring(num + 1);
				text = text.Substring(0, num);
				parameterNames = text2.Trim(ODataJsonLightUtils.CharactersToTrimFromParameters);
			}
			return text;
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000430BC File Offset: 0x000412BC
		internal static string GetUriFragmentFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, propertyName).GetComponents(64, 2);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x000430DA File Offset: 0x000412DA
		internal static Uri GetAbsoluteUriFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			if (propertyName.get_Chars(0) == '#')
			{
				propertyName = UriUtils.EnsureEscapedFragment(propertyName);
				return new Uri(metadataDocumentUri, propertyName);
			}
			return new Uri(propertyName, 1);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x00043100 File Offset: 0x00041300
		internal static string GetMetadataReferenceName(IEdmModel model, IEdmOperation operation)
		{
			string text = operation.FullName();
			bool flag = Enumerable.Count<IEdmOperation>(Enumerable.Take<IEdmOperation>(model.FindDeclaredOperations(operation.FullName()), 2)) > 1;
			if (flag && operation is IEdmFunction)
			{
				text = operation.FullNameWithNonBindingParameters();
			}
			return text;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00043144 File Offset: 0x00041344
		internal static ODataOperation CreateODataOperation(Uri metadataDocumentUri, string metadataReferencePropertyName, IEdmOperation edmOperation, out bool isAction)
		{
			isAction = edmOperation.IsAction();
			ODataOperation odataOperation = (isAction ? new ODataAction() : new ODataFunction());
			int num;
			if (isAction && (num = metadataReferencePropertyName.IndexOf('(')) > 0)
			{
				metadataReferencePropertyName = metadataReferencePropertyName.Substring(0, num);
			}
			odataOperation.Metadata = ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, metadataReferencePropertyName);
			return odataOperation;
		}

		// Token: 0x04000A43 RID: 2627
		private static readonly char[] CharactersToTrimFromParameters = new char[] { '(', ')' };
	}
}
