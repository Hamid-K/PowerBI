using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000210 RID: 528
	public sealed class ActionInfoWithDynamicImageMapCollection : ReportElementCollectionBase<ActionInfoWithDynamicImageMap>
	{
		// Token: 0x06001412 RID: 5138 RVA: 0x00051E00 File Offset: 0x00050000
		internal ActionInfoWithDynamicImageMapCollection()
		{
			this.m_list = new List<ActionInfoWithDynamicImageMap>();
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00051E14 File Offset: 0x00050014
		internal ActionInfoWithDynamicImageMapCollection(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, ImageMapAreasCollection imageMaps)
		{
			int count = imageMaps.Count;
			this.m_list = new List<ActionInfoWithDynamicImageMap>(count);
			for (int i = 0; i < count; i++)
			{
				ImageMapArea imageMapArea = imageMaps[i];
				if (imageMapArea != null && imageMapArea.ActionInfo != null)
				{
					ImageMapAreasCollection imageMapAreasCollection = new ImageMapAreasCollection(1);
					imageMapAreasCollection.Add(imageMapArea);
					this.m_list.Add(new ActionInfoWithDynamicImageMap(renderingContext, imageMapArea.ActionInfo, imageMapAreasCollection));
				}
			}
		}

		// Token: 0x17000AB2 RID: 2738
		public override ActionInfoWithDynamicImageMap this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				return this.m_list[index];
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06001415 RID: 5141 RVA: 0x00051ED7 File Offset: 0x000500D7
		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06001416 RID: 5142 RVA: 0x00051EE4 File Offset: 0x000500E4
		internal List<ActionInfoWithDynamicImageMap> InternalList
		{
			get
			{
				return this.m_list;
			}
		}

		// Token: 0x06001417 RID: 5143 RVA: 0x00051EEC File Offset: 0x000500EC
		internal ActionInfoWithDynamicImageMap Add(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext, Microsoft.ReportingServices.OnDemandReportRendering.ReportItem owner, IROMActionOwner romActionOwner)
		{
			ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap = new ActionInfoWithDynamicImageMap(renderingContext, owner, romActionOwner);
			this.m_list.Add(actionInfoWithDynamicImageMap);
			return actionInfoWithDynamicImageMap;
		}

		// Token: 0x06001418 RID: 5144 RVA: 0x00051F10 File Offset: 0x00050110
		internal void ConstructDefinitions()
		{
			foreach (ActionInfoWithDynamicImageMap actionInfoWithDynamicImageMap in this.m_list)
			{
				actionInfoWithDynamicImageMap.ConstructActionDefinition();
			}
		}

		// Token: 0x0400097D RID: 2429
		private List<ActionInfoWithDynamicImageMap> m_list;
	}
}
