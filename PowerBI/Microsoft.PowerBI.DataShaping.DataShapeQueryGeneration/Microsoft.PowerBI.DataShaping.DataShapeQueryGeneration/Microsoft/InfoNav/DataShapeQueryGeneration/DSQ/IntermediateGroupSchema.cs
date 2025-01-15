using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x0200010A RID: 266
	[DataContract]
	internal sealed class IntermediateGroupSchema
	{
		// Token: 0x060008C0 RID: 2240 RVA: 0x000232E4 File Offset: 0x000214E4
		internal IntermediateGroupSchema(ExpressionNode dsqReferenceExpression, ExpressionNode totalDsqReferenceExpression, SubtotalType subtotalType, IReadOnlyList<IntermediateGroupingKey> groupKeys, IReadOnlyList<IntermediateSortingKey> sortKeys, IReadOnlyList<IntermediateGroupingDetailValue> detailValues)
		{
			this.DsqReferenceExpression = dsqReferenceExpression;
			this.TotalDsqReferenceExpression = totalDsqReferenceExpression;
			this.SubtotalType = subtotalType;
			this.GroupKeys = groupKeys;
			this.SortKeys = sortKeys;
			this.DetailValues = detailValues;
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060008C1 RID: 2241 RVA: 0x00023319 File Offset: 0x00021519
		internal ExpressionNode DsqReferenceExpression { get; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00023321 File Offset: 0x00021521
		[DataMember(Name = "DsqReferenceExpression", Order = 1)]
		private string DsqReferenceExpressionForSerialization
		{
			get
			{
				return this.DsqReferenceExpression.ToString();
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060008C3 RID: 2243 RVA: 0x0002332E File Offset: 0x0002152E
		internal ExpressionNode TotalDsqReferenceExpression { get; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00023336 File Offset: 0x00021536
		[DataMember(Name = "TotalDsqReferenceExpression", Order = 2, EmitDefaultValue = false)]
		private string TotalDsqReferenceExpressionForSerialization
		{
			get
			{
				if (this.TotalDsqReferenceExpression == null)
				{
					return null;
				}
				return this.TotalDsqReferenceExpression.ToString();
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x0002334D File Offset: 0x0002154D
		[DataMember(Order = 3, EmitDefaultValue = false)]
		internal SubtotalType SubtotalType { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00023355 File Offset: 0x00021555
		[DataMember(Order = 4, EmitDefaultValue = false)]
		internal IReadOnlyList<IntermediateGroupingKey> GroupKeys { get; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x0002335D File Offset: 0x0002155D
		[DataMember(Order = 5, EmitDefaultValue = false)]
		internal IReadOnlyList<IntermediateSortingKey> SortKeys { get; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00023365 File Offset: 0x00021565
		[DataMember(Order = 6, EmitDefaultValue = false)]
		internal IReadOnlyList<IntermediateGroupingDetailValue> DetailValues { get; }
	}
}
