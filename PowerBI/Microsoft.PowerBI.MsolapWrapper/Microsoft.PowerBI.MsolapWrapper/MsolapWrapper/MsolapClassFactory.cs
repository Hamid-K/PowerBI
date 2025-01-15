using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using ATL;
using msclr;

namespace MsolapWrapper
{
	// Token: 0x02000080 RID: 128
	internal class MsolapClassFactory
	{
		// Token: 0x060001AF RID: 431 RVA: 0x000088B8 File Offset: 0x00007CB8
		public unsafe static CComPtr<IDBInitialize>* CreateDbInitialize(CComPtr<IDBInitialize>* A_0)
		{
			try
			{
				uint num = 0U;
				delegate* unmanaged[Cdecl, Cdecl]<_GUID*, _GUID*, void**, int> msolapDllClassObjectFunc = MsolapClassFactory.GetMsolapDllClassObjectFunc();
				CComPtr<IClassFactory> ccomPtr<IClassFactory> = 0L;
				try
				{
					_GUID guid = MsolapClassFactory.ToGUID(MsolapClassFactory.MsolapClsId);
					Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ref guid, ref <Module>.IID_IClassFactory, (void**)(&ccomPtr<IClassFactory>), msolapDllClassObjectFunc), "Failed to create class factory");
					*(long*)A_0 = 0L;
					num = 1U;
					Utils.ThrowErrorIfHrFailed(calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ccomPtr<IClassFactory>, null, ref <Module>.IID_IDBInitialize, (void**)A_0, (IntPtr)(*(*ccomPtr<IClassFactory> + 24L))), "Failed to create IDBInitialize");
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IClassFactory>.{dtor}), (void*)(&ccomPtr<IClassFactory>));
					throw;
				}
				if (ccomPtr<IClassFactory> != null)
				{
					uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IClassFactory>, (IntPtr)(*(*ccomPtr<IClassFactory> + 16L)));
				}
			}
			catch
			{
				uint num;
				if ((num & 1U) != 0U)
				{
					num &= 4294967294U;
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBInitialize>.{dtor}), (void*)A_0);
				}
				throw;
			}
			return A_0;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00008314 File Offset: 0x00007714
		public static _GUID ToGUID(ValueType guid)
		{
			ref byte ptr = ref ((Guid)guid).ToByteArray()[0];
			_GUID guid2;
			cpblk(ref guid2, ref ptr, 16);
			return guid2;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00008570 File Offset: 0x00007970
		public unsafe static delegate* unmanaged[Cdecl, Cdecl]<_GUID*, _GUID*, void**, int> GetMsolapDllClassObjectFunc()
		{
			@lock @lock = null;
			if (MsolapClassFactory.s_msolapDllGetClassObjectFunc == null)
			{
				@lock lock2 = new @lock(MsolapClassFactory.s_mutex);
				try
				{
					@lock = lock2;
					if (MsolapClassFactory.s_msolapDllGetClassObjectFunc == null)
					{
						ref byte ptr = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)), MsolapClassFactory.MsolapDllName);
						if ((ref ptr) != null)
						{
							ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
						}
						ref char ptr2 = ref ptr;
						HINSTANCE__* ptr3 = <Module>.LoadLibraryExW(ref ptr2, null, 4352);
						if (ptr3 == null)
						{
							uint lastError = <Module>.GetLastError();
							if (lastError == 87)
							{
								Utils.ThrowError(WrapperErrorCodes.InsecureLibraryLoadingPatchMissing, "Failed to load MSOLAP due to missing security patch KB2533623.", WrapperErrorSource.User, null, false);
								<Module>._CxxThrowException(null, null);
							}
							Utils.ThrowError(WrapperErrorSource.PowerBI, "Failed to load MSOLAP. Error {0}", lastError);
						}
						MsolapClassFactory.s_msolapDllGetClassObjectFunc = <Module>.GetProcAddress(ptr3, MsolapClassFactory.DllGetClassObjectFunctionName);
						if (MsolapClassFactory.s_msolapDllGetClassObjectFunc == null)
						{
							uint lastError2 = <Module>.GetLastError();
							Utils.ThrowError(WrapperErrorSource.PowerBI, "Failed to find DllGetClassObject. Error {0}", lastError2);
						}
					}
				}
				catch
				{
					((IDisposable)@lock).Dispose();
					throw;
				}
				((IDisposable)@lock).Dispose();
			}
			return MsolapClassFactory.s_msolapDllGetClassObjectFunc;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000089A0 File Offset: 0x00007DA0
		public unsafe static CComPtr<IErrorLookup>* CreateErrorLookup(CComPtr<IErrorLookup>* A_0)
		{
			try
			{
				uint num = 0U;
				delegate* unmanaged[Cdecl, Cdecl]<_GUID*, _GUID*, void**, int> msolapDllClassObjectFunc = MsolapClassFactory.GetMsolapDllClassObjectFunc();
				CComPtr<IClassFactory> ccomPtr<IClassFactory> = 0L;
				try
				{
					_GUID guid = MsolapClassFactory.ToGUID(MsolapClassFactory.MsolapErrorLookupClsId);
					int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ref guid, ref <Module>.IID_IClassFactory, (void**)(&ccomPtr<IClassFactory>), msolapDllClassObjectFunc);
					*(long*)A_0 = 0L;
					num = 1U;
					int num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ccomPtr<IClassFactory>, null, ref <Module>.IID_IErrorLookup, (void**)A_0, (IntPtr)(*(*ccomPtr<IClassFactory> + 24L)));
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IClassFactory>.{dtor}), (void*)(&ccomPtr<IClassFactory>));
					throw;
				}
				if (ccomPtr<IClassFactory> != null)
				{
					uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IClassFactory>, (IntPtr)(*(*ccomPtr<IClassFactory> + 16L)));
				}
			}
			catch
			{
				uint num;
				if ((num & 1U) != 0U)
				{
					num &= 4294967294U;
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IErrorLookup>.{dtor}), (void*)A_0);
				}
				throw;
			}
			return A_0;
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x00005ED8 File Offset: 0x000052D8
		// Note: this type is marked as 'beforefieldinit'.
		unsafe static MsolapClassFactory()
		{
			ValueType valueType = default(Guid);
			(Guid)valueType = new Guid("DBC724B0-DD86-4772-BB5A-FCC6CAB2FC1A");
			MsolapClassFactory.MsolapClsId = valueType;
			ValueType valueType2 = default(Guid);
			(Guid)valueType2 = new Guid("E464F134-7987-40FC-8F57-F12A2286DCDB");
			MsolapClassFactory.MsolapErrorLookupClsId = valueType2;
			MsolapClassFactory.s_mutex = new object();
			MsolapClassFactory.s_msolapDllGetClassObjectFunc = 0L;
		}

		// Token: 0x04000200 RID: 512
		private unsafe static sbyte* DllGetClassObjectFunctionName = (sbyte*)(&<Module>.??_C@_0BC@JDLGBILA@DllGetClassObject@);

		// Token: 0x04000201 RID: 513
		private static uint LoadLibraryExInvalidParamErrorCode = 87U;

		// Token: 0x04000202 RID: 514
		private static string MsolapDllName = "msolap";

		// Token: 0x04000203 RID: 515
		private static ValueType MsolapClsId;

		// Token: 0x04000204 RID: 516
		private static ValueType MsolapErrorLookupClsId;

		// Token: 0x04000205 RID: 517
		private static object s_mutex;

		// Token: 0x04000206 RID: 518
		private unsafe static delegate* unmanaged[Cdecl, Cdecl]<_GUID*, _GUID*, void**, int> s_msolapDllGetClassObjectFunc;
	}
}
