using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012DA RID: 4826
	public class ColumnNameCleaning : Constraint<string, ITable<string>>, IOptionConstraint<Options>
	{
		// Token: 0x06009199 RID: 37273 RVA: 0x001EB069 File Offset: 0x001E9269
		public ColumnNameCleaning(ColumnNameCleaningType cleaningType)
		{
			this.CleaningType = cleaningType;
		}

		// Token: 0x1700190A RID: 6410
		// (get) Token: 0x0600919A RID: 37274 RVA: 0x001EB078 File Offset: 0x001E9278
		public ColumnNameCleaningType CleaningType { get; }

		// Token: 0x0600919B RID: 37275 RVA: 0x001EB080 File Offset: 0x001E9280
		public void SetOptions(Options options)
		{
			options.ColumnNameCleaning = this.CleaningType;
		}

		// Token: 0x0600919C RID: 37276 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x0600919D RID: 37277 RVA: 0x001EB090 File Offset: 0x001E9290
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			ColumnNameCleaning columnNameCleaning = other as ColumnNameCleaning;
			return columnNameCleaning != null && this.CleaningType != columnNameCleaning.CleaningType;
		}

		// Token: 0x0600919E RID: 37278 RVA: 0x001EB0BC File Offset: 0x001E92BC
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			ColumnNameCleaning columnNameCleaning = other as ColumnNameCleaning;
			return columnNameCleaning != null && this.CleaningType == columnNameCleaning.CleaningType;
		}

		// Token: 0x0600919F RID: 37279 RVA: 0x001EB0E4 File Offset: 0x001E92E4
		public override int GetHashCode()
		{
			return 499 ^ this.CleaningType.GetHashCode();
		}
	}
}
