namespace Microsoft.Identity.Client.Platforms.Features.WinFormsLegacyWebUi
{
	// Token: 0x020001B3 RID: 435
	[global::System.Runtime.InteropServices.ComVisible(true)]
	[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
	public abstract partial class WindowsFormsWebAuthenticationDialogBase : global::System.Windows.Forms.Form
	{
		// Token: 0x060013A6 RID: 5030 RVA: 0x00041DF4 File Offset: 0x0003FFF4
		private void InitializeComponent()
		{
			this.InvokeHandlingOwnerWindow(delegate
			{
				int num = (int)((double)global::System.Math.Max(((this.ownerWindow != null) ? global::System.Windows.Forms.Screen.FromHandle(this.ownerWindow.Handle) : global::System.Windows.Forms.Screen.PrimaryScreen).WorkingArea.Height, 160) * 70.0 / (double)global::Microsoft.Identity.Client.Platforms.Features.DesktopOs.WindowsDpiHelper.ZoomPercent);
				this._webBrowserPanel = new global::System.Windows.Forms.Panel();
				this._webBrowserPanel.SuspendLayout();
				base.SuspendLayout();
				this._webBrowser.Dock = global::System.Windows.Forms.DockStyle.Fill;
				this._webBrowser.Location = new global::System.Drawing.Point(0, 25);
				this._webBrowser.MinimumSize = new global::System.Drawing.Size(20, 20);
				this._webBrowser.Name = "webBrowser";
				this._webBrowser.Size = new global::System.Drawing.Size(566, 565);
				this._webBrowser.TabIndex = 1;
				this._webBrowser.IsWebBrowserContextMenuEnabled = false;
				this._webBrowserPanel.Controls.Add(this._webBrowser);
				this._webBrowserPanel.Dock = global::System.Windows.Forms.DockStyle.Fill;
				this._webBrowserPanel.BorderStyle = global::System.Windows.Forms.BorderStyle.None;
				this._webBrowserPanel.Location = new global::System.Drawing.Point(0, 0);
				this._webBrowserPanel.Name = "webBrowserPanel";
				this._webBrowserPanel.Size = new global::System.Drawing.Size(566, num);
				this._webBrowserPanel.TabIndex = 2;
				base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
				base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
				base.ClientSize = new global::System.Drawing.Size(566, num);
				base.Controls.Add(this._webBrowserPanel);
				base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedSingle;
				base.Name = "BrowserAuthenticationWindow";
				base.StartPosition = ((this.ownerWindow != null) ? global::System.Windows.Forms.FormStartPosition.CenterParent : global::System.Windows.Forms.FormStartPosition.CenterScreen);
				this.Text = string.Empty;
				base.ShowIcon = false;
				base.MaximizeBox = false;
				base.MinimizeBox = false;
				base.ShowInTaskbar = this.ownerWindow == null;
				this._webBrowserPanel.ResumeLayout(false);
				base.ResumeLayout(false);
			});
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x00041E08 File Offset: 0x00040008
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopWebBrowser();
			}
			base.Dispose(disposing);
		}
	}
}
