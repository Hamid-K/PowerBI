using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4.Write
{
	// Token: 0x020008A8 RID: 2216
	internal static class WriteHelper
	{
		// Token: 0x06003F65 RID: 16229 RVA: 0x000D0D48 File Offset: 0x000CEF48
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
						object obj = new ValueODataValueVisitor().ToSource(row[entityKey], edmPrimitiveTypeReference.Definition);
						KeyValuePair<string, object>[] array = new KeyValuePair<string, object>[]
						{
							new KeyValuePair<string, object>(entityKey, obj)
						};
						list.Add(new KeySegment(array, edmEntityType, edmEntitySet));
					}
					goto IL_01C9;
				}
			}
			if (metadata.NavigationSource != null && metadata.NavigationSource.NavigationSourceKind() == Microsoft.OData.Edm.EdmNavigationSourceKind.Singleton)
			{
				Microsoft.OData.Edm.IEdmSingleton edmSingleton = (Microsoft.OData.Edm.IEdmSingleton)metadata.NavigationSource;
				list.Add(new SingletonSegment(edmSingleton));
			}
			IL_01C9:
			odataUri.Path = new ODataPath(list);
			return odataUri.GetUri();
		}

		// Token: 0x06003F66 RID: 16230 RVA: 0x000D0F40 File Offset: 0x000CF140
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

		// Token: 0x06003F67 RID: 16231 RVA: 0x000D0F88 File Offset: 0x000CF188
		public static Value ReadFromHttpResponseData(HttpResponseData data, ODataEnvironment environment, Uri odataUri)
		{
			string text = data.TryGetContextUrl();
			ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataJsonLightContextUriParser.Parse(environment.Settings.EdmModel, text, true);
			return ODataResponse.CreateValueFromPayload(environment, odataUri, data, odataJsonLightContextUriParseResult);
		}
	}
}
