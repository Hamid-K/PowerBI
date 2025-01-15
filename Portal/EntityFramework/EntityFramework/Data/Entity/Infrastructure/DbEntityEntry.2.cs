using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000227 RID: 551
	public class DbEntityEntry<TEntity> where TEntity : class
	{
		// Token: 0x06001CE7 RID: 7399 RVA: 0x000528A9 File Offset: 0x00050AA9
		internal DbEntityEntry(InternalEntityEntry internalEntityEntry)
		{
			this._internalEntityEntry = internalEntityEntry;
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06001CE8 RID: 7400 RVA: 0x000528B8 File Offset: 0x00050AB8
		public TEntity Entity
		{
			get
			{
				return (TEntity)((object)this._internalEntityEntry.Entity);
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06001CE9 RID: 7401 RVA: 0x000528CA File Offset: 0x00050ACA
		// (set) Token: 0x06001CEA RID: 7402 RVA: 0x000528D7 File Offset: 0x00050AD7
		public EntityState State
		{
			get
			{
				return this._internalEntityEntry.State;
			}
			set
			{
				this._internalEntityEntry.State = value;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001CEB RID: 7403 RVA: 0x000528E5 File Offset: 0x00050AE5
		public DbPropertyValues CurrentValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.CurrentValues);
			}
		}

		// Token: 0x17000676 RID: 1654
		// (get) Token: 0x06001CEC RID: 7404 RVA: 0x000528F7 File Offset: 0x00050AF7
		public DbPropertyValues OriginalValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.OriginalValues);
			}
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0005290C File Offset: 0x00050B0C
		public DbPropertyValues GetDatabaseValues()
		{
			InternalPropertyValues databaseValues = this._internalEntityEntry.GetDatabaseValues();
			if (databaseValues != null)
			{
				return new DbPropertyValues(databaseValues);
			}
			return null;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x00052930 File Offset: 0x00050B30
		public Task<DbPropertyValues> GetDatabaseValuesAsync()
		{
			return this.GetDatabaseValuesAsync(CancellationToken.None);
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x00052940 File Offset: 0x00050B40
		public async Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
		{
			InternalPropertyValues internalPropertyValues = await this._internalEntityEntry.GetDatabaseValuesAsync(cancellationToken).WithCurrentCulture<InternalPropertyValues>();
			return (internalPropertyValues == null) ? null : new DbPropertyValues(internalPropertyValues);
		}

		// Token: 0x06001CF0 RID: 7408 RVA: 0x0005298D File Offset: 0x00050B8D
		public void Reload()
		{
			this._internalEntityEntry.Reload();
		}

		// Token: 0x06001CF1 RID: 7409 RVA: 0x0005299A File Offset: 0x00050B9A
		public Task ReloadAsync()
		{
			return this._internalEntityEntry.ReloadAsync(CancellationToken.None);
		}

		// Token: 0x06001CF2 RID: 7410 RVA: 0x000529AC File Offset: 0x00050BAC
		public Task ReloadAsync(CancellationToken cancellationToken)
		{
			return this._internalEntityEntry.ReloadAsync(cancellationToken);
		}

		// Token: 0x06001CF3 RID: 7411 RVA: 0x000529BA File Offset: 0x00050BBA
		public DbReferenceEntry Reference(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbReferenceEntry.Create(this._internalEntityEntry.Reference(navigationProperty, null));
		}

		// Token: 0x06001CF4 RID: 7412 RVA: 0x000529DA File Offset: 0x00050BDA
		public DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(string navigationProperty) where TProperty : class
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbReferenceEntry<TEntity, TProperty>.Create(this._internalEntityEntry.Reference(navigationProperty, typeof(TProperty)));
		}

		// Token: 0x06001CF5 RID: 7413 RVA: 0x00052A03 File Offset: 0x00050C03
		public DbReferenceEntry<TEntity, TProperty> Reference<TProperty>(Expression<Func<TEntity, TProperty>> navigationProperty) where TProperty : class
		{
			Check.NotNull<Expression<Func<TEntity, TProperty>>>(navigationProperty, "navigationProperty");
			return DbReferenceEntry<TEntity, TProperty>.Create(this._internalEntityEntry.Reference(DbHelpers.ParsePropertySelector<TEntity, TProperty>(navigationProperty, "Reference", "navigationProperty"), typeof(TProperty)));
		}

		// Token: 0x06001CF6 RID: 7414 RVA: 0x00052A3B File Offset: 0x00050C3B
		public DbCollectionEntry Collection(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbCollectionEntry.Create(this._internalEntityEntry.Collection(navigationProperty, null));
		}

		// Token: 0x06001CF7 RID: 7415 RVA: 0x00052A5B File Offset: 0x00050C5B
		public DbCollectionEntry<TEntity, TElement> Collection<TElement>(string navigationProperty) where TElement : class
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbCollectionEntry<TEntity, TElement>.Create(this._internalEntityEntry.Collection(navigationProperty, typeof(TElement)));
		}

		// Token: 0x06001CF8 RID: 7416 RVA: 0x00052A84 File Offset: 0x00050C84
		public DbCollectionEntry<TEntity, TElement> Collection<TElement>(Expression<Func<TEntity, ICollection<TElement>>> navigationProperty) where TElement : class
		{
			Check.NotNull<Expression<Func<TEntity, ICollection<TElement>>>>(navigationProperty, "navigationProperty");
			return this.Collection<TElement>(DbHelpers.ParsePropertySelector<TEntity, ICollection<TElement>>(navigationProperty, "Collection", "navigationProperty"));
		}

		// Token: 0x06001CF9 RID: 7417 RVA: 0x00052AA8 File Offset: 0x00050CA8
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, false));
		}

		// Token: 0x06001CFA RID: 7418 RVA: 0x00052AC9 File Offset: 0x00050CC9
		public DbPropertyEntry<TEntity, TProperty> Property<TProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry<TEntity, TProperty>.Create(this._internalEntityEntry.Property(propertyName, typeof(TProperty), false));
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x00052AF3 File Offset: 0x00050CF3
		public DbPropertyEntry<TEntity, TProperty> Property<TProperty>(Expression<Func<TEntity, TProperty>> property)
		{
			Check.NotNull<Expression<Func<TEntity, TProperty>>>(property, "property");
			return this.Property<TProperty>(DbHelpers.ParsePropertySelector<TEntity, TProperty>(property, "Property", "property"));
		}

		// Token: 0x06001CFC RID: 7420 RVA: 0x00052B17 File Offset: 0x00050D17
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, true));
		}

		// Token: 0x06001CFD RID: 7421 RVA: 0x00052B38 File Offset: 0x00050D38
		public DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry<TEntity, TComplexProperty>.Create(this._internalEntityEntry.Property(propertyName, typeof(TComplexProperty), true));
		}

		// Token: 0x06001CFE RID: 7422 RVA: 0x00052B62 File Offset: 0x00050D62
		public DbComplexPropertyEntry<TEntity, TComplexProperty> ComplexProperty<TComplexProperty>(Expression<Func<TEntity, TComplexProperty>> property)
		{
			Check.NotNull<Expression<Func<TEntity, TComplexProperty>>>(property, "property");
			return this.ComplexProperty<TComplexProperty>(DbHelpers.ParsePropertySelector<TEntity, TComplexProperty>(property, "Property", "property"));
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x00052B86 File Offset: 0x00050D86
		public DbMemberEntry Member(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbMemberEntry.Create(this._internalEntityEntry.Member(propertyName, null));
		}

		// Token: 0x06001D00 RID: 7424 RVA: 0x00052BA6 File Offset: 0x00050DA6
		public DbMemberEntry<TEntity, TMember> Member<TMember>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return this._internalEntityEntry.Member(propertyName, typeof(TMember)).CreateDbMemberEntry<TEntity, TMember>();
		}

		// Token: 0x06001D01 RID: 7425 RVA: 0x00052BCF File Offset: 0x00050DCF
		public static implicit operator DbEntityEntry(DbEntityEntry<TEntity> entry)
		{
			return new DbEntityEntry(entry._internalEntityEntry);
		}

		// Token: 0x06001D02 RID: 7426 RVA: 0x00052BDC File Offset: 0x00050DDC
		public DbEntityValidationResult GetValidationResult()
		{
			return this._internalEntityEntry.InternalContext.Owner.CallValidateEntity(this);
		}

		// Token: 0x06001D03 RID: 7427 RVA: 0x00052BF9 File Offset: 0x00050DF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != typeof(DbEntityEntry<TEntity>)) && this.Equals((DbEntityEntry<TEntity>)obj);
		}

		// Token: 0x06001D04 RID: 7428 RVA: 0x00052C23 File Offset: 0x00050E23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(DbEntityEntry<TEntity> other)
		{
			return this == other || (other != null && this._internalEntityEntry.Equals(other._internalEntityEntry));
		}

		// Token: 0x06001D05 RID: 7429 RVA: 0x00052C41 File Offset: 0x00050E41
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return this._internalEntityEntry.GetHashCode();
		}

		// Token: 0x06001D06 RID: 7430 RVA: 0x00052C4E File Offset: 0x00050E4E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001D07 RID: 7431 RVA: 0x00052C56 File Offset: 0x00050E56
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B11 RID: 2833
		private readonly InternalEntityEntry _internalEntityEntry;
	}
}
