using System;
using System.Configuration;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B3 RID: 179
	[LayoutRenderer("appsetting")]
	[ThreadAgnostic]
	[ThreadSafe]
	public sealed class AppSettingLayoutRenderer2 : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B88 RID: 2952 RVA: 0x0001E350 File Offset: 0x0001C550
		// (set) Token: 0x06000B89 RID: 2953 RVA: 0x0001E358 File Offset: 0x0001C558
		[RequiredParameter]
		[DefaultParameter]
		public string Item { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0001E361 File Offset: 0x0001C561
		// (set) Token: 0x06000B8B RID: 2955 RVA: 0x0001E369 File Offset: 0x0001C569
		[Obsolete("Allows easier conversion from NLog.Extended. Instead use Item-property")]
		public string Name
		{
			get
			{
				return this.Item;
			}
			set
			{
				this.Item = value;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0001E372 File Offset: 0x0001C572
		// (set) Token: 0x06000B8D RID: 2957 RVA: 0x0001E37A File Offset: 0x0001C57A
		public string Default { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B8E RID: 2958 RVA: 0x0001E383 File Offset: 0x0001C583
		// (set) Token: 0x06000B8F RID: 2959 RVA: 0x0001E38B File Offset: 0x0001C58B
		internal IConfigurationManager2 ConfigurationManager { get; set; } = new ConfigurationManager2();

		// Token: 0x06000B90 RID: 2960 RVA: 0x0001E394 File Offset: 0x0001C594
		protected override void InitializeLayoutRenderer()
		{
			string text = "ConnectionStrings.";
			string item = this.Item;
			this._connectionStringName = ((item != null && item.TrimStart(new char[0]).StartsWith(text, StringComparison.InvariantCultureIgnoreCase)) ? this.Item.TrimStart(new char[0]).Substring(text.Length) : null);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x0001E3ED File Offset: 0x0001C5ED
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.GetStringValue());
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x0001E3FC File Offset: 0x0001C5FC
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue();
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x0001E404 File Offset: 0x0001C604
		private string GetStringValue()
		{
			if (string.IsNullOrEmpty(this.Item))
			{
				return this.Default;
			}
			string text;
			if (this._connectionStringName == null)
			{
				text = this.ConfigurationManager.AppSettings[this.Item];
			}
			else
			{
				ConnectionStringSettings connectionStringSettings = this.ConfigurationManager.LookupConnectionString(this._connectionStringName);
				text = ((connectionStringSettings != null) ? connectionStringSettings.ConnectionString : null);
			}
			string text2 = text;
			if (text2 == null && this.Default != null)
			{
				text2 = this.Default;
			}
			return text2 ?? string.Empty;
		}

		// Token: 0x040002C7 RID: 711
		private string _connectionStringName;
	}
}
