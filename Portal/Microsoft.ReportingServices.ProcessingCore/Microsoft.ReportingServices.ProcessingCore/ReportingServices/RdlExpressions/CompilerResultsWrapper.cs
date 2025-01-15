using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.RdlExpressions
{
	// Token: 0x0200055F RID: 1375
	internal class CompilerResultsWrapper
	{
		// Token: 0x06004A2E RID: 18990 RVA: 0x001396B2 File Offset: 0x001378B2
		public CompilerResultsWrapper(CompilerResults compilerResults)
		{
			this.NativeCompilerReturnValue = compilerResults.NativeCompilerReturnValue;
			this.Errors = compilerResults.Errors;
			this.Output = compilerResults.Output;
		}

		// Token: 0x06004A2F RID: 18991 RVA: 0x001396DE File Offset: 0x001378DE
		public CompilerResultsWrapper(int nativeCompilerErrorCode, CompilerErrorCollection errors, StringCollection output)
		{
			this.NativeCompilerReturnValue = nativeCompilerErrorCode;
			this.Errors = errors;
			this.Output = output;
		}

		// Token: 0x17001DF0 RID: 7664
		// (get) Token: 0x06004A30 RID: 18992 RVA: 0x001396FB File Offset: 0x001378FB
		// (set) Token: 0x06004A31 RID: 18993 RVA: 0x00139703 File Offset: 0x00137903
		public CompilerErrorCollection Errors { get; private set; }

		// Token: 0x17001DF1 RID: 7665
		// (get) Token: 0x06004A32 RID: 18994 RVA: 0x0013970C File Offset: 0x0013790C
		// (set) Token: 0x06004A33 RID: 18995 RVA: 0x00139714 File Offset: 0x00137914
		public StringCollection Output { get; private set; }

		// Token: 0x17001DF2 RID: 7666
		// (get) Token: 0x06004A34 RID: 18996 RVA: 0x0013971D File Offset: 0x0013791D
		// (set) Token: 0x06004A35 RID: 18997 RVA: 0x00139725 File Offset: 0x00137925
		public int NativeCompilerReturnValue { get; private set; }
	}
}
