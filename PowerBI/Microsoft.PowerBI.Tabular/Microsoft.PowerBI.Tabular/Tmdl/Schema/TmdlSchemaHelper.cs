using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Tabular.Json;
using Microsoft.AnalysisServices.Tabular.Serialization;
using Microsoft.AnalysisServices.Tabular.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Tmdl.Schema
{
	// Token: 0x0200015E RID: 350
	internal static class TmdlSchemaHelper
	{
		// Token: 0x06001600 RID: 5632 RVA: 0x000926E4 File Offset: 0x000908E4
		public static void GenerateSchema(CompatibilityMode mode, int dbCompatibilityLevel, JsonWriter writer)
		{
			TmdlSchema tmdlSchema;
			if (mode == CompatibilityMode.Unknown && dbCompatibilityLevel == -1)
			{
				tmdlSchema = MetadataObjectConfiguration.GetFullSchema();
				mode = CompatibilityMode.PowerBI;
				dbCompatibilityLevel = 1000000;
			}
			else
			{
				if (mode == CompatibilityMode.Unknown)
				{
					mode = CompatibilityMode.PowerBI;
				}
				if (dbCompatibilityLevel == -1)
				{
					dbCompatibilityLevel = 1000000;
				}
				tmdlSchema = TmdlSchema.CreateStandardReadOnlySchema(TmdlObjectInfoWriter.BuildFullMetadataSchema(TmdlSerializationHelper.DefaultFilter, new SerializationActivityContext(MetadataSerializationMode.Tmdl, mode, dbCompatibilityLevel, false, false)), true);
			}
			using (JsonSchemaBuilder jsonSchemaBuilder = new JsonSchemaBuilder(writer, true))
			{
				jsonSchemaBuilder.OpenScope(JsonSchemaScopeType.Global);
				TmdlSchemaHelper.WriteRootElement(jsonSchemaBuilder, tmdlSchema.RootObjects);
				jsonSchemaBuilder.WriteSeperationLine();
				TmdlSchemaHelper.WriteDefinitionsElement(jsonSchemaBuilder, tmdlSchema, mode, dbCompatibilityLevel);
				jsonSchemaBuilder.CloseScope();
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x00092784 File Offset: 0x00090984
		private static void WriteRootElement(JsonSchemaBuilder schemaBuilder, IReadOnlyCollection<TmdlObjectInfo> rootObjects)
		{
			schemaBuilder.WriteRawEntry("type", "object");
			schemaBuilder.WriteRawName("properties");
			schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
			foreach (TmdlObjectInfo tmdlObjectInfo in rootObjects)
			{
				TmdlSchemaHelper.WriteMetadataObjectReference(schemaBuilder, tmdlObjectInfo, true);
			}
			schemaBuilder.CloseScope();
			schemaBuilder.WriteRawEntry("additionalProperties", false);
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x00092804 File Offset: 0x00090A04
		private static void WriteDefinitionsElement(JsonSchemaBuilder schemaBuilder, TmdlSchema schema, CompatibilityMode mode, int dbCompatibilityLevel)
		{
			schemaBuilder.OpenScope(JsonSchemaScopeType.Definitions);
			TmdlSchemaHelper.WriteCommonElements(schemaBuilder, dbCompatibilityLevel);
			schemaBuilder.WriteSeperationLine();
			foreach (Type type in schema.GetAllMetadataEnums())
			{
				schemaBuilder.WriteMetadataEnum(type, PropertyHelper.GetEnumDescription(type), PropertyHelper.GetEnumCompatibleValues(type, mode, dbCompatibilityLevel));
			}
			schemaBuilder.WriteSeperationLine();
			foreach (TmdlObjectInfo tmdlObjectInfo in schema.GetAllMetadataObjects())
			{
				if (tmdlObjectInfo.HasVariants)
				{
					TmdlSchemaHelper.WriteMetadataObjectWithVariants(schemaBuilder, tmdlObjectInfo, schema.RootObjects);
				}
				else
				{
					TmdlSchemaHelper.WriteMetadataObject(schemaBuilder, tmdlObjectInfo, schema.RootObjects);
				}
			}
			schemaBuilder.CloseScope();
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x000928D8 File Offset: 0x00090AD8
		private static void WriteMetadataObject(JsonSchemaBuilder schemaBuilder, TmdlObjectInfo metadataObject, IReadOnlyCollection<TmdlObjectInfo> rootObjects)
		{
			schemaBuilder.WriteMetadataObjectStart(metadataObject.Keyword.ToJsonCase(), metadataObject.Description);
			ICollection<string> collection;
			TmdlSchemaHelper.WriteMetadataObjectProperties(schemaBuilder, metadataObject, rootObjects, out collection);
			TmdlSchemaHelper.WriteMetadataObjectEnd(schemaBuilder, collection, new bool?(false), null);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x00092914 File Offset: 0x00090B14
		private static void WriteMetadataObjectWithVariants(JsonSchemaBuilder schemaBuilder, TmdlObjectInfo metadataObject, IReadOnlyCollection<TmdlObjectInfo> rootObjects)
		{
			schemaBuilder.WriteElementStart(metadataObject.Keyword.ToJsonCase(), null, null);
			schemaBuilder.OpenScope(JsonSchemaScopeType.Choice);
			foreach (KeyValuePair<string, TmdlObjectInfo> keyValuePair in metadataObject.Variants.Where((KeyValuePair<string, TmdlObjectInfo> v) => v.Value.HasAnyProperty(true, true) || v.Value.HasAnyChild(true)))
			{
				schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
				if (!string.IsNullOrEmpty(keyValuePair.Value.Description))
				{
					schemaBuilder.WriteRawEntry("description", keyValuePair.Value.Description);
				}
				schemaBuilder.WriteRawEntry("type", "object");
				ICollection<string> collection;
				TmdlSchemaHelper.WriteMetadataObjectProperties(schemaBuilder, keyValuePair.Value, rootObjects, out collection);
				schemaBuilder.WriteMetadataObjectEnd((collection != null && collection.Count<string>() > 0) ? collection : null, false);
			}
			schemaBuilder.CloseScope();
			schemaBuilder.CloseScope();
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x00092A0C File Offset: 0x00090C0C
		private static void WriteMetadataObjectProperties(JsonSchemaBuilder schemaBuilder, TmdlObjectInfo metadataObject, IReadOnlyCollection<TmdlObjectInfo> rootObjects, out ICollection<string> requiredProperties)
		{
			requiredProperties = new List<string>();
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			schemaBuilder.WriteRawName("properties");
			schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
			if (metadataObject.RequiresName || metadataObject.ObjectType == ObjectType.Database)
			{
				if (metadataObject.ObjectType == ObjectType.TimeUnitColumnAssociation)
				{
					list.Add(new KeyValuePair<string, object>("embeddedProperty", "name"));
					requiredProperties.Add("timeUnit");
					TmdlSchemaHelper.WriteMetadataElementPropertyWithExtensionProperties(schemaBuilder, "timeUnit", "timeUnit", list);
				}
				else if (metadataObject.NameProperty != null)
				{
					if (metadataObject.ObjectType != ObjectType.Database)
					{
						requiredProperties.Add(metadataObject.NameProperty.Name);
					}
					schemaBuilder.WriteMetadataElementProperty(metadataObject.NameProperty.Name, "objectName");
				}
			}
			if (metadataObject.HasDescription)
			{
				schemaBuilder.WriteMetadataElementProperty(metadataObject.DescriptionProperty.Name, "description");
			}
			if (metadataObject.DefaultProperty != null && !metadataObject.DefaultProperty.IsDeprecated)
			{
				TmdlSchemaHelper.WriteProperty(schemaBuilder, metadataObject.DefaultProperty);
			}
			foreach (TmdlPropertyInfo tmdlPropertyInfo in metadataObject.Properties.Where((TmdlPropertyInfo p) => !p.IsDeprecated))
			{
				TmdlSchemaHelper.WriteProperty(schemaBuilder, tmdlPropertyInfo);
			}
			foreach (TmdlObjectInfo tmdlObjectInfo in metadataObject.ChildObjects)
			{
				if (tmdlObjectInfo.ObjectType == ObjectType.Null)
				{
					if (tmdlObjectInfo.HasVariants)
					{
						TmdlSchemaHelper.WriteMetadataObjectWithVariants(schemaBuilder, tmdlObjectInfo, rootObjects);
					}
					else
					{
						TmdlSchemaHelper.WriteMetadataObject(schemaBuilder, tmdlObjectInfo, rootObjects);
					}
				}
				else
				{
					TmdlSchemaHelper.WriteMetadataObjectReference(schemaBuilder, tmdlObjectInfo, false);
				}
			}
			schemaBuilder.CloseScope();
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x00092BD8 File Offset: 0x00090DD8
		private static void WriteMetadataObjectReference(JsonSchemaBuilder schemaBuilder, TmdlObjectInfo objectInfo, bool isRootObject)
		{
			string text;
			if (objectInfo.ObjectType == ObjectType.Culture || objectInfo.ObjectType == ObjectType.RoleMembership)
			{
				text = objectInfo.Keyword.ToJsonCase();
			}
			else
			{
				text = JsonPropertyName.ObjectPath.GetObjectPathPropertyName(objectInfo.ObjectType);
			}
			if (objectInfo.IsSingleton && !isRootObject)
			{
				schemaBuilder.WriteMetadataElementProperty(objectInfo.Keyword.ToJsonCase(), text);
				return;
			}
			schemaBuilder.WriteMetadataElementArrayProperty(objectInfo.Keyword.ToJsonCase(), text);
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x00092C44 File Offset: 0x00090E44
		private static void WriteTranslationElementInfo(JsonSchemaBuilder schemaBuilder, TmdlTranslationElementInfo translationElementInfo)
		{
			string objectPathPropertyName = JsonPropertyName.ObjectPath.GetObjectPathPropertyName(translationElementInfo.ObjectType);
			schemaBuilder.WriteMetadataObjectStart(objectPathPropertyName, null);
			ICollection<string> collection;
			TmdlSchemaHelper.WriteTranslationElementInfoProperties(schemaBuilder, translationElementInfo, out collection);
			TmdlSchemaHelper.WriteMetadataObjectEnd(schemaBuilder, collection, new bool?(false), null);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00092C7C File Offset: 0x00090E7C
		private static void WriteTranslationElementInfoProperties(JsonSchemaBuilder schemaBuilder, TmdlTranslationElementInfo translationElementInfo, out ICollection<string> requiredProperties)
		{
			requiredProperties = new List<string>();
			schemaBuilder.WriteRawName("properties");
			schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
			if (translationElementInfo.RequiresName)
			{
				requiredProperties.Add(translationElementInfo.NameProperty.Name);
				schemaBuilder.WriteMetadataElementProperty(translationElementInfo.NameProperty.Name, "objectName");
			}
			foreach (TmdlPropertyInfo tmdlPropertyInfo in translationElementInfo.Properties.Where((TmdlPropertyInfo p) => !p.IsDeprecated))
			{
				TmdlSchemaHelper.WriteProperty(schemaBuilder, tmdlPropertyInfo);
			}
			foreach (TmdlTranslationElementInfo tmdlTranslationElementInfo in translationElementInfo.ChildElements)
			{
				if (tmdlTranslationElementInfo.IsSingleton)
				{
					TmdlSchemaHelper.WriteTranslationElementInfo(schemaBuilder, tmdlTranslationElementInfo);
				}
				else
				{
					schemaBuilder.WriteMetadataObjectArrayStart(JsonPropertyName.ObjectPath.GetObjectPathPropertyName(tmdlTranslationElementInfo.ObjectType), null);
					ICollection<string> collection;
					TmdlSchemaHelper.WriteTranslationElementInfoProperties(schemaBuilder, tmdlTranslationElementInfo, out collection);
					TmdlSchemaHelper.WriteMetadataObjectArrayEnd(schemaBuilder, collection, false, null);
				}
			}
			schemaBuilder.CloseScope();
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00092DA8 File Offset: 0x00090FA8
		private static void WriteProperty(JsonSchemaBuilder schemaBuilder, TmdlPropertyInfo propertyInfo)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			if (propertyInfo.IsDefaultProperty)
			{
				list.Add(new KeyValuePair<string, object>("defaultProperty", propertyInfo.IsDefaultProperty));
			}
			string text;
			switch (propertyInfo.Type)
			{
			case TmdlValueType.String:
				if (propertyInfo.ExpressionLanguage != null)
				{
					switch (propertyInfo.ExpressionLanguage.Value)
					{
					case TmdlExpressionLanguage.Dax:
						schemaBuilder.WriteMetadataElementProperty(propertyInfo.Name.ToJsonCase(), propertyInfo.IsDefaultProperty ? "defaultDaxExpression" : "daxExpression");
						return;
					case TmdlExpressionLanguage.M:
						schemaBuilder.WriteMetadataElementProperty(propertyInfo.Name.ToJsonCase(), propertyInfo.IsDefaultProperty ? "defaultMExpression" : "mExpression");
						return;
					case TmdlExpressionLanguage.Json:
						schemaBuilder.WriteMetadataElementProperty(propertyInfo.Name.ToJsonCase(), propertyInfo.IsDefaultProperty ? "defaultJsonExpression" : "jsonExpression");
						return;
					case TmdlExpressionLanguage.XmlOrJson:
						list.Insert(0, new KeyValuePair<string, object>("expressionLanguage", "xmlOrJson"));
						break;
					case TmdlExpressionLanguage.NativeQuery:
						list.Insert(0, new KeyValuePair<string, object>("expressionLanguage", "nativeQuery"));
						break;
					case TmdlExpressionLanguage.Other:
						list.Insert(0, new KeyValuePair<string, object>("expressionLanguage", "other"));
						break;
					}
				}
				text = "string";
				break;
			case TmdlValueType.Scalar:
				Utils.Verify(propertyInfo.ScalarValueType != null);
				switch (propertyInfo.ScalarValueType.Value)
				{
				case TmdlScalarValueType.Int:
				case TmdlScalarValueType.Long:
					text = "integer";
					break;
				case TmdlScalarValueType.Double:
					text = "number";
					break;
				case TmdlScalarValueType.Date:
					TmdlSchemaHelper.WriteMetadataElementPropertyWithExtensionProperties(schemaBuilder, propertyInfo.Name.ToJsonCase(), "datetime", list);
					return;
				case TmdlScalarValueType.Bool:
					text = "boolean";
					break;
				case TmdlScalarValueType.Enum:
					TmdlSchemaHelper.WriteMetadataElementPropertyWithExtensionProperties(schemaBuilder, propertyInfo.Name.ToJsonCase(), propertyInfo.EnumType.Name.ToJsonCase(), list);
					return;
				default:
					throw new TomInternalException("Invalid TmdlScalarValueType");
				}
				break;
			case TmdlValueType.Struct:
				schemaBuilder.WriteMetadataObjectStart(propertyInfo.Name, null);
				if (propertyInfo.Children != null)
				{
					schemaBuilder.WriteRawName("properties");
					schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
					foreach (TmdlPropertyInfo tmdlPropertyInfo in propertyInfo.Children.Where((TmdlPropertyInfo c) => !c.IsDeprecated))
					{
						TmdlSchemaHelper.WriteProperty(schemaBuilder, tmdlPropertyInfo);
					}
					schemaBuilder.CloseScope();
				}
				schemaBuilder.WriteMetadataObjectEnd(null, false);
				return;
			case TmdlValueType.Collection:
				schemaBuilder.WritePropertyStart(propertyInfo.Name.ToJsonCase(), "array");
				schemaBuilder.WriteRawName("items");
				schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
				foreach (TmdlPropertyInfo tmdlPropertyInfo2 in propertyInfo.Children.Where((TmdlPropertyInfo c) => !c.IsDeprecated))
				{
					TmdlSchemaHelper.WriteProperty(schemaBuilder, tmdlPropertyInfo2);
				}
				schemaBuilder.CloseScope();
				schemaBuilder.WritePropertyEnd();
				return;
			case TmdlValueType.MetadataObject:
				return;
			case TmdlValueType.ModelReference:
				Utils.Verify(propertyInfo.MetadataObjectType != null);
				list.Insert(0, new KeyValuePair<string, object>("metadataObjectReference", JsonPropertyName.ObjectPath.GetObjectPathPropertyName(propertyInfo.MetadataObjectType.Value)));
				text = "string";
				break;
			case TmdlValueType.TranslationRoot:
				schemaBuilder.WriteMetadataObjectStart(propertyInfo.Name, null);
				schemaBuilder.WriteRawName("properties");
				schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
				TmdlSchemaHelper.WriteTranslationElementInfo(schemaBuilder, propertyInfo.RootElementInfo);
				schemaBuilder.CloseScope();
				TmdlSchemaHelper.WriteMetadataObjectEnd(schemaBuilder, new List<string> { "model" }, new bool?(false), null);
				return;
			default:
				throw new TomInternalException("Invalid TmdlValueType");
			}
			if (propertyInfo.CanBeDuplicated)
			{
				TmdlSchemaHelper.WriteArrayProperty(schemaBuilder, propertyInfo.Name.ToJsonCase(), text, list);
				return;
			}
			schemaBuilder.WritePropertyStart(propertyInfo.Name.ToJsonCase(), text);
			if (list.Count > 0)
			{
				TmdlSchemaHelper.WriteTmdlExtension(schemaBuilder, list);
			}
			schemaBuilder.WritePropertyEnd();
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x000931E0 File Offset: 0x000913E0
		private static void WriteMetadataObjectEnd(JsonSchemaBuilder schemaBuilder, IEnumerable<string> requiredProperties, bool? additionalProperties, IEnumerable<KeyValuePair<string, object>> entries)
		{
			if (requiredProperties != null && requiredProperties.Count<string>() > 0)
			{
				schemaBuilder.WriteRawName("required");
				schemaBuilder.OpenScope(JsonSchemaScopeType.Array);
				foreach (string text in requiredProperties)
				{
					schemaBuilder.WriteRawValue(text);
				}
				schemaBuilder.CloseScope();
			}
			if (additionalProperties != null)
			{
				schemaBuilder.WriteRawEntry("additionalProperties", additionalProperties.Value);
			}
			if (entries != null && entries.Count<KeyValuePair<string, object>>() > 0)
			{
				TmdlSchemaHelper.WriteTmdlExtension(schemaBuilder, entries);
			}
			schemaBuilder.CloseScope();
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x00093280 File Offset: 0x00091480
		private static void WriteMetadataObjectArrayEnd(JsonSchemaBuilder schemaBuilder, IEnumerable<string> requiredProperties, bool additionalProperties, IEnumerable<KeyValuePair<string, object>> entries)
		{
			if (requiredProperties != null && requiredProperties.Count<string>() > 0)
			{
				schemaBuilder.WriteRawName("required");
				schemaBuilder.OpenScope(JsonSchemaScopeType.Array);
				foreach (string text in requiredProperties)
				{
					schemaBuilder.WriteRawValue(text);
				}
				schemaBuilder.CloseScope();
			}
			schemaBuilder.WriteRawEntry("additionalProperties", additionalProperties);
			if (entries != null && entries.Count<KeyValuePair<string, object>>() > 0)
			{
				TmdlSchemaHelper.WriteTmdlExtension(schemaBuilder, entries);
			}
			schemaBuilder.CloseScope();
			schemaBuilder.CloseScope();
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x00093318 File Offset: 0x00091518
		private static void WriteCommonElements(JsonSchemaBuilder schemaBuilder, int dbCompatibilityLevel)
		{
			foreach (KeyValuePair<TmdlPropertyInfo, string> keyValuePair in TmdlSchemaHelper.GetGeneralElements(dbCompatibilityLevel))
			{
				if (keyValuePair.Key.Type == TmdlValueType.Scalar)
				{
					TmdlScalarValueType? scalarValueType = keyValuePair.Key.ScalarValueType;
					TmdlScalarValueType tmdlScalarValueType = TmdlScalarValueType.Date;
					if ((scalarValueType.GetValueOrDefault() == tmdlScalarValueType) & (scalarValueType != null))
					{
						schemaBuilder.WriteElementStart(keyValuePair.Key.Name, "string", keyValuePair.Value);
						schemaBuilder.WriteRawEntry("format", "date-time");
						schemaBuilder.WriteElementEnd(null);
						continue;
					}
				}
				List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
				if (keyValuePair.Key.ExpressionLanguage != null)
				{
					switch (keyValuePair.Key.ExpressionLanguage.Value)
					{
					case TmdlExpressionLanguage.Dax:
						list.Add(new KeyValuePair<string, object>("expressionLanguage", "dax"));
						break;
					case TmdlExpressionLanguage.M:
						list.Add(new KeyValuePair<string, object>("expressionLanguage", "m"));
						break;
					case TmdlExpressionLanguage.Json:
						list.Add(new KeyValuePair<string, object>("expressionLanguage", "json"));
						break;
					}
				}
				else if (keyValuePair.Key.Name == "objectName")
				{
					list.Add(new KeyValuePair<string, object>("embeddedProperty", "name"));
				}
				else if (keyValuePair.Key.Name == "description")
				{
					list.Add(new KeyValuePair<string, object>("embeddedProperty", "description"));
				}
				if (keyValuePair.Key.IsDefaultProperty)
				{
					list.Add(new KeyValuePair<string, object>("defaultProperty", keyValuePair.Key.IsDefaultProperty));
				}
				schemaBuilder.WriteElementStart(keyValuePair.Key.Name, "string", keyValuePair.Value);
				if (list.Count<KeyValuePair<string, object>>() > 0)
				{
					TmdlSchemaHelper.WriteTmdlExtension(schemaBuilder, list);
				}
				schemaBuilder.WriteElementEnd(null);
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x00093544 File Offset: 0x00091744
		private static IEnumerable<KeyValuePair<TmdlPropertyInfo, string>> GetGeneralElements(int dbCompatibilityLevel)
		{
			string text = "objectName";
			TmdlValueType tmdlValueType = TmdlValueType.String;
			TmdlScalarValueType? tmdlScalarValueType = null;
			TmdlExpressionLanguage? tmdlExpressionLanguage = null;
			yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text, tmdlValueType, tmdlScalarValueType, tmdlExpressionLanguage, null, null, null, false, false), "A property with a string that is the name of a named-metadata object");
			string text2 = "description";
			TmdlValueType tmdlValueType2 = TmdlValueType.String;
			TmdlScalarValueType? tmdlScalarValueType2 = null;
			tmdlExpressionLanguage = null;
			yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text2, tmdlValueType2, tmdlScalarValueType2, tmdlExpressionLanguage, null, null, null, false, false), "A property with a set of strings that is the description of a metadata object");
			string text3 = "daxExpression";
			TmdlValueType tmdlValueType3 = TmdlValueType.String;
			tmdlExpressionLanguage = new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
			yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text3, tmdlValueType3, null, tmdlExpressionLanguage, null, null, null, false, false), "A property with a set of strings that contains a DAX expression");
			string text4 = "defaultDaxExpression";
			TmdlValueType tmdlValueType4 = TmdlValueType.String;
			tmdlExpressionLanguage = new TmdlExpressionLanguage?(TmdlExpressionLanguage.Dax);
			yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text4, tmdlValueType4, null, tmdlExpressionLanguage, null, null, null, true, false), "A default property with a set of strings that contains a DAX expression");
			if (dbCompatibilityLevel >= 1450)
			{
				string text5 = "datetime";
				TmdlValueType tmdlValueType5 = TmdlValueType.Scalar;
				TmdlScalarValueType? tmdlScalarValueType3 = new TmdlScalarValueType?(TmdlScalarValueType.Date);
				tmdlExpressionLanguage = null;
				yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text5, tmdlValueType5, tmdlScalarValueType3, tmdlExpressionLanguage, null, null, null, false, false), "A property with a string that contains a datetime");
			}
			if (dbCompatibilityLevel >= 1400)
			{
				string text6 = "mExpression";
				TmdlValueType tmdlValueType6 = TmdlValueType.String;
				tmdlExpressionLanguage = new TmdlExpressionLanguage?(TmdlExpressionLanguage.M);
				yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text6, tmdlValueType6, null, tmdlExpressionLanguage, null, null, null, false, false), "A property with a set of strings that contains an M expression");
				string text7 = "defaultMExpression";
				TmdlValueType tmdlValueType7 = TmdlValueType.String;
				tmdlExpressionLanguage = new TmdlExpressionLanguage?(TmdlExpressionLanguage.M);
				yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text7, tmdlValueType7, null, tmdlExpressionLanguage, null, null, null, true, false), "A default property with a set of strings that contains an M expression");
				string text8 = "jsonExpression";
				TmdlValueType tmdlValueType8 = TmdlValueType.String;
				tmdlExpressionLanguage = new TmdlExpressionLanguage?(TmdlExpressionLanguage.Json);
				yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text8, tmdlValueType8, null, tmdlExpressionLanguage, null, null, null, false, false), "A property with a set of strings that contains a JSON expression");
				string text9 = "defaultJsonExpression";
				TmdlValueType tmdlValueType9 = TmdlValueType.String;
				tmdlExpressionLanguage = new TmdlExpressionLanguage?(TmdlExpressionLanguage.Json);
				yield return new KeyValuePair<TmdlPropertyInfo, string>(new TmdlPropertyInfo(text9, tmdlValueType9, null, tmdlExpressionLanguage, null, null, null, true, false), "A default property with a set of strings that contains a JSON expression");
			}
			yield break;
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x00093554 File Offset: 0x00091754
		private static void WriteMetadataElementPropertyWithExtensionProperties(JsonSchemaBuilder schemaBuilder, string propertyName, string elementName, IEnumerable<KeyValuePair<string, object>> entries)
		{
			schemaBuilder.WriteRawName(propertyName);
			schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
			schemaBuilder.WriteMetadataElementReference(elementName);
			if (entries != null && entries.Count<KeyValuePair<string, object>>() > 0)
			{
				TmdlSchemaHelper.WriteTmdlExtension(schemaBuilder, entries);
			}
			schemaBuilder.CloseScope();
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x00093584 File Offset: 0x00091784
		private static void WriteArrayProperty(JsonSchemaBuilder schemaBuilder, string propertyName, string propertyType, IEnumerable<KeyValuePair<string, object>> entries)
		{
			schemaBuilder.WritePropertyStart(propertyName, "array");
			schemaBuilder.WriteRawName("items");
			schemaBuilder.OpenScope(JsonSchemaScopeType.Object);
			schemaBuilder.WriteRawEntry("type", propertyType);
			if (entries != null && entries.Count<KeyValuePair<string, object>>() > 0)
			{
				TmdlSchemaHelper.WriteTmdlExtension(schemaBuilder, entries);
			}
			schemaBuilder.CloseScope();
			schemaBuilder.WritePropertyEnd();
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x000935DC File Offset: 0x000917DC
		private static void WriteTmdlExtension(JsonSchemaBuilder schemaBuilder, IEnumerable<KeyValuePair<string, object>> entries)
		{
			schemaBuilder.WriteExtensionStart("tmdl");
			foreach (KeyValuePair<string, object> keyValuePair in entries)
			{
				object obj = keyValuePair.Value;
				if (obj is int)
				{
					int num = (int)obj;
					schemaBuilder.WriteRawEntry(keyValuePair.Key, num);
				}
				else
				{
					obj = keyValuePair.Value;
					if (obj is bool)
					{
						bool flag = (bool)obj;
						schemaBuilder.WriteRawEntry(keyValuePair.Key, flag);
					}
					else
					{
						schemaBuilder.WriteRawEntry(keyValuePair.Key, keyValuePair.Value.ToString());
					}
				}
			}
			schemaBuilder.WriteExtensionEnd();
		}

		// Token: 0x0200033C RID: 828
		private static class CommonElement
		{
			// Token: 0x04000E22 RID: 3618
			public const string ObjectName = "objectName";

			// Token: 0x04000E23 RID: 3619
			public const string Description = "description";

			// Token: 0x04000E24 RID: 3620
			public const string Datetime = "datetime";

			// Token: 0x04000E25 RID: 3621
			public const string DaxExpression = "daxExpression";

			// Token: 0x04000E26 RID: 3622
			public const string DefaultDaxExpression = "defaultDaxExpression";

			// Token: 0x04000E27 RID: 3623
			public const string MExpression = "mExpression";

			// Token: 0x04000E28 RID: 3624
			public const string DefaultMExpression = "defaultMExpression";

			// Token: 0x04000E29 RID: 3625
			public const string JsonExpression = "jsonExpression";

			// Token: 0x04000E2A RID: 3626
			public const string DefaultJsonExpression = "defaultJsonExpression";
		}

		// Token: 0x0200033D RID: 829
		private static class TmdlExtension
		{
			// Token: 0x04000E2B RID: 3627
			public const string Marker = "tmdl";

			// Token: 0x04000E2C RID: 3628
			public const string DefaultProperty = "defaultProperty";

			// Token: 0x04000E2D RID: 3629
			public const string ExpressionLanguage = "expressionLanguage";

			// Token: 0x04000E2E RID: 3630
			public const string EmbeddedProperty = "embeddedProperty";

			// Token: 0x04000E2F RID: 3631
			public const string MetadataObjectReference = "metadataObjectReference";
		}

		// Token: 0x0200033E RID: 830
		private static class EmbeddedPropertyValue
		{
			// Token: 0x04000E30 RID: 3632
			public const string Description = "description";

			// Token: 0x04000E31 RID: 3633
			public const string Name = "name";
		}

		// Token: 0x0200033F RID: 831
		private static class ExpressionLanguageValue
		{
			// Token: 0x04000E32 RID: 3634
			public const string Dax = "dax";

			// Token: 0x04000E33 RID: 3635
			public const string M = "m";

			// Token: 0x04000E34 RID: 3636
			public const string Json = "json";

			// Token: 0x04000E35 RID: 3637
			public const string XmlOrJson = "xmlOrJson";

			// Token: 0x04000E36 RID: 3638
			public const string NativeQuery = "nativeQuery";

			// Token: 0x04000E37 RID: 3639
			public const string Other = "other";
		}
	}
}
