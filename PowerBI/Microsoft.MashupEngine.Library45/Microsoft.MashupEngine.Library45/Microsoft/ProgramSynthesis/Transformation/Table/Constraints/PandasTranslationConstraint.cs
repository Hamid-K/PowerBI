using System;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Transformation.Table.Constraints
{
	// Token: 0x02001B5D RID: 7005
	public class PandasTranslationConstraint : TranslationConstraint
	{
		// Token: 0x1700264D RID: 9805
		// (get) Token: 0x0600E5F5 RID: 58869 RVA: 0x0030B671 File Offset: 0x00309871
		// (set) Token: 0x0600E5F6 RID: 58870 RVA: 0x0030B679 File Offset: 0x00309879
		public string DataFrameName { get; set; } = "df";

		// Token: 0x1700264E RID: 9806
		// (get) Token: 0x0600E5F7 RID: 58871 RVA: 0x0030B682 File Offset: 0x00309882
		// (set) Token: 0x0600E5F8 RID: 58872 RVA: 0x0030B68A File Offset: 0x0030988A
		public string PandasAlias { get; set; } = "pd";

		// Token: 0x0600E5F9 RID: 58873 RVA: 0x0030B694 File Offset: 0x00309894
		public override bool Equals(Constraint<ITable<object>, ITable<object>> other)
		{
			PandasTranslationConstraint pandasTranslationConstraint = other as PandasTranslationConstraint;
			return pandasTranslationConstraint != null && this.DataFrameName == pandasTranslationConstraint.DataFrameName;
		}

		// Token: 0x0600E5FA RID: 58874 RVA: 0x0030B6BE File Offset: 0x003098BE
		public override int GetHashCode()
		{
			return 17 * 17 + this.DataFrameName.GetHashCode();
		}
	}
}
