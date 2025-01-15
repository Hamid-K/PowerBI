using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualSchema.Annotations;
using Microsoft.Reporting.Common.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000242 RID: 578
	public static class Extensions
	{
		// Token: 0x0600196B RID: 6507 RVA: 0x000455CC File Offset: 0x000437CC
		internal static string GetStringAttributeOrDefault(this XElement element, XName name, string defaultValue)
		{
			if (element == null)
			{
				return null;
			}
			XAttribute xattribute = element.Attribute(name);
			if (xattribute == null)
			{
				return defaultValue;
			}
			return xattribute.Value;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000455F4 File Offset: 0x000437F4
		internal static bool GetBooleanAttributeOrDefault(this XElement element, XName name, bool defaultValue)
		{
			string stringAttributeOrDefault = element.GetStringAttributeOrDefault(name, null);
			if (stringAttributeOrDefault == null)
			{
				return defaultValue;
			}
			return XmlConvert.ToBoolean(stringAttributeOrDefault);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x00045618 File Offset: 0x00043818
		internal static T GetEnumAttributeOrDefault<T>(this XElement element, XName name, T defaultValue) where T : struct
		{
			T? enumAttributeOrNull = element.GetEnumAttributeOrNull(name);
			if (enumAttributeOrNull == null)
			{
				return defaultValue;
			}
			return enumAttributeOrNull.GetValueOrDefault();
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x0004563F File Offset: 0x0004383F
		internal static T? GetEnumAttributeOrNull<T>(this XElement element, XName name) where T : struct
		{
			return Extensions.GetEnumOrNull<T>(element.GetStringAttributeOrDefault(name, null));
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x00045650 File Offset: 0x00043850
		internal static bool TryGetInt32Attribute(this XElement element, XName name, out int value)
		{
			string stringAttributeOrDefault = element.GetStringAttributeOrDefault(name, null);
			if (stringAttributeOrDefault != null && int.TryParse(stringAttributeOrDefault, out value))
			{
				return true;
			}
			value = 0;
			return false;
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x00045678 File Offset: 0x00043878
		internal static bool GetBooleanElementOrDefault(this XElement element, XName name, bool defaultValue)
		{
			XElement elementOrNull = element.GetElementOrNull(name);
			if (elementOrNull == null)
			{
				return defaultValue;
			}
			string value = elementOrNull.Value;
			if (value == null)
			{
				return defaultValue;
			}
			return XmlConvert.ToBoolean(value);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000456A4 File Offset: 0x000438A4
		internal static int GetInt32ElementOrDefault(this XElement element, XName name, int defaultValue)
		{
			XElement elementOrNull = element.GetElementOrNull(name);
			if (elementOrNull == null)
			{
				return defaultValue;
			}
			string value = elementOrNull.Value;
			if (value == null)
			{
				return defaultValue;
			}
			return XmlConvert.ToInt32(value);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000456D0 File Offset: 0x000438D0
		internal static CultureInfo GetCultureInfoAttributeOrDefault(this XElement element, XName name, CultureInfo defaultValue)
		{
			string stringAttributeOrDefault = element.GetStringAttributeOrDefault(name, null);
			if (stringAttributeOrDefault == null)
			{
				return defaultValue;
			}
			return new CultureInfo(stringAttributeOrDefault);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000456F4 File Offset: 0x000438F4
		internal static T GetEnumElementOrDefault<T>(this XElement element, XName name, T defaultValue) where T : struct
		{
			T? enumElementOrNull = element.GetEnumElementOrNull(name);
			if (enumElementOrNull == null)
			{
				return defaultValue;
			}
			return enumElementOrNull.GetValueOrDefault();
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x0004571C File Offset: 0x0004391C
		private static T? GetEnumElementOrNull<T>(this XElement element, XName name) where T : struct
		{
			XElement elementOrNull = element.GetElementOrNull(name);
			if (elementOrNull == null)
			{
				return null;
			}
			return Extensions.GetEnumOrNull<T>(elementOrNull.Value);
		}

		// Token: 0x06001975 RID: 6517 RVA: 0x0004574C File Offset: 0x0004394C
		public static T? GetEnumOrNull<T>(string value) where T : struct
		{
			T t;
			if (value != null && Enum.TryParse<T>(value, false, out t))
			{
				return new T?(t);
			}
			return null;
		}

		// Token: 0x06001976 RID: 6518 RVA: 0x00045777 File Offset: 0x00043977
		internal static XElement GetElementOrNull(this XElement element, XName name)
		{
			if (element == null)
			{
				return null;
			}
			return element.Element(name);
		}

		// Token: 0x06001977 RID: 6519 RVA: 0x00045785 File Offset: 0x00043985
		internal static void GetKnownSubtypes(this EdmProperty property, out EdmField field, out EdmMeasure measure)
		{
			ArgumentValidation.CheckNotNull<EdmProperty>(property, "property");
			field = property as EdmField;
			measure = property as EdmMeasure;
			if (field == null && measure == null)
			{
				throw new NotSupportedException(DevErrors.UnexpectedEdmPropertySubtype(property.GetType().FullName));
			}
		}

		// Token: 0x06001978 RID: 6520 RVA: 0x000457C1 File Offset: 0x000439C1
		internal static EdmPropertyInstance PropertyInstance(this EntitySet entitySet, EdmProperty property)
		{
			return new EdmPropertyInstance(entitySet, property);
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000457CA File Offset: 0x000439CA
		internal static EdmPropertyInstance PropertyInstance(this EntitySet entitySet, EdmHierarchyLevel hierarchyLevel)
		{
			return new EdmPropertyInstance(entitySet, hierarchyLevel);
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000457D3 File Offset: 0x000439D3
		internal static EdmFieldInstance FieldInstance(this EntitySet entitySet, EdmField field)
		{
			return new EdmFieldInstance(entitySet, field);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000457DC File Offset: 0x000439DC
		internal static EdmFieldInstance FieldInstance(this EntitySet entitySet, string fieldName)
		{
			EdmField edmField = entitySet.ElementType.Fields[fieldName];
			return entitySet.FieldInstance(edmField);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x00045802 File Offset: 0x00043A02
		internal static EdmDisplayFolderInstance DisplayFolderInstance(this EntitySet entitySet, EdmDisplayFolder displayFolder)
		{
			return new EdmDisplayFolderInstance(entitySet, displayFolder);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x0004580B File Offset: 0x00043A0B
		internal static EdmHierarchyInstance HierarchyInstance(this EntitySet entitySet, EdmHierarchy hierarchy)
		{
			return new EdmHierarchyInstance(entitySet, hierarchy);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x00045814 File Offset: 0x00043A14
		internal static EdmHierarchyLevelInstance HierarchyLevelInstance(this EntitySet entitySet, EdmHierarchyLevel hierarchyLevel)
		{
			return new EdmHierarchyLevelInstance(entitySet, hierarchyLevel);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x0004581D File Offset: 0x00043A1D
		internal static EdmMeasureInstance MeasureInstance(this EntitySet entitySet, EdmMeasure measure)
		{
			return new EdmMeasureInstance(entitySet, measure);
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x00045828 File Offset: 0x00043A28
		internal static T FindByEdmReferenceName<T>(this IEnumerable<T> items, string referenceName) where T : class, ISupportsReferenceName
		{
			foreach (T t in items)
			{
				if (EdmItem.IdentityComparer.Equals(referenceName, t.ReferenceName))
				{
					return t;
				}
			}
			return default(T);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x00045890 File Offset: 0x00043A90
		internal static bool IsImageField(this EdmProperty property)
		{
			EdmField edmField = property as EdmField;
			if (edmField == null)
			{
				return false;
			}
			FieldContentType? fieldContentType = edmField.Contents;
			FieldContentType fieldContentType2 = FieldContentType.Image;
			if (!((fieldContentType.GetValueOrDefault() == fieldContentType2) & (fieldContentType != null)))
			{
				fieldContentType = edmField.Contents;
				fieldContentType2 = FieldContentType.ImageUrl;
				if (!((fieldContentType.GetValueOrDefault() == fieldContentType2) & (fieldContentType != null)))
				{
					return edmField.ConceptualType.ConceptualDataType == ConceptualPrimitiveType.Binary;
				}
			}
			return true;
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000458F4 File Offset: 0x00043AF4
		internal static bool IsGeographicField(this EdmProperty property)
		{
			EdmField edmField = property as EdmField;
			return edmField != null && edmField.Contents.IsGeographic();
		}

		// Token: 0x06001983 RID: 6531 RVA: 0x00045918 File Offset: 0x00043B18
		internal static bool IsGeocodable(this EdmProperty property)
		{
			EdmField edmField = property as EdmField;
			return edmField != null && edmField.Contents.IsGeocodable();
		}

		// Token: 0x06001984 RID: 6532 RVA: 0x0004593C File Offset: 0x00043B3C
		public static bool IsGeographic(this FieldContentType? fieldContentType)
		{
			return fieldContentType.IsLongitudeOrLatitude() || fieldContentType.IsGeocodable();
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x00045950 File Offset: 0x00043B50
		public static bool IsLongitudeOrLatitude(this FieldContentType? fieldContentType)
		{
			if (fieldContentType == null)
			{
				return false;
			}
			FieldContentType? fieldContentType2 = fieldContentType;
			FieldContentType fieldContentType3 = FieldContentType.Longitude;
			if (!((fieldContentType2.GetValueOrDefault() == fieldContentType3) & (fieldContentType2 != null)))
			{
				fieldContentType2 = fieldContentType;
				fieldContentType3 = FieldContentType.Latitude;
				return (fieldContentType2.GetValueOrDefault() == fieldContentType3) & (fieldContentType2 != null);
			}
			return true;
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x0004599C File Offset: 0x00043B9C
		public static bool IsGeocodable(this FieldContentType? fieldContentType)
		{
			if (fieldContentType == null)
			{
				return false;
			}
			FieldContentType value = fieldContentType.Value;
			return value - FieldContentType.Continent <= 7;
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000459C8 File Offset: 0x00043BC8
		internal static bool IsDate(this EdmField field)
		{
			if (field.IsDateTime())
			{
				return true;
			}
			EntityType entityType = field.DeclaringType as EntityType;
			if (entityType != null)
			{
				EntityContentType? contents = entityType.Contents;
				EntityContentType entityContentType = EntityContentType.Time;
				if ((contents.GetValueOrDefault() == entityContentType) & (contents != null))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x00045A10 File Offset: 0x00043C10
		internal static bool IsDateTime(this EdmField field)
		{
			ConceptualPrimitiveType conceptualDataType = field.ConceptualType.ConceptualDataType;
			return conceptualDataType == ConceptualPrimitiveType.DateTime || conceptualDataType == ConceptualPrimitiveType.DateTimeZone;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x00045A34 File Offset: 0x00043C34
		internal static bool IsDateCategory(this EdmProperty property)
		{
			EdmField edmField = property as EdmField;
			if (edmField == null)
			{
				return false;
			}
			FieldContentType? contents = edmField.Contents;
			FieldContentType fieldContentType = FieldContentType.Date;
			return (contents.GetValueOrDefault() == fieldContentType) & (contents != null);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x00045A6C File Offset: 0x00043C6C
		internal static bool IsDatabaseImageField(this EdmProperty property)
		{
			EdmField edmField = property as EdmField;
			if (edmField == null)
			{
				return false;
			}
			FieldContentType? contents = edmField.Contents;
			FieldContentType fieldContentType = FieldContentType.Image;
			return (contents.GetValueOrDefault() == fieldContentType) & (contents != null);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x00045AA0 File Offset: 0x00043CA0
		internal static bool IsExternalImageField(this EdmProperty property)
		{
			EdmField edmField = property as EdmField;
			if (edmField == null)
			{
				return false;
			}
			FieldContentType? contents = edmField.Contents;
			FieldContentType fieldContentType = FieldContentType.ImageUrl;
			return (contents.GetValueOrDefault() == fieldContentType) & (contents != null);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x00045AD4 File Offset: 0x00043CD4
		internal static bool IsKpiMeasure(this EdmMeasure measure, KpiMeasureKinds kpiKinds)
		{
			Contracts.CheckParam(kpiKinds > KpiMeasureKinds.None, "kpiKinds");
			return (measure.GetKpiMeasureKinds() & kpiKinds) > KpiMeasureKinds.None;
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x00045AF0 File Offset: 0x00043CF0
		internal static EntityDataModel OverrideExtensionSchema(this EntityDataModel model, ExtensionSchema extSchema)
		{
			if (((extSchema != null) ? extSchema.Entities : null) == null)
			{
				return model;
			}
			Dictionary<string, ExtensionEntity> dictionary = null;
			foreach (ExtensionEntity extensionEntity in extSchema.Entities)
			{
				if (!extensionEntity.Columns.IsNullOrEmpty<ExtensionColumn>())
				{
					Microsoft.DataShaping.Util.AddToLazyDictionary<string, ExtensionEntity>(ref dictionary, extensionEntity.Name, extensionEntity, null);
				}
			}
			if (dictionary.IsNullOrEmpty<KeyValuePair<string, ExtensionEntity>>())
			{
				return model;
			}
			GlobalItem[] array = model.EdmItems.ToArray<GlobalItem>();
			Dictionary<string, EntityType> dictionary2 = null;
			for (int i = 0; i < array.Length; i++)
			{
				EntityType entityType = array[i] as EntityType;
				ExtensionEntity extensionEntity2;
				EntitySet entitySet;
				if (entityType != null && (dictionary.TryGetValue(entityType.Name, out extensionEntity2) || (model.EntitySets.TryGetItem(entityType.FullName, out entitySet) && dictionary.TryGetValue(entitySet.ReferenceName, out extensionEntity2))))
				{
					EntityType entityType2 = Extensions.OverrideExtensionColumns(entityType, extensionEntity2);
					if (entityType2 != null)
					{
						Microsoft.DataShaping.Util.AddToLazyDictionary<string, EntityType>(ref dictionary2, entityType2.Name, entityType2, null);
						array[i] = entityType2;
					}
				}
			}
			if (dictionary2.IsNullOrEmpty<KeyValuePair<string, EntityType>>())
			{
				return model;
			}
			EntityContainer entityContainer = (EntityContainer)model.EntityContainer.InternalEdmItem;
			List<EntitySetBase> list = entityContainer.BaseEntitySets.Clone(dictionary2);
			EntityContainer entityContainer2 = entityContainer.Clone(list);
			return new EntityDataModel(new EdmItemCollection(new MetadataCollection<GlobalItem>(array)), entityContainer2, model.EntityContainer.ModelCapabilities);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x00045C54 File Offset: 0x00043E54
		private static EntityType OverrideExtensionColumns(EntityType entityType, ExtensionEntity extensionEntity)
		{
			if (extensionEntity.Columns.IsNullOrEmpty<ExtensionColumn>())
			{
				return null;
			}
			List<EdmMember> list = new List<EdmMember>(extensionEntity.Columns.Count);
			foreach (ExtensionColumn extensionColumn in extensionEntity.Columns)
			{
				TypeUsage typeUsage = extensionColumn.DataType.Value.ConvertToTypeUsage();
				list.Add(new EdmProperty(extensionColumn.Name, typeUsage));
			}
			return entityType.Clone(list);
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x00045CEC File Offset: 0x00043EEC
		private static KpiMeasureKinds GetKpiMeasureKinds(this EdmMeasure measure)
		{
			KpiMeasureKinds kpiMeasureKinds = KpiMeasureKinds.None;
			if (measure != null && measure.Kpi != null)
			{
				if (measure.Kpi.Goal == measure)
				{
					kpiMeasureKinds |= KpiMeasureKinds.Goal;
				}
				if (measure.Kpi.Status == measure)
				{
					kpiMeasureKinds |= KpiMeasureKinds.Status;
				}
				if (measure.Kpi.Value == measure)
				{
					kpiMeasureKinds |= KpiMeasureKinds.Value;
				}
				if (measure.Kpi.Trend == measure)
				{
					kpiMeasureKinds |= KpiMeasureKinds.Trend;
				}
			}
			return kpiMeasureKinds;
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x00045D50 File Offset: 0x00043F50
		internal static bool IsDefaultImage(this EdmField field)
		{
			ArgumentValidation.CheckNotNull<EdmField>(field, "field");
			EntityType entityType = field.DeclaringType as EntityType;
			return entityType != null && field.Equals(entityType.DefaultImage);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x00045D88 File Offset: 0x00043F88
		public static bool IsDateTime(this EdmType edmType)
		{
			ArgumentValidation.CheckNotNull<EdmType>(edmType, "edmType");
			PrimitiveTypeKind? primitiveTypeKind = edmType.GetPrimitiveTypeKind();
			PrimitiveTypeKind primitiveTypeKind2 = PrimitiveTypeKind.DateTime;
			return (primitiveTypeKind.GetValueOrDefault() == primitiveTypeKind2) & (primitiveTypeKind != null);
		}

		// Token: 0x06001992 RID: 6546 RVA: 0x00045DBC File Offset: 0x00043FBC
		public static bool IsDouble(this EdmType edmType)
		{
			PrimitiveTypeKind? primitiveTypeKind = edmType.GetPrimitiveTypeKind();
			PrimitiveTypeKind primitiveTypeKind2 = PrimitiveTypeKind.Double;
			return (primitiveTypeKind.GetValueOrDefault() == primitiveTypeKind2) & (primitiveTypeKind != null);
		}

		// Token: 0x06001993 RID: 6547 RVA: 0x00045DE4 File Offset: 0x00043FE4
		public static bool IsString(this EdmType edmType)
		{
			PrimitiveTypeKind? primitiveTypeKind = edmType.GetPrimitiveTypeKind();
			PrimitiveTypeKind primitiveTypeKind2 = PrimitiveTypeKind.String;
			return (primitiveTypeKind.GetValueOrDefault() == primitiveTypeKind2) & (primitiveTypeKind != null);
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00045E10 File Offset: 0x00044010
		public static bool IsBoolean(this EdmType edmType)
		{
			PrimitiveTypeKind? primitiveTypeKind = edmType.GetPrimitiveTypeKind();
			PrimitiveTypeKind primitiveTypeKind2 = PrimitiveTypeKind.Boolean;
			return (primitiveTypeKind.GetValueOrDefault() == primitiveTypeKind2) & (primitiveTypeKind != null);
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00045E38 File Offset: 0x00044038
		public static bool IsNumeric(this EdmType edmType)
		{
			ArgumentValidation.CheckNotNull<EdmType>(edmType, "edmType");
			PrimitiveTypeKind? primitiveTypeKind = edmType.GetPrimitiveTypeKind();
			if (primitiveTypeKind != null)
			{
				switch (primitiveTypeKind.GetValueOrDefault())
				{
				case PrimitiveTypeKind.Byte:
				case PrimitiveTypeKind.Decimal:
				case PrimitiveTypeKind.Double:
				case PrimitiveTypeKind.Single:
				case PrimitiveTypeKind.SByte:
				case PrimitiveTypeKind.Int16:
				case PrimitiveTypeKind.Int32:
				case PrimitiveTypeKind.Int64:
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00045EB0 File Offset: 0x000440B0
		public static bool IsBinary(this EdmType edmType)
		{
			ArgumentValidation.CheckNotNull<EdmType>(edmType, "edmType");
			PrimitiveTypeKind? primitiveTypeKind = edmType.GetPrimitiveTypeKind();
			PrimitiveTypeKind primitiveTypeKind2 = PrimitiveTypeKind.Binary;
			return (primitiveTypeKind.GetValueOrDefault() == primitiveTypeKind2) & (primitiveTypeKind != null);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x00045EE4 File Offset: 0x000440E4
		public static bool IsScalarType(this EdmType edmType)
		{
			ArgumentValidation.CheckNotNull<EdmType>(edmType, "edmType");
			return !(edmType is RowType) && !(edmType is CollectionType);
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00045F08 File Offset: 0x00044108
		internal static bool IsSingleDisplayKey(this EdmField field)
		{
			ArgumentValidation.CheckNotNull<EdmField>(field, "field");
			EntityType entityType = field.DeclaringType as EntityType;
			return entityType != null && entityType.DisplayKey.Count <= 1 && entityType.DisplayKey.Contains(field);
		}

		// Token: 0x06001999 RID: 6553 RVA: 0x00045F50 File Offset: 0x00044150
		internal static bool IsSingleKey(this EdmField field)
		{
			ArgumentValidation.CheckNotNull<EdmField>(field, "field");
			EntityType entityType = field.DeclaringType as EntityType;
			return entityType != null && entityType.KeyFields.Count <= 1 && entityType.KeyFields.Contains(field);
		}

		// Token: 0x0600199A RID: 6554 RVA: 0x00045F96 File Offset: 0x00044196
		internal static bool IsSortable(this EdmField field)
		{
			return !field.IsImageField() && field.ConceptualType.ConceptualDataType != ConceptualPrimitiveType.Binary;
		}

		// Token: 0x0600199B RID: 6555 RVA: 0x00045FB4 File Offset: 0x000441B4
		internal static bool IsStable(this EdmField field)
		{
			ArgumentValidation.CheckNotNull<EdmField>(field, "field");
			return field.Stability == Stability.Stable;
		}

		// Token: 0x0600199C RID: 6556 RVA: 0x00045FCB File Offset: 0x000441CB
		internal static bool IsRowNumber(this EdmField field)
		{
			ArgumentValidation.CheckNotNull<EdmField>(field, "field");
			return field.Stability == Stability.RowNumber;
		}

		// Token: 0x0600199D RID: 6557 RVA: 0x00045FE2 File Offset: 0x000441E2
		internal static bool DeclaringTypeHasStableKey(this EdmField field)
		{
			ArgumentValidation.CheckNotNull<EdmField>(field, "field");
			return ArgumentValidation.CheckAs<EntityType>(field.DeclaringType, "field").IsKeyStable();
		}

		// Token: 0x0600199E RID: 6558 RVA: 0x00046008 File Offset: 0x00044208
		internal static PrimitiveTypeKind? GetPrimitiveTypeKind(this EdmType edmType)
		{
			PrimitiveType primitiveType = edmType as PrimitiveType;
			if (primitiveType == null)
			{
				return null;
			}
			return new PrimitiveTypeKind?(primitiveType.PrimitiveTypeKind);
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x00046034 File Offset: 0x00044234
		internal static IEnumerable<EdmFieldInstance> GetKeyFieldInstances(this EntitySet entitySet)
		{
			ArgumentValidation.CheckNotNull<EntitySet>(entitySet, "entitySet");
			return Extensions.GetFieldInstances(entitySet, entitySet.ElementType.KeyFields);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x00046053 File Offset: 0x00044253
		internal static IEnumerable<EdmFieldInstance> GetIdentityFieldInstances(this IEdmFieldInstance fieldInstance)
		{
			return Extensions.GetFieldInstances(fieldInstance.Entity, fieldInstance.Field.Grouping.IdentityFields);
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00046070 File Offset: 0x00044270
		internal static IEnumerable<EdmFieldInstance> GetQueryGroupFieldInstances(this IEdmFieldInstance fieldInstance)
		{
			return Extensions.GetFieldInstances(fieldInstance.Entity, fieldInstance.Field.Grouping.QueryGroupFields);
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x0004608D File Offset: 0x0004428D
		internal static IEnumerable<IConceptualColumn> GetQueryGroupColumns(this IConceptualColumn column)
		{
			return column.Grouping.QueryGroupColumns;
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x0004609A File Offset: 0x0004429A
		internal static IEnumerable<EdmFieldInstance> GetOrderByFieldInstances(this IEdmFieldInstance fieldInstance)
		{
			return Extensions.GetFieldInstances(fieldInstance.Entity, fieldInstance.Field.OrderByFields);
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000460B2 File Offset: 0x000442B2
		internal static IEnumerable<EdmFieldInstance> GetFieldsThatGroupOnCurrentFieldInstances(this IEdmFieldInstance fieldInstance)
		{
			return Extensions.GetFieldInstances(fieldInstance.Entity, fieldInstance.Field.Grouping.FieldsWithThisAsIdentity);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000460D0 File Offset: 0x000442D0
		internal static IEdmFieldInstance GetLowestAttributeRelationshipField(this IEnumerable<IEdmFieldInstance> fieldInstances)
		{
			IEdmFieldInstance lowestAttributeRelationshipField = EdmFieldRelationship.GetLowestAttributeRelationshipField<IEdmFieldInstance>(fieldInstances, (IEdmFieldInstance f) => f.Field);
			if (lowestAttributeRelationshipField == null)
			{
				return EdmFieldInstance.Empty;
			}
			return lowestAttributeRelationshipField;
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x00046112 File Offset: 0x00044312
		internal static IEnumerable<IEdmFieldInstance> GetLowerRelationshipPath(this IEdmFieldInstance fieldInstance)
		{
			if (!fieldInstance.IsValid)
			{
				return Enumerable.Empty<IEdmFieldInstance>();
			}
			return Extensions.GetIEdmFieldInstances(fieldInstance.Entity, fieldInstance.Field.Relationship.GetLowerRelationshipPath());
		}

		// Token: 0x060019A7 RID: 6567 RVA: 0x0004613D File Offset: 0x0004433D
		internal static IEnumerable<IConceptualColumn> GetLowerRelationshipPath(this IConceptualColumn column, FieldRelationshipAnnotations annotations)
		{
			yield return column;
			FieldRelationshipAnnotation fieldRelationshipAnnotation;
			if (!annotations.TryGetAnnotation(column, out fieldRelationshipAnnotation))
			{
				yield break;
			}
			IConceptualColumn relatedToSource = fieldRelationshipAnnotation.RelatedToSource;
			IConceptualColumn relatedToSource2;
			for (IConceptualColumn currentField = ((relatedToSource != null) ? relatedToSource.AsColumn() : null); currentField != null; currentField = ((relatedToSource2 != null) ? relatedToSource2.AsColumn() : null))
			{
				yield return currentField;
				FieldRelationshipAnnotation fieldRelationshipAnnotation2;
				if (!annotations.TryGetAnnotation(currentField, out fieldRelationshipAnnotation2))
				{
					yield break;
				}
				relatedToSource2 = fieldRelationshipAnnotation2.RelatedToSource;
			}
			yield break;
		}

		// Token: 0x060019A8 RID: 6568 RVA: 0x00046154 File Offset: 0x00044354
		internal static IEnumerable<IEdmFieldInstance> GetAllFieldsOnPath(this IEdmFieldInstance fieldInstance)
		{
			if (!fieldInstance.IsValid)
			{
				return Enumerable.Empty<IEdmFieldInstance>();
			}
			return Extensions.GetIEdmFieldInstances(fieldInstance.Entity, fieldInstance.Field.Relationship.AllFieldsOnPath);
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x00046180 File Offset: 0x00044380
		internal static IEnumerable<IConceptualColumn> GetAllFieldsOnPath(this IConceptualColumn column, FieldRelationshipAnnotations annotations)
		{
			if (column == null)
			{
				return Enumerable.Empty<IConceptualColumn>();
			}
			FieldRelationshipAnnotation fieldRelationshipAnnotation;
			if (annotations.TryGetAnnotation(column, out fieldRelationshipAnnotation))
			{
				IEnumerable<IConceptualColumn> allFieldsOnPath = fieldRelationshipAnnotation.AllFieldsOnPath;
				return allFieldsOnPath ?? Enumerable.Empty<IConceptualColumn>();
			}
			return new IConceptualColumn[] { column };
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000461C0 File Offset: 0x000443C0
		internal static IEnumerable<IEdmFieldInstance> GetAllFieldsHigherOnPath(this IEdmFieldInstance fieldInstance)
		{
			if (!fieldInstance.IsValid)
			{
				return Enumerable.Empty<IEdmFieldInstance>();
			}
			List<EdmField> list = new List<EdmField>();
			fieldInstance.Field.Relationship.AddAllFieldsOnHigherRelationshipPaths(list);
			return Extensions.GetIEdmFieldInstances(fieldInstance.Entity, list);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x00046200 File Offset: 0x00044400
		internal static IEnumerable<IConceptualColumn> GetAllFieldsHigherOnPath(this IConceptualColumn column, FieldRelationshipAnnotations annotations)
		{
			if (column == null)
			{
				return Enumerable.Empty<IConceptualColumn>();
			}
			List<IConceptualColumn> list = new List<IConceptualColumn>();
			Extensions.AddAllFieldsOnHigherRelationshipPaths(column, list, annotations);
			return list;
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x00046228 File Offset: 0x00044428
		internal static void AddAllFieldsOnHigherRelationshipPaths(IConceptualColumn column, IList<IConceptualColumn> allHigherColumns, FieldRelationshipAnnotations annotations)
		{
			allHigherColumns.Add(column);
			FieldRelationshipAnnotation fieldRelationshipAnnotation;
			if (!annotations.TryGetAnnotation(column, out fieldRelationshipAnnotation) || fieldRelationshipAnnotation.RelatedToFields == null)
			{
				return;
			}
			foreach (IConceptualColumn conceptualColumn in fieldRelationshipAnnotation.RelatedToFields)
			{
				Extensions.AddAllFieldsOnHigherRelationshipPaths(conceptualColumn.AsColumn(), allHigherColumns, annotations);
			}
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x00046294 File Offset: 0x00044494
		private static IEnumerable<EdmFieldInstance> GetFieldInstances(EntitySet entitySet, IEnumerable<EdmField> fields)
		{
			return fields.Select((EdmField f) => entitySet.FieldInstance(f));
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000462C0 File Offset: 0x000444C0
		private static IEnumerable<IEdmFieldInstance> GetIEdmFieldInstances(EntitySet entitySet, IEnumerable<EdmField> fields)
		{
			return fields.Select((EdmField f) => entitySet.PropertyInstance(f).ToIEdmFieldInstance());
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000462EC File Offset: 0x000444EC
		internal static IEnumerable<EdmPropertyInstance> GetProperties(this EntitySet entitySet)
		{
			ArgumentValidation.CheckNotNull<EntitySet>(entitySet, "entitySet");
			return from prop in entitySet.ElementType.Members.OfType<EdmProperty>()
				select entitySet.PropertyInstance(prop);
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00046340 File Offset: 0x00044540
		internal static IEnumerable<EdmHierarchyInstance> GetHierarchies(this EntitySet entitySet)
		{
			ArgumentValidation.CheckNotNull<EntitySet>(entitySet, "entitySet");
			return entitySet.ElementType.Hierarchies.Select((EdmHierarchy hierarchy) => entitySet.HierarchyInstance(hierarchy));
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x0004638C File Offset: 0x0004458C
		internal static IEnumerable<EdmDisplayFolderInstance> GetDisplayFolders(this EntitySet entitySet)
		{
			ArgumentValidation.CheckNotNull<EntitySet>(entitySet, "entitySet");
			return entitySet.ElementType.DisplayFolders.Select((EdmDisplayFolder displayFolder) => entitySet.DisplayFolderInstance(displayFolder));
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000463D8 File Offset: 0x000445D8
		internal static IEnumerable<EdmPropertyInstance> GetProperties(this EdmDisplayFolder displayFolder, EntitySet entitySet)
		{
			ArgumentValidation.CheckNotNull<EdmDisplayFolder>(displayFolder, "displayFolder");
			return from prop in displayFolder.DisplayFolders.OfType<EdmProperty>()
				select entitySet.PropertyInstance(prop);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x0004641C File Offset: 0x0004461C
		internal static IEnumerable<EdmHierarchyInstance> GetHierarchies(this EdmDisplayFolder displayFolder, EntitySet entitySet)
		{
			ArgumentValidation.CheckNotNull<EdmDisplayFolder>(displayFolder, "displayFolder");
			return from hierarchy in displayFolder.DisplayFolders.OfType<EdmHierarchy>()
				select entitySet.HierarchyInstance(hierarchy);
		}

		// Token: 0x04000DDA RID: 3546
		internal const string AnnotationsNsString = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions";

		// Token: 0x04000DDB RID: 3547
		internal const string XsiNsString = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x04000DDC RID: 3548
		internal const string AssociationSetPropId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:AssociationSet";

		// Token: 0x04000DDD RID: 3549
		internal const string EntityContainerPropId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:EntityContainer";

		// Token: 0x04000DDE RID: 3550
		internal const string EntitySetPropId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:EntitySet";

		// Token: 0x04000DDF RID: 3551
		internal const string EntityTypePropId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:EntityType";

		// Token: 0x04000DE0 RID: 3552
		internal const string FieldPropId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:Property";

		// Token: 0x04000DE1 RID: 3553
		internal const string MeasurePropId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:Measure";

		// Token: 0x04000DE2 RID: 3554
		internal const string NavigationPropertyId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:NavigationProperty";

		// Token: 0x04000DE3 RID: 3555
		internal const string VersionPropId = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:Version";

		// Token: 0x04000DE4 RID: 3556
		internal static readonly XNamespace AnnotationsXNamespace = "http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions";

		// Token: 0x04000DE5 RID: 3557
		internal static readonly XNamespace XsiXNamespace = "http://www.w3.org/2001/XMLSchema-instance";

		// Token: 0x04000DE6 RID: 3558
		internal static readonly XName NilAttr = Extensions.XsiXNamespace + "nil";

		// Token: 0x04000DE7 RID: 3559
		internal static readonly XName ActualTypeAttr = "ActualType";

		// Token: 0x04000DE8 RID: 3560
		internal static readonly XName AggregateBehaviorAttr = "AggregateBehavior";

		// Token: 0x04000DE9 RID: 3561
		internal static readonly XName AlignmentAttr = "Alignment";

		// Token: 0x04000DEA RID: 3562
		internal static readonly XName BehaviorAttr = "Behavior";

		// Token: 0x04000DEB RID: 3563
		internal static readonly XName CaptionAttr = "Caption";

		// Token: 0x04000DEC RID: 3564
		internal static readonly XName ContentsAttr = "Contents";

		// Token: 0x04000DED RID: 3565
		internal static readonly XName CrossFilterDirectionAttr = "CrossFilterDirection";

		// Token: 0x04000DEE RID: 3566
		internal static readonly XName CultureAttr = "Culture";

		// Token: 0x04000DEF RID: 3567
		internal static readonly XName DefaultAttr = "Default";

		// Token: 0x04000DF0 RID: 3568
		internal static readonly XName DefaultAggregateFunctionAttr = "DefaultAggregateFunction";

		// Token: 0x04000DF1 RID: 3569
		internal static readonly XName FormatStringAttr = "FormatString";

		// Token: 0x04000DF2 RID: 3570
		internal static readonly XName GroupingBehaviorAttr = "GroupingBehavior";

		// Token: 0x04000DF3 RID: 3571
		internal static readonly XName HiddenAttr = "Hidden";

		// Token: 0x04000DF4 RID: 3572
		internal static readonly XName ShowAsVariationsOnlyAttr = "ShowAsVariationsOnly";

		// Token: 0x04000DF5 RID: 3573
		internal static readonly XName PrivateAttr = "Private";

		// Token: 0x04000DF6 RID: 3574
		internal static readonly XName IsSimpleMeasureAttr = "IsSimpleMeasure";

		// Token: 0x04000DF7 RID: 3575
		internal static readonly XName NameAttr = "Name";

		// Token: 0x04000DF8 RID: 3576
		internal static readonly XName Documentation = "Documentation";

		// Token: 0x04000DF9 RID: 3577
		internal static readonly XName Summary = "Summary";

		// Token: 0x04000DFA RID: 3578
		internal static readonly XName ReferenceNameAttr = "ReferenceName";

		// Token: 0x04000DFB RID: 3579
		internal static readonly XName StabilityAttr = "Stability";

		// Token: 0x04000DFC RID: 3580
		internal static readonly XName StateAttr = "State";

		// Token: 0x04000DFD RID: 3581
		internal static readonly XName StatusGraphicAttr = "StatusGraphic";

		// Token: 0x04000DFE RID: 3582
		internal static readonly XName TrendGraphicAttr = "TrendGraphic";

		// Token: 0x04000DFF RID: 3583
		internal static readonly XName PreferOrdinalStringEqualityAttr = "PreferOrdinalStringEquality";

		// Token: 0x04000E00 RID: 3584
		internal static readonly XName IgnoreCaseAttr = "IgnoreCase";

		// Token: 0x04000E01 RID: 3585
		internal static readonly XName IgnoreNonSpaceAttr = "IgnoreNonSpace";

		// Token: 0x04000E02 RID: 3586
		internal static readonly XName IgnoreKanaTypeAttr = "IgnoreKanaType";

		// Token: 0x04000E03 RID: 3587
		internal static readonly XName IgnoreWidthAttr = "IgnoreWidth";

		// Token: 0x04000E04 RID: 3588
		internal static readonly XName CompareOptionsElem = Extensions.AnnotationsXNamespace + "CompareOptions";

		// Token: 0x04000E05 RID: 3589
		internal static readonly XName DefaultDetailsElem = Extensions.AnnotationsXNamespace + "DefaultDetails";

		// Token: 0x04000E06 RID: 3590
		internal static readonly XName DefaultImageElem = Extensions.AnnotationsXNamespace + "DefaultImage";

		// Token: 0x04000E07 RID: 3591
		internal static readonly XName DefaultMeasureElem = Extensions.AnnotationsXNamespace + "DefaultMeasure";

		// Token: 0x04000E08 RID: 3592
		internal static readonly XName DisplayKeyElem = Extensions.AnnotationsXNamespace + "DisplayKey";

		// Token: 0x04000E09 RID: 3593
		internal static readonly XName DefaultMemberElem = Extensions.AnnotationsXNamespace + "DefaultValue";

		// Token: 0x04000E0A RID: 3594
		internal static readonly XName EncourageIsEmptyDAXFunctionalUsageElem = Extensions.AnnotationsXNamespace + "EncourageIsEmptyDAXFunctionUsage";

		// Token: 0x04000E0B RID: 3595
		internal static readonly XName KpiElem = Extensions.AnnotationsXNamespace + "Kpi";

		// Token: 0x04000E0C RID: 3596
		internal static readonly XName KpiGoalElem = Extensions.AnnotationsXNamespace + "KpiGoal";

		// Token: 0x04000E0D RID: 3597
		internal static readonly XName KpiStatusElem = Extensions.AnnotationsXNamespace + "KpiStatus";

		// Token: 0x04000E0E RID: 3598
		internal static readonly XName KpiTrendElem = Extensions.AnnotationsXNamespace + "KpiTrend";

		// Token: 0x04000E0F RID: 3599
		internal static readonly XName FilterNullsByElem = Extensions.AnnotationsXNamespace + "FilterNullsBy";

		// Token: 0x04000E10 RID: 3600
		internal static readonly XName FormatByElem = Extensions.AnnotationsXNamespace + "FormatBy";

		// Token: 0x04000E11 RID: 3601
		internal static readonly XName ApplyCultureElem = Extensions.AnnotationsXNamespace + "ApplyCulture";

		// Token: 0x04000E12 RID: 3602
		internal static readonly XName GroupByElem = Extensions.AnnotationsXNamespace + "GroupBy";

		// Token: 0x04000E13 RID: 3603
		internal static readonly XName OrderByElem = Extensions.AnnotationsXNamespace + "OrderBy";

		// Token: 0x04000E14 RID: 3604
		internal static readonly XName MemberRefElem = Extensions.AnnotationsXNamespace + "MemberRef";

		// Token: 0x04000E15 RID: 3605
		internal static readonly XName PropertyRefElem = Extensions.AnnotationsXNamespace + "PropertyRef";

		// Token: 0x04000E16 RID: 3606
		internal static readonly XName KpiRefElem = Extensions.AnnotationsXNamespace + "KpiRef";

		// Token: 0x04000E17 RID: 3607
		internal static readonly XName HierarchyRefElem = Extensions.AnnotationsXNamespace + "HierarchyRef";

		// Token: 0x04000E18 RID: 3608
		internal static readonly XName HierarchyElem = Extensions.AnnotationsXNamespace + "Hierarchy";

		// Token: 0x04000E19 RID: 3609
		internal static readonly XName LevelElem = Extensions.AnnotationsXNamespace + "Level";

		// Token: 0x04000E1A RID: 3610
		internal static readonly XName RelatedToElem = Extensions.AnnotationsXNamespace + "RelatedTo";

		// Token: 0x04000E1B RID: 3611
		internal static readonly XName SourceElem = Extensions.AnnotationsXNamespace + "Source";

		// Token: 0x04000E1C RID: 3612
		internal static readonly XName VariationsElem = Extensions.AnnotationsXNamespace + "Variations";

		// Token: 0x04000E1D RID: 3613
		internal static readonly XName VariationElem = Extensions.AnnotationsXNamespace + "Variation";

		// Token: 0x04000E1E RID: 3614
		internal static readonly XName VariationDefaultHierarchyRefElem = Extensions.AnnotationsXNamespace + "DefaultHierarchyRef";

		// Token: 0x04000E1F RID: 3615
		internal static readonly XName VariationDefaultPropertyRefElem = Extensions.AnnotationsXNamespace + "DefaultPropertyRef";

		// Token: 0x04000E20 RID: 3616
		internal static readonly XName NavigationPropertyRefElem = Extensions.AnnotationsXNamespace + "NavigationPropertyRef";

		// Token: 0x04000E21 RID: 3617
		internal static readonly XName ModelCapabilitiesElem = Extensions.AnnotationsXNamespace + "ModelCapabilities";

		// Token: 0x04000E22 RID: 3618
		internal static readonly XName CrossFilteringWithinTableElem = Extensions.AnnotationsXNamespace + "CrossFilteringWithinTable";

		// Token: 0x04000E23 RID: 3619
		internal static readonly XName GroupByValidationElem = Extensions.AnnotationsXNamespace + "GroupByValidation";

		// Token: 0x04000E24 RID: 3620
		internal static readonly XName QueryAggregateUsageElem = Extensions.AnnotationsXNamespace + "QueryAggregateUsage";

		// Token: 0x04000E25 RID: 3621
		internal static readonly XName DisplayFoldersElem = Extensions.AnnotationsXNamespace + "DisplayFolders";

		// Token: 0x04000E26 RID: 3622
		internal static readonly XName DisplayFolderElem = Extensions.AnnotationsXNamespace + "DisplayFolder";

		// Token: 0x04000E27 RID: 3623
		internal static readonly XName FiveStateKPIRange = Extensions.AnnotationsXNamespace + "FiveStateKPIRange";

		// Token: 0x04000E28 RID: 3624
		internal static readonly XName MultiColumnFiltering = Extensions.AnnotationsXNamespace + "MultiColumnFiltering";

		// Token: 0x04000E29 RID: 3625
		internal static readonly XName DataSourceVariablesElem = Extensions.AnnotationsXNamespace + "DataSourceVariables";

		// Token: 0x04000E2A RID: 3626
		internal static readonly XName ExecutionMetricsElem = Extensions.AnnotationsXNamespace + "ExecutionMetrics";

		// Token: 0x04000E2B RID: 3627
		internal static readonly XName DiscourageCompositeModelsElem = Extensions.AnnotationsXNamespace + "DiscourageCompositeModels";

		// Token: 0x04000E2C RID: 3628
		internal static readonly XName VisualCalculationsElem = Extensions.AnnotationsXNamespace + "VisualCalculations";

		// Token: 0x04000E2D RID: 3629
		internal static readonly XName QueryBatchingElem = Extensions.AnnotationsXNamespace + "QueryBatching";

		// Token: 0x04000E2E RID: 3630
		internal static readonly XName VariablesElem = Extensions.AnnotationsXNamespace + "Variables";

		// Token: 0x04000E2F RID: 3631
		internal static readonly XName DAXFunctionsElem = Extensions.AnnotationsXNamespace + "DAXFunctions";

		// Token: 0x04000E30 RID: 3632
		internal static readonly XName DaxExtensionFunctionsElem = Extensions.AnnotationsXNamespace + "DaxExtensionFunctions";

		// Token: 0x04000E31 RID: 3633
		internal static readonly XName DaxExtensionFunctionElem = Extensions.AnnotationsXNamespace + "DaxExtensionFunction";

		// Token: 0x04000E32 RID: 3634
		internal static readonly XName LeftOuterJoinElem = Extensions.AnnotationsXNamespace + "LeftOuterJoin";

		// Token: 0x04000E33 RID: 3635
		internal static readonly XName SubstituteWithIndexElem = Extensions.AnnotationsXNamespace + "SubstituteWithIndex";

		// Token: 0x04000E34 RID: 3636
		internal static readonly XName SummarizeColumnsElem = Extensions.AnnotationsXNamespace + "SummarizeColumns";

		// Token: 0x04000E35 RID: 3637
		internal static readonly XName BinaryMinMaxElem = Extensions.AnnotationsXNamespace + "BinaryMinMax";

		// Token: 0x04000E36 RID: 3638
		internal static readonly XName StringMinMaxElem = Extensions.AnnotationsXNamespace + "StringMinMax";

		// Token: 0x04000E37 RID: 3639
		internal static readonly XName TreatAsElem = Extensions.AnnotationsXNamespace + "TreatAs";

		// Token: 0x04000E38 RID: 3640
		internal static readonly XName SampleAxisWithLocalMinMaxElem = Extensions.AnnotationsXNamespace + "SampleAxisWithLocalMinMax";

		// Token: 0x04000E39 RID: 3641
		internal static readonly XName SampleCartesianPointsByCoverElem = Extensions.AnnotationsXNamespace + "SampleCartesianPointsByCover";

		// Token: 0x04000E3A RID: 3642
		internal static readonly XName InOperatorElem = Extensions.AnnotationsXNamespace + "InOperator";

		// Token: 0x04000E3B RID: 3643
		internal static readonly XName VirtualColumnsElem = Extensions.AnnotationsXNamespace + "VirtualColumns";

		// Token: 0x04000E3C RID: 3644
		internal static readonly XName TableConstructorElem = Extensions.AnnotationsXNamespace + "TableConstructor";

		// Token: 0x04000E3D RID: 3645
		internal static readonly XName OptimizedNotInOperatorElem = Extensions.AnnotationsXNamespace + "OptimizedNotInOperator";

		// Token: 0x04000E3E RID: 3646
		internal static readonly XName NonVisualElem = Extensions.AnnotationsXNamespace + "NonVisual";

		// Token: 0x04000E3F RID: 3647
		internal static readonly XName TopNPerLevelElem = Extensions.AnnotationsXNamespace + "TopNPerLevel";

		// Token: 0x04000E40 RID: 3648
		internal static readonly XName IsAfterElem = Extensions.AnnotationsXNamespace + "IsAfter";

		// Token: 0x04000E41 RID: 3649
		internal static readonly XName FormatByLocaleElem = Extensions.AnnotationsXNamespace + "FormatByLocale";

		// Token: 0x04000E42 RID: 3650
		internal static readonly XName StatisticsElem = Extensions.AnnotationsXNamespace + "Statistics";

		// Token: 0x04000E43 RID: 3651
		internal static readonly XName EntityRefElem = Extensions.AnnotationsXNamespace + "EntityRef";

		// Token: 0x04000E44 RID: 3652
		internal static readonly XName MParametersElem = Extensions.AnnotationsXNamespace + "MParameters";

		// Token: 0x04000E45 RID: 3653
		internal static readonly XName MParameterElem = Extensions.AnnotationsXNamespace + "MParameter";

		// Token: 0x04000E46 RID: 3654
		internal static readonly XName ParameterValuesColumnElem = Extensions.AnnotationsXNamespace + "ParameterValuesColumn";

		// Token: 0x04000E47 RID: 3655
		internal static readonly XName DistinctValueCountAttr = "DistinctValueCount";
	}
}
