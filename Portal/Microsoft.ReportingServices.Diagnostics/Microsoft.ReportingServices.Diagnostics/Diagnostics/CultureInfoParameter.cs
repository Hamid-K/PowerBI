using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000024 RID: 36
	internal sealed class CultureInfoParameter : ApplicationParameter
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00003202 File Offset: 0x00001402
		public CultureInfoParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, CultureInfo defaultValue, string units)
			: base(parameterSource, tracer, name, configValue, defaultValue, units)
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000334F File Offset: 0x0000154F
		protected override bool Validate(string valueToValidate, out object validatedValue)
		{
			validatedValue = new CultureInfo(valueToValidate);
			return true;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000335A File Offset: 0x0000155A
		public CultureInfo Value
		{
			get
			{
				return (CultureInfo)this.BaseValue;
			}
		}
	}
}
