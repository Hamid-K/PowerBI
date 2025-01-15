using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000526 RID: 1318
	internal class DefaultObjectMappingItemCollection : MappingItemCollection
	{
		// Token: 0x060040E5 RID: 16613 RVA: 0x000DB4F4 File Offset: 0x000D96F4
		public DefaultObjectMappingItemCollection(EdmItemCollection edmCollection, ObjectItemCollection objectCollection)
			: base(DataSpace.OCSpace)
		{
			this._edmCollection = edmCollection;
			this._objectCollection = objectCollection;
			foreach (PrimitiveType primitiveType in this._edmCollection.GetPrimitiveTypes())
			{
				PrimitiveType mappedPrimitiveType = this._objectCollection.GetMappedPrimitiveType(primitiveType.PrimitiveTypeKind);
				this.AddInternalMapping(new ObjectTypeMapping(mappedPrimitiveType, primitiveType), this._clrTypeIndexes, this._edmTypeIndexes);
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x060040E6 RID: 16614 RVA: 0x000DB5AC File Offset: 0x000D97AC
		public ObjectItemCollection ObjectItemCollection
		{
			get
			{
				return this._objectCollection;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x000DB5B4 File Offset: 0x000D97B4
		public EdmItemCollection EdmItemCollection
		{
			get
			{
				return this._edmCollection;
			}
		}

		// Token: 0x060040E8 RID: 16616 RVA: 0x000DB5BC File Offset: 0x000D97BC
		internal override MappingBase GetMap(string identity, DataSpace typeSpace, bool ignoreCase)
		{
			MappingBase mappingBase;
			if (!this.TryGetMap(identity, typeSpace, ignoreCase, out mappingBase))
			{
				throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(identity));
			}
			return mappingBase;
		}

		// Token: 0x060040E9 RID: 16617 RVA: 0x000DB5E4 File Offset: 0x000D97E4
		internal override bool TryGetMap(string identity, DataSpace typeSpace, bool ignoreCase, out MappingBase map)
		{
			EdmType edmType = null;
			EdmType edmType2 = null;
			if (typeSpace == DataSpace.CSpace)
			{
				if (ignoreCase)
				{
					if (!this._edmCollection.TryGetItem<EdmType>(identity, true, out edmType))
					{
						map = null;
						return false;
					}
					identity = edmType.Identity;
				}
				int num;
				if (this._edmTypeIndexes.TryGetValue(identity, out num))
				{
					map = (MappingBase)base[num];
					return true;
				}
				if (edmType != null || this._edmCollection.TryGetItem<EdmType>(identity, ignoreCase, out edmType))
				{
					this._objectCollection.TryGetOSpaceType(edmType, out edmType2);
				}
			}
			else if (typeSpace == DataSpace.OSpace)
			{
				if (ignoreCase)
				{
					if (!this._objectCollection.TryGetItem<EdmType>(identity, true, out edmType2))
					{
						map = null;
						return false;
					}
					identity = edmType2.Identity;
				}
				int num2;
				if (this._clrTypeIndexes.TryGetValue(identity, out num2))
				{
					map = (MappingBase)base[num2];
					return true;
				}
				if (edmType2 != null || this._objectCollection.TryGetItem<EdmType>(identity, ignoreCase, out edmType2))
				{
					string text = ObjectItemCollection.TryGetMappingCSpaceTypeIdentity(edmType2);
					this._edmCollection.TryGetItem<EdmType>(text, out edmType);
				}
			}
			if (edmType2 == null || edmType == null)
			{
				map = null;
				return false;
			}
			map = this.GetDefaultMapping(edmType, edmType2);
			return true;
		}

		// Token: 0x060040EA RID: 16618 RVA: 0x000DB6EE File Offset: 0x000D98EE
		internal override MappingBase GetMap(string identity, DataSpace typeSpace)
		{
			return this.GetMap(identity, typeSpace, false);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000DB6F9 File Offset: 0x000D98F9
		internal override bool TryGetMap(string identity, DataSpace typeSpace, out MappingBase map)
		{
			return this.TryGetMap(identity, typeSpace, false, out map);
		}

		// Token: 0x060040EC RID: 16620 RVA: 0x000DB708 File Offset: 0x000D9908
		internal override MappingBase GetMap(GlobalItem item)
		{
			MappingBase mappingBase;
			if (!this.TryGetMap(item, out mappingBase))
			{
				throw new InvalidOperationException(Strings.Mapping_Object_InvalidType(item.Identity));
			}
			return mappingBase;
		}

		// Token: 0x060040ED RID: 16621 RVA: 0x000DB734 File Offset: 0x000D9934
		internal override bool TryGetMap(GlobalItem item, out MappingBase map)
		{
			if (item == null)
			{
				map = null;
				return false;
			}
			DataSpace dataSpace = item.DataSpace;
			EdmType edmType = item as EdmType;
			if (edmType != null && Helper.IsTransientType(edmType))
			{
				map = this.GetOCMapForTransientType(edmType, dataSpace);
				return map != null;
			}
			return this.TryGetMap(item.Identity, dataSpace, out map);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x000DB782 File Offset: 0x000D9982
		private MappingBase GetDefaultMapping(EdmType cdmType, EdmType clrType)
		{
			return DefaultObjectMappingItemCollection.LoadObjectMapping(cdmType, clrType, this);
		}

		// Token: 0x060040EF RID: 16623 RVA: 0x000DB78C File Offset: 0x000D998C
		private MappingBase GetOCMapForTransientType(EdmType edmType, DataSpace typeSpace)
		{
			EdmType edmType2 = null;
			EdmType edmType3 = null;
			int num = -1;
			if (typeSpace != DataSpace.OSpace)
			{
				if (this._edmTypeIndexes.TryGetValue(edmType.Identity, out num))
				{
					return (MappingBase)base[num];
				}
				edmType3 = edmType;
				edmType2 = this.ConvertCSpaceToOSpaceType(edmType);
			}
			else if (typeSpace == DataSpace.OSpace)
			{
				if (this._clrTypeIndexes.TryGetValue(edmType.Identity, out num))
				{
					return (MappingBase)base[num];
				}
				edmType2 = edmType;
				edmType3 = this.ConvertOSpaceToCSpaceType(edmType2);
			}
			ObjectTypeMapping objectTypeMapping = new ObjectTypeMapping(edmType2, edmType3);
			if (BuiltInTypeKind.RowType == edmType.BuiltInTypeKind)
			{
				RowType rowType = (RowType)edmType2;
				RowType rowType2 = (RowType)edmType3;
				for (int i = 0; i < rowType.Properties.Count; i++)
				{
					objectTypeMapping.AddMemberMap(new ObjectPropertyMapping(rowType2.Properties[i], rowType.Properties[i]));
				}
			}
			if (!this._edmTypeIndexes.ContainsKey(edmType3.Identity) && !this._clrTypeIndexes.ContainsKey(edmType2.Identity))
			{
				object @lock = this._lock;
				lock (@lock)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(this._clrTypeIndexes);
					Dictionary<string, int> dictionary2 = new Dictionary<string, int>(this._edmTypeIndexes);
					objectTypeMapping = this.AddInternalMapping(objectTypeMapping, dictionary, dictionary2);
					this._clrTypeIndexes = dictionary;
					this._edmTypeIndexes = dictionary2;
				}
			}
			return objectTypeMapping;
		}

		// Token: 0x060040F0 RID: 16624 RVA: 0x000DB8F4 File Offset: 0x000D9AF4
		private EdmType ConvertCSpaceToOSpaceType(EdmType cdmType)
		{
			EdmType edmType;
			if (Helper.IsCollectionType(cdmType))
			{
				edmType = new CollectionType(this.ConvertCSpaceToOSpaceType(((CollectionType)cdmType).TypeUsage.EdmType));
			}
			else if (Helper.IsRowType(cdmType))
			{
				List<EdmProperty> list = new List<EdmProperty>();
				RowType rowType = (RowType)cdmType;
				foreach (EdmProperty edmProperty in rowType.Properties)
				{
					EdmType edmType2 = this.ConvertCSpaceToOSpaceType(edmProperty.TypeUsage.EdmType);
					EdmProperty edmProperty2 = new EdmProperty(edmProperty.Name, TypeUsage.Create(edmType2));
					list.Add(edmProperty2);
				}
				edmType = new RowType(list, rowType.InitializerMetadata);
			}
			else if (Helper.IsRefType(cdmType))
			{
				edmType = new RefType((EntityType)this.ConvertCSpaceToOSpaceType(((RefType)cdmType).ElementType));
			}
			else if (Helper.IsPrimitiveType(cdmType))
			{
				edmType = this._objectCollection.GetMappedPrimitiveType(((PrimitiveType)cdmType).PrimitiveTypeKind);
			}
			else
			{
				edmType = ((ObjectTypeMapping)this.GetMap(cdmType)).ClrType;
			}
			return edmType;
		}

		// Token: 0x060040F1 RID: 16625 RVA: 0x000DBA1C File Offset: 0x000D9C1C
		private EdmType ConvertOSpaceToCSpaceType(EdmType clrType)
		{
			EdmType edmType;
			if (Helper.IsCollectionType(clrType))
			{
				edmType = new CollectionType(this.ConvertOSpaceToCSpaceType(((CollectionType)clrType).TypeUsage.EdmType));
			}
			else if (Helper.IsRowType(clrType))
			{
				List<EdmProperty> list = new List<EdmProperty>();
				RowType rowType = (RowType)clrType;
				foreach (EdmProperty edmProperty in rowType.Properties)
				{
					EdmType edmType2 = this.ConvertOSpaceToCSpaceType(edmProperty.TypeUsage.EdmType);
					EdmProperty edmProperty2 = new EdmProperty(edmProperty.Name, TypeUsage.Create(edmType2));
					list.Add(edmProperty2);
				}
				edmType = new RowType(list, rowType.InitializerMetadata);
			}
			else if (Helper.IsRefType(clrType))
			{
				edmType = new RefType((EntityType)this.ConvertOSpaceToCSpaceType(((RefType)clrType).ElementType));
			}
			else
			{
				edmType = ((ObjectTypeMapping)this.GetMap(clrType)).EdmType;
			}
			return edmType;
		}

		// Token: 0x060040F2 RID: 16626 RVA: 0x000DBB24 File Offset: 0x000D9D24
		private void AddInternalMappings(IEnumerable<ObjectTypeMapping> typeMappings)
		{
			object @lock = this._lock;
			lock (@lock)
			{
				Dictionary<string, int> dictionary = new Dictionary<string, int>(this._clrTypeIndexes);
				Dictionary<string, int> dictionary2 = new Dictionary<string, int>(this._edmTypeIndexes);
				foreach (ObjectTypeMapping objectTypeMapping in typeMappings)
				{
					this.AddInternalMapping(objectTypeMapping, dictionary, dictionary2);
				}
				this._clrTypeIndexes = dictionary;
				this._edmTypeIndexes = dictionary2;
			}
		}

		// Token: 0x060040F3 RID: 16627 RVA: 0x000DBBC8 File Offset: 0x000D9DC8
		private ObjectTypeMapping AddInternalMapping(ObjectTypeMapping objectMap, Dictionary<string, int> clrTypeIndexes, Dictionary<string, int> edmTypeIndexes)
		{
			if (base.Source.ContainsIdentity(objectMap.Identity))
			{
				return (ObjectTypeMapping)base.Source[objectMap.Identity];
			}
			objectMap.DataSpace = DataSpace.OCSpace;
			int count = base.Count;
			base.AddInternal(objectMap);
			string identity = objectMap.ClrType.Identity;
			if (!clrTypeIndexes.ContainsKey(identity))
			{
				clrTypeIndexes.Add(identity, count);
			}
			string identity2 = objectMap.EdmType.Identity;
			if (!edmTypeIndexes.ContainsKey(identity2))
			{
				edmTypeIndexes.Add(identity2, count);
			}
			return objectMap;
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000DBC50 File Offset: 0x000D9E50
		internal static ObjectTypeMapping LoadObjectMapping(EdmType cdmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection)
		{
			Dictionary<string, ObjectTypeMapping> dictionary = new Dictionary<string, ObjectTypeMapping>(StringComparer.Ordinal);
			ObjectTypeMapping objectTypeMapping = DefaultObjectMappingItemCollection.LoadObjectMapping(cdmType, objectType, ocItemCollection, dictionary);
			if (ocItemCollection != null)
			{
				ocItemCollection.AddInternalMappings(dictionary.Values);
			}
			return objectTypeMapping;
		}

		// Token: 0x060040F5 RID: 16629 RVA: 0x000DBC80 File Offset: 0x000D9E80
		private static ObjectTypeMapping LoadObjectMapping(EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			if (Helper.IsEnumType(edmType) ^ Helper.IsEnumType(objectType))
			{
				throw new MappingException(Strings.Mapping_EnumTypeMappingToNonEnumType(edmType.FullName, objectType.FullName));
			}
			if (edmType.Abstract != objectType.Abstract)
			{
				throw new MappingException(Strings.Mapping_AbstractTypeMappingToNonAbstractType(edmType.FullName, objectType.FullName));
			}
			ObjectTypeMapping objectTypeMapping = new ObjectTypeMapping(objectType, edmType);
			typeMappings.Add(edmType.FullName, objectTypeMapping);
			if (Helper.IsEntityType(edmType) || Helper.IsComplexType(edmType))
			{
				DefaultObjectMappingItemCollection.LoadEntityTypeOrComplexTypeMapping(objectTypeMapping, edmType, objectType, ocItemCollection, typeMappings);
			}
			else if (Helper.IsEnumType(edmType))
			{
				DefaultObjectMappingItemCollection.ValidateEnumTypeMapping((EnumType)edmType, (EnumType)objectType);
			}
			else
			{
				DefaultObjectMappingItemCollection.LoadAssociationTypeMapping(objectTypeMapping, edmType, objectType, ocItemCollection, typeMappings);
			}
			return objectTypeMapping;
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000DBD30 File Offset: 0x000D9F30
		private static EdmMember GetObjectMember(EdmMember edmMember, StructuralType objectType)
		{
			EdmMember edmMember2;
			if (!objectType.Members.TryGetValue(edmMember.Name, false, out edmMember2))
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_Clr_Member(edmMember.Name, edmMember.DeclaringType.FullName, objectType.FullName));
			}
			return edmMember2;
		}

		// Token: 0x060040F7 RID: 16631 RVA: 0x000DBD78 File Offset: 0x000D9F78
		private static void ValidateMembersMatch(EdmMember edmMember, EdmMember objectMember)
		{
			if (edmMember.BuiltInTypeKind != objectMember.BuiltInTypeKind)
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_MemberKind_Mismatch(edmMember.Name, edmMember.DeclaringType.FullName, edmMember.BuiltInTypeKind, objectMember.Name, objectMember.DeclaringType.FullName, objectMember.BuiltInTypeKind));
			}
			if (edmMember.TypeUsage.EdmType.BuiltInTypeKind != objectMember.TypeUsage.EdmType.BuiltInTypeKind)
			{
				throw Error.Mapping_Default_OCMapping_Member_Type_Mismatch(edmMember.TypeUsage.EdmType.Name, edmMember.TypeUsage.EdmType.BuiltInTypeKind, edmMember.Name, edmMember.DeclaringType.FullName, objectMember.TypeUsage.EdmType.Name, objectMember.TypeUsage.EdmType.BuiltInTypeKind, objectMember.Name, objectMember.DeclaringType.FullName);
			}
			if (Helper.IsPrimitiveType(edmMember.TypeUsage.EdmType))
			{
				if (Helper.GetSpatialNormalizedPrimitiveType(edmMember.TypeUsage.EdmType).PrimitiveTypeKind != ((PrimitiveType)objectMember.TypeUsage.EdmType).PrimitiveTypeKind)
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_Invalid_MemberType(edmMember.TypeUsage.EdmType.FullName, edmMember.Name, edmMember.DeclaringType.FullName, objectMember.TypeUsage.EdmType.FullName, objectMember.Name, objectMember.DeclaringType.FullName));
				}
			}
			else
			{
				if (Helper.IsEnumType(edmMember.TypeUsage.EdmType))
				{
					DefaultObjectMappingItemCollection.ValidateEnumTypeMapping((EnumType)edmMember.TypeUsage.EdmType, (EnumType)objectMember.TypeUsage.EdmType);
					return;
				}
				EdmType edmType;
				EdmType edmType2;
				if (edmMember.BuiltInTypeKind == BuiltInTypeKind.AssociationEndMember)
				{
					edmType = ((RefType)edmMember.TypeUsage.EdmType).ElementType;
					edmType2 = ((RefType)objectMember.TypeUsage.EdmType).ElementType;
				}
				else if (BuiltInTypeKind.NavigationProperty == edmMember.BuiltInTypeKind && Helper.IsCollectionType(edmMember.TypeUsage.EdmType))
				{
					edmType = ((CollectionType)edmMember.TypeUsage.EdmType).TypeUsage.EdmType;
					edmType2 = ((CollectionType)objectMember.TypeUsage.EdmType).TypeUsage.EdmType;
				}
				else
				{
					edmType = edmMember.TypeUsage.EdmType;
					edmType2 = objectMember.TypeUsage.EdmType;
				}
				if (edmType.Identity != ObjectItemCollection.TryGetMappingCSpaceTypeIdentity(edmType2))
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_Invalid_MemberType(edmMember.TypeUsage.EdmType.FullName, edmMember.Name, edmMember.DeclaringType.FullName, objectMember.TypeUsage.EdmType.FullName, objectMember.Name, objectMember.DeclaringType.FullName));
				}
			}
		}

		// Token: 0x060040F8 RID: 16632 RVA: 0x000DC032 File Offset: 0x000DA232
		private static ObjectPropertyMapping LoadScalarPropertyMapping(EdmProperty edmProperty, EdmProperty objectProperty)
		{
			return new ObjectPropertyMapping(edmProperty, objectProperty);
		}

		// Token: 0x060040F9 RID: 16633 RVA: 0x000DC03C File Offset: 0x000DA23C
		private static void LoadEntityTypeOrComplexTypeMapping(ObjectTypeMapping objectMapping, EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			StructuralType structuralType = (StructuralType)edmType;
			StructuralType structuralType2 = (StructuralType)objectType;
			DefaultObjectMappingItemCollection.ValidateAllMembersAreMapped(structuralType, structuralType2);
			foreach (EdmMember edmMember in structuralType.Members)
			{
				EdmMember objectMember = DefaultObjectMappingItemCollection.GetObjectMember(edmMember, structuralType2);
				DefaultObjectMappingItemCollection.ValidateMembersMatch(edmMember, objectMember);
				if (Helper.IsEdmProperty(edmMember))
				{
					EdmProperty edmProperty = (EdmProperty)edmMember;
					EdmProperty edmProperty2 = (EdmProperty)objectMember;
					if (Helper.IsComplexType(edmMember.TypeUsage.EdmType))
					{
						objectMapping.AddMemberMap(DefaultObjectMappingItemCollection.LoadComplexMemberMapping(edmProperty, edmProperty2, ocItemCollection, typeMappings));
					}
					else
					{
						objectMapping.AddMemberMap(DefaultObjectMappingItemCollection.LoadScalarPropertyMapping(edmProperty, edmProperty2));
					}
				}
				else
				{
					NavigationProperty navigationProperty = (NavigationProperty)edmMember;
					NavigationProperty navigationProperty2 = (NavigationProperty)objectMember;
					DefaultObjectMappingItemCollection.LoadTypeMapping(navigationProperty.RelationshipType, navigationProperty2.RelationshipType, ocItemCollection, typeMappings);
					objectMapping.AddMemberMap(new ObjectNavigationPropertyMapping(navigationProperty, navigationProperty2));
				}
			}
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000DC138 File Offset: 0x000DA338
		private static void ValidateAllMembersAreMapped(StructuralType cdmStructuralType, StructuralType objectStructuralType)
		{
			if (cdmStructuralType.Members.Count != objectStructuralType.Members.Count)
			{
				throw new MappingException(Strings.Mapping_Default_OCMapping_Member_Count_Mismatch(cdmStructuralType.FullName, objectStructuralType.FullName));
			}
			foreach (EdmMember edmMember in objectStructuralType.Members)
			{
				if (!cdmStructuralType.Members.Contains(edmMember.Identity))
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_Clr_Member2(edmMember.Name, objectStructuralType.FullName, cdmStructuralType.FullName));
				}
			}
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000DC1E4 File Offset: 0x000DA3E4
		private static void ValidateEnumTypeMapping(EnumType edmEnumType, EnumType objectEnumType)
		{
			if (edmEnumType.UnderlyingType.PrimitiveTypeKind != objectEnumType.UnderlyingType.PrimitiveTypeKind)
			{
				throw new MappingException(Strings.Mapping_Enum_OCMapping_UnderlyingTypesMismatch(edmEnumType.UnderlyingType.Name, edmEnumType.FullName, objectEnumType.UnderlyingType.Name, objectEnumType.FullName));
			}
			IEnumerator<EnumMember> enumerator = (from m in edmEnumType.Members
				orderby Convert.ToInt64(m.Value, CultureInfo.InvariantCulture), m.Name
				select m).GetEnumerator();
			IEnumerator<EnumMember> enumerator2 = (from m in objectEnumType.Members
				orderby Convert.ToInt64(m.Value, CultureInfo.InvariantCulture), m.Name
				select m).GetEnumerator();
			if (enumerator.MoveNext())
			{
				while (enumerator2.MoveNext())
				{
					if (enumerator.Current.Name == enumerator2.Current.Name && enumerator.Current.Value.Equals(enumerator2.Current.Value) && !enumerator.MoveNext())
					{
						return;
					}
				}
				throw new MappingException(Strings.Mapping_Enum_OCMapping_MemberMismatch(objectEnumType.FullName, enumerator.Current.Name, enumerator.Current.Value, edmEnumType.FullName));
			}
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000DC360 File Offset: 0x000DA560
		private static void LoadAssociationTypeMapping(ObjectTypeMapping objectMapping, EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			AssociationType associationType = (AssociationType)edmType;
			AssociationType associationType2 = (AssociationType)objectType;
			foreach (AssociationEndMember associationEndMember in associationType.AssociationEndMembers)
			{
				AssociationEndMember associationEndMember2 = (AssociationEndMember)DefaultObjectMappingItemCollection.GetObjectMember(associationEndMember, associationType2);
				DefaultObjectMappingItemCollection.ValidateMembersMatch(associationEndMember, associationEndMember2);
				if (associationEndMember.RelationshipMultiplicity != associationEndMember2.RelationshipMultiplicity)
				{
					throw new MappingException(Strings.Mapping_Default_OCMapping_MultiplicityMismatch(associationEndMember.RelationshipMultiplicity, associationEndMember.Name, associationType.FullName, associationEndMember2.RelationshipMultiplicity, associationEndMember2.Name, associationType2.FullName));
				}
				DefaultObjectMappingItemCollection.LoadTypeMapping(((RefType)associationEndMember.TypeUsage.EdmType).ElementType, ((RefType)associationEndMember2.TypeUsage.EdmType).ElementType, ocItemCollection, typeMappings);
				objectMapping.AddMemberMap(new ObjectAssociationEndMapping(associationEndMember, associationEndMember2));
			}
		}

		// Token: 0x060040FD RID: 16637 RVA: 0x000DC464 File Offset: 0x000DA664
		private static ObjectComplexPropertyMapping LoadComplexMemberMapping(EdmProperty containingEdmMember, EdmProperty containingClrMember, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			EdmType edmType = (ComplexType)containingEdmMember.TypeUsage.EdmType;
			ComplexType complexType = (ComplexType)containingClrMember.TypeUsage.EdmType;
			DefaultObjectMappingItemCollection.LoadTypeMapping(edmType, complexType, ocItemCollection, typeMappings);
			return new ObjectComplexPropertyMapping(containingEdmMember, containingClrMember);
		}

		// Token: 0x060040FE RID: 16638 RVA: 0x000DC4A4 File Offset: 0x000DA6A4
		private static ObjectTypeMapping LoadTypeMapping(EdmType edmType, EdmType objectType, DefaultObjectMappingItemCollection ocItemCollection, Dictionary<string, ObjectTypeMapping> typeMappings)
		{
			ObjectTypeMapping objectTypeMapping;
			if (typeMappings.TryGetValue(edmType.FullName, out objectTypeMapping))
			{
				return objectTypeMapping;
			}
			ObjectTypeMapping objectTypeMapping2;
			if (ocItemCollection != null && ocItemCollection.ContainsMap(edmType, out objectTypeMapping2))
			{
				return objectTypeMapping2;
			}
			return DefaultObjectMappingItemCollection.LoadObjectMapping(edmType, objectType, ocItemCollection, typeMappings);
		}

		// Token: 0x060040FF RID: 16639 RVA: 0x000DC4DC File Offset: 0x000DA6DC
		private bool ContainsMap(GlobalItem cspaceItem, out ObjectTypeMapping map)
		{
			int num;
			if (this._edmTypeIndexes.TryGetValue(cspaceItem.Identity, out num))
			{
				map = (ObjectTypeMapping)base[num];
				return true;
			}
			map = null;
			return false;
		}

		// Token: 0x04001687 RID: 5767
		private readonly ObjectItemCollection _objectCollection;

		// Token: 0x04001688 RID: 5768
		private readonly EdmItemCollection _edmCollection;

		// Token: 0x04001689 RID: 5769
		private Dictionary<string, int> _clrTypeIndexes = new Dictionary<string, int>(StringComparer.Ordinal);

		// Token: 0x0400168A RID: 5770
		private Dictionary<string, int> _edmTypeIndexes = new Dictionary<string, int>(StringComparer.Ordinal);

		// Token: 0x0400168B RID: 5771
		private readonly object _lock = new object();
	}
}
