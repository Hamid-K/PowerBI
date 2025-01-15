using System;
using System.Diagnostics;
using Microsoft.ReportingServices.Diagnostics;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000014 RID: 20
	internal static class DataProtection
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00006236 File Offset: 0x00004436
		public static int OldUntaggedVersion
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00006239 File Offset: 0x00004439
		public static int OldUnsaltedVersion
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000623C File Offset: 0x0000443C
		public static int CurrentVersion
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000623F File Offset: 0x0000443F
		public static bool IsCurrentVersion(int version)
		{
			return version == DataProtection.CurrentVersion;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00006249 File Offset: 0x00004449
		public static IDataProtection Instance
		{
			[DebuggerStepThrough]
			get
			{
				if (DataProtection._dpInstance == null)
				{
					DataProtection._dpInstance = new DataProtection.DataProtectionInstance();
				}
				return DataProtection._dpInstance;
			}
		}

		// Token: 0x040000A0 RID: 160
		private static IDataProtection _dpInstance;

		// Token: 0x0200004E RID: 78
		private class DataProtectionInstance : IDataProtection
		{
			// Token: 0x0600028C RID: 652 RVA: 0x0000B010 File Offset: 0x00009210
			public byte[] ProtectData(string unprotectedData, string tag)
			{
				return SymmetricKeyEncryption.Instance.Encrypt(unprotectedData, tag);
			}

			// Token: 0x0600028D RID: 653 RVA: 0x0000B01E File Offset: 0x0000921E
			public string UnprotectDataToString(byte[] protectedData, string tag)
			{
				return SymmetricKeyEncryption.Instance.DecryptToString(protectedData, tag);
			}
		}
	}
}
