using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Internal.Linq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity
{
	// Token: 0x02000062 RID: 98
	public abstract class DbSet : DbQuery, IInternalSetAdapter
	{
		// Token: 0x0600032F RID: 815 RVA: 0x0000BA88 File Offset: 0x00009C88
		protected internal DbSet()
		{
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000BA90 File Offset: 0x00009C90
		public virtual object Find(params object[] keyValues)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("Find", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000BABB File Offset: 0x00009CBB
		public virtual Task<object> FindAsync(params object[] keyValues)
		{
			return this.FindAsync(CancellationToken.None, keyValues);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000BAC9 File Offset: 0x00009CC9
		public virtual Task<object> FindAsync(CancellationToken cancellationToken, params object[] keyValues)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("FindAsync", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000BAF4 File Offset: 0x00009CF4
		public virtual IList Local
		{
			get
			{
				throw new NotImplementedException(Strings.TestDoubleNotImplemented("Local", this.GetType().Name, typeof(DbSet).Name));
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000BB1F File Offset: 0x00009D1F
		public virtual object Attach(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.GetInternalSetWithCheck("Attach").Attach(entity);
			return entity;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000BB3F File Offset: 0x00009D3F
		public virtual object Add(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.GetInternalSetWithCheck("Add").Add(entity);
			return entity;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000BB5F File Offset: 0x00009D5F
		public virtual IEnumerable AddRange(IEnumerable entities)
		{
			Check.NotNull<IEnumerable>(entities, "entities");
			this.GetInternalSetWithCheck("AddRange").AddRange(entities);
			return entities;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000BB7F File Offset: 0x00009D7F
		public virtual object Remove(object entity)
		{
			Check.NotNull<object>(entity, "entity");
			this.GetInternalSetWithCheck("Remove").Remove(entity);
			return entity;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000BB9F File Offset: 0x00009D9F
		public virtual IEnumerable RemoveRange(IEnumerable entities)
		{
			Check.NotNull<IEnumerable>(entities, "entities");
			this.GetInternalSetWithCheck("RemoveRange").RemoveRange(entities);
			return entities;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000BBBF File Offset: 0x00009DBF
		public virtual object Create()
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("Create", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000BBEA File Offset: 0x00009DEA
		public virtual object Create(Type derivedEntityType)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented("Create", this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000BC18 File Offset: 0x00009E18
		public new DbSet<TEntity> Cast<TEntity>() where TEntity : class
		{
			if (this.InternalSet == null)
			{
				throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
			}
			if (typeof(TEntity) != this.InternalSet.ElementType)
			{
				throw Error.DbEntity_BadTypeForCast(typeof(DbSet).Name, typeof(TEntity).Name, this.InternalSet.ElementType.Name);
			}
			return (DbSet<TEntity>)this.InternalSet.InternalContext.Set<TEntity>();
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600033C RID: 828 RVA: 0x0000BC9D File Offset: 0x00009E9D
		IInternalSet IInternalSetAdapter.InternalSet
		{
			get
			{
				return this.InternalSet;
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000BCA5 File Offset: 0x00009EA5
		internal virtual IInternalSet InternalSet
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BCA8 File Offset: 0x00009EA8
		internal virtual IInternalSet GetInternalSetWithCheck(string memberName)
		{
			throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, this.GetType().Name, typeof(DbSet).Name));
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BCCF File Offset: 0x00009ECF
		public virtual DbSqlQuery SqlQuery(string sql, params object[] parameters)
		{
			Check.NotEmpty(sql, "sql");
			Check.NotNull<object[]>(parameters, "parameters");
			return new DbSqlQuery((this.InternalSet == null) ? null : new InternalSqlSetQuery(this.InternalSet, sql, false, parameters));
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BD07 File Offset: 0x00009F07
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BD10 File Offset: 0x00009F10
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000BD18 File Offset: 0x00009F18
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
