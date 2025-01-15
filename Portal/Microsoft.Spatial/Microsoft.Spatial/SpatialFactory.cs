using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000034 RID: 52
	public abstract class SpatialFactory
	{
		// Token: 0x0600018E RID: 398 RVA: 0x00004462 File Offset: 0x00002662
		internal SpatialFactory()
		{
			this.containers = new Stack<SpatialType>();
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00004475 File Offset: 0x00002675
		private SpatialType CurrentType
		{
			get
			{
				if (this.containers.Count == 0)
				{
					return SpatialType.Unknown;
				}
				return this.containers.Peek();
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00004491 File Offset: 0x00002691
		protected virtual void BeginGeo(SpatialType type)
		{
			while (!this.CanContain(type))
			{
				this.EndGeo();
			}
			this.containers.Push(type);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000044B0 File Offset: 0x000026B0
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		protected virtual void BeginFigure(double x, double y, double? z, double? m)
		{
			this.figureDrawn = true;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000044B9 File Offset: 0x000026B9
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		protected virtual void AddLine(double x, double y, double? z, double? m)
		{
			if (this.inRing)
			{
				this.ringClosed = x == this.ringStartX && y == this.ringStartY;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000044E0 File Offset: 0x000026E0
		protected virtual void EndFigure()
		{
			if (this.inRing)
			{
				if (!this.ringClosed)
				{
					this.AddLine(this.ringStartX, this.ringStartY, this.ringStartZ, this.ringStartM);
				}
				this.inRing = false;
				this.ringClosed = true;
			}
			this.figureDrawn = false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004530 File Offset: 0x00002730
		protected virtual void EndGeo()
		{
			if (this.figureDrawn)
			{
				this.EndFigure();
			}
			this.containers.Pop();
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000454C File Offset: 0x0000274C
		protected virtual void Finish()
		{
			while (this.containers.Count > 0)
			{
				this.EndGeo();
			}
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00004564 File Offset: 0x00002764
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		protected virtual void AddPos(double x, double y, double? z, double? m)
		{
			if (!this.figureDrawn)
			{
				this.BeginFigure(x, y, z, m);
				return;
			}
			this.AddLine(x, y, z, m);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00004588 File Offset: 0x00002788
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z and m are meaningful")]
		protected virtual void StartRing(double x, double y, double? z, double? m)
		{
			if (this.figureDrawn)
			{
				this.EndFigure();
			}
			this.BeginFigure(x, y, z, m);
			this.ringStartX = x;
			this.ringStartY = y;
			this.ringStartM = m;
			this.ringStartZ = z;
			this.inRing = true;
			this.ringClosed = false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000045DC File Offset: 0x000027DC
		private bool CanContain(SpatialType type)
		{
			switch (this.CurrentType)
			{
			case SpatialType.Unknown:
			case SpatialType.Collection:
				return true;
			case SpatialType.MultiPoint:
				return type == SpatialType.Point;
			case SpatialType.MultiLineString:
				return type == SpatialType.LineString;
			case SpatialType.MultiPolygon:
				return type == SpatialType.Polygon;
			}
			return false;
		}

		// Token: 0x04000022 RID: 34
		private Stack<SpatialType> containers;

		// Token: 0x04000023 RID: 35
		private bool figureDrawn;

		// Token: 0x04000024 RID: 36
		private bool inRing;

		// Token: 0x04000025 RID: 37
		private bool ringClosed;

		// Token: 0x04000026 RID: 38
		private double ringStartX;

		// Token: 0x04000027 RID: 39
		private double ringStartY;

		// Token: 0x04000028 RID: 40
		private double? ringStartZ;

		// Token: 0x04000029 RID: 41
		private double? ringStartM;
	}
}
