using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Data.Common;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x020000B5 RID: 181
	internal sealed class SNIHandle : SafeHandle
	{
		// Token: 0x06000CDD RID: 3293 RVA: 0x00027520 File Offset: 0x00025720
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, string serverName, byte[] spnBuffer, bool ignoreSniOpenTimeout, int timeout, out byte[] instanceName, bool flushCache, bool fSync, bool fParallel, TransparentNetworkResolutionState transparentNetworkResolutionState, int totalTimeout, SqlConnectionIPAddressPreference ipPreference, SQLDNSInfo cachedDNSInfo, string hostNameInCertificate)
			: base(IntPtr.Zero, true)
		{
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				this._fSync = fSync;
				instanceName = new byte[256];
				if (ignoreSniOpenTimeout)
				{
					timeout = -1;
				}
				this._status = SNINativeMethodWrapper.SNIOpenSyncEx(myInfo, serverName, ref this.handle, spnBuffer, instanceName, flushCache, fSync, timeout, fParallel, (int)transparentNetworkResolutionState, totalTimeout, ADP.IsAzureSqlServerEndpoint(serverName), ipPreference, cachedDNSInfo, hostNameInCertificate);
			}
		}

		// Token: 0x06000CDE RID: 3294 RVA: 0x000275A8 File Offset: 0x000257A8
		internal SNIHandle(SNINativeMethodWrapper.ConsumerInfo myInfo, SNIHandle parent, SqlConnectionIPAddressPreference ipPreference, SQLDNSInfo cachedDNSInfo)
			: base(IntPtr.Zero, true)
		{
			try
			{
			}
			finally
			{
				this._status = SNINativeMethodWrapper.SNIOpenMarsSession(myInfo, parent, ref this.handle, parent._fSync, ipPreference, cachedDNSInfo);
			}
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0002743C File Offset: 0x0002563C
		public override bool IsInvalid
		{
			get
			{
				return IntPtr.Zero == this.handle;
			}
		}

		// Token: 0x06000CE0 RID: 3296 RVA: 0x000275F8 File Offset: 0x000257F8
		protected override bool ReleaseHandle()
		{
			IntPtr handle = this.handle;
			this.handle = IntPtr.Zero;
			return !(IntPtr.Zero != handle) || SNINativeMethodWrapper.SNIClose(handle) == 0U;
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0002762F File Offset: 0x0002582F
		internal uint Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x04000578 RID: 1400
		private readonly uint _status = uint.MaxValue;

		// Token: 0x04000579 RID: 1401
		private readonly bool _fSync;
	}
}
