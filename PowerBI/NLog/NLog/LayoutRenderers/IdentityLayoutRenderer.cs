using System;
using System.ComponentModel;
using System.Security.Principal;
using System.Text;
using System.Threading;
using NLog.Config;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CA RID: 202
	[LayoutRenderer("identity")]
	[ThreadSafe]
	public class IdentityLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0001FF1B File Offset: 0x0001E11B
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0001FF23 File Offset: 0x0001E123
		[DefaultValue(":")]
		public string Separator { get; set; } = ":";

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0001FF2C File Offset: 0x0001E12C
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0001FF34 File Offset: 0x0001E134
		[DefaultValue(true)]
		public bool Name { get; set; } = true;

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0001FF3D File Offset: 0x0001E13D
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0001FF45 File Offset: 0x0001E145
		[DefaultValue(true)]
		public bool AuthType { get; set; } = true;

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0001FF4E File Offset: 0x0001E14E
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x0001FF56 File Offset: 0x0001E156
		[DefaultValue(true)]
		public bool IsAuthenticated { get; set; } = true;

		// Token: 0x06000C76 RID: 3190 RVA: 0x0001FF60 File Offset: 0x0001E160
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			IIdentity value = IdentityLayoutRenderer.GetValue();
			if (value != null)
			{
				string text = string.Empty;
				if (this.IsAuthenticated)
				{
					builder.Append(text);
					text = this.Separator;
					builder.Append(value.IsAuthenticated ? "auth" : "notauth");
				}
				if (this.AuthType)
				{
					builder.Append(text);
					text = this.Separator;
					builder.Append(value.AuthenticationType);
				}
				if (this.Name)
				{
					builder.Append(text);
					builder.Append(value.Name);
				}
			}
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0001FFEF File Offset: 0x0001E1EF
		private static IIdentity GetValue()
		{
			IPrincipal currentPrincipal = Thread.CurrentPrincipal;
			if (currentPrincipal == null)
			{
				return null;
			}
			return currentPrincipal.Identity;
		}
	}
}
