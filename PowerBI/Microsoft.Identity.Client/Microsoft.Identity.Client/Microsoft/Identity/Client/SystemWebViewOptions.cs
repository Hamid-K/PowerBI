using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200012A RID: 298
	public class SystemWebViewOptions
	{
		// Token: 0x06000EA7 RID: 3751 RVA: 0x000383FE File Offset: 0x000365FE
		public SystemWebViewOptions()
		{
			SystemWebViewOptions.ValidatePlatformAvailability();
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000EA8 RID: 3752 RVA: 0x0003840B File Offset: 0x0003660B
		// (set) Token: 0x06000EA9 RID: 3753 RVA: 0x00038413 File Offset: 0x00036613
		public string HtmlMessageSuccess { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0003841C File Offset: 0x0003661C
		// (set) Token: 0x06000EAB RID: 3755 RVA: 0x00038424 File Offset: 0x00036624
		public string HtmlMessageError { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0003842D File Offset: 0x0003662D
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x00038435 File Offset: 0x00036635
		public Uri BrowserRedirectSuccess { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000EAE RID: 3758 RVA: 0x0003843E File Offset: 0x0003663E
		// (set) Token: 0x06000EAF RID: 3759 RVA: 0x00038446 File Offset: 0x00036646
		public Uri BrowserRedirectError { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000EB0 RID: 3760 RVA: 0x0003844F File Offset: 0x0003664F
		// (set) Token: 0x06000EB1 RID: 3761 RVA: 0x00038457 File Offset: 0x00036657
		public bool iOSHidePrivacyPrompt { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x00038460 File Offset: 0x00036660
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x00038468 File Offset: 0x00036668
		public Func<Uri, Task> OpenBrowserAsync { get; set; }

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00038474 File Offset: 0x00036674
		internal void LogParameters(ILoggerAdapter logger)
		{
			logger.Info(string.Format("DefaultBrowserOptions configured. HidePrivacyPrompt {0}", this.iOSHidePrivacyPrompt));
			if (logger.IsLoggingEnabled(LogLevel.Verbose))
			{
				logger.VerbosePii(() => "HtmlMessageSuccess " + this.HtmlMessageSuccess, () => "HtmlMessageSuccess? " + (!string.IsNullOrEmpty(this.HtmlMessageSuccess)).ToString());
				logger.VerbosePii(() => "HtmlMessageError " + this.HtmlMessageError, () => "HtmlMessageError? " + (!string.IsNullOrEmpty(this.HtmlMessageError)).ToString());
				logger.VerbosePii(delegate
				{
					string text = "BrowserRedirectSuccess ";
					Uri browserRedirectSuccess = this.BrowserRedirectSuccess;
					return text + ((browserRedirectSuccess != null) ? browserRedirectSuccess.ToString() : null);
				}, () => "BrowserRedirectSuccess? " + (this.BrowserRedirectSuccess != null).ToString());
				logger.VerbosePii(delegate
				{
					string text2 = "BrowserRedirectError ";
					Uri browserRedirectError = this.BrowserRedirectError;
					return text2 + ((browserRedirectError != null) ? browserRedirectError.ToString() : null);
				}, () => "BrowserRedirectError? " + (this.BrowserRedirectError != null).ToString());
			}
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x0003851D File Offset: 0x0003671D
		internal static void ValidatePlatformAvailability()
		{
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x00038520 File Offset: 0x00036720
		public static async Task OpenWithEdgeBrowserAsync(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			string text = uri.AbsoluteUri;
			text = text.Replace("&", "^&");
			Process.Start(new ProcessStartInfo("cmd", "/c start microsoft-edge:" + text)
			{
				CreateNoWindow = true
			});
			await Task.FromResult<int>(0).ConfigureAwait(false);
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x00038564 File Offset: 0x00036764
		public static async Task OpenWithChromeEdgeBrowserAsync(Uri uri)
		{
			if (uri == null)
			{
				throw new ArgumentNullException("uri");
			}
			string text = uri.AbsoluteUri;
			text = text.Replace("&", "^&");
			Process.Start(new ProcessStartInfo("cmd", "/c start msedge " + text)
			{
				CreateNoWindow = true
			});
			await Task.FromResult<int>(0).ConfigureAwait(false);
		}
	}
}
