using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002E6 RID: 742
	public class OpaqueGeneratedFunction : GeneratedFunction
	{
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0002E45F File Offset: 0x0002C65F
		public CodeBuilder DynamicCode { get; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600100C RID: 4108 RVA: 0x0002E467 File Offset: 0x0002C667
		public CodeBuilder StaticCode { get; }

		// Token: 0x0600100D RID: 4109 RVA: 0x0002E46F File Offset: 0x0002C66F
		public OpaqueGeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType, CodeBuilder staticCode, CodeBuilder dynamicCode)
			: base(parameters, returnType)
		{
			this.StaticCode = staticCode;
			this.DynamicCode = dynamicCode;
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0002E488 File Offset: 0x0002C688
		public OpaqueGeneratedFunction(IEnumerable<Record<string, Type>> parameters, Type returnType, CodeBuilder dynamicCode)
			: this(parameters, returnType, new CodeBuilder(4U), dynamicCode)
		{
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0002E49C File Offset: 0x0002C69C
		public override CodeForGeneratedFunction GenerateCode(string headerModuleName, OptimizeFor optimization)
		{
			CodeForGeneratedFunction codeForGeneratedFunction;
			if ((codeForGeneratedFunction = this._returnCode) == null)
			{
				codeForGeneratedFunction = (this._returnCode = new CodeForGeneratedFunction(this.StaticCode, this.DynamicCode));
			}
			return codeForGeneratedFunction;
		}

		// Token: 0x040007CB RID: 1995
		private CodeForGeneratedFunction _returnCode;
	}
}
