using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Common.Creators;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Drda
{
	// Token: 0x02000CB0 RID: 3248
	internal class MsDb2Environment : DrdaEnvironment
	{
		// Token: 0x060057EB RID: 22507 RVA: 0x00132D9B File Offset: 0x00130F9B
		protected override void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			MsDb2AstExpressionChecker.Check(expression, cursor, this);
		}

		// Token: 0x060057EC RID: 22508 RVA: 0x00132DA5 File Offset: 0x00130FA5
		protected override void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			MsDb2AstExpressionChecker.CheckStatement(expression, cursor, this);
		}

		// Token: 0x060057ED RID: 22509 RVA: 0x00132DB0 File Offset: 0x00130FB0
		public override TableValue GetDirectQueryCapabilities()
		{
			if (this.capabilities == null)
			{
				List<Value> list = new List<Value>();
				list.Add(CapabilityModule.NewCapability("Core", Value.Null));
				list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(2100)));
				list.AddRange(DbEnvironment.Capabilities.TableFunctions.Select((string tableFunction) => CapabilityModule.NewCapability(tableFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.DateFunctions.Select((string dateFunction) => CapabilityModule.NewCapability(dateFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.NumericFunctions.Select((string numericFunction) => CapabilityModule.NewCapability(numericFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.StringFunctions.Select((string stringFunction) => CapabilityModule.NewCapability(stringFunction, Value.Null)).Cast<Value>());
				list.AddRange(DbEnvironment.Capabilities.ListFunctions.Select((string listFunction) => CapabilityModule.NewCapability(listFunction, Value.Null)).Cast<Value>());
				TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
				this.capabilities = ListValue.New(list.ToArray()).ToTable(asTableType);
			}
			return this.capabilities;
		}

		// Token: 0x060057EE RID: 22510 RVA: 0x00132F38 File Offset: 0x00131138
		private static Dictionary<string, TypeValue> GenerateNativeToClrTypeMapping(bool isBinaryAsChar)
		{
			TypeValue typeValue = (isBinaryAsChar ? TypeValue.Text : TypeValue.Binary);
			return new Dictionary<string, TypeValue>
			{
				{
					"bigint",
					TypeValue.Int64
				},
				{
					"binary",
					TypeValue.Binary
				},
				{
					"blob",
					TypeValue.Binary
				},
				{
					"boolean",
					TypeValue.Int16
				},
				{ "char () for bit data", typeValue },
				{
					"char",
					TypeValue.Text
				},
				{
					"character",
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
					"dbclob",
					TypeValue.Text
				},
				{
					"decfloat",
					TypeValue.Decimal
				},
				{
					"decimal",
					TypeValue.Decimal
				},
				{
					"double precision",
					TypeValue.Double
				},
				{
					"double",
					TypeValue.Double
				},
				{
					"float",
					TypeValue.Single
				},
				{
					"graphic",
					TypeValue.Text
				},
				{
					"int",
					TypeValue.Int32
				},
				{
					"integer",
					TypeValue.Int32
				},
				{
					"long nvarchar",
					TypeValue.Text
				},
				{ "long varbinary", typeValue },
				{ "long varchar () for bit data", typeValue },
				{
					"long varchar",
					TypeValue.Text
				},
				{
					"long vargraphic",
					TypeValue.Text
				},
				{
					"nchar",
					TypeValue.Text
				},
				{
					"nclob",
					TypeValue.Text
				},
				{
					"numeric",
					TypeValue.Decimal
				},
				{
					"nvarchar",
					TypeValue.Text
				},
				{
					"real",
					TypeValue.Single
				},
				{
					"rowid",
					TypeValue.Text
				},
				{
					"smallint",
					TypeValue.Int16
				},
				{
					"time",
					TypeValue.Time
				},
				{
					"timestamp with time zone",
					TypeValue.DateTime
				},
				{
					"timestamp",
					TypeValue.DateTime
				},
				{
					"varbinary",
					TypeValue.Binary
				},
				{ "varchar () for bit data", typeValue },
				{
					"varchar",
					TypeValue.Text
				},
				{
					"vargraphic",
					TypeValue.Text
				},
				{
					"xml",
					TypeValue.Text
				}
			};
		}

		// Token: 0x060057EF RID: 22511 RVA: 0x001331AA File Offset: 0x001313AA
		public static DbEnvironment Create(IEngineHost host, string server, string database, Value options)
		{
			return new MsDb2Environment(host, server, database, options);
		}

		// Token: 0x060057F0 RID: 22512 RVA: 0x001331B8 File Offset: 0x001313B8
		public MsDb2Environment(IEngineHost host, string server, string database, Value options)
			: base(host, "DB2", "Microsoft Db2 Client", server, database, options)
		{
			this.binaryCodePage = 0;
			this.packageCollection = "NULLID";
			if (!options.IsNull)
			{
				object obj;
				if (base.UserOptions.TryGetValue("BinaryCodePage", out obj))
				{
					this.binaryCodePage = (int)obj;
					if (this.binaryCodePage < 0 || this.binaryCodePage > 65535)
					{
						throw ValueException.NewExpressionError<Message1>(Strings.InvalidBinaryCodePage(this.binaryCodePage), null, null);
					}
				}
				string text;
				if (base.UserOptions.TryGetString("PackageCollection", out text))
				{
					this.packageCollection = text;
				}
				object obj2;
				if (base.UserOptions.TryGetValue("UseDb2ConnectGateway", out obj2))
				{
					base.UseDb2ConnectGateway = new bool?((bool)obj2);
				}
			}
		}

		// Token: 0x17001A6B RID: 6763
		// (get) Token: 0x060057F1 RID: 22513 RVA: 0x00133286 File Offset: 0x00131486
		public override HashSet<string> SearchableTypes
		{
			get
			{
				return MsDb2Environment.searchableTypes;
			}
		}

		// Token: 0x17001A6C RID: 6764
		// (get) Token: 0x060057F2 RID: 22514 RVA: 0x0013328D File Offset: 0x0013148D
		public override Dictionary<string, TypeValue> NativeToClrTypeMapping
		{
			get
			{
				if (this.binaryCodePage > 0 && this.binaryCodePage < 65535)
				{
					return MsDb2Environment.nativeToClrTypeMappingWithBinaryCodePage;
				}
				return MsDb2Environment.nativeToClrTypeMapping;
			}
		}

		// Token: 0x060057F3 RID: 22515 RVA: 0x001332B0 File Offset: 0x001314B0
		protected override SqlSettings LoadSqlSettings()
		{
			return MsDb2Environment.sqlSettings;
		}

		// Token: 0x17001A6D RID: 6765
		// (get) Token: 0x060057F4 RID: 22516 RVA: 0x001332B7 File Offset: 0x001314B7
		protected override int DefaultPort
		{
			get
			{
				return 50000;
			}
		}

		// Token: 0x17001A6E RID: 6766
		// (get) Token: 0x060057F5 RID: 22517 RVA: 0x00002105 File Offset: 0x00000305
		protected override DrdaFlavor Flavor
		{
			get
			{
				return DrdaFlavor.Db2;
			}
		}

		// Token: 0x17001A6F RID: 6767
		// (get) Token: 0x060057F6 RID: 22518 RVA: 0x001332BE File Offset: 0x001314BE
		protected override int BinaryCodePage
		{
			get
			{
				return this.binaryCodePage;
			}
		}

		// Token: 0x17001A70 RID: 6768
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x001332C6 File Offset: 0x001314C6
		protected override string PackageCollection
		{
			get
			{
				return this.packageCollection;
			}
		}

		// Token: 0x060057F8 RID: 22520 RVA: 0x001332D0 File Offset: 0x001314D0
		public override SqlDataType GetSqlScalarType(TypeValue type)
		{
			switch (type.TypeKind)
			{
			case ValueKind.Time:
				return new SqlDataType(type, new ConstantSqlString("time"));
			case ValueKind.Date:
				return new SqlDataType(type, new ConstantSqlString("date"));
			case ValueKind.DateTime:
				return new SqlDataType(type, new ConstantSqlString("timestamp"));
			case ValueKind.DateTimeZone:
			case ValueKind.Logical:
				throw ValueException.NewExpressionError<Message1>(Strings.Catalog_UnsupportedColumnType(type.ToSource()), null, null);
			case ValueKind.Number:
				if (type.Equals(TypeValue.Byte))
				{
					return SqlDataType.SmallInt;
				}
				if (type.Equals(TypeValue.Int8))
				{
					return SqlDataType.SmallInt;
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
					return SqlDataType.Decimal;
				}
				return SqlDataType.Float;
			case ValueKind.Text:
				return new SqlDataType(type, new ConstantSqlString("clob"));
			case ValueKind.Binary:
				return new SqlDataType(type, new ConstantSqlString("varbinary(16352)"));
			}
			return base.GetSqlScalarType(type);
		}

		// Token: 0x060057F9 RID: 22521 RVA: 0x00133422 File Offset: 0x00131622
		public override DbAstCreator NewAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			return MsDb2AstCreator.Create(expression, cursor, this);
		}

		// Token: 0x17001A71 RID: 6769
		// (get) Token: 0x060057FA RID: 22522 RVA: 0x0013342C File Offset: 0x0013162C
		public override OptionRecordDefinition ValidOptions
		{
			get
			{
				return MsDb2Module.OptionRecord;
			}
		}

		// Token: 0x060057FB RID: 22523 RVA: 0x00133434 File Offset: 0x00131634
		public override Exception ProcessDbException(DbException exception)
		{
			PropertyInfo property = exception.GetType().GetProperty("SqlCode", typeof(int));
			if (property != null && (int)property.GetValue(exception, null) == -357)
			{
				return new RebuildConnectionException("Retry for Db2Connect Gateway", exception);
			}
			return base.ProcessDbException(exception);
		}

		// Token: 0x04003185 RID: 12677
		private const string DefaultPackageCollection = "NULLID";

		// Token: 0x04003186 RID: 12678
		private const string DataSourceName = "Microsoft Db2 Client";

		// Token: 0x04003187 RID: 12679
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMapping = MsDb2Environment.GenerateNativeToClrTypeMapping(false);

		// Token: 0x04003188 RID: 12680
		private static readonly Dictionary<string, TypeValue> nativeToClrTypeMappingWithBinaryCodePage = MsDb2Environment.GenerateNativeToClrTypeMapping(true);

		// Token: 0x04003189 RID: 12681
		private TableValue capabilities;

		// Token: 0x0400318A RID: 12682
		private static readonly HashSet<string> searchableTypes = new HashSet<string>
		{
			"char () for bit data", "varchar () for bit data", "bigint", "boolean", "char", "character", "date", "decfloat", "decimal", "double precision",
			"double", "float", "graphic", "int", "integer", "long varchar", "long vargraphic", "numeric", "real", "rowid",
			"smallint", "time", "timestamp", "timestamp with time zone", "varchar", "vargraphic", "nchar", "nvarchar", "long nvarchar"
		};

		// Token: 0x0400318B RID: 12683
		private static readonly SqlSettings sqlSettings = new SqlSettings
		{
			InvalidIdentifierCharacters = EmptyArray<char>.Instance,
			MaxIdentifierLength = 128,
			QuoteNationalStringLiteral = SqlSettings.StandardQuote("'"),
			QuoteIdentifier = SqlSettings.StandardQuote("\""),
			RequiresAsForFromAlias = false,
			DateTimePrefix = "timestamp('",
			DateTimeSuffix = "')",
			DateTimeOffsetPrefix = "timestamp_tz('",
			DateTimeOffsetSuffix = "')",
			PagingStrategy = PagingStrategy.RowCountOnly,
			SelectItemNull = SqlLanguageStrings.NullWithTrivialCastSqlString,
			SupportsCaseExpression = true,
			SupportsForeignKeys = true,
			SupportsFullOuterJoinExpression = true,
			SupportsOutputClause = true,
			TimePrefix = "time('",
			TimeSuffix = "')",
			BinaryPrefix = "BX'",
			BinarySuffix = "'",
			DatePrefix = "date('",
			DateSuffix = "')",
			IsMaxPrecision = true
		};

		// Token: 0x0400318C RID: 12684
		private readonly int binaryCodePage;

		// Token: 0x0400318D RID: 12685
		private readonly string packageCollection;
	}
}
