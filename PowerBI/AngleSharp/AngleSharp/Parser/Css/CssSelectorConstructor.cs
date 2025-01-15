using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Html;
using AngleSharp.Extensions;
using AngleSharp.Services;

namespace AngleSharp.Parser.Css
{
	// Token: 0x0200007D RID: 125
	internal sealed class CssSelectorConstructor
	{
		// Token: 0x060003BF RID: 959 RVA: 0x00019370 File Offset: 0x00017570
		public CssSelectorConstructor(IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector)
		{
			this._combinators = new Stack<CssCombinator>();
			this.Reset(attributeSelector, pseudoClassSelector, pseudoElementSelector);
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001938D File Offset: 0x0001758D
		public bool IsValid
		{
			get
			{
				return this._invoked && this._valid && this._ready;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000193A7 File Offset: 0x000175A7
		public bool IsNested
		{
			get
			{
				return this._nested;
			}
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000193B0 File Offset: 0x000175B0
		public ISelector GetResult()
		{
			if (!this.IsValid)
			{
				return new UnknownSelector();
			}
			if (this._complex != null)
			{
				this._complex.ConcludeSelector(this._temp);
				this._temp = this._complex;
				this._complex = null;
			}
			if (this._group == null || this._group.Length == 0)
			{
				return this._temp ?? SimpleSelector.All;
			}
			if (this._temp == null && this._group.Length == 1)
			{
				return this._group[0];
			}
			if (this._temp != null)
			{
				this._group.Add(this._temp);
				this._temp = null;
			}
			return this._group;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00019468 File Offset: 0x00017668
		public void Apply(CssToken token)
		{
			if (token.Type != CssTokenType.Comment)
			{
				this._invoked = true;
				switch (this._state)
				{
				case CssSelectorConstructor.State.Data:
					this.OnData(token);
					return;
				case CssSelectorConstructor.State.Attribute:
					this.OnAttribute(token);
					return;
				case CssSelectorConstructor.State.AttributeOperator:
					this.OnAttributeOperator(token);
					return;
				case CssSelectorConstructor.State.AttributeValue:
					this.OnAttributeValue(token);
					return;
				case CssSelectorConstructor.State.AttributeEnd:
					this.OnAttributeEnd(token);
					return;
				case CssSelectorConstructor.State.Class:
					this.OnClass(token);
					return;
				case CssSelectorConstructor.State.PseudoClass:
					this.OnPseudoClass(token);
					return;
				case CssSelectorConstructor.State.PseudoElement:
					this.OnPseudoElement(token);
					return;
				default:
					this._valid = false;
					break;
				}
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000194FC File Offset: 0x000176FC
		public CssSelectorConstructor Reset(IAttributeSelectorFactory attributeSelector, IPseudoClassSelectorFactory pseudoClassSelector, IPseudoElementSelectorFactory pseudoElementSelector)
		{
			this._attrName = null;
			this._attrValue = null;
			this._attrNs = null;
			this._attrInsensitive = false;
			this._attrOp = string.Empty;
			this._state = CssSelectorConstructor.State.Data;
			this._combinators.Clear();
			this._temp = null;
			this._group = null;
			this._complex = null;
			this._valid = true;
			this._nested = false;
			this._invoked = false;
			this._ready = true;
			this._attributeSelector = attributeSelector;
			this._pseudoClassSelector = pseudoClassSelector;
			this._pseudoElementSelector = pseudoElementSelector;
			return this;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001958C File Offset: 0x0001778C
		private void OnData(CssToken token)
		{
			CssTokenType type = token.Type;
			if (type <= CssTokenType.Ident)
			{
				if (type == CssTokenType.Hash)
				{
					this.Insert(SimpleSelector.Id(token.Data));
					this._ready = true;
					return;
				}
				if (type == CssTokenType.Ident)
				{
					this.Insert(SimpleSelector.Type(token.Data));
					this._ready = true;
					return;
				}
			}
			else
			{
				if (type == CssTokenType.Delim)
				{
					this.OnDelim(token);
					return;
				}
				switch (type)
				{
				case CssTokenType.SquareBracketOpen:
					this._attrName = null;
					this._attrValue = null;
					this._attrOp = string.Empty;
					this._attrNs = null;
					this._state = CssSelectorConstructor.State.Attribute;
					this._ready = false;
					return;
				case CssTokenType.Colon:
					this._state = CssSelectorConstructor.State.PseudoClass;
					this._ready = false;
					return;
				case CssTokenType.Comma:
					this.InsertOr();
					this._ready = false;
					return;
				case CssTokenType.Whitespace:
					this.Insert(CssCombinator.Descendent);
					return;
				}
			}
			this._valid = false;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001967C File Offset: 0x0001787C
		private void OnAttribute(CssToken token)
		{
			if (token.Type != CssTokenType.Whitespace)
			{
				if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String)
				{
					this._state = CssSelectorConstructor.State.AttributeOperator;
					this._attrName = token.Data;
					return;
				}
				if (token.Type == CssTokenType.Delim && token.Data.Is(CombinatorSymbols.Pipe))
				{
					this._state = CssSelectorConstructor.State.Attribute;
					this._attrNs = string.Empty;
					return;
				}
				if (token.Type == CssTokenType.Delim && token.Data.Is(Keywords.Asterisk))
				{
					this._state = CssSelectorConstructor.State.AttributeOperator;
					this._attrName = token.ToValue();
					return;
				}
				this._state = CssSelectorConstructor.State.Data;
				this._valid = false;
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00019728 File Offset: 0x00017928
		private void OnAttributeOperator(CssToken token)
		{
			if (token.Type != CssTokenType.Whitespace)
			{
				if (token.Type == CssTokenType.SquareBracketClose)
				{
					this._state = CssSelectorConstructor.State.AttributeValue;
					this.OnAttributeEnd(token);
					return;
				}
				if (token.Type == CssTokenType.Match || token.Type == CssTokenType.Delim)
				{
					this._state = CssSelectorConstructor.State.AttributeValue;
					this._attrOp = token.ToValue();
					if (this._attrOp == CombinatorSymbols.Pipe)
					{
						this._attrNs = this._attrName;
						this._attrName = null;
						this._attrOp = string.Empty;
						this._state = CssSelectorConstructor.State.Attribute;
						return;
					}
				}
				else
				{
					this._state = CssSelectorConstructor.State.AttributeEnd;
					this._valid = false;
				}
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000197C8 File Offset: 0x000179C8
		private void OnAttributeValue(CssToken token)
		{
			if (token.Type != CssTokenType.Whitespace)
			{
				if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String || token.Type == CssTokenType.Number)
				{
					this._state = CssSelectorConstructor.State.AttributeEnd;
					this._attrValue = token.Data;
					return;
				}
				this._state = CssSelectorConstructor.State.Data;
				this._valid = false;
			}
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001981C File Offset: 0x00017A1C
		private void OnAttributeEnd(CssToken token)
		{
			if (!this._attrInsensitive && token.Type == CssTokenType.Ident && token.Data == "i")
			{
				this._attrInsensitive = true;
				return;
			}
			if (token.Type != CssTokenType.Whitespace)
			{
				this._state = CssSelectorConstructor.State.Data;
				this._ready = true;
				if (token.Type == CssTokenType.SquareBracketClose)
				{
					ISelector selector = this._attributeSelector.Create(this._attrOp, this._attrName, this._attrValue, this._attrNs, this._attrInsensitive);
					this._attrInsensitive = false;
					this.Insert(selector);
					return;
				}
				this._valid = false;
			}
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000198B8 File Offset: 0x00017AB8
		private void OnPseudoClass(CssToken token)
		{
			this._state = CssSelectorConstructor.State.Data;
			this._ready = true;
			if (token.Type == CssTokenType.Colon)
			{
				this._state = CssSelectorConstructor.State.PseudoElement;
				return;
			}
			if (token.Type == CssTokenType.Function)
			{
				ISelector pseudoFunction = this.GetPseudoFunction(token as CssFunctionToken);
				if (pseudoFunction != null)
				{
					this.Insert(pseudoFunction);
					return;
				}
			}
			else if (token.Type == CssTokenType.Ident)
			{
				ISelector selector = this._pseudoClassSelector.Create(token.Data);
				if (selector != null)
				{
					this.Insert(selector);
					return;
				}
			}
			this._valid = false;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x00019934 File Offset: 0x00017B34
		private void OnPseudoElement(CssToken token)
		{
			this._state = CssSelectorConstructor.State.Data;
			this._ready = true;
			if (token.Type == CssTokenType.Ident)
			{
				ISelector selector = this._pseudoElementSelector.Create(token.Data);
				if (selector != null)
				{
					this._valid = this._valid && !this._nested;
					this.Insert(selector);
					return;
				}
			}
			this._valid = false;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00019996 File Offset: 0x00017B96
		private void OnClass(CssToken token)
		{
			this._state = CssSelectorConstructor.State.Data;
			this._ready = true;
			if (token.Type == CssTokenType.Ident)
			{
				this.Insert(SimpleSelector.Class(token.Data));
				return;
			}
			this._valid = false;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x000199C8 File Offset: 0x00017BC8
		private void InsertOr()
		{
			if (this._temp != null)
			{
				if (this._group == null)
				{
					this._group = new ListSelector();
				}
				if (this._complex != null)
				{
					this._complex.ConcludeSelector(this._temp);
					this._group.Add(this._complex);
					this._complex = null;
				}
				else
				{
					this._group.Add(this._temp);
				}
				this._temp = null;
			}
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00019A3C File Offset: 0x00017C3C
		private void Insert(ISelector selector)
		{
			if (this._temp == null)
			{
				this._combinators.Clear();
				this._temp = selector;
				return;
			}
			if (this._combinators.Count == 0)
			{
				CompoundSelector compoundSelector = this._temp as CompoundSelector;
				if (compoundSelector == null)
				{
					compoundSelector = new CompoundSelector { this._temp };
				}
				compoundSelector.Add(selector);
				this._temp = compoundSelector;
				return;
			}
			if (this._complex == null)
			{
				this._complex = new ComplexSelector();
			}
			CssCombinator combinator = this.GetCombinator();
			this._complex.AppendSelector(this._temp, combinator);
			this._temp = selector;
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00019AD4 File Offset: 0x00017CD4
		private CssCombinator GetCombinator()
		{
			while (this._combinators.Count > 1 && this._combinators.Peek() == CssCombinator.Descendent)
			{
				this._combinators.Pop();
			}
			if (this._combinators.Count > 1)
			{
				CssCombinator cssCombinator = this._combinators.Pop();
				CssCombinator cssCombinator2 = this._combinators.Pop();
				if (cssCombinator == CssCombinator.Child && cssCombinator2 == CssCombinator.Child)
				{
					if (this._combinators.Count == 0 || this._combinators.Peek() != CssCombinator.Child)
					{
						cssCombinator = CssCombinator.Descendent;
					}
					else if (this._combinators.Pop() == CssCombinator.Child)
					{
						cssCombinator = CssCombinator.Deep;
					}
				}
				else if (cssCombinator == CssCombinator.Namespace && cssCombinator2 == CssCombinator.Namespace)
				{
					cssCombinator = CssCombinator.Column;
				}
				else
				{
					this._combinators.Push(cssCombinator2);
				}
				while (this._combinators.Count > 0)
				{
					this._valid = this._combinators.Pop() == CssCombinator.Descendent && this._valid;
				}
				return cssCombinator;
			}
			return this._combinators.Pop();
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00019BEB File Offset: 0x00017DEB
		private void Insert(CssCombinator cssCombinator)
		{
			this._combinators.Push(cssCombinator);
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00019BFC File Offset: 0x00017DFC
		private void OnDelim(CssToken token)
		{
			char c = token.Data[0];
			if (c <= '>')
			{
				switch (c)
				{
				case '*':
					this.Insert(SimpleSelector.All);
					this._ready = true;
					return;
				case '+':
					this.Insert(CssCombinator.AdjacentSibling);
					this._ready = false;
					return;
				case ',':
					this.InsertOr();
					this._ready = false;
					return;
				case '-':
					break;
				case '.':
					this._state = CssSelectorConstructor.State.Class;
					this._ready = false;
					return;
				default:
					if (c == '>')
					{
						this.Insert(CssCombinator.Child);
						this._ready = false;
						return;
					}
					break;
				}
			}
			else
			{
				if (c == '|')
				{
					if (this._combinators.Count > 0 && this._combinators.Peek() == CssCombinator.Descendent)
					{
						this.Insert(SimpleSelector.Type(string.Empty));
					}
					this.Insert(CssCombinator.Namespace);
					this._ready = false;
					return;
				}
				if (c == '~')
				{
					this.Insert(CssCombinator.Sibling);
					this._ready = false;
					return;
				}
			}
			this._valid = false;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00019D04 File Offset: 0x00017F04
		private ISelector GetPseudoFunction(CssFunctionToken arguments)
		{
			Func<CssSelectorConstructor, CssSelectorConstructor.FunctionState> func = null;
			if (CssSelectorConstructor.pseudoClassFunctions.TryGetValue(arguments.Data, out func))
			{
				using (CssSelectorConstructor.FunctionState functionState = func(this))
				{
					this._ready = false;
					foreach (CssToken cssToken in arguments)
					{
						if (functionState.Finished(cssToken))
						{
							ISelector selector = functionState.Produce();
							if (this._nested && functionState is CssSelectorConstructor.NotFunctionState)
							{
								selector = null;
							}
							this._ready = true;
							return selector;
						}
					}
				}
			}
			return null;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00019DB8 File Offset: 0x00017FB8
		private CssSelectorConstructor CreateChild()
		{
			CssSelectorConstructor cssSelectorConstructor = Pool.NewSelectorConstructor(this._attributeSelector, this._pseudoClassSelector, this._pseudoElementSelector);
			cssSelectorConstructor._invoked = true;
			return cssSelectorConstructor;
		}

		// Token: 0x040002F8 RID: 760
		private static readonly Dictionary<string, Func<CssSelectorConstructor, CssSelectorConstructor.FunctionState>> pseudoClassFunctions = new Dictionary<string, Func<CssSelectorConstructor, CssSelectorConstructor.FunctionState>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				PseudoClassNames.NthChild,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.ChildFunctionState<FirstChildSelector>(ctx, true)
			},
			{
				PseudoClassNames.NthLastChild,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.ChildFunctionState<LastChildSelector>(ctx, true)
			},
			{
				PseudoClassNames.NthOfType,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.ChildFunctionState<FirstTypeSelector>(ctx, false)
			},
			{
				PseudoClassNames.NthLastOfType,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.ChildFunctionState<LastTypeSelector>(ctx, false)
			},
			{
				PseudoClassNames.NthColumn,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.ChildFunctionState<FirstColumnSelector>(ctx, false)
			},
			{
				PseudoClassNames.NthLastColumn,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.ChildFunctionState<LastColumnSelector>(ctx, false)
			},
			{
				PseudoClassNames.Not,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.NotFunctionState(ctx)
			},
			{
				PseudoClassNames.Dir,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.DirFunctionState()
			},
			{
				PseudoClassNames.Lang,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.LangFunctionState()
			},
			{
				PseudoClassNames.Contains,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.ContainsFunctionState()
			},
			{
				PseudoClassNames.Has,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.HasFunctionState(ctx)
			},
			{
				PseudoClassNames.Matches,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.MatchesFunctionState(ctx)
			},
			{
				PseudoClassNames.HostContext,
				(CssSelectorConstructor ctx) => new CssSelectorConstructor.HostContextFunctionState(ctx)
			}
		};

		// Token: 0x040002F9 RID: 761
		private readonly Stack<CssCombinator> _combinators;

		// Token: 0x040002FA RID: 762
		private CssSelectorConstructor.State _state;

		// Token: 0x040002FB RID: 763
		private ISelector _temp;

		// Token: 0x040002FC RID: 764
		private ListSelector _group;

		// Token: 0x040002FD RID: 765
		private ComplexSelector _complex;

		// Token: 0x040002FE RID: 766
		private string _attrName;

		// Token: 0x040002FF RID: 767
		private string _attrValue;

		// Token: 0x04000300 RID: 768
		private bool _attrInsensitive;

		// Token: 0x04000301 RID: 769
		private string _attrOp;

		// Token: 0x04000302 RID: 770
		private string _attrNs;

		// Token: 0x04000303 RID: 771
		private bool _valid;

		// Token: 0x04000304 RID: 772
		private bool _invoked;

		// Token: 0x04000305 RID: 773
		private bool _nested;

		// Token: 0x04000306 RID: 774
		private bool _ready;

		// Token: 0x04000307 RID: 775
		private IAttributeSelectorFactory _attributeSelector;

		// Token: 0x04000308 RID: 776
		private IPseudoElementSelectorFactory _pseudoElementSelector;

		// Token: 0x04000309 RID: 777
		private IPseudoClassSelectorFactory _pseudoClassSelector;

		// Token: 0x02000451 RID: 1105
		private enum State : byte
		{
			// Token: 0x04000F94 RID: 3988
			Data,
			// Token: 0x04000F95 RID: 3989
			Attribute,
			// Token: 0x04000F96 RID: 3990
			AttributeOperator,
			// Token: 0x04000F97 RID: 3991
			AttributeValue,
			// Token: 0x04000F98 RID: 3992
			AttributeEnd,
			// Token: 0x04000F99 RID: 3993
			Class,
			// Token: 0x04000F9A RID: 3994
			PseudoClass,
			// Token: 0x04000F9B RID: 3995
			PseudoElement
		}

		// Token: 0x02000452 RID: 1106
		private abstract class FunctionState : IDisposable
		{
			// Token: 0x06002365 RID: 9061 RVA: 0x0005B7D2 File Offset: 0x000599D2
			public bool Finished(CssToken token)
			{
				return this.OnToken(token);
			}

			// Token: 0x06002366 RID: 9062
			public abstract ISelector Produce();

			// Token: 0x06002367 RID: 9063
			protected abstract bool OnToken(CssToken token);

			// Token: 0x06002368 RID: 9064 RVA: 0x00003C25 File Offset: 0x00001E25
			public virtual void Dispose()
			{
			}
		}

		// Token: 0x02000453 RID: 1107
		private sealed class NotFunctionState : CssSelectorConstructor.FunctionState
		{
			// Token: 0x0600236A RID: 9066 RVA: 0x0005B7DB File Offset: 0x000599DB
			public NotFunctionState(CssSelectorConstructor parent)
			{
				this._selector = parent.CreateChild();
				this._selector._nested = true;
			}

			// Token: 0x0600236B RID: 9067 RVA: 0x0005B7FB File Offset: 0x000599FB
			protected override bool OnToken(CssToken token)
			{
				if (token.Type != CssTokenType.RoundBracketClose || this._selector._state != CssSelectorConstructor.State.Data)
				{
					this._selector.Apply(token);
					return false;
				}
				return true;
			}

			// Token: 0x0600236C RID: 9068 RVA: 0x0005B824 File Offset: 0x00059A24
			public override ISelector Produce()
			{
				bool isValid = this._selector.IsValid;
				ISelector sel = this._selector.GetResult();
				if (isValid)
				{
					string text = PseudoClassNames.Not.CssFunction(sel.Text);
					return SimpleSelector.PseudoClass((IElement el) => !sel.Match(el), text);
				}
				return null;
			}

			// Token: 0x0600236D RID: 9069 RVA: 0x0005B87F File Offset: 0x00059A7F
			public override void Dispose()
			{
				base.Dispose();
				this._selector.ToPool();
			}

			// Token: 0x04000F9C RID: 3996
			private readonly CssSelectorConstructor _selector;
		}

		// Token: 0x02000454 RID: 1108
		private sealed class HasFunctionState : CssSelectorConstructor.FunctionState
		{
			// Token: 0x0600236E RID: 9070 RVA: 0x0005B893 File Offset: 0x00059A93
			public HasFunctionState(CssSelectorConstructor parent)
			{
				this._nested = parent.CreateChild();
			}

			// Token: 0x0600236F RID: 9071 RVA: 0x0005B8A7 File Offset: 0x00059AA7
			protected override bool OnToken(CssToken token)
			{
				if (token.Type != CssTokenType.RoundBracketClose || this._nested._state != CssSelectorConstructor.State.Data)
				{
					this._nested.Apply(token);
					return false;
				}
				return true;
			}

			// Token: 0x06002370 RID: 9072 RVA: 0x0005B8D0 File Offset: 0x00059AD0
			public override ISelector Produce()
			{
				bool isValid = this._nested.IsValid;
				ISelector sel = this._nested.GetResult();
				if (isValid)
				{
					string text = PseudoClassNames.Has.CssFunction(sel.Text);
					return SimpleSelector.PseudoClass((IElement el) => el.ChildNodes.QuerySelector(sel) != null, text);
				}
				return null;
			}

			// Token: 0x06002371 RID: 9073 RVA: 0x0005B92B File Offset: 0x00059B2B
			public override void Dispose()
			{
				base.Dispose();
				this._nested.ToPool();
			}

			// Token: 0x04000F9D RID: 3997
			private readonly CssSelectorConstructor _nested;
		}

		// Token: 0x02000455 RID: 1109
		private sealed class MatchesFunctionState : CssSelectorConstructor.FunctionState
		{
			// Token: 0x06002372 RID: 9074 RVA: 0x0005B93F File Offset: 0x00059B3F
			public MatchesFunctionState(CssSelectorConstructor parent)
			{
				this._selector = parent.CreateChild();
			}

			// Token: 0x06002373 RID: 9075 RVA: 0x0005B953 File Offset: 0x00059B53
			protected override bool OnToken(CssToken token)
			{
				if (token.Type != CssTokenType.RoundBracketClose || this._selector._state != CssSelectorConstructor.State.Data)
				{
					this._selector.Apply(token);
					return false;
				}
				return true;
			}

			// Token: 0x06002374 RID: 9076 RVA: 0x0005B97C File Offset: 0x00059B7C
			public override ISelector Produce()
			{
				bool isValid = this._selector.IsValid;
				ISelector sel = this._selector.GetResult();
				if (isValid)
				{
					string text = PseudoClassNames.Matches.CssFunction(sel.Text);
					return SimpleSelector.PseudoClass((IElement el) => sel.Match(el), text);
				}
				return null;
			}

			// Token: 0x06002375 RID: 9077 RVA: 0x0005B9D7 File Offset: 0x00059BD7
			public override void Dispose()
			{
				base.Dispose();
				this._selector.ToPool();
			}

			// Token: 0x04000F9E RID: 3998
			private readonly CssSelectorConstructor _selector;
		}

		// Token: 0x02000456 RID: 1110
		private sealed class DirFunctionState : CssSelectorConstructor.FunctionState
		{
			// Token: 0x06002376 RID: 9078 RVA: 0x0005B9EB File Offset: 0x00059BEB
			public DirFunctionState()
			{
				this._valid = true;
				this._value = null;
			}

			// Token: 0x06002377 RID: 9079 RVA: 0x0005BA01 File Offset: 0x00059C01
			protected override bool OnToken(CssToken token)
			{
				if (token.Type == CssTokenType.Ident)
				{
					this._value = token.Data;
				}
				else
				{
					if (token.Type == CssTokenType.RoundBracketClose)
					{
						return true;
					}
					if (token.Type != CssTokenType.Whitespace)
					{
						this._valid = false;
					}
				}
				return false;
			}

			// Token: 0x06002378 RID: 9080 RVA: 0x0005BA38 File Offset: 0x00059C38
			public override ISelector Produce()
			{
				if (this._valid && this._value != null)
				{
					string text = PseudoClassNames.Dir.CssFunction(this._value);
					return SimpleSelector.PseudoClass((IElement el) => el is IHtmlElement && this._value.Isi(((IHtmlElement)el).Direction), text);
				}
				return null;
			}

			// Token: 0x04000F9F RID: 3999
			private bool _valid;

			// Token: 0x04000FA0 RID: 4000
			private string _value;
		}

		// Token: 0x02000457 RID: 1111
		private sealed class LangFunctionState : CssSelectorConstructor.FunctionState
		{
			// Token: 0x0600237A RID: 9082 RVA: 0x0005BA9C File Offset: 0x00059C9C
			public LangFunctionState()
			{
				this.valid = true;
				this.value = null;
			}

			// Token: 0x0600237B RID: 9083 RVA: 0x0005BAB2 File Offset: 0x00059CB2
			protected override bool OnToken(CssToken token)
			{
				if (token.Type == CssTokenType.Ident)
				{
					this.value = token.Data;
				}
				else
				{
					if (token.Type == CssTokenType.RoundBracketClose)
					{
						return true;
					}
					if (token.Type != CssTokenType.Whitespace)
					{
						this.valid = false;
					}
				}
				return false;
			}

			// Token: 0x0600237C RID: 9084 RVA: 0x0005BAEC File Offset: 0x00059CEC
			public override ISelector Produce()
			{
				if (this.valid && this.value != null)
				{
					string text = PseudoClassNames.Lang.CssFunction(this.value);
					return SimpleSelector.PseudoClass((IElement el) => el is IHtmlElement && ((IHtmlElement)el).Language.StartsWith(this.value, StringComparison.OrdinalIgnoreCase), text);
				}
				return null;
			}

			// Token: 0x04000FA1 RID: 4001
			private bool valid;

			// Token: 0x04000FA2 RID: 4002
			private string value;
		}

		// Token: 0x02000458 RID: 1112
		private sealed class ContainsFunctionState : CssSelectorConstructor.FunctionState
		{
			// Token: 0x0600237E RID: 9086 RVA: 0x0005BB51 File Offset: 0x00059D51
			public ContainsFunctionState()
			{
				this._valid = true;
				this._value = null;
			}

			// Token: 0x0600237F RID: 9087 RVA: 0x0005BB67 File Offset: 0x00059D67
			protected override bool OnToken(CssToken token)
			{
				if (token.Type == CssTokenType.Ident || token.Type == CssTokenType.String)
				{
					this._value = token.Data;
				}
				else
				{
					if (token.Type == CssTokenType.RoundBracketClose)
					{
						return true;
					}
					if (token.Type != CssTokenType.Whitespace)
					{
						this._valid = false;
					}
				}
				return false;
			}

			// Token: 0x06002380 RID: 9088 RVA: 0x0005BBA8 File Offset: 0x00059DA8
			public override ISelector Produce()
			{
				if (this._valid && this._value != null)
				{
					string text = PseudoClassNames.Contains.CssFunction(this._value);
					return SimpleSelector.PseudoClass((IElement el) => el.TextContent.Contains(this._value), text);
				}
				return null;
			}

			// Token: 0x04000FA3 RID: 4003
			private bool _valid;

			// Token: 0x04000FA4 RID: 4004
			private string _value;
		}

		// Token: 0x02000459 RID: 1113
		private sealed class HostContextFunctionState : CssSelectorConstructor.FunctionState
		{
			// Token: 0x06002382 RID: 9090 RVA: 0x0005BBFD File Offset: 0x00059DFD
			public HostContextFunctionState(CssSelectorConstructor parent)
			{
				this._selector = parent.CreateChild();
			}

			// Token: 0x06002383 RID: 9091 RVA: 0x0005BC11 File Offset: 0x00059E11
			protected override bool OnToken(CssToken token)
			{
				if (token.Type != CssTokenType.RoundBracketClose || this._selector._state != CssSelectorConstructor.State.Data)
				{
					this._selector.Apply(token);
					return false;
				}
				return true;
			}

			// Token: 0x06002384 RID: 9092 RVA: 0x0005BC3C File Offset: 0x00059E3C
			public override ISelector Produce()
			{
				bool isValid = this._selector.IsValid;
				ISelector sel = this._selector.GetResult();
				if (isValid)
				{
					string text = PseudoClassNames.HostContext.CssFunction(sel.Text);
					return SimpleSelector.PseudoClass(delegate(IElement el)
					{
						IShadowRoot shadowRoot = el.Parent as IShadowRoot;
						for (IElement element = ((shadowRoot != null) ? shadowRoot.Host : null); element != null; element = element.ParentElement)
						{
							if (sel.Match(element))
							{
								return true;
							}
						}
						return false;
					}, text);
				}
				return null;
			}

			// Token: 0x06002385 RID: 9093 RVA: 0x0005BC97 File Offset: 0x00059E97
			public override void Dispose()
			{
				base.Dispose();
				this._selector.ToPool();
			}

			// Token: 0x04000FA5 RID: 4005
			private readonly CssSelectorConstructor _selector;
		}

		// Token: 0x0200045A RID: 1114
		private sealed class ChildFunctionState<T> : CssSelectorConstructor.FunctionState where T : ChildSelector, ISelector, new()
		{
			// Token: 0x06002386 RID: 9094 RVA: 0x0005BCAB File Offset: 0x00059EAB
			public ChildFunctionState(CssSelectorConstructor parent, bool withOptionalSelector = true)
			{
				this._parent = parent;
				this._allowOf = withOptionalSelector;
				this._valid = true;
				this._sign = 1;
				this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.Initial;
			}

			// Token: 0x06002387 RID: 9095 RVA: 0x0005BCD8 File Offset: 0x00059ED8
			public override ISelector Produce()
			{
				bool flag = !this._valid || (this._nested != null && !this._nested.IsValid);
				CssSelectorConstructor nested = this._nested;
				ISelector selector = ((nested != null) ? nested.ToPool() : null) ?? SimpleSelector.All;
				if (flag)
				{
					return null;
				}
				return new T().With(this._step, this._offset, selector);
			}

			// Token: 0x06002388 RID: 9096 RVA: 0x0005BD48 File Offset: 0x00059F48
			protected override bool OnToken(CssToken token)
			{
				switch (this._state)
				{
				case CssSelectorConstructor.ChildFunctionState<T>.ParseState.Initial:
					return this.OnInitial(token);
				case CssSelectorConstructor.ChildFunctionState<T>.ParseState.AfterInitialSign:
					return this.OnAfterInitialSign(token);
				case CssSelectorConstructor.ChildFunctionState<T>.ParseState.Offset:
					return this.OnOffset(token);
				case CssSelectorConstructor.ChildFunctionState<T>.ParseState.BeforeOf:
					return this.OnBeforeOf(token);
				default:
					return this.OnAfter(token);
				}
			}

			// Token: 0x06002389 RID: 9097 RVA: 0x0005BD9C File Offset: 0x00059F9C
			private bool OnAfterInitialSign(CssToken token)
			{
				if (token.Type == CssTokenType.Number)
				{
					return this.OnOffset(token);
				}
				if (token.Type == CssTokenType.Dimension)
				{
					CssUnitToken cssUnitToken = (CssUnitToken)token;
					this._valid = this._valid && cssUnitToken.Unit.Isi("n") && int.TryParse(token.Data, out this._step);
					this._step *= this._sign;
					this._sign = 1;
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.Offset;
					return false;
				}
				if (token.Type == CssTokenType.Ident && token.Data.Isi("n"))
				{
					this._step = this._sign;
					this._sign = 1;
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.Offset;
					return false;
				}
				if (this._state == CssSelectorConstructor.ChildFunctionState<T>.ParseState.Initial && token.Type == CssTokenType.Ident && token.Data.Isi("-n"))
				{
					this._step = -1;
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.Offset;
					return false;
				}
				this._valid = false;
				return token.Type == CssTokenType.RoundBracketClose;
			}

			// Token: 0x0600238A RID: 9098 RVA: 0x0005BE9D File Offset: 0x0005A09D
			private bool OnAfter(CssToken token)
			{
				if (token.Type != CssTokenType.RoundBracketClose || this._nested._state != CssSelectorConstructor.State.Data)
				{
					this._nested.Apply(token);
					return false;
				}
				return true;
			}

			// Token: 0x0600238B RID: 9099 RVA: 0x0005BEC8 File Offset: 0x0005A0C8
			private bool OnBeforeOf(CssToken token)
			{
				if (token.Type == CssTokenType.Whitespace)
				{
					return false;
				}
				if (token.Data.Isi(Keywords.Of))
				{
					this._valid = this._allowOf;
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.AfterOf;
					this._nested = this._parent.CreateChild();
					return false;
				}
				if (token.Type == CssTokenType.RoundBracketClose)
				{
					return true;
				}
				this._valid = false;
				return false;
			}

			// Token: 0x0600238C RID: 9100 RVA: 0x0005BF30 File Offset: 0x0005A130
			private bool OnOffset(CssToken token)
			{
				if (token.Type == CssTokenType.Whitespace)
				{
					return false;
				}
				if (token.Type == CssTokenType.Number)
				{
					this._valid = this._valid && ((CssNumberToken)token).IsInteger && int.TryParse(token.Data, out this._offset);
					this._offset *= this._sign;
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.BeforeOf;
					return false;
				}
				return this.OnBeforeOf(token);
			}

			// Token: 0x0600238D RID: 9101 RVA: 0x0005BFA4 File Offset: 0x0005A1A4
			private bool OnInitial(CssToken token)
			{
				if (token.Type == CssTokenType.Whitespace)
				{
					return false;
				}
				if (token.Data.Isi(Keywords.Odd))
				{
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.BeforeOf;
					this._step = 2;
					this._offset = 1;
					return false;
				}
				if (token.Data.Isi(Keywords.Even))
				{
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.BeforeOf;
					this._step = 2;
					this._offset = 0;
					return false;
				}
				if (token.Type == CssTokenType.Delim && token.Data.IsOneOf("+", "-"))
				{
					this._sign = ((token.Data == "-") ? (-1) : 1);
					this._state = CssSelectorConstructor.ChildFunctionState<T>.ParseState.AfterInitialSign;
					return false;
				}
				return this.OnAfterInitialSign(token);
			}

			// Token: 0x04000FA6 RID: 4006
			private readonly CssSelectorConstructor _parent;

			// Token: 0x04000FA7 RID: 4007
			private bool _valid;

			// Token: 0x04000FA8 RID: 4008
			private int _step;

			// Token: 0x04000FA9 RID: 4009
			private int _offset;

			// Token: 0x04000FAA RID: 4010
			private int _sign;

			// Token: 0x04000FAB RID: 4011
			private CssSelectorConstructor.ChildFunctionState<T>.ParseState _state;

			// Token: 0x04000FAC RID: 4012
			private CssSelectorConstructor _nested;

			// Token: 0x04000FAD RID: 4013
			private bool _allowOf;

			// Token: 0x02000542 RID: 1346
			private enum ParseState : byte
			{
				// Token: 0x040012E5 RID: 4837
				Initial,
				// Token: 0x040012E6 RID: 4838
				AfterInitialSign,
				// Token: 0x040012E7 RID: 4839
				Offset,
				// Token: 0x040012E8 RID: 4840
				BeforeOf,
				// Token: 0x040012E9 RID: 4841
				AfterOf
			}
		}
	}
}
