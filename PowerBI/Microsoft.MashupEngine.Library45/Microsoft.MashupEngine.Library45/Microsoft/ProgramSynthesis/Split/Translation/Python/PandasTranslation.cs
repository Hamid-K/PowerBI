using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Split.Text;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation;
using Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Split.Translation.Python
{
	// Token: 0x02001405 RID: 5125
	public class PandasTranslation : SplitTranslation
	{
		// Token: 0x06009E4A RID: 40522 RVA: 0x00218ED0 File Offset: 0x002170D0
		internal PandasTranslation(SplitProgram program, PythonProgram pythonProgram, PandasTranslationConstraint translationConstraint, Metadata metadata)
			: base(program, pythonProgram, translationConstraint, metadata)
		{
			this._pythonProgram = pythonProgram;
			this._translationConstraint = translationConstraint;
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x06009E4B RID: 40523 RVA: 0x00218EEC File Offset: 0x002170EC
		public IReadOnlyList<PythonFormulaParameter> Parameters
		{
			get
			{
				IReadOnlyList<PythonFormulaParameter> readOnlyList;
				if ((readOnlyList = this._parameters) == null)
				{
					PythonProgram pythonProgram = this._pythonProgram;
					IEnumerable<PythonVariable> enumerable;
					if (pythonProgram == null)
					{
						enumerable = null;
					}
					else
					{
						PythonDefinition pythonDefinition = pythonProgram.Definitions.FirstOrDefault((PythonDefinition d) => d.Name == this._translationConstraint.InputColumnName);
						enumerable = ((pythonDefinition != null) ? pythonDefinition.Parameters : null);
					}
					readOnlyList = (this._parameters = enumerable.Select((PythonVariable parameter) => new PythonFormulaParameter(parameter)).ToList<PythonFormulaParameter>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06009E4C RID: 40524 RVA: 0x0000A5FD File Offset: 0x000087FD
		public override TargetLanguage Target
		{
			get
			{
				return TargetLanguage.Pandas;
			}
		}

		// Token: 0x06009E4D RID: 40525 RVA: 0x00218F64 File Offset: 0x00217164
		public string ToCodeString(uint indentLevel, uint indentSize)
		{
			PandasTranslationConstraint translationConstraint = this._translationConstraint;
			if (translationConstraint != null)
			{
				IFormulaBlock formulaBlock = base.TranslatedExpression as IFormulaBlock;
				if (formulaBlock != null)
				{
					return formulaBlock.ToString(translationConstraint.IndentLevel, translationConstraint.IndentSize).TrimEnd(Array.Empty<char>());
				}
			}
			return this._pythonProgram.ToString(indentLevel, indentSize).TrimEnd(Array.Empty<char>());
		}

		// Token: 0x06009E4E RID: 40526 RVA: 0x00218FBE File Offset: 0x002171BE
		protected override string ToCodeString()
		{
			return this.ToCodeString(this._translationConstraint.IndentLevel, this._translationConstraint.IndentSize);
		}

		// Token: 0x0400400C RID: 16396
		private IReadOnlyList<PythonFormulaParameter> _parameters;

		// Token: 0x0400400D RID: 16397
		private readonly PythonProgram _pythonProgram;

		// Token: 0x0400400E RID: 16398
		private readonly PandasTranslationConstraint _translationConstraint;
	}
}
