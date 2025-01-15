using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Identity.Client.PlatformsCommon.Shared;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x020001A1 RID: 417
	internal class SspiSecurityContext : IDisposable
	{
		// Token: 0x0600131D RID: 4893 RVA: 0x000403A0 File Offset: 0x0003E5A0
		public SspiSecurityContext(Credential credential, string package, long logonId = 0L, InitContextFlag clientFlags = InitContextFlag.Delegate | InitContextFlag.ReplayDetect | InitContextFlag.SequenceDetect | InitContextFlag.Confidentiality | InitContextFlag.AllocateMemory | InitContextFlag.Connection | InitContextFlag.InitExtendedError)
		{
			if (!DesktopOsHelper.IsWindows())
			{
				throw new PlatformNotSupportedException("Ticket Cache interface is not supported for this OS platform.");
			}
			this._credential = credential;
			this._clientFlags = clientFlags;
			this.Package = package;
			this._logonId = logonId;
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x000403ED File Offset: 0x0003E5ED
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x000403F5 File Offset: 0x0003E5F5
		public string Package { get; private set; }

		// Token: 0x06001320 RID: 4896 RVA: 0x000403FE File Offset: 0x0003E5FE
		private static void ThrowIfError(uint result)
		{
			if (result != 0U && result != 2148074241U)
			{
				throw new Win32Exception((int)result);
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00040414 File Offset: 0x0003E614
		public ContextStatus InitializeSecurityContext(string targetName, out byte[] clientRequest)
		{
			string text = targetName.ToLowerInvariant();
			clientRequest = null;
			SecStatus secStatus = SecStatus.SEC_E_OK;
			int num = 0;
			NativeMethods.SecBufferDesc secBufferDesc = default(NativeMethods.SecBufferDesc);
			ContextStatus contextStatus;
			try
			{
				do
				{
					secBufferDesc = new NativeMethods.SecBufferDesc(num);
					if (!this._credentialsHandle.IsSet || secStatus == SecStatus.SEC_I_CONTINUE_NEEDED)
					{
						this.AcquireCredentials();
					}
					InitContextFlag initContextFlag;
					secStatus = NativeMethods.InitializeSecurityContext_0(ref this._credentialsHandle, IntPtr.Zero, text, this._clientFlags, 0, 0, IntPtr.Zero, 0, ref this._securityContext, ref secBufferDesc, out initContextFlag, IntPtr.Zero);
					if (secStatus == (SecStatus)2148074240U)
					{
						if (num > 16384)
						{
							break;
						}
						num += 1000;
					}
				}
				while (secStatus == SecStatus.SEC_I_INCOMPLETE_CREDENTIALS || secStatus == (SecStatus)2148074240U);
				if (secStatus > (SecStatus)2147483648U)
				{
					throw new Win32Exception((int)secStatus);
				}
				clientRequest = secBufferDesc.ReadBytes();
				if (secStatus == SecStatus.SEC_I_CONTINUE_NEEDED)
				{
					contextStatus = ContextStatus.RequiresContinuation;
				}
				else
				{
					contextStatus = ContextStatus.Accepted;
				}
			}
			finally
			{
				secBufferDesc.Dispose();
			}
			return contextStatus;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000404F4 File Offset: 0x0003E6F4
		private void TrackUnmanaged(object thing)
		{
			this._disposable.Add(thing);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00040504 File Offset: 0x0003E704
		private unsafe void AcquireCredentials()
		{
			CredentialHandle credentialHandle = this._credential.Structify();
			this.TrackUnmanaged(credentialHandle);
			IntPtr intPtr = IntPtr.Zero;
			if (this._logonId != 0L)
			{
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf<long>());
				Marshal.StructureToPtr<long>(this._logonId, intPtr, false);
			}
			SecStatus secStatus = NativeMethods.AcquireCredentialsHandle(null, this.Package, 3, intPtr, (void*)credentialHandle.DangerousGetHandle(), IntPtr.Zero, IntPtr.Zero, ref this._credentialsHandle, IntPtr.Zero);
			if (secStatus != SecStatus.SEC_E_OK)
			{
				throw new Win32Exception((int)secStatus);
			}
			this.TrackUnmanaged(this._credentialsHandle);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x00040594 File Offset: 0x0003E794
		public unsafe void Dispose()
		{
			foreach (object obj in this._disposable)
			{
				IDisposable disposable = obj as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
				else if (obj is NativeMethods.SECURITY_HANDLE)
				{
					NativeMethods.SECURITY_HANDLE security_HANDLE = (NativeMethods.SECURITY_HANDLE)obj;
					NativeMethods.DeleteSecurityContext(&security_HANDLE);
					SspiSecurityContext.ThrowIfError(NativeMethods.FreeCredentialsHandle(&security_HANDLE));
				}
				else if (obj is IntPtr)
				{
					IntPtr intPtr = (IntPtr)obj;
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x04000790 RID: 1936
		private const int SECPKG_CRED_BOTH = 3;

		// Token: 0x04000791 RID: 1937
		private const int SECURITY_NETWORK_DREP = 0;

		// Token: 0x04000792 RID: 1938
		private const int _maxTokenSize = 16384;

		// Token: 0x04000793 RID: 1939
		private const InitContextFlag _defaultRequiredFlags = InitContextFlag.Delegate | InitContextFlag.ReplayDetect | InitContextFlag.SequenceDetect | InitContextFlag.Confidentiality | InitContextFlag.AllocateMemory | InitContextFlag.Connection | InitContextFlag.InitExtendedError;

		// Token: 0x04000794 RID: 1940
		private readonly HashSet<object> _disposable = new HashSet<object>();

		// Token: 0x04000795 RID: 1941
		private readonly Credential _credential;

		// Token: 0x04000796 RID: 1942
		private readonly InitContextFlag _clientFlags;

		// Token: 0x04000797 RID: 1943
		private NativeMethods.SECURITY_HANDLE _credentialsHandle;

		// Token: 0x04000798 RID: 1944
		private NativeMethods.SECURITY_HANDLE _securityContext;

		// Token: 0x04000799 RID: 1945
		private long _logonId;
	}
}
