using System;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x020001A3 RID: 419
	internal class TicketCacheWriter : IDisposable
	{
		// Token: 0x06001329 RID: 4905 RVA: 0x000406B8 File Offset: 0x0003E8B8
		internal TicketCacheWriter(LsaSafeHandle lsaHandle, string packageName = "Kerberos")
		{
			this._lsaHandle = lsaHandle;
			NativeMethods.LSA_STRING lsa_STRING = new NativeMethods.LSA_STRING
			{
				Buffer = packageName,
				Length = (ushort)packageName.Length,
				MaximumLength = (ushort)packageName.Length
			};
			NativeMethods.LsaThrowIfError(NativeMethods.LsaLookupAuthenticationPackage(this._lsaHandle, ref lsa_STRING, out this._selectedAuthPackage));
			NativeMethods.LSA_STRING lsa_STRING2 = new NativeMethods.LSA_STRING
			{
				Buffer = "Negotiate",
				Length = (ushort)"Negotiate".Length,
				MaximumLength = (ushort)"Negotiate".Length
			};
			NativeMethods.LsaThrowIfError(NativeMethods.LsaLookupAuthenticationPackage(this._lsaHandle, ref lsa_STRING2, out this._negotiateAuthPackage));
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004076C File Offset: 0x0003E96C
		public static TicketCacheWriter Connect(string package = "Kerberos")
		{
			if (string.IsNullOrWhiteSpace(package))
			{
				package = "Kerberos";
			}
			LsaSafeHandle lsaSafeHandle;
			NativeMethods.LsaThrowIfError(NativeMethods.LsaConnectUntrusted(out lsaSafeHandle));
			return new TicketCacheWriter(lsaSafeHandle, package);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004079C File Offset: 0x0003E99C
		public void ImportCredential(byte[] ticketBytes, long luid = 0L)
		{
			if (ticketBytes == null)
			{
				throw new ArgumentNullException("ticketBytes");
			}
			NativeMethods.KERB_SUBMIT_TKT_REQUEST kerb_SUBMIT_TKT_REQUEST = new NativeMethods.KERB_SUBMIT_TKT_REQUEST
			{
				MessageType = NativeMethods.KERB_PROTOCOL_MESSAGE_TYPE.KerbSubmitTicketMessage,
				KerbCredSize = ticketBytes.Length,
				KerbCredOffset = Marshal.SizeOf(typeof(NativeMethods.KERB_SUBMIT_TKT_REQUEST)),
				LogonId = luid
			};
			int num = kerb_SUBMIT_TKT_REQUEST.KerbCredOffset + ticketBytes.Length;
			IntPtr intPtr = Marshal.AllocHGlobal(num);
			Marshal.StructureToPtr<NativeMethods.KERB_SUBMIT_TKT_REQUEST>(kerb_SUBMIT_TKT_REQUEST, intPtr, false);
			Marshal.Copy(ticketBytes, 0, intPtr + kerb_SUBMIT_TKT_REQUEST.KerbCredOffset, ticketBytes.Length);
			this.LsaCallAuthenticationPackage(intPtr.ToPointer(), num);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x00040838 File Offset: 0x0003EA38
		private unsafe void LsaCallAuthenticationPackage(void* pBuffer, int bufferSize)
		{
			LsaBufferSafeHandle lsaBufferSafeHandle = null;
			try
			{
				int num;
				int num2;
				NativeMethods.LsaThrowIfError(NativeMethods.LsaCallAuthenticationPackage(this._lsaHandle, this._selectedAuthPackage, pBuffer, bufferSize, out lsaBufferSafeHandle, out num, out num2));
				NativeMethods.LsaThrowIfError(num2);
			}
			finally
			{
				if (lsaBufferSafeHandle != null)
				{
					lsaBufferSafeHandle.Dispose();
				}
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x00040888 File Offset: 0x0003EA88
		protected virtual void Dispose(bool disposing)
		{
			if (!this._disposedValue)
			{
				this._lsaHandle.Dispose();
				this._disposedValue = true;
			}
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000408A4 File Offset: 0x0003EAA4
		~TicketCacheWriter()
		{
			this.Dispose(false);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000408D4 File Offset: 0x0003EAD4
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400079E RID: 1950
		private const string _kerberosPackageName = "Kerberos";

		// Token: 0x0400079F RID: 1951
		private const string _negotiatePackageName = "Negotiate";

		// Token: 0x040007A0 RID: 1952
		private readonly LsaSafeHandle _lsaHandle;

		// Token: 0x040007A1 RID: 1953
		private readonly int _selectedAuthPackage;

		// Token: 0x040007A2 RID: 1954
		private readonly int _negotiateAuthPackage;

		// Token: 0x040007A3 RID: 1955
		private bool _disposedValue;
	}
}
