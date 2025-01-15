using System;
using AngleSharp.Dom;

namespace AngleSharp.Commands
{
	// Token: 0x02000408 RID: 1032
	public interface ICommand
	{
		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060020F8 RID: 8440
		string CommandId { get; }

		// Token: 0x060020F9 RID: 8441
		bool Execute(IDocument document, bool showUserInterface, string value);

		// Token: 0x060020FA RID: 8442
		bool IsEnabled(IDocument document);

		// Token: 0x060020FB RID: 8443
		bool IsIndeterminate(IDocument document);

		// Token: 0x060020FC RID: 8444
		bool IsExecuted(IDocument document);

		// Token: 0x060020FD RID: 8445
		bool IsSupported(IDocument document);

		// Token: 0x060020FE RID: 8446
		string GetValue(IDocument document);
	}
}
