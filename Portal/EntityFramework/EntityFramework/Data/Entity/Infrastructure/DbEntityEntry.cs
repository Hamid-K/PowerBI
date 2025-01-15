using System;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000226 RID: 550
	public class DbEntityEntry
	{
		// Token: 0x06001CCE RID: 7374 RVA: 0x0005260E File Offset: 0x0005080E
		internal DbEntityEntry(InternalEntityEntry internalEntityEntry)
		{
			this._internalEntityEntry = internalEntityEntry;
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06001CCF RID: 7375 RVA: 0x0005261D File Offset: 0x0005081D
		public object Entity
		{
			get
			{
				return this._internalEntityEntry.Entity;
			}
		}

		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x0005262A File Offset: 0x0005082A
		// (set) Token: 0x06001CD1 RID: 7377 RVA: 0x00052637 File Offset: 0x00050837
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

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00052645 File Offset: 0x00050845
		public DbPropertyValues CurrentValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.CurrentValues);
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00052657 File Offset: 0x00050857
		public DbPropertyValues OriginalValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.OriginalValues);
			}
		}

		// Token: 0x06001CD4 RID: 7380 RVA: 0x0005266C File Offset: 0x0005086C
		public DbPropertyValues GetDatabaseValues()
		{
			InternalPropertyValues databaseValues = this._internalEntityEntry.GetDatabaseValues();
			if (databaseValues != null)
			{
				return new DbPropertyValues(databaseValues);
			}
			return null;
		}

		// Token: 0x06001CD5 RID: 7381 RVA: 0x00052690 File Offset: 0x00050890
		public Task<DbPropertyValues> GetDatabaseValuesAsync()
		{
			return this.GetDatabaseValuesAsync(CancellationToken.None);
		}

		// Token: 0x06001CD6 RID: 7382 RVA: 0x000526A0 File Offset: 0x000508A0
		public async Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
		{
			InternalPropertyValues internalPropertyValues = await this._internalEntityEntry.GetDatabaseValuesAsync(cancellationToken).WithCurrentCulture<InternalPropertyValues>();
			return (internalPropertyValues == null) ? null : new DbPropertyValues(internalPropertyValues);
		}

		// Token: 0x06001CD7 RID: 7383 RVA: 0x000526ED File Offset: 0x000508ED
		public void Reload()
		{
			this._internalEntityEntry.Reload();
		}

		// Token: 0x06001CD8 RID: 7384 RVA: 0x000526FA File Offset: 0x000508FA
		public Task ReloadAsync()
		{
			return this._internalEntityEntry.ReloadAsync(CancellationToken.None);
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0005270C File Offset: 0x0005090C
		public Task ReloadAsync(CancellationToken cancellationToken)
		{
			return this._internalEntityEntry.ReloadAsync(cancellationToken);
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0005271A File Offset: 0x0005091A
		public DbReferenceEntry Reference(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbReferenceEntry.Create(this._internalEntityEntry.Reference(navigationProperty, null));
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0005273A File Offset: 0x0005093A
		public DbCollectionEntry Collection(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbCollectionEntry.Create(this._internalEntityEntry.Collection(navigationProperty, null));
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x0005275A File Offset: 0x0005095A
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, false));
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x0005277B File Offset: 0x0005097B
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, true));
		}

		// Token: 0x06001CDE RID: 7390 RVA: 0x0005279C File Offset: 0x0005099C
		public DbMemberEntry Member(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbMemberEntry.Create(this._internalEntityEntry.Member(propertyName, null));
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x000527BC File Offset: 0x000509BC
		public DbEntityEntry<TEntity> Cast<TEntity>() where TEntity : class
		{
			if (!typeof(TEntity).IsAssignableFrom(this._internalEntityEntry.EntityType))
			{
				throw Error.DbEntity_BadTypeForCast(typeof(DbEntityEntry).Name, typeof(TEntity).Name, this._internalEntityEntry.EntityType.Name);
			}
			return new DbEntityEntry<TEntity>(this._internalEntityEntry);
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x00052824 File Offset: 0x00050A24
		public DbEntityValidationResult GetValidationResult()
		{
			return this._internalEntityEntry.InternalContext.Owner.CallValidateEntity(this);
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x0005283C File Offset: 0x00050A3C
		internal InternalEntityEntry InternalEntry
		{
			get
			{
				return this._internalEntityEntry;
			}
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x00052844 File Offset: 0x00050A44
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return obj != null && !(obj.GetType() != typeof(DbEntityEntry)) && this.Equals((DbEntityEntry)obj);
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0005286E File Offset: 0x00050A6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(DbEntityEntry other)
		{
			return this == other || (other != null && this._internalEntityEntry.Equals(other._internalEntityEntry));
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0005288C File Offset: 0x00050A8C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return this._internalEntityEntry.GetHashCode();
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00052899 File Offset: 0x00050A99
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001CE6 RID: 7398 RVA: 0x000528A1 File Offset: 0x00050AA1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000B10 RID: 2832
		private readonly InternalEntityEntry _internalEntityEntry;
	}
}
