using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Mappers;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200018D RID: 397
	public class ForeignKeyPrimitivePropertyAttributeConvention : PropertyAttributeConfigurationConvention<ForeignKeyAttribute>
	{
		// Token: 0x06001727 RID: 5927 RVA: 0x0003D9D0 File Offset: 0x0003BBD0
		public override void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, ForeignKeyAttribute attribute)
		{
			Check.NotNull<PropertyInfo>(memberInfo, "memberInfo");
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<ForeignKeyAttribute>(attribute, "attribute");
			if (memberInfo.IsValidEdmScalarProperty())
			{
				PropertyInfo propertyInfo = (from pi in new PropertyFilter(DbModelBuilderVersion.Latest).GetProperties(configuration.ClrType, false, null, null, false)
					where pi.Name.Equals(attribute.Name, StringComparison.Ordinal)
					select pi).SingleOrDefault<PropertyInfo>();
				if (propertyInfo == null)
				{
					throw Error.ForeignKeyAttributeConvention_InvalidNavigationProperty(memberInfo.Name, configuration.ClrType, attribute.Name);
				}
				configuration.NavigationProperty(propertyInfo).HasConstraint<ForeignKeyConstraintConfiguration>(delegate(ForeignKeyConstraintConfiguration fk)
				{
					fk.AddColumn(memberInfo);
				});
			}
		}
	}
}
