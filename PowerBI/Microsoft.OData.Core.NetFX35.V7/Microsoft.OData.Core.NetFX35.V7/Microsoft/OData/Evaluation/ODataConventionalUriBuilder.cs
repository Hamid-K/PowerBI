using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Evaluation
{
	// Token: 0x02000227 RID: 551
	internal sealed class ODataConventionalUriBuilder : ODataUriBuilder
	{
		// Token: 0x0600165E RID: 5726 RVA: 0x00045097 File Offset: 0x00043297
		internal ODataConventionalUriBuilder(Uri serviceBaseUri, ODataUrlKeyDelimiter urlKeyDelimiter)
		{
			this.serviceBaseUri = serviceBaseUri;
			this.keySerializer = KeySerializer.Create(urlKeyDelimiter.EnableKeyAsSegment);
		}

		// Token: 0x0600165F RID: 5727 RVA: 0x000450B7 File Offset: 0x000432B7
		internal override Uri BuildBaseUri()
		{
			return this.serviceBaseUri;
		}

		// Token: 0x06001660 RID: 5728 RVA: 0x000450BF File Offset: 0x000432BF
		internal override Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, entitySetName, true);
		}

		// Token: 0x06001661 RID: 5729 RVA: 0x000450D4 File Offset: 0x000432D4
		internal override Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder(UriUtils.UriToString(baseUri));
			this.AppendKeyExpression(stringBuilder, keyProperties, entityTypeName);
			return new Uri(stringBuilder.ToString(), 1);
		}

		// Token: 0x06001662 RID: 5730 RVA: 0x00045102 File Offset: 0x00043302
		internal override Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x06001663 RID: 5731 RVA: 0x00045102 File Offset: 0x00043302
		internal override Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x06001664 RID: 5732 RVA: 0x00045127 File Offset: 0x00043327
		internal override Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
		}

		// Token: 0x06001665 RID: 5733 RVA: 0x0004513C File Offset: 0x0004333C
		internal override Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			Uri uri = ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
			return ODataConventionalUriBuilder.AppendSegment(uri, "$ref", false);
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x0004516C File Offset: 0x0004336C
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
				operationName += string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<string, string>(parameterNames.Split(new char[] { ',' }), (string p) => p + "=@" + p)));
				operationName += ")";
			}
			return ODataConventionalUriBuilder.AppendSegment(uri, operationName, false);
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x0004520F File Offset: 0x0004340F
		internal override Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, typeName, true);
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x0000250D File Offset: 0x0000070D
		[Conditional("DEBUG")]
		private static void ValidateBaseUri(Uri baseUri)
		{
		}

		// Token: 0x06001669 RID: 5737 RVA: 0x00045224 File Offset: 0x00043424
		private static Uri AppendSegment(Uri baseUri, string segment, bool escapeSegment)
		{
			string text = UriUtils.UriToString(baseUri);
			if (escapeSegment)
			{
				segment = Uri.EscapeDataString(segment);
			}
			if (text.get_Chars(text.Length - 1) != '/')
			{
				return new Uri(text + "/" + segment, 0);
			}
			return new Uri(baseUri, segment);
		}

		// Token: 0x0600166A RID: 5738 RVA: 0x0004526F File Offset: 0x0004346F
		private static object ValidateKeyValue(string keyPropertyName, object keyPropertyValue, string entityTypeName)
		{
			if (keyPropertyValue == null)
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_NullKeyValue(keyPropertyName, entityTypeName));
			}
			return keyPropertyValue;
		}

		// Token: 0x0600166B RID: 5739 RVA: 0x00045284 File Offset: 0x00043484
		private void AppendKeyExpression(StringBuilder builder, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			if (!Enumerable.Any<KeyValuePair<string, object>>(keyProperties))
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(entityTypeName));
			}
			this.keySerializer.AppendKeyExpression<KeyValuePair<string, object>>(builder, keyProperties, (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => ODataConventionalUriBuilder.ValidateKeyValue(p.Key, p.Value, entityTypeName));
		}

		// Token: 0x04000A5B RID: 2651
		private readonly Uri serviceBaseUri;

		// Token: 0x04000A5C RID: 2652
		private readonly KeySerializer keySerializer;
	}
}
