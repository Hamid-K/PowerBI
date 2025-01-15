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
using ATL;
using ATL._ATL_SAFE_ALLOCA_IMPL;
using MsolapWrapper;
using std;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x000010F4 File Offset: 0x000004F4
	internal unsafe static CAtlException* ATL.CAtlException.{ctor}(CAtlException* A_0, int hr)
	{
		*A_0 = hr;
		return A_0;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000110C File Offset: 0x0000050C
	internal unsafe static void ATL.AtlThrowImpl(int hr)
	{
		CAtlException ex;
		<Module>.ATL.CAtlException.{ctor}(ref ex, hr);
		<Module>._CxxThrowException((void*)(&ex), (_s__ThrowInfo*)(&<Module>._TI1?AVCAtlException@ATL@@));
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000025F8 File Offset: 0x000019F8
	internal unsafe static char* ATL.CComBSTR.Copy(CComBSTR* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (((num == 0UL) ? 1 : 0) != 0)
		{
			return 0L;
		}
		if (num != 0UL)
		{
			return <Module>.SysAllocStringByteLen(*A_0, <Module>.SysStringByteLen(num));
		}
		return <Module>.SysAllocStringByteLen(null, 0U);
	}

	// Token: 0x06000004 RID: 4 RVA: 0x0000114C File Offset: 0x0000054C
	internal unsafe static void ATL.CComBSTR.{dtor}(CComBSTR* A_0)
	{
		<Module>.SysFreeString(*A_0);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002CE4 File Offset: 0x000020E4
	internal unsafe static int ATL.CAccessorBase.ReleaseAccessors(CAccessorBase* A_0, IUnknown* pUnk)
	{
		if (pUnk == null)
		{
			return -2147467259;
		}
		int num = 0;
		if (*(A_0 + 8L) != 0)
		{
			CComPtr<IAccessor> ccomPtr<IAccessor> = 0L;
			try
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnk, ref <Module>._GUID_0c733a8c_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IAccessor>), (IntPtr)(*(*(long*)pUnk)));
				if (num < 0)
				{
					goto IL_00AF;
				}
				if (*A_0 != 0L)
				{
					goto IL_0068;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IAccessor>.{dtor}), (void*)(&ccomPtr<IAccessor>));
				throw;
			}
			if (ccomPtr<IAccessor> != null)
			{
				uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IAccessor>, (IntPtr)(*(*ccomPtr<IAccessor> + 16L)));
			}
			return -2147467259;
			IL_0068:
			try
			{
				uint num3 = 0;
				if (0 < *(A_0 + 8L))
				{
					do
					{
						int num4 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ccomPtr<IAccessor>, (ulong)(*(num3 * 16UL + (ulong)(*A_0))), null, (IntPtr)(*(*ccomPtr<IAccessor> + 48L)));
						num3++;
					}
					while (num3 < *(A_0 + 8L));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IAccessor>.{dtor}), (void*)(&ccomPtr<IAccessor>));
				throw;
			}
			IL_00AF:
			try
			{
				*(A_0 + 8L) = 0;
				<Module>.delete[](*A_0);
				*A_0 = 0L;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IAccessor>.{dtor}), (void*)(&ccomPtr<IAccessor>));
				throw;
			}
			if (ccomPtr<IAccessor> != null)
			{
				uint num5 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IAccessor>, (IntPtr)(*(*ccomPtr<IAccessor> + 16L)));
			}
		}
		return num;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00001168 File Offset: 0x00000568
	internal unsafe static int ATL.CAccessorBase.AllocateAccessorMemory(CAccessorBase* A_0, int nAccessors)
	{
		ulong num = (ulong)((long)nAccessors);
		void* ptr = <Module>.new[]((num > 1152921504606846975UL) ? ulong.MaxValue : (num * 16UL), ref <Module>.std.nothrow);
		*A_0 = ptr;
		if (ptr == null)
		{
			return -2147024882;
		}
		*(A_0 + 8L) = nAccessors;
		return 0;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000011B8 File Offset: 0x000005B8
	internal unsafe static int ATL.CAccessorBase.BindEntries(tagDBBINDING* pBindings, ulong nColumns, ulong* pHAccessor, ulong nSize, IAccessor* pAccessor)
	{
		if (pBindings == null)
		{
			return -2147467259;
		}
		if (pHAccessor == null)
		{
			return -2147467259;
		}
		if (pAccessor == null)
		{
			return -2147467259;
		}
		DBACCESSORFLAGSENUM dbaccessorflagsenum = ((*(int*)(pBindings + 64L / (long)sizeof(tagDBBINDING)) == 0) ? ((DBACCESSORFLAGSENUM)2) : ((DBACCESSORFLAGSENUM)4));
		int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt64,tagDBBINDING modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt64,System.UInt64*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), pAccessor, dbaccessorflagsenum, nColumns, pBindings, nSize, pHAccessor, 0L, (IntPtr)(*(*(long*)pAccessor + 32L)));
		if (0UL < nColumns)
		{
			tagDBBINDING* ptr = pBindings + 40L / (long)sizeof(tagDBBINDING);
			ulong num2 = nColumns;
			do
			{
				<Module>.delete(*(long*)ptr, 20UL);
				ptr += 88L / (long)sizeof(tagDBBINDING);
				num2 -= 1UL;
			}
			while (num2 > 0UL);
		}
		return num;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00001238 File Offset: 0x00000638
	internal unsafe static void ATL.CAccessorBase.Bind(tagDBBINDING* pBinding, ulong nOrdinal, ushort wType, ulong nLength, byte nPrecision, byte nScale, uint eParamIO, ulong nDataOffset, ulong nLengthOffset, ulong nStatusOffset, tagDBOBJECT* pdbobject)
	{
		if (pBinding == null)
		{
			<Module>.ATL.AtlThrowImpl(-2147467259);
		}
		if ((wType & 16384) != 0)
		{
			*(int*)(pBinding + 60L / (long)sizeof(tagDBBINDING)) = 1;
		}
		else
		{
			*(int*)(pBinding + 60L / (long)sizeof(tagDBBINDING)) = 0;
		}
		*(long*)(pBinding + 40L / (long)sizeof(tagDBBINDING)) = pdbobject;
		*(int*)(pBinding + 64L / (long)sizeof(tagDBBINDING)) = eParamIO;
		*(long*)pBinding = (long)nOrdinal;
		*(short*)(pBinding + 84L / (long)sizeof(tagDBBINDING)) = (short)wType;
		*(byte*)(pBinding + 86L / (long)sizeof(tagDBBINDING)) = nPrecision;
		*(byte*)(pBinding + 87L / (long)sizeof(tagDBBINDING)) = nScale;
		*(int*)(pBinding + 80L / (long)sizeof(tagDBBINDING)) = 0;
		*(long*)(pBinding + 8L / (long)sizeof(tagDBBINDING)) = (long)nDataOffset;
		*(long*)(pBinding + 16L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 24L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 32L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 48L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 72L / (long)sizeof(tagDBBINDING)) = (long)nLength;
		*(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) = 1;
		if (nLengthOffset != 0UL)
		{
			*(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) = 3;
			*(long*)(pBinding + 16L / (long)sizeof(tagDBBINDING)) = (long)nLengthOffset;
		}
		if (nStatusOffset != 0UL)
		{
			*(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) = *(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) | 4;
			*(long*)(pBinding + 24L / (long)sizeof(tagDBBINDING)) = (long)nStatusOffset;
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00003984 File Offset: 0x00002D84
	internal unsafe static void ATL.CAccessorBase.FreeType(ushort wType, byte* pValue, IRowset* pRowset)
	{
		if (pValue != null)
		{
			if ((wType & 8192) != 0)
			{
				ulong num = (ulong)(*(long*)pValue);
				if (num != 0UL)
				{
					<Module>.SafeArrayDestroy(num);
					*(long*)pValue = 0L;
				}
			}
			else if (wType != 8)
			{
				if (wType != 9)
				{
					if (wType == 12)
					{
						<Module>.VariantClear((tagVARIANT*)pValue);
						goto IL_0106;
					}
					if (wType != 13)
					{
						if (wType != 136)
						{
							goto IL_0106;
						}
						CComQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d> ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d> = 0L;
						try
						{
							if (pRowset != null)
							{
								ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d> = ((calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pRowset, ref <Module>._GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>), (IntPtr)(*(*(long*)pRowset))) < 0) ? 0L : ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>);
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IChapteredRowset>.{dtor}), (void*)(&ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>));
							throw;
						}
						try
						{
							if (((((ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d> == 0L) ? 1 : 0) == 0) ? 1 : 0) != 0)
							{
								int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>, (ulong)(*(long*)pValue), null, (IntPtr)(*(*ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d> + 32L)));
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>.{dtor}), (void*)(&ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>));
							throw;
						}
						if (ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d> != null)
						{
							uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>, (IntPtr)(*(*ccomQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d> + 16L)));
							goto IL_0106;
						}
						goto IL_0106;
					}
				}
				ulong num4 = (ulong)(*(long*)pValue);
				if (num4 != 0UL)
				{
					ulong num5 = num4;
					uint num6 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num5, (IntPtr)(*(*num5 + 16L)));
					*(long*)pValue = 0L;
				}
			}
			else
			{
				<Module>.SysFreeString(*(long*)pValue);
				*(long*)pValue = 0L;
			}
			IL_0106:
			if ((wType & 4096) != 0 && ~((wType & 16384) != 0) != 0)
			{
				<Module>.CoTaskMemFree(*(long*)(pValue + 8L));
			}
		}
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002634 File Offset: 0x00001A34
	internal unsafe static void ATL.CComPtr<IAccessor>.{dtor}(CComPtr<IAccessor>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002E30 File Offset: 0x00002230
	internal unsafe static void ATL.CComQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>.{dtor}(CComQIPtr<IChapteredRowset,&_GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002660 File Offset: 0x00001A60
	internal unsafe static void ATL.CComPtr<IChapteredRowset>.{dtor}(CComPtr<IChapteredRowset>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000268C File Offset: 0x00001A8C
	internal unsafe static void ATL.CDynamicAccessor.{dtor}(CDynamicAccessor* A_0)
	{
		<Module>.ATL.CDynamicAccessor.Close(A_0);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00001308 File Offset: 0x00000708
	internal unsafe static void ATL.CDynamicAccessor.Close(CDynamicAccessor* A_0)
	{
		<Module>.CoTaskMemFree(*(A_0 + 40L));
		*(A_0 + 40L) = 0L;
		<Module>.CoTaskMemFree(*(A_0 + 48L));
		*(A_0 + 48L) = 0L;
		<Module>.delete[](*(A_0 + 16L));
		*(A_0 + 16L) = 0L;
		<Module>.delete[](*(A_0 + 32L));
		*(A_0 + 32L) = 0L;
		*(A_0 + 24L) = 0L;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00003AF4 File Offset: 0x00002EF4
	internal unsafe static void ATL.CDynamicAccessor.FreeRecordMemory(CDynamicAccessor* A_0, IRowset* pRowset)
	{
		uint num = 0;
		if (0L < *(A_0 + 24L))
		{
			CDynamicAccessor* ptr = A_0 + 32L;
			for (;;)
			{
				ulong num2 = (ulong)(*ptr);
				if (num2 == 0UL)
				{
					goto IL_00A5;
				}
				long num3 = (long)num;
				if (*(num3 + (long)num2) != 1)
				{
					goto IL_00A5;
				}
				long num4 = num3 * 80L + *(A_0 + 40L);
				long num5 = *(num4 + 8L);
				long num6 = *(A_0 + 16L);
				if (*(((((num5 + *(num4 + 32L) + 7L) & -8L) + 8L) & -4L) + num6) != 3)
				{
					long num7 = num6 + num5;
					void* ptr2 = num7;
					if (ptr2 != null && *(long*)ptr2 != 0L)
					{
						<Module>.CoTaskMemFree(*num7);
						*(long*)ptr2 = 0L;
					}
				}
				IL_011A:
				num++;
				if (num >= (ulong)(*(A_0 + 24L)))
				{
					break;
				}
				continue;
				IL_00A5:
				long num8 = (long)(num * 80UL + (ulong)(*(A_0 + 40L)));
				ushort num9 = *(num8 + 40L);
				if (num9 != 13 && num9 != 9)
				{
					<Module>.ATL.CAccessorBase.FreeType(num9, *(num8 + 8L) + *(A_0 + 16L), pRowset);
					goto IL_011A;
				}
				long num10 = *(num8 + 8L);
				num6 = *(A_0 + 16L);
				if (*(((((num10 + *(num8 + 32L) + 7L) & -8L) + 8L) & -4L) + num6) == 0)
				{
					<Module>.ATL.CAccessorBase.FreeType(num9, num10 + num6, pRowset);
					goto IL_011A;
				}
				goto IL_011A;
			}
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x00001370 File Offset: 0x00000770
	internal unsafe static void ATL.CDynamicAccessor.BindEx(tagDBBINDING* pBinding, ulong nOrdinal, ushort wType, ulong nLength, byte nPrecision, byte nScale, uint eParamIO, ulong nDataOffset, ulong nLengthOffset, ulong nStatusOffset, tagDBOBJECT* pdbobject, uint dwMemOwner, [MarshalAs(UnmanagedType.U1)] bool fSkipData)
	{
		if (pBinding == null)
		{
			<Module>.ATL.AtlThrowImpl(-2147467259);
		}
		*(int*)(pBinding + 60L / (long)sizeof(tagDBBINDING)) = dwMemOwner;
		*(long*)(pBinding + 40L / (long)sizeof(tagDBBINDING)) = pdbobject;
		*(int*)(pBinding + 64L / (long)sizeof(tagDBBINDING)) = eParamIO;
		*(long*)pBinding = (long)nOrdinal;
		*(short*)(pBinding + 84L / (long)sizeof(tagDBBINDING)) = (short)wType;
		*(byte*)(pBinding + 86L / (long)sizeof(tagDBBINDING)) = nPrecision;
		*(byte*)(pBinding + 87L / (long)sizeof(tagDBBINDING)) = nScale;
		*(int*)(pBinding + 80L / (long)sizeof(tagDBBINDING)) = 0;
		*(long*)(pBinding + 8L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 16L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 24L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 32L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 48L / (long)sizeof(tagDBBINDING)) = 0L;
		*(long*)(pBinding + 72L / (long)sizeof(tagDBBINDING)) = (long)nLength;
		*(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) = 0;
		if (!fSkipData)
		{
			*(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) = 1;
			*(long*)(pBinding + 8L / (long)sizeof(tagDBBINDING)) = (long)nDataOffset;
		}
		if (nLengthOffset != 0UL)
		{
			*(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) = *(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) | 2;
			*(long*)(pBinding + 16L / (long)sizeof(tagDBBINDING)) = (long)nLengthOffset;
		}
		if (nStatusOffset != 0UL)
		{
			*(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) = *(int*)(pBinding + 56L / (long)sizeof(tagDBBINDING)) | 4;
			*(long*)(pBinding + 24L / (long)sizeof(tagDBBINDING)) = (long)nStatusOffset;
		}
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00003DE4 File Offset: 0x000031E4
	internal unsafe static int ATL.CDynamicAccessor.GetRowsetProperties(CDynamicAccessor* A_0, IUnknown* pUnk, uint* prgPropertyIDs, int* pbValues, uint nPropCount)
	{
		if (pUnk == null)
		{
			return -2147467259;
		}
		if (pbValues != null)
		{
			CComPtr<IRowsetInfo> ccomPtr<IRowsetInfo> = 0L;
			int num;
			try
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnk, ref <Module>._GUID_0c733a55_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IRowsetInfo>), (IntPtr)(*(*(long*)pUnk)));
				if (0 < nPropCount)
				{
					initblk(pbValues, 0, nPropCount << 2);
				}
				if (num >= 0)
				{
					uint num2 = 0;
					CComHeapPtr<tagDBPROPSET> ccomHeapPtr<tagDBPROPSET> = 0L;
					try
					{
						$ArrayType$$$BY00UtagDBPROPIDSET@@ $ArrayType$$$BY00UtagDBPROPIDSET@@ = prgPropertyIDs;
						*((ref $ArrayType$$$BY00UtagDBPROPIDSET@@) + 8) = nPropCount;
						cpblk((ref $ArrayType$$$BY00UtagDBPROPIDSET@@) + 12, ref <Module>.DBPROPSET_ROWSET, 16);
						num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.IsConst),tagDBPROPIDSET modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*,tagDBPROPSET**), ccomPtr<IRowsetInfo>, 1, ref $ArrayType$$$BY00UtagDBPROPIDSET@@, &num2, (tagDBPROPSET**)(&ccomHeapPtr<tagDBPROPSET>), (IntPtr)(*(*ccomPtr<IRowsetInfo> + 24L)));
						if (num >= 0)
						{
							uint num3 = 0;
							long num4 = ccomHeapPtr<tagDBPROPSET> + 8L;
							for (;;)
							{
								uint num5 = (uint)(*num4);
								uint num6 = ((num5 < nPropCount) ? num5 : nPropCount);
								if (num3 >= num6)
								{
									break;
								}
								long num7 = (long)num3;
								(num7 * 4L / 4L)[pbValues] = (int)(*(*ccomHeapPtr<tagDBPROPSET> + num7 * 72L + 56L));
								num3++;
							}
							long num8 = *ccomHeapPtr<tagDBPROPSET>;
							if (num8 != 0L)
							{
								<Module>.CoTaskMemFree(num8);
							}
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComHeapPtr<tagDBPROPSET>.{dtor}), (void*)(&ccomHeapPtr<tagDBPROPSET>));
						throw;
					}
					<Module>.CoTaskMemFree(ccomHeapPtr<tagDBPROPSET>);
					ccomHeapPtr<tagDBPROPSET> = 0L;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowsetInfo>.{dtor}), (void*)(&ccomPtr<IRowsetInfo>));
				throw;
			}
			if (ccomPtr<IRowsetInfo> != null)
			{
				uint num9 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IRowsetInfo>, (IntPtr)(*(*ccomPtr<IRowsetInfo> + 16L)));
			}
			return num;
		}
		return -2147467259;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00003F48 File Offset: 0x00003348
	internal unsafe static int ATL.CDynamicAccessor.BindColumns(CDynamicAccessor* A_0, IUnknown* pUnk)
	{
		_GUID guid = <Module>._GUID_0c733a30_2a1c_11ce_ade5_00aa0044773d;
		bool flag = false;
		if (pUnk != null)
		{
			CComPtr<IAccessor> ccomPtr<IAccessor> = 0L;
			int num;
			try
			{
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnk, ref <Module>._GUID_0c733a8c_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IAccessor>), (IntPtr)(*(*(long*)pUnk)));
				if (num < 0)
				{
					num = num;
				}
				else
				{
					ulong num2 = 0UL;
					CDynamicAccessor* ptr = A_0 + 40L;
					int num4;
					if (*ptr == 0L)
					{
						CComPtr<IColumnsInfo> ccomPtr<IColumnsInfo> = 0L;
						try
						{
							num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), pUnk, ref <Module>._GUID_0c733a11_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IColumnsInfo>), (IntPtr)(*(*(long*)pUnk)));
							if (num >= 0)
							{
								num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64*,tagDBCOLUMNINFO**,System.Char**), ccomPtr<IColumnsInfo>, A_0 / 8 + 24L, ptr, A_0 / sizeof(char*) + 48L, (IntPtr)(*(*ccomPtr<IColumnsInfo> + 24L)));
								if (num >= 0)
								{
									goto IL_00AD;
								}
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IColumnsInfo>.{dtor}), (void*)(&ccomPtr<IColumnsInfo>));
							throw;
						}
						if (ccomPtr<IColumnsInfo> != null)
						{
							uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IColumnsInfo>, (IntPtr)(*(*ccomPtr<IColumnsInfo> + 16L)));
						}
						num4 = num;
						goto IL_0122;
						IL_00AD:
						try
						{
							*(A_0 + 56L) = 0;
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IColumnsInfo>.{dtor}), (void*)(&ccomPtr<IColumnsInfo>));
							throw;
						}
						if (ccomPtr<IColumnsInfo> != null)
						{
							uint num5 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IColumnsInfo>, (IntPtr)(*(*ccomPtr<IColumnsInfo> + 16L)));
						}
					}
					else
					{
						*(A_0 + 56L) = 1;
					}
					ulong num6 = (ulong)(*(A_0 + 24L));
					tagDBBINDING* ptr2 = <Module>.new[]((num6 > 209622091746699450UL) ? ulong.MaxValue : (num6 * 88UL), ref <Module>.std.nothrow);
					if (ptr2 != null)
					{
						CAutoVectorPtr<tagDBBINDING> cautoVectorPtr<tagDBBINDING> = ptr2;
						try
						{
							bool* ptr3 = <Module>.new[]((ulong)(*(A_0 + 24L)), ref <Module>.std.nothrow);
							*(A_0 + 32L) = ptr3;
							int num41;
							int num43;
							if (ptr3 != null)
							{
								tagDBBINDING* ptr4 = ptr2;
								uint num7 = 0;
								if (0L < *(A_0 + 24L))
								{
									CDynamicAccessor* ptr5 = A_0 + 64L;
									for (;;)
									{
										long num8 = (long)num7;
										*(*(A_0 + 32L) + num8) = 0;
										long num9 = num8 * 80L;
										long num10 = *ptr + num9;
										ulong num11 = (ulong)(*(num10 + 32L));
										if (num11 <= (ulong)(*ptr5))
										{
											goto IL_042B;
										}
										ushort num12 = *(num10 + 40L);
										if (num12 == 13)
										{
											goto IL_042B;
										}
										int num13 = *(A_0 + 60L);
										ulong num14;
										if (num13 == 2)
										{
											num14 = 0UL;
											num2 = (num2 + 7UL) & 18446744073709551608UL;
											ulong num15 = num2;
											num2 = (num2 + 11UL) & 18446744073709551612UL;
											ulong num16 = num2;
											num2 += 4UL;
											*(num10 + 32L) = 0L;
											long num17 = *ptr + num9;
											<Module>.ATL.CDynamicAccessor.BindEx(ptr4, (ulong)(*(num17 + 16L)), *(num17 + 40L), (ulong)(*(num17 + 32L)), *(num17 + 42L), *(num17 + 43L), 0, 0UL, num15, num16, null, 0, true);
											ptr4 += 88L / (long)sizeof(tagDBBINDING);
										}
										else if (num13 == 1)
										{
											*(num10 + 40L) = (short)(num12 | 16384);
											*(num9 + *ptr + 32L) = 8L;
											*(*(A_0 + 32L) + num8) = 1;
											uint num18 = <Module>.ATL.CDynamicAccessor.GetAlignment(*(num9 + *ptr + 40L));
											long num17 = *ptr + num9;
											ulong num19 = (ulong)(*(num17 + 32L));
											ulong num20 = num19;
											uint num21 = num18;
											num2 = (num21 - 1 + num2) & ~(num21 - 1UL);
											num14 = num2;
											num2 = (num2 + num20 + 7UL) & 18446744073709551608UL;
											ulong num15 = num2;
											num2 = (num2 + 11UL) & 18446744073709551612UL;
											ulong num22 = num2;
											num2 += 4UL;
											<Module>.ATL.CDynamicAccessor.BindEx(ptr4, (ulong)(*(num17 + 16L)), *(num17 + 40L), num19, *(num17 + 42L), *(num17 + 43L), 0, num14, num15, num22, null, 0, false);
											ptr4 += 88L / (long)sizeof(tagDBBINDING);
										}
										else
										{
											if (!flag)
											{
												$ArrayType$$$BY01K $ArrayType$$$BY01K = 137;
												*((ref $ArrayType$$$BY01K) + 4) = 139;
												$ArrayType$$$BY01H $ArrayType$$$BY01H = 0;
												*((ref $ArrayType$$$BY01H) + 4) = 0;
												<Module>.ATL.CDynamicAccessor.GetRowsetProperties(A_0, pUnk, (uint*)(&$ArrayType$$$BY01K), (int*)(&$ArrayType$$$BY01H), 2);
												if ($ArrayType$$$BY01H != null)
												{
													guid = <Module>._GUID_0c733a30_2a1c_11ce_ade5_00aa0044773d;
												}
												else if (*((ref $ArrayType$$$BY01H) + 4) != 0)
												{
													guid = <Module>._GUID_0000000c_0000_0000_c000_000000000046;
												}
												flag = true;
											}
											tagDBOBJECT* ptr6 = <Module>.@new(20UL, ref <Module>.std.nothrow);
											if (ptr6 == null)
											{
												break;
											}
											*(int*)ptr6 = 0;
											cpblk(ptr6 + 4L / (long)sizeof(tagDBOBJECT), ref guid, 16);
											*(num9 + *ptr + 40L) = 13;
											*(num9 + *ptr + 32L) = 8L;
											uint num23 = <Module>.ATL.CDynamicAccessor.GetAlignment(*(num9 + *ptr + 40L));
											long num17 = *ptr + num9;
											ulong num24 = (ulong)(*(num17 + 32L));
											ulong num25 = num24;
											uint num26 = num23;
											num2 = (num26 - 1 + num2) & ~(num26 - 1UL);
											num14 = num2;
											num2 = (num2 + num25 + 7UL) & 18446744073709551608UL;
											ulong num15 = num2;
											num2 = (num2 + 11UL) & 18446744073709551612UL;
											num25 = num2;
											num2 += 4UL;
											<Module>.ATL.CAccessorBase.Bind(ptr4, (ulong)(*(num17 + 16L)), *(num17 + 40L), num24, *(num17 + 42L), *(num17 + 43L), 0, num14, num15, num25, ptr6);
											ptr4 += 88L / (long)sizeof(tagDBBINDING);
										}
										IL_05EF:
										*(num9 + *ptr + 8L) = (long)num14;
										num7++;
										if (num7 < (ulong)(*(A_0 + 24L)))
										{
											continue;
										}
										goto IL_06A2;
										IL_042B:
										num12 = *(num10 + 40L);
										if (num12 != 13)
										{
											if (num12 == 129)
											{
												*(num10 + 32L) = (long)(num11 + 1UL);
											}
											long num27 = *ptr + num9;
											if (*(num27 + 40L) == 130)
											{
												*(num27 + 32L) = (*(num27 + 32L) + 1L) * 2L;
											}
											uint num28 = <Module>.ATL.CDynamicAccessor.GetAlignment(*(num9 + *ptr + 40L));
											long num17 = *ptr + num9;
											ulong num29 = (ulong)(*(num17 + 32L));
											ulong num30 = num29;
											uint num31 = num28;
											num2 = (num31 - 1 + num2) & ~(num31 - 1UL);
											num14 = num2;
											num2 = (num2 + num30 + 7UL) & 18446744073709551608UL;
											ulong num15 = num2;
											num2 = (num2 + 11UL) & 18446744073709551612UL;
											ulong num32 = num2;
											num2 += 4UL;
											<Module>.ATL.CAccessorBase.Bind(ptr4, (ulong)(*(num17 + 16L)), *(num17 + 40L), num29, *(num17 + 42L), *(num17 + 43L), 0, num14, num15, num32, null);
											ptr4 += 88L / (long)sizeof(tagDBBINDING);
											goto IL_05EF;
										}
										tagDBOBJECT* ptr7 = <Module>.@new(20UL, ref <Module>.std.nothrow);
										if (ptr7 != null)
										{
											*(int*)ptr7 = 0;
											cpblk(ptr7 + 4L / (long)sizeof(tagDBOBJECT), ref <Module>._GUID_00000000_0000_0000_c000_000000000046, 16);
											*(num9 + *ptr + 40L) = 13;
											*(num9 + *ptr + 32L) = 8L;
											uint num33 = <Module>.ATL.CDynamicAccessor.GetAlignment(*(num9 + *ptr + 40L));
											long num17 = *ptr + num9;
											ulong num34 = (ulong)(*(num17 + 32L));
											ulong num35 = num34;
											uint num36 = num33;
											num2 = (num36 - 1 + num2) & ~(num36 - 1UL);
											num14 = num2;
											num2 = (num2 + num35 + 7UL) & 18446744073709551608UL;
											ulong num15 = num2;
											num2 = (num2 + 11UL) & 18446744073709551612UL;
											ulong num37 = num2;
											num2 += 4UL;
											<Module>.ATL.CAccessorBase.Bind(ptr4, (ulong)(*(num17 + 16L)), *(num17 + 40L), num34, *(num17 + 42L), *(num17 + 43L), 0, num14, num15, num37, ptr7);
											ptr4 += 88L / (long)sizeof(tagDBBINDING);
											goto IL_05EF;
										}
										goto IL_0657;
									}
									if (0 < num7)
									{
										tagDBBINDING* ptr8 = ptr2 + 40L / (long)sizeof(tagDBBINDING);
										uint num38 = num7;
										do
										{
											<Module>.delete(*(long*)ptr8, 20UL);
											ptr8 += 88L / (long)sizeof(tagDBBINDING);
											num38 += -1;
										}
										while (num38 > 0);
									}
									<Module>.delete[](*(A_0 + 32L));
									*(A_0 + 32L) = 0L;
									goto IL_0699;
									IL_0657:
									if (0 < num7)
									{
										tagDBBINDING* ptr9 = ptr2 + 40L / (long)sizeof(tagDBBINDING);
										uint num39 = num7;
										do
										{
											<Module>.delete(*(long*)ptr9, 20UL);
											ptr9 += 88L / (long)sizeof(tagDBBINDING);
											num39 += -1;
										}
										while (num39 > 0);
									}
									<Module>.delete[](*(A_0 + 32L));
									*(A_0 + 32L) = 0L;
									goto IL_0699;
								}
								IL_06A2:
								if (*A_0 == 0L)
								{
									num = <Module>.ATL.CAccessorBase.AllocateAccessorMemory(A_0, 1);
									if (num < 0)
									{
										uint num40 = 0;
										if (0L < *(A_0 + 24L))
										{
											do
											{
												<Module>.delete(*(long*)(ptr2 + num40 * 88UL / (ulong)sizeof(tagDBBINDING) + 40L / (long)sizeof(tagDBBINDING)), 20UL);
												num40++;
											}
											while (num40 < (ulong)(*(A_0 + 24L)));
										}
										<Module>.delete[](*(A_0 + 32L));
										*(A_0 + 32L) = 0L;
										num41 = num;
										goto IL_06FD;
									}
									*(*A_0 + 8L) = 1;
								}
								byte* ptr10 = <Module>.new[](num2, ref <Module>.std.nothrow);
								*(A_0 + 16L) = ptr10;
								if (ptr10 == null)
								{
									uint num42 = 0;
									if (0L < *(A_0 + 24L))
									{
										do
										{
											<Module>.delete(*(long*)(ptr2 + num42 * 88UL / (ulong)sizeof(tagDBBINDING) + 40L / (long)sizeof(tagDBBINDING)), 20UL);
											num42++;
										}
										while (num42 < (ulong)(*(A_0 + 24L)));
									}
									<Module>.delete[](*(A_0 + 32L));
									*(A_0 + 32L) = 0L;
									num43 = -2147024882;
									goto IL_0772;
								}
								initblk(ptr10, 0, num2);
								num = <Module>.ATL.CAccessorBase.BindEntries(ptr2, (ulong)(*(A_0 + 24L)), *A_0, num2, ccomPtr<IAccessor>);
								if (num < 0)
								{
									<Module>.delete[](*(A_0 + 32L));
									*(A_0 + 32L) = 0L;
								}
								num = num;
								goto IL_07BD;
							}
							IL_0699:
							num41 = -2147024882;
							IL_06FD:
							num43 = num41;
							IL_0772:
							num = num43;
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CAutoVectorPtr<tagDBBINDING>.{dtor}), (void*)(&cautoVectorPtr<tagDBBINDING>));
							throw;
						}
						IL_07BD:
						<Module>.delete[]((void*)ptr2);
						cautoVectorPtr<tagDBBINDING> = 0L;
						goto IL_07C9;
					}
					num4 = -2147024882;
					IL_0122:
					num = num4;
				}
				IL_07C9:;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IAccessor>.{dtor}), (void*)(&ccomPtr<IAccessor>));
				throw;
			}
			if (ccomPtr<IAccessor> != null)
			{
				uint num44 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IAccessor>, (IntPtr)(*(*ccomPtr<IAccessor> + 16L)));
			}
			return num;
		}
		return -2147467259;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00001444 File Offset: 0x00000844
	internal static ulong ATL.CDynamicAccessor.GetAlignment(ushort bType)
	{
		if ((bType & 16384) != 0)
		{
			return 8L;
		}
		if ((bType & 8192) != 0)
		{
			return 8L;
		}
		if ((bType & 4096) != 0)
		{
			return 8L;
		}
		switch (bType)
		{
		case 2:
			return 2L;
		case 3:
			return 4L;
		case 4:
			return 4L;
		case 5:
			return 8L;
		case 6:
			return 8L;
		case 7:
			return 8L;
		case 8:
			return 8L;
		case 9:
			return 8L;
		case 10:
			return 4L;
		case 11:
			return 2L;
		case 12:
			return 8L;
		case 13:
			return 8L;
		case 14:
			return 8L;
		case 16:
			return 1L;
		case 17:
			return 1L;
		case 18:
			return 2L;
		case 19:
			return 4L;
		case 20:
			return 1L;
		case 21:
			return 1L;
		case 72:
			return 4L;
		case 128:
			return 1L;
		case 129:
			return 1L;
		case 130:
			return 2L;
		case 131:
			return 1L;
		case 133:
			return 2L;
		case 134:
			return 2L;
		case 135:
			return 4L;
		}
		return 8L;
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000026A8 File Offset: 0x00001AA8
	internal unsafe static void ATL.CComPtr<IColumnsInfo>.{dtor}(CComPtr<IColumnsInfo>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000026D4 File Offset: 0x00001AD4
	internal unsafe static void ATL.CComPtr<IRowsetInfo>.{dtor}(CComPtr<IRowsetInfo>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00003C34 File Offset: 0x00003034
	internal unsafe static void ATL.CComHeapPtr<tagDBPROPSET>.{dtor}(CComHeapPtr<tagDBPROPSET>* A_0)
	{
		<Module>.CoTaskMemFree(*A_0);
		*A_0 = 0L;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002E5C File Offset: 0x0000225C
	internal unsafe static void ATL.CSession.{dtor}(CSession* A_0)
	{
		try
		{
			IOpenRowset* ptr = *A_0;
			if (ptr != null)
			{
				*A_0 = 0L;
				IOpenRowset* ptr2 = ptr;
				uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IOpenRowset>.{dtor}), A_0);
			throw;
		}
		long num2 = *A_0;
		if (num2 != 0L)
		{
			long num3 = num2;
			uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num3, (IntPtr)(*(*num3 + 16L)));
		}
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002700 File Offset: 0x00001B00
	internal unsafe static void ATL.CComPtr<IOpenRowset>.{dtor}(CComPtr<IOpenRowset>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002EC8 File Offset: 0x000022C8
	internal unsafe static void ATL.CDataSource.{dtor}(CDataSource* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600001A RID: 26 RVA: 0x0000272C File Offset: 0x00001B2C
	internal unsafe static void ATL.CComPtr<IDBInitialize>.{dtor}(CComPtr<IDBInitialize>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00003C54 File Offset: 0x00003054
	internal unsafe static void ATL.CCommandBase.{dtor}(CCommandBase* A_0)
	{
		try
		{
			<Module>.ATL.CCommandBase.ReleaseCommand(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<ICommand>.{dtor}), A_0);
			throw;
		}
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003CB0 File Offset: 0x000030B0
	internal unsafe static int ATL.CCommandBase.CreateCommand(CCommandBase* A_0, CSession* session)
	{
		<Module>.ATL.CCommandBase.ReleaseCommand(A_0);
		CComPtr<IDBCreateCommand> ccomPtr<IDBCreateCommand> = 0L;
		int num3;
		try
		{
			long num = *session;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), (IntPtr)num, ref <Module>._GUID_0c733a1d_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IDBCreateCommand>), (IntPtr)(*(*num)));
			if (num2 < 0)
			{
				num3 = num2;
			}
			else
			{
				num3 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),IUnknown**), ccomPtr<IDBCreateCommand>, null, ref <Module>._GUID_0c733a63_2a1c_11ce_ade5_00aa0044773d, A_0, (IntPtr)(*(*ccomPtr<IDBCreateCommand> + 24L)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBCreateCommand>.{dtor}), (void*)(&ccomPtr<IDBCreateCommand>));
			throw;
		}
		if (ccomPtr<IDBCreateCommand> != null)
		{
			uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBCreateCommand>, (IntPtr)(*(*ccomPtr<IDBCreateCommand> + 16L)));
		}
		return num3;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003D4C File Offset: 0x0000314C
	internal unsafe static int ATL.CCommandBase.Create(CCommandBase* A_0, CSession* session, char* wszCommand, _GUID* guidCommand)
	{
		int num = <Module>.ATL.CCommandBase.CreateCommand(A_0, session);
		if (num >= 0)
		{
			CComPtr<ICommandText> ccomPtr<ICommandText> = 0L;
			try
			{
				_NoAddRefReleaseOnCComPtr<ICommand>* ptr = *A_0;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_0c733a27_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<ICommandText>), (IntPtr)(*(*(long*)ptr)));
				if (num >= 0)
				{
					num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Char modopt(System.Runtime.CompilerServices.IsConst)*), ccomPtr<ICommandText>, guidCommand, wszCommand, (IntPtr)(*(*ccomPtr<ICommandText> + 56L)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<ICommandText>.{dtor}), (void*)(&ccomPtr<ICommandText>));
				throw;
			}
			if (ccomPtr<ICommandText> != null)
			{
				uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<ICommandText>, (IntPtr)(*(*ccomPtr<ICommandText> + 16L)));
			}
		}
		return num;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00002EF4 File Offset: 0x000022F4
	internal unsafe static void ATL.CCommandBase.ReleaseCommand(CCommandBase* A_0)
	{
		if (*(A_0 + 8L) != 0L && ((((*A_0 == 0L) ? 1 : 0) == 0) ? 1 : 0) != 0)
		{
			CComPtr<IAccessor> ccomPtr<IAccessor> = 0L;
			try
			{
				_NoAddRefReleaseOnCComPtr<ICommand>* ptr = *A_0;
				if (calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_0c733a8c_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IAccessor>), (IntPtr)(*(*(long*)ptr))) >= 0)
				{
					int num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ccomPtr<IAccessor>, (ulong)(*(A_0 + 8L)), null, (IntPtr)(*(*ccomPtr<IAccessor> + 48L)));
					*(A_0 + 8L) = 0L;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IAccessor>.{dtor}), (void*)(&ccomPtr<IAccessor>));
				throw;
			}
			if (ccomPtr<IAccessor> != null)
			{
				uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IAccessor>, (IntPtr)(*(*ccomPtr<IAccessor> + 16L)));
			}
		}
		ICommand* ptr2 = *A_0;
		if (ptr2 != null)
		{
			*A_0 = 0L;
			ICommand* ptr3 = ptr2;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, (IntPtr)(*(*(long*)ptr3 + 16L)));
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<ICommand>.{dtor}(CComPtr<ICommand>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IDBCreateCommand>.{dtor}(CComPtr<IDBCreateCommand>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<ICommandText>.{dtor}(CComPtr<ICommandText>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<ICommandProperties>.{dtor}(CComPtr<ICommandProperties>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00004F10 File Offset: 0x00004310
	internal unsafe static void ATL.CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>.{dtor}(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* A_0)
	{
		try
		{
			try
			{
				CMultipleResults* ptr = A_0 + 184L;
				ulong num = (ulong)(*ptr);
				if (num != 0UL)
				{
					ulong num2 = num;
					uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CCommandBase.{dtor}), (void*)(A_0 + (byte*)168L));
				throw;
			}
			CCommandBase* ptr2 = A_0 + 168L;
			try
			{
				<Module>.ATL.CCommandBase.ReleaseCommand(ptr2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<ICommand>.{dtor}), ptr2);
				throw;
			}
			ulong num4 = (ulong)(*ptr2);
			if (num4 != 0UL)
			{
				ulong num5 = num4;
				uint num6 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num5, (IntPtr)(*(*num5 + 16L)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{dtor}), A_0);
			throw;
		}
		<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{dtor}(A_0);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00004BC4 File Offset: 0x00003FC4
	internal unsafe static int ATL.CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>.GetNextResult(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* A_0, long* pulRowsAffected, [MarshalAs(UnmanagedType.U1)] bool bBind)
	{
		if (*(A_0 + 184L) == 0L)
		{
			return -2147467259;
		}
		<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Close(A_0);
		long num = *(A_0 + 184L);
		CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* ptr = A_0 + 72L;
		int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,System.Int64,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Int64*,IUnknown**), (IntPtr)num, null, 0L, ref <Module>._GUID_0c733a7c_2a1c_11ce_ade5_00aa0044773d, pulRowsAffected, ptr, (IntPtr)(*(*num + 24L)));
		if (num2 < 0)
		{
			return num2;
		}
		if (bBind && *ptr != 0L)
		{
			return <Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Bind(A_0);
		}
		return num2;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x00004AD0 File Offset: 0x00003ED0
	internal unsafe static void ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Close(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* A_0)
	{
		CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* ptr = A_0 + 72L;
		long num = *ptr;
		if (num != 0L)
		{
			IRowset* ptr2 = num;
			<Module>.ATL.CDynamicAccessor.FreeRecordMemory(A_0, ptr2);
			<Module>.ATL.CAccessorBase.ReleaseAccessors(A_0, (IUnknown*)ptr2);
			<Module>.ATL.CDynamicAccessor.Close(A_0);
			<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.Close(ptr);
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x000047B8 File Offset: 0x00003BB8
	internal unsafe static int ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Bind(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* A_0)
	{
		CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* ptr = A_0 + 72L;
		long num = *ptr;
		if (num == 0L)
		{
			return -2147467259;
		}
		IRowset* ptr2 = num;
		int num2 = <Module>.ATL.CDynamicAccessor.BindColumns(A_0, (IUnknown*)ptr2);
		if (num2 >= 0)
		{
			num2 = <Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.BindFinished(ptr);
		}
		return num2;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x00004C2C File Offset: 0x0000402C
	internal unsafe static void ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{dtor}(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* A_0)
	{
		try
		{
			try
			{
				<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Close(A_0);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.{dtor}), (void*)(A_0 + (byte*)72L));
				throw;
			}
			CChapterBulkRowset<ATL::CDynamicAccessor>* ptr = A_0 + 72L;
			try
			{
				<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.Close(ptr);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CRowset<ATL::CDynamicAccessor>.{dtor}), ptr);
				throw;
			}
			<Module>.ATL.CRowset<ATL::CDynamicAccessor>.{dtor}(ptr);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CDynamicAccessor.{dtor}), A_0);
			throw;
		}
		<Module>.ATL.CDynamicAccessor.Close(A_0);
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00004CDC File Offset: 0x000040DC
	internal unsafe static CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{ctor}(CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* A_0)
	{
		*A_0 = 0L;
		*(A_0 + 8L) = 0;
		*(A_0 + 16L) = 0L;
		*(A_0 + 24L) = 0L;
		*(A_0 + 40L) = 0L;
		*(A_0 + 48L) = 0L;
		*(A_0 + 60L) = 0;
		*(A_0 + 64L) = 8000L;
		*(A_0 + 32L) = 0L;
		try
		{
			CChapterBulkRowset<ATL::CDynamicAccessor>* ptr = A_0 + 72L;
			<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.{ctor}(ptr);
			try
			{
				*(ptr + 80L) = 0;
				*(ptr + 88L) = 0L;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CBulkRowset<ATL::CDynamicAccessor>.{dtor}), ptr);
				throw;
			}
			try
			{
				*(A_0 + 88L) = A_0;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.{dtor}), (void*)(A_0 + (byte*)72L));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CDynamicAccessor.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000038CC File Offset: 0x00002CCC
	internal unsafe static void ATL.CRowset<ATL::CDynamicAccessor>.SetupOptionalRowsetInterfaces(CRowset<ATL::CDynamicAccessor>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (((((num == 0UL) ? 1 : 0) == 0) ? 1 : 0) != 0)
		{
			_NoAddRefReleaseOnCComPtr<IRowset>* ptr = num;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), ptr, ref <Module>._GUID_0c733a05_2a1c_11ce_ade5_00aa0044773d, A_0 / sizeof(void*) + 8L, (IntPtr)(*(*(long*)ptr)));
		}
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00003904 File Offset: 0x00002D04
	internal unsafe static IDBInitialize* ATL.CComPtr<IDBInitialize>.=(CComPtr<IDBInitialize>* A_0, CComPtr<IDBInitialize>* lp)
	{
		ulong num = (ulong)(*lp);
		if (*A_0 != (long)num)
		{
			IDBInitialize* ptr = num;
			CComPtr<IDBInitialize> ccomPtr<IDBInitialize> = ptr;
			if (ptr != null)
			{
				IDBInitialize* ptr2 = ptr;
				uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 8L)));
			}
			try
			{
				ccomPtr<IDBInitialize> = *A_0;
				*A_0 = ptr;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBInitialize>.{dtor}), (void*)(&ccomPtr<IDBInitialize>));
				throw;
			}
			if (ccomPtr<IDBInitialize> != null)
			{
				uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBInitialize>, (IntPtr)(*(*ccomPtr<IDBInitialize> + 16L)));
			}
		}
		return *A_0;
	}

	// Token: 0x0600002B RID: 43 RVA: 0x0000247C File Offset: 0x0000187C
	internal unsafe static void ATL.CComPtrBase<IDBInitialize>.{dtor}(CComPtrBase<IDBInitialize>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00002C6C File Offset: 0x0000206C
	internal unsafe static void ATL.CAutoVectorPtr<tagDBBINDING>.{dtor}(CAutoVectorPtr<tagDBBINDING>* A_0)
	{
		<Module>.delete[](*A_0);
		*A_0 = 0L;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00004B0C File Offset: 0x00003F0C
	internal unsafe static void MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.{dtor}(CChapterBulkRowset<ATL::CDynamicAccessor>* A_0)
	{
		try
		{
			<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.Close(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CRowset<ATL::CDynamicAccessor>.{dtor}), A_0);
			throw;
		}
		<Module>.ATL.CRowset<ATL::CDynamicAccessor>.{dtor}(A_0);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x000024A4 File Offset: 0x000018A4
	internal unsafe static int ATL.CBulkRowset<ATL::CDynamicAccessor>.BindFinished(CBulkRowset<ATL::CDynamicAccessor>* A_0)
	{
		*(A_0 + 64L) = 0L;
		*(A_0 + 72L) = 0L;
		*(A_0 + 40L) = 0;
		if (*(A_0 + 48L) == 0L)
		{
			ulong num = (ulong)(*(A_0 + 56L));
			void* ptr = <Module>.new[]((num > 2305843009213693951UL) ? ulong.MaxValue : (num * 8UL), ref <Module>.std.nothrow);
			*(A_0 + 48L) = ptr;
			if (ptr == null)
			{
				return -2147024882;
			}
		}
		return 0;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x0000495C File Offset: 0x00003D5C
	internal unsafe static void ATL.CBulkRowset<ATL::CDynamicAccessor>.Close(CBulkRowset<ATL::CDynamicAccessor>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (((((num == 0UL) ? 1 : 0) == 0) ? 1 : 0) != 0)
		{
			<Module>.ATL.CDynamicAccessor.FreeRecordMemory(*(A_0 + 16L), num);
			<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.ReleaseRows(A_0);
		}
		<Module>.ATL.CRowset<ATL::CDynamicAccessor>.Close(A_0);
		<Module>.delete[](*(A_0 + 48L));
		*(A_0 + 48L) = 0L;
		*(A_0 + 40L) = 0;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x000049B4 File Offset: 0x00003DB4
	internal unsafe static void ATL.CBulkRowset<ATL::CDynamicAccessor>.{dtor}(CBulkRowset<ATL::CDynamicAccessor>* A_0)
	{
		try
		{
			<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.Close(A_0);
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CRowset<ATL::CDynamicAccessor>.{dtor}), A_0);
			throw;
		}
		<Module>.ATL.CRowset<ATL::CDynamicAccessor>.{dtor}(A_0);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00004A00 File Offset: 0x00003E00
	internal unsafe static CBulkRowset<ATL::CDynamicAccessor>* ATL.CBulkRowset<ATL::CDynamicAccessor>.{ctor}(CBulkRowset<ATL::CDynamicAccessor>* A_0)
	{
		*A_0 = 0L;
		try
		{
			CComPtr<IRowsetChange>* ptr = A_0 + 8L;
			*ptr = 0L;
			try
			{
				*(A_0 + 32L) = 0L;
				*(A_0 + 16L) = 0L;
				*(A_0 + 24L) = 0L;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowsetChange>.{dtor}), (void*)(A_0 + (byte*)8L));
				throw;
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), A_0);
			throw;
		}
		try
		{
			*(A_0 + 56L) = 10L;
			*(A_0 + 40L) = 0;
			*(A_0 + 48L) = 0L;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CRowset<ATL::CDynamicAccessor>.{dtor}), A_0);
			throw;
		}
		return A_0;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00002514 File Offset: 0x00001914
	internal unsafe static int ATL.CBulkRowset<ATL::CDynamicAccessor>.ReleaseRows(CBulkRowset<ATL::CDynamicAccessor>* A_0)
	{
		*(A_0 + 72L) = 0L;
		*(A_0 + 24L) = 0L;
		ulong num = (ulong)(*(A_0 + 64L));
		*(A_0 + 64L) = 0L;
		long num2 = *A_0;
		return calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.UInt64 modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), (IntPtr)num2, num, *(A_0 + 48L), 0L, 0L, 0L, (IntPtr)(*(*num2 + 48L)));
	}

	// Token: 0x06000033 RID: 51 RVA: 0x000047F4 File Offset: 0x00003BF4
	internal unsafe static void ATL.CRowset<ATL::CDynamicAccessor>.Close(CRowset<ATL::CDynamicAccessor>* A_0)
	{
		ulong num = (ulong)(*(A_0 + 32L));
		if (num != 0UL)
		{
			ulong num2 = (ulong)(*A_0);
			if (((((num2 == 0UL) ? 1 : 0) == 0) ? 1 : 0) != 0)
			{
				IRowset* ptr = num2;
				CDynamicAccessor* ptr2 = num;
				<Module>.ATL.CDynamicAccessor.FreeRecordMemory(ptr2, ptr);
				<Module>.ATL.CAccessorBase.ReleaseAccessors(ptr2, (IUnknown*)ptr);
			}
			CXMLAccessor* ptr3 = *(A_0 + 32L);
			if (ptr3 != null)
			{
				<Module>.ATL.CDynamicAccessor.Close(ptr3);
				<Module>.delete((void*)ptr3, 72UL);
			}
			*(A_0 + 32L) = 0L;
		}
		ulong num3 = (ulong)(*A_0);
		if (((((num3 == 0UL) ? 1 : 0) == 0) ? 1 : 0) != 0)
		{
			<Module>.ATL.CDynamicAccessor.FreeRecordMemory(*(A_0 + 16L), num3);
			<Module>.ATL.CRowset<ATL::CDynamicAccessor>.ReleaseRows(A_0);
			IRowset* ptr4 = *A_0;
			if (ptr4 != null)
			{
				*A_0 = 0L;
				IRowset* ptr5 = ptr4;
				uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr5, (IntPtr)(*(*(long*)ptr5 + 16L)));
			}
			IRowsetChange* ptr6 = *(A_0 + 8L);
			if (ptr6 != null)
			{
				*(A_0 + 8L) = 0L;
				IRowsetChange* ptr7 = ptr6;
				uint num5 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr7, (IntPtr)(*(*(long*)ptr7 + 16L)));
			}
		}
	}

	// Token: 0x06000034 RID: 52 RVA: 0x000048BC File Offset: 0x00003CBC
	internal unsafe static void ATL.CRowset<ATL::CDynamicAccessor>.{dtor}(CRowset<ATL::CDynamicAccessor>* A_0)
	{
		try
		{
			try
			{
				<Module>.ATL.CRowset<ATL::CDynamicAccessor>.Close(A_0);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowsetChange>.{dtor}), (void*)(A_0 + (byte*)8L));
				throw;
			}
			CComPtr<IRowsetChange>* ptr = A_0 + 8L;
			ulong num = (ulong)(*ptr);
			if (num != 0UL)
			{
				ulong num2 = num;
				uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), A_0);
			throw;
		}
		ulong num4 = (ulong)(*A_0);
		if (num4 != 0UL)
		{
			ulong num5 = num4;
			uint num6 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num5, (IntPtr)(*(*num5 + 16L)));
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002C8C File Offset: 0x0000208C
	internal unsafe static void ATL.CComPtr<IRowset>.{dtor}(CComPtr<IRowset>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002CB8 File Offset: 0x000020B8
	internal unsafe static void ATL.CComPtr<IRowsetChange>.{dtor}(CComPtr<IRowsetChange>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002564 File Offset: 0x00001964
	internal unsafe static int ATL.CRowset<ATL::CDynamicAccessor>.ReleaseRows(CRowset<ATL::CDynamicAccessor>* A_0)
	{
		int num = 0;
		CRowset<ATL::CDynamicAccessor>* ptr = A_0 + 24L;
		if (*ptr != 0L)
		{
			long num2 = *A_0;
			num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.UInt64 modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), (IntPtr)num2, 1UL, ptr, 0L, 0L, 0L, (IntPtr)(*(*num2 + 48L)));
			*ptr = 0L;
		}
		return num;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000025A0 File Offset: 0x000019A0
	internal unsafe static void ATL.CComPtrBase<IRowset>.{dtor}(CComPtrBase<IRowset>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000052A8 File Offset: 0x000046A8
	internal unsafe static int ?A0x47c6cda6.memcpy_s(void* _Destination, ulong _DestinationSize, void* _Source, ulong _SourceSize)
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

	// Token: 0x0600003A RID: 58 RVA: 0x00005318 File Offset: 0x00004718
	internal unsafe static int IsEqualGUID(_GUID* rguid1, _GUID* rguid2)
	{
		ulong num = 16UL;
		_GUID* ptr = rguid2;
		byte b = *rguid1;
		byte b2 = *rguid2;
		if (b >= b2)
		{
			long num2 = rguid1 - rguid2;
			while (b <= b2)
			{
				if (num == 1UL)
				{
					return 1;
				}
				num -= 1UL;
				ptr += 1L;
				b = *(num2 + ptr);
				b2 = *ptr;
				if (b < b2)
				{
					break;
				}
			}
		}
		return 0;
	}

	// Token: 0x0600003B RID: 59 RVA: 0x0000536C File Offset: 0x0000476C
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool ATL.CAccessorBase.IsAutoAccessor(CAccessorBase* A_0, uint nAccessor)
	{
		if (nAccessor >= *(A_0 + 8L))
		{
			return 0;
		}
		ulong num = (ulong)(*A_0);
		if (num == 0UL)
		{
			return 0;
		}
		return *(num + nAccessor * 16UL + 8UL);
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000067C0 File Offset: 0x00005BC0
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool ATL.CDynamicAccessor.GetLength(CDynamicAccessor* A_0, ulong nColumn, ulong* pLength)
	{
		if (pLength == null)
		{
			<Module>.ATL.AtlThrowImpl(-2147467259);
		}
		if (<Module>.ATL.CDynamicAccessor.TranslateColumnNo(A_0, ref nColumn) != null)
		{
			long num = (long)(nColumn * 80UL + (ulong)(*(A_0 + 40L)));
			*pLength = (ulong)(*(((*(num + 8L) + *(num + 32L) + 7L) & -8L) + *(A_0 + 16L)));
			return 1;
		}
		return 0;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00006820 File Offset: 0x00005C20
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool ATL.CDynamicAccessor.GetStatus(CDynamicAccessor* A_0, ulong nColumn, uint* pStatus)
	{
		if (pStatus == null)
		{
			<Module>.ATL.AtlThrowImpl(-2147467259);
		}
		if (<Module>.ATL.CDynamicAccessor.TranslateColumnNo(A_0, ref nColumn) != null)
		{
			long num = (long)(nColumn * 80UL + (ulong)(*(A_0 + 40L)));
			*(int*)pStatus = *(((((*(num + 8L) + *(num + 32L) + 7L) & -8L) + 8L) & -4L) + *(A_0 + 16L));
			return 1;
		}
		return 0;
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000053A0 File Offset: 0x000047A0
	[return: MarshalAs(UnmanagedType.U1)]
	internal unsafe static bool ATL.CDynamicAccessor.TranslateColumnNo(CDynamicAccessor* A_0, ulong* nColumn)
	{
		ulong num4;
		if (*(A_0 + 56L) != 0)
		{
			uint num = 0;
			ulong num2 = (ulong)(*(A_0 + 24L));
			if (0UL < num2)
			{
				long num3 = *(A_0 + 40L);
				num4 = (ulong)(*nColumn);
				while (*(num3 + (long)(num * 80UL) + 16L) != (long)num4)
				{
					num++;
					if (num >= num2)
					{
						return 0;
					}
				}
				*nColumn = (long)num;
				return 1;
			}
			return 0;
		}
		ulong num5 = (ulong)(*(*(A_0 + 40L) + 16L));
		num4 = (ulong)(*nColumn);
		if (num4 > num5 + (ulong)(*(A_0 + 24L)) - 1UL)
		{
			return 0;
		}
		*nColumn = (long)(num4 - num5);
		return 1;
	}

	// Token: 0x0600003F RID: 63 RVA: 0x0000643C File Offset: 0x0000583C
	internal unsafe static void ATL.CComPtr<IDBCreateSession>.{dtor}(CComPtr<IDBCreateSession>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000040 RID: 64 RVA: 0x0000688C File Offset: 0x00005C8C
	internal unsafe static int MsolapWrapper.CDbSchemaRowset.CreateSchemaRowset(CDbSchemaRowset* A_0, IDBSchemaRowset** schemaRowset)
	{
		CComPtr<IDBCreateSession> ccomPtr<IDBCreateSession> = 0L;
		int num2;
		try
		{
			long num = *(A_0 + 168L);
			num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), (IntPtr)num, ref <Module>._GUID_0c733a5d_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IDBCreateSession>), (IntPtr)(*(*num)));
			if (num2 >= 0)
			{
				num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),IUnknown**), ccomPtr<IDBCreateSession>, null, ref <Module>._GUID_0c733a7b_2a1c_11ce_ade5_00aa0044773d, (IUnknown**)schemaRowset, (IntPtr)(*(*ccomPtr<IDBCreateSession> + 24L)));
				if (num2 >= 0)
				{
					*(A_0 + 60L) = 1;
				}
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBCreateSession>.{dtor}), (void*)(&ccomPtr<IDBCreateSession>));
			throw;
		}
		if (ccomPtr<IDBCreateSession> != null)
		{
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBCreateSession>, (IntPtr)(*(*ccomPtr<IDBCreateSession> + 16L)));
		}
		return num2;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x00007C14 File Offset: 0x00007014
	internal unsafe static void MsolapWrapper.CDbSchemaRowset.{dtor}(CDbSchemaRowset* A_0)
	{
		try
		{
			try
			{
				<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Close(A_0);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CDataSource.{dtor}), (void*)(A_0 + (byte*)168L));
				throw;
			}
			CDataSource* ptr = A_0 + 168L;
			ulong num = (ulong)(*ptr);
			if (num != 0UL)
			{
				ulong num2 = num;
				uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{dtor}), A_0);
			throw;
		}
		<Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.{dtor}(A_0);
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000077BC File Offset: 0x00006BBC
	internal unsafe static int MsolapWrapper.CDbSchemaRowset.GetRowset(CDbSchemaRowset* A_0, _GUID schema, tagVARIANT* restrictions, uint restrictionsLength, tagDBPROPSET* rgPropSets, uint cPropSets)
	{
		CComPtr<IDBSchemaRowset> ccomPtr<IDBSchemaRowset> = 0L;
		int num2;
		try
		{
			int num = <Module>.MsolapWrapper.CDbSchemaRowset.CreateSchemaRowset(A_0, (IDBSchemaRowset**)(&ccomPtr<IDBSchemaRowset>));
			if (num >= 0)
			{
				CDbSchemaRowset* ptr = A_0 + 72L;
				num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagVARIANT modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagDBPROPSET* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst),IUnknown**), ccomPtr<IDBSchemaRowset>, null, ref schema, restrictionsLength, restrictions, ref <Module>._GUID_0c733a7c_2a1c_11ce_ade5_00aa0044773d, cPropSets, rgPropSets, ptr, (IntPtr)(*(*ccomPtr<IDBSchemaRowset> + 24L)));
				if (num >= 0 && *ptr != 0L)
				{
					num2 = <Module>.ATL.CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>.Bind(A_0);
					goto IL_004D;
				}
			}
			num2 = num;
			IL_004D:;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBSchemaRowset>.{dtor}), (void*)(&ccomPtr<IDBSchemaRowset>));
			throw;
		}
		if (ccomPtr<IDBSchemaRowset> != null)
		{
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBSchemaRowset>, (IntPtr)(*(*ccomPtr<IDBSchemaRowset> + 16L)));
		}
		return num2;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x00006930 File Offset: 0x00005D30
	internal unsafe static int MsolapWrapper.CDbSchemaRowset.GetSchemas(CDbSchemaRowset* A_0, uint* pcSchemas, _GUID** prgSchemas, uint** prgRestrictionSupport)
	{
		CComPtr<IDBSchemaRowset> ccomPtr<IDBSchemaRowset> = 0L;
		int num2;
		try
		{
			int num = <Module>.MsolapWrapper.CDbSchemaRowset.CreateSchemaRowset(A_0, (IDBSchemaRowset**)(&ccomPtr<IDBSchemaRowset>));
			if (num < 0)
			{
				num2 = num;
			}
			else
			{
				num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*,_GUID**,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)**), ccomPtr<IDBSchemaRowset>, pcSchemas, prgSchemas, prgRestrictionSupport, (IntPtr)(*(*ccomPtr<IDBSchemaRowset> + 32L)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBSchemaRowset>.{dtor}), (void*)(&ccomPtr<IDBSchemaRowset>));
			throw;
		}
		if (ccomPtr<IDBSchemaRowset> != null)
		{
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBSchemaRowset>, (IntPtr)(*(*ccomPtr<IDBSchemaRowset> + 16L)));
		}
		return num2;
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IDBSchemaRowset>.{dtor}(CComPtr<IDBSchemaRowset>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00005458 File Offset: 0x00004858
	internal unsafe static void MsolapWrapper.CSchemasWrapper.{dtor}(CSchemasWrapper* A_0)
	{
		<Module>.CoTaskMemFree(*(A_0 + 8L));
		<Module>.CoTaskMemFree(*(A_0 + 16L));
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IParentRowset>.{dtor}(CComPtr<IParentRowset>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000047 RID: 71 RVA: 0x00007748 File Offset: 0x00006B48
	internal unsafe static int MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.MoveFirst(CChapterBulkRowset<ATL::CDynamicAccessor>* A_0)
	{
		*(A_0 + 40L) = 0;
		<Module>.ATL.CDynamicAccessor.FreeRecordMemory(*(A_0 + 16L), *A_0);
		<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.ReleaseRows(A_0);
		long num = *A_0;
		int num2 = *(A_0 + 80L);
		ulong num3;
		if (num2 == 0)
		{
			num3 = 0UL;
		}
		else
		{
			if (num2 == 1)
			{
				Utils.ThrowError(WrapperErrorSource.PowerBI, "Expected state ChapterSet before using chapter rowset.");
			}
			num3 = (ulong)(*(A_0 + 88L));
		}
		int num4 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64), (IntPtr)num, num3, (IntPtr)(*(*num + 56L)));
		if (num4 < 0)
		{
			return num4;
		}
		return <Module>.MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.MoveNext(A_0, 0L, true);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x0000750C File Offset: 0x0000690C
	internal unsafe static int MsolapWrapper.CChapterBulkRowset<ATL::CDynamicAccessor>.MoveNext(CChapterBulkRowset<ATL::CDynamicAccessor>* A_0, long lSkip, [MarshalAs(UnmanagedType.U1)] bool bForward)
	{
		<Module>.ATL.CDynamicAccessor.FreeRecordMemory(*(A_0 + 16L), *A_0);
		int num2;
		int num = (num2 = 1);
		if (!bForward)
		{
			num2 = -num;
		}
		long num3 = *(A_0 + 72L);
		long num4 = (long)num2 + num3 + lSkip;
		CChapterBulkRowset<ATL::CDynamicAccessor>* ptr = A_0 + 64L;
		ulong num5 = (ulong)(*ptr);
		if (num5 != 0UL)
		{
			if (num4 >= (long)num5)
			{
				lSkip = ((!bForward) ? (2L - *(A_0 + 56L)) : 0L) - (long)num5 + num4;
			}
			else
			{
				if (num4 >= 0L)
				{
					goto IL_00FD;
				}
				long num6;
				if (bForward)
				{
					num6 = 0L;
				}
				else
				{
					num6 = 2L - *(A_0 + 56L);
				}
				lSkip = num3 - (long)num5 + num6 + num4;
			}
		}
		num4 = 0L;
		int num7 = *(A_0 + 40L);
		if (num7 != 0 && num7 != 265920)
		{
			return num7;
		}
		<Module>.ATL.CBulkRowset<ATL::CDynamicAccessor>.ReleaseRows(A_0);
		long num8 = *A_0;
		int num9 = *(A_0 + 80L);
		ulong num10;
		if (num9 == 0)
		{
			num10 = 0UL;
		}
		else
		{
			if (num9 == 1)
			{
				Utils.ThrowError(WrapperErrorSource.PowerBI, "Expected state ChapterSet before using chapter rowset.");
			}
			num10 = (ulong)(*(A_0 + 88L));
		}
		int num11 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.Int64,System.Int64,System.UInt64*,System.UInt64**), (IntPtr)num8, num10, lSkip, *(A_0 + 56L), ptr, A_0 / sizeof(ulong*) + 48L, (IntPtr)(*(*num8 + 40L)));
		*(A_0 + 40L) = num11;
		if (num11 >= 0)
		{
			num5 = (ulong)(*ptr);
			if (num5 != 0UL)
			{
				if (!bForward)
				{
					num4 = (long)(num5 - 1UL);
					goto IL_00FD;
				}
				goto IL_00FD;
			}
		}
		return num11;
		IL_00FD:
		*(A_0 + 72L) = num4;
		*(A_0 + 24L) = *(num4 * 8L + *(A_0 + 48L));
		return <Module>.ATL.CRowset<ATL::CDynamicAccessor>.GetData(A_0);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000066F0 File Offset: 0x00005AF0
	internal unsafe static IRowset* ATL.CComPtr<IRowset>.=(CComPtr<IRowset>* A_0, CComPtr<IRowset>* lp)
	{
		ulong num = (ulong)(*lp);
		if (*A_0 != (long)num)
		{
			IRowset* ptr = num;
			CComPtr<IRowset> ccomPtr<IRowset> = ptr;
			if (ptr != null)
			{
				IRowset* ptr2 = ptr;
				uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 8L)));
			}
			try
			{
				ccomPtr<IRowset> = *A_0;
				*A_0 = ptr;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IRowset>.{dtor}), (void*)(&ccomPtr<IRowset>));
				throw;
			}
			if (ccomPtr<IRowset> != null)
			{
				uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IRowset>, (IntPtr)(*(*ccomPtr<IRowset> + 16L)));
			}
		}
		return *A_0;
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00006770 File Offset: 0x00005B70
	internal unsafe static int ATL.CRowset<ATL::CDynamicAccessor>.GetData(CRowset<ATL::CDynamicAccessor>* A_0)
	{
		int num = 0;
		uint num2 = *(*(A_0 + 16L) + 8L);
		uint num3 = 0;
		if (0 < num2)
		{
			for (;;)
			{
				if (<Module>.ATL.CAccessorBase.IsAutoAccessor(*(A_0 + 16L), num3) != null)
				{
					num = <Module>.ATL.CRowset<ATL::CDynamicAccessor>.GetData(A_0, num3);
					if (num < 0)
					{
						break;
					}
				}
				num3++;
				if (num3 >= num2)
				{
					return num;
				}
			}
			return num;
		}
		return num;
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000063BC File Offset: 0x000057BC
	internal unsafe static int ATL.CRowset<ATL::CDynamicAccessor>.GetData(CRowset<ATL::CDynamicAccessor>* A_0, int nAccessor)
	{
		long num = *A_0;
		long num2 = *(A_0 + 16L);
		byte* ptr = *(num2 + 16L);
		CAccessorBase* ptr2 = num2;
		ulong num3;
		if (nAccessor >= *(ptr2 + 8L))
		{
			num3 = 0UL;
		}
		else
		{
			num3 = (ulong)(*((ulong)nAccessor * 16UL + (ulong)(*ptr2)));
		}
		return calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt64,System.UInt64,System.Void*), (IntPtr)num, (ulong)(*(A_0 + 24L)), num3, (void*)ptr, (IntPtr)(*(*num + 32L)));
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00008534 File Offset: 0x00007934
	internal unsafe static CComBSTR* ATL.CComBSTR.{ctor}(CComBSTR* A_0, char* pSrc)
	{
		if (pSrc == null)
		{
			*A_0 = 0L;
		}
		else
		{
			char* ptr = <Module>.SysAllocString(pSrc);
			*A_0 = ptr;
			if (((ptr == null) ? 1 : 0) != 0)
			{
				<Module>.ATL.AtlThrowImpl(-2147024882);
			}
		}
		return A_0;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IDBProperties>.{dtor}(CComPtr<IDBProperties>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x00008748 File Offset: 0x00007B48
	internal unsafe static int ATL.CSession.Open(CSession* A_0, CDataSource* ds, tagDBPROPSET* pPropSet, uint ulPropSets)
	{
		CComPtr<IDBCreateSession> ccomPtr<IDBCreateSession> = 0L;
		int num2;
		try
		{
			long num = *ds;
			num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), (IntPtr)num, ref <Module>._GUID_0c733a5d_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<IDBCreateSession>), (IntPtr)(*(*num)));
			if (num2 >= 0)
			{
				num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,IUnknown*,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),IUnknown**), ccomPtr<IDBCreateSession>, null, ref <Module>._GUID_0c733a69_2a1c_11ce_ade5_00aa0044773d, A_0, (IntPtr)(*(*ccomPtr<IDBCreateSession> + 24L)));
				if (pPropSet != null && num2 >= 0 && ((((*A_0 == 0L) ? 1 : 0) == 0) ? 1 : 0) != 0)
				{
					ulPropSets = ((ulPropSets == 0) ? 1 : ulPropSets);
					CComPtr<ISessionProperties> ccomPtr<ISessionProperties> = 0L;
					try
					{
						ulong num3 = (ulong)(*A_0);
						num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), (IntPtr)num3, ref <Module>._GUID_0c733a85_2a1c_11ce_ade5_00aa0044773d, (void**)(&ccomPtr<ISessionProperties>), (IntPtr)(*(*num3)));
						if (num2 >= 0)
						{
							goto IL_00A9;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<ISessionProperties>.{dtor}), (void*)(&ccomPtr<ISessionProperties>));
						throw;
					}
					if (ccomPtr<ISessionProperties> != null)
					{
						uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<ISessionProperties>, (IntPtr)(*(*ccomPtr<ISessionProperties> + 16L)));
						goto IL_00E5;
					}
					goto IL_00E5;
					IL_00A9:
					try
					{
						num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagDBPROPSET* modopt(System.Runtime.CompilerServices.IsConst) modopt(System.Runtime.CompilerServices.IsConst)), ccomPtr<ISessionProperties>, ulPropSets, pPropSet, (IntPtr)(*(*ccomPtr<ISessionProperties> + 32L)));
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<ISessionProperties>.{dtor}), (void*)(&ccomPtr<ISessionProperties>));
						throw;
					}
					if (ccomPtr<ISessionProperties> != null)
					{
						uint num5 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<ISessionProperties>, (IntPtr)(*(*ccomPtr<ISessionProperties> + 16L)));
					}
				}
			}
			IL_00E5:;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBCreateSession>.{dtor}), (void*)(&ccomPtr<IDBCreateSession>));
			throw;
		}
		if (ccomPtr<IDBCreateSession> != null)
		{
			uint num6 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBCreateSession>, (IntPtr)(*(*ccomPtr<IDBCreateSession> + 16L)));
		}
		return num2;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<ISessionProperties>.{dtor}(CComPtr<ISessionProperties>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000093E8 File Offset: 0x000087E8
	internal unsafe static void MsolapWrapper.NativeProxyTracer.Trace(NativeProxyTracer* A_0, __MIDL_IMsolapTracer_0001 in_eTraceLevel, char* in_bstrMessage)
	{
		string text = Marshal.PtrToStringBSTR((IntPtr)in_bstrMessage);
		<Module>.gcroot<MsolapWrapper::MsolapTracerBase\u0020^>.->(A_0 + 8L).Trace((TraceLevel)in_eTraceLevel, text);
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00009388 File Offset: 0x00008788
	internal unsafe static uint MsolapWrapper.NativeProxyTracer.AddRef(NativeProxyTracer* A_0)
	{
		NativeProxyTracer* ptr = A_0 + 16L;
		Interlocked.Increment(ptr);
		return *ptr;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000093AC File Offset: 0x000087AC
	internal unsafe static uint MsolapWrapper.NativeProxyTracer.Release(NativeProxyTracer* A_0)
	{
		NativeProxyTracer* ptr = A_0 + 16L;
		uint num = Interlocked.Decrement(ptr);
		if (0 == *ptr)
		{
			<Module>.gcroot<MsolapWrapper::MsolapTracerBase\u0020^>.{dtor}(A_0 + 8L);
			<Module>.delete(A_0, 24UL);
		}
		return num;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00009370 File Offset: 0x00008770
	internal unsafe static int MsolapWrapper.NativeProxyTracer.QueryInterface(NativeProxyTracer* A_0, _GUID* riid, void** ppvObj)
	{
		return -2147467262;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IClassFactory>.{dtor}(CComPtr<IClassFactory>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000055 RID: 85 RVA: 0x0000868C File Offset: 0x00007A8C
	internal unsafe static void ATL.CComPtr<IErrorLookup>.{dtor}(CComPtr<IErrorLookup>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IASTracerContext>.{dtor}(CComPtr<IASTracerContext>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00009418 File Offset: 0x00008818
	[SecuritySafeCritical]
	internal unsafe static MsolapTracerBase gcroot<MsolapWrapper::MsolapTracerBase\u0020^>.->(gcroot<MsolapWrapper::MsolapTracerBase\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00008504 File Offset: 0x00007904
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<MsolapWrapper::MsolapTracerBase\u0020^>.{dtor}(gcroot<MsolapWrapper::MsolapTracerBase\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00008E48 File Offset: 0x00008248
	internal unsafe static IDBInitialize* ATL.CComPtr<IDBInitialize>.=(CComPtr<IDBInitialize>* A_0, CComPtr<IDBInitialize>* lp)
	{
		if (*A_0 != *lp)
		{
			CComPtr<IDBInitialize> ccomPtr<IDBInitialize> = 0L;
			IDBInitialize* ptr;
			try
			{
				ptr = *lp;
				*lp = 0L;
				ccomPtr<IDBInitialize> = ptr;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtrBase<IDBInitialize>.{dtor}), (void*)(&ccomPtr<IDBInitialize>));
				throw;
			}
			try
			{
				ccomPtr<IDBInitialize> = *A_0;
				*A_0 = ptr;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IDBInitialize>.{dtor}), (void*)(&ccomPtr<IDBInitialize>));
				throw;
			}
			if (ccomPtr<IDBInitialize> != null)
			{
				uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IDBInitialize>, (IntPtr)(*(*ccomPtr<IDBInitialize> + 16L)));
			}
		}
		return *A_0;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00009D64 File Offset: 0x00009164
	internal unsafe static char* A2WBSTR(sbyte* lp, int nLen)
	{
		if (lp != null && nLen != 0)
		{
			CAtlSafeAllocBufferManager<ATL::CCRTAllocator> catlSafeAllocBufferManager<ATL::CCRTAllocator> = 0L;
			char* ptr2;
			try
			{
				int num = <Module>.MultiByteToWideChar(3U, 0, lp, nLen, null, 0);
				int num2 = num;
				if (nLen == -1)
				{
					num2 = num - 1;
				}
				char* ptr = <Module>.SysAllocStringLen(null, (uint)num2);
				if (ptr != null && <Module>.MultiByteToWideChar(3U, 0, lp, nLen, ptr, num) != num)
				{
					<Module>.SysFreeString(ptr);
					ptr2 = null;
				}
				else
				{
					ptr2 = ptr;
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL._ATL_SAFE_ALLOCA_IMPL.CAtlSafeAllocBufferManager<ATL::CCRTAllocator>.{dtor}), (void*)(&catlSafeAllocBufferManager<ATL::CCRTAllocator>));
				throw;
			}
			return ptr2;
		}
		return 0L;
	}

	// Token: 0x0600005B RID: 91 RVA: 0x00009DF4 File Offset: 0x000091F4
	internal unsafe static CComBSTR* ATL.CComBSTR.{ctor}(CComBSTR* A_0, int nSize, char* sz)
	{
		if (nSize < 0)
		{
			<Module>.ATL.AtlThrowImpl(-2147024809);
		}
		if (nSize == 0)
		{
			*A_0 = 0L;
		}
		else
		{
			char* ptr = <Module>.SysAllocStringLen(sz, (uint)nSize);
			*A_0 = ptr;
			if (((ptr == null) ? 1 : 0) != 0)
			{
				<Module>.ATL.AtlThrowImpl(-2147024882);
			}
		}
		return A_0;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00009E3C File Offset: 0x0000923C
	internal unsafe static CComBSTR* ATL.CComBSTR.{ctor}(CComBSTR* A_0, sbyte* pSrc)
	{
		if (pSrc != null)
		{
			char* ptr = <Module>.A2WBSTR(pSrc, -1);
			*A_0 = ptr;
			if (((ptr == null) ? 1 : 0) == 0)
			{
				return A_0;
			}
			<Module>.ATL.AtlThrowImpl(-2147024882);
		}
		*A_0 = 0L;
		return A_0;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00009E74 File Offset: 0x00009274
	internal unsafe static int ATL.CDBErrorInfo.GetErrorRecords(CDBErrorInfo* A_0, uint* pcRecords)
	{
		if (pcRecords == null)
		{
			return -2147467259;
		}
		IErrorInfo* ptr = *A_0;
		if (ptr != null)
		{
			*A_0 = 0L;
			IErrorInfo* ptr2 = ptr;
			uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 16L)));
		}
		CDBErrorInfo* ptr3 = A_0 + 8L;
		CComPtrBase<IErrorRecords>* ptr4 = ptr3;
		IErrorRecords* ptr5 = *ptr4;
		if (ptr5 != null)
		{
			*ptr4 = 0L;
			IErrorRecords* ptr6 = ptr5;
			uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr6, (IntPtr)(*(*(long*)ptr6 + 16L)));
		}
		if (<Module>.GetErrorInfo(0, A_0) == 1)
		{
			return -2147467259;
		}
		long num3 = *A_0;
		if (calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),System.Void**), (IntPtr)num3, ref <Module>._GUID_0c733a67_2a1c_11ce_ade5_00aa0044773d, ptr3, (IntPtr)(*(*num3))) < 0)
		{
			*(int*)pcRecords = 1;
			return 0;
		}
		long num4 = *ptr3;
		return calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), (IntPtr)num4, pcRecords, (IntPtr)(*(*num4 + 64L)));
	}

	// Token: 0x0600005E RID: 94 RVA: 0x0000A598 File Offset: 0x00009998
	internal unsafe static int ATL.CDBErrorInfo.GetAllErrorInfo(CDBErrorInfo* A_0, uint ulRecordNum, uint lcid, char** pbstrDescription, char** pbstrSource, _GUID* pguid, uint* pdwHelpContext, char** pbstrHelpFile)
	{
		CComPtr<IErrorInfo> ccomPtr<IErrorInfo> = 0L;
		int num3;
		try
		{
			ulong num = (ulong)(*(A_0 + 8L));
			if (((((num == 0UL) ? 1 : 0) == 0) ? 1 : 0) != 0)
			{
				int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),IErrorInfo**), (IntPtr)num, ulRecordNum, lcid, (IErrorInfo**)(&ccomPtr<IErrorInfo>), (IntPtr)(*(*num + 48L)));
				if (num2 < 0)
				{
					num3 = num2;
					goto IL_00E7;
				}
			}
			else
			{
				<Module>.ATL.CComPtr<IErrorInfo>.=(ref ccomPtr<IErrorInfo>, A_0);
			}
			if (pbstrDescription != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char**), ccomPtr<IErrorInfo>, pbstrDescription, (IntPtr)(*(*ccomPtr<IErrorInfo> + 40L))) < 0)
			{
				*(long*)pbstrDescription = 0L;
			}
			if (pguid != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,_GUID*), ccomPtr<IErrorInfo>, pguid, (IntPtr)(*(*ccomPtr<IErrorInfo> + 24L))) < 0)
			{
				_GUID guid;
				initblk(ref guid, 0, 16L);
				cpblk(pguid, ref guid, 16);
			}
			if (pdwHelpContext != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong)*), ccomPtr<IErrorInfo>, pdwHelpContext, (IntPtr)(*(*ccomPtr<IErrorInfo> + 56L))) < 0)
			{
				*(int*)pdwHelpContext = 0;
			}
			if (pbstrHelpFile != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char**), ccomPtr<IErrorInfo>, pbstrHelpFile, (IntPtr)(*(*ccomPtr<IErrorInfo> + 48L))) < 0)
			{
				*(long*)pbstrHelpFile = 0L;
			}
			if (pbstrSource != null && calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Char**), ccomPtr<IErrorInfo>, pbstrSource, (IntPtr)(*(*ccomPtr<IErrorInfo> + 32L))) < 0)
			{
				*(long*)pbstrSource = 0L;
			}
			num3 = 0;
			IL_00E7:;
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IErrorInfo>.{dtor}), (void*)(&ccomPtr<IErrorInfo>));
			throw;
		}
		if (ccomPtr<IErrorInfo> != null)
		{
			uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IErrorInfo>, (IntPtr)(*(*ccomPtr<IErrorInfo> + 16L)));
		}
		return num3;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IErrorInfo>.{dtor}(CComPtr<IErrorInfo>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000060 RID: 96 RVA: 0x0000A13C File Offset: 0x0000953C
	internal unsafe static void ATL.CDBErrorInfo.{dtor}(CDBErrorInfo* A_0)
	{
		try
		{
			CComPtr<IErrorRecords>* ptr = A_0 + 8L;
			ulong num = (ulong)(*ptr);
			if (num != 0UL)
			{
				ulong num2 = num;
				uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
			}
		}
		catch
		{
			<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IErrorInfo>.{dtor}), A_0);
			throw;
		}
		ulong num4 = (ulong)(*A_0);
		if (num4 != 0UL)
		{
			ulong num5 = num4;
			uint num6 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num5, (IntPtr)(*(*num5 + 16L)));
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IErrorRecords>.{dtor}(CComPtr<IErrorRecords>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000062 RID: 98 RVA: 0x00002758 File Offset: 0x00001B58
	internal unsafe static void ATL.CComPtr<IASErrorInfo>.{dtor}(CComPtr<IASErrorInfo>* A_0)
	{
		ulong num = (ulong)(*A_0);
		if (num != 0UL)
		{
			ulong num2 = num;
			uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), (IntPtr)num2, (IntPtr)(*(*num2 + 16L)));
		}
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000A518 File Offset: 0x00009918
	internal unsafe static IErrorInfo* ATL.CComPtr<IErrorInfo>.=(CComPtr<IErrorInfo>* A_0, CComPtr<IErrorInfo>* lp)
	{
		ulong num = (ulong)(*lp);
		if (*A_0 != (long)num)
		{
			IErrorInfo* ptr = num;
			CComPtr<IErrorInfo> ccomPtr<IErrorInfo> = ptr;
			if (ptr != null)
			{
				IErrorInfo* ptr2 = ptr;
				uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 8L)));
			}
			try
			{
				ccomPtr<IErrorInfo> = *A_0;
				*A_0 = ptr;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IErrorInfo>.{dtor}), (void*)(&ccomPtr<IErrorInfo>));
				throw;
			}
			if (ccomPtr<IErrorInfo> != null)
			{
				uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IErrorInfo>, (IntPtr)(*(*ccomPtr<IErrorInfo> + 16L)));
			}
		}
		return *A_0;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x00009D38 File Offset: 0x00009138
	internal unsafe static void ATL._ATL_SAFE_ALLOCA_IMPL.CAtlSafeAllocBufferManager<ATL::CCRTAllocator>.{dtor}(CAtlSafeAllocBufferManager<ATL::CCRTAllocator>* A_0)
	{
		if (*A_0 != 0L)
		{
			do
			{
				ulong num = (ulong)(*A_0);
				void* ptr = num;
				*A_0 = *num;
				<Module>.free(ptr);
			}
			while (*A_0 != 0L);
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000C228 File Offset: 0x0000B628
	internal static void <CrtImplementationDetails>.ThrowNestedModuleLoadException(Exception innerException, Exception nestedException)
	{
		throw new ModuleLoadExceptionHandlerException("A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n", innerException, nestedException);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000BBB0 File Offset: 0x0000AFB0
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage)
	{
		throw new ModuleLoadException(errorMessage);
	}

	// Token: 0x06000067 RID: 103 RVA: 0x0000BBCC File Offset: 0x0000AFCC
	internal static void <CrtImplementationDetails>.ThrowModuleLoadException(string errorMessage, Exception innerException)
	{
		throw new ModuleLoadException(errorMessage, innerException);
	}

	// Token: 0x06000068 RID: 104 RVA: 0x0000BCFC File Offset: 0x0000B0FC
	internal static void <CrtImplementationDetails>.RegisterModuleUninitializer(EventHandler handler)
	{
		ModuleUninitializer._ModuleUninitializer.AddHandler(handler);
	}

	// Token: 0x06000069 RID: 105 RVA: 0x0000BD1C File Offset: 0x0000B11C
	[SecuritySafeCritical]
	internal unsafe static Guid <CrtImplementationDetails>.FromGUID(_GUID* guid)
	{
		Guid guid2 = new Guid((uint)(*guid), *(guid + 4L), *(guid + 6L), *(guid + 8L), *(guid + 9L), *(guid + 10L), *(guid + 11L), *(guid + 12L), *(guid + 13L), *(guid + 14L), *(guid + 15L));
		return guid2;
	}

	// Token: 0x0600006A RID: 106 RVA: 0x0000BD74 File Offset: 0x0000B174
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

	// Token: 0x0600006B RID: 107 RVA: 0x0000BDFC File Offset: 0x0000B1FC
	internal unsafe static void __release_appdomain(IUnknown* ppUnk)
	{
		uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ppUnk, (IntPtr)(*(*(long*)ppUnk + 16L)));
	}

	// Token: 0x0600006C RID: 108 RVA: 0x0000BE20 File Offset: 0x0000B220
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

	// Token: 0x0600006D RID: 109 RVA: 0x0000BE88 File Offset: 0x0000B288
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.DoCallBackInDefaultDomain(delegate* unmanaged[Cdecl, Cdecl]<void*, int> function, void* cookie)
	{
		Guid guid = <Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02);
		ICLRRuntimeHost* ptr = (ICLRRuntimeHost*)RuntimeEnvironment.GetRuntimeInterfaceAsIntPtr(<Module>.<CrtImplementationDetails>.FromGUID(ref <Module>._GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02), guid).ToPointer();
		try
		{
			AppDomain appDomain = <Module>.<CrtImplementationDetails>.GetDefaultDomain();
			long num = *(*(long*)ptr + 64L);
			uint id = (uint)appDomain.Id;
			int num2 = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl) (System.Void*),System.Void*), ptr, id, function, cookie, (IntPtr)num);
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

	// Token: 0x0600006E RID: 110 RVA: 0x0000BF1C File Offset: 0x0000B31C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool __scrt_is_safe_for_managed_code()
	{
		uint _scrt_native_dllmain_reason = <Module>.__scrt_native_dllmain_reason;
		if (_scrt_native_dllmain_reason != 0U && _scrt_native_dllmain_reason != 1U)
		{
			return 1;
		}
		return 0;
	}

	// Token: 0x0600006F RID: 111 RVA: 0x0000BF54 File Offset: 0x0000B354
	[SecuritySafeCritical]
	internal unsafe static int <CrtImplementationDetails>.DefaultDomain.DoNothing(void* cookie)
	{
		GC.KeepAlive(int.MaxValue);
		return 0;
	}

	// Token: 0x06000070 RID: 112 RVA: 0x0000BF78 File Offset: 0x0000B378
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

	// Token: 0x06000071 RID: 113 RVA: 0x0000BFD4 File Offset: 0x0000B3D4
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

	// Token: 0x06000072 RID: 114 RVA: 0x0000C05C File Offset: 0x0000B45C
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

	// Token: 0x06000073 RID: 115 RVA: 0x0000C09C File Offset: 0x0000B49C
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.DefaultDomain.NeedsUninitialization()
	{
		return <Module>.?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000C0B4 File Offset: 0x0000B4B4
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.DefaultDomain.Initialize()
	{
		<Module>.<CrtImplementationDetails>.DoCallBackInDefaultDomain(<Module>.__unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z, null);
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00001008 File Offset: 0x00000408
	internal static void ?A0xe7265dad.??__E?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00001024 File Offset: 0x00000424
	internal static void ?A0xe7265dad.??__E?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA@@YMXXZ()
	{
		<Module>.?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA = 0;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00001040 File Offset: 0x00000440
	internal static void ?A0xe7265dad.??__E?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA@@YMXXZ()
	{
		<Module>.?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA = false;
	}

	// Token: 0x06000078 RID: 120 RVA: 0x0000105C File Offset: 0x0000045C
	internal static void ?A0xe7265dad.??__E?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x00001078 File Offset: 0x00000478
	internal static void ?A0xe7265dad.??__E?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x00001094 File Offset: 0x00000494
	internal static void ?A0xe7265dad.??__E?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x000010B0 File Offset: 0x000004B0
	internal static void ?A0xe7265dad.??__E?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A@@YMXXZ()
	{
		<Module>.?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)0;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x0000C284 File Offset: 0x0000B684
	[SecuritySafeCritical]
	[DebuggerStepThrough]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeVtables(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during vtable initialization.\n");
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)1;
		<Module>._initterm_m((delegate*<void*>*)(&<Module>.__xi_vt_a), (delegate*<void*>*)(&<Module>.__xi_vt_z));
		<Module>.?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A = (Progress)2;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x0000C2C0 File Offset: 0x0000B6C0
	[SecuritySafeCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeDefaultAppDomain(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load while attempting to initialize the default appdomain.\n");
		<Module>.<CrtImplementationDetails>.DefaultDomain.Initialize();
	}

	// Token: 0x0600007E RID: 126 RVA: 0x0000C2E4 File Offset: 0x0000B6E4
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

	// Token: 0x0600007F RID: 127 RVA: 0x0000C378 File Offset: 0x0000B778
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

	// Token: 0x06000080 RID: 128 RVA: 0x0000C3C0 File Offset: 0x0000B7C0
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

	// Token: 0x06000081 RID: 129 RVA: 0x0000C400 File Offset: 0x0000B800
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.InitializeUninitializer(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.=(A_0, "The C++ module failed to load during registration for the unload events.\n");
		<Module>.<CrtImplementationDetails>.RegisterModuleUninitializer(new EventHandler(<Module>.<CrtImplementationDetails>.LanguageSupport.DomainUnload));
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000C430 File Offset: 0x0000B830
	[DebuggerStepThrough]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
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

	// Token: 0x06000083 RID: 131 RVA: 0x0000C0D4 File Offset: 0x0000B4D4
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.LanguageSupport.UninitializeAppDomain()
	{
		<Module>._app_exit_callback();
	}

	// Token: 0x06000084 RID: 132 RVA: 0x0000C0EC File Offset: 0x0000B4EC
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

	// Token: 0x06000085 RID: 133 RVA: 0x0000C12C File Offset: 0x0000B52C
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

	// Token: 0x06000086 RID: 134 RVA: 0x0000C168 File Offset: 0x0000B568
	[PrePrepareMethod]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x06000087 RID: 135 RVA: 0x0000C53C File Offset: 0x0000B93C
	[DebuggerStepThrough]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
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

	// Token: 0x06000088 RID: 136 RVA: 0x0000C5B8 File Offset: 0x0000B9B8
	[SecurityCritical]
	internal unsafe static LanguageSupport* <CrtImplementationDetails>.LanguageSupport.{ctor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{ctor}(A_0);
		return A_0;
	}

	// Token: 0x06000089 RID: 137 RVA: 0x0000C5D8 File Offset: 0x0000B9D8
	[SecurityCritical]
	internal unsafe static void <CrtImplementationDetails>.LanguageSupport.{dtor}(LanguageSupport* A_0)
	{
		<Module>.gcroot<System::String\u0020^>.{dtor}(A_0);
	}

	// Token: 0x0600008A RID: 138 RVA: 0x0000C5F4 File Offset: 0x0000B9F4
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

	// Token: 0x0600008B RID: 139 RVA: 0x0000C6B8 File Offset: 0x0000BAB8
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

	// Token: 0x0600008C RID: 140 RVA: 0x00009418 File Offset: 0x00008818
	[SecuritySafeCritical]
	internal unsafe static string gcroot<System::String\u0020^>..PE$AAVString@System@@(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		return ((GCHandle)intPtr).Target;
	}

	// Token: 0x0600008D RID: 141 RVA: 0x0000C1AC File Offset: 0x0000B5AC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.=(gcroot<System::String\u0020^>* A_0, string t)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Target = t;
		return A_0;
	}

	// Token: 0x0600008E RID: 142 RVA: 0x00008504 File Offset: 0x00007904
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static void gcroot<System::String\u0020^>.{dtor}(gcroot<System::String\u0020^>* A_0)
	{
		IntPtr intPtr = new IntPtr(*A_0);
		((GCHandle)intPtr).Free();
		*A_0 = 0L;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000C1D8 File Offset: 0x0000B5D8
	[DebuggerStepThrough]
	[SecuritySafeCritical]
	internal unsafe static gcroot<System::String\u0020^>* gcroot<System::String\u0020^>.{ctor}(gcroot<System::String\u0020^>* A_0)
	{
		*A_0 = ((IntPtr)GCHandle.Alloc(null)).ToPointer();
		return A_0;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x0000C738 File Offset: 0x0000BB38
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

	// Token: 0x06000091 RID: 145 RVA: 0x0000C784 File Offset: 0x0000BB84
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

	// Token: 0x06000092 RID: 146 RVA: 0x0000CA74 File Offset: 0x0000BE74
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void <CrtImplementationDetails>.AtExitLock._lock_Construct(object value)
	{
		<Module>.?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA = null;
		<Module>.<CrtImplementationDetails>.AtExitLock._lock_Set(value);
	}

	// Token: 0x06000093 RID: 147 RVA: 0x0000C7B8 File Offset: 0x0000BBB8
	[SecurityCritical]
	[DebuggerStepThrough]
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

	// Token: 0x06000094 RID: 148 RVA: 0x0000C80C File Offset: 0x0000BC0C
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

	// Token: 0x06000095 RID: 149 RVA: 0x0000C838 File Offset: 0x0000BC38
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

	// Token: 0x06000096 RID: 150 RVA: 0x0000C868 File Offset: 0x0000BC68
	[SecurityCritical]
	[DebuggerStepThrough]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool <CrtImplementationDetails>.AtExitLock.IsInitialized()
	{
		return (<Module>.<CrtImplementationDetails>.AtExitLock._lock_Get() != null) ? 1 : 0;
	}

	// Token: 0x06000097 RID: 151 RVA: 0x0000CA94 File Offset: 0x0000BE94
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

	// Token: 0x06000098 RID: 152 RVA: 0x0000C888 File Offset: 0x0000BC88
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

	// Token: 0x06000099 RID: 153 RVA: 0x0000CACC File Offset: 0x0000BECC
	[DebuggerStepThrough]
	[SecurityCritical]
	[return: MarshalAs(UnmanagedType.U1)]
	internal static bool ?A0xd35bd18a.__alloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.AddRef();
		return <Module>.<CrtImplementationDetails>.AtExitLock.IsInitialized();
	}

	// Token: 0x0600009A RID: 154 RVA: 0x0000C8B4 File Offset: 0x0000BCB4
	[DebuggerStepThrough]
	[SecurityCritical]
	internal static void ?A0xd35bd18a.__dealloc_global_lock()
	{
		<Module>.<CrtImplementationDetails>.AtExitLock.RemoveRef();
	}

	// Token: 0x0600009B RID: 155 RVA: 0x0000C8CC File Offset: 0x0000BCCC
	[SecurityCritical]
	internal unsafe static void _exit_callback()
	{
		if (<Module>.?A0xd35bd18a.__exit_list_size != 0UL)
		{
			delegate*<void>* ptr = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0xd35bd18a.__onexitbegin_m);
			delegate*<void>* ptr2 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0xd35bd18a.__onexitend_m);
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
						delegate*<void>* ptr5 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0xd35bd18a.__onexitbegin_m);
						delegate*<void>* ptr6 = (delegate*<void>*)<Module>.DecodePointer((void*)<Module>.?A0xd35bd18a.__onexitend_m);
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
			<Module>.?A0xd35bd18a.__dealloc_global_lock();
		}
	}

	// Token: 0x0600009C RID: 156 RVA: 0x0000CAEC File Offset: 0x0000BEEC
	[DebuggerStepThrough]
	[SecurityCritical]
	internal unsafe static int _initatexit_m()
	{
		int num = 0;
		if (<Module>.?A0xd35bd18a.__alloc_global_lock() == 1)
		{
			<Module>.?A0xd35bd18a.__onexitbegin_m = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.?A0xd35bd18a.__onexitend_m = <Module>.?A0xd35bd18a.__onexitbegin_m;
			<Module>.?A0xd35bd18a.__exit_list_size = 32UL;
			num = 1;
		}
		return num;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x0000CB38 File Offset: 0x0000BF38
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static int _initatexit_app_domain()
	{
		if (<Module>.?A0xd35bd18a.__alloc_global_lock() == 1)
		{
			<Module>.__onexitbegin_app_domain = (delegate*<void>*)<Module>.EncodePointer(Marshal.AllocHGlobal(256).ToPointer());
			<Module>.__onexitend_app_domain = <Module>.__onexitbegin_app_domain;
			<Module>.__exit_list_size_app_domain = 32UL;
		}
		return 1;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000C980 File Offset: 0x0000BD80
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
				<Module>.?A0xd35bd18a.__dealloc_global_lock();
			}
		}
	}

	// Token: 0x0600009F RID: 159
	[SuppressUnmanagedCodeSecurity]
	[SecurityCritical]
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* DecodePointer(void* _Ptr);

	// Token: 0x060000A0 RID: 160
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
	[SecurityCritical]
	[SuppressUnmanagedCodeSecurity]
	[DllImport("KERNEL32.dll")]
	public unsafe static extern void* EncodePointer(void* _Ptr);

	// Token: 0x060000A1 RID: 161 RVA: 0x0000CB84 File Offset: 0x0000BF84
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

	// Token: 0x060000A2 RID: 162 RVA: 0x0000CBB8 File Offset: 0x0000BFB8
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

	// Token: 0x060000A3 RID: 163 RVA: 0x0000CBE8 File Offset: 0x0000BFE8
	[DebuggerStepThrough]
	internal static ModuleHandle <CrtImplementationDetails>.ThisModule.Handle()
	{
		return typeof(ThisModule).Module.ModuleHandle;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x0000CC44 File Offset: 0x0000C044
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

	// Token: 0x060000A5 RID: 165 RVA: 0x0000CC10 File Offset: 0x0000C010
	[SecurityCritical]
	[DebuggerStepThrough]
	internal unsafe static delegate*<void*> <CrtImplementationDetails>.ThisModule.ResolveMethod<void\u0020const\u0020*\u0020__clrcall(void)>(delegate*<void*> methodToken)
	{
		return <Module>.<CrtImplementationDetails>.ThisModule.Handle().ResolveMethodHandle(methodToken).GetFunctionPointer()
			.ToPointer();
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x00001140 File Offset: 0x00000540
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int ATL._AtlInitializeCriticalSectionEx(_RTL_CRITICAL_SECTION*, uint, uint);

	// Token: 0x060000A7 RID: 167 RVA: 0x0000CF6C File Offset: 0x0000C36C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* new[](ulong);

	// Token: 0x060000A8 RID: 168 RVA: 0x0000CF28 File Offset: 0x0000C328
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(ulong);

	// Token: 0x060000A9 RID: 169 RVA: 0x0000CED1 File Offset: 0x0000C2D1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int SafeArrayDestroy(tagSAFEARRAY*);

	// Token: 0x060000AA RID: 170 RVA: 0x0000CF78 File Offset: 0x0000C378
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* @new(ulong, nothrow_t*);

	// Token: 0x060000AB RID: 171 RVA: 0x0000CF1C File Offset: 0x0000C31C
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete[](void*);

	// Token: 0x060000AC RID: 172 RVA: 0x0000CF10 File Offset: 0x0000C310
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* new[](ulong, nothrow_t*);

	// Token: 0x060000AD RID: 173 RVA: 0x0000B0EC File Offset: 0x0000A4EC
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void delete(void*, ulong);

	// Token: 0x060000AE RID: 174 RVA: 0x0000CE89 File Offset: 0x0000C289
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void CoTaskMemFree(void*);

	// Token: 0x060000AF RID: 175 RVA: 0x0000CEDD File Offset: 0x0000C2DD
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int VariantClear(tagVARIANT*);

	// Token: 0x060000B0 RID: 176 RVA: 0x0000CEAD File Offset: 0x0000C2AD
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void VariantInit(tagVARIANT*);

	// Token: 0x060000B1 RID: 177 RVA: 0x0000CEC5 File Offset: 0x0000C2C5
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* SysAllocStringByteLen(sbyte*, uint);

	// Token: 0x060000B2 RID: 178 RVA: 0x0000CEB9 File Offset: 0x0000C2B9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern uint SysStringByteLen(char*);

	// Token: 0x060000B3 RID: 179 RVA: 0x0000CE1E File Offset: 0x0000C21E
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern uint GetLastError();

	// Token: 0x060000B4 RID: 180 RVA: 0x0000D168 File Offset: 0x0000C568
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void _CxxThrowException(void*, _s__ThrowInfo*);

	// Token: 0x060000B5 RID: 181 RVA: 0x0000CEA1 File Offset: 0x0000C2A1
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void SysFreeString(char*);

	// Token: 0x060000B6 RID: 182 RVA: 0x0000CD82 File Offset: 0x0000C182
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void free(void*);

	// Token: 0x060000B7 RID: 183 RVA: 0x0000CE41 File Offset: 0x0000C241
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int CompareStringOrdinal(char*, int, char*, int, int);

	// Token: 0x060000B8 RID: 184 RVA: 0x0000CE06 File Offset: 0x0000C206
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _invalid_parameter_noinfo();

	// Token: 0x060000B9 RID: 185 RVA: 0x0000CE12 File Offset: 0x0000C212
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int* _errno();

	// Token: 0x060000BA RID: 186 RVA: 0x0000D1A4 File Offset: 0x0000C5A4
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern double log2(double);

	// Token: 0x060000BB RID: 187 RVA: 0x0000CE4D File Offset: 0x0000C24D
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern HINSTANCE__* LoadLibraryExW(char*, void*, uint);

	// Token: 0x060000BC RID: 188 RVA: 0x0000CE59 File Offset: 0x0000C259
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern delegate* unmanaged[Cdecl, Cdecl]<long> GetProcAddress(HINSTANCE__*, sbyte*);

	// Token: 0x060000BD RID: 189 RVA: 0x0000CEE9 File Offset: 0x0000C2E9
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* SysAllocString(char*);

	// Token: 0x060000BE RID: 190 RVA: 0x0000CE71 File Offset: 0x0000C271
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int GetLocaleInfoEx(char*, uint, char*, int);

	// Token: 0x060000BF RID: 191 RVA: 0x0000CF01 File Offset: 0x0000C301
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int GetErrorInfo(uint, IErrorInfo**);

	// Token: 0x060000C0 RID: 192 RVA: 0x0000CE95 File Offset: 0x0000C295
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int StringFromCLSID(_GUID*, char**);

	// Token: 0x060000C1 RID: 193 RVA: 0x0000CEF5 File Offset: 0x0000C2F5
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern char* SysAllocStringLen(char*, uint);

	// Token: 0x060000C2 RID: 194 RVA: 0x0000CE65 File Offset: 0x0000C265
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int MultiByteToWideChar(uint, uint, sbyte*, int, char*, int);

	// Token: 0x060000C3 RID: 195 RVA: 0x0000BF40 File Offset: 0x0000B340
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern void* _getFiberPtrId();

	// Token: 0x060000C4 RID: 196 RVA: 0x0000CDFA File Offset: 0x0000C1FA
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void _cexit();

	// Token: 0x060000C5 RID: 197 RVA: 0x0000CE7D File Offset: 0x0000C27D
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void Sleep(uint);

	// Token: 0x060000C6 RID: 198 RVA: 0x0000D1B0 File Offset: 0x0000C5B0
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal static extern void abort();

	// Token: 0x060000C7 RID: 199 RVA: 0x0000B0F8 File Offset: 0x0000A4F8
	[SuppressUnmanagedCodeSecurity]
	[MethodImpl(MethodImplOptions.Unmanaged | MethodImplOptions.PreserveSig, MethodCodeType = MethodCodeType.Native)]
	internal static extern void __security_init_cookie();

	// Token: 0x060000C8 RID: 200 RVA: 0x0000D174 File Offset: 0x0000C574
	[SuppressUnmanagedCodeSecurity]
	[DllImport("", CallingConvention = CallingConvention.Cdecl, SetLastError = true)]
	[MethodImpl(MethodImplOptions.Unmanaged, MethodCodeType = MethodCodeType.Native)]
	internal unsafe static extern int __FrameUnwindFilter(_EXCEPTION_POINTERS*);

	// Token: 0x04000001 RID: 1 RVA: 0x0000F380 File Offset: 0x0000CD80
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a05_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000002 RID: 2 RVA: 0x0000F370 File Offset: 0x0000CD70
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a79_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000003 RID: 3 RVA: 0x0000F360 File Offset: 0x0000CD60
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a27_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000004 RID: 4 RVA: 0x0000F340 File Offset: 0x0000CD40
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a55_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000005 RID: 5 RVA: 0x0000F330 File Offset: 0x0000CD30
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a11_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000006 RID: 6 RVA: 0x0000F390 File Offset: 0x0000CD90
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a93_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000007 RID: 7 RVA: 0x0000F3F0 File Offset: 0x0000CDF0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a90_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000008 RID: 8 RVA: 0x0000F310 File Offset: 0x0000CD10
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID IID_IASErrorInfo;

	// Token: 0x04000009 RID: 9 RVA: 0x0000F300 File Offset: 0x0000CD00
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID DBPROPSET_MDCOMMAND;

	// Token: 0x0400000A RID: 10 RVA: 0x0000F3B0 File Offset: 0x0000CDB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a63_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400000B RID: 11 RVA: 0x0000F3A0 File Offset: 0x0000CDA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a1d_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400000C RID: 12 RVA: 0x0000F320 File Offset: 0x0000CD20
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a7c_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400000D RID: 13 RVA: 0x0000F3D0 File Offset: 0x0000CDD0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0000000c_0000_0000_c000_000000000046;

	// Token: 0x0400000E RID: 14 RVA: 0x0000F3C0 File Offset: 0x0000CDC0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a30_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400000F RID: 15 RVA: 0x0000F350 File Offset: 0x0000CD50
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a8c_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000010 RID: 16 RVA: 0x0000F3E0 File Offset: 0x0000CDE0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_00000000_0000_0000_c000_000000000046;

	// Token: 0x04000011 RID: 17 RVA: 0x00026A00 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?m_bInitFailed@CAtlBaseModule@ATL@@2_NA;

	// Token: 0x04000012 RID: 18 RVA: 0x00025550 File Offset: 0x00022F50
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_8 _CTA1?AVCAtlException@ATL@@;

	// Token: 0x04000013 RID: 19 RVA: 0x00025528 File Offset: 0x00022F28
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0?AVCAtlException@ATL@@@84;

	// Token: 0x04000014 RID: 20 RVA: 0x000260D0 File Offset: 0x00023AD0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_24 ??_R0?AVCAtlException@ATL@@@8;

	// Token: 0x04000015 RID: 21 RVA: 0x00025560 File Offset: 0x00022F60
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI1?AVCAtlException@ATL@@;

	// Token: 0x04000016 RID: 22 RVA: 0x0000F400 File Offset: 0x0000CE00
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0BC@$$CBD ??_C@_0BC@JDLGBILA@DllGetClassObject@;

	// Token: 0x04000017 RID: 23 RVA: 0x0000F418 File Offset: 0x0000CE18
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733aaa_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000018 RID: 24 RVA: 0x0000F438 File Offset: 0x0000CE38
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a7b_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000019 RID: 25 RVA: 0x0000F428 File Offset: 0x0000CE28
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a5d_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400001A RID: 26 RVA: 0x0000F488 File Offset: 0x0000CE88
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0L@$$CB_W ??_C@_1BG@NDPOPHON@?$AAS?$AAe?$AAt?$AA?5?$AAT?$AAr?$AAa?$AAc?$AAe?$AAr@;

	// Token: 0x0400001B RID: 27 RVA: 0x00024CB0 File Offset: 0x000226B0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_24 ??_R2NativeProxyTracer@MsolapWrapper@@8;

	// Token: 0x0400001C RID: 28 RVA: 0x00024C58 File Offset: 0x00022658
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_16 ??_R2IMsolapTracer@@8;

	// Token: 0x0400001D RID: 29 RVA: 0x00024C08 File Offset: 0x00022608
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__RTTIBaseClassArray$_extraBytes_8 ??_R2IUnknown@@8;

	// Token: 0x0400001E RID: 30 RVA: 0x00024BE0 File Offset: 0x000225E0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@NativeProxyTracer@MsolapWrapper@@8;

	// Token: 0x0400001F RID: 31 RVA: 0x00024C88 File Offset: 0x00022688
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@IMsolapTracer@@8;

	// Token: 0x04000020 RID: 32 RVA: 0x00024C30 File Offset: 0x00022630
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIBaseClassDescriptor ??_R1A@?0A@EA@IUnknown@@8;

	// Token: 0x04000021 RID: 33 RVA: 0x00024CD0 File Offset: 0x000226D0
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3NativeProxyTracer@MsolapWrapper@@8;

	// Token: 0x04000022 RID: 34 RVA: 0x00026190 File Offset: 0x00023B90
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_38 ??_R0?AVNativeProxyTracer@MsolapWrapper@@@8;

	// Token: 0x04000023 RID: 35 RVA: 0x00024C70 File Offset: 0x00022670
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3IMsolapTracer@@8;

	// Token: 0x04000024 RID: 36 RVA: 0x000261C8 File Offset: 0x00023BC8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_20 ??_R0?AUIMsolapTracer@@@8;

	// Token: 0x04000025 RID: 37 RVA: 0x00024C18 File Offset: 0x00022618
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTIClassHierarchyDescriptor ??_R3IUnknown@@8;

	// Token: 0x04000026 RID: 38 RVA: 0x000261F0 File Offset: 0x00023BF0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_15 ??_R0?AUIUnknown@@@8;

	// Token: 0x04000027 RID: 39 RVA: 0x00024CE8 File Offset: 0x000226E8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__RTTICompleteObjectLocator2 ??_R4NativeProxyTracer@MsolapWrapper@@6B@;

	// Token: 0x04000028 RID: 40 RVA: 0x0000F448 File Offset: 0x0000CE48
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_62b9e9ad_e0e5_4ab2_9626_249e06fa5b9e;

	// Token: 0x04000029 RID: 41 RVA: 0x00026008 File Offset: 0x00023A08
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY04Q6AXXZ ??_7NativeProxyTracer@MsolapWrapper@@6B@;

	// Token: 0x0400002A RID: 42 RVA: 0x0000F478 File Offset: 0x0000CE78
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a85_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400002B RID: 43 RVA: 0x0000F468 File Offset: 0x0000CE68
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a69_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400002C RID: 44 RVA: 0x0000F458 File Offset: 0x0000CE58
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a8a_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x0400002D RID: 45 RVA: 0x00026058 File Offset: 0x00023A58
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<NativeProxyTracer*, __MIDL_IMsolapTracer_0001, char*, void> __m2mep@?Trace@NativeProxyTracer@MsolapWrapper@@$$FUEAAXW4__MIDL_IMsolapTracer_0001@@QEA_W@Z;

	// Token: 0x0400002E RID: 46 RVA: 0x00026038 File Offset: 0x00023A38
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<NativeProxyTracer*, uint> __m2mep@?AddRef@NativeProxyTracer@MsolapWrapper@@$$FUEAAKXZ;

	// Token: 0x0400002F RID: 47 RVA: 0x00026048 File Offset: 0x00023A48
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<NativeProxyTracer*, uint> __m2mep@?Release@NativeProxyTracer@MsolapWrapper@@$$FUEAAKXZ;

	// Token: 0x04000030 RID: 48 RVA: 0x00026028 File Offset: 0x00023A28
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<NativeProxyTracer*, _GUID*, void**, int> __m2mep@?QueryInterface@NativeProxyTracer@MsolapWrapper@@$$FUEAAJAEBU_GUID@@PEAPEAX@Z;

	// Token: 0x04000031 RID: 49 RVA: 0x00028BBC File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static int __@@_PchSym_@00@UzUPdlipUBUhOlyqOcGEivovzhvUhjoUxlnnlmUnhlozkdizkkviUnhlozkdizkkviOexckilqUhgwzucOlyq@4B2008FD98C1DD4;

	// Token: 0x04000032 RID: 50 RVA: 0x0000F4B0 File Offset: 0x0000CEB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY05$$CBD ??_C@_05JJLPJMLG@en?9US@;

	// Token: 0x04000033 RID: 51 RVA: 0x000255A8 File Offset: 0x00022FA8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_s__CatchableTypeArray$_extraBytes_8 _CTA1J;

	// Token: 0x04000034 RID: 52 RVA: 0x00025580 File Offset: 0x00022F80
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__CatchableType _CT??_R0J@84;

	// Token: 0x04000035 RID: 53 RVA: 0x000260F8 File Offset: 0x00023AF8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $_TypeDescriptor$_extraBytes_3 ??_R0J@8;

	// Token: 0x04000036 RID: 54 RVA: 0x000255B8 File Offset: 0x00022FB8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _s__ThrowInfo _TI1J;

	// Token: 0x04000037 RID: 55 RVA: 0x0000F4A0 File Offset: 0x0000CEA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_0c733a67_2a1c_11ce_ade5_00aa0044773d;

	// Token: 0x04000038 RID: 56 RVA: 0x0000F590 File Offset: 0x0000CF90
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6723_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x04000039 RID: 57 RVA: 0x0000F580 File Offset: 0x0000CF80
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_cb2f6722_ab3a_11d2_9c40_00c04fa30a3e;

	// Token: 0x0400003A RID: 58
	[FixedAddressValueType]
	internal static int ?Uninitialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x0400003B RID: 59 RVA: 0x0000F2A8 File Offset: 0x0000CCA8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xe7265dad.?Uninitialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400003C RID: 60
	[FixedAddressValueType]
	internal static Progress ?InitializedNative@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400003D RID: 61 RVA: 0x0000F2C0 File Offset: 0x0000CCC0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xe7265dad.?InitializedNative$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400003E RID: 62 RVA: 0x0000F5A0 File Offset: 0x0000CFA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06c_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x0400003F RID: 63 RVA: 0x0000F5B0 File Offset: 0x0000CFB0
	// Note: this field is marked with 'hasfieldrva'.
	internal static __s_GUID _GUID_90f1a06e_7712_4762_86b5_7a5eba6bdb02;

	// Token: 0x04000040 RID: 64
	[FixedAddressValueType]
	internal static Progress ?InitializedPerAppDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000041 RID: 65 RVA: 0x00029C64 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?Entered@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000042 RID: 66 RVA: 0x000260A4 File Offset: 0x00023AA4
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasNative@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x04000043 RID: 67 RVA: 0x00029C67 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedPerProcess@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000044 RID: 68 RVA: 0x00029C60 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '9460301'.
	internal static int ?Count@AllDomains@<CrtImplementationDetails>@@2HA;

	// Token: 0x04000045 RID: 69
	[FixedAddressValueType]
	internal static int ?Initialized@CurrentDomain@<CrtImplementationDetails>@@$$Q2HA;

	// Token: 0x04000046 RID: 70 RVA: 0x00029C66 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNativeFromCCTOR@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x04000047 RID: 71
	[FixedAddressValueType]
	internal static bool ?IsDefaultDomain@CurrentDomain@<CrtImplementationDetails>@@$$Q2_NA;

	// Token: 0x04000048 RID: 72
	[FixedAddressValueType]
	internal static Progress ?InitializedVtables@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x04000049 RID: 73 RVA: 0x00029C65 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of 'True'.
	internal static bool ?InitializedNative@DefaultDomain@<CrtImplementationDetails>@@2_NA;

	// Token: 0x0400004A RID: 74
	[FixedAddressValueType]
	internal static Progress ?InitializedPerProcess@CurrentDomain@<CrtImplementationDetails>@@$$Q2W4Progress@2@A;

	// Token: 0x0400004B RID: 75 RVA: 0x000260A0 File Offset: 0x00023AA0
	// Note: this field is marked with 'hasfieldrva'.
	internal static TriBool ?hasPerProcess@DefaultDomain@<CrtImplementationDetails>@@0W4TriBool@2@A;

	// Token: 0x0400004C RID: 76 RVA: 0x0000F2E8 File Offset: 0x0000CCE8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_z;

	// Token: 0x0400004D RID: 77 RVA: 0x0000F2F8 File Offset: 0x0000CCF8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_z;

	// Token: 0x0400004E RID: 78 RVA: 0x0000F2C8 File Offset: 0x0000CCC8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xe7265dad.?InitializedPerProcess$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x0400004F RID: 79 RVA: 0x0000F298 File Offset: 0x0000CC98
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_a;

	// Token: 0x04000050 RID: 80 RVA: 0x0000F2D8 File Offset: 0x0000CCD8
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_ma_z;

	// Token: 0x04000051 RID: 81 RVA: 0x0000F2D0 File Offset: 0x0000CCD0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xe7265dad.?InitializedPerAppDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000052 RID: 82 RVA: 0x0000F2F0 File Offset: 0x0000CCF0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xi_vt_a;

	// Token: 0x04000053 RID: 83 RVA: 0x0000F2A0 File Offset: 0x0000CCA0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xe7265dad.?Initialized$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000054 RID: 84 RVA: 0x0000F2E0 File Offset: 0x0000CCE0
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY00Q6MPEBXXZ __xc_mp_a;

	// Token: 0x04000055 RID: 85 RVA: 0x0000F2B8 File Offset: 0x0000CCB8
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xe7265dad.?InitializedVtables$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000056 RID: 86 RVA: 0x0000F2B0 File Offset: 0x0000CCB0
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void> ?A0xe7265dad.?IsDefaultDomain$initializer$@CurrentDomain@<CrtImplementationDetails>@@$$Q2P6MXXZEA;

	// Token: 0x04000057 RID: 87 RVA: 0x000260A8 File Offset: 0x00023AA8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000058 RID: 88 RVA: 0x000260B8 File Offset: 0x00023AB8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static delegate*<void*, int> __m2mep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x04000059 RID: 89 RVA: 0x0000F5C0 File Offset: 0x0000CFC0
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?DoNothing@DefaultDomain@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400005A RID: 90 RVA: 0x0000F5C8 File Offset: 0x0000CFC8
	// Note: this field is marked with 'hasfieldrva'.
	public unsafe static int** __unep@?_UninitializeDefaultDomain@LanguageSupport@<CrtImplementationDetails>@@$$FCAJPEAX@Z;

	// Token: 0x0400005B RID: 91 RVA: 0x00029DD0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0xd35bd18a.__onexitbegin_m;

	// Token: 0x0400005C RID: 92 RVA: 0x00029DC8 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '12894362189'.
	internal static ulong ?A0xd35bd18a.__exit_list_size;

	// Token: 0x0400005D RID: 93
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitend_app_domain;

	// Token: 0x0400005E RID: 94
	[FixedAddressValueType]
	internal unsafe static void* ?_lock@AtExitLock@<CrtImplementationDetails>@@$$Q0PEAXEA;

	// Token: 0x0400005F RID: 95
	[FixedAddressValueType]
	internal static int ?_ref_count@AtExitLock@<CrtImplementationDetails>@@$$Q0HA;

	// Token: 0x04000060 RID: 96 RVA: 0x00029DD8 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static delegate*<void>* ?A0xd35bd18a.__onexitend_m;

	// Token: 0x04000061 RID: 97
	[FixedAddressValueType]
	internal static ulong __exit_list_size_app_domain;

	// Token: 0x04000062 RID: 98
	[FixedAddressValueType]
	internal unsafe static delegate*<void>* __onexitbegin_app_domain;

	// Token: 0x04000063 RID: 99 RVA: 0x0000F4F8 File Offset: 0x0000CEF8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID DBGUID_DEFAULT;

	// Token: 0x04000064 RID: 100 RVA: 0x0000F4E8 File Offset: 0x0000CEE8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID DBPROPSET_ROWSET;

	// Token: 0x04000065 RID: 101 RVA: 0x0000F4B8 File Offset: 0x0000CEB8
	// Note: this field is marked with 'hasfieldrva'.
	internal static tagDBID DB_NULLID;

	// Token: 0x04000066 RID: 102 RVA: 0x0000F558 File Offset: 0x0000CF58
	// Note: this field is marked with 'hasfieldrva'.
	internal static nothrow_t std.nothrow;

	// Token: 0x04000067 RID: 103
	// Note: this field is marked with 'hasfieldrva'.
	internal static _IMAGE_DOS_HEADER __ImageBase;

	// Token: 0x04000068 RID: 104 RVA: 0x00029DF0 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static CAtlBaseModule ATL._AtlBaseModule;

	// Token: 0x04000069 RID: 105 RVA: 0x0000F550 File Offset: 0x0000CF50
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY01Q6AXXZ ??_7type_info@@6B@;

	// Token: 0x0400006A RID: 106 RVA: 0x0000F528 File Offset: 0x0000CF28
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID IID_IRowset;

	// Token: 0x0400006B RID: 107 RVA: 0x0000F518 File Offset: 0x0000CF18
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID IID_IErrorLookup;

	// Token: 0x0400006C RID: 108 RVA: 0x0000F508 File Offset: 0x0000CF08
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID IID_IDBInitialize;

	// Token: 0x0400006D RID: 109 RVA: 0x0000F538 File Offset: 0x0000CF38
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID IID_IClassFactory;

	// Token: 0x0400006E RID: 110 RVA: 0x0000F4D8 File Offset: 0x0000CED8
	// Note: this field is marked with 'hasfieldrva'.
	internal static _GUID DBPROPSET_DBINIT;

	// Token: 0x0400006F RID: 111 RVA: 0x0000F270 File Offset: 0x0000CC70
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_z;

	// Token: 0x04000070 RID: 112 RVA: 0x00029C00 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal static __scrt_native_startup_state __scrt_current_native_startup_state;

	// Token: 0x04000071 RID: 113 RVA: 0x00029C08 File Offset: 0x00000000
	// Note: this field is marked with 'hasfieldrva'.
	internal unsafe static void* __scrt_native_startup_lock;

	// Token: 0x04000072 RID: 114 RVA: 0x0000F250 File Offset: 0x0000CC50
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_a;

	// Token: 0x04000073 RID: 115 RVA: 0x0000F268 File Offset: 0x0000CC68
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AHXZ __xi_a;

	// Token: 0x04000074 RID: 116 RVA: 0x0002606C File Offset: 0x00023A6C
	// Note: this field is marked with 'hasfieldrva' and has an initial value of '4294967295'.
	internal static uint __scrt_native_dllmain_reason;

	// Token: 0x04000075 RID: 117 RVA: 0x0000F260 File Offset: 0x0000CC60
	// Note: this field is marked with 'hasfieldrva'.
	internal static $ArrayType$$$BY0A@P6AXXZ __xc_z;
}
