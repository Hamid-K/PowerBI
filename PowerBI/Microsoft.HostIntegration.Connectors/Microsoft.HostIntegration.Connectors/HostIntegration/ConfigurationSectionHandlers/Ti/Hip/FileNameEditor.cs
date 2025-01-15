using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200057A RID: 1402
	public class FileNameEditor : UITypeEditor
	{
		// Token: 0x06002FA3 RID: 12195 RVA: 0x0003A22E File Offset: 0x0003842E
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x06002FA4 RID: 12196 RVA: 0x000A2BEC File Offset: 0x000A0DEC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			string text = (string)value;
			if (context != null && context.Instance != null && provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
				folderBrowserDialog.ShowNewFolderButton = true;
				folderBrowserDialog.SelectedPath = text;
				folderBrowserDialog.Description = "Current Host-Initiated Process Assembly Path:\n" + text;
				try
				{
					if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
					{
						return folderBrowserDialog.SelectedPath;
					}
					return text;
				}
				catch (Exception)
				{
					return text;
				}
				return text;
			}
			return text;
		}
	}
}
