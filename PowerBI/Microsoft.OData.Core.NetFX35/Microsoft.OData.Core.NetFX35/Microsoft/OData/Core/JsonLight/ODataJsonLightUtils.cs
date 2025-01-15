using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.JsonLight
{
	// Token: 0x020000FD RID: 253
	internal static class ODataJsonLightUtils
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x00023169 File Offset: 0x00021369
		internal static bool IsMetadataReferenceProperty(string propertyName)
		{
			return propertyName.IndexOf('#') >= 0;
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0002317C File Offset: 0x0002137C
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

		// Token: 0x06000998 RID: 2456 RVA: 0x000231C4 File Offset: 0x000213C4
		internal static string GetUriFragmentFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			return ODataJsonLightUtils.GetAbsoluteUriFromMetadataReferencePropertyName(metadataDocumentUri, propertyName).GetComponents(64, 2);
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x000231E2 File Offset: 0x000213E2
		internal static Uri GetAbsoluteUriFromMetadataReferencePropertyName(Uri metadataDocumentUri, string propertyName)
		{
			if (propertyName.get_Chars(0) == '#')
			{
				propertyName = UriUtils.EnsureEscapedFragment(propertyName);
				return new Uri(metadataDocumentUri, propertyName);
			}
			return new Uri(propertyName, 1);
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00023208 File Offset: 0x00021408
		[SuppressMessage("DataWeb.Usage", "AC0003:MethodCallNotAllowed", Justification = "This method is used for matching the name of the operation to something written by the server. So using the name is safe without resolving the type from the client.")]
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

		// Token: 0x0600099B RID: 2459 RVA: 0x0002324C File Offset: 0x0002144C
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

		// Token: 0x040003D3 RID: 979
		private static readonly char[] CharactersToTrimFromParameters = new char[] { '(', ')' };
	}
}
