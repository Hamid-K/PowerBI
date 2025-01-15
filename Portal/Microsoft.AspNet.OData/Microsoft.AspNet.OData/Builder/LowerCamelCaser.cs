using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200011B RID: 283
	public class LowerCamelCaser
	{
		// Token: 0x060009C3 RID: 2499 RVA: 0x000286A7 File Offset: 0x000268A7
		public LowerCamelCaser()
			: this(NameResolverOptions.ProcessReflectedPropertyNames | NameResolverOptions.ProcessDataMemberAttributePropertyNames | NameResolverOptions.ProcessExplicitPropertyNames)
		{
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000286B0 File Offset: 0x000268B0
		public LowerCamelCaser(NameResolverOptions options)
		{
			this._options = options;
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000286C0 File Offset: 0x000268C0
		public void ApplyLowerCamelCase(ODataConventionModelBuilder builder)
		{
			foreach (StructuralTypeConfiguration structuralTypeConfiguration in builder.StructuralTypes)
			{
				foreach (PropertyConfiguration propertyConfiguration in structuralTypeConfiguration.Properties)
				{
					if (this.ShouldApplyLowerCamelCase(propertyConfiguration))
					{
						propertyConfiguration.Name = this.ToLowerCamelCase(propertyConfiguration.Name);
					}
				}
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x00028754 File Offset: 0x00026954
		public virtual string ToLowerCamelCase(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return name;
			}
			if (!char.IsUpper(name[0]))
			{
				return name;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < name.Length; i++)
			{
				if (i != 0 && i + 1 < name.Length && !char.IsUpper(name[i + 1]))
				{
					stringBuilder.Append(name.Substring(i));
					break;
				}
				stringBuilder.Append(char.ToLowerInvariant(name[i]));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060009C7 RID: 2503 RVA: 0x000287DC File Offset: 0x000269DC
		private bool ShouldApplyLowerCamelCase(PropertyConfiguration property)
		{
			if (property.AddedExplicitly)
			{
				return this._options.HasFlag(NameResolverOptions.ProcessExplicitPropertyNames);
			}
			DataMemberAttribute customAttribute = property.PropertyInfo.GetCustomAttribute(false);
			if (customAttribute != null && !string.IsNullOrWhiteSpace(customAttribute.Name))
			{
				return this._options.HasFlag(NameResolverOptions.ProcessDataMemberAttributePropertyNames);
			}
			return this._options.HasFlag(NameResolverOptions.ProcessReflectedPropertyNames);
		}

		// Token: 0x0400031C RID: 796
		private readonly NameResolverOptions _options;
	}
}
