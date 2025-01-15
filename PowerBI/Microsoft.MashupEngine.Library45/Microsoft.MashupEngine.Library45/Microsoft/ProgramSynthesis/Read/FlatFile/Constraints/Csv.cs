using System;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012DB RID: 4827
	public class Csv : CsvConstraint
	{
		// Token: 0x060091A0 RID: 37280 RVA: 0x001EB10B File Offset: 0x001E930B
		private Csv()
		{
		}

		// Token: 0x1700190B RID: 6411
		// (get) Token: 0x060091A1 RID: 37281 RVA: 0x001EB113 File Offset: 0x001E9313
		public static Csv Instance { get; } = new Csv();

		// Token: 0x060091A2 RID: 37282 RVA: 0x001EB11A File Offset: 0x001E931A
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			return other is Csv;
		}

		// Token: 0x060091A3 RID: 37283 RVA: 0x001EB125 File Offset: 0x001E9325
		public override int GetHashCode()
		{
			return 1093;
		}
	}
}
