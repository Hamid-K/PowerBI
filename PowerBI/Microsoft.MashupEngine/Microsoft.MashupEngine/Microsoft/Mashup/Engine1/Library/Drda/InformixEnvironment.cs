using System;
using System.Collections.Generic;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CAB RID: 3243
	internal class InformixEnvironment : DrdaEnvironment
	{
		// Token: 0x0600579E RID: 22430 RVA: 0x00130DFC File Offset: 0x0012EFFC
		public static DbEnvironment Create(IEngineHost host, string server, string database, Value options)
		{
			return new InformixEnvironment(host, server, database, options);
		}

		// Token: 0x0600579F RID: 22431 RVA: 0x00130E07 File Offset: 0x0012F007
		public InformixEnvironment(IEngineHost host, string server, string database, Value options)
			: base(host, "Informix", "Microsoft Informix Client", server, database, options)
		{
		}

		// Token: 0x060057A0 RID: 22432 RVA: 0x00130E1E File Offset: 0x0012F01E
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			InformixAstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x060057A1 RID: 22433 RVA: 0x00130E28 File Offset: 0x0012F028
		protected override void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			InformixAstExpressionChecker.CheckStatement(expression, cursor, this);
		}

		// Token: 0x17001A5E RID: 6750
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x00130E32 File Offset: 0x0012F032
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return InformixEnvironment.searchableTypes;
			}
		}

		// Token: 0x17001A5F RID: 6751
		// (get) Token: 0x060057A3 RID: 22435 RVA: 0x00130E39 File Offset: 0x0012F039
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				return InformixEnvironment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x060057A4 RID: 22436 RVA: 0x00130E40 File Offset: 0x0012F040
		protected override SqlSettings LoadSqlSettings()
		{
			return InformixEnvironment.sqlSettings;
		}

		// Token: 0x17001A60 RID: 6752
		// (get) Token: 0x060057A5 RID: 22437 RVA: 0x00130E47 File Offset: 0x0012F047
		protected override int DefaultPort
		{
			get
			{
				return 9089;
			}
		}

		// Token: 0x17001A61 RID: 6753
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x00002139 File Offset: 0x00000339
		protected override DrdaFlavor Flavor
		{
			get
			{
				return DrdaFlavor.Informix;
			}
		}

		// Token: 0x17001A62 RID: 6754
		// (get) Token: 0x060057A7 RID: 22439 RVA: 0x00002105 File Offset: 0x00000305
		protected override int BinaryCodePage
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17001A63 RID: 6755
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x00130E4E File Offset: 0x0012F04E
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return InformixModule.OptionRecord;
			}
		}

		// Token: 0x060057A9 RID: 22441 RVA: 0x00130E58 File Offset: 0x0012F058
		public override bool SupportsTake(TableTypeValue type)
		{
			string text;
			return !base.UserOptions.TryGetString("Query", out text) && type.GetPrimaryKey() != null;
		}

		// Token: 0x060057AA RID: 22442 RVA: 0x00130E84 File Offset: 0x0012F084
		public override SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return new SqlDataType(type, new ConstantSqlString("datetime hour to fraction(5)"));
			case ValueKind.Date:
				return new SqlDataType(type, new ConstantSqlString("date"));
			case ValueKind.DateTime:
				return new SqlDataType(type, new ConstantSqlString("datetime year to fraction(5)"));
			case ValueKind.DateTimeZone:
				throw ValueException.NewExpressionError<Message1>(Strings.Catalog_UnsupportedColumnType(type.ToSource()), null, null);
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte) || type.Equals(TypeValue.Int8))
				{
					return new SqlDataType(type, new ConstantSqlString("int8"));
				}
				if (type.Equals(TypeValue.Int16))
				{
					return SqlDataType.SmallInt;
				}
				if (type.Equals(TypeValue.Int32))
				{
					return SqlDataType.Int;
				}
				if (type.Equals(TypeValue.Int64))
				{
					return SqlDataType.BigInt;
				}
				if (type.Equals(TypeValue.Single))
				{
					return SqlDataType.Real;
				}
				if (type.Equals(TypeValue.Decimal))
				{
					return SqlDataType.Decimal;
				}
				if (type.Equals(TypeValue.Currency))
				{
					return new SqlDataType(type, new ConstantSqlString("money"));
				}
				return SqlDataType.Float;
			case ValueKind.Logical:
				return new SqlDataType(type, new ConstantSqlString("boolean"));
			case ValueKind.Text:
				return new SqlDataType(type, new ConstantSqlString("text"));
			case ValueKind.Binary:
				return new SqlDataType(type, new ConstantSqlString("byte"));
			}
			return base.GetSqlScalarType(type);
		}

		// Token: 0x060057AB RID: 22443 RVA: 0x00130FF7 File Offset: 0x0012F1F7
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return InformixAstCreator.Create(expression, cursor, this);
		}

		// Token: 0x04003179 RID: 12665
		public const string DataSourceName = "Microsoft Informix Client";

		// Token: 0x0400317A RID: 12666
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = new Dictionary<string, TypeValue>
		{
			{
				"bigint",
				TypeValue.Int64
			},
			{
				"bigserial",
				TypeValue.Int64
			},
			{
				"blob",
				TypeValue.Binary
			},
			{
				"boolean",
				TypeValue.Logical
			},
			{
				"byte",
				TypeValue.Binary
			},
			{
				"char",
				TypeValue.Text
			},
			{
				"clob",
				TypeValue.Text
			},
			{
				"date",
				TypeValue.Date
			},
			{
				"datetime",
				TypeValue.DateTime
			},
			{
				"decimal",
				TypeValue.Decimal
			},
			{
				"float",
				TypeValue.Single
			},
			{
				"hour to fraction(1)",
				TypeValue.Time
			},
			{
				"hour to fraction(2)",
				TypeValue.Time
			},
			{
				"hour to fraction(3)",
				TypeValue.Time
			},
			{
				"hour to fraction(4)",
				TypeValue.Time
			},
			{
				"hour to fraction(5)",
				TypeValue.Time
			},
			{
				"hour to minute",
				TypeValue.Time
			},
			{
				"hour to second",
				TypeValue.Time
			},
			{
				"int8",
				TypeValue.Int64
			},
			{
				"integer",
				TypeValue.Int32
			},
			{
				"lvarchar",
				TypeValue.Text
			},
			{
				"money",
				TypeValue.Currency
			},
			{
				"month to day",
				TypeValue.Date
			},
			{
				"nchar",
				TypeValue.Text
			},
			{
				"nvarchar",
				TypeValue.Text
			},
			{
				"serial",
				TypeValue.Int32
			},
			{
				"serial8",
				TypeValue.Int64
			},
			{
				"smallfloat",
				TypeValue.Single
			},
			{
				"smallint",
				TypeValue.Int16
			},
			{
				"text",
				TypeValue.Text
			},
			{
				"time",
				TypeValue.Time
			},
			{
				"varchar",
				TypeValue.Text
			},
			{
				"year to day",
				TypeValue.Date
			},
			{
				"year to fraction(1)",
				TypeValue.DateTime
			},
			{
				"year to fraction(2)",
				TypeValue.DateTime
			},
			{
				"year to fraction(3)",
				TypeValue.DateTime
			},
			{
				"year to fraction(4)",
				TypeValue.DateTime
			},
			{
				"year to fraction(5)",
				TypeValue.DateTime
			},
			{
				"year to minute",
				TypeValue.DateTime
			},
			{
				"year to month",
				TypeValue.Date
			},
			{
				"year to second",
				TypeValue.DateTime
			}
		};

		// Token: 0x0400317B RID: 12667
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"bigint", "bigserial", "boolean", "char", "date", "datetime", "decimal", "float", "hour to fraction(1)", "hour to fraction(2)",
			"hour to fraction(3)", "hour to fraction(4)", "hour to fraction(5)", "int8", "integer", "lvarchar", "money", "month to day", "nchar", "nvarchar",
			"serial", "serial8", "smallfloat", "smallint", "time", "varchar", "year to day", "year to fraction(1)", "year to fraction(2)", "year to fraction(3)",
			"year to fraction(4)", "year to fraction(5)", "year to minute", "year to month", "year to second"
		};

		// Token: 0x0400317C RID: 12668
		private static readonly SqlSettings sqlSettings = new SqlSettings
		{
			InvalidIdentifierCharacters = EmptyArray<char>.Instance,
			MaxIdentifierLength = 128,
			QuoteNationalStringLiteral = SqlSettings.StandardQuote("'"),
			QuoteIdentifier = SqlSettings.StandardQuote("\""),
			RequiresAsForFromAlias = false,
			DateTimePrefix = "datetime(",
			DateTimeSuffix = ")year to second",
			DateTimeOffsetPrefix = "'",
			DateTimeOffsetSuffix = "'",
			PagingStrategy = PagingStrategy.RowCountOnly,
			SelectItemNull = SqlLanguageStrings.NullWithTrivialCastSqlString,
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsOutputClause = true,
			SupportsIntervalConstants = true,
			TimePrefix = "'",
			TimeSuffix = "'",
			DatePrefix = "datetime(",
			DateSuffix = ")year to day",
			IntervalPrefix = "interval(",
			IntervalSuffix = ")day to fraction(5)",
			IsMaxPrecision = true
		};
	}
}
