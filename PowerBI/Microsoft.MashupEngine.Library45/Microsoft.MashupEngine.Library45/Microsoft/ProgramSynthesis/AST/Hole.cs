using System;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008CC RID: 2252
	public class Hole : TerminalNode
	{
		// Token: 0x06003068 RID: 12392 RVA: 0x0008E909 File Offset: 0x0008CB09
		public Hole(Symbol symbol, string holeId = null)
			: base(symbol)
		{
			this.HoleId = holeId;
		}

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x0008E919 File Offset: 0x0008CB19
		public string HoleId { get; }

		// Token: 0x0600306A RID: 12394 RVA: 0x00002188 File Offset: 0x00000388
		protected override object Evaluate(State state)
		{
			return null;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x0008E921 File Offset: 0x0008CB21
		public override ProgramNode Clone()
		{
			return new Hole(this.Symbol, this.HoleId);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0008E934 File Offset: 0x0008CB34
		public override T AcceptVisitor<T>(ProgramNodeVisitor<T> visitor)
		{
			return visitor.VisitHole(this);
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x0008E93D File Offset: 0x0008CB3D
		public override TResult AcceptVisitor<TResult, TArgs>(ProgramNodeVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitHole(this, args);
		}

		// Token: 0x0600306E RID: 12398 RVA: 0x0008E948 File Offset: 0x0008CB48
		internal new static Hole ParseXML(XElement node, Type expectedType, ParseContext context)
		{
			string text;
			if (node == null)
			{
				text = null;
			}
			else
			{
				XAttribute xattribute = node.Attribute("symbol");
				text = ((xattribute != null) ? xattribute.Value : null);
			}
			string text2 = text;
			if (text2 == null)
			{
				return null;
			}
			text2 = text2.TrimStart(new char[] { '$' });
			Symbol symbol = context.Grammar.Symbol(text2);
			if (symbol == null)
			{
				return null;
			}
			if (expectedType != null && symbol.ResolvedType != expectedType)
			{
				return null;
			}
			XAttribute xattribute2 = node.Attribute("holeId");
			string text3 = ((xattribute2 != null) ? xattribute2.Value : null);
			return new Hole(symbol, text3);
		}

		// Token: 0x0600306F RID: 12399 RVA: 0x0008E9E8 File Offset: 0x0008CBE8
		public override bool Equals(ProgramNode other)
		{
			return other != null && (this == other || (!(other.GetType() != base.GetType()) && object.Equals(this.Symbol, other.Symbol) && this.HoleId == ((Hole)other).HoleId));
		}

		// Token: 0x06003070 RID: 12400 RVA: 0x0008EA40 File Offset: 0x0008CC40
		public override int GetHashCode()
		{
			int num = 6719;
			int hashCode = this.Symbol.GetHashCode();
			string holeId = this.HoleId;
			return HashHelpers.Combine(num, HashHelpers.Combine(hashCode, (holeId != null) ? holeId.GetHashCode() : 0));
		}
	}
}
