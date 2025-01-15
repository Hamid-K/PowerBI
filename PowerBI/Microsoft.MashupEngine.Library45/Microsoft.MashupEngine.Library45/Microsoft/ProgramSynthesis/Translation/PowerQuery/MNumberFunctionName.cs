using System;

namespace Microsoft.ProgramSynthesis.Translation.PowerQuery
{
	// Token: 0x0200032D RID: 813
	public readonly struct MNumberFunctionName
	{
		// Token: 0x060011F1 RID: 4593 RVA: 0x00034FFC File Offset: 0x000331FC
		public MNumberFunctionName(string name)
		{
			this.Name = name;
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060011F2 RID: 4594 RVA: 0x00035005 File Offset: 0x00033205
		public string Name { get; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x0003500D File Offset: 0x0003320D
		public string QualifiedName
		{
			get
			{
				return "Number." + this.Name;
			}
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0003501F File Offset: 0x0003321F
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00035027 File Offset: 0x00033227
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00035034 File Offset: 0x00033234
		public override bool Equals(object obj)
		{
			return obj is MNumberFunctionName && ((MNumberFunctionName)obj).Name == this.Name;
		}

		// Token: 0x040008E1 RID: 2273
		public static readonly MNumberFunctionName Round = new MNumberFunctionName("Round");

		// Token: 0x040008E2 RID: 2274
		public static readonly MNumberFunctionName RoundDown = new MNumberFunctionName("RoundDown");

		// Token: 0x040008E3 RID: 2275
		public static readonly MNumberFunctionName RoundUp = new MNumberFunctionName("RoundUp");
	}
}
