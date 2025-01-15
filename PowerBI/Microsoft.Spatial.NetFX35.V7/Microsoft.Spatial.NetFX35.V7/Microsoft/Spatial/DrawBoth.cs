using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000041 RID: 65
	internal abstract class DrawBoth
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000467B File Offset: 0x0000287B
		public virtual GeographyPipeline GeographyPipeline
		{
			get
			{
				return new DrawBoth.DrawGeographyInput(this);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00004683 File Offset: 0x00002883
		public virtual GeometryPipeline GeometryPipeline
		{
			get
			{
				return new DrawBoth.DrawGeometryInput(this);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000468B File Offset: 0x0000288B
		public static implicit operator SpatialPipeline(DrawBoth both)
		{
			if (both != null)
			{
				return new SpatialPipeline(both.GeographyPipeline, both.GeometryPipeline);
			}
			return null;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000046A3 File Offset: 0x000028A3
		protected virtual GeographyPosition OnLineTo(GeographyPosition position)
		{
			return position;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000046A3 File Offset: 0x000028A3
		protected virtual GeometryPosition OnLineTo(GeometryPosition position)
		{
			return position;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000046A3 File Offset: 0x000028A3
		protected virtual GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			return position;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000046A3 File Offset: 0x000028A3
		protected virtual GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			return position;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000046A3 File Offset: 0x000028A3
		protected virtual SpatialType OnBeginGeography(SpatialType type)
		{
			return type;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000046A3 File Offset: 0x000028A3
		protected virtual SpatialType OnBeginGeometry(SpatialType type)
		{
			return type;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000046A6 File Offset: 0x000028A6
		protected virtual void OnEndFigure()
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000046A6 File Offset: 0x000028A6
		protected virtual void OnEndGeography()
		{
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000046A6 File Offset: 0x000028A6
		protected virtual void OnEndGeometry()
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000046A6 File Offset: 0x000028A6
		protected virtual void OnReset()
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000046A3 File Offset: 0x000028A3
		protected virtual CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			return coordinateSystem;
		}

		// Token: 0x0200007E RID: 126
		private class DrawGeographyInput : GeographyPipeline
		{
			// Token: 0x060002F7 RID: 759 RVA: 0x00007561 File Offset: 0x00005761
			public DrawGeographyInput(DrawBoth both)
			{
				this.both = both;
			}

			// Token: 0x060002F8 RID: 760 RVA: 0x00007570 File Offset: 0x00005770
			public override void LineTo(GeographyPosition position)
			{
				this.both.OnLineTo(position);
			}

			// Token: 0x060002F9 RID: 761 RVA: 0x0000757F File Offset: 0x0000577F
			public override void BeginFigure(GeographyPosition position)
			{
				this.both.OnBeginFigure(position);
			}

			// Token: 0x060002FA RID: 762 RVA: 0x0000758E File Offset: 0x0000578E
			public override void BeginGeography(SpatialType type)
			{
				this.both.OnBeginGeography(type);
			}

			// Token: 0x060002FB RID: 763 RVA: 0x0000759D File Offset: 0x0000579D
			public override void EndFigure()
			{
				this.both.OnEndFigure();
			}

			// Token: 0x060002FC RID: 764 RVA: 0x000075AA File Offset: 0x000057AA
			public override void EndGeography()
			{
				this.both.OnEndGeography();
			}

			// Token: 0x060002FD RID: 765 RVA: 0x000075B7 File Offset: 0x000057B7
			public override void Reset()
			{
				this.both.OnReset();
			}

			// Token: 0x060002FE RID: 766 RVA: 0x000075C4 File Offset: 0x000057C4
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.both.OnSetCoordinateSystem(coordinateSystem);
			}

			// Token: 0x04000113 RID: 275
			private readonly DrawBoth both;
		}

		// Token: 0x0200007F RID: 127
		private class DrawGeometryInput : GeometryPipeline
		{
			// Token: 0x060002FF RID: 767 RVA: 0x000075D3 File Offset: 0x000057D3
			public DrawGeometryInput(DrawBoth both)
			{
				this.both = both;
			}

			// Token: 0x06000300 RID: 768 RVA: 0x000075E2 File Offset: 0x000057E2
			public override void LineTo(GeometryPosition position)
			{
				this.both.OnLineTo(position);
			}

			// Token: 0x06000301 RID: 769 RVA: 0x000075F1 File Offset: 0x000057F1
			public override void BeginFigure(GeometryPosition position)
			{
				this.both.OnBeginFigure(position);
			}

			// Token: 0x06000302 RID: 770 RVA: 0x00007600 File Offset: 0x00005800
			public override void BeginGeometry(SpatialType type)
			{
				this.both.OnBeginGeometry(type);
			}

			// Token: 0x06000303 RID: 771 RVA: 0x0000760F File Offset: 0x0000580F
			public override void EndFigure()
			{
				this.both.OnEndFigure();
			}

			// Token: 0x06000304 RID: 772 RVA: 0x0000761C File Offset: 0x0000581C
			public override void EndGeometry()
			{
				this.both.OnEndGeometry();
			}

			// Token: 0x06000305 RID: 773 RVA: 0x00007629 File Offset: 0x00005829
			public override void Reset()
			{
				this.both.OnReset();
			}

			// Token: 0x06000306 RID: 774 RVA: 0x00007636 File Offset: 0x00005836
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.both.OnSetCoordinateSystem(coordinateSystem);
			}

			// Token: 0x04000114 RID: 276
			private readonly DrawBoth both;
		}
	}
}
