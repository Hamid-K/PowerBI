using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000CC RID: 204
	[DataContract]
	internal sealed class IntermediateDataShapeTableSchema : IIntermediateTableSchema
	{
		// Token: 0x06000755 RID: 1877 RVA: 0x0001BEE7 File Offset: 0x0001A0E7
		internal IntermediateDataShapeTableSchema(ExpressionNode dataShapeReferenceExpression, IReadOnlyList<IntermediateTableSchemaColumn> columns)
		{
			this.ReferenceExpression = dataShapeReferenceExpression;
			this.Columns = columns;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001BEFD File Offset: 0x0001A0FD
		internal bool TryGetColumn(string name, out IntermediateTableSchemaColumn column)
		{
			if (this._columnsByName == null)
			{
				this._columnsByName = IntermediateDataShapeTableSchema.CreateColumnsByName(this.Columns);
			}
			return this._columnsByName.TryGetValue(name, out column);
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001BF28 File Offset: 0x0001A128
		private static IReadOnlyDictionary<string, IntermediateTableSchemaColumn> CreateColumnsByName(IReadOnlyList<IntermediateTableSchemaColumn> columns)
		{
			Dictionary<string, IntermediateTableSchemaColumn> dictionary = new Dictionary<string, IntermediateTableSchemaColumn>(columns.Count, QueryNameComparer.Instance);
			foreach (IntermediateTableSchemaColumn intermediateTableSchemaColumn in columns)
			{
				if (intermediateTableSchemaColumn != null && intermediateTableSchemaColumn.Name != null)
				{
					dictionary.Add(intermediateTableSchemaColumn.Name, intermediateTableSchemaColumn);
				}
			}
			return dictionary;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000758 RID: 1880 RVA: 0x0001BF94 File Offset: 0x0001A194
		public ExpressionNode ReferenceExpression { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000759 RID: 1881 RVA: 0x0001BF9C File Offset: 0x0001A19C
		[DataMember(Name = "DataShapeReferenceExpression", Order = 1)]
		private string ExpressionForSerialization
		{
			get
			{
				return this.ReferenceExpression.ToString();
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x0001BFA9 File Offset: 0x0001A1A9
		[DataMember(Order = 2)]
		internal IReadOnlyList<IntermediateTableSchemaColumn> Columns { get; }

		// Token: 0x040003D3 RID: 979
		private IReadOnlyDictionary<string, IntermediateTableSchemaColumn> _columnsByName;
	}
}
