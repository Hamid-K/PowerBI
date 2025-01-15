using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C2F RID: 7215
	public struct LetSharedDateTimeFormat : IProgramNodeBuilder, IEquatable<LetSharedDateTimeFormat>
	{
		// Token: 0x170028B5 RID: 10421
		// (get) Token: 0x0600F30C RID: 62220 RVA: 0x00341EB6 File Offset: 0x003400B6
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F30D RID: 62221 RVA: 0x00341EBE File Offset: 0x003400BE
		private LetSharedDateTimeFormat(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F30E RID: 62222 RVA: 0x00341EC7 File Offset: 0x003400C7
		public static LetSharedDateTimeFormat CreateUnsafe(ProgramNode node)
		{
			return new LetSharedDateTimeFormat(node);
		}

		// Token: 0x0600F30F RID: 62223 RVA: 0x00341ED0 File Offset: 0x003400D0
		public static LetSharedDateTimeFormat? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSharedDateTimeFormat)
			{
				return null;
			}
			return new LetSharedDateTimeFormat?(LetSharedDateTimeFormat.CreateUnsafe(node));
		}

		// Token: 0x0600F310 RID: 62224 RVA: 0x00341F05 File Offset: 0x00340105
		public LetSharedDateTimeFormat(GrammarBuilders g, outputDtFormat value0, dtRangeString value1)
		{
			this._node = new LetNode(g.Rule.LetSharedDateTimeFormat, value0.Node, value1.Node);
		}

		// Token: 0x0600F311 RID: 62225 RVA: 0x00341F2B File Offset: 0x0034012B
		public static implicit operator _LetB1(LetSharedDateTimeFormat arg)
		{
			return _LetB1.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028B6 RID: 10422
		// (get) Token: 0x0600F312 RID: 62226 RVA: 0x00341F39 File Offset: 0x00340139
		public outputDtFormat outputDtFormat
		{
			get
			{
				return outputDtFormat.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028B7 RID: 10423
		// (get) Token: 0x0600F313 RID: 62227 RVA: 0x00341F4D File Offset: 0x0034014D
		public dtRangeString dtRangeString
		{
			get
			{
				return dtRangeString.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F314 RID: 62228 RVA: 0x00341F61 File Offset: 0x00340161
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F315 RID: 62229 RVA: 0x00341F74 File Offset: 0x00340174
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F316 RID: 62230 RVA: 0x00341F9E File Offset: 0x0034019E
		public bool Equals(LetSharedDateTimeFormat other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B1E RID: 23326
		private ProgramNode _node;
	}
}
