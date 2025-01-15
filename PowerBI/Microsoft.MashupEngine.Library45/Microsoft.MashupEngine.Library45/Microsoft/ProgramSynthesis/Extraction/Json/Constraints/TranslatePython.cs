using System;
using Microsoft.ProgramSynthesis.Extraction.Json.Learning;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Translation.Python;
using Microsoft.ProgramSynthesis.Wrangling.Constraints;
using Microsoft.ProgramSynthesis.Wrangling.Schema.TableOutput;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Constraints
{
	// Token: 0x02000BAA RID: 2986
	public class TranslatePython : Constraint<string, ITable<string>>, IOptionConstraint<SynthesisOptions>
	{
		// Token: 0x06004BEB RID: 19435 RVA: 0x000EF02B File Offset: 0x000ED22B
		public TranslatePython(PythonTarget target)
		{
			this._target = target;
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x000EF03A File Offset: 0x000ED23A
		public void SetOptions(SynthesisOptions options)
		{
			options.TranslationTargets.Add(TargetLanguage.Python);
			options.PythonTargets.Add(this._target);
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x000EF05C File Offset: 0x000ED25C
		public override bool Equals(Constraint<string, ITable<string>> other)
		{
			TranslatePython translatePython = other as TranslatePython;
			return translatePython != null && translatePython._target == this._target;
		}

		// Token: 0x06004BEE RID: 19438 RVA: 0x0000FA11 File Offset: 0x0000DC11
		public override bool ConflictsWith(Constraint<string, ITable<string>> other)
		{
			return false;
		}

		// Token: 0x06004BEF RID: 19439 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override bool Valid(Program<string, ITable<string>> program)
		{
			return true;
		}

		// Token: 0x06004BF0 RID: 19440 RVA: 0x000EF084 File Offset: 0x000ED284
		public override int GetHashCode()
		{
			return this._target.GetHashCode();
		}

		// Token: 0x06004BF1 RID: 19441 RVA: 0x000EF01D File Offset: 0x000ED21D
		public override bool Equals(object obj)
		{
			return object.Equals(this, obj as Constraint<string, ITable<string>>);
		}

		// Token: 0x0400220E RID: 8718
		private readonly PythonTarget _target;
	}
}
