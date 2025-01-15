using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x0200121A RID: 4634
	internal sealed class SqlSettings : IDataSourceCapabilities
	{
		// Token: 0x06007A2F RID: 31279 RVA: 0x001A6CE8 File Offset: 0x001A4EE8
		public SqlSettings()
		{
			this.InvalidIdentifierCharacters = EmptyArray<char>.Instance;
			this.MaxIdentifierLength = 18;
			this.QuoteNationalStringLiteral = new Func<string, string>(SqlSettings.StandardQuoteNationalCharacterString);
			this.QuoteAnsiStringLiteral = SqlSettings.StandardQuote("'");
			this.QuoteIdentifier = SqlSettings.StandardQuote("\"");
			this.RequiresAsForFromAlias = true;
			this.CatalogSeparator = SqlLanguageSymbols.DotSqlString;
			this.SchemaSeparator = SqlLanguageSymbols.DotSqlString;
			this.CatalogLocation = CatalogNameLocation.Start;
			this.UseCommaForCrossJoin = true;
			this.EmptyRowInsertStrategy = EmptyRowInsertStrategy.Legacy;
			this.DateTimePrefix = "'";
			this.DateTimeSuffix = "'";
			this.DateTimeOffsetPrefix = "'";
			this.DateTimeOffsetSuffix = "'";
			this.PagingStrategy = PagingStrategy.RowCountOnly;
			this.SelectItemNull = SqlLanguageStrings.NullSqlString;
			this.TimePrefix = "'";
			this.TimeSuffix = "'";
			this.BinaryPrefix = "0x";
			this.BinarySuffix = string.Empty;
			this.CreateTable = SqlLanguageStrings.CreateTableSqlString;
			this.CreateView = SqlLanguageStrings.CreateViewSqlString;
			this.DeleteCommand = SqlLanguageStrings.DeleteSqlString;
			this.IsMaxPrecision = true;
			this.TypesWithLength = new HashSet<string>
			{
				SqlLanguageStrings.VarCharSqlString.String,
				SqlLanguageStrings.NVarCharSqlString.String,
				SqlLanguageStrings.VarBinarySqlString.String
			};
		}

		// Token: 0x1700215E RID: 8542
		// (get) Token: 0x06007A30 RID: 31280 RVA: 0x001A6E53 File Offset: 0x001A5053
		// (set) Token: 0x06007A31 RID: 31281 RVA: 0x001A6E5B File Offset: 0x001A505B
		public char[] InvalidIdentifierCharacters { get; set; }

		// Token: 0x1700215F RID: 8543
		// (get) Token: 0x06007A32 RID: 31282 RVA: 0x001A6E64 File Offset: 0x001A5064
		// (set) Token: 0x06007A33 RID: 31283 RVA: 0x001A6E6C File Offset: 0x001A506C
		public int MaxIdentifierLength { get; set; }

		// Token: 0x17002160 RID: 8544
		// (get) Token: 0x06007A34 RID: 31284 RVA: 0x001A6E75 File Offset: 0x001A5075
		// (set) Token: 0x06007A35 RID: 31285 RVA: 0x001A6E7D File Offset: 0x001A507D
		public int MaxColumnNameLength { get; set; }

		// Token: 0x17002161 RID: 8545
		// (get) Token: 0x06007A36 RID: 31286 RVA: 0x001A6E86 File Offset: 0x001A5086
		// (set) Token: 0x06007A37 RID: 31287 RVA: 0x001A6E8E File Offset: 0x001A508E
		public Func<string, string> QuoteIdentifier { get; set; }

		// Token: 0x17002162 RID: 8546
		// (get) Token: 0x06007A38 RID: 31288 RVA: 0x001A6E97 File Offset: 0x001A5097
		// (set) Token: 0x06007A39 RID: 31289 RVA: 0x001A6E9F File Offset: 0x001A509F
		public Func<string, string> QuoteAnsiStringLiteral { get; set; }

		// Token: 0x17002163 RID: 8547
		// (get) Token: 0x06007A3A RID: 31290 RVA: 0x001A6EA8 File Offset: 0x001A50A8
		// (set) Token: 0x06007A3B RID: 31291 RVA: 0x001A6EB0 File Offset: 0x001A50B0
		public Func<string, string> QuoteNationalStringLiteral { get; set; }

		// Token: 0x17002164 RID: 8548
		// (get) Token: 0x06007A3C RID: 31292 RVA: 0x001A6EB9 File Offset: 0x001A50B9
		// (set) Token: 0x06007A3D RID: 31293 RVA: 0x001A6EC1 File Offset: 0x001A50C1
		public bool RequiresAsForFromAlias { get; set; }

		// Token: 0x17002165 RID: 8549
		// (get) Token: 0x06007A3E RID: 31294 RVA: 0x001A6ECA File Offset: 0x001A50CA
		// (set) Token: 0x06007A3F RID: 31295 RVA: 0x001A6ED2 File Offset: 0x001A50D2
		public ConstantSqlString CatalogSeparator { get; set; }

		// Token: 0x17002166 RID: 8550
		// (get) Token: 0x06007A40 RID: 31296 RVA: 0x001A6EDB File Offset: 0x001A50DB
		// (set) Token: 0x06007A41 RID: 31297 RVA: 0x001A6EE3 File Offset: 0x001A50E3
		public CatalogNameLocation CatalogLocation { get; set; }

		// Token: 0x17002167 RID: 8551
		// (get) Token: 0x06007A42 RID: 31298 RVA: 0x001A6EEC File Offset: 0x001A50EC
		// (set) Token: 0x06007A43 RID: 31299 RVA: 0x001A6EF4 File Offset: 0x001A50F4
		public ConstantSqlString SchemaSeparator { get; set; }

		// Token: 0x17002168 RID: 8552
		// (get) Token: 0x06007A44 RID: 31300 RVA: 0x001A6EFD File Offset: 0x001A50FD
		// (set) Token: 0x06007A45 RID: 31301 RVA: 0x001A6F05 File Offset: 0x001A5105
		public bool UseCommaForCrossJoin { get; set; }

		// Token: 0x17002169 RID: 8553
		// (get) Token: 0x06007A46 RID: 31302 RVA: 0x001A6F0E File Offset: 0x001A510E
		// (set) Token: 0x06007A47 RID: 31303 RVA: 0x001A6F16 File Offset: 0x001A5116
		public int MaxVariableStringLength { get; set; }

		// Token: 0x1700216A RID: 8554
		// (get) Token: 0x06007A48 RID: 31304 RVA: 0x001A6F1F File Offset: 0x001A511F
		// (set) Token: 0x06007A49 RID: 31305 RVA: 0x001A6F27 File Offset: 0x001A5127
		public EmptyRowInsertStrategy EmptyRowInsertStrategy { get; set; }

		// Token: 0x1700216B RID: 8555
		// (get) Token: 0x06007A4A RID: 31306 RVA: 0x001A6F30 File Offset: 0x001A5130
		// (set) Token: 0x06007A4B RID: 31307 RVA: 0x001A6F38 File Offset: 0x001A5138
		public string DateTimePrefix { get; set; }

		// Token: 0x1700216C RID: 8556
		// (get) Token: 0x06007A4C RID: 31308 RVA: 0x001A6F41 File Offset: 0x001A5141
		// (set) Token: 0x06007A4D RID: 31309 RVA: 0x001A6F49 File Offset: 0x001A5149
		public string DateTimeSuffix { get; set; }

		// Token: 0x1700216D RID: 8557
		// (get) Token: 0x06007A4E RID: 31310 RVA: 0x001A6F52 File Offset: 0x001A5152
		// (set) Token: 0x06007A4F RID: 31311 RVA: 0x001A6F5A File Offset: 0x001A515A
		public string DateTimeOffsetPrefix { get; set; }

		// Token: 0x1700216E RID: 8558
		// (get) Token: 0x06007A50 RID: 31312 RVA: 0x001A6F63 File Offset: 0x001A5163
		// (set) Token: 0x06007A51 RID: 31313 RVA: 0x001A6F6B File Offset: 0x001A516B
		public string DateTimeOffsetSuffix { get; set; }

		// Token: 0x1700216F RID: 8559
		// (get) Token: 0x06007A52 RID: 31314 RVA: 0x001A6F74 File Offset: 0x001A5174
		// (set) Token: 0x06007A53 RID: 31315 RVA: 0x001A6F7C File Offset: 0x001A517C
		public string IntervalPrefix { get; set; }

		// Token: 0x17002170 RID: 8560
		// (get) Token: 0x06007A54 RID: 31316 RVA: 0x001A6F85 File Offset: 0x001A5185
		// (set) Token: 0x06007A55 RID: 31317 RVA: 0x001A6F8D File Offset: 0x001A518D
		public string IntervalSuffix { get; set; }

		// Token: 0x17002171 RID: 8561
		// (get) Token: 0x06007A56 RID: 31318 RVA: 0x001A6F96 File Offset: 0x001A5196
		// (set) Token: 0x06007A57 RID: 31319 RVA: 0x001A6F9E File Offset: 0x001A519E
		public string BinaryPrefix { get; set; }

		// Token: 0x17002172 RID: 8562
		// (get) Token: 0x06007A58 RID: 31320 RVA: 0x001A6FA7 File Offset: 0x001A51A7
		// (set) Token: 0x06007A59 RID: 31321 RVA: 0x001A6FAF File Offset: 0x001A51AF
		public string BinarySuffix { get; set; }

		// Token: 0x17002173 RID: 8563
		// (get) Token: 0x06007A5A RID: 31322 RVA: 0x001A6FB8 File Offset: 0x001A51B8
		// (set) Token: 0x06007A5B RID: 31323 RVA: 0x001A6FC0 File Offset: 0x001A51C0
		public string DatePrefix { get; set; }

		// Token: 0x17002174 RID: 8564
		// (get) Token: 0x06007A5C RID: 31324 RVA: 0x001A6FC9 File Offset: 0x001A51C9
		// (set) Token: 0x06007A5D RID: 31325 RVA: 0x001A6FD1 File Offset: 0x001A51D1
		public string DateSuffix { get; set; }

		// Token: 0x17002175 RID: 8565
		// (get) Token: 0x06007A5E RID: 31326 RVA: 0x001A6FDA File Offset: 0x001A51DA
		// (set) Token: 0x06007A5F RID: 31327 RVA: 0x001A6FE2 File Offset: 0x001A51E2
		public PagingStrategy PagingStrategy { get; set; }

		// Token: 0x17002176 RID: 8566
		// (get) Token: 0x06007A60 RID: 31328 RVA: 0x001A6FEB File Offset: 0x001A51EB
		// (set) Token: 0x06007A61 RID: 31329 RVA: 0x001A6FF3 File Offset: 0x001A51F3
		public ConstantSqlString SelectItemNull { get; set; }

		// Token: 0x17002177 RID: 8567
		// (get) Token: 0x06007A62 RID: 31330 RVA: 0x001A6FFC File Offset: 0x001A51FC
		// (set) Token: 0x06007A63 RID: 31331 RVA: 0x001A7004 File Offset: 0x001A5204
		public bool SupportsCaseExpression { get; set; }

		// Token: 0x17002178 RID: 8568
		// (get) Token: 0x06007A64 RID: 31332 RVA: 0x001A700D File Offset: 0x001A520D
		// (set) Token: 0x06007A65 RID: 31333 RVA: 0x001A7015 File Offset: 0x001A5215
		public bool SupportsTableRotationFunctions { get; set; }

		// Token: 0x17002179 RID: 8569
		// (get) Token: 0x06007A66 RID: 31334 RVA: 0x001A701E File Offset: 0x001A521E
		// (set) Token: 0x06007A67 RID: 31335 RVA: 0x001A7026 File Offset: 0x001A5226
		public bool SupportsFullOuterJoinExpression { get; set; }

		// Token: 0x1700217A RID: 8570
		// (get) Token: 0x06007A68 RID: 31336 RVA: 0x001A702F File Offset: 0x001A522F
		// (set) Token: 0x06007A69 RID: 31337 RVA: 0x001A7037 File Offset: 0x001A5237
		public bool SupportsForeignKeys { get; set; }

		// Token: 0x1700217B RID: 8571
		// (get) Token: 0x06007A6A RID: 31338 RVA: 0x001A7040 File Offset: 0x001A5240
		// (set) Token: 0x06007A6B RID: 31339 RVA: 0x001A7048 File Offset: 0x001A5248
		public bool SupportsStoredFunctions { get; set; }

		// Token: 0x1700217C RID: 8572
		// (get) Token: 0x06007A6C RID: 31340 RVA: 0x001A7051 File Offset: 0x001A5251
		// (set) Token: 0x06007A6D RID: 31341 RVA: 0x001A7059 File Offset: 0x001A5259
		public bool SupportsStoredProcedures { get; set; }

		// Token: 0x1700217D RID: 8573
		// (get) Token: 0x06007A6E RID: 31342 RVA: 0x001A7062 File Offset: 0x001A5262
		// (set) Token: 0x06007A6F RID: 31343 RVA: 0x001A706A File Offset: 0x001A526A
		public bool SupportsExtendedProperties { get; set; }

		// Token: 0x1700217E RID: 8574
		// (get) Token: 0x06007A70 RID: 31344 RVA: 0x001A7073 File Offset: 0x001A5273
		// (set) Token: 0x06007A71 RID: 31345 RVA: 0x001A707B File Offset: 0x001A527B
		public bool SupportsOutputClause { get; set; }

		// Token: 0x1700217F RID: 8575
		// (get) Token: 0x06007A72 RID: 31346 RVA: 0x001A7084 File Offset: 0x001A5284
		// (set) Token: 0x06007A73 RID: 31347 RVA: 0x001A708C File Offset: 0x001A528C
		public bool SupportsViewCreation { get; set; }

		// Token: 0x17002180 RID: 8576
		// (get) Token: 0x06007A74 RID: 31348 RVA: 0x001A7095 File Offset: 0x001A5295
		// (set) Token: 0x06007A75 RID: 31349 RVA: 0x001A709D File Offset: 0x001A529D
		public bool SupportsIntervalConstants { get; set; }

		// Token: 0x17002181 RID: 8577
		// (get) Token: 0x06007A76 RID: 31350 RVA: 0x001A70A6 File Offset: 0x001A52A6
		// (set) Token: 0x06007A77 RID: 31351 RVA: 0x001A70AE File Offset: 0x001A52AE
		public string TimePrefix { get; set; }

		// Token: 0x17002182 RID: 8578
		// (get) Token: 0x06007A78 RID: 31352 RVA: 0x001A70B7 File Offset: 0x001A52B7
		// (set) Token: 0x06007A79 RID: 31353 RVA: 0x001A70BF File Offset: 0x001A52BF
		public string TimeSuffix { get; set; }

		// Token: 0x17002183 RID: 8579
		// (get) Token: 0x06007A7A RID: 31354 RVA: 0x001A70C8 File Offset: 0x001A52C8
		// (set) Token: 0x06007A7B RID: 31355 RVA: 0x001A70D0 File Offset: 0x001A52D0
		public HashSet<string> TypesWithLength { get; set; }

		// Token: 0x17002184 RID: 8580
		// (get) Token: 0x06007A7C RID: 31356 RVA: 0x001A70D9 File Offset: 0x001A52D9
		// (set) Token: 0x06007A7D RID: 31357 RVA: 0x001A70E1 File Offset: 0x001A52E1
		public bool UseMaxTypes { get; set; }

		// Token: 0x17002185 RID: 8581
		// (get) Token: 0x06007A7E RID: 31358 RVA: 0x001A70EA File Offset: 0x001A52EA
		// (set) Token: 0x06007A7F RID: 31359 RVA: 0x001A70F2 File Offset: 0x001A52F2
		public bool IsMaxPrecision { get; set; }

		// Token: 0x17002186 RID: 8582
		// (get) Token: 0x06007A80 RID: 31360 RVA: 0x001A70FB File Offset: 0x001A52FB
		// (set) Token: 0x06007A81 RID: 31361 RVA: 0x001A7103 File Offset: 0x001A5303
		public Func<string, ConstantSqlString> MaxTypesLiteral { get; set; }

		// Token: 0x17002187 RID: 8583
		// (get) Token: 0x06007A82 RID: 31362 RVA: 0x001A710C File Offset: 0x001A530C
		// (set) Token: 0x06007A83 RID: 31363 RVA: 0x001A7114 File Offset: 0x001A5314
		public ConstantSqlString CreateTable { get; set; }

		// Token: 0x17002188 RID: 8584
		// (get) Token: 0x06007A84 RID: 31364 RVA: 0x001A711D File Offset: 0x001A531D
		// (set) Token: 0x06007A85 RID: 31365 RVA: 0x001A7125 File Offset: 0x001A5325
		public ConstantSqlString CreateView { get; set; }

		// Token: 0x17002189 RID: 8585
		// (get) Token: 0x06007A86 RID: 31366 RVA: 0x001A712E File Offset: 0x001A532E
		// (set) Token: 0x06007A87 RID: 31367 RVA: 0x001A7136 File Offset: 0x001A5336
		public ConstantSqlString DeleteCommand { get; set; }

		// Token: 0x1700218A RID: 8586
		// (get) Token: 0x06007A88 RID: 31368 RVA: 0x001A713F File Offset: 0x001A533F
		// (set) Token: 0x06007A89 RID: 31369 RVA: 0x001A7147 File Offset: 0x001A5347
		public Dictionary<string, object> AdditionalSettings { get; set; }

		// Token: 0x06007A8A RID: 31370 RVA: 0x001A7150 File Offset: 0x001A5350
		public static string StandardQuoteNationalCharacterString(string literal)
		{
			return "N'" + literal.Replace("'", "''") + "'";
		}

		// Token: 0x06007A8B RID: 31371 RVA: 0x001A7174 File Offset: 0x001A5374
		public static Func<string, string> StandardQuote(string quote)
		{
			string doubleQuote = quote + quote;
			return (string i) => quote + i.Replace(quote, doubleQuote) + quote;
		}

		// Token: 0x06007A8C RID: 31372 RVA: 0x001A71B1 File Offset: 0x001A53B1
		public SqlSettings AddSetting(string setting, object value)
		{
			if (this.AdditionalSettings == null)
			{
				this.AdditionalSettings = new Dictionary<string, object>();
			}
			this.AdditionalSettings[setting] = value;
			return this;
		}

		// Token: 0x06007A8D RID: 31373 RVA: 0x001A71D4 File Offset: 0x001A53D4
		public T GetSetting<T>(string setting, T defaultValue)
		{
			object obj;
			if (this.AdditionalSettings != null && this.AdditionalSettings.TryGetValue(setting, out obj))
			{
				return (T)((object)obj);
			}
			return defaultValue;
		}

		// Token: 0x06007A8E RID: 31374 RVA: 0x001A7201 File Offset: 0x001A5401
		public SqlSettings With(Action<SqlSettings> applySettings)
		{
			applySettings(this);
			return this;
		}

		// Token: 0x06007A8F RID: 31375 RVA: 0x001A720C File Offset: 0x001A540C
		public SqlSettings Clone()
		{
			return new SqlSettings
			{
				InvalidIdentifierCharacters = this.InvalidIdentifierCharacters,
				MaxIdentifierLength = this.MaxIdentifierLength,
				MaxColumnNameLength = this.MaxColumnNameLength,
				QuoteIdentifier = this.QuoteIdentifier,
				QuoteAnsiStringLiteral = this.QuoteAnsiStringLiteral,
				QuoteNationalStringLiteral = this.QuoteNationalStringLiteral,
				RequiresAsForFromAlias = this.RequiresAsForFromAlias,
				CatalogSeparator = this.CatalogSeparator,
				CatalogLocation = this.CatalogLocation,
				SchemaSeparator = this.SchemaSeparator,
				UseCommaForCrossJoin = this.UseCommaForCrossJoin,
				MaxVariableStringLength = this.MaxVariableStringLength,
				DateTimePrefix = this.DateTimePrefix,
				DateTimeSuffix = this.DateTimeSuffix,
				DateTimeOffsetPrefix = this.DateTimeOffsetPrefix,
				DateTimeOffsetSuffix = this.DateTimeOffsetSuffix,
				IntervalPrefix = this.IntervalPrefix,
				IntervalSuffix = this.IntervalSuffix,
				BinaryPrefix = this.BinaryPrefix,
				BinarySuffix = this.BinarySuffix,
				DatePrefix = this.DatePrefix,
				DateSuffix = this.DateSuffix,
				PagingStrategy = this.PagingStrategy,
				EmptyRowInsertStrategy = this.EmptyRowInsertStrategy,
				SelectItemNull = this.SelectItemNull,
				SupportsCaseExpression = this.SupportsCaseExpression,
				SupportsTableRotationFunctions = this.SupportsTableRotationFunctions,
				SupportsFullOuterJoinExpression = this.SupportsFullOuterJoinExpression,
				SupportsForeignKeys = this.SupportsForeignKeys,
				SupportsStoredFunctions = this.SupportsStoredFunctions,
				SupportsStoredProcedures = this.SupportsStoredProcedures,
				SupportsExtendedProperties = this.SupportsExtendedProperties,
				SupportsOutputClause = this.SupportsOutputClause,
				SupportsViewCreation = this.SupportsViewCreation,
				SupportsIntervalConstants = this.SupportsIntervalConstants,
				TimePrefix = this.TimePrefix,
				TimeSuffix = this.TimeSuffix,
				TypesWithLength = ((this.TypesWithLength == null) ? null : new HashSet<string>(this.TypesWithLength)),
				UseMaxTypes = this.UseMaxTypes,
				IsMaxPrecision = this.IsMaxPrecision,
				MaxTypesLiteral = this.MaxTypesLiteral,
				CreateTable = this.CreateTable,
				CreateView = this.CreateView,
				DeleteCommand = this.DeleteCommand,
				AdditionalSettings = ((this.AdditionalSettings == null) ? null : new Dictionary<string, object>(this.AdditionalSettings))
			};
		}

		// Token: 0x040043BC RID: 17340
		private const string StandardLiteralQuote = "'";

		// Token: 0x040043BD RID: 17341
		private const string StandardIdentifierQuote = "\"";
	}
}
