using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002EC RID: 748
	public sealed class SemanticQueryDataShapeCommandBuilder
	{
		// Token: 0x060018D5 RID: 6357 RVA: 0x0002C96F File Offset: 0x0002AB6F
		public SemanticQueryDataShapeCommandBuilder()
		{
			this._command = new SemanticQueryDataShapeCommand();
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0002C982 File Offset: 0x0002AB82
		public SemanticQueryDefinitionBuilder<SemanticQueryDataShapeCommandBuilder> WithQuery(int? version = null)
		{
			this._queryBuilder = new SemanticQueryDefinitionBuilder<SemanticQueryDataShapeCommandBuilder>(this, version);
			return this._queryBuilder;
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0002C997 File Offset: 0x0002AB97
		public SemanticQueryDataShapeCommandBuilder WithQuery(QueryDefinition queryDefinition)
		{
			this._command.Query = queryDefinition;
			return this;
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0002C9A6 File Offset: 0x0002ABA6
		public DataShapeBindingBuilder WithBinding(int? version = null)
		{
			this._bindingBuilder = new DataShapeBindingBuilder(this, version);
			return this._bindingBuilder;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0002C9BB File Offset: 0x0002ABBB
		public SemanticQueryDataShapeCommandBuilder WithMaxRowCount(int maxRowCount)
		{
			this._command.MaxRowCount = new int?(maxRowCount);
			return this;
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0002C9CF File Offset: 0x0002ABCF
		public SemanticQueryDataShapeCommandBuilder WithExtensionSchema(QueryExtensionSchema queryExtensionSchema)
		{
			this._command.Extension = queryExtensionSchema;
			return this;
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0002C9DE File Offset: 0x0002ABDE
		public SemanticQueryDataShapeCommandBuilder WithDataSourceVariables(string dataSourceVariables)
		{
			this._command.DataSourceVariables = dataSourceVariables;
			return this;
		}

		// Token: 0x060018DC RID: 6364 RVA: 0x0002C9ED File Offset: 0x0002ABED
		public SemanticQueryDataShapeCommandBuilder WithExecutionMetricsKind(ExecutionMetricsKind executionMetricsKind)
		{
			this._command.ExecutionMetricsKind = executionMetricsKind;
			return this;
		}

		// Token: 0x060018DD RID: 6365 RVA: 0x0002C9FC File Offset: 0x0002ABFC
		public SemanticQueryDataShapeCommandBuilder WithAnchorTime(string anchorTime)
		{
			this._command.AnchorTime = anchorTime;
			return this;
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x0002CA0C File Offset: 0x0002AC0C
		public SemanticQueryDataShapeCommand Build()
		{
			if (this._queryBuilder != null)
			{
				this._command.Query = this._queryBuilder.Build();
			}
			if (this._bindingBuilder != null)
			{
				this._command.Binding = this._bindingBuilder.Build();
			}
			return this._command;
		}

		// Token: 0x04000907 RID: 2311
		private readonly SemanticQueryDataShapeCommand _command;

		// Token: 0x04000908 RID: 2312
		private SemanticQueryDefinitionBuilder<SemanticQueryDataShapeCommandBuilder> _queryBuilder;

		// Token: 0x04000909 RID: 2313
		private DataShapeBindingBuilder _bindingBuilder;
	}
}
