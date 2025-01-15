using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.Data.Contracts.QueryExtensionSchema
{
	// Token: 0x020000C5 RID: 197
	public sealed class QueryExtensionEntityBuilder<TParent> : BaseSchemaExtensionBuilder<QueryExtensionEntity, TParent>
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x0000BDC1 File Offset: 0x00009FC1
		public QueryExtensionEntityBuilder(QueryExtensionEntity activeObject, TParent parent)
			: base(activeObject, parent)
		{
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x0000BDCC File Offset: 0x00009FCC
		public QueryExtensionEntityBuilder<TParent> WithMeasure(string name, string expression, ConceptualPrimitiveType dataType)
		{
			QueryExtensionMeasure queryExtensionMeasure;
			return this.WithMeasure(name, expression, dataType, out queryExtensionMeasure);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0000BDE4 File Offset: 0x00009FE4
		public QueryExtensionEntityBuilder<TParent> WithMeasure(string name, string expression, ConceptualPrimitiveType dataType, out QueryExtensionMeasure measure)
		{
			if (base.ActiveObject.Measures == null)
			{
				base.ActiveObject.Measures = new List<QueryExtensionMeasure>();
			}
			measure = new QueryExtensionMeasure
			{
				Name = name,
				Expression = expression,
				DataType = dataType
			};
			base.ActiveObject.Measures.Add(measure);
			return this;
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0000BE40 File Offset: 0x0000A040
		public QueryExtensionEntityBuilder<TParent> WithColumn(string name, string expression, ConceptualPrimitiveType dataType, QueryExtensionNamingBehavior namingBehavior = QueryExtensionNamingBehavior.Preserve)
		{
			QueryExtensionColumn queryExtensionColumn;
			return this.WithColumn(name, expression, dataType, namingBehavior, out queryExtensionColumn);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public QueryExtensionEntityBuilder<TParent> WithColumn(string name, string expression, ConceptualPrimitiveType dataType, QueryExtensionNamingBehavior namingBehavior, out QueryExtensionColumn column)
		{
			if (base.ActiveObject.Columns == null)
			{
				base.ActiveObject.Columns = new List<QueryExtensionColumn>();
			}
			column = new QueryExtensionColumn
			{
				Name = name,
				Expression = expression,
				DataType = dataType,
				NamingBehavior = namingBehavior
			};
			base.ActiveObject.Columns.Add(column);
			return this;
		}
	}
}
