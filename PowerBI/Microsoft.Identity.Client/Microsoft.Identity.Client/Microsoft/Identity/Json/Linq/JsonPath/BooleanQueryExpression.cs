using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Identity.Json.Utilities;

namespace Microsoft.Identity.Json.Linq.JsonPath
{
	// Token: 0x020000D9 RID: 217
	internal class BooleanQueryExpression : QueryExpression
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x000303C4 File Offset: 0x0002E5C4
		public BooleanQueryExpression(QueryOperator @operator, object left, [Nullable(2)] object right)
			: base(@operator)
		{
			this.Left = left;
			this.Right = right;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000303DC File Offset: 0x0002E5DC
		private IEnumerable<JToken> GetResult(JToken root, JToken t, [Nullable(2)] object o)
		{
			JToken jtoken = o as JToken;
			if (jtoken != null)
			{
				return new JToken[] { jtoken };
			}
			List<PathFilter> list = o as List<PathFilter>;
			if (list != null)
			{
				return JPath.Evaluate(list, root, t, null);
			}
			return CollectionUtils.ArrayEmpty<JToken>();
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00030418 File Offset: 0x0002E618
		public override bool IsMatch(JToken root, JToken t, [Nullable(2)] JsonSelectSettings settings)
		{
			if (this.Operator == QueryOperator.Exists)
			{
				return this.GetResult(root, t, this.Left).Any<JToken>();
			}
			using (IEnumerator<JToken> enumerator = this.GetResult(root, t, this.Left).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					IEnumerable<JToken> result = this.GetResult(root, t, this.Right);
					ICollection<JToken> collection = (result as ICollection<JToken>) ?? result.ToList<JToken>();
					do
					{
						JToken jtoken = enumerator.Current;
						foreach (JToken jtoken2 in collection)
						{
							if (this.MatchTokens(jtoken, jtoken2, settings))
							{
								return true;
							}
						}
					}
					while (enumerator.MoveNext());
				}
			}
			return false;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000304F4 File Offset: 0x0002E6F4
		private bool MatchTokens(JToken leftResult, JToken rightResult, [Nullable(2)] JsonSelectSettings settings)
		{
			JValue jvalue = leftResult as JValue;
			if (jvalue != null)
			{
				JValue jvalue2 = rightResult as JValue;
				if (jvalue2 != null)
				{
					switch (this.Operator)
					{
					case QueryOperator.Equals:
						if (BooleanQueryExpression.EqualsWithStringCoercion(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					case QueryOperator.NotEquals:
						if (!BooleanQueryExpression.EqualsWithStringCoercion(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					case QueryOperator.Exists:
						return true;
					case QueryOperator.LessThan:
						if (jvalue.CompareTo(jvalue2) < 0)
						{
							return true;
						}
						return false;
					case QueryOperator.LessThanOrEquals:
						if (jvalue.CompareTo(jvalue2) <= 0)
						{
							return true;
						}
						return false;
					case QueryOperator.GreaterThan:
						if (jvalue.CompareTo(jvalue2) > 0)
						{
							return true;
						}
						return false;
					case QueryOperator.GreaterThanOrEquals:
						if (jvalue.CompareTo(jvalue2) >= 0)
						{
							return true;
						}
						return false;
					case QueryOperator.And:
					case QueryOperator.Or:
						return false;
					case QueryOperator.RegexEquals:
						if (BooleanQueryExpression.RegexEquals(jvalue, jvalue2, settings))
						{
							return true;
						}
						return false;
					case QueryOperator.StrictEquals:
						if (BooleanQueryExpression.EqualsWithStrictMatch(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					case QueryOperator.StrictNotEquals:
						if (!BooleanQueryExpression.EqualsWithStrictMatch(jvalue, jvalue2))
						{
							return true;
						}
						return false;
					default:
						return false;
					}
				}
			}
			QueryOperator @operator = this.Operator;
			if (@operator - QueryOperator.NotEquals <= 1)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000305D8 File Offset: 0x0002E7D8
		private static bool RegexEquals(JValue input, JValue pattern, [Nullable(2)] JsonSelectSettings settings)
		{
			if (input.Type != JTokenType.String || pattern.Type != JTokenType.String)
			{
				return false;
			}
			string text = (string)pattern.Value;
			int num = text.LastIndexOf('/');
			string text2 = text.Substring(1, num - 1);
			string text3 = text.Substring(num + 1);
			return Regex.IsMatch((string)input.Value, text2, MiscellaneousUtils.GetRegexOptions(text3));
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00030638 File Offset: 0x0002E838
		internal static bool EqualsWithStringCoercion(JValue value, JValue queryValue)
		{
			if (value.Equals(queryValue))
			{
				return true;
			}
			if ((value.Type == JTokenType.Integer && queryValue.Type == JTokenType.Float) || (value.Type == JTokenType.Float && queryValue.Type == JTokenType.Integer))
			{
				return JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			if (queryValue.Type != JTokenType.String)
			{
				return false;
			}
			string text = (string)queryValue.Value;
			string text2;
			switch (value.Type)
			{
			case JTokenType.Date:
			{
				using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
				{
					object value2 = value.Value;
					if (value2 is DateTimeOffset)
					{
						DateTimeOffset dateTimeOffset = (DateTimeOffset)value2;
						DateTimeUtils.WriteDateTimeOffsetString(stringWriter, dateTimeOffset, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					else
					{
						DateTimeUtils.WriteDateTimeString(stringWriter, (DateTime)value.Value, DateFormatHandling.IsoDateFormat, null, CultureInfo.InvariantCulture);
					}
					text2 = stringWriter.ToString();
					goto IL_0122;
				}
				break;
			}
			case JTokenType.Raw:
				return false;
			case JTokenType.Bytes:
				break;
			case JTokenType.Guid:
			case JTokenType.TimeSpan:
				text2 = value.Value.ToString();
				goto IL_0122;
			case JTokenType.Uri:
				text2 = ((Uri)value.Value).OriginalString;
				goto IL_0122;
			default:
				return false;
			}
			text2 = Convert.ToBase64String((byte[])value.Value);
			IL_0122:
			return string.Equals(text2, text, StringComparison.Ordinal);
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00030780 File Offset: 0x0002E980
		internal static bool EqualsWithStrictMatch(JValue value, JValue queryValue)
		{
			if ((value.Type == JTokenType.Integer && queryValue.Type == JTokenType.Float) || (value.Type == JTokenType.Float && queryValue.Type == JTokenType.Integer))
			{
				return JValue.Compare(value.Type, value.Value, queryValue.Value) == 0;
			}
			return value.Type == queryValue.Type && value.Equals(queryValue);
		}

		// Token: 0x040003C9 RID: 969
		public readonly object Left;

		// Token: 0x040003CA RID: 970
		[Nullable(2)]
		public readonly object Right;
	}
}
