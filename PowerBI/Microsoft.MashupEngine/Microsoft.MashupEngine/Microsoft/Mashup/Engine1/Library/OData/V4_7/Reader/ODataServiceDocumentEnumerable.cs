using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x020007B6 RID: 1974
	internal class ODataServiceDocumentEnumerable : IEnumerable<IValueReference>, IEnumerable
	{
		// Token: 0x06003994 RID: 14740 RVA: 0x000B9718 File Offset: 0x000B7918
		public ODataServiceDocumentEnumerable(ODataEnvironment environment, ODataServiceDocument serviceDocument)
		{
			this.environment = environment;
			this.elements = new SortedDictionary<ODataSchemaItem, ODataServiceDocumentElement>();
			foreach (ODataServiceDocumentElement odataServiceDocumentElement in (serviceDocument.EntitySets ?? Enumerable.Empty<ODataEntitySetInfo>()))
			{
				ODataSchemaItem odataSchemaItem = new ODataSchemaItem(odataServiceDocumentElement.Name, "table");
				if (!this.elements.ContainsKey(odataSchemaItem))
				{
					this.elements.Add(odataSchemaItem, odataServiceDocumentElement);
				}
			}
			foreach (ODataServiceDocumentElement odataServiceDocumentElement2 in (serviceDocument.Singletons ?? Enumerable.Empty<ODataSingletonInfo>()))
			{
				ODataSchemaItem odataSchemaItem2 = new ODataSchemaItem(odataServiceDocumentElement2.Name, "singleton");
				if (!this.elements.ContainsKey(odataSchemaItem2))
				{
					this.elements.Add(odataSchemaItem2, odataServiceDocumentElement2);
				}
			}
			foreach (ODataServiceDocumentElement odataServiceDocumentElement3 in (serviceDocument.FunctionImports ?? Enumerable.Empty<ODataFunctionImportInfo>()))
			{
				ODataSchemaItem odataSchemaItem3 = new ODataSchemaItem(odataServiceDocumentElement3.Name, ODataServiceDocumentEnumerable.PlaceholderFunctionImportSignature);
				if (!this.elements.ContainsKey(odataSchemaItem3))
				{
					this.elements.Add(odataSchemaItem3, odataServiceDocumentElement3);
				}
			}
			foreach (KeyValuePair<string, List<KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>> keyValuePair in environment.UnboundFunctionOverloads)
			{
				ODataSchemaItem odataSchemaItem4 = new ODataSchemaItem(keyValuePair.Key, ODataServiceDocumentEnumerable.PlaceholderFunctionImportSignature);
				if (!this.elements.ContainsKey(odataSchemaItem4) && keyValuePair.Value.Count > 0)
				{
					ODataServiceDocumentElement odataServiceDocumentElement4 = new ODataFunctionImportInfo
					{
						Name = keyValuePair.Key
					};
					this.elements.Add(odataSchemaItem4, odataServiceDocumentElement4);
				}
			}
		}

		// Token: 0x06003995 RID: 14741 RVA: 0x000B9924 File Offset: 0x000B7B24
		public IEnumerator<IValueReference> GetEnumerator()
		{
			return this.elements.Values.SelectMany(delegate(ODataServiceDocumentElement element)
			{
				if (element is ODataEntitySetInfo || element is ODataSingletonInfo)
				{
					return this.CreateNavigationSourceRows(element);
				}
				if (element is ODataFunctionImportInfo)
				{
					return this.CreateFunctionImportRows(element);
				}
				return Enumerable.Empty<IValueReference>();
			}).GetEnumerator();
		}

		// Token: 0x06003996 RID: 14742 RVA: 0x000B9947 File Offset: 0x000B7B47
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003997 RID: 14743 RVA: 0x000B994F File Offset: 0x000B7B4F
		private IEnumerable<IValueReference> CreateNavigationSourceRows(ODataServiceDocumentElement element)
		{
			Microsoft.OData.Edm.IEdmNavigationSource edmNavigationSource;
			string text;
			if (element is ODataEntitySetInfo)
			{
				edmNavigationSource = this.environment.EdmModel.EntityContainer.FindEntitySet(element.Name);
				text = "table";
			}
			else
			{
				if (!(element is ODataSingletonInfo))
				{
					yield break;
				}
				edmNavigationSource = this.environment.EdmModel.EntityContainer.FindSingleton(element.Name);
				text = "singleton";
			}
			TypeValue navigationSourceTypeValue = this.GetNavigationSourceTypeValue(edmNavigationSource);
			IValueReference valueReference;
			if (navigationSourceTypeValue.IsTableType)
			{
				TableValue tableValue = ODataValue.CreateNavigationSourceTableValue(this.environment, element.Url, edmNavigationSource, navigationSourceTypeValue.AsTableType);
				valueReference = this.AddRelationships(tableValue, edmNavigationSource);
			}
			else
			{
				valueReference = new ExceptionValueReference(DataSourceException.NewDataSourceError<Message1>(this.environment.Host, Strings.ODataCannotDetermineEntityType(element.Name), this.environment.Resource, null, null));
			}
			yield return ODataServiceDocumentEnumerable.CreateServiceDocumentRow(new ODataSchemaItem(element.Name, text), valueReference);
			yield break;
		}

		// Token: 0x06003998 RID: 14744 RVA: 0x000B9968 File Offset: 0x000B7B68
		private IEnumerable<IValueReference> CreateFunctionImportRows(ODataServiceDocumentElement element)
		{
			List<KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>> list;
			if (!this.environment.UnboundFunctionOverloads.TryGetValue(element.Name, out list))
			{
				return Enumerable.Empty<IValueReference>();
			}
			Dictionary<ODataSchemaItem, KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>> dictionary = new Dictionary<ODataSchemaItem, KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>>(list.Count);
			foreach (KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue> keyValuePair in list)
			{
				ODataSchemaItem odataSchemaItem = new ODataSchemaItem(element.Name, keyValuePair.Value.CreateSignature());
				if (!dictionary.ContainsKey(odataSchemaItem))
				{
					dictionary.Add(odataSchemaItem, keyValuePair);
				}
			}
			return dictionary.OrderBy((KeyValuePair<ODataSchemaItem, KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>> kvp) => kvp.Key).Select(delegate(KeyValuePair<ODataSchemaItem, KeyValuePair<Microsoft.OData.Edm.IEdmFunctionImport, FunctionTypeValue>> kvp)
			{
				bool flag = element.Url != null && kvp.Value.Value.ParameterCount == 0;
				Uri uri = (flag ? element.Url : this.environment.GetCanonicalUrl(kvp.Value.Key));
				return ODataServiceDocumentEnumerable.CreateServiceDocumentRow(kvp.Key, new UnboundFunctionValue(this.environment, uri, kvp.Value.Key, this.GetFunctionImportTypeValue(kvp.Value.Key, kvp.Value.Value), !flag));
			});
		}

		// Token: 0x06003999 RID: 14745 RVA: 0x000B9A60 File Offset: 0x000B7C60
		private TableValue AddRelationships(TableValue table, Microsoft.OData.Edm.IEdmNavigationSource source)
		{
			TableValue tableValue;
			try
			{
				foreach (Microsoft.OData.Edm.IEdmNavigationPropertyBinding edmNavigationPropertyBinding in source.NavigationPropertyBindings)
				{
					Microsoft.OData.Edm.IEdmReferentialConstraint referentialConstraint = edmNavigationPropertyBinding.NavigationProperty.ReferentialConstraint;
					if (referentialConstraint != null)
					{
						Microsoft.OData.Edm.IEdmNavigationSource navigationTarget = edmNavigationPropertyBinding.Target;
						Microsoft.OData.Edm.EdmNavigationSourceKind edmNavigationSourceKind = navigationTarget.NavigationSourceKind();
						string text;
						if (edmNavigationSourceKind != Microsoft.OData.Edm.EdmNavigationSourceKind.EntitySet)
						{
							if (edmNavigationSourceKind != Microsoft.OData.Edm.EdmNavigationSourceKind.Singleton)
							{
								continue;
							}
							text = "singleton";
						}
						else
						{
							text = "table";
						}
						ODataSchemaItem odataSchemaItem = new ODataSchemaItem(navigationTarget.Name, text);
						TypeValue targetType = this.GetNavigationSourceTypeValue(navigationTarget);
						ODataServiceDocumentElement element;
						if (this.elements.TryGetValue(odataSchemaItem, out element) && targetType.IsTableType)
						{
							KeysBuilder keysBuilder = default(KeysBuilder);
							List<int> list = new List<int>();
							foreach (Microsoft.OData.Edm.EdmReferentialConstraintPropertyPair edmReferentialConstraintPropertyPair in referentialConstraint.PropertyPairs)
							{
								keysBuilder.Add(edmReferentialConstraintPropertyPair.PrincipalProperty.Name);
								int num = table.Columns.IndexOfKey(edmReferentialConstraintPropertyPair.DependentProperty.Name);
								list.Add(num);
							}
							RecordTypeValue itemType = targetType.AsTableType.ItemType;
							bool flag = false;
							foreach (string text2 in keysBuilder.ToKeys())
							{
								if (itemType.Fields.Keys.IndexOfKey(text2) == -1)
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								Value value = new LinkTableFunctionValue(() => ODataValue.CreateNavigationSourceTableValue(this.environment, element.Url, navigationTarget, targetType.AsTableType).ReplaceRelationshipIdentity(ODataServiceDocumentEnumerable.GetRelationshipIdentity(this.environment, navigationTarget.Name)));
								table = RelatedTablesTableValue.New(table, table.RelatedTables, table.ColumnIdentities, Relationships.NestedJoin(table.Relationships, list.ToArray(), value, keysBuilder.ToKeys()));
							}
						}
					}
				}
				table = table.ReplaceRelationshipIdentity(ODataServiceDocumentEnumerable.GetRelationshipIdentity(this.environment, source.Name));
				tableValue = table;
			}
			catch (Exception ex)
			{
				using (IHostTrace hostTrace = TracingService.CreateTrace(this.environment.Host, "ODataRelationshipDetection/AddRelationships", TraceEventType.Information, this.environment.Resource))
				{
					hostTrace.Add(ex, true);
					if (!SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
				}
				tableValue = table;
			}
			return tableValue;
		}

		// Token: 0x0600399A RID: 14746 RVA: 0x000B9D48 File Offset: 0x000B7F48
		private TypeValue GetNavigationSourceTypeValue(Microsoft.OData.Edm.IEdmNavigationSource navigationSource)
		{
			TypeValue typeValue;
			if (navigationSource is Microsoft.OData.Edm.IEdmEntitySetBase)
			{
				typeValue = this.environment.ConvertType(navigationSource.Type);
			}
			else if (navigationSource is Microsoft.OData.Edm.IEdmSingleton)
			{
				typeValue = this.environment.ConvertType(new EdmCollectionType(new EdmEntityTypeReference(navigationSource.EntityType(), false)));
			}
			else
			{
				typeValue = TypeValue.Any;
			}
			if (this.environment.UserSettings.IncludeMetadataAnnotations == null)
			{
				typeValue = ODataServiceDocumentEnumerable.AddDisplayAnnotationsMetadata(this.environment.Annotations.GetElementCapability(navigationSource).DisplayAnnotations, typeValue);
			}
			return typeValue;
		}

		// Token: 0x0600399B RID: 14747 RVA: 0x000B9DCE File Offset: 0x000B7FCE
		private FunctionTypeValue GetFunctionImportTypeValue(Microsoft.OData.Edm.IEdmFunctionImport functionImport, FunctionTypeValue functionTypeValue)
		{
			return ODataServiceDocumentEnumerable.AddDisplayAnnotationsMetadata(this.environment.Annotations.GetElementCapability(functionImport).DisplayAnnotations, functionTypeValue).AsFunctionType;
		}

		// Token: 0x0600399C RID: 14748 RVA: 0x000B9DF4 File Offset: 0x000B7FF4
		private static string GetRelationshipIdentity(ODataEnvironment environment, string navSource)
		{
			return ODataConstants.ODataCacheKey.Qualify(environment.ServiceUri.OriginalString, navSource);
		}

		// Token: 0x0600399D RID: 14749 RVA: 0x000B9E1A File Offset: 0x000B801A
		private static IValueReference CreateServiceDocumentRow(ODataSchemaItem schemaItem, IValueReference data)
		{
			return RecordValue.New(NavigationTableServices.ODataNavigationTableKeys, new IValueReference[]
			{
				TextValue.New(schemaItem.Name),
				data,
				TextValue.New(schemaItem.Signature)
			});
		}

		// Token: 0x0600399E RID: 14750 RVA: 0x000B9E4C File Offset: 0x000B804C
		private static TypeValue AddDisplayAnnotationsMetadata(IList<NamedValue> namedValueAnnotations, TypeValue type)
		{
			if (namedValueAnnotations.Count > 0)
			{
				return BinaryOperator.AddMeta.Invoke(type, RecordValue.New(namedValueAnnotations.ToArray<NamedValue>())).AsType;
			}
			return type;
		}

		// Token: 0x04001DCA RID: 7626
		private static readonly string PlaceholderFunctionImportSignature = "function";

		// Token: 0x04001DCB RID: 7627
		private readonly ODataEnvironment environment;

		// Token: 0x04001DCC RID: 7628
		private readonly IDictionary<ODataSchemaItem, ODataServiceDocumentElement> elements;
	}
}
