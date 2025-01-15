using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface.DataSourceReference;
using Microsoft.Mashup.Engine.Interface.Help;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000030 RID: 48
	public interface IEngine : ITinyEngine
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000AF RID: 175
		ICollection<string> DisabledModules { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000B0 RID: 176
		ICollection<string> RemovedModules { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000B1 RID: 177
		IDictionary<string, object> Features { get; }

		// Token: 0x060000B2 RID: 178
		ITokens Tokenize(string text);

		// Token: 0x060000B3 RID: 179
		ITokens Tokenize(SegmentedString text);

		// Token: 0x060000B4 RID: 180
		IDocument Parse(ITokens tokens, IDocumentHost host, Action<IError> log);

		// Token: 0x060000B5 RID: 181
		IDocument[] ParseMany(ITokens tokens, IDocumentHost host, Action<IError> log);

		// Token: 0x060000B6 RID: 182
		IModule Compile(IDocument document, IRecordValue library, CompileOptions options, Action<IError> log);

		// Token: 0x060000B7 RID: 183
		IModule Link(IList<IModule> modules, Action<IError> log, LinkOptions options = LinkOptions.None);

		// Token: 0x060000B8 RID: 184
		IModule Link(IList<IModule> modules, IRecordValue library, Action<IError> log, LinkOptions options = LinkOptions.None);

		// Token: 0x060000B9 RID: 185
		IAssembly Assemble(IList<IModule> modules, IRecordValue library, IEngineHost host, Action<IError> log);

		// Token: 0x060000BA RID: 186
		IRecordValue GetLibrary(IEngineHost host, IEnumerable<string> additionalModules = null);

		// Token: 0x060000BB RID: 187
		IRecordValue LinkLibrary(IEngineHost host, IList<IModule> modules);

		// Token: 0x060000BC RID: 188
		IModule LibraryCachingModule(IModule module);

		// Token: 0x060000BD RID: 189
		IModule DelayLoadingModule(IModule definitions, Func<IEngineHost, IModule> moduleLoader);

		// Token: 0x060000BE RID: 190
		IModule OverrideEngineHostModule(IModule module, Func<IEngineHost, IEngineHost> binder);

		// Token: 0x060000BF RID: 191
		IModule InternalizeModule(IModule module, string newName = null);

		// Token: 0x060000C0 RID: 192
		IValue Deserialize(IRecordValue library, IExpression expression);

		// Token: 0x060000C1 RID: 193
		byte[] SerializeValue(IEngineHost engineHost, IValue value, IEnumerable<string> additionalModules);

		// Token: 0x060000C2 RID: 194
		IValue DeserializeValue(IEngineHost engineHost, byte[] serializedValue, IEnumerable<string> additionalModules);

		// Token: 0x060000C3 RID: 195
		IExpression GetExpression(IValue value);

		// Token: 0x060000C4 RID: 196
		IConstantExpression2 ConstantExpression(IValue value);

		// Token: 0x060000C5 RID: 197
		ISourceBuilder CreateSourceBuilder();

		// Token: 0x060000C6 RID: 198
		IDictionary<DocumentRange, IIdentifierBinding> Bind(IList<IDocument> documents);

		// Token: 0x060000C7 RID: 199
		IDictionary<DocumentRange, IList<DocumentRangePair>> GetDependencies(IList<IDocument> documents, IList<DocumentRange> identifiers);

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C8 RID: 200
		string TrueText { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C9 RID: 201
		string FalseText { get; }

		// Token: 0x060000CA RID: 202
		string ContextualKeywordText(ContextualKeyword keyword);

		// Token: 0x060000CB RID: 203
		string EnumText(EnumReference type);

		// Token: 0x060000CC RID: 204
		bool TryParseIdentifier(string text, out string identifier);

		// Token: 0x060000CD RID: 205
		bool TryParseFieldIdentifier(string text, out string identifier);

		// Token: 0x060000CE RID: 206
		string EscapeIdentifier(string identifier, ContextualKeyword[] keywords);

		// Token: 0x060000CF RID: 207
		bool TryParseString(string text, out string s);

		// Token: 0x060000D0 RID: 208
		bool TryParseSourceValue(string text, out IValue value);

		// Token: 0x060000D1 RID: 209
		bool TryParseNumberValue(string text, CultureInfo culture, out INumberValue number);

		// Token: 0x060000D2 RID: 210
		bool TryParseLogicalValue(string text, out bool value);

		// Token: 0x060000D3 RID: 211
		bool TryParseNullValue(string text);

		// Token: 0x060000D4 RID: 212
		bool TryParseDate(string text, CultureInfo culture, out IDateValue value);

		// Token: 0x060000D5 RID: 213
		bool TryParseDateTimeWithoutTimezone(string text, CultureInfo culture, out IDateTime value);

		// Token: 0x060000D6 RID: 214
		bool TryParseDateTimeWithTimezone(string text, CultureInfo culture, out IDateTimeZone value);

		// Token: 0x060000D7 RID: 215
		bool TryParseTime(string text, CultureInfo culture, out ITimeValue value);

		// Token: 0x060000D8 RID: 216
		bool TryParseDuration(string text, out IDurationValue value);

		// Token: 0x060000D9 RID: 217
		IValue Function(IEngineHost engineHost, FunctionHandle value);

		// Token: 0x060000DA RID: 218
		ITypeValue UnionTypes(ITypeValue type1, ITypeValue type2);

		// Token: 0x060000DB RID: 219
		Type ClrType(ITypeValue type);

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000DC RID: 220
		INumberValue NaN { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000DD RID: 221
		IValue Null { get; }

		// Token: 0x060000DE RID: 222
		IValue Logical(bool value);

		// Token: 0x060000DF RID: 223
		ITextValue Text(string value);

		// Token: 0x060000E0 RID: 224
		INumberValue Number(double value);

		// Token: 0x060000E1 RID: 225
		INumberValue Decimal(decimal value);

		// Token: 0x060000E2 RID: 226
		IDurationValue Duration(TimeSpan value);

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000E3 RID: 227
		IRecordValue EmptyRecord { get; }

		// Token: 0x060000E4 RID: 228
		IBinaryValue Binary(byte[] value, string contentType);

		// Token: 0x060000E5 RID: 229
		IKeys Keys(params string[] keys);

		// Token: 0x060000E6 RID: 230
		IRecordValue Record(IKeys keys, params IValue[] values);

		// Token: 0x060000E7 RID: 231
		IListValue List(params IValue[] values);

		// Token: 0x060000E8 RID: 232
		IListValue List(ITypeValue type, IRecordValue metaValue, params IValueReference2[] references);

		// Token: 0x060000E9 RID: 233
		ITableValue Table(IKeys columns, params IValue[] value);

		// Token: 0x060000EA RID: 234
		ITableValue Table(ITypeValue type, IRecordValue metaValue, params IValueReference2[] references);

		// Token: 0x060000EB RID: 235
		ITableValue Table(ITypeValue type, IRecordValue metaValue, IEnumerable<IRelationship> relationships, IColumnIdentity[] columnIdentities, params IValueReference2[] references);

		// Token: 0x060000EC RID: 236
		ITableValue Table(IEngineHost engineHost, Func<IDataReaderSource> getSource);

		// Token: 0x060000ED RID: 237
		ValueException2 Exception(IRecordValue value);

		// Token: 0x060000EE RID: 238
		IRecordValue ExceptionRecord(ITextValue reason, IValue message, IValue detail);

		// Token: 0x060000EF RID: 239
		IRecordValue ExceptionRecord(ITextValue reason, IValue message, IValue detail, IValue messageFormat, IValue messageParameters);

		// Token: 0x060000F0 RID: 240
		bool TryGetExceptionDetails(Exception e, out string detail);

		// Token: 0x060000F1 RID: 241
		byte[] BinaryFromFile(string fileName, out string contentType);

		// Token: 0x060000F2 RID: 242
		IValue Invoke(IValue function, params IValue[] arguments);

		// Token: 0x060000F3 RID: 243
		bool TryGetDocumentation(IValue value, out IDocumentation documentation);

		// Token: 0x060000F4 RID: 244
		IValue FromObject(object obj);

		// Token: 0x060000F5 RID: 245
		IValue SkipAndTake(IValue value, long skipCount, long? takeCount);

		// Token: 0x060000F6 RID: 246
		ITableValue FilterTable(ITableValue table, DataTable dataTable);

		// Token: 0x060000F7 RID: 247
		bool IsExtensionResourceKind(string resourceKind);

		// Token: 0x060000F8 RID: 248
		bool TryLookupResourceKind(string resourceKind, out ResourceKindInfo resourceKindInfo);

		// Token: 0x060000F9 RID: 249
		bool TryLookupResourceKind(string resourceKind, out ResourceKindInfo resourceKindInfo, out string moduleName);

		// Token: 0x060000FA RID: 250
		bool TryRegisterResourceKind(ResourceKindInfo resourceKindInfo, out Exception error);

		// Token: 0x060000FB RID: 251
		bool TryRegisterResourceKind(ResourceKindInfo resourceKindInfo, string moduleName, out Exception error);

		// Token: 0x060000FC RID: 252
		bool TryDelayedRegisterResourceKind(ResourceKindInfo resourceKindInfo, string moduleName, out Exception error);

		// Token: 0x060000FD RID: 253
		bool UnregisterResourceKind(string resourceKind);

		// Token: 0x060000FE RID: 254
		bool TryGetLocation(IExpression expression, bool deepInspection, out IDataSourceLocation location, out IRecordValue options, out IKeys unknownKeys, out bool mayHaveMoreNativeQueries);

		// Token: 0x060000FF RID: 255
		bool TryExtractPattern(IFunctionValue function, string[] patterns, out Dictionary<string, object> resultPattern);

		// Token: 0x06000100 RID: 256
		bool TryGetModule(string moduleName, out IModule module);

		// Token: 0x06000101 RID: 257
		bool TryLoadDllExtension(string path, out string moduleName, out Exception error);

		// Token: 0x06000102 RID: 258
		bool TryLoadExtension(string moduleSource, ILibraryService libraryService, out string moduleName, out Exception error);

		// Token: 0x06000103 RID: 259
		bool TryDelayedLoadExtension(IModule moduleInfo, ILibraryService libraryService, out Exception error);

		// Token: 0x06000104 RID: 260
		bool TryReplaceExtension(IModule moduleInfo, ILibraryService libraryService, bool delayLoad, out Exception error);

		// Token: 0x06000105 RID: 261
		bool UnloadExtension(string moduleName);

		// Token: 0x06000106 RID: 262
		bool TryWrapExpressionDataSource(IExpressionDocument extension, ILibraryService libraryService, out ISectionDocument document, out Exception error);

		// Token: 0x06000107 RID: 263
		bool TryCompileDataSource(ISectionDocument document, IModule library, ILibraryService libraryService, CompileOptions compileOptions, Action<IError> log, out IModule module);

		// Token: 0x06000108 RID: 264
		IEnumerable<KeyValuePair<string, string>> GetDllExtensions();

		// Token: 0x06000109 RID: 265
		IEnumerable<string> GetModuleNames();

		// Token: 0x0600010A RID: 266
		IPageReader CreatePageReader(ITableValue value);

		// Token: 0x0600010B RID: 267
		ITableValue NormalizeToTable(IValue value);

		// Token: 0x0600010C RID: 268
		bool TryGetPropertiesFromException(Exception exception, out ISerializedException properties);

		// Token: 0x0600010D RID: 269
		Exception CreateExceptionFromProperties(ISerializedException properties);

		// Token: 0x0600010E RID: 270
		IActionValue Action(Func<IValue> action);

		// Token: 0x0600010F RID: 271
		IConnectionStringService GetConnectionStringService(string providerName, bool validateProvider = false);

		// Token: 0x06000110 RID: 272
		void AddConnectionStringService(IConnectionStringServiceHandler handler);

		// Token: 0x06000111 RID: 273
		void RemoveConnectionStringService(IConnectionStringServiceHandler handler);

		// Token: 0x06000112 RID: 274
		IValue ApplyPreviewInference(IValue value);

		// Token: 0x06000113 RID: 275
		IValue ApplyPreviewInference(IValue value, char[] candidateDelimiters, EnumReference[] candidateQuoteStyles);

		// Token: 0x06000114 RID: 276
		[Obsolete]
		IValue ApplyPreviewInference(IValue value, char[] candidateDelimiters, EnumReference[] candidateQuoteStyles, bool inferUtf8);

		// Token: 0x06000115 RID: 277
		void AddStackTrace(ValueException2 exception, IList<SourceLocation> stack);

		// Token: 0x06000116 RID: 278
		IList<EmbeddingReference> DiscoverEmbeddingReferences(IDocument document);

		// Token: 0x06000117 RID: 279
		void RegisterBinaryFileMapping(string fileExtension, string contentType);

		// Token: 0x06000118 RID: 280
		void UnregisterBinaryFileMapping(string fileExtension);

		// Token: 0x06000119 RID: 281
		bool TryCreateLocationFromResource(IResource resource, bool normalize, out IDataSourceLocation location);

		// Token: 0x0600011A RID: 282
		IDataSourceLocation NewLocation(string protocol, string authentication, IDictionary<string, object> address, string query);

		// Token: 0x0600011B RID: 283
		object MarshalToClr(IValue value, ITypeValue expectedType);
	}
}
