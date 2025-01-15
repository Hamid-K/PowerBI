using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C50 RID: 7248
	public struct castToNumber : IProgramNodeBuilder, IEquatable<castToNumber>
	{
		// Token: 0x170028E6 RID: 10470
		// (get) Token: 0x0600F52A RID: 62762 RVA: 0x003472BE File Offset: 0x003454BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F52B RID: 62763 RVA: 0x003472C6 File Offset: 0x003454C6
		private castToNumber(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F52C RID: 62764 RVA: 0x003472CF File Offset: 0x003454CF
		public static castToNumber CreateUnsafe(ProgramNode node)
		{
			return new castToNumber(node);
		}

		// Token: 0x0600F52D RID: 62765 RVA: 0x003472D8 File Offset: 0x003454D8
		public static castToNumber? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.castToNumber)
			{
				return null;
			}
			return new castToNumber?(castToNumber.CreateUnsafe(node));
		}

		// Token: 0x0600F52E RID: 62766 RVA: 0x00347312 File Offset: 0x00345512
		public static castToNumber CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new castToNumber(new Hole(g.Symbol.castToNumber, holeId));
		}

		// Token: 0x0600F52F RID: 62767 RVA: 0x0034732A File Offset: 0x0034552A
		public AsDecimal Cast_AsDecimal()
		{
			return AsDecimal.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F530 RID: 62768 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_AsDecimal(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F531 RID: 62769 RVA: 0x00347337 File Offset: 0x00345537
		public bool Is_AsDecimal(GrammarBuilders g, out AsDecimal value)
		{
			value = AsDecimal.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F532 RID: 62770 RVA: 0x0034734B File Offset: 0x0034554B
		public AsDecimal? As_AsDecimal(GrammarBuilders g)
		{
			return new AsDecimal?(AsDecimal.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F533 RID: 62771 RVA: 0x0034735D File Offset: 0x0034555D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F534 RID: 62772 RVA: 0x00347370 File Offset: 0x00345570
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F535 RID: 62773 RVA: 0x0034739A File Offset: 0x0034559A
		public bool Equals(castToNumber other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B3F RID: 23359
		private ProgramNode _node;
	}
}
