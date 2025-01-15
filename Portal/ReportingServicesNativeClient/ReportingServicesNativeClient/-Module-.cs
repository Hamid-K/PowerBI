using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Threading;
using <CppImplementationDetails>;
using <CrtImplementationDetails>;
using std;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00005BDC File Offset: 0x00004FDC
	internal static int HRESULT_FROM_WIN32(uint x)
	{
		return (x > 0) ? ((x & 65535) | -2147024896) : x;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00005C04 File Offset: 0x00005004
	internal unsafe static void std.exception.{dtor}(exception* A_0)
	{
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00005D88 File Offset: 0x00005188
	internal unsafe static void* std.exception.__vecDelDtor(exception* A_0, uint A_0)
	{
		if ((A_0 & 2U) != 0U)
		{
			exception* ptr = A_0 - 8L;
			<Module>.__ehvec_dtor(A_0, 24UL, (ulong)(*ptr), ldftn(std.exception.{dtor}));
			if ((A_0 & 1U) != 0U)
			{
				exception* ptr2 = ptr;
				<Module>.delete[](ptr2, (ulong)(*ptr2 * 24L + 8L));
			}
			return ptr;
		}
		*A_0 = ref <Module>.??_7exception@std@@6B@;
		<Module>.__std_exception_destroy(A_0 / sizeof(__std_exception_data) + 8L);
		if ((A_0 & 1U) != 0U)
		{
			<Module>.delete(A_0, 24UL);
		}
		return A_0;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00008560 File Offset: 0x00007960
	internal unsafe static int ?A0x2cda8610.memcpy_s(void* _Destination, ulong _DestinationSize, void* _Source, ulong _SourceSize)
	{
		if (_SourceSize == null)
		{
			return 0;
		}
		if (_Destination == null)
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
			return 22;
		}
		if (_Source != null && _DestinationSize >= _SourceSize)
		{
			cpblk(_Destination, _Source, _SourceSize);
			return 0;
		}
		initblk(_Destination, 0, _DestinationSize);
		if (_Source == null)
		{
			*<Module>._errno() = 22;
			<Module>._invalid_parameter_noinfo();
			return 22;
		}
		if (_DestinationSize < _SourceSize)
		{
			*<Module>._errno() = 34;
			<Module>._invalid_parameter_noinfo();
			return 34;
		}
		return 22;
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00009EE8 File Offset: 0x000092E8
	internal unsafe static int StringCchLengthW(ushort* psz, ulong cchMax, ulong* pcchLength)
	{
		int num3;
		if (psz != null && cchMax <= 2147483647UL)
		{
			ulong num = cchMax;
			ushort* ptr = psz;
			int num2 = 0;
			if (cchMax != 0UL)
			{
				while (*(ushort*)ptr != 0)
				{
					ptr += 2L / (long)sizeof(ushort);
					num -= 1UL;
					if (num == 0UL)
					{
						goto IL_002B;
					}
				}
				if (num != 0UL)
				{
					goto IL_0031;
				}
			}
			IL_002B:
			num2 = -2147024809;
			IL_0031:
			if (pcchLength != null)
			{
				if (num2 >= 0)
				{
					*pcchLength = cchMax - num;
				}
				else
				{
					*pcchLength = 0UL;
				}
			}
			num3 = num2;
			if (num2 >= 0)
			{
				return num3;
			}
		}
		else
		{
			num3 = -2147024809;
		}
		if (pcchLength != null)
		{
			*pcchLength = 0UL;
		}
		return num3;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000BCA4 File Offset: 0x0000B0A4
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(Exception innerException, Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000B5D4 File Offset: 0x0000A9D4
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000B5F0 File Offset: 0x0000A9F0
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000B720 File Offset: 0x0000AB20
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x0000B740 File Offset: 0x0000AB40
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid guid2 = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return guid2;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000B798 File Offset: 0x0000AB98
	[SecurityCritical]
	internal unsafe static int __get_default_appdomain(IUnknown** ppUnk)
	{
		ICorRuntimeHost* ptr = null;
		int num;
		try
		{
			Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e);
			ptr = (ICorRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e), guid).ToPointer();
			goto IL_0036;
		}
		catch (Exception ex)
		{
			num = Marshal.GetHRForException(ex);
		}
		if (num < 0)
		{
			return num;
		}
		IL_0036:
		long num2 = *(*(long*)ptr + 104L);
		num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown**), ptr, ppUnk, (IntPtr)num2);
		ICorRuntimeHost* ptr2 = ptr;
		uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		return num;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000B820 File Offset: 0x0000AC20
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, (IntPtr)(*(*(long*)ppUnk + 16L)));
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000B844 File Offset: 0x0000AC44
	[SecurityCritical]
	internal unsafe static AppDomain <CrtImplementationDetails>.GetDefaultDomain()
	{
		IUnknown* ptr = null;
		int num = <Module>.__get_default_appdomain(&ptr);
		if (num >= 0)
		{
			try
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				return (AppDomain)Marshal.GetObjectForIUnknown(intPtr);
			}
			finally
			{
				<Module>.__release_appdomain(ptr);
			}
		}
		Marshal.ThrowExceptionForHR(num);
		return null;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000B8AC File Offset: 0x0000ACAC
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(delegate* unmanaged[Cdecl, Cdecl]<void*, int> function, void* cookie)
	{
		Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), guid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)ptr + 64L);
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, appDomain.Id, function, cookie, (IntPtr)num);
			if (num2 < 0)
			{
				Marshal.ThrowExceptionForHR(num2);
			}
		}
		finally
		{
			ICLRRuntimeHost* ptr2 = ptr;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000B940 File Offset: 0x0000AD40
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		return (<Module>.__scrt_native_dllmain_reason <= 1U) ? 0 : 1;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000B974 File Offset: 0x0000AD74
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x0000B998 File Offset: 0x0000AD98
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasPerProcess()
	{
		if (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xc_mp_a);
			if ((ref <Module>.__xc_mp_a) < (ref <Module>.__xc_mp_z))
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xc_mp_z))
					{
						goto IL_0035;
					}
				}
				<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0035:
			<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x0000B9F4 File Offset: 0x0000ADF4
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool <CrtImplementationDetails>.DefaultDomain.HasNative()
	{
		if (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)2)
		{
			void** ptr = (void**)(&<Module>.__xi_a);
			if ((ref <Module>.__xi_a) < (ref <Module>.__xi_z))
			{
				while (*(long*)ptr == 0L)
				{
					ptr += 8L / (long)sizeof(void*);
					if (ptr >= (void**)(&<Module>.__xi_z))
					{
						goto IL_0035;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0035:
			void** ptr2 = (void**)(&<Module>.__xc_a);
			if ((ref <Module>.__xc_a) < (ref <Module>.__xc_z))
			{
				while (*(long*)ptr2 == 0L)
				{
					ptr2 += 8L / (long)sizeof(void*);
					if (ptr2 >= (void**)(&<Module>.__xc_z))
					{
						goto IL_0062;
					}
				}
				<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)(-1);
				return 1;
			}
			IL_0062:
			<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A = (TriBool)0;
			return 0;
		}
		return (<Module>.?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A == (TriBool)(-1)) ? 1 : 0;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x0000BA7C File Offset: 0x0000AE7C
	[SecuritySafeCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsInitialization()
	{
		int num;
		if ((<Module>.<CrtImplementationDetails>.DefaultDomain.HasPerProcess() != null && !<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA) || (<Module>.<CrtImplementationDetails>.DefaultDomain.HasNative() != null && !<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA && <Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0))
		{
			num = 1;
		}
		else
		{
			num = 0;
		}
		return (byte)num;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x0000BABC File Offset: 0x0000AEBC
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000BAD4 File Offset: 0x0000AED4
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000016 RID: 22 RVA: 0x0000102C File Offset: 0x0000042C
	internal static void ?A0x21d33d66.??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00001048 File Offset: 0x00000448
	internal static void ?A0x21d33d66.??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00001064 File Offset: 0x00000464
	internal static void ?A0x21d33d66.??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00001080 File Offset: 0x00000480
	internal static void ?A0x21d33d66.??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000109C File Offset: 0x0000049C
	internal static void ?A0x21d33d66.??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x000010B8 File Offset: 0x000004B8
	internal static void ?A0x21d33d66.??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000010D4 File Offset: 0x000004D4
	internal static void ?A0x21d33d66.??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x0000BD00 File Offset: 0x0000B100
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xi_vt_a), (delegate*<void*>*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000BD3C File Offset: 0x0000B13C
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x0600001F RID: 31 RVA: 0x0000BD60 File Offset: 0x0000B160
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeNative(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during native initialization.\n");
		<Module>.__security_init_cookie();
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
		if (<Module>.__scrt_is_safe_for_managed_code() == null)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)1)
		{
			<Module>.abort();
		}
		if (<Module>.__scrt_current_native_startup_state == (__scrt_native_startup_state)0)
		{
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)1;
			if (<Module>._initterm_e((delegate* unmanaged[Cdecl, Cdecl]<int>*)(&<Module>.__xi_a), (delegate* unmanaged[Cdecl, Cdecl]<int>*)(&<Module>.__xi_z)) != 0)
			{
				<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0));
			}
			<Module>._initterm((delegate* unmanaged[Cdecl, Cdecl]<void>*)(&<Module>.__xc_a), (delegate* unmanaged[Cdecl, Cdecl]<void>*)(&<Module>.__xc_z));
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)2;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
			<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x0000BDF4 File Offset: 0x0000B1F4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerProcess(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during process initialization.\n");
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_m();
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xc_mp_a), (delegate*<void*>*)(&<Module>.__xc_mp_z));
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = true;
	}

	// Token: 0x06000021 RID: 33 RVA: 0x0000BE3C File Offset: 0x0000B23C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xc_ma_a), (delegate*<void*>*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x0000BE7C File Offset: 0x0000B27C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000023 RID: 35 RVA: 0x0000BEAC File Offset: 0x0000B2AC
	[SecurityCritical]
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport._Initialize(LanguageSupport* A_0)
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = AppDomain.CurrentDomain.IsDefaultAppDomain();
		<Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA = <Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA || <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
		void* ptr = <Module>._getFiberPtrId();
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			while (num2 == 0)
			{
				try
				{
				}
				finally
				{
					void* ptr2 = Interlocked.CompareExchange(ref <Module>.__scrt_native_startup_lock, ptr, 0L);
					if (ptr2 == null)
					{
						num2 = 1;
					}
					else if (ptr2 == ptr)
					{
						num = 1;
						num2 = 1;
					}
				}
				if (num2 == 0)
				{
					<Module>.Sleep(1000);
				}
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeVtables(A_0);
			if (<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeNative(A_0);
				<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerProcess(A_0);
			}
			else
			{
				num3 = ((<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsInitialization() != 0) ? 1 : num3);
			}
		}
		finally
		{
			if (num == 0)
			{
				Interlocked.Exchange(ref <Module>.__scrt_native_startup_lock, 0L);
			}
		}
		if (num3 != 0)
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(A_0);
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(A_0);
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 1;
		<Module>.<CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(A_0);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x0000BAF4 File Offset: 0x0000AEF4
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000025 RID: 37 RVA: 0x0000BB0C File Offset: 0x0000AF0C
	[SecurityCritical]
	internal unsafe static int <CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(void* cookie)
	{
		<Module>._exit_callback();
		<Module>.?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		if (<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA)
		{
			<Module>._cexit();
			<Module>.__scrt_current_native_startup_state = (__scrt_native_startup_state)0;
			<Module>.?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		}
		<Module>.?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA = false;
		return 0;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x0000BB4C File Offset: 0x0000AF4C
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain()
	{
		if (<Module>.<CrtImplementationDetails>.DefaultDomain.NeedsUninitialization() != null)
		{
			if (AppDomain.CurrentDomain.IsDefaultAppDomain())
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport._UninitializeDefaultDomain(null);
			}
			else
			{
				<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
			}
		}
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000BB88 File Offset: 0x0000AF88
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[PrePrepareMethod]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.DomainUnload(object A_0, EventArgs A_1)
	{
		if (<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA != 0 && Interlocked.Exchange(ref <Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA, 1) == 0)
		{
			byte b = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0);
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (b != 0)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
	}

	// Token: 0x06000028 RID: 40 RVA: 0x0000BFB8 File Offset: 0x0000B3B8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Cleanup(LanguageSupport* A_0, Exception innerException)
	{
		try
		{
			bool flag = ((Interlocked.Decrement(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA) == 0) ? 1 : 0) != 0;
			<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain();
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.UninitializeDefaultDomain();
			}
		}
		catch (Exception ex)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, ex);
		}
		catch (object obj)
		{
			<Module>.<CrtImplementationDetails>.ThrowNestedModuleLoadException(innerException, null);
		}
	}

	// Token: 0x06000029 RID: 41 RVA: 0x0000C034 File Offset: 0x0000B434
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x0000C054 File Offset: 0x0000B454
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x0600002B RID: 43 RVA: 0x0000C070 File Offset: 0x0000B470
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.Initialize(LanguageSupport* A_0)
	{
		bool flag = false;
		RuntimeHelpers.PrepareConstrainedRegions();
		try
		{
			<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load.\n");
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				Interlocked.Increment(ref <Module>.?Count@AllDomains@<CrtImplementationDetails>@@2HA);
				flag = true;
			}
			<Module>.<CrtImplementationDetails>.LanguageSupport._Initialize(A_0);
		}
		catch (Exception ex)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, ex);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), ex);
		}
		catch (object obj)
		{
			if (flag)
			{
				<Module>.<CrtImplementationDetails>.LanguageSupport.Cleanup(A_0, null);
			}
			<Module>.<CrtImplementationDetails>.ThrowModuleLoadException(<Module>.gcroot<System::String\u0020^>..PE$AAVString@System@@(A_0), null);
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000C134 File Offset: 0x0000B534
	[DebuggerStepThrough]
	[SecurityCritical]
	static unsafe <Module>()
	{
		LanguageSupport languageSupport;
		<Module>.<CrtImplementationDetails>.LanguageSupport.{ctor}(ref languageSupport);
		try
		{
			<Module>.<CrtImplementationDetails>.LanguageSupport.Initialize(ref languageSupport);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(<CrtImplementationDetails>.LanguageSupport.{dtor}), (void*)(&languageSupport));
			throw;
		}
		<Module>.<CrtImplementationDetails>.LanguageSupport.{dtor}(ref languageSupport);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x0000BBCC File Offset: 0x0000AFCC
	[SecuritySafeCritical]
	internal unsafe static string gcroot<System::String\u0020^>..PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000BBF8 File Offset: 0x0000AFF8
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.=(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000BC24 File Offset: 0x0000B024
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<System::String\u0020^>.{dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x0000BC54 File Offset: 0x0000B054
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.{ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000031 RID: 49 RVA: 0x0000C1B4 File Offset: 0x0000B5B4
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void ___CxxCallUnwindDtor(delegate*<void*, void> pDtor, void* pThis)
	{
		try
		{
			calli(System.Void(System.Void*), pThis, pDtor);
		}
		catch when (endfilter(<Module>.__FrameUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000C288 File Offset: 0x0000B688
	[HandleProcessCorruptedStateExceptions]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	internal unsafe static void __ehvec_dtor(void* ptr, ulong size, ulong count, delegate*<void*, void> destructor)
	{
		bool flag = false;
		ptr = (void*)(size * count + (byte*)ptr);
		try
		{
			for (;;)
			{
				long num = (long)count;
				count -= 1UL;
				if (num == 0L)
				{
					break;
				}
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
			flag = true;
		}
		finally
		{
			if (!flag)
			{
				<Module>.__ArrayUnwind(ptr, size, count, destructor);
			}
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x0000C200 File Offset: 0x0000B600
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static int ?A0xe2d012b5.ArrayUnwindFilter(_EXCEPTION_POINTERS* pExPtrs)
	{
		if (*(*(long*)pExPtrs) != -529697949)
		{
			return 0;
		}
		<Module>.terminate();
		return 0;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x0000C228 File Offset: 0x0000B628
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void __ArrayUnwind(void* ptr, ulong size, ulong count, delegate*<void*, void> destructor)
	{
		try
		{
			for (ulong num = 0UL; num != count; num += 1UL)
			{
				ptr = (void*)((byte*)ptr - size);
				calli(System.Void(System.Void*), ptr, destructor);
			}
		}
		catch when (endfilter(<Module>.?A0xe2d012b5.ArrayUnwindFilter(Marshal.GetExceptionPointers()) != null))
		{
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000C2EC File Offset: 0x0000B6EC
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr intPtr = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(intPtr);
		}
		return null;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x0000C5DC File Offset: 0x0000B9DC
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000C320 File Offset: 0x0000B720
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Set(object value)
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType == null)
		{
			valueType = GCHandle.Alloc(value);
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = GCHandle.ToIntPtr((GCHandle)valueType).ToPointer();
		}
		else
		{
			((GCHandle)valueType).Target = value;
		}
	}

	// Token: 0x06000038 RID: 56 RVA: 0x0000C374 File Offset: 0x0000B774
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static object <CrtImplementationDetails>.AtExitLock._lock_Get()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			return ((GCHandle)valueType).Target;
		}
		return null;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x0000C3A0 File Offset: 0x0000B7A0
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Destruct()
	{
		ValueType valueType = <Module>.<CrtImplementationDetails>.AtExitLock._handle();
		if (valueType != null)
		{
			((GCHandle)valueType).Free();
			<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x0000C3D0 File Offset: 0x0000B7D0
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000C5FC File Offset: 0x0000B9FC
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.AddRef()
	{
		if (<Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized() == null)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Construct(new object());
			<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA = 0;
		}
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA++;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x0000C3F0 File Offset: 0x0000B7F0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x0600003D RID: 61 RVA: 0x0000C634 File Offset: 0x0000BA34
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0x15aeb141.__alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x0600003E RID: 62 RVA: 0x0000C41C File Offset: 0x0000B81C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void ?A0x15aeb141.__dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x0600003F RID: 63 RVA: 0x0000C434 File Offset: 0x0000B834
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0x15aeb141.__exit_list_size != 0UL)
		{
			delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x15aeb141.__onexitbegin_m);
			delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x15aeb141.__onexitend_m);
			if (ptr != -1L && ptr != null && ptr2 != null)
			{
				delegate*<void>* ptr3 = ptr;
				delegate*<void>* ptr4 = ptr2;
				for (;;)
				{
					ptr2 -= 8L / (long)sizeof(delegate*<void>);
					if (ptr2 < ptr)
					{
						break;
					}
					if (*(long*)ptr2 != <Module>.EncodePointer(null))
					{
						IntPtr intPtr = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), intPtr);
						delegate*<void>* ptr5 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x15aeb141.__onexitbegin_m);
						delegate*<void>* ptr6 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0x15aeb141.__onexitend_m);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
				IntPtr intPtr2 = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr2);
			}
			<Module>.?A0x15aeb141.__dealloc_global_lock();
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000C654 File Offset: 0x0000BA54
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_m()
	{
		int num = 0;
		if (<Module>.?A0x15aeb141.__alloc_global_lock() == 1)
		{
			<Module>.?A0x15aeb141.__onexitbegin_m = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0x15aeb141.__onexitend_m = <Module>.?A0x15aeb141.__onexitbegin_m;
			<Module>.?A0x15aeb141.__exit_list_size = 32UL;
			num = 1;
		}
		return num;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x0000C6A0 File Offset: 0x0000BAA0
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0x15aeb141.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x06000042 RID: 66 RVA: 0x0000C4E8 File Offset: 0x0000B8E8
	[SecurityCritical]
	[HandleProcessCorruptedStateExceptions]
	internal unsafe static void _app_exit_callback()
	{
		if (<Module>.__exit_list_size_app_domain != 0UL)
		{
			delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
			delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
			try
			{
				if (ptr != -1L && ptr != null && ptr2 != null)
				{
					delegate*<void>* ptr3 = ptr;
					delegate*<void>* ptr4 = ptr2;
					for (;;)
					{
						do
						{
							ptr2 -= 8L / (long)sizeof(delegate*<void>);
						}
						while (ptr2 >= ptr && *(long*)ptr2 == <Module>.EncodePointer(null));
						if (ptr2 < ptr)
						{
							break;
						}
						delegate*<void> system.Void_u0020() = <Module>.DecodePointer(*(long*)ptr2);
						*(long*)ptr2 = <Module>.EncodePointer(null);
						calli(System.Void(), system.Void_u0020());
						delegate*<void>* ptr5 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitbegin_app_domain);
						delegate*<void>* ptr6 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.__onexitend_app_domain);
						if (ptr3 != ptr5 || ptr4 != ptr6)
						{
							ptr3 = ptr5;
							ptr = ptr5;
							ptr4 = ptr6;
							ptr2 = ptr6;
						}
					}
				}
			}
			finally
			{
				IntPtr intPtr = new IntPtr((void*)ptr);
				Marshal.FreeHGlobal(intPtr);
				<Module>.?A0x15aeb141.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x06000043 RID: 67
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x06000044 RID: 68
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x06000045 RID: 69 RVA: 0x0000C6EC File Offset: 0x0000BAEC
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initterm_e(delegate* unmanaged[Cdecl, Cdecl]<int>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<int>* pfend)
	{
		int num = 0;
		if (pfbegin < pfend)
		{
			while (num == 0)
			{
				ulong num2 = (ulong)(*(long*)pfbegin);
				if (num2 != 0UL)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num2);
				}
				pfbegin += 8L / (long)sizeof(delegate* unmanaged[Cdecl, Cdecl]<int>);
				if (pfbegin >= pfend)
				{
					break;
				}
			}
		}
		return num;
	}

	// Token: 0x06000046 RID: 70 RVA: 0x0000C720 File Offset: 0x0000BB20
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void _initterm(delegate* unmanaged[Cdecl, Cdecl]<void>* pfbegin, delegate* unmanaged[Cdecl, Cdecl]<void>* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					calli(System.Void modopt(System.Runtime.CompilerServices.CallConvCdecl)(), (IntPtr)num);
				}
				pfbegin += 8L / (long)sizeof(delegate* unmanaged[Cdecl, Cdecl]<void>);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000C750 File Offset: 0x0000BB50
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000C7AC File Offset: 0x0000BBAC
	[DebuggerStepThrough]
	[SecurityCritical]
	[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
	internal unsafe static void _initterm_m(delegate*<void*>* pfbegin, delegate*<void*>* pfend)
	{
		if (pfbegin < pfend)
		{
			do
			{
				ulong num = (ulong)(*(long*)pfbegin);
				if (num != 0UL)
				{
					void* ptr = calli(System.Void modopt(System.Runtime.CompilerServices.IsConst)*(), <Module>.<CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(num));
				}
				pfbegin += 8L / (long)sizeof(delegate*<void*>);
			}
			while (pfbegin < pfend);
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x0000C778 File Offset: 0x0000BB78
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static delegate*<void*> <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(delegate*<void*> methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer()
			.ToPointer();
	}

	// Token: 0x0600004A RID: 74 RVA: 0x0000A740 File Offset: 0x00009B40
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*, ulong);

	// Token: 0x0600004B RID: 75 RVA: 0x0000B34E File Offset: 0x0000A74E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __std_exception_copy(__std_exception_data*, __std_exception_data*);

	// Token: 0x0600004C RID: 76 RVA: 0x0000AA0C File Offset: 0x00009E0C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(ulong);

	// Token: 0x0600004D RID: 77 RVA: 0x0000CA50 File Offset: 0x0000BE50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*, ulong);

	// Token: 0x0600004E RID: 78 RVA: 0x0000B35A File Offset: 0x0000A75A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __std_exception_destroy(__std_exception_data*);

	// Token: 0x0600004F RID: 79 RVA: 0x00007CF0 File Offset: 0x000070F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* RtlSecureZeroMemory(void*, ulong);

	// Token: 0x06000050 RID: 80 RVA: 0x00006EF0 File Offset: 0x000062F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRPCEndpointFromInstanceID(ushort*, ushort*, uint);

	// Token: 0x06000051 RID: 81 RVA: 0x0000CAEB File Offset: 0x0000BEEB
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int SafeArrayDestroy(tagSAFEARRAY*);

	// Token: 0x06000052 RID: 82 RVA: 0x00006620 File Offset: 0x00005A20
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcApplyKey(ushort*, ushort*, tagSAFEARRAY*, tagSAFEARRAY**);

	// Token: 0x06000053 RID: 83 RVA: 0x00006FF0 File Offset: 0x000063F0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeServiceNameFromInstanceName(ushort*, ushort*, uint);

	// Token: 0x06000054 RID: 84 RVA: 0x00006C90 File Offset: 0x00006090
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeInstanceNameFromInstanceID(ushort*, ushort*, uint);

	// Token: 0x06000055 RID: 85 RVA: 0x00006C80 File Offset: 0x00006080
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern ushort* NativeGetDefaultInstanceName();

	// Token: 0x06000056 RID: 86 RVA: 0x00006840 File Offset: 0x00005C40
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcCatalogEncrypt(ushort*, tagSAFEARRAY*, tagSAFEARRAY**, tagSAFEARRAY**);

	// Token: 0x06000057 RID: 87 RVA: 0x000066E0 File Offset: 0x00005AE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcDeleteEncryptedContent(ushort*, tagSAFEARRAY**);

	// Token: 0x06000058 RID: 88 RVA: 0x00006780 File Offset: 0x00005B80
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcDeleteKey(ushort*, ushort*, tagSAFEARRAY**);

	// Token: 0x06000059 RID: 89 RVA: 0x00006D90 File Offset: 0x00006190
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeInstanceIDFromInstanceName(ushort*, ushort*, uint);

	// Token: 0x0600005A RID: 90 RVA: 0x00006BB0 File Offset: 0x00005FB0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcReencryptSecureInformation(ushort*, tagSAFEARRAY**);

	// Token: 0x0600005B RID: 91 RVA: 0x00006AE0 File Offset: 0x00005EE0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcListReportServersInDB(ushort*, tagSAFEARRAY**, tagSAFEARRAY**, tagSAFEARRAY**, tagSAFEARRAY**, tagSAFEARRAY**);

	// Token: 0x0600005C RID: 92 RVA: 0x00006560 File Offset: 0x00005960
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcExtractKey(ushort*, ushort*, tagSAFEARRAY**, tagSAFEARRAY**);

	// Token: 0x0600005D RID: 93 RVA: 0x00006E60 File Offset: 0x00006260
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRPCEndpointFromInstanceName(ushort*, ushort*, uint);

	// Token: 0x0600005E RID: 94 RVA: 0x00006900 File Offset: 0x00005D00
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcCatalogDecrypt(ushort*, tagSAFEARRAY*, [MarshalAs(UnmanagedType.U1)] bool, tagSAFEARRAY**, tagSAFEARRAY**);

	// Token: 0x0600005F RID: 95 RVA: 0x000069D0 File Offset: 0x00005DD0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcSavePowerBIInformation(ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*, ushort*);

	// Token: 0x06000060 RID: 96 RVA: 0x000064A0 File Offset: 0x000058A0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int NativeRpcActivateWebService(ushort*, ushort*, tagSAFEARRAY**);

	// Token: 0x06000061 RID: 97 RVA: 0x00008550 File Offset: 0x00007950
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static extern bool RSNativeCrypto.HasSymmetricKey(RSNativeCrypto*);

	// Token: 0x06000062 RID: 98 RVA: 0x0000AA50 File Offset: 0x00009E50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*);

	// Token: 0x06000063 RID: 99 RVA: 0x000085D0 File Offset: 0x000079D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern RSNativeCrypto* RSNativeCrypto.{ctor}(RSNativeCrypto*, ushort*, ushort*);

	// Token: 0x06000064 RID: 100 RVA: 0x000087D0 File Offset: 0x00007BD0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void RSNativeCrypto.{dtor}(RSNativeCrypto*);

	// Token: 0x06000065 RID: 101 RVA: 0x000088F0 File Offset: 0x00007CF0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.InitializeInMemoryContext(RSNativeCrypto*);

	// Token: 0x06000066 RID: 102 RVA: 0x00008950 File Offset: 0x00007D50
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.CreateKeyContainer(RSNativeCrypto*);

	// Token: 0x06000067 RID: 103 RVA: 0x000089D0 File Offset: 0x00007DD0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.CreateSymmetricKey(RSNativeCrypto*, byte**, int*);

	// Token: 0x06000068 RID: 104 RVA: 0x00008BD0 File Offset: 0x00007FD0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.ImportSymmetricKey(RSNativeCrypto*, byte*, int, ushort*, byte**, int*);

	// Token: 0x06000069 RID: 105 RVA: 0x00008B30 File Offset: 0x00007F30
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.ImportSymmetricKey(RSNativeCrypto*, byte*, int);

	// Token: 0x0600006A RID: 106 RVA: 0x00008F30 File Offset: 0x00008330
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.ExportSymmetricKey(RSNativeCrypto*, ushort*, byte**, int*);

	// Token: 0x0600006B RID: 107 RVA: 0x00008E50 File Offset: 0x00008250
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.ExportSymmetricKey(RSNativeCrypto*, byte*, int, byte**, int*);

	// Token: 0x0600006C RID: 108 RVA: 0x00008D70 File Offset: 0x00008170
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.ExportPublicKey(RSNativeCrypto*, byte**, int*);

	// Token: 0x0600006D RID: 109 RVA: 0x00009000 File Offset: 0x00008400
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.EncryptData(RSNativeCrypto*, byte*, int, byte**, int*);

	// Token: 0x0600006E RID: 110 RVA: 0x00009240 File Offset: 0x00008640
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.DecryptData(RSNativeCrypto*, byte*, int, [MarshalAs(UnmanagedType.U1)] bool, byte**, int*);

	// Token: 0x0600006F RID: 111 RVA: 0x000093E0 File Offset: 0x000087E0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int RSNativeCrypto.ReencryptSymmetricKey(RSNativeCrypto*, byte*, int, byte*, int, byte**, int*);

	// Token: 0x06000070 RID: 112 RVA: 0x0000A3A8 File Offset: 0x000097A8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x06000071 RID: 113 RVA: 0x0000CA2B File Offset: 0x0000BE2B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int* _errno();

	// Token: 0x06000072 RID: 114 RVA: 0x0000CA1F File Offset: 0x0000BE1F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _invalid_parameter_noinfo();

	// Token: 0x06000073 RID: 115 RVA: 0x0000CAC7 File Offset: 0x0000BEC7
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int GetProcessTimes(void*, _FILETIME*, _FILETIME*, _FILETIME*, _FILETIME*);

	// Token: 0x06000074 RID: 116 RVA: 0x00005C30 File Offset: 0x00005030
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int AllowsAnonymous(ushort*, bool*);

	// Token: 0x06000075 RID: 117 RVA: 0x0000C87B File Offset: 0x0000BC7B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int CloseHandle(void*);

	// Token: 0x06000076 RID: 118 RVA: 0x0000C86F File Offset: 0x0000BC6F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern uint GetLastError();

	// Token: 0x06000077 RID: 119 RVA: 0x0000CADF File Offset: 0x0000BEDF
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CoTaskMemFree(void*);

	// Token: 0x06000078 RID: 120 RVA: 0x0000CAAF File Offset: 0x0000BEAF
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int GetProcessAffinityMask(void*, ulong*, ulong*);

	// Token: 0x06000079 RID: 121 RVA: 0x0000CABB File Offset: 0x0000BEBB
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int SetProcessAffinityMask(void*, ulong);

	// Token: 0x0600007A RID: 122 RVA: 0x0000C8AB File Offset: 0x0000BCAB
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* GetCurrentProcess();

	// Token: 0x0600007B RID: 123 RVA: 0x00005D40 File Offset: 0x00005140
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int GetProcessorInformation(ulong*);

	// Token: 0x0600007C RID: 124 RVA: 0x0000CA5B File Offset: 0x0000BE5B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern int __CxxQueryExceptionSize();

	// Token: 0x0600007D RID: 125 RVA: 0x0000B366 File Offset: 0x0000A766
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void _CxxThrowException(void*, _s__ThrowInfo*);

	// Token: 0x0600007E RID: 126 RVA: 0x0000A39C File Offset: 0x0000979C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* new[](ulong);

	// Token: 0x0600007F RID: 127 RVA: 0x0000CA7F File Offset: 0x0000BE7F
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxDetectRethrow(void*);

	// Token: 0x06000080 RID: 128 RVA: 0x0000CA8B File Offset: 0x0000BE8B
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void __CxxUnregisterExceptionObject(void*, int);

	// Token: 0x06000081 RID: 129 RVA: 0x0000CA67 File Offset: 0x0000BE67
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxExceptionFilter(void*, void*, int, void*);

	// Token: 0x06000082 RID: 130 RVA: 0x0000CA73 File Offset: 0x0000BE73
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __CxxRegisterExceptionObject(void*, void*);

	// Token: 0x06000083 RID: 131 RVA: 0x0000CAF7 File Offset: 0x0000BEF7
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern tagSAFEARRAY* SafeArrayCreate(ushort, uint, tagSAFEARRAYBOUND*);

	// Token: 0x06000084 RID: 132 RVA: 0x0000B960 File Offset: 0x0000AD60
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000085 RID: 133 RVA: 0x0000B40E File Offset: 0x0000A80E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _cexit();

	// Token: 0x06000086 RID: 134 RVA: 0x0000CAD3 File Offset: 0x0000BED3
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void Sleep(uint);

	// Token: 0x06000087 RID: 135 RVA: 0x0000CAA3 File Offset: 0x0000BEA3
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void abort();

	// Token: 0x06000088 RID: 136 RVA: 0x0000AA5C File Offset: 0x00009E5C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void __security_init_cookie();

	// Token: 0x06000089 RID: 137 RVA: 0x0000CA97 File Offset: 0x0000BE97
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x0600008A RID: 138 RVA: 0x0000B41A File Offset: 0x0000A81A
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void terminate();

	// Token: 0x04000001 RID: 1 RVA: 0x0000E4E0 File Offset: 0x0000C8E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@EOODALEL@Unknown?5exception@;

	// Token: 0x04000002 RID: 2 RVA: 0x0000F610 File Offset: 0x0000DA10
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0N@$$CBG ??_C@_1BK@MGMFAEKH@?$AAk?$AAe?$AAr?$AAn?$AAe?$AAl?$AA3?$AA2?$AA?4?$AAd?$AAl?$AAl@;

	// Token: 0x04000003 RID: 3 RVA: 0x0000E4F8 File Offset: 0x0000C8F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BP@$$CBD ??_C@_0BP@KHGALBC@GetLogicalProcessorInformation@;

	// Token: 0x04000004 RID: 4 RVA: 0x000193B0 File Offset: 0x000177B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4exception@std@@6B@;

	// Token: 0x04000005 RID: 5 RVA: 0x00019360 File Offset: 0x00017760
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@exception@std@@8;

	// Token: 0x04000006 RID: 6 RVA: 0x0000E4C0 File Offset: 0x0000C8C0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID CLSID_MSAdminBase_W;

	// Token: 0x04000007 RID: 7 RVA: 0x00019388 File Offset: 0x00017788
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2exception@std@@8;

	// Token: 0x04000008 RID: 8 RVA: 0x0001C008 File Offset: 0x00019608
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY02Q6AXXZ ??_7exception@std@@6B@;

	// Token: 0x04000009 RID: 9 RVA: 0x00019398 File Offset: 0x00017798
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3exception@std@@8;

	// Token: 0x0400000A RID: 10 RVA: 0x0000E4D0 File Offset: 0x0000C8D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID IID_IMSAdminBase_W;

	// Token: 0x0400000B RID: 11 RVA: 0x0001C920 File Offset: 0x00019F20
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AVexception@std@@@8;

	// Token: 0x0400000C RID: 12 RVA: 0x0001C018 File Offset: 0x00019618
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<uint, int> __m2mep@?HRESULT_FROM_WIN32@@$$J0YAJK@Z;

	// Token: 0x0400000D RID: 13 RVA: 0x0001C028 File Offset: 0x00019628
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<exception*, uint, void*> __m2mep@??_Eexception@std@@$$FUEAAPEAXI@Z;

	// Token: 0x0400000E RID: 14 RVA: 0x0000E538 File Offset: 0x0000C938
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY07$$CBG ??_C@_1BA@EONDGCCM@?$AAn?$AAc?$AAa?$AAl?$AAr?$AAp?$AAc@;

	// Token: 0x0400000F RID: 15 RVA: 0x0000E548 File Offset: 0x0000C948
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY05$$CBG ??_C@_1M@IMDCGEHH@?$AAP?$AAB?$AAI?$AAR?$AAS@;

	// Token: 0x04000010 RID: 16 RVA: 0x0000E558 File Offset: 0x0000C958
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BD@$$CBG ??_C@_1CG@JHJCOHDH@?$AAR?$AAe?$AAp?$AAo?$AAr?$AAt?$AAi?$AAn?$AAg?$AAS?$AAe?$AAr?$AAv?$AAi?$AAc@;

	// Token: 0x04000011 RID: 17 RVA: 0x0000E518 File Offset: 0x0000C918
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY07$$CBK ?A0x6d1ab10d.FatalExceptions;

	// Token: 0x04000012 RID: 18 RVA: 0x0000E580 File Offset: 0x0000C980
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DL@$$CBG ??_C@_1HG@CPDDCFFG@?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs?$AAo?$AAf?$AAt?$AA?5?$AAS?$AAQ?$AAL?$AA?5?$AAS@;

	// Token: 0x04000013 RID: 19 RVA: 0x0000E600 File Offset: 0x0000CA00
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0DG@$$CBG ??_C@_1GM@LACNADEC@?$AAM?$AAi?$AAc?$AAr?$AAo?$AAs?$AAo?$AAf?$AAt?$AA?5?$AAS?$AAQ?$AAL?$AA?5?$AAS@;

	// Token: 0x04000014 RID: 20 RVA: 0x0000F5CC File Offset: 0x0000D9CC
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01$$CBG ??_C@_13FPGAJAPJ@?$AA?2@;

	// Token: 0x04000015 RID: 21 RVA: 0x0000E670 File Offset: 0x0000CA70
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0N@$$CBG ??_C@_1BK@LFCCPOKG@?$AAn?$AAe?$AAt?$AAa?$AAp?$AAi?$AA3?$AA2?$AA?4?$AAd?$AAl?$AAl@;

	// Token: 0x04000016 RID: 22 RVA: 0x0000E690 File Offset: 0x0000CA90
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BK@$$CBD ??_C@_0BK@EGPDHJJE@NetValidatePasswordPolicy@;

	// Token: 0x04000017 RID: 23 RVA: 0x0000E6B0 File Offset: 0x0000CAB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BO@$$CBD ??_C@_0BO@IJDPCACA@NetValidatePasswordPolicyFree@;

	// Token: 0x04000018 RID: 24 RVA: 0x0001C038 File Offset: 0x00019638
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, ulong, void*, ulong, int> __m2mep@?memcpy_s@?A0x2cda8610@@$$J0YAHQEAX_KQEBX1@Z;

	// Token: 0x04000019 RID: 25 RVA: 0x0000E760 File Offset: 0x0000CB60
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400001A RID: 26 RVA: 0x0000E750 File Offset: 0x0000CB50
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400001B RID: 27
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400001C RID: 28 RVA: 0x0000E468 File Offset: 0x0000C868
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001D RID: 29
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400001E RID: 30 RVA: 0x0000E480 File Offset: 0x0000C880
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001F RID: 31 RVA: 0x0000E770 File Offset: 0x0000CB70
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000020 RID: 32 RVA: 0x0000E780 File Offset: 0x0000CB80
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000021 RID: 33
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000022 RID: 34 RVA: 0x0001CFA0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000023 RID: 35 RVA: 0x0001C084 File Offset: 0x00019684
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000024 RID: 36 RVA: 0x0001CFA3 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000025 RID: 37 RVA: 0x0001CFA4 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000026 RID: 38
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000027 RID: 39 RVA: 0x0001CFA2 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000028 RID: 40
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000029 RID: 41
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400002A RID: 42 RVA: 0x0001CFA1 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400002B RID: 43
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400002C RID: 44 RVA: 0x0001C080 File Offset: 0x00019680
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x0400002D RID: 45 RVA: 0x0000E4A8 File Offset: 0x0000C8A8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x0400002E RID: 46 RVA: 0x0000E4B8 File Offset: 0x0000C8B8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400002F RID: 47 RVA: 0x0000E488 File Offset: 0x0000C888
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000030 RID: 48 RVA: 0x0000E458 File Offset: 0x0000C858
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x04000031 RID: 49 RVA: 0x0000E498 File Offset: 0x0000C898
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x04000032 RID: 50 RVA: 0x0000E490 File Offset: 0x0000C890
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000033 RID: 51 RVA: 0x0000E4B0 File Offset: 0x0000C8B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000034 RID: 52 RVA: 0x0000E460 File Offset: 0x0000C860
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000035 RID: 53 RVA: 0x0000E4A0 File Offset: 0x0000C8A0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000036 RID: 54 RVA: 0x0000E478 File Offset: 0x0000C878
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000037 RID: 55 RVA: 0x0000E470 File Offset: 0x0000C870
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000038 RID: 56 RVA: 0x0001C088 File Offset: 0x00019688
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000039 RID: 57 RVA: 0x0001C098 File Offset: 0x00019698
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400003A RID: 58 RVA: 0x0000E790 File Offset: 0x0000CB90
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400003B RID: 59 RVA: 0x0000E798 File Offset: 0x0000CB98
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400003C RID: 60 RVA: 0x0001D110 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x15aeb141.__onexitbegin_m;

	// Token: 0x0400003D RID: 61 RVA: 0x0001D108 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '12894362189'.
	internal static ulong ?A0x15aeb141.__exit_list_size;

	// Token: 0x0400003E RID: 62
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitend_app_domain;

	// Token: 0x0400003F RID: 63
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x04000040 RID: 64
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x04000041 RID: 65 RVA: 0x0001D118 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x15aeb141.__onexitend_m;

	// Token: 0x04000042 RID: 66
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x04000043 RID: 67
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitbegin_app_domain;

	// Token: 0x04000044 RID: 68 RVA: 0x0000E6D8 File Offset: 0x0000CAD8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x04000045 RID: 69 RVA: 0x0000E430 File Offset: 0x0000C830
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000046 RID: 70 RVA: 0x0001CF50 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x04000047 RID: 71 RVA: 0x0001CF58 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x04000048 RID: 72 RVA: 0x0000E410 File Offset: 0x0000C810
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000049 RID: 73 RVA: 0x0000E428 File Offset: 0x0000C828
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x0400004A RID: 74 RVA: 0x0001C060 File Offset: 0x00019660
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '4294967295'.
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x0400004B RID: 75 RVA: 0x0000E420 File Offset: 0x0000C820
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
