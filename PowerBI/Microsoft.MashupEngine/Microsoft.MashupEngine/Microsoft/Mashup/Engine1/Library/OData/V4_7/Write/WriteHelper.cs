using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Write
{
	// Token: 0x0200078B RID: 1931
	internal static class WriteHelper
	{
		// Token: 0x060038AB RID: 14507 RVA: 0x000B6D60 File Offset: 0x000B4F60
		public static Uri CreateCanonicalUri(RecordValue row, ODataQueryMetadata metadata, Uri serviceRoot)
		{
			RecordValue asRecord = row.MetaValue.AsRecord;
			Value value;
			if (asRecord.TryGetValue("@odata.id", out value) && !value.IsNull)
			{
				return new Uri(value.AsText.AsString);
			}
			if (asRecord.TryGetValue("@odata.editLink", out value) && !value.IsNull)
			{
				return new Uri(value.AsText.AsString);
			}
			ODataUri odataUri = new ODataUri();
			odataUri.ServiceRoot = serviceRoot;
			List<ODataPathSegment> list = new List<ODataPathSegment>();
			if (metadata.NavigationSource != null && metadata.NavigationSource.NavigationSourceKind() == Microsoft.OData.Edm.EdmNavigationSourceKind.EntitySet)
			{
				Microsoft.OData.Edm.IEdmEntitySet edmEntitySet = (Microsoft.OData.Edm.IEdmEntitySet)metadata.NavigationSource;
				Microsoft.OData.Edm.IEdmEntityType edmEntityType = edmEntitySet.EntityType();
				list.Add(new EntitySetSegment(edmEntitySet));
				IEnumerable<Microsoft.OData.Edm.IEdmStructuralProperty> enumerable = edmEntityType.Key();
				using (Keys.StringKeysEnumerator enumerator = metadata.KeyColumns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string entityKey = enumerator.Current;
						Microsoft.OData.Edm.IEdmTypeReference type = enumerable.Where((Microsoft.OData.Edm.IEdmStructuralProperty s) => s.Name == entityKey).Single<Microsoft.OData.Edm.IEdmStructuralProperty>().Type;
						if (type == null || type.TypeKind() != Microsoft.OData.Edm.EdmTypeKind.Primitive)
						{
							throw ValueException.NewDataSourceError<Message0>(Strings.CannotBuildCanonicalUrl, TextValue.New(type.FullName()), null);
						}
						Microsoft.OData.Edm.IEdmPrimitiveTypeReference edmPrimitiveTypeReference = type as Microsoft.OData.Edm.IEdmPrimitiveTypeReference;
						object obj = ODataResourceValueWriter.MarshalSimpleValue(row[entityKey], edmPrimitiveTypeReference.Definition);
						KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>(entityKey, obj)
						};
						list.Add(new KeySegment(array, edmEntityType, edmEntitySet));
					}
					goto IL_01C4;
				}
			}
			if (metadata.NavigationSource != null && metadata.NavigationSource.NavigationSourceKind() == Microsoft.OData.Edm.EdmNavigationSourceKind.Singleton)
			{
				Microsoft.OData.Edm.IEdmSingleton edmSingleton = (Microsoft.OData.Edm.IEdmSingleton)metadata.NavigationSource;
				list.Add(new SingletonSegment(edmSingleton));
			}
			IL_01C4:
			odataUri.Path = new ODataPath(list);
			return odataUri.GetUri();
		}

		// Token: 0x060038AC RID: 14508 RVA: 0x000B6F54 File Offset: 0x000B5154
		public static Dictionary<string, string> GetETagHeader(RecordValue row)
		{
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Value value;
			if (row.MetaValue.TryGetValue("@odata.etag", out value) && !value.IsNull)
			{
				dictionary.Add("If-Match", value.AsText.ToString());
			}
			return dictionary;
		}

		// Token: 0x060038AD RID: 14509 RVA: 0x000B6F9C File Offset: 0x000B519C
		public static Value ReadFromHttpResponseData(HttpResponseData data, ODataEnvironment environment, Uri odataUri)
		{
			Value value;
			using (IODataPayloadReader iodataPayloadReader = ODataResponse.CreateResponseReader(environment.Host, environment.HttpResource, odataUri, data, environment.EdmModel))
			{
				ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(environment.Settings.EdmModel, iodataPayloadReader.ContextUrl, true);
				value = ODataResponse.CreateValueFromPayload(environment, odataUri, iodataPayloadReader, odataJsonLightContextUriParseResult, null);
			}
			return value;
		}
	}
}
