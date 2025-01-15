using System;
using System.Linq;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.JsonLight
{
	// Token: 0x02000254 RID: 596
	internal static class ODataJsonLightUtils
	{
		// Token: 0x06001AC1 RID: 6849 RVA: 0x000515C3 File Offset: 0x0004F7C3
		internal static bool IsMetadataReferenceProperty(string propertyName)
		{
			return propertyName.IndexOf('#') >= 0;
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x000515D4 File Offset: 0x0004F7D4
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

		// Token: 0x06001AC3 RID: 6851 RVA: 0x0005161C File Offset: 0x0004F81C
		internal static string GetUriFragmentFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, propertyName).GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0005163A File Offset: 0x0004F83A
		internal static Uri GetAbsoluteUriFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			if (propertyName[0] == '#')
			{
				propertyName = UriUtils.EnsureEscapedFragment(propertyName);
				return new Uri(metadataDocumentUri, propertyName);
			}
			return new Uri(propertyName, UriKind.Absolute);
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x00051660 File Offset: 0x0004F860
		internal static string GetMetadataReferenceName(IEdmModel model, IEdmOperation operation)
		{
			string text = operation.FullName();
			bool flag = model.FindDeclaredOperations(operation.FullName()).Take(2).Count<IEdmOperation>() > 1;
			if (flag && operation is IEdmFunction)
			{
				text = operation.FullNameWithNonBindingParameters();
			}
			return text;
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x000516A4 File Offset: 0x0004F8A4
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

		// Token: 0x04000B5F RID: 2911
		private static readonly char[] CharactersToTrimFromParameters = new char[] { '(', ')' };
	}
}
