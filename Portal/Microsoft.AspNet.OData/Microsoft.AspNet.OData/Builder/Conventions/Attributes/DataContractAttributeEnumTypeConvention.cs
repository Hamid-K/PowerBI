using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder.Conventions.Attributes
{
	// Token: 0x02000168 RID: 360
	internal class DataContractAttributeEnumTypeConvention : AttributeEdmTypeConvention<EnumTypeConfiguration>
	{
		// Token: 0x06000C80 RID: 3200 RVA: 0x000313A8 File Offset: 0x0002F5A8
		public DataContractAttributeEnumTypeConvention()
			: base((Attribute attribute) => attribute.GetType() == typeof(DataContractAttribute), false)
		{
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000313D0 File Offset: 0x0002F5D0
		public override void Apply(EnumTypeConfiguration enumTypeConfiguration, ODataConventionModelBuilder model, Attribute attribute)
		{
			if (enumTypeConfiguration == null)
			{
				throw Error.ArgumentNull("enumTypeConfiguration");
			}
			if (model == null)
			{
				throw Error.ArgumentNull("model");
			}
			if (!enumTypeConfiguration.AddedExplicitly && model.ModelAliasingEnabled)
			{
				DataContractAttribute dataContractAttribute = attribute as DataContractAttribute;
				if (dataContractAttribute != null)
				{
					if (dataContractAttribute.Name != null)
					{
						enumTypeConfiguration.Name = dataContractAttribute.Name;
					}
					if (dataContractAttribute.Namespace != null)
					{
						enumTypeConfiguration.Namespace = dataContractAttribute.Namespace;
					}
				}
				enumTypeConfiguration.AddedExplicitly = false;
			}
			foreach (EnumMemberConfiguration enumMemberConfiguration in ((IEnumerable<EnumMemberConfiguration>)enumTypeConfiguration.Members.ToArray<EnumMemberConfiguration>()))
			{
				EnumMemberAttribute enumMemberAttribute = enumTypeConfiguration.ClrType.GetField(enumMemberConfiguration.Name).GetCustomAttributes(typeof(EnumMemberAttribute), true).FirstOrDefault<object>() as EnumMemberAttribute;
				if (!enumMemberConfiguration.AddedExplicitly)
				{
					if (model.ModelAliasingEnabled && enumMemberAttribute != null)
					{
						if (!string.IsNullOrWhiteSpace(enumMemberAttribute.Value))
						{
							enumMemberConfiguration.Name = enumMemberAttribute.Value;
						}
					}
					else
					{
						enumTypeConfiguration.RemoveMember(enumMemberConfiguration.MemberInfo);
					}
				}
			}
		}
	}
}
