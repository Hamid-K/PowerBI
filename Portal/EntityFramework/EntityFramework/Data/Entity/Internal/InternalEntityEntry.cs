using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Internal.Validation;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000105 RID: 261
	internal class InternalEntityEntry
	{
		// Token: 0x060012A4 RID: 4772 RVA: 0x00030BC2 File Offset: 0x0002EDC2
		public InternalEntityEntry(InternalContext internalContext, IEntityStateEntry stateEntry)
		{
			this._internalContext = internalContext;
			this._stateEntry = stateEntry;
			this._entity = stateEntry.Entity;
			this._entityType = ObjectContextTypeCache.GetObjectType(this._entity.GetType());
		}

		// Token: 0x060012A5 RID: 4773 RVA: 0x00030BFC File Offset: 0x0002EDFC
		public InternalEntityEntry(InternalContext internalContext, object entity)
		{
			this._internalContext = internalContext;
			this._entity = entity;
			this._entityType = ObjectContextTypeCache.GetObjectType(this._entity.GetType());
			this._stateEntry = this._internalContext.GetStateEntry(entity);
			if (this._stateEntry == null)
			{
				this._internalContext.Set(this._entityType).InternalSet.Initialize();
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00030C68 File Offset: 0x0002EE68
		public virtual object Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060012A7 RID: 4775 RVA: 0x00030C70 File Offset: 0x0002EE70
		// (set) Token: 0x060012A8 RID: 4776 RVA: 0x00030C88 File Offset: 0x0002EE88
		public virtual EntityState State
		{
			get
			{
				if (!this.IsDetached)
				{
					return this._stateEntry.State;
				}
				return EntityState.Detached;
			}
			set
			{
				if (!this.IsDetached)
				{
					if (this._stateEntry.State == EntityState.Modified && value == EntityState.Unchanged)
					{
						this.CurrentValues.SetValues(this.OriginalValues);
					}
					this._stateEntry.ChangeState(value);
					return;
				}
				if (value <= EntityState.Added)
				{
					if (value == EntityState.Unchanged)
					{
						this._internalContext.Set(this._entityType).InternalSet.Attach(this._entity);
						return;
					}
					if (value != EntityState.Added)
					{
						return;
					}
					this._internalContext.Set(this._entityType).InternalSet.Add(this._entity);
					return;
				}
				else
				{
					if (value != EntityState.Deleted && value != EntityState.Modified)
					{
						return;
					}
					this._internalContext.Set(this._entityType).InternalSet.Attach(this._entity);
					this._stateEntry = this._internalContext.GetStateEntry(this._entity);
					this._stateEntry.ChangeState(value);
					return;
				}
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060012A9 RID: 4777 RVA: 0x00030D6D File Offset: 0x0002EF6D
		public virtual InternalPropertyValues CurrentValues
		{
			get
			{
				this.ValidateStateToGetValues("CurrentValues", EntityState.Deleted);
				return new DbDataRecordPropertyValues(this._internalContext, this._entityType, this._stateEntry.CurrentValues, true);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00030D98 File Offset: 0x0002EF98
		public virtual InternalPropertyValues OriginalValues
		{
			get
			{
				this.ValidateStateToGetValues("OriginalValues", EntityState.Added);
				return new DbDataRecordPropertyValues(this._internalContext, this._entityType, this._stateEntry.GetUpdatableOriginalValues(), true);
			}
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x00030DC4 File Offset: 0x0002EFC4
		public virtual InternalPropertyValues GetDatabaseValues()
		{
			this.ValidateStateToGetValues("GetDatabaseValues", EntityState.Added);
			DbDataRecord dbDataRecord = this.GetDatabaseValuesQuery().SingleOrDefault<DbDataRecord>();
			if (dbDataRecord != null)
			{
				return new ClonedPropertyValues(this.OriginalValues, dbDataRecord);
			}
			return null;
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x00030DFC File Offset: 0x0002EFFC
		public virtual async Task<InternalPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
		{
			this.ValidateStateToGetValues("GetDatabaseValuesAsync", EntityState.Added);
			cancellationToken.ThrowIfCancellationRequested();
			DbDataRecord dbDataRecord = await this.GetDatabaseValuesQuery().SingleOrDefaultAsync(cancellationToken).WithCurrentCulture<DbDataRecord>();
			return (dbDataRecord == null) ? null : new ClonedPropertyValues(this.OriginalValues, dbDataRecord);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x00030E4C File Offset: 0x0002F04C
		private ObjectQuery<DbDataRecord> GetDatabaseValuesQuery()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ");
			this.AppendEntitySqlRow(stringBuilder, "X", this.OriginalValues);
			string text = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				DbHelpers.QuoteIdentifier(this._stateEntry.EntitySet.EntityContainer.Name),
				DbHelpers.QuoteIdentifier(this._stateEntry.EntitySet.Name)
			});
			string text2 = string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				DbHelpers.QuoteIdentifier(this.EntityType.NestingNamespace()),
				DbHelpers.QuoteIdentifier(this.EntityType.Name)
			});
			stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " FROM (SELECT VALUE TREAT (Y AS {0}) FROM {1} AS Y) AS X WHERE ", new object[] { text2, text });
			EntityKeyMember[] entityKeyValues = this._stateEntry.EntityKey.EntityKeyValues;
			ObjectParameter[] array = new ObjectParameter[entityKeyValues.Length];
			for (int i = 0; i < entityKeyValues.Length; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(" AND ");
				}
				string text3 = string.Format(CultureInfo.InvariantCulture, "p{0}", new object[] { i.ToString(CultureInfo.InvariantCulture) });
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "X.{0} = @{1}", new object[]
				{
					DbHelpers.QuoteIdentifier(entityKeyValues[i].Key),
					text3
				});
				array[i] = new ObjectParameter(text3, entityKeyValues[i].Value);
			}
			return this._internalContext.ObjectContext.CreateQuery<DbDataRecord>(stringBuilder.ToString(), array);
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x00030FE4 File Offset: 0x0002F1E4
		private void AppendEntitySqlRow(StringBuilder queryBuilder, string prefix, InternalPropertyValues templateValues)
		{
			bool flag = false;
			foreach (string text in templateValues.PropertyNames)
			{
				if (flag)
				{
					queryBuilder.Append(", ");
				}
				else
				{
					flag = true;
				}
				string text2 = DbHelpers.QuoteIdentifier(text);
				IPropertyValuesItem item = templateValues.GetItem(text);
				if (item.IsComplex)
				{
					InternalPropertyValues internalPropertyValues = item.Value as InternalPropertyValues;
					if (internalPropertyValues == null)
					{
						throw Error.DbPropertyValues_CannotGetStoreValuesWhenComplexPropertyIsNull(text, this.EntityType.Name);
					}
					queryBuilder.Append("ROW(");
					this.AppendEntitySqlRow(queryBuilder, string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[] { prefix, text2 }), internalPropertyValues);
					queryBuilder.AppendFormat(CultureInfo.InvariantCulture, ") AS {0}", new object[] { text2 });
				}
				else
				{
					queryBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}.{1} ", new object[] { prefix, text2 });
				}
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000310F0 File Offset: 0x0002F2F0
		private void ValidateStateToGetValues(string method, EntityState invalidState)
		{
			this.ValidateNotDetachedAndInitializeRelatedEnd(method);
			if (this.State == invalidState)
			{
				throw Error.DbPropertyValues_CannotGetValuesForState(method, this.State);
			}
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x00031114 File Offset: 0x0002F314
		public virtual void Reload()
		{
			this.ValidateStateToGetValues("Reload", EntityState.Added);
			this._internalContext.ObjectContext.Refresh(RefreshMode.StoreWins, this.Entity);
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00031139 File Offset: 0x0002F339
		public virtual Task ReloadAsync(CancellationToken cancellationToken)
		{
			this.ValidateStateToGetValues("ReloadAsync", EntityState.Added);
			return this._internalContext.ObjectContext.RefreshAsync(RefreshMode.StoreWins, this.Entity, cancellationToken);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0003115F File Offset: 0x0002F35F
		public virtual InternalReferenceEntry Reference(string navigationProperty, Type requestedType = null)
		{
			return (InternalReferenceEntry)this.ValidateAndGetNavigationMetadata(navigationProperty, requestedType ?? typeof(object), false).CreateMemberEntry(this, null);
		}

		// Token: 0x060012B3 RID: 4787 RVA: 0x00031184 File Offset: 0x0002F384
		public virtual InternalCollectionEntry Collection(string navigationProperty, Type requestedType = null)
		{
			return (InternalCollectionEntry)this.ValidateAndGetNavigationMetadata(navigationProperty, requestedType ?? typeof(object), true).CreateMemberEntry(this, null);
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x000311AC File Offset: 0x0002F3AC
		public virtual InternalMemberEntry Member(string propertyName, Type requestedType = null)
		{
			requestedType = requestedType ?? typeof(object);
			IList<string> list = InternalEntityEntry.SplitName(propertyName);
			if (list.Count > 1)
			{
				return this.Property(null, propertyName, list, requestedType, false);
			}
			MemberEntryMetadata memberEntryMetadata = this.GetNavigationMetadata(propertyName) ?? this.ValidateAndGetPropertyMetadata(propertyName, this.EntityType, requestedType);
			if (memberEntryMetadata == null)
			{
				throw Error.DbEntityEntry_NotAProperty(propertyName, this.EntityType.Name);
			}
			if (memberEntryMetadata.MemberEntryType != MemberEntryType.CollectionNavigationProperty && !requestedType.IsAssignableFrom(memberEntryMetadata.MemberType))
			{
				throw Error.DbEntityEntry_WrongGenericForNavProp(propertyName, this.EntityType.Name, requestedType.Name, memberEntryMetadata.MemberType.Name);
			}
			return memberEntryMetadata.CreateMemberEntry(this, null);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x00031257 File Offset: 0x0002F457
		public virtual InternalPropertyEntry Property(string property, Type requestedType = null, bool requireComplex = false)
		{
			return this.Property(null, property, requestedType ?? typeof(object), requireComplex);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00031271 File Offset: 0x0002F471
		public InternalPropertyEntry Property(InternalPropertyEntry parentProperty, string propertyName, Type requestedType, bool requireComplex)
		{
			return this.Property(parentProperty, propertyName, InternalEntityEntry.SplitName(propertyName), requestedType, requireComplex);
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00031284 File Offset: 0x0002F484
		private InternalPropertyEntry Property(InternalPropertyEntry parentProperty, string propertyName, IList<string> properties, Type requestedType, bool requireComplex)
		{
			bool flag = properties.Count > 1;
			Type type = (flag ? typeof(object) : requestedType);
			Type type2 = ((parentProperty != null) ? parentProperty.EntryMetadata.ElementType : this.EntityType);
			PropertyEntryMetadata propertyEntryMetadata = this.ValidateAndGetPropertyMetadata(properties[0], type2, type);
			if (propertyEntryMetadata == null || ((flag || requireComplex) && !propertyEntryMetadata.IsComplex))
			{
				if (flag)
				{
					throw Error.DbEntityEntry_DottedPartNotComplex(properties[0], propertyName, type2.Name);
				}
				throw requireComplex ? Error.DbEntityEntry_NotAComplexProperty(properties[0], type2.Name) : Error.DbEntityEntry_NotAScalarProperty(properties[0], type2.Name);
			}
			else
			{
				InternalPropertyEntry internalPropertyEntry = (InternalPropertyEntry)propertyEntryMetadata.CreateMemberEntry(this, parentProperty);
				if (!flag)
				{
					return internalPropertyEntry;
				}
				return this.Property(internalPropertyEntry, propertyName, properties.Skip(1).ToList<string>(), requestedType, requireComplex);
			}
		}

		// Token: 0x060012B8 RID: 4792 RVA: 0x00031358 File Offset: 0x0002F558
		private NavigationEntryMetadata ValidateAndGetNavigationMetadata(string navigationProperty, Type requestedType, bool requireCollection)
		{
			if (InternalEntityEntry.SplitName(navigationProperty).Count != 1)
			{
				throw Error.DbEntityEntry_DottedPathMustBeProperty(navigationProperty);
			}
			NavigationEntryMetadata navigationMetadata = this.GetNavigationMetadata(navigationProperty);
			if (navigationMetadata == null)
			{
				throw Error.DbEntityEntry_NotANavigationProperty(navigationProperty, this.EntityType.Name);
			}
			if (requireCollection)
			{
				if (navigationMetadata.MemberEntryType == MemberEntryType.ReferenceNavigationProperty)
				{
					throw Error.DbEntityEntry_UsedCollectionForReferenceProp(navigationProperty, this.EntityType.Name);
				}
			}
			else if (navigationMetadata.MemberEntryType == MemberEntryType.CollectionNavigationProperty)
			{
				throw Error.DbEntityEntry_UsedReferenceForCollectionProp(navigationProperty, this.EntityType.Name);
			}
			if (!requestedType.IsAssignableFrom(navigationMetadata.ElementType))
			{
				throw Error.DbEntityEntry_WrongGenericForNavProp(navigationProperty, this.EntityType.Name, requestedType.Name, navigationMetadata.ElementType.Name);
			}
			return navigationMetadata;
		}

		// Token: 0x060012B9 RID: 4793 RVA: 0x00031404 File Offset: 0x0002F604
		public virtual NavigationEntryMetadata GetNavigationMetadata(string propertyName)
		{
			EdmMember edmMember;
			this.EdmEntityType.Members.TryGetValue(propertyName, false, out edmMember);
			NavigationProperty navigationProperty = edmMember as NavigationProperty;
			if (navigationProperty != null)
			{
				return new NavigationEntryMetadata(this.EntityType, this.GetNavigationTargetType(navigationProperty), propertyName, navigationProperty.ToEndMember.RelationshipMultiplicity == RelationshipMultiplicity.Many);
			}
			return null;
		}

		// Token: 0x060012BA RID: 4794 RVA: 0x00031454 File Offset: 0x0002F654
		private Type GetNavigationTargetType(NavigationProperty navigationProperty)
		{
			MetadataWorkspace metadataWorkspace = this._internalContext.ObjectContext.MetadataWorkspace;
			EntityType entityType = navigationProperty.RelationshipType.RelationshipEndMembers.Single((RelationshipEndMember e) => navigationProperty.ToEndMember.Name == e.Name).GetEntityType();
			StructuralType objectSpaceType = metadataWorkspace.GetObjectSpaceType(entityType);
			return ((ObjectItemCollection)metadataWorkspace.GetItemCollection(DataSpace.OSpace)).GetClrType(objectSpaceType);
		}

		// Token: 0x060012BB RID: 4795 RVA: 0x000314C0 File Offset: 0x0002F6C0
		public virtual IRelatedEnd GetRelatedEnd(string navigationProperty)
		{
			EdmMember edmMember;
			this.EdmEntityType.Members.TryGetValue(navigationProperty, false, out edmMember);
			NavigationProperty navigationProperty2 = (NavigationProperty)edmMember;
			return this._internalContext.ObjectContext.ObjectStateManager.GetRelationshipManager(this.Entity).GetRelatedEnd(navigationProperty2.RelationshipType.FullName, navigationProperty2.ToEndMember.Name);
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x0003151F File Offset: 0x0002F71F
		public virtual PropertyEntryMetadata ValidateAndGetPropertyMetadata(string propertyName, Type declaringType, Type requestedType)
		{
			return PropertyEntryMetadata.ValidateNameAndGetMetadata(this._internalContext, declaringType, requestedType, propertyName);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x0003152F File Offset: 0x0002F72F
		private static IList<string> SplitName(string propertyName)
		{
			return propertyName.Split(new char[] { '.' });
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x00031542 File Offset: 0x0002F742
		private void ValidateNotDetachedAndInitializeRelatedEnd(string method)
		{
			if (this.IsDetached)
			{
				throw Error.DbEntityEntry_NotSupportedForDetached(method, this._entityType.Name);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0003155E File Offset: 0x0002F75E
		public virtual bool IsDetached
		{
			get
			{
				if (this._stateEntry == null || this._stateEntry.State == EntityState.Detached)
				{
					this._stateEntry = this._internalContext.GetStateEntry(this._entity);
					if (this._stateEntry == null)
					{
						return true;
					}
				}
				return false;
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x00031598 File Offset: 0x0002F798
		public virtual Type EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x000315A0 File Offset: 0x0002F7A0
		public virtual EntityType EdmEntityType
		{
			get
			{
				if (this._edmEntityType == null)
				{
					MetadataWorkspace metadataWorkspace = this._internalContext.ObjectContext.MetadataWorkspace;
					EntityType item = metadataWorkspace.GetItem<EntityType>(this._entityType.FullNameWithNesting(), DataSpace.OSpace);
					this._edmEntityType = (EntityType)metadataWorkspace.GetEdmSpaceType(item);
				}
				return this._edmEntityType;
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x000315F1 File Offset: 0x0002F7F1
		public IEntityStateEntry ObjectStateEntry
		{
			get
			{
				return this._stateEntry;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x000315F9 File Offset: 0x0002F7F9
		public InternalContext InternalContext
		{
			get
			{
				return this._internalContext;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x00031604 File Offset: 0x0002F804
		public virtual DbEntityValidationResult GetValidationResult(IDictionary<object, object> items)
		{
			EntityValidator entityValidator = this.InternalContext.ValidationProvider.GetEntityValidator(this);
			bool lazyLoadingEnabled = this.InternalContext.LazyLoadingEnabled;
			this.InternalContext.LazyLoadingEnabled = false;
			DbEntityValidationResult dbEntityValidationResult;
			try
			{
				dbEntityValidationResult = ((entityValidator != null) ? entityValidator.Validate(this.InternalContext.ValidationProvider.GetEntityValidationContext(this, items)) : new DbEntityValidationResult(this, Enumerable.Empty<DbValidationError>()));
			}
			finally
			{
				this.InternalContext.LazyLoadingEnabled = lazyLoadingEnabled;
			}
			return dbEntityValidationResult;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x00031684 File Offset: 0x0002F884
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != typeof(InternalEntityEntry)) && this.Equals((InternalEntityEntry)obj);
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000316AE File Offset: 0x0002F8AE
		public bool Equals(InternalEntityEntry other)
		{
			return this == other || (other != null && this._entity == other._entity && this._internalContext == other._internalContext);
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x000316D7 File Offset: 0x0002F8D7
		public override int GetHashCode()
		{
			return RuntimeHelpers.GetHashCode(this._entity);
		}

		// Token: 0x0400092C RID: 2348
		private readonly Type _entityType;

		// Token: 0x0400092D RID: 2349
		private readonly InternalContext _internalContext;

		// Token: 0x0400092E RID: 2350
		private readonly object _entity;

		// Token: 0x0400092F RID: 2351
		private IEntityStateEntry _stateEntry;

		// Token: 0x04000930 RID: 2352
		private EntityType _edmEntityType;
	}
}
