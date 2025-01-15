using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Microsoft.Identity.Client.Platforms.Features.DesktopOs.Kerberos
{
	// Token: 0x0200019E RID: 414
	internal class NativeMethods
	{
		// Token: 0x0600130E RID: 4878
		[DllImport("secur32.dll", BestFitMapping = false, CharSet = CharSet.Auto, EntryPoint = "InitializeSecurityContext", SetLastError = true, ThrowOnUnmappableChar = true)]
		internal static extern SecStatus InitializeSecurityContext_0(ref NativeMethods.SECURITY_HANDLE phCredential, IntPtr phContext, string pszTargetName, InitContextFlag fContextReq, int Reserved1, int TargetDataRep, IntPtr pInput, int Reserved2, ref NativeMethods.SECURITY_HANDLE phNewContext, ref NativeMethods.SecBufferDesc pOutput, out InitContextFlag pfContextAttr, IntPtr ptsExpiry);

		// Token: 0x0600130F RID: 4879
		[DllImport("secur32.dll", BestFitMapping = false, CharSet = CharSet.Auto, ThrowOnUnmappableChar = true)]
		internal unsafe static extern SecStatus AcquireCredentialsHandle(string pszPrincipal, string pszPackage, int fCredentialUse, IntPtr PAuthenticationID, void* pAuthData, IntPtr pGetKeyFn, IntPtr pvGetKeyArgument, ref NativeMethods.SECURITY_HANDLE phCredential, IntPtr ptsExpiry);

		// Token: 0x06001310 RID: 4880
		[DllImport("secur32.dll")]
		internal unsafe static extern uint FreeCredentialsHandle(NativeMethods.SECURITY_HANDLE* handle);

		// Token: 0x06001311 RID: 4881
		[DllImport("secur32.dll")]
		public unsafe static extern SecStatus DeleteSecurityContext(NativeMethods.SECURITY_HANDLE* context);

		// Token: 0x06001312 RID: 4882
		[DllImport("secur32.dll")]
		public static extern int LsaDeregisterLogonProcess(IntPtr LsaHandle);

		// Token: 0x06001313 RID: 4883
		[DllImport("secur32.dll")]
		public static extern int LsaLookupAuthenticationPackage(LsaSafeHandle LsaHandle, ref NativeMethods.LSA_STRING PackageName, out int AuthenticationPackage);

		// Token: 0x06001314 RID: 4884
		[DllImport("secur32.dll")]
		public static extern int LsaConnectUntrusted(out LsaSafeHandle LsaHandle);

		// Token: 0x06001315 RID: 4885
		[DllImport("secur32.dll")]
		public unsafe static extern int LsaCallAuthenticationPackage(LsaSafeHandle LsaHandle, int AuthenticationPackage, void* ProtocolSubmitBuffer, int SubmitBufferLength, out LsaBufferSafeHandle ProtocolReturnBuffer, out int ReturnBufferLength, out int ProtocolStatus);

		// Token: 0x06001316 RID: 4886
		[DllImport("secur32.dll")]
		public static extern int LsaFreeReturnBuffer(IntPtr Buffer);

		// Token: 0x06001317 RID: 4887
		[DllImport("advapi32.dll")]
		public static extern int LsaNtStatusToWinError(int Status);

		// Token: 0x06001318 RID: 4888
		[DllImport("kernel32.dll")]
		public static extern bool CloseHandle(IntPtr hObject);

		// Token: 0x06001319 RID: 4889
		[DllImport("advapi32.dll")]
		public static extern bool ImpersonateLoggedOnUser(LsaTokenSafeHandle hToken);

		// Token: 0x0600131A RID: 4890
		[DllImport("advapi32.dll")]
		public static extern bool RevertToSelf();

		// Token: 0x0600131B RID: 4891 RVA: 0x00040381 File Offset: 0x0003E581
		public static void LsaThrowIfError(int result)
		{
			if (result != 0)
			{
				result = NativeMethods.LsaNtStatusToWinError(result);
				throw new Win32Exception(result);
			}
		}

		// Token: 0x04000766 RID: 1894
		private const string SECUR32 = "secur32.dll";

		// Token: 0x04000767 RID: 1895
		private const string ADVAPI32 = "advapi32.dll";

		// Token: 0x04000768 RID: 1896
		private const string KERNEL32 = "kernel32.dll";

		// Token: 0x02000417 RID: 1047
		public struct KERB_INTERACTIVE_LOGON
		{
			// Token: 0x04001232 RID: 4658
			public NativeMethods.KERB_LOGON_SUBMIT_TYPE MessageType;

			// Token: 0x04001233 RID: 4659
			public NativeMethods.UNICODE_STRING LogonDomainName;

			// Token: 0x04001234 RID: 4660
			public NativeMethods.UNICODE_STRING UserName;

			// Token: 0x04001235 RID: 4661
			public NativeMethods.UNICODE_STRING Password;
		}

		// Token: 0x02000418 RID: 1048
		public struct TOKEN_SOURCE
		{
			// Token: 0x04001236 RID: 4662
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] SourceName;

			// Token: 0x04001237 RID: 4663
			public NativeMethods.LUID SourceIdentifier;
		}

		// Token: 0x02000419 RID: 1049
		public struct KERB_S4U_LOGON
		{
			// Token: 0x04001238 RID: 4664
			public NativeMethods.KERB_LOGON_SUBMIT_TYPE MessageType;

			// Token: 0x04001239 RID: 4665
			public NativeMethods.S4uFlags Flags;

			// Token: 0x0400123A RID: 4666
			public NativeMethods.UNICODE_STRING ClientUpn;

			// Token: 0x0400123B RID: 4667
			public NativeMethods.UNICODE_STRING ClientRealm;
		}

		// Token: 0x0200041A RID: 1050
		public struct UNICODE_STRING
		{
			// Token: 0x0400123C RID: 4668
			public ushort Length;

			// Token: 0x0400123D RID: 4669
			public ushort MaximumLength;

			// Token: 0x0400123E RID: 4670
			public IntPtr Buffer;
		}

		// Token: 0x0200041B RID: 1051
		[Flags]
		public enum S4uFlags
		{
			// Token: 0x04001240 RID: 4672
			KERB_S4U_LOGON_FLAG_CHECK_LOGONHOURS = 2,
			// Token: 0x04001241 RID: 4673
			KERB_S4U_LOGON_FLAG_IDENTIFY = 8
		}

		// Token: 0x0200041C RID: 1052
		public enum KERB_LOGON_SUBMIT_TYPE
		{
			// Token: 0x04001243 RID: 4675
			KerbInteractiveLogon = 2,
			// Token: 0x04001244 RID: 4676
			KerbSmartCardLogon = 6,
			// Token: 0x04001245 RID: 4677
			KerbWorkstationUnlockLogon,
			// Token: 0x04001246 RID: 4678
			KerbSmartCardUnlockLogon,
			// Token: 0x04001247 RID: 4679
			KerbProxyLogon,
			// Token: 0x04001248 RID: 4680
			KerbTicketLogon,
			// Token: 0x04001249 RID: 4681
			KerbTicketUnlockLogon,
			// Token: 0x0400124A RID: 4682
			KerbS4ULogon,
			// Token: 0x0400124B RID: 4683
			KerbCertificateLogon,
			// Token: 0x0400124C RID: 4684
			KerbCertificateS4ULogon,
			// Token: 0x0400124D RID: 4685
			KerbCertificateUnlockLogon,
			// Token: 0x0400124E RID: 4686
			KerbNoElevationLogon = 83,
			// Token: 0x0400124F RID: 4687
			KerbLuidLogon
		}

		// Token: 0x0200041D RID: 1053
		public enum SECURITY_LOGON_TYPE
		{
			// Token: 0x04001251 RID: 4689
			UndefinedLogonType,
			// Token: 0x04001252 RID: 4690
			Interactive = 2,
			// Token: 0x04001253 RID: 4691
			Network,
			// Token: 0x04001254 RID: 4692
			Batch,
			// Token: 0x04001255 RID: 4693
			Service,
			// Token: 0x04001256 RID: 4694
			Proxy,
			// Token: 0x04001257 RID: 4695
			Unlock,
			// Token: 0x04001258 RID: 4696
			NetworkCleartext,
			// Token: 0x04001259 RID: 4697
			NewCredentials,
			// Token: 0x0400125A RID: 4698
			RemoteInteractive,
			// Token: 0x0400125B RID: 4699
			CachedInteractive,
			// Token: 0x0400125C RID: 4700
			CachedRemoteInteractive,
			// Token: 0x0400125D RID: 4701
			CachedUnlock
		}

		// Token: 0x0200041E RID: 1054
		internal struct LSA_STRING
		{
			// Token: 0x0400125E RID: 4702
			public ushort Length;

			// Token: 0x0400125F RID: 4703
			public ushort MaximumLength;

			// Token: 0x04001260 RID: 4704
			public string Buffer;
		}

		// Token: 0x0200041F RID: 1055
		internal struct LUID
		{
			// Token: 0x06001ED1 RID: 7889 RVA: 0x0006E79D File Offset: 0x0006C99D
			public static implicit operator ulong(NativeMethods.LUID luid)
			{
				return (ulong)(((long)luid.HighPart << 32) + (long)((ulong)luid.LowPart));
			}

			// Token: 0x06001ED2 RID: 7890 RVA: 0x0006E7B4 File Offset: 0x0006C9B4
			public static implicit operator NativeMethods.LUID(long luid)
			{
				return new NativeMethods.LUID
				{
					LowPart = (uint)(luid & (long)((ulong)(-1))),
					HighPart = (int)(luid >> 32)
				};
			}

			// Token: 0x04001261 RID: 4705
			public uint LowPart;

			// Token: 0x04001262 RID: 4706
			public int HighPart;
		}

		// Token: 0x02000420 RID: 1056
		public struct KERB_SUBMIT_TKT_REQUEST
		{
			// Token: 0x04001263 RID: 4707
			public NativeMethods.KERB_PROTOCOL_MESSAGE_TYPE MessageType;

			// Token: 0x04001264 RID: 4708
			public NativeMethods.LUID LogonId;

			// Token: 0x04001265 RID: 4709
			public int Flags;

			// Token: 0x04001266 RID: 4710
			public NativeMethods.KERB_CRYPTO_KEY32 Key;

			// Token: 0x04001267 RID: 4711
			public int KerbCredSize;

			// Token: 0x04001268 RID: 4712
			public int KerbCredOffset;
		}

		// Token: 0x02000421 RID: 1057
		public struct KERB_PURGE_TKT_CACHE_EX_REQUEST
		{
			// Token: 0x04001269 RID: 4713
			public NativeMethods.KERB_PROTOCOL_MESSAGE_TYPE MessageType;

			// Token: 0x0400126A RID: 4714
			public NativeMethods.LUID LogonId;

			// Token: 0x0400126B RID: 4715
			public int Flags;

			// Token: 0x0400126C RID: 4716
			public NativeMethods.KERB_TICKET_CACHE_INFO_EX TicketTemplate;
		}

		// Token: 0x02000422 RID: 1058
		public struct KERB_TICKET_CACHE_INFO_EX
		{
			// Token: 0x0400126D RID: 4717
			public NativeMethods.UNICODE_STRING ClientName;

			// Token: 0x0400126E RID: 4718
			public NativeMethods.UNICODE_STRING ClientRealm;

			// Token: 0x0400126F RID: 4719
			public NativeMethods.UNICODE_STRING ServerName;

			// Token: 0x04001270 RID: 4720
			public NativeMethods.UNICODE_STRING ServerRealm;

			// Token: 0x04001271 RID: 4721
			public long StartTime;

			// Token: 0x04001272 RID: 4722
			public long EndTime;

			// Token: 0x04001273 RID: 4723
			public long RenewTime;

			// Token: 0x04001274 RID: 4724
			public int EncryptionType;

			// Token: 0x04001275 RID: 4725
			public int TicketFlags;
		}

		// Token: 0x02000423 RID: 1059
		public enum KERB_PROTOCOL_MESSAGE_TYPE : uint
		{
			// Token: 0x04001277 RID: 4727
			KerbDebugRequestMessage,
			// Token: 0x04001278 RID: 4728
			KerbQueryTicketCacheMessage,
			// Token: 0x04001279 RID: 4729
			KerbChangeMachinePasswordMessage,
			// Token: 0x0400127A RID: 4730
			KerbVerifyPacMessage,
			// Token: 0x0400127B RID: 4731
			KerbRetrieveTicketMessage,
			// Token: 0x0400127C RID: 4732
			KerbUpdateAddressesMessage,
			// Token: 0x0400127D RID: 4733
			KerbPurgeTicketCacheMessage,
			// Token: 0x0400127E RID: 4734
			KerbChangePasswordMessage,
			// Token: 0x0400127F RID: 4735
			KerbRetrieveEncodedTicketMessage,
			// Token: 0x04001280 RID: 4736
			KerbDecryptDataMessage,
			// Token: 0x04001281 RID: 4737
			KerbAddBindingCacheEntryMessage,
			// Token: 0x04001282 RID: 4738
			KerbSetPasswordMessage,
			// Token: 0x04001283 RID: 4739
			KerbSetPasswordExMessage,
			// Token: 0x04001284 RID: 4740
			KerbVerifyCredentialsMessage,
			// Token: 0x04001285 RID: 4741
			KerbQueryTicketCacheExMessage,
			// Token: 0x04001286 RID: 4742
			KerbPurgeTicketCacheExMessage,
			// Token: 0x04001287 RID: 4743
			KerbRefreshSmartcardCredentialsMessage,
			// Token: 0x04001288 RID: 4744
			KerbAddExtraCredentialsMessage,
			// Token: 0x04001289 RID: 4745
			KerbQuerySupplementalCredentialsMessage,
			// Token: 0x0400128A RID: 4746
			KerbTransferCredentialsMessage,
			// Token: 0x0400128B RID: 4747
			KerbQueryTicketCacheEx2Message,
			// Token: 0x0400128C RID: 4748
			KerbSubmitTicketMessage,
			// Token: 0x0400128D RID: 4749
			KerbAddExtraCredentialsExMessage,
			// Token: 0x0400128E RID: 4750
			KerbQueryKdcProxyCacheMessage,
			// Token: 0x0400128F RID: 4751
			KerbPurgeKdcProxyCacheMessage,
			// Token: 0x04001290 RID: 4752
			KerbQueryTicketCacheEx3Message,
			// Token: 0x04001291 RID: 4753
			KerbCleanupMachinePkinitCredsMessage,
			// Token: 0x04001292 RID: 4754
			KerbAddBindingCacheEntryExMessage,
			// Token: 0x04001293 RID: 4755
			KerbQueryBindingCacheMessage,
			// Token: 0x04001294 RID: 4756
			KerbPurgeBindingCacheMessage,
			// Token: 0x04001295 RID: 4757
			KerbPinKdcMessage,
			// Token: 0x04001296 RID: 4758
			KerbUnpinAllKdcsMessage,
			// Token: 0x04001297 RID: 4759
			KerbQueryDomainExtendedPoliciesMessage,
			// Token: 0x04001298 RID: 4760
			KerbQueryS4U2ProxyCacheMessage,
			// Token: 0x04001299 RID: 4761
			KerbRetrieveKeyTabMessage,
			// Token: 0x0400129A RID: 4762
			KerbRefreshPolicyMessage
		}

		// Token: 0x02000424 RID: 1060
		public struct KERB_CRYPTO_KEY32
		{
			// Token: 0x0400129B RID: 4763
			public int KeyType;

			// Token: 0x0400129C RID: 4764
			public int Length;

			// Token: 0x0400129D RID: 4765
			public int Offset;
		}

		// Token: 0x02000425 RID: 1061
		internal enum SecBufferType
		{
			// Token: 0x0400129F RID: 4767
			SECBUFFER_VERSION,
			// Token: 0x040012A0 RID: 4768
			SECBUFFER_DATA,
			// Token: 0x040012A1 RID: 4769
			SECBUFFER_TOKEN
		}

		// Token: 0x02000426 RID: 1062
		internal struct SECURITY_HANDLE
		{
			// Token: 0x17000618 RID: 1560
			// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x0006E7E2 File Offset: 0x0006C9E2
			public bool IsSet
			{
				get
				{
					return this.dwLower > 0UL || this.dwUpper > 0UL;
				}
			}

			// Token: 0x040012A2 RID: 4770
			public ulong dwLower;

			// Token: 0x040012A3 RID: 4771
			public ulong dwUpper;
		}

		// Token: 0x02000427 RID: 1063
		internal struct SECURITY_INTEGER
		{
			// Token: 0x040012A4 RID: 4772
			public uint LowPart;

			// Token: 0x040012A5 RID: 4773
			public int HighPart;
		}

		// Token: 0x02000428 RID: 1064
		internal struct SecPkgContext_SecString
		{
			// Token: 0x040012A6 RID: 4774
			public unsafe void* sValue;
		}

		// Token: 0x02000429 RID: 1065
		internal struct SecBuffer
		{
			// Token: 0x06001ED4 RID: 7892 RVA: 0x0006E7FA File Offset: 0x0006C9FA
			public SecBuffer(int bufferSize)
			{
				this.cbBuffer = bufferSize;
				this.BufferType = NativeMethods.SecBufferType.SECBUFFER_TOKEN;
				this.pvBuffer = Marshal.AllocHGlobal(bufferSize);
			}

			// Token: 0x06001ED5 RID: 7893 RVA: 0x0006E816 File Offset: 0x0006CA16
			public SecBuffer(byte[] secBufferBytes)
			{
				this = new NativeMethods.SecBuffer(secBufferBytes.Length);
				Marshal.Copy(secBufferBytes, 0, this.pvBuffer, this.cbBuffer);
			}

			// Token: 0x06001ED6 RID: 7894 RVA: 0x0006E834 File Offset: 0x0006CA34
			public void Dispose()
			{
				if (this.pvBuffer != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.pvBuffer);
					this.pvBuffer = IntPtr.Zero;
				}
			}

			// Token: 0x040012A7 RID: 4775
			public int cbBuffer;

			// Token: 0x040012A8 RID: 4776
			public NativeMethods.SecBufferType BufferType;

			// Token: 0x040012A9 RID: 4777
			public IntPtr pvBuffer;
		}

		// Token: 0x0200042A RID: 1066
		internal struct SecBufferDesc : IDisposable
		{
			// Token: 0x06001ED7 RID: 7895 RVA: 0x0006E85E File Offset: 0x0006CA5E
			public SecBufferDesc(int bufferSize)
			{
				this = new NativeMethods.SecBufferDesc(new NativeMethods.SecBuffer(bufferSize));
			}

			// Token: 0x06001ED8 RID: 7896 RVA: 0x0006E86C File Offset: 0x0006CA6C
			public SecBufferDesc(byte[] secBufferBytes)
			{
				this = new NativeMethods.SecBufferDesc(new NativeMethods.SecBuffer(secBufferBytes));
			}

			// Token: 0x06001ED9 RID: 7897 RVA: 0x0006E87A File Offset: 0x0006CA7A
			private SecBufferDesc(NativeMethods.SecBuffer secBuffer)
			{
				this.ulVersion = NativeMethods.SecBufferType.SECBUFFER_VERSION;
				this.cBuffers = 1;
				this.pBuffers = Marshal.AllocHGlobal(Marshal.SizeOf<NativeMethods.SecBuffer>(secBuffer));
				Marshal.StructureToPtr<NativeMethods.SecBuffer>(secBuffer, this.pBuffers, false);
			}

			// Token: 0x06001EDA RID: 7898 RVA: 0x0006E8A8 File Offset: 0x0006CAA8
			public void Dispose()
			{
				if (this.pBuffers != IntPtr.Zero)
				{
					this.ForEachBuffer(delegate(NativeMethods.SecBuffer thisSecBuffer)
					{
						thisSecBuffer.Dispose();
					});
					Marshal.FreeHGlobal(this.pBuffers);
					this.pBuffers = IntPtr.Zero;
				}
			}

			// Token: 0x06001EDB RID: 7899 RVA: 0x0006E904 File Offset: 0x0006CB04
			private void ForEachBuffer(Action<NativeMethods.SecBuffer> onBuffer)
			{
				for (int i = 0; i < this.cBuffers; i++)
				{
					int num = i * Marshal.SizeOf(typeof(NativeMethods.SecBuffer));
					NativeMethods.SecBuffer secBuffer = (NativeMethods.SecBuffer)Marshal.PtrToStructure(IntPtr.Add(this.pBuffers, num), typeof(NativeMethods.SecBuffer));
					onBuffer(secBuffer);
				}
			}

			// Token: 0x06001EDC RID: 7900 RVA: 0x0006E95C File Offset: 0x0006CB5C
			public byte[] ReadBytes()
			{
				if (this.cBuffers <= 0)
				{
					return Array.Empty<byte>();
				}
				int finalLen = 0;
				List<byte[]> bufferList = new List<byte[]>();
				this.ForEachBuffer(delegate(NativeMethods.SecBuffer thisSecBuffer)
				{
					if (thisSecBuffer.cbBuffer <= 0)
					{
						return;
					}
					byte[] array2 = new byte[thisSecBuffer.cbBuffer];
					Marshal.Copy(thisSecBuffer.pvBuffer, array2, 0, thisSecBuffer.cbBuffer);
					bufferList.Add(array2);
					finalLen += thisSecBuffer.cbBuffer;
				});
				byte[] array = new byte[finalLen];
				int num = 0;
				for (int i = 0; i < bufferList.Count; i++)
				{
					bufferList[i].CopyTo(array, num);
					num += bufferList[i].Length - 1;
				}
				return array;
			}

			// Token: 0x040012AA RID: 4778
			private readonly NativeMethods.SecBufferType ulVersion;

			// Token: 0x040012AB RID: 4779
			public int cBuffers;

			// Token: 0x040012AC RID: 4780
			public IntPtr pBuffers;
		}
	}
}
