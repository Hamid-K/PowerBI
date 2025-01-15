using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.TracingGlobals;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000650 RID: 1616
	public class DecisionTreeAndNode : DecisionTreeNode
	{
		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000B6AFD File Offset: 0x000B4CFD
		public override string Name
		{
			get
			{
				if (this.notted)
				{
					return "!AND";
				}
				return "AND";
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06003605 RID: 13829 RVA: 0x000B6B12 File Offset: 0x000B4D12
		// (set) Token: 0x06003606 RID: 13830 RVA: 0x000B6B1A File Offset: 0x000B4D1A
		public List<DecisionTreeNode> Children
		{
			get
			{
				return this.children;
			}
			protected set
			{
				this.children = value;
			}
		}

		// Token: 0x06003607 RID: 13831 RVA: 0x000B6B24 File Offset: 0x000B4D24
		public DecisionTreeAndNode(DecisionTree tree, XmlNode node)
			: base(tree, node, false)
		{
			if (!node.HasChildNodes)
			{
				throw new TraceException(SR.AndOrNodesHaveChildren);
			}
			int num = (this.notted ? 1 : 0);
			if (node.Attributes.Count != num)
			{
				throw new TraceException(SR.AndOrNodesNotAttribute);
			}
			this.children = new List<DecisionTreeNode>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				DecisionTreeNode decisionTreeNode = DecisionTreeNode.CreateNode(base.DecisionTree, xmlNode);
				this.children.Add(decisionTreeNode);
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000B6BE0 File Offset: 0x000B4DE0
		public override DecisionTreeNode AddPastedInfo(XmlNode decisionTreeNode)
		{
			DecisionTreeNode decisionTreeNode2 = DecisionTreeNode.CreateNode(base.DecisionTree, decisionTreeNode);
			this.AddChild(decisionTreeNode2);
			return decisionTreeNode2;
		}

		// Token: 0x06003609 RID: 13833 RVA: 0x000B6C04 File Offset: 0x000B4E04
		protected override EvaluationResult InternalEvaluate()
		{
			foreach (DecisionTreeNode decisionTreeNode in this.children)
			{
				EvaluationResult evaluationResult = decisionTreeNode.Evaluate();
				if (evaluationResult == EvaluationResult.Indeterminate)
				{
					return evaluationResult;
				}
				if (evaluationResult == EvaluationResult.False)
				{
					return this.notted ? EvaluationResult.True : EvaluationResult.False;
				}
			}
			if (!this.notted)
			{
				return EvaluationResult.True;
			}
			return EvaluationResult.False;
		}

		// Token: 0x0600360A RID: 13834 RVA: 0x000B6C80 File Offset: 0x000B4E80
		public DecisionTreeAndNode(DecisionTree tree, bool useNot, List<DecisionTreeNode> newChildren)
			: base(tree, useNot, false)
		{
			this.children = newChildren;
			if (this.children != null)
			{
				foreach (DecisionTreeNode decisionTreeNode in this.children)
				{
					decisionTreeNode.SetParent(this);
				}
			}
		}

		// Token: 0x0600360B RID: 13835 RVA: 0x000B6CEC File Offset: 0x000B4EEC
		public override DecisionTreeNode AddEqualNode(int identifier, int integerValue, string stringValue, bool isInteger)
		{
			if (this.children == null)
			{
				this.children = new List<DecisionTreeNode>();
			}
			DecisionTreeEqualNode decisionTreeEqualNode = new DecisionTreeEqualNode(base.DecisionTree, identifier, false, integerValue, stringValue, isInteger);
			this.children.Add(decisionTreeEqualNode);
			decisionTreeEqualNode.SetParent(this);
			return decisionTreeEqualNode;
		}

		// Token: 0x0600360C RID: 13836 RVA: 0x000B6D32 File Offset: 0x000B4F32
		public override void AddChild(DecisionTreeNode newNode)
		{
			if (this.children == null)
			{
				this.children = new List<DecisionTreeNode>();
			}
			this.children.Add(newNode);
			newNode.SetParent(this);
		}

		// Token: 0x0600360D RID: 13837 RVA: 0x000B6D5A File Offset: 0x000B4F5A
		public override void RemoveThis(DecisionTreeNode child)
		{
			this.children.Remove(child);
		}

		// Token: 0x0600360E RID: 13838 RVA: 0x000B6D69 File Offset: 0x000B4F69
		protected override void RemoveChildren()
		{
			this.children = null;
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000B6D74 File Offset: 0x000B4F74
		public override DecisionTreeNode InternalClone(DecisionTree decisionTree)
		{
			List<DecisionTreeNode> list = new List<DecisionTreeNode>();
			foreach (DecisionTreeNode decisionTreeNode in this.children)
			{
				list.Add(decisionTreeNode.InternalClone(decisionTree));
			}
			return new DecisionTreeAndNode(decisionTree, this.notted, list);
		}

		// Token: 0x06003610 RID: 13840 RVA: 0x000B6DE0 File Offset: 0x000B4FE0
		public override string GenerateXml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.children != null)
			{
				foreach (DecisionTreeNode decisionTreeNode in this.children)
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
				return string.Format("<ands not=\"true\">{0}</ands>", stringBuilder.ToString());
			}
			return string.Format("<ands>{0}</ands>", stringBuilder.ToString());
		}

		// Token: 0x04001F30 RID: 7984
		private List<DecisionTreeNode> children;
	}
}
