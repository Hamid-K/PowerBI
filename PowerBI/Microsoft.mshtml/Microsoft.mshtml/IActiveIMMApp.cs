using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000C9A RID: 3226
	[InterfaceType(1)]
	[ComConversionLoss]
	[Guid("08C0E040-62D1-11D1-9326-0060B067B86E")]
	[ComImport]
	public interface IActiveIMMApp
	{
		// Token: 0x0601620B RID: 90635
		[MethodImpl(4096, MethodCodeType = 3)]
		void AssociateContext([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint hIME, out uint phPrev);

		// Token: 0x0601620C RID: 90636
		[MethodImpl(4096, MethodCodeType = 3)]
		void ConfigureIMEA([In] IntPtr hKL, [ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint dwMode, [In] ref __MIDL___MIDL_itf_mshtml_0250_0001 pData);

		// Token: 0x0601620D RID: 90637
		[MethodImpl(4096, MethodCodeType = 3)]
		void ConfigureIMEW([In] IntPtr hKL, [ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint dwMode, [In] ref __MIDL___MIDL_itf_mshtml_0250_0002 pData);

		// Token: 0x0601620E RID: 90638
		[MethodImpl(4096, MethodCodeType = 3)]
		void CreateContext(out uint phIMC);

		// Token: 0x0601620F RID: 90639
		[MethodImpl(4096, MethodCodeType = 3)]
		void DestroyContext([In] uint hIME);

		// Token: 0x06016210 RID: 90640
		[MethodImpl(4096, MethodCodeType = 3)]
		void EnumRegisterWordA([In] IntPtr hKL, [MarshalAs(20)] [In] string szReading, [In] uint dwStyle, [MarshalAs(20)] [In] string szRegister, [In] IntPtr pData, [MarshalAs(28)] out IEnumRegisterWordA pEnum);

		// Token: 0x06016211 RID: 90641
		[MethodImpl(4096, MethodCodeType = 3)]
		void EnumRegisterWordW([In] IntPtr hKL, [MarshalAs(21)] [In] string szReading, [In] uint dwStyle, [MarshalAs(21)] [In] string szRegister, [In] IntPtr pData, [MarshalAs(28)] out IEnumRegisterWordW pEnum);

		// Token: 0x06016212 RID: 90642
		[MethodImpl(4096, MethodCodeType = 3)]
		void EscapeA([In] IntPtr hKL, [In] uint hIMC, [In] uint uEscape, [In] [Out] IntPtr pData, [ComAliasName("mshtml.LONG_PTR")] out int plResult);

		// Token: 0x06016213 RID: 90643
		[MethodImpl(4096, MethodCodeType = 3)]
		void EscapeW([In] IntPtr hKL, [In] uint hIMC, [In] uint uEscape, [In] [Out] IntPtr pData, [ComAliasName("mshtml.LONG_PTR")] out int plResult);

		// Token: 0x06016214 RID: 90644
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCandidateListA([In] uint hIMC, [In] uint dwIndex, [In] uint uBufLen, out __MIDL___MIDL_itf_mshtml_0250_0007 pCandList, out uint puCopied);

		// Token: 0x06016215 RID: 90645
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCandidateListW([In] uint hIMC, [In] uint dwIndex, [In] uint uBufLen, out __MIDL___MIDL_itf_mshtml_0250_0007 pCandList, out uint puCopied);

		// Token: 0x06016216 RID: 90646
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCandidateListCountA([In] uint hIMC, out uint pdwListSize, out uint pdwBufLen);

		// Token: 0x06016217 RID: 90647
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCandidateListCountW([In] uint hIMC, out uint pdwListSize, out uint pdwBufLen);

		// Token: 0x06016218 RID: 90648
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCandidateWindow([In] uint hIMC, [In] uint dwIndex, out __MIDL___MIDL_itf_mshtml_0250_0005 pCandidate);

		// Token: 0x06016219 RID: 90649
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCompositionFontA([In] uint hIMC, out __MIDL___MIDL_itf_mshtml_0250_0003 plf);

		// Token: 0x0601621A RID: 90650
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCompositionFontW([In] uint hIMC, out __MIDL___MIDL_itf_mshtml_0250_0004 plf);

		// Token: 0x0601621B RID: 90651
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCompositionStringA([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, out int plCopied, [Out] IntPtr pBuf);

		// Token: 0x0601621C RID: 90652
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCompositionStringW([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, out int plCopied, [Out] IntPtr pBuf);

		// Token: 0x0601621D RID: 90653
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCompositionWindow([In] uint hIMC, out __MIDL___MIDL_itf_mshtml_0250_0006 pCompForm);

		// Token: 0x0601621E RID: 90654
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetContext([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, out uint phIMC);

		// Token: 0x0601621F RID: 90655
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetConversionListA([In] IntPtr hKL, [In] uint hIMC, [MarshalAs(20)] [In] string pSrc, [In] uint uBufLen, [In] uint uFlag, out __MIDL___MIDL_itf_mshtml_0250_0007 pDst, out uint puCopied);

		// Token: 0x06016220 RID: 90656
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetConversionListW([In] IntPtr hKL, [In] uint hIMC, [MarshalAs(21)] [In] string pSrc, [In] uint uBufLen, [In] uint uFlag, out __MIDL___MIDL_itf_mshtml_0250_0007 pDst, out uint puCopied);

		// Token: 0x06016221 RID: 90657
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetConversionStatus([In] uint hIMC, out uint pfdwConversion, out uint pfdwSentence);

		// Token: 0x06016222 RID: 90658
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetDefaultIMEWnd([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [ComAliasName("mshtml.wireHWND")] [Out] IntPtr phDefWnd);

		// Token: 0x06016223 RID: 90659
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetDescriptionA([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(20)] [Out] string szDescription, out uint puCopied);

		// Token: 0x06016224 RID: 90660
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetDescriptionW([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(21)] [Out] string szDescription, out uint puCopied);

		// Token: 0x06016225 RID: 90661
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetGuideLineA([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, [MarshalAs(20)] [Out] string pBuf, out uint pdwResult);

		// Token: 0x06016226 RID: 90662
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetGuideLineW([In] uint hIMC, [In] uint dwIndex, [In] uint dwBufLen, [MarshalAs(21)] [Out] string pBuf, out uint pdwResult);

		// Token: 0x06016227 RID: 90663
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetIMEFileNameA([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(20)] [Out] string szFileName, out uint puCopied);

		// Token: 0x06016228 RID: 90664
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetIMEFileNameW([In] IntPtr hKL, [In] uint uBufLen, [MarshalAs(21)] [Out] string szFileName, out uint puCopied);

		// Token: 0x06016229 RID: 90665
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetOpenStatus([In] uint hIMC);

		// Token: 0x0601622A RID: 90666
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetProperty([In] IntPtr hKL, [In] uint fdwIndex, out uint pdwProperty);

		// Token: 0x0601622B RID: 90667
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetRegisterWordStyleA([In] IntPtr hKL, [In] uint nItem, out __MIDL___MIDL_itf_mshtml_0250_0008 pStyleBuf, out uint puCopied);

		// Token: 0x0601622C RID: 90668
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetRegisterWordStyleW([In] IntPtr hKL, [In] uint nItem, out __MIDL___MIDL_itf_mshtml_0250_0009 pStyleBuf, out uint puCopied);

		// Token: 0x0601622D RID: 90669
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetStatusWindowPos([In] uint hIMC, out tagPOINT pptPos);

		// Token: 0x0601622E RID: 90670
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetVirtualKey([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, out uint puVirtualKey);

		// Token: 0x0601622F RID: 90671
		[MethodImpl(4096, MethodCodeType = 3)]
		void InstallIMEA([MarshalAs(20)] [In] string szIMEFileName, [MarshalAs(20)] [In] string szLayoutText, out IntPtr phKL);

		// Token: 0x06016230 RID: 90672
		[MethodImpl(4096, MethodCodeType = 3)]
		void InstallIMEW([MarshalAs(21)] [In] string szIMEFileName, [MarshalAs(21)] [In] string szLayoutText, out IntPtr phKL);

		// Token: 0x06016231 RID: 90673
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsIME([In] IntPtr hKL);

		// Token: 0x06016232 RID: 90674
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsUIMessageA([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWndIME, [In] uint msg, [ComAliasName("mshtml.UINT_PTR")] [In] uint wParam, [ComAliasName("mshtml.LONG_PTR")] [In] int lParam);

		// Token: 0x06016233 RID: 90675
		[MethodImpl(4096, MethodCodeType = 3)]
		void IsUIMessageW([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWndIME, [In] uint msg, [ComAliasName("mshtml.UINT_PTR")] [In] uint wParam, [ComAliasName("mshtml.LONG_PTR")] [In] int lParam);

		// Token: 0x06016234 RID: 90676
		[MethodImpl(4096, MethodCodeType = 3)]
		void NotifyIME([In] uint hIMC, [In] uint dwAction, [In] uint dwIndex, [In] uint dwValue);

		// Token: 0x06016235 RID: 90677
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterWordA([In] IntPtr hKL, [MarshalAs(20)] [In] string szReading, [In] uint dwStyle, [MarshalAs(20)] [In] string szRegister);

		// Token: 0x06016236 RID: 90678
		[MethodImpl(4096, MethodCodeType = 3)]
		void RegisterWordW([In] IntPtr hKL, [MarshalAs(21)] [In] string szReading, [In] uint dwStyle, [MarshalAs(21)] [In] string szRegister);

		// Token: 0x06016237 RID: 90679
		[MethodImpl(4096, MethodCodeType = 3)]
		void ReleaseContext([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint hIMC);

		// Token: 0x06016238 RID: 90680
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCandidateWindow([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0005 pCandidate);

		// Token: 0x06016239 RID: 90681
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCompositionFontA([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0003 plf);

		// Token: 0x0601623A RID: 90682
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCompositionFontW([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0004 plf);

		// Token: 0x0601623B RID: 90683
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCompositionStringA([In] uint hIMC, [In] uint dwIndex, [In] IntPtr pComp, [In] uint dwCompLen, [In] IntPtr pRead, [In] uint dwReadLen);

		// Token: 0x0601623C RID: 90684
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCompositionStringW([In] uint hIMC, [In] uint dwIndex, [In] IntPtr pComp, [In] uint dwCompLen, [In] IntPtr pRead, [In] uint dwReadLen);

		// Token: 0x0601623D RID: 90685
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetCompositionWindow([In] uint hIMC, [In] ref __MIDL___MIDL_itf_mshtml_0250_0006 pCompForm);

		// Token: 0x0601623E RID: 90686
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetConversionStatus([In] uint hIMC, [In] uint fdwConversion, [In] uint fdwSentence);

		// Token: 0x0601623F RID: 90687
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetOpenStatus([In] uint hIMC, [In] int fOpen);

		// Token: 0x06016240 RID: 90688
		[MethodImpl(4096, MethodCodeType = 3)]
		void SetStatusWindowPos([In] uint hIMC, [In] ref tagPOINT pptPos);

		// Token: 0x06016241 RID: 90689
		[MethodImpl(4096, MethodCodeType = 3)]
		void SimulateHotKey([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint dwHotKeyID);

		// Token: 0x06016242 RID: 90690
		[MethodImpl(4096, MethodCodeType = 3)]
		void UnregisterWordA([In] IntPtr hKL, [MarshalAs(20)] [In] string szReading, [In] uint dwStyle, [MarshalAs(20)] [In] string szUnregister);

		// Token: 0x06016243 RID: 90691
		[MethodImpl(4096, MethodCodeType = 3)]
		void UnregisterWordW([In] IntPtr hKL, [MarshalAs(21)] [In] string szReading, [In] uint dwStyle, [MarshalAs(21)] [In] string szUnregister);

		// Token: 0x06016244 RID: 90692
		[MethodImpl(4096, MethodCodeType = 3)]
		void Activate([In] int fRestoreLayout);

		// Token: 0x06016245 RID: 90693
		[MethodImpl(4096, MethodCodeType = 3)]
		void Deactivate();

		// Token: 0x06016246 RID: 90694
		[MethodImpl(4096, MethodCodeType = 3)]
		void OnDefWindowProc([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint msg, [ComAliasName("mshtml.UINT_PTR")] [In] uint wParam, [ComAliasName("mshtml.LONG_PTR")] [In] int lParam, [ComAliasName("mshtml.LONG_PTR")] out int plResult);

		// Token: 0x06016247 RID: 90695
		[MethodImpl(4096, MethodCodeType = 3)]
		void FilterClientWindows([In] ref ushort aaClassList, [In] uint uSize);

		// Token: 0x06016248 RID: 90696
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetCodePageA([In] IntPtr hKL, out uint uCodePage);

		// Token: 0x06016249 RID: 90697
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetLangId([In] IntPtr hKL, out ushort plid);

		// Token: 0x0601624A RID: 90698
		[MethodImpl(4096, MethodCodeType = 3)]
		void AssociateContextEx([ComAliasName("mshtml.wireHWND")] [In] ref _RemotableHandle hWnd, [In] uint hIMC, [In] uint dwFlags);

		// Token: 0x0601624B RID: 90699
		[MethodImpl(4096, MethodCodeType = 3)]
		void DisableIME([In] uint idThread);

		// Token: 0x0601624C RID: 90700
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetImeMenuItemsA([In] uint hIMC, [In] uint dwFlags, [In] uint dwType, [In] ref __MIDL___MIDL_itf_mshtml_0250_0010 pImeParentMenu, out __MIDL___MIDL_itf_mshtml_0250_0010 pImeMenu, [In] uint dwSize, out uint pdwResult);

		// Token: 0x0601624D RID: 90701
		[MethodImpl(4096, MethodCodeType = 3)]
		void GetImeMenuItemsW([In] uint hIMC, [In] uint dwFlags, [In] uint dwType, [In] ref __MIDL___MIDL_itf_mshtml_0250_0011 pImeParentMenu, out __MIDL___MIDL_itf_mshtml_0250_0011 pImeMenu, [In] uint dwSize, out uint pdwResult);

		// Token: 0x0601624E RID: 90702
		[MethodImpl(4096, MethodCodeType = 3)]
		void EnumInputContext([In] uint idThread, [MarshalAs(28)] out IEnumInputContext ppEnum);
	}
}
