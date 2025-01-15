using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001893 RID: 6291
	public class PythonTranslation : FormulaTranslation
	{
		// Token: 0x0600CDCF RID: 52687 RVA: 0x002BF457 File Offset: 0x002BD657
		internal PythonTranslation(Program program, PythonProgram pythonProgram, IPythonTranslationOptions translationOptions, TranslationMeta meta)
			: base(program, pythonProgram, TargetLanguage.Python, meta)
		{
			this._pythonProgram = pythonProgram;
			this._translationOptions = translationOptions;
		}

		// Token: 0x17002292 RID: 8850
		// (get) Token: 0x0600CDD0 RID: 52688 RVA: 0x002BF474 File Offset: 0x002BD674
		public IReadOnlyList<PythonFormulaParameter> Parameters
		{
			get
			{
				IReadOnlyList<PythonFormulaParameter> readOnlyList;
				if ((readOnlyList = this._parameters) == null)
				{
					PythonDefinition pythonDefinition = this._pythonProgram.Definitions.FirstOrDefault((PythonDefinition d) => d.Name == this._translationOptions.DefinitionName);
					readOnlyList = (this._parameters = ((pythonDefinition != null) ? pythonDefinition.Parameters : null).Select((PythonVariable parameter) => new PythonFormulaParameter(parameter)).ToList<PythonFormulaParameter>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600CDD1 RID: 52689 RVA: 0x002BF4E5 File Offset: 0x002BD6E5
		public string ToString(uint indentLevel, uint indentSize)
		{
			PythonProgram pythonProgram = this._pythonProgram;
			if (pythonProgram == null)
			{
				return null;
			}
			return pythonProgram.ToString(indentLevel, indentSize);
		}

		// Token: 0x0600CDD2 RID: 52690 RVA: 0x002BF4FC File Offset: 0x002BD6FC
		public override string ToString()
		{
			string text;
			if ((text = this._toStringDefault) == null)
			{
				text = (this._toStringDefault = this.ToString(this._translationOptions.IndentLevel, this._translationOptions.IndentSize));
			}
			return text;
		}

		// Token: 0x04005068 RID: 20584
		private IReadOnlyList<PythonFormulaParameter> _parameters;

		// Token: 0x04005069 RID: 20585
		private readonly PythonProgram _pythonProgram;

		// Token: 0x0400506A RID: 20586
		private string _toStringDefault;

		// Token: 0x0400506B RID: 20587
		private readonly IPythonTranslationOptions _translationOptions;
	}
}
