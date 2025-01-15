using System;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008CB RID: 2251
	public class EvaluationFinishedEventArgs : EventArgs
	{
		// Token: 0x06003064 RID: 12388 RVA: 0x0008E8D4 File Offset: 0x0008CAD4
		public EvaluationFinishedEventArgs(ProgramNode program, State input, object result)
		{
			this.Program = program;
			this.Result = result;
			this.Input = input;
		}

		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x0008E8F1 File Offset: 0x0008CAF1
		public ProgramNode Program { get; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x0008E8F9 File Offset: 0x0008CAF9
		public State Input { get; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x0008E901 File Offset: 0x0008CB01
		public object Result { get; }
	}
}
