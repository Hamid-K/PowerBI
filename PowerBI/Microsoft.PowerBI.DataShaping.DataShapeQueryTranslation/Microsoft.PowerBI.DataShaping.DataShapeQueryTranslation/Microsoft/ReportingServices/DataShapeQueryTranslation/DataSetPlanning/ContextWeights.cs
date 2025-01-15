using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000DB RID: 219
	internal sealed class ContextWeights : DataShapeVisitor
	{
		// Token: 0x06000902 RID: 2306 RVA: 0x00022FB8 File Offset: 0x000211B8
		private ContextWeights(DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			this.m_weights = new Dictionary<IContextItem, int>();
			this.m_annotations = annotations;
			this.m_scopeTree = scopeTree;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00022FD9 File Offset: 0x000211D9
		public static ContextWeights DetermineWeights(DataShape dataShape, DataShapeAnnotations annotations, ScopeTree scopeTree)
		{
			ContextWeights contextWeights = new ContextWeights(annotations, scopeTree);
			contextWeights.Visit(dataShape);
			contextWeights.PostProcessSyncGroupWeights();
			return contextWeights;
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x00022FF0 File Offset: 0x000211F0
		private void PostProcessSyncGroupWeights()
		{
			foreach (KeyValuePair<IContextItem, int> keyValuePair in this.m_weights.ToDictionary((KeyValuePair<IContextItem, int> k) => k.Key, (KeyValuePair<IContextItem, int> v) => v.Value))
			{
				if (keyValuePair.Value < 0)
				{
					this.m_weights[keyValuePair.Key] = this.m_positiveWeightsCount + (keyValuePair.Value - int.MinValue);
				}
			}
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000230B0 File Offset: 0x000212B0
		public int GetWeight(IContextItem item)
		{
			return this.m_weights[item];
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000230C0 File Offset: 0x000212C0
		protected override void Visit(DataShape dataShape)
		{
			this.Enter(dataShape);
			base.Visit<Calculation>(dataShape.Calculations, new Action<Calculation>(this.Visit));
			base.Visit<DataShape>(dataShape.DataShapes, new Action<DataShape>(this.Visit));
			this.TraverseDataShapeStructure(dataShape);
			this.Exit(dataShape);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00023114 File Offset: 0x00021314
		protected override void TraverseDataShapeStructure(DataShape dataShape)
		{
			base.Visit(dataShape.PrimaryHierarchy);
			base.Visit(dataShape.SecondaryHierarchy);
			base.Visit<DataRow>(dataShape.DataRows, new Action<DataRow>(base.Visit));
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00023148 File Offset: 0x00021348
		protected override void Visit(DataMember dataMember)
		{
			this.Enter(dataMember);
			base.Visit<Calculation>(dataMember.Calculations, new Action<Calculation>(this.Visit));
			base.Visit<DataShape>(dataMember.DataShapes, new Action<DataShape>(this.Visit));
			this.TraverseDataMemberStructure(dataMember);
			this.Exit(dataMember);
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x0002319C File Offset: 0x0002139C
		protected override void TraverseDataMemberStructure(DataMember dataMember)
		{
			base.Visit<DataMember>(dataMember.DataMembers, new Action<DataMember>(this.Visit));
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x000231B8 File Offset: 0x000213B8
		protected override void Visit(DataIntersection dataIntersection)
		{
			this.Enter(dataIntersection);
			base.Visit<Calculation>(dataIntersection.Calculations, new Action<Calculation>(this.Visit));
			base.Visit<DataShape>(dataIntersection.DataShapes, new Action<DataShape>(this.Visit));
			this.Exit(dataIntersection);
		}

		// Token: 0x0600090B RID: 2315 RVA: 0x00023205 File Offset: 0x00021405
		protected override void Enter(DataShape dataShape)
		{
			this.AssignWeight(dataShape);
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x0002320E File Offset: 0x0002140E
		protected override void Enter(DataMember dataMember)
		{
			this.AssignWeight(dataMember);
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00023217 File Offset: 0x00021417
		protected override void Enter(DataIntersection dataIntersection)
		{
			this.AssignWeight(dataIntersection);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00023220 File Offset: 0x00021420
		protected override void Visit(Calculation calculation)
		{
			this.AssignWeight(calculation);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00023229 File Offset: 0x00021429
		private void AssignWeight(IContextItem item)
		{
			this.m_weights.Add(item, this.m_positiveWeightsCount);
			this.m_positiveWeightsCount++;
		}

		// Token: 0x04000447 RID: 1095
		private const int MinSyncGroupWeight = -2147483648;

		// Token: 0x04000448 RID: 1096
		private readonly Dictionary<IContextItem, int> m_weights;

		// Token: 0x04000449 RID: 1097
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x0400044A RID: 1098
		private readonly ScopeTree m_scopeTree;

		// Token: 0x0400044B RID: 1099
		private int m_positiveWeightsCount;
	}
}
