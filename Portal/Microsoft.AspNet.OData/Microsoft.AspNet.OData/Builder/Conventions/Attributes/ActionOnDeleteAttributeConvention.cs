using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200016A RID: 362
	internal class ActionOnDeleteAttributeConvention : AttributeEdmPropertyConvention<NavigationPropertyConfiguration>
	{
		// Token: 0x06000C84 RID: 3204 RVA: 0x00031527 File Offset: 0x0002F727
		public ActionOnDeleteAttributeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(ActionOnDeleteAttribute), false)
		{
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x00031550 File Offset: 0x0002F750
		public override void Apply(NavigationPropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			ActionOnDeleteAttribute actionOnDeleteAttribute = attribute as ActionOnDeleteAttribute;
			if (actionOnDeleteAttribute != null && !edmProperty.AddedExplicitly && edmProperty.DependentProperties.Any<PropertyInfo>())
			{
				edmProperty.OnDeleteAction = actionOnDeleteAttribute.OnDeleteAction;
			}
		}
	}
}
