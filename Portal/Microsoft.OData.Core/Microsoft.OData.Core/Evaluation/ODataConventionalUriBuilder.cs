using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000262 RID: 610
	internal sealed class ODataConventionalUriBuilder : ODataUriBuilder
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x0005504C File Offset: 0x0005324C
		internal ODataConventionalUriBuilder(Uri serviceBaseUri, ODataUrlKeyDelimiter urlKeyDelimiter)
		{
			this.serviceBaseUri = serviceBaseUri;
			this.keySerializer = KeySerializer.Create(urlKeyDelimiter.EnableKeyAsSegment);
		}

		// Token: 0x06001B87 RID: 7047 RVA: 0x0005506C File Offset: 0x0005326C
		internal override Uri BuildBaseUri()
		{
			return this.serviceBaseUri;
		}

		// Token: 0x06001B88 RID: 7048 RVA: 0x00055074 File Offset: 0x00053274
		internal override Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, entitySetName, true);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x0005508C File Offset: 0x0005328C
		internal override Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder(UriUtils.UriToString(baseUri));
			this.AppendKeyExpression(stringBuilder, keyProperties, entityTypeName);
			return new Uri(stringBuilder.ToString(), UriKind.Absolute);
		}

		// Token: 0x06001B8A RID: 7050 RVA: 0x000550BA File Offset: 0x000532BA
		internal override Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x06001B8B RID: 7051 RVA: 0x000550BA File Offset: 0x000532BA
		internal override Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x06001B8C RID: 7052 RVA: 0x000550DF File Offset: 0x000532DF
		internal override Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
		}

		// Token: 0x06001B8D RID: 7053 RVA: 0x000550F4 File Offset: 0x000532F4
		internal override Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			Uri uri = ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
			return ODataConventionalUriBuilder.AppendSegment(uri, "$ref", false);
		}

		// Token: 0x06001B8E RID: 7054 RVA: 0x00055124 File Offset: 0x00053324
		internal override Uri BuildOperationTargetUri(Uri baseUri, string operationName, string bindingParameterTypeName, string parameterNames)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(operationName, "operationName");
			Uri uri = baseUri;
			if (!string.IsNullOrEmpty(bindingParameterTypeName))
			{
				uri = ODataConventionalUriBuilder.AppendSegment(baseUri, bindingParameterTypeName, true);
			}
			if (!string.IsNullOrEmpty(parameterNames))
			{
				operationName += "(";
				operationName += string.Join(",", (from p in parameterNames.Split(new char[] { ',' })
					select p + "=@" + p).ToArray<string>());
				operationName += ")";
			}
			return ODataConventionalUriBuilder.AppendSegment(uri, operationName, false);
		}

		// Token: 0x06001B8F RID: 7055 RVA: 0x000551C7 File Offset: 0x000533C7
		internal override Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, typeName, true);
		}

		// Token: 0x06001B90 RID: 7056 RVA: 0x0000239D File Offset: 0x0000059D
		[Conditional("DEBUG")]
		private static void ValidateBaseUri(Uri baseUri)
		{
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000551DC File Offset: 0x000533DC
		private static Uri AppendSegment(Uri baseUri, string segment, bool escapeSegment)
		{
			string text = UriUtils.UriToString(baseUri);
			if (escapeSegment)
			{
				segment = Uri.EscapeDataString(segment);
			}
			if (text[text.Length - 1] != '/')
			{
				return new Uri(text + "/" + segment, UriKind.RelativeOrAbsolute);
			}
			return new Uri(baseUri, segment);
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x00055227 File Offset: 0x00053427
		private static object ValidateKeyValue(string keyPropertyName, object keyPropertyValue, string entityTypeName)
		{
			if (keyPropertyValue == null)
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_NullKeyValue(keyPropertyName, entityTypeName));
			}
			return keyPropertyValue;
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x0005523C File Offset: 0x0005343C
		private void AppendKeyExpression(StringBuilder builder, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			if (!keyProperties.Any<KeyValuePair<string, object>>())
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(entityTypeName));
			}
			this.keySerializer.AppendKeyExpression<KeyValuePair<string, object>>(builder, keyProperties, (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => ODataConventionalUriBuilder.ValidateKeyValue(p.Key, p.Value, entityTypeName));
		}

		// Token: 0x04000B86 RID: 2950
		private readonly Uri serviceBaseUri;

		// Token: 0x04000B87 RID: 2951
		private readonly KeySerializer keySerializer;
	}
}
