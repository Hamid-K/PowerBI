using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.Data.OData.Evaluation
{
	// Token: 0x0200013A RID: 314
	internal sealed class ODataConventionalUriBuilder : ODataUriBuilder
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x0001ACEE File Offset: 0x00018EEE
		internal ODataConventionalUriBuilder(Uri serviceBaseUri, UrlConvention urlConvention)
		{
			this.serviceBaseUri = serviceBaseUri;
			this.urlConvention = urlConvention;
			this.keySerializer = KeySerializer.Create(this.urlConvention);
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001AD15 File Offset: 0x00018F15
		internal override Uri BuildBaseUri()
		{
			return this.serviceBaseUri;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001AD1D File Offset: 0x00018F1D
		internal override Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, entitySetName, true);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001AD34 File Offset: 0x00018F34
		internal override Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder(UriUtilsCommon.UriToString(baseUri));
			this.AppendKeyExpression(stringBuilder, keyProperties, entityTypeName);
			return new Uri(stringBuilder.ToString(), 1);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001AD62 File Offset: 0x00018F62
		internal override Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001AD87 File Offset: 0x00018F87
		internal override Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001ADAC File Offset: 0x00018FAC
		internal override Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001ADC4 File Offset: 0x00018FC4
		internal override Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			Uri uri = ODataConventionalUriBuilder.AppendSegment(baseUri, "$links/", false);
			return ODataConventionalUriBuilder.AppendSegment(uri, navigationPropertyName, true);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		internal override Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			if (!string.IsNullOrEmpty(bindingParameterTypeName))
			{
				Uri uri = ODataConventionalUriBuilder.AppendSegment(baseUri, bindingParameterTypeName, true);
				return ODataConventionalUriBuilder.AppendSegment(uri, operationName, true);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, operationName, true);
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0001AE2E File Offset: 0x0001902E
		internal override Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, typeName, true);
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0001AE43 File Offset: 0x00019043
		[Conditional("DEBUG")]
		private static void ValidateBaseUri(Uri baseUri)
		{
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0001AE48 File Offset: 0x00019048
		private static Uri AppendSegment(Uri baseUri, string segment, bool escapeSegment)
		{
			string text = UriUtilsCommon.UriToString(baseUri);
			if (escapeSegment)
			{
				segment = Uri.EscapeDataString(segment);
			}
			if (text.get_Chars(text.Length - 1) != '/')
			{
				return new Uri(text + "/" + segment, 1);
			}
			return new Uri(baseUri, segment);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0001AE93 File Offset: 0x00019093
		private static object ValidateKeyValue(string keyPropertyName, object keyPropertyValue, string entityTypeName)
		{
			if (keyPropertyValue == null)
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_NullKeyValue(keyPropertyName, entityTypeName));
			}
			return keyPropertyValue;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0001AED4 File Offset: 0x000190D4
		private void AppendKeyExpression(StringBuilder builder, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			if (!Enumerable.Any<KeyValuePair<string, object>>(keyProperties))
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(entityTypeName));
			}
			this.keySerializer.AppendKeyExpression<KeyValuePair<string, object>>(builder, keyProperties, (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => ODataConventionalUriBuilder.ValidateKeyValue(p.Key, p.Value, entityTypeName));
		}

		// Token: 0x0400032E RID: 814
		private readonly Uri serviceBaseUri;

		// Token: 0x0400032F RID: 815
		private readonly UrlConvention urlConvention;

		// Token: 0x04000330 RID: 816
		private readonly KeySerializer keySerializer;
	}
}
