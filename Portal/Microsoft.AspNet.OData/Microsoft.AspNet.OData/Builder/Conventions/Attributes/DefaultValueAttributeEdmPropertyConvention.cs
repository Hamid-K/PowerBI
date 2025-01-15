using System;
using System.ComponentModel;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000158 RID: 344
	internal class DefaultValueAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000C60 RID: 3168 RVA: 0x000306C8 File Offset: 0x0002E8C8
		public DefaultValueAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(DefaultValueAttribute), false)
		{
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000306F0 File Offset: 0x0002E8F0
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			DefaultValueAttribute defaultValueAttribute = attribute as DefaultValueAttribute;
			if (!edmProperty.AddedExplicitly && defaultValueAttribute != null && defaultValueAttribute.Value != null)
			{
				if (edmProperty.Kind == PropertyKind.Primitive)
				{
					(edmProperty as PrimitivePropertyConfiguration).DefaultValueString = defaultValueAttribute.Value.ToString();
				}
				if (edmProperty.Kind == PropertyKind.Enum)
				{
					(edmProperty as EnumPropertyConfiguration).DefaultValueString = defaultValueAttribute.Value.ToString();
				}
			}
		}
	}
}
