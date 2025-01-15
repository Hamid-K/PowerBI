using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.DataShapeResultRenderer
{
	// Token: 0x02000580 RID: 1408
	internal abstract class DataShapeResultWriter
	{
		// Token: 0x0600513C RID: 20796 RVA: 0x00158F94 File Offset: 0x00157194
		public DataShapeResultWriter()
		{
			this.m_restartManager = new RestartManager();
			this.m_restartMemberMapping = new Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.DataShapeMember, Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember>();
		}

		// Token: 0x0600513D RID: 20797
		protected abstract void WriteObjectStart();

		// Token: 0x0600513E RID: 20798
		protected abstract void WriteObjectEnd();

		// Token: 0x0600513F RID: 20799
		protected abstract void WriteCollectionStart();

		// Token: 0x06005140 RID: 20800
		protected abstract void WriteCollectionEnd();

		// Token: 0x06005141 RID: 20801
		protected abstract void WritePropertyName(string name);

		// Token: 0x06005142 RID: 20802
		protected abstract void WriteValue(bool value);

		// Token: 0x06005143 RID: 20803
		protected abstract void WriteValue(string value);

		// Token: 0x06005144 RID: 20804
		protected abstract void WriteVariantValue(object value);

		// Token: 0x06005145 RID: 20805
		protected abstract void WriteRestartFlag(RestartFlag flag);

		// Token: 0x06005146 RID: 20806 RVA: 0x00158FB2 File Offset: 0x001571B2
		protected byte[] AsImageData(object value)
		{
			return value as byte[];
		}

		// Token: 0x06005147 RID: 20807 RVA: 0x00158FBA File Offset: 0x001571BA
		private void WriteIdProperty(string id)
		{
			this.WriteProperty("Id", id);
		}

		// Token: 0x06005148 RID: 20808 RVA: 0x00158FC8 File Offset: 0x001571C8
		private void WriteCalculation(DataShapeCalculation calculation)
		{
			this.WriteObjectStart();
			this.WriteIdProperty(calculation.ClientID);
			object obj = calculation.Instance.CalculationValue;
			byte[] array = this.AsImageData(obj);
			if (array != null)
			{
				obj = ImageUtility.ScaleImage(array, DataShapeResultWriter.ClientImageTargetFrameWidth, DataShapeResultWriter.ClientImageTargetFrameHeight);
			}
			this.WriteVariantProperty("Value", obj);
			this.WriteObjectEnd();
		}

		// Token: 0x06005149 RID: 20809 RVA: 0x00159024 File Offset: 0x00157224
		private void WriteCalculationCollection(DataShapeCalculationCollection calculationCollection)
		{
			if (calculationCollection != null && calculationCollection.Count > 0)
			{
				this.WritePropertyName("Calculations");
				this.WriteCollectionStart();
				foreach (DataShapeCalculation dataShapeCalculation in calculationCollection)
				{
					this.WriteCalculation(dataShapeCalculation);
				}
				this.WriteCollectionEnd();
			}
		}

		// Token: 0x0600514A RID: 20810 RVA: 0x00159090 File Offset: 0x00157290
		private void WriteDataIntersection(Microsoft.ReportingServices.OnDemandReportRendering.DataShapeIntersection dataIntersection)
		{
			this.WriteObjectStart();
			this.WriteIdProperty(dataIntersection.ClientID);
			this.WriteCalculationCollection(dataIntersection.Calculations);
			this.WriteDataShapeCollection(dataIntersection.DataShapes);
			dataIntersection.Instance.IncrementDataIntersectionLimitCounter();
			this.WriteObjectEnd();
		}

		// Token: 0x0600514B RID: 20811 RVA: 0x001590D0 File Offset: 0x001572D0
		private void WriteDataIntersections(int primaryMemberInstanceIndex, Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember, DataShapeRowCollection rowCollection)
		{
			if (dataShapeMember.Children == null)
			{
				Microsoft.ReportingServices.OnDemandReportRendering.DataShapeIntersection dataShapeIntersection = rowCollection[primaryMemberInstanceIndex][dataShapeMember.MemberCellIndex];
				this.WriteDataIntersection(dataShapeIntersection);
				return;
			}
			foreach (Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember2 in dataShapeMember.Children)
			{
				this.WriteDataIntersectionInstances(primaryMemberInstanceIndex, dataShapeMember2, rowCollection);
			}
		}

		// Token: 0x0600514C RID: 20812 RVA: 0x00159144 File Offset: 0x00157344
		private void WriteDataIntersectionInstances(int primaryMemberInstanceIndex, Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember, DataShapeRowCollection rowCollection)
		{
			if (dataShapeMember.IsStatic)
			{
				this.WriteDataIntersections(primaryMemberInstanceIndex, dataShapeMember, rowCollection);
				return;
			}
			DataShapeDynamicMemberInstance dataShapeDynamicMemberInstance = (DataShapeDynamicMemberInstance)dataShapeMember.Instance;
			dataShapeDynamicMemberInstance.ResetContext();
			while (dataShapeDynamicMemberInstance.MoveNext())
			{
				this.WriteDataIntersections(primaryMemberInstanceIndex, dataShapeMember, rowCollection);
			}
		}

		// Token: 0x0600514D RID: 20813 RVA: 0x00159188 File Offset: 0x00157388
		private void WriteSecondaryHierarchy(int primaryMemberInstanceIndex, DataShapeMemberHierarchy secondaryHierachy, DataShapeRowCollection rowCollection)
		{
			if (secondaryHierachy.MemberCollection != null)
			{
				this.WritePropertyName("Intersections");
				this.WriteCollectionStart();
				foreach (Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember in secondaryHierachy.MemberCollection)
				{
					this.WriteDataIntersectionInstances(primaryMemberInstanceIndex, dataShapeMember, rowCollection);
				}
				this.WriteCollectionEnd();
			}
		}

		// Token: 0x0600514E RID: 20814 RVA: 0x001591F8 File Offset: 0x001573F8
		private void WriteScopeValue(ScopeValue scopeValue)
		{
			this.WriteObjectStart();
			this.WriteVariantProperty("Value", scopeValue.Value);
			this.WriteProperty("Key", scopeValue.Key);
			this.WriteObjectEnd();
		}

		// Token: 0x0600514F RID: 20815 RVA: 0x00159228 File Offset: 0x00157428
		private void WriteScopeID(ScopeID scopeID)
		{
			this.WritePropertyName("ScopeId");
			this.WriteObjectStart();
			this.WritePropertyName("ScopeValues");
			this.WriteCollectionStart();
			for (int i = 0; i < scopeID.ScopeValueCount; i++)
			{
				ScopeValue scopeValue = scopeID.GetScopeValue(i);
				this.WriteScopeValue(scopeValue);
			}
			this.WriteCollectionEnd();
			this.WriteObjectEnd();
		}

		// Token: 0x06005150 RID: 20816 RVA: 0x00159284 File Offset: 0x00157484
		private void WriteGroup(DataShapeDynamicMemberInstance dynamicMemberInstance)
		{
			if (dynamicMemberInstance != null)
			{
				ScopeID scopeID = dynamicMemberInstance.GetScopeID();
				if (scopeID == null)
				{
					return;
				}
				this.WritePropertyName("Group");
				this.WriteObjectStart();
				this.WriteScopeID(scopeID);
				this.WriteObjectEnd();
			}
		}

		// Token: 0x06005151 RID: 20817 RVA: 0x001592C4 File Offset: 0x001574C4
		private void WriteDataShapeMemberInstance(Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember, DataShapeMemberHierarchy secondaryHierachy, DataShapeRowCollection rowCollection)
		{
			DataShapeDynamicMemberInstance dataShapeDynamicMemberInstance = dataShapeMember.Instance as DataShapeDynamicMemberInstance;
			this.WriteObjectStart();
			this.WriteGroup(dataShapeDynamicMemberInstance);
			this.WriteRestartFlag(dataShapeMember);
			this.WriteCalculationCollection(dataShapeMember.Calculations);
			this.WriteDataShapeCollection(dataShapeMember.DataShapes);
			if (dataShapeMember.Children == null)
			{
				if (secondaryHierachy != null)
				{
					this.WriteSecondaryHierarchy(dataShapeMember.MemberCellIndex, secondaryHierachy, rowCollection);
				}
			}
			else
			{
				this.WriteMemberCollection("Members", dataShapeMember.Children, secondaryHierachy, rowCollection);
			}
			this.WriteObjectEnd();
		}

		// Token: 0x06005152 RID: 20818 RVA: 0x00159340 File Offset: 0x00157540
		private void WriteRestartFlag(Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember)
		{
			DataShapeDynamicMemberInstance dataShapeDynamicMemberInstance = dataShapeMember.Instance as DataShapeDynamicMemberInstance;
			if (dataShapeMember.RifDataShapeMemberDefinition.Grouping != null && dataShapeMember.RifDataShapeMemberDefinition.Grouping.StartPositions != null)
			{
				ScopeID lastScopeID = dataShapeDynamicMemberInstance.GetLastScopeID();
				if (lastScopeID != null)
				{
					RestartFlag restartFlag = this.m_restartManager.GetRestartFlag(lastScopeID, dataShapeMember.RifDataShapeMemberDefinition.Grouping.StartPositions, dataShapeMember.OwnerDataShape.RenderingContext.OdpContext.ProcessingComparer);
					if (restartFlag != RestartFlag.Append)
					{
						this.WritePropertyName("RestartFlag");
						this.WriteRestartFlag(restartFlag);
					}
				}
			}
		}

		// Token: 0x06005153 RID: 20819 RVA: 0x001593D0 File Offset: 0x001575D0
		private void WriteDataShapeMember(Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember, DataShapeMemberHierarchy secondaryHierarchy, DataShapeRowCollection rowCollection)
		{
			if (dataShapeMember.IsStatic && !dataShapeMember.OwnerDataShape.RenderingContext.SegmentationManager.CanRender(dataShapeMember))
			{
				return;
			}
			this.WriteObjectStart();
			this.WriteIdProperty(dataShapeMember.ClientID);
			this.WritePropertyName("Instances");
			this.WriteCollectionStart();
			if (dataShapeMember.IsStatic)
			{
				this.WriteDataShapeMemberInstance(dataShapeMember, secondaryHierarchy, rowCollection);
			}
			else
			{
				DataShapeDynamicMemberInstance dataShapeDynamicMemberInstance = (DataShapeDynamicMemberInstance)dataShapeMember.Instance;
				dataShapeDynamicMemberInstance.ResetContext();
				while (dataShapeDynamicMemberInstance.MoveNext())
				{
					this.WriteDataShapeMemberInstance(dataShapeMember, secondaryHierarchy, rowCollection);
				}
			}
			this.WriteCollectionEnd();
			this.WriteObjectEnd();
			if (dataShapeMember.HasRestartDefinition() && !this.m_restartMemberMapping.ContainsKey(dataShapeMember.RifDataShapeMemberDefinition))
			{
				this.m_restartMemberMapping.Add(dataShapeMember.RifDataShapeMemberDefinition, dataShapeMember);
			}
		}

		// Token: 0x06005154 RID: 20820 RVA: 0x00159494 File Offset: 0x00157694
		private void WriteMemberCollection(string collectionName, DataShapeMemberCollection memberCollection, DataShapeMemberHierarchy secondaryHierachy, DataShapeRowCollection rowCollection)
		{
			this.WritePropertyName(collectionName);
			this.WriteCollectionStart();
			foreach (Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember dataShapeMember in memberCollection)
			{
				this.WriteDataShapeMember(dataShapeMember, secondaryHierachy, rowCollection);
			}
			this.WriteCollectionEnd();
		}

		// Token: 0x06005155 RID: 20821 RVA: 0x001594F4 File Offset: 0x001576F4
		private void WriteMemberHierarchy(string collectionName, DataShapeMemberHierarchy primaryHierachy, DataShapeMemberHierarchy secondaryHierachy, DataShapeRowCollection rowCollection)
		{
			if (primaryHierachy.MemberCollection != null)
			{
				this.WriteMemberCollection(collectionName, primaryHierachy.MemberCollection, secondaryHierachy, rowCollection);
			}
		}

		// Token: 0x06005156 RID: 20822 RVA: 0x0015950E File Offset: 0x0015770E
		private void WriteSecondaryHierachy(DataShapeMemberHierarchy memberHierarchy)
		{
			this.WriteMemberHierarchy("SecondaryHierarchy", memberHierarchy, null, null);
		}

		// Token: 0x06005157 RID: 20823 RVA: 0x00159520 File Offset: 0x00157720
		private void WriteDataShapeMessage(DataShapeErrorMessage message)
		{
			this.WriteObjectStart();
			this.WriteProperty("Code", message.ErrorCode);
			this.WriteProperty("Severity", message.Severity.ToString());
			this.WriteProperty("Message", message.Message);
			this.WriteProperty("ObjectType", message.ObjectType);
			this.WriteProperty("ObjectName", message.ObjectName);
			this.WriteProperty("PropertyName", message.PropertyName);
			this.WriteObjectEnd();
		}

		// Token: 0x06005158 RID: 20824 RVA: 0x001595B0 File Offset: 0x001577B0
		private void WriteDataShapeMessageCollection(DataShapeMessageCollection messageCollection)
		{
			if (messageCollection != null && messageCollection.Count > 0)
			{
				this.WritePropertyName("DataShapeMessages");
				this.WriteCollectionStart();
				foreach (DataShapeErrorMessage dataShapeErrorMessage in messageCollection)
				{
					this.WriteDataShapeMessage(dataShapeErrorMessage);
				}
				this.WriteCollectionEnd();
			}
		}

		// Token: 0x06005159 RID: 20825 RVA: 0x0015961C File Offset: 0x0015781C
		private void WriteDataLimitsExceeded(List<Microsoft.ReportingServices.OnDemandReportRendering.DataShapeLimit> limits)
		{
			if (limits != null && limits.Count > 0)
			{
				if (limits.Any((Microsoft.ReportingServices.OnDemandReportRendering.DataShapeLimit p) => p.Exceeded))
				{
					this.WritePropertyName("DataLimitsExceeded");
					this.WriteCollectionStart();
					foreach (Microsoft.ReportingServices.OnDemandReportRendering.DataShapeLimit dataShapeLimit in limits.Where((Microsoft.ReportingServices.OnDemandReportRendering.DataShapeLimit p) => p.Exceeded))
					{
						this.WriteObjectStart();
						this.WriteIdProperty(dataShapeLimit.ClientID);
						this.WriteObjectEnd();
					}
					this.WriteCollectionEnd();
				}
			}
		}

		// Token: 0x0600515A RID: 20826 RVA: 0x001596EC File Offset: 0x001578EC
		private void WriteRestartTokens(List<RestartDefinition> restartDefinitions)
		{
			if (restartDefinitions.Count == 0)
			{
				return;
			}
			this.WritePropertyName("RestartTokens");
			this.WriteCollectionStart();
			foreach (RestartDefinition restartDefinition in restartDefinitions)
			{
				this.WriteCollectionStart();
				if (restartDefinition.IsTotal)
				{
					this.WriteVariantValue(false);
				}
				else
				{
					ScopeID lastScopeID = ((DataShapeDynamicMemberInstance)this.m_restartMemberMapping[restartDefinition.DataMember].Instance).GetLastScopeID();
					if (lastScopeID != null)
					{
						for (int i = 0; i < lastScopeID.ScopeValueCount; i++)
						{
							ScopeValue scopeValue = lastScopeID.GetScopeValue(i);
							this.WriteVariantValue(scopeValue.Value);
						}
					}
				}
				this.WriteCollectionEnd();
			}
			this.WriteCollectionEnd();
		}

		// Token: 0x0600515B RID: 20827 RVA: 0x001597CC File Offset: 0x001579CC
		private void WriteDataShape(Microsoft.ReportingServices.OnDemandReportRendering.DataShape dataShape)
		{
			this.WriteObjectStart();
			this.WriteIdProperty(dataShape.ClientID);
			this.WriteDataShapeMessageCollection(dataShape.Messages);
			this.WriteCalculationCollection(dataShape.Calculations);
			this.WriteDataShapeCollection(dataShape.DataShapes);
			this.WriteSecondaryHierachy(dataShape.ColumnHierarchy);
			dataShape.PrepareRowHierarchy();
			this.WriteMemberHierarchy("PrimaryHierarchy", dataShape.RowHierarchy, dataShape.ColumnHierarchy, dataShape.DataRows);
			this.WriteDataLimitsExceeded(dataShape.Limits);
			this.WriteProperty("IsComplete", dataShape.IsComplete);
			if (dataShape.RifDataShapeDefinition.RestartDefinitions != null && !dataShape.IsComplete)
			{
				this.WriteRestartTokens(dataShape.RifDataShapeDefinition.RestartDefinitions);
			}
			this.WriteObjectEnd();
		}

		// Token: 0x0600515C RID: 20828 RVA: 0x00159888 File Offset: 0x00157A88
		private void WriteDataShapeCollection(DataShapeCollection dataShapes)
		{
			if (dataShapes == null || dataShapes.Count == 0)
			{
				return;
			}
			this.WritePropertyName("DataShapes");
			this.WriteCollectionStart();
			foreach (Microsoft.ReportingServices.OnDemandReportRendering.DataShape dataShape in dataShapes)
			{
				this.WriteDataShape(dataShape);
			}
			this.WriteCollectionEnd();
		}

		// Token: 0x0600515D RID: 20829 RVA: 0x001598F4 File Offset: 0x00157AF4
		public void WriteProperty(string name, bool value)
		{
			this.WritePropertyName(name);
			this.WriteValue(value);
		}

		// Token: 0x0600515E RID: 20830 RVA: 0x00159904 File Offset: 0x00157B04
		public void WriteProperty(string name, string value)
		{
			this.WritePropertyName(name);
			this.WriteValue(value);
		}

		// Token: 0x0600515F RID: 20831 RVA: 0x00159914 File Offset: 0x00157B14
		public void WriteVariantProperty(string name, object value)
		{
			this.WritePropertyName(name);
			this.WriteVariantValue(value);
		}

		// Token: 0x06005160 RID: 20832 RVA: 0x00159924 File Offset: 0x00157B24
		public void WriteDataShapeResult(Microsoft.ReportingServices.OnDemandReportRendering.DataShape dataShape)
		{
			this.WriteDataShape(dataShape);
		}

		// Token: 0x040028F3 RID: 10483
		private const string PropertyNameId = "Id";

		// Token: 0x040028F4 RID: 10484
		private const string PropertyNameName = "Name";

		// Token: 0x040028F5 RID: 10485
		private const string PropertyNameGroup = "Group";

		// Token: 0x040028F6 RID: 10486
		private const string PropertyNameScopeId = "ScopeId";

		// Token: 0x040028F7 RID: 10487
		private const string PropertyNameValue = "Value";

		// Token: 0x040028F8 RID: 10488
		private const string PropertyNameScopeValueKey = "Key";

		// Token: 0x040028F9 RID: 10489
		private const string PropertyNameScopeValueType = "Type";

		// Token: 0x040028FA RID: 10490
		private const string PropertyNameIsComplete = "IsComplete";

		// Token: 0x040028FB RID: 10491
		private const string PropertyNameCode = "Code";

		// Token: 0x040028FC RID: 10492
		private const string PropertyNameSeverity = "Severity";

		// Token: 0x040028FD RID: 10493
		private const string PropertyNameMessage = "Message";

		// Token: 0x040028FE RID: 10494
		private const string PropertyNameObjectType = "ObjectType";

		// Token: 0x040028FF RID: 10495
		private const string PropertyNameObjectName = "ObjectName";

		// Token: 0x04002900 RID: 10496
		private const string PropertyNamePropertyName = "PropertyName";

		// Token: 0x04002901 RID: 10497
		private const string PropertyNameRestartFlag = "RestartFlag";

		// Token: 0x04002902 RID: 10498
		private const string CollectionNamePrimaryHierarchy = "PrimaryHierarchy";

		// Token: 0x04002903 RID: 10499
		private const string CollectionNameSecondaryHierarchy = "SecondaryHierarchy";

		// Token: 0x04002904 RID: 10500
		private const string CollectionNameInstances = "Instances";

		// Token: 0x04002905 RID: 10501
		private const string CollectionNameMembers = "Members";

		// Token: 0x04002906 RID: 10502
		private const string CollectionNameIntersections = "Intersections";

		// Token: 0x04002907 RID: 10503
		private const string CollectionNameCalculations = "Calculations";

		// Token: 0x04002908 RID: 10504
		private const string CollectionNameScopeValues = "ScopeValues";

		// Token: 0x04002909 RID: 10505
		private const string CollectionNameDataShapeMessages = "DataShapeMessages";

		// Token: 0x0400290A RID: 10506
		private const string CollectionNameDataLimitsExceeded = "DataLimitsExceeded";

		// Token: 0x0400290B RID: 10507
		private const string CollectionNameDataShapes = "DataShapes";

		// Token: 0x0400290C RID: 10508
		private const string CollectionNameRestartTokens = "RestartTokens";

		// Token: 0x0400290D RID: 10509
		private static readonly RVUnit ClientImageTargetFrameWidth = new RVUnit("1.5in", CultureInfo.InvariantCulture);

		// Token: 0x0400290E RID: 10510
		private static readonly RVUnit ClientImageTargetFrameHeight = new RVUnit("1.5in", CultureInfo.InvariantCulture);

		// Token: 0x0400290F RID: 10511
		private readonly RestartManager m_restartManager;

		// Token: 0x04002910 RID: 10512
		private readonly Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.DataShapeMember, Microsoft.ReportingServices.OnDemandReportRendering.DataShapeMember> m_restartMemberMapping;
	}
}
