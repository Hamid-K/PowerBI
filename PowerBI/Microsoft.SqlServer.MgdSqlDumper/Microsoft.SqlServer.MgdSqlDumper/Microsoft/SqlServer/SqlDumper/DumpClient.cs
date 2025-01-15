using System;
using System.Runtime.InteropServices;

namespace Microsoft.SqlServer.SqlDumper
{
	// Token: 0x02000003 RID: 3
	public sealed class DumpClient
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00004808 File Offset: 0x00003C08
		public unsafe static void Initialize()
		{
			IDmpClient* ptr = null;
			if (<Module>.?A0x72402008.g_bIsLocaleInitialized == 0)
			{
				<Module>.?A0x72402008.g_locCrtLocale = <Module>._create_locale(2, (sbyte*)(&<Module>.??_C@_00CNPNBAHC@@));
				<Module>.?A0x72402008.g_bIsLocaleInitialized = 1;
			}
			int num = <Module>.DmpGetClientExportInternal(&ptr);
			if (num >= 0)
			{
				IDmpClient* ptr2 = ptr;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 32L)));
				if (num >= 0)
				{
					DumpClient.s_pDmpClient = ptr;
				}
				else
				{
					IDmpClient* ptr3 = ptr;
					uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, (IntPtr)(*(*(long*)ptr3 + 16L)));
					Marshal.ThrowExceptionForHR(num);
				}
			}
			else
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000048EC File Offset: 0x00003CEC
		public unsafe static void InitializeStandalone()
		{
			IDmpClient* ptr = null;
			int num = <Module>.DmpGetClientExportInternal(&ptr);
			if (num >= 0)
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32), ptr, 1, (IntPtr)(*(*(long*)ptr + 24L)));
				if (num >= 0)
				{
					DumpClient.s_pDmpClient = ptr;
				}
				else
				{
					IDmpClient* ptr2 = ptr;
					uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
					Marshal.ThrowExceptionForHR(num);
				}
			}
			else
			{
				Marshal.ThrowExceptionForHR(num);
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x0000562C File Offset: 0x00004A2C
		public static Dumper GetDumper()
		{
			if (DumpClient.s_pDmpClient == null)
			{
				return null;
			}
			return new Dumper();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00005700 File Offset: 0x00004B00
		public static void SetDefaultExceptionHandling(AppDomain appDomain)
		{
			if (appDomain == null)
			{
				appDomain = AppDomain.CurrentDomain;
			}
			appDomain.UnhandledException += DumpClient.DefaultExceptionHandler;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004880 File Offset: 0x00003C80
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe static bool IsInitialized()
		{
			if (DumpClient.s_pDmpClient == null)
			{
				IDmpClient* ptr = null;
				int num = <Module>.DmpGetClientExportInternal(&ptr);
				if (num >= 0)
				{
					int num2 = 0;
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32*), ptr, &num2, (IntPtr)(*(*(long*)ptr + 72L)));
					if (num >= 0 && num2 != 0)
					{
						DumpClient.s_pDmpClient = ptr;
						return true;
					}
					IDmpClient* ptr2 = ptr;
					uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
					Marshal.ThrowExceptionForHR(num);
				}
				else
				{
					Marshal.ThrowExceptionForHR(num);
				}
				return false;
			}
			return true;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00005650 File Offset: 0x00004A50
		private static void DefaultExceptionHandler(object sender, UnhandledExceptionEventArgs args)
		{
			Dumper dumper = null;
			try
			{
				dumper = DumpClient.GetDumper();
				if (dumper != null)
				{
					Exception ex = args.ExceptionObject as Exception;
					dumper.SetTitleName(sender.ToString());
					dumper.SetException(ex);
					dumper.Dump();
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				if (dumper != null)
				{
					dumper.DumperDispose(true);
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000047B8 File Offset: 0x00003BB8
		private DumpClient()
		{
		}

		// Token: 0x04000044 RID: 68
		internal unsafe static IDmpClient* s_pDmpClient = null;
	}
}
