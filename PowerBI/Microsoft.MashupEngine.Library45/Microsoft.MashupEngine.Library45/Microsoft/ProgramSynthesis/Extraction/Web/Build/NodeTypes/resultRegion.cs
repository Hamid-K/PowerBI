using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.UnnamedConversionNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes
{
	// Token: 0x0200105C RID: 4188
	public struct resultRegion : IProgramNodeBuilder, IEquatable<resultRegion>
	{
		// Token: 0x17001645 RID: 5701
		// (get) Token: 0x06007C73 RID: 31859 RVA: 0x001A5012 File Offset: 0x001A3212
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06007C74 RID: 31860 RVA: 0x001A501A File Offset: 0x001A321A
		private resultRegion(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06007C75 RID: 31861 RVA: 0x001A5023 File Offset: 0x001A3223
		public static resultRegion CreateUnsafe(ProgramNode node)
		{
			return new resultRegion(node);
		}

		// Token: 0x06007C76 RID: 31862 RVA: 0x001A502C File Offset: 0x001A322C
		public static resultRegion? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.resultRegion)
			{
				return null;
			}
			return new resultRegion?(resultRegion.CreateUnsafe(node));
		}

		// Token: 0x06007C77 RID: 31863 RVA: 0x001A5066 File Offset: 0x001A3266
		public static resultRegion CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new resultRegion(new Hole(g.Symbol.resultRegion, holeId));
		}

		// Token: 0x06007C78 RID: 31864 RVA: 0x001A507E File Offset: 0x001A327E
		public bool Is_resultRegion_subNode(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.resultRegion_subNode;
		}

		// Token: 0x06007C79 RID: 31865 RVA: 0x001A5098 File Offset: 0x001A3298
		public bool Is_resultRegion_subNode(GrammarBuilders g, out resultRegion_subNode value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultRegion_subNode)
			{
				value = resultRegion_subNode.CreateUnsafe(this.Node);
				return true;
			}
			value = default(resultRegion_subNode);
			return false;
		}

		// Token: 0x06007C7A RID: 31866 RVA: 0x001A50D0 File Offset: 0x001A32D0
		public resultRegion_subNode? As_resultRegion_subNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.resultRegion_subNode)
			{
				return null;
			}
			return new resultRegion_subNode?(resultRegion_subNode.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C7B RID: 31867 RVA: 0x001A5110 File Offset: 0x001A3310
		public resultRegion_subNode Cast_resultRegion_subNode(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultRegion_subNode)
			{
				return resultRegion_subNode.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_resultRegion_subNode is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007C7C RID: 31868 RVA: 0x001A5165 File Offset: 0x001A3365
		public bool Is_resultRegion_region(GrammarBuilders g)
		{
			return this.Node.GrammarRule == g.UnnamedConversion.resultRegion_region;
		}

		// Token: 0x06007C7D RID: 31869 RVA: 0x001A517F File Offset: 0x001A337F
		public bool Is_resultRegion_region(GrammarBuilders g, out resultRegion_region value)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultRegion_region)
			{
				value = resultRegion_region.CreateUnsafe(this.Node);
				return true;
			}
			value = default(resultRegion_region);
			return false;
		}

		// Token: 0x06007C7E RID: 31870 RVA: 0x001A51B4 File Offset: 0x001A33B4
		public resultRegion_region? As_resultRegion_region(GrammarBuilders g)
		{
			if (this.Node.GrammarRule != g.UnnamedConversion.resultRegion_region)
			{
				return null;
			}
			return new resultRegion_region?(resultRegion_region.CreateUnsafe(this.Node));
		}

		// Token: 0x06007C7F RID: 31871 RVA: 0x001A51F4 File Offset: 0x001A33F4
		public resultRegion_region Cast_resultRegion_region(GrammarBuilders g)
		{
			if (this.Node.GrammarRule == g.UnnamedConversion.resultRegion_region)
			{
				return resultRegion_region.CreateUnsafe(this.Node);
			}
			throw new InvalidOperationException("Cast_resultRegion_region is not valid on a " + this.Node.GrammarRule.Id + " node");
		}

		// Token: 0x06007C80 RID: 31872 RVA: 0x001A524C File Offset: 0x001A344C
		public T Switch<T>(GrammarBuilders g, Func<resultRegion_subNode, T> func0, Func<resultRegion_region, T> func1)
		{
			resultRegion_subNode resultRegion_subNode;
			if (this.Is_resultRegion_subNode(g, out resultRegion_subNode))
			{
				return func0(resultRegion_subNode);
			}
			resultRegion_region resultRegion_region;
			if (this.Is_resultRegion_region(g, out resultRegion_region))
			{
				return func1(resultRegion_region);
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultRegion");
		}

		// Token: 0x06007C81 RID: 31873 RVA: 0x001A52A4 File Offset: 0x001A34A4
		public void Switch(GrammarBuilders g, Action<resultRegion_subNode> func0, Action<resultRegion_region> func1)
		{
			resultRegion_subNode resultRegion_subNode;
			if (this.Is_resultRegion_subNode(g, out resultRegion_subNode))
			{
				func0(resultRegion_subNode);
				return;
			}
			resultRegion_region resultRegion_region;
			if (this.Is_resultRegion_region(g, out resultRegion_region))
			{
				func1(resultRegion_region);
				return;
			}
			throw new InvalidOperationException("Rule " + this.Node.GrammarRule.Id + " is not valid for a Switch operation from symbol resultRegion");
		}

		// Token: 0x06007C82 RID: 31874 RVA: 0x001A52FB File Offset: 0x001A34FB
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06007C83 RID: 31875 RVA: 0x001A5310 File Offset: 0x001A3510
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06007C84 RID: 31876 RVA: 0x001A533A File Offset: 0x001A353A
		public bool Equals(resultRegion other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04003375 RID: 13173
		private ProgramNode _node;
	}
}
