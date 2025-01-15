using System;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Conventions.Sets
{
	// Token: 0x020001BB RID: 443
	internal static class V1ConventionSet
	{
		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0004036B File Offset: 0x0003E56B
		public static ConventionSet Conventions
		{
			get
			{
				return V1ConventionSet._conventions;
			}
		}

		// Token: 0x04000A40 RID: 2624
		private static readonly ConventionSet _conventions = new ConventionSet(new IConvention[]
		{
			new NotMappedTypeAttributeConvention(),
			new ComplexTypeAttributeConvention(),
			new TableAttributeConvention(),
			new NotMappedPropertyAttributeConvention(),
			new KeyAttributeConvention(),
			new RequiredPrimitivePropertyAttributeConvention(),
			new RequiredNavigationPropertyAttributeConvention(),
			new TimestampAttributeConvention(),
			new ConcurrencyCheckAttributeConvention(),
			new DatabaseGeneratedAttributeConvention(),
			new MaxLengthAttributeConvention(),
			new StringLengthAttributeConvention(),
			new ColumnAttributeConvention(),
			new IndexAttributeConvention(),
			new InversePropertyAttributeConvention(),
			new ForeignKeyPrimitivePropertyAttributeConvention()
		}.Reverse<IConvention>(), new IConvention[]
		{
			new IdKeyDiscoveryConvention(),
			new AssociationInverseDiscoveryConvention(),
			new ForeignKeyNavigationPropertyAttributeConvention(),
			new OneToOneConstraintIntroductionConvention(),
			new NavigationPropertyNameForeignKeyDiscoveryConvention(),
			new PrimaryKeyNameForeignKeyDiscoveryConvention(),
			new TypeNameForeignKeyDiscoveryConvention(),
			new ForeignKeyAssociationMultiplicityConvention(),
			new OneToManyCascadeDeleteConvention(),
			new ComplexTypeDiscoveryConvention(),
			new StoreGeneratedIdentityKeyConvention(),
			new PluralizingEntitySetNameConvention(),
			new DeclaredPropertyOrderingConvention(),
			new SqlCePropertyMaxLengthConvention(),
			new PropertyMaxLengthConvention(),
			new DecimalPropertyConvention()
		}, new IConvention[]
		{
			new ManyToManyCascadeDeleteConvention(),
			new MappingInheritedPropertiesSupportConvention()
		}, new IConvention[]
		{
			new PluralizingTableNameConvention(),
			new ColumnOrderingConvention(),
			new ForeignKeyIndexConvention()
		});
	}
}
