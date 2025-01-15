using System;
using Microsoft.ProgramSynthesis.Utils.Geometry;

namespace Microsoft.ProgramSynthesis.Extraction.Spreadsheet.Semantics
{
	// Token: 0x02000EC0 RID: 3776
	public class DefinedRange
	{
		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x060066D3 RID: 26323 RVA: 0x0014F3A7 File Offset: 0x0014D5A7
		public Bounds<TableUnit> Span { get; }

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x060066D4 RID: 26324 RVA: 0x0014F3AF File Offset: 0x0014D5AF
		public string Kind { get; }

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x060066D5 RID: 26325 RVA: 0x0014F3B7 File Offset: 0x0014D5B7
		public bool Hidden { get; }

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x060066D6 RID: 26326 RVA: 0x0014F3BF File Offset: 0x0014D5BF
		public string Name { get; }

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x060066D7 RID: 26327 RVA: 0x0014F3C7 File Offset: 0x0014D5C7
		public bool InternalName { get; }

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x060066D8 RID: 26328 RVA: 0x0014F3CF File Offset: 0x0014D5CF
		public bool IsSpecialName
		{
			get
			{
				return this.Name.StartsWith("_xlnm.");
			}
		}

		// Token: 0x060066D9 RID: 26329 RVA: 0x0014F3E1 File Offset: 0x0014D5E1
		public DefinedRange(Bounds<TableUnit> span, string kind, bool hidden, string name, bool internalName)
		{
			this.Span = span;
			this.Kind = kind;
			this.Hidden = hidden;
			this.Name = name;
			this.InternalName = internalName;
		}
	}
}
