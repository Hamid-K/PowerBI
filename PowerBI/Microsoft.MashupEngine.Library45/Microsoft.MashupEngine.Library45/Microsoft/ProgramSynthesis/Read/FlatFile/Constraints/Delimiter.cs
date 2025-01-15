using System;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Read.FlatFile.Constraints
{
	// Token: 0x020012DD RID: 4829
	public class Delimiter : CsvConstraint
	{
		// Token: 0x060091A9 RID: 37289 RVA: 0x001EB157 File Offset: 0x001E9357
		public Delimiter(string delimiterString)
		{
			if (delimiterString == null)
			{
				throw new ArgumentNullException("delimiterString");
			}
			this.DelimiterString = delimiterString;
		}

		// Token: 0x1700190C RID: 6412
		// (get) Token: 0x060091AA RID: 37290 RVA: 0x001EB174 File Offset: 0x001E9374
		public string DelimiterString { get; }

		// Token: 0x060091AB RID: 37291 RVA: 0x001EB17C File Offset: 0x001E937C
		public override void SetOptions(Options options)
		{
			base.SetOptions(options);
			options.Delimiter = this.DelimiterString;
		}

		// Token: 0x060091AC RID: 37292 RVA: 0x001EB194 File Offset: 0x001E9394
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			if (!base.ConflictsWith(other))
			{
				Delimiter delimiter = other as Delimiter;
				return delimiter != null && this.DelimiterString != delimiter.DelimiterString;
			}
			return true;
		}

		// Token: 0x060091AD RID: 37293 RVA: 0x001EB1CC File Offset: 0x001E93CC
		public override bool Valid(Program<string, ITable<string>> program)
		{
			CsvProgram csvProgram = program as CsvProgram;
			return csvProgram != null && csvProgram.Delimiter == this.DelimiterString;
		}

		// Token: 0x060091AE RID: 37294 RVA: 0x001EB1F8 File Offset: 0x001E93F8
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			Delimiter delimiter = other as Delimiter;
			return delimiter != null && this.DelimiterString == delimiter.DelimiterString;
		}

		// Token: 0x060091AF RID: 37295 RVA: 0x001EB222 File Offset: 0x001E9422
		public override int GetHashCode()
		{
			return 769 ^ this.DelimiterString.GetHashCode();
		}
	}
}
