using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Forms;
using Microsoft.Identity.Client.Platforms.Features.DesktopOs;

namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001A4 RID: 420
	internal class CustomWebBrowser : WebBrowser
	{
		// Token: 0x06001330 RID: 4912 RVA: 0x000408E4 File Offset: 0x0003EAE4
		static CustomWebBrowser()
		{
			CustomWebBrowser.shortcutDisallowedList.Add(Shortcut.AltBksp);
			CustomWebBrowser.shortcutDisallowedList.Add(Shortcut.AltDownArrow);
			CustomWebBrowser.shortcutDisallowedList.Add(Shortcut.AltLeftArrow);
			CustomWebBrowser.shortcutDisallowedList.Add(Shortcut.AltRightArrow);
			CustomWebBrowser.shortcutDisallowedList.Add(Shortcut.AltUpArrow);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x0004094B File Offset: 0x0003EB4B
		protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			return new CustomWebBrowser.CustomSite(this);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00040954 File Offset: 0x0003EB54
		protected override void CreateSink()
		{
			base.CreateSink();
			object activeXInstance = base.ActiveXInstance;
			if (activeXInstance != null)
			{
				this.webBrowserEvent = new CustomWebBrowser.CustomWebBrowserEvent(this);
				this.webBrowserEventCookie = new AxHost.ConnectionPointCookie(activeXInstance, this.webBrowserEvent, typeof(NativeWrapper.DWebBrowserEvents2));
			}
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x00040999 File Offset: 0x0003EB99
		protected override void DetachSink()
		{
			if (this.webBrowserEventCookie != null)
			{
				this.webBrowserEventCookie.Disconnect();
				this.webBrowserEventCookie = null;
			}
			base.DetachSink();
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x000409BB File Offset: 0x0003EBBB
		protected virtual void OnNavigateError(WebBrowserNavigateErrorEventArgs e)
		{
			if (this.NavigateError != null)
			{
				this.NavigateError(this, e);
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x000409D2 File Offset: 0x0003EBD2
		protected virtual void OnBeforeNavigate(WebBrowserBeforeNavigateEventArgs e)
		{
			WebBrowserBeforeNavigateEventHandler beforeNavigate = this.BeforeNavigate;
			if (beforeNavigate == null)
			{
				return;
			}
			beforeNavigate(this, e);
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06001336 RID: 4918 RVA: 0x000409E8 File Offset: 0x0003EBE8
		// (remove) Token: 0x06001337 RID: 4919 RVA: 0x00040A20 File Offset: 0x0003EC20
		public event WebBrowserNavigateErrorEventHandler NavigateError;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06001338 RID: 4920 RVA: 0x00040A58 File Offset: 0x0003EC58
		// (remove) Token: 0x06001339 RID: 4921 RVA: 0x00040A90 File Offset: 0x0003EC90
		public event WebBrowserBeforeNavigateEventHandler BeforeNavigate;

		// Token: 0x040007A4 RID: 1956
		private const int S_OK = 0;

		// Token: 0x040007A5 RID: 1957
		private const int S_FALSE = 1;

		// Token: 0x040007A6 RID: 1958
		private const int WM_CHAR = 258;

		// Token: 0x040007A7 RID: 1959
		private static readonly HashSet<Shortcut> shortcutDisallowedList = new HashSet<Shortcut>();

		// Token: 0x040007A8 RID: 1960
		private CustomWebBrowser.CustomWebBrowserEvent webBrowserEvent;

		// Token: 0x040007A9 RID: 1961
		private AxHost.ConnectionPointCookie webBrowserEventCookie;

		// Token: 0x0200042B RID: 1067
		[ComVisible(true)]
		[ComDefaultInterface(typeof(NativeWrapper.IDocHostUIHandler))]
		protected class CustomSite : WebBrowser.WebBrowserSite, NativeWrapper.IDocHostUIHandler, ICustomQueryInterface
		{
			// Token: 0x06001EDD RID: 7901 RVA: 0x0006E9ED File Offset: 0x0006CBED
			public CustomSite(WebBrowser host)
				: base(host)
			{
				this.host = host;
			}

			// Token: 0x06001EDE RID: 7902 RVA: 0x0006E9FD File Offset: 0x0006CBFD
			public CustomQueryInterfaceResult GetInterface(ref Guid iid, out IntPtr ppv)
			{
				ppv = IntPtr.Zero;
				if (iid == typeof(NativeWrapper.IDocHostUIHandler).GUID)
				{
					ppv = Marshal.GetComInterfaceForObject(this, typeof(NativeWrapper.IDocHostUIHandler), CustomQueryInterfaceMode.Ignore);
					return CustomQueryInterfaceResult.Handled;
				}
				return CustomQueryInterfaceResult.NotHandled;
			}

			// Token: 0x06001EDF RID: 7903 RVA: 0x0006EA38 File Offset: 0x0006CC38
			public int EnableModeless(bool fEnable)
			{
				return -2147467263;
			}

			// Token: 0x06001EE0 RID: 7904 RVA: 0x0006EA3F File Offset: 0x0006CC3F
			public int FilterDataObject(global::System.Runtime.InteropServices.ComTypes.IDataObject pDO, out global::System.Runtime.InteropServices.ComTypes.IDataObject ppDORet)
			{
				ppDORet = null;
				return 1;
			}

			// Token: 0x06001EE1 RID: 7905 RVA: 0x0006EA45 File Offset: 0x0006CC45
			public int GetDropTarget(NativeWrapper.IOleDropTarget pDropTarget, out NativeWrapper.IOleDropTarget ppDropTarget)
			{
				ppDropTarget = null;
				return 0;
			}

			// Token: 0x06001EE2 RID: 7906 RVA: 0x0006EA4B File Offset: 0x0006CC4B
			public int GetExternal(out object ppDispatch)
			{
				ppDispatch = this.host.ObjectForScripting;
				return 0;
			}

			// Token: 0x06001EE3 RID: 7907 RVA: 0x0006EA5C File Offset: 0x0006CC5C
			public int GetHostInfo(NativeWrapper.DOCHOSTUIINFO info)
			{
				info.dwDoubleClick = 0;
				info.dwFlags = 131088;
				if (WindowsDpiHelper.IsProcessDPIAware())
				{
					info.dwFlags |= 1073741824;
				}
				if (this.host.ScrollBarsEnabled)
				{
					info.dwFlags |= 128;
				}
				else
				{
					info.dwFlags |= 8;
				}
				if (Application.RenderWithVisualStyles)
				{
					info.dwFlags |= 262144;
				}
				else
				{
					info.dwFlags |= 524288;
				}
				info.dwFlags |= 67108864;
				return 0;
			}

			// Token: 0x06001EE4 RID: 7908 RVA: 0x0006EB03 File Offset: 0x0006CD03
			public int GetOptionKeyPath(string[] pbstrKey, int dw)
			{
				return -2147467263;
			}

			// Token: 0x06001EE5 RID: 7909 RVA: 0x0006EB0A File Offset: 0x0006CD0A
			public int HideUI()
			{
				return -2147467263;
			}

			// Token: 0x06001EE6 RID: 7910 RVA: 0x0006EB11 File Offset: 0x0006CD11
			public int OnDocWindowActivate(bool fActivate)
			{
				return -2147467263;
			}

			// Token: 0x06001EE7 RID: 7911 RVA: 0x0006EB18 File Offset: 0x0006CD18
			public int OnFrameWindowActivate(bool fActivate)
			{
				return -2147467263;
			}

			// Token: 0x06001EE8 RID: 7912 RVA: 0x0006EB1F File Offset: 0x0006CD1F
			public int ResizeBorder(NativeWrapper.COMRECT rect, NativeWrapper.IOleInPlaceUIWindow doc, bool fFrameWindow)
			{
				return -2147467263;
			}

			// Token: 0x06001EE9 RID: 7913 RVA: 0x0006EB26 File Offset: 0x0006CD26
			public int ShowContextMenu(int dwID, NativeWrapper.POINT pt, object pcmdtReserved, object pdispReserved)
			{
				if (dwID <= 4)
				{
					if (dwID != 2 && dwID != 4)
					{
						return 0;
					}
				}
				else if (dwID != 9 && dwID != 16)
				{
					return 0;
				}
				return 1;
			}

			// Token: 0x06001EEA RID: 7914 RVA: 0x0006EB43 File Offset: 0x0006CD43
			public int ShowUI(int dwID, NativeWrapper.IOleInPlaceActiveObject activeObject, NativeWrapper.IOleCommandTarget commandTarget, NativeWrapper.IOleInPlaceFrame frame, NativeWrapper.IOleInPlaceUIWindow doc)
			{
				return 1;
			}

			// Token: 0x06001EEB RID: 7915 RVA: 0x0006EB48 File Offset: 0x0006CD48
			public int TranslateAccelerator(ref NativeWrapper.MSG msg, ref Guid group, int nCmdID)
			{
				if (msg.message != 258 && (Control.ModifierKeys == Keys.Shift || Control.ModifierKeys == Keys.Alt || Control.ModifierKeys == Keys.Control))
				{
					Shortcut shortcut = (Shortcut)((int)msg.wParam | (int)Control.ModifierKeys);
					if (CustomWebBrowser.shortcutDisallowedList.Contains(shortcut))
					{
						return 0;
					}
				}
				return 1;
			}

			// Token: 0x06001EEC RID: 7916 RVA: 0x0006EBA8 File Offset: 0x0006CDA8
			public int TranslateUrl(int dwTranslate, string strUrlIn, out string pstrUrlOut)
			{
				pstrUrlOut = null;
				return 1;
			}

			// Token: 0x06001EED RID: 7917 RVA: 0x0006EBAE File Offset: 0x0006CDAE
			public int UpdateUI()
			{
				return -2147467263;
			}

			// Token: 0x040012AD RID: 4781
			private const int NotImplemented = -2147467263;

			// Token: 0x040012AE RID: 4782
			private readonly WebBrowser host;
		}

		// Token: 0x0200042C RID: 1068
		[ClassInterface(ClassInterfaceType.None)]
		private sealed class CustomWebBrowserEvent : StandardOleMarshalObject, NativeWrapper.DWebBrowserEvents2
		{
			// Token: 0x06001EEE RID: 7918 RVA: 0x0006EBB5 File Offset: 0x0006CDB5
			public CustomWebBrowserEvent(CustomWebBrowser parent)
			{
				this.parent = parent;
			}

			// Token: 0x06001EEF RID: 7919 RVA: 0x0006EBC4 File Offset: 0x0006CDC4
			public void NavigateError(object pDisp, ref object url, ref object frame, ref object statusCode, ref bool cancel)
			{
				string text = ((url == null) ? "" : ((string)url));
				string text2 = ((frame == null) ? "" : ((string)frame));
				int num = ((statusCode == null) ? 0 : ((int)statusCode));
				WebBrowserNavigateErrorEventArgs webBrowserNavigateErrorEventArgs = new WebBrowserNavigateErrorEventArgs(text, text2, num, pDisp);
				this.parent.OnNavigateError(webBrowserNavigateErrorEventArgs);
				cancel = webBrowserNavigateErrorEventArgs.Cancel;
			}

			// Token: 0x06001EF0 RID: 7920 RVA: 0x0006EC28 File Offset: 0x0006CE28
			public void BeforeNavigate2(object pDisp, ref object url, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
			{
				string text = ((url == null) ? string.Empty : ((string)url));
				int num = ((flags == null) ? 0 : ((int)flags));
				string text2 = ((targetFrameName == null) ? string.Empty : ((string)targetFrameName));
				byte[] array = (byte[])postData;
				string text3 = ((headers == null) ? string.Empty : ((string)headers));
				WebBrowserBeforeNavigateEventArgs webBrowserBeforeNavigateEventArgs = new WebBrowserBeforeNavigateEventArgs(text, array, text3, num, text2, pDisp);
				this.parent.OnBeforeNavigate(webBrowserBeforeNavigateEventArgs);
				cancel = webBrowserBeforeNavigateEventArgs.Cancel;
			}

			// Token: 0x06001EF1 RID: 7921 RVA: 0x0006ECAC File Offset: 0x0006CEAC
			public void ClientToHostWindow(ref long cX, ref long cY)
			{
			}

			// Token: 0x06001EF2 RID: 7922 RVA: 0x0006ECAE File Offset: 0x0006CEAE
			public void CommandStateChange(long command, bool enable)
			{
			}

			// Token: 0x06001EF3 RID: 7923 RVA: 0x0006ECB0 File Offset: 0x0006CEB0
			public void DocumentComplete(object pDisp, ref object urlObject)
			{
			}

			// Token: 0x06001EF4 RID: 7924 RVA: 0x0006ECB2 File Offset: 0x0006CEB2
			public void DownloadBegin()
			{
			}

			// Token: 0x06001EF5 RID: 7925 RVA: 0x0006ECB4 File Offset: 0x0006CEB4
			public void DownloadComplete()
			{
			}

			// Token: 0x06001EF6 RID: 7926 RVA: 0x0006ECB6 File Offset: 0x0006CEB6
			public void FileDownload(ref bool cancel)
			{
			}

			// Token: 0x06001EF7 RID: 7927 RVA: 0x0006ECB8 File Offset: 0x0006CEB8
			public void NavigateComplete2(object pDisp, ref object urlObject)
			{
			}

			// Token: 0x06001EF8 RID: 7928 RVA: 0x0006ECBA File Offset: 0x0006CEBA
			public void NewWindow2(ref object ppDisp, ref bool cancel)
			{
			}

			// Token: 0x06001EF9 RID: 7929 RVA: 0x0006ECBC File Offset: 0x0006CEBC
			public void OnFullScreen(bool fullScreen)
			{
			}

			// Token: 0x06001EFA RID: 7930 RVA: 0x0006ECBE File Offset: 0x0006CEBE
			public void OnMenuBar(bool menuBar)
			{
			}

			// Token: 0x06001EFB RID: 7931 RVA: 0x0006ECC0 File Offset: 0x0006CEC0
			public void OnQuit()
			{
			}

			// Token: 0x06001EFC RID: 7932 RVA: 0x0006ECC2 File Offset: 0x0006CEC2
			public void OnStatusBar(bool statusBar)
			{
			}

			// Token: 0x06001EFD RID: 7933 RVA: 0x0006ECC4 File Offset: 0x0006CEC4
			public void OnTheaterMode(bool theaterMode)
			{
			}

			// Token: 0x06001EFE RID: 7934 RVA: 0x0006ECC6 File Offset: 0x0006CEC6
			public void OnToolBar(bool toolBar)
			{
			}

			// Token: 0x06001EFF RID: 7935 RVA: 0x0006ECC8 File Offset: 0x0006CEC8
			public void OnVisible(bool visible)
			{
			}

			// Token: 0x06001F00 RID: 7936 RVA: 0x0006ECCA File Offset: 0x0006CECA
			public void PrintTemplateInstantiation(object pDisp)
			{
			}

			// Token: 0x06001F01 RID: 7937 RVA: 0x0006ECCC File Offset: 0x0006CECC
			public void PrintTemplateTeardown(object pDisp)
			{
			}

			// Token: 0x06001F02 RID: 7938 RVA: 0x0006ECCE File Offset: 0x0006CECE
			public void PrivacyImpactedStateChange(bool bImpacted)
			{
			}

			// Token: 0x06001F03 RID: 7939 RVA: 0x0006ECD0 File Offset: 0x0006CED0
			public void ProgressChange(int progress, int progressMax)
			{
			}

			// Token: 0x06001F04 RID: 7940 RVA: 0x0006ECD2 File Offset: 0x0006CED2
			public void PropertyChange(string szProperty)
			{
			}

			// Token: 0x06001F05 RID: 7941 RVA: 0x0006ECD4 File Offset: 0x0006CED4
			public void SetSecureLockIcon(int secureLockIcon)
			{
			}

			// Token: 0x06001F06 RID: 7942 RVA: 0x0006ECD6 File Offset: 0x0006CED6
			public void StatusTextChange(string text)
			{
			}

			// Token: 0x06001F07 RID: 7943 RVA: 0x0006ECD8 File Offset: 0x0006CED8
			public void TitleChange(string text)
			{
			}

			// Token: 0x06001F08 RID: 7944 RVA: 0x0006ECDA File Offset: 0x0006CEDA
			public void UpdatePageStatus(object pDisp, ref object nPage, ref object fDone)
			{
			}

			// Token: 0x06001F09 RID: 7945 RVA: 0x0006ECDC File Offset: 0x0006CEDC
			public void WindowClosing(bool isChildWindow, ref bool cancel)
			{
			}

			// Token: 0x06001F0A RID: 7946 RVA: 0x0006ECDE File Offset: 0x0006CEDE
			public void WindowSetHeight(int height)
			{
			}

			// Token: 0x06001F0B RID: 7947 RVA: 0x0006ECE0 File Offset: 0x0006CEE0
			public void WindowSetLeft(int left)
			{
			}

			// Token: 0x06001F0C RID: 7948 RVA: 0x0006ECE2 File Offset: 0x0006CEE2
			public void WindowSetResizable(bool resizable)
			{
			}

			// Token: 0x06001F0D RID: 7949 RVA: 0x0006ECE4 File Offset: 0x0006CEE4
			public void WindowSetTop(int top)
			{
			}

			// Token: 0x06001F0E RID: 7950 RVA: 0x0006ECE6 File Offset: 0x0006CEE6
			public void WindowSetWidth(int width)
			{
			}

			// Token: 0x040012AF RID: 4783
			private readonly CustomWebBrowser parent;
		}
	}
}
