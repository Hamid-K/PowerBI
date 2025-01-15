using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000027 RID: 39
	internal sealed class EnumParameter<EnumType> : ApplicationParameter
	{
		// Token: 0x06000096 RID: 150 RVA: 0x000033B8 File Offset: 0x000015B8
		public EnumParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, EnumType defaultValue, string units)
			: base(parameterSource, tracer, name, configValue, defaultValue, units)
		{
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000033D0 File Offset: 0x000015D0
		protected override bool Validate(string valueToValidate, out object validatedValue)
		{
			object obj = Enum.Parse(typeof(EnumType), valueToValidate, true);
			validatedValue = obj;
			return true;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000033F3 File Offset: 0x000015F3
		public EnumType Value
		{
			get
			{
				return (EnumType)((object)this.BaseValue);
			}
		}
	}
}
