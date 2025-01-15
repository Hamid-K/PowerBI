using System;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008D7 RID: 2263
	public struct ParseSettings
	{
		// Token: 0x060030B3 RID: 12467 RVA: 0x0008F9DB File Offset: 0x0008DBDB
		public ParseSettings(DeserializationContext context, ConversionRule ruleForBackCompatParsing = null)
		{
			this.Context = context;
			this.RuleForBackCompatParsing = ruleForBackCompatParsing;
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x0008F9EB File Offset: 0x0008DBEB
		public readonly DeserializationContext Context { get; }

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x060030B5 RID: 12469 RVA: 0x0008F9F3 File Offset: 0x0008DBF3
		public readonly ConversionRule RuleForBackCompatParsing { get; }
	}
}
