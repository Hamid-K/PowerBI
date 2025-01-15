using System;

namespace Microsoft.MachineLearning.Data.Expr.Internal
{
	// Token: 0x0200018B RID: 395
	internal abstract class NodeVisitor
	{
		// Token: 0x060007EB RID: 2027
		public abstract void Visit(BoolLitNode node);

		// Token: 0x060007EC RID: 2028
		public abstract void Visit(StrLitNode node);

		// Token: 0x060007ED RID: 2029
		public abstract void Visit(NumLitNode node);

		// Token: 0x060007EE RID: 2030
		public abstract void Visit(NameNode node);

		// Token: 0x060007EF RID: 2031
		public abstract void Visit(IdentNode node);

		// Token: 0x060007F0 RID: 2032
		public abstract void Visit(ParamNode node);

		// Token: 0x060007F1 RID: 2033 RVA: 0x0002A5F0 File Offset: 0x000287F0
		public virtual bool PreVisit(LambdaNode node)
		{
			return true;
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0002A5F3 File Offset: 0x000287F3
		public virtual bool PreVisit(UnaryOpNode node)
		{
			return true;
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0002A5F6 File Offset: 0x000287F6
		public virtual bool PreVisit(BinaryOpNode node)
		{
			return true;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0002A5F9 File Offset: 0x000287F9
		public virtual bool PreVisit(ConditionalNode node)
		{
			return true;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0002A5FC File Offset: 0x000287FC
		public virtual bool PreVisit(CompareNode node)
		{
			return true;
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0002A5FF File Offset: 0x000287FF
		public virtual bool PreVisit(CallNode node)
		{
			return true;
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x0002A602 File Offset: 0x00028802
		public virtual bool PreVisit(ListNode node)
		{
			return true;
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x0002A605 File Offset: 0x00028805
		public virtual bool PreVisit(WithNode node)
		{
			return true;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002A608 File Offset: 0x00028808
		public virtual bool PreVisit(WithLocalNode node)
		{
			return true;
		}

		// Token: 0x060007FA RID: 2042
		public abstract void PostVisit(LambdaNode node);

		// Token: 0x060007FB RID: 2043
		public abstract void PostVisit(UnaryOpNode node);

		// Token: 0x060007FC RID: 2044
		public abstract void PostVisit(BinaryOpNode node);

		// Token: 0x060007FD RID: 2045
		public abstract void PostVisit(ConditionalNode node);

		// Token: 0x060007FE RID: 2046
		public abstract void PostVisit(CompareNode node);

		// Token: 0x060007FF RID: 2047
		public abstract void PostVisit(CallNode node);

		// Token: 0x06000800 RID: 2048
		public abstract void PostVisit(ListNode node);

		// Token: 0x06000801 RID: 2049
		public abstract void PostVisit(WithNode node);

		// Token: 0x06000802 RID: 2050
		public abstract void PostVisit(WithLocalNode node);
	}
}
