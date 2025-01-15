using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Conventions
{
	// Token: 0x02000190 RID: 400
	public class KeyAttributeConvention : Convention
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x0003DBBC File Offset: 0x0003BDBC
		internal override void ApplyPropertyTypeConfiguration<TStructuralTypeConfiguration>(PropertyInfo propertyInfo, Func<TStructuralTypeConfiguration> structuralTypeConfiguration, ModelConfiguration modelConfiguration)
		{
			if (typeof(TStructuralTypeConfiguration) == typeof(EntityTypeConfiguration) && this._attributeProvider.GetAttributes(propertyInfo).OfType<KeyAttribute>().Any<KeyAttribute>())
			{
				EntityTypeConfiguration entityTypeConfiguration = (EntityTypeConfiguration)((object)structuralTypeConfiguration());
				if (propertyInfo.IsValidEdmScalarProperty())
				{
					entityTypeConfiguration.Key(propertyInfo);
				}
			}
		}

		// Token: 0x04000A2B RID: 2603
		private readonly AttributeProvider _attributeProvider = DbConfiguration.DependencyResolver.GetService<AttributeProvider>();
	}
}
