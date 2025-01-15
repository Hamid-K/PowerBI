using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x0200017C RID: 380
	internal class IgnoreDataMemberAttributeEdmPropertyConvention : AttributeEdmPropertyConvention<PropertyConfiguration>
	{
		// Token: 0x06000CB4 RID: 3252 RVA: 0x00032070 File Offset: 0x00030270
		public IgnoreDataMemberAttributeEdmPropertyConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(IgnoreDataMemberAttribute), false)
		{
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00032098 File Offset: 0x00030298
		public override void Apply(PropertyConfiguration edmProperty, StructuralTypeConfiguration structuralTypeConfiguration, Attribute attribute, ODataConventionModelBuilder model)
		{
			if (structuralTypeConfiguration == null)
			{
				throw Error.ArgumentNull("structuralTypeConfiguration");
			}
			if (edmProperty == null)
			{
				throw Error.ArgumentNull("edmProperty");
			}
			if (!edmProperty.AddedExplicitly)
			{
				bool flag = TypeHelper.AsMemberInfo(structuralTypeConfiguration.ClrType).GetCustomAttributes(typeof(DataContractAttribute), true).Any<object>();
				bool flag2 = edmProperty.PropertyInfo.GetCustomAttributes(typeof(DataMemberAttribute), true).Any<object>();
				if (flag && flag2)
				{
					return;
				}
				structuralTypeConfiguration.RemoveProperty(edmProperty.PropertyInfo);
			}
		}
	}
}
