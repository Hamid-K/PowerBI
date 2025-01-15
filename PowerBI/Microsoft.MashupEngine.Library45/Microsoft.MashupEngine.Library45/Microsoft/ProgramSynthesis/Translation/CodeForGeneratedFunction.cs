using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Translation
{
	// Token: 0x020002D0 RID: 720
	public class CodeForGeneratedFunction
	{
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0002D747 File Offset: 0x0002B947
		public CodeBuilder StaticCode { get; }

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0002D74F File Offset: 0x0002B94F
		public CodeBuilder DynamicCode { get; }

		// Token: 0x06000FAC RID: 4012 RVA: 0x0002D757 File Offset: 0x0002B957
		public CodeForGeneratedFunction(CodeBuilder staticCode, CodeBuilder dynamicCode)
		{
			this.StaticCode = staticCode;
			this.DynamicCode = dynamicCode;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0002D770 File Offset: 0x0002B970
		public static CodeForGeneratedFunction Concat(IEnumerable<CodeForGeneratedFunction> codes)
		{
			CodeBuilder codeBuilder = new CodeBuilder(4U);
			CodeBuilder codeBuilder2 = new CodeBuilder(4U);
			foreach (CodeForGeneratedFunction codeForGeneratedFunction in codes)
			{
				codeBuilder.Append(codeForGeneratedFunction.StaticCode);
				codeBuilder2.Append(codeForGeneratedFunction.DynamicCode);
			}
			return new CodeForGeneratedFunction(codeBuilder, codeBuilder2);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0002D7E0 File Offset: 0x0002B9E0
		public static CodeForGeneratedFunction Concat(params CodeForGeneratedFunction[] codes)
		{
			return CodeForGeneratedFunction.Concat(codes);
		}
	}
}
