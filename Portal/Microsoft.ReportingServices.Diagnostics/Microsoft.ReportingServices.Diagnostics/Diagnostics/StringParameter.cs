using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000040 RID: 64
	internal sealed class StringParameter : ApplicationParameter
	{
		// Token: 0x06000205 RID: 517 RVA: 0x00003202 File Offset: 0x00001402
		public StringParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, string defaultValue, string units)
			: base(parameterSource, tracer, name, configValue, defaultValue, units)
		{
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00003213 File Offset: 0x00001413
		public string Value
		{
			get
			{
				return (string)this.BaseValue;
			}
		}
	}
}
