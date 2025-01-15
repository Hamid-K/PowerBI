using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes
{
	// Token: 0x02001C4A RID: 7242
	public struct parsedDateTime : IProgramNodeBuilder, IEquatable<parsedDateTime>
	{
		// Token: 0x170028E0 RID: 10464
		// (get) Token: 0x0600F4BA RID: 62650 RVA: 0x00345FBE File Offset: 0x003441BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F4BB RID: 62651 RVA: 0x00345FC6 File Offset: 0x003441C6
		private parsedDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F4BC RID: 62652 RVA: 0x00345FCF File Offset: 0x003441CF
		public static parsedDateTime CreateUnsafe(ProgramNode node)
		{
			return new parsedDateTime(node);
		}

		// Token: 0x0600F4BD RID: 62653 RVA: 0x00345FD8 File Offset: 0x003441D8
		public static parsedDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.parsedDateTime)
			{
				return null;
			}
			return new parsedDateTime?(parsedDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F4BE RID: 62654 RVA: 0x00346012 File Offset: 0x00344212
		public static parsedDateTime CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new parsedDateTime(new Hole(g.Symbol.parsedDateTime, holeId));
		}

		// Token: 0x0600F4BF RID: 62655 RVA: 0x0034602A File Offset: 0x0034422A
		public ParsePartialDateTime Cast_ParsePartialDateTime()
		{
			return ParsePartialDateTime.CreateUnsafe(this.Node);
		}

		// Token: 0x0600F4C0 RID: 62656 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_ParsePartialDateTime(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x0600F4C1 RID: 62657 RVA: 0x00346037 File Offset: 0x00344237
		public bool Is_ParsePartialDateTime(GrammarBuilders g, out ParsePartialDateTime value)
		{
			value = ParsePartialDateTime.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x0600F4C2 RID: 62658 RVA: 0x0034604B File Offset: 0x0034424B
		public ParsePartialDateTime? As_ParsePartialDateTime(GrammarBuilders g)
		{
			return new ParsePartialDateTime?(ParsePartialDateTime.CreateUnsafe(this.Node));
		}

		// Token: 0x0600F4C3 RID: 62659 RVA: 0x0034605D File Offset: 0x0034425D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F4C4 RID: 62660 RVA: 0x00346070 File Offset: 0x00344270
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F4C5 RID: 62661 RVA: 0x0034609A File Offset: 0x0034429A
		public bool Equals(parsedDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B39 RID: 23353
		private ProgramNode _node;
	}
}
