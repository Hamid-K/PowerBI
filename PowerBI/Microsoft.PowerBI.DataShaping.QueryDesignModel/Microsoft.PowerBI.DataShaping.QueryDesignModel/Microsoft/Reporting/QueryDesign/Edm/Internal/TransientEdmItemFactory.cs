using System;
using System.Collections.Generic;
using Microsoft.Data.Metadata.Edm;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Library;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x02000250 RID: 592
	internal static class TransientEdmItemFactory
	{
		// Token: 0x060019EF RID: 6639 RVA: 0x00047862 File Offset: 0x00045A62
		internal static EdmMeasure CreateMeasure(EntitySet entitySet, string measureName, ConceptualPrimitiveResultType measureType)
		{
			return new EdmMeasure(TransientEdmItemFactory.CreateInternalEdmProperty(entitySet, measureName, measureType), measureType, entitySet.ElementType, null);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00047879 File Offset: 0x00045A79
		internal static EdmField CreateField(EntitySet entitySet, string fieldName, ConceptualPrimitiveResultType fieldType)
		{
			return new EdmField(TransientEdmItemFactory.CreateInternalEdmProperty(entitySet, fieldName, fieldType), fieldType, entitySet.ElementType, null);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00047890 File Offset: 0x00045A90
		private static EdmProperty CreateInternalEdmProperty(EntitySet entitySet, string propertyName, ConceptualPrimitiveResultType fieldType)
		{
			EntityType internalEntityType = entitySet.ElementType.InternalEntityType;
			TypeUsage typeUsage = fieldType.ConceptualDataType.ConvertToTypeUsage();
			EdmProperty edmProperty = new EdmProperty(propertyName, typeUsage);
			edmProperty.ChangeDeclaringTypeWithoutCollectionFixup(internalEntityType);
			return edmProperty;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000478C4 File Offset: 0x00045AC4
		internal static EntitySet BuildEntitySet(string entityName, Version modelVersion, IReadOnlyList<ConceptualTypeColumn> columns)
		{
			List<EdmMember> list = new List<EdmMember>(columns.Count);
			foreach (ConceptualTypeColumn conceptualTypeColumn in columns)
			{
				TypeUsage typeUsage = conceptualTypeColumn.PrimitiveType.ConceptualDataType.ConvertToTypeUsage();
				list.Add(new EdmProperty(conceptualTypeColumn.EdmName, typeUsage));
			}
			return TransientEdmItemFactory.BuildEntitySet(entityName, modelVersion, list);
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x0004793C File Offset: 0x00045B3C
		internal static IConceptualEntity BuildEntity(string entityName, IConceptualSchema schema, IReadOnlyList<ConceptualTypeColumn> columns)
		{
			return ConceptualEntity.Create(entityName, entityName, null, null, null, null, ConceptualEntityVisibilityType.AlwaysVisible, null, TransientEdmItemFactory.BuildColumns(columns), null).CompleteInitialization(schema);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x00047964 File Offset: 0x00045B64
		private static IReadOnlyList<ConceptualColumn.Builder> BuildColumns(IReadOnlyList<ConceptualTypeColumn> columns)
		{
			List<ConceptualColumn.Builder> list = new List<ConceptualColumn.Builder>();
			if (columns != null)
			{
				list.Capacity = columns.Count;
				for (int i = 0; i < columns.Count; i++)
				{
					ConceptualTypeColumn conceptualTypeColumn = columns[i];
					DataType typeForPrimitive = DataTypeExtensions.GetTypeForPrimitive(conceptualTypeColumn.PrimitiveType.ConceptualDataType);
					string name = conceptualTypeColumn.Name;
					string name2 = conceptualTypeColumn.Name;
					string name3 = conceptualTypeColumn.Name;
					DataType dataType = typeForPrimitive;
					ConceptualPrimitiveType conceptualDataType = conceptualTypeColumn.PrimitiveType.ConceptualDataType;
					ConceptualColumn.Builder builder = ConceptualColumn.Create(name, i, name2, name3, null, dataType, false, false, null, conceptualDataType, ConceptualDataCategory.None, null, true, false);
					list.Add(builder);
				}
			}
			return list;
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000479F8 File Offset: 0x00045BF8
		internal static EntitySet BuildEntitySet(string entityName, Version modelVersion, IReadOnlyList<EdmMember> edmMembers)
		{
			EntityContainer entityContainer = new EntityContainer("Default", DataSpace.CSpace);
			EntityType entityType = new EntityType(entityName, "Default", DataSpace.CSpace, null, edmMembers);
			entityType.SetReadOnly();
			EntitySet entitySet = new EntitySet(entityName, null, null, null, entityType);
			entitySet.ChangeEntityContainerWithoutCollectionFixup(entityContainer);
			EntityType entityType2 = EntityType.Create(entityType, modelVersion);
			return new EntitySet(entitySet, entityType2);
		}

		// Token: 0x04000E71 RID: 3697
		private const string DefaultEntityNamespace = "Default";

		// Token: 0x04000E72 RID: 3698
		private const DataSpace DefaultEntityDataSpace = DataSpace.CSpace;
	}
}
