using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010B8 RID: 4280
	internal abstract class EnvironmentBase
	{
		// Token: 0x06007022 RID: 28706 RVA: 0x00181A26 File Offset: 0x0017FC26
		protected EnvironmentBase(IEngineHost host, string foldingFailureEntryName)
		{
			this.host = host;
			this.foldingTracingService = new FoldingTracingService(host, foldingFailureEntryName);
		}

		// Token: 0x17001F90 RID: 8080
		// (get) Token: 0x06007023 RID: 28707 RVA: 0x00181A42 File Offset: 0x0017FC42
		public IEngineHost Host
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001F91 RID: 8081
		// (get) Token: 0x06007024 RID: 28708
		public abstract IResource Resource { get; }

		// Token: 0x17001F92 RID: 8082
		// (get) Token: 0x06007025 RID: 28709 RVA: 0x00181A4A File Offset: 0x0017FC4A
		public FoldingTracingService FoldingTracingService
		{
			get
			{
				return this.foldingTracingService;
			}
		}

		// Token: 0x06007026 RID: 28710
		protected abstract void Check(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor);

		// Token: 0x06007027 RID: 28711
		protected abstract ValueBuilderBase Compile(Query originalQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor);

		// Token: 0x06007028 RID: 28712 RVA: 0x00181A52 File Offset: 0x0017FC52
		public void VerifyActionPermitted()
		{
			this.Host.VerifyActionPermitted(this.Resource);
		}

		// Token: 0x06007029 RID: 28713 RVA: 0x00181A65 File Offset: 0x0017FC65
		protected virtual void CheckStatement(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor)
		{
			throw this.FoldingTracingService.NewFoldingFailureException("Not implemented");
		}

		// Token: 0x0600702A RID: 28714 RVA: 0x000033E7 File Offset: 0x000015E7
		protected virtual ActionValue CompileStatement(Query targetQuery, IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, string statementType)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600702B RID: 28715 RVA: 0x00181A78 File Offset: 0x0017FC78
		public virtual TableValue GetDirectQueryCapabilities()
		{
			TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
			return ListValue.Empty.ToTable(asTableType);
		}

		// Token: 0x0600702C RID: 28716
		public abstract TableValue CreateCatalogTableValue(IEngineHost host);

		// Token: 0x0600702D RID: 28717
		public abstract bool OtherCanFoldToThis(EnvironmentBase other);

		// Token: 0x0600702E RID: 28718
		public abstract bool TryGetRelationshipIdentity(SchemaItem schemaItem, out string relationshipIdentity);

		// Token: 0x0600702F RID: 28719 RVA: 0x00181AAA File Offset: 0x0017FCAA
		public bool IsSameDataSourceEnvironment(EnvironmentBase other)
		{
			return this.OtherCanFoldToThis(other) && other.OtherCanFoldToThis(this);
		}

		// Token: 0x06007030 RID: 28720 RVA: 0x00181AC0 File Offset: 0x0017FCC0
		public ValueBuilderBase Compile(Query originalQuery, IExpression expression)
		{
			if (expression == null)
			{
				return null;
			}
			LogicalAstToCachedTypeflowResultCursor logicalAstToCachedTypeflowResultCursor = LogicalAstToCachedTypeflowResultCursor.Create(expression);
			return this.Compile(originalQuery, expression, logicalAstToCachedTypeflowResultCursor);
		}

		// Token: 0x06007031 RID: 28721 RVA: 0x00181AE2 File Offset: 0x0017FCE2
		public virtual EnvironmentStatementBuilder NewStatementBuilder(TableValue table, ValueBuilderBase valueBuilder, IExpression expression)
		{
			return new EnvironmentStatementBuilder(table, this, valueBuilder, expression);
		}

		// Token: 0x06007032 RID: 28722 RVA: 0x00181AF0 File Offset: 0x0017FCF0
		public virtual ActionValue CompileStatement(Query targetQuery, IExpression expression, string statementType)
		{
			if (expression == null)
			{
				return null;
			}
			LogicalAstToCachedTypeflowResultCursor logicalAstToCachedTypeflowResultCursor = LogicalAstToCachedTypeflowResultCursor.Create(expression);
			return this.CompileStatement(targetQuery, expression, logicalAstToCachedTypeflowResultCursor, statementType);
		}

		// Token: 0x06007033 RID: 28723 RVA: 0x00181B14 File Offset: 0x0017FD14
		public virtual bool IsExpressionSupported(IExpression expression, IEngineHost host)
		{
			if (expression == null)
			{
				return false;
			}
			LogicalAstToCachedTypeflowResultCursor logicalAstToCachedTypeflowResultCursor = LogicalAstToCachedTypeflowResultCursor.Create(expression);
			bool flag;
			try
			{
				this.Check(expression, logicalAstToCachedTypeflowResultCursor);
				flag = true;
			}
			catch (FoldingFailureException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06007034 RID: 28724 RVA: 0x00181B50 File Offset: 0x0017FD50
		public virtual bool IsStatementSupported(IExpression expression)
		{
			if (expression == null)
			{
				return false;
			}
			LogicalAstToCachedTypeflowResultCursor logicalAstToCachedTypeflowResultCursor = LogicalAstToCachedTypeflowResultCursor.Create(expression);
			bool flag;
			try
			{
				this.CheckStatement(expression, logicalAstToCachedTypeflowResultCursor);
				flag = true;
			}
			catch (FoldingFailureException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x06007035 RID: 28725 RVA: 0x00179436 File Offset: 0x00177636
		public virtual bool SupportsSkip(TableTypeValue type)
		{
			return type.GetPrimaryKey() != null;
		}

		// Token: 0x06007036 RID: 28726 RVA: 0x00002139 File Offset: 0x00000339
		public virtual bool SupportsTake(TableTypeValue type)
		{
			return true;
		}

		// Token: 0x17001F93 RID: 8083
		// (get) Token: 0x06007037 RID: 28727 RVA: 0x00002139 File Offset: 0x00000339
		public virtual int InsertBatchSize
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17001F94 RID: 8084
		// (get) Token: 0x06007038 RID: 28728 RVA: 0x0017811C File Offset: 0x0017631C
		public virtual int BulkInsertMinimumSize
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06007039 RID: 28729 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void ReportFoldingFailure()
		{
		}

		// Token: 0x04003DFC RID: 15868
		private readonly IEngineHost host;

		// Token: 0x04003DFD RID: 15869
		private readonly FoldingTracingService foldingTracingService;
	}
}
