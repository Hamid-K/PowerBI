using System;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000029 RID: 41
	internal class EnclavePackage
	{
		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060006C6 RID: 1734 RVA: 0x0000DE18 File Offset: 0x0000C018
		internal SqlEnclaveSession EnclaveSession { get; }

		// Token: 0x17000604 RID: 1540
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000DE20 File Offset: 0x0000C020
		internal byte[] EnclavePackageBytes { get; }

		// Token: 0x060006C8 RID: 1736 RVA: 0x0000DE28 File Offset: 0x0000C028
		internal EnclavePackage(byte[] enclavePackageBytes, SqlEnclaveSession enclaveSession)
		{
			this.EnclavePackageBytes = enclavePackageBytes;
			this.EnclaveSession = enclaveSession;
		}
	}
}
