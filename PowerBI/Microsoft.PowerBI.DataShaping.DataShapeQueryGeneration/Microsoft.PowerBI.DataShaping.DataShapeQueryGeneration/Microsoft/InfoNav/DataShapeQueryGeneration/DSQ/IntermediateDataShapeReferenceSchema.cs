using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions;

namespace Microsoft.InfoNav.DataShapeQueryGeneration.DSQ
{
	// Token: 0x02000109 RID: 265
	[DataContract]
	internal sealed class IntermediateDataShapeReferenceSchema
	{
		// Token: 0x060008BA RID: 2234 RVA: 0x00023284 File Offset: 0x00021484
		internal IntermediateDataShapeReferenceSchema(ExpressionNode dsqReferenceExpression, IReadOnlyList<IntermediateGroupSchema> primaryGroups, IReadOnlyList<IntermediateGroupSchema> secondaryGroups)
		{
			this.DsqReferenceExpression = dsqReferenceExpression;
			this.PrimaryGroups = primaryGroups;
			this.SecondaryGroups = secondaryGroups;
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x000232A1 File Offset: 0x000214A1
		internal IntermediateGroupSchema GetGrouping(int groupingIndex, bool isPrimary)
		{
			if (isPrimary)
			{
				return this.PrimaryGroups[groupingIndex];
			}
			return this.SecondaryGroups[groupingIndex];
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000232BF File Offset: 0x000214BF
		internal ExpressionNode DsqReferenceExpression { get; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008BD RID: 2237 RVA: 0x000232C7 File Offset: 0x000214C7
		[DataMember(Name = "DsqReferenceExpression", Order = 1)]
		private string DsqReferenceExpressionForSerialization
		{
			get
			{
				return this.DsqReferenceExpression.ToString();
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x000232D4 File Offset: 0x000214D4
		[DataMember(Order = 2, EmitDefaultValue = false)]
		private IReadOnlyList<IntermediateGroupSchema> PrimaryGroups { get; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008BF RID: 2239 RVA: 0x000232DC File Offset: 0x000214DC
		[DataMember(Order = 3, EmitDefaultValue = false)]
		private IReadOnlyList<IntermediateGroupSchema> SecondaryGroups { get; }
	}
}
