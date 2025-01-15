using System;
using System.Collections.Generic;
using Microsoft.Data.OData.Query.SyntacticAst;

namespace Microsoft.Data.OData.Query
{
	// Token: 0x02000025 RID: 37
	internal abstract class SelectExpandTermParser : ISelectExpandTermParser
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00004522 File Offset: 0x00002722
		protected SelectExpandTermParser(string clauseToParse, int maxDepth)
		{
			this.maxDepth = maxDepth;
			this.recursionDepth = 0;
			this.Lexer = ((clauseToParse != null) ? new ExpressionLexer(clauseToParse, false, true) : null);
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000454C File Offset: 0x0000274C
		public int MaxDepth
		{
			get
			{
				return this.maxDepth;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00004554 File Offset: 0x00002754
		public SelectToken ParseSelect()
		{
			this.isSelect = true;
			if (this.Lexer == null)
			{
				return new SelectToken(new List<PathSegmentToken>());
			}
			List<PathSegmentToken> list = new List<PathSegmentToken>();
			bool flag = this.Lexer.CurrentToken.Kind == ExpressionTokenKind.Equal;
			while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.End && this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.CloseParen)
			{
				list.Add(this.ParseSingleSelectTerm(flag));
			}
			return new SelectToken(list);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000045D4 File Offset: 0x000027D4
		public ExpandToken ParseExpand()
		{
			this.isSelect = false;
			if (this.Lexer == null)
			{
				return new ExpandToken(new List<ExpandTermToken>());
			}
			List<ExpandTermToken> list = new List<ExpandTermToken>();
			bool flag = this.Lexer.CurrentToken.Kind == ExpressionTokenKind.Equal;
			while (this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.End && this.Lexer.PeekNextToken().Kind != ExpressionTokenKind.CloseParen)
			{
				list.Add(this.ParseSingleExpandTerm(flag));
			}
			return new ExpandToken(list);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004654 File Offset: 0x00002854
		public PathSegmentToken ParseSingleSelectTerm(bool isInnerTerm)
		{
			this.isSelect = true;
			PathSegmentToken pathSegmentToken = this.ParseSelectExpandProperty();
			if (this.IsNotEndOfTerm(isInnerTerm))
			{
				throw new ODataException(Strings.UriSelectParser_TermIsNotValid(this.Lexer.ExpressionText));
			}
			return pathSegmentToken;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00004690 File Offset: 0x00002890
		public ExpandTermToken ParseSingleExpandTerm(bool isInnerTerm)
		{
			this.isSelect = false;
			this.RecurseEnter();
			PathSegmentToken pathSegmentToken = this.ParseSelectExpandProperty();
			this.RecurseLeave();
			return this.BuildExpandTermToken(isInnerTerm, pathSegmentToken);
		}

		// Token: 0x060000F2 RID: 242
		internal abstract ExpandTermToken BuildExpandTermToken(bool isInnerTerm, PathSegmentToken pathToken);

		// Token: 0x060000F3 RID: 243
		internal abstract bool IsNotEndOfTerm(bool isInnerTerm);

		// Token: 0x060000F4 RID: 244 RVA: 0x000046C0 File Offset: 0x000028C0
		private PathSegmentToken ParseSelectExpandProperty()
		{
			PathSegmentToken pathSegmentToken = null;
			int num = 0;
			for (;;)
			{
				num++;
				if (num > this.maxDepth)
				{
					break;
				}
				this.Lexer.NextToken();
				if (num > 1 && this.Lexer.CurrentToken.Kind == ExpressionTokenKind.End)
				{
					return pathSegmentToken;
				}
				pathSegmentToken = this.ParseNext(pathSegmentToken);
				if (this.Lexer.CurrentToken.Kind != ExpressionTokenKind.Slash)
				{
					return pathSegmentToken;
				}
			}
			throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x0000472C File Offset: 0x0000292C
		private PathSegmentToken ParseNext(PathSegmentToken previousToken)
		{
			if (this.Lexer.CurrentToken.Text.StartsWith("$", 0))
			{
				throw new ODataException(Strings.UriSelectParser_SystemTokenInSelectExpand(this.Lexer.CurrentToken.Text, this.Lexer.ExpressionText));
			}
			return this.ParseSegment(previousToken);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004784 File Offset: 0x00002984
		private PathSegmentToken ParseSegment(PathSegmentToken parent)
		{
			string text;
			if (this.Lexer.PeekNextToken().Kind == ExpressionTokenKind.Dot)
			{
				text = this.Lexer.ReadDottedIdentifier(this.isSelect);
			}
			else if (this.Lexer.CurrentToken.Kind == ExpressionTokenKind.Star)
			{
				if (this.Lexer.PeekNextToken().Kind == ExpressionTokenKind.Slash)
				{
					throw new ODataException(Strings.ExpressionToken_IdentifierExpected(this.Lexer.Position));
				}
				text = this.Lexer.CurrentToken.Text;
				this.Lexer.NextToken();
			}
			else
			{
				text = this.Lexer.CurrentToken.GetIdentifier();
				this.Lexer.NextToken();
			}
			return new NonSystemToken(text, null, parent);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004846 File Offset: 0x00002A46
		private void RecurseEnter()
		{
			this.recursionDepth++;
			if (this.recursionDepth > this.maxDepth)
			{
				throw new ODataException(Strings.UriQueryExpressionParser_TooDeep);
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000486F File Offset: 0x00002A6F
		private void RecurseLeave()
		{
			this.recursionDepth--;
		}

		// Token: 0x04000051 RID: 81
		public ExpressionLexer Lexer;

		// Token: 0x04000052 RID: 82
		private bool isSelect;

		// Token: 0x04000053 RID: 83
		private int maxDepth;

		// Token: 0x04000054 RID: 84
		private int recursionDepth;
	}
}
