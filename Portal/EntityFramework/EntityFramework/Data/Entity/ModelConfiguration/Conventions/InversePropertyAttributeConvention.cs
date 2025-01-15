using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Mappers;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x0200018F RID: 399
	public class InversePropertyAttributeConvention : PropertyAttributeConfigurationConvention<InversePropertyAttribute>
	{
		// Token: 0x0600172A RID: 5930 RVA: 0x0003DAD0 File Offset: 0x0003BCD0
		public override void Apply(PropertyInfo memberInfo, ConventionTypeConfiguration configuration, InversePropertyAttribute attribute)
		{
			Check.NotNull<PropertyInfo>(memberInfo, "memberInfo");
			Check.NotNull<ConventionTypeConfiguration>(configuration, "configuration");
			Check.NotNull<InversePropertyAttribute>(attribute, "attribute");
			if (!memberInfo.IsValidEdmNavigationProperty())
			{
				return;
			}
			Type targetType = memberInfo.PropertyType.GetTargetType();
			PropertyInfo inverseNavigationProperty = new PropertyFilter(DbModelBuilderVersion.Latest).GetProperties(targetType, false, null, null, false).SingleOrDefault((PropertyInfo p) => string.Equals(p.Name, attribute.Property, StringComparison.OrdinalIgnoreCase));
			if (inverseNavigationProperty == null)
			{
				throw Error.InversePropertyAttributeConvention_PropertyNotFound(attribute.Property, targetType, memberInfo.Name, memberInfo.ReflectedType);
			}
			if (memberInfo == inverseNavigationProperty)
			{
				throw Error.InversePropertyAttributeConvention_SelfInverseDetected(memberInfo.Name, memberInfo.ReflectedType);
			}
			configuration.NavigationProperty(memberInfo).HasInverseNavigationProperty((PropertyInfo p) => inverseNavigationProperty);
		}
	}
}
