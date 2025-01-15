using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200001D RID: 29
	internal sealed class BooleanParameter : ApplicationParameter
	{
		// Token: 0x06000074 RID: 116 RVA: 0x000031BF File Offset: 0x000013BF
		public BooleanParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, bool defaultValue, string units)
			: base(parameterSource, tracer, name, configValue, defaultValue, units)
		{
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000031D8 File Offset: 0x000013D8
		protected override bool Validate(string valueToValidate, out object validatedValue)
		{
			bool flag = bool.Parse(valueToValidate);
			validatedValue = flag;
			return true;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000076 RID: 118 RVA: 0x000031F5 File Offset: 0x000013F5
		public bool Value
		{
			get
			{
				return (bool)this.BaseValue;
			}
		}
	}
}
