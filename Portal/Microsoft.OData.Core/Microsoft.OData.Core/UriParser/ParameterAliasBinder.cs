using System;
using Microsoft.OData.Edm;
using Microsoft.OData.Metadata;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200012E RID: 302
	internal sealed class ParameterAliasBinder
	{
		// Token: 0x06001023 RID: 4131 RVA: 0x0002A038 File Offset: 0x00028238
		internal ParameterAliasBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0002A054 File Offset: 0x00028254
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
			if (!parameterAliasValueAccessor.ParameterAliasValueNodesCached.TryGetValue(alias, out singleValueNode))
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

		// Token: 0x06001025 RID: 4133 RVA: 0x0002A0EC File Offset: 0x000282EC
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

		// Token: 0x06001026 RID: 4134 RVA: 0x0002A148 File Offset: 0x00028348
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

		// Token: 0x040007AA RID: 1962
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
