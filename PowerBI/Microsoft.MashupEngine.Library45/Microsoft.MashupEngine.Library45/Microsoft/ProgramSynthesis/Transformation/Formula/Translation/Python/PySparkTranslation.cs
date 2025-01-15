using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001846 RID: 6214
	public class PySparkTranslation : FormulaTranslation
	{
		// Token: 0x0600CB7D RID: 52093 RVA: 0x002B7E80 File Offset: 0x002B6080
		internal PySparkTranslation(Program program, PythonProgram pythonProgram, IPySparkTranslationOptions translationOptions, TranslationMeta meta)
			: base(program, pythonProgram, TargetLanguage.PySpark, meta)
		{
			this._pythonProgram = pythonProgram;
			this._translationOptions = translationOptions;
		}

		// Token: 0x1700224A RID: 8778
		// (get) Token: 0x0600CB7E RID: 52094 RVA: 0x002B7E9C File Offset: 0x002B609C
		public IReadOnlyList<PythonFormulaParameter> Parameters
		{
			get
			{
				IReadOnlyList<PythonFormulaParameter> readOnlyList;
				if ((readOnlyList = this._parameters) == null)
				{
					PythonDefinition pythonDefinition = this._pythonProgram.Definitions.FirstOrDefault((PythonDefinition d) => d.Name == this._translationOptions.DerivedColumnName);
					readOnlyList = (this._parameters = ((pythonDefinition != null) ? pythonDefinition.Parameters : null).Select((PythonVariable parameter) => new PythonFormulaParameter(parameter)).ToList<PythonFormulaParameter>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x0600CB7F RID: 52095 RVA: 0x002B7F0D File Offset: 0x002B610D
		public string ToString(uint indentLevel, uint indentSize)
		{
			PythonProgram pythonProgram = this._pythonProgram;
			return ((pythonProgram != null) ? pythonProgram.ToString(indentLevel, indentSize) : null) ?? string.Empty;
		}

		// Token: 0x0600CB80 RID: 52096 RVA: 0x002B7F2C File Offset: 0x002B612C
		public override string ToString()
		{
			string text;
			if ((text = this._toStringDefault) == null)
			{
				text = (this._toStringDefault = this.ToString(this._translationOptions.IndentLevel, this._translationOptions.IndentSize));
			}
			return text;
		}

		// Token: 0x04004FDF RID: 20447
		private IReadOnlyList<PythonFormulaParameter> _parameters;

		// Token: 0x04004FE0 RID: 20448
		private readonly PythonProgram _pythonProgram;

		// Token: 0x04004FE1 RID: 20449
		private string _toStringDefault;

		// Token: 0x04004FE2 RID: 20450
		private readonly IPySparkTranslationOptions _translationOptions;
	}
}
