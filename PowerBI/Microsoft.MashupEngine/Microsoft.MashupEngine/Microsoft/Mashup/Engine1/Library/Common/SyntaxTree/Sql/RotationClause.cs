using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011F3 RID: 4595
	internal abstract class RotationClause : IScriptable
	{
		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x06007926 RID: 31014 RVA: 0x001A329A File Offset: 0x001A149A
		// (set) Token: 0x06007927 RID: 31015 RVA: 0x001A32A2 File Offset: 0x001A14A2
		public Alias AttributeColumn { get; set; }

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x06007928 RID: 31016 RVA: 0x001A32AB File Offset: 0x001A14AB
		// (set) Token: 0x06007929 RID: 31017 RVA: 0x001A32B3 File Offset: 0x001A14B3
		public IEnumerable<Alias> PivotValues { get; set; }

		// Token: 0x0600792A RID: 31018
		public abstract void WriteCreateScript(ScriptWriter writer);
	}
}
