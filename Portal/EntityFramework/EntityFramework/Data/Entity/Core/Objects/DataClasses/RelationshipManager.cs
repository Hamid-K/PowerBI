using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000482 RID: 1154
	[Serializable]
	public class RelationshipManager
	{
		// Token: 0x060038CF RID: 14543 RVA: 0x000BB422 File Offset: 0x000B9622
		private RelationshipManager()
		{
			this._entityWrapperFactory = new EntityWrapperFactory();
			this._expensiveLoader = new ExpensiveOSpaceLoader();
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x000BB440 File Offset: 0x000B9640
		internal RelationshipManager(ExpensiveOSpaceLoader expensiveLoader)
		{
			this._entityWrapperFactory = new EntityWrapperFactory();
			this._expensiveLoader = expensiveLoader ?? new ExpensiveOSpaceLoader();
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x000BB463 File Offset: 0x000B9663
		internal void SetExpensiveLoader(ExpensiveOSpaceLoader loader)
		{
			this._expensiveLoader = loader;
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x000BB46C File Offset: 0x000B966C
		internal IEnumerable<RelatedEnd> Relationships
		{
			get
			{
				this.EnsureRelationshipsInitialized();
				return this._relationships.ToArray();
			}
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000BB47F File Offset: 0x000B967F
		private void EnsureRelationshipsInitialized()
		{
			if (this._relationships == null)
			{
				this._relationships = new List<RelatedEnd>();
			}
		}

		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x000BB494 File Offset: 0x000B9694
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x000BB49C File Offset: 0x000B969C
		internal bool NodeVisited
		{
			get
			{
				return this._nodeVisited;
			}
			set
			{
				this._nodeVisited = value;
			}
		}

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x000BB4A5 File Offset: 0x000B96A5
		internal IEntityWrapper WrappedOwner
		{
			get
			{
				if (this._wrappedOwner == null)
				{
					this._wrappedOwner = EntityWrapperFactory.CreateNewWrapper(this._owner, null);
				}
				return this._wrappedOwner;
			}
		}

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x060038D7 RID: 14551 RVA: 0x000BB4C7 File Offset: 0x000B96C7
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000BB4CF File Offset: 0x000B96CF
		public static RelationshipManager Create(IEntityWithRelationships owner)
		{
			Check.NotNull<IEntityWithRelationships>(owner, "owner");
			return new RelationshipManager
			{
				_owner = owner
			};
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x000BB4E9 File Offset: 0x000B96E9
		internal static RelationshipManager Create()
		{
			return new RelationshipManager();
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x000BB4F0 File Offset: 0x000B96F0
		internal void SetWrappedOwner(IEntityWrapper wrappedOwner, object expectedOwner)
		{
			this._wrappedOwner = wrappedOwner;
			if (this._owner != null && expectedOwner != this._owner)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_InvalidRelationshipManagerOwner);
			}
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.SetWrappedOwner(wrappedOwner);
				}
			}
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x000BB56C File Offset: 0x000B976C
		internal EntityCollection<TTargetEntity> GetRelatedCollection<TSourceEntity, TTargetEntity>(AssociationEndMember sourceMember, AssociationEndMember targetMember, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			string fullName = sourceMember.DeclaringType.FullName;
			string name = targetMember.Name;
			RelationshipMultiplicity relationshipMultiplicity = sourceMember.RelationshipMultiplicity;
			RelatedEnd relatedEnd;
			this.TryGetCachedRelatedEnd(fullName, name, out relatedEnd);
			EntityCollection<TTargetEntity> entityCollection = relatedEnd as EntityCollection<TTargetEntity>;
			if (existingRelatedEnd != null)
			{
				if (relatedEnd != null)
				{
					this._relationships.Remove(relatedEnd);
				}
				RelationshipNavigation relationshipNavigation = new RelationshipNavigation((AssociationType)sourceMember.DeclaringType, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor);
				EntityCollection<TTargetEntity> entityCollection2 = this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(relationshipNavigation, relationshipMultiplicity, RelationshipMultiplicity.Many, existingRelatedEnd) as EntityCollection<TTargetEntity>;
				if (entityCollection2 != null)
				{
					bool flag = true;
					try
					{
						RelationshipManager.RemergeCollections<TTargetEntity>(entityCollection, entityCollection2);
						flag = false;
					}
					finally
					{
						if (flag && relatedEnd != null)
						{
							this._relationships.Remove(entityCollection2);
							this._relationships.Add(relatedEnd);
						}
					}
				}
				return entityCollection2;
			}
			if (relatedEnd != null)
			{
				return entityCollection;
			}
			RelationshipNavigation relationshipNavigation2 = new RelationshipNavigation((AssociationType)sourceMember.DeclaringType, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor);
			return this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(relationshipNavigation2, relationshipMultiplicity, RelationshipMultiplicity.Many, existingRelatedEnd) as EntityCollection<TTargetEntity>;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000BB670 File Offset: 0x000B9870
		private static void RemergeCollections<TTargetEntity>(EntityCollection<TTargetEntity> previousCollection, EntityCollection<TTargetEntity> collection) where TTargetEntity : class
		{
			int num = 0;
			List<IEntityWrapper> list = new List<IEntityWrapper>(collection.CountInternal);
			foreach (IEntityWrapper entityWrapper in collection.GetWrappedEntities())
			{
				list.Add(entityWrapper);
			}
			foreach (IEntityWrapper entityWrapper2 in list)
			{
				bool flag = true;
				if (previousCollection != null && previousCollection.ContainsEntity(entityWrapper2))
				{
					num++;
					flag = false;
				}
				if (flag)
				{
					collection.Remove(entityWrapper2, false);
					collection.Add(entityWrapper2);
				}
			}
			if (previousCollection != null && num != previousCollection.CountInternal)
			{
				throw new InvalidOperationException(Strings.Collections_UnableToMergeCollections);
			}
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x000BB748 File Offset: 0x000B9948
		internal EntityReference<TTargetEntity> GetRelatedReference<TSourceEntity, TTargetEntity>(AssociationEndMember sourceMember, AssociationEndMember targetMember, NavigationPropertyAccessor sourceAccessor, NavigationPropertyAccessor targetAccessor, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			string fullName = sourceMember.DeclaringType.FullName;
			string name = targetMember.Name;
			RelationshipMultiplicity relationshipMultiplicity = sourceMember.RelationshipMultiplicity;
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(fullName, name, out relatedEnd))
			{
				return relatedEnd as EntityReference<TTargetEntity>;
			}
			RelationshipNavigation relationshipNavigation = new RelationshipNavigation((AssociationType)sourceMember.DeclaringType, sourceMember.Name, targetMember.Name, sourceAccessor, targetAccessor);
			return this.CreateRelatedEnd<TSourceEntity, TTargetEntity>(relationshipNavigation, relationshipMultiplicity, RelationshipMultiplicity.One, existingRelatedEnd) as EntityReference<TTargetEntity>;
		}

		// Token: 0x060038DE RID: 14558 RVA: 0x000BB7B4 File Offset: 0x000B99B4
		internal RelatedEnd GetRelatedEnd(string navigationProperty, bool throwArgumentException = false)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityType item = wrappedOwner.Context.MetadataWorkspace.GetItem<EntityType>(wrappedOwner.IdentityType.FullNameWithNesting(), DataSpace.OSpace);
			EdmMember edmMember;
			if (!wrappedOwner.Context.Perspective.TryGetMember(item, navigationProperty, false, out edmMember) || !(edmMember is NavigationProperty))
			{
				string text = Strings.RelationshipManager_NavigationPropertyNotFound(navigationProperty);
				throw throwArgumentException ? new ArgumentException(text) : new InvalidOperationException(text);
			}
			NavigationProperty navigationProperty2 = (NavigationProperty)edmMember;
			return this.GetRelatedEndInternal(navigationProperty2.RelationshipType.FullName, navigationProperty2.ToEndMember.Name);
		}

		// Token: 0x060038DF RID: 14559 RVA: 0x000BB843 File Offset: 0x000B9A43
		public IRelatedEnd GetRelatedEnd(string relationshipName, string targetRoleName)
		{
			return this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName);
		}

		// Token: 0x060038E0 RID: 14560 RVA: 0x000BB854 File Offset: 0x000B9A54
		internal RelatedEnd GetRelatedEndInternal(string relationshipName, string targetRoleName)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null && wrappedOwner.RequiresRelationshipChangeTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CannotGetRelatEndForDetachedPocoEntity);
			}
			AssociationType relationshipType = this.GetRelationshipType(relationshipName);
			return this.GetRelatedEndInternal(relationshipName, targetRoleName, null, relationshipType);
		}

		// Token: 0x060038E1 RID: 14561 RVA: 0x000BB898 File Offset: 0x000B9A98
		private RelatedEnd GetRelatedEndInternal(string relationshipName, string targetRoleName, RelatedEnd existingRelatedEnd, AssociationType relationship)
		{
			AssociationEndMember associationEndMember;
			AssociationEndMember associationEndMember2;
			RelationshipManager.GetAssociationEnds(relationship, targetRoleName, out associationEndMember, out associationEndMember2);
			Type clrType = MetadataHelper.GetEntityTypeForEnd(associationEndMember).ClrType;
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (!clrType.IsAssignableFrom(wrappedOwner.IdentityType))
			{
				throw new InvalidOperationException(Strings.RelationshipManager_OwnerIsNotSourceType(wrappedOwner.IdentityType.FullName, clrType.FullName, associationEndMember.Name, relationshipName));
			}
			if (!this.VerifyRelationship(relationship, associationEndMember.Name))
			{
				return null;
			}
			return DelegateFactory.GetRelatedEnd(this, associationEndMember, associationEndMember2, existingRelatedEnd);
		}

		// Token: 0x060038E2 RID: 14562 RVA: 0x000BB910 File Offset: 0x000B9B10
		internal RelatedEnd GetRelatedEndInternal(AssociationType csAssociationType, AssociationEndMember csTargetEnd)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null && wrappedOwner.RequiresRelationshipChangeTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CannotGetRelatEndForDetachedPocoEntity);
			}
			AssociationType relationshipType = this.GetRelationshipType(csAssociationType);
			AssociationEndMember associationEndMember;
			AssociationEndMember associationEndMember2;
			RelationshipManager.GetAssociationEnds(relationshipType, csTargetEnd.Name, out associationEndMember, out associationEndMember2);
			Type clrType = MetadataHelper.GetEntityTypeForEnd(associationEndMember).ClrType;
			if (!clrType.IsAssignableFrom(wrappedOwner.IdentityType))
			{
				throw new InvalidOperationException(Strings.RelationshipManager_OwnerIsNotSourceType(wrappedOwner.IdentityType.FullName, clrType.FullName, associationEndMember.Name, csAssociationType.FullName));
			}
			if (!this.VerifyRelationship(relationshipType, csAssociationType, associationEndMember.Name))
			{
				return null;
			}
			return DelegateFactory.GetRelatedEnd(this, associationEndMember, associationEndMember2, null);
		}

		// Token: 0x060038E3 RID: 14563 RVA: 0x000BB9B8 File Offset: 0x000B9BB8
		private static void GetAssociationEnds(AssociationType associationType, string targetRoleName, out AssociationEndMember sourceEnd, out AssociationEndMember targetEnd)
		{
			targetEnd = associationType.TargetEnd;
			if (targetEnd.Identity != targetRoleName)
			{
				sourceEnd = targetEnd;
				targetEnd = associationType.SourceEnd;
				if (targetEnd.Identity != targetRoleName)
				{
					throw new InvalidOperationException(Strings.RelationshipManager_InvalidTargetRole(associationType.FullName, targetRoleName));
				}
			}
			else
			{
				sourceEnd = associationType.SourceEnd;
			}
		}

		// Token: 0x060038E4 RID: 14564 RVA: 0x000BBA14 File Offset: 0x000B9C14
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeRelatedReference<TTargetEntity>(string relationshipName, string targetRoleName, EntityReference<TTargetEntity> entityReference) where TTargetEntity : class
		{
			Check.NotNull<string>(relationshipName, "relationshipName");
			Check.NotNull<string>(targetRoleName, "targetRoleName");
			Check.NotNull<EntityReference<TTargetEntity>>(entityReference, "entityReference");
			if (entityReference.WrappedOwner.Entity != null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_ReferenceAlreadyInitialized(Strings.RelationshipManager_InitializeIsForDeserialization));
			}
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_RelationshipManagerAttached(Strings.RelationshipManager_InitializeIsForDeserialization));
			}
			relationshipName = this.PrependNamespaceToRelationshipName(relationshipName);
			AssociationType relationshipType = this.GetRelationshipType(relationshipName);
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(relationshipName, targetRoleName, out relatedEnd))
			{
				if (!relatedEnd.IsEmpty())
				{
					entityReference.InitializeWithValue(relatedEnd);
				}
				this._relationships.Remove(relatedEnd);
			}
			if (!(this.GetRelatedEndInternal(relationshipName, targetRoleName, entityReference, relationshipType) is EntityReference<TTargetEntity>))
			{
				throw new InvalidOperationException(Strings.EntityReference_ExpectedReferenceGotCollection(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
		}

		// Token: 0x060038E5 RID: 14565 RVA: 0x000BBAF0 File Offset: 0x000B9CF0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void InitializeRelatedCollection<TTargetEntity>(string relationshipName, string targetRoleName, EntityCollection<TTargetEntity> entityCollection) where TTargetEntity : class
		{
			Check.NotNull<string>(relationshipName, "relationshipName");
			Check.NotNull<string>(targetRoleName, "targetRoleName");
			Check.NotNull<EntityCollection<TTargetEntity>>(entityCollection, "entityCollection");
			if (entityCollection.WrappedOwner.Entity != null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CollectionAlreadyInitialized(Strings.RelationshipManager_CollectionInitializeIsForDeserialization));
			}
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_CollectionRelationshipManagerAttached(Strings.RelationshipManager_CollectionInitializeIsForDeserialization));
			}
			relationshipName = this.PrependNamespaceToRelationshipName(relationshipName);
			AssociationType relationshipType = this.GetRelationshipType(relationshipName);
			if (!(this.GetRelatedEndInternal(relationshipName, targetRoleName, entityCollection, relationshipType) is EntityCollection<TTargetEntity>))
			{
				throw new InvalidOperationException(Strings.Collections_ExpectedCollectionGotReference(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
		}

		// Token: 0x060038E6 RID: 14566 RVA: 0x000BBBA4 File Offset: 0x000B9DA4
		internal string PrependNamespaceToRelationshipName(string relationshipName)
		{
			if (!relationshipName.Contains("."))
			{
				AssociationType associationType;
				if (EntityProxyFactory.TryGetAssociationTypeFromProxyInfo(this.WrappedOwner, relationshipName, out associationType))
				{
					return associationType.FullName;
				}
				if (this._relationships != null)
				{
					string text = this._relationships.Select((RelatedEnd r) => r.RelationshipName).FirstOrDefault((string n) => n.Substring(n.LastIndexOf('.') + 1) == relationshipName);
					if (text != null)
					{
						return text;
					}
				}
				string text2 = this.WrappedOwner.IdentityType.FullNameWithNesting();
				ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
				EdmType edmType = null;
				if (objectItemCollection != null)
				{
					objectItemCollection.TryGetItem<EdmType>(text2, out edmType);
				}
				else
				{
					Dictionary<string, EdmType> dictionary = this._expensiveLoader.LoadTypesExpensiveWay(this.WrappedOwner.IdentityType.Assembly());
					if (dictionary != null)
					{
						dictionary.TryGetValue(text2, out edmType);
					}
				}
				ClrEntityType clrEntityType = edmType as ClrEntityType;
				if (clrEntityType != null)
				{
					return clrEntityType.CSpaceNamespaceName + "." + relationshipName;
				}
			}
			return relationshipName;
		}

		// Token: 0x060038E7 RID: 14567 RVA: 0x000BBCC1 File Offset: 0x000B9EC1
		private static ObjectItemCollection GetObjectItemCollection(IEntityWrapper wrappedOwner)
		{
			if (wrappedOwner.Context != null)
			{
				return (ObjectItemCollection)wrappedOwner.Context.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
			}
			return null;
		}

		// Token: 0x060038E8 RID: 14568 RVA: 0x000BBCE4 File Offset: 0x000B9EE4
		private bool TryGetOwnerEntityType(out EntityType entityType)
		{
			DefaultObjectMappingItemCollection defaultObjectMappingItemCollection;
			MappingBase mappingBase;
			if (RelationshipManager.TryGetObjectMappingItemCollection(this.WrappedOwner, out defaultObjectMappingItemCollection) && defaultObjectMappingItemCollection.TryGetMap(this.WrappedOwner.IdentityType.FullNameWithNesting(), DataSpace.OSpace, out mappingBase))
			{
				ObjectTypeMapping objectTypeMapping = (ObjectTypeMapping)mappingBase;
				if (Helper.IsEntityType(objectTypeMapping.EdmType))
				{
					entityType = (EntityType)objectTypeMapping.EdmType;
					return true;
				}
			}
			entityType = null;
			return false;
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x000BBD42 File Offset: 0x000B9F42
		private static bool TryGetObjectMappingItemCollection(IEntityWrapper wrappedOwner, out DefaultObjectMappingItemCollection collection)
		{
			if (wrappedOwner.Context != null && wrappedOwner.Context.MetadataWorkspace != null)
			{
				collection = (DefaultObjectMappingItemCollection)wrappedOwner.Context.MetadataWorkspace.GetItemCollection(DataSpace.OCSpace);
				return collection != null;
			}
			collection = null;
			return false;
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x000BBD7C File Offset: 0x000B9F7C
		internal AssociationType GetRelationshipType(AssociationType csAssociationType)
		{
			MetadataWorkspace metadataWorkspace = this.WrappedOwner.Context.MetadataWorkspace;
			if (metadataWorkspace != null)
			{
				return metadataWorkspace.MetadataOptimization.GetOSpaceAssociationType(csAssociationType, () => this.GetRelationshipType(csAssociationType.FullName));
			}
			return this.GetRelationshipType(csAssociationType.FullName);
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x000BBDE0 File Offset: 0x000B9FE0
		internal AssociationType GetRelationshipType(string relationshipName)
		{
			AssociationType associationType = null;
			ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
			if (objectItemCollection != null)
			{
				associationType = objectItemCollection.GetRelationshipType(relationshipName);
			}
			if (associationType == null)
			{
				EntityProxyFactory.TryGetAssociationTypeFromProxyInfo(this.WrappedOwner, relationshipName, out associationType);
			}
			if (associationType == null && this._relationships != null)
			{
				associationType = (from e in this._relationships
					where e.RelationshipName == relationshipName
					select e.RelationMetadata).OfType<AssociationType>().FirstOrDefault<AssociationType>();
			}
			if (associationType == null)
			{
				associationType = this._expensiveLoader.GetRelationshipTypeExpensiveWay(this.WrappedOwner.IdentityType, relationshipName);
			}
			if (associationType == null)
			{
				throw RelationshipManager.UnableToGetMetadata(this.WrappedOwner, relationshipName);
			}
			return associationType;
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x000BBEB8 File Offset: 0x000BA0B8
		internal static Exception UnableToGetMetadata(IEntityWrapper wrappedOwner, string relationshipName)
		{
			ArgumentException ex = new ArgumentException(Strings.RelationshipManager_UnableToFindRelationshipTypeInMetadata(relationshipName), "relationshipName");
			if (EntityProxyFactory.IsProxyType(wrappedOwner.Entity.GetType()))
			{
				return new InvalidOperationException(Strings.EntityProxyTypeInfo_ProxyMetadataIsUnavailable(wrappedOwner.IdentityType.FullName), ex);
			}
			return ex;
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x000BBF00 File Offset: 0x000BA100
		private static IEnumerable<AssociationEndMember> GetAllTargetEnds(EntityType ownerEntityType, EntitySet ownerEntitySet)
		{
			foreach (AssociationSet assocSet in ownerEntitySet.AssociationSets)
			{
				if (assocSet.ElementType.AssociationEndMembers[1].GetEntityType().IsAssignableFrom(ownerEntityType))
				{
					yield return assocSet.ElementType.AssociationEndMembers[0];
				}
				if (assocSet.ElementType.AssociationEndMembers[0].GetEntityType().IsAssignableFrom(ownerEntityType))
				{
					yield return assocSet.ElementType.AssociationEndMembers[1];
				}
				assocSet = null;
			}
			IEnumerator<AssociationSet> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x000BBF17 File Offset: 0x000BA117
		private IEnumerable<AssociationEndMember> GetAllTargetEnds(Type entityClrType)
		{
			ObjectItemCollection objectItemCollection = RelationshipManager.GetObjectItemCollection(this.WrappedOwner);
			IEnumerable<AssociationType> enumerable;
			if (objectItemCollection != null)
			{
				enumerable = objectItemCollection.GetItems<AssociationType>();
			}
			else
			{
				enumerable = EntityProxyFactory.TryGetAllAssociationTypesFromProxyInfo(this.WrappedOwner);
				if (enumerable == null)
				{
					enumerable = this._expensiveLoader.GetAllRelationshipTypesExpensiveWay(entityClrType.Assembly());
				}
			}
			foreach (AssociationType association in enumerable)
			{
				RefType refType = association.AssociationEndMembers[0].TypeUsage.EdmType as RefType;
				if (refType != null && refType.ElementType.ClrType.IsAssignableFrom(entityClrType))
				{
					yield return association.AssociationEndMembers[1];
				}
				refType = association.AssociationEndMembers[1].TypeUsage.EdmType as RefType;
				if (refType != null && refType.ElementType.ClrType.IsAssignableFrom(entityClrType))
				{
					yield return association.AssociationEndMembers[0];
				}
				association = null;
			}
			IEnumerator<AssociationType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x000BBF30 File Offset: 0x000BA130
		private bool VerifyRelationship(AssociationType relationship, string sourceEndName)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null)
			{
				return true;
			}
			EntityKey entityKey = wrappedOwner.EntityKey;
			return entityKey == null || RelationshipManager.VerifyRelationship(wrappedOwner, entityKey, relationship, sourceEndName);
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x000BBF6C File Offset: 0x000BA16C
		private bool VerifyRelationship(AssociationType osAssociationType, AssociationType csAssociationType, string sourceEndName)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (wrappedOwner.Context == null)
			{
				return true;
			}
			EntityKey entityKey = wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				return true;
			}
			if (osAssociationType.Index < 0)
			{
				return RelationshipManager.VerifyRelationship(wrappedOwner, entityKey, osAssociationType, sourceEndName);
			}
			EntitySet entitySet;
			if (wrappedOwner.Context.MetadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet(csAssociationType, sourceEndName, entityKey.EntitySetName, entityKey.EntityContainerName, out entitySet) == null)
			{
				throw Error.Collections_NoRelationshipSetMatched(osAssociationType.FullName);
			}
			return true;
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x000BBFE8 File Offset: 0x000BA1E8
		private static bool VerifyRelationship(IEntityWrapper wrappedOwner, EntityKey ownerKey, AssociationType relationship, string sourceEndName)
		{
			TypeUsage typeUsage;
			EntitySet entitySet;
			if (wrappedOwner.Context.Perspective.TryGetTypeByName(relationship.FullName, false, out typeUsage) && wrappedOwner.Context.MetadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet((AssociationType)typeUsage.EdmType, sourceEndName, ownerKey.EntitySetName, ownerKey.EntityContainerName, out entitySet) == null)
			{
				throw Error.Collections_NoRelationshipSetMatched(relationship.FullName);
			}
			return true;
		}

		// Token: 0x060038F2 RID: 14578 RVA: 0x000BC04E File Offset: 0x000BA24E
		public EntityCollection<TTargetEntity> GetRelatedCollection<TTargetEntity>(string relationshipName, string targetRoleName) where TTargetEntity : class
		{
			EntityCollection<TTargetEntity> entityCollection = this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName) as EntityCollection<TTargetEntity>;
			if (entityCollection == null)
			{
				throw new InvalidOperationException(Strings.Collections_ExpectedCollectionGotReference(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
			return entityCollection;
		}

		// Token: 0x060038F3 RID: 14579 RVA: 0x000BC082 File Offset: 0x000BA282
		public EntityReference<TTargetEntity> GetRelatedReference<TTargetEntity>(string relationshipName, string targetRoleName) where TTargetEntity : class
		{
			EntityReference<TTargetEntity> entityReference = this.GetRelatedEndInternal(this.PrependNamespaceToRelationshipName(relationshipName), targetRoleName) as EntityReference<TTargetEntity>;
			if (entityReference == null)
			{
				throw new InvalidOperationException(Strings.EntityReference_ExpectedReferenceGotCollection(typeof(TTargetEntity).Name, targetRoleName, relationshipName));
			}
			return entityReference;
		}

		// Token: 0x060038F4 RID: 14580 RVA: 0x000BC0B8 File Offset: 0x000BA2B8
		internal RelatedEnd GetRelatedEnd(RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(navigation.RelationshipName, navigation.To, out relatedEnd))
			{
				return relatedEnd;
			}
			relatedEnd = relationshipFixer.CreateSourceEnd(navigation, this);
			return relatedEnd;
		}

		// Token: 0x060038F5 RID: 14581 RVA: 0x000BC0E8 File Offset: 0x000BA2E8
		internal RelatedEnd CreateRelatedEnd<TSourceEntity, TTargetEntity>(RelationshipNavigation navigation, RelationshipMultiplicity sourceRoleMultiplicity, RelationshipMultiplicity targetRoleMultiplicity, RelatedEnd existingRelatedEnd) where TSourceEntity : class where TTargetEntity : class
		{
			IRelationshipFixer relationshipFixer = new RelationshipFixer<TSourceEntity, TTargetEntity>(sourceRoleMultiplicity, targetRoleMultiplicity);
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			RelatedEnd relatedEnd;
			if (targetRoleMultiplicity > RelationshipMultiplicity.One)
			{
				if (targetRoleMultiplicity != RelationshipMultiplicity.Many)
				{
					Type typeFromHandle = typeof(RelationshipMultiplicity);
					string name = typeFromHandle.Name;
					object name2 = typeFromHandle.Name;
					int num = (int)targetRoleMultiplicity;
					throw new ArgumentOutOfRangeException(name, Strings.ADP_InvalidEnumerationValue(name2, num.ToString(CultureInfo.InvariantCulture)));
				}
				if (existingRelatedEnd != null)
				{
					existingRelatedEnd.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
					relatedEnd = existingRelatedEnd;
				}
				else
				{
					relatedEnd = new EntityCollection<TTargetEntity>(wrappedOwner, navigation, relationshipFixer);
				}
			}
			else if (existingRelatedEnd != null)
			{
				existingRelatedEnd.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
				relatedEnd = existingRelatedEnd;
			}
			else
			{
				relatedEnd = new EntityReference<TTargetEntity>(wrappedOwner, navigation, relationshipFixer);
			}
			if (wrappedOwner.Context != null)
			{
				relatedEnd.AttachContext(wrappedOwner.Context, wrappedOwner.MergeOption);
			}
			this.EnsureRelationshipsInitialized();
			this._relationships.Add(relatedEnd);
			return relatedEnd;
		}

		// Token: 0x060038F6 RID: 14582 RVA: 0x000BC1AA File Offset: 0x000BA3AA
		public IEnumerable<IRelatedEnd> GetAllRelatedEnds()
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityType entityType;
			if (wrappedOwner.Context != null && wrappedOwner.Context.MetadataWorkspace != null && this.TryGetOwnerEntityType(out entityType))
			{
				EntitySet entitySet = wrappedOwner.Context.GetEntitySet(wrappedOwner.EntityKey.EntitySetName, wrappedOwner.EntityKey.EntityContainerName);
				foreach (AssociationEndMember associationEndMember in RelationshipManager.GetAllTargetEnds(entityType, entitySet))
				{
					yield return this.GetRelatedEnd(associationEndMember.DeclaringType.FullName, associationEndMember.Name);
				}
				IEnumerator<AssociationEndMember> enumerator = null;
			}
			else if (wrappedOwner.Entity != null)
			{
				foreach (AssociationEndMember associationEndMember2 in this.GetAllTargetEnds(wrappedOwner.IdentityType))
				{
					yield return this.GetRelatedEnd(associationEndMember2.DeclaringType.FullName, associationEndMember2.Name);
				}
				IEnumerator<AssociationEndMember> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060038F7 RID: 14583 RVA: 0x000BC1BC File Offset: 0x000BA3BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[OnSerializing]
		public void OnSerializing(StreamingContext context)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			if (!(wrappedOwner.Entity is IEntityWithRelationships))
			{
				throw new InvalidOperationException(Strings.RelatedEnd_CannotSerialize("RelationshipManager"));
			}
			if (wrappedOwner.Context != null && wrappedOwner.MergeOption != MergeOption.NoTracking)
			{
				foreach (IRelatedEnd relatedEnd in this.GetAllRelatedEnds())
				{
					EntityReference entityReference = ((RelatedEnd)relatedEnd) as EntityReference;
					if (entityReference != null && entityReference.EntityKey != null)
					{
						entityReference.DetachedEntityKey = entityReference.EntityKey;
					}
				}
			}
		}

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x060038F8 RID: 14584 RVA: 0x000BC260 File Offset: 0x000BA460
		internal bool HasRelationships
		{
			get
			{
				return this._relationships != null;
			}
		}

		// Token: 0x060038F9 RID: 14585 RVA: 0x000BC26C File Offset: 0x000BA46C
		internal void AddRelatedEntitiesToObjectStateManager(bool doAttach)
		{
			if (this._relationships != null)
			{
				bool flag = true;
				try
				{
					foreach (RelatedEnd relatedEnd in this.Relationships)
					{
						relatedEnd.Include(false, doAttach);
					}
					flag = false;
				}
				finally
				{
					if (flag)
					{
						IEntityWrapper wrappedOwner = this.WrappedOwner;
						TransactionManager transactionManager = wrappedOwner.Context.ObjectStateManager.TransactionManager;
						wrappedOwner.Context.ObjectStateManager.DegradePromotedRelationships();
						this.NodeVisited = true;
						RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(wrappedOwner);
						EntityEntry entityEntry;
						if (transactionManager.IsAttachTracking && transactionManager.PromotedKeyEntries.TryGetValue(wrappedOwner.Entity, out entityEntry))
						{
							entityEntry.DegradeEntry();
						}
						else
						{
							RelatedEnd.RemoveEntityFromObjectStateManager(wrappedOwner);
						}
					}
				}
			}
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x000BC33C File Offset: 0x000BA53C
		internal static void RemoveRelatedEntitiesFromObjectStateManager(IEntityWrapper wrappedEntity)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity.RelationshipManager.Relationships)
			{
				if (relatedEnd.ObjectContext != null)
				{
					relatedEnd.Exclude();
					relatedEnd.DetachContext();
				}
			}
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x000BC39C File Offset: 0x000BA59C
		internal void RemoveEntityFromRelationships()
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					relatedEnd.RemoveAll();
				}
			}
		}

		// Token: 0x060038FC RID: 14588 RVA: 0x000BC3F0 File Offset: 0x000BA5F0
		internal void NullAllFKsInDependentsForWhichThisIsThePrincipal()
		{
			if (this._relationships != null)
			{
				List<EntityReference> list = new List<EntityReference>();
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					if (relatedEnd.IsForeignKey)
					{
						foreach (IEntityWrapper entityWrapper in relatedEnd.GetWrappedEntities())
						{
							RelatedEnd otherEndOfRelationship = relatedEnd.GetOtherEndOfRelationship(entityWrapper);
							if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
							{
								list.Add((EntityReference)otherEndOfRelationship);
							}
						}
					}
				}
				foreach (EntityReference entityReference in list)
				{
					entityReference.NullAllForeignKeys();
				}
			}
		}

		// Token: 0x060038FD RID: 14589 RVA: 0x000BC4E4 File Offset: 0x000BA6E4
		internal void DetachEntityFromRelationships(EntityState ownerEntityState)
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					relatedEnd.DetachAll(ownerEntityState);
				}
			}
		}

		// Token: 0x060038FE RID: 14590 RVA: 0x000BC538 File Offset: 0x000BA738
		internal void RemoveEntity(string toRole, string relationshipName, IEntityWrapper wrappedEntity)
		{
			RelatedEnd relatedEnd;
			if (this.TryGetCachedRelatedEnd(relationshipName, toRole, out relatedEnd))
			{
				relatedEnd.Remove(wrappedEntity, false);
			}
		}

		// Token: 0x060038FF RID: 14591 RVA: 0x000BC55C File Offset: 0x000BA75C
		internal void ClearRelatedEndWrappers()
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					((RelatedEnd)relatedEnd).ClearWrappedValues();
				}
			}
		}

		// Token: 0x06003900 RID: 14592 RVA: 0x000BC5B4 File Offset: 0x000BA7B4
		internal void RetrieveReferentialConstraintProperties(out Dictionary<string, KeyValuePair<object, IntBox>> properties, HashSet<object> visited, bool includeOwnValues)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			properties = new Dictionary<string, KeyValuePair<object, IntBox>>();
			EntityKey entityKey = wrappedOwner.EntityKey;
			if (entityKey.IsTemporary)
			{
				List<string> list;
				bool flag;
				this.FindNamesOfReferentialConstraintProperties(out list, out flag, false);
				if (list != null)
				{
					if (this._relationships != null)
					{
						foreach (RelatedEnd relatedEnd in this._relationships)
						{
							relatedEnd.RetrieveReferentialConstraintProperties(properties, visited);
						}
					}
					if (!RelationshipManager.CheckIfAllPropertiesWereRetrieved(properties, list))
					{
						wrappedOwner.Context.ObjectStateManager.FindEntityEntry(entityKey).RetrieveReferentialConstraintPropertiesFromKeyEntries(properties);
						if (!RelationshipManager.CheckIfAllPropertiesWereRetrieved(properties, list))
						{
							throw new InvalidOperationException(Strings.RelationshipManager_UnableToRetrieveReferentialConstraintProperties);
						}
					}
				}
			}
			if (!entityKey.IsTemporary || includeOwnValues)
			{
				wrappedOwner.Context.ObjectStateManager.FindEntityEntry(entityKey).GetOtherKeyProperties(properties);
			}
		}

		// Token: 0x06003901 RID: 14593 RVA: 0x000BC69C File Offset: 0x000BA89C
		private static bool CheckIfAllPropertiesWereRetrieved(Dictionary<string, KeyValuePair<object, IntBox>> properties, List<string> propertiesToRetrieve)
		{
			bool flag = true;
			List<int> list = new List<int>();
			ICollection<KeyValuePair<object, IntBox>> values = properties.Values;
			foreach (KeyValuePair<object, IntBox> keyValuePair in values)
			{
				list.Add(keyValuePair.Value.Value);
			}
			foreach (string text in propertiesToRetrieve)
			{
				if (!properties.ContainsKey(text))
				{
					flag = false;
					break;
				}
				KeyValuePair<object, IntBox> keyValuePair2 = properties[text];
				keyValuePair2.Value.Value = keyValuePair2.Value.Value - 1;
				if (keyValuePair2.Value.Value < 0)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				foreach (KeyValuePair<object, IntBox> keyValuePair3 in values)
				{
					if (keyValuePair3.Value.Value != 0)
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				IEnumerator<int> enumerator3 = list.GetEnumerator();
				foreach (KeyValuePair<object, IntBox> keyValuePair4 in values)
				{
					enumerator3.MoveNext();
					keyValuePair4.Value.Value = enumerator3.Current;
				}
			}
			return flag;
		}

		// Token: 0x06003902 RID: 14594 RVA: 0x000BC820 File Offset: 0x000BAA20
		internal void CheckReferentialConstraintProperties(EntityEntry ownerEntry)
		{
			if (this.HasReferentialConstraintPropertiesToCheck() && this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.CheckReferentialConstraintProperties(ownerEntry);
				}
			}
		}

		// Token: 0x06003903 RID: 14595 RVA: 0x000BC884 File Offset: 0x000BAA84
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
			this._entityWrapperFactory = new EntityWrapperFactory();
			this._expensiveLoader = new ExpensiveOSpaceLoader();
			this._wrappedOwner = this.EntityWrapperFactory.WrapEntityUsingContext(this._owner, null);
		}

		// Token: 0x06003904 RID: 14596 RVA: 0x000BC8B4 File Offset: 0x000BAAB4
		private bool TryGetCachedRelatedEnd(string relationshipName, string targetRoleName, out RelatedEnd relatedEnd)
		{
			relatedEnd = null;
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd2 in this._relationships)
				{
					RelationshipNavigation relationshipNavigation = relatedEnd2.RelationshipNavigation;
					if (relationshipNavigation.RelationshipName == relationshipName && relationshipNavigation.To == targetRoleName)
					{
						relatedEnd = relatedEnd2;
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003905 RID: 14597 RVA: 0x000BC938 File Offset: 0x000BAB38
		internal bool FindNamesOfReferentialConstraintProperties(out List<string> propertiesToRetrieve, out bool propertiesToPropagateExist, bool skipFK)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityKey entityKey = wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			propertiesToRetrieve = null;
			propertiesToPropagateExist = false;
			if (wrappedOwner.Context == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNullContext);
			}
			EntitySet entitySet = entityKey.GetEntitySet(wrappedOwner.Context.MetadataWorkspace);
			ReadOnlyCollection<AssociationSet> associationSets = entitySet.AssociationSets;
			bool flag = false;
			foreach (AssociationSet associationSet in associationSets)
			{
				if (skipFK && associationSet.ElementType.IsForeignKey)
				{
					flag = true;
				}
				else
				{
					foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
					{
						if (referentialConstraint.ToRole.TypeUsage.EdmType == entitySet.ElementType.GetReferenceType())
						{
							propertiesToRetrieve = propertiesToRetrieve ?? new List<string>();
							foreach (EdmProperty edmProperty in referentialConstraint.ToProperties)
							{
								propertiesToRetrieve.Add(edmProperty.Name);
							}
						}
						if (referentialConstraint.FromRole.TypeUsage.EdmType == entitySet.ElementType.GetReferenceType())
						{
							propertiesToPropagateExist = true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x06003906 RID: 14598 RVA: 0x000BCAC4 File Offset: 0x000BACC4
		internal bool HasReferentialConstraintPropertiesToCheck()
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			EntityKey entityKey = wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			if (wrappedOwner.Context == null)
			{
				throw new InvalidOperationException(Strings.RelationshipManager_UnexpectedNullContext);
			}
			EntitySet entitySet = entityKey.GetEntitySet(wrappedOwner.Context.MetadataWorkspace);
			foreach (AssociationSet associationSet in entitySet.AssociationSets)
			{
				foreach (ReferentialConstraint referentialConstraint in associationSet.ElementType.ReferentialConstraints)
				{
					if (referentialConstraint.ToRole.TypeUsage.EdmType == entitySet.ElementType.GetReferenceType())
					{
						return true;
					}
					if (referentialConstraint.FromRole.TypeUsage.EdmType == entitySet.ElementType.GetReferenceType())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06003907 RID: 14599 RVA: 0x000BCBD4 File Offset: 0x000BADD4
		internal bool IsOwner(IEntityWrapper wrappedEntity)
		{
			IEntityWrapper wrappedOwner = this.WrappedOwner;
			return wrappedEntity.Entity == wrappedOwner.Entity;
		}

		// Token: 0x06003908 RID: 14600 RVA: 0x000BCBF8 File Offset: 0x000BADF8
		internal void AttachContextToRelatedEnds(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					EdmType edmType;
					RelationshipSet relationshipSet;
					relatedEnd.FindRelationshipSet(context, entitySet, out edmType, out relationshipSet);
					if (relationshipSet != null || !relatedEnd.IsEmpty())
					{
						relatedEnd.AttachContext(context, entitySet, mergeOption);
					}
					else
					{
						this._relationships.Remove(relatedEnd);
					}
				}
			}
		}

		// Token: 0x06003909 RID: 14601 RVA: 0x000BCC78 File Offset: 0x000BAE78
		internal void ResetContextOnRelatedEnds(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this.Relationships)
				{
					relatedEnd.AttachContext(context, entitySet, mergeOption);
					foreach (IEntityWrapper entityWrapper in relatedEnd.GetWrappedEntities())
					{
						entityWrapper.ResetContext(context, relatedEnd.GetTargetEntitySetFromRelationshipSet(), mergeOption);
					}
				}
			}
		}

		// Token: 0x0600390A RID: 14602 RVA: 0x000BCD10 File Offset: 0x000BAF10
		internal void DetachContextFromRelatedEnds()
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.DetachContext();
				}
			}
		}

		// Token: 0x0600390B RID: 14603 RVA: 0x000BCD68 File Offset: 0x000BAF68
		[Conditional("DEBUG")]
		internal void VerifyIsNotRelated()
		{
			if (this._relationships != null)
			{
				foreach (RelatedEnd relatedEnd in this._relationships)
				{
					relatedEnd.IsEmpty();
				}
			}
		}

		// Token: 0x040012F9 RID: 4857
		private IEntityWithRelationships _owner;

		// Token: 0x040012FA RID: 4858
		private List<RelatedEnd> _relationships;

		// Token: 0x040012FB RID: 4859
		[NonSerialized]
		private bool _nodeVisited;

		// Token: 0x040012FC RID: 4860
		[NonSerialized]
		private IEntityWrapper _wrappedOwner;

		// Token: 0x040012FD RID: 4861
		[NonSerialized]
		private EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x040012FE RID: 4862
		[NonSerialized]
		private ExpensiveOSpaceLoader _expensiveLoader;
	}
}
