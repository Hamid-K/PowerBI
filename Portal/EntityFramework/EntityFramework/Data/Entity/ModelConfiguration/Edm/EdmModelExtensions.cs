using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Edm.Services;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;
using System.Xml;

namespace System.Data.Entity.ModelConfiguration.Edm
{
	// Token: 0x02000164 RID: 356
	internal static class EdmModelExtensions
	{
		// Token: 0x0600163A RID: 5690 RVA: 0x0003A73C File Offset: 0x0003893C
		public static EntityType AddTable(this EdmModel database, string name)
		{
			string text = database.EntityTypes.UniquifyName(name);
			EntityType entityType = new EntityType(text, "CodeFirstDatabaseSchema", DataSpace.SSpace);
			database.AddItem(entityType);
			database.AddEntitySet(entityType.Name, entityType, text);
			return entityType;
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0003A77C File Offset: 0x0003897C
		public static EntityType AddTable(this EdmModel database, string name, EntityType pkSource)
		{
			EntityType entityType = database.AddTable(name);
			foreach (EdmProperty edmProperty in pkSource.KeyProperties)
			{
				entityType.AddKeyMember(edmProperty.Clone());
			}
			return entityType;
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0003A7E0 File Offset: 0x000389E0
		public static EdmFunction AddFunction(this EdmModel database, string name, EdmFunctionPayload functionPayload)
		{
			EdmFunction edmFunction = new EdmFunction(database.Functions.UniquifyName(name), "CodeFirstDatabaseSchema", DataSpace.SSpace, functionPayload);
			database.AddItem(edmFunction);
			return edmFunction;
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0003A810 File Offset: 0x00038A10
		public static EntityType FindTableByName(this EdmModel database, DatabaseName tableName)
		{
			IList<EntityType> list = (database.EntityTypes as IList<EntityType>) ?? database.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				EntityType entityType = list[i];
				DatabaseName tableName2 = entityType.GetTableName();
				if ((tableName2 != null) ? tableName2.Equals(tableName) : (string.Equals(entityType.Name, tableName.Name, StringComparison.Ordinal) && tableName.Schema == null))
				{
					return entityType;
				}
			}
			return null;
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0003A88C File Offset: 0x00038A8C
		public static bool HasCascadeDeletePath(this EdmModel model, EntityType sourceEntityType, EntityType targetEntityType)
		{
			return (from a in model.AssociationTypes
				from ae in a.Members.Cast<AssociationEndMember>()
				where ae.GetEntityType() == sourceEntityType && ae.DeleteBehavior == OperationAction.Cascade
				select a.GetOtherEnd(ae).GetEntityType()).Any((EntityType et) => et == targetEntityType || model.HasCascadeDeletePath(et, targetEntityType));
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0003A948 File Offset: 0x00038B48
		public static IEnumerable<Type> GetClrTypes(this EdmModel model)
		{
			return model.EntityTypes.Select((EntityType e) => e.GetClrType()).Union(model.ComplexTypes.Select((ComplexType ct) => ct.GetClrType()));
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0003A9B0 File Offset: 0x00038BB0
		public static NavigationProperty GetNavigationProperty(this EdmModel model, PropertyInfo propertyInfo)
		{
			IList<EntityType> list = (model.EntityTypes as IList<EntityType>) ?? model.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				NavigationProperty navigationProperty = list[i].GetNavigationProperty(propertyInfo);
				if (navigationProperty != null)
				{
					return navigationProperty;
				}
			}
			return null;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0003AA00 File Offset: 0x00038C00
		public static void ValidateAndSerializeCsdl(this EdmModel model, XmlWriter writer)
		{
			List<DataModelErrorEventArgs> list = model.SerializeAndGetCsdlErrors(writer);
			if (list.Count > 0)
			{
				throw new ModelValidationException(list);
			}
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0003AA28 File Offset: 0x00038C28
		private static List<DataModelErrorEventArgs> SerializeAndGetCsdlErrors(this EdmModel model, XmlWriter writer)
		{
			List<DataModelErrorEventArgs> validationErrors = new List<DataModelErrorEventArgs>();
			CsdlSerializer csdlSerializer = new CsdlSerializer();
			csdlSerializer.OnError += delegate(object s, DataModelErrorEventArgs e)
			{
				validationErrors.Add(e);
			};
			csdlSerializer.Serialize(model, writer, null);
			return validationErrors;
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0003AA6C File Offset: 0x00038C6C
		public static DbDatabaseMapping GenerateDatabaseMapping(this EdmModel model, DbProviderInfo providerInfo, DbProviderManifest providerManifest)
		{
			return new DatabaseMappingGenerator(providerInfo, providerManifest).Generate(model);
		}

		// Token: 0x06001644 RID: 5700 RVA: 0x0003AA7B File Offset: 0x00038C7B
		public static EdmType GetStructuralOrEnumType(this EdmModel model, string name)
		{
			return model.GetStructuralType(name) ?? model.GetEnumType(name);
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0003AA8F File Offset: 0x00038C8F
		public static EdmType GetStructuralType(this EdmModel model, string name)
		{
			return model.GetEntityType(name) ?? model.GetComplexType(name);
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0003AAA4 File Offset: 0x00038CA4
		public static EntityType GetEntityType(this EdmModel model, string name)
		{
			return model.EntityTypes.SingleOrDefault((EntityType e) => e.Name == name);
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0003AAD8 File Offset: 0x00038CD8
		public static EntityType GetEntityType(this EdmModel model, Type clrType)
		{
			IList<EntityType> list = (model.EntityTypes as IList<EntityType>) ?? model.EntityTypes.ToList<EntityType>();
			for (int i = 0; i < list.Count; i++)
			{
				EntityType entityType = list[i];
				if (entityType.GetClrType() == clrType)
				{
					return entityType;
				}
			}
			return null;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0003AB2C File Offset: 0x00038D2C
		public static ComplexType GetComplexType(this EdmModel model, string name)
		{
			return model.ComplexTypes.SingleOrDefault((ComplexType e) => e.Name == name);
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0003AB60 File Offset: 0x00038D60
		public static ComplexType GetComplexType(this EdmModel model, Type clrType)
		{
			return model.ComplexTypes.SingleOrDefault((ComplexType e) => e.GetClrType() == clrType);
		}

		// Token: 0x0600164A RID: 5706 RVA: 0x0003AB94 File Offset: 0x00038D94
		public static EnumType GetEnumType(this EdmModel model, string name)
		{
			return model.EnumTypes.SingleOrDefault((EnumType e) => e.Name == name);
		}

		// Token: 0x0600164B RID: 5707 RVA: 0x0003ABC8 File Offset: 0x00038DC8
		public static EntityType AddEntityType(this EdmModel model, string name, string modelNamespace = null)
		{
			EntityType entityType = new EntityType(name, modelNamespace ?? "CodeFirstNamespace", DataSpace.CSpace);
			model.AddItem(entityType);
			return entityType;
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0003ABF0 File Offset: 0x00038DF0
		public static EntitySet GetEntitySet(this EdmModel model, EntityType entityType)
		{
			return model.GetEntitySets().SingleOrDefault((EntitySet e) => e.ElementType == entityType.GetRootType());
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0003AC24 File Offset: 0x00038E24
		public static AssociationSet GetAssociationSet(this EdmModel model, AssociationType associationType)
		{
			return model.Containers.Single<EntityContainer>().AssociationSets.SingleOrDefault((AssociationSet a) => a.ElementType == associationType);
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0003AC5F File Offset: 0x00038E5F
		public static IEnumerable<EntitySet> GetEntitySets(this EdmModel model)
		{
			return model.Containers.Single<EntityContainer>().EntitySets;
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0003AC74 File Offset: 0x00038E74
		public static EntitySet AddEntitySet(this EdmModel model, string name, EntityType elementType, string table = null)
		{
			EntitySet entitySet = new EntitySet(name, null, table, null, elementType);
			model.Containers.Single<EntityContainer>().AddEntitySetBase(entitySet);
			return entitySet;
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0003ACA0 File Offset: 0x00038EA0
		public static ComplexType AddComplexType(this EdmModel model, string name, string modelNamespace = null)
		{
			ComplexType complexType = new ComplexType(name, modelNamespace ?? "CodeFirstNamespace", DataSpace.CSpace);
			model.AddItem(complexType);
			return complexType;
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0003ACC8 File Offset: 0x00038EC8
		public static EnumType AddEnumType(this EdmModel model, string name, string modelNamespace = null)
		{
			EnumType enumType = new EnumType(name, modelNamespace ?? "CodeFirstNamespace", PrimitiveType.GetEdmPrimitiveType(PrimitiveTypeKind.Int32), false, DataSpace.CSpace);
			model.AddItem(enumType);
			return enumType;
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0003ACF8 File Offset: 0x00038EF8
		public static AssociationType GetAssociationType(this EdmModel model, string name)
		{
			return model.AssociationTypes.SingleOrDefault((AssociationType a) => a.Name == name);
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0003AD2C File Offset: 0x00038F2C
		public static IEnumerable<AssociationType> GetAssociationTypesBetween(this EdmModel model, EntityType first, EntityType second)
		{
			return model.AssociationTypes.Where((AssociationType a) => (a.SourceEnd.GetEntityType() == first && a.TargetEnd.GetEntityType() == second) || (a.SourceEnd.GetEntityType() == second && a.TargetEnd.GetEntityType() == first));
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0003AD64 File Offset: 0x00038F64
		public static AssociationType AddAssociationType(this EdmModel model, string name, EntityType sourceEntityType, RelationshipMultiplicity sourceAssociationEndKind, EntityType targetEntityType, RelationshipMultiplicity targetAssociationEndKind, string modelNamespace = null)
		{
			AssociationType associationType = new AssociationType(name, modelNamespace ?? "CodeFirstNamespace", false, DataSpace.CSpace)
			{
				SourceEnd = new AssociationEndMember(name + "_Source", sourceEntityType.GetReferenceType(), sourceAssociationEndKind),
				TargetEnd = new AssociationEndMember(name + "_Target", targetEntityType.GetReferenceType(), targetAssociationEndKind)
			};
			model.AddAssociationType(associationType);
			return associationType;
		}

		// Token: 0x06001655 RID: 5717 RVA: 0x0003ADC9 File Offset: 0x00038FC9
		public static void AddAssociationType(this EdmModel model, AssociationType associationType)
		{
			model.AddItem(associationType);
		}

		// Token: 0x06001656 RID: 5718 RVA: 0x0003ADD2 File Offset: 0x00038FD2
		public static void AddAssociationSet(this EdmModel model, AssociationSet associationSet)
		{
			model.Containers.Single<EntityContainer>().AddEntitySetBase(associationSet);
		}

		// Token: 0x06001657 RID: 5719 RVA: 0x0003ADE8 File Offset: 0x00038FE8
		public static void RemoveEntityType(this EdmModel model, EntityType entityType)
		{
			model.RemoveItem(entityType);
			EntityContainer entityContainer = model.Containers.Single<EntityContainer>();
			EntitySet entitySet = entityContainer.EntitySets.SingleOrDefault((EntitySet a) => a.ElementType == entityType);
			if (entitySet != null)
			{
				entityContainer.RemoveEntitySetBase(entitySet);
			}
		}

		// Token: 0x06001658 RID: 5720 RVA: 0x0003AE3C File Offset: 0x0003903C
		public static void ReplaceEntitySet(this EdmModel model, EntityType entityType, EntitySet newSet)
		{
			EntityContainer entityContainer = model.Containers.Single<EntityContainer>();
			EntitySet entitySet = entityContainer.EntitySets.SingleOrDefault((EntitySet a) => a.ElementType == entityType);
			if (entitySet != null)
			{
				entityContainer.RemoveEntitySetBase(entitySet);
				if (newSet != null)
				{
					foreach (AssociationSet associationSet in model.Containers.Single<EntityContainer>().AssociationSets)
					{
						if (associationSet.SourceSet == entitySet)
						{
							associationSet.SourceSet = newSet;
						}
						if (associationSet.TargetSet == entitySet)
						{
							associationSet.TargetSet = newSet;
						}
					}
				}
			}
		}

		// Token: 0x06001659 RID: 5721 RVA: 0x0003AEF8 File Offset: 0x000390F8
		public static void RemoveAssociationType(this EdmModel model, AssociationType associationType)
		{
			model.RemoveItem(associationType);
			EntityContainer entityContainer = model.Containers.Single<EntityContainer>();
			AssociationSet associationSet = entityContainer.AssociationSets.SingleOrDefault((AssociationSet a) => a.ElementType == associationType);
			if (associationSet != null)
			{
				entityContainer.RemoveEntitySetBase(associationSet);
			}
		}

		// Token: 0x0600165A RID: 5722 RVA: 0x0003AF4C File Offset: 0x0003914C
		public static AssociationSet AddAssociationSet(this EdmModel model, string name, AssociationType associationType)
		{
			AssociationSet associationSet = new AssociationSet(name, associationType)
			{
				SourceSet = model.GetEntitySet(associationType.SourceEnd.GetEntityType()),
				TargetSet = model.GetEntitySet(associationType.TargetEnd.GetEntityType())
			};
			model.Containers.Single<EntityContainer>().AddEntitySetBase(associationSet);
			return associationSet;
		}

		// Token: 0x0600165B RID: 5723 RVA: 0x0003AFA4 File Offset: 0x000391A4
		public static IEnumerable<EntityType> GetDerivedTypes(this EdmModel model, EntityType entityType)
		{
			return model.EntityTypes.Where((EntityType et) => et.BaseType == entityType);
		}

		// Token: 0x0600165C RID: 5724 RVA: 0x0003AFD8 File Offset: 0x000391D8
		public static IEnumerable<EntityType> GetSelfAndAllDerivedTypes(this EdmModel model, EntityType entityType)
		{
			List<EntityType> list = new List<EntityType>();
			EdmModelExtensions.AddSelfAndAllDerivedTypes(model, entityType, list);
			return list;
		}

		// Token: 0x0600165D RID: 5725 RVA: 0x0003AFF4 File Offset: 0x000391F4
		private static void AddSelfAndAllDerivedTypes(EdmModel model, EntityType entityType, List<EntityType> entityTypes)
		{
			entityTypes.Add(entityType);
			IEnumerable<EntityType> entityTypes2 = model.EntityTypes;
			Func<EntityType, bool> <>9__0;
			Func<EntityType, bool> func;
			if ((func = <>9__0) == null)
			{
				func = (<>9__0 = (EntityType et) => et.BaseType == entityType);
			}
			foreach (EntityType entityType2 in entityTypes2.Where(func))
			{
				EdmModelExtensions.AddSelfAndAllDerivedTypes(model, entityType2, entityTypes);
			}
		}

		// Token: 0x04000A02 RID: 2562
		public const string DefaultSchema = "dbo";

		// Token: 0x04000A03 RID: 2563
		public const string DefaultModelNamespace = "CodeFirstNamespace";

		// Token: 0x04000A04 RID: 2564
		public const string DefaultStoreNamespace = "CodeFirstDatabaseSchema";
	}
}
