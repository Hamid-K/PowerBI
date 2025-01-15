using System;
using System.ComponentModel;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000BF RID: 191
	[LayoutRenderer("environment-user")]
	[ThreadSafe]
	public class EnvironmentUserLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x0001EF0C File Offset: 0x0001D10C
		public EnvironmentUserLayoutRenderer()
		{
			this.UserName = true;
			this.Domain = false;
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x0001EF38 File Offset: 0x0001D138
		// (set) Token: 0x06000BFD RID: 3069 RVA: 0x0001EF40 File Offset: 0x0001D140
		[DefaultValue(true)]
		public bool UserName { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x0001EF49 File Offset: 0x0001D149
		// (set) Token: 0x06000BFF RID: 3071 RVA: 0x0001EF51 File Offset: 0x0001D151
		[DefaultValue(false)]
		public bool Domain { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x0001EF5A File Offset: 0x0001D15A
		// (set) Token: 0x06000C01 RID: 3073 RVA: 0x0001EF62 File Offset: 0x0001D162
		[DefaultValue("UserUnknown")]
		public string DefaultUser { get; set; } = "UserUnknown";

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x0001EF6B File Offset: 0x0001D16B
		// (set) Token: 0x06000C03 RID: 3075 RVA: 0x0001EF73 File Offset: 0x0001D173
		[DefaultValue("DomainUnknown")]
		public string DefaultDomain { get; set; } = "DomainUnknown";

		// Token: 0x06000C04 RID: 3076 RVA: 0x0001EF7C File Offset: 0x0001D17C
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.GetStringValue());
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x0001EF8B File Offset: 0x0001D18B
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue();
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x0001EF94 File Offset: 0x0001D194
		private string GetStringValue()
		{
			if (this.UserName)
			{
				if (!this.Domain)
				{
					return this.GetUserName();
				}
				return this.GetDomainName() + "\\" + this.GetUserName();
			}
			else
			{
				if (!this.Domain)
				{
					return string.Empty;
				}
				return this.GetDomainName();
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0001EFE3 File Offset: 0x0001D1E3
		private string GetUserName()
		{
			return this.GetValueSafe(() => Environment.UserName, this.DefaultUser);
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0001F010 File Offset: 0x0001D210
		private string GetDomainName()
		{
			return this.GetValueSafe(() => Environment.UserDomainName, this.DefaultDomain);
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0001F040 File Offset: 0x0001D240
		private string GetValueSafe(Func<string> getValue, string defaultValue)
		{
			string text2;
			try
			{
				string text = getValue();
				text2 = (string.IsNullOrEmpty(text) ? (defaultValue ?? string.Empty) : text);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Failed to lookup Environment-User. Fallback value={0}", new object[] { defaultValue });
				text2 = defaultValue ?? string.Empty;
			}
			return text2;
		}
	}
}
