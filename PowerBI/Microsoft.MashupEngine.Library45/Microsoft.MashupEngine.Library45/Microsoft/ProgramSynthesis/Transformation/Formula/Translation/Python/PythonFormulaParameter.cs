using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Translation.Python
{
	// Token: 0x02001895 RID: 6293
	public class PythonFormulaParameter
	{
		// Token: 0x0600CDD7 RID: 52695 RVA: 0x002BF55C File Offset: 0x002BD75C
		internal PythonFormulaParameter(PythonVariable variable)
		{
			this.Name = variable.Name;
			Type type = variable.Type;
			this.Type = ((type == typeof(int)) ? "int" : ((type == typeof(double)) ? "float" : ((type == typeof(decimal)) ? "Decimal" : ((type == typeof(string)) ? "string" : ((type == typeof(DateTime)) ? "datetime" : null)))));
		}

		// Token: 0x17002293 RID: 8851
		// (get) Token: 0x0600CDD8 RID: 52696 RVA: 0x002BF606 File Offset: 0x002BD806
		public string Name { get; }

		// Token: 0x17002294 RID: 8852
		// (get) Token: 0x0600CDD9 RID: 52697 RVA: 0x002BF60E File Offset: 0x002BD80E
		public string Type { get; }
	}
}
