using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020000F0 RID: 240
	internal sealed class ParameterAliasBinder
	{
		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		internal ParameterAliasBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001E5C0 File Offset: 0x0001C7C0
		internal ParameterAliasNode BindParameterAlias(BindingState bindingState, FunctionParameterAliasToken aliasToken)
		{
			ExceptionUtils.CheckArgumentNotNull<BindingState>(bindingState, "bindingState");
			ExceptionUtils.CheckArgumentNotNull<FunctionParameterAliasToken>(aliasToken, "aliasToken");
			string alias = aliasToken.Alias;
			ParameterAliasValueAccessor parameterAliasValueAccessor = bindingState.Configuration.ParameterAliasValueAccessor;
			if (parameterAliasValueAccessor == null)
			{
				return new ParameterAliasNode(alias, null);
			}
			SingleValueNode singleValueNode = null;
			if (!parameterAliasValueAccessor.ParameterAliasValueNodesCached.TryGetValue(alias, ref singleValueNode))
			{
				string aliasValueExpression = parameterAliasValueAccessor.GetAliasValueExpression(alias);
				if (aliasValueExpression == null)
				{
					parameterAliasValueAccessor.ParameterAliasValueNodesCached[alias] = null;
				}
				else
				{
					singleValueNode = this.ParseAndBindParameterAliasValueExpression(bindingState, aliasValueExpression, aliasToken.ExpectedParameterType);
					parameterAliasValueAccessor.ParameterAliasValueNodesCached[alias] = singleValueNode;
				}
			}
			return new ParameterAliasNode(alias, singleValueNode.GetEdmTypeReference());
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001E658 File Offset: 0x0001C858
		private SingleValueNode ParseAndBindParameterAliasValueExpression(BindingState bindingState, string aliasValueExpression, IEdmTypeReference parameterType)
		{
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(bindingState.Configuration.Settings.FilterLimit);
			QueryToken queryToken = uriQueryExpressionParser.ParseExpressionText(aliasValueExpression);
			queryToken = ParameterAliasBinder.ParseComplexOrCollectionAlias(queryToken, parameterType, bindingState.Model);
			QueryNode queryNode = this.bindMethod(queryToken);
			SingleValueNode singleValueNode = queryNode as SingleValueNode;
			if (singleValueNode == null)
			{
				throw new ODataException("ODataErrorStrings.MetadataBinder_ParameterAliasValueExpressionNotSingleValue");
			}
			return singleValueNode;
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x0001E6B4 File Offset: 0x0001C8B4
		private static QueryToken ParseComplexOrCollectionAlias(QueryToken queryToken, IEdmTypeReference parameterType, IEdmModel model)
		{
			LiteralToken literalToken = queryToken as LiteralToken;
			string text;
			if (literalToken != null && (text = literalToken.Value as string) != null && !string.IsNullOrEmpty(literalToken.OriginalText))
			{
				ExpressionLexer expressionLexer = new ExpressionLexer(literalToken.OriginalText, true, false, true);
				if (expressionLexer.CurrentToken.Kind == ExpressionTokenKind.BracketedExpression || expressionLexer.CurrentToken.Kind == ExpressionTokenKind.BracedExpression)
				{
					object obj = text;
					if (!parameterType.IsStructured() && !parameterType.IsStructuredCollectionType())
					{
						obj = ODataUriUtils.ConvertFromUriLiteral(text, ODataVersion.V4, model, parameterType);
					}
					return new LiteralToken(obj, literalToken.OriginalText, parameterType);
				}
			}
			return queryToken;
		}

		// Token: 0x04000697 RID: 1687
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
