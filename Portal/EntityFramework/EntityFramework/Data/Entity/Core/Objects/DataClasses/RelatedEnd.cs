using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200047F RID: 1151
	[DataContract]
	[Serializable]
	public abstract class RelatedEnd : IRelatedEnd
	{
		// Token: 0x06003847 RID: 14407 RVA: 0x000B89F1 File Offset: 0x000B6BF1
		internal RelatedEnd()
		{
			this._wrappedOwner = NullEntityWrapper.NullWrapper;
		}

		// Token: 0x06003848 RID: 14408 RVA: 0x000B8A04 File Offset: 0x000B6C04
		internal RelatedEnd(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			this.InitializeRelatedEnd(wrappedOwner, navigation, relationshipFixer);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06003849 RID: 14409 RVA: 0x000B8A15 File Offset: 0x000B6C15
		// (remove) Token: 0x0600384A RID: 14410 RVA: 0x000B8A34 File Offset: 0x000B6C34
		public event CollectionChangeEventHandler AssociationChanged
		{
			add
			{
				this.CheckOwnerNull();
				this._onAssociationChanged = (CollectionChangeEventHandler)Delegate.Combine(this._onAssociationChanged, value);
			}
			remove
			{
				this.CheckOwnerNull();
				this._onAssociationChanged = (CollectionChangeEventHandler)Delegate.Remove(this._onAssociationChanged, value);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600384B RID: 14411 RVA: 0x000B8A53 File Offset: 0x000B6C53
		// (remove) Token: 0x0600384C RID: 14412 RVA: 0x000B8A55 File Offset: 0x000B6C55
		internal virtual event CollectionChangeEventHandler AssociationChangedForObjectView
		{
			add
			{
			}
			remove
			{
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x0600384D RID: 14413 RVA: 0x000B8A57 File Offset: 0x000B6C57
		internal bool IsForeignKey
		{
			get
			{
				return ((AssociationType)this._relationMetadata).IsForeignKey;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x0600384E RID: 14414 RVA: 0x000B8A69 File Offset: 0x000B6C69
		internal RelationshipNavigation RelationshipNavigation
		{
			get
			{
				return this._navigation;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x0600384F RID: 14415 RVA: 0x000B8A71 File Offset: 0x000B6C71
		[SoapIgnore]
		[XmlIgnore]
		public string RelationshipName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.RelationshipName;
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06003850 RID: 14416 RVA: 0x000B8A84 File Offset: 0x000B6C84
		[SoapIgnore]
		[XmlIgnore]
		public virtual string SourceRoleName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.From;
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06003851 RID: 14417 RVA: 0x000B8A97 File Offset: 0x000B6C97
		[SoapIgnore]
		[XmlIgnore]
		public virtual string TargetRoleName
		{
			get
			{
				this.CheckOwnerNull();
				return this._navigation.To;
			}
		}

		// Token: 0x06003852 RID: 14418 RVA: 0x000B8AAA File Offset: 0x000B6CAA
		IEnumerable IRelatedEnd.CreateSourceQuery()
		{
			this.CheckOwnerNull();
			return this.CreateSourceQueryInternal();
		}

		// Token: 0x17000AD1 RID: 2769
		// (get) Token: 0x06003853 RID: 14419 RVA: 0x000B8AB8 File Offset: 0x000B6CB8
		internal virtual IEntityWrapper WrappedOwner
		{
			get
			{
				return this._wrappedOwner;
			}
		}

		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000B8AC0 File Offset: 0x000B6CC0
		internal virtual ObjectContext ObjectContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x06003855 RID: 14421 RVA: 0x000B8AC8 File Offset: 0x000B6CC8
		internal virtual EntityWrapperFactory EntityWrapperFactory
		{
			get
			{
				if (this._entityWrapperFactory == null)
				{
					this._entityWrapperFactory = new EntityWrapperFactory();
				}
				return this._entityWrapperFactory;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000B8AE3 File Offset: 0x000B6CE3
		[SoapIgnore]
		[XmlIgnore]
		public virtual RelationshipSet RelationshipSet
		{
			get
			{
				this.CheckOwnerNull();
				return this._relationshipSet;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06003857 RID: 14423 RVA: 0x000B8AF1 File Offset: 0x000B6CF1
		internal virtual RelationshipType RelationMetadata
		{
			get
			{
				return this._relationMetadata;
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06003858 RID: 14424 RVA: 0x000B8AF9 File Offset: 0x000B6CF9
		internal virtual RelationshipEndMember ToEndMember
		{
			get
			{
				return this._toEndMember;
			}
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06003859 RID: 14425 RVA: 0x000B8B01 File Offset: 0x000B6D01
		internal bool UsingNoTracking
		{
			get
			{
				return this._usingNoTracking;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x0600385A RID: 14426 RVA: 0x000B8B09 File Offset: 0x000B6D09
		internal MergeOption DefaultMergeOption
		{
			get
			{
				if (!this.UsingNoTracking)
				{
					return MergeOption.AppendOnly;
				}
				return MergeOption.NoTracking;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000B8B16 File Offset: 0x000B6D16
		internal virtual RelationshipEndMember FromEndMember
		{
			get
			{
				return this._fromEndMember;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x0600385C RID: 14428 RVA: 0x000B8B1E File Offset: 0x000B6D1E
		// (set) Token: 0x0600385D RID: 14429 RVA: 0x000B8B2C File Offset: 0x000B6D2C
		[SoapIgnore]
		[XmlIgnore]
		public bool IsLoaded
		{
			get
			{
				this.CheckOwnerNull();
				return this._isLoaded;
			}
			set
			{
				this.CheckOwnerNull();
				this._isLoaded = value;
			}
		}

		// Token: 0x0600385E RID: 14430 RVA: 0x000B8B3C File Offset: 0x000B6D3C
		internal ObjectQuery<TEntity> CreateSourceQuery<TEntity>(MergeOption mergeOption, out bool hasResults)
		{
			if (this._context == null)
			{
				hasResults = false;
				return null;
			}
			EntityEntry entityEntry = this._context.ObjectStateManager.FindEntityEntry(this._wrappedOwner.Entity);
			EntityState entityState;
			if (entityEntry == null)
			{
				if (!this.UsingNoTracking)
				{
					throw Error.Collections_InvalidEntityStateSource();
				}
				entityState = EntityState.Detached;
			}
			else
			{
				entityState = entityEntry.State;
			}
			if (entityState == EntityState.Added && (!this.IsForeignKey || !this.IsDependentEndOfReferentialConstraint(false)))
			{
				throw Error.Collections_InvalidEntityStateSource();
			}
			if ((entityState != EntityState.Detached || !this.UsingNoTracking) && entityState != EntityState.Modified && entityState != EntityState.Unchanged && entityState != EntityState.Deleted && entityState != EntityState.Added)
			{
				hasResults = false;
				return null;
			}
			if (this._sourceQuery == null)
			{
				this._sourceQuery = this.GenerateQueryText();
			}
			ObjectQuery<TEntity> objectQuery = new ObjectQuery<TEntity>(this._sourceQuery, this._context, mergeOption);
			hasResults = this.AddQueryParameters<TEntity>(objectQuery);
			objectQuery.Parameters.SetReadOnly(true);
			return objectQuery;
		}

		// Token: 0x0600385F RID: 14431 RVA: 0x000B8C08 File Offset: 0x000B6E08
		private string GenerateQueryText()
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			AssociationType associationType = (AssociationType)this._relationMetadata;
			EntitySet entitySet = ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._toEndMember.Name].EntitySet;
			EntityType entityType = MetadataHelper.GetEntityTypeForEnd((AssociationEndMember)this._toEndMember);
			bool flag = false;
			if (!entitySet.ElementType.EdmEquals(entityType) && !TypeSemantics.IsSubTypeOf(entitySet.ElementType, entityType))
			{
				flag = true;
				entityType = (EntityType)this.ObjectContext.MetadataWorkspace.GetOSpaceTypeUsage(TypeUsage.Create(entityType)).EdmType;
			}
			StringBuilder stringBuilder;
			if (associationType.IsForeignKey)
			{
				ReferentialConstraint referentialConstraint = associationType.ReferentialConstraints[0];
				ReadOnlyMetadataCollection<EdmProperty> fromProperties = referentialConstraint.FromProperties;
				ReadOnlyMetadataCollection<EdmProperty> toProperties = referentialConstraint.ToProperties;
				if (!referentialConstraint.ToRole.EdmEquals(this._toEndMember))
				{
					stringBuilder = new StringBuilder("SELECT VALUE P FROM ");
					RelatedEnd.AppendEntitySet(stringBuilder, entitySet, entityType, flag);
					stringBuilder.Append(" AS P WHERE ");
					AliasGenerator aliasGenerator = new AliasGenerator("EntityKeyValue");
					this._sourceQueryParamProperties = toProperties;
					for (int i = 0; i < fromProperties.Count; i++)
					{
						if (i > 0)
						{
							stringBuilder.Append(" AND ");
						}
						stringBuilder.Append("P.[");
						stringBuilder.Append(fromProperties[i].Name);
						stringBuilder.Append("] = @");
						stringBuilder.Append(aliasGenerator.Next());
					}
					return stringBuilder.ToString();
				}
				stringBuilder = new StringBuilder("SELECT VALUE D FROM ");
				RelatedEnd.AppendEntitySet(stringBuilder, entitySet, entityType, flag);
				stringBuilder.Append(" AS D WHERE ");
				AliasGenerator aliasGenerator2 = new AliasGenerator("EntityKeyValue");
				this._sourceQueryParamProperties = fromProperties;
				for (int j = 0; j < toProperties.Count; j++)
				{
					if (j > 0)
					{
						stringBuilder.Append(" AND ");
					}
					stringBuilder.Append("D.[");
					stringBuilder.Append(toProperties[j].Name);
					stringBuilder.Append("] = @");
					stringBuilder.Append(aliasGenerator2.Next());
				}
			}
			else
			{
				stringBuilder = new StringBuilder("SELECT VALUE [TargetEntity] FROM (SELECT VALUE x FROM ");
				stringBuilder.Append("[");
				stringBuilder.Append(this._relationshipSet.EntityContainer.Name);
				stringBuilder.Append("].[");
				stringBuilder.Append(this._relationshipSet.Name);
				stringBuilder.Append("] AS x WHERE Key(x.[");
				stringBuilder.Append(this._fromEndMember.Name);
				stringBuilder.Append("]) = ");
				RelatedEnd.AppendKeyParameterRow(stringBuilder, entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace).ElementType.KeyMembers);
				stringBuilder.Append(") AS [AssociationEntry] INNER JOIN ");
				RelatedEnd.AppendEntitySet(stringBuilder, entitySet, entityType, flag);
				stringBuilder.Append(" AS [TargetEntity] ON Key([AssociationEntry].[");
				stringBuilder.Append(this._toEndMember.Name);
				stringBuilder.Append("]) = Key(Ref([TargetEntity]))");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003860 RID: 14432 RVA: 0x000B8F2C File Offset: 0x000B712C
		private bool AddQueryParameters<TEntity>(ObjectQuery<TEntity> query)
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			bool flag = true;
			AliasGenerator aliasGenerator = new AliasGenerator("EntityKeyValue");
			using (IEnumerator<EdmMember> enumerator = (this._sourceQueryParamProperties ?? entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace).ElementType.KeyMembers).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EdmMember parameterMember = enumerator.Current;
					object obj;
					if (this._sourceQueryParamProperties == null)
					{
						obj = this._wrappedOwner.EntityKey.EntityKeyValues.Single((EntityKeyMember ekv) => ekv.Key == parameterMember.Name).Value;
					}
					else if (this.CachedForeignKeyIsConceptualNull())
					{
						obj = null;
					}
					else
					{
						obj = this.GetCurrentValueFromEntity(parameterMember);
					}
					ObjectParameter objectParameter;
					if (obj == null)
					{
						EdmType edmType = parameterMember.TypeUsage.EdmType;
						Type type = (Helper.IsPrimitiveType(edmType) ? ((PrimitiveType)edmType).ClrEquivalentType : this.ObjectContext.MetadataWorkspace.GetObjectSpaceType((EnumType)edmType).ClrType);
						objectParameter = new ObjectParameter(aliasGenerator.Next(), type);
						flag = false;
					}
					else
					{
						objectParameter = new ObjectParameter(aliasGenerator.Next(), obj);
					}
					objectParameter.TypeUsage = Helper.GetModelTypeUsage(parameterMember);
					query.Parameters.Add(objectParameter);
				}
			}
			return flag;
		}

		// Token: 0x06003861 RID: 14433 RVA: 0x000B90B8 File Offset: 0x000B72B8
		private object GetCurrentValueFromEntity(EdmMember member)
		{
			StateManagerTypeMetadata orAddStateManagerTypeMetadata = this._context.ObjectStateManager.GetOrAddStateManagerTypeMetadata(member.DeclaringType);
			return orAddStateManagerTypeMetadata.Member(orAddStateManagerTypeMetadata.GetOrdinalforCLayerMemberName(member.Name)).GetValue(this._wrappedOwner.Entity);
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000B90F4 File Offset: 0x000B72F4
		private static void AppendKeyParameterRow(StringBuilder sourceBuilder, IList<EdmMember> keyMembers)
		{
			sourceBuilder.Append("ROW(");
			AliasGenerator aliasGenerator = new AliasGenerator("EntityKeyValue");
			int count = keyMembers.Count;
			for (int i = 0; i < count; i++)
			{
				string text = aliasGenerator.Next();
				sourceBuilder.Append("@");
				sourceBuilder.Append(text);
				sourceBuilder.Append(" AS ");
				sourceBuilder.Append(text);
				if (i < count - 1)
				{
					sourceBuilder.Append(",");
				}
			}
			sourceBuilder.Append(")");
		}

		// Token: 0x06003863 RID: 14435 RVA: 0x000B9178 File Offset: 0x000B7378
		private static void AppendEntitySet(StringBuilder sourceBuilder, EntitySet targetEntitySet, EntityType targetEntityType, bool ofTypeRequired)
		{
			if (ofTypeRequired)
			{
				sourceBuilder.Append("OfType(");
			}
			sourceBuilder.Append("[");
			sourceBuilder.Append(targetEntitySet.EntityContainer.Name);
			sourceBuilder.Append("].[");
			sourceBuilder.Append(targetEntitySet.Name);
			sourceBuilder.Append("]");
			if (ofTypeRequired)
			{
				sourceBuilder.Append(", [");
				if (!string.IsNullOrEmpty(targetEntityType.NamespaceName))
				{
					sourceBuilder.Append(targetEntityType.NamespaceName);
					sourceBuilder.Append("].[");
				}
				sourceBuilder.Append(targetEntityType.Name);
				sourceBuilder.Append("])");
			}
		}

		// Token: 0x06003864 RID: 14436 RVA: 0x000B9228 File Offset: 0x000B7428
		internal virtual ObjectQuery<TEntity> ValidateLoad<TEntity>(MergeOption mergeOption, string relatedEndName, out bool hasResults)
		{
			ObjectQuery<TEntity> objectQuery = this.CreateSourceQuery<TEntity>(mergeOption, out hasResults);
			if (objectQuery == null)
			{
				throw Error.RelatedEnd_RelatedEndNotAttachedToContext(relatedEndName);
			}
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(this._wrappedOwner.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw Error.Collections_InvalidEntityStateLoad(relatedEndName);
			}
			if (this.UsingNoTracking != (mergeOption == MergeOption.NoTracking))
			{
				throw Error.RelatedEnd_MismatchedMergeOptionOnLoad(mergeOption);
			}
			if (this.UsingNoTracking)
			{
				if (this.IsLoaded)
				{
					throw Error.RelatedEnd_LoadCalledOnAlreadyLoadedNoTrackedRelatedEnd();
				}
				if (!this.IsEmpty())
				{
					throw Error.RelatedEnd_LoadCalledOnNonEmptyNoTrackedRelatedEnd();
				}
			}
			return objectQuery;
		}

		// Token: 0x06003865 RID: 14437 RVA: 0x000B92B4 File Offset: 0x000B74B4
		public void Load()
		{
			this.Load(this.DefaultMergeOption);
		}

		// Token: 0x06003866 RID: 14438 RVA: 0x000B92C2 File Offset: 0x000B74C2
		public Task LoadAsync(CancellationToken cancellationToken)
		{
			return this.LoadAsync(this.DefaultMergeOption, cancellationToken);
		}

		// Token: 0x06003867 RID: 14439
		public abstract void Load(MergeOption mergeOption);

		// Token: 0x06003868 RID: 14440
		public abstract Task LoadAsync(MergeOption mergeOption, CancellationToken cancellationToken);

		// Token: 0x06003869 RID: 14441 RVA: 0x000B92D4 File Offset: 0x000B74D4
		internal void DeferredLoad()
		{
			if (this._wrappedOwner != null && this._wrappedOwner != NullEntityWrapper.NullWrapper && !this.IsLoaded && this._context != null && this._context.ContextOptions.LazyLoadingEnabled && !this._context.InMaterialization && this.CanDeferredLoad && (this.UsingNoTracking || (this._wrappedOwner.ObjectStateEntry != null && (this._wrappedOwner.ObjectStateEntry.State == EntityState.Unchanged || this._wrappedOwner.ObjectStateEntry.State == EntityState.Modified || (this._wrappedOwner.ObjectStateEntry.State == EntityState.Added && this.IsForeignKey && this.IsDependentEndOfReferentialConstraint(false))))))
			{
				this._context.ContextOptions.LazyLoadingEnabled = false;
				try
				{
					this.Load();
				}
				finally
				{
					this._context.ContextOptions.LazyLoadingEnabled = true;
				}
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000B93E0 File Offset: 0x000B75E0
		internal virtual bool CanDeferredLoad
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600386B RID: 14443 RVA: 0x000B93E4 File Offset: 0x000B75E4
		internal virtual void Merge<TEntity>(IEnumerable<TEntity> collection, MergeOption mergeOption, bool setIsLoaded)
		{
			List<IEntityWrapper> list = collection as List<IEntityWrapper>;
			if (list == null)
			{
				list = new List<IEntityWrapper>();
				EntitySet entitySet = ((AssociationSet)this.RelationshipSet).AssociationSetEnds[this.TargetRoleName].EntitySet;
				foreach (TEntity tentity in collection)
				{
					IEntityWrapper entityWrapper = this.EntityWrapperFactory.WrapEntityUsingContext(tentity, this.ObjectContext);
					if (mergeOption == MergeOption.NoTracking)
					{
						this.EntityWrapperFactory.UpdateNoTrackingWrapper(entityWrapper, this.ObjectContext, entitySet);
					}
					list.Add(entityWrapper);
				}
			}
			this.Merge<TEntity>(list, mergeOption, setIsLoaded);
		}

		// Token: 0x0600386C RID: 14444 RVA: 0x000B949C File Offset: 0x000B769C
		internal virtual void Merge<TEntity>(List<IEntityWrapper> collection, MergeOption mergeOption, bool setIsLoaded)
		{
			if (this.WrappedOwner.EntityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			this.ObjectContext.ObjectStateManager.UpdateRelationships(this.ObjectContext, mergeOption, (AssociationSet)this.RelationshipSet, (AssociationEndMember)this.FromEndMember, this.WrappedOwner, (AssociationEndMember)this.ToEndMember, collection, setIsLoaded);
			if (setIsLoaded)
			{
				this._isLoaded = true;
			}
		}

		// Token: 0x0600386D RID: 14445 RVA: 0x000B950D File Offset: 0x000B770D
		void IRelatedEnd.Attach(IEntityWithRelationships entity)
		{
			Check.NotNull<IEntityWithRelationships>(entity, "entity");
			((IRelatedEnd)this).Attach(entity);
		}

		// Token: 0x0600386E RID: 14446 RVA: 0x000B9522 File Offset: 0x000B7722
		void IRelatedEnd.Attach(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.CheckOwnerNull();
			this.Attach(new IEntityWrapper[] { this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext) }, false);
		}

		// Token: 0x0600386F RID: 14447 RVA: 0x000B9558 File Offset: 0x000B7758
		internal void Attach(IEnumerable<IEntityWrapper> wrappedEntities, bool allowCollection)
		{
			this.CheckOwnerNull();
			this.ValidateOwnerForAttach();
			int num = 0;
			List<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (IEntityWrapper entityWrapper in wrappedEntities)
			{
				this.ValidateEntityForAttach(entityWrapper, num++, allowCollection);
				list.Add(entityWrapper);
			}
			this._suppressEvents = true;
			try
			{
				this.Merge<IEntityWrapper>(list, MergeOption.OverwriteChanges, false);
				ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints.FirstOrDefault<ReferentialConstraint>();
				if (referentialConstraint != null)
				{
					ObjectStateManager objectStateManager = this.ObjectContext.ObjectStateManager;
					EntityEntry entityEntry = objectStateManager.FindEntityEntry(this._wrappedOwner.Entity);
					if (this.IsDependentEndOfReferentialConstraint(false))
					{
						if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(entityEntry.GetCurrentEntityValue), list[0].ObjectStateEntry.EntityKey))
						{
							throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
						}
					}
					else
					{
						foreach (IEntityWrapper entityWrapper2 in list)
						{
							RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(entityWrapper2);
							if (otherEndOfRelationship.IsDependentEndOfReferentialConstraint(false))
							{
								EntityEntry entityEntry2 = objectStateManager.FindEntityEntry(otherEndOfRelationship.WrappedOwner.Entity);
								if (!RelatedEnd.VerifyRIConstraintsWithRelatedEntry(referentialConstraint, new Func<string, object>(entityEntry2.GetCurrentEntityValue), entityEntry.EntityKey))
								{
									throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
								}
							}
						}
					}
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x06003870 RID: 14448 RVA: 0x000B9720 File Offset: 0x000B7920
		internal void ValidateOwnerForAttach()
		{
			if (this.ObjectContext == null || this.UsingNoTracking)
			{
				throw Error.RelatedEnd_InvalidOwnerStateForAttach();
			}
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.GetEntityEntry(this._wrappedOwner.Entity);
			if (entityEntry.State != EntityState.Modified && entityEntry.State != EntityState.Unchanged)
			{
				throw Error.RelatedEnd_InvalidOwnerStateForAttach();
			}
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000B9778 File Offset: 0x000B7978
		internal void ValidateEntityForAttach(IEntityWrapper wrappedEntity, int index, bool allowCollection)
		{
			if (wrappedEntity == null || wrappedEntity.Entity == null)
			{
				if (allowCollection)
				{
					throw Error.RelatedEnd_InvalidNthElementNullForAttach(index);
				}
				throw new ArgumentNullException("wrappedEntity");
			}
			else
			{
				this.VerifyType(wrappedEntity);
				EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
				if (entityEntry == null || entityEntry.Entity != wrappedEntity.Entity)
				{
					if (allowCollection)
					{
						throw Error.RelatedEnd_InvalidNthElementContextForAttach(index);
					}
					throw Error.RelatedEnd_InvalidEntityContextForAttach();
				}
				else
				{
					if (entityEntry.State == EntityState.Unchanged || entityEntry.State == EntityState.Modified)
					{
						return;
					}
					if (allowCollection)
					{
						throw Error.RelatedEnd_InvalidNthElementStateForAttach(index);
					}
					throw Error.RelatedEnd_InvalidEntityStateForAttach();
				}
			}
		}

		// Token: 0x06003872 RID: 14450
		internal abstract IEnumerable CreateSourceQueryInternal();

		// Token: 0x06003873 RID: 14451 RVA: 0x000B9816 File Offset: 0x000B7A16
		void IRelatedEnd.Add(IEntityWithRelationships entity)
		{
			Check.NotNull<IEntityWithRelationships>(entity, "entity");
			((IRelatedEnd)this).Add(entity);
		}

		// Token: 0x06003874 RID: 14452 RVA: 0x000B982B File Offset: 0x000B7A2B
		void IRelatedEnd.Add(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.Add(this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext));
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000B9851 File Offset: 0x000B7A51
		internal void Add(IEntityWrapper wrappedEntity)
		{
			if (this._wrappedOwner.Entity != null)
			{
				this.Add(wrappedEntity, true);
				return;
			}
			this.DisconnectedAdd(wrappedEntity);
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000B9870 File Offset: 0x000B7A70
		bool IRelatedEnd.Remove(IEntityWithRelationships entity)
		{
			Check.NotNull<IEntityWithRelationships>(entity, "entity");
			return ((IRelatedEnd)this).Remove(entity);
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000B9885 File Offset: 0x000B7A85
		bool IRelatedEnd.Remove(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.DeferredLoad();
			return this.Remove(this.EntityWrapperFactory.WrapEntityUsingContext(entity, this.ObjectContext), false);
		}

		// Token: 0x06003878 RID: 14456 RVA: 0x000B98B2 File Offset: 0x000B7AB2
		internal bool Remove(IEntityWrapper wrappedEntity, bool preserveForeignKey)
		{
			if (this._wrappedOwner.Entity == null)
			{
				return this.DisconnectedRemove(wrappedEntity);
			}
			if (this.ContainsEntity(wrappedEntity))
			{
				this.Remove(wrappedEntity, true, false, false, true, preserveForeignKey);
				return true;
			}
			return false;
		}

		// Token: 0x06003879 RID: 14457
		internal abstract void DisconnectedAdd(IEntityWrapper wrappedEntity);

		// Token: 0x0600387A RID: 14458
		internal abstract bool DisconnectedRemove(IEntityWrapper wrappedEntity);

		// Token: 0x0600387B RID: 14459 RVA: 0x000B98E1 File Offset: 0x000B7AE1
		internal void Add(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			if (this._context != null && !this.UsingNoTracking)
			{
				this.ValidateStateForAdd(this._wrappedOwner);
				this.ValidateStateForAdd(wrappedEntity);
			}
			this.Add(wrappedEntity, applyConstraints, false, false, true, true);
		}

		// Token: 0x0600387C RID: 14460 RVA: 0x000B9914 File Offset: 0x000B7B14
		internal void CheckRelationEntitySet(EntitySet set)
		{
			if (((AssociationSet)this._relationshipSet).AssociationSetEnds[this._navigation.To] != null && ((AssociationSet)this._relationshipSet).AssociationSetEnds[this._navigation.To].EntitySet != set)
			{
				throw Error.RelatedEnd_EntitySetIsNotValidForRelationship(set.EntityContainer.Name, set.Name, this._navigation.To, this._relationshipSet.EntityContainer.Name, this._relationshipSet.Name);
			}
		}

		// Token: 0x0600387D RID: 14461 RVA: 0x000B99A8 File Offset: 0x000B7BA8
		internal void ValidateStateForAdd(IEntityWrapper wrappedEntity)
		{
			EntityEntry entityEntry = this.ObjectContext.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw Error.RelatedEnd_UnableToAddRelationshipWithDeletedEntity();
			}
		}

		// Token: 0x0600387E RID: 14462 RVA: 0x000B99E0 File Offset: 0x000B7BE0
		internal void Add(IEntityWrapper wrappedTarget, bool applyConstraints, bool addRelationshipAsUnchanged, bool relationshipAlreadyExists, bool allowModifyingOtherEndOfRelationship, bool forceForeignKeyChanges)
		{
			if (!this.VerifyEntityForAdd(wrappedTarget, relationshipAlreadyExists))
			{
				return;
			}
			EntityKey entityKey = wrappedTarget.EntityKey;
			if (entityKey != null && this.ObjectContext != null)
			{
				this.CheckRelationEntitySet(entityKey.GetEntitySet(this.ObjectContext.MetadataWorkspace));
			}
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedTarget);
			this.ValidateContextsAreCompatible(otherEndOfRelationship);
			otherEndOfRelationship.VerifyEntityForAdd(this._wrappedOwner, relationshipAlreadyExists);
			otherEndOfRelationship.VerifyMultiplicityConstraintsForAdd(!allowModifyingOtherEndOfRelationship);
			if (this.CheckIfNavigationPropertyContainsEntity(wrappedTarget))
			{
				this.AddToLocalCache(wrappedTarget, applyConstraints);
			}
			else
			{
				this.AddToCache(wrappedTarget, applyConstraints);
			}
			if (otherEndOfRelationship.CheckIfNavigationPropertyContainsEntity(this.WrappedOwner))
			{
				otherEndOfRelationship.AddToLocalCache(this._wrappedOwner, false);
			}
			else
			{
				otherEndOfRelationship.AddToCache(this._wrappedOwner, false);
			}
			this.SynchronizeContexts(otherEndOfRelationship, relationshipAlreadyExists, addRelationshipAsUnchanged);
			if (this.ObjectContext != null && this.IsForeignKey && !this.ObjectContext.ObjectStateManager.TransactionManager.IsGraphUpdate && !this.UpdateDependentEndForeignKey(otherEndOfRelationship, forceForeignKeyChanges))
			{
				otherEndOfRelationship.UpdateDependentEndForeignKey(this, forceForeignKeyChanges);
			}
			otherEndOfRelationship.OnAssociationChanged(CollectionChangeAction.Add, this._wrappedOwner.Entity);
			this.OnAssociationChanged(CollectionChangeAction.Add, wrappedTarget.Entity);
		}

		// Token: 0x0600387F RID: 14463 RVA: 0x000B9AFB File Offset: 0x000B7CFB
		internal virtual void AddToNavigationPropertyIfCompatible(RelatedEnd otherRelatedEnd)
		{
			this.AddToNavigationProperty(otherRelatedEnd.WrappedOwner);
		}

		// Token: 0x06003880 RID: 14464 RVA: 0x000B9B09 File Offset: 0x000B7D09
		internal virtual bool CachedForeignKeyIsConceptualNull()
		{
			return false;
		}

		// Token: 0x06003881 RID: 14465 RVA: 0x000B9B0C File Offset: 0x000B7D0C
		internal virtual bool UpdateDependentEndForeignKey(RelatedEnd targetRelatedEnd, bool forceForeignKeyChanges)
		{
			return false;
		}

		// Token: 0x06003882 RID: 14466 RVA: 0x000B9B0F File Offset: 0x000B7D0F
		internal virtual void VerifyDetachedKeyMatches(EntityKey entityKey)
		{
		}

		// Token: 0x06003883 RID: 14467 RVA: 0x000B9B14 File Offset: 0x000B7D14
		private void ValidateContextsAreCompatible(RelatedEnd targetRelatedEnd)
		{
			if (this.ObjectContext == targetRelatedEnd.ObjectContext && this.ObjectContext != null)
			{
				if (this.UsingNoTracking != targetRelatedEnd.UsingNoTracking)
				{
					throw Error.RelatedEnd_CannotCreateRelationshipBetweenTrackedAndNoTrackedEntities(this.UsingNoTracking ? this._navigation.From : this._navigation.To);
				}
			}
			else if (this.ObjectContext != null && targetRelatedEnd.ObjectContext != null)
			{
				if (this.UsingNoTracking && targetRelatedEnd.UsingNoTracking)
				{
					targetRelatedEnd.WrappedOwner.ResetContext(this.ObjectContext, this.GetTargetEntitySetFromRelationshipSet(), MergeOption.NoTracking);
					return;
				}
				throw Error.RelatedEnd_CannotCreateRelationshipEntitiesInDifferentContexts();
			}
			else if ((this._context == null || this.UsingNoTracking) && targetRelatedEnd.ObjectContext != null && !targetRelatedEnd.UsingNoTracking)
			{
				targetRelatedEnd.ValidateStateForAdd(targetRelatedEnd.WrappedOwner);
			}
		}

		// Token: 0x06003884 RID: 14468 RVA: 0x000B9BDC File Offset: 0x000B7DDC
		private void SynchronizeContexts(RelatedEnd targetRelatedEnd, bool relationshipAlreadyExists, bool addRelationshipAsUnchanged)
		{
			RelatedEnd relatedEnd = null;
			IEntityWrapper entityWrapper = null;
			IEntityWrapper wrappedOwner = targetRelatedEnd.WrappedOwner;
			if (this.ObjectContext == targetRelatedEnd.ObjectContext && this.ObjectContext != null)
			{
				if (!this.IsForeignKey && !relationshipAlreadyExists && !this.UsingNoTracking)
				{
					if (!this.ObjectContext.ObjectStateManager.TransactionManager.IsLocalPublicAPI && this.WrappedOwner.EntityKey != null && !this.WrappedOwner.EntityKey.IsTemporary && this.IsDependentEndOfReferentialConstraint(false))
					{
						addRelationshipAsUnchanged = true;
					}
					this.AddRelationshipToObjectStateManager(wrappedOwner, addRelationshipAsUnchanged, false);
				}
				if (wrappedOwner.RequiresRelationshipChangeTracking && (this.ObjectContext.ObjectStateManager.TransactionManager.IsAddTracking || this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking || this.ObjectContext.ObjectStateManager.TransactionManager.IsDetectChanges))
				{
					this.AddToNavigationProperty(wrappedOwner);
					targetRelatedEnd.AddToNavigationProperty(this._wrappedOwner);
					return;
				}
			}
			else if (this.ObjectContext != null || targetRelatedEnd.ObjectContext != null)
			{
				if (this.ObjectContext == null)
				{
					relatedEnd = targetRelatedEnd;
					entityWrapper = this._wrappedOwner;
				}
				else
				{
					relatedEnd = this;
					entityWrapper = wrappedOwner;
				}
				if (!relatedEnd.UsingNoTracking)
				{
					TransactionManager transactionManager = relatedEnd.WrappedOwner.Context.ObjectStateManager.TransactionManager;
					transactionManager.BeginAddTracking();
					try
					{
						bool flag = true;
						try
						{
							if (transactionManager.TrackProcessedEntities)
							{
								if (!transactionManager.WrappedEntities.ContainsKey(entityWrapper.Entity))
								{
									transactionManager.WrappedEntities.Add(entityWrapper.Entity, entityWrapper);
								}
								transactionManager.ProcessedEntities.Add(relatedEnd.WrappedOwner);
							}
							relatedEnd.AddGraphToObjectStateManager(entityWrapper, relationshipAlreadyExists, addRelationshipAsUnchanged, false);
							if (entityWrapper.RequiresRelationshipChangeTracking && this.TargetAccessor.HasProperty)
							{
								targetRelatedEnd.AddToNavigationProperty(this._wrappedOwner);
							}
							flag = false;
						}
						finally
						{
							if (flag)
							{
								relatedEnd.WrappedOwner.Context.ObjectStateManager.DegradePromotedRelationships();
								relatedEnd.FixupOtherEndOfRelationshipForRemove(entityWrapper, false);
								relatedEnd.RemoveFromCache(entityWrapper, false, false);
								entityWrapper.RelationshipManager.NodeVisited = true;
								RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(entityWrapper);
								RelatedEnd.RemoveEntityFromObjectStateManager(entityWrapper);
							}
						}
					}
					finally
					{
						transactionManager.EndAddTracking();
					}
				}
			}
		}

		// Token: 0x06003885 RID: 14469 RVA: 0x000B9E08 File Offset: 0x000B8008
		private void AddGraphToObjectStateManager(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists, bool addRelationshipAsUnchanged, bool doAttach)
		{
			this.AddEntityToObjectStateManager(wrappedEntity, doAttach);
			if (!relationshipAlreadyExists && this.ObjectContext != null && wrappedEntity.Context != null)
			{
				if (!this.IsForeignKey)
				{
					this.AddRelationshipToObjectStateManager(wrappedEntity, addRelationshipAsUnchanged, doAttach);
				}
				if (wrappedEntity.RequiresRelationshipChangeTracking || this.WrappedOwner.RequiresRelationshipChangeTracking)
				{
					this.UpdateSnapshotOfRelationships(wrappedEntity);
					if (doAttach)
					{
						EntityEntry entityEntry = this._context.ObjectStateManager.GetEntityEntry(wrappedEntity.Entity);
						wrappedEntity.RelationshipManager.CheckReferentialConstraintProperties(entityEntry);
					}
				}
			}
			RelatedEnd.WalkObjectGraphToIncludeAllRelatedEntities(wrappedEntity, addRelationshipAsUnchanged, doAttach);
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000B9E90 File Offset: 0x000B8090
		private void UpdateSnapshotOfRelationships(IEntityWrapper wrappedEntity)
		{
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
			if (!otherEndOfRelationship.ContainsEntity(this.WrappedOwner))
			{
				otherEndOfRelationship.AddToLocalCache(this.WrappedOwner, false);
			}
		}

		// Token: 0x06003887 RID: 14471 RVA: 0x000B9EC0 File Offset: 0x000B80C0
		internal void Remove(IEntityWrapper wrappedEntity, bool doFixup, bool deleteEntity, bool deleteOwner, bool applyReferentialConstraints, bool preserveForeignKey)
		{
			if (wrappedEntity.RequiresRelationshipChangeTracking && doFixup && this.TargetAccessor.HasProperty && !this.CheckIfNavigationPropertyContainsEntity(wrappedEntity))
			{
				this.GetOtherEndOfRelationship(wrappedEntity).RemoveFromNavigationProperty(this.WrappedOwner);
			}
			if (!this.ContainsEntity(wrappedEntity))
			{
				return;
			}
			if (this._context != null && doFixup && applyReferentialConstraints && this.IsDependentEndOfReferentialConstraint(false))
			{
				this.GetOtherEndOfRelationship(wrappedEntity).Remove(this._wrappedOwner, doFixup, deleteEntity, deleteOwner, applyReferentialConstraints, preserveForeignKey);
				return;
			}
			bool flag = this.RemoveFromCache(wrappedEntity, false, preserveForeignKey);
			if (!this.UsingNoTracking && this.ObjectContext != null && !this.IsForeignKey)
			{
				RelatedEnd.MarkRelationshipAsDeletedInObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
			}
			if (doFixup)
			{
				this.FixupOtherEndOfRelationshipForRemove(wrappedEntity, preserveForeignKey);
				if ((this._context == null || !this._context.ObjectStateManager.TransactionManager.IsLocalPublicAPI) && this._context != null && (deleteEntity || (deleteOwner && RelatedEnd.CheckCascadeDeleteFlag(this._fromEndMember)) || (applyReferentialConstraints && this.IsPrincipalEndOfReferentialConstraint())) && wrappedEntity.Entity != this._context.ObjectStateManager.TransactionManager.EntityBeingReparented && this._context.ObjectStateManager.EntityInvokingFKSetter != wrappedEntity.Entity)
				{
					this.EnsureRelationshipNavigationAccessorsInitialized();
					RelatedEnd.RemoveEntityFromRelatedEnds(wrappedEntity, this._wrappedOwner, this._navigation.Reverse);
					RelatedEnd.MarkEntityAsDeletedInObjectStateManager(wrappedEntity);
				}
			}
			if (flag)
			{
				this.OnAssociationChanged(CollectionChangeAction.Remove, wrappedEntity.Entity);
			}
		}

		// Token: 0x06003888 RID: 14472 RVA: 0x000BA03C File Offset: 0x000B823C
		internal bool IsDependentEndOfReferentialConstraint(bool checkIdentifying)
		{
			if (this._relationMetadata != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.ToRole == this.FromEndMember)
					{
						if (checkIdentifying)
						{
							return RelatedEnd.CheckIfAllPropertiesAreKeyProperties(referentialConstraint.ToRole.GetEntityType().KeyMemberNames, referentialConstraint.ToProperties);
						}
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06003889 RID: 14473 RVA: 0x000BA0D0 File Offset: 0x000B82D0
		internal bool IsPrincipalEndOfReferentialConstraint()
		{
			if (this._relationMetadata != null)
			{
				foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this._relationMetadata).ReferentialConstraints)
				{
					if (referentialConstraint.FromRole == this._fromEndMember)
					{
						return RelatedEnd.CheckIfAllPropertiesAreKeyProperties(referentialConstraint.ToRole.GetEntityType().KeyMemberNames, referentialConstraint.ToProperties);
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000BA160 File Offset: 0x000B8360
		internal static bool CheckIfAllPropertiesAreKeyProperties(string[] keyMemberNames, ReadOnlyMetadataCollection<EdmProperty> toProperties)
		{
			foreach (EdmProperty edmProperty in toProperties)
			{
				bool flag = false;
				for (int i = 0; i < keyMemberNames.Length; i++)
				{
					if (keyMemberNames[i] == edmProperty.Name)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000BA1E0 File Offset: 0x000B83E0
		internal void IncludeEntity(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			EntityEntry entityEntry = this._context.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
			if (entityEntry != null && entityEntry.State == EntityState.Deleted)
			{
				throw Error.RelatedEnd_UnableToAddRelationshipWithDeletedEntity();
			}
			if (wrappedEntity.RequiresRelationshipChangeTracking || this.WrappedOwner.RequiresRelationshipChangeTracking)
			{
				RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
				this.ObjectContext.GetTypeUsage(otherEndOfRelationship.WrappedOwner.IdentityType);
				otherEndOfRelationship.AddToNavigationPropertyIfCompatible(this);
			}
			if (entityEntry == null)
			{
				this.AddGraphToObjectStateManager(wrappedEntity, false, addRelationshipAsUnchanged, doAttach);
				return;
			}
			if (this.FindRelationshipEntryInObjectStateManager(wrappedEntity) == null)
			{
				this.VerifyDetachedKeyMatches(wrappedEntity.EntityKey);
				if (this.ObjectContext != null && wrappedEntity.Context != null)
				{
					if (!this.IsForeignKey)
					{
						if (entityEntry.State == EntityState.Added)
						{
							this.AddRelationshipToObjectStateManager(wrappedEntity, addRelationshipAsUnchanged, false);
						}
						else
						{
							this.AddRelationshipToObjectStateManager(wrappedEntity, addRelationshipAsUnchanged, doAttach);
						}
					}
					if (wrappedEntity.RequiresRelationshipChangeTracking || this.WrappedOwner.RequiresRelationshipChangeTracking)
					{
						this.UpdateSnapshotOfRelationships(wrappedEntity);
						if (doAttach && entityEntry.State != EntityState.Added)
						{
							EntityEntry entityEntry2 = this.ObjectContext.ObjectStateManager.GetEntityEntry(wrappedEntity.Entity);
							wrappedEntity.RelationshipManager.CheckReferentialConstraintProperties(entityEntry2);
						}
					}
				}
			}
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000BA2FC File Offset: 0x000B84FC
		internal void MarkForeignKeyPropertiesModified()
		{
			ReferentialConstraint referentialConstraint = ((AssociationType)this.RelationMetadata).ReferentialConstraints[0];
			EntityEntry objectStateEntry = this.WrappedOwner.ObjectStateEntry;
			if (objectStateEntry.State == EntityState.Unchanged || objectStateEntry.State == EntityState.Modified)
			{
				foreach (EdmProperty edmProperty in referentialConstraint.ToProperties)
				{
					objectStateEntry.SetModifiedProperty(edmProperty.Name);
				}
			}
		}

		// Token: 0x0600388D RID: 14477
		internal abstract bool CheckIfNavigationPropertyContainsEntity(IEntityWrapper wrapper);

		// Token: 0x0600388E RID: 14478
		internal abstract void VerifyNavigationPropertyForAdd(IEntityWrapper wrapper);

		// Token: 0x0600388F RID: 14479 RVA: 0x000BA38C File Offset: 0x000B858C
		internal void AddToNavigationProperty(IEntityWrapper wrapper)
		{
			if (this.TargetAccessor.HasProperty && !this.CheckIfNavigationPropertyContainsEntity(wrapper))
			{
				TransactionManager transactionManager = wrapper.Context.ObjectStateManager.TransactionManager;
				if (transactionManager.IsAddTracking || transactionManager.IsAttachTracking)
				{
					wrapper.Context.ObjectStateManager.TrackPromotedRelationship(this, wrapper);
				}
				this.AddToObjectCache(wrapper);
			}
		}

		// Token: 0x06003890 RID: 14480 RVA: 0x000BA3E9 File Offset: 0x000B85E9
		internal void RemoveFromNavigationProperty(IEntityWrapper wrapper)
		{
			if (this.TargetAccessor.HasProperty && this.CheckIfNavigationPropertyContainsEntity(wrapper))
			{
				this.RemoveFromObjectCache(wrapper);
			}
		}

		// Token: 0x06003891 RID: 14481 RVA: 0x000BA40C File Offset: 0x000B860C
		internal void ExcludeEntity(IEntityWrapper wrappedEntity)
		{
			if (!this._context.ObjectStateManager.TransactionManager.TrackProcessedEntities || (!this._context.ObjectStateManager.TransactionManager.IsAttachTracking && !this._context.ObjectStateManager.TransactionManager.IsAddTracking) || this._context.ObjectStateManager.TransactionManager.ProcessedEntities.Contains(wrappedEntity))
			{
				EntityEntry entityEntry = this._context.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
				if (entityEntry != null && entityEntry.State != EntityState.Deleted && !wrappedEntity.RelationshipManager.NodeVisited)
				{
					wrappedEntity.RelationshipManager.NodeVisited = true;
					RelationshipManager.RemoveRelatedEntitiesFromObjectStateManager(wrappedEntity);
					if (!this.IsForeignKey)
					{
						RelatedEnd.RemoveRelationshipFromObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
					}
					RelatedEnd.RemoveEntityFromObjectStateManager(wrappedEntity);
					return;
				}
				if (!this.IsForeignKey && this.FindRelationshipEntryInObjectStateManager(wrappedEntity) != null)
				{
					RelatedEnd.RemoveRelationshipFromObjectStateManager(wrappedEntity, this._wrappedOwner, this._relationshipSet, this._navigation);
				}
			}
		}

		// Token: 0x06003892 RID: 14482 RVA: 0x000BA510 File Offset: 0x000B8710
		internal RelationshipEntry FindRelationshipEntryInObjectStateManager(IEntityWrapper wrappedEntity)
		{
			EntityKey entityKey = wrappedEntity.EntityKey;
			EntityKey entityKey2 = this._wrappedOwner.EntityKey;
			return this._context.ObjectStateManager.FindRelationship(this._relationshipSet, new KeyValuePair<string, EntityKey>(this._navigation.From, entityKey2), new KeyValuePair<string, EntityKey>(this._navigation.To, entityKey));
		}

		// Token: 0x06003893 RID: 14483 RVA: 0x000BA568 File Offset: 0x000B8768
		internal void Clear(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete)
		{
			this.ClearCollectionOrRef(wrappedEntity, navigation, doCascadeDelete);
		}

		// Token: 0x06003894 RID: 14484 RVA: 0x000BA574 File Offset: 0x000B8774
		internal void CheckReferentialConstraintProperties(EntityEntry ownerEntry)
		{
			foreach (ReferentialConstraint referentialConstraint in ((AssociationType)this.RelationMetadata).ReferentialConstraints)
			{
				if (referentialConstraint.ToRole == this.FromEndMember)
				{
					if (!this.CheckReferentialConstraintPrincipalProperty(ownerEntry, referentialConstraint))
					{
						throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
					}
				}
				else if (referentialConstraint.FromRole == this.FromEndMember && !this.CheckReferentialConstraintDependentProperty(ownerEntry, referentialConstraint))
				{
					throw new InvalidOperationException(referentialConstraint.BuildConstraintExceptionMessage());
				}
			}
		}

		// Token: 0x06003895 RID: 14485 RVA: 0x000BA614 File Offset: 0x000B8814
		internal virtual bool CheckReferentialConstraintPrincipalProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			return false;
		}

		// Token: 0x06003896 RID: 14486 RVA: 0x000BA618 File Offset: 0x000B8818
		internal virtual bool CheckReferentialConstraintDependentProperty(EntityEntry ownerEntry, ReferentialConstraint constraint)
		{
			if (!this.IsEmpty())
			{
				foreach (IEntityWrapper entityWrapper in this.GetWrappedEntities())
				{
					EntityEntry objectStateEntry = entityWrapper.ObjectStateEntry;
					if (objectStateEntry != null && objectStateEntry.State != EntityState.Added && objectStateEntry.State != EntityState.Deleted && objectStateEntry.State != EntityState.Detached && !RelatedEnd.VerifyRIConstraintsWithRelatedEntry(constraint, new Func<string, object>(objectStateEntry.GetCurrentEntityValue), ownerEntry.EntityKey))
					{
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x06003897 RID: 14487 RVA: 0x000BA6AC File Offset: 0x000B88AC
		internal static bool VerifyRIConstraintsWithRelatedEntry(ReferentialConstraint constraint, Func<string, object> getDependentPropertyValue, EntityKey principalKey)
		{
			for (int i = 0; i < constraint.FromProperties.Count; i++)
			{
				string name = constraint.FromProperties[i].Name;
				string name2 = constraint.ToProperties[i].Name;
				object obj = principalKey.FindValueByName(name);
				object obj2 = getDependentPropertyValue(name2);
				if (!ByValueEqualityComparer.Default.Equals(obj, obj2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003898 RID: 14488 RVA: 0x000BA716 File Offset: 0x000B8916
		public IEnumerator GetEnumerator()
		{
			this.DeferredLoad();
			return this.GetInternalEnumerable().GetEnumerator();
		}

		// Token: 0x06003899 RID: 14489 RVA: 0x000BA72C File Offset: 0x000B892C
		internal void RemoveAll()
		{
			List<IEntityWrapper> list = null;
			bool flag = false;
			try
			{
				this._suppressEvents = true;
				foreach (IEntityWrapper entityWrapper in this.GetWrappedEntities())
				{
					if (list == null)
					{
						list = new List<IEntityWrapper>();
					}
					list.Add(entityWrapper);
				}
				if (flag = list != null && list.Count > 0)
				{
					foreach (IEntityWrapper entityWrapper2 in list)
					{
						this.Remove(entityWrapper2, true, false, true, true, false);
					}
				}
			}
			finally
			{
				this._suppressEvents = false;
			}
			if (flag)
			{
				this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
			}
		}

		// Token: 0x0600389A RID: 14490 RVA: 0x000BA808 File Offset: 0x000B8A08
		internal virtual void DetachAll(EntityState ownerEntityState)
		{
			List<IEntityWrapper> list = new List<IEntityWrapper>();
			foreach (IEntityWrapper entityWrapper in this.GetWrappedEntities())
			{
				list.Add(entityWrapper);
			}
			bool flag = ownerEntityState == EntityState.Added || this._fromEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many;
			foreach (IEntityWrapper entityWrapper2 in list)
			{
				if (!this.ContainsEntity(entityWrapper2))
				{
					return;
				}
				if (flag)
				{
					RelatedEnd.DetachRelationshipFromObjectStateManager(entityWrapper2, this._wrappedOwner, this._relationshipSet, this._navigation);
				}
				RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(entityWrapper2);
				otherEndOfRelationship.RemoveFromCache(this._wrappedOwner, true, false);
				otherEndOfRelationship.OnAssociationChanged(CollectionChangeAction.Remove, this._wrappedOwner.Entity);
			}
			foreach (IEntityWrapper entityWrapper3 in list)
			{
				this.GetOtherEndOfRelationship(entityWrapper3);
				this.RemoveFromCache(entityWrapper3, false, false);
			}
			this.OnAssociationChanged(CollectionChangeAction.Refresh, null);
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000BA950 File Offset: 0x000B8B50
		internal void AddToCache(IEntityWrapper wrappedEntity, bool applyConstraints)
		{
			this.AddToLocalCache(wrappedEntity, applyConstraints);
			this.AddToObjectCache(wrappedEntity);
		}

		// Token: 0x0600389C RID: 14492
		internal abstract void AddToLocalCache(IEntityWrapper wrappedEntity, bool applyConstraints);

		// Token: 0x0600389D RID: 14493
		internal abstract void AddToObjectCache(IEntityWrapper wrappedEntity);

		// Token: 0x0600389E RID: 14494 RVA: 0x000BA961 File Offset: 0x000B8B61
		internal bool RemoveFromCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey)
		{
			bool flag = this.RemoveFromLocalCache(wrappedEntity, resetIsLoaded, preserveForeignKey);
			this.RemoveFromObjectCache(wrappedEntity);
			return flag;
		}

		// Token: 0x0600389F RID: 14495
		internal abstract bool RemoveFromLocalCache(IEntityWrapper wrappedEntity, bool resetIsLoaded, bool preserveForeignKey);

		// Token: 0x060038A0 RID: 14496
		internal abstract bool RemoveFromObjectCache(IEntityWrapper wrappedEntity);

		// Token: 0x060038A1 RID: 14497 RVA: 0x000BA974 File Offset: 0x000B8B74
		internal virtual bool VerifyEntityForAdd(IEntityWrapper wrappedEntity, bool relationshipAlreadyExists)
		{
			if (relationshipAlreadyExists && this.ContainsEntity(wrappedEntity))
			{
				return false;
			}
			this.VerifyType(wrappedEntity);
			return true;
		}

		// Token: 0x060038A2 RID: 14498
		internal abstract void VerifyType(IEntityWrapper wrappedEntity);

		// Token: 0x060038A3 RID: 14499
		internal abstract bool CanSetEntityType(IEntityWrapper wrappedEntity);

		// Token: 0x060038A4 RID: 14500
		internal abstract void Include(bool addRelationshipAsUnchanged, bool doAttach);

		// Token: 0x060038A5 RID: 14501
		internal abstract void Exclude();

		// Token: 0x060038A6 RID: 14502
		internal abstract void ClearCollectionOrRef(IEntityWrapper wrappedEntity, RelationshipNavigation navigation, bool doCascadeDelete);

		// Token: 0x060038A7 RID: 14503
		internal abstract bool ContainsEntity(IEntityWrapper wrappedEntity);

		// Token: 0x060038A8 RID: 14504
		internal abstract IEnumerable GetInternalEnumerable();

		// Token: 0x060038A9 RID: 14505
		internal abstract IEnumerable<IEntityWrapper> GetWrappedEntities();

		// Token: 0x060038AA RID: 14506
		internal abstract void RetrieveReferentialConstraintProperties(Dictionary<string, KeyValuePair<object, IntBox>> keyValues, HashSet<object> visited);

		// Token: 0x060038AB RID: 14507
		internal abstract bool IsEmpty();

		// Token: 0x060038AC RID: 14508
		internal abstract void OnRelatedEndClear();

		// Token: 0x060038AD RID: 14509
		internal abstract void ClearWrappedValues();

		// Token: 0x060038AE RID: 14510
		internal abstract void VerifyMultiplicityConstraintsForAdd(bool applyConstraints);

		// Token: 0x060038AF RID: 14511 RVA: 0x000BA98C File Offset: 0x000B8B8C
		internal virtual void OnAssociationChanged(CollectionChangeAction collectionChangeAction, object entity)
		{
			if (!this._suppressEvents && this._onAssociationChanged != null)
			{
				this._onAssociationChanged(this, new CollectionChangeEventArgs(collectionChangeAction, entity));
			}
		}

		// Token: 0x060038B0 RID: 14512 RVA: 0x000BA9B4 File Offset: 0x000B8BB4
		internal virtual void AddEntityToObjectStateManager(IEntityWrapper wrappedEntity, bool doAttach)
		{
			EntitySet targetEntitySetFromRelationshipSet = this.GetTargetEntitySetFromRelationshipSet();
			if (!doAttach)
			{
				this._context.AddSingleObject(targetEntitySetFromRelationshipSet, wrappedEntity, "entity");
				return;
			}
			this._context.AttachSingleObject(wrappedEntity, targetEntitySetFromRelationshipSet);
		}

		// Token: 0x060038B1 RID: 14513 RVA: 0x000BA9EC File Offset: 0x000B8BEC
		internal EntitySet GetTargetEntitySetFromRelationshipSet()
		{
			AssociationSet associationSet = (AssociationSet)this._relationshipSet;
			AssociationEndMember associationEndMember = (AssociationEndMember)this.ToEndMember;
			return associationSet.AssociationSetEnds[associationEndMember.Name].EntitySet;
		}

		// Token: 0x060038B2 RID: 14514 RVA: 0x000BAA28 File Offset: 0x000B8C28
		private RelationshipEntry AddRelationshipToObjectStateManager(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			EntityKey entityKey = this._wrappedOwner.EntityKey;
			EntityKey entityKey2 = wrappedEntity.EntityKey;
			if (entityKey == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			if (entityKey2 == null)
			{
				throw Error.EntityKey_UnexpectedNull();
			}
			return this.ObjectContext.ObjectStateManager.AddRelation(new RelationshipWrapper((AssociationSet)this._relationshipSet, new KeyValuePair<string, EntityKey>(this._navigation.From, entityKey), new KeyValuePair<string, EntityKey>(this._navigation.To, entityKey2)), (addRelationshipAsUnchanged || doAttach) ? EntityState.Unchanged : EntityState.Added);
		}

		// Token: 0x060038B3 RID: 14515 RVA: 0x000BAAA8 File Offset: 0x000B8CA8
		private static void WalkObjectGraphToIncludeAllRelatedEntities(IEntityWrapper wrappedEntity, bool addRelationshipAsUnchanged, bool doAttach)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity.RelationshipManager.Relationships)
			{
				relatedEnd.Include(addRelationshipAsUnchanged, doAttach);
			}
		}

		// Token: 0x060038B4 RID: 14516 RVA: 0x000BAAFC File Offset: 0x000B8CFC
		internal static void RemoveEntityFromObjectStateManager(IEntityWrapper wrappedEntity)
		{
			EntityEntry entityEntry;
			if (wrappedEntity.Context != null && wrappedEntity.Context.ObjectStateManager.TransactionManager.IsAttachTracking && wrappedEntity.Context.ObjectStateManager.TransactionManager.PromotedKeyEntries.TryGetValue(wrappedEntity.Entity, out entityEntry))
			{
				entityEntry.DegradeEntry();
				return;
			}
			entityEntry = RelatedEnd.MarkEntityAsDeletedInObjectStateManager(wrappedEntity);
			if (entityEntry != null && entityEntry.State != EntityState.Detached)
			{
				entityEntry.AcceptChanges();
			}
		}

		// Token: 0x060038B5 RID: 14517 RVA: 0x000BAB6C File Offset: 0x000B8D6C
		private static void RemoveRelationshipFromObjectStateManager(IEntityWrapper wrappedEntity, IEntityWrapper wrappedOwner, RelationshipSet relationshipSet, RelationshipNavigation navigation)
		{
			RelationshipEntry relationshipEntry = RelatedEnd.MarkRelationshipAsDeletedInObjectStateManager(wrappedEntity, wrappedOwner, relationshipSet, navigation);
			if (relationshipEntry != null && relationshipEntry.State != EntityState.Detached)
			{
				relationshipEntry.AcceptChanges();
			}
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000BAB95 File Offset: 0x000B8D95
		private void FixupOtherEndOfRelationshipForRemove(IEntityWrapper wrappedEntity, bool preserveForeignKey)
		{
			RelatedEnd otherEndOfRelationship = this.GetOtherEndOfRelationship(wrappedEntity);
			otherEndOfRelationship.Remove(this._wrappedOwner, false, false, false, false, preserveForeignKey);
			otherEndOfRelationship.RemoveFromNavigationProperty(this._wrappedOwner);
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x000BABBC File Offset: 0x000B8DBC
		private static EntityEntry MarkEntityAsDeletedInObjectStateManager(IEntityWrapper wrappedEntity)
		{
			EntityEntry entityEntry = null;
			if (wrappedEntity.Context != null)
			{
				entityEntry = wrappedEntity.Context.ObjectStateManager.FindEntityEntry(wrappedEntity.Entity);
				if (entityEntry != null)
				{
					entityEntry.Delete(false);
				}
			}
			return entityEntry;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x000BABF8 File Offset: 0x000B8DF8
		private static RelationshipEntry MarkRelationshipAsDeletedInObjectStateManager(IEntityWrapper wrappedEntity, IEntityWrapper wrappedOwner, RelationshipSet relationshipSet, RelationshipNavigation navigation)
		{
			RelationshipEntry relationshipEntry = null;
			if (wrappedOwner.Context != null && wrappedEntity.Context != null && relationshipSet != null)
			{
				EntityKey entityKey = wrappedOwner.EntityKey;
				EntityKey entityKey2 = wrappedEntity.EntityKey;
				relationshipEntry = wrappedEntity.Context.ObjectStateManager.DeleteRelationship(relationshipSet, new KeyValuePair<string, EntityKey>(navigation.From, entityKey), new KeyValuePair<string, EntityKey>(navigation.To, entityKey2));
			}
			return relationshipEntry;
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x000BAC54 File Offset: 0x000B8E54
		private static void DetachRelationshipFromObjectStateManager(IEntityWrapper wrappedEntity, IEntityWrapper wrappedOwner, RelationshipSet relationshipSet, RelationshipNavigation navigation)
		{
			if (wrappedOwner.Context != null && wrappedEntity.Context != null && relationshipSet != null)
			{
				EntityKey entityKey = wrappedOwner.EntityKey;
				EntityKey entityKey2 = wrappedEntity.EntityKey;
				RelationshipEntry relationshipEntry = wrappedEntity.Context.ObjectStateManager.FindRelationship(relationshipSet, new KeyValuePair<string, EntityKey>(navigation.From, entityKey), new KeyValuePair<string, EntityKey>(navigation.To, entityKey2));
				if (relationshipEntry != null)
				{
					relationshipEntry.DetachRelationshipEntry();
				}
			}
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x000BACB8 File Offset: 0x000B8EB8
		private static void RemoveEntityFromRelatedEnds(IEntityWrapper wrappedEntity1, IEntityWrapper wrappedEntity2, RelationshipNavigation navigation)
		{
			foreach (RelatedEnd relatedEnd in wrappedEntity1.RelationshipManager.Relationships)
			{
				bool flag = RelatedEnd.CheckCascadeDeleteFlag(relatedEnd.FromEndMember) || relatedEnd.IsPrincipalEndOfReferentialConstraint();
				relatedEnd.Clear(wrappedEntity2, navigation, flag);
			}
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x000BAD28 File Offset: 0x000B8F28
		private static bool CheckCascadeDeleteFlag(RelationshipEndMember relationEndProperty)
		{
			return relationEndProperty != null && relationEndProperty.DeleteBehavior == OperationAction.Cascade;
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000BAD38 File Offset: 0x000B8F38
		internal void AttachContext(ObjectContext context, MergeOption mergeOption)
		{
			if (!this._wrappedOwner.InitializingProxyRelatedEnds)
			{
				EntityKey entityKey = this._wrappedOwner.EntityKey;
				if (entityKey == null)
				{
					throw Error.EntityKey_UnexpectedNull();
				}
				EntitySet entitySet = entityKey.GetEntitySet(context.MetadataWorkspace);
				this.AttachContext(context, entitySet, mergeOption);
			}
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000BAD7C File Offset: 0x000B8F7C
		internal void AttachContext(ObjectContext context, EntitySet entitySet, MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			this._wrappedOwner.RelationshipManager.NodeVisited = false;
			if (this._context == context && this._usingNoTracking == (mergeOption == MergeOption.NoTracking))
			{
				return;
			}
			bool flag = true;
			try
			{
				this._sourceQuery = null;
				this._context = context;
				this._entityWrapperFactory = context.EntityWrapperFactory;
				this._usingNoTracking = mergeOption == MergeOption.NoTracking;
				EdmType edmType;
				RelationshipSet relationshipSet;
				this.FindRelationshipSet(this._context, entitySet, out edmType, out relationshipSet);
				if (relationshipSet == null)
				{
					foreach (EntitySetBase entitySetBase in entitySet.EntityContainer.BaseEntitySets)
					{
						AssociationSet associationSet = entitySetBase as AssociationSet;
						if (associationSet != null && associationSet.ElementType == edmType && associationSet.AssociationSetEnds[this._navigation.From].EntitySet != entitySet && associationSet.AssociationSetEnds[this._navigation.From].EntitySet.ElementType == entitySet.ElementType)
						{
							throw Error.RelatedEnd_EntitySetIsNotValidForRelationship(entitySet.EntityContainer.Name, entitySet.Name, this._navigation.From, entitySetBase.EntityContainer.Name, entitySetBase.Name);
						}
					}
					throw Error.Collections_NoRelationshipSetMatched(this._navigation.RelationshipName);
				}
				this._relationshipSet = relationshipSet;
				this._relationMetadata = (RelationshipType)edmType;
				bool flag2 = false;
				bool flag3 = false;
				foreach (AssociationEndMember associationEndMember in ((AssociationType)this._relationMetadata).AssociationEndMembers)
				{
					if (associationEndMember.Name == this._navigation.From)
					{
						flag2 = true;
						this._fromEndMember = associationEndMember;
					}
					if (associationEndMember.Name == this._navigation.To)
					{
						flag3 = true;
						this._toEndMember = associationEndMember;
					}
				}
				if (!flag2 || !flag3)
				{
					throw Error.RelatedEnd_RelatedEndNotFound();
				}
				this.ValidateDetachedEntityKey();
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.DetachContext();
				}
			}
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x000BAFE0 File Offset: 0x000B91E0
		internal virtual void ValidateDetachedEntityKey()
		{
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x000BAFE4 File Offset: 0x000B91E4
		internal void FindRelationshipSet(ObjectContext context, EntitySet entitySet, out EdmType relationshipType, out RelationshipSet relationshipSet)
		{
			if (this._navigation.AssociationType == null || this._navigation.AssociationType.Index < 0)
			{
				RelatedEnd.FindRelationshipSet(context, this._navigation, entitySet, out relationshipType, out relationshipSet);
				return;
			}
			MetadataOptimization metadataOptimization = context.MetadataWorkspace.MetadataOptimization;
			AssociationType cspaceAssociationType = metadataOptimization.GetCSpaceAssociationType(this._navigation.AssociationType);
			relationshipType = cspaceAssociationType;
			relationshipSet = metadataOptimization.FindCSpaceAssociationSet(cspaceAssociationType, this._navigation.From, entitySet);
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x000BB05C File Offset: 0x000B925C
		internal static void FindRelationshipSet(ObjectContext context, RelationshipNavigation navigation, EntitySet entitySet, out EdmType relationshipType, out RelationshipSet relationshipSet)
		{
			relationshipType = context.MetadataWorkspace.GetItem<EdmType>(navigation.RelationshipName, DataSpace.CSpace);
			if (relationshipType == null)
			{
				throw Error.Collections_NoRelationshipSetMatched(navigation.RelationshipName);
			}
			foreach (AssociationSet associationSet in entitySet.AssociationSets)
			{
				if (associationSet.ElementType == relationshipType && associationSet.AssociationSetEnds[navigation.From].EntitySet == entitySet)
				{
					relationshipSet = associationSet;
					return;
				}
			}
			relationshipSet = null;
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x000BB0F4 File Offset: 0x000B92F4
		internal void DetachContext()
		{
			if (this._context != null && this.ObjectContext.ObjectStateManager.TransactionManager.IsAttachTracking)
			{
				MergeOption? originalMergeOption = this.ObjectContext.ObjectStateManager.TransactionManager.OriginalMergeOption;
				MergeOption mergeOption = MergeOption.NoTracking;
				if ((originalMergeOption.GetValueOrDefault() == mergeOption) & (originalMergeOption != null))
				{
					this._usingNoTracking = true;
					return;
				}
			}
			this._sourceQuery = null;
			this._context = null;
			this._relationshipSet = null;
			this._fromEndMember = null;
			this._toEndMember = null;
			this._relationMetadata = null;
			this._isLoaded = false;
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x000BB185 File Offset: 0x000B9385
		internal RelatedEnd GetOtherEndOfRelationship(IEntityWrapper wrappedEntity)
		{
			this.EnsureRelationshipNavigationAccessorsInitialized();
			return wrappedEntity.RelationshipManager.GetRelatedEnd(this._navigation.Reverse, this._relationshipFixer);
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x000BB1A9 File Offset: 0x000B93A9
		internal virtual void CheckOwnerNull()
		{
			if (this._wrappedOwner.Entity == null)
			{
				throw Error.RelatedEnd_OwnerIsNull();
			}
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x000BB1BE File Offset: 0x000B93BE
		internal void InitializeRelatedEnd(IEntityWrapper wrappedOwner, RelationshipNavigation navigation, IRelationshipFixer relationshipFixer)
		{
			this.SetWrappedOwner(wrappedOwner);
			this._navigation = navigation;
			this._relationshipFixer = relationshipFixer;
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x000BB1D5 File Offset: 0x000B93D5
		internal void SetWrappedOwner(IEntityWrapper wrappedOwner)
		{
			this._wrappedOwner = ((wrappedOwner != null) ? wrappedOwner : NullEntityWrapper.NullWrapper);
			this._owner = wrappedOwner.Entity as IEntityWithRelationships;
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x000BB1F9 File Offset: 0x000B93F9
		internal static bool IsValidEntityKeyType(EntityKey entityKey)
		{
			return !entityKey.IsTemporary && EntityKey.EntityNotValidKey != entityKey && EntityKey.NoEntitySetKey != entityKey;
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x000BB218 File Offset: 0x000B9418
		[OnDeserialized]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void OnDeserialized(StreamingContext context)
		{
			this._wrappedOwner = this.EntityWrapperFactory.WrapEntityUsingContext(this._owner, this.ObjectContext);
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x000BB238 File Offset: 0x000B9438
		internal NavigationProperty NavigationProperty
		{
			get
			{
				if (this.navigationPropertyCache == null && this._wrappedOwner.Context != null && this.TargetAccessor.HasProperty)
				{
					string propertyName = this.TargetAccessor.PropertyName;
					NavigationProperty navigationProperty;
					if (!this._wrappedOwner.Context.MetadataWorkspace.GetItem<EntityType>(this._wrappedOwner.IdentityType.FullNameWithNesting(), DataSpace.OSpace).NavigationProperties.TryGetValue(propertyName, false, out navigationProperty))
					{
						throw Error.RelationshipManager_NavigationPropertyNotFound(propertyName);
					}
					this.navigationPropertyCache = navigationProperty;
				}
				return this.navigationPropertyCache;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x060038C9 RID: 14537 RVA: 0x000BB2BD File Offset: 0x000B94BD
		internal NavigationPropertyAccessor TargetAccessor
		{
			get
			{
				if (this._wrappedOwner.Entity != null)
				{
					this.EnsureRelationshipNavigationAccessorsInitialized();
					return this.RelationshipNavigation.ToPropertyAccessor;
				}
				return NavigationPropertyAccessor.NoNavigationProperty;
			}
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x000BB2E4 File Offset: 0x000B94E4
		private void EnsureRelationshipNavigationAccessorsInitialized()
		{
			if (!this.RelationshipNavigation.IsInitialized)
			{
				NavigationPropertyAccessor navigationPropertyAccessor = null;
				NavigationPropertyAccessor navigationPropertyAccessor2 = null;
				string relationshipName = this._navigation.RelationshipName;
				string from = this._navigation.From;
				string to = this._navigation.To;
				AssociationType associationType = (this.RelationMetadata as AssociationType) ?? this._wrappedOwner.RelationshipManager.GetRelationshipType(relationshipName);
				AssociationEndMember associationEndMember;
				if (associationType.AssociationEndMembers.TryGetValue(from, false, out associationEndMember))
				{
					navigationPropertyAccessor2 = MetadataHelper.GetNavigationPropertyAccessor(MetadataHelper.GetEntityTypeForEnd(associationEndMember), relationshipName, from, to);
				}
				AssociationEndMember associationEndMember2;
				if (associationType.AssociationEndMembers.TryGetValue(to, false, out associationEndMember2))
				{
					navigationPropertyAccessor = MetadataHelper.GetNavigationPropertyAccessor(MetadataHelper.GetEntityTypeForEnd(associationEndMember2), relationshipName, to, from);
				}
				if (navigationPropertyAccessor == null || navigationPropertyAccessor2 == null)
				{
					throw RelationshipManager.UnableToGetMetadata(this.WrappedOwner, relationshipName);
				}
				this.RelationshipNavigation.InitializeAccessors(navigationPropertyAccessor, navigationPropertyAccessor2);
			}
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x000BB3AE File Offset: 0x000B95AE
		internal bool DisableLazyLoading()
		{
			if (this._context == null)
			{
				return false;
			}
			bool lazyLoadingEnabled = this._context.ContextOptions.LazyLoadingEnabled;
			this._context.ContextOptions.LazyLoadingEnabled = false;
			return lazyLoadingEnabled;
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x000BB3DB File Offset: 0x000B95DB
		internal void ResetLazyLoading(bool state)
		{
			if (this._context != null)
			{
				this._context.ContextOptions.LazyLoadingEnabled = state;
			}
		}

		// Token: 0x040012E3 RID: 4835
		private const string _entityKeyParamName = "EntityKeyValue";

		// Token: 0x040012E4 RID: 4836
		[Obsolete]
		private IEntityWithRelationships _owner;

		// Token: 0x040012E5 RID: 4837
		private RelationshipNavigation _navigation;

		// Token: 0x040012E6 RID: 4838
		private IRelationshipFixer _relationshipFixer;

		// Token: 0x040012E7 RID: 4839
		internal bool _isLoaded;

		// Token: 0x040012E8 RID: 4840
		[NonSerialized]
		private RelationshipSet _relationshipSet;

		// Token: 0x040012E9 RID: 4841
		[NonSerialized]
		private ObjectContext _context;

		// Token: 0x040012EA RID: 4842
		[NonSerialized]
		private bool _usingNoTracking;

		// Token: 0x040012EB RID: 4843
		[NonSerialized]
		private RelationshipType _relationMetadata;

		// Token: 0x040012EC RID: 4844
		[NonSerialized]
		private RelationshipEndMember _fromEndMember;

		// Token: 0x040012ED RID: 4845
		[NonSerialized]
		private RelationshipEndMember _toEndMember;

		// Token: 0x040012EE RID: 4846
		[NonSerialized]
		private string _sourceQuery;

		// Token: 0x040012EF RID: 4847
		[NonSerialized]
		private IEnumerable<EdmMember> _sourceQueryParamProperties;

		// Token: 0x040012F0 RID: 4848
		[NonSerialized]
		internal bool _suppressEvents;

		// Token: 0x040012F1 RID: 4849
		[NonSerialized]
		internal CollectionChangeEventHandler _onAssociationChanged;

		// Token: 0x040012F2 RID: 4850
		[NonSerialized]
		private IEntityWrapper _wrappedOwner;

		// Token: 0x040012F3 RID: 4851
		[NonSerialized]
		private EntityWrapperFactory _entityWrapperFactory;

		// Token: 0x040012F4 RID: 4852
		[NonSerialized]
		private NavigationProperty navigationPropertyCache;
	}
}
