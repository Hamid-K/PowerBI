using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Help;
using Microsoft.Mashup.Engine1.DataSourceReference;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Content;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Json;
using Microsoft.Mashup.Engine1.Library.Lines;
using Microsoft.Mashup.Engine1.Library.Normalization;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.OleDb;
using Microsoft.Mashup.Engine1.Library.Resources;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Extensibility;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1
{
	// Token: 0x02000221 RID: 545
	public class Engine : IEngine, ITinyEngine
	{
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x00019556 File Offset: 0x00017756
		public static Engine Instance
		{
			get
			{
				if (Engine.engine == null)
				{
					Engine.engine = new Engine();
				}
				return Engine.engine;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0001956E File Offset: 0x0001776E
		ICollection<string> IEngine.DisabledModules
		{
			get
			{
				return Modules.DisabledModules;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x00019575 File Offset: 0x00017775
		ICollection<string> IEngine.RemovedModules
		{
			get
			{
				return Modules.RemovedModules;
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000B12 RID: 2834 RVA: 0x0001957C File Offset: 0x0001777C
		public IDictionary<string, object> Features
		{
			get
			{
				return EngineFeatures.Instance;
			}
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00019583 File Offset: 0x00017783
		public ITokens Tokenize(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return this.Tokenize(SegmentedString.New(text));
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x0001959F File Offset: 0x0001779F
		public ITokens Tokenize(SegmentedString text)
		{
			return TextTokens.New(text);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000195A8 File Offset: 0x000177A8
		public IDocument Parse(ITokens tokens, IDocumentHost host, Action<IError> log)
		{
			Engine.ParseEntry parseEntry = this.Parse(tokens, host, (ITokens t, IDocumentHost h, Action<IError> l) => new IDocument[] { new DocumentReader().ReadDocument(t, h, l) });
			foreach (IError error in parseEntry.Errors)
			{
				log(error);
			}
			return parseEntry.Documents[0];
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x0001962C File Offset: 0x0001782C
		public IDocument[] ParseMany(ITokens tokens, IDocumentHost host, Action<IError> log)
		{
			Engine.ParseEntry parseEntry = this.Parse(tokens, host, new Func<ITokens, IDocumentHost, Action<IError>, IDocument[]>(new DocumentReader().ReadDocuments));
			foreach (IError error in parseEntry.Errors)
			{
				log(error);
			}
			return parseEntry.Documents;
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000196A0 File Offset: 0x000178A0
		private Engine.ParseEntry Parse(ITokens tokens, IDocumentHost host, Func<ITokens, IDocumentHost, Action<IError>, IDocument[]> read)
		{
			Engine.ParseKey parseKey = null;
			ICacheableDocumentHost cacheableDocumentHost = host as ICacheableDocumentHost;
			if (cacheableDocumentHost != null)
			{
				parseKey = new Engine.ParseKey(cacheableDocumentHost, tokens);
				LruCache<Engine.ParseKey, Engine.ParseEntry> lruCache = this.parseCache;
				lock (lruCache)
				{
					Engine.ParseEntry parseEntry;
					if (this.parseCache.TryGetValue(parseKey, out parseEntry))
					{
						return parseEntry;
					}
				}
			}
			List<IError> errors = new List<IError>();
			Engine.ParseEntry parseEntry2 = new Engine.ParseEntry(read(tokens, host, delegate(IError e)
			{
				errors.Add(e);
			}), errors);
			if (parseKey != null)
			{
				LruCache<Engine.ParseKey, Engine.ParseEntry> lruCache = this.parseCache;
				lock (lruCache)
				{
					Engine.ParseEntry parseEntry3;
					if (this.parseCache.TryGetValue(parseKey, out parseEntry3))
					{
						return parseEntry3;
					}
					this.parseCache.Add(parseKey, parseEntry2);
				}
			}
			return parseEntry2;
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x00019798 File Offset: 0x00017998
		public Module Compile(IDocument document, RecordValue environment, CompileOptions options, Action<IError> log)
		{
			document = AstCheckingRewriter.Rewrite(document, log);
			document = ReducingVisitor.Rewrite(document, log);
			document = TypeRemovalVisitor.Rewrite(document);
			return new Microsoft.Mashup.Engine1.Language.ExpressionBuilder(document, environment, options).Build();
		}

		// Token: 0x06000B19 RID: 2841 RVA: 0x000197C3 File Offset: 0x000179C3
		public Assembly Assemble(IList<Module> modules, IEngineHost hostEnvironment, Action<IError> log)
		{
			return Linker.Assemble(modules, hostEnvironment, log);
		}

		// Token: 0x06000B1A RID: 2842 RVA: 0x000197CD File Offset: 0x000179CD
		public SourceBuilder CreateSourceBuilder()
		{
			return SourceBuilder.New();
		}

		// Token: 0x06000B1B RID: 2843 RVA: 0x000197D4 File Offset: 0x000179D4
		public string EscapeIdentifier(string name, ContextualKeyword[] keywords)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LanguageServices.Identifier.Escape(name, keywords);
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x000197EB File Offset: 0x000179EB
		string ITinyEngine.EscapeFieldIdentifier(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return LanguageServices.FieldIdentifier.Escape(name);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00019801 File Offset: 0x00017A01
		ITokens IEngine.Tokenize(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return this.Tokenize(text);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00019818 File Offset: 0x00017A18
		ITokens IEngine.Tokenize(SegmentedString text)
		{
			return this.Tokenize(text);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00019821 File Offset: 0x00017A21
		IDocument IEngine.Parse(ITokens tokens, IDocumentHost host, Action<IError> log)
		{
			return this.Parse(tokens, host, log);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x0001982C File Offset: 0x00017A2C
		private IList<Module> GetModules(IList<IModule> modules, IRecordValue library)
		{
			Module[] array = new Module[modules.Count + 1];
			for (int i = 0; i < modules.Count; i++)
			{
				array[i] = (Module)modules[i];
			}
			array[array.Length - 1] = new EnvironmentModule((RecordValue)library);
			return array;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001987A File Offset: 0x00017A7A
		IModule IEngine.Link(IList<IModule> modules, Action<IError> log, LinkOptions options)
		{
			return Linker.Link(this.GetModules(modules, RecordValue.Empty), log, options);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001988F File Offset: 0x00017A8F
		IModule IEngine.Link(IList<IModule> modules, IRecordValue library, Action<IError> log, LinkOptions options)
		{
			return Linker.Link(this.GetModules(modules, library), log, options);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000198A1 File Offset: 0x00017AA1
		IAssembly IEngine.Assemble(IList<IModule> modules, IRecordValue library, IEngineHost hostEnvironment, Action<IError> log)
		{
			return this.Assemble(this.GetModules(modules, library), hostEnvironment, log);
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000198B4 File Offset: 0x00017AB4
		ISourceBuilder IEngine.CreateSourceBuilder()
		{
			return this.CreateSourceBuilder();
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000198BC File Offset: 0x00017ABC
		IDictionary<DocumentRange, IIdentifierBinding> IEngine.Bind(IList<IDocument> documents)
		{
			return BindingVisitor.Bind(documents);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000198C4 File Offset: 0x00017AC4
		IDictionary<DocumentRange, IList<DocumentRangePair>> IEngine.GetDependencies(IList<IDocument> documents, IList<DocumentRange> identifiers)
		{
			return DependencyVisitor.Visit(documents, identifiers);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000198CD File Offset: 0x00017ACD
		bool IEngine.TryParseIdentifier(string text, out string identifier)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return LanguageServices.Identifier.TryParse(text, out identifier);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000198E4 File Offset: 0x00017AE4
		bool IEngine.TryParseFieldIdentifier(string text, out string identifier)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return LanguageServices.FieldIdentifier.TryParse(text, out identifier);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000198FB File Offset: 0x00017AFB
		string ITinyEngine.EscapeIdentifier(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.EscapeIdentifier(name, null);
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00019913 File Offset: 0x00017B13
		string IEngine.EscapeIdentifier(string name, ContextualKeyword[] keywords)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return this.EscapeIdentifier(name, keywords);
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x0001992B File Offset: 0x00017B2B
		bool IEngine.TryParseString(string text, out string s)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return LiteralValue.TryParseStringLiteral(text, 0, text.Length, out s);
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x00019949 File Offset: 0x00017B49
		string ITinyEngine.EscapeString(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Escape.AsQuotedString(s);
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000B2D RID: 2861 RVA: 0x0001995F File Offset: 0x00017B5F
		string IEngine.TrueText
		{
			get
			{
				return "true";
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00019966 File Offset: 0x00017B66
		string IEngine.FalseText
		{
			get
			{
				return "false";
			}
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001996D File Offset: 0x00017B6D
		string IEngine.ContextualKeywordText(ContextualKeyword keyword)
		{
			switch (keyword)
			{
			case ContextualKeyword.Optional:
				return "optional";
			case ContextualKeyword.Nullable:
				return "nullable";
			case ContextualKeyword.Catch:
				return "catch";
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001999C File Offset: 0x00017B9C
		string IEngine.EnumText(EnumReference enumReference)
		{
			switch (enumReference)
			{
			case EnumReference.JoinKindInner:
				return "JoinKind.Inner";
			case EnumReference.JoinKindLeftOuter:
				return "JoinKind.LeftOuter";
			case EnumReference.JoinKindFullOuter:
				return "JoinKind.FullOuter";
			case EnumReference.JoinKindRightOuter:
				return "JoinKind.RightOuter";
			case EnumReference.OrderAscending:
				return "Order.Ascending";
			case EnumReference.OrderDescending:
				return "Order.Descending";
			case EnumReference.QuoteStyleCsv:
				return "QuoteStyle.Csv";
			case EnumReference.QuoteStyleNone:
				return "QuoteStyle.None";
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x00019A08 File Offset: 0x00017C08
		private IExpression NextExpression(IExpression inlinedExpression, out bool done)
		{
			done = false;
			ExpressionKind kind = inlinedExpression.Kind;
			if (kind == ExpressionKind.ElementAccess)
			{
				return ((IElementAccessExpression)inlinedExpression).Collection;
			}
			if (kind == ExpressionKind.FieldAccess)
			{
				return ((IFieldAccessExpression)inlinedExpression).Expression;
			}
			if (kind != ExpressionKind.Invocation)
			{
				done = true;
				return inlinedExpression;
			}
			IInvocationExpression invocationExpression = (IInvocationExpression)inlinedExpression;
			if (invocationExpression.Arguments.Count == 0)
			{
				done = true;
				return inlinedExpression;
			}
			return invocationExpression.Arguments[0];
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x00019A70 File Offset: 0x00017C70
		bool IEngine.TryGetLocation(IExpression expression, bool deepInspection, out IDataSourceLocation location, out IRecordValue foundOptions, out IKeys unknownOptions, out bool mayHaveMoreNativeQueries)
		{
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			FunctionValue functionValue;
			try
			{
				functionValue = FindResourceAccessFunctionVisitor.Find(expression);
			}
			catch (ValueException)
			{
				location = null;
				foundOptions = null;
				unknownOptions = null;
				mayHaveMoreNativeQueries = false;
				return false;
			}
			IExpression expression2 = FieldAccessInliner.Inline(this, expression);
			bool flag = !deepInspection;
			while (!flag)
			{
				if (Engine.TryGetLocation(functionValue, expression2, out location, out foundOptions, out unknownOptions, out mayHaveMoreNativeQueries))
				{
					return true;
				}
				expression2 = this.NextExpression(expression2, out flag);
			}
			return Engine.TryGetLocation(functionValue, expression2, out location, out foundOptions, out unknownOptions, out mayHaveMoreNativeQueries);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00019AF8 File Offset: 0x00017CF8
		private static bool TryGetLocation(FunctionValue funcValue, IExpression expression, out IDataSourceLocation location, out IRecordValue optionsRecord, out IKeys unknownKeys, out bool mayHaveMoreNativeQueries)
		{
			try
			{
				RecordValue recordValue;
				Keys keys;
				if (funcValue.TryGetLocation(expression, out location, out recordValue, out keys))
				{
					optionsRecord = recordValue;
					unknownKeys = keys;
					ResourceKindInfo resourceKindInfo;
					mayHaveMoreNativeQueries = (keys == null || keys.Contains("Query")) && ResourceKinds.Lookup(location.ResourceKind, out resourceKindInfo) && resourceKindInfo.SupportsNativeQuery;
					return true;
				}
			}
			catch (ValueException)
			{
			}
			location = null;
			optionsRecord = null;
			unknownKeys = null;
			mayHaveMoreNativeQueries = false;
			return false;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00019B70 File Offset: 0x00017D70
		bool IEngine.TryExtractPattern(IFunctionValue function, string[] patterns, out Dictionary<string, object> patternResult)
		{
			patternResult = null;
			if (function == null)
			{
				throw new ArgumentNullException("expression");
			}
			if (patterns == null)
			{
				throw new ArgumentNullException("patterns");
			}
			ExpressionPattern expressionPattern = new ExpressionPattern(patterns);
			IFunctionExpression functionExpression = ((Value)function).Expression as IFunctionExpression;
			if (functionExpression == null)
			{
				throw new ArgumentException("expression");
			}
			IExpression expression = functionExpression.Expression;
			FindResourceAccessFunctionVisitor.Find(expression);
			IExpression expression2 = FieldAccessInliner.Inline(this, expression);
			bool flag = false;
			Dictionary<string, IExpression> dictionary = null;
			while (!flag && !expressionPattern.TryMatch(expression2, out dictionary))
			{
				dictionary = null;
				expression2 = this.NextExpression(expression2, out flag);
			}
			if (dictionary == null || !expressionPattern.TryMatch(expression2, out dictionary))
			{
				patternResult = null;
				return false;
			}
			patternResult = new Dictionary<string, object>(dictionary.Count);
			foreach (KeyValuePair<string, IExpression> keyValuePair in dictionary)
			{
				Value value;
				if (keyValuePair.Value.TryGetConstant(out value))
				{
					try
					{
						patternResult[keyValuePair.Key] = ValueMarshaller.MarshalToClr(value, value.Type);
					}
					catch (ValueException)
					{
					}
				}
			}
			return true;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00019C98 File Offset: 0x00017E98
		bool IEngine.TryParseSourceValue(string text, out IValue value)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			Value value2;
			bool flag = LiteralValue.TryParseSourceValue(text, out value2);
			value = value2;
			return flag;
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00019CC0 File Offset: 0x00017EC0
		bool IEngine.TryParseNumberValue(string text, CultureInfo cultureInfo, out INumberValue number)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			NumberValue numberValue;
			if (NumberValue.TryParse(text, NumberStyles.Any, cultureInfo, out numberValue))
			{
				number = numberValue;
				return true;
			}
			number = null;
			return false;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x00019CF4 File Offset: 0x00017EF4
		bool IEngine.TryParseLogicalValue(string text, out bool value)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			LogicalValue logicalValue;
			if (LogicalValue.TryParseFromText(text, out logicalValue))
			{
				value = logicalValue.AsBoolean;
				return true;
			}
			value = false;
			return false;
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x00019D27 File Offset: 0x00017F27
		bool IEngine.TryParseNullValue(string text)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			return string.CompareOrdinal(text, "null") == 0;
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x00019D48 File Offset: 0x00017F48
		bool IEngine.TryParseDate(string text, CultureInfo cultureInfo, out IDateValue dateTime)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			DateValue dateValue;
			if (DateValue.TryParseFromText(text, cultureInfo, out dateValue))
			{
				dateTime = dateValue;
				return true;
			}
			dateTime = null;
			return false;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00019D78 File Offset: 0x00017F78
		bool IEngine.TryParseDateTimeWithoutTimezone(string text, CultureInfo cultureInfo, out IDateTime dateTime)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			DateTimeValue dateTimeValue;
			if (DateTimeValue.TryParseFromText(text, cultureInfo, out dateTimeValue))
			{
				dateTime = dateTimeValue;
				return true;
			}
			dateTime = null;
			return false;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00019DA8 File Offset: 0x00017FA8
		bool IEngine.TryParseDateTimeWithTimezone(string text, CultureInfo cultureInfo, out IDateTimeZone dateTime)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			DateTimeZoneValue dateTimeZoneValue;
			if (DateTimeZoneValue.TryParseFromText(text, cultureInfo, out dateTimeZoneValue))
			{
				dateTime = dateTimeZoneValue;
				return true;
			}
			dateTime = null;
			return false;
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00019DD8 File Offset: 0x00017FD8
		bool IEngine.TryParseTime(string text, CultureInfo cultureInfo, out ITimeValue dateTime)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			TimeValue timeValue;
			if (TimeValue.TryParseFromText(text, cultureInfo, out timeValue))
			{
				dateTime = timeValue;
				return true;
			}
			dateTime = null;
			return false;
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00019E08 File Offset: 0x00018008
		bool IEngine.TryParseDuration(string text, out IDurationValue duration)
		{
			if (text == null)
			{
				throw new ArgumentNullException("text");
			}
			TimeSpan timeSpan;
			if (DurationValue.TryParse(text, out timeSpan))
			{
				duration = new DurationValue(timeSpan);
				return true;
			}
			duration = null;
			return false;
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00019E3B File Offset: 0x0001803B
		INumberValue IEngine.NaN
		{
			get
			{
				return NumberValue.NaN;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00019E42 File Offset: 0x00018042
		IValue IEngine.Null
		{
			get
			{
				return Value.Null;
			}
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x00019E49 File Offset: 0x00018049
		INumberValue IEngine.Number(double value)
		{
			return NumberValue.New(value);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x00019E51 File Offset: 0x00018051
		INumberValue IEngine.Decimal(decimal value)
		{
			return NumberValue.New(value);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00019E59 File Offset: 0x00018059
		IDurationValue IEngine.Duration(TimeSpan value)
		{
			return DurationValue.New(value);
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00019E61 File Offset: 0x00018061
		IRecordValue IEngine.EmptyRecord
		{
			get
			{
				return RecordValue.Empty;
			}
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x00019E68 File Offset: 0x00018068
		IBinaryValue IEngine.Binary(byte[] value, string contentType)
		{
			return BinaryValue.New(value).NewMeta(ContentHelper.CreateContentTypeMetadata(contentType)).AsBinary;
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x00019E80 File Offset: 0x00018080
		byte[] IEngine.BinaryFromFile(string fileName, out string contentType)
		{
			if (fileName == null)
			{
				throw new ArgumentNullException("fileName");
			}
			byte[] asBytes = new FileStreamBinaryValue(fileName, ProgressService.GetNullHostProgress(), () => null, false).AsBytes;
			contentType = ContentHelper.GetContentType(fileName);
			return asBytes;
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x00019ED3 File Offset: 0x000180D3
		IValue IEngine.Logical(bool value)
		{
			return LogicalValue.New(value);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x00019EDB File Offset: 0x000180DB
		ITextValue IEngine.Text(string value)
		{
			return TextValue.New(value);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00019EE3 File Offset: 0x000180E3
		IKeys IEngine.Keys(params string[] keys)
		{
			return Keys.New(keys);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00019EEC File Offset: 0x000180EC
		private static Value[] GetValues(IValue[] values)
		{
			Value[] array = new Value[values.Length];
			for (int i = 0; i < values.Length; i++)
			{
				array[i] = (Value)values[i];
			}
			return array;
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00019F1C File Offset: 0x0001811C
		IRecordValue IEngine.Record(IKeys keys, params IValue[] values)
		{
			return RecordValue.New((Keys)keys, Engine.GetValues(values));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00019F2F File Offset: 0x0001812F
		IListValue IEngine.List(params IValue[] values)
		{
			return ListValue.New(Engine.GetValues(values));
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00019F3C File Offset: 0x0001813C
		ITableValue IEngine.Table(IEngineHost engineHost, Func<IDataReaderSource> getSource)
		{
			return DataReaderTableValue.New(engineHost.QueryService<ILifetimeService>(), getSource);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x00019F4A File Offset: 0x0001814A
		ITableValue IEngine.Table(IKeys keys, IValue[] values)
		{
			return ListValue.New(Engine.GetValues(values)).ToTable((Keys)keys);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x00019F64 File Offset: 0x00018164
		bool IEngine.TryGetExceptionDetails(Exception e, out string detail)
		{
			detail = null;
			ValueException ex = e as ValueException;
			if (ex == null)
			{
				return false;
			}
			Value value = ex.Detail as Value;
			if (value == null)
			{
				return false;
			}
			detail = value.PrimitiveAndRecordToString(2);
			return true;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x00019F9B File Offset: 0x0001819B
		ValueException2 IEngine.Exception(IRecordValue value)
		{
			return ValueException.New((RecordValue)value, null);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00019FA9 File Offset: 0x000181A9
		IRecordValue IEngine.ExceptionRecord(ITextValue reason, IValue message, IValue detail)
		{
			return ErrorRecord.New((TextValue)reason, (Value)message, (Value)detail);
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00019FC2 File Offset: 0x000181C2
		IRecordValue IEngine.ExceptionRecord(ITextValue reason, IValue message, IValue detail, IValue messageFormat, IValue messageParameters)
		{
			return ErrorRecord.New((TextValue)reason, (Value)message, (Value)detail, (Value)messageFormat, (Value)messageParameters);
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00019FE9 File Offset: 0x000181E9
		IValue IEngine.Invoke(IValue function, params IValue[] arguments)
		{
			return ((Value)function).AsFunction.Invoke(Engine.GetValues(arguments));
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001A004 File Offset: 0x00018204
		IValue IEngine.Function(IEngineHost engineHost, FunctionHandle value)
		{
			switch (value)
			{
			case FunctionHandle.CubeAttributeMemberId:
				return CubeModule.Cube.AttributeMemberId;
			case FunctionHandle.CubeAttributeMemberProperty:
				return CubeModule.Cube.AttributeMemberProperty;
			case FunctionHandle.CubePropertyKey:
				return CubeModule.Cube.PropertyKey;
			case FunctionHandle.CubeMeasureProperty:
				return CubeModule.Cube.MeasureProperty;
			case FunctionHandle.ListDistinct:
				return LanguageLibrary.List.Distinct;
			case FunctionHandle.ListFirstN:
				return LanguageLibrary.List.FirstN;
			case FunctionHandle.ListSort:
				return LanguageLibrary.List.Sort;
			case FunctionHandle.RecordRemoveFields:
				return Library.Record.RemoveFields;
			case FunctionHandle.TableSelectRows:
				return TableModule.Table.SelectRows;
			case FunctionHandle.ValueFromText:
				return new Library._Value.FromTextFunctionValue(engineHost);
			case FunctionHandle.DateTimeFrom:
				return new Library.DateTime.FromFunctionValue(engineHost, null);
			case FunctionHandle.DateTimeToText:
				return new Library.DateTime.ToTextFunctionValue(engineHost);
			case FunctionHandle.TablePartitionValues:
				return TableModule.Table.PartitionValues;
			case FunctionHandle.LaxOrdinalIgnoreCaseComparer:
				return Library.Comparer.LaxOrdinalIgnoreCase;
			case FunctionHandle.TableRemoveColumns:
				return TableModule.Table.RemoveColumns;
			case FunctionHandle.TableRenameColumns:
				return TableModule.Table.RenameColumns;
			case FunctionHandle.TableReorderColumns:
				return TableModule.Table.ReorderColumns;
			case FunctionHandle.TypeIs:
				return Library.Type.Is;
			case FunctionHandle.ValueAndTypeFromText:
				return new Library.ValueAndType.FromTextFunctionValue(engineHost);
			case FunctionHandle.AddBinaryOperator:
				return BinaryOperator.Add;
			case FunctionHandle.SubtractBinaryOperator:
				return BinaryOperator.Subtract;
			case FunctionHandle.MultiplyBinaryOperator:
				return BinaryOperator.Multiply;
			case FunctionHandle.DivideBinaryOperator:
				return BinaryOperator.Divide;
			case FunctionHandle.ModBinaryOperator:
				return BinaryOperator.Mod;
			case FunctionHandle.NumberIntegerDivide:
				return Library.Number.IntegerDivide;
			case FunctionHandle.DateTimeZoneFrom:
				return new Library.DateTimeZone.FromFunctionValue(engineHost, null);
			case FunctionHandle.DateTimeZoneToText:
				return new Library.DateTimeZone.ToTextFunctionValue(engineHost);
			case FunctionHandle.TextFrom:
				return new Library.Text.FromFunctionValue(engineHost, null);
			case FunctionHandle.TextTrim:
				return Library.Text.Trim;
			case FunctionHandle.TextUpper:
				return new Library.Text.UpperFunctionValue(engineHost);
			case FunctionHandle.TextLower:
				return new Library.Text.LowerFunctionValue(engineHost);
			case FunctionHandle.TextMiddle:
				return Library.Text.Middle;
			case FunctionHandle.TextSplit:
				return Library.Text.Split;
			case FunctionHandle.TextPositionOf:
				return Library.Text.PositionOf;
			case FunctionHandle.TextLength:
				return Library.Text.Length;
			case FunctionHandle.ConcatenateBinaryOperator:
				return BinaryOperator.Concatenate;
			case FunctionHandle.TextReplace:
				return Library.Text.Replace;
			case FunctionHandle.TextProper:
				return new Library.Text.ProperFunctionValue(engineHost);
			case FunctionHandle.DateDay:
				return Library.Date.Day;
			case FunctionHandle.DateDayOfWeek:
				return new Library.Date.DayOfWeekFunctionValue(engineHost);
			case FunctionHandle.DateDayOfYear:
				return Library.Date.DayOfYear;
			case FunctionHandle.DateMonth:
				return Library.Date.Month;
			case FunctionHandle.DateQuarterOfYear:
				return Library.Date.QuarterOfYear;
			case FunctionHandle.DateWeekOfMonth:
				return new Library.Date.WeekOfMonthFunctionValue(engineHost);
			case FunctionHandle.DateWeekOfYear:
				return new Library.Date.WeekOfYearFunctionValue(engineHost);
			case FunctionHandle.DateYear:
				return Library.Date.Year;
			case FunctionHandle.TimeHour:
				return Library.Time.Hour;
			case FunctionHandle.TimeMinute:
				return Library.Time.Minute;
			case FunctionHandle.TimeSecond:
				return Library.Time.Second;
			case FunctionHandle.TimeFrom:
				return new Library.Time.FromFunctionValue(engineHost, null);
			case FunctionHandle.DateFrom:
				return new Library.Date.FromFunctionValue(engineHost, null);
			case FunctionHandle.DateToText:
				return new Library.Date.ToTextFunctionValue(engineHost);
			case FunctionHandle.TextCombine:
				return Library.Text.Combine;
			case FunctionHandle.JsonFromValue:
				return JsonModule.Json.FromValue;
			case FunctionHandle.TextFromBinary:
				return Library.Text.FromBinary;
			case FunctionHandle.TextEnd:
				return Library.Text.End;
			case FunctionHandle.TextStart:
				return Library.Text.Start;
			case FunctionHandle.TextAfterDelimiter:
				return Modules.GetLibrary(engineHost, null)["Text.AfterDelimiter"];
			case FunctionHandle.TextBeforeDelimiter:
				return Modules.GetLibrary(engineHost, null)["Text.BeforeDelimiter"];
			case FunctionHandle.TextBetweenDelimiters:
				return Modules.GetLibrary(engineHost, null)["Text.BetweenDelimiters"];
			case FunctionHandle.NumberFrom:
				return new Library.Number.FromFunctionValue(engineHost, null);
			case FunctionHandle.TextClean:
				return Library.Text.Clean;
			case FunctionHandle.DateDaysInMonth:
				return Library.Date.DaysInMonth;
			case FunctionHandle.DateStartOfDay:
				return Library.Date.StartOfDay;
			case FunctionHandle.DateEndOfDay:
				return Library.Date.EndOfDay;
			case FunctionHandle.DateStartOfMonth:
				return Library.Date.StartOfMonth;
			case FunctionHandle.DateEndOfMonth:
				return Library.Date.EndOfMonth;
			case FunctionHandle.DateStartOfQuarter:
				return Library.Date.StartOfQuarter;
			case FunctionHandle.DateEndOfQuarter:
				return Library.Date.EndOfQuarter;
			case FunctionHandle.DateStartOfWeek:
				return new Library.Date.StartOfWeekFunctionValue(engineHost);
			case FunctionHandle.DateEndOfWeek:
				return new Library.Date.EndOfWeekFunctionValue(engineHost);
			case FunctionHandle.DateStartOfYear:
				return Library.Date.StartOfYear;
			case FunctionHandle.DateEndOfYear:
				return Library.Date.EndOfYear;
			case FunctionHandle.TimeStartOfHour:
				return Library.Time.StartOfHour;
			case FunctionHandle.TimeEndOfHour:
				return Library.Time.EndOfHour;
			case FunctionHandle.DateTimeLocalNow:
				return new Library.DateTime.LocalNowFunctionValue(engineHost);
			case FunctionHandle.DateDayOfWeekName:
				return Modules.GetLibrary(engineHost, null)["Date.DayOfWeekName"];
			case FunctionHandle.DateMonthName:
				return Modules.GetLibrary(engineHost, null)["Date.MonthName"];
			case FunctionHandle.DateTimeZoneToLocal:
				return new Library.DateTimeZone.ToLocalFunctionValue(engineHost);
			case FunctionHandle.NumberAbs:
				return Library.Number.Abs;
			case FunctionHandle.NumberAcos:
				return Library.Number.Acos;
			case FunctionHandle.NumberAsin:
				return Library.Number.Asin;
			case FunctionHandle.NumberAtan:
				return Library.Number.Atan;
			case FunctionHandle.NumberCos:
				return Library.Number.Cos;
			case FunctionHandle.NumberExp:
				return Library.Number.Exp;
			case FunctionHandle.NumberFactorial:
				return Library.Number.Factorial;
			case FunctionHandle.NumberIsEven:
				return Library.Number.IsEven;
			case FunctionHandle.NumberIsOdd:
				return Library.Number.IsOdd;
			case FunctionHandle.NumberLn:
				return Library.Number.Ln;
			case FunctionHandle.NumberLog10:
				return Library.Number.Log10;
			case FunctionHandle.NumberPower:
				return Library.Number.Power;
			case FunctionHandle.NumberRoundDown:
				return Library.Number.RoundDown;
			case FunctionHandle.NumberRoundUp:
				return Library.Number.RoundUp;
			case FunctionHandle.NumberSign:
				return Library.Number.Sign;
			case FunctionHandle.NumberSin:
				return Library.Number.Sin;
			case FunctionHandle.NumberSqrt:
				return Library.Number.Sqrt;
			case FunctionHandle.NumberTan:
				return Library.Number.Tan;
			case FunctionHandle.ListSum:
				return Library.List.Sum;
			case FunctionHandle.ListProduct:
				return Library.List.Product;
			case FunctionHandle.NumberMod:
				return Library.Number.Mod;
			case FunctionHandle.ListAverage:
				return Library.List.Average;
			case FunctionHandle.ListMax:
				return Library.List.Max;
			case FunctionHandle.ListMedian:
				return Library.List.Median;
			case FunctionHandle.ListMin:
				return Library.List.Min;
			case FunctionHandle.ListStandardDeviation:
				return Library.List.StandardDeviation;
			case FunctionHandle.NumberRound:
				return Library.Number.Round;
			case FunctionHandle.ListNonNullCount:
				return Library.List.CountOfNotNull;
			case FunctionHandle.DurationDays:
				return Library.Duration.Days;
			case FunctionHandle.ValueCompare:
				return Library._Value.Compare;
			case FunctionHandle.TextRemove:
				return Library.Text.Remove;
			case FunctionHandle.TextSelect:
				return Library.Text.Select;
			case FunctionHandle.ResourceAccess:
				return ResourceModule.Resource.Access;
			case FunctionHandle.Date:
				return Library.Date.date;
			case FunctionHandle.TextFormat:
				return Modules.GetLibrary(engineHost, null)["Text.Format"];
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001A4F8 File Offset: 0x000186F8
		ITypeValue ITinyEngine.Type(TypeHandle kind)
		{
			switch (kind)
			{
			case TypeHandle.Text:
				return TypeValue.Text;
			case TypeHandle.Binary:
				return TypeValue.Binary;
			case TypeHandle.Character:
				return TypeValue.Character;
			case TypeHandle.List:
				return TypeValue.List;
			case TypeHandle.Number:
				return TypeValue.Number;
			case TypeHandle.Int8:
				return TypeValue.Int8;
			case TypeHandle.Int16:
				return TypeValue.Int16;
			case TypeHandle.Int32:
				return TypeValue.Int32;
			case TypeHandle.Int64:
				return TypeValue.Int64;
			case TypeHandle.Double:
				return TypeValue.Double;
			case TypeHandle.Decimal:
				return TypeValue.Decimal;
			case TypeHandle.Currency:
				return TypeValue.Currency;
			case TypeHandle.Percentage:
				return TypeValue.Percentage;
			case TypeHandle.Date:
				return TypeValue.Date;
			case TypeHandle.DateTimeZone:
				return TypeValue.DateTimeZone;
			case TypeHandle.DateTime:
				return TypeValue.DateTime;
			case TypeHandle.Time:
				return TypeValue.Time;
			case TypeHandle.Logical:
				return TypeValue.Logical;
			case TypeHandle.Record:
				return TypeValue.Record;
			case TypeHandle.Function:
				return TypeValue.Function;
			case TypeHandle.Duration:
				return TypeValue.Duration;
			case TypeHandle.Single:
				return TypeValue.Single;
			case TypeHandle.Table:
				return TypeValue.Table;
			case TypeHandle.Type:
				return TypeValue._Type;
			case TypeHandle.Null:
				return TypeValue.Null;
			case TypeHandle.Action:
				return TypeValue.Action;
			case TypeHandle.Any:
				return TypeValue.Any;
			case TypeHandle.Byte:
				return TypeValue.Byte;
			case TypeHandle.Guid:
				return TypeValue.Guid;
			case TypeHandle.Uri:
				return TypeValue.Uri;
			case TypeHandle.Password:
				return TypeValue.Password;
			default:
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x0001A64B File Offset: 0x0001884B
		ITypeValue IEngine.UnionTypes(ITypeValue type1, ITypeValue type2)
		{
			return TypeAlgebra.Union((TypeValue)type1, (TypeValue)type2);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x0001A65E File Offset: 0x0001885E
		Type IEngine.ClrType(ITypeValue type)
		{
			return ((TypeValue)type).ToClrType();
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001A66B File Offset: 0x0001886B
		ITableValue IEngine.NormalizeToTable(IValue value)
		{
			return NormalizationModule.TableFromValue.Invoke((Value)value).AsTable;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001A682 File Offset: 0x00018882
		bool IEngine.TryGetPropertiesFromException(Exception exception, out ISerializedException properties)
		{
			return PageExceptionSerializer.TryGetPropertiesFromException(exception, out properties);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001A68B File Offset: 0x0001888B
		Exception IEngine.CreateExceptionFromProperties(ISerializedException properties)
		{
			return PageExceptionSerializer.GetExceptionFromProperties(properties);
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001A693 File Offset: 0x00018893
		IActionValue IEngine.Action(Func<IValue> action)
		{
			return ActionValue.New(() => (Value)action());
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001A6B1 File Offset: 0x000188B1
		IPageReader IEngine.CreatePageReader(ITableValue value)
		{
			return ((TableValue)value).GetReader();
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001A6BE File Offset: 0x000188BE
		IValue IEngine.ApplyPreviewInference(IValue value)
		{
			return ((IEngine)this).ApplyPreviewInference(value, null, null);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001A6CC File Offset: 0x000188CC
		IValue IEngine.ApplyPreviewInference(IValue value, char[] candidateDelimiters, EnumReference[] candidateQuoteStyles)
		{
			LinesModule.QuoteStyle[] array;
			if (candidateQuoteStyles != null)
			{
				array = candidateQuoteStyles.Select(delegate(EnumReference quoteStyle)
				{
					if (quoteStyle == EnumReference.QuoteStyleCsv)
					{
						return LinesModule.QuoteStyle.Csv;
					}
					if (quoteStyle != EnumReference.QuoteStyleNone)
					{
						throw new ArgumentException("Unexpected quote style value: " + quoteStyle.ToString());
					}
					return LinesModule.QuoteStyle.None;
				}).ToArray<LinesModule.QuoteStyle>();
			}
			else
			{
				array = null;
			}
			LinesModule.QuoteStyle[] array2 = array;
			return PreviewInference.Apply((Value)value, candidateDelimiters, array2);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001A717 File Offset: 0x00018917
		[Obsolete]
		IValue IEngine.ApplyPreviewInference(IValue value, char[] candidateDelimiters, EnumReference[] candidateQuoteStyles, bool inferUtf8)
		{
			return ((IEngine)this).ApplyPreviewInference(value, candidateDelimiters, candidateQuoteStyles);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001A722 File Offset: 0x00018922
		bool IEngine.TryGetDocumentation(IValue value, out IDocumentation documentation)
		{
			return LibraryDescriptions.TryGetDocumentation((Value)value, out documentation);
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0001A730 File Offset: 0x00018930
		public IListValue List(ITypeValue type, IRecordValue metaValue, params IValueReference2[] references)
		{
			IValueReference[] array = new IValueReference[references.Length];
			for (int i = 0; i < references.Length; i++)
			{
				array[i] = new Engine.ValueReference(references[i]);
			}
			return ListValue.New(array).NewType((ListTypeValue)type).NewMeta((RecordValue)metaValue)
				.AsList;
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001A780 File Offset: 0x00018980
		public ITableValue Table(ITypeValue type, IRecordValue metaValue, params IValueReference2[] references)
		{
			return this.Table(type, metaValue, new IRelationship[0], null, references);
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001A794 File Offset: 0x00018994
		public ITableValue Table(ITypeValue type, IRecordValue metaValue, IEnumerable<IRelationship> relationships, IColumnIdentity[] columnIdentities, params IValueReference2[] references)
		{
			IValueReference[] array = new IValueReference[references.Length];
			for (int i = 0; i < references.Length; i++)
			{
				array[i] = new Engine.ValueReference(references[i]);
			}
			return ListValue.New(array).ToTable((TableTypeValue)type).NewMeta((RecordValue)metaValue)
				.AsTable.ReplaceRelationships(Engine.CreateRelationships(relationships)).ReplaceColumnIdentities(Engine.CreateColumnIdentities(columnIdentities));
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001A800 File Offset: 0x00018A00
		IValue IEngine.SkipAndTake(IValue _value, long skip, long? take)
		{
			Value value = (Value)_value;
			Value value2;
			if (value.IsTable)
			{
				TableValue tableValue = value.AsTable;
				if (skip > 0L)
				{
					tableValue = tableValue.Skip(NumberValue.New(skip));
				}
				if (take != null)
				{
					tableValue = tableValue.Take(NumberValue.New(take.Value));
				}
				value2 = tableValue;
			}
			else
			{
				ListValue listValue = value.AsList;
				if (skip > 0L)
				{
					listValue = LanguageLibrary.List.Skip.Invoke(listValue, NumberValue.New(skip)).AsList;
				}
				if (take != null)
				{
					listValue = LanguageLibrary.List.Take.Invoke(listValue, NumberValue.New(take.Value)).AsList;
				}
				value2 = listValue;
			}
			return value2.NewMeta(value.MetaValue);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001A8AC File Offset: 0x00018AAC
		ITableValue IEngine.FilterTable(ITableValue tableValue, DataTable dataTable)
		{
			TableValue tableValue2 = (TableValue)tableValue;
			KeysBuilder keysBuilder = default(KeysBuilder);
			foreach (object obj in dataTable.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				keysBuilder.Add(dataColumn.ColumnName);
			}
			Keys keys = keysBuilder.ToKeys();
			Value[] array = new Value[dataTable.Rows.Count];
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				Value[] array2 = new Value[keys.Length];
				for (int j = 0; j < keys.Length; j++)
				{
					object obj2 = dataRow[j];
					array2[j] = ValueMarshaller.MarshalFromClr(dataRow[j]);
				}
				RecordValue recordValue = RecordValue.New(keys, array2);
				array[i] = tableValue2.SelectRows(recordValue);
			}
			return TableModule.Table.Combine.Invoke(ListValue.New(array)).AsTable;
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		IValue IEngine.Deserialize(IRecordValue library, IExpression expression)
		{
			return ValueCreator.CreateValue((RecordValue)library, expression);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001A9D6 File Offset: 0x00018BD6
		byte[] IEngine.SerializeValue(IEngineHost engineHost, IValue value, IEnumerable<string> additionalModules)
		{
			return ValueTreeSerializer.SerializeValue(value);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x0001A9DE File Offset: 0x00018BDE
		IValue IEngine.DeserializeValue(IEngineHost engineHost, byte[] bytes, IEnumerable<string> additionalModules)
		{
			return ValueTreeDeserializer.DeserializeValue(bytes);
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001A9E6 File Offset: 0x00018BE6
		IExpression IEngine.GetExpression(IValue value)
		{
			return ((Value)value).Expression;
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001A9F3 File Offset: 0x00018BF3
		IConstantExpression2 IEngine.ConstantExpression(IValue value)
		{
			return ConstantExpressionSyntaxNode.New((Value)value);
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x0001AA00 File Offset: 0x00018C00
		IValue IEngine.FromObject(object obj)
		{
			return ValueMarshaller.MarshalFromClr(obj);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001AA08 File Offset: 0x00018C08
		IConnectionStringService IEngine.GetConnectionStringService(string providerName, bool validateProvider)
		{
			List<IConnectionStringServiceHandler> list = this.connectionStringHandlers;
			IConnectionStringService connectionStringService2;
			lock (list)
			{
				using (List<IConnectionStringServiceHandler>.Enumerator enumerator = this.connectionStringHandlers.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IConnectionStringService connectionStringService;
						if (enumerator.Current.TryGetConnectionStringService(providerName, validateProvider, out connectionStringService))
						{
							return connectionStringService;
						}
					}
				}
				connectionStringService2 = null;
			}
			return connectionStringService2;
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001AA94 File Offset: 0x00018C94
		void IEngine.AddConnectionStringService(IConnectionStringServiceHandler handler)
		{
			List<IConnectionStringServiceHandler> list = this.connectionStringHandlers;
			lock (list)
			{
				this.connectionStringHandlers.Add(handler);
			}
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x0001AADC File Offset: 0x00018CDC
		void IEngine.RemoveConnectionStringService(IConnectionStringServiceHandler handler)
		{
			List<IConnectionStringServiceHandler> list = this.connectionStringHandlers;
			lock (list)
			{
				this.connectionStringHandlers.Remove(handler);
			}
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x0001AB24 File Offset: 0x00018D24
		IRecordValue IEngine.LinkLibrary(IEngineHost host, IList<IModule> modules)
		{
			return LanguageLibrary.LinkLibrary(host, modules);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001AB2D File Offset: 0x00018D2D
		IModule IEngine.LibraryCachingModule(IModule module)
		{
			return LanguageLibrary.LibraryCachingModule(module);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001AB35 File Offset: 0x00018D35
		IModule IEngine.DelayLoadingModule(IModule definitions, Func<IEngineHost, IModule> moduleLoader)
		{
			return Modules.DelayLoadingModule(definitions, moduleLoader);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001AB3E File Offset: 0x00018D3E
		IModule IEngine.OverrideEngineHostModule(IModule module, Func<IEngineHost, IEngineHost> binder)
		{
			return Modules.OverrideEngineHostModule((Module)module, binder);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001AB4C File Offset: 0x00018D4C
		IModule IEngine.InternalizeModule(IModule module, string newName)
		{
			return Modules.InternalizeModule((Module)module, newName);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001AB5A File Offset: 0x00018D5A
		IRecordValue IEngine.GetLibrary(IEngineHost host, IEnumerable<string> additionalModules)
		{
			return Modules.GetLibrary(host, additionalModules);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0001AB63 File Offset: 0x00018D63
		IModule IEngine.Compile(IDocument document, IRecordValue library, CompileOptions options, Action<IError> log)
		{
			return this.Compile(document, (RecordValue)library, options, log);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0001AB75 File Offset: 0x00018D75
		IList<EmbeddingReference> IEngine.DiscoverEmbeddingReferences(IDocument document)
		{
			return new EmbeddingReferenceDiscoveryVisitor().DiscoverEmbeddingReferences(document);
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0001AB84 File Offset: 0x00018D84
		void IEngine.AddStackTrace(ValueException2 exception, IList<SourceLocation> stack)
		{
			ValueException ex = (ValueException)exception;
			foreach (SourceLocation sourceLocation in stack)
			{
				ex.AddFrame(sourceLocation);
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		public static List<Relationship> CreateRelationships(IEnumerable<IRelationship> relationships)
		{
			List<Relationship> list = new List<Relationship>();
			foreach (IRelationship relationship in relationships)
			{
				int[] array = new int[relationship.KeyColumnCount];
				ColumnIdentity[] array2 = new ColumnIdentity[relationship.KeyColumnCount];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = relationship.KeyColumn(i);
					array2[i] = Engine.CreateColumnIdentity(relationship.OtherKeyColumn(i));
				}
				list.Add(new Relationship(array, array2));
			}
			return list;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001AC74 File Offset: 0x00018E74
		public static ColumnIdentity[] CreateColumnIdentities(IColumnIdentity[] columnIdentities)
		{
			if (columnIdentities != null)
			{
				ColumnIdentity[] array = new ColumnIdentity[columnIdentities.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Engine.CreateColumnIdentity(columnIdentities[i]);
				}
				return array;
			}
			return null;
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001ACA9 File Offset: 0x00018EA9
		private static ColumnIdentity CreateColumnIdentity(IColumnIdentity columnIdentity)
		{
			if (columnIdentity != null)
			{
				return new ColumnIdentity(columnIdentity.Identity);
			}
			return null;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001ACBB File Offset: 0x00018EBB
		bool IEngine.TryLookupResourceKind(string resourceKind, out ResourceKindInfo resourceKindInfo)
		{
			if (resourceKind == null)
			{
				throw new ArgumentNullException("resourceKind");
			}
			return ResourceKinds.Lookup(resourceKind, out resourceKindInfo);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001ACD2 File Offset: 0x00018ED2
		bool IEngine.TryLookupResourceKind(string resourceKind, out ResourceKindInfo resourceKindInfo, out string moduleName)
		{
			if (resourceKind == null)
			{
				throw new ArgumentNullException("resourceKind");
			}
			return ResourceKinds.Lookup(resourceKind, out resourceKindInfo, out moduleName);
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x0001ACEA File Offset: 0x00018EEA
		bool IEngine.TryRegisterResourceKind(ResourceKindInfo resourceKindInfo, out Exception error)
		{
			return ResourceKinds.TryAddResourceKind(resourceKindInfo, null, out error);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001ACF4 File Offset: 0x00018EF4
		bool IEngine.TryRegisterResourceKind(ResourceKindInfo resourceKindInfo, string moduleName, out Exception error)
		{
			return ResourceKinds.TryAddResourceKind(resourceKindInfo, moduleName, out error);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001ACFE File Offset: 0x00018EFE
		bool IEngine.TryDelayedRegisterResourceKind(ResourceKindInfo resourceKindInfo, string moduleName, out Exception error)
		{
			return ResourceKinds.AddDelayedResourceKind(moduleName, resourceKindInfo, out error);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x0001AD08 File Offset: 0x00018F08
		bool IEngine.UnregisterResourceKind(string resourceKind)
		{
			return ResourceKinds.RemoveResourceKind(resourceKind);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x0001AD10 File Offset: 0x00018F10
		bool IEngine.IsExtensionResourceKind(string resourceKind)
		{
			if (resourceKind == null)
			{
				throw new ArgumentNullException("resourceKind");
			}
			ResourceKindInfo resourceKindInfo;
			return ResourceKinds.Lookup(resourceKind, out resourceKindInfo) && resourceKindInfo is IExtensionResourceKind;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x0001AD40 File Offset: 0x00018F40
		bool IEngine.TryGetModule(string moduleName, out IModule module)
		{
			Module module2;
			bool flag = Modules.TryGetModule(moduleName, out module2);
			module = module2;
			return flag;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0001AD58 File Offset: 0x00018F58
		bool IEngine.TryLoadDllExtension(string path, out string moduleName, out Exception error)
		{
			return Modules.TryLoadDllExtension(this, path, out moduleName, out error);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0001AD63 File Offset: 0x00018F63
		bool IEngine.TryLoadExtension(string moduleSource, ILibraryService libraryService, out string moduleName, out Exception error)
		{
			moduleName = null;
			return Modules.TryLoadExtension(this, moduleSource, libraryService, out moduleName, out error);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0001AD73 File Offset: 0x00018F73
		bool IEngine.TryDelayedLoadExtension(IModule moduleInfo, ILibraryService libraryService, out Exception error)
		{
			return Modules.TryDelayedLoadExtension(this, moduleInfo, libraryService, out error);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0001AD7E File Offset: 0x00018F7E
		bool IEngine.TryReplaceExtension(IModule moduleInfo, ILibraryService libraryService, bool delayLoad, out Exception error)
		{
			return Modules.TryReplaceExtension(this, moduleInfo, libraryService, delayLoad, out error);
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x0001AD8B File Offset: 0x00018F8B
		bool IEngine.UnloadExtension(string moduleName)
		{
			return Modules.UnloadExtension(this, moduleName);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x0001AD94 File Offset: 0x00018F94
		bool IEngine.TryWrapExpressionDataSource(IExpressionDocument extension, ILibraryService libraryService, out ISectionDocument document, out Exception error)
		{
			document = ExtensionModule.WrapExpressionExtension(this, extension, libraryService, out error);
			return document != null;
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x0001ADA7 File Offset: 0x00018FA7
		bool IEngine.TryCompileDataSource(ISectionDocument document, IModule library, ILibraryService libraryService, CompileOptions compileOptions, Action<IError> log, out IModule module)
		{
			return Modules.TryCompileDataSource(this, document, library, libraryService, compileOptions, log, out module);
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x0001ADB8 File Offset: 0x00018FB8
		IEnumerable<KeyValuePair<string, string>> IEngine.GetDllExtensions()
		{
			return Modules.GetDllExtensions();
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x0001ADBF File Offset: 0x00018FBF
		IEnumerable<string> IEngine.GetModuleNames()
		{
			return Modules.GetModuleNames();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x0001ADC6 File Offset: 0x00018FC6
		void IEngine.RegisterBinaryFileMapping(string fileExtension, string contentType)
		{
			ContentHelper.AddContentType(fileExtension, contentType);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x0001ADCF File Offset: 0x00018FCF
		void IEngine.UnregisterBinaryFileMapping(string fileExtension)
		{
			ContentHelper.RemoveContentType(fileExtension);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x0001ADD7 File Offset: 0x00018FD7
		bool IEngine.TryCreateLocationFromResource(IResource resource, bool normalize, out IDataSourceLocation location)
		{
			return ResourceKinds.TryCreateLocationFromResource(resource, normalize, out location);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		IDataSourceLocation IEngine.NewLocation(string protocol, string authentication, IDictionary<string, object> address, string query)
		{
			IDataSourceLocation dataSourceLocation = DataSourceLocationFactory.New(protocol);
			dataSourceLocation.Authentication = authentication;
			if (address != null)
			{
				dataSourceLocation.Address = new Dictionary<string, object>(address);
				dataSourceLocation.Normalize();
				if (dataSourceLocation.Query != null && !string.IsNullOrEmpty(query))
				{
					throw new ArgumentException("json");
				}
			}
			else
			{
				dataSourceLocation.Address = null;
			}
			dataSourceLocation.Query = dataSourceLocation.Query ?? query;
			return dataSourceLocation;
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x0001AE4A File Offset: 0x0001904A
		object IEngine.MarshalToClr(IValue value, ITypeValue expectedType)
		{
			return ValueMarshaller.MarshalToClr((Value)value, (TypeValue)expectedType);
		}

		// Token: 0x04000671 RID: 1649
		private const string MinusToken = "-";

		// Token: 0x04000672 RID: 1650
		private static Engine engine;

		// Token: 0x04000673 RID: 1651
		private readonly LruCache<Engine.ParseKey, Engine.ParseEntry> parseCache = new LruCache<Engine.ParseKey, Engine.ParseEntry>(16, null);

		// Token: 0x04000674 RID: 1652
		private readonly List<IConnectionStringServiceHandler> connectionStringHandlers = new List<IConnectionStringServiceHandler>
		{
			OdbcConnectionStringService.Handler,
			OleDbConnectionStringService.Handler
		};

		// Token: 0x02000222 RID: 546
		private class ValueReference : IValueReference
		{
			// Token: 0x06000B91 RID: 2961 RVA: 0x0001AE94 File Offset: 0x00019094
			public ValueReference(IValueReference2 reference)
			{
				this.reference = reference;
			}

			// Token: 0x1700033C RID: 828
			// (get) Token: 0x06000B92 RID: 2962 RVA: 0x0001AEA3 File Offset: 0x000190A3
			public bool Evaluated
			{
				get
				{
					return this.reference.Evaluated;
				}
			}

			// Token: 0x1700033D RID: 829
			// (get) Token: 0x06000B93 RID: 2963 RVA: 0x0001AEB0 File Offset: 0x000190B0
			public Value Value
			{
				get
				{
					return (Value)this.reference.Value;
				}
			}

			// Token: 0x04000675 RID: 1653
			private IValueReference2 reference;
		}

		// Token: 0x02000223 RID: 547
		private class ParseKey : IEquatable<Engine.ParseKey>
		{
			// Token: 0x06000B94 RID: 2964 RVA: 0x0001AEC2 File Offset: 0x000190C2
			public ParseKey(ICacheableDocumentHost host, ITokens tokens)
			{
				this.host = host;
				this.tokens = tokens;
			}

			// Token: 0x06000B95 RID: 2965 RVA: 0x0001AED8 File Offset: 0x000190D8
			public bool Equals(Engine.ParseKey other)
			{
				return other != null && this.host.CacheIdentity.Equals(other.host.CacheIdentity) && this.tokens.Equals(other.tokens);
			}

			// Token: 0x06000B96 RID: 2966 RVA: 0x0001AF0D File Offset: 0x0001910D
			public override bool Equals(object other)
			{
				return this.Equals(other as Engine.ParseKey);
			}

			// Token: 0x06000B97 RID: 2967 RVA: 0x0001AF1B File Offset: 0x0001911B
			public override int GetHashCode()
			{
				return this.host.CacheIdentity.GetHashCode() + 37 * this.tokens.GetHashCode();
			}

			// Token: 0x04000676 RID: 1654
			private readonly ICacheableDocumentHost host;

			// Token: 0x04000677 RID: 1655
			private readonly ITokens tokens;
		}

		// Token: 0x02000224 RID: 548
		private class ParseEntry
		{
			// Token: 0x06000B98 RID: 2968 RVA: 0x0001AF3C File Offset: 0x0001913C
			public ParseEntry(IDocument[] documents, List<IError> errors)
			{
				this.documents = documents;
				this.errors = errors;
			}

			// Token: 0x1700033E RID: 830
			// (get) Token: 0x06000B99 RID: 2969 RVA: 0x0001AF52 File Offset: 0x00019152
			public IDocument[] Documents
			{
				get
				{
					return this.documents;
				}
			}

			// Token: 0x1700033F RID: 831
			// (get) Token: 0x06000B9A RID: 2970 RVA: 0x0001AF5A File Offset: 0x0001915A
			public List<IError> Errors
			{
				get
				{
					return this.errors;
				}
			}

			// Token: 0x04000678 RID: 1656
			private readonly IDocument[] documents;

			// Token: 0x04000679 RID: 1657
			private readonly List<IError> errors;
		}
	}
}
