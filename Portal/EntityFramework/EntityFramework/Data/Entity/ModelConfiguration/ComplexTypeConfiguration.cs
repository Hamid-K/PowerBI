using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration
{
	// Token: 0x02000155 RID: 341
	public class ComplexTypeConfiguration<TComplexType> : StructuralTypeConfiguration<TComplexType> where TComplexType : class
	{
		// Token: 0x060015CD RID: 5581 RVA: 0x00038891 File Offset: 0x00036A91
		public ComplexTypeConfiguration()
			: this(new ComplexTypeConfiguration(typeof(TComplexType)))
		{
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000388A8 File Offset: 0x00036AA8
		public ComplexTypeConfiguration<TComplexType> Ignore<TProperty>(Expression<Func<TComplexType, TProperty>> propertyExpression)
		{
			Check.NotNull<Expression<Func<TComplexType, TProperty>>>(propertyExpression, "propertyExpression");
			this.Configuration.Ignore(propertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>());
			return this;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x000388CD File Offset: 0x00036ACD
		internal ComplexTypeConfiguration(ComplexTypeConfiguration configuration)
		{
			this._complexTypeConfiguration = configuration;
		}

		// Token: 0x170005AB RID: 1451
		// (get) Token: 0x060015D0 RID: 5584 RVA: 0x000388DC File Offset: 0x00036ADC
		internal override StructuralTypeConfiguration Configuration
		{
			get
			{
				return this._complexTypeConfiguration;
			}
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x000388E4 File Offset: 0x00036AE4
		internal override TPrimitivePropertyConfiguration Property<TPrimitivePropertyConfiguration>(LambdaExpression lambdaExpression)
		{
			return this.Configuration.Property<TPrimitivePropertyConfiguration>(lambdaExpression.GetSimplePropertyAccess(), delegate
			{
				TPrimitivePropertyConfiguration tprimitivePropertyConfiguration = new TPrimitivePropertyConfiguration();
				tprimitivePropertyConfiguration.OverridableConfigurationParts = OverridableConfigurationParts.OverridableInSSpace;
				return tprimitivePropertyConfiguration;
			});
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00038916 File Offset: 0x00036B16
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x0003891E File Offset: 0x00036B1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00038927 File Offset: 0x00036B27
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x0003892F File Offset: 0x00036B2F
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040009F3 RID: 2547
		private readonly ComplexTypeConfiguration _complexTypeConfiguration;
	}
}
