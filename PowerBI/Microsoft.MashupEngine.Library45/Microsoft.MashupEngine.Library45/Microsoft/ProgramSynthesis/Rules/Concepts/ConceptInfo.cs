using System;

namespace Microsoft.ProgramSynthesis.Rules.Concepts
{
	// Token: 0x020003B8 RID: 952
	public class ConceptInfo
	{
		// Token: 0x0600155C RID: 5468 RVA: 0x0003E805 File Offset: 0x0003CA05
		public ConceptInfo(string name, bool lazy, Type type)
		{
			this.Lazy = lazy;
			this.Type = type;
			this.Name = name;
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x0003E822 File Offset: 0x0003CA22
		public string Name { get; }

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x0600155E RID: 5470 RVA: 0x0003E82A File Offset: 0x0003CA2A
		public bool Lazy { get; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x0003E832 File Offset: 0x0003CA32
		public Type Type { get; }
	}
}
