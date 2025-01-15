using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Build.RuleNodeTypes
{
	// Token: 0x02001C31 RID: 7217
	public struct LetSharedParsedDateTime : IProgramNodeBuilder, IEquatable<LetSharedParsedDateTime>
	{
		// Token: 0x170028BB RID: 10427
		// (get) Token: 0x0600F322 RID: 62242 RVA: 0x003420AE File Offset: 0x003402AE
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600F323 RID: 62243 RVA: 0x003420B6 File Offset: 0x003402B6
		private LetSharedParsedDateTime(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600F324 RID: 62244 RVA: 0x003420BF File Offset: 0x003402BF
		public static LetSharedParsedDateTime CreateUnsafe(ProgramNode node)
		{
			return new LetSharedParsedDateTime(node);
		}

		// Token: 0x0600F325 RID: 62245 RVA: 0x003420C8 File Offset: 0x003402C8
		public static LetSharedParsedDateTime? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.Rule.LetSharedParsedDateTime)
			{
				return null;
			}
			return new LetSharedParsedDateTime?(LetSharedParsedDateTime.CreateUnsafe(node));
		}

		// Token: 0x0600F326 RID: 62246 RVA: 0x003420FD File Offset: 0x003402FD
		public LetSharedParsedDateTime(GrammarBuilders g, inputDateTime value0, _LetB1 value1)
		{
			this._node = new LetNode(g.Rule.LetSharedParsedDateTime, value0.Node, value1.Node);
		}

		// Token: 0x0600F327 RID: 62247 RVA: 0x00342123 File Offset: 0x00340323
		public static implicit operator conv(LetSharedParsedDateTime arg)
		{
			return conv.CreateUnsafe(arg.Node);
		}

		// Token: 0x170028BC RID: 10428
		// (get) Token: 0x0600F328 RID: 62248 RVA: 0x00342131 File Offset: 0x00340331
		public inputDateTime inputDateTime
		{
			get
			{
				return inputDateTime.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x170028BD RID: 10429
		// (get) Token: 0x0600F329 RID: 62249 RVA: 0x00342145 File Offset: 0x00340345
		public _LetB1 _LetB1
		{
			get
			{
				return _LetB1.CreateUnsafe(this.Node.Children[1]);
			}
		}

		// Token: 0x0600F32A RID: 62250 RVA: 0x00342159 File Offset: 0x00340359
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600F32B RID: 62251 RVA: 0x0034216C File Offset: 0x0034036C
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600F32C RID: 62252 RVA: 0x00342196 File Offset: 0x00340396
		public bool Equals(LetSharedParsedDateTime other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x04005B20 RID: 23328
		private ProgramNode _node;
	}
}
