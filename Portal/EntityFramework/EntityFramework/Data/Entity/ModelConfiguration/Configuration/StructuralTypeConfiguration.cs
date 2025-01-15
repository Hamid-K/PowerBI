using System;
using System.ComponentModel;
using System.Data.Entity.Hierarchy;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Spatial;
using System.Linq.Expressions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001FC RID: 508
	public abstract class StructuralTypeConfiguration<TStructuralType> where TStructuralType : class
	{
		// Token: 0x06001ABD RID: 6845 RVA: 0x000486DD File Offset: 0x000468DD
		public PrimitivePropertyConfiguration Property<T>(Expression<Func<TStructuralType, T>> propertyExpression) where T : struct
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001ABE RID: 6846 RVA: 0x000486EB File Offset: 0x000468EB
		public PrimitivePropertyConfiguration Property<T>(Expression<Func<TStructuralType, T?>> propertyExpression) where T : struct
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001ABF RID: 6847 RVA: 0x000486F9 File Offset: 0x000468F9
		public PrimitivePropertyConfiguration Property(Expression<Func<TStructuralType, HierarchyId>> propertyExpression)
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC0 RID: 6848 RVA: 0x00048707 File Offset: 0x00046907
		public PrimitivePropertyConfiguration Property(Expression<Func<TStructuralType, DbGeometry>> propertyExpression)
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC1 RID: 6849 RVA: 0x00048715 File Offset: 0x00046915
		public PrimitivePropertyConfiguration Property(Expression<Func<TStructuralType, DbGeography>> propertyExpression)
		{
			return new PrimitivePropertyConfiguration(this.Property<PrimitivePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC2 RID: 6850 RVA: 0x00048723 File Offset: 0x00046923
		public StringPropertyConfiguration Property(Expression<Func<TStructuralType, string>> propertyExpression)
		{
			return new StringPropertyConfiguration(this.Property<StringPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC3 RID: 6851 RVA: 0x00048731 File Offset: 0x00046931
		public BinaryPropertyConfiguration Property(Expression<Func<TStructuralType, byte[]>> propertyExpression)
		{
			return new BinaryPropertyConfiguration(this.Property<BinaryPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC4 RID: 6852 RVA: 0x0004873F File Offset: 0x0004693F
		public DecimalPropertyConfiguration Property(Expression<Func<TStructuralType, decimal>> propertyExpression)
		{
			return new DecimalPropertyConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC5 RID: 6853 RVA: 0x0004874D File Offset: 0x0004694D
		public DecimalPropertyConfiguration Property(Expression<Func<TStructuralType, decimal?>> propertyExpression)
		{
			return new DecimalPropertyConfiguration(this.Property<DecimalPropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x0004875B File Offset: 0x0004695B
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTime>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00048769 File Offset: 0x00046969
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTime?>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00048777 File Offset: 0x00046977
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTimeOffset>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00048785 File Offset: 0x00046985
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, DateTimeOffset?>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00048793 File Offset: 0x00046993
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, TimeSpan>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000487A1 File Offset: 0x000469A1
		public DateTimePropertyConfiguration Property(Expression<Func<TStructuralType, TimeSpan?>> propertyExpression)
		{
			return new DateTimePropertyConfiguration(this.Property<DateTimePropertyConfiguration>(propertyExpression));
		}

		// Token: 0x17000607 RID: 1543
		// (get) Token: 0x06001ACC RID: 6860
		internal abstract StructuralTypeConfiguration Configuration { get; }

		// Token: 0x06001ACD RID: 6861
		internal abstract TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression) where TPrimitivePropertyConfiguration : PrimitivePropertyConfiguration, new();

		// Token: 0x06001ACE RID: 6862 RVA: 0x000487AF File Offset: 0x000469AF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x000487B7 File Offset: 0x000469B7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000487C0 File Offset: 0x000469C0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000487C8 File Offset: 0x000469C8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
