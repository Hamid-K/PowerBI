using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000653 RID: 1619
	public class DecisionTreeOrNode : DecisionTreeAndNode
	{
		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06003635 RID: 13877 RVA: 0x000B750B File Offset: 0x000B570B
		public override string Name
		{
			get
			{
				if (this.notted)
				{
					return "!OR";
				}
				return "OR";
			}
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000B7520 File Offset: 0x000B5720
		public DecisionTreeOrNode(DecisionTree tree, XmlNode node)
			: base(tree, node)
		{
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000B752C File Offset: 0x000B572C
		protected override EvaluationResult InternalEvaluate()
		{
			bool flag = false;
			foreach (DecisionTreeNode decisionTreeNode in base.Children)
			{
				EvaluationResult evaluationResult = decisionTreeNode.Evaluate();
				if (evaluationResult == EvaluationResult.True)
				{
					return this.notted ? EvaluationResult.False : EvaluationResult.True;
				}
				if (evaluationResult == EvaluationResult.False)
				{
					flag = true;
				}
			}
			if (!flag)
			{
				return EvaluationResult.Indeterminate;
			}
			if (!this.notted)
			{
				return EvaluationResult.False;
			}
			return EvaluationResult.True;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000B75AC File Offset: 0x000B57AC
		public DecisionTreeOrNode(DecisionTree tree, bool useNot, List<DecisionTreeNode> newChildren)
			: base(tree, useNot, newChildren)
		{
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000B75B8 File Offset: 0x000B57B8
		public override DecisionTreeNode InternalClone(DecisionTree decisionTree)
		{
			List<DecisionTreeNode> list = new List<DecisionTreeNode>();
			foreach (DecisionTreeNode decisionTreeNode in base.Children)
			{
				list.Add(decisionTreeNode.InternalClone(decisionTree));
			}
			return new DecisionTreeOrNode(decisionTree, this.notted, list);
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000B7624 File Offset: 0x000B5824
		public override string GenerateXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.Children != null)
			{
				foreach (DecisionTreeNode decisionTreeNode in base.Children)
				{
					stringBuilder.Append(decisionTreeNode.GenerateXml());
				}
			}
			if (stringBuilder.Length == 0)
			{
				return null;
			}
			if (this.notted)
			{
				return string.Format("<ors not=\"true\">{0}</ors>", stringBuilder.ToString());
			}
			return string.Format("<ors>{0}</ors>", stringBuilder.ToString());
		}
	}
}
