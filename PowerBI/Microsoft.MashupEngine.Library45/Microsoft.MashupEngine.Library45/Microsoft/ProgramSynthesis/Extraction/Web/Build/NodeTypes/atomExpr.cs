using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x02001078 RID: 4216
	public struct atomExpr : IProgramNodeBuilder, IEquatable<atomExpr>
	{
		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06007E13 RID: 32275 RVA: 0x001A87FA File Offset: 0x001A69FA
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007E14 RID: 32276 RVA: 0x001A8802 File Offset: 0x001A6A02
		private atomExpr(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007E15 RID: 32277 RVA: 0x001A880B File Offset: 0x001A6A0B
		public static atomExpr CreateUnsafe(ProgramNode node)
		{
			return new atomExpr(node);
		}

		// Token: 0x06007E16 RID: 32278 RVA: 0x001A8814 File Offset: 0x001A6A14
		public static atomExpr? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.atomExpr)
			{
				return null;
			}
			return new atomExpr?(atomExpr.CreateUnsafe(node));
		}

		// Token: 0x06007E17 RID: 32279 RVA: 0x001A884E File Offset: 0x001A6A4E
		public static atomExpr CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new atomExpr(new Hole(g.Symbol.atomExpr, holeId));
		}

		// Token: 0x06007E18 RID: 32280 RVA: 0x001A8866 File Offset: 0x001A6A66
		public bool Is_ContainsDate(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ContainsDate;
		}

		// Token: 0x06007E19 RID: 32281 RVA: 0x001A8880 File Offset: 0x001A6A80
		public bool Is_ContainsDate(GrammarBuilders g, out ContainsDate value)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsDate)
			{
				value = ContainsDate.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ContainsDate);
			return false;
		}

		// Token: 0x06007E1A RID: 32282 RVA: 0x001A88B8 File Offset: 0x001A6AB8
		public ContainsDate? As_ContainsDate(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ContainsDate)
			{
				return null;
			}
			return new ContainsDate?(ContainsDate.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E1B RID: 32283 RVA: 0x001A88F8 File Offset: 0x001A6AF8
		public ContainsDate Cast_ContainsDate(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsDate)
			{
				return ContainsDate.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ContainsDate is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E1C RID: 32284 RVA: 0x001A894D File Offset: 0x001A6B4D
		public bool Is_ContainsNum(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ContainsNum;
		}

		// Token: 0x06007E1D RID: 32285 RVA: 0x001A8967 File Offset: 0x001A6B67
		public bool Is_ContainsNum(GrammarBuilders g, out ContainsNum value)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsNum)
			{
				value = ContainsNum.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ContainsNum);
			return false;
		}

		// Token: 0x06007E1E RID: 32286 RVA: 0x001A899C File Offset: 0x001A6B9C
		public ContainsNum? As_ContainsNum(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ContainsNum)
			{
				return null;
			}
			return new ContainsNum?(ContainsNum.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E1F RID: 32287 RVA: 0x001A89DC File Offset: 0x001A6BDC
		public ContainsNum Cast_ContainsNum(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsNum)
			{
				return ContainsNum.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ContainsNum is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E20 RID: 32288 RVA: 0x001A8A31 File Offset: 0x001A6C31
		public bool Is_ID_substring(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ID_substring;
		}

		// Token: 0x06007E21 RID: 32289 RVA: 0x001A8A4B File Offset: 0x001A6C4B
		public bool Is_ID_substring(GrammarBuilders g, out ID_substring value)
		{
			if (this.Node.GrammarRule == g.Rule.ID_substring)
			{
				value = ID_substring.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ID_substring);
			return false;
		}

		// Token: 0x06007E22 RID: 32290 RVA: 0x001A8A80 File Offset: 0x001A6C80
		public ID_substring? As_ID_substring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ID_substring)
			{
				return null;
			}
			return new ID_substring?(ID_substring.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E23 RID: 32291 RVA: 0x001A8AC0 File Offset: 0x001A6CC0
		public ID_substring Cast_ID_substring(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ID_substring)
			{
				return ID_substring.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ID_substring is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E24 RID: 32292 RVA: 0x001A8B15 File Offset: 0x001A6D15
		public bool Is_Class(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.Class;
		}

		// Token: 0x06007E25 RID: 32293 RVA: 0x001A8B2F File Offset: 0x001A6D2F
		public bool Is_Class(GrammarBuilders g, out Class value)
		{
			if (this.Node.GrammarRule == g.Rule.Class)
			{
				value = Class.CreateUnsafe(this.Node);
				return true;
			}
			value = default(Class);
			return false;
		}

		// Token: 0x06007E26 RID: 32294 RVA: 0x001A8B64 File Offset: 0x001A6D64
		public Class? As_Class(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.Class)
			{
				return null;
			}
			return new Class?(Class.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E27 RID: 32295 RVA: 0x001A8BA4 File Offset: 0x001A6DA4
		public Class Cast_Class(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.Class)
			{
				return Class.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_Class is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E28 RID: 32296 RVA: 0x001A8BF9 File Offset: 0x001A6DF9
		public bool Is_TitleIs(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.TitleIs;
		}

		// Token: 0x06007E29 RID: 32297 RVA: 0x001A8C13 File Offset: 0x001A6E13
		public bool Is_TitleIs(GrammarBuilders g, out TitleIs value)
		{
			if (this.Node.GrammarRule == g.Rule.TitleIs)
			{
				value = TitleIs.CreateUnsafe(this.Node);
				return true;
			}
			value = default(TitleIs);
			return false;
		}

		// Token: 0x06007E2A RID: 32298 RVA: 0x001A8C48 File Offset: 0x001A6E48
		public TitleIs? As_TitleIs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.TitleIs)
			{
				return null;
			}
			return new TitleIs?(TitleIs.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E2B RID: 32299 RVA: 0x001A8C88 File Offset: 0x001A6E88
		public TitleIs Cast_TitleIs(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.TitleIs)
			{
				return TitleIs.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_TitleIs is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E2C RID: 32300 RVA: 0x001A8CDD File Offset: 0x001A6EDD
		public bool Is_NodeName(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NodeName;
		}

		// Token: 0x06007E2D RID: 32301 RVA: 0x001A8CF7 File Offset: 0x001A6EF7
		public bool Is_NodeName(GrammarBuilders g, out NodeName value)
		{
			if (this.Node.GrammarRule == g.Rule.NodeName)
			{
				value = NodeName.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NodeName);
			return false;
		}

		// Token: 0x06007E2E RID: 32302 RVA: 0x001A8D2C File Offset: 0x001A6F2C
		public NodeName? As_NodeName(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NodeName)
			{
				return null;
			}
			return new NodeName?(NodeName.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E2F RID: 32303 RVA: 0x001A8D6C File Offset: 0x001A6F6C
		public NodeName Cast_NodeName(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NodeName)
			{
				return NodeName.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NodeName is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E30 RID: 32304 RVA: 0x001A8DC1 File Offset: 0x001A6FC1
		public bool Is_NodeNames(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NodeNames;
		}

		// Token: 0x06007E31 RID: 32305 RVA: 0x001A8DDB File Offset: 0x001A6FDB
		public bool Is_NodeNames(GrammarBuilders g, out NodeNames value)
		{
			if (this.Node.GrammarRule == g.Rule.NodeNames)
			{
				value = NodeNames.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NodeNames);
			return false;
		}

		// Token: 0x06007E32 RID: 32306 RVA: 0x001A8E10 File Offset: 0x001A7010
		public NodeNames? As_NodeNames(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NodeNames)
			{
				return null;
			}
			return new NodeNames?(NodeNames.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E33 RID: 32307 RVA: 0x001A8E50 File Offset: 0x001A7050
		public NodeNames Cast_NodeNames(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NodeNames)
			{
				return NodeNames.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NodeNames is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E34 RID: 32308 RVA: 0x001A8EA5 File Offset: 0x001A70A5
		public bool Is_NthChild(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NthChild;
		}

		// Token: 0x06007E35 RID: 32309 RVA: 0x001A8EBF File Offset: 0x001A70BF
		public bool Is_NthChild(GrammarBuilders g, out NthChild value)
		{
			if (this.Node.GrammarRule == g.Rule.NthChild)
			{
				value = NthChild.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NthChild);
			return false;
		}

		// Token: 0x06007E36 RID: 32310 RVA: 0x001A8EF4 File Offset: 0x001A70F4
		public NthChild? As_NthChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NthChild)
			{
				return null;
			}
			return new NthChild?(NthChild.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E37 RID: 32311 RVA: 0x001A8F34 File Offset: 0x001A7134
		public NthChild Cast_NthChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NthChild)
			{
				return NthChild.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NthChild is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E38 RID: 32312 RVA: 0x001A8F89 File Offset: 0x001A7189
		public bool Is_NthLastChild(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.NthLastChild;
		}

		// Token: 0x06007E39 RID: 32313 RVA: 0x001A8FA3 File Offset: 0x001A71A3
		public bool Is_NthLastChild(GrammarBuilders g, out NthLastChild value)
		{
			if (this.Node.GrammarRule == g.Rule.NthLastChild)
			{
				value = NthLastChild.CreateUnsafe(this.Node);
				return true;
			}
			value = default(NthLastChild);
			return false;
		}

		// Token: 0x06007E3A RID: 32314 RVA: 0x001A8FD8 File Offset: 0x001A71D8
		public NthLastChild? As_NthLastChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.NthLastChild)
			{
				return null;
			}
			return new NthLastChild?(NthLastChild.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E3B RID: 32315 RVA: 0x001A9018 File Offset: 0x001A7218
		public NthLastChild Cast_NthLastChild(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.NthLastChild)
			{
				return NthLastChild.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_NthLastChild is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E3C RID: 32316 RVA: 0x001A906D File Offset: 0x001A726D
		public bool Is_ContainsLeafNodes(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ContainsLeafNodes;
		}

		// Token: 0x06007E3D RID: 32317 RVA: 0x001A9087 File Offset: 0x001A7287
		public bool Is_ContainsLeafNodes(GrammarBuilders g, out ContainsLeafNodes value)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsLeafNodes)
			{
				value = ContainsLeafNodes.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ContainsLeafNodes);
			return false;
		}

		// Token: 0x06007E3E RID: 32318 RVA: 0x001A90BC File Offset: 0x001A72BC
		public ContainsLeafNodes? As_ContainsLeafNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ContainsLeafNodes)
			{
				return null;
			}
			return new ContainsLeafNodes?(ContainsLeafNodes.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E3F RID: 32319 RVA: 0x001A90FC File Offset: 0x001A72FC
		public ContainsLeafNodes Cast_ContainsLeafNodes(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ContainsLeafNodes)
			{
				return ContainsLeafNodes.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ContainsLeafNodes is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E40 RID: 32320 RVA: 0x001A9151 File Offset: 0x001A7351
		public bool Is_ChildrenCount(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.ChildrenCount;
		}

		// Token: 0x06007E41 RID: 32321 RVA: 0x001A916B File Offset: 0x001A736B
		public bool Is_ChildrenCount(GrammarBuilders g, out ChildrenCount value)
		{
			if (this.Node.GrammarRule == g.Rule.ChildrenCount)
			{
				value = ChildrenCount.CreateUnsafe(this.Node);
				return true;
			}
			value = default(ChildrenCount);
			return false;
		}

		// Token: 0x06007E42 RID: 32322 RVA: 0x001A91A0 File Offset: 0x001A73A0
		public ChildrenCount? As_ChildrenCount(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.ChildrenCount)
			{
				return null;
			}
			return new ChildrenCount?(ChildrenCount.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E43 RID: 32323 RVA: 0x001A91E0 File Offset: 0x001A73E0
		public ChildrenCount Cast_ChildrenCount(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.ChildrenCount)
			{
				return ChildrenCount.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_ChildrenCount is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E44 RID: 32324 RVA: 0x001A9235 File Offset: 0x001A7435
		public bool Is_HasAttribute(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.HasAttribute;
		}

		// Token: 0x06007E45 RID: 32325 RVA: 0x001A924F File Offset: 0x001A744F
		public bool Is_HasAttribute(GrammarBuilders g, out HasAttribute value)
		{
			if (this.Node.GrammarRule == g.Rule.HasAttribute)
			{
				value = HasAttribute.CreateUnsafe(this.Node);
				return true;
			}
			value = default(HasAttribute);
			return false;
		}

		// Token: 0x06007E46 RID: 32326 RVA: 0x001A9284 File Offset: 0x001A7484
		public HasAttribute? As_HasAttribute(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.HasAttribute)
			{
				return null;
			}
			return new HasAttribute?(HasAttribute.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E47 RID: 32327 RVA: 0x001A92C4 File Offset: 0x001A74C4
		public HasAttribute Cast_HasAttribute(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.HasAttribute)
			{
				return HasAttribute.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_HasAttribute is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E48 RID: 32328 RVA: 0x001A9319 File Offset: 0x001A7519
		public bool Is_HasStyle(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.HasStyle;
		}

		// Token: 0x06007E49 RID: 32329 RVA: 0x001A9333 File Offset: 0x001A7533
		public bool Is_HasStyle(GrammarBuilders g, out HasStyle value)
		{
			if (this.Node.GrammarRule == g.Rule.HasStyle)
			{
				value = HasStyle.CreateUnsafe(this.Node);
				return true;
			}
			value = default(HasStyle);
			return false;
		}

		// Token: 0x06007E4A RID: 32330 RVA: 0x001A9368 File Offset: 0x001A7568
		public HasStyle? As_HasStyle(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.HasStyle)
			{
				return null;
			}
			return new HasStyle?(HasStyle.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E4B RID: 32331 RVA: 0x001A93A8 File Offset: 0x001A75A8
		public HasStyle Cast_HasStyle(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.HasStyle)
			{
				return HasStyle.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_HasStyle is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E4C RID: 32332 RVA: 0x001A93FD File Offset: 0x001A75FD
		public bool Is_HasEntityAnchor(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.Rule.HasEntityAnchor;
		}

		// Token: 0x06007E4D RID: 32333 RVA: 0x001A9417 File Offset: 0x001A7617
		public bool Is_HasEntityAnchor(GrammarBuilders g, out HasEntityAnchor value)
		{
			if (this.Node.GrammarRule == g.Rule.HasEntityAnchor)
			{
				value = HasEntityAnchor.CreateUnsafe(this.Node);
				return true;
			}
			value = default(HasEntityAnchor);
			return false;
		}

		// Token: 0x06007E4E RID: 32334 RVA: 0x001A944C File Offset: 0x001A764C
		public HasEntityAnchor? As_HasEntityAnchor(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.Rule.HasEntityAnchor)
			{
				return null;
			}
			return new HasEntityAnchor?(HasEntityAnchor.CreateUnsafe(this.Node));
		}

		// Token: 0x06007E4F RID: 32335 RVA: 0x001A948C File Offset: 0x001A768C
		public HasEntityAnchor Cast_HasEntityAnchor(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.Rule.HasEntityAnchor)
			{
				return HasEntityAnchor.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_HasEntityAnchor is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007E50 RID: 32336 RVA: 0x001A94E4 File Offset: 0x001A76E4
		public T Switch<T>(GrammarBuilders g, Func<ContainsDate, T> func0, Func<ContainsNum, T> func1, Func<ID_substring, T> func2, Func<Class, T> func3, Func<TitleIs, T> func4, Func<NodeName, T> func5, Func<NodeNames, T> func6, Func<NthChild, T> func7, Func<NthLastChild, T> func8, Func<ContainsLeafNodes, T> func9, Func<ChildrenCount, T> func10, Func<HasAttribute, T> func11, Func<HasStyle, T> func12, Func<HasEntityAnchor, T> func13)
		{
			ContainsDate containsDate;
			if (this.Is_ContainsDate(g, out containsDate))
			{
				return func0(containsDate);
			}
			ContainsNum containsNum;
			if (this.Is_ContainsNum(g, out containsNum))
			{
				return func1(containsNum);
			}
			ID_substring id_substring;
			if (this.Is_ID_substring(g, out id_substring))
			{
				return func2(id_substring);
			}
			Class @class;
			if (this.Is_Class(g, out @class))
			{
				return func3(@class);
			}
			TitleIs titleIs;
			if (this.Is_TitleIs(g, out titleIs))
			{
				return func4(titleIs);
			}
			NodeName nodeName;
			if (this.Is_NodeName(g, out nodeName))
			{
				return func5(nodeName);
			}
			NodeNames nodeNames;
			if (this.Is_NodeNames(g, out nodeNames))
			{
				return func6(nodeNames);
			}
			NthChild nthChild;
			if (this.Is_NthChild(g, out nthChild))
			{
				return func7(nthChild);
			}
			NthLastChild nthLastChild;
			if (this.Is_NthLastChild(g, out nthLastChild))
			{
				return func8(nthLastChild);
			}
			ContainsLeafNodes containsLeafNodes;
			if (this.Is_ContainsLeafNodes(g, out containsLeafNodes))
			{
				return func9(containsLeafNodes);
			}
			ChildrenCount childrenCount;
			if (this.Is_ChildrenCount(g, out childrenCount))
			{
				return func10(childrenCount);
			}
			HasAttribute hasAttribute;
			if (this.Is_HasAttribute(g, out hasAttribute))
			{
				return func11(hasAttribute);
			}
			HasStyle hasStyle;
			if (this.Is_HasStyle(g, out hasStyle))
			{
				return func12(hasStyle);
			}
			HasEntityAnchor hasEntityAnchor;
			if (this.Is_HasEntityAnchor(g, out hasEntityAnchor))
			{
				return func13(hasEntityAnchor);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol atomExpr");
		}

		// Token: 0x06007E51 RID: 32337 RVA: 0x001A9638 File Offset: 0x001A7838
		public void Switch(GrammarBuilders g, Action<ContainsDate> func0, Action<ContainsNum> func1, Action<ID_substring> func2, Action<Class> func3, Action<TitleIs> func4, Action<NodeName> func5, Action<NodeNames> func6, Action<NthChild> func7, Action<NthLastChild> func8, Action<ContainsLeafNodes> func9, Action<ChildrenCount> func10, Action<HasAttribute> func11, Action<HasStyle> func12, Action<HasEntityAnchor> func13)
		{
			ContainsDate containsDate;
			if (this.Is_ContainsDate(g, out containsDate))
			{
				func0(containsDate);
				return;
			}
			ContainsNum containsNum;
			if (this.Is_ContainsNum(g, out containsNum))
			{
				func1(containsNum);
				return;
			}
			ID_substring id_substring;
			if (this.Is_ID_substring(g, out id_substring))
			{
				func2(id_substring);
				return;
			}
			Class @class;
			if (this.Is_Class(g, out @class))
			{
				func3(@class);
				return;
			}
			TitleIs titleIs;
			if (this.Is_TitleIs(g, out titleIs))
			{
				func4(titleIs);
				return;
			}
			NodeName nodeName;
			if (this.Is_NodeName(g, out nodeName))
			{
				func5(nodeName);
				return;
			}
			NodeNames nodeNames;
			if (this.Is_NodeNames(g, out nodeNames))
			{
				func6(nodeNames);
				return;
			}
			NthChild nthChild;
			if (this.Is_NthChild(g, out nthChild))
			{
				func7(nthChild);
				return;
			}
			NthLastChild nthLastChild;
			if (this.Is_NthLastChild(g, out nthLastChild))
			{
				func8(nthLastChild);
				return;
			}
			ContainsLeafNodes containsLeafNodes;
			if (this.Is_ContainsLeafNodes(g, out containsLeafNodes))
			{
				func9(containsLeafNodes);
				return;
			}
			ChildrenCount childrenCount;
			if (this.Is_ChildrenCount(g, out childrenCount))
			{
				func10(childrenCount);
				return;
			}
			HasAttribute hasAttribute;
			if (this.Is_HasAttribute(g, out hasAttribute))
			{
				func11(hasAttribute);
				return;
			}
			HasStyle hasStyle;
			if (this.Is_HasStyle(g, out hasStyle))
			{
				func12(hasStyle);
				return;
			}
			HasEntityAnchor hasEntityAnchor;
			if (this.Is_HasEntityAnchor(g, out hasEntityAnchor))
			{
				func13(hasEntityAnchor);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol atomExpr");
		}

		// Token: 0x06007E52 RID: 32338 RVA: 0x001A9789 File Offset: 0x001A7989
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007E53 RID: 32339 RVA: 0x001A979C File Offset: 0x001A799C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007E54 RID: 32340 RVA: 0x001A97C6 File Offset: 0x001A79C6
		public bool Equals(atomExpr other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003391 RID: 13201
		private ProgramNode _node;
	}
}
