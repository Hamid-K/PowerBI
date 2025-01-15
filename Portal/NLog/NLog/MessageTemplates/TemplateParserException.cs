using System;

namespace NLog.MessageTemplates
{
	// Token: 0x0200008A RID: 138
	public class TemplateParserException : Exception
	{
		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00019822 File Offset: 0x00017A22
		public int Index { get; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x0001982A File Offset: 0x00017A2A
		public string Template { get; }

		// Token: 0x0600099B RID: 2459 RVA: 0x00019832 File Offset: 0x00017A32
		public TemplateParserException(string message, int index, string template)
			: base(message)
		{
			this.Index = index;
			this.Template = template;
		}
	}
}
