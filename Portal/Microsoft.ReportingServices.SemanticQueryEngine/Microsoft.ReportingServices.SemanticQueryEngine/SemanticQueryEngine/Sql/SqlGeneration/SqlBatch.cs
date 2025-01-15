using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Modeling;
using Microsoft.ReportingServices.SemanticQueryEngine.Sql.QueryPlanGeneration;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x02000027 RID: 39
	internal abstract class SqlBatch
	{
		// Token: 0x0600017F RID: 383 RVA: 0x000073E4 File Offset: 0x000055E4
		protected SqlBatch(SemanticModel model, string serverVersion, bool enableMathOpCasting)
		{
			if (string.IsNullOrEmpty(serverVersion))
			{
				throw SQEAssert.AssertFalseAndThrow(new ArgumentNullException("serverVersion"));
			}
			int? num = SqlBatch.ParseServerMajorVersion(serverVersion);
			if (num == null)
			{
				throw SQEAssert.AssertFalseAndThrow("Can not parse major version number in: {0}", new object[] { serverVersion });
			}
			this.m_majorVersion = num.Value;
			this.m_model = model;
			this.m_enableMathOpCasting = enableMathOpCasting;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000745A File Offset: 0x0000565A
		internal SqlSelectUnionedQuery CreateSelectUnionedQuery()
		{
			return new SqlSelectUnionedQuery();
		}

		// Token: 0x06000181 RID: 385
		internal abstract SqlSelectQuery CreateSelectQuery(ModelEntity primaryTableSource);

		// Token: 0x06000182 RID: 386
		internal abstract SqlSelectQuery CreateSelectQuery(ISelectList primaryTableSource);

		// Token: 0x06000183 RID: 387
		internal abstract SqlSelectQuery CreateSelectQuery();

		// Token: 0x06000184 RID: 388 RVA: 0x00007461 File Offset: 0x00005661
		internal virtual INameGenerator CreateSqlAliasGenerator(string defaultCandidate)
		{
			return new SqlBatch.SqlAliasGenerator(defaultCandidate, this);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000746A File Offset: 0x0000566A
		internal virtual ISqlSnippet CreateSqlSnippetForInteger(long integer)
		{
			return new SqlStringSnippet(integer.ToString(CultureInfo.InvariantCulture));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00007480 File Offset: 0x00005680
		internal bool IsStringInDatabaseCharset(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return true;
			}
			CultureInfo cultureInfo = ((this.DsvCompareInfo != null) ? this.DsvCompareInfo.Culture : null);
			Encoding encoding = Encoding.GetEncoding((cultureInfo != null) ? cultureInfo.TextInfo.ANSICodePage : Encoding.ASCII.CodePage);
			return string.Compare(value, encoding.GetString(encoding.GetBytes(value)), StringComparison.Ordinal) == 0;
		}

		// Token: 0x06000187 RID: 391
		internal abstract string GetDelimitedIdentifier(string name);

		// Token: 0x06000188 RID: 392
		internal abstract bool IsBlob(DsvColumn column);

		// Token: 0x06000189 RID: 393 RVA: 0x000074E8 File Offset: 0x000056E8
		internal bool NeedToCastReturnValueAsDecimal(FunctionNode currentFunctionNode, FunctionContext functionContext)
		{
			if (!this.m_enableMathOpCasting)
			{
				return false;
			}
			bool flag = false;
			if (this.CheckTypeForMathOpCasting(currentFunctionNode.GetFunctionInfo().ReturnType.DataType))
			{
				flag = true;
				if (currentFunctionNode.FunctionName == FunctionName.Integer || currentFunctionNode.FunctionName == FunctionName.Decimal || currentFunctionNode.FunctionName == FunctionName.Float)
				{
					flag = false;
				}
				else if (functionContext.Count > 0)
				{
					FunctionNode functionNode = functionContext.Current.FunctionNode;
					ResultType returnType = functionNode.GetFunctionInfo().ReturnType;
					if (returnType.DataType == DataType.Integer || returnType.DataType == DataType.Decimal || returnType.DataType == DataType.Float || functionNode.FunctionName == FunctionName.String)
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000758C File Offset: 0x0000578C
		internal CompiledSql Compile(ISelectList primarySelect, IEnumerable<Expression> queryResultExpressions)
		{
			if (this.m_topLevelSelectExpressions == null)
			{
				throw SQEAssert.AssertFalseAndThrow("m_topLevelSelectExpressions is not initialized.", Array.Empty<object>());
			}
			if (!this.m_statements.Contains(primarySelect))
			{
				throw SQEAssert.AssertFalseAndThrow("Specified primarySelect does not belong to the collection of statements of the current batch.", Array.Empty<object>());
			}
			FormattedStringWriter formattedStringWriter = new FormattedStringWriter(CultureInfo.InvariantCulture);
			for (int i = 0; i < this.m_statements.Count; i++)
			{
				if (i > 0)
				{
					formattedStringWriter.WriteLineIndent(this.StatementSeparator);
				}
				this.m_statements[i].Compile(formattedStringWriter);
			}
			return new CompiledSql(formattedStringWriter.ToString(), primarySelect, this.m_topLevelSelectExpressions, queryResultExpressions);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007625 File Offset: 0x00005825
		internal void AssociateQPSqlQueryWithISelectList(SqlQuery qpSqlQuery, ISelectList selectList)
		{
			if (this.m_qpSqlQueryToISelectList == null)
			{
				this.m_qpSqlQueryToISelectList = new Dictionary<SqlQuery, ISelectList>();
			}
			if (this.m_qpSqlQueryToISelectList.ContainsKey(qpSqlQuery))
			{
				throw SQEAssert.AssertFalseAndThrow();
			}
			this.m_qpSqlQueryToISelectList.Add(qpSqlQuery, selectList);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000765C File Offset: 0x0000585C
		internal ISelectList GetAssociatedISelectList(SqlQuery qpSqlQuery)
		{
			ISelectList selectList;
			if (this.m_qpSqlQueryToISelectList != null && this.m_qpSqlQueryToISelectList.TryGetValue(qpSqlQuery, out selectList))
			{
				return selectList;
			}
			return null;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00007684 File Offset: 0x00005884
		internal static int? ParseServerMajorVersion(string version)
		{
			int num;
			if (!string.IsNullOrEmpty(version) && int.TryParse(version.Split(new char[] { '.' })[0], out num))
			{
				return new int?(num);
			}
			return null;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000076C5 File Offset: 0x000058C5
		internal int ServerMajorVersion
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_majorVersion;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000076CD File Offset: 0x000058CD
		internal SqlSnippetCollection Statements
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_statements;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000190 RID: 400
		internal abstract int IdentifierMaxLength { get; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000191 RID: 401 RVA: 0x000076D5 File Offset: 0x000058D5
		internal virtual DsvCompareInfo DsvCompareInfo
		{
			[DebuggerStepThrough]
			get
			{
				if (this.m_model.DataSourceView == null)
				{
					throw SQEAssert.AssertFalseAndThrow("Model has no DSV.", Array.Empty<object>());
				}
				return this.m_model.DataSourceView.CompareInfo;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00007704 File Offset: 0x00005904
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000770C File Offset: 0x0000590C
		internal IQPExpressionInfoCollection TopLevelSelectExpressions
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_topLevelSelectExpressions;
			}
			set
			{
				this.m_topLevelSelectExpressions = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007715 File Offset: 0x00005915
		internal SemanticModel SemanticModel
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_model;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00007720 File Offset: 0x00005920
		internal DayOfWeek FirstDayOfWeek
		{
			get
			{
				if (this.__firstDayOfWeek == null)
				{
					CultureInfo cultureInfo;
					if (this.m_model.Culture != null)
					{
						cultureInfo = this.m_model.Culture;
					}
					else
					{
						cultureInfo = SemanticQueryConnection.DefaultModelCulture;
					}
					if (cultureInfo.IsNeutralCulture)
					{
						cultureInfo = CultureInfo.CreateSpecificCulture(cultureInfo.Name);
					}
					this.__firstDayOfWeek = new DayOfWeek?(cultureInfo.DateTimeFormat.FirstDayOfWeek);
				}
				return this.__firstDayOfWeek.Value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000196 RID: 406
		internal abstract ISqlSnippet SqlBitTrueSnippet { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000197 RID: 407
		internal abstract ISqlSnippet SqlBitFalseSnippet { get; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007790 File Offset: 0x00005990
		internal virtual string StatementSeparator
		{
			get
			{
				return "";
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007797 File Offset: 0x00005997
		protected virtual bool CheckTypeForMathOpCasting(DataType returnDataType)
		{
			return returnDataType == DataType.Decimal;
		}

		// Token: 0x04000072 RID: 114
		private readonly int m_majorVersion;

		// Token: 0x04000073 RID: 115
		private readonly SemanticModel m_model;

		// Token: 0x04000074 RID: 116
		private readonly SqlSnippetCollection m_statements = new SqlSnippetCollection();

		// Token: 0x04000075 RID: 117
		private IQPExpressionInfoCollection m_topLevelSelectExpressions;

		// Token: 0x04000076 RID: 118
		private Dictionary<SqlQuery, ISelectList> m_qpSqlQueryToISelectList;

		// Token: 0x04000077 RID: 119
		private DayOfWeek? __firstDayOfWeek;

		// Token: 0x04000078 RID: 120
		private readonly bool m_enableMathOpCasting;

		// Token: 0x020000B6 RID: 182
		protected class SqlAliasGenerator : INameGenerator
		{
			// Token: 0x060006AA RID: 1706 RVA: 0x0001AC3C File Offset: 0x00018E3C
			internal SqlAliasGenerator(string defaultCandidate, SqlBatch sqlBatch)
			{
				IEqualityComparer<string> equalityComparer;
				if (sqlBatch.DsvCompareInfo != null)
				{
					equalityComparer = sqlBatch.DsvCompareInfo.CreateComparer();
				}
				else
				{
					equalityComparer = Microsoft.ReportingServices.Common.StringUtil.CreateComparer(CultureInfo.InvariantCulture, CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth);
				}
				this.m_nameGenerator = new NameGenerator(null, equalityComparer);
				this.m_nameGenerator.ClsCompliant = true;
				this.m_nameGenerator.DefaultName = defaultCandidate;
				this.m_nameGenerator.MaxLength = sqlBatch.IdentifierMaxLength;
			}

			// Token: 0x060006AB RID: 1707 RVA: 0x0001ACA8 File Offset: 0x00018EA8
			public virtual string CreateName(string candidate)
			{
				return this.m_nameGenerator.CreateName(candidate);
			}

			// Token: 0x060006AC RID: 1708 RVA: 0x0001ACB6 File Offset: 0x00018EB6
			public virtual string CreateName(string candidate, object key)
			{
				return this.m_nameGenerator.CreateName(candidate, key);
			}

			// Token: 0x060006AD RID: 1709 RVA: 0x0001ACC5 File Offset: 0x00018EC5
			void INameGenerator.AddExistingName(string name)
			{
				this.m_nameGenerator.AddExistingName(name);
			}

			// Token: 0x0400034B RID: 843
			private readonly NameGenerator m_nameGenerator;
		}
	}
}
