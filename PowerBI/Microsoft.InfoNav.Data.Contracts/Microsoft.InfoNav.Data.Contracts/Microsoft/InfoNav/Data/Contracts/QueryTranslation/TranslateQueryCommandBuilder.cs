using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryTranslation
{
	// Token: 0x020000BE RID: 190
	public sealed class TranslateQueryCommandBuilder
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x0000BC33 File Offset: 0x00009E33
		public TranslateQueryCommandBuilder(int version)
		{
			this._command = new TranslateQueryCommand();
			this._command.Version = version;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000BC54 File Offset: 0x00009E54
		public SemanticQueryDefinitionBuilder<TranslateQueryCommandBuilder> WithQuery(int? version)
		{
			Action<QueryDefinition> action = delegate(QueryDefinition query)
			{
				this._command.Query = query;
			};
			return new SemanticQueryDefinitionBuilder<TranslateQueryCommandBuilder>(this, action, version);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000BC76 File Offset: 0x00009E76
		public TranslateQueryCommandBuilder WithQuery(QueryDefinition query)
		{
			this._command.Query = query;
			return this;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000BC85 File Offset: 0x00009E85
		public TranslateQueryCommandBuilder WithBinding(DataShapeBinding binding)
		{
			this._command.Binding = binding;
			return this;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000BC94 File Offset: 0x00009E94
		public TranslateQueryCommand Build()
		{
			return this._command;
		}

		// Token: 0x0400021E RID: 542
		private readonly TranslateQueryCommand _command;
	}
}
