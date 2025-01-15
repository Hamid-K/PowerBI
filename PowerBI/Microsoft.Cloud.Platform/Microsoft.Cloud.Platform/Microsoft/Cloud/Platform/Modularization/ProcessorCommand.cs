using System;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000BE RID: 190
	public class ProcessorCommand
	{
		// Token: 0x06000574 RID: 1396 RVA: 0x00013EA4 File Offset: 0x000120A4
		public ProcessorCommand([NotNull] CommandFunction cmd, [NotNull] string helpText, [NotNull] string usageText)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<CommandFunction>(cmd, "cmd");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(helpText, "helpText");
			ExtendedDiagnostics.EnsureArgumentNotNull<string>(usageText, "usageText");
			this.m_cmdDelegate = cmd;
			this.m_helpString = helpText;
			this.m_usageString = usageText;
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00013EE2 File Offset: 0x000120E2
		public void Invoke(string[] args)
		{
			this.m_cmdDelegate(args);
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00013EF0 File Offset: 0x000120F0
		public virtual string Help
		{
			get
			{
				return this.m_helpString;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00013EF8 File Offset: 0x000120F8
		public virtual string Usage
		{
			get
			{
				return this.m_usageString;
			}
		}

		// Token: 0x040001DF RID: 479
		private readonly CommandFunction m_cmdDelegate;

		// Token: 0x040001E0 RID: 480
		private readonly string m_helpString;

		// Token: 0x040001E1 RID: 481
		private readonly string m_usageString;
	}
}
