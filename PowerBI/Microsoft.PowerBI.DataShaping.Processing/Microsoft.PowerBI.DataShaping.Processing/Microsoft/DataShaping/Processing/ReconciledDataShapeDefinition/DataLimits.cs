using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200002F RID: 47
	internal sealed class DataLimits
	{
		// Token: 0x0600014A RID: 330 RVA: 0x00005158 File Offset: 0x00003358
		internal DataLimits(IList<DataLimit> limits, DataBinding binding, TelemetryItems telemetry)
		{
			this._limits = limits;
			this._binding = binding;
			this._telemetry = telemetry;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00005175 File Offset: 0x00003375
		internal IList<DataLimit> Limits
		{
			get
			{
				return this._limits;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000517D File Offset: 0x0000337D
		internal DataBinding Binding
		{
			get
			{
				return this._binding;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00005185 File Offset: 0x00003385
		internal TelemetryItems TelemetryItems
		{
			get
			{
				return this._telemetry;
			}
		}

		// Token: 0x040000C6 RID: 198
		private readonly IList<DataLimit> _limits;

		// Token: 0x040000C7 RID: 199
		private readonly DataBinding _binding;

		// Token: 0x040000C8 RID: 200
		private readonly TelemetryItems _telemetry;
	}
}
