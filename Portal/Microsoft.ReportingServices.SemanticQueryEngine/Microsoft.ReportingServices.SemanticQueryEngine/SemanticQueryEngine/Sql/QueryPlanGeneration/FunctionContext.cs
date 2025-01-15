using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Modeling;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration
{
	// Token: 0x0200005E RID: 94
	internal sealed class FunctionContext : Stack<FunctionContext.Frame>
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00013B0D File Offset: 0x00011D0D
		internal FunctionContext()
		{
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00013B18 File Offset: 0x00011D18
		internal static FunctionContext CreateForFilterCondition(Expression filterCondition)
		{
			FunctionNode functionNode = new FunctionNode(FunctionName.Filter, new Expression[]
			{
				new Expression(FunctionContext.ZeroEntityKeyLiteral),
				filterCondition
			});
			FunctionContext functionContext = new FunctionContext();
			functionContext.Push(new FunctionContext.Frame(functionNode));
			functionContext.Current.IsInBooleanArgument = true;
			return functionContext;
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00013B61 File Offset: 0x00011D61
		internal FunctionContext.Frame Current
		{
			[DebuggerStepThrough]
			get
			{
				if (base.Count <= 0)
				{
					return null;
				}
				return base.Peek();
			}
		}

		// Token: 0x040001ED RID: 493
		private static readonly LiteralNode ZeroEntityKeyLiteral = new LiteralNode(EntityKey.FromBase64String("AA=="));

		// Token: 0x020000D4 RID: 212
		internal sealed class Frame
		{
			// Token: 0x0600075E RID: 1886 RVA: 0x0001C8C4 File Offset: 0x0001AAC4
			internal Frame(FunctionNode fNode)
			{
				if (fNode == null)
				{
					throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("fNode"));
				}
				this.FunctionNode = fNode;
			}

			// Token: 0x040003B4 RID: 948
			internal readonly FunctionNode FunctionNode;

			// Token: 0x040003B5 RID: 949
			internal bool IsInBooleanArgument;
		}
	}
}
