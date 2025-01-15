using System;
using System.Collections.Generic;

namespace Microsoft.BIServer.HostingEnvironment.HostingInfo
{
	// Token: 0x02000048 RID: 72
	[CLSCompliant(false)]
	public sealed class ManagementState
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00006608 File Offset: 0x00004808
		public static ManagementState Pending
		{
			get
			{
				return new ManagementState
				{
					EncryptionKey = new byte[0],
					_databaseValidationStatus = DatabaseValidationStatus.Pending,
					RsConfigSwitches = new Dictionary<string, bool>(),
					ConfigurationInfo = new Dictionary<string, string>()
				};
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00006648 File Offset: 0x00004848
		public static ManagementState Unavailable
		{
			get
			{
				return new ManagementState
				{
					EncryptionKey = new byte[0],
					_databaseValidationStatus = DatabaseValidationStatus.DatabaseUnavailable,
					RsConfigSwitches = new Dictionary<string, bool>(),
					ConfigurationInfo = new Dictionary<string, string>()
				};
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006685 File Offset: 0x00004885
		public bool EncryptionKeyPresent
		{
			get
			{
				return this.EncryptionKey != null && this.EncryptionKey.Length != 0;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x0000669B File Offset: 0x0000489B
		// (set) Token: 0x060001C7 RID: 455 RVA: 0x000066A3 File Offset: 0x000048A3
		public DatabaseValidationStatus DatabaseValidationStatus
		{
			get
			{
				return this._databaseValidationStatus;
			}
			set
			{
				if (value == (DatabaseValidationStatus)0)
				{
					value = DatabaseValidationStatus.Pending;
				}
				this._databaseValidationStatus = value;
			}
		}

		// Token: 0x0400010A RID: 266
		public byte[] EncryptionKey;

		// Token: 0x0400010B RID: 267
		private DatabaseValidationStatus _databaseValidationStatus = DatabaseValidationStatus.Pending;

		// Token: 0x0400010C RID: 268
		public Dictionary<string, bool> RsConfigSwitches;

		// Token: 0x0400010D RID: 269
		public Dictionary<string, string> ConfigurationInfo;
	}
}
