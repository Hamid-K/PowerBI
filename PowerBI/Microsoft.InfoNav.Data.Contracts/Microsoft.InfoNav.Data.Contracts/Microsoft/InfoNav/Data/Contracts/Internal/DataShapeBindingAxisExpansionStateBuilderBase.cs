using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020001A2 RID: 418
	public abstract class DataShapeBindingAxisExpansionStateBuilderBase<TParent, TObject, TInstanceBuilder> : BaseBindingBuilder<TObject, TParent>
	{
		// Token: 0x06000B5D RID: 2909 RVA: 0x000165F5 File Offset: 0x000147F5
		public DataShapeBindingAxisExpansionStateBuilderBase(TParent parent)
			: base(parent)
		{
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000165FE File Offset: 0x000147FE
		public DataShapeBindingAxisExpansionStateBuilderBase<TParent, TObject, TInstanceBuilder> WithFrom(EntitySource source)
		{
			BaseBindingBuilder<TObject, TParent>.AddToLazyList<EntitySource>(ref this._froms, source);
			return this;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001660D File Offset: 0x0001480D
		public DataShapeBindingAxisExpansionStateBuilderBase<TParent, TObject, TInstanceBuilder> WithFrom(string name, string entity, string schema = null)
		{
			return this.WithFrom(SemanticQueryDefinitionBuilder<SemanticQueryDataShapeCommandBuilder>.BuildEntitySource(name, entity, schema));
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x00016620 File Offset: 0x00014820
		public DataShapeBindingAxisExpansionStateBuilderBase<TParent, TObject, TInstanceBuilder> WithLevel(ExpansionDefaultState state, params QueryExpressionContainer[] expressions)
		{
			DataShapeBindingAxisExpansionLevel dataShapeBindingAxisExpansionLevel = new DataShapeBindingAxisExpansionLevel
			{
				Default = state,
				Expressions = expressions.ToList<QueryExpressionContainer>()
			};
			BaseBindingBuilder<TObject, TParent>.AddToLazyList<DataShapeBindingAxisExpansionLevel>(ref this._levels, dataShapeBindingAxisExpansionLevel);
			return this;
		}

		// Token: 0x06000B61 RID: 2913
		public abstract TInstanceBuilder WithInstance();

		// Token: 0x0400061B RID: 1563
		protected List<DataShapeBindingAxisExpansionLevel> _levels;

		// Token: 0x0400061C RID: 1564
		protected List<EntitySource> _froms;
	}
}
