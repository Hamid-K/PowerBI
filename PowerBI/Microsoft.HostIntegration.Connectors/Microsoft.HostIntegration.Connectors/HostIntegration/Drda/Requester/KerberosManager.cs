using System;
using System.Runtime.InteropServices;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Tracing;
using Microsoft.HostIntegration.Tracing.DrdaClient;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x0200091F RID: 2335
	internal class KerberosManager : Manager
	{
		// Token: 0x060049A2 RID: 18850 RVA: 0x0011290C File Offset: 0x00110B0C
		public KerberosManager(Requester requester, SecurityManagerTracePoint parentTracepoint)
			: base(requester)
		{
			this._tracePoint = new KerberosManagerTracePoint(parentTracepoint);
			this._managerCodepoint = ManagerCodePoint.UNKNOWN;
			this._credHandle.SetToInvalid();
		}

		// Token: 0x170011E3 RID: 4579
		// (get) Token: 0x060049A3 RID: 18851 RVA: 0x0011293E File Offset: 0x00110B3E
		// (set) Token: 0x060049A4 RID: 18852 RVA: 0x00112946 File Offset: 0x00110B46
		public string PrincipleName
		{
			get
			{
				return this._principleName;
			}
			set
			{
				this._principleName = value;
			}
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00112950 File Offset: 0x00110B50
		public override void Initialize()
		{
			base.Initialize();
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter KerberosManager::Initialize");
			}
			this._credHandle.SetToInvalid();
			this._maxTokenSize = 0U;
			this._context = IntPtr.Zero;
			this._outputDesc = null;
			IntPtr zero = IntPtr.Zero;
			int num = KerberosManager.QuerySecurityPackageInfo("Kerberos", out zero);
			if (num != 0)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Error))
				{
					this._tracePoint.Trace(TraceFlags.Error, "Kerberos security package is not available.");
				}
				this.ReleaseResource();
				throw this._requester.MakeException(RequesterResource.KerberosInitializeError(num), "HY000", -1006);
			}
			KerberosManager.SecPkgInfo secPkgInfo = (KerberosManager.SecPkgInfo)Marshal.PtrToStructure(zero, typeof(KerberosManager.SecPkgInfo));
			this._maxTokenSize = secPkgInfo.cbMaxToken;
			KerberosManager.FreeContextBuffer(zero);
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Kerberos Max Token Size: " + this._maxTokenSize.ToString());
			}
			this._outputDesc = new KerberosManager.SingleTokenSecBufferDesc((int)this._maxTokenSize);
			this._context = Marshal.AllocHGlobal(IntPtr.Size * 2);
			Marshal.WriteIntPtr(this._context, IntPtr.Zero);
			Marshal.WriteIntPtr(IntPtr.Add(this._context, IntPtr.Size), IntPtr.Zero);
			long num2 = 0L;
			if (KerberosManager.AcquireCredentialsHandle(null, "Kerberos", 2, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, ref this._credHandle, out num2) == 0)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Kerberos handle has been successfully acquired.");
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Exit KerberosManager::Initialize");
				}
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (this._tracePoint.IsEnabled(TraceFlags.Error))
			{
				this._tracePoint.Trace(TraceFlags.Error, "Kerberos handle could NOT be acquired. error code: " + lastWin32Error.ToString());
			}
			this.ReleaseResource();
			throw this._requester.MakeException(RequesterResource.KerberosInitializeError(lastWin32Error), "HY000", -1006, lastWin32Error);
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x00112B7C File Offset: 0x00110D7C
		public void ProcessSecurityToken(ref byte[] secToken)
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter KerberosManager::ProcessSecurityToken");
			}
			KerberosManager.SingleTokenSecBufferDesc singleTokenSecBufferDesc = null;
			if (secToken != null)
			{
				singleTokenSecBufferDesc = new KerberosManager.SingleTokenSecBufferDesc();
				singleTokenSecBufferDesc.SetBuffer(secToken);
				this._outputDesc.ResetBuffer();
			}
			uint num = 0U;
			long num2 = 0L;
			int num3 = KerberosManager.InitializeSecurityContext(ref this._credHandle, (secToken == null) ? IntPtr.Zero : this._context, this._principleName, 65694U, 0, 16U, (singleTokenSecBufferDesc == null) ? IntPtr.Zero : singleTokenSecBufferDesc.UnmanagedPointer, 0, this._context, this._outputDesc.UnmanagedPointer, out num, out num2);
			if (singleTokenSecBufferDesc != null)
			{
				singleTokenSecBufferDesc.ReleaseBuffer();
			}
			if (num3 == 0 || num3 == 590612 || num3 == 590610)
			{
				secToken = this._outputDesc.Read();
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "InitializeSecurityContext is successfull at: " + num3.ToString());
				}
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Exit KerberosManager::ProcessSecurityToken");
				}
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (this._tracePoint.IsEnabled(TraceFlags.Error))
			{
				this._tracePoint.Trace(TraceFlags.Error, "InitializeSecurityContext failed at: " + lastWin32Error.ToString());
			}
			this.ReleaseResource();
			throw this._requester.MakeException(RequesterResource.KerberosValidateError(lastWin32Error), "HY000", -1007, lastWin32Error);
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x00112CF0 File Offset: 0x00110EF0
		public override void Reset()
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter KerberosManager::Reset");
			}
			this.ReleaseResource();
			this.Initialize();
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit KerberosManager::Reset");
			}
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x00112D4C File Offset: 0x00110F4C
		public void ReleaseResource()
		{
			if (this._tracePoint.IsEnabled(TraceFlags.Debug))
			{
				this._tracePoint.Trace(TraceFlags.Debug, "Enter KerberosManager::ReleaseResource");
			}
			if (!this._credHandle.IsZero)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Free Kerberos handle.");
				}
				KerberosManager.FreeCredentialsHandle(ref this._credHandle);
				this._credHandle.SetToInvalid();
			}
			if (this._outputDesc != null)
			{
				this._outputDesc.ReleaseBuffer();
				this._outputDesc = null;
			}
			if (this._context != IntPtr.Zero)
			{
				if (this._tracePoint.IsEnabled(TraceFlags.Information))
				{
					this._tracePoint.Trace(TraceFlags.Information, "Free Kerberos security context.");
				}
				if (IntPtr.Zero != Marshal.ReadIntPtr(this._context) || IntPtr.Zero != Marshal.ReadIntPtr(IntPtr.Add(this._context, IntPtr.Size)))
				{
					KerberosManager.DeleteSecurityContext(this._context);
				}
				Marshal.FreeHGlobal(this._context);
				this._context = IntPtr.Zero;
			}
			if (this._tracePoint.IsEnabled(TraceFlags.Information))
			{
				this._tracePoint.Trace(TraceFlags.Information, "Exit KerberosManager::ReleaseResource");
			}
		}

		// Token: 0x060049A9 RID: 18857
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int AcquireCredentialsHandle([In] string principal, [In] string package, [In] int usage, [In] IntPtr logonID, [In] IntPtr authData, [In] IntPtr getKeyFn, [In] IntPtr keyArgument, [In] [Out] ref KerberosManager.SSPIHandle credentialHandle, out long expiryTime);

		// Token: 0x060049AA RID: 18858
		[DllImport("secur32.dll", SetLastError = true)]
		private static extern int AcceptSecurityContext([In] ref KerberosManager.SSPIHandle credentialHandle, [In] IntPtr context, [In] IntPtr pInput, [In] uint contextReq, [In] uint targetDataRep, [In] IntPtr newContext, [In] IntPtr pOutput, out uint contextAttr, out long timeStamp);

		// Token: 0x060049AB RID: 18859
		[DllImport("secur32.dll", SetLastError = true)]
		private static extern int InitializeSecurityContext([In] ref KerberosManager.SSPIHandle credentialHandle, [In] IntPtr context, [In] string targetName, [In] uint contextReq, [In] int reserved1, [In] uint targetDataRep, [In] IntPtr pInput, [In] int reserved2, [In] IntPtr newContext, [In] IntPtr pOutput, out uint contextAttr, out long timeStamp);

		// Token: 0x060049AC RID: 18860
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int QuerySecurityPackageInfo([In] string packageName, out IntPtr packageInfo);

		// Token: 0x060049AD RID: 18861
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int FreeCredentialsHandle([In] ref KerberosManager.SSPIHandle handle);

		// Token: 0x060049AE RID: 18862
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int FreeContextBuffer([In] IntPtr contextBuffer);

		// Token: 0x060049AF RID: 18863
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int DeleteSecurityContext([In] IntPtr context);

		// Token: 0x060049B0 RID: 18864
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern int ImpersonateSecurityContext([In] IntPtr context);

		// Token: 0x04003721 RID: 14113
		private KerberosManager.SSPIHandle _credHandle;

		// Token: 0x04003722 RID: 14114
		private uint _maxTokenSize;

		// Token: 0x04003723 RID: 14115
		private IntPtr _context = IntPtr.Zero;

		// Token: 0x04003724 RID: 14116
		private KerberosManager.SingleTokenSecBufferDesc _outputDesc;

		// Token: 0x04003725 RID: 14117
		private string _principleName;

		// Token: 0x02000920 RID: 2336
		private class RequestFlag
		{
			// Token: 0x04003726 RID: 14118
			public const int ISC_REQ_DELEGATE = 1;

			// Token: 0x04003727 RID: 14119
			public const int ISC_REQ_MUTUAL_AUTH = 2;

			// Token: 0x04003728 RID: 14120
			public const int ISC_REQ_REPLAY_DETECT = 4;

			// Token: 0x04003729 RID: 14121
			public const int ISC_REQ_SEQUENCE_DETECT = 8;

			// Token: 0x0400372A RID: 14122
			public const int ISC_REQ_CONFIDENTIALITY = 16;

			// Token: 0x0400372B RID: 14123
			public const int ISC_REQ_USE_SESSION_KEY = 32;

			// Token: 0x0400372C RID: 14124
			public const int ISC_REQ_PROMPT_FOR_CREDS = 64;

			// Token: 0x0400372D RID: 14125
			public const int ISC_REQ_USE_SUPPLIED_CREDS = 128;

			// Token: 0x0400372E RID: 14126
			public const int ISC_REQ_ALLOCATE_MEMORY = 256;

			// Token: 0x0400372F RID: 14127
			public const int ISC_REQ_USE_DCE_STYLE = 512;

			// Token: 0x04003730 RID: 14128
			public const int ISC_REQ_DATAGRAM = 1024;

			// Token: 0x04003731 RID: 14129
			public const int ISC_REQ_CONNECTION = 2048;

			// Token: 0x04003732 RID: 14130
			public const int ISC_REQ_CALL_LEVEL = 4096;

			// Token: 0x04003733 RID: 14131
			public const int ISC_REQ_FRAGMENT_SUPPLIED = 8192;

			// Token: 0x04003734 RID: 14132
			public const int ISC_REQ_EXTENDED_ERROR = 16384;

			// Token: 0x04003735 RID: 14133
			public const int ISC_REQ_STREAM = 32768;

			// Token: 0x04003736 RID: 14134
			public const int ISC_REQ_INTEGRITY = 65536;

			// Token: 0x04003737 RID: 14135
			public const int ISC_REQ_IDENTIFY = 131072;

			// Token: 0x04003738 RID: 14136
			public const int ISC_REQ_NULL_SESSION = 262144;

			// Token: 0x04003739 RID: 14137
			public const int ISC_REQ_MANUAL_CRED_VALIDATION = 524288;

			// Token: 0x0400373A RID: 14138
			public const int ISC_REQ_RESERVED1 = 1048576;

			// Token: 0x0400373B RID: 14139
			public const int ISC_REQ_FRAGMENT_TO_FIT = 2097152;
		}

		// Token: 0x02000921 RID: 2337
		private enum CredentialUse
		{
			// Token: 0x0400373D RID: 14141
			Inbound = 1,
			// Token: 0x0400373E RID: 14142
			Outbound,
			// Token: 0x0400373F RID: 14143
			Both
		}

		// Token: 0x02000922 RID: 2338
		private enum Endianness
		{
			// Token: 0x04003741 RID: 14145
			Network,
			// Token: 0x04003742 RID: 14146
			Native = 16
		}

		// Token: 0x02000923 RID: 2339
		internal enum SecurityStatus
		{
			// Token: 0x04003744 RID: 14148
			OK,
			// Token: 0x04003745 RID: 14149
			ContinueNeeded = 590610,
			// Token: 0x04003746 RID: 14150
			CompAndContinue = 590612,
			// Token: 0x04003747 RID: 14151
			ContextExpired = 590615,
			// Token: 0x04003748 RID: 14152
			CredentialsNeeded = 590624,
			// Token: 0x04003749 RID: 14153
			Renegotiate,
			// Token: 0x0400374A RID: 14154
			OutOfMemory = -2146893056,
			// Token: 0x0400374B RID: 14155
			InvalidHandle,
			// Token: 0x0400374C RID: 14156
			Unsupported,
			// Token: 0x0400374D RID: 14157
			TargetUnknown,
			// Token: 0x0400374E RID: 14158
			InternalError,
			// Token: 0x0400374F RID: 14159
			PackageNotFound,
			// Token: 0x04003750 RID: 14160
			NotOwner,
			// Token: 0x04003751 RID: 14161
			CannotInstall,
			// Token: 0x04003752 RID: 14162
			InvalidToken,
			// Token: 0x04003753 RID: 14163
			CannotPack,
			// Token: 0x04003754 RID: 14164
			QopNotSupported,
			// Token: 0x04003755 RID: 14165
			NoImpersonation,
			// Token: 0x04003756 RID: 14166
			LogonDenied,
			// Token: 0x04003757 RID: 14167
			UnknownCredentials,
			// Token: 0x04003758 RID: 14168
			NoCredentials,
			// Token: 0x04003759 RID: 14169
			MessageAltered,
			// Token: 0x0400375A RID: 14170
			OutOfSequence,
			// Token: 0x0400375B RID: 14171
			NoAuthenticatingAuthority,
			// Token: 0x0400375C RID: 14172
			IncompleteMessage = -2146893032,
			// Token: 0x0400375D RID: 14173
			IncompleteCredentials = -2146893024,
			// Token: 0x0400375E RID: 14174
			BufferNotEnough,
			// Token: 0x0400375F RID: 14175
			WrongPrincipal,
			// Token: 0x04003760 RID: 14176
			TimeSkew = -2146893020,
			// Token: 0x04003761 RID: 14177
			UntrustedRoot,
			// Token: 0x04003762 RID: 14178
			IllegalMessage,
			// Token: 0x04003763 RID: 14179
			CertUnknown,
			// Token: 0x04003764 RID: 14180
			CertExpired,
			// Token: 0x04003765 RID: 14181
			AlgorithmMismatch = -2146893007,
			// Token: 0x04003766 RID: 14182
			SecurityQosFailed,
			// Token: 0x04003767 RID: 14183
			SmartcardLogonRequired = -2146892994,
			// Token: 0x04003768 RID: 14184
			UnsupportedPreauth = -2146892989
		}

		// Token: 0x02000924 RID: 2340
		[Flags]
		private enum ContextFlags
		{
			// Token: 0x0400376A RID: 14186
			Zero = 0,
			// Token: 0x0400376B RID: 14187
			MutalAuth = 2,
			// Token: 0x0400376C RID: 14188
			ReplayDetect = 4,
			// Token: 0x0400376D RID: 14189
			SequenceDetect = 8,
			// Token: 0x0400376E RID: 14190
			Confidentiality = 16,
			// Token: 0x0400376F RID: 14191
			Integrity = 131072
		}

		// Token: 0x02000925 RID: 2341
		private struct SecPkgInfo
		{
			// Token: 0x04003770 RID: 14192
			public uint fCapabilities;

			// Token: 0x04003771 RID: 14193
			public ushort wVersion;

			// Token: 0x04003772 RID: 14194
			public ushort wRPCID;

			// Token: 0x04003773 RID: 14195
			public uint cbMaxToken;

			// Token: 0x04003774 RID: 14196
			public IntPtr Name;

			// Token: 0x04003775 RID: 14197
			public IntPtr Comment;
		}

		// Token: 0x02000926 RID: 2342
		private enum BufferType
		{
			// Token: 0x04003777 RID: 14199
			Token = 2
		}

		// Token: 0x02000927 RID: 2343
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		private struct SSPIHandle
		{
			// Token: 0x170011E4 RID: 4580
			// (get) Token: 0x060049B2 RID: 18866 RVA: 0x00112E81 File Offset: 0x00111081
			public bool IsZero
			{
				get
				{
					return this.dwLower == IntPtr.Zero && this.dwUpper == IntPtr.Zero;
				}
			}

			// Token: 0x060049B3 RID: 18867 RVA: 0x00112EA7 File Offset: 0x001110A7
			internal void SetToInvalid()
			{
				this.dwLower = IntPtr.Zero;
				this.dwUpper = IntPtr.Zero;
			}

			// Token: 0x04003778 RID: 14200
			private IntPtr dwLower;

			// Token: 0x04003779 RID: 14201
			private IntPtr dwUpper;
		}

		// Token: 0x02000928 RID: 2344
		private class SingleTokenSecBufferDesc
		{
			// Token: 0x060049B4 RID: 18868 RVA: 0x00112EBF File Offset: 0x001110BF
			internal SingleTokenSecBufferDesc()
			{
				this.UnmanagedPointer = IntPtr.Zero;
				this.originalBufferLength = 0;
			}

			// Token: 0x060049B5 RID: 18869 RVA: 0x00112ED9 File Offset: 0x001110D9
			internal SingleTokenSecBufferDesc(int size)
			{
				this.AllocBufferMemory(size);
			}

			// Token: 0x060049B6 RID: 18870 RVA: 0x00112EEC File Offset: 0x001110EC
			internal byte[] Read()
			{
				IntPtr intPtr = Marshal.ReadIntPtr(this.UnmanagedPointer, 8);
				int num = Marshal.ReadInt32(intPtr);
				IntPtr intPtr2 = Marshal.ReadIntPtr(intPtr, 8);
				byte[] array = new byte[num];
				Marshal.Copy(intPtr2, array, 0, num);
				return array;
			}

			// Token: 0x060049B7 RID: 18871 RVA: 0x00112F24 File Offset: 0x00111124
			internal void SetBuffer(byte[] buffer)
			{
				this.ReleaseBuffer();
				IntPtr intPtr = this.AllocBufferMemory(buffer.Length);
				Marshal.Copy(buffer, 0, intPtr, buffer.Length);
			}

			// Token: 0x060049B8 RID: 18872 RVA: 0x00112F4C File Offset: 0x0011114C
			internal void ResetBuffer()
			{
				if (this.UnmanagedPointer != IntPtr.Zero)
				{
					Marshal.WriteInt32(Marshal.ReadIntPtr(this.UnmanagedPointer, 8), this.originalBufferLength);
				}
			}

			// Token: 0x060049B9 RID: 18873 RVA: 0x00112F77 File Offset: 0x00111177
			internal void ReleaseBuffer()
			{
				if (this.UnmanagedPointer != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(this.UnmanagedPointer);
					this.UnmanagedPointer = IntPtr.Zero;
					this.originalBufferLength = 0;
				}
			}

			// Token: 0x060049BA RID: 18874 RVA: 0x00112FA8 File Offset: 0x001111A8
			private IntPtr AllocBufferMemory(int bufferLength)
			{
				int num = bufferLength + 16 + 2 * IntPtr.Size;
				this.UnmanagedPointer = Marshal.AllocHGlobal(num);
				IntPtr unmanagedPointer = this.UnmanagedPointer;
				Marshal.WriteInt32(unmanagedPointer, 0);
				IntPtr intPtr = IntPtr.Add(unmanagedPointer, 4);
				Marshal.WriteInt32(intPtr, 1);
				IntPtr intPtr2 = IntPtr.Add(intPtr, 4);
				Marshal.WriteIntPtr(intPtr2, IntPtr.Add(intPtr2, IntPtr.Size));
				IntPtr intPtr3 = IntPtr.Add(intPtr2, IntPtr.Size);
				Marshal.WriteInt32(intPtr3, bufferLength);
				IntPtr intPtr4 = IntPtr.Add(intPtr3, 4);
				Marshal.WriteInt32(intPtr4, 2);
				IntPtr intPtr5 = IntPtr.Add(intPtr4, 4);
				Marshal.WriteIntPtr(intPtr5, IntPtr.Add(intPtr5, IntPtr.Size));
				IntPtr intPtr6 = IntPtr.Add(intPtr5, IntPtr.Size);
				this.originalBufferLength = bufferLength;
				return intPtr6;
			}

			// Token: 0x0400377A RID: 14202
			private int originalBufferLength;

			// Token: 0x0400377B RID: 14203
			internal IntPtr UnmanagedPointer;
		}
	}
}
