using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002B RID: 43
	internal sealed class LongParameter : RangedParameter<long>
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000346C File Offset: 0x0000166C
		public LongParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, long defaultValue, string units)
			: base(parameterSource, tracer, name, configValue, defaultValue, units, long.MinValue, long.MaxValue)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000349C File Offset: 0x0000169C
		protected override long Parse(string valueToValidate)
		{
			long num;
			if (valueToValidate.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				num = long.Parse(valueToValidate.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
			}
			else
			{
				num = long.Parse(valueToValidate, CultureInfo.InvariantCulture);
			}
			return num;
		}
	}
}
