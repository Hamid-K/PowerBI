using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x0200010C RID: 268
	[DataContract]
	internal sealed class IntermediateSortingKey
	{
		// Token: 0x060008D2 RID: 2258 RVA: 0x000233EC File Offset: 0x000215EC
		internal IntermediateSortingKey(ExpressionNode dsqRefNode, SortDirection sortDirection, IConceptualProperty lineageProp, int? selectIndex)
		{
			this.DsqReferenceExpression = dsqRefNode;
			this.SortDirection = sortDirection;
			this.LineageProperty = lineageProp;
			this.SelectIndex = selectIndex;
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00023411 File Offset: 0x00021611
		internal ExpressionNode DsqReferenceExpression { get; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00023419 File Offset: 0x00021619
		[DataMember(Name = "DsqReferenceExpression", Order = 1)]
		private string DsqReferenceExpressionForSerialization
		{
			get
			{
				return this.DsqReferenceExpression.ToString();
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x00023426 File Offset: 0x00021626
		[DataMember(Order = 2, EmitDefaultValue = false)]
		internal SortDirection SortDirection { get; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x0002342E File Offset: 0x0002162E
		internal IConceptualProperty LineageProperty { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00023436 File Offset: 0x00021636
		[DataMember(Name = "LineageProperty", EmitDefaultValue = false, Order = 3)]
		private string LineagePropertyForSerialization
		{
			get
			{
				return IntermediateTableSchemaSerializationUtils.Serialize(this.LineageProperty);
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00023443 File Offset: 0x00021643
		[DataMember(Order = 4, EmitDefaultValue = false)]
		internal int? SelectIndex { get; }
	}
}
