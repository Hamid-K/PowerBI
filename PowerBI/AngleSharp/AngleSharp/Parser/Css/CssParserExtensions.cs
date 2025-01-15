using System;
using System.Collections.Generic;
using AngleSharp.Css;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Parser.Css
{
	// Token: 0x0200007B RID: 123
	internal static class CssParserExtensions
	{
		// Token: 0x060003A7 RID: 935 RVA: 0x00018F98 File Offset: 0x00017198
		private static IConditionFunction CreateAndCondition(IEnumerable<IConditionFunction> conditions)
		{
			AndCondition andCondition = new AndCondition();
			foreach (IConditionFunction conditionFunction in conditions)
			{
				andCondition.AppendChild(conditionFunction);
			}
			return andCondition;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00018FE8 File Offset: 0x000171E8
		private static IConditionFunction CreateOrCondition(IEnumerable<IConditionFunction> conditions)
		{
			OrCondition orCondition = new OrCondition();
			foreach (IConditionFunction conditionFunction in conditions)
			{
				orCondition.AppendChild(conditionFunction);
			}
			return orCondition;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00019038 File Offset: 0x00017238
		public static CssTokenType GetTypeFromName(this string functionName)
		{
			Func<string, DocumentFunction> func = null;
			if (!CssParserExtensions.functionTypes.TryGetValue(functionName, out func))
			{
				return CssTokenType.Function;
			}
			return CssTokenType.Url;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0001905C File Offset: 0x0001725C
		public static Func<IEnumerable<IConditionFunction>, IConditionFunction> GetCreator(this string conjunction)
		{
			Func<IEnumerable<IConditionFunction>, IConditionFunction> func = null;
			CssParserExtensions.groupCreators.TryGetValue(conjunction, out func);
			return func;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000D9F4 File Offset: 0x0000BBF4
		public static int GetCode(this CssParseError code)
		{
			return (int)code;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001907C File Offset: 0x0001727C
		public static bool Is(this CssToken token, CssTokenType a, CssTokenType b)
		{
			CssTokenType type = token.Type;
			return type == a || type == b;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001909C File Offset: 0x0001729C
		public static bool IsNot(this CssToken token, CssTokenType a, CssTokenType b)
		{
			CssTokenType type = token.Type;
			return type != a && type != b;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000190C0 File Offset: 0x000172C0
		public static bool IsNot(this CssToken token, CssTokenType a, CssTokenType b, CssTokenType c)
		{
			CssTokenType type = token.Type;
			return type != a && type != b && type != c;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000190E8 File Offset: 0x000172E8
		public static bool IsDeclarationName(this CssToken token)
		{
			return token.Type != CssTokenType.EndOfFile && token.Type != CssTokenType.Colon && token.Type != CssTokenType.Whitespace && token.Type != CssTokenType.Comment && token.Type != CssTokenType.CurlyBracketOpen && token.Type != CssTokenType.Semicolon;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00019138 File Offset: 0x00017338
		public static DocumentFunction ToDocumentFunction(this CssToken token)
		{
			if (token.Type == CssTokenType.Url)
			{
				Func<string, DocumentFunction> func = null;
				string functionName = ((CssUrlToken)token).FunctionName;
				CssParserExtensions.functionTypes.TryGetValue(functionName, out func);
				return func(token.Data);
			}
			if (token.Type == CssTokenType.Function && token.Data.Isi(FunctionNames.Regexp))
			{
				string text = ((CssFunctionToken)token).ArgumentTokens.ToCssString();
				if (text != null)
				{
					return new RegexpFunction(text);
				}
			}
			return null;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000191B0 File Offset: 0x000173B0
		public static CssRule CreateRule(this CssParser parser, CssRuleType type)
		{
			switch (type)
			{
			case CssRuleType.Style:
				return new CssStyleRule(parser);
			case CssRuleType.Charset:
				return new CssCharsetRule(parser);
			case CssRuleType.Import:
				return new CssImportRule(parser);
			case CssRuleType.Media:
				return new CssMediaRule(parser);
			case CssRuleType.FontFace:
				return new CssFontFaceRule(parser);
			case CssRuleType.Page:
				return new CssPageRule(parser);
			case CssRuleType.Keyframes:
				return new CssKeyframesRule(parser);
			case CssRuleType.Keyframe:
				return new CssKeyframeRule(parser);
			case CssRuleType.Namespace:
				return new CssNamespaceRule(parser);
			case CssRuleType.Supports:
				return new CssSupportsRule(parser);
			case CssRuleType.Document:
				return new CssDocumentRule(parser);
			case CssRuleType.Viewport:
				return new CssViewportRule(parser);
			}
			return null;
		}

		// Token: 0x040002F0 RID: 752
		private static readonly Dictionary<string, Func<string, DocumentFunction>> functionTypes = new Dictionary<string, Func<string, DocumentFunction>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				FunctionNames.Url,
				(string str) => new UrlFunction(str)
			},
			{
				FunctionNames.Domain,
				(string str) => new DomainFunction(str)
			},
			{
				FunctionNames.UrlPrefix,
				(string str) => new UrlPrefixFunction(str)
			}
		};

		// Token: 0x040002F1 RID: 753
		private static readonly Dictionary<string, Func<IEnumerable<IConditionFunction>, IConditionFunction>> groupCreators = new Dictionary<string, Func<IEnumerable<IConditionFunction>, IConditionFunction>>(StringComparer.OrdinalIgnoreCase)
		{
			{
				Keywords.And,
				new Func<IEnumerable<IConditionFunction>, IConditionFunction>(CssParserExtensions.CreateAndCondition)
			},
			{
				Keywords.Or,
				new Func<IEnumerable<IConditionFunction>, IConditionFunction>(CssParserExtensions.CreateOrCondition)
			}
		};
	}
}
