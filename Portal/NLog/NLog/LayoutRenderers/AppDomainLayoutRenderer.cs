using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal.Fakeables;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000B2 RID: 178
	[LayoutRenderer("appdomain")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class AppDomainLayoutRenderer : LayoutRenderer
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0001E266 File Offset: 0x0001C466
		public AppDomainLayoutRenderer()
			: this(LogFactory.CurrentAppDomain)
		{
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001E273 File Offset: 0x0001C473
		public AppDomainLayoutRenderer(IAppDomain currentDomain)
		{
			this._currentDomain = currentDomain;
			this.Format = "Long";
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B82 RID: 2946 RVA: 0x0001E28D File Offset: 0x0001C48D
		// (set) Token: 0x06000B83 RID: 2947 RVA: 0x0001E295 File Offset: 0x0001C495
		[DefaultParameter]
		[DefaultValue("Long")]
		public string Format { get; set; }

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001E29E File Offset: 0x0001C49E
		protected override void InitializeLayoutRenderer()
		{
			this._assemblyName = null;
			base.InitializeLayoutRenderer();
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001E2AD File Offset: 0x0001C4AD
		protected override void CloseLayoutRenderer()
		{
			this._assemblyName = null;
			base.CloseLayoutRenderer();
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001E2BC File Offset: 0x0001C4BC
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (this._assemblyName == null)
			{
				string formattingString = AppDomainLayoutRenderer.GetFormattingString(this.Format);
				this._assemblyName = string.Format(formattingString, this._currentDomain.Id, this._currentDomain.FriendlyName);
			}
			builder.Append(this._assemblyName);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0001E314 File Offset: 0x0001C514
		private static string GetFormattingString(string format)
		{
			string text;
			if (format.Equals("Long", StringComparison.OrdinalIgnoreCase))
			{
				text = "{0:0000}:{1}";
			}
			else if (format.Equals("Short", StringComparison.OrdinalIgnoreCase))
			{
				text = "{0:00}";
			}
			else
			{
				text = format;
			}
			return text;
		}

		// Token: 0x040002C0 RID: 704
		private const string ShortFormat = "{0:00}";

		// Token: 0x040002C1 RID: 705
		private const string LongFormat = "{0:0000}:{1}";

		// Token: 0x040002C2 RID: 706
		private const string LongFormatCode = "Long";

		// Token: 0x040002C3 RID: 707
		private const string ShortFormatCode = "Short";

		// Token: 0x040002C4 RID: 708
		private readonly IAppDomain _currentDomain;

		// Token: 0x040002C6 RID: 710
		private string _assemblyName;
	}
}
