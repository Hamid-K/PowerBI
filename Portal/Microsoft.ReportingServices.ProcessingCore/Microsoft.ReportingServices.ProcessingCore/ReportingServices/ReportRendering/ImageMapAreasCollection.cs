using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200004F RID: 79
	public sealed class ImageMapAreasCollection
	{
		// Token: 0x060005FD RID: 1533 RVA: 0x00014B9B File Offset: 0x00012D9B
		public ImageMapAreasCollection()
		{
			this.m_list = new ArrayList();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00014BAE File Offset: 0x00012DAE
		public ImageMapAreasCollection(int capacity)
		{
			this.m_list = new ArrayList(capacity);
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00014BC2 File Offset: 0x00012DC2
		internal ImageMapAreasCollection(ImageMapAreaInstanceList mapAreasInstances, RenderingContext renderingContext)
		{
			Global.Tracer.Assert(renderingContext != null);
			this.m_renderingContext = renderingContext;
			this.m_list = mapAreasInstances;
		}

		// Token: 0x170004BC RID: 1212
		public ImageMapArea this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				if (this.IsChartOrCustomControlImageMap)
				{
					return this.m_list[index] as ImageMapArea;
				}
				return new ImageMapArea(((ImageMapAreaInstanceList)this.m_list)[index], this.m_renderingContext);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x00014C69 File Offset: 0x00012E69
		private bool IsChartOrCustomControlImageMap
		{
			get
			{
				return this.m_renderingContext == null;
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000602 RID: 1538 RVA: 0x00014C74 File Offset: 0x00012E74
		public int Count
		{
			get
			{
				if (this.m_list != null)
				{
					return this.m_list.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x00014C8B File Offset: 0x00012E8B
		public void Add(ImageMapArea mapArea)
		{
			this.m_list.Add(mapArea);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x00014C9C File Offset: 0x00012E9C
		internal ImageMapAreasCollection DeepClone()
		{
			Global.Tracer.Assert(this.IsChartOrCustomControlImageMap);
			if (this.m_list == null || this.m_list.Count == 0)
			{
				return null;
			}
			int count = this.m_list.Count;
			ImageMapAreasCollection imageMapAreasCollection = new ImageMapAreasCollection(count);
			for (int i = 0; i < count; i++)
			{
				imageMapAreasCollection.m_list.Add(((ImageMapArea)this.m_list[i]).DeepClone());
			}
			return imageMapAreasCollection;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00014D14 File Offset: 0x00012F14
		internal ImageMapAreaInstanceList Deconstruct(ReportProcessing.ProcessingContext processingContext, CustomReportItem context)
		{
			Global.Tracer.Assert(context != null && processingContext != null);
			if (this.m_list == null || this.m_list.Count == 0)
			{
				return null;
			}
			int count = this.m_list.Count;
			ImageMapAreaInstanceList imageMapAreaInstanceList = new ImageMapAreaInstanceList(count);
			for (int i = 0; i < count; i++)
			{
				ImageMapAreaInstance imageMapAreaInstance = ((ImageMapArea)this.m_list[i]).Deconstruct(context);
				imageMapAreaInstanceList.Add(imageMapAreaInstance);
			}
			return imageMapAreaInstanceList;
		}

		// Token: 0x04000181 RID: 385
		private RenderingContext m_renderingContext;

		// Token: 0x04000182 RID: 386
		private ArrayList m_list;
	}
}
