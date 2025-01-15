using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000029 RID: 41
	internal sealed class IntParameter : RangedParameter<int>
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00003400 File Offset: 0x00001600
		public IntParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, int defaultValue, string units)
			: base(parameterSource, tracer, name, configValue, defaultValue, units, int.MinValue, int.MaxValue)
		{
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003428 File Offset: 0x00001628
		protected override int Parse(string valueToValidate)
		{
			int num;
			if (valueToValidate.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
			{
				num = int.Parse(valueToValidate.Substring(2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture);
			}
			else
			{
				num = int.Parse(valueToValidate, CultureInfo.InvariantCulture);
			}
			return num;
		}
	}
}
