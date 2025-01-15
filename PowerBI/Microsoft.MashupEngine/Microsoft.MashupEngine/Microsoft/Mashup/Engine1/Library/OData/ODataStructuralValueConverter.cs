using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Library.Web;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;
using Microsoft.OData.Core;
using Microsoft.OData.Edm;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000751 RID: 1873
	internal static class ODataStructuralValueConverter
	{
		// Token: 0x0600376E RID: 14190 RVA: 0x000B1008 File Offset: 0x000AF208
		public static Value ConvertPropertyComplexCollectionOrPrimitive(object propertyValue, TypeValue propertyType, Func<object, Value> marshalFromClr, IODataService environment = null)
		{
			IODataPropertyWrapper iodataPropertyWrapper = propertyValue as IODataPropertyWrapper;
			propertyValue = ((iodataPropertyWrapper != null) ? iodataPropertyWrapper.Value : propertyValue);
			RecordValue recordValue = null;
			if (iodataPropertyWrapper != null)
			{
				recordValue = iodataPropertyWrapper.Annotations;
			}
			IODataComplexValueWrapper iodataComplexValueWrapper = propertyValue as IODataComplexValueWrapper;
			Value value2;
			if (iodataComplexValueWrapper != null)
			{
				RecordTypeValue recordType = ((propertyType.TypeKind == ValueKind.Record) ? propertyType.AsRecordType : TypeValue.Record);
				Dictionary<string, IODataPropertyWrapper> props = iodataComplexValueWrapper.Properties.ToDictionary((IODataPropertyWrapper p) => EdmNameEncoder.Decode(p.Name));
				Value value = null;
				bool usesMoreColumnsColumn = environment != null && environment.UserSettings.MoreColumns && propertyType.TryGetMetaField("MoreColumns", out value);
				string moreColumnsColumnName = null;
				if (usesMoreColumnsColumn)
				{
					moreColumnsColumnName = value.AsString;
				}
				Keys keys = null;
				if ((!recordType.Open && !ODataStructuralValueConverter.GetKeys(recordType, true).Any<string>()) | usesMoreColumnsColumn)
				{
					keys = recordType.Fields.Keys;
				}
				else
				{
					List<string> list = ODataStructuralValueConverter.GetKeys(recordType, false).ToList<string>();
					HashSet<string> keysLookup = new HashSet<string>(list);
					list.AddRange(props.Keys.Where((string k) => !keysLookup.Contains(k)));
					keys = Keys.New(list.ToArray());
				}
				value2 = RecordValue.New(keys, delegate(int i)
				{
					string key = keys[i];
					TypeValue fieldTypeInformation = ODataStructuralValueConverter.GetFieldTypeInformation(key, recordType);
					if (usesMoreColumnsColumn && key == moreColumnsColumnName)
					{
						return ODataStructuralValueConverter.CreateMoreColumnsRecord(environment, (from kvp in props
							where !recordType.Fields.Keys.Contains(kvp.Key) || kvp.Key == key
							select kvp.Value).ToArray<IODataPropertyWrapper>(), marshalFromClr);
					}
					IODataPropertyWrapper iodataPropertyWrapper2;
					if (props.TryGetValue(key, out iodataPropertyWrapper2))
					{
						return ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(iodataPropertyWrapper2, fieldTypeInformation, marshalFromClr, environment);
					}
					return ODataStructuralValueConverter.CreateDefaultValue(fieldTypeInformation);
				});
			}
			else
			{
				IODataCollectionValueWrapper iodataCollectionValueWrapper = propertyValue as IODataCollectionValueWrapper;
				IODataEnumValueWrapper iodataEnumValueWrapper = propertyValue as IODataEnumValueWrapper;
				if (iodataCollectionValueWrapper != null)
				{
					object[] items = iodataCollectionValueWrapper.Items.OfType<object>().ToArray<object>();
					ListTypeValue listType = ((propertyType.TypeKind == ValueKind.Any) ? ListTypeValue.Any : propertyType.AsListType);
					value2 = ListValue.New(items.Length, (int i) => ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(items[i], listType.ItemType, marshalFromClr, environment)).NewType(listType);
				}
				else if (iodataEnumValueWrapper != null)
				{
					value2 = TextValue.New(iodataEnumValueWrapper.Value);
				}
				else
				{
					value2 = ODataStructuralValueConverter.GetFieldValue(propertyValue, propertyType, marshalFromClr);
				}
			}
			if (recordValue != null)
			{
				value2 = BinaryOperator.AddMeta.Invoke(value2, recordValue);
			}
			return value2;
		}

		// Token: 0x0600376F RID: 14191 RVA: 0x000B129D File Offset: 0x000AF49D
		public static IEnumerable<string> GetKeys(RecordTypeValue recordType, bool optional)
		{
			int num;
			for (int i = 0; i < recordType.Fields.Count; i = num + 1)
			{
				if (recordType.Fields[i]["Optional"].AsBoolean == optional)
				{
					yield return recordType.Fields.Keys[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x06003770 RID: 14192 RVA: 0x000B12B4 File Offset: 0x000AF4B4
		private static bool SafeAddValueWrapper(string name, object property, IODataService odataEnvironment, Dictionary<string, object> props)
		{
			object obj = null;
			if (!props.TryGetValue(name, out obj))
			{
				props.Add(name, property);
				return true;
			}
			IODataPropertyWrapper iodataPropertyWrapper = obj as IODataPropertyWrapper;
			IODataPropertyWrapper iodataPropertyWrapper2 = property as IODataPropertyWrapper;
			if (iodataPropertyWrapper != null && iodataPropertyWrapper2 != null && iodataPropertyWrapper.Value.Equals(iodataPropertyWrapper2.Value))
			{
				return false;
			}
			NavigationLinkWrapperPropertyLookupValue navigationLinkWrapperPropertyLookupValue = obj as NavigationLinkWrapperPropertyLookupValue;
			NavigationLinkWrapperPropertyLookupValue navigationLinkWrapperPropertyLookupValue2 = property as NavigationLinkWrapperPropertyLookupValue;
			if (navigationLinkWrapperPropertyLookupValue != null && navigationLinkWrapperPropertyLookupValue2 != null)
			{
				bool flag = (navigationLinkWrapperPropertyLookupValue.InlineEntries == null && navigationLinkWrapperPropertyLookupValue2.InlineEntries == null) || navigationLinkWrapperPropertyLookupValue.InlineEntries.Count == navigationLinkWrapperPropertyLookupValue2.InlineEntries.Count;
				if (navigationLinkWrapperPropertyLookupValue.NavigationLinkWrapper.Equals(navigationLinkWrapperPropertyLookupValue2.NavigationLinkWrapper) && navigationLinkWrapperPropertyLookupValue.NextPage.Equals(navigationLinkWrapperPropertyLookupValue2.NextPage) && flag)
				{
					return false;
				}
			}
			throw DataSourceException.NewDataSourceError<Message0>(odataEnvironment.Host, Strings.ODataDuplicateProperty, odataEnvironment.Resource, "Property", TextValue.New(name), TypeValue.Text, null);
		}

		// Token: 0x06003771 RID: 14193 RVA: 0x000B13A0 File Offset: 0x000AF5A0
		private static RecordTypeValue CreateEntryRecordWithStructuralProperties(IODataService odataEnvironment, TypeValue type, IEnumerable<IODataPropertyWrapper> properties, Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links, out Keys keys, out Dictionary<string, object> props)
		{
			RecordTypeValue recordTypeValue = ODataStructuralValueConverter.GetItemType(type);
			props = new Dictionary<string, object>();
			if (recordTypeValue.Equals(RecordTypeValue.Any))
			{
				NamedValue[] array = new NamedValue[properties.Count<IODataPropertyWrapper>() + links.Count];
				int num = 0;
				foreach (IODataPropertyWrapper iodataPropertyWrapper in properties)
				{
					string text = EdmNameEncoder.Decode(iodataPropertyWrapper.Name);
					if (ODataStructuralValueConverter.SafeAddValueWrapper(text, iodataPropertyWrapper, odataEnvironment, props))
					{
						array[num++] = new NamedValue(text, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							TypeValue.Any.Nullable,
							LogicalValue.False
						}));
					}
				}
				foreach (KeyValuePair<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> keyValuePair in links)
				{
					string text2 = EdmNameEncoder.Decode(keyValuePair.Key.Name);
					if (ODataStructuralValueConverter.SafeAddValueWrapper(text2, new NavigationLinkWrapperPropertyLookupValue(keyValuePair.Key, keyValuePair.Value), odataEnvironment, props))
					{
						TypeValue typeValue = ((keyValuePair.Key.IsCollection != null && keyValuePair.Key.IsCollection.Value) ? PreviewServices.ConvertToDelayedValue(NullableTypeValue.Table, "Table") : PreviewServices.ConvertToDelayedValue(NullableTypeValue.Record, "Record"));
						array[num++] = new NamedValue(text2, RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
						{
							typeValue,
							LogicalValue.False
						}));
					}
				}
				recordTypeValue = RecordTypeValue.New(RecordValue.New(array), false);
				keys = recordTypeValue.Fields.Keys;
			}
			else
			{
				foreach (IODataPropertyWrapper iodataPropertyWrapper2 in properties)
				{
					ODataStructuralValueConverter.SafeAddValueWrapper(EdmNameEncoder.Decode(iodataPropertyWrapper2.Name), iodataPropertyWrapper2, odataEnvironment, props);
				}
				foreach (KeyValuePair<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> keyValuePair2 in links)
				{
					ODataStructuralValueConverter.SafeAddValueWrapper(EdmNameEncoder.Decode(keyValuePair2.Key.Name), new NavigationLinkWrapperPropertyLookupValue(keyValuePair2.Key, keyValuePair2.Value), odataEnvironment, props);
				}
				if (!recordTypeValue.Open)
				{
					keys = recordTypeValue.Fields.Keys;
				}
				else
				{
					List<string> list = recordTypeValue.Fields.Keys.ToList<string>();
					HashSet<string> keysLookup = new HashSet<string>(list);
					list.AddRange(props.Keys.Where((string k) => !keysLookup.Contains(k)));
					keys = Keys.New(list.ToArray());
				}
			}
			return recordTypeValue;
		}

		// Token: 0x06003772 RID: 14194 RVA: 0x000B169C File Offset: 0x000AF89C
		public static Value GetStreamOrStructuralValue(IODataService environment, IODataPropertyWrapper property, TypeValue fieldType, Func<object, Value> marshalFromClr)
		{
			IODataStreamReferenceValueWrapper iodataStreamReferenceValueWrapper = property.Value as IODataStreamReferenceValueWrapper;
			if (iodataStreamReferenceValueWrapper == null)
			{
				return ODataStructuralValueConverter.ConvertPropertyComplexCollectionOrPrimitive(property, fieldType, marshalFromClr, environment);
			}
			Uri uri = iodataStreamReferenceValueWrapper.ReadLink ?? iodataStreamReferenceValueWrapper.EditLink;
			if (uri == null)
			{
				return Value.Null;
			}
			return WebContentsBinaryValue.New(Request.Create(environment.Host, "Web", uri.AbsoluteUri, TextValue.New(uri.AbsoluteUri), null, null, null, null, null, null, null, null, null, null), environment.Credentials, Value.Null, false, null);
		}

		// Token: 0x06003773 RID: 14195 RVA: 0x000B1720 File Offset: 0x000AF920
		public static RecordValue CreateEntryValue<TODataException>(ODataEnvironmentBase environment, TypeValue type, IEnumerable<IODataPropertyWrapper> properties, Dictionary<IODataNavigationLinkWrapper, NavigationLinkWrapperInlineEntries> links, Func<NavigationLinkWrapperPropertyLookupValue, TypeValue, int, IValueReference> createEntryLinkValueReference, Func<object, Value> marshalFromClr, ODataEntry entry = null, Microsoft.OData.Edm.IEdmEntitySetBase entryEntitySet = null) where TODataException : Exception
		{
			ODataStructuralValueConverter.<>c__DisplayClass5_0<TODataException> CS$<>8__locals1 = new ODataStructuralValueConverter.<>c__DisplayClass5_0<TODataException>();
			CS$<>8__locals1.environment = environment;
			CS$<>8__locals1.properties = properties;
			CS$<>8__locals1.marshalFromClr = marshalFromClr;
			CS$<>8__locals1.createEntryLinkValueReference = createEntryLinkValueReference;
			CS$<>8__locals1.entry = entry;
			CS$<>8__locals1.entryEntitySet = entryEntitySet;
			Keys keys;
			CS$<>8__locals1.recordType = ODataStructuralValueConverter.CreateEntryRecordWithStructuralProperties(CS$<>8__locals1.environment, type, CS$<>8__locals1.properties, links, out keys, out CS$<>8__locals1.props);
			TypeValue typeValue = ((type.TypeKind == ValueKind.Table) ? type.AsTableType.ItemType : type);
			Value value = null;
			CS$<>8__locals1.usesMoreColumnsColumn = CS$<>8__locals1.environment.UserSettings.MoreColumns && typeValue.TryGetMetaField("MoreColumns", out value);
			CS$<>8__locals1.moreColumnsColumnName = null;
			CS$<>8__locals1.declaredProperties = null;
			if (CS$<>8__locals1.usesMoreColumnsColumn)
			{
				CS$<>8__locals1.moreColumnsColumnName = value.AsString;
				ODataStructuralValueConverter.<>c__DisplayClass5_0<TODataException> CS$<>8__locals2 = CS$<>8__locals1;
				HashSet<string> hashSet;
				if (CS$<>8__locals1.entryEntitySet == null)
				{
					hashSet = new HashSet<string>();
				}
				else
				{
					hashSet = new HashSet<string>(CS$<>8__locals1.entryEntitySet.EntityType().DeclaredProperties.Select((Microsoft.OData.Edm.IEdmProperty p) => p.Name));
				}
				CS$<>8__locals2.declaredProperties = hashSet;
			}
			return RecordValue.New(CS$<>8__locals1.recordType, CS$<>8__locals1.recordType.Fields.Keys.Select(delegate(string key, int i)
			{
				if (CS$<>8__locals1.usesMoreColumnsColumn && key == CS$<>8__locals1.moreColumnsColumnName)
				{
					return ODataStructuralValueConverter.CreateMoreColumnsRecord(CS$<>8__locals1.environment, CS$<>8__locals1.properties.Where((IODataPropertyWrapper property) => property.Name == key || (!CS$<>8__locals1.recordType.Fields.Keys.Contains(property.Name) && !CS$<>8__locals1.declaredProperties.Contains(property.Name))).ToArray<IODataPropertyWrapper>(), CS$<>8__locals1.marshalFromClr);
				}
				TypeValue fieldType = ODataStructuralValueConverter.GetFieldTypeInformation(key, CS$<>8__locals1.recordType);
				object obj;
				if (CS$<>8__locals1.props.TryGetValue(key, out obj))
				{
					IODataPropertyWrapper property = obj as IODataPropertyWrapper;
					if (property != null)
					{
						return ODataStructuralValueConverter.GetDelayed<TODataException>(CS$<>8__locals1.environment, () => ODataStructuralValueConverter.GetStreamOrStructuralValue(CS$<>8__locals1.environment, property, fieldType, CS$<>8__locals1.marshalFromClr));
					}
					NavigationLinkWrapperPropertyLookupValue navigationLinkWrapperPropertyLookupValue = (NavigationLinkWrapperPropertyLookupValue)obj;
					return CS$<>8__locals1.createEntryLinkValueReference(navigationLinkWrapperPropertyLookupValue, fieldType, i);
				}
				else
				{
					if (fieldType.TypeKind == ValueKind.Function && CS$<>8__locals1.entry != null && CS$<>8__locals1.entryEntitySet != null)
					{
						return CS$<>8__locals1.environment.BuildFunctionValue(key, fieldType, CS$<>8__locals1.entry, CS$<>8__locals1.entryEntitySet);
					}
					return ODataStructuralValueConverter.CreateDefaultValue(fieldType);
				}
			}).ToArray<IValueReference>());
		}

		// Token: 0x06003774 RID: 14196 RVA: 0x000B1866 File Offset: 0x000AFA66
		public static IValueReference GetDelayed<TODataException>(IODataService environment, Func<Value> func) where TODataException : Exception
		{
			return new DelayedValue(delegate
			{
				Value value;
				try
				{
					value = func();
				}
				catch (TODataException ex)
				{
					TODataException ex2 = (TODataException)((object)ex);
					throw ODataCommonErrors.ODataFailedToParseODataResult(environment.Host, ex2, environment.ServiceUri, environment.Resource.Kind);
				}
				return value;
			});
		}

		// Token: 0x06003775 RID: 14197 RVA: 0x000B188C File Offset: 0x000AFA8C
		private static RecordValue CreateMoreColumnsRecord(IODataService environment, IODataPropertyWrapper[] properties, Func<object, Value> marshalFromClr)
		{
			return RecordValue.New(Keys.New(properties.Select((IODataPropertyWrapper property) => property.Name).ToArray<string>()), (int i) => ODataStructuralValueConverter.GetStreamOrStructuralValue(environment, properties[i], TypeValue.Any, marshalFromClr));
		}

		// Token: 0x06003776 RID: 14198 RVA: 0x000B18F9 File Offset: 0x000AFAF9
		public static TypeValue GetFieldTypeInformation(string fieldName, RecordTypeValue recordType)
		{
			return RecordTypeAlgebra.FieldOrDefault(recordType, fieldName, null);
		}

		// Token: 0x06003777 RID: 14199 RVA: 0x000B1904 File Offset: 0x000AFB04
		private static Value GetFieldValue(object fieldValue, TypeValue fieldType, Func<object, Value> marshalFromClr)
		{
			Value value = marshalFromClr(fieldValue);
			if (value == null && fieldValue != null && fieldType.TypeKind == ValueKind.Text)
			{
				return TextValue.New(fieldValue.ToString());
			}
			if (value != null && !value.IsNull)
			{
				return value;
			}
			if (fieldType != null && !fieldType.IsNullable && fieldType.TypeKind != ValueKind.Any)
			{
				return ODataStructuralValueConverter.CreateDefaultValue(fieldType);
			}
			return Value.Null;
		}

		// Token: 0x06003778 RID: 14200 RVA: 0x000B1964 File Offset: 0x000AFB64
		private static RecordTypeValue GetItemType(TypeValue type)
		{
			if (type.TypeKind == ValueKind.Table)
			{
				return type.AsTableType.ItemType;
			}
			if (type.TypeKind == ValueKind.List)
			{
				type = type.AsListType.ItemType;
			}
			type = TypeServices.StripNullableAndMetadata(type);
			if (type.TypeKind != ValueKind.Record)
			{
				return RecordTypeValue.Any;
			}
			return type.AsRecordType;
		}

		// Token: 0x06003779 RID: 14201 RVA: 0x000B19BC File Offset: 0x000AFBBC
		public static Value CreateDefaultValue(TypeValue type)
		{
			if (type.TypeKind == ValueKind.Null || type.IsNullable)
			{
				return Value.Null;
			}
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return TimeValue.New(0L);
			case ValueKind.Date:
				return DateValue.New(0L);
			case ValueKind.DateTime:
				return DateTimeValue.New(0L);
			case ValueKind.DateTimeZone:
				return DateTimeZoneValue.New(DateTimeOffset.MinValue);
			case ValueKind.Duration:
				return DurationValue.New(TimeSpan.MinValue);
			case ValueKind.Number:
				return NumberValue.Zero;
			case ValueKind.Logical:
				return LogicalValue.False;
			case ValueKind.Text:
				return TextValue.Empty;
			case ValueKind.Binary:
				return BinaryValue.New(new byte[0]);
			case ValueKind.List:
				return ListValue.Empty;
			case ValueKind.Record:
				return RecordValue.Empty;
			case ValueKind.Table:
				return ListValue.Empty.ToTable(type.AsTableType);
			case ValueKind.Function:
				return Value.Null;
			default:
				throw new InvalidOperationException("Unexpected TypeValue: " + type.ToSource());
			}
		}
	}
}
