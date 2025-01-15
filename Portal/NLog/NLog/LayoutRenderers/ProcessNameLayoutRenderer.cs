using System;
using System.ComponentModel;
using System.Text;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000E1 RID: 225
	[LayoutRenderer("processname")]
	[AppDomainFixedOutput]
	[ThreadAgnostic]
	[ThreadSafe]
	public class ProcessNameLayoutRenderer : LayoutRenderer
	{
		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00021AE3 File Offset: 0x0001FCE3
		// (set) Token: 0x06000D39 RID: 3385 RVA: 0x00021AEB File Offset: 0x0001FCEB
		[DefaultValue(false)]
		public bool FullName { get; set; }

		// Token: 0x06000D3A RID: 3386 RVA: 0x00021AF4 File Offset: 0x0001FCF4
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			string text = (this.FullName ? ProcessIDHelper.Instance.CurrentProcessFilePath : ProcessIDHelper.Instance.CurrentProcessBaseName);
			builder.Append(text);
		}
	}
}
