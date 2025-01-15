using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Translation;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation
{
	// Token: 0x02001D8E RID: 7566
	internal class TransformationTextOpaqueGeneratedFunction : OpaqueGeneratedFunction, ITransformationTextGeneratedFunction, IGeneratedFunction
	{
		// Token: 0x17002A5B RID: 10843
		// (get) Token: 0x0600FE49 RID: 65097 RVA: 0x00364E24 File Offset: 0x00363024
		public IEnumerable<string> UsedColumns { get; }

		// Token: 0x0600FE4A RID: 65098 RVA: 0x00364E2C File Offset: 0x0036302C
		public TransformationTextOpaqueGeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType, IEnumerable<string> usedColumns, CodeBuilder staticCode, CodeBuilder dynamicCode)
			: base(parameters, returnType, staticCode, dynamicCode)
		{
			this.UsedColumns = usedColumns;
		}
	}
}
