using System;
using Microsoft.OData.Core.Metadata;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Core.UriParser.Syntactic;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Parsers
{
	// Token: 0x020001D3 RID: 467
	internal sealed class ParameterAliasBinder
	{
		// Token: 0x06001152 RID: 4434 RVA: 0x0003D492 File Offset: 0x0003B692
		internal ParameterAliasBinder(MetadataBinder.QueryTokenVisitor bindMethod)
		{
			ExceptionUtils.CheckArgumentNotNull<MetadataBinder.QueryTokenVisitor>(bindMethod, "bindMethod");
			this.bindMethod = bindMethod;
		}

		// Token: 0x06001153 RID: 4435 RVA: 0x0003D4AC File Offset: 0x0003B6AC
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

		// Token: 0x06001154 RID: 4436 RVA: 0x0003D544 File Offset: 0x0003B744
		private SingleValueNode ParseAndBindParameterAliasValueExpression(BindingState bindingState, string aliasValueExpression, IEdmTypeReference parameterType)
		{
			UriQueryExpressionParser uriQueryExpressionParser = new UriQueryExpressionParser(bindingState.Configuration.Settings.FilterLimit, false);
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

		// Token: 0x06001155 RID: 4437 RVA: 0x0003D5A4 File Offset: 0x0003B7A4
		private static QueryToken ParseComplexOrCollectionAlias(QueryToken queryToken, IEdmTypeReference parameterType, IEdmModel model)
		{
			LiteralToken literalToken = queryToken as LiteralToken;
			string text;
			if (literalToken != null && (text = literalToken.Value as string) != null && !string.IsNullOrEmpty(literalToken.OriginalText))
			{
				ExpressionLexer expressionLexer = new ExpressionLexer(literalToken.OriginalText, true, false, true);
				if (expressionLexer.CurrentToken.Kind == ExpressionTokenKind.BracketedExpression)
				{
					object obj = text;
					if (!parameterType.IsEntity() && !parameterType.IsEntityCollectionType())
					{
						obj = ODataUriUtils.ConvertFromUriLiteral(text, ODataVersion.V4, model, parameterType);
					}
					return new LiteralToken(obj, literalToken.OriginalText, parameterType);
				}
			}
			return queryToken;
		}

		// Token: 0x0400078A RID: 1930
		private readonly MetadataBinder.QueryTokenVisitor bindMethod;
	}
}
