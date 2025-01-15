namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001AC RID: 428
	[global::System.Runtime.InteropServices.ComVisible(true)]
	[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
	internal partial class SilentWindowsFormsAuthenticationDialog : global::Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi.WindowsFormsWebAuthenticationDialogBase
	{
		// Token: 0x06001367 RID: 4967 RVA: 0x00041451 File Offset: 0x0003F651
		protected override void Dispose(bool disposing)
		{
			if (this.timer != null)
			{
				this.timer.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x040007EC RID: 2028
		private global::System.Windows.Forms.Timer timer;
	}
}
