using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020002D5 RID: 725
	public sealed class ImageMapAreaInstanceCollection : ReportElementCollectionBase<ImageMapAreaInstance>
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x0006C3FA File Offset: 0x0006A5FA
		internal ImageMapAreaInstanceCollection()
		{
			this.m_list = new List<ImageMapAreaInstance>();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0006C410 File Offset: 0x0006A610
		internal ImageMapAreaInstanceCollection(ImageMapAreasCollection imageMaps)
		{
			if (imageMaps == null)
			{
				this.m_list = new List<ImageMapAreaInstance>();
				return;
			}
			int count = imageMaps.Count;
			this.m_list = new List<ImageMapAreaInstance>(count);
			for (int i = 0; i < count; i++)
			{
				this.m_list.Add(new ImageMapAreaInstance(imageMaps[i]));
			}
		}

		// Token: 0x17000F3C RID: 3900
		public override ImageMapAreaInstance this[int index]
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

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x06001B25 RID: 6949 RVA: 0x0006C4BF File Offset: 0x0006A6BF
		public override int Count
		{
			get
			{
				return this.m_list.Count;
			}
		}

		// Token: 0x17000F3E RID: 3902
		// (get) Token: 0x06001B26 RID: 6950 RVA: 0x0006C4CC File Offset: 0x0006A6CC
		internal List<ImageMapAreaInstance> InternalList
		{
			get
			{
				return this.m_list;
			}
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x0006C4D4 File Offset: 0x0006A6D4
		internal ImageMapAreaInstance Add(ImageMapArea.ImageMapAreaShape shape, float[] coordinates)
		{
			return this.Add(shape, coordinates, null);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x0006C4E0 File Offset: 0x0006A6E0
		internal ImageMapAreaInstance Add(ImageMapArea.ImageMapAreaShape shape, float[] coordinates, string toolTip)
		{
			ImageMapAreaInstance imageMapAreaInstance = new ImageMapAreaInstance(shape, coordinates, toolTip);
			this.m_list.Add(imageMapAreaInstance);
			return imageMapAreaInstance;
		}

		// Token: 0x04000D6A RID: 3434
		private List<ImageMapAreaInstance> m_list;
	}
}
