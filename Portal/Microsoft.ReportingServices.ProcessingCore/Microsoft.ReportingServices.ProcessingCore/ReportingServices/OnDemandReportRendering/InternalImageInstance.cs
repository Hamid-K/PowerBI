using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000333 RID: 819
	internal sealed class InternalImageInstance : ImageInstance
	{
		// Token: 0x06001E92 RID: 7826 RVA: 0x000765A1 File Offset: 0x000747A1
		internal InternalImageInstance(Microsoft.ReportingServices.OnDemandReportRendering.Image reportItemDef)
			: base(reportItemDef)
		{
			this.m_imageDataHandler = ImageDataHandlerFactory.Create(this.m_reportElementDef, reportItemDef);
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06001E93 RID: 7827 RVA: 0x000765BC File Offset: 0x000747BC
		// (set) Token: 0x06001E94 RID: 7828 RVA: 0x000765C9 File Offset: 0x000747C9
		public override byte[] ImageData
		{
			get
			{
				return this.m_imageDataHandler.ImageData;
			}
			set
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06001E95 RID: 7829 RVA: 0x000765D5 File Offset: 0x000747D5
		// (set) Token: 0x06001E96 RID: 7830 RVA: 0x000765E2 File Offset: 0x000747E2
		public override string StreamName
		{
			get
			{
				return this.m_imageDataHandler.StreamName;
			}
			internal set
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
			}
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06001E97 RID: 7831 RVA: 0x000765EE File Offset: 0x000747EE
		// (set) Token: 0x06001E98 RID: 7832 RVA: 0x000765FB File Offset: 0x000747FB
		public override string MIMEType
		{
			get
			{
				return this.m_imageDataHandler.MIMEType;
			}
			set
			{
				throw new RenderingObjectModelException(RPRes.rsErrorDuringROMWriteback);
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06001E99 RID: 7833 RVA: 0x00076607 File Offset: 0x00074807
		public override TypeCode TagDataType
		{
			get
			{
				if (base.ImageDef.Tags != null)
				{
					return base.ImageDef.Tags[0].Instance.DataType;
				}
				return TypeCode.Empty;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06001E9A RID: 7834 RVA: 0x00076633 File Offset: 0x00074833
		public override object Tag
		{
			get
			{
				if (base.ImageDef.Tags != null)
				{
					return base.ImageDef.Tags[0].Instance.Value;
				}
				return null;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x06001E9B RID: 7835 RVA: 0x0007665F File Offset: 0x0007485F
		internal override string ImageDataId
		{
			get
			{
				return this.m_imageDataHandler.ImageDataId;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x06001E9C RID: 7836 RVA: 0x0007666C File Offset: 0x0007486C
		public override ActionInfoWithDynamicImageMapCollection ActionInfoWithDynamicImageMapAreas
		{
			get
			{
				if (this.m_actionInfoImageMapAreas == null)
				{
					this.m_actionInfoImageMapAreas = new ActionInfoWithDynamicImageMapCollection();
				}
				return this.m_actionInfoImageMapAreas;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x06001E9D RID: 7837 RVA: 0x00076687 File Offset: 0x00074887
		internal override bool IsNullImage
		{
			get
			{
				return this.m_imageDataHandler.IsNullImage;
			}
		}

		// Token: 0x06001E9E RID: 7838 RVA: 0x00076694 File Offset: 0x00074894
		internal override List<string> GetFieldsUsedInValueExpression()
		{
			return this.m_imageDataHandler.FieldsUsedInValue;
		}

		// Token: 0x06001E9F RID: 7839 RVA: 0x000766A1 File Offset: 0x000748A1
		public override ActionInfoWithDynamicImageMap CreateActionInfoWithDynamicImageMap()
		{
			throw new RenderingObjectModelException(RPRes.rsErrorDuringROMDefinitionWriteback);
		}

		// Token: 0x06001EA0 RID: 7840 RVA: 0x000766AD File Offset: 0x000748AD
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_actionInfoImageMapAreas = null;
			this.m_imageDataHandler.ClearCache();
		}

		// Token: 0x06001EA1 RID: 7841 RVA: 0x000766C7 File Offset: 0x000748C7
		internal string LoadAndCacheTransparentImage(out string mimeType, out byte[] imageData)
		{
			return this.m_imageDataHandler.LoadAndCacheTransparentImage(out mimeType, out imageData);
		}

		// Token: 0x04000FA4 RID: 4004
		private ActionInfoWithDynamicImageMapCollection m_actionInfoImageMapAreas;

		// Token: 0x04000FA5 RID: 4005
		private readonly ImageDataHandler m_imageDataHandler;
	}
}
