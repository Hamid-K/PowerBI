using System;
using System.Collections.Generic;
using Microsoft.Internal;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language
{
	// Token: 0x02001721 RID: 5921
	public class ReducingVisitor : AstVisitor
	{
		// Token: 0x0600966D RID: 38509 RVA: 0x001F25D9 File Offset: 0x001F07D9
		public static IDocument Rewrite(IDocument document, Action<IError> log)
		{
			return new ReducingVisitor(document.Host, document.Tokens, log).VisitDocument(document);
		}

		// Token: 0x0600966E RID: 38510 RVA: 0x001F25F3 File Offset: 0x001F07F3
		private ReducingVisitor(IDocumentHost host, ITokens tokens, Action<IError> log)
		{
			this.host = host;
			this.tokens = tokens;
			this.log = log;
		}

		// Token: 0x0600966F RID: 38511 RVA: 0x001F2610 File Offset: 0x001F0810
		protected override IExpression VisitImplicitIdentifier(IImplicitIdentifierExpression implicitIdentifier)
		{
			return new ExclusiveIdentifierExpressionSyntaxNode(Identifier.Underscore);
		}

		// Token: 0x06009670 RID: 38512 RVA: 0x001F261C File Offset: 0x001F081C
		protected override IExpression VisitNotImplemented(INotImplementedExpression expression)
		{
			return new ThrowExpressionSyntaxNode(new InvocationExpressionSyntaxNode0(new ConstantExpressionSyntaxNode(Library.Expression.NotImplemented)), expression.Range);
		}

		// Token: 0x06009671 RID: 38513 RVA: 0x001F2638 File Offset: 0x001F0838
		protected override IExpression VisitThrow(IThrowExpression @throw)
		{
			return new ThrowExpressionSyntaxNode(new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(Library.Expression.SafeErrorRecord), this.VisitExpression(@throw.Expression)), @throw.Range);
		}

		// Token: 0x06009672 RID: 38514 RVA: 0x001F2660 File Offset: 0x001F0860
		protected override IExpression VisitVerbatim(IVerbatimExpression verbatim)
		{
			List<IError> errors = new List<IError>();
			Action<IError> action = delegate(IError entry)
			{
				errors.Add(entry);
			};
			IDocument document = Engine.Instance.Parse(verbatim.Text.Value.AsString, action);
			if (document.Kind == DocumentKind.Section)
			{
				errors.Clear();
				errors.Add(SourceErrors.SyntaxError(this.GetLocation(document, document.Tokens), Strings.SemanticsBuilder_ExpressionExpected));
			}
			string text;
			if (errors.Count == 0)
			{
				text = Strings.SemanticsBuilder_VebatimLiteralDoesNotContainError;
				this.log(SourceErrors.SyntaxError(this.GetLocation(verbatim, this.tokens), text));
			}
			else
			{
				text = errors[0].Message;
			}
			return new ThrowExpressionSyntaxNode(new RecordExpressionSyntaxNode(new VariableInitializer[]
			{
				new VariableInitializer(Identifier.New(ErrorRecord.ReasonKey), new ConstantExpressionSyntaxNode(ValueException.ExpressionError)),
				new VariableInitializer(Identifier.New(ErrorRecord.MessageKey), new ConstantExpressionSyntaxNode(TextValue.New(text))),
				new VariableInitializer(Identifier.New(ErrorRecord.DetailKey), verbatim.Text)
			}), verbatim.Range);
		}

		// Token: 0x06009673 RID: 38515 RVA: 0x001F27A0 File Offset: 0x001F09A0
		private SourceLocation GetLocation(ISyntaxNode node, ITokens tokens)
		{
			if (tokens != null)
			{
				return new SourceLocation(this.host, new TextRange(tokens.GetRange(node.Range.Start).Start, tokens.GetRange(node.Range.End).End));
			}
			return SourceLocation.None;
		}

		// Token: 0x06009674 RID: 38516 RVA: 0x001F2800 File Offset: 0x001F0A00
		private IExpression RewriteRangeListExpression(IRangeListExpression rangeListExpression)
		{
			IExpression expression = null;
			IList<IExpression> list = new List<IExpression>();
			for (int i = 0; i < rangeListExpression.Members.Count; i++)
			{
				IRangeExpression rangeExpression = rangeListExpression.Members[i];
				if (rangeExpression.Lower == rangeExpression.Upper)
				{
					list.Add(rangeExpression.Lower);
				}
				else
				{
					if (list.Count > 0)
					{
						IExpression expression2 = new ListExpressionSyntaxNode(list.ToArray<IExpression>());
						list.Clear();
						IExpression expression3;
						if (expression == null)
						{
							expression3 = expression2;
						}
						else
						{
							IExpression expression4 = new ConcatenateBinaryExpressionSyntaxNode(expression, expression2);
							expression3 = expression4;
						}
						expression = expression3;
					}
					IExpression expression5 = new RangeBinaryExpressionSyntaxNode(rangeExpression.Lower, rangeExpression.Upper, new TokenRange(rangeExpression.Range.Start, rangeExpression.Upper.Range.End));
					IExpression expression6;
					if (expression == null)
					{
						expression6 = expression5;
					}
					else
					{
						IExpression expression4 = new ConcatenateBinaryExpressionSyntaxNode(expression, expression5);
						expression6 = expression4;
					}
					expression = expression6;
				}
			}
			if (list.Count > 0)
			{
				IExpression expression7 = new ListExpressionSyntaxNode(list.ToArray<IExpression>());
				list.Clear();
				IExpression expression8;
				if (expression == null)
				{
					expression8 = expression7;
				}
				else
				{
					IExpression expression4 = new ConcatenateBinaryExpressionSyntaxNode(expression, expression7);
					expression8 = expression4;
				}
				expression = expression8;
			}
			return expression;
		}

		// Token: 0x06009675 RID: 38517 RVA: 0x001F290A File Offset: 0x001F0B0A
		protected override IExpression VisitRangeList(IRangeListExpression rangeList)
		{
			rangeList = (IRangeListExpression)base.VisitRangeList(rangeList);
			return this.RewriteRangeListExpression(rangeList);
		}

		// Token: 0x06009676 RID: 38518 RVA: 0x001F2921 File Offset: 0x001F0B21
		protected override IExpression VisitType(ITypeExpression type)
		{
			return this.VisitExpression(type.Expression);
		}

		// Token: 0x06009677 RID: 38519 RVA: 0x001F292F File Offset: 0x001F0B2F
		protected override IExpression VisitParentheses(IParenthesesExpression parentheses)
		{
			return this.VisitExpression(parentheses.Expression);
		}

		// Token: 0x04005000 RID: 20480
		private ITokens tokens;

		// Token: 0x04005001 RID: 20481
		private IDocumentHost host;

		// Token: 0x04005002 RID: 20482
		private Action<IError> log;
	}
}
