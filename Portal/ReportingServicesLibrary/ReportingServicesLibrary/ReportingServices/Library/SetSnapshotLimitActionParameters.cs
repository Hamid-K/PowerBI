using System;
using System.Globalization;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001B6 RID: 438
	internal sealed class SetSnapshotLimitActionParameters : SnapshotLimitActionParameters
	{
		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00037F64 File Offset: 0x00036164
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}, {2}", base.ReportPath, base.UseSystem, base.ScopedLimit);
			}
		}
	}
}
