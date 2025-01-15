using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x020001A2 RID: 418
	internal class TicketCacheReader : IDisposable
	{
		// Token: 0x06001325 RID: 4901 RVA: 0x00040630 File Offset: 0x0003E830
		public TicketCacheReader(string spn, long logonId = 0L, string package = "Kerberos")
		{
			this._spn = spn;
			this._context = new SspiSecurityContext(Credential.Current(), package, logonId, InitContextFlag.Delegate | InitContextFlag.ReplayDetect | InitContextFlag.SequenceDetect | InitContextFlag.Confidentiality | InitContextFlag.AllocateMemory | InitContextFlag.Connection | InitContextFlag.InitExtendedError);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x00040658 File Offset: 0x0003E858
		public byte[] RequestToken()
		{
			byte[] array;
			if (this._context.InitializeSecurityContext(this._spn, out array) == ContextStatus.Error)
			{
				throw new Win32Exception(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x00040687 File Offset: 0x0003E887
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				if (disposing)
				{
					this._context.Dispose();
				}
				this._disposedValue = true;
			}
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000406A6 File Offset: 0x0003E8A6
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400079B RID: 1947
		private readonly string _spn;

		// Token: 0x0400079C RID: 1948
		private readonly SspiSecurityContext _context;

		// Token: 0x0400079D RID: 1949
		private bool _disposedValue;
	}
}
