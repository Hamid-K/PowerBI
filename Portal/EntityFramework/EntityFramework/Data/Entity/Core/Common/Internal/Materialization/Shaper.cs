using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.Resources;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x0200063E RID: 1598
	internal abstract class Shaper
	{
		// Token: 0x06004CD5 RID: 19669 RVA: 0x0010F404 File Offset: 0x0010D604
		internal Shaper(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, int stateCount, bool streaming)
		{
			this.Reader = reader;
			this.MergeOption = mergeOption;
			this.State = new object[stateCount];
			this.Context = context;
			this.Workspace = workspace;
			this._spatialReader = new Lazy<DbSpatialDataReader>(new Func<DbSpatialDataReader>(this.CreateSpatialDataReader));
			this.Streaming = streaming;
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x0010F464 File Offset: 0x0010D664
		public TElement Discriminate<TElement>(object[] discriminatorValues, Func<object[], EntityType> discriminate, KeyValuePair<EntityType, Func<Shaper, TElement>>[] elementDelegates)
		{
			EntityType entityType = discriminate(discriminatorValues);
			Func<Shaper, TElement> func = null;
			foreach (KeyValuePair<EntityType, Func<Shaper, TElement>> keyValuePair in elementDelegates)
			{
				if (keyValuePair.Key == entityType)
				{
					func = keyValuePair.Value;
				}
			}
			return func(this);
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x0010F4AD File Offset: 0x0010D6AD
		public IEntityWrapper HandleEntityNoTracking<TEntity>(IEntityWrapper wrappedEntity)
		{
			this.RegisterMaterializedEntityForEvent(wrappedEntity);
			return wrappedEntity;
		}

		// Token: 0x06004CD8 RID: 19672 RVA: 0x0010F4B8 File Offset: 0x0010D6B8
		public IEntityWrapper HandleEntity<TEntity>(IEntityWrapper wrappedEntity, EntityKey entityKey, EntitySet entitySet)
		{
			IEntityWrapper entityWrapper = wrappedEntity;
			if (entityKey != null)
			{
				EntityEntry entityEntry = this.Context.ObjectStateManager.FindEntityEntry(entityKey);
				if (entityEntry != null && !entityEntry.IsKeyEntry)
				{
					this.UpdateEntry<TEntity>(wrappedEntity, entityEntry);
					entityWrapper = entityEntry.WrappedEntity;
				}
				else
				{
					this.RegisterMaterializedEntityForEvent(entityWrapper);
					if (entityEntry == null)
					{
						this.Context.ObjectStateManager.AddEntry(wrappedEntity, entityKey, entitySet, "HandleEntity", false);
					}
					else
					{
						this.Context.ObjectStateManager.PromoteKeyEntry(entityEntry, wrappedEntity, false, true, false);
					}
				}
			}
			return entityWrapper;
		}

		// Token: 0x06004CD9 RID: 19673 RVA: 0x0010F534 File Offset: 0x0010D734
		public IEntityWrapper HandleEntityAppendOnly<TEntity>(Func<Shaper, IEntityWrapper> constructEntityDelegate, EntityKey entityKey, EntitySet entitySet)
		{
			IEntityWrapper entityWrapper;
			if (entityKey == null)
			{
				entityWrapper = constructEntityDelegate(this);
				this.RegisterMaterializedEntityForEvent(entityWrapper);
			}
			else
			{
				EntityEntry entityEntry = this.Context.ObjectStateManager.FindEntityEntry(entityKey);
				if (entityEntry != null && !entityEntry.IsKeyEntry)
				{
					if (typeof(TEntity) != entityEntry.WrappedEntity.IdentityType)
					{
						EntityKey entityKey2 = entityEntry.EntityKey;
						throw new NotSupportedException(Strings.Materializer_RecyclingEntity(TypeHelpers.GetFullName(entityKey2.EntityContainerName, entityKey2.EntitySetName), typeof(TEntity).FullName, entityEntry.WrappedEntity.IdentityType.FullName));
					}
					if (EntityState.Added == entityEntry.State)
					{
						throw new InvalidOperationException(Strings.Materializer_AddedEntityAlreadyExists(typeof(TEntity).FullName));
					}
					entityWrapper = entityEntry.WrappedEntity;
				}
				else
				{
					entityWrapper = constructEntityDelegate(this);
					this.RegisterMaterializedEntityForEvent(entityWrapper);
					if (entityEntry == null)
					{
						this.Context.ObjectStateManager.AddEntry(entityWrapper, entityKey, entitySet, "HandleEntity", false);
					}
					else
					{
						this.Context.ObjectStateManager.PromoteKeyEntry(entityEntry, entityWrapper, false, true, false);
					}
				}
			}
			return entityWrapper;
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x0010F64C File Offset: 0x0010D84C
		public IEntityWrapper HandleFullSpanCollection<TTargetEntity>(IEntityWrapper wrappedEntity, Coordinator<TTargetEntity> coordinator, AssociationEndMember targetMember)
		{
			if (wrappedEntity.Entity != null)
			{
				coordinator.RegisterCloseHandler(delegate(Shaper state, List<IEntityWrapper> spannedEntities)
				{
					this.FullSpanAction<IEntityWrapper>(wrappedEntity, spannedEntities, targetMember);
				});
			}
			return wrappedEntity;
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x0010F69C File Offset: 0x0010D89C
		public IEntityWrapper HandleFullSpanElement(IEntityWrapper wrappedSource, IEntityWrapper wrappedSpannedEntity, AssociationEndMember targetMember)
		{
			if (wrappedSource.Entity == null)
			{
				return wrappedSource;
			}
			List<IEntityWrapper> list = null;
			if (wrappedSpannedEntity.Entity != null)
			{
				list = new List<IEntityWrapper>(1);
				list.Add(wrappedSpannedEntity);
			}
			else
			{
				EntityKey entityKey = wrappedSource.EntityKey;
				this.CheckClearedEntryOnSpan(null, wrappedSource, entityKey, targetMember);
			}
			this.FullSpanAction<IEntityWrapper>(wrappedSource, list, targetMember);
			return wrappedSource;
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x0010F6E8 File Offset: 0x0010D8E8
		public IEntityWrapper HandleRelationshipSpan(IEntityWrapper wrappedEntity, EntityKey targetKey, AssociationEndMember targetMember)
		{
			if (wrappedEntity.Entity == null)
			{
				return wrappedEntity;
			}
			EntityKey entityKey = wrappedEntity.EntityKey;
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
			this.CheckClearedEntryOnSpan(targetKey, wrappedEntity, entityKey, targetMember);
			RelatedEnd relatedEnd;
			if (targetKey != null)
			{
				EntitySet entitySet;
				AssociationSet associationSet = this.Context.MetadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet((AssociationType)targetMember.DeclaringType, targetMember.Name, targetKey.EntitySetName, targetKey.EntityContainerName, out entitySet);
				ObjectStateManager objectStateManager = this.Context.ObjectStateManager;
				ILookup<EntityKey, RelationshipEntry> relationshipLookup = ObjectStateManager.GetRelationshipLookup(this.Context.ObjectStateManager, associationSet, otherAssociationEnd, entityKey);
				EntityState entityState;
				if (!ObjectStateManager.TryUpdateExistingRelationships(this.Context, this.MergeOption, associationSet, otherAssociationEnd, relationshipLookup, wrappedEntity, targetMember, targetKey, true, out entityState))
				{
					EntityEntry entityEntry = objectStateManager.GetOrAddKeyEntry(targetKey, entitySet);
					bool flag = true;
					RelationshipMultiplicity relationshipMultiplicity = otherAssociationEnd.RelationshipMultiplicity;
					if (relationshipMultiplicity > RelationshipMultiplicity.One)
					{
						if (relationshipMultiplicity != RelationshipMultiplicity.Many)
						{
						}
					}
					else
					{
						ILookup<EntityKey, RelationshipEntry> relationshipLookup2 = ObjectStateManager.GetRelationshipLookup(this.Context.ObjectStateManager, associationSet, targetMember, targetKey);
						flag = !ObjectStateManager.TryUpdateExistingRelationships(this.Context, this.MergeOption, associationSet, targetMember, relationshipLookup2, entityEntry.WrappedEntity, otherAssociationEnd, entityKey, true, out entityState);
						if (entityEntry.State == EntityState.Detached)
						{
							entityEntry = objectStateManager.AddKeyEntry(targetKey, entitySet);
						}
					}
					if (flag)
					{
						if (entityEntry.IsKeyEntry || entityState == EntityState.Deleted)
						{
							RelationshipWrapper relationshipWrapper = new RelationshipWrapper(associationSet, otherAssociationEnd.Name, entityKey, targetMember.Name, targetKey);
							objectStateManager.AddNewRelation(relationshipWrapper, entityState);
						}
						else if (entityEntry.State != EntityState.Deleted)
						{
							ObjectStateManager.AddEntityToCollectionOrReference(this.MergeOption, wrappedEntity, otherAssociationEnd, entityEntry.WrappedEntity, targetMember, true, false, false);
						}
						else
						{
							RelationshipWrapper relationshipWrapper2 = new RelationshipWrapper(associationSet, otherAssociationEnd.Name, entityKey, targetMember.Name, targetKey);
							objectStateManager.AddNewRelation(relationshipWrapper2, EntityState.Deleted);
						}
					}
				}
			}
			else if (this.TryGetRelatedEnd(wrappedEntity, (AssociationType)targetMember.DeclaringType, otherAssociationEnd.Name, targetMember.Name, out relatedEnd))
			{
				this.SetIsLoadedForSpan(relatedEnd, false);
			}
			return wrappedEntity;
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x0010F8B4 File Offset: 0x0010DAB4
		private bool TryGetRelatedEnd(IEntityWrapper wrappedEntity, AssociationType associationType, string sourceEndName, string targetEndName, out RelatedEnd relatedEnd)
		{
			AssociationType ospaceAssociationType = this.Workspace.MetadataOptimization.GetOSpaceAssociationType(associationType, () => this.Workspace.GetItemCollection(DataSpace.OSpace).GetItem<AssociationType>(associationType.FullName));
			AssociationEndMember associationEndMember = null;
			AssociationEndMember associationEndMember2 = null;
			foreach (AssociationEndMember associationEndMember3 in ospaceAssociationType.AssociationEndMembers)
			{
				if (associationEndMember3.Name == sourceEndName)
				{
					associationEndMember = associationEndMember3;
				}
				else if (associationEndMember3.Name == targetEndName)
				{
					associationEndMember2 = associationEndMember3;
				}
			}
			if (associationEndMember != null && associationEndMember2 != null)
			{
				bool flag = false;
				EntitySet entitySet;
				if (wrappedEntity.EntityKey == null)
				{
					flag = true;
				}
				else if (this.Workspace.MetadataOptimization.FindCSpaceAssociationSet(associationType, sourceEndName, wrappedEntity.EntityKey.EntitySetName, wrappedEntity.EntityKey.EntityContainerName, out entitySet) != null)
				{
					flag = true;
				}
				if (flag)
				{
					relatedEnd = DelegateFactory.GetRelatedEnd(wrappedEntity.RelationshipManager, associationEndMember, associationEndMember2, null);
					return true;
				}
			}
			relatedEnd = null;
			return false;
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x0010F9D0 File Offset: 0x0010DBD0
		private void SetIsLoadedForSpan(RelatedEnd relatedEnd, bool forceToTrue)
		{
			if (!forceToTrue)
			{
				forceToTrue = relatedEnd.IsEmpty();
				EntityReference entityReference = relatedEnd as EntityReference;
				if (entityReference != null)
				{
					forceToTrue &= entityReference.EntityKey == null;
				}
			}
			if (forceToTrue || this.MergeOption == MergeOption.OverwriteChanges)
			{
				relatedEnd.IsLoaded = true;
			}
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x0010FA15 File Offset: 0x0010DC15
		public IEntityWrapper HandleIEntityWithKey<TEntity>(IEntityWrapper wrappedEntity, EntitySet entitySet)
		{
			return this.HandleEntity<TEntity>(wrappedEntity, wrappedEntity.EntityKey, entitySet);
		}

		// Token: 0x06004CE0 RID: 19680 RVA: 0x0010FA25 File Offset: 0x0010DC25
		public bool SetColumnValue(int recordStateSlotNumber, int ordinal, object value)
		{
			((RecordState)this.State[recordStateSlotNumber]).SetColumnValue(ordinal, value);
			return true;
		}

		// Token: 0x06004CE1 RID: 19681 RVA: 0x0010FA3D File Offset: 0x0010DC3D
		public bool SetEntityRecordInfo(int recordStateSlotNumber, EntityKey entityKey, EntitySet entitySet)
		{
			((RecordState)this.State[recordStateSlotNumber]).SetEntityRecordInfo(entityKey, entitySet);
			return true;
		}

		// Token: 0x06004CE2 RID: 19682 RVA: 0x0010FA55 File Offset: 0x0010DC55
		public bool SetState<T>(int ordinal, T value)
		{
			this.State[ordinal] = value;
			return true;
		}

		// Token: 0x06004CE3 RID: 19683 RVA: 0x0010FA66 File Offset: 0x0010DC66
		public T SetStatePassthrough<T>(int ordinal, T value)
		{
			this.State[ordinal] = value;
			return value;
		}

		// Token: 0x06004CE4 RID: 19684 RVA: 0x0010FA77 File Offset: 0x0010DC77
		public TProperty GetPropertyValueWithErrorHandling<TProperty>(int ordinal, string propertyName, string typeName)
		{
			return new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName).GetValue(this.Reader, ordinal);
		}

		// Token: 0x06004CE5 RID: 19685 RVA: 0x0010FA8C File Offset: 0x0010DC8C
		public TColumn GetColumnValueWithErrorHandling<TColumn>(int ordinal)
		{
			return new Shaper.ColumnErrorHandlingValueReader<TColumn>().GetValue(this.Reader, ordinal);
		}

		// Token: 0x06004CE6 RID: 19686 RVA: 0x0010FA9F File Offset: 0x0010DC9F
		public HierarchyId GetHierarchyIdColumnValue(int ordinal)
		{
			return new HierarchyId(this.Reader.GetValue(ordinal).ToString());
		}

		// Token: 0x06004CE7 RID: 19687 RVA: 0x0010FAB7 File Offset: 0x0010DCB7
		protected virtual DbSpatialDataReader CreateSpatialDataReader()
		{
			return SpatialHelpers.CreateSpatialDataReader(this.Workspace, this.Reader);
		}

		// Token: 0x06004CE8 RID: 19688 RVA: 0x0010FACA File Offset: 0x0010DCCA
		public DbGeography GetGeographyColumnValue(int ordinal)
		{
			if (this.Streaming)
			{
				return this._spatialReader.Value.GetGeography(ordinal);
			}
			return (DbGeography)this.Reader.GetValue(ordinal);
		}

		// Token: 0x06004CE9 RID: 19689 RVA: 0x0010FAF7 File Offset: 0x0010DCF7
		public DbGeometry GetGeometryColumnValue(int ordinal)
		{
			if (this.Streaming)
			{
				return this._spatialReader.Value.GetGeometry(ordinal);
			}
			return (DbGeometry)this.Reader.GetValue(ordinal);
		}

		// Token: 0x06004CEA RID: 19690 RVA: 0x0010FB24 File Offset: 0x0010DD24
		public TColumn GetSpatialColumnValueWithErrorHandling<TColumn>(int ordinal, PrimitiveTypeKind spatialTypeKind)
		{
			TColumn tcolumn;
			if (spatialTypeKind == PrimitiveTypeKind.Geography)
			{
				if (this.Streaming)
				{
					tcolumn = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this._spatialReader.Value.GetGeography(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeography(column)).GetValue(this.Reader, ordinal);
				}
				else
				{
					tcolumn = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
				}
			}
			else if (this.Streaming)
			{
				tcolumn = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this._spatialReader.Value.GetGeometry(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeometry(column)).GetValue(this.Reader, ordinal);
			}
			else
			{
				tcolumn = new Shaper.ColumnErrorHandlingValueReader<TColumn>((DbDataReader reader, int column) => (TColumn)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
			}
			return tcolumn;
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0010FBF8 File Offset: 0x0010DDF8
		public TProperty GetSpatialPropertyValueWithErrorHandling<TProperty>(int ordinal, string propertyName, string typeName, PrimitiveTypeKind spatialTypeKind)
		{
			TProperty tproperty;
			if (Helper.IsGeographicTypeKind(spatialTypeKind))
			{
				if (this.Streaming)
				{
					tproperty = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this._spatialReader.Value.GetGeography(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeography(column)).GetValue(this.Reader, ordinal);
				}
				else
				{
					tproperty = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
				}
			}
			else if (this.Streaming)
			{
				tproperty = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this._spatialReader.Value.GetGeometry(column)), (DbDataReader reader, int column) => this._spatialReader.Value.GetGeometry(column)).GetValue(this.Reader, ordinal);
			}
			else
			{
				tproperty = new Shaper.PropertyErrorHandlingValueReader<TProperty>(propertyName, typeName, (DbDataReader reader, int column) => (TProperty)((object)this.Reader.GetValue(column)), (DbDataReader reader, int column) => this.Reader.GetValue(column)).GetValue(this.Reader, ordinal);
			}
			return tproperty;
		}

		// Token: 0x06004CEC RID: 19692 RVA: 0x0010FCD8 File Offset: 0x0010DED8
		private void CheckClearedEntryOnSpan(object targetValue, IEntityWrapper wrappedSource, EntityKey sourceKey, AssociationEndMember targetMember)
		{
			if (sourceKey != null && targetValue == null && (this.MergeOption == MergeOption.PreserveChanges || this.MergeOption == MergeOption.OverwriteChanges))
			{
				EdmType elementType = ((RefType)MetadataHelper.GetOtherAssociationEnd(targetMember).TypeUsage.EdmType).ElementType;
				TypeUsage typeUsage;
				if (!this.Context.Perspective.TryGetType(wrappedSource.IdentityType, out typeUsage) || typeUsage.EdmType.EdmEquals(elementType) || TypeSemantics.IsSubTypeOf(typeUsage.EdmType, elementType))
				{
					this.CheckClearedEntryOnSpan(sourceKey, targetMember);
				}
			}
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x0010FD58 File Offset: 0x0010DF58
		private void CheckClearedEntryOnSpan(EntityKey sourceKey, AssociationEndMember targetMember)
		{
			AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
			EntitySet entitySet;
			AssociationSet associationSet = this.Context.MetadataWorkspace.MetadataOptimization.FindCSpaceAssociationSet((AssociationType)otherAssociationEnd.DeclaringType, otherAssociationEnd.Name, sourceKey.EntitySetName, sourceKey.EntityContainerName, out entitySet);
			if (associationSet != null)
			{
				this.Context.ObjectStateManager.RemoveRelationships(this.MergeOption, associationSet, sourceKey, otherAssociationEnd);
			}
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0010FDC0 File Offset: 0x0010DFC0
		private void FullSpanAction<TTargetEntity>(IEntityWrapper wrappedSource, IList<TTargetEntity> spannedEntities, AssociationEndMember targetMember)
		{
			if (wrappedSource.Entity != null)
			{
				AssociationEndMember otherAssociationEnd = MetadataHelper.GetOtherAssociationEnd(targetMember);
				RelatedEnd relatedEnd;
				if (this.TryGetRelatedEnd(wrappedSource, (AssociationType)targetMember.DeclaringType, otherAssociationEnd.Name, targetMember.Name, out relatedEnd))
				{
					int num = this.Context.ObjectStateManager.UpdateRelationships(this.Context, this.MergeOption, (AssociationSet)relatedEnd.RelationshipSet, otherAssociationEnd, wrappedSource, targetMember, (List<TTargetEntity>)spannedEntities, true);
					this.SetIsLoadedForSpan(relatedEnd, num > 0);
				}
			}
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0010FE3C File Offset: 0x0010E03C
		private void UpdateEntry<TEntity>(IEntityWrapper wrappedEntity, EntityEntry existingEntry)
		{
			Type typeFromHandle = typeof(TEntity);
			if (typeFromHandle != existingEntry.WrappedEntity.IdentityType)
			{
				EntityKey entityKey = existingEntry.EntityKey;
				throw new NotSupportedException(Strings.Materializer_RecyclingEntity(TypeHelpers.GetFullName(entityKey.EntityContainerName, entityKey.EntitySetName), typeFromHandle.FullName, existingEntry.WrappedEntity.IdentityType.FullName));
			}
			if (EntityState.Added == existingEntry.State)
			{
				throw new InvalidOperationException(Strings.Materializer_AddedEntityAlreadyExists(typeFromHandle.FullName));
			}
			if (this.MergeOption != MergeOption.AppendOnly)
			{
				if (MergeOption.OverwriteChanges == this.MergeOption)
				{
					if (EntityState.Deleted == existingEntry.State)
					{
						existingEntry.RevertDelete();
					}
					existingEntry.UpdateCurrentValueRecord(wrappedEntity.Entity);
					this.Context.ObjectStateManager.ForgetEntryWithConceptualNull(existingEntry, true);
					existingEntry.AcceptChanges();
					this.Context.ObjectStateManager.FixupReferencesByForeignKeys(existingEntry, true);
					return;
				}
				if (EntityState.Unchanged == existingEntry.State)
				{
					existingEntry.UpdateCurrentValueRecord(wrappedEntity.Entity);
					this.Context.ObjectStateManager.ForgetEntryWithConceptualNull(existingEntry, true);
					existingEntry.AcceptChanges();
					this.Context.ObjectStateManager.FixupReferencesByForeignKeys(existingEntry, true);
					return;
				}
				if (this.Context.ContextOptions.UseLegacyPreserveChangesBehavior)
				{
					existingEntry.UpdateRecordWithoutSetModified(wrappedEntity.Entity, existingEntry.EditableOriginalValues);
					return;
				}
				existingEntry.UpdateRecordWithSetModified(wrappedEntity.Entity, existingEntry.EditableOriginalValues);
			}
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x0010FF8C File Offset: 0x0010E18C
		public void RaiseMaterializedEvents()
		{
			if (this._materializedEntities != null)
			{
				foreach (IEntityWrapper entityWrapper in this._materializedEntities)
				{
					this.Context.OnObjectMaterialized(entityWrapper.Entity);
				}
				this._materializedEntities.Clear();
			}
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x0010FFF8 File Offset: 0x0010E1F8
		public void InitializeForOnMaterialize()
		{
			if (this.Context.OnMaterializedHasHandlers)
			{
				if (this._materializedEntities == null)
				{
					this._materializedEntities = new List<IEntityWrapper>();
					return;
				}
			}
			else if (this._materializedEntities != null)
			{
				this._materializedEntities = null;
			}
		}

		// Token: 0x06004CF2 RID: 19698 RVA: 0x0011002A File Offset: 0x0010E22A
		protected void RegisterMaterializedEntityForEvent(IEntityWrapper wrappedEntity)
		{
			if (this._materializedEntities != null)
			{
				this._materializedEntities.Add(wrappedEntity);
			}
		}

		// Token: 0x04001B51 RID: 6993
		private IList<IEntityWrapper> _materializedEntities;

		// Token: 0x04001B52 RID: 6994
		public readonly DbDataReader Reader;

		// Token: 0x04001B53 RID: 6995
		public readonly object[] State;

		// Token: 0x04001B54 RID: 6996
		public readonly ObjectContext Context;

		// Token: 0x04001B55 RID: 6997
		public readonly MetadataWorkspace Workspace;

		// Token: 0x04001B56 RID: 6998
		public readonly MergeOption MergeOption;

		// Token: 0x04001B57 RID: 6999
		protected readonly bool Streaming;

		// Token: 0x04001B58 RID: 7000
		private readonly Lazy<DbSpatialDataReader> _spatialReader;

		// Token: 0x02000C5E RID: 3166
		internal abstract class ErrorHandlingValueReader<T>
		{
			// Token: 0x06006AAF RID: 27311 RVA: 0x0016C570 File Offset: 0x0016A770
			protected ErrorHandlingValueReader(Func<DbDataReader, int, T> typedValueAccessor, Func<DbDataReader, int, object> untypedValueAccessor)
			{
				this.getTypedValue = typedValueAccessor;
				this.getUntypedValue = untypedValueAccessor;
			}

			// Token: 0x06006AB0 RID: 27312 RVA: 0x0016C586 File Offset: 0x0016A786
			protected ErrorHandlingValueReader()
				: this(new Func<DbDataReader, int, T>(Shaper.ErrorHandlingValueReader<T>.GetTypedValueDefault), new Func<DbDataReader, int, object>(Shaper.ErrorHandlingValueReader<T>.GetUntypedValueDefault))
			{
			}

			// Token: 0x06006AB1 RID: 27313 RVA: 0x0016C5A8 File Offset: 0x0016A7A8
			private static T GetTypedValueDefault(DbDataReader reader, int ordinal)
			{
				Type underlyingType = Nullable.GetUnderlyingType(typeof(T));
				if (underlyingType != null && underlyingType.IsEnum())
				{
					return (T)((object)Shaper.ErrorHandlingValueReader<T>.GetGenericTypedValueDefaultMethod(underlyingType).Invoke(null, new object[] { reader, ordinal }));
				}
				bool flag;
				return (T)((object)CodeGenEmitter.GetReaderMethod(typeof(T), out flag).Invoke(reader, new object[] { ordinal }));
			}

			// Token: 0x06006AB2 RID: 27314 RVA: 0x0016C626 File Offset: 0x0016A826
			public static MethodInfo GetGenericTypedValueDefaultMethod(Type underlyingType)
			{
				return typeof(Shaper.ErrorHandlingValueReader<>).MakeGenericType(new Type[] { underlyingType }).GetOnlyDeclaredMethod("GetTypedValueDefault");
			}

			// Token: 0x06006AB3 RID: 27315 RVA: 0x0016C64B File Offset: 0x0016A84B
			private static object GetUntypedValueDefault(DbDataReader reader, int ordinal)
			{
				return reader.GetValue(ordinal);
			}

			// Token: 0x06006AB4 RID: 27316 RVA: 0x0016C654 File Offset: 0x0016A854
			internal T GetValue(DbDataReader reader, int ordinal)
			{
				if (reader.IsDBNull(ordinal))
				{
					try
					{
						return (T)((object)null);
					}
					catch (NullReferenceException)
					{
						throw this.CreateNullValueException();
					}
				}
				T t;
				try
				{
					t = this.getTypedValue(reader, ordinal);
				}
				catch (Exception ex)
				{
					if (ex.IsCatchableExceptionType())
					{
						object obj = this.getUntypedValue(reader, ordinal);
						Type type = ((obj == null) ? null : obj.GetType());
						if (!typeof(T).IsAssignableFrom(type))
						{
							throw this.CreateWrongTypeException(type);
						}
					}
					throw;
				}
				return t;
			}

			// Token: 0x06006AB5 RID: 27317
			protected abstract Exception CreateNullValueException();

			// Token: 0x06006AB6 RID: 27318
			protected abstract Exception CreateWrongTypeException(Type resultType);

			// Token: 0x040030E9 RID: 12521
			private readonly Func<DbDataReader, int, T> getTypedValue;

			// Token: 0x040030EA RID: 12522
			private readonly Func<DbDataReader, int, object> getUntypedValue;
		}

		// Token: 0x02000C5F RID: 3167
		private class ColumnErrorHandlingValueReader<TColumn> : Shaper.ErrorHandlingValueReader<TColumn>
		{
			// Token: 0x06006AB7 RID: 27319 RVA: 0x0016C6E8 File Offset: 0x0016A8E8
			internal ColumnErrorHandlingValueReader()
			{
			}

			// Token: 0x06006AB8 RID: 27320 RVA: 0x0016C6F0 File Offset: 0x0016A8F0
			internal ColumnErrorHandlingValueReader(Func<DbDataReader, int, TColumn> typedAccessor, Func<DbDataReader, int, object> untypedAccessor)
				: base(typedAccessor, untypedAccessor)
			{
			}

			// Token: 0x06006AB9 RID: 27321 RVA: 0x0016C6FA File Offset: 0x0016A8FA
			protected override Exception CreateNullValueException()
			{
				return new InvalidOperationException(Strings.Materializer_NullReferenceCast(typeof(TColumn)));
			}

			// Token: 0x06006ABA RID: 27322 RVA: 0x0016C710 File Offset: 0x0016A910
			protected override Exception CreateWrongTypeException(Type resultType)
			{
				return EntityUtil.ValueInvalidCast(resultType, typeof(TColumn));
			}
		}

		// Token: 0x02000C60 RID: 3168
		private class PropertyErrorHandlingValueReader<TProperty> : Shaper.ErrorHandlingValueReader<TProperty>
		{
			// Token: 0x06006ABB RID: 27323 RVA: 0x0016C722 File Offset: 0x0016A922
			internal PropertyErrorHandlingValueReader(string propertyName, string typeName)
			{
				this._propertyName = propertyName;
				this._typeName = typeName;
			}

			// Token: 0x06006ABC RID: 27324 RVA: 0x0016C738 File Offset: 0x0016A938
			internal PropertyErrorHandlingValueReader(string propertyName, string typeName, Func<DbDataReader, int, TProperty> typedAccessor, Func<DbDataReader, int, object> untypedAccessor)
				: base(typedAccessor, untypedAccessor)
			{
				this._propertyName = propertyName;
				this._typeName = typeName;
			}

			// Token: 0x06006ABD RID: 27325 RVA: 0x0016C751 File Offset: 0x0016A951
			protected override Exception CreateNullValueException()
			{
				return new ConstraintException(Strings.Materializer_SetInvalidValue(Nullable.GetUnderlyingType(typeof(TProperty)) ?? typeof(TProperty), this._typeName, this._propertyName, "null"));
			}

			// Token: 0x06006ABE RID: 27326 RVA: 0x0016C78B File Offset: 0x0016A98B
			protected override Exception CreateWrongTypeException(Type resultType)
			{
				return new InvalidOperationException(Strings.Materializer_SetInvalidValue(Nullable.GetUnderlyingType(typeof(TProperty)) ?? typeof(TProperty), this._typeName, this._propertyName, resultType));
			}

			// Token: 0x040030EB RID: 12523
			private readonly string _propertyName;

			// Token: 0x040030EC RID: 12524
			private readonly string _typeName;
		}
	}
}
