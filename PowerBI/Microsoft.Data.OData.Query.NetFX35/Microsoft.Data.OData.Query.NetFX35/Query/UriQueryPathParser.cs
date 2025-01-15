using System;
using System.Collections.Generic;
using Microsoft.Data.OData;

namespace Microsoft.Data.Experimental.OData.Query
{
	// Token: 0x02000059 RID: 89
	internal class UriQueryPathParser
	{
		// Token: 0x06000233 RID: 563 RVA: 0x0000C272 File Offset: 0x0000A472
		internal UriQueryPathParser(int maxDepth)
		{
			this.maxDepth = maxDepth;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C284 File Offset: 0x0000A484
		internal QueryToken ParseUri(Uri queryUri, Uri serviceBaseUri)
		{
			QueryToken queryToken = this.CreatePathSegments(queryUri, serviceBaseUri);
			if (queryToken == null)
			{
				queryToken = new SegmentQueryToken(string.Empty, null, null);
			}
			return queryToken;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000C2AC File Offset: 0x0000A4AC
		private static bool ExtractSegmentIdentifier(string segment, out string identifier)
		{
			int num = segment.IndexOf('(');
			if (num < 0)
			{
				identifier = segment;
				return false;
			}
			identifier = segment.Substring(0, num);
			return true;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000C2D8 File Offset: 0x0000A4D8
		private static string RemoveKeyValuesParens(string keyValues)
		{
			if (keyValues.Length <= 0 || keyValues.get_Chars(0) != '(' || keyValues.get_Chars(keyValues.Length - 1) != ')')
			{
				throw new ODataException(Strings.UriQueryPathParser_SyntaxError);
			}
			return keyValues.Substring(1, keyValues.Length - 2);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000C328 File Offset: 0x0000A528
		private static IEnumerable<NamedValue> ParseKeyValues(string keyValuesString)
		{
			keyValuesString = UriQueryPathParser.RemoveKeyValuesParens(keyValuesString);
			IEnumerable<NamedValue> enumerable = UriQueryPathParser.ParseKeyValuesFromUri(keyValuesString);
			if (enumerable == null)
			{
				throw new ODataException(Strings.UriQueryPathParser_SyntaxError);
			}
			return enumerable;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000C354 File Offset: 0x0000A554
		private static IEnumerable<NamedValue> ParseKeyValuesFromUri(string text)
		{
			ExpressionLexer expressionLexer = new ExpressionLexer(text, true);
			ExpressionToken expressionToken = expressionLexer.CurrentToken;
			if (expressionToken.Kind == ExpressionTokenKind.End)
			{
				return UriQueryPathParser.EmptyKeyValues;
			}
			List<NamedValue> list = new List<NamedValue>();
			for (;;)
			{
				if (expressionToken.Kind == ExpressionTokenKind.Identifier)
				{
					string identifier = expressionLexer.CurrentToken.GetIdentifier();
					expressionLexer.NextToken();
					if (expressionLexer.CurrentToken.Kind != ExpressionTokenKind.Equal)
					{
						break;
					}
					expressionLexer.NextToken();
					if (!expressionLexer.CurrentToken.IsKeyValueToken)
					{
						goto Block_4;
					}
					list.Add(new NamedValue(identifier, UriQueryPathParser.ParseKeyValueLiteral(expressionLexer)));
				}
				else
				{
					if (!expressionToken.IsKeyValueToken)
					{
						goto IL_00A1;
					}
					list.Add(new NamedValue(null, UriQueryPathParser.ParseKeyValueLiteral(expressionLexer)));
				}
				expressionToken = expressionLexer.NextToken();
				if (expressionToken.Kind == ExpressionTokenKind.Comma)
				{
					expressionToken = expressionLexer.NextToken();
					if (expressionToken.Kind == ExpressionTokenKind.End)
					{
						goto Block_7;
					}
				}
				if (expressionToken.Kind == ExpressionTokenKind.End)
				{
					return list;
				}
			}
			return null;
			Block_4:
			return null;
			IL_00A1:
			return null;
			Block_7:
			return null;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000C438 File Offset: 0x0000A638
		private static LiteralQueryToken ParseKeyValueLiteral(ExpressionLexer lexer)
		{
			LiteralQueryToken literalQueryToken = UriQueryExpressionParser.TryParseLiteral(lexer);
			if (literalQueryToken == null)
			{
				throw new ODataException(Strings.UriQueryPathParser_InvalidKeyValueLiteral(lexer.CurrentToken.Text));
			}
			return literalQueryToken;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C468 File Offset: 0x0000A668
		private QueryToken CreatePathSegments(Uri queryUri, Uri serviceBaseUri)
		{
			List<string> list = this.EnumerateSegments(queryUri, serviceBaseUri);
			SegmentQueryToken segmentQueryToken = null;
			foreach (string text in list)
			{
				string text2;
				bool flag = UriQueryPathParser.ExtractSegmentIdentifier(text, out text2);
				string text3 = (flag ? text.Substring(text2.Length) : null);
				KeywordKind? keywordKind = QueryTokenUtils.ParseKeywordKind(text2);
				segmentQueryToken = ((keywordKind == null) ? new SegmentQueryToken(text2, segmentQueryToken, flag ? UriQueryPathParser.ParseKeyValues(text3) : null) : new KeywordSegmentQueryToken(keywordKind.Value, segmentQueryToken));
			}
			return segmentQueryToken;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000C514 File Offset: 0x0000A714
		private List<string> EnumerateSegments(Uri absoluteUri, Uri serviceBaseUri)
		{
			if (!UriUtils.UriInvariantInsensitiveIsBaseOf(serviceBaseUri, absoluteUri))
			{
				throw new ODataException(Strings.UriQueryPathParser_RequestUriDoesNotHaveTheCorrectBaseUri(absoluteUri, serviceBaseUri));
			}
			List<string> list2;
			try
			{
				int num = serviceBaseUri.Segments.Length;
				string[] segments = absoluteUri.Segments;
				List<string> list = new List<string>();
				for (int i = num; i < segments.Length; i++)
				{
					string text = segments[i];
					if (text.Length != 0 && text != "/")
					{
						if (text.get_Chars(text.Length - 1) == '/')
						{
							text = text.Substring(0, text.Length - 1);
						}
						if (list.Count == this.maxDepth)
						{
							throw new ODataException(Strings.UriQueryPathParser_TooManySegments);
						}
						list.Add(Uri.UnescapeDataString(text));
					}
				}
				list2 = list;
			}
			catch (UriFormatException ex)
			{
				throw new ODataException(Strings.UriQueryPathParser_SyntaxError, ex);
			}
			return list2;
		}

		// Token: 0x04000212 RID: 530
		private static NamedValue[] EmptyKeyValues = new NamedValue[0];

		// Token: 0x04000213 RID: 531
		private int maxDepth;
	}
}
