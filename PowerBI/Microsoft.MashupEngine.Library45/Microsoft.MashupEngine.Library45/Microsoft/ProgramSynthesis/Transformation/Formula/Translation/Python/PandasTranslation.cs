using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x0200183F RID: 6207
	public class PandasTranslation : FormulaTranslation
	{
		// Token: 0x0600CB5F RID: 52063 RVA: 0x002B71E0 File Offset: 0x002B53E0
		internal PandasTranslation(Program program, PythonProgram pythonProgram, IPandasTranslationOptions translationOptions, TranslationMeta meta)
			: base(program, pythonProgram, TargetLanguage.Pandas, meta)
		{
			this._pythonProgram = pythonProgram;
			this._translationOptions = translationOptions;
		}

		// Token: 0x17002249 RID: 8777
		// (get) Token: 0x0600CB60 RID: 52064 RVA: 0x002B71FC File Offset: 0x002B53FC
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
						PythonDefinition pythonDefinition = pythonProgram.Definitions.FirstOrDefault((PythonDefinition d) => d.Name == this._translationOptions.DerivedColumnName);
						enumerable = ((pythonDefinition != null) ? pythonDefinition.Parameters : null);
					}
					readOnlyList = (this._parameters = enumerable.Select((PythonVariable parameter) => new PythonFormulaParameter(parameter)).ToList<PythonFormulaParameter>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600CB61 RID: 52065 RVA: 0x002B7274 File Offset: 0x002B5474
		public string ToString(uint indentLevel, uint indentSize)
		{
			PythonProgram pythonProgram = this._pythonProgram;
			if (pythonProgram == null)
			{
				return null;
			}
			return pythonProgram.ToString(indentLevel, indentSize);
		}

		// Token: 0x0600CB62 RID: 52066 RVA: 0x002B728C File Offset: 0x002B548C
		public override string ToString()
		{
			string text;
			if ((text = this._toStringDefault) == null)
			{
				text = (this._toStringDefault = this.ToString(this._translationOptions.IndentLevel, this._translationOptions.IndentSize));
			}
			return text;
		}

		// Token: 0x04004FCE RID: 20430
		private IReadOnlyList<PythonFormulaParameter> _parameters;

		// Token: 0x04004FCF RID: 20431
		private readonly PythonProgram _pythonProgram;

		// Token: 0x04004FD0 RID: 20432
		private string _toStringDefault;

		// Token: 0x04004FD1 RID: 20433
		private readonly IPandasTranslationOptions _translationOptions;
	}
}
