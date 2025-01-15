using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.ContextUrl;
using Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.Reader
{
	// Token: 0x020007A6 RID: 1958
	internal class ODataResourceValueConverter
	{
		// Token: 0x0600394E RID: 14670 RVA: 0x000B85CE File Offset: 0x000B67CE
		public ODataResourceValueConverter(ODataEnvironment environment, GetReader getReader)
		{
			this.environment = environment;
			this.getReader = getReader;
			this.navigationTargetCache = new Dictionary<ODataResourceValueConverter.NavigationTargetCacheKey, Microsoft.OData.Edm.IEdmNavigationSource>();
		}

		// Token: 0x0600394F RID: 14671 RVA: 0x000B85F0 File Offset: 0x000B67F0
		public RecordValue CreateResourceValue(ODataNestedResource resource, RecordTypeValue resourceType, ODataBindingPath bindingPath = null, string omitValues = null)
		{
			RecordValue recordValue = this.CreateResourceValueCore(resource, resourceType, bindingPath, omitValues);
			return BinaryOperator.AddMeta.Invoke(recordValue, RecordValue.New(ODataResourceValueConverter.ResourceAnnotations, new Value[]
			{
				ODataResourceValueConverter.ReadValue(() => resource.Resource.ETag),
				ODataResourceValueConverter.ReadValue(() => resource.Resource.Id),
				ODataResourceValueConverter.ReadValue(() => resource.Resource.EditLink),
				ODataResourceValueConverter.ReadValue(() => resource.Resource.ReadLink),
				ODataResourceValueConverter.ReadValue(() => resource.Resource.TypeName)
			})).AsRecord;
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x000B86A0 File Offset: 0x000B68A0
		private RecordValue CreateResourceValueCore(ODataNestedResource resource, RecordTypeValue resourceType, ODataBindingPath bindingPath = null, string omitValues = null)
		{
			ODataResourceValueConverter.<>c__DisplayClass6_0 CS$<>8__locals1 = new ODataResourceValueConverter.<>c__DisplayClass6_0();
			CS$<>8__locals1.bindingPath = bindingPath;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.resourceType = resourceType;
			CS$<>8__locals1.resource = resource;
			CS$<>8__locals1.omitValues = omitValues;
			CS$<>8__locals1.record = this.CreateRecord(CS$<>8__locals1.resource);
			Value value = null;
			CS$<>8__locals1.usesMoreColumnsColumn = this.environment.UserSettings.MoreColumns && CS$<>8__locals1.resourceType.TryGetMetaField("MoreColumns", out value);
			CS$<>8__locals1.structuredType = ((CS$<>8__locals1.bindingPath != null) ? (CS$<>8__locals1.bindingPath.TargetType as Microsoft.OData.Edm.IEdmStructuredType) : null);
			CS$<>8__locals1.moreColumnsColumnName = null;
			CS$<>8__locals1.declaredProperties = null;
			CS$<>8__locals1.actualResourceType = null;
			CS$<>8__locals1.actualResourceTypeValue = null;
			if (CS$<>8__locals1.usesMoreColumnsColumn)
			{
				CS$<>8__locals1.moreColumnsColumnName = value.AsString;
				ODataResourceValueConverter.<>c__DisplayClass6_0 CS$<>8__locals2 = CS$<>8__locals1;
				HashSet<string> hashSet;
				if (CS$<>8__locals1.structuredType == null)
				{
					hashSet = new HashSet<string>();
				}
				else
				{
					hashSet = new HashSet<string>(CS$<>8__locals1.structuredType.DeclaredProperties.Select((Microsoft.OData.Edm.IEdmProperty p) => p.Name));
				}
				CS$<>8__locals2.declaredProperties = hashSet;
				if (this.TryGetActualResourceType(CS$<>8__locals1.resource, out CS$<>8__locals1.actualResourceType))
				{
					CS$<>8__locals1.actualResourceTypeValue = this.environment.ConvertType(CS$<>8__locals1.actualResourceType).AsRecordType;
				}
				else
				{
					CS$<>8__locals1.actualResourceTypeValue = RecordTypeValue.Any;
				}
			}
			CS$<>8__locals1.keys = (CS$<>8__locals1.usesMoreColumnsColumn ? CS$<>8__locals1.resourceType.Fields.Keys : this.ComputeKeys(CS$<>8__locals1.record, CS$<>8__locals1.resourceType));
			return RecordValue.New(CS$<>8__locals1.keys, CS$<>8__locals1.keys.Select(delegate(string key, int i)
			{
				if (CS$<>8__locals1.usesMoreColumnsColumn && key == CS$<>8__locals1.moreColumnsColumnName)
				{
					ODataResourceValueConverter.<>c__DisplayClass6_1 CS$<>8__locals3 = new ODataResourceValueConverter.<>c__DisplayClass6_1();
					CS$<>8__locals3.CS$<>8__locals1 = CS$<>8__locals1;
					ODataResourceValueConverter.<>c__DisplayClass6_1 CS$<>8__locals4 = CS$<>8__locals3;
					IEnumerable<string> keys = CS$<>8__locals1.record.Keys;
					Func<string, bool> func;
					if ((func = CS$<>8__locals1.<>9__2) == null)
					{
						func = (CS$<>8__locals1.<>9__2 = (string name) => name == CS$<>8__locals1.moreColumnsColumnName || (!CS$<>8__locals1.keys.Contains(name) && !CS$<>8__locals1.declaredProperties.Contains(name)));
					}
					CS$<>8__locals4.moreColumnsKeys = Keys.New(keys.Where(func).ToArray<string>());
					return RecordValue.New(CS$<>8__locals3.moreColumnsKeys, delegate(int j)
					{
						string text2 = CS$<>8__locals3.moreColumnsKeys[j];
						ODataBindingPath odataBindingPath = CS$<>8__locals3.CS$<>8__locals1.bindingPath;
						if (CS$<>8__locals3.CS$<>8__locals1.actualResourceTypeValue.Fields.Keys.Contains(text2))
						{
							odataBindingPath = CS$<>8__locals3.CS$<>8__locals1.bindingPath.AppendType(CS$<>8__locals3.CS$<>8__locals1.actualResourceType);
						}
						TypeValue typeValue2 = RecordTypeAlgebra.FieldOrDefault(CS$<>8__locals3.CS$<>8__locals1.actualResourceTypeValue, text2, TypeValue.Any);
						return CS$<>8__locals3.CS$<>8__locals1.<>4__this.CreatePropertyValueReference(CS$<>8__locals3.CS$<>8__locals1.record[text2], typeValue2, odataBindingPath, null).Value;
					});
				}
				TypeValue typeValue = RecordTypeAlgebra.FieldOrDefault(CS$<>8__locals1.resourceType, key, TypeValue.Any);
				ODataResourceValueConverter.IODataPropertyValue iodataPropertyValue;
				if (CS$<>8__locals1.record.TryGetValue(key, out iodataPropertyValue))
				{
					return CS$<>8__locals1.<>4__this.CreatePropertyValueReference(iodataPropertyValue, typeValue, CS$<>8__locals1.bindingPath, new int?(i));
				}
				if (typeValue.TypeKind == ValueKind.Function && CS$<>8__locals1.bindingPath != null)
				{
					Microsoft.OData.Edm.IEdmEntitySetBase edmEntitySetBase = CS$<>8__locals1.bindingPath.NavigationSource as Microsoft.OData.Edm.IEdmEntitySetBase;
					if (edmEntitySetBase != null)
					{
						return CS$<>8__locals1.<>4__this.environment.BuildFunctionValue(key, typeValue, CS$<>8__locals1.resource.Resource, edmEntitySetBase);
					}
					return Value.Null;
				}
				else
				{
					if (typeValue.IsNullable && CS$<>8__locals1.omitValues == "nulls")
					{
						return Value.Null;
					}
					string text = EdmNameEncoder.Encode(key);
					Microsoft.OData.Edm.IEdmStructuralProperty edmStructuralProperty = null;
					if (CS$<>8__locals1.structuredType != null)
					{
						edmStructuralProperty = CS$<>8__locals1.structuredType.FindProperty(text) as Microsoft.OData.Edm.IEdmStructuralProperty;
					}
					IValueReference valueReference = null;
					Uri uri;
					if (edmStructuralProperty != null && CS$<>8__locals1.<>4__this.TryCreatePropertyLink(CS$<>8__locals1.resource, edmStructuralProperty, out uri))
					{
						valueReference = CS$<>8__locals1.<>4__this.CreateLinkValueReference(uri, typeValue, new int?(i));
					}
					return new ExceptionValueReference(ODataCommonErrors.MissingProperty(text, valueReference));
				}
			}).ToArray<IValueReference>());
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x000B884C File Offset: 0x000B6A4C
		private IValueReference CreatePropertyValueReference(ODataResourceValueConverter.IODataPropertyValue property, TypeValue fieldType, ODataBindingPath parentBindingPath, int? column = null)
		{
			ODataResourceValueConverter.ODataPropertyValueKind kind = property.Kind;
			if (kind == ODataResourceValueConverter.ODataPropertyValueKind.SimpleProperty)
			{
				return this.CreateSimplePropertyValueReference((ODataResourceValueConverter.ODataSimplePropertyValue)property, fieldType);
			}
			if (kind != ODataResourceValueConverter.ODataPropertyValueKind.NestedValueProperty)
			{
				throw new InvalidOperationException();
			}
			return this.CreateNestedValuePropertyValueReference((ODataResourceValueConverter.ODataNestedValuePropertyValue)property, fieldType, parentBindingPath, column);
		}

		// Token: 0x06003952 RID: 14674 RVA: 0x000B8890 File Offset: 0x000B6A90
		private IValueReference CreateSimplePropertyValueReference(ODataResourceValueConverter.ODataSimplePropertyValue propertyValue, TypeValue fieldType)
		{
			return ODataStructuralValueConverter.GetDelayed<ODataException>(this.environment, () => ODataStructuralValueConverter.GetStreamOrStructuralValue(this.environment, propertyValue.PropertyWrapper, fieldType, new Func<object, Value>(ODataTypeServices.MarshalFromClr)));
		}

		// Token: 0x06003953 RID: 14675 RVA: 0x000B88D0 File Offset: 0x000B6AD0
		private IValueReference CreateNestedValuePropertyValueReference(ODataResourceValueConverter.ODataNestedValuePropertyValue propertyValue, TypeValue fieldType, ODataBindingPath parentBindingPath, int? column)
		{
			Uri navigationUri = null;
			if (propertyValue.InlineValues == null)
			{
				ODataException ex = null;
				try
				{
					navigationUri = propertyValue.NestedResourceInfoWrapper.Url;
				}
				catch (ODataException ex)
				{
				}
				if (navigationUri == null)
				{
					return new ExceptionValueReference(ODataCommonErrors.CouldNotFindNavigationUrl(propertyValue.Name, ex));
				}
			}
			else
			{
				navigationUri = propertyValue.NextPage;
			}
			Lazy<IODataPayloadReader> reader = null;
			if (propertyValue.InlineValues == null && !propertyValue.NestedResourceInfoWrapper.IsCollection())
			{
				reader = this.GetReader(navigationUri, column);
			}
			return new DelayedValue(delegate
			{
				ODataBindingPath targetPath = null;
				Microsoft.OData.Edm.IEdmNavigationProperty edmNavigationProperty = null;
				Microsoft.OData.Edm.IEdmNavigationSource edmNavigationSource = null;
				if (parentBindingPath != null)
				{
					Microsoft.OData.Edm.IEdmProperty edmProperty = parentBindingPath.FindProperty(propertyValue.Name);
					edmNavigationProperty = edmProperty as Microsoft.OData.Edm.IEdmNavigationProperty;
					if (edmNavigationProperty != null)
					{
						edmNavigationSource = this.FindNavigationTarget(parentBindingPath, edmNavigationProperty);
					}
					if (edmNavigationSource != null)
					{
						targetPath = ODataBindingPath.RootOf(edmNavigationSource);
					}
					else if (edmProperty != null)
					{
						targetPath = parentBindingPath.AppendProperty(edmProperty);
					}
				}
				Query query;
				if (fieldType.IsTableType && propertyValue.InlineValues == null && edmNavigationSource != null && this.TryCreateNavigationQuery(navigationUri, fieldType.AsTableType, edmNavigationProperty, edmNavigationSource, out query))
				{
					return new QueryTableValue(query, fieldType);
				}
				TypeValue nestedResourceType = ODataTypeServices.GetItemType(fieldType);
				IEnumerable<IValueReference> enumerable = ((propertyValue.InlineValues != null) ? propertyValue.InlineValues.Select((ODataNestedValue e) => e(nestedResourceType, targetPath)) : Enumerable.Empty<IValueReference>());
				if (propertyValue.NestedResourceInfoWrapper.IsCollection())
				{
					IEnumerable<IValueReference> enumerable2;
					if (!(navigationUri != null))
					{
						enumerable2 = Enumerable.Empty<IValueReference>();
					}
					else
					{
						IEnumerable<IValueReference> enumerable3 = new ODataReaderEnumerable(this.environment, navigationUri, nestedResourceType, true, edmNavigationSource, null);
						enumerable2 = enumerable3;
					}
					IEnumerable<IValueReference> enumerable4 = enumerable2;
					return ODataValue.CreateResourceSetValueReference(enumerable.Concat(enumerable4), fieldType).Value;
				}
				if (propertyValue.InlineValues != null)
				{
					return (enumerable.SingleOrDefault<IValueReference>() ?? Value.Null).Value;
				}
				return this.GetResourceValue(reader, nestedResourceType, edmNavigationSource);
			});
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000B89C4 File Offset: 0x000B6BC4
		private bool TryCreateNavigationQuery(Uri navigationLink, TableTypeValue type, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty, Microsoft.OData.Edm.IEdmNavigationSource navigationTarget, out Query query)
		{
			try
			{
				ODataPath odataPath;
				if (!this.environment.TryConvertToOpaquePath(navigationLink, out odataPath))
				{
					query = null;
					return false;
				}
				query = new OptimizableQuery(new ODataQuery(this.environment, odataPath, navigationTarget, navigationProperty.ToEntityType(), type.ItemType, this.environment.Annotations.GetElementCapability(navigationTarget), true));
				return true;
			}
			catch (NotSupportedException)
			{
			}
			query = null;
			return false;
		}

		// Token: 0x06003955 RID: 14677 RVA: 0x000B8A3C File Offset: 0x000B6C3C
		private Value GetResourceValue(Lazy<IODataPayloadReader> reader, TypeValue resourceType, Microsoft.OData.Edm.IEdmNavigationSource navigationTarget)
		{
			IValueReference resourceValueReference = null;
			IValueReference navigationLinkValueReference = new DelayedValue(delegate
			{
				if (resourceValueReference == null)
				{
					try
					{
						using (IODataPayloadReader value = reader.Value)
						{
							resourceValueReference = ODataValue.GetResourceValueReference(this.environment, this.getReader, value.ToResourceReader(false), resourceType, navigationTarget);
						}
					}
					catch (ValueException ex)
					{
						resourceValueReference = new ExceptionValueReference(ex);
					}
				}
				return resourceValueReference.Value;
			});
			if (resourceType.Equals(TypeValue.Any) || resourceType.AsRecordType.Open)
			{
				return navigationLinkValueReference.Value;
			}
			return RecordValue.New(resourceType.AsRecordType, delegate(int index)
			{
				if (navigationLinkValueReference.Value.IsNull)
				{
					return Value.Null;
				}
				return navigationLinkValueReference.Value[index];
			});
		}

		// Token: 0x06003956 RID: 14678 RVA: 0x000B8AD8 File Offset: 0x000B6CD8
		private IValueReference CreateLinkValueReference(Uri uri, TypeValue typeValue, int? column)
		{
			Lazy<IODataPayloadReader> delayedReader = this.GetReader(uri, column);
			return new DelayedValue(delegate
			{
				Value value2;
				using (IODataPayloadReader value = delayedReader.Value)
				{
					if (value.IsNull)
					{
						value2 = Value.Null;
					}
					else
					{
						ODataJsonLightContextUriParseResult odataJsonLightContextUriParseResult = ODataResponse.ParseODataContextUri(this.environment.Settings.EdmModel, value.ContextUrl, uri);
						value2 = ODataResponse.CreateValueFromPayload(this.environment, uri, value, odataJsonLightContextUriParseResult, typeValue);
					}
				}
				return value2;
			});
		}

		// Token: 0x06003957 RID: 14679 RVA: 0x000B8B24 File Offset: 0x000B6D24
		private bool TryCreatePropertyLink(ODataNestedResource resource, Microsoft.OData.Edm.IEdmStructuralProperty property, out Uri propertyLink)
		{
			propertyLink = null;
			bool flag;
			try
			{
				ODataPath odataPath;
				if (resource.Resource.ReadLink == null || !this.environment.TryConvertToOpaquePath(resource.Resource.ReadLink, out odataPath))
				{
					flag = false;
				}
				else
				{
					ODataPath odataPath2 = new ODataPath(odataPath.Concat(new ODataPathSegment[]
					{
						new PropertySegment(property)
					}));
					propertyLink = new ODataUri
					{
						ServiceRoot = this.environment.ServiceUri,
						Path = odataPath2
					}.GetUri();
					flag = true;
				}
			}
			catch (ODataException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x000B8BC0 File Offset: 0x000B6DC0
		private Lazy<IODataPayloadReader> GetReader(Uri uri, int? column = null)
		{
			return this.getReader(new GetReaderArgs
			{
				Uri = uri,
				Catch404 = false,
				Column = column
			});
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x000B8BE8 File Offset: 0x000B6DE8
		private bool TryGetActualResourceType(ODataNestedResource resource, out Microsoft.OData.Edm.IEdmStructuredType actualType)
		{
			actualType = null;
			string typeName;
			try
			{
				typeName = resource.Resource.TypeName;
			}
			catch (ODataException)
			{
				return false;
			}
			actualType = ODataTypeServices.FindEdmType(this.environment.EdmModel, typeName) as Microsoft.OData.Edm.IEdmStructuredType;
			return actualType != null;
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x000B8C3C File Offset: 0x000B6E3C
		private Microsoft.OData.Edm.IEdmNavigationSource FindNavigationTarget(ODataBindingPath bindingPath, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty)
		{
			ODataResourceValueConverter.NavigationTargetCacheKey navigationTargetCacheKey = new ODataResourceValueConverter.NavigationTargetCacheKey(bindingPath, navigationProperty);
			Microsoft.OData.Edm.IEdmNavigationSource edmNavigationSource;
			if (!this.navigationTargetCache.TryGetValue(navigationTargetCacheKey, out edmNavigationSource))
			{
				edmNavigationSource = bindingPath.FindNavigationTarget(navigationProperty);
				this.navigationTargetCache.Add(navigationTargetCacheKey, edmNavigationSource);
			}
			return edmNavigationSource;
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x000B8C78 File Offset: 0x000B6E78
		private Keys ComputeKeys(ODataResourceValueConverter.ODataResourceRecord record, RecordTypeValue recordType)
		{
			if (recordType.Open || ODataStructuralValueConverter.GetKeys(recordType, true).Any<string>())
			{
				KeysBuilder keysBuilder = new KeysBuilder(record.Keys.Length);
				foreach (string text in ODataStructuralValueConverter.GetKeys(recordType, false))
				{
					keysBuilder.Add(text);
				}
				keysBuilder.Union(record.Keys);
				return keysBuilder.ToKeys();
			}
			return recordType.Fields.Keys;
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x000B8D14 File Offset: 0x000B6F14
		private ODataResourceValueConverter.ODataResourceRecord CreateRecord(ODataNestedResource resource)
		{
			KeysBuilder keysBuilder = default(KeysBuilder);
			Dictionary<string, ODataResourceValueConverter.IODataPropertyValue> dictionary = new Dictionary<string, ODataResourceValueConverter.IODataPropertyValue>();
			IEnumerable<ODataResourceValueConverter.IODataPropertyValue> enumerable = resource.SimpleProperties.Select((ODataPropertyWrapper p) => new ODataResourceValueConverter.ODataSimplePropertyValue(p));
			IEnumerable<ODataResourceValueConverter.IODataPropertyValue> enumerable2 = resource.NestedValueProperties.Select((KeyValuePair<ODataNestedResourceInfoWrapper, ODataNestedValues> kvp) => new ODataResourceValueConverter.ODataNestedValuePropertyValue(kvp.Key, kvp.Value));
			foreach (ODataResourceValueConverter.IODataPropertyValue iodataPropertyValue in enumerable.Concat(enumerable2))
			{
				string text = EdmNameEncoder.Decode(iodataPropertyValue.Name);
				ODataResourceValueConverter.IODataPropertyValue iodataPropertyValue2;
				if (dictionary.TryGetValue(text, out iodataPropertyValue2))
				{
					if (!iodataPropertyValue.IsEquivalent(iodataPropertyValue2))
					{
						throw this.NewDuplicatePropertyError(text);
					}
				}
				else
				{
					dictionary.Add(text, iodataPropertyValue);
					keysBuilder.Add(text);
				}
			}
			return new ODataResourceValueConverter.ODataResourceRecord(keysBuilder.ToKeys(), dictionary);
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x000B8E10 File Offset: 0x000B7010
		private ValueException NewDuplicatePropertyError(string name)
		{
			return DataSourceException.NewDataSourceError<Message0>(this.environment.Host, Strings.ODataDuplicateProperty, this.environment.Resource, "Property", TextValue.New(name), TypeValue.Text, null);
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x000B8E44 File Offset: 0x000B7044
		private static Value ReadValue(Func<string> readItem)
		{
			Value value;
			try
			{
				value = TextValue.NewOrNull(readItem());
			}
			catch (ODataException)
			{
				value = Value.Null;
			}
			return value;
		}

		// Token: 0x0600395F RID: 14687 RVA: 0x000B8E7C File Offset: 0x000B707C
		private static Value ReadValue(Func<Uri> readItem)
		{
			Value value;
			try
			{
				Uri uri = readItem();
				value = ((uri != null) ? TextValue.New(uri.OriginalString) : Value.Null);
			}
			catch (ODataException)
			{
				value = Value.Null;
			}
			return value;
		}

		// Token: 0x04001D92 RID: 7570
		private static Keys ResourceAnnotations = Keys.New(new string[] { "@odata.etag", "@odata.id", "@odata.editLink", "@odata.readLink", "@odata.type" });

		// Token: 0x04001D93 RID: 7571
		private readonly ODataEnvironment environment;

		// Token: 0x04001D94 RID: 7572
		private readonly GetReader getReader;

		// Token: 0x04001D95 RID: 7573
		private readonly Dictionary<ODataResourceValueConverter.NavigationTargetCacheKey, Microsoft.OData.Edm.IEdmNavigationSource> navigationTargetCache;

		// Token: 0x020007A7 RID: 1959
		private struct ODataResourceRecord
		{
			// Token: 0x06003961 RID: 14689 RVA: 0x000B8F02 File Offset: 0x000B7102
			public ODataResourceRecord(Keys keys, Dictionary<string, ODataResourceValueConverter.IODataPropertyValue> values)
			{
				this.keys = keys;
				this.values = values;
			}

			// Token: 0x1700136B RID: 4971
			// (get) Token: 0x06003962 RID: 14690 RVA: 0x000B8F12 File Offset: 0x000B7112
			public Keys Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x1700136C RID: 4972
			public ODataResourceValueConverter.IODataPropertyValue this[string key]
			{
				get
				{
					return this.values[key];
				}
			}

			// Token: 0x06003964 RID: 14692 RVA: 0x000B8F28 File Offset: 0x000B7128
			public bool TryGetValue(string key, out ODataResourceValueConverter.IODataPropertyValue value)
			{
				return this.values.TryGetValue(key, out value);
			}

			// Token: 0x04001D96 RID: 7574
			private readonly Keys keys;

			// Token: 0x04001D97 RID: 7575
			private readonly Dictionary<string, ODataResourceValueConverter.IODataPropertyValue> values;
		}

		// Token: 0x020007A8 RID: 1960
		private struct NavigationTargetCacheKey : IEquatable<ODataResourceValueConverter.NavigationTargetCacheKey>
		{
			// Token: 0x06003965 RID: 14693 RVA: 0x000B8F37 File Offset: 0x000B7137
			public NavigationTargetCacheKey(ODataBindingPath bindingPath, Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty)
			{
				this.bindingPath = bindingPath;
				this.navigationProperty = navigationProperty;
			}

			// Token: 0x06003966 RID: 14694 RVA: 0x000B8F47 File Offset: 0x000B7147
			public bool Equals(ODataResourceValueConverter.NavigationTargetCacheKey other)
			{
				return this.bindingPath.Equals(other.bindingPath) && this.navigationProperty.Equals(other.navigationProperty);
			}

			// Token: 0x06003967 RID: 14695 RVA: 0x000B8F70 File Offset: 0x000B7170
			public override bool Equals(object obj)
			{
				ODataResourceValueConverter.NavigationTargetCacheKey? navigationTargetCacheKey = obj as ODataResourceValueConverter.NavigationTargetCacheKey?;
				return navigationTargetCacheKey != null && this.Equals(navigationTargetCacheKey.Value);
			}

			// Token: 0x06003968 RID: 14696 RVA: 0x000B8FA1 File Offset: 0x000B71A1
			public override int GetHashCode()
			{
				return this.bindingPath.GetHashCode() ^ this.navigationProperty.GetHashCode();
			}

			// Token: 0x04001D98 RID: 7576
			private readonly ODataBindingPath bindingPath;

			// Token: 0x04001D99 RID: 7577
			private readonly Microsoft.OData.Edm.IEdmNavigationProperty navigationProperty;
		}

		// Token: 0x020007A9 RID: 1961
		private interface IODataPropertyValue
		{
			// Token: 0x1700136D RID: 4973
			// (get) Token: 0x06003969 RID: 14697
			ODataResourceValueConverter.ODataPropertyValueKind Kind { get; }

			// Token: 0x1700136E RID: 4974
			// (get) Token: 0x0600396A RID: 14698
			string Name { get; }

			// Token: 0x0600396B RID: 14699
			bool IsEquivalent(ODataResourceValueConverter.IODataPropertyValue other);
		}

		// Token: 0x020007AA RID: 1962
		private class ODataSimplePropertyValue : ODataResourceValueConverter.IODataPropertyValue
		{
			// Token: 0x0600396C RID: 14700 RVA: 0x000B8FBA File Offset: 0x000B71BA
			public ODataSimplePropertyValue(ODataPropertyWrapper property)
			{
				this.property = property;
			}

			// Token: 0x1700136F RID: 4975
			// (get) Token: 0x0600396D RID: 14701 RVA: 0x00002105 File Offset: 0x00000305
			public ODataResourceValueConverter.ODataPropertyValueKind Kind
			{
				get
				{
					return ODataResourceValueConverter.ODataPropertyValueKind.SimpleProperty;
				}
			}

			// Token: 0x17001370 RID: 4976
			// (get) Token: 0x0600396E RID: 14702 RVA: 0x000B8FC9 File Offset: 0x000B71C9
			public string Name
			{
				get
				{
					return this.property.Name;
				}
			}

			// Token: 0x17001371 RID: 4977
			// (get) Token: 0x0600396F RID: 14703 RVA: 0x000B8FD6 File Offset: 0x000B71D6
			public object Value
			{
				get
				{
					return this.property.Value;
				}
			}

			// Token: 0x17001372 RID: 4978
			// (get) Token: 0x06003970 RID: 14704 RVA: 0x000B8FE3 File Offset: 0x000B71E3
			public ODataPropertyWrapper PropertyWrapper
			{
				get
				{
					return this.property;
				}
			}

			// Token: 0x06003971 RID: 14705 RVA: 0x000B8FEB File Offset: 0x000B71EB
			public bool IsEquivalent(ODataResourceValueConverter.IODataPropertyValue other)
			{
				return other.Kind == ODataResourceValueConverter.ODataPropertyValueKind.SimpleProperty && object.Equals(this.Value, ((ODataResourceValueConverter.ODataSimplePropertyValue)other).Value);
			}

			// Token: 0x04001D9A RID: 7578
			private readonly ODataPropertyWrapper property;
		}

		// Token: 0x020007AB RID: 1963
		private class ODataNestedValuePropertyValue : ODataResourceValueConverter.IODataPropertyValue
		{
			// Token: 0x06003972 RID: 14706 RVA: 0x000B900D File Offset: 0x000B720D
			public ODataNestedValuePropertyValue(ODataNestedResourceInfoWrapper nestedResourceInfoWrapper, ODataNestedValues nestedValues)
			{
				this.nestedResourceInfoWrapper = nestedResourceInfoWrapper;
				this.nestedValues = nestedValues;
			}

			// Token: 0x17001373 RID: 4979
			// (get) Token: 0x06003973 RID: 14707 RVA: 0x00002139 File Offset: 0x00000339
			public ODataResourceValueConverter.ODataPropertyValueKind Kind
			{
				get
				{
					return ODataResourceValueConverter.ODataPropertyValueKind.NestedValueProperty;
				}
			}

			// Token: 0x17001374 RID: 4980
			// (get) Token: 0x06003974 RID: 14708 RVA: 0x000B9023 File Offset: 0x000B7223
			public string Name
			{
				get
				{
					return this.NestedResourceInfoWrapper.Name;
				}
			}

			// Token: 0x17001375 RID: 4981
			// (get) Token: 0x06003975 RID: 14709 RVA: 0x000B9030 File Offset: 0x000B7230
			public ODataNestedResourceInfoWrapper NestedResourceInfoWrapper
			{
				get
				{
					return this.nestedResourceInfoWrapper;
				}
			}

			// Token: 0x17001376 RID: 4982
			// (get) Token: 0x06003976 RID: 14710 RVA: 0x000B9038 File Offset: 0x000B7238
			public List<ODataNestedValue> InlineValues
			{
				get
				{
					return this.nestedValues.InlineValues;
				}
			}

			// Token: 0x17001377 RID: 4983
			// (get) Token: 0x06003977 RID: 14711 RVA: 0x000B9045 File Offset: 0x000B7245
			public Uri NextPage
			{
				get
				{
					return this.nestedValues.NextPage;
				}
			}

			// Token: 0x06003978 RID: 14712 RVA: 0x000B9054 File Offset: 0x000B7254
			public bool IsEquivalent(ODataResourceValueConverter.IODataPropertyValue other)
			{
				if (other.Kind != ODataResourceValueConverter.ODataPropertyValueKind.NestedValueProperty)
				{
					return false;
				}
				ODataResourceValueConverter.ODataNestedValuePropertyValue odataNestedValuePropertyValue = (ODataResourceValueConverter.ODataNestedValuePropertyValue)other;
				bool flag = (this.InlineValues == null && odataNestedValuePropertyValue.InlineValues == null) || this.InlineValues.Count == odataNestedValuePropertyValue.InlineValues.Count;
				return object.Equals(this.NestedResourceInfoWrapper, odataNestedValuePropertyValue.NestedResourceInfoWrapper) && this.NextPage == odataNestedValuePropertyValue.NextPage && flag;
			}

			// Token: 0x04001D9B RID: 7579
			private readonly ODataNestedResourceInfoWrapper nestedResourceInfoWrapper;

			// Token: 0x04001D9C RID: 7580
			private readonly ODataNestedValues nestedValues;
		}

		// Token: 0x020007AC RID: 1964
		private enum ODataPropertyValueKind
		{
			// Token: 0x04001D9E RID: 7582
			SimpleProperty,
			// Token: 0x04001D9F RID: 7583
			NestedValueProperty
		}
	}
}
