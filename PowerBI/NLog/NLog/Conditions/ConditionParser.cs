using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Conditions
{
	// Token: 0x020001B1 RID: 433
	public class ConditionParser
	{
		// Token: 0x06001333 RID: 4915 RVA: 0x00033F41 File Offset: 0x00032141
		private ConditionParser(SimpleStringReader stringReader, ConfigurationItemFactory configurationItemFactory)
		{
			this._configurationItemFactory = configurationItemFactory;
			this._tokenizer = new ConditionTokenizer(stringReader);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00033F5C File Offset: 0x0003215C
		public static ConditionExpression ParseExpression(string expressionText)
		{
			return ConditionParser.ParseExpression(expressionText, ConfigurationItemFactory.Default);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x00033F6C File Offset: 0x0003216C
		public static ConditionExpression ParseExpression(string expressionText, ConfigurationItemFactory configurationItemFactories)
		{
			if (expressionText == null)
			{
				return null;
			}
			ConditionParser conditionParser = new ConditionParser(new SimpleStringReader(expressionText), configurationItemFactories);
			ConditionExpression conditionExpression = conditionParser.ParseExpression();
			if (!conditionParser._tokenizer.IsEOF())
			{
				throw new ConditionParseException("Unexpected token: " + conditionParser._tokenizer.TokenValue);
			}
			return conditionExpression;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00033FB9 File Offset: 0x000321B9
		internal static ConditionExpression ParseExpression(SimpleStringReader stringReader, ConfigurationItemFactory configurationItemFactories)
		{
			return new ConditionParser(stringReader, configurationItemFactories).ParseExpression();
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00033FC8 File Offset: 0x000321C8
		private ConditionMethodExpression ParsePredicate(string functionName)
		{
			List<ConditionExpression> list = new List<ConditionExpression>();
			while (!this._tokenizer.IsEOF() && this._tokenizer.TokenType != ConditionTokenType.RightParen)
			{
				list.Add(this.ParseExpression());
				if (this._tokenizer.TokenType != ConditionTokenType.Comma)
				{
					break;
				}
				this._tokenizer.GetNextToken();
			}
			this._tokenizer.Expect(ConditionTokenType.RightParen);
			ConditionMethodExpression conditionMethodExpression;
			try
			{
				MethodInfo methodInfo = this._configurationItemFactory.ConditionMethods.CreateInstance(functionName);
				conditionMethodExpression = new ConditionMethodExpression(functionName, methodInfo, list);
			}
			catch (Exception ex)
			{
				InternalLogger.Warn(ex, "Cannot resolve function '{0}'", new object[] { functionName });
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				throw new ConditionParseException("Cannot resolve function '" + functionName + "'", ex);
			}
			return conditionMethodExpression;
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00034094 File Offset: 0x00032294
		private ConditionExpression ParseLiteralExpression()
		{
			if (this._tokenizer.IsToken(ConditionTokenType.LeftParen))
			{
				this._tokenizer.GetNextToken();
				ConditionExpression conditionExpression = this.ParseExpression();
				this._tokenizer.Expect(ConditionTokenType.RightParen);
				return conditionExpression;
			}
			if (this._tokenizer.IsToken(ConditionTokenType.Minus))
			{
				this._tokenizer.GetNextToken();
				if (!this._tokenizer.IsNumber())
				{
					throw new ConditionParseException(string.Format("Number expected, got {0}", this._tokenizer.TokenType));
				}
				return this.ParseNumber(true);
			}
			else
			{
				if (this._tokenizer.IsNumber())
				{
					return this.ParseNumber(false);
				}
				if (this._tokenizer.TokenType == ConditionTokenType.String)
				{
					ConditionExpression conditionExpression2 = new ConditionLayoutExpression(Layout.FromString(this._tokenizer.StringTokenValue, this._configurationItemFactory));
					this._tokenizer.GetNextToken();
					return conditionExpression2;
				}
				if (this._tokenizer.TokenType == ConditionTokenType.Keyword)
				{
					string text = this._tokenizer.EatKeyword();
					ConditionExpression conditionExpression3;
					if (this.TryPlainKeywordToExpression(text, out conditionExpression3))
					{
						return conditionExpression3;
					}
					if (this._tokenizer.TokenType == ConditionTokenType.LeftParen)
					{
						this._tokenizer.GetNextToken();
						return this.ParsePredicate(text);
					}
				}
				throw new ConditionParseException("Unexpected token: " + this._tokenizer.TokenValue);
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000341CC File Offset: 0x000323CC
		private bool TryPlainKeywordToExpression(string keyword, out ConditionExpression expression)
		{
			if (string.Compare(keyword, "level", StringComparison.OrdinalIgnoreCase) == 0)
			{
				expression = new ConditionLevelExpression();
				return true;
			}
			if (string.Compare(keyword, "logger", StringComparison.OrdinalIgnoreCase) == 0)
			{
				expression = new ConditionLoggerNameExpression();
				return true;
			}
			if (string.Compare(keyword, "message", StringComparison.OrdinalIgnoreCase) == 0)
			{
				expression = new ConditionMessageExpression();
				return true;
			}
			if (string.Compare(keyword, "loglevel", StringComparison.OrdinalIgnoreCase) == 0)
			{
				this._tokenizer.Expect(ConditionTokenType.Dot);
				expression = new ConditionLiteralExpression(LogLevel.FromString(this._tokenizer.EatKeyword()));
				return true;
			}
			if (string.Compare(keyword, "true", StringComparison.OrdinalIgnoreCase) == 0)
			{
				expression = new ConditionLiteralExpression(true);
				return true;
			}
			if (string.Compare(keyword, "false", StringComparison.OrdinalIgnoreCase) == 0)
			{
				expression = new ConditionLiteralExpression(false);
				return true;
			}
			if (string.Compare(keyword, "null", StringComparison.OrdinalIgnoreCase) == 0)
			{
				expression = new ConditionLiteralExpression(null);
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000342A8 File Offset: 0x000324A8
		private ConditionExpression ParseNumber(bool negative)
		{
			string tokenValue = this._tokenizer.TokenValue;
			this._tokenizer.GetNextToken();
			if (tokenValue.IndexOf('.') >= 0)
			{
				double num = double.Parse(tokenValue, CultureInfo.InvariantCulture);
				return new ConditionLiteralExpression(negative ? (-num) : num);
			}
			int num2 = int.Parse(tokenValue, CultureInfo.InvariantCulture);
			return new ConditionLiteralExpression(negative ? (-num2) : num2);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00034314 File Offset: 0x00032514
		private ConditionExpression ParseBooleanRelation()
		{
			ConditionExpression conditionExpression = this.ParseLiteralExpression();
			if (this._tokenizer.IsToken(ConditionTokenType.EqualTo))
			{
				this._tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Equal);
			}
			if (this._tokenizer.IsToken(ConditionTokenType.NotEqual))
			{
				this._tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.NotEqual);
			}
			if (this._tokenizer.IsToken(ConditionTokenType.LessThan))
			{
				this._tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Less);
			}
			if (this._tokenizer.IsToken(ConditionTokenType.GreaterThan))
			{
				this._tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.Greater);
			}
			if (this._tokenizer.IsToken(ConditionTokenType.LessThanOrEqualTo))
			{
				this._tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.LessOrEqual);
			}
			if (this._tokenizer.IsToken(ConditionTokenType.GreaterThanOrEqualTo))
			{
				this._tokenizer.GetNextToken();
				return new ConditionRelationalExpression(conditionExpression, this.ParseLiteralExpression(), ConditionRelationalOperator.GreaterOrEqual);
			}
			return conditionExpression;
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00034417 File Offset: 0x00032617
		private ConditionExpression ParseBooleanPredicate()
		{
			if (this._tokenizer.IsKeyword("not") || this._tokenizer.IsToken(ConditionTokenType.Not))
			{
				this._tokenizer.GetNextToken();
				return new ConditionNotExpression(this.ParseBooleanPredicate());
			}
			return this.ParseBooleanRelation();
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00034458 File Offset: 0x00032658
		private ConditionExpression ParseBooleanAnd()
		{
			ConditionExpression conditionExpression = this.ParseBooleanPredicate();
			while (this._tokenizer.IsKeyword("and") || this._tokenizer.IsToken(ConditionTokenType.And))
			{
				this._tokenizer.GetNextToken();
				conditionExpression = new ConditionAndExpression(conditionExpression, this.ParseBooleanPredicate());
			}
			return conditionExpression;
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000344A8 File Offset: 0x000326A8
		private ConditionExpression ParseBooleanOr()
		{
			ConditionExpression conditionExpression = this.ParseBooleanAnd();
			while (this._tokenizer.IsKeyword("or") || this._tokenizer.IsToken(ConditionTokenType.Or))
			{
				this._tokenizer.GetNextToken();
				conditionExpression = new ConditionOrExpression(conditionExpression, this.ParseBooleanAnd());
			}
			return conditionExpression;
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000344F8 File Offset: 0x000326F8
		private ConditionExpression ParseBooleanExpression()
		{
			return this.ParseBooleanOr();
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00034500 File Offset: 0x00032700
		private ConditionExpression ParseExpression()
		{
			return this.ParseBooleanExpression();
		}

		// Token: 0x0400051F RID: 1311
		private readonly ConditionTokenizer _tokenizer;

		// Token: 0x04000520 RID: 1312
		private readonly ConfigurationItemFactory _configurationItemFactory;
	}
}
