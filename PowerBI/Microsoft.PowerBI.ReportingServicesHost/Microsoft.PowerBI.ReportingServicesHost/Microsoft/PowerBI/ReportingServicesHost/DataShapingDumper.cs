using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.PowerBI.ReportingServicesHost
{
	// Token: 0x02000036 RID: 54
	internal sealed class DataShapingDumper : IDumper
	{
		// Token: 0x06000121 RID: 289 RVA: 0x000045A9 File Offset: 0x000027A9
		private DataShapingDumper()
		{
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000045B1 File Offset: 0x000027B1
		public void Dump(string message, Exception exception)
		{
			Dumper.Current.DumpHere(exception, message, false);
		}

		// Token: 0x040000E8 RID: 232
		internal static readonly DataShapingDumper Instance = new DataShapingDumper();
	}
}
