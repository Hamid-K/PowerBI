using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000334 RID: 820
	internal sealed class ShimImageInstance : ImageInstance
	{
		// Token: 0x06001EA2 RID: 7842 RVA: 0x000766D6 File Offset: 0x000748D6
		internal ShimImageInstance(Microsoft.ReportingServices.OnDemandReportRendering.Image reportItemDef)
			: base(reportItemDef)
		{
		}

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06001EA3 RID: 7843 RVA: 0x000766DF File Offset: 0x000748DF
		// (set) Token: 0x06001EA4 RID: 7844 RVA: 0x000766F6 File Offset: 0x000748F6
		public override byte[] ImageData
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportRendering.Image)this.m_reportElementDef.RenderReportItem).ImageData;
			}
			set
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
			}
		}

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06001EA5 RID: 7845 RVA: 0x00076702 File Offset: 0x00074902
		// (set) Token: 0x06001EA6 RID: 7846 RVA: 0x00076719 File Offset: 0x00074919
		public override string StreamName
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportRendering.Image)this.m_reportElementDef.RenderReportItem).StreamName;
			}
			internal set
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
			}
		}

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06001EA7 RID: 7847 RVA: 0x00076725 File Offset: 0x00074925
		// (set) Token: 0x06001EA8 RID: 7848 RVA: 0x0007673C File Offset: 0x0007493C
		public override string MIMEType
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportRendering.Image)this.m_reportElementDef.RenderReportItem).MIMEType;
			}
			set
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
			}
		}

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06001EA9 RID: 7849 RVA: 0x00076748 File Offset: 0x00074948
		internal override string ImageDataId
		{
			get
			{
				return this.StreamName;
			}
		}

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06001EAA RID: 7850 RVA: 0x00076750 File Offset: 0x00074950
		public override TypeCode TagDataType
		{
			get
			{
				return TypeCode.Empty;
			}
		}

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x06001EAB RID: 7851 RVA: 0x00076753 File Offset: 0x00074953
		public override object Tag
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x06001EAC RID: 7852 RVA: 0x00076758 File Offset: 0x00074958
		public override ActionInfoWithDynamicImageMapCollection ActionInfoWithDynamicImageMapAreas
		{
			get
			{
				if (this.m_actionInfoImageMapAreas == null && ((Microsoft.ReportingServices.ReportRendering.Image)this.m_reportElementDef.RenderReportItem).ImageMap != null && 0 < ((Microsoft.ReportingServices.ReportRendering.Image)this.m_reportElementDef.RenderReportItem).ImageMap.Count)
				{
					this.m_actionInfoImageMapAreas = new ActionInfoWithDynamicImageMapCollection(this.m_reportElementDef.RenderingContext, ((Microsoft.ReportingServices.ReportRendering.Image)this.m_reportElementDef.RenderReportItem).ImageMap);
				}
				return this.m_actionInfoImageMapAreas;
			}
		}

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06001EAD RID: 7853 RVA: 0x000767D2 File Offset: 0x000749D2
		internal override bool IsNullImage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001EAE RID: 7854 RVA: 0x000767D5 File Offset: 0x000749D5
		internal override List<string> GetFieldsUsedInValueExpression()
		{
			return null;
		}

		// Token: 0x06001EAF RID: 7855 RVA: 0x000767D8 File Offset: 0x000749D8
		public override ActionInfoWithDynamicImageMap CreateActionInfoWithDynamicImageMap()
		{
			throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
		}

		// Token: 0x06001EB0 RID: 7856 RVA: 0x000767E4 File Offset: 0x000749E4
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_actionInfoImageMapAreas = null;
		}

		// Token: 0x04000FA6 RID: 4006
		private ActionInfoWithDynamicImageMapCollection m_actionInfoImageMapAreas;
	}
}
