using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Formula.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Build.UnnamedConversionNodeTypes
{
	// Token: 0x02001541 RID: 5441
	public struct parseSubject_letSubstring : IProgramNodeBuilder, IEquatable<parseSubject_letSubstring>
	{
		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x0600B181 RID: 45441 RVA: 0x0027094E File Offset: 0x0026EB4E
		public ProgramNode Node
		{
			get
			{
				return this._node;
			}
		}

		// Token: 0x0600B182 RID: 45442 RVA: 0x00270956 File Offset: 0x0026EB56
		private parseSubject_letSubstring(ProgramNode node)
		{
			this._node = node;
		}

		// Token: 0x0600B183 RID: 45443 RVA: 0x0027095F File Offset: 0x0026EB5F
		public static parseSubject_letSubstring CreateUnsafe(ProgramNode node)
		{
			return new parseSubject_letSubstring(node);
		}

		// Token: 0x0600B184 RID: 45444 RVA: 0x00270968 File Offset: 0x0026EB68
		public static parseSubject_letSubstring? CreateSafe(GrammarBuilders g, ProgramNode node)
		{
			if (node.GrammarRule != g.UnnamedConversion.parseSubject_letSubstring)
			{
				return null;
			}
			return new parseSubject_letSubstring?(parseSubject_letSubstring.CreateUnsafe(node));
		}

		// Token: 0x0600B185 RID: 45445 RVA: 0x0027099D File Offset: 0x0026EB9D
		public parseSubject_letSubstring(GrammarBuilders g, letSubstring value0)
		{
			this._node = g.UnnamedConversion.parseSubject_letSubstring.BuildASTNode(value0.Node);
		}

		// Token: 0x0600B186 RID: 45446 RVA: 0x002709BC File Offset: 0x0026EBBC
		public static implicit operator parseSubject(parseSubject_letSubstring arg)
		{
			return parseSubject.CreateUnsafe(arg.Node);
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x0600B187 RID: 45447 RVA: 0x002709CA File Offset: 0x0026EBCA
		public letSubstring letSubstring
		{
			get
			{
				return letSubstring.CreateUnsafe(this.Node.Children[0]);
			}
		}

		// Token: 0x0600B188 RID: 45448 RVA: 0x002709DE File Offset: 0x0026EBDE
		public override int GetHashCode()
		{
			ProgramNode node = this.Node;
			if (node == null)
			{
				return 0;
			}
			return node.GetHashCode();
		}

		// Token: 0x0600B189 RID: 45449 RVA: 0x002709F4 File Offset: 0x0026EBF4
		public override bool Equals(object other)
		{
			IProgramNodeBuilder programNodeBuilder = other as IProgramNodeBuilder;
			return programNodeBuilder != null && this.Node.Equals(programNodeBuilder.Node);
		}

		// Token: 0x0600B18A RID: 45450 RVA: 0x00270A1E File Offset: 0x0026EC1E
		public bool Equals(parseSubject_letSubstring other)
		{
			return this.Node.Equals(other.Node);
		}

		// Token: 0x040045EF RID: 17903
		private ProgramNode _node;
	}
}
