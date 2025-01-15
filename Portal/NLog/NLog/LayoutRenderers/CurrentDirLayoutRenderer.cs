using System;
using System.IO;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000BB RID: 187
	[LayoutRenderer("currentdir")]
	[ThreadAgnostic]
	[ThreadSafe]
	public class CurrentDirLayoutRenderer : LayoutRenderer, IStringValueRenderer
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x0001EBD2 File Offset: 0x0001CDD2
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x0001EBDA File Offset: 0x0001CDDA
		public string File { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0001EBE3 File Offset: 0x0001CDE3
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x0001EBEB File Offset: 0x0001CDEB
		public string Dir { get; set; }

		// Token: 0x06000BDF RID: 3039 RVA: 0x0001EBF4 File Offset: 0x0001CDF4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			builder.Append(this.GetStringValue());
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x0001EC03 File Offset: 0x0001CE03
		string IStringValueRenderer.GetFormattedString(LogEventInfo logEvent)
		{
			return this.GetStringValue();
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0001EC0B File Offset: 0x0001CE0B
		private string GetStringValue()
		{
			return PathHelpers.CombinePaths(Directory.GetCurrentDirectory(), this.Dir, this.File);
		}
	}
}
