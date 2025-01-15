using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.Modeling.ModelGeneration.Oracle;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration.Oracle
{
	// Token: 0x02000042 RID: 66
	internal sealed class OraSqlBatch : SqlBatch
	{
		// Token: 0x060002EC RID: 748 RVA: 0x0000E51D File Offset: 0x0000C71D
		internal OraSqlBatch(SemanticModel model, string serverVersion, bool enableMathOpCasting, bool enableNO_MERGEInLeftOuters, bool enableUnistr, bool enableTSTruncation)
			: base(model, serverVersion, enableMathOpCasting)
		{
			this.m_enableNO_MERGEInLeftOuters = enableNO_MERGEInLeftOuters;
			this.m_enableUnistr = enableUnistr;
			this.m_enableTSTruncation = enableTSTruncation;
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000E54B File Offset: 0x0000C74B
		internal bool EnableNO_MERGEInLeftOuters
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_enableNO_MERGEInLeftOuters;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000E553 File Offset: 0x0000C753
		internal bool EnableUnistr
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_enableUnistr;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000E55B File Offset: 0x0000C75B
		internal bool EnableTSTruncation
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_enableTSTruncation;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000E563 File Offset: 0x0000C763
		internal Dictionary<OraSqlSelectQuery, bool?> LeftOuterInfo
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_leftOuterInfo;
			}
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000E56B File Offset: 0x0000C76B
		internal static string GetDelimitedIdentifierStatic(string name)
		{
			if (name.Contains("\""))
			{
				throw SQEAssert.AssertFalseAndThrow("Found db object name with double quotation marks: [{0}]. Double quotation marks are not supported.", new object[] { name });
			}
			return Microsoft.ReportingServices.Common.StringUtil.FormatInvariant("\"{0}\"", new object[] { name });
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000E5A3 File Offset: 0x0000C7A3
		internal override SqlSelectQuery CreateSelectQuery(ModelEntity primaryTableSource)
		{
			return new OraSqlSelectQuery(primaryTableSource, this);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000E5AC File Offset: 0x0000C7AC
		internal override SqlSelectQuery CreateSelectQuery(ISelectList primaryTableSource)
		{
			return new OraSqlSelectQuery(primaryTableSource, this);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0000E5B5 File Offset: 0x0000C7B5
		internal override SqlSelectQuery CreateSelectQuery()
		{
			return new OraSqlSelectQuery(this);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x0000E5BD File Offset: 0x0000C7BD
		internal override INameGenerator CreateSqlAliasGenerator(string defaultCandidate)
		{
			return new OraSqlBatch.OraSqlAliasGenerator(defaultCandidate, this);
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x00004118 File Offset: 0x00002318
		internal override string GetDelimitedIdentifier(string name)
		{
			return OraSqlBatch.GetDelimitedIdentifierStatic(name);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x00003FA3 File Offset: 0x000021A3
		internal override bool IsBlob(DsvColumn column)
		{
			return OraSqlDsvStatisticsProvider.IsBlobStatic(column);
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x0000C663 File Offset: 0x0000A863
		internal override int IdentifierMaxLength
		{
			[DebuggerStepThrough]
			get
			{
				return 30;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000E5C6 File Offset: 0x0000C7C6
		internal override ISqlSnippet SqlBitTrueSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return OraSqlBatch.OraSqlBitTrueSnippet;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060002FA RID: 762 RVA: 0x0000E5CD File Offset: 0x0000C7CD
		internal override ISqlSnippet SqlBitFalseSnippet
		{
			[DebuggerStepThrough]
			get
			{
				return OraSqlBatch.OraSqlBitFalseSnippet;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000C675 File Offset: 0x0000A875
		internal override string StatementSeparator
		{
			get
			{
				return ";";
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000E5D4 File Offset: 0x0000C7D4
		protected override bool CheckTypeForMathOpCasting(DataType returnDataType)
		{
			return returnDataType == DataType.Decimal || returnDataType == DataType.Float;
		}

		// Token: 0x0400013E RID: 318
		private readonly bool m_enableNO_MERGEInLeftOuters;

		// Token: 0x0400013F RID: 319
		private readonly bool m_enableUnistr;

		// Token: 0x04000140 RID: 320
		private readonly bool m_enableTSTruncation;

		// Token: 0x04000141 RID: 321
		private readonly Dictionary<OraSqlSelectQuery, bool?> m_leftOuterInfo = new Dictionary<OraSqlSelectQuery, bool?>();

		// Token: 0x04000142 RID: 322
		private static readonly ISqlSnippet OraSqlBitTrueSnippet = new SqlStringSnippet("CAST(1 AS NUMERIC(1,0))");

		// Token: 0x04000143 RID: 323
		private static readonly ISqlSnippet OraSqlBitFalseSnippet = new SqlStringSnippet("CAST(0 AS NUMERIC(1,0))");

		// Token: 0x020000C8 RID: 200
		private sealed class OraSqlAliasGenerator : SqlBatch.SqlAliasGenerator
		{
			// Token: 0x06000722 RID: 1826 RVA: 0x0001BCE3 File Offset: 0x00019EE3
			internal OraSqlAliasGenerator(string defaultCandidate, SqlBatch sqlBatch)
				: base(defaultCandidate, sqlBatch)
			{
				this.m_sqlBatch = sqlBatch;
			}

			// Token: 0x06000723 RID: 1827 RVA: 0x0001BCF4 File Offset: 0x00019EF4
			public override string CreateName(string candidate)
			{
				return base.CreateName(this.PreProcessCandidate(candidate));
			}

			// Token: 0x06000724 RID: 1828 RVA: 0x0001BD03 File Offset: 0x00019F03
			public override string CreateName(string candidate, object key)
			{
				return base.CreateName(this.PreProcessCandidate(candidate), key);
			}

			// Token: 0x06000725 RID: 1829 RVA: 0x0001BD13 File Offset: 0x00019F13
			private string PreProcessCandidate(string candidate)
			{
				if (this.m_sqlBatch.IsStringInDatabaseCharset(candidate))
				{
					return candidate;
				}
				return null;
			}

			// Token: 0x04000390 RID: 912
			private readonly SqlBatch m_sqlBatch;
		}
	}
}
