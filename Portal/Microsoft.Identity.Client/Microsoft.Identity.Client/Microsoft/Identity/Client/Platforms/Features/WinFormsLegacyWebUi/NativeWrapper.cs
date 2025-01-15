using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001AE RID: 430
	internal class NativeWrapper
	{
		// Token: 0x02000430 RID: 1072
		[StructLayout(LayoutKind.Sequential)]
		public class POINT
		{
			// Token: 0x06001F18 RID: 7960 RVA: 0x0006ED62 File Offset: 0x0006CF62
			public POINT()
			{
			}

			// Token: 0x06001F19 RID: 7961 RVA: 0x0006ED6A File Offset: 0x0006CF6A
			public POINT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x040012B3 RID: 4787
			public int x;

			// Token: 0x040012B4 RID: 4788
			public int y;
		}

		// Token: 0x02000431 RID: 1073
		[StructLayout(LayoutKind.Sequential)]
		public class OLECMD
		{
			// Token: 0x040012B5 RID: 4789
			[MarshalAs(UnmanagedType.U4)]
			public int cmdID;

			// Token: 0x040012B6 RID: 4790
			[MarshalAs(UnmanagedType.U4)]
			public int cmdf;
		}

		// Token: 0x02000432 RID: 1074
		[ComVisible(true)]
		[Guid("B722BCCB-4E68-101B-A2BC-00AA00404770")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleCommandTarget
		{
			// Token: 0x06001F1B RID: 7963
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int QueryStatus(ref Guid pguidCmdGroup, int cCmds, [In] [Out] NativeWrapper.OLECMD prgCmds, [In] [Out] IntPtr pCmdText);

			// Token: 0x06001F1C RID: 7964
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int Exec(IntPtr guid, int nCmdID, int nCmdexecopt, [MarshalAs(UnmanagedType.LPArray)] [In] object[] pvaIn, IntPtr pvaOut);
		}

		// Token: 0x02000433 RID: 1075
		[ComVisible(true)]
		[StructLayout(LayoutKind.Sequential)]
		public class DOCHOSTUIINFO
		{
			// Token: 0x040012B7 RID: 4791
			[MarshalAs(UnmanagedType.U4)]
			public int cbSize = Marshal.SizeOf(typeof(NativeWrapper.DOCHOSTUIINFO));

			// Token: 0x040012B8 RID: 4792
			[MarshalAs(UnmanagedType.I4)]
			public int dwFlags;

			// Token: 0x040012B9 RID: 4793
			[MarshalAs(UnmanagedType.I4)]
			public int dwDoubleClick;

			// Token: 0x040012BA RID: 4794
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved1;

			// Token: 0x040012BB RID: 4795
			[MarshalAs(UnmanagedType.I4)]
			public int dwReserved2;
		}

		// Token: 0x02000434 RID: 1076
		[Serializable]
		public struct MSG
		{
			// Token: 0x040012BC RID: 4796
			public IntPtr hwnd;

			// Token: 0x040012BD RID: 4797
			public int message;

			// Token: 0x040012BE RID: 4798
			public IntPtr wParam;

			// Token: 0x040012BF RID: 4799
			public IntPtr lParam;

			// Token: 0x040012C0 RID: 4800
			public int time;

			// Token: 0x040012C1 RID: 4801
			public int pt_x;

			// Token: 0x040012C2 RID: 4802
			public int pt_y;
		}

		// Token: 0x02000435 RID: 1077
		[StructLayout(LayoutKind.Sequential)]
		public class COMRECT
		{
			// Token: 0x06001F1E RID: 7966 RVA: 0x0006EDA8 File Offset: 0x0006CFA8
			public override string ToString()
			{
				return string.Concat(new object[] { "Left = ", this.left, " Top ", this.top, " Right = ", this.right, " Bottom = ", this.bottom });
			}

			// Token: 0x06001F1F RID: 7967 RVA: 0x0006EE18 File Offset: 0x0006D018
			public COMRECT()
			{
			}

			// Token: 0x06001F20 RID: 7968 RVA: 0x0006EE20 File Offset: 0x0006D020
			public COMRECT(Rectangle r)
			{
				this.left = r.X;
				this.top = r.Y;
				this.right = r.Right;
				this.bottom = r.Bottom;
			}

			// Token: 0x06001F21 RID: 7969 RVA: 0x0006EE5C File Offset: 0x0006D05C
			public COMRECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x06001F22 RID: 7970 RVA: 0x0006EE81 File Offset: 0x0006D081
			public static NativeWrapper.COMRECT FromXYWH(int x, int y, int width, int height)
			{
				return new NativeWrapper.COMRECT(x, y, x + width, y + height);
			}

			// Token: 0x040012C3 RID: 4803
			public int left;

			// Token: 0x040012C4 RID: 4804
			public int top;

			// Token: 0x040012C5 RID: 4805
			public int right;

			// Token: 0x040012C6 RID: 4806
			public int bottom;
		}

		// Token: 0x02000436 RID: 1078
		[Guid("00000115-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleInPlaceUIWindow
		{
			// Token: 0x06001F23 RID: 7971
			IntPtr GetWindow();

			// Token: 0x06001F24 RID: 7972
			[PreserveSig]
			int ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06001F25 RID: 7973
			[PreserveSig]
			int GetBorder([Out] NativeWrapper.COMRECT lprectBorder);

			// Token: 0x06001F26 RID: 7974
			[PreserveSig]
			int RequestBorderSpace([In] NativeWrapper.COMRECT pborderwidths);

			// Token: 0x06001F27 RID: 7975
			[PreserveSig]
			int SetBorderSpace([In] NativeWrapper.COMRECT pborderwidths);

			// Token: 0x06001F28 RID: 7976
			void SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] NativeWrapper.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);
		}

		// Token: 0x02000437 RID: 1079
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("00000117-0000-0000-C000-000000000046")]
		[ComImport]
		public interface IOleInPlaceActiveObject
		{
			// Token: 0x06001F29 RID: 7977
			[PreserveSig]
			int GetWindow(out IntPtr hwnd);

			// Token: 0x06001F2A RID: 7978
			void ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06001F2B RID: 7979
			[PreserveSig]
			int TranslateAccelerator([In] ref NativeWrapper.MSG lpmsg);

			// Token: 0x06001F2C RID: 7980
			void OnFrameWindowActivate(bool fActivate);

			// Token: 0x06001F2D RID: 7981
			void OnDocWindowActivate(int fActivate);

			// Token: 0x06001F2E RID: 7982
			void ResizeBorder([In] NativeWrapper.COMRECT prcBorder, [In] NativeWrapper.IOleInPlaceUIWindow pUIWindow, bool fFrameWindow);

			// Token: 0x06001F2F RID: 7983
			void EnableModeless(int fEnable);
		}

		// Token: 0x02000438 RID: 1080
		[StructLayout(LayoutKind.Sequential)]
		public sealed class tagOleMenuGroupWidths
		{
			// Token: 0x040012C7 RID: 4807
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
			public int[] widths = new int[6];
		}

		// Token: 0x02000439 RID: 1081
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("00000116-0000-0000-C000-000000000046")]
		[ComImport]
		public interface IOleInPlaceFrame
		{
			// Token: 0x06001F31 RID: 7985
			IntPtr GetWindow();

			// Token: 0x06001F32 RID: 7986
			[PreserveSig]
			int ContextSensitiveHelp(int fEnterMode);

			// Token: 0x06001F33 RID: 7987
			[PreserveSig]
			int GetBorder([Out] NativeWrapper.COMRECT lprectBorder);

			// Token: 0x06001F34 RID: 7988
			[PreserveSig]
			int RequestBorderSpace([In] NativeWrapper.COMRECT pborderwidths);

			// Token: 0x06001F35 RID: 7989
			[PreserveSig]
			int SetBorderSpace([In] NativeWrapper.COMRECT pborderwidths);

			// Token: 0x06001F36 RID: 7990
			[PreserveSig]
			int SetActiveObject([MarshalAs(UnmanagedType.Interface)] [In] NativeWrapper.IOleInPlaceActiveObject pActiveObject, [MarshalAs(UnmanagedType.LPWStr)] [In] string pszObjName);

			// Token: 0x06001F37 RID: 7991
			[PreserveSig]
			int InsertMenus([In] IntPtr hmenuShared, [In] [Out] NativeWrapper.tagOleMenuGroupWidths lpMenuWidths);

			// Token: 0x06001F38 RID: 7992
			[PreserveSig]
			int SetMenu([In] IntPtr hmenuShared, [In] IntPtr holemenu, [In] IntPtr hwndActiveObject);

			// Token: 0x06001F39 RID: 7993
			[PreserveSig]
			int RemoveMenus([In] IntPtr hmenuShared);

			// Token: 0x06001F3A RID: 7994
			[PreserveSig]
			int SetStatusText([MarshalAs(UnmanagedType.LPWStr)] [In] string pszStatusText);

			// Token: 0x06001F3B RID: 7995
			[PreserveSig]
			int EnableModeless(bool fEnable);

			// Token: 0x06001F3C RID: 7996
			[PreserveSig]
			int TranslateAccelerator([In] ref NativeWrapper.MSG lpmsg, [MarshalAs(UnmanagedType.U2)] [In] short wID);
		}

		// Token: 0x0200043A RID: 1082
		[Guid("00000122-0000-0000-C000-000000000046")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[ComImport]
		public interface IOleDropTarget
		{
			// Token: 0x06001F3D RID: 7997
			[PreserveSig]
			int OleDragEnter([MarshalAs(UnmanagedType.Interface)] [In] object pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] NativeWrapper.POINT pt, [In] [Out] ref int pdwEffect);

			// Token: 0x06001F3E RID: 7998
			[PreserveSig]
			int OleDragOver([MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] NativeWrapper.POINT pt, [In] [Out] ref int pdwEffect);

			// Token: 0x06001F3F RID: 7999
			[PreserveSig]
			int OleDragLeave();

			// Token: 0x06001F40 RID: 8000
			[PreserveSig]
			int OleDrop([MarshalAs(UnmanagedType.Interface)] [In] object pDataObj, [MarshalAs(UnmanagedType.U4)] [In] int grfKeyState, [In] NativeWrapper.POINT pt, [In] [Out] ref int pdwEffect);
		}

		// Token: 0x0200043B RID: 1083
		[ComVisible(true)]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		[Guid("BD3F23C0-D43E-11CF-893B-00AA00BDCE1A")]
		[ComImport]
		public interface IDocHostUIHandler
		{
			// Token: 0x06001F41 RID: 8001
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ShowContextMenu([MarshalAs(UnmanagedType.U4)] [In] int dwID, [In] NativeWrapper.POINT pt, [MarshalAs(UnmanagedType.Interface)] [In] object pcmdtReserved, [MarshalAs(UnmanagedType.Interface)] [In] object pdispReserved);

			// Token: 0x06001F42 RID: 8002
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetHostInfo([In] [Out] NativeWrapper.DOCHOSTUIINFO info);

			// Token: 0x06001F43 RID: 8003
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ShowUI([MarshalAs(UnmanagedType.I4)] [In] int dwID, [In] NativeWrapper.IOleInPlaceActiveObject activeObject, [In] NativeWrapper.IOleCommandTarget commandTarget, [In] NativeWrapper.IOleInPlaceFrame frame, [In] NativeWrapper.IOleInPlaceUIWindow doc);

			// Token: 0x06001F44 RID: 8004
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int HideUI();

			// Token: 0x06001F45 RID: 8005
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int UpdateUI();

			// Token: 0x06001F46 RID: 8006
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int EnableModeless([MarshalAs(UnmanagedType.Bool)] [In] bool fEnable);

			// Token: 0x06001F47 RID: 8007
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnDocWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);

			// Token: 0x06001F48 RID: 8008
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int OnFrameWindowActivate([MarshalAs(UnmanagedType.Bool)] [In] bool fActivate);

			// Token: 0x06001F49 RID: 8009
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int ResizeBorder([In] NativeWrapper.COMRECT rect, [In] NativeWrapper.IOleInPlaceUIWindow doc, bool fFrameWindow);

			// Token: 0x06001F4A RID: 8010
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateAccelerator([In] ref NativeWrapper.MSG msg, [In] ref Guid group, [MarshalAs(UnmanagedType.I4)] [In] int nCmdID);

			// Token: 0x06001F4B RID: 8011
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetOptionKeyPath([MarshalAs(UnmanagedType.LPArray)] [Out] string[] pbstrKey, [MarshalAs(UnmanagedType.U4)] [In] int dw);

			// Token: 0x06001F4C RID: 8012
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetDropTarget([MarshalAs(UnmanagedType.Interface)] [In] NativeWrapper.IOleDropTarget pDropTarget, [MarshalAs(UnmanagedType.Interface)] out NativeWrapper.IOleDropTarget ppDropTarget);

			// Token: 0x06001F4D RID: 8013
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int GetExternal([MarshalAs(UnmanagedType.Interface)] out object ppDispatch);

			// Token: 0x06001F4E RID: 8014
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int TranslateUrl([MarshalAs(UnmanagedType.U4)] [In] int dwTranslate, [MarshalAs(UnmanagedType.LPWStr)] [In] string strURLIn, [MarshalAs(UnmanagedType.LPWStr)] out string pstrURLOut);

			// Token: 0x06001F4F RID: 8015
			[PreserveSig]
			[return: MarshalAs(UnmanagedType.I4)]
			int FilterDataObject(IDataObject pDO, out IDataObject ppDORet);
		}

		// Token: 0x0200043C RID: 1084
		[Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		public interface IWebBrowser2
		{
			// Token: 0x17000619 RID: 1561
			// (get) Token: 0x06001F50 RID: 8016
			[DispId(203)]
			object Document
			{
				[DispId(203)]
				[return: MarshalAs(UnmanagedType.IDispatch)]
				get;
			}

			// Token: 0x1700061A RID: 1562
			// (set) Token: 0x06001F51 RID: 8017
			[DispId(551)]
			bool Silent
			{
				[DispId(551)]
				[param: MarshalAs(UnmanagedType.Bool)]
				set;
			}
		}

		// Token: 0x0200043D RID: 1085
		[Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[ComImport]
		public interface DWebBrowserEvents2
		{
			// Token: 0x06001F52 RID: 8018
			[DispId(102)]
			void StatusTextChange([In] string text);

			// Token: 0x06001F53 RID: 8019
			[DispId(108)]
			void ProgressChange([In] int progress, [In] int progressMax);

			// Token: 0x06001F54 RID: 8020
			[DispId(105)]
			void CommandStateChange([In] long command, [In] bool enable);

			// Token: 0x06001F55 RID: 8021
			[DispId(106)]
			void DownloadBegin();

			// Token: 0x06001F56 RID: 8022
			[DispId(104)]
			void DownloadComplete();

			// Token: 0x06001F57 RID: 8023
			[DispId(113)]
			void TitleChange([In] string text);

			// Token: 0x06001F58 RID: 8024
			[DispId(112)]
			void PropertyChange([In] string szProperty);

			// Token: 0x06001F59 RID: 8025
			[DispId(250)]
			void BeforeNavigate2([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL, [In] ref object flags, [In] ref object targetFrameName, [In] ref object postData, [In] ref object headers, [In] [Out] ref bool cancel);

			// Token: 0x06001F5A RID: 8026
			[DispId(251)]
			void NewWindow2([MarshalAs(UnmanagedType.IDispatch)] [In] [Out] ref object pDisp, [In] [Out] ref bool cancel);

			// Token: 0x06001F5B RID: 8027
			[DispId(252)]
			void NavigateComplete2([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL);

			// Token: 0x06001F5C RID: 8028
			[DispId(259)]
			void DocumentComplete([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL);

			// Token: 0x06001F5D RID: 8029
			[DispId(253)]
			void OnQuit();

			// Token: 0x06001F5E RID: 8030
			[DispId(254)]
			void OnVisible([In] bool visible);

			// Token: 0x06001F5F RID: 8031
			[DispId(255)]
			void OnToolBar([In] bool toolBar);

			// Token: 0x06001F60 RID: 8032
			[DispId(256)]
			void OnMenuBar([In] bool menuBar);

			// Token: 0x06001F61 RID: 8033
			[DispId(257)]
			void OnStatusBar([In] bool statusBar);

			// Token: 0x06001F62 RID: 8034
			[DispId(258)]
			void OnFullScreen([In] bool fullScreen);

			// Token: 0x06001F63 RID: 8035
			[DispId(260)]
			void OnTheaterMode([In] bool theaterMode);

			// Token: 0x06001F64 RID: 8036
			[DispId(262)]
			void WindowSetResizable([In] bool resizable);

			// Token: 0x06001F65 RID: 8037
			[DispId(264)]
			void WindowSetLeft([In] int left);

			// Token: 0x06001F66 RID: 8038
			[DispId(265)]
			void WindowSetTop([In] int top);

			// Token: 0x06001F67 RID: 8039
			[DispId(266)]
			void WindowSetWidth([In] int width);

			// Token: 0x06001F68 RID: 8040
			[DispId(267)]
			void WindowSetHeight([In] int height);

			// Token: 0x06001F69 RID: 8041
			[DispId(263)]
			void WindowClosing([In] bool isChildWindow, [In] [Out] ref bool cancel);

			// Token: 0x06001F6A RID: 8042
			[DispId(268)]
			void ClientToHostWindow([In] [Out] ref long cx, [In] [Out] ref long cy);

			// Token: 0x06001F6B RID: 8043
			[DispId(269)]
			void SetSecureLockIcon([In] int secureLockIcon);

			// Token: 0x06001F6C RID: 8044
			[DispId(270)]
			void FileDownload([In] [Out] ref bool cancel);

			// Token: 0x06001F6D RID: 8045
			[DispId(271)]
			void NavigateError([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL, [In] ref object frame, [In] ref object statusCode, [In] [Out] ref bool cancel);

			// Token: 0x06001F6E RID: 8046
			[DispId(225)]
			void PrintTemplateInstantiation([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp);

			// Token: 0x06001F6F RID: 8047
			[DispId(226)]
			void PrintTemplateTeardown([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp);

			// Token: 0x06001F70 RID: 8048
			[DispId(227)]
			void UpdatePageStatus([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object nPage, [In] ref object fDone);

			// Token: 0x06001F71 RID: 8049
			[DispId(272)]
			void PrivacyImpactedStateChange([In] bool bImpacted);
		}
	}
}
