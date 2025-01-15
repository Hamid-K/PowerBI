using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Microsoft.OData.Core.Evaluation
{
	// Token: 0x02000083 RID: 131
	internal sealed class ODataConventionalUriBuilder : ODataUriBuilder
	{
		// Token: 0x06000546 RID: 1350 RVA: 0x00013346 File Offset: 0x00011546
		internal ODataConventionalUriBuilder(Uri serviceBaseUri, UrlConvention urlConvention)
		{
			this.serviceBaseUri = serviceBaseUri;
			this.urlConvention = urlConvention;
			this.keySerializer = KeySerializer.Create(this.urlConvention);
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x0001336D File Offset: 0x0001156D
		internal override Uri BuildBaseUri()
		{
			return this.serviceBaseUri;
		}

		// Token: 0x06000548 RID: 1352 RVA: 0x00013375 File Offset: 0x00011575
		internal override Uri BuildEntitySetUri(Uri baseUri, string entitySetName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(entitySetName, "entitySetName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, entitySetName, true);
		}

		// Token: 0x06000549 RID: 1353 RVA: 0x0001338C File Offset: 0x0001158C
		internal override Uri BuildEntityInstanceUri(Uri baseUri, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			StringBuilder stringBuilder = new StringBuilder(UriUtils.UriToString(baseUri));
			this.AppendKeyExpression(stringBuilder, keyProperties, entityTypeName);
			return new Uri(stringBuilder.ToString(), 1);
		}

		// Token: 0x0600054A RID: 1354 RVA: 0x000133BA File Offset: 0x000115BA
		internal override Uri BuildStreamEditLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x0600054B RID: 1355 RVA: 0x000133DF File Offset: 0x000115DF
		internal override Uri BuildStreamReadLinkUri(Uri baseUri, string streamPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotEmpty(streamPropertyName, "streamPropertyName");
			if (streamPropertyName == null)
			{
				return ODataConventionalUriBuilder.AppendSegment(baseUri, "$value", false);
			}
			return ODataConventionalUriBuilder.AppendSegment(baseUri, streamPropertyName, true);
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00013404 File Offset: 0x00011604
		internal override Uri BuildNavigationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x0001341C File Offset: 0x0001161C
		internal override Uri BuildAssociationLinkUri(Uri baseUri, string navigationPropertyName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(navigationPropertyName, "navigationPropertyName");
			Uri uri = ODataConventionalUriBuilder.AppendSegment(baseUri, navigationPropertyName, true);
			return ODataConventionalUriBuilder.AppendSegment(uri, "$ref", false);
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x00013458 File Offset: 0x00011658
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
				operationName += '(';
				operationName += string.Join(",", Enumerable.ToArray<string>(Enumerable.Select<string, string>(parameterNames.Split(new char[] { ',' }), (string p) => p + "=@" + p)));
				operationName += ')';
			}
			return ODataConventionalUriBuilder.AppendSegment(uri, operationName, false);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x000134FF File Offset: 0x000116FF
		internal override Uri AppendTypeSegment(Uri baseUri, string typeName)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(typeName, "typeName");
			return ODataConventionalUriBuilder.AppendSegment(baseUri, typeName, true);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00013514 File Offset: 0x00011714
		[Conditional("DEBUG")]
		private static void ValidateBaseUri(Uri baseUri)
		{
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00013518 File Offset: 0x00011718
		[SuppressMessage("DataWeb.Usage", "AC0018:SystemUriEscapeDataStringRule", Justification = "Values passed to this method are model elements like property names or keywords.")]
		private static Uri AppendSegment(Uri baseUri, string segment, bool escapeSegment)
		{
			string text = UriUtils.UriToString(baseUri);
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

		// Token: 0x06000552 RID: 1362 RVA: 0x00013563 File Offset: 0x00011763
		private static object ValidateKeyValue(string keyPropertyName, object keyPropertyValue, string entityTypeName)
		{
			if (keyPropertyValue == null)
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_NullKeyValue(keyPropertyName, entityTypeName));
			}
			return keyPropertyValue;
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x000135A4 File Offset: 0x000117A4
		private void AppendKeyExpression(StringBuilder builder, ICollection<KeyValuePair<string, object>> keyProperties, string entityTypeName)
		{
			if (!Enumerable.Any<KeyValuePair<string, object>>(keyProperties))
			{
				throw new ODataException(Strings.ODataConventionalUriBuilder_EntityTypeWithNoKeyProperties(entityTypeName));
			}
			this.keySerializer.AppendKeyExpression<KeyValuePair<string, object>>(builder, keyProperties, (KeyValuePair<string, object> p) => p.Key, (KeyValuePair<string, object> p) => ODataConventionalUriBuilder.ValidateKeyValue(p.Key, p.Value, entityTypeName));
		}

		// Token: 0x0400023A RID: 570
		private readonly Uri serviceBaseUri;

		// Token: 0x0400023B RID: 571
		private readonly UrlConvention urlConvention;

		// Token: 0x0400023C RID: 572
		private readonly KeySerializer keySerializer;
	}
}
