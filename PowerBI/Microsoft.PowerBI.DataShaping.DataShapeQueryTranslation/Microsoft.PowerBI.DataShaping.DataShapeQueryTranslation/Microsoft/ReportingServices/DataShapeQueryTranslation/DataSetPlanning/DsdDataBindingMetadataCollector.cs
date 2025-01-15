using System;
using System.Collections.Generic;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQuery;
using Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataSetPlanning
{
	// Token: 0x020000FF RID: 255
	internal sealed class DsdDataBindingMetadataCollector : DataShapeVisitor
	{
		// Token: 0x06000A13 RID: 2579 RVA: 0x00027064 File Offset: 0x00025264
		private DsdDataBindingMetadataCollector(DataShapeAnnotations annotations, OutputPlanMapping outputPlanMapping)
		{
			this.m_annotations = annotations;
			this.m_outputPlanMapping = outputPlanMapping;
			this.m_bindingInfoCollector = new BindingInformationCollector();
			this.m_secondaryLeaves = new List<DataMember>();
			this.m_itemBindingIndices = new Dictionary<IScope, int>();
			this.m_secondaryStaticToDynamicParentMapping = new Dictionary<DataMember, DataMember>();
			this.m_itemsWithEnforcedBinding = new HashSet<IScope>();
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000270BC File Offset: 0x000252BC
		public static DsdDataBindingMetadata Collect(DataShapeContext dataShapeContext, DataShapeAnnotations annotations, OutputPlanMapping outputPlanMapping)
		{
			DsdDataBindingMetadataCollector dsdDataBindingMetadataCollector = new DsdDataBindingMetadataCollector(annotations, outputPlanMapping);
			dsdDataBindingMetadataCollector.Collect(dataShapeContext);
			return new DsdDataBindingMetadata(dsdDataBindingMetadataCollector.m_itemBindingIndices, dsdDataBindingMetadataCollector.m_itemsWithEnforcedBinding, dsdDataBindingMetadataCollector.m_secondaryLeaves, dsdDataBindingMetadataCollector.m_secondaryStaticToDynamicParentMapping);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000270F5 File Offset: 0x000252F5
		private void Collect(DataShapeContext dataShapeContext)
		{
			this.m_dataShapeContext = dataShapeContext;
			this.Visit(dataShapeContext.DataShape);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002710C File Offset: 0x0002530C
		protected override void Visit(DataShape dataShape)
		{
			if (dataShape.ContextOnly.GetValueOrDefault<bool>())
			{
				return;
			}
			DataShape currentDataShape = this.m_currentDataShape;
			this.m_currentDataShape = dataShape;
			int num;
			if (!this.m_outputPlanMapping.TryGetValue(dataShape, out num))
			{
				Contract.RetailFail(" DataShape should always have a binding");
			}
			this.m_bindingInfoCollector.StartRecording(dataShape);
			base.Visit<Calculation>(dataShape.Calculations, new Action<Calculation>(this.Visit));
			int num2 = this.m_bindingInfoCollector.StopRecording(dataShape);
			if (this.m_bindingInfoCollector.IsValidBindingIndex(num2))
			{
				num = num2;
			}
			this.m_itemBindingIndices.Add(dataShape, num);
			base.Visit<DataShape>(dataShape.DataShapes, new Action<DataShape>(this.Visit));
			this.Visit(dataShape.SecondaryHierarchy, true);
			this.Visit(dataShape.PrimaryHierarchy, false);
			this.m_currentDataShape = currentDataShape;
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x000271D8 File Offset: 0x000253D8
		private void Visit(DataHierarchy hierarchy, bool isSecondary)
		{
			if (hierarchy == null)
			{
				return;
			}
			this.Visit(hierarchy.DataMembers, isSecondary, null);
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x000271EC File Offset: 0x000253EC
		private void Visit(IList<DataMember> dataMembers, bool isSecondary, DataMember parentDynamicMember)
		{
			if (dataMembers == null)
			{
				return;
			}
			foreach (DataMember dataMember in dataMembers)
			{
				this.Visit(dataMember, isSecondary, parentDynamicMember);
			}
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002723C File Offset: 0x0002543C
		private void Visit(DataMember dataMember, bool isSecondary, DataMember parentDynamicMember)
		{
			int num;
			bool flag = this.m_outputPlanMapping.TryGetValue(dataMember, out num);
			if (isSecondary && !dataMember.ContextOnly)
			{
				if (this.m_annotations.IsLeaf(dataMember))
				{
					this.m_secondaryLeaves.Add(dataMember);
				}
				if (!dataMember.IsDynamic)
				{
					this.m_secondaryStaticToDynamicParentMapping.Add(dataMember, parentDynamicMember);
				}
			}
			if (!flag)
			{
				this.m_bindingInfoCollector.StartRecording(dataMember);
			}
			base.Visit<Calculation>(dataMember.Calculations, new Action<Calculation>(this.Visit));
			DataMember dataMember2 = ((dataMember.IsDynamic && !dataMember.ContextOnly) ? dataMember : parentDynamicMember);
			this.Visit(dataMember.DataMembers, isSecondary, dataMember2);
			if (!isSecondary && !dataMember.ContextOnly && this.m_annotations.IsLeaf(dataMember) && this.m_currentDataShape.DataRows != null)
			{
				this.VisitRow(dataMember);
			}
			if (!flag)
			{
				num = this.m_bindingInfoCollector.StopRecording(dataMember);
				if (!this.m_bindingInfoCollector.IsValidBindingIndex(num) && (parentDynamicMember == null || !this.m_outputPlanMapping.TryGetValue(parentDynamicMember, out num)))
				{
					this.m_outputPlanMapping.TryGetValue(this.m_currentDataShape, out num);
				}
			}
			this.m_itemBindingIndices.Add(dataMember, num);
			this.m_bindingInfoCollector.RecordItemPlanIndex(num);
			if (!isSecondary && !dataMember.IsDynamic && parentDynamicMember == null && this.m_dataShapeContext.HasAnySecondaryDynamic)
			{
				this.m_itemsWithEnforcedBinding.Add(dataMember);
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00027390 File Offset: 0x00025590
		private void VisitRow(DataMember dataMember)
		{
			DataRow dataRow = this.m_currentDataShape.DataRows[this.m_annotations.GetLeafIndex(dataMember)];
			for (int i = 0; i < dataRow.Intersections.Count; i++)
			{
				this.Visit(dataRow.Intersections[i], i);
			}
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000273E4 File Offset: 0x000255E4
		private void Visit(DataIntersection dataIntersection, int secondaryParentLeafIndex)
		{
			int num;
			bool flag = this.m_outputPlanMapping.TryGetValue(dataIntersection, out num);
			if (!flag)
			{
				this.m_bindingInfoCollector.StartRecording(dataIntersection);
			}
			base.Visit<Calculation>(dataIntersection.Calculations, new Action<Calculation>(this.Visit));
			base.Visit<DataShape>(dataIntersection.DataShapes, new Action<DataShape>(this.Visit));
			if (!flag)
			{
				num = this.m_bindingInfoCollector.StopRecording(dataIntersection);
				flag = this.m_bindingInfoCollector.IsValidBindingIndex(num);
			}
			if (flag)
			{
				if (this.m_dataShapeContext.HasAnySecondaryDynamic)
				{
					this.m_itemBindingIndices.Add(dataIntersection, num);
				}
				this.m_bindingInfoCollector.RecordItemPlanIndex(num);
			}
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00027488 File Offset: 0x00025688
		protected override void Visit(Calculation calculation)
		{
			int num;
			if (this.m_outputPlanMapping.TryGetValue(calculation, out num) && !this.m_annotations.CanBeHandledByProcessing(calculation))
			{
				this.m_bindingInfoCollector.RecordItemPlanIndex(num);
			}
		}

		// Token: 0x040004D9 RID: 1241
		private readonly DataShapeAnnotations m_annotations;

		// Token: 0x040004DA RID: 1242
		private readonly OutputPlanMapping m_outputPlanMapping;

		// Token: 0x040004DB RID: 1243
		private readonly BindingInformationCollector m_bindingInfoCollector;

		// Token: 0x040004DC RID: 1244
		private readonly List<DataMember> m_secondaryLeaves;

		// Token: 0x040004DD RID: 1245
		private readonly Dictionary<DataMember, DataMember> m_secondaryStaticToDynamicParentMapping;

		// Token: 0x040004DE RID: 1246
		private readonly Dictionary<IScope, int> m_itemBindingIndices;

		// Token: 0x040004DF RID: 1247
		private readonly HashSet<IScope> m_itemsWithEnforcedBinding;

		// Token: 0x040004E0 RID: 1248
		private DataShapeContext m_dataShapeContext;

		// Token: 0x040004E1 RID: 1249
		private DataShape m_currentDataShape;
	}
}
