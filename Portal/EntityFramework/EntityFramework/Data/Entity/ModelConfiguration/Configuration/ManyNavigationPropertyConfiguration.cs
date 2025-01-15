using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E7 RID: 487
	public class ManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x060019AE RID: 6574 RVA: 0x000459EF File Offset: 0x00043BEF
		internal ManyNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			navigationPropertyConfiguration.Reset();
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
			this._navigationPropertyConfiguration.RelationshipMultiplicity = new RelationshipMultiplicity?(RelationshipMultiplicity.Many);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x00045A15 File Offset: 0x00043C15
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> WithMany(Expression<Func<TTargetEntityType, ICollection<TEntityType>>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithMany();
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x00045A3F File Offset: 0x00043C3F
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> WithMany()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.Many);
			return new ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x00045A5D File Offset: 0x00043C5D
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithRequired(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithRequired();
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x00045A87 File Offset: 0x00043C87
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithRequired()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
			return new DependentNavigationPropertyConfiguration<TTargetEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x00045AA5 File Offset: 0x00043CA5
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithOptional(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithOptional();
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x00045ACF File Offset: 0x00043CCF
		public DependentNavigationPropertyConfiguration<TTargetEntityType> WithOptional()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
			return new DependentNavigationPropertyConfiguration<TTargetEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x00045AED File Offset: 0x00043CED
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x00045AF5 File Offset: 0x00043CF5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x00045AFE File Offset: 0x00043CFE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060019B8 RID: 6584 RVA: 0x00045B06 File Offset: 0x00043D06
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A83 RID: 2691
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}
