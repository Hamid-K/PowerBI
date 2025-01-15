using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.AnalysisServices.AdomdClient.Interop;
using Microsoft.Win32;

namespace Microsoft.AnalysisServices.AdomdClient.MsoId
{
	// Token: 0x0200012A RID: 298
	internal static class MsoIdClient
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x00035F68 File Offset: 0x00034168
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

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00036014 File Offset: 0x00034214
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

		// Token: 0x06000FAA RID: 4010 RVA: 0x000360CC File Offset: 0x000342CC
		public static int Initialize(Guid guid, int version, UPDATE_FLAG flags, IDCRL_OPTION[] options)
		{
			return MsoIdClient.InitializeExImpl(guid, version, flags, options);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000360D8 File Offset: 0x000342D8
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

		// Token: 0x06000FAC RID: 4012 RVA: 0x00036134 File Offset: 0x00034334
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

		// Token: 0x06000FAD RID: 4013 RVA: 0x0003616C File Offset: 0x0003436C
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

		// Token: 0x06000FAE RID: 4014 RVA: 0x000361A8 File Offset: 0x000343A8
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

		// Token: 0x06000FAF RID: 4015 RVA: 0x00036228 File Offset: 0x00034428
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

		// Token: 0x06000FB0 RID: 4016 RVA: 0x00036300 File Offset: 0x00034500
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

		// Token: 0x06000FB1 RID: 4017 RVA: 0x00036340 File Offset: 0x00034540
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

		// Token: 0x06000FB2 RID: 4018 RVA: 0x0003637C File Offset: 0x0003457C
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

		// Token: 0x06000FB3 RID: 4019 RVA: 0x00036404 File Offset: 0x00034604
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

		// Token: 0x06000FB4 RID: 4020
		[DllImport("msoidcli.dll")]
		private static extern int AuthIdentityToService([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string szServiceTarget, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string szServicePolicy, [In] uint dwTokenRequestFlags, out IntPtr szToken, out uint pdwResultFlags, out IntPtr ppbSessionKey, out uint pcbSessionKeyLength);

		// Token: 0x06000FB5 RID: 4021
		[DllImport("msoidcli.dll")]
		private static extern int CloseIdentityHandle([In] IntPtr hIdentity);

		// Token: 0x06000FB6 RID: 4022
		[DllImport("msoidcli.dll")]
		private static extern int CreateIdentityHandle([MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszMemberName, [In] uint dwFlags, out IntPtr pihIdentity);

		// Token: 0x06000FB7 RID: 4023
		[DllImport("msoidcli.dll")]
		private static extern int GetAuthState([In] IntPtr hIdentity, out int phrAuthState, out int phrAuthRequired, out int phrRequestStatus, out IntPtr szWebFlowUrl);

		// Token: 0x06000FB8 RID: 4024
		[DllImport("msoidcli.dll")]
		private static extern int InitializeEx([In] ref Guid guid, [In] int lPPCRLVersion, [In] uint dwFlags, [MarshalAs(UnmanagedType.LPArray)] [In] [Optional] IDCRL_OPTION[] pOptions, [In] [Optional] uint dwOptions);

		// Token: 0x06000FB9 RID: 4025
		[DllImport("msoidcli.dll")]
		private static extern int LogonIdentityEx([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] [Optional] string wszAuthPolicy, [In] uint dwAuthFlags, [MarshalAs(UnmanagedType.LPArray)] [In] RSTParams[] pcRSTParams, [In] uint dwRSTParamsCount);

		// Token: 0x06000FBA RID: 4026
		[DllImport("msoidcli.dll")]
		private static extern int PassportFreeMemory([In] IntPtr pMemoryToFree);

		// Token: 0x06000FBB RID: 4027
		[DllImport("msoidcli.dll")]
		private static extern int SetCredential([In] IntPtr hIdentity, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredType, [MarshalAs(UnmanagedType.LPWStr)] [In] string wszCredValue);

		// Token: 0x06000FBC RID: 4028
		[DllImport("msoidcli.dll")]
		private static extern int Uninitialize();

		// Token: 0x04000A69 RID: 2665
		private const string MsoIdcrlRegistryPath = "SOFTWARE\\Microsoft\\MSOIdentityCRL";

		// Token: 0x04000A6A RID: 2666
		private const string MsoIdcrlRegistryValueName = "TargetDir";

		// Token: 0x04000A6B RID: 2667
		private const string MsoIdcrlFileName = "msoidcli.dll";

		// Token: 0x04000A6C RID: 2668
		private const int STATE_IDLE = 0;

		// Token: 0x04000A6D RID: 2669
		private const int STATE_INITIALIZED = 1;

		// Token: 0x04000A6E RID: 2670
		private const int STATE_INITIALIZING = -1;

		// Token: 0x04000A6F RID: 2671
		private const int STATE_UNINITIALIZING = -2;

		// Token: 0x04000A70 RID: 2672
		private static IntPtr module;

		// Token: 0x04000A71 RID: 2673
		private static MsoIdClient.MsoIdClientFinalizer finalizer;

		// Token: 0x04000A72 RID: 2674
		private static int state;

		// Token: 0x020001F1 RID: 497
		private sealed class MsoIdClientFinalizer : CriticalFinalizerObject
		{
			// Token: 0x0600149C RID: 5276 RVA: 0x0004695C File Offset: 0x00044B5C
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
