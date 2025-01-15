using System;
using System.Collections.Immutable;
using System.Linq;
using System.Xml.Linq;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008E8 RID: 2280
	public class VariableNode : TerminalNode
	{
		// Token: 0x06003146 RID: 12614 RVA: 0x000915F5 File Offset: 0x0008F7F5
		public VariableNode(Symbol symbol)
			: base(symbol)
		{
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x00091600 File Offset: 0x0008F800
		protected override object Evaluate(State state)
		{
			object obj;
			if (!state.TryGetValue(this.Symbol, out obj))
			{
				throw new InvalidOperationException();
			}
			return obj;
		}

		// Token: 0x06003148 RID: 12616 RVA: 0x00091624 File Offset: 0x0008F824
		public override ProgramNode Clone()
		{
			return new VariableNode(this.Symbol);
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x00091631 File Offset: 0x0008F831
		public override T AcceptVisitor<T>(ProgramNodeVisitor<T> visitor)
		{
			return visitor.VisitVariable(this);
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x0009163A File Offset: 0x0008F83A
		public override TResult AcceptVisitor<TResult, TArgs>(ProgramNodeVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitVariable(this, args);
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x00091644 File Offset: 0x0008F844
		internal new static ProgramNode ParseXML(XElement node, Type type, ParseContext context)
		{
			if (node.Name != typeof(VariableNode).Name)
			{
				return null;
			}
			XAttribute xattribute = node.Attribute("symbol");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				return null;
			}
			text = text.TrimStart(new char[] { '$' });
			string text2 = text;
			ImmutableStack<ScopeElement> immutableStack = context.Scope;
			while (!immutableStack.IsEmpty)
			{
				ScopeElement scopeElement;
				immutableStack = immutableStack.Pop(out scopeElement);
				if (scopeElement.Symbol.Name == text2)
				{
					if (scopeElement.IsDefinition)
					{
						return new VariableNode(scopeElement.Symbol);
					}
					text2 = scopeElement.Replacement.Name;
				}
			}
			foreach (Symbol symbol in context.Grammar.Symbols.Values.Where((Symbol s) => s.IsVariable))
			{
				if (!(symbol.Name != text) && (!(type != null) || !(type != symbol.ResolvedType)))
				{
					return new VariableNode(symbol);
				}
			}
			return null;
		}
	}
}
