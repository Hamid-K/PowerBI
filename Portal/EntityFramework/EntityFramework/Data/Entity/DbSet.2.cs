using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x02000063 RID: 99
	public class DbSet<TEntity> : DbQuery<TEntity>, IDbSet<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable, IInternalSetAdapter where TEntity : class
	{
		// Token: 0x06000343 RID: 835 RVA: 0x0000BD20 File Offset: 0x00009F20
		internal DbSet(InternalSet<TEntity> internalSet)
			: base(internalSet)
		{
			this._internalSet = internalSet;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000BD30 File Offset: 0x00009F30
		protected DbSet()
			: this(null)
		{
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000BD39 File Offset: 0x00009F39
		public virtual TEntity Find(params object[] keyValues)
		{
			return this.GetInternalSetWithCheck("Find").Find(keyValues);
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000BD4C File Offset: 0x00009F4C
		public virtual Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			return this.GetInternalSetWithCheck("FindAsync").FindAsync(cancellationToken, keyValues);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000BD60 File Offset: 0x00009F60
		public virtual Task<TEntity> FindAsync(params object[] keyValues)
		{
			return this.FindAsync(CancellationToken.None, keyValues);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000348 RID: 840 RVA: 0x0000BD6E File Offset: 0x00009F6E
		public virtual ObservableCollection<TEntity> Local
		{
			get
			{
				return this.GetInternalSetWithCheck("Local").Local;
			}
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000BD80 File Offset: 0x00009F80
		public virtual TEntity Attach(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.GetInternalSetWithCheck("Attach").Attach(entity);
			return entity;
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BDA5 File Offset: 0x00009FA5
		public virtual TEntity Add(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.GetInternalSetWithCheck("Add").Add(entity);
			return entity;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000BDCA File Offset: 0x00009FCA
		public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
		{
			Check.NotNull<IEnumerable<TEntity>>(entities, "entities");
			this.GetInternalSetWithCheck("AddRange").AddRange(entities);
			return entities;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000BDEA File Offset: 0x00009FEA
		public virtual TEntity Remove(TEntity entity)
		{
			Check.NotNull<TEntity>(entity, "entity");
			this.GetInternalSetWithCheck("Remove").Remove(entity);
			return entity;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000BE0F File Offset: 0x0000A00F
		public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
		{
			Check.NotNull<IEnumerable<TEntity>>(entities, "entities");
			this.GetInternalSetWithCheck("RemoveRange").RemoveRange(entities);
			return entities;
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000BE2F File Offset: 0x0000A02F
		public virtual TEntity Create()
		{
			return this.GetInternalSetWithCheck("Create").Create();
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000BE41 File Offset: 0x0000A041
		public virtual TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity
		{
			return (TDerivedEntity)((object)this.GetInternalSetWithCheck("Create").Create(typeof(TDerivedEntity)));
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000BE68 File Offset: 0x0000A068
		public static implicit operator DbSet(DbSet<TEntity> entry)
		{
			Check.NotNull<DbSet<TEntity>>(entry, "entry");
			if (entry._internalSet == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			return (DbSet)entry._internalSet.InternalContext.Set(entry._internalSet.ElementType);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		IInternalSet IInternalSetAdapter.InternalSet
		{
			get
			{
				return this._internalSet;
			}
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000BEBC File Offset: 0x0000A0BC
		private InternalSet<TEntity> GetInternalSetWithCheck(string memberName)
		{
			if (this._internalSet == null)
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet<>).Name));
			}
			return this._internalSet;
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000BEF2 File Offset: 0x0000A0F2
		public virtual DbSqlQuery<TEntity> SqlQuery(string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbSqlQuery<TEntity>((this._internalSet != null) ? new InternalSqlSetQuery(this._internalSet, sql, false, parameters) : null);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000BF2A File Offset: 0x0000A12A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000BF33 File Offset: 0x0000A133
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000BF3B File Offset: 0x0000A13B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040000C3 RID: 195
		private readonly InternalSet<TEntity> _internalSet;
	}
}
