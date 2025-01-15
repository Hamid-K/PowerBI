using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000221 RID: 545
	public class DbComplexPropertyEntry<TEntity, TComplexProperty> : DbPropertyEntry<TEntity, TComplexProperty> where TEntity : class
	{
		// Token: 0x06001C91 RID: 7313 RVA: 0x00051D34 File Offset: 0x0004FF34
		internal new static DbComplexPropertyEntry<TEntity, TComplexProperty> Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbComplexPropertyEntry<TEntity, TComplexProperty>)internalPropertyEntry.CreateDbMemberEntry<TEntity, TComplexProperty>();
		}

		// Token: 0x06001C92 RID: 7314 RVA: 0x00051D41 File Offset: 0x0004FF41
		internal DbComplexPropertyEntry(InternalPropertyEntry internalPropertyEntry)
			: base(internalPropertyEntry)
		{
		}

		// Token: 0x06001C93 RID: 7315 RVA: 0x00051D4A File Offset: 0x0004FF4A
		public static implicit operator DbComplexPropertyEntry(DbComplexPropertyEntry<TEntity, TComplexProperty> entry)
		{
			return DbComplexPropertyEntry.Create(entry.InternalPropertyEntry);
		}

		// Token: 0x06001C94 RID: 7316 RVA: 0x00051D57 File Offset: 0x0004FF57
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(base.InternalPropertyEntry.Property(propertyName, null, false));
		}

		// Token: 0x06001C95 RID: 7317 RVA: 0x00051D78 File Offset: 0x0004FF78
		public DbPropertyEntry<TEntity, TNestedProperty> Property<TNestedProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry<TEntity, TNestedProperty>.Create(base.InternalPropertyEntry.Property(propertyName, typeof(TNestedProperty), false));
		}

		// Token: 0x06001C96 RID: 7318 RVA: 0x00051DA2 File Offset: 0x0004FFA2
		public DbPropertyEntry<TEntity, TNestedProperty> Property<TNestedProperty>(Expression<Func<TComplexProperty, TNestedProperty>> property)
		{
			Check.NotNull<Expression<Func<TComplexProperty, TNestedProperty>>>(property, "property");
			return this.Property<TNestedProperty>(DbHelpers.ParsePropertySelector<TComplexProperty, TNestedProperty>(property, "Property", "property"));
		}

		// Token: 0x06001C97 RID: 7319 RVA: 0x00051DC6 File Offset: 0x0004FFC6
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(base.InternalPropertyEntry.Property(propertyName, null, true));
		}

		// Token: 0x06001C98 RID: 7320 RVA: 0x00051DE7 File Offset: 0x0004FFE7
		public DbComplexPropertyEntry<TEntity, TNestedComplexProperty> ComplexProperty<TNestedComplexProperty>(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry<TEntity, TNestedComplexProperty>.Create(base.InternalPropertyEntry.Property(propertyName, typeof(TNestedComplexProperty), true));
		}

		// Token: 0x06001C99 RID: 7321 RVA: 0x00051E11 File Offset: 0x00050011
		public DbComplexPropertyEntry<TEntity, TNestedComplexProperty> ComplexProperty<TNestedComplexProperty>(Expression<Func<TComplexProperty, TNestedComplexProperty>> property)
		{
			Check.NotNull<Expression<Func<TComplexProperty, TNestedComplexProperty>>>(property, "property");
			return this.ComplexProperty<TNestedComplexProperty>(DbHelpers.ParsePropertySelector<TComplexProperty, TNestedComplexProperty>(property, "Property", "property"));
		}
	}
}
