using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002DC RID: 732
	public sealed class QueryTransformBuilder<TParent> : BaseBuilder<QueryTransform, SemanticQueryDefinitionBuilder<TParent>>
	{
		// Token: 0x0600187C RID: 6268 RVA: 0x0002BEBD File Offset: 0x0002A0BD
		public QueryTransformBuilder(string name, string algorithm, SemanticQueryDefinitionBuilder<TParent> parent)
			: base(parent)
		{
			this._queryTransform = new QueryTransform();
			this._queryTransform.Name = name;
			this._queryTransform.Algorithm = algorithm;
		}

		// Token: 0x0600187D RID: 6269 RVA: 0x0002BEE9 File Offset: 0x0002A0E9
		public QueryTransformBuilder(QueryTransform queryTransform, SemanticQueryDefinitionBuilder<TParent> parent)
			: base(parent)
		{
			this._queryTransform = queryTransform;
		}

		// Token: 0x0600187E RID: 6270 RVA: 0x0002BEFC File Offset: 0x0002A0FC
		public QueryTransformInputBuilder<TParent> WithInput()
		{
			QueryTransformInput queryTransformInput = new QueryTransformInput();
			this._queryTransform.Input = queryTransformInput;
			return new QueryTransformInputBuilder<TParent>(queryTransformInput, this);
		}

		// Token: 0x0600187F RID: 6271 RVA: 0x0002BF24 File Offset: 0x0002A124
		public QueryTransformOutputBuilder<TParent> WithOutput()
		{
			QueryTransformOutput queryTransformOutput = new QueryTransformOutput();
			this._queryTransform.Output = queryTransformOutput;
			return new QueryTransformOutputBuilder<TParent>(queryTransformOutput, this);
		}

		// Token: 0x06001880 RID: 6272 RVA: 0x0002BF4A File Offset: 0x0002A14A
		public override QueryTransform Build()
		{
			return this._queryTransform;
		}

		// Token: 0x040008A5 RID: 2213
		private readonly QueryTransform _queryTransform;
	}
}
