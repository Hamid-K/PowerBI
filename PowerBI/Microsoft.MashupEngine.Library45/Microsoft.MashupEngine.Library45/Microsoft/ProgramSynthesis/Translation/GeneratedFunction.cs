using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002D8 RID: 728
	public abstract class GeneratedFunction : IGeneratedFunction
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0002DBD6 File Offset: 0x0002BDD6
		public IReadOnlyList<Record<string, Type>> Parameters { get; }

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0002DBDE File Offset: 0x0002BDDE
		public Type ReturnType { get; }

		// Token: 0x06000FCD RID: 4045 RVA: 0x0000CC37 File Offset: 0x0000AE37
		public virtual void Optimize(IOptimizer optimizer)
		{
		}

		// Token: 0x06000FCE RID: 4046
		public abstract CodeForGeneratedFunction GenerateCode(string headerModuleName, OptimizeFor optimization);

		// Token: 0x06000FCF RID: 4047 RVA: 0x0002DBE6 File Offset: 0x0002BDE6
		protected GeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType)
		{
			this.Parameters = parameters.ToList<Record<string, Type>>();
			this.ReturnType = returnType;
		}
	}
}
