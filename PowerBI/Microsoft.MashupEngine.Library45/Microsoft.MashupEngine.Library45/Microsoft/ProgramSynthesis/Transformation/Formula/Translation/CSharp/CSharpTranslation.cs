using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.CSharp
{
	// Token: 0x0200197E RID: 6526
	public class CSharpTranslation : FormulaTranslation
	{
		// Token: 0x0600D5AC RID: 54700 RVA: 0x002D866B File Offset: 0x002D686B
		internal CSharpTranslation(Program program, CSharpProgram csharpProgram, TranslationMeta meta)
			: base(program, csharpProgram, TargetLanguage.CSharp, meta)
		{
			this._csharpProgram = csharpProgram;
		}

		// Token: 0x17002360 RID: 9056
		// (get) Token: 0x0600D5AD RID: 54701 RVA: 0x002D8680 File Offset: 0x002D6880
		public IReadOnlyList<CSharpFormulaParameter> Parameters
		{
			get
			{
				IReadOnlyList<CSharpFormulaParameter> readOnlyList;
				if ((readOnlyList = this._parameters) == null)
				{
					CSharpProgram csharpProgram = this._csharpProgram;
					IEnumerable<CSharpMethodParameter> enumerable;
					if (csharpProgram == null)
					{
						enumerable = null;
					}
					else
					{
						CSharpMethod csharpMethod = csharpProgram.Methods.FirstOrDefault<CSharpMethod>();
						enumerable = ((csharpMethod != null) ? csharpMethod.Parameters : null);
					}
					readOnlyList = (this._parameters = enumerable.Select((CSharpMethodParameter parameter) => new CSharpFormulaParameter
					{
						Name = parameter.Name,
						Type = parameter.Type
					}).ToList<CSharpFormulaParameter>());
				}
				return readOnlyList;
			}
		}

		// Token: 0x17002361 RID: 9057
		// (get) Token: 0x0600D5AE RID: 54702 RVA: 0x000D9110 File Offset: 0x000D7310
		public override TargetLanguage Target
		{
			get
			{
				return TargetLanguage.CSharp;
			}
		}

		// Token: 0x040051D7 RID: 20951
		private readonly CSharpProgram _csharpProgram;

		// Token: 0x040051D8 RID: 20952
		private IReadOnlyList<CSharpFormulaParameter> _parameters;
	}
}
