using System;
using System.Collections;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200001E RID: 30
	internal sealed class CollectionParameter : ApplicationParameter
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00003202 File Offset: 0x00001402
		public CollectionParameter(IParameterSource parameterSource, RSTrace tracer, string name, string configValue, ICollection defaultValue, string units)
			: base(parameterSource, tracer, name, configValue, defaultValue, units)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003213 File Offset: 0x00001413
		public string Value
		{
			get
			{
				return (string)this.BaseValue;
			}
		}
	}
}
