using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x0200032E RID: 814
	public readonly struct MCsvFunctionName
	{
		// Token: 0x060011F8 RID: 4600 RVA: 0x00035093 File Offset: 0x00033293
		public MCsvFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x0003509C File Offset: 0x0003329C
		public string Name { get; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x000350A4 File Offset: 0x000332A4
		public string QualifiedName
		{
			get
			{
				return "Csv." + this.Name;
			}
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x000350B6 File Offset: 0x000332B6
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x000350BE File Offset: 0x000332BE
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x000350CC File Offset: 0x000332CC
		public override bool Equals(object obj)
		{
			return obj is MCsvFunctionName && ((MCsvFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008E5 RID: 2277
		public static readonly MCsvFunctionName Document = new MCsvFunctionName("Document");
	}
}
