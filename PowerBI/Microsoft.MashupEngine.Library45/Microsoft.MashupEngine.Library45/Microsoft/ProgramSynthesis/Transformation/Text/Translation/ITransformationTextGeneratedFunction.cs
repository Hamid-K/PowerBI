using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Translation;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation
{
	// Token: 0x02001D8D RID: 7565
	internal interface ITransformationTextGeneratedFunction : IGeneratedFunction
	{
		// Token: 0x17002A5A RID: 10842
		// (get) Token: 0x0600FE48 RID: 65096
		IEnumerable<string> UsedColumns { get; }
	}
}
