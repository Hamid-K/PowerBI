using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Text;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000F1 RID: 241
	[LayoutRenderer("windows-identity")]
	[ThreadSafe]
	public class WindowsIdentityLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000DAA RID: 3498 RVA: 0x00022899 File Offset: 0x00020A99
		public WindowsIdentityLayoutRenderer()
		{
			this.UserName = true;
			this.Domain = true;
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x000228AF File Offset: 0x00020AAF
		// (set) Token: 0x06000DAC RID: 3500 RVA: 0x000228B7 File Offset: 0x00020AB7
		[DefaultValue(true)]
		public bool Domain { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000228C0 File Offset: 0x00020AC0
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x000228C8 File Offset: 0x00020AC8
		[DefaultValue(true)]
		public bool UserName { get; set; }

		// Token: 0x06000DAF RID: 3503 RVA: 0x000228D4 File Offset: 0x00020AD4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			WindowsIdentity value = WindowsIdentityLayoutRenderer.GetValue();
			if (value != null)
			{
				string text;
				if (this.UserName)
				{
					text = (this.Domain ? WindowsIdentityLayoutRenderer.GetUserNameWithDomain(value) : WindowsIdentityLayoutRenderer.GetUserNameWithoutDomain(value));
				}
				else
				{
					if (!this.Domain)
					{
						return;
					}
					text = WindowsIdentityLayoutRenderer.GetDomainOnly(value);
				}
				builder.Append(text);
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x00022924 File Offset: 0x00020B24
		private static string GetDomainOnly(WindowsIdentity currentIdentity)
		{
			int num = currentIdentity.Name.IndexOf('\\');
			string text;
			if (num >= 0)
			{
				text = currentIdentity.Name.Substring(0, num);
			}
			else
			{
				text = currentIdentity.Name;
			}
			return text;
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0002295C File Offset: 0x00020B5C
		private static string GetUserNameWithoutDomain(WindowsIdentity currentIdentity)
		{
			int num = currentIdentity.Name.LastIndexOf('\\');
			string text;
			if (num >= 0)
			{
				text = currentIdentity.Name.Substring(num + 1);
			}
			else
			{
				text = currentIdentity.Name;
			}
			return text;
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x00022994 File Offset: 0x00020B94
		private static string GetUserNameWithDomain(WindowsIdentity currentIdentity)
		{
			return currentIdentity.Name;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0002299C File Offset: 0x00020B9C
		private static WindowsIdentity GetValue()
		{
			return WindowsIdentity.GetCurrent();
		}
	}
}
