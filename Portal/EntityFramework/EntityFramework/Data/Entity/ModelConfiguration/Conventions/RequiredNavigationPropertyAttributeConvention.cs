using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Properties;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000195 RID: 405
	public class RequiredNavigationPropertyAttributeConvention : Convention
	{
		// Token: 0x06001739 RID: 5945 RVA: 0x0003DE44 File Offset: 0x0003C044
		internal override void ApplyPropertyConfiguration(PropertyInfo propertyInfo, Func<PropertyConfiguration> propertyConfiguration, ModelConfiguration modelConfiguration)
		{
			if (propertyInfo.IsValidEdmNavigationProperty() && !propertyInfo.PropertyType.IsCollection() && this._attributeProvider.GetAttributes(propertyInfo).OfType<RequiredAttribute>().Any<RequiredAttribute>())
			{
				NavigationPropertyConfiguration navigationPropertyConfiguration = (NavigationPropertyConfiguration)propertyConfiguration();
				if (navigationPropertyConfiguration.RelationshipMultiplicity == null)
				{
					navigationPropertyConfiguration.RelationshipMultiplicity = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
				}
			}
		}

		// Token: 0x04000A2F RID: 2607
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
