using System;
using System.Globalization;
using Dundas.Charting.WebControl;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000050 RID: 80
	public sealed class ImageMapArea
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x00014D8C File Offset: 0x00012F8C
		public ImageMapArea()
		{
			this.m_members = new ImageMapAreaProcessing();
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x00014DA8 File Offset: 0x00012FA8
		internal ImageMapArea(MapArea dundasMapArea, int id)
		{
			this.m_members = new ImageMapAreaRendering();
			this.m_shape = this.ConvertShape(dundasMapArea.Shape);
			if (dundasMapArea.Coordinates != null)
			{
				this.m_coordinates = (float[])dundasMapArea.Coordinates.Clone();
			}
			this.m_id = id.ToString(CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x06000608 RID: 1544 RVA: 0x00014E14 File Offset: 0x00013014
		internal ImageMapArea(ImageMapAreaInstance mapAreaInstance, RenderingContext renderingContext)
		{
			this.m_members = new ImageMapAreaRendering();
			this.Rendering.m_mapAreaInstance = mapAreaInstance;
			this.Rendering.m_renderingContext = renderingContext;
			if (mapAreaInstance != null)
			{
				this.m_id = mapAreaInstance.ID;
				this.m_shape = mapAreaInstance.Shape;
				this.m_coordinates = mapAreaInstance.Coordinates;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00014E78 File Offset: 0x00013078
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x00014E80 File Offset: 0x00013080
		public string ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_id = value;
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00014E9C File Offset: 0x0001309C
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x00014F2D File Offset: 0x0001312D
		public ActionInfo ActionInfo
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (!this.IsCustomControl && this.Rendering.m_mapAreaInstance != null)
				{
					actionInfo = new ActionInfo(this.Rendering.m_mapAreaInstance.Action, this.Rendering.m_mapAreaInstance.ActionInstance, this.Rendering.m_mapAreaInstance.UniqueName.ToString(CultureInfo.InvariantCulture), this.Rendering.m_renderingContext);
					if (this.Rendering.m_renderingContext.CacheState)
					{
						this.m_actionInfo = actionInfo;
					}
				}
				return actionInfo;
			}
			set
			{
				if (!this.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				this.m_actionInfo = value;
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00014F49 File Offset: 0x00013149
		public ImageMapArea.ImageMapAreaShape Shape
		{
			get
			{
				return this.m_shape;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00014F51 File Offset: 0x00013151
		public float[] Coordinates
		{
			get
			{
				return this.m_coordinates;
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00014F59 File Offset: 0x00013159
		private bool IsCustomControl
		{
			get
			{
				return this.m_members.IsCustomControl;
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00014F68 File Offset: 0x00013168
		private ImageMapAreaRendering Rendering
		{
			get
			{
				Global.Tracer.Assert(!this.m_members.IsCustomControl);
				ImageMapAreaRendering imageMapAreaRendering = this.m_members as ImageMapAreaRendering;
				if (imageMapAreaRendering == null)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return imageMapAreaRendering;
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x00014FA8 File Offset: 0x000131A8
		public void SetCoordinates(ImageMapArea.ImageMapAreaShape shape, float[] coordinates)
		{
			if (!this.IsCustomControl)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			if (coordinates == null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterValue, new object[] { "coordinates" });
			}
			this.m_shape = shape;
			this.m_coordinates = coordinates;
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x00014FE7 File Offset: 0x000131E7
		private ImageMapArea.ImageMapAreaShape ConvertShape(MapAreaShape shape)
		{
			if (shape == null)
			{
				return ImageMapArea.ImageMapAreaShape.Rectangle;
			}
			if (1 == shape)
			{
				return ImageMapArea.ImageMapAreaShape.Circle;
			}
			return ImageMapArea.ImageMapAreaShape.Polygon;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x00014FF8 File Offset: 0x000131F8
		internal ImageMapArea DeepClone()
		{
			Global.Tracer.Assert(this.IsCustomControl);
			ImageMapArea imageMapArea = new ImageMapArea();
			imageMapArea.m_members = null;
			imageMapArea.m_shape = this.m_shape;
			if (this.m_id != null)
			{
				imageMapArea.m_id = string.Copy(this.m_id);
			}
			if (this.m_coordinates != null)
			{
				imageMapArea.m_coordinates = new float[this.m_coordinates.Length];
				this.m_coordinates.CopyTo(imageMapArea.m_coordinates, 0);
			}
			if (this.m_actionInfo != null)
			{
				imageMapArea.m_actionInfo = this.m_actionInfo.DeepClone();
			}
			return imageMapArea;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x00015090 File Offset: 0x00013290
		internal ImageMapAreaInstance Deconstruct(CustomReportItem context)
		{
			Global.Tracer.Assert(context != null);
			ImageMapAreaInstance imageMapAreaInstance = new ImageMapAreaInstance(context.ProcessingContext);
			imageMapAreaInstance.ID = this.m_id;
			imageMapAreaInstance.Shape = this.m_shape;
			imageMapAreaInstance.Coordinates = this.m_coordinates;
			if (this.m_actionInfo != null)
			{
				Microsoft.ReportingServices.ReportProcessing.Action action = null;
				ActionInstance actionInstance;
				this.m_actionInfo.Deconstruct(imageMapAreaInstance.UniqueName, ref action, out actionInstance, context);
				imageMapAreaInstance.Action = action;
				imageMapAreaInstance.ActionInstance = actionInstance;
			}
			return imageMapAreaInstance;
		}

		// Token: 0x04000183 RID: 387
		private string m_id;

		// Token: 0x04000184 RID: 388
		private ImageMapArea.ImageMapAreaShape m_shape = ImageMapArea.ImageMapAreaShape.Polygon;

		// Token: 0x04000185 RID: 389
		private float[] m_coordinates;

		// Token: 0x04000186 RID: 390
		private ActionInfo m_actionInfo;

		// Token: 0x04000187 RID: 391
		private MemberBase m_members;

		// Token: 0x02000912 RID: 2322
		public enum ImageMapAreaShape
		{
			// Token: 0x04003EF8 RID: 16120
			Rectangle,
			// Token: 0x04003EF9 RID: 16121
			Polygon,
			// Token: 0x04003EFA RID: 16122
			Circle
		}
	}
}
