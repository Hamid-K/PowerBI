using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x0200024F RID: 591
	internal sealed class CalculationAnnotation
	{
		// Token: 0x06001477 RID: 5239 RVA: 0x0004E7B4 File Offset: 0x0004C9B4
		internal CalculationAnnotation(bool isSubtotal, Calculation subtotalTargetCalculation, SortDirection? subtotalSortDirection, IScope rollupParent, bool isMeasure, bool isNeededForQueryCalculationContext, IIdentifiable parent, bool canBeHandledByProcessing, IReadOnlyList<IScope> referencedScopes, bool isStructureAggregate, bool isSynchronizationIndex, bool isVisualCalculation)
		{
			this.IsSubtotal = isSubtotal;
			this.SubtotalTargetCalculation = subtotalTargetCalculation;
			this.SubtotalSortDirection = subtotalSortDirection;
			this.RollupParent = rollupParent;
			this.IsMeasure = isMeasure;
			this.IsSynchronizationIndex = isSynchronizationIndex;
			this.Parent = parent;
			this.CanBeHandledByProcessing = canBeHandledByProcessing;
			this.ReferencedScopes = referencedScopes;
			this.IsStructureAggregate = isStructureAggregate;
			this.IsNeededForQueryCalculationContext = isNeededForQueryCalculationContext;
			this.IsVisualCalculation = isVisualCalculation;
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001478 RID: 5240 RVA: 0x0004E824 File Offset: 0x0004CA24
		public bool IsMeasure { get; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001479 RID: 5241 RVA: 0x0004E82C File Offset: 0x0004CA2C
		public bool IsSubtotal { get; }

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600147A RID: 5242 RVA: 0x0004E834 File Offset: 0x0004CA34
		public SortDirection? SubtotalSortDirection { get; }

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600147B RID: 5243 RVA: 0x0004E83C File Offset: 0x0004CA3C
		public IScope RollupParent { get; }

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x0600147C RID: 5244 RVA: 0x0004E844 File Offset: 0x0004CA44
		public Calculation SubtotalTargetCalculation { get; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600147D RID: 5245 RVA: 0x0004E84C File Offset: 0x0004CA4C
		public bool IsNeededForQueryCalculationContext { get; }

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600147E RID: 5246 RVA: 0x0004E854 File Offset: 0x0004CA54
		public bool CanBeHandledByProcessing { get; }

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x0600147F RID: 5247 RVA: 0x0004E85C File Offset: 0x0004CA5C
		public bool IsStructureAggregate { get; }

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001480 RID: 5248 RVA: 0x0004E864 File Offset: 0x0004CA64
		public bool IsSynchronizationIndex { get; }

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x0004E86C File Offset: 0x0004CA6C
		public IIdentifiable Parent { get; }

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001482 RID: 5250 RVA: 0x0004E874 File Offset: 0x0004CA74
		public IReadOnlyList<IScope> ReferencedScopes { get; }

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x0004E87C File Offset: 0x0004CA7C
		public bool IsVisualCalculation { get; }
	}
}
