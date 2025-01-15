using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Json.Build.RuleNodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Json.Build.NodeTypes
{
	// Token: 0x02000B69 RID: 2921
	public struct sequenceBody : IProgramNodeBuilder, IEquatable<sequenceBody>
	{
		// Token: 0x17000D50 RID: 3408
		// (get) Token: 0x06004A07 RID: 18951 RVA: 0x000E92BE File Offset: 0x000E74BE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x06004A08 RID: 18952 RVA: 0x000E92C6 File Offset: 0x000E74C6
		private sequenceBody(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x06004A09 RID: 18953 RVA: 0x000E92CF File Offset: 0x000E74CF
		public static sequenceBody CreateUnsafe(ProgramNode node)
		{
			return new sequenceBody(node);
		}

		// Token: 0x06004A0A RID: 18954 RVA: 0x000E92D8 File Offset: 0x000E74D8
		public static sequenceBody? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.Symbol != g.Symbol.sequenceBody)
			{
				return null;
			}
			return new sequenceBody?(sequenceBody.CreateUnsafe(node));
		}

		// Token: 0x06004A0B RID: 18955 RVA: 0x000E9312 File Offset: 0x000E7512
		public static sequenceBody CreateHole(GrammarBuilders g, string holeId = null)
		{
			return new sequenceBody(new Hole(g.Symbol.sequenceBody, holeId));
		}

		// Token: 0x06004A0C RID: 18956 RVA: 0x000E932A File Offset: 0x000E752A
		public SequenceBody Cast_SequenceBody()
		{
			return SequenceBody.CreateUnsafe(this.Node);
		}

		// Token: 0x06004A0D RID: 18957 RVA: 0x0000A5FD File Offset: 0x000087FD
		public bool Is_SequenceBody(GrammarBuilders g)
		{
			return true;
		}

		// Token: 0x06004A0E RID: 18958 RVA: 0x000E9337 File Offset: 0x000E7537
		public bool Is_SequenceBody(GrammarBuilders g, out SequenceBody value)
		{
			value = SequenceBody.CreateUnsafe(this.Node);
			return true;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x000E934B File Offset: 0x000E754B
		public SequenceBody? As_SequenceBody(GrammarBuilders g)
		{
			return new SequenceBody?(SequenceBody.CreateUnsafe(this.Node));
		}

		// Token: 0x06004A10 RID: 18960 RVA: 0x000E935D File Offset: 0x000E755D
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x06004A11 RID: 18961 RVA: 0x000E9370 File Offset: 0x000E7570
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x06004A12 RID: 18962 RVA: 0x000E939A File Offset: 0x000E759A
		public bool Equals(sequenceBody other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04002164 RID: 8548
		private ProgramNode _node;
	}
}
