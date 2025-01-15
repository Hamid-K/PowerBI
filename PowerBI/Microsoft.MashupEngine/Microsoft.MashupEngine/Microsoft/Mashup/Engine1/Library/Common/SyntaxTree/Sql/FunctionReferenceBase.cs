using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D5 RID: 4565
	internal abstract class FunctionReferenceBase : ScalarExpression
	{
		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06007886 RID: 30854 RVA: 0x0014025A File Offset: 0x0013E45A
		public static int FunctionParameterPrecedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06007887 RID: 30855 RVA: 0x001A156E File Offset: 0x0019F76E
		public IList<FunctionParameterValue> Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06007888 RID: 30856 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007889 RID: 30857 RVA: 0x001A1578 File Offset: 0x0019F778
		public void WriteParameters(ScriptWriter writer)
		{
			bool flag = false;
			foreach (SqlExpression sqlExpression in this.parameters)
			{
				flag = writer.WriteCommaIfNeeded(flag);
				sqlExpression.WriteCreateScript(writer);
			}
		}

		// Token: 0x040041B4 RID: 16820
		private readonly List<FunctionParameterValue> parameters = new List<FunctionParameterValue>();
	}
}
