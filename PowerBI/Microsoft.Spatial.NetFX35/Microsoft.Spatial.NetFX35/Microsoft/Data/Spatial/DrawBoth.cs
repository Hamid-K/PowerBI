using System;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000003 RID: 3
	internal abstract class DrawBoth
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002443 File Offset: 0x00000643
		public virtual GeographyPipeline GeographyPipeline
		{
			get
			{
				return new DrawBoth.DrawGeographyInput(this);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000244B File Offset: 0x0000064B
		public virtual GeometryPipeline GeometryPipeline
		{
			get
			{
				return new DrawBoth.DrawGeometryInput(this);
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002453 File Offset: 0x00000653
		public static implicit operator SpatialPipeline(DrawBoth both)
		{
			if (both != null)
			{
				return new SpatialPipeline(both.GeographyPipeline, both.GeometryPipeline);
			}
			return null;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000246B File Offset: 0x0000066B
		protected virtual GeographyPosition OnLineTo(GeographyPosition position)
		{
			return position;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000246E File Offset: 0x0000066E
		protected virtual GeometryPosition OnLineTo(GeometryPosition position)
		{
			return position;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002471 File Offset: 0x00000671
		protected virtual GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			return position;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002474 File Offset: 0x00000674
		protected virtual GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			return position;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002477 File Offset: 0x00000677
		protected virtual SpatialType OnBeginGeography(SpatialType type)
		{
			return type;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000247A File Offset: 0x0000067A
		protected virtual SpatialType OnBeginGeometry(SpatialType type)
		{
			return type;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000247D File Offset: 0x0000067D
		protected virtual void OnEndFigure()
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000247F File Offset: 0x0000067F
		protected virtual void OnEndGeography()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002481 File Offset: 0x00000681
		protected virtual void OnEndGeometry()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002483 File Offset: 0x00000683
		protected virtual void OnReset()
		{
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002485 File Offset: 0x00000685
		protected virtual CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			return coordinateSystem;
		}

		// Token: 0x02000005 RID: 5
		private class DrawGeographyInput : GeographyPipeline
		{
			// Token: 0x0600003E RID: 62 RVA: 0x00002498 File Offset: 0x00000698
			public DrawGeographyInput(DrawBoth both)
			{
				this.both = both;
			}

			// Token: 0x0600003F RID: 63 RVA: 0x000024A7 File Offset: 0x000006A7
			public override void LineTo(GeographyPosition position)
			{
				this.both.OnLineTo(position);
			}

			// Token: 0x06000040 RID: 64 RVA: 0x000024B6 File Offset: 0x000006B6
			public override void BeginFigure(GeographyPosition position)
			{
				this.both.OnBeginFigure(position);
			}

			// Token: 0x06000041 RID: 65 RVA: 0x000024C5 File Offset: 0x000006C5
			public override void BeginGeography(SpatialType type)
			{
				this.both.OnBeginGeography(type);
			}

			// Token: 0x06000042 RID: 66 RVA: 0x000024D4 File Offset: 0x000006D4
			public override void EndFigure()
			{
				this.both.OnEndFigure();
			}

			// Token: 0x06000043 RID: 67 RVA: 0x000024E1 File Offset: 0x000006E1
			public override void EndGeography()
			{
				this.both.OnEndGeography();
			}

			// Token: 0x06000044 RID: 68 RVA: 0x000024EE File Offset: 0x000006EE
			public override void Reset()
			{
				this.both.OnReset();
			}

			// Token: 0x06000045 RID: 69 RVA: 0x000024FB File Offset: 0x000006FB
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.both.OnSetCoordinateSystem(coordinateSystem);
			}

			// Token: 0x04000007 RID: 7
			private readonly DrawBoth both;
		}

		// Token: 0x02000007 RID: 7
		private class DrawGeometryInput : GeometryPipeline
		{
			// Token: 0x0600004E RID: 78 RVA: 0x00002512 File Offset: 0x00000712
			public DrawGeometryInput(DrawBoth both)
			{
				this.both = both;
			}

			// Token: 0x0600004F RID: 79 RVA: 0x00002521 File Offset: 0x00000721
			public override void LineTo(GeometryPosition position)
			{
				this.both.OnLineTo(position);
			}

			// Token: 0x06000050 RID: 80 RVA: 0x00002530 File Offset: 0x00000730
			public override void BeginFigure(GeometryPosition position)
			{
				this.both.OnBeginFigure(position);
			}

			// Token: 0x06000051 RID: 81 RVA: 0x0000253F File Offset: 0x0000073F
			public override void BeginGeometry(SpatialType type)
			{
				this.both.OnBeginGeometry(type);
			}

			// Token: 0x06000052 RID: 82 RVA: 0x0000254E File Offset: 0x0000074E
			public override void EndFigure()
			{
				this.both.OnEndFigure();
			}

			// Token: 0x06000053 RID: 83 RVA: 0x0000255B File Offset: 0x0000075B
			public override void EndGeometry()
			{
				this.both.OnEndGeometry();
			}

			// Token: 0x06000054 RID: 84 RVA: 0x00002568 File Offset: 0x00000768
			public override void Reset()
			{
				this.both.OnReset();
			}

			// Token: 0x06000055 RID: 85 RVA: 0x00002575 File Offset: 0x00000775
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.both.OnSetCoordinateSystem(coordinateSystem);
			}

			// Token: 0x04000008 RID: 8
			private readonly DrawBoth both;
		}
	}
}
