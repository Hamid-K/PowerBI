using System;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000073 RID: 115
	public sealed class Image : Microsoft.ReportingServices.ReportRendering.ReportItem, IImage, IDeepCloneable
	{
		// Token: 0x06000712 RID: 1810 RVA: 0x0001B180 File Offset: 0x00019380
		public Image(string definitionName, string instanceName)
			: base(definitionName, instanceName)
		{
			if (definitionName == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "definitionName" });
			}
			if (instanceName == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "instanceName" });
			}
			Global.Tracer.Assert(base.IsCustomControl && base.Processing != null);
			this.m_internalImage = new ImageProcessing();
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0001B1F8 File Offset: 0x000193F8
		internal Image(string uniqueName, int intUniqueName, Microsoft.ReportingServices.ReportProcessing.Image reportItemDef, Microsoft.ReportingServices.ReportProcessing.ImageInstance reportItemInstance, Microsoft.ReportingServices.ReportRendering.RenderingContext renderingContext)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
			ImageInstanceInfo imageInstanceInfo = (ImageInstanceInfo)base.InstanceInfo;
			string text = null;
			if (reportItemDef.Source == Microsoft.ReportingServices.ReportProcessing.Image.SourceType.Database && reportItemDef.MIMEType.Type == ExpressionInfo.Types.Constant)
			{
				text = reportItemDef.MIMEType.Value;
			}
			this.m_internalImage = new InternalImage((Microsoft.ReportingServices.ReportRendering.Image.SourceType)reportItemDef.Source, text, (imageInstanceInfo != null) ? imageInstanceInfo.ValueObject : reportItemDef.Value.Value, renderingContext, imageInstanceInfo != null && imageInstanceInfo.BrokenImage, (imageInstanceInfo != null) ? imageInstanceInfo.ImageMapAreas : null);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001B285 File Offset: 0x00019485
		private Image()
		{
		}

		// Token: 0x17000540 RID: 1344
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001B28D File Offset: 0x0001948D
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x0001B2AE File Offset: 0x000194AE
		public byte[] ImageData
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.Processing.m_imageData;
				}
				return this.Rendering.ImageData;
			}
			set
			{
				if (!base.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.m_imageData = value;
			}
		}

		// Token: 0x17000541 RID: 1345
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001B2CF File Offset: 0x000194CF
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x0001B2F0 File Offset: 0x000194F0
		public string MIMEType
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.Processing.m_mimeType;
				}
				return this.Rendering.MIMEType;
			}
			set
			{
				if (!base.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				if (value == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "MimeType" });
				}
				if (!Validator.ValidateMimeType(value))
				{
					throw new ReportRenderingException(ErrorCode.rrInvalidMimeType, new object[] { value });
				}
				this.Processing.m_mimeType = value;
			}
		}

		// Token: 0x17000542 RID: 1346
		// (get) Token: 0x06000719 RID: 1817 RVA: 0x0001B355 File Offset: 0x00019555
		public string StreamName
		{
			get
			{
				if (base.IsCustomControl)
				{
					return null;
				}
				return this.Rendering.StreamName;
			}
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x0600071A RID: 1818 RVA: 0x0001B36C File Offset: 0x0001956C
		public ReportUrl HyperLinkURL
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].HyperLinkURL;
				}
				return null;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x0600071B RID: 1819 RVA: 0x0001B3A0 File Offset: 0x000195A0
		public ReportUrl DrillthroughReport
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].DrillthroughReport;
				}
				return null;
			}
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x0600071C RID: 1820 RVA: 0x0001B3D4 File Offset: 0x000195D4
		public NameValueCollection DrillthroughParameters
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].DrillthroughParameters;
				}
				return null;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x0001B408 File Offset: 0x00019608
		public string BookmarkLink
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].BookmarkLink;
				}
				return null;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600071E RID: 1822 RVA: 0x0001B43C File Offset: 0x0001963C
		// (set) Token: 0x0600071F RID: 1823 RVA: 0x0001B50D File Offset: 0x0001970D
		public ActionInfo ActionInfo
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (!base.IsCustomControl && actionInfo == null)
				{
					Microsoft.ReportingServices.ReportProcessing.Action action = ((Microsoft.ReportingServices.ReportProcessing.Image)base.ReportItemDef).Action;
					if (action != null)
					{
						Microsoft.ReportingServices.ReportProcessing.ActionInstance actionInstance = null;
						string text = base.UniqueName;
						if (base.ReportItemInstance != null)
						{
							actionInstance = ((ImageInstanceInfo)base.InstanceInfo).Action;
							if (base.RenderingContext.InPageSection)
							{
								text = base.ReportItemInstance.UniqueName.ToString(CultureInfo.InvariantCulture);
							}
						}
						else if (base.RenderingContext.InPageSection && this.m_intUniqueName != 0)
						{
							text = this.m_intUniqueName.ToString(CultureInfo.InvariantCulture);
						}
						actionInfo = new ActionInfo(action, actionInstance, text, base.RenderingContext);
						if (base.RenderingContext.CacheState)
						{
							this.m_actionInfo = actionInfo;
						}
					}
				}
				return actionInfo;
			}
			set
			{
				if (!base.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_actionInfo = value;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x06000720 RID: 1824 RVA: 0x0001B529 File Offset: 0x00019729
		// (set) Token: 0x06000721 RID: 1825 RVA: 0x0001B54F File Offset: 0x0001974F
		public Microsoft.ReportingServices.ReportRendering.Image.Sizings Sizing
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.Processing.m_sizing;
				}
				return (Microsoft.ReportingServices.ReportRendering.Image.Sizings)((Microsoft.ReportingServices.ReportProcessing.Image)base.ReportItemDef).Sizing;
			}
			set
			{
				if (!base.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.Processing.m_sizing = value;
			}
		}

		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x0001B570 File Offset: 0x00019770
		// (set) Token: 0x06000723 RID: 1827 RVA: 0x0001B5D4 File Offset: 0x000197D4
		public ImageMapAreasCollection ImageMap
		{
			get
			{
				if (base.IsCustomControl)
				{
					return this.m_imageMap;
				}
				ImageMapAreasCollection imageMapAreasCollection = this.m_imageMap;
				if (this.m_imageMap == null && this.Rendering.ImageMapAreaInstances != null)
				{
					imageMapAreasCollection = new ImageMapAreasCollection(this.Rendering.ImageMapAreaInstances, base.RenderingContext);
					if (base.RenderingContext.CacheState)
					{
						this.m_imageMap = imageMapAreasCollection;
					}
				}
				return imageMapAreasCollection;
			}
			set
			{
				if (!base.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_imageMap = value;
			}
		}

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001B5F0 File Offset: 0x000197F0
		private InternalImage Rendering
		{
			get
			{
				Global.Tracer.Assert(!base.IsCustomControl);
				InternalImage internalImage = this.m_internalImage as InternalImage;
				if (internalImage == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return internalImage;
			}
		}

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x06000725 RID: 1829 RVA: 0x0001B62C File Offset: 0x0001982C
		internal new ImageProcessing Processing
		{
			get
			{
				Global.Tracer.Assert(base.IsCustomControl);
				ImageProcessing imageProcessing = this.m_internalImage as ImageProcessing;
				if (imageProcessing == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return imageProcessing;
			}
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001B664 File Offset: 0x00019864
		Microsoft.ReportingServices.ReportRendering.ReportItem IDeepCloneable.DeepClone()
		{
			if (!base.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			Microsoft.ReportingServices.ReportRendering.Image image = new Microsoft.ReportingServices.ReportRendering.Image();
			base.DeepClone(image);
			Global.Tracer.Assert(this.m_internalImage != null && this.m_internalImage is ImageProcessing);
			image.m_internalImage = this.Processing.DeepClone();
			if (this.m_actionInfo != null)
			{
				image.m_actionInfo = this.m_actionInfo.DeepClone();
			}
			if (this.m_imageMap != null)
			{
				image.m_imageMap = this.m_imageMap.DeepClone();
			}
			return image;
		}

		// Token: 0x040001FF RID: 511
		private ImageBase m_internalImage;

		// Token: 0x04000200 RID: 512
		private ActionInfo m_actionInfo;

		// Token: 0x04000201 RID: 513
		private ImageMapAreasCollection m_imageMap;

		// Token: 0x02000914 RID: 2324
		public enum Sizings
		{
			// Token: 0x04003F01 RID: 16129
			AutoSize,
			// Token: 0x04003F02 RID: 16130
			Fit,
			// Token: 0x04003F03 RID: 16131
			FitProportional,
			// Token: 0x04003F04 RID: 16132
			Clip
		}

		// Token: 0x02000915 RID: 2325
		internal enum SourceType
		{
			// Token: 0x04003F06 RID: 16134
			External,
			// Token: 0x04003F07 RID: 16135
			Embedded,
			// Token: 0x04003F08 RID: 16136
			Database
		}
	}
}
