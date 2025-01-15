using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.DslLibrary.EntityDetectors;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.DslLibrary
{
	// Token: 0x0200080B RID: 2059
	public abstract class Token : IEquatable<Token>
	{
		// Token: 0x06002C21 RID: 11297 RVA: 0x0007BD18 File Offset: 0x00079F18
		static Token()
		{
			int num = 101;
			double log256_4 = Math.Log(1024.0);
			double log22_7 = Math.Log(154.0);
			double log12_60 = Math.Log(720.0);
			double log365 = Math.Log(365.0);
			double log64 = Math.Log(64.0);
			double log62 = Math.Log(62.0);
			double log54 = Math.Log(54.0);
			double log52 = Math.Log(52.0);
			double log26 = Math.Log(26.0);
			double log10 = Math.Log(10.0);
			double log2 = Math.Log(2.0);
			Dictionary<string, Token> dictionary = new Dictionary<string, Token>();
			dictionary["',' or 'and'"] = new RegexToken("(?:\\p{Zs}|\\r?\\n)*(?:,(?:\\p{Zs}|\\r?\\n)*and|\\band\\b|,)(?:\\p{Zs}|\\r?\\n)*", "',' or 'and'", num, -5.5, (string s) => -log2, false, true, ", ");
			dictionary["Camel Case"] = new RegexToken("\\p{Lu}(\\p{Ll})+", "Camel Case", ++num, -5.5, (string s) => -2.3 - log26 * (double)s.Length, false, true, null);
			dictionary["Lowercase word"] = new RegexToken("\\p{Ll}+", "Lowercase word", ++num, -5.5, (string s) => -2.3 - log26 * (double)s.Length, false, true, null);
			dictionary["ALL CAPS"] = new RegexToken("\\p{Lu}+", "ALL CAPS", num, -5.5, (string s) => -2.3 - log26 * (double)s.Length, false, true, null);
			dictionary["Number"] = new RegexToken("[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?", "Number", ++num, -5.5, (string s) => -2.3 - log2 - log10 * (double)(s.Length - ((s.Contains('.') > false) ? 1 : 0)), false, true, null);
			dictionary["SignedNumber"] = new RegexToken("-?[0-9]+(\\,[0-9]{3})*(\\.[0-9]+)?", "SignedNumber", num, -5.5, (string s) => -2.3 - log2 - log10 * (double)(s.Length - ((s.Contains('.') > false) ? 1 : 0)), false, false, null);
			dictionary["Digits"] = new RegexToken("[0-9]+", "Digits", num, -5.5, (string s) => -2.3 - log10 * (double)s.Length, false, false, null);
			dictionary["Words/dots/hyphens"] = new RegexToken("[-.\\p{Lu}\\p{Ll}]+", "Words/dots/hyphens", num, -5.5, (string s) => -2.3 - log54 * (double)s.Length, false, true, null);
			dictionary["Alphabet"] = new RegexToken("[\\p{Lu}\\p{Ll}]+", "Alphabet", num, -5.5, (string s) => -2.3 - log52 * (double)s.Length, false, true, null);
			dictionary["Alphanumeric"] = new RegexToken("[-.\\p{Lu}\\p{Ll}0-9]+", "Alphanumeric", ++num, -5.5, (string s) => -2.3 - log64 * (double)s.Length, false, true, null);
			dictionary["Alphanum"] = new RegexToken("[\\p{Lu}\\p{Ll}0-9]+", "Alphanum", num, -5.5, (string s) => -2.3 - log62 * (double)s.Length, false, true, null);
			dictionary["WhiteSpace"] = new RegexToken("\\p{Zs}+", "WhiteSpace", ++num, -5.5, (string s) => 0.0, false, true, " ");
			dictionary["Tab"] = new StringToken("\t", ++num, -5.5, "Tab", true, true, (string s) => 0.0);
			dictionary["Comma"] = new StringToken(",", num, -5.5, "Comma", true, true, (string s) => 0.0);
			dictionary["Dot"] = new StringToken(".", num, -5.5, "Dot", true, true, (string s) => 0.0);
			dictionary["Colon"] = new StringToken(":", num, -5.5, "Colon", true, true, (string s) => 0.0);
			dictionary["Semicolon"] = new StringToken(";", num, -5.5, "Semicolon", true, true, (string s) => 0.0);
			dictionary["Exclamation"] = new StringToken("!", num, -5.5, "Exclamation", true, true, (string s) => 0.0);
			dictionary["Right Parenthesis"] = new StringToken(")", num, -5.5, "Right Parenthesis", true, true, (string s) => 0.0);
			dictionary["Left Parenthesis"] = new StringToken("(", num, -5.5, "Left Parenthesis", true, true, (string s) => 0.0);
			dictionary["Double Quote"] = new StringToken("\"", num, -5.5, "Double Quote", true, true, (string s) => 0.0);
			dictionary["Single Quote"] = new StringToken("'", num, -5.5, "Single Quote", true, true, (string s) => 0.0);
			dictionary["Forward Slash"] = new StringToken("/", num, -5.5, "Forward Slash", true, true, (string s) => 0.0);
			dictionary["Backward Slash"] = new StringToken("\\", num, -5.5, "Backward Slash", true, true, (string s) => 0.0);
			dictionary["Hyphen"] = new StringToken("-", num, -5.5, "Hyphen", true, true, (string s) => 0.0);
			dictionary["Star"] = new StringToken("*", num, -5.5, "Star", true, true, (string s) => 0.0);
			dictionary["Plus"] = new StringToken("+", num, -5.5, "Plus", true, true, (string s) => 0.0);
			dictionary["Underscore"] = new StringToken("_", num, -5.5, "Underscore", true, true, (string s) => 0.0);
			dictionary["Equal"] = new StringToken("=", num, -5.5, "Equal", true, true, (string s) => 0.0);
			dictionary["Greater-than"] = new StringToken(">", num, -5.5, "Greater-than", true, true, (string s) => 0.0);
			dictionary["Left-than"] = new StringToken("<", num, -5.5, "Left-than", true, true, (string s) => 0.0);
			dictionary["Right Bracket"] = new StringToken("]", num, -5.5, "Right Bracket", true, true, (string s) => 0.0);
			dictionary["Left Bracket"] = new StringToken("[", num, -5.5, "Left Bracket", true, true, (string s) => 0.0);
			dictionary["Right Brace"] = new StringToken("}", num, -5.5, "Right Brace", true, true, (string s) => 0.0);
			dictionary["Left Brace"] = new StringToken("{", num, -5.5, "Left Brace", true, true, (string s) => 0.0);
			dictionary["Bar"] = new StringToken("|", num, -5.5, "Bar", true, true, (string s) => 0.0);
			dictionary["Ampersand"] = new StringToken("&", num, -5.5, "Ampersand", true, true, (string s) => 0.0);
			dictionary["Hash"] = new StringToken("#", num, -5.5, "Hash", true, true, (string s) => 0.0);
			dictionary["Dollar"] = new StringToken("$", num, -5.5, "Dollar", true, true, (string s) => 0.0);
			dictionary["Hat"] = new StringToken("^", num, -5.5, "Hat", true, true, (string s) => 0.0);
			dictionary["At"] = new StringToken("@", num, -5.5, "At", true, true, (string s) => 0.0);
			dictionary["Percentage"] = new StringToken("%", num, -5.5, "Percentage", true, true, (string s) => 0.0);
			dictionary["Question Mark"] = new StringToken("?", num, -5.5, "Question Mark", true, true, (string s) => 0.0);
			dictionary["Tilde"] = new StringToken("~", num, -5.5, "Tilde", true, true, (string s) => 0.0);
			dictionary["Back Prime"] = new StringToken("`", num, -5.5, "Back Prime", true, true, (string s) => 0.0);
			dictionary["RightArrow"] = new StringToken("→", num, -5.5, "RightArrow", true, true, (string s) => 0.0);
			dictionary["LeftArrow"] = new StringToken("←", num, -5.5, "LeftArrow", true, true, (string s) => 0.0);
			dictionary["Empty Line"] = new RegexToken("(?<=\\n)[\\p{Zs}\\t]*(\\r)?\\n", "Empty Line", ++num, -5.5, (string s) => 0.0, true, true, null);
			dictionary["Line Separator"] = new RegexToken("(?<![\\p{Zs}\\t])[\\p{Zs}\\t]*((\\r)?\\n|^|(?<!\\n)$)", "Line Separator", ++num, -5.5, (string s) => 0.0, false, true, "\n");
			dictionary["Date"] = new RegexToken("((?<!\\d)(\\d?\\d)(-(\\d?\\d)-|\\/(\\d?\\d)\\/|\\.(\\d?\\d)\\.)(19|20)?\\d\\d(?!\\d)|(?<!\\d)(19|20)?\\d\\d(-(\\d?\\d)-|\\/(\\d?\\d)\\/|\\.(\\d?\\d)\\.)(\\d?\\d)(?!\\d))", "Date", ++num, -5.5, (string s) => -log365, true, true, null);
			dictionary["Time"] = new RegexToken("(?<!\\d)([0-2])?\\d:[0-6]\\d(:[0-6]\\d(\\.\\d+)?)?(\\s)*([AaPp][Mm])?(?!\\d)", "Time", num, -5.5, (string s) => -log12_60, true, true, null);
			dictionary["IP"] = new RegexToken("(?<!\\d)(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)(?!\\d)", "IP", num, -5.5, (string s) => -log256_4, true, true, null);
			dictionary["Email"] = new RegexToken("\\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\\.[A-Za-z]{2,4}\\b", "Email", num, -5.5, (string s) => -10.0, true, true, null);
			dictionary["MAC"] = new RegexToken("\\b([0-9A-Fa-f]{2}[:-]){5}([0-9A-Fa-f]{2})\\b", "MAC", num, -5.5, (string s) => -log22_7, true, true, null);
			Dictionary<string, Token> dictionary2 = dictionary;
			Dictionary<string, Token> dictionary3 = new Dictionary<string, Token>();
			dictionary3["',' or 'and'"] = new RegexToken("(?:\\s|\\r?\\n)*(?:,(?:\\s|\\r?\\n)*and|\\band\\b|,)(?:\\s|\\r?\\n)*", "',' or 'and'", num, -5.5, (string s) => -log2, false, true, ", ");
			dictionary3["Camel Case"] = new RegexToken("[A-Z][a-z]+", "Camel Case", ++num, -5.5, (string s) => -2.3 - log26 * (double)s.Length, false, true, null);
			dictionary3["Lowercase word"] = new RegexToken("[a-z]+", "Lowercase word", ++num, -5.5, (string s) => -2.3 - log26 * (double)s.Length, false, true, null);
			dictionary3["ALL CAPS"] = new RegexToken("[A-Z]+", "ALL CAPS", num, -5.5, (string s) => -2.3 - log26 * (double)s.Length, false, true, null);
			dictionary3["Number"] = dictionary2["Number"];
			dictionary3["SignedNumber"] = dictionary2["SignedNumber"];
			dictionary3["Digits"] = dictionary2["Digits"];
			dictionary3["Words/dots/hyphens"] = new RegexToken("[-.A-Za-z]+", "Words/dots/hyphens", num, -5.5, (string s) => -2.3 - log54 * (double)s.Length, false, true, null);
			dictionary3["Alphabet"] = new RegexToken("[A-Za-z]+", "Alphabet", num, -5.5, (string s) => -2.3 - log52 * (double)s.Length, false, true, null);
			dictionary3["Alphanumeric"] = new RegexToken("[-.A-Za-z0-9]+", "Alphanumeric", ++num, -5.5, (string s) => -2.3 - log64 * (double)s.Length, false, true, null);
			dictionary3["Alphanum"] = new RegexToken("[A-Za-z0-9]+", "Alphanum", num, -5.5, (string s) => -2.3 - log62 * (double)s.Length, false, true, null);
			dictionary3["WhiteSpace"] = new RegexToken("\\s+", "WhiteSpace", ++num, -5.5, (string s) => 0.0, false, true, " ");
			dictionary3["Tab"] = dictionary2["Tab"];
			dictionary3["Comma"] = dictionary2["Comma"];
			dictionary3["Dot"] = dictionary2["Dot"];
			dictionary3["Colon"] = dictionary2["Colon"];
			dictionary3["Semicolon"] = dictionary2["Semicolon"];
			dictionary3["Exclamation"] = dictionary2["Exclamation"];
			dictionary3["Right Parenthesis"] = dictionary2["Right Parenthesis"];
			dictionary3["Left Parenthesis"] = dictionary2["Left Parenthesis"];
			dictionary3["Double Quote"] = dictionary2["Double Quote"];
			dictionary3["Single Quote"] = dictionary2["Single Quote"];
			dictionary3["Forward Slash"] = dictionary2["Forward Slash"];
			dictionary3["Backward Slash"] = dictionary2["Backward Slash"];
			dictionary3["Hyphen"] = dictionary2["Hyphen"];
			dictionary3["Star"] = dictionary2["Star"];
			dictionary3["Plus"] = dictionary2["Plus"];
			dictionary3["Underscore"] = dictionary2["Underscore"];
			dictionary3["Equal"] = dictionary2["Equal"];
			dictionary3["Greater-than"] = dictionary2["Greater-than"];
			dictionary3["Left-than"] = dictionary2["Left-than"];
			dictionary3["Right Bracket"] = dictionary2["Right Bracket"];
			dictionary3["Left Bracket"] = dictionary2["Left Bracket"];
			dictionary3["Right Brace"] = dictionary2["Right Brace"];
			dictionary3["Left Brace"] = dictionary2["Left Brace"];
			dictionary3["Bar"] = dictionary2["Bar"];
			dictionary3["Ampersand"] = dictionary2["Ampersand"];
			dictionary3["Hash"] = dictionary2["Hash"];
			dictionary3["Dollar"] = dictionary2["Dollar"];
			dictionary3["Hat"] = dictionary2["Hat"];
			dictionary3["At"] = dictionary2["At"];
			dictionary3["Percentage"] = dictionary2["Percentage"];
			dictionary3["Question Mark"] = dictionary2["Question Mark"];
			dictionary3["Tilde"] = dictionary2["Tilde"];
			dictionary3["Back Prime"] = dictionary2["Back Prime"];
			dictionary3["RightArrow"] = dictionary2["RightArrow"];
			dictionary3["LeftArrow"] = dictionary2["LeftArrow"];
			dictionary3["Empty Line"] = new RegexToken("(?<=\\n)[\\s\\t]*(\\r)?\\n", "Empty Line", ++num, -5.5, (string s) => 0.0, true, true, null);
			dictionary3["Line Separator"] = new RegexToken("(?<![\\s\\t])[\\s\\t]*((\\r)?\\n|^|(?<!\\n)$)", "Line Separator", num + 1, -5.5, (string s) => 0.0, false, true, "\n");
			dictionary3["Date"] = dictionary2["Date"];
			dictionary3["Time"] = dictionary2["Time"];
			dictionary3["IP"] = dictionary2["IP"];
			dictionary3["Email"] = dictionary2["Email"];
			dictionary3["MAC"] = dictionary2["MAC"];
			Token.RegisterTokens(dictionary2);
			Token.RegisterTokensAscii(dictionary3);
			Dictionary<string, Token> dictionary4 = new Dictionary<string, Token>(dictionary2);
			dictionary4.Remove("Alphabet");
			dictionary4.Remove("Alphanum");
			Token.Tokens = dictionary4;
			Dictionary<string, Token> dictionary5 = new Dictionary<string, Token>(dictionary3);
			dictionary5.Remove("Alphabet");
			dictionary5.Remove("Alphanum");
			Token.TokensAscii = dictionary5;
			dictionary2.Remove("Alphanumeric");
			dictionary2.Remove("Words/dots/hyphens");
			dictionary2.Remove("Number");
			RegexToken regexToken = (RegexToken)dictionary2["SignedNumber"];
			dictionary2["SignedNumber"] = new RegexToken(regexToken.Regex, regexToken.Name, regexToken.Score, regexToken.LogPrior, new Func<string, double>(regexToken.EvaluateLogLikelihood), regexToken.IsSymbol, true, regexToken.CanonicalRepresentation);
			RegexToken regexToken2 = (RegexToken)dictionary2["Digits"];
			dictionary2["Digits"] = new RegexToken(regexToken2.Regex, regexToken2.Name, regexToken2.Score, regexToken2.LogPrior, new Func<string, double>(regexToken2.EvaluateLogLikelihood), regexToken2.IsSymbol, true, regexToken2.CanonicalRepresentation);
			Token.NonDisjunctiveTokens = dictionary2;
			dictionary3.Remove("Alphanumeric");
			dictionary3.Remove("Words/dots/hyphens");
			dictionary3.Remove("Number");
			dictionary3["SignedNumber"] = dictionary2["SignedNumber"];
			dictionary3["Digits"] = dictionary2["Digits"];
			Token.NonDisjunctiveTokensAscii = dictionary3;
			Dictionary<string, Token> dictionary6 = new Dictionary<string, Token>();
			dictionary6["Digits"] = regexToken2;
			dictionary6["ALL CAPS"] = dictionary2["ALL CAPS"];
			dictionary6["Lowercase word"] = dictionary2["Lowercase word"];
			dictionary6["Camel Case"] = dictionary2["Camel Case"];
			Token.DateTimeTokens = dictionary6;
		}

		// Token: 0x06002C22 RID: 11298 RVA: 0x0007D17D File Offset: 0x0007B37D
		protected Token(string name, int score, double logPrior, bool isSymbol = true, bool useForLearning = true, string canonicalRepresentation = null, Func<string, double> evaluateLogLikelihood = null)
		{
			this.LogPrior = logPrior;
			this._evaluateLogLikelihood = evaluateLogLikelihood;
			this.Name = name;
			this.Score = score;
			this.IsSymbol = isSymbol;
			this.UseForLearning = useForLearning;
			this.CanonicalRepresentation = canonicalRepresentation;
		}

		// Token: 0x06002C23 RID: 11299 RVA: 0x0007D1BC File Offset: 0x0007B3BC
		public static void RegisterTokens(IEnumerable<KeyValuePair<string, Token>> tokens)
		{
			foreach (KeyValuePair<string, Token> keyValuePair in tokens)
			{
				Token.AllDslsTokens.GetOrAdd(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06002C24 RID: 11300 RVA: 0x0007D218 File Offset: 0x0007B418
		public static void RegisterTokensAscii(IEnumerable<KeyValuePair<string, Token>> tokens)
		{
			foreach (KeyValuePair<string, Token> keyValuePair in tokens)
			{
				Token.AllDslsTokensAscii.GetOrAdd(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x06002C25 RID: 11301 RVA: 0x0007D274 File Offset: 0x0007B474
		public virtual bool MatchesEntireString(string target)
		{
			return (from m in this.GetMatches(target).MaybeFirst<PositionMatch>()
				select (ulong)m.Length == (ulong)((long)target.Length)).OrElse(false);
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002C26 RID: 11302 RVA: 0x0007D2B6 File Offset: 0x0007B4B6
		public string Name { get; }

		// Token: 0x06002C27 RID: 11303
		public abstract IEnumerable<PositionMatch> GetMatches(string content);

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002C28 RID: 11304 RVA: 0x0007D2BE File Offset: 0x0007B4BE
		public string CanonicalRepresentation { get; }

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06002C29 RID: 11305 RVA: 0x0007D2C6 File Offset: 0x0007B4C6
		// (set) Token: 0x06002C2A RID: 11306 RVA: 0x0007D2CE File Offset: 0x0007B4CE
		public int Score { get; set; }

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06002C2B RID: 11307 RVA: 0x0007D2D7 File Offset: 0x0007B4D7
		public double LogPrior { get; }

		// Token: 0x06002C2C RID: 11308 RVA: 0x0007D2DF File Offset: 0x0007B4DF
		public double EvaluateLogLikelihood(string x)
		{
			return this._evaluateLogLikelihood(x);
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002C2D RID: 11309 RVA: 0x0007D2ED File Offset: 0x0007B4ED
		public bool UseForLearning { get; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06002C2E RID: 11310 RVA: 0x0007D2F5 File Offset: 0x0007B4F5
		public bool IsSymbol { get; }

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06002C2F RID: 11311 RVA: 0x0007D2FD File Offset: 0x0007B4FD
		public bool IsDynamicToken
		{
			get
			{
				return this.Name.StartsWith("\"", StringComparison.Ordinal);
			}
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x0007D310 File Offset: 0x0007B510
		public static StringToken BuildDynamic(string tokenString, int score = 50)
		{
			string tokenString2 = tokenString;
			string text = tokenString.ToLiteral(null);
			return new StringToken(tokenString2, score, -15.0 + (double)tokenString.Length * -4.1, text, true, true, delegate(string s)
			{
				if (!(s == tokenString))
				{
					return double.NegativeInfinity;
				}
				return 0.0;
			});
		}

		// Token: 0x06002C31 RID: 11313
		internal abstract XElement ToXml();

		// Token: 0x06002C32 RID: 11314 RVA: 0x0007D374 File Offset: 0x0007B574
		internal static Token TryParse(XElement xml, DeserializationContext context)
		{
			if ((xml.Name != "Token" && xml.Name != "StringToken" && xml.Name != "ExternalEntityToken") || xml.Attribute("name") == null || xml.Attribute("score") == null || xml.Attribute("isSymbol") == null)
			{
				return null;
			}
			Token token;
			try
			{
				string value = xml.Attribute("name").Value;
				Optional<Token> staticTokenByName = Token.GetStaticTokenByName(value);
				if (staticTokenByName.HasValue && XNode.DeepEquals(xml, staticTokenByName.Value.ToXml()))
				{
					token = staticTokenByName.Value;
				}
				else
				{
					Optional<Token> staticTokenByNameAscii = Token.GetStaticTokenByNameAscii(value);
					if (staticTokenByNameAscii.HasValue && XNode.DeepEquals(xml, staticTokenByNameAscii.Value.ToXml()))
					{
						token = staticTokenByNameAscii.Value;
					}
					else
					{
						int num = Convert.ToInt32(xml.Attribute("score").Value, CultureInfo.InvariantCulture);
						bool flag = Convert.ToBoolean(xml.Attribute("isSymbol").Value, CultureInfo.InvariantCulture);
						if (xml.Name == "ExternalEntityToken")
						{
							EntityDetector entityDetector = EntityDetector.TryParseFromXML(xml.Elements().First<XElement>(), context);
							if (entityDetector == null)
							{
								throw new Exception("The entity detector named " + value + " is not found in the EntityDetectorsMap, deserialization context");
							}
							token = new ExternalEntityToken(entityDetector);
						}
						else if (xml.HasElements)
						{
							token = null;
						}
						else if (xml.Name == "StringToken")
						{
							token = new StringToken(xml.Value, num, 0.0, value, flag, false, (string x) => 0.0);
						}
						else
						{
							if (staticTokenByName.HasValue)
							{
								AbstractRegexToken abstractRegexToken = staticTokenByName.Value as AbstractRegexToken;
								if (abstractRegexToken != null && abstractRegexToken.Regex.ToString() == xml.Value)
								{
									return staticTokenByName.Value;
								}
							}
							if (staticTokenByNameAscii.HasValue)
							{
								AbstractRegexToken abstractRegexToken2 = staticTokenByNameAscii.Value as AbstractRegexToken;
								if (abstractRegexToken2 != null && abstractRegexToken2.Regex.ToString() == xml.Value)
								{
									return staticTokenByNameAscii.Value;
								}
							}
							token = new RegexToken(xml.Value, staticTokenByName.HasValue ? (value + " (deserialized: pattern=" + xml.Value + ")") : value, num, 0.0, (string x) => 0.0, flag, false, null);
						}
					}
				}
			}
			catch
			{
				token = null;
			}
			return token;
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x0007D670 File Offset: 0x0007B870
		internal static Optional<Token> TryParse(string literal)
		{
			if (literal[0] != '"' || literal.Last<char>() != '"')
			{
				return Token.GetStaticTokenByName(literal);
			}
			return from s in StdLiteralParsing.TryParse<string>(literal, default(DeserializationContext))
				select Token.BuildDynamic(s, 50);
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x0007D6CC File Offset: 0x0007B8CC
		internal static Optional<Token> GetStaticTokenByName(string name)
		{
			return Token.AllDslsTokens.MaybeGet(name);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x0007D6D9 File Offset: 0x0007B8D9
		private static Optional<Token> GetStaticTokenByNameAscii(string name)
		{
			return Token.AllDslsTokensAscii.MaybeGet(name);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0007D6E6 File Offset: 0x0007B8E6
		public static Token FromRegex(Regex regex, int score)
		{
			return new RegexToken(regex, regex.ToString(), score, 0.0, null, true, true, null);
		}

		// Token: 0x06002C37 RID: 11319
		public abstract Token Clone();

		// Token: 0x06002C38 RID: 11320 RVA: 0x0000BE9E File Offset: 0x0000A09E
		public static bool operator ==(Token left, Token right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
		public static bool operator !=(Token left, Token right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x0007D702 File Offset: 0x0007B902
		public bool Equals(Token other)
		{
			return !(other == null) && (this == other || this.Name == other.Name);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x0007D726 File Offset: 0x0007B926
		public override bool Equals(object obj)
		{
			return obj is Token && this.Equals((Token)obj);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0007D73E File Offset: 0x0007B93E
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x0007D74B File Offset: 0x0007B94B
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04001516 RID: 5398
		public static readonly IReadOnlyDictionary<string, Token> Tokens;

		// Token: 0x04001517 RID: 5399
		public static readonly IReadOnlyDictionary<string, Token> TokensAscii;

		// Token: 0x04001518 RID: 5400
		public static readonly IReadOnlyDictionary<string, Token> NonDisjunctiveTokens;

		// Token: 0x04001519 RID: 5401
		public static readonly IReadOnlyDictionary<string, Token> NonDisjunctiveTokensAscii;

		// Token: 0x0400151A RID: 5402
		internal static readonly IReadOnlyDictionary<string, Token> DateTimeTokens;

		// Token: 0x0400151B RID: 5403
		public const int MinScore = 100;

		// Token: 0x0400151C RID: 5404
		public const double VariableLengthPenalty = -2.3;

		// Token: 0x0400151D RID: 5405
		public const double LogDefaultStaticPrior = -5.5;

		// Token: 0x0400151E RID: 5406
		public const string LineSeparatorName = "Line Separator";

		// Token: 0x0400151F RID: 5407
		public const string DateName = "Date";

		// Token: 0x04001520 RID: 5408
		public const string AlphanumericName = "Alphanumeric";

		// Token: 0x04001521 RID: 5409
		public const string CamelCaseName = "Camel Case";

		// Token: 0x04001522 RID: 5410
		public const string LowerCaseName = "Lowercase word";

		// Token: 0x04001523 RID: 5411
		public const string UpperCaseName = "ALL CAPS";

		// Token: 0x04001524 RID: 5412
		public const string NumberName = "Number";

		// Token: 0x04001525 RID: 5413
		public const string SignedNumberName = "SignedNumber";

		// Token: 0x04001526 RID: 5414
		public const string DigitsName = "Digits";

		// Token: 0x04001527 RID: 5415
		public const string WordDotHyphenName = "Words/dots/hyphens";

		// Token: 0x04001528 RID: 5416
		public const string AlphabetName = "Alphabet";

		// Token: 0x04001529 RID: 5417
		public const string AlphanumName = "Alphanum";

		// Token: 0x0400152A RID: 5418
		public const string PositiveIntegerName = "PositiveInteger";

		// Token: 0x0400152B RID: 5419
		public const string DoubleQuoteName = "Double Quote";

		// Token: 0x0400152C RID: 5420
		internal const int DynamicTokenScore = 50;

		// Token: 0x0400152D RID: 5421
		public const double DynamicLogPrior = -15.0;

		// Token: 0x0400152E RID: 5422
		public const double DynamicLogPriorPerCharacter = -4.1;

		// Token: 0x0400152F RID: 5423
		private static readonly Dictionary<string, Token> AllDslsTokens = new Dictionary<string, Token>();

		// Token: 0x04001530 RID: 5424
		private static readonly Dictionary<string, Token> AllDslsTokensAscii = new Dictionary<string, Token>();

		// Token: 0x04001535 RID: 5429
		private readonly Func<string, double> _evaluateLogLikelihood;
	}
}
