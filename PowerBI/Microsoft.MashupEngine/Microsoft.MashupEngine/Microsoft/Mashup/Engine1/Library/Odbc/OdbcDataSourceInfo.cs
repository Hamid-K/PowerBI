using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005E8 RID: 1512
	internal abstract class OdbcDataSourceInfo
	{
		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x06002EFA RID: 12026
		public abstract string IdentifierQuoteChar { get; }

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x06002EFB RID: 12027
		public abstract bool UseSchemaInDmlStatements { get; }

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x06002EFC RID: 12028
		public abstract bool UseCatalogInDmlStatements { get; }

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06002EFD RID: 12029
		public abstract string CatalogNameSeparator { get; }

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06002EFE RID: 12030
		public abstract Odbc32.SQL_QL? CatalogNameLocation { get; }

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06002EFF RID: 12031
		public abstract Odbc32.SQL_SC Sql92Conformance { get; }

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06002F00 RID: 12032
		public abstract int MaxColumnsInOrderBy { get; }

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06002F01 RID: 12033
		public abstract int MaxIdentifierNameLength { get; }

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x06002F02 RID: 12034
		public abstract int MaxColumnsInGroupBy { get; }

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x06002F03 RID: 12035
		public abstract int MaxColumnsInSelect { get; }

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x06002F04 RID: 12036
		public abstract int MaxParameters { get; }

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x06002F05 RID: 12037
		public abstract bool OrderByColumnsInSelect { get; }

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x06002F06 RID: 12038
		public abstract string DriverOdbcVersion { get; }

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x06002F07 RID: 12039
		public abstract Odbc32.SQL_FN_STR SupportedStringFunctions { get; }

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x06002F08 RID: 12040
		public abstract Odbc32.SQL_AF SupportedAggregateFunctions { get; }

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06002F09 RID: 12041
		public abstract Odbc32.SQL_SP SupportedPredicates { get; }

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x06002F0A RID: 12042
		public abstract Odbc32.SQL_SRJO SupportedSql92RelationalJoinOperators { get; }

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x06002F0B RID: 12043
		public abstract Odbc32.SQL_SVE SupportedValueExpressions { get; }

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x06002F0C RID: 12044
		public abstract bool SupportsColumnAliases { get; }

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x06002F0D RID: 12045
		public abstract Odbc32.SQL_GB GroupByCapabilities { get; }

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x06002F0E RID: 12046
		public abstract Odbc32.SQL_FUN_NUM SupportedNumericFunctions { get; }

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06002F0F RID: 12047
		public abstract Odbc32.SQL_FN_TD SupportedTimeDateFunctions { get; }

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06002F10 RID: 12048
		public abstract Odbc32.SQL_FN_SYSTEM SupportedSystemFunctions { get; }

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06002F11 RID: 12049
		public abstract Odbc32.SQL_TSI SupportedTimeDateAddIntervals { get; }

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06002F12 RID: 12050
		public abstract Odbc32.SQL_TSI SupportedTimeDateDiffIntervals { get; }

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06002F13 RID: 12051
		public abstract Odbc32.SQL_CB? StringConcatNullBehavior { get; }

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06002F14 RID: 12052
		public abstract bool? CatalogName { get; }

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06002F15 RID: 12053
		public abstract string CatalogTerm { get; }

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06002F16 RID: 12054
		public abstract string SchemaTerm { get; }

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06002F17 RID: 12055
		public abstract Odbc32.SQL_OIC InterfaceConformanceLevel { get; }

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06002F18 RID: 12056
		public abstract string SearchPatternEscapeCharacter { get; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06002F19 RID: 12057
		public abstract Odbc32.SQL_FN_CVT SupportedConvertFunctions { get; }

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06002F1A RID: 12058
		public abstract IDictionary<Odbc32.SQL_INFO, Odbc32.SQL_CVT> SupportedConversions { get; }

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06002F1B RID: 12059
		public abstract string IdentifierSpecialCharacters { get; }

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06002F1C RID: 12060
		public abstract int? FractionalSecondsScale { get; }

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06002F1D RID: 12061
		public abstract bool SupportsNumericLiterals { get; }

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06002F1E RID: 12062
		public abstract bool SupportsStringLiterals { get; }

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06002F1F RID: 12063
		public abstract IDictionary<string, string> StringLiteralEscapeCharacters { get; }

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06002F20 RID: 12064
		public abstract bool SupportsOdbcDateLiterals { get; }

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06002F21 RID: 12065
		public abstract bool SupportsOdbcTimeLiterals { get; }

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06002F22 RID: 12066
		public abstract bool SupportsOdbcTimestampLiterals { get; }

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06002F23 RID: 12067
		public abstract bool SupportsTopOrLimit { get; }

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x06002F24 RID: 12068
		public abstract NativeQuerySchemaStrategy NativeQuerySchemaStrategy { get; }

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x06002F25 RID: 12069
		public abstract bool PrepareStatements { get; }

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x06002F26 RID: 12070
		public abstract Odbc32.SQL_RETURN_ESCAPE_CLAUSE ReturnEscapeClause { get; }

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06002F27 RID: 12071
		public abstract RecordValue DefaultTypeParameters { get; }

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06002F28 RID: 12072
		public abstract bool SupportsNativeQuery { get; }

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06002F29 RID: 12073
		public abstract bool TryRecoverDateDiff { get; }

		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06002F2A RID: 12074
		public abstract bool TryRecoverCoalesce { get; }

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x06002F2B RID: 12075 RVA: 0x0008F9D0 File Offset: 0x0008DBD0
		public decimal? TicksPerFractionalSecond
		{
			get
			{
				if (this.FractionalSecondsScale != null)
				{
					return new decimal?((decimal)Math.Pow(10.0, (double)(7 - this.FractionalSecondsScale.Value)));
				}
				return null;
			}
		}

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x06002F2C RID: 12076 RVA: 0x0008FA20 File Offset: 0x0008DC20
		public int? FractionalSecondsPerSecond
		{
			get
			{
				if (this.FractionalSecondsScale != null)
				{
					return new int?((int)Math.Pow(10.0, (double)this.FractionalSecondsScale.Value));
				}
				return null;
			}
		}

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x06002F2D RID: 12077 RVA: 0x0008FA6C File Offset: 0x0008DC6C
		public bool? SupportsCatalogNames
		{
			get
			{
				bool? catalogName = this.CatalogName;
				if (catalogName == null && this.CatalogTerm != null)
				{
					return new bool?(this.CatalogTerm != "");
				}
				return catalogName;
			}
		}

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06002F2E RID: 12078 RVA: 0x0008FAA8 File Offset: 0x0008DCA8
		public bool? SupportsSchemaNames
		{
			get
			{
				if (this.SchemaTerm != null)
				{
					return new bool?(this.SchemaTerm != "");
				}
				return null;
			}
		}

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06002F2F RID: 12079 RVA: 0x0008FADC File Offset: 0x0008DCDC
		public virtual bool SupportsDerivedTable
		{
			get
			{
				return this.Supports(Odbc32.SQL_SC.SQL_SC_SQL92_FULL);
			}
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x000020FD File Offset: 0x000002FD
		public OdbcDataSourceInfo()
		{
		}

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06002F31 RID: 12081 RVA: 0x0008FAE8 File Offset: 0x0008DCE8
		public bool IsDriverV3
		{
			get
			{
				if (this.isDriverV3 == null)
				{
					this.isDriverV3 = new bool?(int.Parse(this.DriverOdbcVersion.Substring(0, 2), CultureInfo.InvariantCulture) >= 3);
				}
				return this.isDriverV3.Value;
			}
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x0008FB35 File Offset: 0x0008DD35
		public virtual bool Supports(Odbc32.SQL_OIC level)
		{
			return this.InterfaceConformanceLevel >= level;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x0008FB43 File Offset: 0x0008DD43
		public virtual bool Supports(Odbc32.SQL_SC level)
		{
			return this.Sql92Conformance >= level;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0008FB51 File Offset: 0x0008DD51
		public virtual bool Supports(Odbc32.SQL_FN_STR stringFunction)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedStringFunctions, (int)stringFunction);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x0008FB5F File Offset: 0x0008DD5F
		public virtual bool Supports(Odbc32.SQL_SP predicate)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedPredicates, (int)predicate);
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x0008FB6D File Offset: 0x0008DD6D
		public virtual bool Supports(Odbc32.SQL_SRJO srjo)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedSql92RelationalJoinOperators, (int)srjo);
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x0008FB7B File Offset: 0x0008DD7B
		public virtual bool Supports(Odbc32.SQL_SVE sve)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedValueExpressions, (int)sve);
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x0008FB89 File Offset: 0x0008DD89
		public virtual bool Supports(Odbc32.SQL_AF aggregateFunction)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedAggregateFunctions, (int)aggregateFunction);
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x0008FB97 File Offset: 0x0008DD97
		public virtual bool Supports(Odbc32.SQL_FUN_NUM numericFunction)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedNumericFunctions, (int)numericFunction);
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x0008FBA5 File Offset: 0x0008DDA5
		public virtual bool Supports(Odbc32.SQL_FN_TD datetimeFunction)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedTimeDateFunctions, (int)datetimeFunction);
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x0008FBB3 File Offset: 0x0008DDB3
		public virtual bool Supports(Odbc32.SQL_FN_SYSTEM systemFunction)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedSystemFunctions, (int)systemFunction);
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0008FBC1 File Offset: 0x0008DDC1
		public bool SupportsTimedateAddInterval(Odbc32.SQL_TSI intervalDimension)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedTimeDateAddIntervals, (int)intervalDimension);
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0008FBCF File Offset: 0x0008DDCF
		public bool SupportsTimedateDiffInterval(Odbc32.SQL_TSI intervalDimension)
		{
			return OdbcDataSourceInfo.Supports((int)this.SupportedTimeDateDiffIntervals, (int)intervalDimension);
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x0008FBDD File Offset: 0x0008DDDD
		public static OdbcDataSourceInfo Load(IOdbcConnection connection)
		{
			return new OdbcDataSourceInfo.OdbcMemoryDataSourceInfo(new OdbcDataSourceInfo.OdbcConnectionDataSourceInfo(connection));
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x0008FBEC File Offset: 0x0008DDEC
		public bool SupportsConversion(Odbc32.SQL_TYPE from, Odbc32.SQL_TYPE to)
		{
			OdbcTypeMap odbcTypeMap;
			OdbcTypeMap odbcTypeMap2;
			Odbc32.SQL_CVT sql_CVT;
			return OdbcTypeMap.TryGet(from, out odbcTypeMap) && OdbcTypeMap.TryGet(to, out odbcTypeMap2) && odbcTypeMap.ConvertInfoType != null && odbcTypeMap2.ConvertBitmask != null && this.SupportedConversions.TryGetValue(odbcTypeMap.ConvertInfoType.Value, out sql_CVT) && (sql_CVT & odbcTypeMap2.ConvertBitmask.Value) > Odbc32.SQL_CVT.None;
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x0008FC53 File Offset: 0x0008DE53
		private static bool Supports(int bitmask, int flag)
		{
			return (bitmask & flag) != 0;
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0008FC5B File Offset: 0x0008DE5B
		public OdbcDataSourceInfo OverrideWithOptions(OdbcOptions options)
		{
			return new OdbcDataSourceInfo.OverriddenDataSourceInfo(this, options);
		}

		// Token: 0x040014DA RID: 5338
		public const int TicksPerSecondMagnitude = 7;

		// Token: 0x040014DB RID: 5339
		public const int MaximumFractionalSecondScale = 9;

		// Token: 0x040014DC RID: 5340
		private static HashSet<Odbc32.SQL_INFO> requiredSqlInfo = new HashSet<Odbc32.SQL_INFO> { Odbc32.SQL_INFO.SQL_IDENTIFIER_QUOTE_CHAR };

		// Token: 0x040014DD RID: 5341
		private bool? isDriverV3;

		// Token: 0x020005E9 RID: 1513
		private sealed class OdbcMemoryDataSourceInfo : OdbcDataSourceInfo
		{
			// Token: 0x06002F43 RID: 12099 RVA: 0x0008FC7C File Offset: 0x0008DE7C
			public OdbcMemoryDataSourceInfo(OdbcDataSourceInfo.OdbcConnectionDataSourceInfo dataSourceInfo)
			{
				this.identifierQuoteChar = dataSourceInfo.IdentifierQuoteChar;
				this.useSchemaInDmlStatements = dataSourceInfo.UseSchemaInDmlStatements;
				this.useCatalogInDmlStatements = dataSourceInfo.UseCatalogInDmlStatements;
				this.catalogNameSeparator = dataSourceInfo.CatalogNameSeparator;
				this.catalogNameLocation = dataSourceInfo.CatalogNameLocation;
				this.sql92Conformance = dataSourceInfo.Sql92Conformance;
				this.maxColumnsInOrderBy = dataSourceInfo.MaxColumnsInOrderBy;
				this.maxIdentifierNameLength = dataSourceInfo.MaxIdentifierNameLength;
				this.maxColumnsInGroupBy = dataSourceInfo.MaxColumnsInGroupBy;
				this.maxColumnsInSelect = dataSourceInfo.MaxColumnsInSelect;
				this.maxParameters = dataSourceInfo.MaxParameters;
				this.orderByColumnsInSelect = dataSourceInfo.OrderByColumnsInSelect;
				this.supportedStringFunctions = dataSourceInfo.SupportedStringFunctions;
				this.supportedAggregateFunctions = dataSourceInfo.SupportedAggregateFunctions;
				this.supportedPredicates = dataSourceInfo.SupportedPredicates;
				this.supportedRelationalJoinOperators = dataSourceInfo.SupportedSql92RelationalJoinOperators;
				this.supportedValueExpressions = dataSourceInfo.SupportedValueExpressions;
				this.supportedColumnAliases = dataSourceInfo.SupportsColumnAliases;
				this.groupByCapabilities = dataSourceInfo.GroupByCapabilities;
				this.supportedNumericFunctions = dataSourceInfo.SupportedNumericFunctions;
				this.supportedTimeDateFunctions = dataSourceInfo.SupportedTimeDateFunctions;
				this.supportedSystemFunctions = dataSourceInfo.SupportedSystemFunctions;
				this.supportedTimeDateAddIntervals = dataSourceInfo.SupportedTimeDateAddIntervals;
				this.supportedTimeDateDiffIntervals = dataSourceInfo.SupportedTimeDateDiffIntervals;
				this.stringConcatNullBehavior = dataSourceInfo.StringConcatNullBehavior;
				this.catalogName = dataSourceInfo.CatalogName;
				this.catalogTerm = dataSourceInfo.CatalogTerm;
				this.schemaTerm = dataSourceInfo.SchemaTerm;
				this.interfaceConformanceLevel = dataSourceInfo.InterfaceConformanceLevel;
				this.searchPatternEscapeCharacter = dataSourceInfo.SearchPatternEscapeCharacter;
				this.supportedConvertFunctions = dataSourceInfo.SupportedConvertFunctions;
				this.supportedConversions = dataSourceInfo.SupportedConversions;
				this.identifierSpecialCharacters = dataSourceInfo.IdentifierSpecialCharacters;
				this.fractionalSecondsScale = dataSourceInfo.FractionalSecondsScale;
				this.supportsOdbcNumericLiterals = dataSourceInfo.SupportsNumericLiterals;
				this.supportsOdbcStringLiterals = dataSourceInfo.SupportsStringLiterals;
				this.stringLiteralEscapeCharaceters = dataSourceInfo.StringLiteralEscapeCharacters;
				this.supportsOdbcDateLiterals = dataSourceInfo.SupportsOdbcDateLiterals;
				this.supportsOdbcTimeLiterals = dataSourceInfo.SupportsOdbcTimeLiterals;
				this.supportsOdbcTimestampLiterals = dataSourceInfo.SupportsOdbcTimestampLiterals;
				this.supportsTopOrLimit = dataSourceInfo.SupportsTopOrLimit;
				this.nativeQuerySchemaStrategy = dataSourceInfo.NativeQuerySchemaStrategy;
				this.prepareStatements = dataSourceInfo.PrepareStatements;
				this.returnEscapeClause = dataSourceInfo.ReturnEscapeClause;
				this.driverOdbcVersion = dataSourceInfo.DriverOdbcVersion;
				this.defaultTypeParameters = dataSourceInfo.DefaultTypeParameters;
				this.supportsNativeQuery = dataSourceInfo.SupportsNativeQuery;
				this.tryRecoverDateDiff = dataSourceInfo.TryRecoverDateDiff;
				this.tryRecoverCoalesce = dataSourceInfo.TryRecoverCoalesce;
			}

			// Token: 0x17001150 RID: 4432
			// (get) Token: 0x06002F44 RID: 12100 RVA: 0x0008FEDB File Offset: 0x0008E0DB
			public override string IdentifierQuoteChar
			{
				get
				{
					return this.identifierQuoteChar;
				}
			}

			// Token: 0x17001151 RID: 4433
			// (get) Token: 0x06002F45 RID: 12101 RVA: 0x0008FEE3 File Offset: 0x0008E0E3
			public override bool UseSchemaInDmlStatements
			{
				get
				{
					return this.useSchemaInDmlStatements;
				}
			}

			// Token: 0x17001152 RID: 4434
			// (get) Token: 0x06002F46 RID: 12102 RVA: 0x0008FEEB File Offset: 0x0008E0EB
			public override bool UseCatalogInDmlStatements
			{
				get
				{
					return this.useCatalogInDmlStatements;
				}
			}

			// Token: 0x17001153 RID: 4435
			// (get) Token: 0x06002F47 RID: 12103 RVA: 0x0008FEF3 File Offset: 0x0008E0F3
			public override string CatalogNameSeparator
			{
				get
				{
					return this.catalogNameSeparator;
				}
			}

			// Token: 0x17001154 RID: 4436
			// (get) Token: 0x06002F48 RID: 12104 RVA: 0x0008FEFB File Offset: 0x0008E0FB
			public override Odbc32.SQL_QL? CatalogNameLocation
			{
				get
				{
					return this.catalogNameLocation;
				}
			}

			// Token: 0x17001155 RID: 4437
			// (get) Token: 0x06002F49 RID: 12105 RVA: 0x0008FF03 File Offset: 0x0008E103
			public override Odbc32.SQL_SC Sql92Conformance
			{
				get
				{
					return this.sql92Conformance;
				}
			}

			// Token: 0x17001156 RID: 4438
			// (get) Token: 0x06002F4A RID: 12106 RVA: 0x0008FF0B File Offset: 0x0008E10B
			public override int MaxColumnsInOrderBy
			{
				get
				{
					return this.maxColumnsInOrderBy;
				}
			}

			// Token: 0x17001157 RID: 4439
			// (get) Token: 0x06002F4B RID: 12107 RVA: 0x0008FF13 File Offset: 0x0008E113
			public override int MaxIdentifierNameLength
			{
				get
				{
					return this.maxIdentifierNameLength;
				}
			}

			// Token: 0x17001158 RID: 4440
			// (get) Token: 0x06002F4C RID: 12108 RVA: 0x0008FF1B File Offset: 0x0008E11B
			public override int MaxColumnsInGroupBy
			{
				get
				{
					return this.maxColumnsInGroupBy;
				}
			}

			// Token: 0x17001159 RID: 4441
			// (get) Token: 0x06002F4D RID: 12109 RVA: 0x0008FF23 File Offset: 0x0008E123
			public override int MaxColumnsInSelect
			{
				get
				{
					return this.maxColumnsInSelect;
				}
			}

			// Token: 0x1700115A RID: 4442
			// (get) Token: 0x06002F4E RID: 12110 RVA: 0x0008FF2B File Offset: 0x0008E12B
			public override int MaxParameters
			{
				get
				{
					return this.maxParameters;
				}
			}

			// Token: 0x1700115B RID: 4443
			// (get) Token: 0x06002F4F RID: 12111 RVA: 0x0008FF33 File Offset: 0x0008E133
			public override bool OrderByColumnsInSelect
			{
				get
				{
					return this.orderByColumnsInSelect;
				}
			}

			// Token: 0x1700115C RID: 4444
			// (get) Token: 0x06002F50 RID: 12112 RVA: 0x0008FF3B File Offset: 0x0008E13B
			public override string DriverOdbcVersion
			{
				get
				{
					return this.driverOdbcVersion;
				}
			}

			// Token: 0x1700115D RID: 4445
			// (get) Token: 0x06002F51 RID: 12113 RVA: 0x0008FF43 File Offset: 0x0008E143
			public override Odbc32.SQL_FN_STR SupportedStringFunctions
			{
				get
				{
					return this.supportedStringFunctions;
				}
			}

			// Token: 0x1700115E RID: 4446
			// (get) Token: 0x06002F52 RID: 12114 RVA: 0x0008FF4B File Offset: 0x0008E14B
			public override Odbc32.SQL_AF SupportedAggregateFunctions
			{
				get
				{
					return this.supportedAggregateFunctions;
				}
			}

			// Token: 0x1700115F RID: 4447
			// (get) Token: 0x06002F53 RID: 12115 RVA: 0x0008FF53 File Offset: 0x0008E153
			public override Odbc32.SQL_SP SupportedPredicates
			{
				get
				{
					return this.supportedPredicates;
				}
			}

			// Token: 0x17001160 RID: 4448
			// (get) Token: 0x06002F54 RID: 12116 RVA: 0x0008FF5B File Offset: 0x0008E15B
			public override Odbc32.SQL_SRJO SupportedSql92RelationalJoinOperators
			{
				get
				{
					return this.supportedRelationalJoinOperators;
				}
			}

			// Token: 0x17001161 RID: 4449
			// (get) Token: 0x06002F55 RID: 12117 RVA: 0x0008FF63 File Offset: 0x0008E163
			public override Odbc32.SQL_SVE SupportedValueExpressions
			{
				get
				{
					return this.supportedValueExpressions;
				}
			}

			// Token: 0x17001162 RID: 4450
			// (get) Token: 0x06002F56 RID: 12118 RVA: 0x0008FF6B File Offset: 0x0008E16B
			public override bool SupportsColumnAliases
			{
				get
				{
					return this.supportedColumnAliases;
				}
			}

			// Token: 0x17001163 RID: 4451
			// (get) Token: 0x06002F57 RID: 12119 RVA: 0x0008FF73 File Offset: 0x0008E173
			public override Odbc32.SQL_GB GroupByCapabilities
			{
				get
				{
					return this.groupByCapabilities;
				}
			}

			// Token: 0x17001164 RID: 4452
			// (get) Token: 0x06002F58 RID: 12120 RVA: 0x0008FF7B File Offset: 0x0008E17B
			public override Odbc32.SQL_FUN_NUM SupportedNumericFunctions
			{
				get
				{
					return this.supportedNumericFunctions;
				}
			}

			// Token: 0x17001165 RID: 4453
			// (get) Token: 0x06002F59 RID: 12121 RVA: 0x0008FF83 File Offset: 0x0008E183
			public override Odbc32.SQL_FN_TD SupportedTimeDateFunctions
			{
				get
				{
					return this.supportedTimeDateFunctions;
				}
			}

			// Token: 0x17001166 RID: 4454
			// (get) Token: 0x06002F5A RID: 12122 RVA: 0x0008FF8B File Offset: 0x0008E18B
			public override Odbc32.SQL_FN_SYSTEM SupportedSystemFunctions
			{
				get
				{
					return this.supportedSystemFunctions;
				}
			}

			// Token: 0x17001167 RID: 4455
			// (get) Token: 0x06002F5B RID: 12123 RVA: 0x0008FF93 File Offset: 0x0008E193
			public override Odbc32.SQL_TSI SupportedTimeDateAddIntervals
			{
				get
				{
					return this.supportedTimeDateAddIntervals;
				}
			}

			// Token: 0x17001168 RID: 4456
			// (get) Token: 0x06002F5C RID: 12124 RVA: 0x0008FF9B File Offset: 0x0008E19B
			public override Odbc32.SQL_TSI SupportedTimeDateDiffIntervals
			{
				get
				{
					return this.supportedTimeDateDiffIntervals;
				}
			}

			// Token: 0x17001169 RID: 4457
			// (get) Token: 0x06002F5D RID: 12125 RVA: 0x0008FFA3 File Offset: 0x0008E1A3
			public override Odbc32.SQL_CB? StringConcatNullBehavior
			{
				get
				{
					return this.stringConcatNullBehavior;
				}
			}

			// Token: 0x1700116A RID: 4458
			// (get) Token: 0x06002F5E RID: 12126 RVA: 0x0008FFAB File Offset: 0x0008E1AB
			public override bool? CatalogName
			{
				get
				{
					return this.catalogName;
				}
			}

			// Token: 0x1700116B RID: 4459
			// (get) Token: 0x06002F5F RID: 12127 RVA: 0x0008FFB3 File Offset: 0x0008E1B3
			public override string CatalogTerm
			{
				get
				{
					return this.catalogTerm;
				}
			}

			// Token: 0x1700116C RID: 4460
			// (get) Token: 0x06002F60 RID: 12128 RVA: 0x0008FFBB File Offset: 0x0008E1BB
			public override string SchemaTerm
			{
				get
				{
					return this.schemaTerm;
				}
			}

			// Token: 0x1700116D RID: 4461
			// (get) Token: 0x06002F61 RID: 12129 RVA: 0x0008FFC3 File Offset: 0x0008E1C3
			public override Odbc32.SQL_OIC InterfaceConformanceLevel
			{
				get
				{
					return this.interfaceConformanceLevel;
				}
			}

			// Token: 0x1700116E RID: 4462
			// (get) Token: 0x06002F62 RID: 12130 RVA: 0x0008FFCB File Offset: 0x0008E1CB
			public override string SearchPatternEscapeCharacter
			{
				get
				{
					return this.searchPatternEscapeCharacter;
				}
			}

			// Token: 0x1700116F RID: 4463
			// (get) Token: 0x06002F63 RID: 12131 RVA: 0x0008FFD3 File Offset: 0x0008E1D3
			public override Odbc32.SQL_FN_CVT SupportedConvertFunctions
			{
				get
				{
					return this.supportedConvertFunctions;
				}
			}

			// Token: 0x17001170 RID: 4464
			// (get) Token: 0x06002F64 RID: 12132 RVA: 0x0008FFDB File Offset: 0x0008E1DB
			public override IDictionary<Odbc32.SQL_INFO, Odbc32.SQL_CVT> SupportedConversions
			{
				get
				{
					return this.supportedConversions;
				}
			}

			// Token: 0x17001171 RID: 4465
			// (get) Token: 0x06002F65 RID: 12133 RVA: 0x0008FFE3 File Offset: 0x0008E1E3
			public override string IdentifierSpecialCharacters
			{
				get
				{
					return this.identifierSpecialCharacters;
				}
			}

			// Token: 0x17001172 RID: 4466
			// (get) Token: 0x06002F66 RID: 12134 RVA: 0x0008FFEB File Offset: 0x0008E1EB
			public override int? FractionalSecondsScale
			{
				get
				{
					return this.fractionalSecondsScale;
				}
			}

			// Token: 0x17001173 RID: 4467
			// (get) Token: 0x06002F67 RID: 12135 RVA: 0x0008FFF3 File Offset: 0x0008E1F3
			public override bool SupportsNumericLiterals
			{
				get
				{
					return this.supportsOdbcNumericLiterals;
				}
			}

			// Token: 0x17001174 RID: 4468
			// (get) Token: 0x06002F68 RID: 12136 RVA: 0x0008FFFB File Offset: 0x0008E1FB
			public override bool SupportsStringLiterals
			{
				get
				{
					return this.supportsOdbcStringLiterals;
				}
			}

			// Token: 0x17001175 RID: 4469
			// (get) Token: 0x06002F69 RID: 12137 RVA: 0x00090003 File Offset: 0x0008E203
			public override IDictionary<string, string> StringLiteralEscapeCharacters
			{
				get
				{
					return this.stringLiteralEscapeCharaceters;
				}
			}

			// Token: 0x17001176 RID: 4470
			// (get) Token: 0x06002F6A RID: 12138 RVA: 0x0009000B File Offset: 0x0008E20B
			public override bool SupportsOdbcDateLiterals
			{
				get
				{
					return this.supportsOdbcDateLiterals;
				}
			}

			// Token: 0x17001177 RID: 4471
			// (get) Token: 0x06002F6B RID: 12139 RVA: 0x00090013 File Offset: 0x0008E213
			public override bool SupportsOdbcTimeLiterals
			{
				get
				{
					return this.supportsOdbcTimeLiterals;
				}
			}

			// Token: 0x17001178 RID: 4472
			// (get) Token: 0x06002F6C RID: 12140 RVA: 0x0009001B File Offset: 0x0008E21B
			public override bool SupportsOdbcTimestampLiterals
			{
				get
				{
					return this.supportsOdbcTimestampLiterals;
				}
			}

			// Token: 0x17001179 RID: 4473
			// (get) Token: 0x06002F6D RID: 12141 RVA: 0x00090023 File Offset: 0x0008E223
			public override bool SupportsTopOrLimit
			{
				get
				{
					return this.supportsTopOrLimit;
				}
			}

			// Token: 0x1700117A RID: 4474
			// (get) Token: 0x06002F6E RID: 12142 RVA: 0x0009002B File Offset: 0x0008E22B
			public override NativeQuerySchemaStrategy NativeQuerySchemaStrategy
			{
				get
				{
					return this.nativeQuerySchemaStrategy;
				}
			}

			// Token: 0x1700117B RID: 4475
			// (get) Token: 0x06002F6F RID: 12143 RVA: 0x00090033 File Offset: 0x0008E233
			public override bool PrepareStatements
			{
				get
				{
					return this.prepareStatements;
				}
			}

			// Token: 0x1700117C RID: 4476
			// (get) Token: 0x06002F70 RID: 12144 RVA: 0x0009003B File Offset: 0x0008E23B
			public override Odbc32.SQL_RETURN_ESCAPE_CLAUSE ReturnEscapeClause
			{
				get
				{
					return this.returnEscapeClause;
				}
			}

			// Token: 0x1700117D RID: 4477
			// (get) Token: 0x06002F71 RID: 12145 RVA: 0x00090043 File Offset: 0x0008E243
			public override RecordValue DefaultTypeParameters
			{
				get
				{
					return this.defaultTypeParameters;
				}
			}

			// Token: 0x1700117E RID: 4478
			// (get) Token: 0x06002F72 RID: 12146 RVA: 0x0009004B File Offset: 0x0008E24B
			public override bool SupportsNativeQuery
			{
				get
				{
					return this.supportsNativeQuery;
				}
			}

			// Token: 0x1700117F RID: 4479
			// (get) Token: 0x06002F73 RID: 12147 RVA: 0x00090053 File Offset: 0x0008E253
			public override bool TryRecoverDateDiff
			{
				get
				{
					return this.tryRecoverDateDiff;
				}
			}

			// Token: 0x17001180 RID: 4480
			// (get) Token: 0x06002F74 RID: 12148 RVA: 0x0009005B File Offset: 0x0008E25B
			public override bool TryRecoverCoalesce
			{
				get
				{
					return this.tryRecoverCoalesce;
				}
			}

			// Token: 0x040014DE RID: 5342
			private readonly string identifierQuoteChar;

			// Token: 0x040014DF RID: 5343
			private readonly bool useSchemaInDmlStatements;

			// Token: 0x040014E0 RID: 5344
			private readonly bool useCatalogInDmlStatements;

			// Token: 0x040014E1 RID: 5345
			private readonly string catalogNameSeparator;

			// Token: 0x040014E2 RID: 5346
			private readonly Odbc32.SQL_QL? catalogNameLocation;

			// Token: 0x040014E3 RID: 5347
			private readonly Odbc32.SQL_SC sql92Conformance;

			// Token: 0x040014E4 RID: 5348
			private readonly int maxColumnsInOrderBy;

			// Token: 0x040014E5 RID: 5349
			private readonly int maxIdentifierNameLength;

			// Token: 0x040014E6 RID: 5350
			private readonly int maxColumnsInGroupBy;

			// Token: 0x040014E7 RID: 5351
			private readonly int maxColumnsInSelect;

			// Token: 0x040014E8 RID: 5352
			private readonly int maxParameters;

			// Token: 0x040014E9 RID: 5353
			private readonly bool orderByColumnsInSelect;

			// Token: 0x040014EA RID: 5354
			private readonly string driverOdbcVersion;

			// Token: 0x040014EB RID: 5355
			private readonly Odbc32.SQL_FN_STR supportedStringFunctions;

			// Token: 0x040014EC RID: 5356
			private readonly Odbc32.SQL_AF supportedAggregateFunctions;

			// Token: 0x040014ED RID: 5357
			private readonly Odbc32.SQL_SP supportedPredicates;

			// Token: 0x040014EE RID: 5358
			private readonly Odbc32.SQL_SRJO supportedRelationalJoinOperators;

			// Token: 0x040014EF RID: 5359
			private readonly Odbc32.SQL_SVE supportedValueExpressions;

			// Token: 0x040014F0 RID: 5360
			private readonly bool supportedColumnAliases;

			// Token: 0x040014F1 RID: 5361
			private readonly Odbc32.SQL_GB groupByCapabilities;

			// Token: 0x040014F2 RID: 5362
			private readonly Odbc32.SQL_FUN_NUM supportedNumericFunctions;

			// Token: 0x040014F3 RID: 5363
			private readonly Odbc32.SQL_FN_TD supportedTimeDateFunctions;

			// Token: 0x040014F4 RID: 5364
			private readonly Odbc32.SQL_FN_SYSTEM supportedSystemFunctions;

			// Token: 0x040014F5 RID: 5365
			private readonly Odbc32.SQL_TSI supportedTimeDateAddIntervals;

			// Token: 0x040014F6 RID: 5366
			private readonly Odbc32.SQL_TSI supportedTimeDateDiffIntervals;

			// Token: 0x040014F7 RID: 5367
			private readonly Odbc32.SQL_CB? stringConcatNullBehavior;

			// Token: 0x040014F8 RID: 5368
			private readonly bool? catalogName;

			// Token: 0x040014F9 RID: 5369
			private readonly string catalogTerm;

			// Token: 0x040014FA RID: 5370
			private readonly string schemaTerm;

			// Token: 0x040014FB RID: 5371
			private readonly Odbc32.SQL_OIC interfaceConformanceLevel;

			// Token: 0x040014FC RID: 5372
			private readonly string searchPatternEscapeCharacter;

			// Token: 0x040014FD RID: 5373
			private readonly Odbc32.SQL_FN_CVT supportedConvertFunctions;

			// Token: 0x040014FE RID: 5374
			private readonly IDictionary<Odbc32.SQL_INFO, Odbc32.SQL_CVT> supportedConversions;

			// Token: 0x040014FF RID: 5375
			private readonly string identifierSpecialCharacters;

			// Token: 0x04001500 RID: 5376
			private readonly int? fractionalSecondsScale;

			// Token: 0x04001501 RID: 5377
			private readonly bool supportsOdbcNumericLiterals;

			// Token: 0x04001502 RID: 5378
			private readonly bool supportsOdbcStringLiterals;

			// Token: 0x04001503 RID: 5379
			private readonly IDictionary<string, string> stringLiteralEscapeCharaceters;

			// Token: 0x04001504 RID: 5380
			private readonly bool supportsOdbcDateLiterals;

			// Token: 0x04001505 RID: 5381
			private readonly bool supportsOdbcTimeLiterals;

			// Token: 0x04001506 RID: 5382
			private readonly bool supportsOdbcTimestampLiterals;

			// Token: 0x04001507 RID: 5383
			private readonly bool supportsTopOrLimit;

			// Token: 0x04001508 RID: 5384
			private readonly NativeQuerySchemaStrategy nativeQuerySchemaStrategy;

			// Token: 0x04001509 RID: 5385
			private readonly bool prepareStatements;

			// Token: 0x0400150A RID: 5386
			private readonly Odbc32.SQL_RETURN_ESCAPE_CLAUSE returnEscapeClause;

			// Token: 0x0400150B RID: 5387
			private readonly RecordValue defaultTypeParameters;

			// Token: 0x0400150C RID: 5388
			private readonly bool supportsNativeQuery;

			// Token: 0x0400150D RID: 5389
			private readonly bool tryRecoverDateDiff;

			// Token: 0x0400150E RID: 5390
			private readonly bool tryRecoverCoalesce;
		}

		// Token: 0x020005EA RID: 1514
		private sealed class OdbcConnectionDataSourceInfo : OdbcDataSourceInfo
		{
			// Token: 0x06002F75 RID: 12149 RVA: 0x00090063 File Offset: 0x0008E263
			public OdbcConnectionDataSourceInfo(IOdbcConnection connection)
			{
				this.connection = connection;
			}

			// Token: 0x17001181 RID: 4481
			// (get) Token: 0x06002F76 RID: 12150 RVA: 0x00090072 File Offset: 0x0008E272
			public override string IdentifierQuoteChar
			{
				get
				{
					return this.connection.GetInfoString(Odbc32.SQL_INFO.SQL_IDENTIFIER_QUOTE_CHAR);
				}
			}

			// Token: 0x17001182 RID: 4482
			// (get) Token: 0x06002F77 RID: 12151 RVA: 0x00090081 File Offset: 0x0008E281
			public override bool UseSchemaInDmlStatements
			{
				get
				{
					return this.GetBitmask(Odbc32.SQL_INFO.SQL_SCHEMA_USAGE, 1);
				}
			}

			// Token: 0x17001183 RID: 4483
			// (get) Token: 0x06002F78 RID: 12152 RVA: 0x0009008C File Offset: 0x0008E28C
			public override bool UseCatalogInDmlStatements
			{
				get
				{
					return this.GetBitmask(Odbc32.SQL_INFO.SQL_CATALOG_USAGE, 1);
				}
			}

			// Token: 0x17001184 RID: 4484
			// (get) Token: 0x06002F79 RID: 12153 RVA: 0x00090097 File Offset: 0x0008E297
			public override string CatalogNameSeparator
			{
				get
				{
					return this.GetStringOrDefault(Odbc32.SQL_INFO.SQL_CATALOG_NAME_SEPARATOR, null);
				}
			}

			// Token: 0x17001185 RID: 4485
			// (get) Token: 0x06002F7A RID: 12154 RVA: 0x000900A4 File Offset: 0x0008E2A4
			public override Odbc32.SQL_QL? CatalogNameLocation
			{
				get
				{
					int? int32OrNull = this.GetInt32OrNull(Odbc32.SQL_INFO.SQL_CATALOG_LOCATION);
					if (int32OrNull == null)
					{
						return null;
					}
					return new Odbc32.SQL_QL?((Odbc32.SQL_QL)int32OrNull.GetValueOrDefault());
				}
			}

			// Token: 0x17001186 RID: 4486
			// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000900D9 File Offset: 0x0008E2D9
			public override Odbc32.SQL_SC Sql92Conformance
			{
				get
				{
					return (Odbc32.SQL_SC)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_SQL_CONFORMANCE, 0);
				}
			}

			// Token: 0x17001187 RID: 4487
			// (get) Token: 0x06002F7C RID: 12156 RVA: 0x000900E4 File Offset: 0x0008E2E4
			public override int MaxColumnsInOrderBy
			{
				get
				{
					return this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_ORDER_BY, 0);
				}
			}

			// Token: 0x17001188 RID: 4488
			// (get) Token: 0x06002F7D RID: 12157 RVA: 0x000900EF File Offset: 0x0008E2EF
			public override int MaxIdentifierNameLength
			{
				get
				{
					return this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_MAX_IDENTIFIER_LEN, 0);
				}
			}

			// Token: 0x17001189 RID: 4489
			// (get) Token: 0x06002F7E RID: 12158 RVA: 0x000900FD File Offset: 0x0008E2FD
			public override bool OrderByColumnsInSelect
			{
				get
				{
					return this.GetYesNo(Odbc32.SQL_INFO.SQL_ORDER_BY_COLUMNS_IN_SELECT);
				}
			}

			// Token: 0x1700118A RID: 4490
			// (get) Token: 0x06002F7F RID: 12159 RVA: 0x00090107 File Offset: 0x0008E307
			public override string DriverOdbcVersion
			{
				get
				{
					return this.connection.GetInfoString(Odbc32.SQL_INFO.SQL_DRIVER_ODBC_VER);
				}
			}

			// Token: 0x1700118B RID: 4491
			// (get) Token: 0x06002F80 RID: 12160 RVA: 0x00090116 File Offset: 0x0008E316
			public override Odbc32.SQL_FN_STR SupportedStringFunctions
			{
				get
				{
					return (Odbc32.SQL_FN_STR)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_STRING_FUNCTIONS, 0);
				}
			}

			// Token: 0x1700118C RID: 4492
			// (get) Token: 0x06002F81 RID: 12161 RVA: 0x00090121 File Offset: 0x0008E321
			public override Odbc32.SQL_AF SupportedAggregateFunctions
			{
				get
				{
					return (Odbc32.SQL_AF)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_AGGREGATE_FUNCTIONS, 0);
				}
			}

			// Token: 0x1700118D RID: 4493
			// (get) Token: 0x06002F82 RID: 12162 RVA: 0x0009012F File Offset: 0x0008E32F
			public override Odbc32.SQL_SP SupportedPredicates
			{
				get
				{
					return (Odbc32.SQL_SP)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_SQL92_PREDICATES, 0);
				}
			}

			// Token: 0x1700118E RID: 4494
			// (get) Token: 0x06002F83 RID: 12163 RVA: 0x0009013D File Offset: 0x0008E33D
			public override Odbc32.SQL_SRJO SupportedSql92RelationalJoinOperators
			{
				get
				{
					return (Odbc32.SQL_SRJO)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_SQL92_RELATIONAL_JOIN_OPERATORS, 0);
				}
			}

			// Token: 0x1700118F RID: 4495
			// (get) Token: 0x06002F84 RID: 12164 RVA: 0x0009014B File Offset: 0x0008E34B
			public override Odbc32.SQL_SVE SupportedValueExpressions
			{
				get
				{
					return (Odbc32.SQL_SVE)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_SQL92_VALUE_EXPRESSIONS, 0);
				}
			}

			// Token: 0x17001190 RID: 4496
			// (get) Token: 0x06002F85 RID: 12165 RVA: 0x00090159 File Offset: 0x0008E359
			public override bool SupportsColumnAliases
			{
				get
				{
					return this.GetYesNo(Odbc32.SQL_INFO.SQL_COLUMN_ALIAS);
				}
			}

			// Token: 0x17001191 RID: 4497
			// (get) Token: 0x06002F86 RID: 12166 RVA: 0x00090163 File Offset: 0x0008E363
			public override int MaxColumnsInGroupBy
			{
				get
				{
					return this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_GROUP_BY, 0);
				}
			}

			// Token: 0x17001192 RID: 4498
			// (get) Token: 0x06002F87 RID: 12167 RVA: 0x0009016E File Offset: 0x0008E36E
			public override int MaxColumnsInSelect
			{
				get
				{
					return this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_MAX_COLUMNS_IN_SELECT, 0);
				}
			}

			// Token: 0x17001193 RID: 4499
			// (get) Token: 0x06002F88 RID: 12168 RVA: 0x00090179 File Offset: 0x0008E379
			public override int MaxParameters
			{
				get
				{
					return 2100;
				}
			}

			// Token: 0x17001194 RID: 4500
			// (get) Token: 0x06002F89 RID: 12169 RVA: 0x00090180 File Offset: 0x0008E380
			public override Odbc32.SQL_GB GroupByCapabilities
			{
				get
				{
					return (Odbc32.SQL_GB)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_GROUP_BY, 1);
				}
			}

			// Token: 0x17001195 RID: 4501
			// (get) Token: 0x06002F8A RID: 12170 RVA: 0x0009018B File Offset: 0x0008E38B
			public override Odbc32.SQL_FUN_NUM SupportedNumericFunctions
			{
				get
				{
					return (Odbc32.SQL_FUN_NUM)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_NUMERIC_FUNCTIONS, 0);
				}
			}

			// Token: 0x17001196 RID: 4502
			// (get) Token: 0x06002F8B RID: 12171 RVA: 0x00090196 File Offset: 0x0008E396
			public override Odbc32.SQL_FN_TD SupportedTimeDateFunctions
			{
				get
				{
					return (Odbc32.SQL_FN_TD)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_TIMEDATE_FUNCTIONS, 0);
				}
			}

			// Token: 0x17001197 RID: 4503
			// (get) Token: 0x06002F8C RID: 12172 RVA: 0x000901A1 File Offset: 0x0008E3A1
			public override Odbc32.SQL_FN_SYSTEM SupportedSystemFunctions
			{
				get
				{
					return (Odbc32.SQL_FN_SYSTEM)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_SYSTEM_FUNCTIONS, 0);
				}
			}

			// Token: 0x17001198 RID: 4504
			// (get) Token: 0x06002F8D RID: 12173 RVA: 0x000901AC File Offset: 0x0008E3AC
			public override Odbc32.SQL_TSI SupportedTimeDateAddIntervals
			{
				get
				{
					return (Odbc32.SQL_TSI)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_TIMEDATE_ADD_INTERVALS, 0);
				}
			}

			// Token: 0x17001199 RID: 4505
			// (get) Token: 0x06002F8E RID: 12174 RVA: 0x000901B7 File Offset: 0x0008E3B7
			public override Odbc32.SQL_TSI SupportedTimeDateDiffIntervals
			{
				get
				{
					return (Odbc32.SQL_TSI)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_TIMEDATE_DIFF_INTERVALS, 0);
				}
			}

			// Token: 0x1700119A RID: 4506
			// (get) Token: 0x06002F8F RID: 12175 RVA: 0x000901C4 File Offset: 0x0008E3C4
			public override Odbc32.SQL_CB? StringConcatNullBehavior
			{
				get
				{
					int? int32OrNull = this.GetInt32OrNull(Odbc32.SQL_INFO.SQL_CONCAT_NULL_BEHAVIOR);
					if (int32OrNull == null)
					{
						return null;
					}
					return new Odbc32.SQL_CB?((Odbc32.SQL_CB)int32OrNull.GetValueOrDefault());
				}
			}

			// Token: 0x1700119B RID: 4507
			// (get) Token: 0x06002F90 RID: 12176 RVA: 0x000901F9 File Offset: 0x0008E3F9
			public override bool? CatalogName
			{
				get
				{
					return this.GetYesNoOrNull(Odbc32.SQL_INFO.SQL_CATALOG_NAME);
				}
			}

			// Token: 0x1700119C RID: 4508
			// (get) Token: 0x06002F91 RID: 12177 RVA: 0x00090206 File Offset: 0x0008E406
			public override string CatalogTerm
			{
				get
				{
					return this.GetStringOrDefault(Odbc32.SQL_INFO.SQL_CATALOG_TERM, null);
				}
			}

			// Token: 0x1700119D RID: 4509
			// (get) Token: 0x06002F92 RID: 12178 RVA: 0x00090211 File Offset: 0x0008E411
			public override string SchemaTerm
			{
				get
				{
					return this.GetStringOrDefault(Odbc32.SQL_INFO.SQL_SCHEMA_TERM, null);
				}
			}

			// Token: 0x1700119E RID: 4510
			// (get) Token: 0x06002F93 RID: 12179 RVA: 0x0009021C File Offset: 0x0008E41C
			public override Odbc32.SQL_OIC InterfaceConformanceLevel
			{
				get
				{
					return (Odbc32.SQL_OIC)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_ODBC_INTERFACE_CONFORMANCE, 1);
				}
			}

			// Token: 0x1700119F RID: 4511
			// (get) Token: 0x06002F94 RID: 12180 RVA: 0x0009022A File Offset: 0x0008E42A
			public override string SearchPatternEscapeCharacter
			{
				get
				{
					return this.connection.GetInfoString(Odbc32.SQL_INFO.SQL_SEARCH_PATTERN_ESCAPE);
				}
			}

			// Token: 0x170011A0 RID: 4512
			// (get) Token: 0x06002F95 RID: 12181 RVA: 0x00090239 File Offset: 0x0008E439
			public override Odbc32.SQL_FN_CVT SupportedConvertFunctions
			{
				get
				{
					return (Odbc32.SQL_FN_CVT)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_CONVERT_FUNCTIONS, 0);
				}
			}

			// Token: 0x170011A1 RID: 4513
			// (get) Token: 0x06002F96 RID: 12182 RVA: 0x00090244 File Offset: 0x0008E444
			public override string IdentifierSpecialCharacters
			{
				get
				{
					return this.GetStringOrDefault(Odbc32.SQL_INFO.SQL_SPECIAL_CHARACTERS, null);
				}
			}

			// Token: 0x170011A2 RID: 4514
			// (get) Token: 0x06002F97 RID: 12183 RVA: 0x00090250 File Offset: 0x0008E450
			public override int? FractionalSecondsScale
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170011A3 RID: 4515
			// (get) Token: 0x06002F98 RID: 12184 RVA: 0x00002105 File Offset: 0x00000305
			public override bool SupportsNumericLiterals
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011A4 RID: 4516
			// (get) Token: 0x06002F99 RID: 12185 RVA: 0x00002105 File Offset: 0x00000305
			public override bool SupportsStringLiterals
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011A5 RID: 4517
			// (get) Token: 0x06002F9A RID: 12186 RVA: 0x00002105 File Offset: 0x00000305
			public override bool SupportsOdbcDateLiterals
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011A6 RID: 4518
			// (get) Token: 0x06002F9B RID: 12187 RVA: 0x00002105 File Offset: 0x00000305
			public override bool SupportsOdbcTimeLiterals
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011A7 RID: 4519
			// (get) Token: 0x06002F9C RID: 12188 RVA: 0x00002105 File Offset: 0x00000305
			public override bool SupportsOdbcTimestampLiterals
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011A8 RID: 4520
			// (get) Token: 0x06002F9D RID: 12189 RVA: 0x00002105 File Offset: 0x00000305
			public override bool SupportsTopOrLimit
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011A9 RID: 4521
			// (get) Token: 0x06002F9E RID: 12190 RVA: 0x00002105 File Offset: 0x00000305
			public override NativeQuerySchemaStrategy NativeQuerySchemaStrategy
			{
				get
				{
					return NativeQuerySchemaStrategy.TopOne;
				}
			}

			// Token: 0x170011AA RID: 4522
			// (get) Token: 0x06002F9F RID: 12191 RVA: 0x00002105 File Offset: 0x00000305
			public override bool PrepareStatements
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011AB RID: 4523
			// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x00090266 File Offset: 0x0008E466
			public override Odbc32.SQL_RETURN_ESCAPE_CLAUSE ReturnEscapeClause
			{
				get
				{
					return (Odbc32.SQL_RETURN_ESCAPE_CLAUSE)this.GetInt32OrDefault(Odbc32.SQL_INFO.SQL_RETURN_ESCAPE_CLAUSE, 0);
				}
			}

			// Token: 0x170011AC RID: 4524
			// (get) Token: 0x06002FA1 RID: 12193 RVA: 0x00090274 File Offset: 0x0008E474
			public override IDictionary<string, string> StringLiteralEscapeCharacters
			{
				get
				{
					return new Dictionary<string, string>();
				}
			}

			// Token: 0x170011AD RID: 4525
			// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x0009027C File Offset: 0x0008E47C
			public override IDictionary<Odbc32.SQL_INFO, Odbc32.SQL_CVT> SupportedConversions
			{
				get
				{
					Dictionary<Odbc32.SQL_INFO, Odbc32.SQL_CVT> dictionary = new Dictionary<Odbc32.SQL_INFO, Odbc32.SQL_CVT>();
					foreach (OdbcTypeMap odbcTypeMap in OdbcTypeMap.All)
					{
						if (odbcTypeMap.ConvertInfoType != null && odbcTypeMap.ConvertBitmask != null && !dictionary.ContainsKey(odbcTypeMap.ConvertInfoType.Value))
						{
							Odbc32.SQL_CVT int32OrDefault = (Odbc32.SQL_CVT)this.GetInt32OrDefault(odbcTypeMap.ConvertInfoType.Value, 0);
							dictionary[odbcTypeMap.ConvertInfoType.Value] = int32OrDefault;
						}
					}
					return dictionary;
				}
			}

			// Token: 0x06002FA3 RID: 12195 RVA: 0x000902FC File Offset: 0x0008E4FC
			private bool GetYesNo(Odbc32.SQL_INFO infoType)
			{
				bool flag;
				try
				{
					flag = this.connection.GetInfoString(infoType) == "Y";
				}
				catch (OdbcException ex)
				{
					if (OdbcDataSourceInfo.requiredSqlInfo.Contains(infoType) || !ex.IsSafe)
					{
						throw;
					}
					flag = false;
				}
				return flag;
			}

			// Token: 0x06002FA4 RID: 12196 RVA: 0x00090350 File Offset: 0x0008E550
			private bool? GetYesNoOrNull(Odbc32.SQL_INFO infoType)
			{
				bool? flag;
				try
				{
					flag = new bool?(this.connection.GetInfoString(infoType) == "Y");
				}
				catch (OdbcException ex)
				{
					if (OdbcDataSourceInfo.requiredSqlInfo.Contains(infoType) || !ex.IsSafe)
					{
						throw;
					}
					flag = null;
				}
				return flag;
			}

			// Token: 0x06002FA5 RID: 12197 RVA: 0x000903B0 File Offset: 0x0008E5B0
			private bool GetBitmask(Odbc32.SQL_INFO infoType, int bitmask)
			{
				bool flag;
				try
				{
					flag = (this.connection.GetInfoInt32(infoType) & bitmask) != 0;
				}
				catch (OdbcException ex)
				{
					if (OdbcDataSourceInfo.requiredSqlInfo.Contains(infoType) || !ex.IsSafe)
					{
						throw;
					}
					flag = false;
				}
				return flag;
			}

			// Token: 0x06002FA6 RID: 12198 RVA: 0x00090400 File Offset: 0x0008E600
			private string GetStringOrDefault(Odbc32.SQL_INFO infoType, string defaultValue)
			{
				string text;
				try
				{
					text = this.connection.GetInfoString(infoType);
				}
				catch (OdbcException ex)
				{
					if (OdbcDataSourceInfo.requiredSqlInfo.Contains(infoType) || !ex.IsSafe)
					{
						throw;
					}
					text = defaultValue;
				}
				return text;
			}

			// Token: 0x06002FA7 RID: 12199 RVA: 0x0009044C File Offset: 0x0008E64C
			private int GetInt32OrDefault(Odbc32.SQL_INFO infoType, int defaultValue)
			{
				int num;
				try
				{
					num = this.connection.GetInfoInt32(infoType);
				}
				catch (OdbcException ex)
				{
					if (OdbcDataSourceInfo.requiredSqlInfo.Contains(infoType) || !ex.IsSafe)
					{
						throw;
					}
					num = defaultValue;
				}
				return num;
			}

			// Token: 0x06002FA8 RID: 12200 RVA: 0x00090498 File Offset: 0x0008E698
			private int? GetInt32OrNull(Odbc32.SQL_INFO infoType)
			{
				int? num;
				try
				{
					num = new int?(this.connection.GetInfoInt32(infoType));
				}
				catch (OdbcException ex)
				{
					if (OdbcDataSourceInfo.requiredSqlInfo.Contains(infoType) || !ex.IsSafe)
					{
						throw;
					}
					num = null;
				}
				return num;
			}

			// Token: 0x170011AE RID: 4526
			// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x00019E61 File Offset: 0x00018061
			public override RecordValue DefaultTypeParameters
			{
				get
				{
					return RecordValue.Empty;
				}
			}

			// Token: 0x170011AF RID: 4527
			// (get) Token: 0x06002FAA RID: 12202 RVA: 0x00002105 File Offset: 0x00000305
			public override bool SupportsNativeQuery
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011B0 RID: 4528
			// (get) Token: 0x06002FAB RID: 12203 RVA: 0x00002105 File Offset: 0x00000305
			public override bool TryRecoverDateDiff
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170011B1 RID: 4529
			// (get) Token: 0x06002FAC RID: 12204 RVA: 0x00002105 File Offset: 0x00000305
			public override bool TryRecoverCoalesce
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400150F RID: 5391
			private const int supportedParameterCount = 2100;

			// Token: 0x04001510 RID: 5392
			private readonly IOdbcConnection connection;
		}

		// Token: 0x020005EB RID: 1515
		private sealed class OverriddenDataSourceInfo : DelegatingOdbcDataSourceInfo
		{
			// Token: 0x06002FAD RID: 12205 RVA: 0x000904F0 File Offset: 0x0008E6F0
			public OverriddenDataSourceInfo(OdbcDataSourceInfo baseInfo, OdbcOptions options)
				: base(baseInfo)
			{
				this.options = options;
			}

			// Token: 0x170011B2 RID: 4530
			// (get) Token: 0x06002FAE RID: 12206 RVA: 0x00090500 File Offset: 0x0008E700
			public override int? FractionalSecondsScale
			{
				get
				{
					return this.options.FractionalSecondsScale;
				}
			}

			// Token: 0x170011B3 RID: 4531
			// (get) Token: 0x06002FAF RID: 12207 RVA: 0x0009050D File Offset: 0x0008E70D
			public override bool SupportsNumericLiterals
			{
				get
				{
					return this.options.SupportsNumericLiterals;
				}
			}

			// Token: 0x170011B4 RID: 4532
			// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x0009051A File Offset: 0x0008E71A
			public override bool SupportsStringLiterals
			{
				get
				{
					return this.options.SupportsStringLiterals;
				}
			}

			// Token: 0x170011B5 RID: 4533
			// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x00090527 File Offset: 0x0008E727
			public override IDictionary<string, string> StringLiteralEscapeCharacters
			{
				get
				{
					return this.options.StringLiteralEscapeCharacters;
				}
			}

			// Token: 0x170011B6 RID: 4534
			// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x00090534 File Offset: 0x0008E734
			public override bool SupportsNativeQuery
			{
				get
				{
					return this.options.Sql92Translation == "PassThrough";
				}
			}

			// Token: 0x170011B7 RID: 4535
			// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x0009054B File Offset: 0x0008E74B
			public override bool SupportsDerivedTable
			{
				get
				{
					return this.Supports(Odbc32.SQL_SC.SQL_SC_SQL92_FULL) || this.options.SupportsDerivedTable;
				}
			}

			// Token: 0x170011B8 RID: 4536
			// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x00090564 File Offset: 0x0008E764
			public override Odbc32.SQL_SC Sql92Conformance
			{
				get
				{
					Odbc32.SQL_SC? sql92Conformance = this.options.Sql92Conformance;
					if (sql92Conformance == null)
					{
						return base.Sql92Conformance;
					}
					return sql92Conformance.GetValueOrDefault();
				}
			}

			// Token: 0x170011B9 RID: 4537
			// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x00090594 File Offset: 0x0008E794
			public override Odbc32.SQL_SP SupportedPredicates
			{
				get
				{
					Odbc32.SQL_SP? supportedPredicates = this.options.SupportedPredicates;
					if (supportedPredicates == null)
					{
						return base.SupportedPredicates;
					}
					return supportedPredicates.GetValueOrDefault();
				}
			}

			// Token: 0x170011BA RID: 4538
			// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000905C4 File Offset: 0x0008E7C4
			public override Odbc32.SQL_AF SupportedAggregateFunctions
			{
				get
				{
					Odbc32.SQL_AF? supportedAggregateFunctions = this.options.SupportedAggregateFunctions;
					if (supportedAggregateFunctions == null)
					{
						return base.SupportedAggregateFunctions;
					}
					return supportedAggregateFunctions.GetValueOrDefault();
				}
			}

			// Token: 0x170011BB RID: 4539
			// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000905F4 File Offset: 0x0008E7F4
			public override Odbc32.SQL_GB GroupByCapabilities
			{
				get
				{
					Odbc32.SQL_GB? groupByCapabilities = this.options.GroupByCapabilities;
					if (groupByCapabilities == null)
					{
						return base.GroupByCapabilities;
					}
					return groupByCapabilities.GetValueOrDefault();
				}
			}

			// Token: 0x170011BC RID: 4540
			// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x00090624 File Offset: 0x0008E824
			public override Odbc32.SQL_SRJO SupportedSql92RelationalJoinOperators
			{
				get
				{
					Odbc32.SQL_SRJO? supportedSql92Joins = this.options.SupportedSql92Joins;
					if (supportedSql92Joins == null)
					{
						return base.SupportedSql92RelationalJoinOperators;
					}
					return supportedSql92Joins.GetValueOrDefault();
				}
			}

			// Token: 0x170011BD RID: 4541
			// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x00090654 File Offset: 0x0008E854
			public override Odbc32.SQL_SVE SupportedValueExpressions
			{
				get
				{
					Odbc32.SQL_SVE? supportedValueExpressions = this.options.SupportedValueExpressions;
					if (supportedValueExpressions == null)
					{
						return base.SupportedValueExpressions;
					}
					return supportedValueExpressions.GetValueOrDefault();
				}
			}

			// Token: 0x170011BE RID: 4542
			// (get) Token: 0x06002FBA RID: 12218 RVA: 0x00090684 File Offset: 0x0008E884
			public override bool SupportsOdbcDateLiterals
			{
				get
				{
					return this.options.SupportsOdbcDateLiterals || base.SupportsOdbcDateLiterals;
				}
			}

			// Token: 0x170011BF RID: 4543
			// (get) Token: 0x06002FBB RID: 12219 RVA: 0x0009069B File Offset: 0x0008E89B
			public override bool SupportsOdbcTimeLiterals
			{
				get
				{
					return this.options.SupportsOdbcTimeLiterals || base.SupportsOdbcTimeLiterals;
				}
			}

			// Token: 0x170011C0 RID: 4544
			// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000906B2 File Offset: 0x0008E8B2
			public override bool SupportsOdbcTimestampLiterals
			{
				get
				{
					return this.options.SupportsOdbcTimestampLiterals || base.SupportsOdbcTimestampLiterals;
				}
			}

			// Token: 0x170011C1 RID: 4545
			// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000906C9 File Offset: 0x0008E8C9
			public override bool SupportsTopOrLimit
			{
				get
				{
					return this.options.SupportsTop || this.options.AstVisitor.Keys.Contains("LimitClause") || this.options.LimitClauseKind != LimitClauseKind.LimitClauseKindType.None || base.SupportsTopOrLimit;
				}
			}

			// Token: 0x170011C2 RID: 4546
			// (get) Token: 0x06002FBE RID: 12222 RVA: 0x0009070C File Offset: 0x0008E90C
			public override NativeQuerySchemaStrategy NativeQuerySchemaStrategy
			{
				get
				{
					string nativeQuerySchemaStrategy = this.options.NativeQuerySchemaStrategy;
					if (nativeQuerySchemaStrategy != null)
					{
						if (nativeQuerySchemaStrategy == "TopZero")
						{
							return NativeQuerySchemaStrategy.TopZero;
						}
						if (nativeQuerySchemaStrategy == "TopOne")
						{
							return NativeQuerySchemaStrategy.TopOne;
						}
						if (!(nativeQuerySchemaStrategy == "WhereZeroEqualsOne"))
						{
							throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue("NativeQuerySchemaStrategy"), TextValue.New(this.options.NativeQuerySchemaStrategy), null);
						}
						return NativeQuerySchemaStrategy.WhereZeroEqualsOne;
					}
					else
					{
						if (!this.options.SupportsLimitZero)
						{
							return NativeQuerySchemaStrategy.TopOne;
						}
						return NativeQuerySchemaStrategy.TopZero;
					}
				}
			}

			// Token: 0x170011C3 RID: 4547
			// (get) Token: 0x06002FBF RID: 12223 RVA: 0x00090788 File Offset: 0x0008E988
			public override bool PrepareStatements
			{
				get
				{
					return this.options.PrepareStatements;
				}
			}

			// Token: 0x170011C4 RID: 4548
			// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x00090798 File Offset: 0x0008E998
			public override Odbc32.SQL_RETURN_ESCAPE_CLAUSE ReturnEscapeClause
			{
				get
				{
					Odbc32.SQL_RETURN_ESCAPE_CLAUSE? returnEscapeClause = this.options.ReturnEscapeClause;
					if (returnEscapeClause == null)
					{
						return base.ReturnEscapeClause;
					}
					return returnEscapeClause.GetValueOrDefault();
				}
			}

			// Token: 0x170011C5 RID: 4549
			// (get) Token: 0x06002FC1 RID: 12225 RVA: 0x000907C8 File Offset: 0x0008E9C8
			public override RecordValue DefaultTypeParameters
			{
				get
				{
					return this.options.DefaultTypeParameters;
				}
			}

			// Token: 0x170011C6 RID: 4550
			// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000907D5 File Offset: 0x0008E9D5
			public override bool TryRecoverDateDiff
			{
				get
				{
					return this.options.TryRecoverDateDiff;
				}
			}

			// Token: 0x170011C7 RID: 4551
			// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000907E2 File Offset: 0x0008E9E2
			public override bool TryRecoverCoalesce
			{
				get
				{
					return this.options.TryRecoverCoalesce;
				}
			}

			// Token: 0x04001511 RID: 5393
			private readonly OdbcOptions options;
		}
	}
}
