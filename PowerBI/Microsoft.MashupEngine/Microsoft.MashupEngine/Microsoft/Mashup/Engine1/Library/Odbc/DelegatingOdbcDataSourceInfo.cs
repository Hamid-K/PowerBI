using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200059E RID: 1438
	internal class DelegatingOdbcDataSourceInfo : OdbcDataSourceInfo
	{
		// Token: 0x06002D56 RID: 11606 RVA: 0x0008A438 File Offset: 0x00088638
		public DelegatingOdbcDataSourceInfo(OdbcDataSourceInfo baseInfo)
		{
			this.baseInfo = baseInfo;
		}

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x06002D57 RID: 11607 RVA: 0x0008A447 File Offset: 0x00088647
		public override string IdentifierQuoteChar
		{
			get
			{
				return this.baseInfo.IdentifierQuoteChar;
			}
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06002D58 RID: 11608 RVA: 0x0008A454 File Offset: 0x00088654
		public override bool UseSchemaInDmlStatements
		{
			get
			{
				return this.baseInfo.UseSchemaInDmlStatements;
			}
		}

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x0008A461 File Offset: 0x00088661
		public override bool UseCatalogInDmlStatements
		{
			get
			{
				return this.baseInfo.UseCatalogInDmlStatements;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x0008A46E File Offset: 0x0008866E
		public override string CatalogNameSeparator
		{
			get
			{
				return this.baseInfo.CatalogNameSeparator;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x0008A47B File Offset: 0x0008867B
		public override Odbc32.SQL_QL? CatalogNameLocation
		{
			get
			{
				return this.baseInfo.CatalogNameLocation;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x0008A488 File Offset: 0x00088688
		public override int MaxColumnsInOrderBy
		{
			get
			{
				return this.baseInfo.MaxColumnsInOrderBy;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x0008A495 File Offset: 0x00088695
		public override int MaxIdentifierNameLength
		{
			get
			{
				return this.baseInfo.MaxIdentifierNameLength;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x0008A4A2 File Offset: 0x000886A2
		public override int MaxColumnsInGroupBy
		{
			get
			{
				return this.baseInfo.MaxColumnsInGroupBy;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x0008A4AF File Offset: 0x000886AF
		public override int MaxColumnsInSelect
		{
			get
			{
				return this.baseInfo.MaxColumnsInSelect;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06002D60 RID: 11616 RVA: 0x0008A4BC File Offset: 0x000886BC
		public override int MaxParameters
		{
			get
			{
				return this.baseInfo.MaxParameters;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x0008A4C9 File Offset: 0x000886C9
		public override bool OrderByColumnsInSelect
		{
			get
			{
				return this.baseInfo.OrderByColumnsInSelect;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06002D62 RID: 11618 RVA: 0x0008A4D6 File Offset: 0x000886D6
		public override string DriverOdbcVersion
		{
			get
			{
				return this.baseInfo.DriverOdbcVersion;
			}
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x0008A4E3 File Offset: 0x000886E3
		public override Odbc32.SQL_FN_STR SupportedStringFunctions
		{
			get
			{
				return this.baseInfo.SupportedStringFunctions;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x0008A4F0 File Offset: 0x000886F0
		public override bool SupportsColumnAliases
		{
			get
			{
				return this.baseInfo.SupportsColumnAliases;
			}
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x0008A4FD File Offset: 0x000886FD
		public override Odbc32.SQL_FUN_NUM SupportedNumericFunctions
		{
			get
			{
				return this.baseInfo.SupportedNumericFunctions;
			}
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06002D66 RID: 11622 RVA: 0x0008A50A File Offset: 0x0008870A
		public override Odbc32.SQL_FN_TD SupportedTimeDateFunctions
		{
			get
			{
				return this.baseInfo.SupportedTimeDateFunctions;
			}
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x0008A517 File Offset: 0x00088717
		public override Odbc32.SQL_FN_SYSTEM SupportedSystemFunctions
		{
			get
			{
				return this.baseInfo.SupportedSystemFunctions;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06002D68 RID: 11624 RVA: 0x0008A524 File Offset: 0x00088724
		public override Odbc32.SQL_TSI SupportedTimeDateAddIntervals
		{
			get
			{
				return this.baseInfo.SupportedTimeDateAddIntervals;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06002D69 RID: 11625 RVA: 0x0008A531 File Offset: 0x00088731
		public override Odbc32.SQL_TSI SupportedTimeDateDiffIntervals
		{
			get
			{
				return this.baseInfo.SupportedTimeDateDiffIntervals;
			}
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06002D6A RID: 11626 RVA: 0x0008A53E File Offset: 0x0008873E
		public override Odbc32.SQL_CB? StringConcatNullBehavior
		{
			get
			{
				return this.baseInfo.StringConcatNullBehavior;
			}
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06002D6B RID: 11627 RVA: 0x0008A54B File Offset: 0x0008874B
		public override bool? CatalogName
		{
			get
			{
				return this.baseInfo.CatalogName;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06002D6C RID: 11628 RVA: 0x0008A558 File Offset: 0x00088758
		public override string CatalogTerm
		{
			get
			{
				return this.baseInfo.CatalogTerm;
			}
		}

		// Token: 0x170010BB RID: 4283
		// (get) Token: 0x06002D6D RID: 11629 RVA: 0x0008A565 File Offset: 0x00088765
		public override string SchemaTerm
		{
			get
			{
				return this.baseInfo.SchemaTerm;
			}
		}

		// Token: 0x170010BC RID: 4284
		// (get) Token: 0x06002D6E RID: 11630 RVA: 0x0008A572 File Offset: 0x00088772
		public override Odbc32.SQL_OIC InterfaceConformanceLevel
		{
			get
			{
				return this.baseInfo.InterfaceConformanceLevel;
			}
		}

		// Token: 0x170010BD RID: 4285
		// (get) Token: 0x06002D6F RID: 11631 RVA: 0x0008A57F File Offset: 0x0008877F
		public override string SearchPatternEscapeCharacter
		{
			get
			{
				return this.baseInfo.SearchPatternEscapeCharacter;
			}
		}

		// Token: 0x170010BE RID: 4286
		// (get) Token: 0x06002D70 RID: 11632 RVA: 0x0008A58C File Offset: 0x0008878C
		public override Odbc32.SQL_FN_CVT SupportedConvertFunctions
		{
			get
			{
				return this.baseInfo.SupportedConvertFunctions;
			}
		}

		// Token: 0x170010BF RID: 4287
		// (get) Token: 0x06002D71 RID: 11633 RVA: 0x0008A599 File Offset: 0x00088799
		public override IDictionary<Odbc32.SQL_INFO, Odbc32.SQL_CVT> SupportedConversions
		{
			get
			{
				return this.baseInfo.SupportedConversions;
			}
		}

		// Token: 0x170010C0 RID: 4288
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x0008A5A6 File Offset: 0x000887A6
		public override string IdentifierSpecialCharacters
		{
			get
			{
				return this.baseInfo.IdentifierSpecialCharacters;
			}
		}

		// Token: 0x170010C1 RID: 4289
		// (get) Token: 0x06002D73 RID: 11635 RVA: 0x0008A5B3 File Offset: 0x000887B3
		public override int? FractionalSecondsScale
		{
			get
			{
				return this.baseInfo.FractionalSecondsScale;
			}
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x06002D74 RID: 11636 RVA: 0x0008A5C0 File Offset: 0x000887C0
		public override bool SupportsNumericLiterals
		{
			get
			{
				return this.baseInfo.SupportsNumericLiterals;
			}
		}

		// Token: 0x170010C3 RID: 4291
		// (get) Token: 0x06002D75 RID: 11637 RVA: 0x0008A5CD File Offset: 0x000887CD
		public override bool SupportsStringLiterals
		{
			get
			{
				return this.baseInfo.SupportsStringLiterals;
			}
		}

		// Token: 0x170010C4 RID: 4292
		// (get) Token: 0x06002D76 RID: 11638 RVA: 0x0008A5DA File Offset: 0x000887DA
		public override IDictionary<string, string> StringLiteralEscapeCharacters
		{
			get
			{
				return this.baseInfo.StringLiteralEscapeCharacters;
			}
		}

		// Token: 0x170010C5 RID: 4293
		// (get) Token: 0x06002D77 RID: 11639 RVA: 0x0008A5E7 File Offset: 0x000887E7
		public override bool SupportsDerivedTable
		{
			get
			{
				return base.SupportsDerivedTable;
			}
		}

		// Token: 0x170010C6 RID: 4294
		// (get) Token: 0x06002D78 RID: 11640 RVA: 0x0008A5EF File Offset: 0x000887EF
		public override Odbc32.SQL_SC Sql92Conformance
		{
			get
			{
				return this.baseInfo.Sql92Conformance;
			}
		}

		// Token: 0x170010C7 RID: 4295
		// (get) Token: 0x06002D79 RID: 11641 RVA: 0x0008A5FC File Offset: 0x000887FC
		public override Odbc32.SQL_SP SupportedPredicates
		{
			get
			{
				return this.baseInfo.SupportedPredicates;
			}
		}

		// Token: 0x170010C8 RID: 4296
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x0008A609 File Offset: 0x00088809
		public override Odbc32.SQL_AF SupportedAggregateFunctions
		{
			get
			{
				return this.baseInfo.SupportedAggregateFunctions;
			}
		}

		// Token: 0x170010C9 RID: 4297
		// (get) Token: 0x06002D7B RID: 11643 RVA: 0x0008A616 File Offset: 0x00088816
		public override Odbc32.SQL_GB GroupByCapabilities
		{
			get
			{
				return this.baseInfo.GroupByCapabilities;
			}
		}

		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06002D7C RID: 11644 RVA: 0x0008A623 File Offset: 0x00088823
		public override Odbc32.SQL_SRJO SupportedSql92RelationalJoinOperators
		{
			get
			{
				return this.baseInfo.SupportedSql92RelationalJoinOperators;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06002D7D RID: 11645 RVA: 0x0008A630 File Offset: 0x00088830
		public override Odbc32.SQL_SVE SupportedValueExpressions
		{
			get
			{
				return this.baseInfo.SupportedValueExpressions;
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06002D7E RID: 11646 RVA: 0x0008A63D File Offset: 0x0008883D
		public override bool SupportsOdbcDateLiterals
		{
			get
			{
				return this.baseInfo.SupportsOdbcDateLiterals;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06002D7F RID: 11647 RVA: 0x0008A64A File Offset: 0x0008884A
		public override bool SupportsOdbcTimeLiterals
		{
			get
			{
				return this.baseInfo.SupportsOdbcTimeLiterals;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06002D80 RID: 11648 RVA: 0x0008A657 File Offset: 0x00088857
		public override bool SupportsOdbcTimestampLiterals
		{
			get
			{
				return this.baseInfo.SupportsOdbcTimestampLiterals;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06002D81 RID: 11649 RVA: 0x0008A664 File Offset: 0x00088864
		public override bool SupportsTopOrLimit
		{
			get
			{
				return this.baseInfo.SupportsTopOrLimit;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06002D82 RID: 11650 RVA: 0x0008A671 File Offset: 0x00088871
		public override NativeQuerySchemaStrategy NativeQuerySchemaStrategy
		{
			get
			{
				return this.baseInfo.NativeQuerySchemaStrategy;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x06002D83 RID: 11651 RVA: 0x0008A67E File Offset: 0x0008887E
		public override bool PrepareStatements
		{
			get
			{
				return this.baseInfo.PrepareStatements;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x0008A68B File Offset: 0x0008888B
		public override Odbc32.SQL_RETURN_ESCAPE_CLAUSE ReturnEscapeClause
		{
			get
			{
				return this.baseInfo.ReturnEscapeClause;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x0008A698 File Offset: 0x00088898
		public override RecordValue DefaultTypeParameters
		{
			get
			{
				return this.baseInfo.DefaultTypeParameters;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06002D86 RID: 11654 RVA: 0x0008A6A5 File Offset: 0x000888A5
		public override bool SupportsNativeQuery
		{
			get
			{
				return this.baseInfo.SupportsNativeQuery;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06002D87 RID: 11655 RVA: 0x0008A6B2 File Offset: 0x000888B2
		public override bool TryRecoverDateDiff
		{
			get
			{
				return this.baseInfo.TryRecoverDateDiff;
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06002D88 RID: 11656 RVA: 0x0008A6BF File Offset: 0x000888BF
		public override bool TryRecoverCoalesce
		{
			get
			{
				return this.baseInfo.TryRecoverCoalesce;
			}
		}

		// Token: 0x040013D0 RID: 5072
		private readonly OdbcDataSourceInfo baseInfo;
	}
}
