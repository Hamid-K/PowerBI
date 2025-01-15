using System;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200030C RID: 780
	internal class ROMInstanceObjectCreator : PersistenceHelper, IRIFObjectCreator
	{
		// Token: 0x06001CC2 RID: 7362 RVA: 0x0007262E File Offset: 0x0007082E
		internal ROMInstanceObjectCreator(ReportItemInstance reportItemInstance)
		{
			this.m_reportItemInstance = reportItemInstance;
		}

		// Token: 0x06001CC3 RID: 7363 RVA: 0x0007263D File Offset: 0x0007083D
		internal void StartActionInfoInstancesDeserialization(ActionInfo actionInfo)
		{
			this.m_currentActionInfo = actionInfo;
			this.m_currentActionIndex = 0;
		}

		// Token: 0x06001CC4 RID: 7364 RVA: 0x0007264D File Offset: 0x0007084D
		internal void CompleteActionInfoInstancesDeserialization()
		{
			this.m_currentActionInfo = null;
			this.m_currentActionIndex = 0;
		}

		// Token: 0x06001CC5 RID: 7365 RVA: 0x0007265D File Offset: 0x0007085D
		internal void StartParameterInstancesDeserialization(ParameterCollection paramCollection)
		{
			this.m_currentParameterCollection = paramCollection;
			this.m_currentParameterIndex = 0;
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x0007266D File Offset: 0x0007086D
		internal void CompleteParameterInstancesDeserialization()
		{
			this.m_currentParameterCollection = null;
			this.m_currentParameterIndex = 0;
		}

		// Token: 0x06001CC7 RID: 7367 RVA: 0x00072680 File Offset: 0x00070880
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable CreateRIFObject(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType objectType, ref Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader context)
		{
			if (objectType == Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Null)
			{
				return null;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistable;
			switch (objectType)
			{
			case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageInstance:
				persistable = (ImageInstance)this.m_reportItemInstance;
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInstance:
				Global.Tracer.Assert(this.m_currentActionInfo != null && this.m_currentActionInfo.Actions.Count > this.m_currentActionIndex, "Ensure m_currentActionInfo is setup properly");
				persistable = this.m_currentActionInfo.Actions[this.m_currentActionIndex].Instance;
				this.m_currentActionIndex++;
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInstance:
				Global.Tracer.Assert(this.m_currentParameterCollection != null && this.m_currentParameterCollection.Count > this.m_currentParameterIndex, "Ensure m_currentParameterCollection is setup properly");
				persistable = this.m_currentParameterCollection[this.m_currentParameterIndex].Instance;
				this.m_currentParameterIndex++;
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ActionInfoWithDynamicImageMap:
				persistable = new ActionInfoWithDynamicImageMap(this.m_reportItemInstance.RenderingContext, null, (ReportItem)this.m_reportItemInstance.ReportElementDef, (IROMActionOwner)this.m_reportItemInstance.ReportElementDef);
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ImageMapAreaInstance:
				persistable = new ImageMapAreaInstance();
				break;
			case Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StyleInstance:
				persistable = this.m_reportItemInstance.Style;
				break;
			default:
				return this.ProcessingRIFObjectCreator.CreateRIFObject(objectType, ref context);
			}
			persistable.Deserialize(context);
			return persistable;
		}

		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x06001CC8 RID: 7368 RVA: 0x000727E9 File Offset: 0x000709E9
		private IRIFObjectCreator ProcessingRIFObjectCreator
		{
			get
			{
				if (this.__processingRIFObjectCreator == null)
				{
					this.__processingRIFObjectCreator = new ProcessingRIFObjectCreator(null, null);
				}
				return this.__processingRIFObjectCreator;
			}
		}

		// Token: 0x04000F15 RID: 3861
		private ReportItemInstance m_reportItemInstance;

		// Token: 0x04000F16 RID: 3862
		private ProcessingRIFObjectCreator __processingRIFObjectCreator;

		// Token: 0x04000F17 RID: 3863
		private ActionInfo m_currentActionInfo;

		// Token: 0x04000F18 RID: 3864
		private int m_currentActionIndex;

		// Token: 0x04000F19 RID: 3865
		private ParameterCollection m_currentParameterCollection;

		// Token: 0x04000F1A RID: 3866
		private int m_currentParameterIndex;
	}
}
