using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Explanations
{
	// Token: 0x020019C0 RID: 6592
	public interface IExplanationTemplateProvider
	{
		// Token: 0x0600D73C RID: 55100
		string FormatDelimiter(string delimiter);

		// Token: 0x0600D73D RID: 55101
		string FormatReplacement(string key, object value);

		// Token: 0x0600D73E RID: 55102
		string Template(string key);
	}
}
