using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Extensions;
using AngleSharp.Services;

namespace AngleSharp.Parser.Css
{
	// Token: 0x0200007A RID: 122
	public class CssParser
	{
		// Token: 0x06000388 RID: 904 RVA: 0x00018AAB File Offset: 0x00016CAB
		public CssParser()
			: this(Configuration.Default)
		{
		}

		// Token: 0x06000389 RID: 905 RVA: 0x00018AB8 File Offset: 0x00016CB8
		public CssParser(CssParserOptions options)
			: this(options, Configuration.Default)
		{
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00018AC8 File Offset: 0x00016CC8
		public CssParser(IConfiguration configuration)
			: this(default(CssParserOptions), configuration)
		{
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00018AE5 File Offset: 0x00016CE5
		public CssParser(CssParserOptions options, IConfiguration configuration)
		{
			this._options = options;
			this._configuration = configuration ?? Configuration.Default;
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600038C RID: 908 RVA: 0x00018B04 File Offset: 0x00016D04
		public CssParserOptions Options
		{
			get
			{
				return this._options;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00018B0C File Offset: 0x00016D0C
		public IConfiguration Config
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00018B14 File Offset: 0x00016D14
		public ICssStyleSheet ParseStylesheet(string content)
		{
			TextSource textSource = new TextSource(content);
			return this.ParseStylesheet(textSource);
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00018B30 File Offset: 0x00016D30
		public ICssStyleSheet ParseStylesheet(Stream content)
		{
			TextSource textSource = new TextSource(content, null);
			return this.ParseStylesheet(textSource);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00018B4C File Offset: 0x00016D4C
		public Task<ICssStyleSheet> ParseStylesheetAsync(string content)
		{
			return this.ParseStylesheetAsync(content, CancellationToken.None);
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00018B5C File Offset: 0x00016D5C
		public async Task<ICssStyleSheet> ParseStylesheetAsync(string content, CancellationToken cancelToken)
		{
			TextSource source = new TextSource(content);
			await source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
			return this.ParseStylesheet(source);
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00018BB1 File Offset: 0x00016DB1
		public Task<ICssStyleSheet> ParseStylesheetAsync(Stream content)
		{
			return this.ParseStylesheetAsync(content, CancellationToken.None);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00018BC0 File Offset: 0x00016DC0
		public async Task<ICssStyleSheet> ParseStylesheetAsync(Stream content, CancellationToken cancelToken)
		{
			TextSource source = new TextSource(content, null);
			await source.PrefetchAllAsync(cancelToken).ConfigureAwait(false);
			return this.ParseStylesheet(source);
		}

		// Token: 0x06000394 RID: 916 RVA: 0x00018C18 File Offset: 0x00016E18
		public ISelector ParseSelector(string selectorText)
		{
			CssTokenizer cssTokenizer = CssParser.CreateTokenizer(selectorText);
			CssToken cssToken = cssTokenizer.Get();
			CssSelectorConstructor selectorCreator = this.GetSelectorCreator();
			while (cssToken.Type != CssTokenType.EndOfFile)
			{
				selectorCreator.Apply(cssToken);
				cssToken = cssTokenizer.Get();
			}
			cssTokenizer.Dispose();
			bool isValid = selectorCreator.IsValid;
			ISelector selector = selectorCreator.ToPool();
			if (!isValid && !this._options.IsToleratingInvalidSelectors)
			{
				return null;
			}
			return selector;
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00018C81 File Offset: 0x00016E81
		public IKeyframeSelector ParseKeyframeSelector(string keyText)
		{
			return this.Parse<KeyframeSelector>(keyText, (CssBuilder b, CssToken t) => Tuple.Create<KeyframeSelector, CssToken>(b.CreateKeyframeSelector(ref t), t));
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00018CAC File Offset: 0x00016EAC
		internal CssSelectorConstructor GetSelectorCreator()
		{
			IAttributeSelectorFactory factory = this._configuration.GetFactory<IAttributeSelectorFactory>();
			IPseudoClassSelectorFactory factory2 = this._configuration.GetFactory<IPseudoClassSelectorFactory>();
			IPseudoElementSelectorFactory factory3 = this._configuration.GetFactory<IPseudoElementSelectorFactory>();
			return Pool.NewSelectorConstructor(factory, factory2, factory3);
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00018CE4 File Offset: 0x00016EE4
		internal ICssStyleSheet ParseStylesheet(TextSource source)
		{
			CssStyleSheet cssStyleSheet = new CssStyleSheet(this);
			CssTokenizer cssTokenizer = new CssTokenizer(source);
			TextPosition currentPosition = cssTokenizer.GetCurrentPosition();
			TextPosition textPosition = new CssBuilder(cssTokenizer, this).CreateRules(cssStyleSheet);
			TextRange textRange = new TextRange(currentPosition, textPosition);
			cssStyleSheet.SourceCode = new TextView(textRange, source);
			return cssStyleSheet;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00018D2C File Offset: 0x00016F2C
		internal async Task<CssStyleSheet> ParseStylesheetAsync(CssStyleSheet sheet, TextSource source)
		{
			await source.PrefetchAllAsync(CancellationToken.None).ConfigureAwait(false);
			CssTokenizer cssTokenizer = new CssTokenizer(source);
			TextPosition currentPosition = cssTokenizer.GetCurrentPosition();
			CssBuilder cssBuilder = new CssBuilder(cssTokenizer, this);
			Document document = sheet.GetDocument() as Document;
			List<Task> list = new List<Task>();
			TextPosition textPosition = cssBuilder.CreateRules(sheet);
			TextRange textRange = new TextRange(currentPosition, textPosition);
			sheet.SourceCode = new TextView(textRange, source);
			foreach (ICssRule cssRule in sheet.Rules)
			{
				if (cssRule.Type != CssRuleType.Charset)
				{
					if (cssRule.Type != CssRuleType.Import)
					{
						break;
					}
					list.Add(((CssImportRule)cssRule).LoadStylesheetFromAsync(document));
				}
			}
			await TaskEx.WhenAll(list).ConfigureAwait(false);
			return sheet;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00018D84 File Offset: 0x00016F84
		internal CssValue ParseValue(string valueText)
		{
			CssTokenizer cssTokenizer = CssParser.CreateTokenizer(valueText);
			CssToken cssToken = null;
			CssValue cssValue = new CssBuilder(cssTokenizer, this).CreateValue(ref cssToken);
			if (cssToken.Type != CssTokenType.EndOfFile)
			{
				return null;
			}
			return cssValue;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00018DB4 File Offset: 0x00016FB4
		internal CssRule ParseRule(string ruleText)
		{
			return this.Parse<CssRule>(ruleText, (CssBuilder b, CssToken t) => b.CreateRule(t));
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00018DDC File Offset: 0x00016FDC
		internal CssProperty ParseDeclaration(string declarationText)
		{
			return this.Parse<CssProperty>(declarationText, (CssBuilder b, CssToken t) => Tuple.Create<CssProperty, CssToken>(b.CreateDeclaration(ref t), t));
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00018E04 File Offset: 0x00017004
		internal List<CssMedium> ParseMediaList(string mediaText)
		{
			return this.Parse<List<CssMedium>>(mediaText, (CssBuilder b, CssToken t) => Tuple.Create<List<CssMedium>, CssToken>(b.CreateMedia(ref t), t));
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00018E2C File Offset: 0x0001702C
		internal IConditionFunction ParseCondition(string conditionText)
		{
			return this.Parse<IConditionFunction>(conditionText, (CssBuilder b, CssToken t) => Tuple.Create<IConditionFunction, CssToken>(b.CreateCondition(ref t), t));
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00018E54 File Offset: 0x00017054
		internal List<DocumentFunction> ParseDocumentRules(string documentText)
		{
			return this.Parse<List<DocumentFunction>>(documentText, (CssBuilder b, CssToken t) => Tuple.Create<List<DocumentFunction>, CssToken>(b.CreateFunctions(ref t), t));
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00018E7C File Offset: 0x0001707C
		internal CssMedium ParseMedium(string mediumText)
		{
			return this.Parse<CssMedium>(mediumText, (CssBuilder b, CssToken t) => Tuple.Create<CssMedium, CssToken>(b.CreateMedium(ref t), t));
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00018EA4 File Offset: 0x000170A4
		internal CssKeyframeRule ParseKeyframeRule(string ruleText)
		{
			return this.Parse<CssKeyframeRule>(ruleText, (CssBuilder b, CssToken t) => b.CreateKeyframeRule(t));
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00018ECC File Offset: 0x000170CC
		internal void AppendDeclarations(CssStyleDeclaration style, string declarations)
		{
			new CssBuilder(CssParser.CreateTokenizer(declarations), this).FillDeclarations(style);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00018EE4 File Offset: 0x000170E4
		private T Parse<T>(string source, Func<CssBuilder, CssToken, T> create)
		{
			CssTokenizer cssTokenizer = CssParser.CreateTokenizer(source);
			CssToken cssToken = cssTokenizer.Get();
			CssBuilder cssBuilder = new CssBuilder(cssTokenizer, this);
			T t = create(cssBuilder, cssToken);
			if (cssTokenizer.Get().Type != CssTokenType.EndOfFile)
			{
				return default(T);
			}
			return t;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00018F28 File Offset: 0x00017128
		private T Parse<T>(string source, Func<CssBuilder, CssToken, Tuple<T, CssToken>> create)
		{
			CssTokenizer cssTokenizer = CssParser.CreateTokenizer(source);
			CssToken cssToken = cssTokenizer.Get();
			CssBuilder cssBuilder = new CssBuilder(cssTokenizer, this);
			Tuple<T, CssToken> tuple = create(cssBuilder, cssToken);
			if (tuple.Item2.Type != CssTokenType.EndOfFile)
			{
				return default(T);
			}
			return tuple.Item1;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00018F71 File Offset: 0x00017171
		private static CssTokenizer CreateTokenizer(string sourceCode)
		{
			return new CssTokenizer(new TextSource(sourceCode));
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00018F7E File Offset: 0x0001717E
		private static CssTokenizer CreateTokenizer(Stream sourceCode)
		{
			return new CssTokenizer(new TextSource(sourceCode, null));
		}

		// Token: 0x040002ED RID: 749
		private readonly CssParserOptions _options;

		// Token: 0x040002EE RID: 750
		private readonly IConfiguration _configuration;

		// Token: 0x040002EF RID: 751
		internal static readonly CssParser Default = new CssParser();
	}
}
