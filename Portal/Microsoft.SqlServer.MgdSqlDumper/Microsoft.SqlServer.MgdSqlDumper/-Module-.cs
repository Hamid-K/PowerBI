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

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x000047F0 File Offset: 0x00003BF0
	internal unsafe static __crt_locale_pointers* GetDefaultLocale()
	{
		return <Module>.?A0x72402008.g_locCrtLocale;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00005FB4 File Offset: 0x000053B4
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(Exception innerException, Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000058DC File Offset: 0x00004CDC
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000058F8 File Offset: 0x00004CF8
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00005A28 File Offset: 0x00004E28
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00005A48 File Offset: 0x00004E48
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid guid2 = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return guid2;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00005AA0 File Offset: 0x00004EA0
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

	// Token: 0x06000008 RID: 8 RVA: 0x00005B28 File Offset: 0x00004F28
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, (IntPtr)(*(*(long*)ppUnk + 16L)));
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00005B4C File Offset: 0x00004F4C
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

	// Token: 0x0600000A RID: 10 RVA: 0x00005BB4 File Offset: 0x00004FB4
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

	// Token: 0x0600000B RID: 11 RVA: 0x00005C48 File Offset: 0x00005048
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		return (<Module>.__scrt_native_dllmain_reason <= 1U) ? 0 : 1;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00005C84 File Offset: 0x00005084
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00005CA8 File Offset: 0x000050A8
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

	// Token: 0x0600000E RID: 14 RVA: 0x00005D04 File Offset: 0x00005104
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

	// Token: 0x0600000F RID: 15 RVA: 0x00005D8C File Offset: 0x0000518C
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

	// Token: 0x06000010 RID: 16 RVA: 0x00005DCC File Offset: 0x000051CC
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00005DE4 File Offset: 0x000051E4
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00001008 File Offset: 0x00000408
	internal static void ?A0x21d33d66.??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00001024 File Offset: 0x00000424
	internal static void ?A0x21d33d66.??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00001040 File Offset: 0x00000440
	internal static void ?A0x21d33d66.??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000105C File Offset: 0x0000045C
	internal static void ?A0x21d33d66.??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00001078 File Offset: 0x00000478
	internal static void ?A0x21d33d66.??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00001094 File Offset: 0x00000494
	internal static void ?A0x21d33d66.??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000010B0 File Offset: 0x000004B0
	internal static void ?A0x21d33d66.??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00006010 File Offset: 0x00005410
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xi_vt_a), (delegate*<void*>*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000604C File Offset: 0x0000544C
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00006070 File Offset: 0x00005470
	[SecuritySafeCritical]
	[DebuggerStepThrough]
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

	// Token: 0x0600001C RID: 28 RVA: 0x00006104 File Offset: 0x00005504
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

	// Token: 0x0600001D RID: 29 RVA: 0x0000614C File Offset: 0x0000554C
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializePerAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during appdomain initialization.\n");
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initatexit_app_domain();
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xc_ma_a), (delegate*<void*>*)(&<Module>.__xc_ma_z));
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x0000618C File Offset: 0x0000558C
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000061BC File Offset: 0x000055BC
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
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

	// Token: 0x06000020 RID: 32 RVA: 0x00005E04 File Offset: 0x00005204
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00005E1C File Offset: 0x0000521C
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

	// Token: 0x06000022 RID: 34 RVA: 0x00005E5C File Offset: 0x0000525C
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

	// Token: 0x06000023 RID: 35 RVA: 0x00005E98 File Offset: 0x00005298
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[PrePrepareMethod]
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

	// Token: 0x06000024 RID: 36 RVA: 0x000062C8 File Offset: 0x000056C8
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DebuggerStepThrough]
	[SecurityCritical]
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

	// Token: 0x06000025 RID: 37 RVA: 0x00006344 File Offset: 0x00005744
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00006364 File Offset: 0x00005764
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00006380 File Offset: 0x00005780
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
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

	// Token: 0x06000028 RID: 40 RVA: 0x00006444 File Offset: 0x00005844
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000029 RID: 41 RVA: 0x00005EDC File Offset: 0x000052DC
	[SecuritySafeCritical]
	internal unsafe static string gcroot<System::String\u0020^>..PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00005F08 File Offset: 0x00005308
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.=(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00005F34 File Offset: 0x00005334
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<System::String\u0020^>.{dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00005F64 File Offset: 0x00005364
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.{ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000064C4 File Offset: 0x000058C4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static ValueType <CrtImplementationDetails>.AtExitLock._handle()
	{
		if (<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA != null)
		{
			IntPtr intPtr = new IntPtr(<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA);
			return GCHandle.FromIntPtr(intPtr);
		}
		return null;
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000067B4 File Offset: 0x00005BB4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x000064F8 File Offset: 0x000058F8
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

	// Token: 0x06000030 RID: 48 RVA: 0x0000654C File Offset: 0x0000594C
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

	// Token: 0x06000031 RID: 49 RVA: 0x00006578 File Offset: 0x00005978
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

	// Token: 0x06000032 RID: 50 RVA: 0x000065A8 File Offset: 0x000059A8
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000067D4 File Offset: 0x00005BD4
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

	// Token: 0x06000034 RID: 52 RVA: 0x000065C8 File Offset: 0x000059C8
	[SecurityCritical]
	[DebuggerStepThrough]
	internal static void <CrtImplementationDetails>.AtExitLock.RemoveRef()
	{
		<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA += -1;
		if (<Module>.?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA == 0)
		{
			<Module>.<CrtImplementationDetails>.AtExitLock._lock_Destruct();
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x0000680C File Offset: 0x00005C0C
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0x15aeb141.__alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x06000036 RID: 54 RVA: 0x000065F4 File Offset: 0x000059F4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void ?A0x15aeb141.__dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x06000037 RID: 55 RVA: 0x0000660C File Offset: 0x00005A0C
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

	// Token: 0x06000038 RID: 56 RVA: 0x0000682C File Offset: 0x00005C2C
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

	// Token: 0x06000039 RID: 57 RVA: 0x00006878 File Offset: 0x00005C78
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

	// Token: 0x0600003A RID: 58 RVA: 0x000066C0 File Offset: 0x00005AC0
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

	// Token: 0x0600003B RID: 59
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x0600003C RID: 60
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x0600003D RID: 61 RVA: 0x000068C4 File Offset: 0x00005CC4
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

	// Token: 0x0600003E RID: 62 RVA: 0x000068F8 File Offset: 0x00005CF8
	[DebuggerStepThrough]
	[SecurityCritical]
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

	// Token: 0x0600003F RID: 63 RVA: 0x00006928 File Offset: 0x00005D28
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x06000040 RID: 64 RVA: 0x00006984 File Offset: 0x00005D84
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000041 RID: 65 RVA: 0x00006950 File Offset: 0x00005D50
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static delegate*<void*> <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(delegate*<void*> methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer()
			.ToPointer();
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000069B8 File Offset: 0x00005DB8
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

	// Token: 0x06000043 RID: 67 RVA: 0x00007725 File Offset: 0x00006B25
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern __crt_locale_pointers* _create_locale(int, sbyte*);

	// Token: 0x06000044 RID: 68 RVA: 0x000010D0 File Offset: 0x000004D0
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int DmpGetClientExportInternal(IDmpClient**);

	// Token: 0x06000045 RID: 69 RVA: 0x00005C70 File Offset: 0x00005070
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x06000046 RID: 70 RVA: 0x000076DE File Offset: 0x00006ADE
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _cexit();

	// Token: 0x06000047 RID: 71 RVA: 0x0000770D File Offset: 0x00006B0D
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void Sleep(uint);

	// Token: 0x06000048 RID: 72 RVA: 0x00007731 File Offset: 0x00006B31
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void abort();

	// Token: 0x06000049 RID: 73 RVA: 0x00007160 File Offset: 0x00006560
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void __security_init_cookie();

	// Token: 0x0600004A RID: 74 RVA: 0x00007719 File Offset: 0x00006B19
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x00009320 File Offset: 0x00007920
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00$$CBD ??_C@_00CNPNBAHC@@;

	// Token: 0x04000002 RID: 2 RVA: 0x00012090 File Offset: 0x0000FE90
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '0'.
	internal static int ?A0x72402008.g_bIsLocaleInitialized;

	// Token: 0x04000003 RID: 3 RVA: 0x00012080 File Offset: 0x0000FE80
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static __crt_locale_pointers* ?A0x72402008.g_locCrtLocale;

	// Token: 0x04000004 RID: 4 RVA: 0x00012000 File Offset: 0x0000FE00
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<__crt_locale_pointers*> __m2mep@?GetDefaultLocale@@$$FYAPEAU__crt_locale_pointers@@XZ;

	// Token: 0x04000005 RID: 5 RVA: 0x00009338 File Offset: 0x00007938
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000006 RID: 6 RVA: 0x00009328 File Offset: 0x00007928
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000007 RID: 7
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000008 RID: 8 RVA: 0x000092C0 File Offset: 0x000078C0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000009 RID: 9
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400000A RID: 10 RVA: 0x000092D8 File Offset: 0x000078D8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400000B RID: 11 RVA: 0x00009348 File Offset: 0x00007948
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400000C RID: 12 RVA: 0x00009358 File Offset: 0x00007958
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400000D RID: 13
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400000E RID: 14 RVA: 0x00012188 File Offset: 0x0000FF88
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400000F RID: 15 RVA: 0x00012014 File Offset: 0x0000FE14
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000010 RID: 16 RVA: 0x0001218B File Offset: 0x0000FF8B
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000011 RID: 17 RVA: 0x0001218C File Offset: 0x0000FF8C
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '0'.
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000012 RID: 18
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000013 RID: 19 RVA: 0x0001218A File Offset: 0x0000FF8A
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000014 RID: 20
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000015 RID: 21
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000016 RID: 22 RVA: 0x00012189 File Offset: 0x0000FF89
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'False'.
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000017 RID: 23
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000018 RID: 24 RVA: 0x00012010 File Offset: 0x0000FE10
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000019 RID: 25 RVA: 0x00009300 File Offset: 0x00007900
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x0400001A RID: 26 RVA: 0x00009310 File Offset: 0x00007910
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400001B RID: 27 RVA: 0x000092E0 File Offset: 0x000078E0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001C RID: 28 RVA: 0x000092B0 File Offset: 0x000078B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x0400001D RID: 29 RVA: 0x000092F0 File Offset: 0x000078F0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x0400001E RID: 30 RVA: 0x000092E8 File Offset: 0x000078E8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400001F RID: 31 RVA: 0x00009308 File Offset: 0x00007908
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000020 RID: 32 RVA: 0x000092B8 File Offset: 0x000078B8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000021 RID: 33 RVA: 0x000092F8 File Offset: 0x000078F8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000022 RID: 34 RVA: 0x000092D0 File Offset: 0x000078D0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000023 RID: 35 RVA: 0x000092C8 File Offset: 0x000078C8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0x21d33d66.?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000024 RID: 36 RVA: 0x00012018 File Offset: 0x0000FE18
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000025 RID: 37 RVA: 0x00012028 File Offset: 0x0000FE28
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000026 RID: 38 RVA: 0x00009368 File Offset: 0x00007968
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000027 RID: 39 RVA: 0x00009370 File Offset: 0x00007970
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000028 RID: 40 RVA: 0x000122F8 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x15aeb141.__onexitbegin_m;

	// Token: 0x04000029 RID: 41 RVA: 0x000122F0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '12894362189'.
	internal static ulong ?A0x15aeb141.__exit_list_size;

	// Token: 0x0400002A RID: 42
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitend_app_domain;

	// Token: 0x0400002B RID: 43
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x0400002C RID: 44
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x0400002D RID: 45 RVA: 0x00012300 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0x15aeb141.__onexitend_m;

	// Token: 0x0400002E RID: 46
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x0400002F RID: 47
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitbegin_app_domain;

	// Token: 0x04000030 RID: 48 RVA: 0x00009288 File Offset: 0x00007888
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000031 RID: 49 RVA: 0x00012310 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x04000032 RID: 50 RVA: 0x00012318 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x04000033 RID: 51 RVA: 0x00009270 File Offset: 0x00007870
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000034 RID: 52 RVA: 0x00009280 File Offset: 0x00007880
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000035 RID: 53 RVA: 0x00012038 File Offset: 0x0000FE38
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '4294967295'.
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x04000036 RID: 54 RVA: 0x00009278 File Offset: 0x00007878
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
