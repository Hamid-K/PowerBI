using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.AnalysisServices.Interop;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices.MsoId
{
	// Token: 0x0200011F RID: 287
	internal static class MsoIdClient
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x0003886C File Offset: 0x00036A6C
		static MsoIdClient()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\MSOIdentityCRL");
			if (registryKey == null)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInstalled);
			}
			string text;
			try
			{
				text = registryKey.GetValue("TargetDir", string.Empty) as string;
			}
			finally
			{
				registryKey.Close();
			}
			if (string.IsNullOrEmpty(text))
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInstalled);
			}
			string text2 = Path.Combine(text, "msoidcli.dll");
			if (!File.Exists(text2))
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInstalled);
			}
			MsoIdClient.module = NativeMethods.LoadLibrary(text2);
			if (MsoIdClient.module == IntPtr.Zero)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.LoadFailure);
			}
			MsoIdClient.finalizer = new MsoIdClient.MsoIdClientFinalizer();
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x00038918 File Offset: 0x00036B18
		public static int Initialize(Guid guid, int version, UPDATE_FLAG flags, params KeyValuePair<IDCRL_OPTION_ID, object>[] options)
		{
			if (options == null || options.Length == 0)
			{
				return MsoIdClient.InitializeExImpl(guid, version, flags, null);
			}
			List<IDCRL_OPTION> list = new List<IDCRL_OPTION>(options.Length);
			int num;
			try
			{
				for (int i = 0; i < options.Length; i++)
				{
					list.Add(MsoIdClient.ConvertManagedOption(options[i].Key, options[i].Value));
				}
				num = MsoIdClient.InitializeExImpl(guid, version, flags, list.ToArray());
			}
			finally
			{
				for (int j = 0; j < list.Count; j++)
				{
					if (list[j].pValue != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(list[j].pValue);
					}
				}
			}
			return num;
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x000389D0 File Offset: 0x00036BD0
		public static int Initialize(Guid guid, int version, UPDATE_FLAG flags, IDCRL_OPTION[] options)
		{
			return MsoIdClient.InitializeExImpl(guid, version, flags, options);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x000389DC File Offset: 0x00036BDC
		public static int Reset()
		{
			int num = -1;
			if (Interlocked.CompareExchange(ref MsoIdClient.state, -2, 1) != 1)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInitialized);
			}
			try
			{
				num = MsoIdClient.Uninitialize();
			}
			finally
			{
				Interlocked.Exchange(ref MsoIdClient.state, (num < 0) ? 1 : 0);
			}
			if (num < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.ResetFailure, num);
			}
			return num;
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x00038A38 File Offset: 0x00036C38
		public static int CloseIdentity(IntPtr hIdentity)
		{
			if (Interlocked.CompareExchange(ref MsoIdClient.state, 1, 1) != 1)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInitialized);
			}
			int num = MsoIdClient.CloseIdentityHandle(hIdentity);
			if (num < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num);
			}
			return num;
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x00038A70 File Offset: 0x00036C70
		public static int CreateIdentity(string memberName, IDENTITY_FLAG flags, out IntPtr hIdentity)
		{
			if (Interlocked.CompareExchange(ref MsoIdClient.state, 1, 1) != 1)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInitialized);
			}
			int num = MsoIdClient.CreateIdentityHandle(memberName, (uint)flags, out hIdentity);
			if (num < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num);
			}
			return num;
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x00038AAC File Offset: 0x00036CAC
		public static int GetAuthenticationState(IntPtr hIdentity, out int hrAuthState, out int hrAuthRequired, out int hrRequestStatus, out string webFlowUrl)
		{
			if (Interlocked.CompareExchange(ref MsoIdClient.state, 1, 1) != 1)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInitialized);
			}
			IntPtr intPtr;
			int authState = MsoIdClient.GetAuthState(hIdentity, out hrAuthState, out hrAuthRequired, out hrRequestStatus, out intPtr);
			if (authState < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, authState);
			}
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					webFlowUrl = Marshal.PtrToStringUni(intPtr);
					return authState;
				}
				finally
				{
					int num = MsoIdClient.PassportFreeMemory(intPtr);
					if (num < 0)
					{
						throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num);
					}
				}
			}
			webFlowUrl = null;
			return authState;
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x00038B2C File Offset: 0x00036D2C
		public static int GetAuthenticationServiceToken(IntPtr hIdentity, string serviceTarget, string servicePolicy, SERVICETOKENFLAGS tokenFlags, out string token, out uint resultFlags, out byte[] sessionKey)
		{
			if (Interlocked.CompareExchange(ref MsoIdClient.state, 1, 1) != 1)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInitialized);
			}
			IntPtr intPtr;
			IntPtr intPtr2;
			uint num2;
			int num = MsoIdClient.AuthIdentityToService(hIdentity, serviceTarget, servicePolicy, (uint)tokenFlags, out intPtr, out resultFlags, out intPtr2, out num2);
			if (num < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num);
			}
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					token = Marshal.PtrToStringUni(intPtr);
					goto IL_0069;
				}
				finally
				{
					int num3 = MsoIdClient.PassportFreeMemory(intPtr);
					if (num3 < 0)
					{
						throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num3);
					}
				}
			}
			token = null;
			IL_0069:
			if (intPtr2 != IntPtr.Zero && num2 > 0U)
			{
				try
				{
					sessionKey = new byte[num2];
					Marshal.Copy(intPtr2, sessionKey, 0, (int)num2);
					return num;
				}
				finally
				{
					int num4 = MsoIdClient.PassportFreeMemory(intPtr2);
					if (num4 < 0)
					{
						throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num4);
					}
				}
			}
			sessionKey = null;
			return num;
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x00038C04 File Offset: 0x00036E04
		public static int LogonIdentity(IntPtr hIdentity, string authPolicy, LOGON_FLAG authFlags, RSTParams[] parameters)
		{
			if (Interlocked.CompareExchange(ref MsoIdClient.state, 1, 1) != 1)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInitialized);
			}
			int num = MsoIdClient.LogonIdentityEx(hIdentity, authPolicy, (uint)authFlags, parameters, (uint)parameters.Length);
			if (num < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num);
			}
			return num;
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x00038C44 File Offset: 0x00036E44
		public static int SetCredentials(IntPtr hIdentity, string credType, string credValue)
		{
			if (Interlocked.CompareExchange(ref MsoIdClient.state, 1, 1) != 1)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInitialized);
			}
			int num = MsoIdClient.SetCredential(hIdentity, credType, credValue);
			if (num < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.OperationalError, num);
			}
			return num;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x00038C80 File Offset: 0x00036E80
		private static int InitializeExImpl(Guid guid, int version, UPDATE_FLAG flags, IDCRL_OPTION[] options)
		{
			int num = -1;
			if (Interlocked.CompareExchange(ref MsoIdClient.state, -1, 0) != 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.AlreadyInitialized);
			}
			try
			{
				num = MsoIdClient.InitializeEx(ref guid, version, (uint)flags, options ?? null, (uint)((options != null) ? options.Length : 0));
			}
			catch (BadImageFormatException ex)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.NotInstalled, ex);
			}
			finally
			{
				Interlocked.Exchange(ref MsoIdClient.state, (num >= 0) ? 1 : 0);
			}
			if (num < 0)
			{
				throw new MsoIdAuthenticationException(MsoIdAuthenticationError.InitFailure, num);
			}
			return num;
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x00038D08 File Offset: 0x00036F08
		private static IDCRL_OPTION ConvertManagedOption(IDCRL_OPTION_ID optionId, object optionValue)
		{
			IDCRL_OPTION idcrl_OPTION = new IDCRL_OPTION
			{
				dwId = optionId
			};
			if (optionId <= IDCRL_OPTION_ID.IDCRL_OPTION_PROXY_PASSWORD)
			{
				switch (optionId)
				{
				case IDCRL_OPTION_ID.IDCRL_OPTION_PROXY:
					idcrl_OPTION.cbValue = (UIntPtr)((ulong)((long)Marshal.SizeOf(optionValue)));
					idcrl_OPTION.pValue = Marshal.AllocHGlobal((int)(uint)idcrl_OPTION.cbValue);
					Marshal.StructureToPtr(optionValue, idcrl_OPTION.pValue, false);
					return idcrl_OPTION;
				case IDCRL_OPTION_ID.IDCRL_OPTION_CONNECT_TIMEOUT:
				case IDCRL_OPTION_ID.IDCRL_OPTION_SEND_TIMEOUT:
					goto IL_00C3;
				case (IDCRL_OPTION_ID)3:
					goto IL_010D;
				default:
					if (optionId == IDCRL_OPTION_ID.IDCRL_OPTION_RECEIVE_TIMEOUT)
					{
						goto IL_00C3;
					}
					if (optionId != IDCRL_OPTION_ID.IDCRL_OPTION_PROXY_PASSWORD)
					{
						goto IL_010D;
					}
					break;
				}
			}
			else if (optionId != IDCRL_OPTION_ID.IDCRL_OPTION_PROXY_USERNAME && optionId != IDCRL_OPTION_ID.IDCRL_OPTION_ENVIRONMENT)
			{
				if (optionId != IDCRL_OPTION_ID.IDCRL_OPTION_MSC_TIMEOUT)
				{
					goto IL_010D;
				}
				goto IL_00C3;
			}
			idcrl_OPTION.pValue = Marshal.StringToHGlobalUni((string)optionValue);
			idcrl_OPTION.cbValue = (UIntPtr)((ulong)((long)(2 * (((string)optionValue).Length + 1))));
			return idcrl_OPTION;
			IL_00C3:
			idcrl_OPTION.cbValue = (UIntPtr)((ulong)((long)(4 * ((int[])optionValue).Length)));
			idcrl_OPTION.pValue = Marshal.AllocHGlobal((int)(uint)idcrl_OPTION.cbValue);
			Marshal.Copy((int[])optionValue, 0, idcrl_OPTION.pValue, ((int[])optionValue).Length);
			return idcrl_OPTION;
			IL_010D:
			idcrl_OPTION.pValue = IntPtr.Zero;
			idcrl_OPTION.cbValue = (UIntPtr)0UL;
			return idcrl_OPTION;
		}

		// Token: 0x06001042 RID: 4162
		[DllImport("msoidcli.dll")]
		private static extern int AuthIdentityToService([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string szServiceTarget, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string szServicePolicy, [In] uint dwTokenRequestFlags, out IntPtr szToken, out uint pdwResultFlags, out IntPtr ppbSessionKey, out uint pcbSessionKeyLength);

		// Token: 0x06001043 RID: 4163
		[DllImport("msoidcli.dll")]
		private static extern int CloseIdentityHandle([In] IntPtr hIdentity);

		// Token: 0x06001044 RID: 4164
		[DllImport("msoidcli.dll")]
		private static extern int CreateIdentityHandle([MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszMemberName, [In] uint dwFlags, out IntPtr pihIdentity);

		// Token: 0x06001045 RID: 4165
		[DllImport("msoidcli.dll")]
		private static extern int GetAuthState([In] IntPtr hIdentity, out int phrAuthState, out int phrAuthRequired, out int phrRequestStatus, out IntPtr szWebFlowUrl);

		// Token: 0x06001046 RID: 4166
		[DllImport("msoidcli.dll")]
		private static extern int InitializeEx([In] ref Guid guid, [In] int lPPCRLVersion, [In] uint dwFlags, [MarshalAs(UnmanagedType.LPArray)] [In] [Optional] IDCRL_OPTION[] pOptions, [In] [Optional] uint dwOptions);

		// Token: 0x06001047 RID: 4167
		[DllImport("msoidcli.dll")]
		private static extern int LogonIdentityEx([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszAuthPolicy, [In] uint dwAuthFlags, [MarshalAs(UnmanagedType.LPArray)] [In] RSTParams[] pcRSTParams, [In] uint dwRSTParamsCount);

		// Token: 0x06001048 RID: 4168
		[DllImport("msoidcli.dll")]
		private static extern int PassportFreeMemory([In] IntPtr pMemoryToFree);

		// Token: 0x06001049 RID: 4169
		[DllImport("msoidcli.dll")]
		private static extern int SetCredential([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredType, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredValue);

		// Token: 0x0600104A RID: 4170
		[DllImport("msoidcli.dll")]
		private static extern int Uninitialize();

		// Token: 0x04000A22 RID: 2594
		private const string MsoIdcrlRegistryPath = "SOFTWARE\\Microsoft\\MSOIdentityCRL";

		// Token: 0x04000A23 RID: 2595
		private const string MsoIdcrlRegistryValueName = "TargetDir";

		// Token: 0x04000A24 RID: 2596
		private const string MsoIdcrlFileName = "msoidcli.dll";

		// Token: 0x04000A25 RID: 2597
		private const int STATE_IDLE = 0;

		// Token: 0x04000A26 RID: 2598
		private const int STATE_INITIALIZED = 1;

		// Token: 0x04000A27 RID: 2599
		private const int STATE_INITIALIZING = -1;

		// Token: 0x04000A28 RID: 2600
		private const int STATE_UNINITIALIZING = -2;

		// Token: 0x04000A29 RID: 2601
		private static IntPtr module;

		// Token: 0x04000A2A RID: 2602
		private static MsoIdClient.MsoIdClientFinalizer finalizer;

		// Token: 0x04000A2B RID: 2603
		private static int state;

		// Token: 0x020001CE RID: 462
		private sealed class MsoIdClientFinalizer : CriticalFinalizerObject
		{
			// Token: 0x060013F7 RID: 5111 RVA: 0x00044B7C File Offset: 0x00042D7C
			~MsoIdClientFinalizer()
			{
				try
				{
					if (MsoIdClient.state == 1)
					{
						MsoIdClient.Uninitialize();
					}
					NativeMethods.FreeLibrary(MsoIdClient.module);
				}
				catch (Exception)
				{
				}
			}
		}
	}
}
