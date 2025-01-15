using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000389 RID: 905
	internal class ColumnMapFactory
	{
		// Token: 0x06002BEB RID: 11243 RVA: 0x0008E34C File Offset: 0x0008C54C
		internal virtual CollectionColumnMap CreateFunctionImportStructuralTypeColumnMap(DbDataReader storeDataReader, FunctionImportMappingNonComposable mapping, int resultSetIndex, EntitySet entitySet, StructuralType baseStructuralType)
		{
			FunctionImportStructuralTypeMappingKB resultMapping = mapping.GetResultMapping(resultSetIndex);
			if (resultMapping.NormalizedEntityTypeMappings.Count == 0)
			{
				return this.CreateColumnMapFromReaderAndType(storeDataReader, baseStructuralType, entitySet, resultMapping.ReturnTypeColumnsRenameMapping);
			}
			EntityType entityType = baseStructuralType as EntityType;
			ScalarColumnMap[] array = ColumnMapFactory.CreateDiscriminatorColumnMaps(storeDataReader, mapping, resultSetIndex);
			HashSet<EntityType> hashSet = new HashSet<EntityType>(resultMapping.MappedEntityTypes);
			hashSet.Add(entityType);
			Dictionary<EntityType, TypedColumnMap> dictionary = new Dictionary<EntityType, TypedColumnMap>(hashSet.Count);
			ColumnMap[] array2 = null;
			foreach (EntityType entityType2 in hashSet)
			{
				ColumnMap[] columnMapsForType = ColumnMapFactory.GetColumnMapsForType(storeDataReader, entityType2, resultMapping.ReturnTypeColumnsRenameMapping);
				EntityColumnMap entityColumnMap = ColumnMapFactory.CreateEntityTypeElementColumnMap(storeDataReader, entityType2, entitySet, columnMapsForType, resultMapping.ReturnTypeColumnsRenameMapping);
				if (!entityType2.Abstract)
				{
					dictionary.Add(entityType2, entityColumnMap);
				}
				if (entityType2 == baseStructuralType)
				{
					array2 = columnMapsForType;
				}
			}
			TypeUsage typeUsage = TypeUsage.Create(baseStructuralType);
			string name = baseStructuralType.Name;
			ColumnMap[] array3 = array2;
			SimpleColumnMap[] array4 = array;
			MultipleDiscriminatorPolymorphicColumnMap multipleDiscriminatorPolymorphicColumnMap = new MultipleDiscriminatorPolymorphicColumnMap(typeUsage, name, array3, array4, dictionary, (object[] discriminatorValues) => mapping.Discriminate(discriminatorValues, resultSetIndex));
			return new SimpleCollectionColumnMap(baseStructuralType.GetCollectionType().TypeUsage, baseStructuralType.Name, multipleDiscriminatorPolymorphicColumnMap, null, null);
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x0008E4A0 File Offset: 0x0008C6A0
		internal virtual CollectionColumnMap CreateColumnMapFromReaderAndType(DbDataReader storeDataReader, EdmType edmType, EntitySet entitySet, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			ColumnMap[] columnMapsForType = ColumnMapFactory.GetColumnMapsForType(storeDataReader, edmType, renameList);
			ColumnMap columnMap = null;
			if (Helper.IsRowType(edmType))
			{
				columnMap = new RecordColumnMap(TypeUsage.Create(edmType), edmType.Name, columnMapsForType, null);
			}
			else if (Helper.IsComplexType(edmType))
			{
				columnMap = new ComplexTypeColumnMap(TypeUsage.Create(edmType), edmType.Name, columnMapsForType, null);
			}
			else if (Helper.IsScalarType(edmType))
			{
				if (storeDataReader.FieldCount != 1)
				{
					throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderFieldCountForScalarType);
				}
				columnMap = new ScalarColumnMap(TypeUsage.Create(edmType), edmType.Name, 0, 0);
			}
			else if (Helper.IsEntityType(edmType))
			{
				columnMap = ColumnMapFactory.CreateEntityTypeElementColumnMap(storeDataReader, edmType, entitySet, columnMapsForType, null);
			}
			return new SimpleCollectionColumnMap(edmType.GetCollectionType().TypeUsage, edmType.Name, columnMap, null, null);
		}

		// Token: 0x06002BED RID: 11245 RVA: 0x0008E554 File Offset: 0x0008C754
		internal virtual CollectionColumnMap CreateColumnMapFromReaderAndClrType(DbDataReader reader, Type type, MetadataWorkspace workspace)
		{
			ConstructorInfo declaredConstructor = type.GetDeclaredConstructor(new Type[0]);
			if (type.IsAbstract() || (null == declaredConstructor && !type.IsValueType()))
			{
				throw new InvalidOperationException(Strings.ObjectContext_InvalidTypeForStoreQuery(type));
			}
			List<Tuple<MemberAssignment, int, EdmProperty>> list = new List<Tuple<MemberAssignment, int, EdmProperty>>();
			foreach (PropertyInfo propertyInfo in from p in type.GetInstanceProperties()
				select p.GetPropertyInfoForSet())
			{
				Type type2 = Nullable.GetUnderlyingType(propertyInfo.PropertyType) ?? propertyInfo.PropertyType;
				Type type3 = (type2.IsEnum() ? type2.GetEnumUnderlyingType() : propertyInfo.PropertyType);
				int num;
				EdmType edmType;
				if (ColumnMapFactory.TryGetColumnOrdinalFromReader(reader, propertyInfo.Name, out num) && workspace.TryDetermineCSpaceModelType(type3, out edmType) && Helper.IsScalarType(edmType) && propertyInfo.CanWriteExtended() && propertyInfo.GetIndexParameters().Length == 0 && null != propertyInfo.Setter())
				{
					list.Add(Tuple.Create<MemberAssignment, int, EdmProperty>(Expression.Bind(propertyInfo, Expression.Parameter(propertyInfo.PropertyType, "placeholder")), num, new EdmProperty(propertyInfo.Name, TypeUsage.Create(edmType))));
				}
			}
			MemberInfo[] array = new MemberInfo[list.Count];
			MemberBinding[] array2 = new MemberBinding[list.Count];
			ColumnMap[] array3 = new ColumnMap[list.Count];
			EdmProperty[] array4 = new EdmProperty[list.Count];
			int num2 = 0;
			foreach (IGrouping<int, Tuple<MemberAssignment, int, EdmProperty>> grouping in from tuple in list
				group tuple by tuple.Item2 into tuple
				orderby tuple.Key
				select tuple)
			{
				if (grouping.Count<Tuple<MemberAssignment, int, EdmProperty>>() != 1)
				{
					throw new InvalidOperationException(Strings.ObjectContext_TwoPropertiesMappedToSameColumn(reader.GetName(grouping.Key), string.Join(", ", grouping.Select((Tuple<MemberAssignment, int, EdmProperty> tuple) => tuple.Item3.Name).ToArray<string>())));
				}
				Tuple<MemberAssignment, int, EdmProperty> tuple2 = grouping.Single<Tuple<MemberAssignment, int, EdmProperty>>();
				MemberAssignment item = tuple2.Item1;
				int item2 = tuple2.Item2;
				EdmProperty item3 = tuple2.Item3;
				array[num2] = item.Member;
				array2[num2] = item;
				array3[num2] = new ScalarColumnMap(item3.TypeUsage, item3.Name, 0, item2);
				array4[num2] = item3;
				num2++;
			}
			MemberInitExpression memberInitExpression = Expression.MemberInit((null == declaredConstructor) ? Expression.New(type) : Expression.New(declaredConstructor), array2);
			InitializerMetadata initializerMetadata = InitializerMetadata.CreateProjectionInitializer((EdmItemCollection)workspace.GetItemCollection(DataSpace.CSpace), memberInitExpression);
			RowType rowType = new RowType(array4, initializerMetadata);
			RecordColumnMap recordColumnMap = new RecordColumnMap(TypeUsage.Create(rowType), "DefaultTypeProjection", array3, null);
			return new SimpleCollectionColumnMap(rowType.GetCollectionType().TypeUsage, rowType.Name, recordColumnMap, null, null);
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0008E890 File Offset: 0x0008CA90
		private static EntityColumnMap CreateEntityTypeElementColumnMap(DbDataReader storeDataReader, EdmType edmType, EntitySet entitySet, ColumnMap[] propertyColumnMaps, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			EntityType entityType = (EntityType)edmType;
			ColumnMap[] array = new ColumnMap[storeDataReader.FieldCount];
			foreach (ColumnMap columnMap in propertyColumnMaps)
			{
				int columnPos = ((ScalarColumnMap)columnMap).ColumnPos;
				array[columnPos] = columnMap;
			}
			ReadOnlyMetadataCollection<EdmMember> keyMembers = entityType.KeyMembers;
			SimpleColumnMap[] array2 = new SimpleColumnMap[((ICollection<EdmMember>)keyMembers).Count];
			int num = 0;
			foreach (EdmMember edmMember in ((IEnumerable<EdmMember>)keyMembers))
			{
				int memberOrdinalFromReader = ColumnMapFactory.GetMemberOrdinalFromReader(storeDataReader, edmMember, edmType, renameList);
				ColumnMap columnMap2 = array[memberOrdinalFromReader];
				array2[num] = (SimpleColumnMap)columnMap2;
				num++;
			}
			SimpleEntityIdentity simpleEntityIdentity = new SimpleEntityIdentity(entitySet, array2);
			return new EntityColumnMap(TypeUsage.Create(edmType), edmType.Name, propertyColumnMaps, simpleEntityIdentity);
		}

		// Token: 0x06002BEF RID: 11247 RVA: 0x0008E96C File Offset: 0x0008CB6C
		private static ColumnMap[] GetColumnMapsForType(DbDataReader storeDataReader, EdmType edmType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(edmType);
			ColumnMap[] array = new ColumnMap[allStructuralMembers.Count];
			int num = 0;
			foreach (object obj in allStructuralMembers)
			{
				EdmMember edmMember = (EdmMember)obj;
				if (!Helper.IsScalarType(edmMember.TypeUsage.EdmType))
				{
					throw new InvalidOperationException(Strings.ADP_InvalidDataReaderUnableToMaterializeNonScalarType(edmMember.Name, edmMember.TypeUsage.EdmType.FullName));
				}
				int memberOrdinalFromReader = ColumnMapFactory.GetMemberOrdinalFromReader(storeDataReader, edmMember, edmType, renameList);
				array[num] = new ScalarColumnMap(edmMember.TypeUsage, edmMember.Name, 0, memberOrdinalFromReader);
				num++;
			}
			return array;
		}

		// Token: 0x06002BF0 RID: 11248 RVA: 0x0008EA2C File Offset: 0x0008CC2C
		private static ScalarColumnMap[] CreateDiscriminatorColumnMaps(DbDataReader storeDataReader, FunctionImportMappingNonComposable mapping, int resultIndex)
		{
			TypeUsage typeUsage = TypeUsage.Create(MetadataItem.EdmProviderManifest.GetPrimitiveType(PrimitiveTypeKind.String));
			IList<string> discriminatorColumns = mapping.GetDiscriminatorColumns(resultIndex);
			ScalarColumnMap[] array = new ScalarColumnMap[discriminatorColumns.Count];
			for (int i = 0; i < array.Length; i++)
			{
				string text = discriminatorColumns[i];
				ScalarColumnMap scalarColumnMap = new ScalarColumnMap(typeUsage, text, 0, ColumnMapFactory.GetDiscriminatorOrdinalFromReader(storeDataReader, text, mapping.FunctionImport));
				array[i] = scalarColumnMap;
			}
			return array;
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0008EA98 File Offset: 0x0008CC98
		private static int GetMemberOrdinalFromReader(DbDataReader storeDataReader, EdmMember member, EdmType currentType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			string renameForMember = ColumnMapFactory.GetRenameForMember(member, currentType, renameList);
			int num;
			if (!ColumnMapFactory.TryGetColumnOrdinalFromReader(storeDataReader, renameForMember, out num))
			{
				throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderMissingColumnForType(currentType.FullName, member.Name));
			}
			return num;
		}

		// Token: 0x06002BF2 RID: 11250 RVA: 0x0008EAD4 File Offset: 0x0008CCD4
		private static string GetRenameForMember(EdmMember member, EdmType currentType, Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> renameList)
		{
			if (renameList != null && renameList.Count != 0 && renameList.Any((KeyValuePair<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> m) => m.Key == member.Name))
			{
				return renameList[member.Name].GetRename(currentType);
			}
			return member.Name;
		}

		// Token: 0x06002BF3 RID: 11251 RVA: 0x0008EB30 File Offset: 0x0008CD30
		private static int GetDiscriminatorOrdinalFromReader(DbDataReader storeDataReader, string columnName, EdmFunction functionImport)
		{
			int num;
			if (!ColumnMapFactory.TryGetColumnOrdinalFromReader(storeDataReader, columnName, out num))
			{
				throw new EntityCommandExecutionException(Strings.ADP_InvalidDataReaderMissingDiscriminatorColumn(columnName, functionImport.FullName));
			}
			return num;
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x0008EB5C File Offset: 0x0008CD5C
		private static bool TryGetColumnOrdinalFromReader(DbDataReader storeDataReader, string columnName, out int ordinal)
		{
			if (storeDataReader.FieldCount == 0)
			{
				ordinal = 0;
				return false;
			}
			bool flag;
			try
			{
				ordinal = storeDataReader.GetOrdinal(columnName);
				flag = true;
			}
			catch (IndexOutOfRangeException)
			{
				ordinal = 0;
				flag = false;
			}
			return flag;
		}
	}
}
