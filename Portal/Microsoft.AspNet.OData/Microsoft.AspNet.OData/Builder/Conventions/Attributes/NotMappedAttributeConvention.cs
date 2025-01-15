using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000179 RID: 377
	internal class NotMappedAttributeConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000CAB RID: 3243 RVA: 0x00031E8C File Offset: 0x0003008C
		public NotMappedAttributeConvention()
			: base(NotMappedAttributeConvention._filter, false)
		{
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00031E9A File Offset: 0x0003009A
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (structuralTypeConfiguration == null)
			{
				throw Error.ArgumentNull("structuralTypeConfiguration");
			}
			if (!edmProperty.AddedExplicitly)
			{
				structuralTypeConfiguration.RemoveProperty(edmProperty.PropertyInfo);
			}
		}

		// Token: 0x040003A8 RID: 936
		private const string EntityFrameworkNotMappedAttributeTypeName = "System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute";

		// Token: 0x040003A9 RID: 937
		private static Func<Attribute, bool> _filter = (Attribute attribute) => attribute.GetType().FullName.Equals("System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute", StringComparison.Ordinal);
	}
}
