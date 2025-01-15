using System;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x0200010D RID: 269
	[DataContract]
	internal sealed class IntermediateGroupingDetailValue
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x0002344B File Offset: 0x0002164B
		internal IntermediateGroupingDetailValue(ExpressionNode dsqRefNode, int selectIndex, IConceptualColumn lineageProperty, string formatString)
		{
			this.DsqReferenceExpression = dsqRefNode;
			this.SelectIndex = selectIndex;
			this.LineageProperty = lineageProperty;
			this.FormatString = formatString;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00023470 File Offset: 0x00021670
		internal ExpressionNode DsqReferenceExpression { get; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060008DB RID: 2267 RVA: 0x00023478 File Offset: 0x00021678
		[DataMember(Name = "DsqReferenceExpression", Order = 1)]
		private string DsqReferenceExpressionForSerialization
		{
			get
			{
				return this.DsqReferenceExpression.ToString();
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00023485 File Offset: 0x00021685
		[DataMember(Order = 2, EmitDefaultValue = false)]
		internal int SelectIndex { get; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060008DD RID: 2269 RVA: 0x0002348D File Offset: 0x0002168D
		internal IConceptualProperty LineageProperty { get; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00023495 File Offset: 0x00021695
		[DataMember(Name = "LineageProperty", EmitDefaultValue = false, Order = 3)]
		private string LineagePropertyForSerialization
		{
			get
			{
				return IntermediateTableSchemaSerializationUtils.Serialize(this.LineageProperty);
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008DF RID: 2271 RVA: 0x000234A2 File Offset: 0x000216A2
		[DataMember(Order = 4, EmitDefaultValue = false)]
		internal string FormatString { get; }
	}
}
