using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x020000CE RID: 206
	[DataContract]
	internal sealed class IntermediateTableSchemaColumn : IIntermediateTableSchemaItem
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0001BFB1 File Offset: 0x0001A1B1
		internal IntermediateTableSchemaColumn(string name, string valueCalculationId, string formatString, IConceptualProperty lineageProperty, IReadOnlyList<IntermediateTableSchemaKey> groupKeys, IReadOnlyList<IntermediateTableSchemaKey> sortKeys)
		{
			this.Name = name;
			this.ValueCalculationId = valueCalculationId;
			this.FormatString = formatString;
			this.LineageProperty = lineageProperty;
			this.GroupKeys = groupKeys;
			this.SortKeys = sortKeys;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001BFE6 File Offset: 0x0001A1E6
		[DataMember(Order = 0, EmitDefaultValue = false)]
		public string Name { get; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0001BFEE File Offset: 0x0001A1EE
		[DataMember(Name = "ValueCalculationId", Order = 1)]
		public string ValueCalculationId { get; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0001BFF6 File Offset: 0x0001A1F6
		[DataMember(Order = 2, EmitDefaultValue = false)]
		public string FormatString { get; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0001BFFE File Offset: 0x0001A1FE
		public IConceptualProperty LineageProperty { get; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0001C006 File Offset: 0x0001A206
		[DataMember(Order = 3, Name = "LineageProperty", EmitDefaultValue = false)]
		private string LineagePropertyForSerialization
		{
			get
			{
				return IntermediateTableSchemaSerializationUtils.Serialize(this.LineageProperty);
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0001C013 File Offset: 0x0001A213
		[DataMember(Order = 4, EmitDefaultValue = false)]
		internal IReadOnlyList<IntermediateTableSchemaKey> GroupKeys { get; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0001C01B File Offset: 0x0001A21B
		[DataMember(Order = 5, EmitDefaultValue = false)]
		internal IReadOnlyList<IntermediateTableSchemaKey> SortKeys { get; }

		// Token: 0x06000767 RID: 1895 RVA: 0x0001C024 File Offset: 0x0001A224
		public bool TryGetRelatedItem(IConceptualProperty lineageProperty, out IIntermediateTableSchemaItem item)
		{
			if (lineageProperty == this.LineageProperty)
			{
				item = this;
				return true;
			}
			IntermediateTableSchemaKey intermediateTableSchemaKey;
			if (this.TryGetMatchingColumn(lineageProperty, this.GroupKeys, out intermediateTableSchemaKey) || this.TryGetMatchingColumn(lineageProperty, this.SortKeys, out intermediateTableSchemaKey))
			{
				item = intermediateTableSchemaKey;
				return true;
			}
			item = null;
			return false;
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001C06C File Offset: 0x0001A26C
		private bool TryGetMatchingColumn(IConceptualProperty column, IReadOnlyList<IntermediateTableSchemaKey> keys, out IntermediateTableSchemaKey key)
		{
			if (keys != null)
			{
				foreach (IntermediateTableSchemaKey intermediateTableSchemaKey in keys)
				{
					if (intermediateTableSchemaKey.Source == column)
					{
						key = intermediateTableSchemaKey;
						return true;
					}
				}
			}
			key = null;
			return false;
		}
	}
}
