using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000D4 RID: 212
	internal class JPath
	{
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x0002F01E File Offset: 0x0002D21E
		public List<PathFilter> Filters { get; }

		// Token: 0x06000BEF RID: 3055 RVA: 0x0002F026 File Offset: 0x0002D226
		public JPath(string expression)
		{
			ValidationUtils.ArgumentNotNull(expression, "expression");
			this._expression = expression;
			this.Filters = new List<PathFilter>();
			this.ParseMain();
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x0002F054 File Offset: 0x0002D254
		private void ParseMain()
		{
			int num = this._currentIndex;
			this.EatWhitespace();
			if (this._expression.Length == this._currentIndex)
			{
				return;
			}
			if (this._expression[this._currentIndex] == '$')
			{
				if (this._expression.Length == 1)
				{
					return;
				}
				char c = this._expression[this._currentIndex + 1];
				if (c == '.' || c == '[')
				{
					this._currentIndex++;
					num = this._currentIndex;
				}
			}
			if (!this.ParsePath(this.Filters, num, false))
			{
				int currentIndex = this._currentIndex;
				this.EatWhitespace();
				if (this._currentIndex < this._expression.Length)
				{
					throw new JsonException("Unexpected character while parsing path: " + this._expression[currentIndex].ToString());
				}
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x0002F130 File Offset: 0x0002D330
		private bool ParsePath(List<PathFilter> filters, int currentPartStartIndex, bool query)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = false;
			while (this._currentIndex < this._expression.Length && !flag4)
			{
				char c = this._expression[this._currentIndex];
				if (c <= ')')
				{
					if (c != ' ')
					{
						if (c != '(')
						{
							if (c != ')')
							{
								goto IL_0189;
							}
							goto IL_00CD;
						}
					}
					else
					{
						if (this._currentIndex < this._expression.Length)
						{
							flag4 = true;
							continue;
						}
						continue;
					}
				}
				else
				{
					if (c == '.')
					{
						if (this._currentIndex > currentPartStartIndex)
						{
							string text = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex);
							if (text == "*")
							{
								text = null;
							}
							filters.Add(JPath.CreatePathFilter(text, flag));
							flag = false;
						}
						if (this._currentIndex + 1 < this._expression.Length && this._expression[this._currentIndex + 1] == '.')
						{
							flag = true;
							this._currentIndex++;
						}
						this._currentIndex++;
						currentPartStartIndex = this._currentIndex;
						flag2 = false;
						flag3 = true;
						continue;
					}
					if (c != '[')
					{
						if (c != ']')
						{
							goto IL_0189;
						}
						goto IL_00CD;
					}
				}
				if (this._currentIndex > currentPartStartIndex)
				{
					string text2 = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex);
					if (text2 == "*")
					{
						text2 = null;
					}
					filters.Add(JPath.CreatePathFilter(text2, flag));
					flag = false;
				}
				filters.Add(this.ParseIndexer(c, flag));
				flag = false;
				this._currentIndex++;
				currentPartStartIndex = this._currentIndex;
				flag2 = true;
				flag3 = false;
				continue;
				IL_00CD:
				flag4 = true;
				continue;
				IL_0189:
				if (query && (c == '=' || c == '<' || c == '!' || c == '>' || c == '|' || c == '&'))
				{
					flag4 = true;
				}
				else
				{
					if (flag2)
					{
						throw new JsonException("Unexpected character following indexer: " + c.ToString());
					}
					this._currentIndex++;
				}
			}
			bool flag5 = this._currentIndex == this._expression.Length;
			if (this._currentIndex > currentPartStartIndex)
			{
				string text3 = this._expression.Substring(currentPartStartIndex, this._currentIndex - currentPartStartIndex).TrimEnd(Array.Empty<char>());
				if (text3 == "*")
				{
					text3 = null;
				}
				filters.Add(JPath.CreatePathFilter(text3, flag));
			}
			else if (flag3 && (flag5 || query))
			{
				throw new JsonException("Unexpected end while parsing path.");
			}
			return flag5;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x0002F3A7 File Offset: 0x0002D5A7
		private static PathFilter CreatePathFilter([Nullable(2)] string member, bool scan)
		{
			if (!scan)
			{
				return new FieldFilter(member);
			}
			return new ScanFilter(member);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x0002F3BC File Offset: 0x0002D5BC
		private PathFilter ParseIndexer(char indexerOpenChar, bool scan)
		{
			this._currentIndex++;
			char c = ((indexerOpenChar == '[') ? ']' : ')');
			this.EnsureLength("Path ended with open indexer.");
			this.EatWhitespace();
			if (this._expression[this._currentIndex] == '\'')
			{
				return this.ParseQuotedField(c, scan);
			}
			if (this._expression[this._currentIndex] == '?')
			{
				return this.ParseQuery(c, scan);
			}
			return this.ParseArrayIndexer(c);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x0002F438 File Offset: 0x0002D638
		private PathFilter ParseArrayIndexer(char indexerCloseChar)
		{
			int num = this._currentIndex;
			int? num2 = null;
			List<int> list = null;
			int num3 = 0;
			int? num4 = null;
			int? num5 = null;
			int? num6 = null;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				if (c == ' ')
				{
					num2 = new int?(this._currentIndex);
					this.EatWhitespace();
				}
				else if (c == indexerCloseChar)
				{
					int num7 = (num2 ?? this._currentIndex) - num;
					if (list != null)
					{
						if (num7 == 0)
						{
							throw new JsonException("Array index expected.");
						}
						int num8 = Convert.ToInt32(this._expression.Substring(num, num7), CultureInfo.InvariantCulture);
						list.Add(num8);
						return new ArrayMultipleIndexFilter(list);
					}
					else
					{
						if (num3 > 0)
						{
							if (num7 > 0)
							{
								int num9 = Convert.ToInt32(this._expression.Substring(num, num7), CultureInfo.InvariantCulture);
								if (num3 == 1)
								{
									num5 = new int?(num9);
								}
								else
								{
									num6 = new int?(num9);
								}
							}
							return new ArraySliceFilter
							{
								Start = num4,
								End = num5,
								Step = num6
							};
						}
						if (num7 == 0)
						{
							throw new JsonException("Array index expected.");
						}
						int num10 = Convert.ToInt32(this._expression.Substring(num, num7), CultureInfo.InvariantCulture);
						return new ArrayIndexFilter
						{
							Index = new int?(num10)
						};
					}
				}
				else if (c == ',')
				{
					int num11 = (num2 ?? this._currentIndex) - num;
					if (num11 == 0)
					{
						throw new JsonException("Array index expected.");
					}
					if (list == null)
					{
						list = new List<int>();
					}
					string text = this._expression.Substring(num, num11);
					list.Add(Convert.ToInt32(text, CultureInfo.InvariantCulture));
					this._currentIndex++;
					this.EatWhitespace();
					num = this._currentIndex;
					num2 = null;
				}
				else if (c == '*')
				{
					this._currentIndex++;
					this.EnsureLength("Path ended with open indexer.");
					this.EatWhitespace();
					if (this._expression[this._currentIndex] != indexerCloseChar)
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + c.ToString());
					}
					return new ArrayIndexFilter();
				}
				else if (c == ':')
				{
					int num12 = (num2 ?? this._currentIndex) - num;
					if (num12 > 0)
					{
						int num13 = Convert.ToInt32(this._expression.Substring(num, num12), CultureInfo.InvariantCulture);
						if (num3 == 0)
						{
							num4 = new int?(num13);
						}
						else if (num3 == 1)
						{
							num5 = new int?(num13);
						}
						else
						{
							num6 = new int?(num13);
						}
					}
					num3++;
					this._currentIndex++;
					this.EatWhitespace();
					num = this._currentIndex;
					num2 = null;
				}
				else
				{
					if (!char.IsDigit(c) && c != '-')
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + c.ToString());
					}
					if (num2 != null)
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + c.ToString());
					}
					this._currentIndex++;
				}
			}
			throw new JsonException("Path ended with open indexer.");
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x0002F787 File Offset: 0x0002D987
		private void EatWhitespace()
		{
			while (this._currentIndex < this._expression.Length && this._expression[this._currentIndex] == ' ')
			{
				this._currentIndex++;
			}
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0002F7C4 File Offset: 0x0002D9C4
		private PathFilter ParseQuery(char indexerCloseChar, bool scan)
		{
			this._currentIndex++;
			this.EnsureLength("Path ended with open indexer.");
			if (this._expression[this._currentIndex] != '(')
			{
				throw new JsonException("Unexpected character while parsing path indexer: " + this._expression[this._currentIndex].ToString());
			}
			this._currentIndex++;
			QueryExpression queryExpression = this.ParseExpression();
			this._currentIndex++;
			this.EnsureLength("Path ended with open indexer.");
			this.EatWhitespace();
			if (this._expression[this._currentIndex] != indexerCloseChar)
			{
				throw new JsonException("Unexpected character while parsing path indexer: " + this._expression[this._currentIndex].ToString());
			}
			if (!scan)
			{
				return new QueryFilter(queryExpression);
			}
			return new QueryScanFilter(queryExpression);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x0002F8AC File Offset: 0x0002DAAC
		private bool TryParseExpression([Nullable(new byte[] { 2, 0 })] out List<PathFilter> expressionPath)
		{
			if (this._expression[this._currentIndex] == '$')
			{
				expressionPath = new List<PathFilter> { RootFilter.Instance };
			}
			else
			{
				if (this._expression[this._currentIndex] != '@')
				{
					expressionPath = null;
					return false;
				}
				expressionPath = new List<PathFilter>();
			}
			this._currentIndex++;
			if (this.ParsePath(expressionPath, this._currentIndex, true))
			{
				throw new JsonException("Path ended with open query.");
			}
			return true;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x0002F930 File Offset: 0x0002DB30
		private JsonException CreateUnexpectedCharacterException()
		{
			return new JsonException("Unexpected character while parsing path query: " + this._expression[this._currentIndex].ToString());
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x0002F968 File Offset: 0x0002DB68
		private object ParseSide()
		{
			this.EatWhitespace();
			List<PathFilter> list;
			if (this.TryParseExpression(out list))
			{
				this.EatWhitespace();
				this.EnsureLength("Path ended with open query.");
				return list;
			}
			object obj;
			if (this.TryParseValue(out obj))
			{
				this.EatWhitespace();
				this.EnsureLength("Path ended with open query.");
				return new JValue(obj);
			}
			throw this.CreateUnexpectedCharacterException();
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0002F9C0 File Offset: 0x0002DBC0
		private QueryExpression ParseExpression()
		{
			QueryExpression queryExpression = null;
			CompositeExpression compositeExpression = null;
			while (this._currentIndex < this._expression.Length)
			{
				object obj = this.ParseSide();
				object obj2 = null;
				QueryOperator queryOperator;
				if (this._expression[this._currentIndex] == ')' || this._expression[this._currentIndex] == '|' || this._expression[this._currentIndex] == '&')
				{
					queryOperator = QueryOperator.Exists;
				}
				else
				{
					queryOperator = this.ParseOperator();
					obj2 = this.ParseSide();
				}
				BooleanQueryExpression booleanQueryExpression = new BooleanQueryExpression(queryOperator, obj, obj2);
				if (this._expression[this._currentIndex] == ')')
				{
					if (compositeExpression != null)
					{
						compositeExpression.Expressions.Add(booleanQueryExpression);
						return queryExpression;
					}
					return booleanQueryExpression;
				}
				else
				{
					if (this._expression[this._currentIndex] == '&')
					{
						if (!this.Match("&&"))
						{
							throw this.CreateUnexpectedCharacterException();
						}
						if (compositeExpression == null || compositeExpression.Operator != QueryOperator.And)
						{
							CompositeExpression compositeExpression2 = new CompositeExpression(QueryOperator.And);
							if (compositeExpression != null)
							{
								compositeExpression.Expressions.Add(compositeExpression2);
							}
							compositeExpression = compositeExpression2;
							if (queryExpression == null)
							{
								queryExpression = compositeExpression;
							}
						}
						compositeExpression.Expressions.Add(booleanQueryExpression);
					}
					if (this._expression[this._currentIndex] == '|')
					{
						if (!this.Match("||"))
						{
							throw this.CreateUnexpectedCharacterException();
						}
						if (compositeExpression == null || compositeExpression.Operator != QueryOperator.Or)
						{
							CompositeExpression compositeExpression3 = new CompositeExpression(QueryOperator.Or);
							if (compositeExpression != null)
							{
								compositeExpression.Expressions.Add(compositeExpression3);
							}
							compositeExpression = compositeExpression3;
							if (queryExpression == null)
							{
								queryExpression = compositeExpression;
							}
						}
						compositeExpression.Expressions.Add(booleanQueryExpression);
					}
				}
			}
			throw new JsonException("Path ended with open query.");
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x0002FB50 File Offset: 0x0002DD50
		[NullableContext(2)]
		private bool TryParseValue(out object value)
		{
			char c = this._expression[this._currentIndex];
			if (c == '\'')
			{
				value = this.ReadQuotedString();
				return true;
			}
			if (char.IsDigit(c) || c == '-')
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(c);
				this._currentIndex++;
				while (this._currentIndex < this._expression.Length)
				{
					c = this._expression[this._currentIndex];
					if (c == ' ' || c == ')')
					{
						string text = stringBuilder.ToString();
						if (text.IndexOfAny(JPath.FloatCharacters) != -1)
						{
							double num;
							bool flag = double.TryParse(text, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, CultureInfo.InvariantCulture, out num);
							value = num;
							return flag;
						}
						long num2;
						bool flag2 = long.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num2);
						value = num2;
						return flag2;
					}
					else
					{
						stringBuilder.Append(c);
						this._currentIndex++;
					}
				}
			}
			else if (c == 't')
			{
				if (this.Match("true"))
				{
					value = true;
					return true;
				}
			}
			else if (c == 'f')
			{
				if (this.Match("false"))
				{
					value = false;
					return true;
				}
			}
			else if (c == 'n')
			{
				if (this.Match("null"))
				{
					value = null;
					return true;
				}
			}
			else if (c == '/')
			{
				value = this.ReadRegexString();
				return true;
			}
			value = null;
			return false;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
		private string ReadQuotedString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this._currentIndex++;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				if (c == '\\' && this._currentIndex + 1 < this._expression.Length)
				{
					this._currentIndex++;
					c = this._expression[this._currentIndex];
					char c2;
					if (c <= '\\')
					{
						if (c <= '\'')
						{
							if (c != '"' && c != '\'')
							{
								goto IL_00CB;
							}
						}
						else if (c != '/' && c != '\\')
						{
							goto IL_00CB;
						}
						c2 = c;
					}
					else if (c <= 'f')
					{
						if (c != 'b')
						{
							if (c != 'f')
							{
								goto IL_00CB;
							}
							c2 = '\f';
						}
						else
						{
							c2 = '\b';
						}
					}
					else if (c != 'n')
					{
						if (c != 'r')
						{
							if (c != 't')
							{
								goto IL_00CB;
							}
							c2 = '\t';
						}
						else
						{
							c2 = '\r';
						}
					}
					else
					{
						c2 = '\n';
					}
					stringBuilder.Append(c2);
					this._currentIndex++;
					continue;
					IL_00CB:
					throw new JsonException("Unknown escape character: \\" + c.ToString());
				}
				if (c == '\'')
				{
					this._currentIndex++;
					return stringBuilder.ToString();
				}
				this._currentIndex++;
				stringBuilder.Append(c);
			}
			throw new JsonException("Path ended with an open string.");
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x0002FDF8 File Offset: 0x0002DFF8
		private string ReadRegexString()
		{
			int currentIndex = this._currentIndex;
			this._currentIndex++;
			while (this._currentIndex < this._expression.Length)
			{
				char c = this._expression[this._currentIndex];
				if (c == '\\' && this._currentIndex + 1 < this._expression.Length)
				{
					this._currentIndex += 2;
				}
				else
				{
					if (c == '/')
					{
						this._currentIndex++;
						while (this._currentIndex < this._expression.Length)
						{
							c = this._expression[this._currentIndex];
							if (!char.IsLetter(c))
							{
								break;
							}
							this._currentIndex++;
						}
						return this._expression.Substring(currentIndex, this._currentIndex - currentIndex);
					}
					this._currentIndex++;
				}
			}
			throw new JsonException("Path ended with an open regex.");
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
		private bool Match(string s)
		{
			int num = this._currentIndex;
			for (int i = 0; i < s.Length; i++)
			{
				if (num >= this._expression.Length || this._expression[num] != s[i])
				{
					return false;
				}
				num++;
			}
			this._currentIndex = num;
			return true;
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0002FF48 File Offset: 0x0002E148
		private QueryOperator ParseOperator()
		{
			if (this._currentIndex + 1 >= this._expression.Length)
			{
				throw new JsonException("Path ended with open query.");
			}
			if (this.Match("==="))
			{
				return QueryOperator.StrictEquals;
			}
			if (this.Match("=="))
			{
				return QueryOperator.Equals;
			}
			if (this.Match("=~"))
			{
				return QueryOperator.RegexEquals;
			}
			if (this.Match("!=="))
			{
				return QueryOperator.StrictNotEquals;
			}
			if (this.Match("!=") || this.Match("<>"))
			{
				return QueryOperator.NotEquals;
			}
			if (this.Match("<="))
			{
				return QueryOperator.LessThanOrEquals;
			}
			if (this.Match("<"))
			{
				return QueryOperator.LessThan;
			}
			if (this.Match(">="))
			{
				return QueryOperator.GreaterThanOrEquals;
			}
			if (this.Match(">"))
			{
				return QueryOperator.GreaterThan;
			}
			throw new JsonException("Could not read query operator.");
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00030018 File Offset: 0x0002E218
		private PathFilter ParseQuotedField(char indexerCloseChar, bool scan)
		{
			List<string> list = null;
			while (this._currentIndex < this._expression.Length)
			{
				string text = this.ReadQuotedString();
				this.EatWhitespace();
				this.EnsureLength("Path ended with open indexer.");
				if (this._expression[this._currentIndex] == indexerCloseChar)
				{
					if (list == null)
					{
						return JPath.CreatePathFilter(text, scan);
					}
					list.Add(text);
					if (!scan)
					{
						return new FieldMultipleFilter(list);
					}
					return new ScanMultipleFilter(list);
				}
				else
				{
					if (this._expression[this._currentIndex] != ',')
					{
						throw new JsonException("Unexpected character while parsing path indexer: " + this._expression[this._currentIndex].ToString());
					}
					this._currentIndex++;
					this.EatWhitespace();
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(text);
				}
			}
			throw new JsonException("Path ended with open indexer.");
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000300FF File Offset: 0x0002E2FF
		private void EnsureLength(string message)
		{
			if (this._currentIndex >= this._expression.Length)
			{
				throw new JsonException(message);
			}
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x0003011B File Offset: 0x0002E31B
		internal IEnumerable<JToken> Evaluate(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings)
		{
			return JPath.Evaluate(this.Filters, root, t, settings);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x0003012C File Offset: 0x0002E32C
		internal static IEnumerable<JToken> Evaluate(List<PathFilter> filters, JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings)
		{
			IEnumerable<JToken> enumerable = new JToken[] { t };
			foreach (PathFilter pathFilter in filters)
			{
				enumerable = pathFilter.ExecuteFilter(root, enumerable, settings);
			}
			return enumerable;
		}

		// Token: 0x040003B5 RID: 949
		private static readonly char[] FloatCharacters = new char[] { '.', 'E', 'e' };

		// Token: 0x040003B6 RID: 950
		private readonly string _expression;

		// Token: 0x040003B8 RID: 952
		private int _currentIndex;
	}
}
