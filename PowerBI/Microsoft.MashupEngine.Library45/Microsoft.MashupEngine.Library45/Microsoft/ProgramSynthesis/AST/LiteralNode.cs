using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008D1 RID: 2257
	public class LiteralNode : TerminalNode
	{
		// Token: 0x0600308C RID: 12428 RVA: 0x0008F0EB File Offset: 0x0008D2EB
		public LiteralNode(Symbol symbol, object value)
			: base(symbol)
		{
			this.Value = value;
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x0008F0FB File Offset: 0x0008D2FB
		public object Value { get; }

		// Token: 0x0600308E RID: 12430 RVA: 0x0008F103 File Offset: 0x0008D303
		protected override object Evaluate(State state)
		{
			return this.Value;
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0008F10B File Offset: 0x0008D30B
		public override ProgramNode Clone()
		{
			return new LiteralNode(this.Symbol, this.Value);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x0008F11E File Offset: 0x0008D31E
		public override T AcceptVisitor<T>(ProgramNodeVisitor<T> visitor)
		{
			return visitor.VisitLiteral(this);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x0008F127 File Offset: 0x0008D327
		public override TResult AcceptVisitor<TResult, TArgs>(ProgramNodeVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitLiteral(this, args);
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x0008F131 File Offset: 0x0008D331
		public override bool Equals(ProgramNode other)
		{
			if (base.Equals(other))
			{
				ValueEquality comparer = ValueEquality.Comparer;
				object value = this.Value;
				LiteralNode literalNode = other as LiteralNode;
				return comparer.Equals(value, (literalNode != null) ? literalNode.Value : null);
			}
			return false;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x0008F160 File Offset: 0x0008D360
		public override int GetHashCode()
		{
			int num = 8217047 * ((this.Value != null) ? ValueEquality.Comparer.GetHashCode(this.Value) : 0);
			Symbol symbol = this.Symbol;
			return num ^ ((symbol != null) ? symbol.GetHashCode() : 0);
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x0008F198 File Offset: 0x0008D398
		internal new static LiteralNode ParseXML(XElement node, Type expectedType, ParseContext context)
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
			Optional<object> optional = Optional<object>.Nothing;
			Symbol symbol;
			if (context.Grammar.Symbols.TryGetValue(text2, out symbol))
			{
				if (expectedType != null && symbol.ResolvedType != expectedType)
				{
					return null;
				}
				optional = StdLiteralParsing.TryParse(node, symbol.ResolvedType, context.Context);
				if (!optional.HasValue)
				{
					return null;
				}
				return symbol.TerminalRule.BuildASTNode(optional.Value) as LiteralNode;
			}
			else
			{
				if (expectedType == null)
				{
					using (IEnumerator<Type> enumerator = StdLiteralParsing.KnownTypes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							Type type = enumerator.Current;
							optional = StdLiteralParsing.TryParse(node, type, context.Context);
							if (optional.HasValue)
							{
								break;
							}
						}
						goto IL_00F5;
					}
				}
				optional = StdLiteralParsing.TryParse(node, expectedType, context.Context);
				IL_00F5:
				if (!optional.HasValue)
				{
					return null;
				}
				Symbol symbol2 = new Symbol(context.Grammar, new ResolvedType(expectedType), "name", false);
				context.Grammar.AddRule(new TerminalRule(symbol2, false, null));
				return symbol2.TerminalRule.BuildASTNode(optional.Value) as LiteralNode;
			}
		}
	}
}
