using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x0200000D RID: 13
	internal static class Utils
	{
		// Token: 0x0600012B RID: 299 RVA: 0x0000A8BC File Offset: 0x00009CBC
		internal unsafe static DataErrorInfo GetDataErrorInfo(int hr)
		{
			CDBErrorInfo cdberrorInfo = 0L;
			try
			{
				CComPtr<IErrorRecords>* ptr = (ref cdberrorInfo) + 8;
				*((ref cdberrorInfo) + 8) = 0L;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IErrorInfo>.{dtor}), (void*)(&cdberrorInfo));
				throw;
			}
			DataErrorInfo dataErrorInfo;
			try
			{
				dataErrorInfo = new DataErrorInfo();
				PrimaryErrorInfo primaryError = Utils.GetPrimaryError(ref cdberrorInfo);
				if (primaryError != null)
				{
					Utils.PopulateDataErrorInfo(ref cdberrorInfo, dataErrorInfo, (int)primaryError.Index);
					dataErrorInfo.Type = primaryError.Type;
					dataErrorInfo.TypeOrigin = primaryError.TypeOrigin;
				}
				else
				{
					dataErrorInfo.Description = "No OLE DB Error Information found.";
					dataErrorInfo.TypeOrigin = WrapperErrorSourceOrigin.MsolapWrapper;
				}
				dataErrorInfo.Hresult = hr;
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CDBErrorInfo.{dtor}), (void*)(&cdberrorInfo));
				throw;
			}
			<Module>.ATL.CDBErrorInfo.{dtor}(ref cdberrorInfo);
			return dataErrorInfo;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A1AC File Offset: 0x000095AC
		internal unsafe static PrimaryErrorInfo GetPrimaryError(CDBErrorInfo* errorInfo)
		{
			uint num2;
			int num = <Module>.ATL.CDBErrorInfo.GetErrorRecords(errorInfo, &num2);
			if (num >= 0 && num2 > 0)
			{
				PrimaryErrorInfo primaryErrorInfo = new PrimaryErrorInfo();
				primaryErrorInfo.Index = 0U;
				primaryErrorInfo.Type = (__MIDL_IASErrorInfo_0001)0;
				primaryErrorInfo.TypeOrigin = WrapperErrorSourceOrigin.MsolapWrapper;
				uint num3 = 0;
				if (0 < num2)
				{
					CDBErrorInfo* ptr = errorInfo + 8L;
					CComPtr<IASErrorInfo> ccomPtr<IASErrorInfo>;
					for (;;)
					{
						ccomPtr<IASErrorInfo> = 0L;
						try
						{
							long num4 = *ptr;
							num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),_GUID modopt(System.Runtime.CompilerServices.IsConst)* modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced),IUnknown**), (IntPtr)num4, num3, ref <Module>.IID_IASErrorInfo, (IUnknown**)(&ccomPtr<IASErrorInfo>), (IntPtr)(*(*num4 + 40L)));
							if (num >= 0 && ((((ccomPtr<IASErrorInfo> == 0L) ? 1 : 0) == 0) ? 1 : 0) != 0)
							{
								short num5;
								num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int16*), ccomPtr<IASErrorInfo>, &num5, (IntPtr)(*(*ccomPtr<IASErrorInfo> + 32L)));
								if (num >= 0 && num5 == -1)
								{
									break;
								}
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IASErrorInfo>.{dtor}), (void*)(&ccomPtr<IASErrorInfo>));
							throw;
						}
						if (ccomPtr<IASErrorInfo> != null)
						{
							uint num6 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IASErrorInfo>, (IntPtr)(*(*ccomPtr<IASErrorInfo> + 16L)));
						}
						num3++;
						if (num3 >= num2)
						{
							return primaryErrorInfo;
						}
					}
					try
					{
						primaryErrorInfo.Index = num3;
						__MIDL_IASErrorInfo_0001 _MIDL_IASErrorInfo_;
						num = calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,__MIDL_IASErrorInfo_0001*), ccomPtr<IASErrorInfo>, &_MIDL_IASErrorInfo_, (IntPtr)(*(*ccomPtr<IASErrorInfo> + 24L)));
						if (num >= 0)
						{
							primaryErrorInfo.Type = _MIDL_IASErrorInfo_;
							primaryErrorInfo.TypeOrigin = WrapperErrorSourceOrigin.AS;
						}
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IASErrorInfo>.{dtor}), (void*)(&ccomPtr<IASErrorInfo>));
						throw;
					}
					if (ccomPtr<IASErrorInfo> != null)
					{
						uint num7 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IASErrorInfo>, (IntPtr)(*(*ccomPtr<IASErrorInfo> + 16L)));
					}
				}
				return primaryErrorInfo;
			}
			return null;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A6D8 File Offset: 0x00009AD8
		internal unsafe static void PopulateDataErrorInfo(CDBErrorInfo* errorInfo, DataErrorInfo dataErrorInfo, int primaryErrorIndex)
		{
			CComBSTR ccomBSTR;
			<Module>.ATL.CComBSTR.{ctor}(ref ccomBSTR, (sbyte*)(&<Module>.??_C@_05JJLPJMLG@en?9US@));
			try
			{
				long num = *(errorInfo + 8L);
				tagERRORINFO tagERRORINFO;
				if (num != 0L && calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagERRORINFO*), (IntPtr)num, primaryErrorIndex, &tagERRORINFO, (IntPtr)(*(*num + 32L))) >= 0)
				{
					dataErrorInfo.ErrorCode = (uint)(*((ref tagERRORINFO) + 4));
					dataErrorInfo.GenericMessage = Utils.GetGenericErrorMessage(errorInfo, ref tagERRORINFO, ref ccomBSTR);
				}
				CComBSTR ccomBSTR2 = 0L;
				try
				{
					CComBSTR ccomBSTR3 = 0L;
					try
					{
						CComBSTR ccomBSTR4 = 0L;
						try
						{
							_GUID guid;
							uint num3;
							int num2 = <Module>.ATL.CDBErrorInfo.GetAllErrorInfo(errorInfo, primaryErrorIndex, *ccomBSTR, (char**)(&ccomBSTR2), (char**)(&ccomBSTR4), &guid, &num3, (char**)(&ccomBSTR3));
							<Module>.SysFreeString(ccomBSTR);
							ccomBSTR = 0L;
							if (num2 >= 0)
							{
								dataErrorInfo.Description = new string(ccomBSTR2);
								dataErrorInfo.Guid = Utils.FromGUID(ref guid);
								dataErrorInfo.HelpContext = num3;
								dataErrorInfo.HelpFile = new string(ccomBSTR3);
								dataErrorInfo.Source = new string(ccomBSTR4);
								<Module>.SysFreeString(ccomBSTR4);
								ccomBSTR4 = 0L;
								<Module>.SysFreeString(ccomBSTR2);
								ccomBSTR2 = 0L;
								<Module>.SysFreeString(ccomBSTR3);
								ccomBSTR3 = 0L;
							}
							else
							{
								dataErrorInfo.Description = "Could not retrieve OLE DB Error Record.";
							}
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR4));
							throw;
						}
						<Module>.SysFreeString(ccomBSTR4);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR3));
						throw;
					}
					<Module>.SysFreeString(ccomBSTR3);
				}
				catch
				{
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR2));
					throw;
				}
				<Module>.SysFreeString(ccomBSTR2);
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR));
				throw;
			}
			<Module>.SysFreeString(ccomBSTR);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A318 File Offset: 0x00009718
		internal unsafe static string GetGenericErrorMessage(CDBErrorInfo* errorInfo, tagERRORINFO* basicErrorInfo, CComBSTR* lang)
		{
			uint num;
			if (<Module>.GetLocaleInfoEx(*lang, 536870913, (char*)(&num), 4) < 0)
			{
				return null;
			}
			CComPtr<IErrorLookup> ccomPtr<IErrorLookup>;
			MsolapClassFactory.CreateErrorLookup(&ccomPtr<IErrorLookup>);
			string text;
			try
			{
				if (((ccomPtr<IErrorLookup> == 0L) ? 1 : 0) != 0)
				{
					text = null;
				}
				else
				{
					IErrorRecords* ptr = *(errorInfo + 8L);
					CComPtr<IErrorRecords> ccomPtr<IErrorRecords> = ptr;
					if (ptr != null)
					{
						IErrorRecords* ptr2 = ptr;
						uint num2 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr2, (IntPtr)(*(*(long*)ptr2 + 8L)));
					}
					try
					{
						CComBSTR ccomBSTR = 0L;
						try
						{
							CComBSTR ccomBSTR2 = 0L;
							try
							{
								string text2 = null;
								tagDISPPARAMS tagDISPPARAMS;
								initblk(ref tagDISPPARAMS, 0, 24L);
								if (calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagDISPPARAMS*), ptr, 0, &tagDISPPARAMS, (IntPtr)(*(*(long*)ptr + 56L))) >= 0)
								{
									if (calli(System.Int32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr,System.Int32 modopt(System.Runtime.CompilerServices.IsLong),System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),tagDISPPARAMS*,System.UInt32 modopt(System.Runtime.CompilerServices.IsLong),System.Char**,System.Char**), ccomPtr<IErrorLookup>, *basicErrorInfo, *(basicErrorInfo + 4L), &tagDISPPARAMS, num, (char**)(&ccomBSTR2), (char**)(&ccomBSTR), (IntPtr)(*(*ccomPtr<IErrorLookup> + 24L))) >= 0)
									{
										text2 = new string(ccomBSTR);
										<Module>.SysFreeString(ccomBSTR2);
										ccomBSTR2 = 0L;
										<Module>.SysFreeString(ccomBSTR);
										ccomBSTR = 0L;
									}
									Utils.ClearDispParams(&tagDISPPARAMS);
								}
								text = text2;
							}
							catch
							{
								<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR2));
								throw;
							}
							<Module>.SysFreeString(ccomBSTR2);
						}
						catch
						{
							<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)(&ccomBSTR));
							throw;
						}
						<Module>.SysFreeString(ccomBSTR);
					}
					catch
					{
						<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IErrorRecords>.{dtor}), (void*)(&ccomPtr<IErrorRecords>));
						throw;
					}
					IErrorRecords* ptr3 = ptr;
					uint num3 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, (IntPtr)(*(*(long*)ptr3 + 16L)));
				}
			}
			catch
			{
				<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComPtr<IErrorLookup>.{dtor}), (void*)(&ccomPtr<IErrorLookup>));
				throw;
			}
			if (ccomPtr<IErrorLookup> != null)
			{
				uint num4 = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ccomPtr<IErrorLookup>, (IntPtr)(*(*ccomPtr<IErrorLookup> + 16L)));
			}
			return text;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00009AF8 File Offset: 0x00008EF8
		internal unsafe static void ClearDispParams(tagDISPPARAMS* pdispparams)
		{
			if (*(long*)pdispparams != 0L)
			{
				uint num = 0;
				if (0 < *(int*)(pdispparams + 16L / (long)sizeof(tagDISPPARAMS)))
				{
					do
					{
						<Module>.VariantClear(num * 24L + *(long*)pdispparams / (long)sizeof(tagVARIANT));
						num++;
					}
					while (num < *(int*)(pdispparams + 16L / (long)sizeof(tagDISPPARAMS)));
				}
				<Module>.CoTaskMemFree(*(long*)pdispparams);
				*(long*)pdispparams = 0L;
			}
			ulong num2 = (ulong)(*(long*)(pdispparams + 8L / (long)sizeof(tagDISPPARAMS)));
			if (num2 != 0UL)
			{
				<Module>.CoTaskMemFree(num2);
				*(long*)(pdispparams + 8L / (long)sizeof(tagDISPPARAMS)) = 0L;
			}
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00009B58 File Offset: 0x00008F58
		internal static _GUID ToGUID(Guid guid)
		{
			ref byte ptr = ref guid.ToByteArray()[0];
			_GUID guid2;
			cpblk(ref guid2, ref ptr, 16);
			return guid2;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00009B84 File Offset: 0x00008F84
		internal unsafe static string FromGUID(_GUID* guid)
		{
			char* ptr;
			int num = <Module>.StringFromCLSID(guid, &ptr);
			if (num < 0)
			{
				int num2 = num;
				<Module>._CxxThrowException((void*)(&num2), (_s__ThrowInfo*)(&<Module>._TI1J));
			}
			string text = new string(ptr);
			<Module>.CoTaskMemFree((void*)ptr);
			return text;
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00009FC4 File Offset: 0x000093C4
		internal unsafe static CComBSTR* StringToBSTR(CComBSTR* A_0, string str)
		{
			uint num = 0U;
			ref byte ptr = str;
			if ((ref ptr) != null)
			{
				ptr = (long)RuntimeHelpers.OffsetToStringData + (ref ptr);
			}
			ref char ptr2 = ref ptr;
			char* ptr3 = ref ptr2;
			<Module>.ATL.CComBSTR.{ctor}(A_0, str.Length, ptr3);
			try
			{
				num = 1U;
			}
			catch
			{
				if ((num & 1U) != 0U)
				{
					num &= 4294967294U;
					<Module>.___CxxCallUnwindDtor(ldftn(ATL.CComBSTR.{dtor}), (void*)A_0);
				}
				throw;
			}
			return A_0;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A994 File Offset: 0x00009D94
		internal static void ThrowErrorIfHrFailed(int hr, string message)
		{
			string text = null;
			if (hr < 0)
			{
				DataErrorInfo dataErrorInfo = Utils.GetDataErrorInfo(hr);
				WrapperErrorSource wrapperErrorSource = WrapperErrorSource.Unknown;
				text = null;
				WrapperErrorSourceOrigin wrapperErrorSourceOrigin = WrapperErrorSourceOrigin.None;
				WrapperErrorCodes wrapperErrorCodes = MsolapErrorCodes.MapMsolapErrorCode(dataErrorInfo.ErrorCode, dataErrorInfo.Hresult, dataErrorInfo.Description, dataErrorInfo.Type, out wrapperErrorSource, out text, out wrapperErrorSourceOrigin);
				dataErrorInfo.HasUserSafeDescription = MsolapErrorCodes.IsUserSafeError(wrapperErrorSource, wrapperErrorCodes, dataErrorInfo.ErrorCode);
				dataErrorInfo.OnPremErrorCode = text;
				byte b = ((dataErrorInfo.TypeOrigin != WrapperErrorSourceOrigin.None) ? 1 : 0);
				WrapperContract.Assert(b != 0, "TypeOrigin should have been set.");
				dataErrorInfo.TypeOrigin |= wrapperErrorSourceOrigin;
				throw new MsolapWrapperException(wrapperErrorCodes, message, dataErrorInfo, wrapperErrorSource);
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000AAEC File Offset: 0x00009EEC
		internal static void ThrowErrorIfHrFailed(int hr, string message, object arg0)
		{
			if (hr < 0)
			{
				object[] array = new object[] { arg0 };
				Utils.ThrowErrorIfHrFailed(hr, string.Format(CultureInfo.InvariantCulture, message, array));
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A038 File Offset: 0x00009438
		internal static void ThrowError(WrapperErrorCodes errorCode, string message, WrapperErrorSource errorSource, Exception exception, [MarshalAs(UnmanagedType.U1)] bool hasUserSafeDescription)
		{
			throw new MsolapWrapperException(errorCode, message, new DataErrorInfo
			{
				Description = message,
				TypeOrigin = WrapperErrorSourceOrigin.MsolapWrapper,
				HasUserSafeDescription = hasUserSafeDescription
			}, errorSource, exception);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A89C File Offset: 0x00009C9C
		internal static void ThrowError(WrapperErrorSource errorSource, string message)
		{
			throw new MsolapWrapperException(WrapperErrorCodes.MsolapWrapperError, message, errorSource, WrapperErrorSourceOrigin.MsolapWrapper);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000AA68 File Offset: 0x00009E68
		internal static void ThrowError(WrapperErrorSource errorSource, string message, object arg0, object arg1)
		{
			object[] array = new object[] { arg0, arg1 };
			string text = string.Format(CultureInfo.InvariantCulture, message, array);
			throw new MsolapWrapperException(WrapperErrorCodes.MsolapWrapperError, text, errorSource, WrapperErrorSourceOrigin.MsolapWrapper);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000AA30 File Offset: 0x00009E30
		internal static void ThrowError(WrapperErrorSource errorSource, string message, object arg0)
		{
			object[] array = new object[] { arg0 };
			string text = string.Format(CultureInfo.InvariantCulture, message, array);
			throw new MsolapWrapperException(WrapperErrorCodes.MsolapWrapperError, text, errorSource, WrapperErrorSourceOrigin.MsolapWrapper);
		}
	}
}
