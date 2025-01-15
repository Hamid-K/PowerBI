using System;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.TracingGlobals;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000652 RID: 1618
	public abstract class DecisionTreeNode : BaseTraceTreeElement
	{
		// Token: 0x06003620 RID: 13856 RVA: 0x000B7394 File Offset: 0x000B5594
		public void SetParent(DecisionTreeNode parentDecisionTreeNode)
		{
			this.parent = parentDecisionTreeNode;
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06003621 RID: 13857
		public abstract string Name { get; }

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06003622 RID: 13858 RVA: 0x000B739D File Offset: 0x000B559D
		// (set) Token: 0x06003623 RID: 13859 RVA: 0x000B73A5 File Offset: 0x000B55A5
		public DecisionTree DecisionTree
		{
			get
			{
				return this.decisionTree;
			}
			protected set
			{
				this.decisionTree = value;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06003624 RID: 13860 RVA: 0x000B73AE File Offset: 0x000B55AE
		// (set) Token: 0x06003625 RID: 13861 RVA: 0x000B73B6 File Offset: 0x000B55B6
		public bool IsNotted
		{
			get
			{
				return this.notted;
			}
			set
			{
				this.notted = value;
			}
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000B73C0 File Offset: 0x000B55C0
		internal static DecisionTreeNode CreateNode(DecisionTree tree, XmlNode decisionXmlNode)
		{
			string name = decisionXmlNode.Name;
			if (name != null)
			{
				DecisionTreeNode decisionTreeNode;
				if (!(name == "equal"))
				{
					if (!(name == "ors"))
					{
						if (!(name == "ands"))
						{
							goto IL_0051;
						}
						decisionTreeNode = new DecisionTreeAndNode(tree, decisionXmlNode);
					}
					else
					{
						decisionTreeNode = new DecisionTreeOrNode(tree, decisionXmlNode);
					}
				}
				else
				{
					decisionTreeNode = new DecisionTreeEqualNode(tree, decisionXmlNode);
				}
				return decisionTreeNode;
			}
			IL_0051:
			throw new TraceException(SR.UnknownDecisionTreeNode(decisionXmlNode.Name));
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000189CC File Offset: 0x00016BCC
		public virtual DecisionTreeNode AddPastedInfo(XmlNode decisionTreeNode)
		{
			return null;
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000B7430 File Offset: 0x000B5630
		private void InternalConstructor(DecisionTree tree, bool useNot, bool valueSettable)
		{
			this.decisionTree = tree;
			this.traceContainer = this.decisionTree.Container;
			this.valueCanBeSet = valueSettable;
			this.isEvaluable = false;
			this.notted = useNot;
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000B7460 File Offset: 0x000B5660
		protected DecisionTreeNode(DecisionTree tree, XmlNode node, bool valueSettable)
		{
			bool flag = false;
			XmlAttribute xmlAttribute = node.Attributes["not"];
			if (xmlAttribute != null && string.Compare(xmlAttribute.Value, "true", StringComparison.InvariantCulture) == 0)
			{
				flag = true;
			}
			this.InternalConstructor(tree, flag, valueSettable);
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000B74A7 File Offset: 0x000B56A7
		protected DecisionTreeNode(DecisionTree tree, bool useNot, bool valueSettable)
		{
			this.InternalConstructor(tree, useNot, valueSettable);
		}

		// Token: 0x0600362B RID: 13867
		public abstract DecisionTreeNode AddEqualNode(int identifier, int integerValue, string stringValue, bool isInteger);

		// Token: 0x0600362C RID: 13868
		public abstract void AddChild(DecisionTreeNode newNode);

		// Token: 0x0600362D RID: 13869 RVA: 0x000B74B8 File Offset: 0x000B56B8
		public void RemoveAll()
		{
			if (this.parent == null)
			{
				this.decisionTree.RootRemoved();
			}
			else
			{
				this.parent.RemoveThis(this);
			}
			this.RemoveChildren();
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000036A9 File Offset: 0x000018A9
		public virtual void RemoveThis(DecisionTreeNode child)
		{
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void RemoveChildren()
		{
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000B74E1 File Offset: 0x000B56E1
		public EvaluationResult Evaluate()
		{
			if (this.valueCanBeSet && !this.isEvaluable)
			{
				return EvaluationResult.Indeterminate;
			}
			return this.InternalEvaluate();
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000B74FB File Offset: 0x000B56FB
		public void SetValue(object value)
		{
			this.InternalSetValue(value);
			this.isEvaluable = true;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000036A9 File Offset: 0x000018A9
		protected virtual void InternalSetValue(object value)
		{
		}

		// Token: 0x06003633 RID: 13875
		protected abstract EvaluationResult InternalEvaluate();

		// Token: 0x06003634 RID: 13876
		public abstract DecisionTreeNode InternalClone(DecisionTree decisionTree);

		// Token: 0x04001F37 RID: 7991
		private DecisionTree decisionTree;

		// Token: 0x04001F38 RID: 7992
		protected ITraceContainer traceContainer;

		// Token: 0x04001F39 RID: 7993
		protected bool notted;

		// Token: 0x04001F3A RID: 7994
		protected DecisionTreeNode parent;

		// Token: 0x04001F3B RID: 7995
		private bool isEvaluable;

		// Token: 0x04001F3C RID: 7996
		private bool valueCanBeSet;
	}
}
