using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using mshtml;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000AB1 RID: 2737
	internal class NoUIWebBrowser : WebBrowser
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06004CB5 RID: 19637 RVA: 0x000FD1C4 File Offset: 0x000FB3C4
		// (remove) Token: 0x06004CB6 RID: 19638 RVA: 0x000FD1FC File Offset: 0x000FB3FC
		public event NoUIWebBrowser.NavigateErrorHandler NavigateError;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06004CB7 RID: 19639 RVA: 0x000FD234 File Offset: 0x000FB434
		// (remove) Token: 0x06004CB8 RID: 19640 RVA: 0x000FD26C File Offset: 0x000FB46C
		public event NoUIWebBrowser.BeforeNavigateHandler BeforeNavigate;

		// Token: 0x06004CB9 RID: 19641 RVA: 0x000FD2A1 File Offset: 0x000FB4A1
		protected override void CreateSink()
		{
			base.CreateSink();
			this.helper = new NoUIWebBrowser.EventHelper(this);
			this.eventsCookie = new AxHost.ConnectionPointCookie(base.ActiveXInstance, this.helper, typeof(NoUIWebBrowser.IDWebBrowserEvents2));
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x000FD2D6 File Offset: 0x000FB4D6
		protected override void DetachSink()
		{
			if (this.eventsCookie != null)
			{
				this.eventsCookie.Disconnect();
				this.eventsCookie = null;
				this.helper = null;
			}
			base.DetachSink();
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x000FD2FF File Offset: 0x000FB4FF
		protected override WebBrowserSiteBase CreateWebBrowserSiteBase()
		{
			return new NoUIWebBrowser.WrapperBrowserSite(this);
		}

		// Token: 0x06004CBC RID: 19644
		[DllImport("urlmon.dll")]
		[return: MarshalAs(UnmanagedType.Error)]
		private static extern int CoInternetSetFeatureEnabled(INTERNETFEATURELIST FeatureEntry, [MarshalAs(UnmanagedType.U4)] int dwFlags, bool fEnable);

		// Token: 0x040028AA RID: 10410
		private const int SET_FEATURE_ON_THREAD = 1;

		// Token: 0x040028AB RID: 10411
		private const int SET_FEATURE_ON_PROCESS = 2;

		// Token: 0x040028AC RID: 10412
		private const int SET_FEATURE_IN_REGISTRY = 4;

		// Token: 0x040028AD RID: 10413
		private const int SET_FEATURE_ON_THREAD_LOCALMACHINE = 8;

		// Token: 0x040028AE RID: 10414
		private const int SET_FEATURE_ON_THREAD_INTRANET = 16;

		// Token: 0x040028AF RID: 10415
		private const int SET_FEATURE_ON_THREAD_TRUSTED = 32;

		// Token: 0x040028B0 RID: 10416
		private const int SET_FEATURE_ON_THREAD_INTERNET = 64;

		// Token: 0x040028B1 RID: 10417
		private const int SET_FEATURE_ON_THREAD_RESTRICTED = 128;

		// Token: 0x040028B2 RID: 10418
		private AxHost.ConnectionPointCookie eventsCookie;

		// Token: 0x040028B3 RID: 10419
		private NoUIWebBrowser.EventHelper helper;

		// Token: 0x02000AB2 RID: 2738
		// (Invoke) Token: 0x06004CBF RID: 19647
		public delegate void NavigateErrorHandler(WebBrowser browser, object pDisp, string frameName, string url, int statusCode, ref bool cancel);

		// Token: 0x02000AB3 RID: 2739
		// (Invoke) Token: 0x06004CC3 RID: 19651
		public delegate void BeforeNavigateHandler(WebBrowser browser, object pDisp, string url, string targetFrameName, ref string headers, ref bool cancel);

		// Token: 0x02000AB4 RID: 2740
		private class EventHelper : StandardOleMarshalObject, NoUIWebBrowser.IDWebBrowserEvents2
		{
			// Token: 0x06004CC6 RID: 19654 RVA: 0x000FD30F File Offset: 0x000FB50F
			public EventHelper(NoUIWebBrowser parent)
			{
				this.parent = parent;
			}

			// Token: 0x06004CC7 RID: 19655 RVA: 0x000FD320 File Offset: 0x000FB520
			public void NavigateError(object pDisp, ref object url, ref object frame, ref object statusCode, ref bool cancel)
			{
				NoUIWebBrowser.NavigateErrorHandler navigateError = this.parent.NavigateError;
				if (navigateError != null)
				{
					navigateError(this.parent, pDisp, (string)frame, (string)url, (int)statusCode, ref cancel);
				}
			}

			// Token: 0x06004CC8 RID: 19656 RVA: 0x000FD364 File Offset: 0x000FB564
			public void BeforeNavigate2(object pDisp, ref object url, ref object flags, ref object targetFrameName, ref object postData, ref object headers, ref bool cancel)
			{
				NoUIWebBrowser.BeforeNavigateHandler beforeNavigate = this.parent.BeforeNavigate;
				if (beforeNavigate != null)
				{
					string text = headers as string;
					beforeNavigate(this.parent, pDisp, url as string, targetFrameName as string, ref text, ref cancel);
					headers = text;
				}
			}

			// Token: 0x040028B6 RID: 10422
			private readonly NoUIWebBrowser parent;
		}

		// Token: 0x02000AB5 RID: 2741
		private class WrapperBrowserSite : WebBrowser.WebBrowserSite, IDocHostShowUI
		{
			// Token: 0x06004CC9 RID: 19657 RVA: 0x000FD3B0 File Offset: 0x000FB5B0
			public WrapperBrowserSite(WebBrowser host)
				: base(host)
			{
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_WEBOC_POPUPMANAGEMENT, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_BLOCK_INPUT_PROMPTS, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_RESTRICT_ACTIVEXINSTALL, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_RESTRICT_FILEDOWNLOAD, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_SSLUX, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_DISABLE_NAVIGATION_SOUNDS, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_MIME_HANDLING, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_MIME_SNIFFING, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_OBJECT_CACHING, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_SAFE_BINDTOOBJECT, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_ZONE_ELEVATION, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_ADDON_MANAGEMENT, 2, true));
				NoUIWebBrowser.WrapperBrowserSite.Verify(NoUIWebBrowser.CoInternetSetFeatureEnabled(INTERNETFEATURELIST.FEATURE_HTTP_USERNAME_PASSWORD_DISABLE, 2, true));
			}

			// Token: 0x06004CCA RID: 19658 RVA: 0x00002105 File Offset: 0x00000305
			public uint ShowHelp(IntPtr hwnd, string pszHelpFile, uint uCommand, uint dwData, tagPOINT ptMouse, object pDispatchObjectHit)
			{
				return 0U;
			}

			// Token: 0x06004CCB RID: 19659 RVA: 0x000FD475 File Offset: 0x000FB675
			public uint ShowMessage(IntPtr hwnd, string lpstrText, string lpstrCaption, uint dwType, string lpstrHelpFile, uint dwHelpContext, out int lpResult)
			{
				lpResult = 0;
				return 0U;
			}

			// Token: 0x06004CCC RID: 19660 RVA: 0x000FD47C File Offset: 0x000FB67C
			private static void Verify(int result)
			{
				if (result != 0)
				{
					throw new InvalidOperationException("CoInternetSetFeatureEnabled failed");
				}
			}
		}

		// Token: 0x02000AB6 RID: 2742
		[Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
		[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
		[TypeLibType(TypeLibTypeFlags.FHidden)]
		[ComImport]
		public interface IDWebBrowserEvents2
		{
			// Token: 0x06004CCD RID: 19661
			[DispId(271)]
			void NavigateError([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL, [In] ref object frame, [In] ref object statusCode, [In] [Out] ref bool cancel);

			// Token: 0x06004CCE RID: 19662
			[DispId(250)]
			void BeforeNavigate2([MarshalAs(UnmanagedType.IDispatch)] [In] object pDisp, [In] ref object URL, [In] ref object Flags, [In] ref object TargetFrameName, [In] ref object PostData, [In] ref object Headers, [MarshalAs(UnmanagedType.VariantBool)] [In] [Out] ref bool Cancel);
		}
	}
}
