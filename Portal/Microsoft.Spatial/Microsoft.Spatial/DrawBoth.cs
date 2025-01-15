using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000046 RID: 70
	internal abstract class DrawBoth
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000202 RID: 514 RVA: 0x0000534F File Offset: 0x0000354F
		public virtual GeographyPipeline GeographyPipeline
		{
			get
			{
				return new DrawBoth.DrawGeographyInput(this);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000203 RID: 515 RVA: 0x00005357 File Offset: 0x00003557
		public virtual GeometryPipeline GeometryPipeline
		{
			get
			{
				return new DrawBoth.DrawGeometryInput(this);
			}
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000535F File Offset: 0x0000355F
		public static implicit operator SpatialPipeline(DrawBoth both)
		{
			if (both != null)
			{
				return new SpatialPipeline(both.GeographyPipeline, both.GeometryPipeline);
			}
			return null;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00005377 File Offset: 0x00003577
		protected virtual GeographyPosition OnLineTo(GeographyPosition position)
		{
			return position;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00005377 File Offset: 0x00003577
		protected virtual GeometryPosition OnLineTo(GeometryPosition position)
		{
			return position;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00005377 File Offset: 0x00003577
		protected virtual GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			return position;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00005377 File Offset: 0x00003577
		protected virtual GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			return position;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00005377 File Offset: 0x00003577
		protected virtual SpatialType OnBeginGeography(SpatialType type)
		{
			return type;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00005377 File Offset: 0x00003577
		protected virtual SpatialType OnBeginGeometry(SpatialType type)
		{
			return type;
		}

		// Token: 0x0600020B RID: 523 RVA: 0x0000537A File Offset: 0x0000357A
		protected virtual void OnEndFigure()
		{
		}

		// Token: 0x0600020C RID: 524 RVA: 0x0000537A File Offset: 0x0000357A
		protected virtual void OnEndGeography()
		{
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000537A File Offset: 0x0000357A
		protected virtual void OnEndGeometry()
		{
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000537A File Offset: 0x0000357A
		protected virtual void OnReset()
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00005377 File Offset: 0x00003577
		protected virtual CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			return coordinateSystem;
		}

		// Token: 0x0200008A RID: 138
		private class DrawGeographyInput : GeographyPipeline
		{
			// Token: 0x0600037F RID: 895 RVA: 0x000082C5 File Offset: 0x000064C5
			public DrawGeographyInput(DrawBoth both)
			{
				this.both = both;
			}

			// Token: 0x06000380 RID: 896 RVA: 0x000082D4 File Offset: 0x000064D4
			public override void LineTo(GeographyPosition position)
			{
				this.both.OnLineTo(position);
			}

			// Token: 0x06000381 RID: 897 RVA: 0x000082E3 File Offset: 0x000064E3
			public override void BeginFigure(GeographyPosition position)
			{
				this.both.OnBeginFigure(position);
			}

			// Token: 0x06000382 RID: 898 RVA: 0x000082F2 File Offset: 0x000064F2
			public override void BeginGeography(SpatialType type)
			{
				this.both.OnBeginGeography(type);
			}

			// Token: 0x06000383 RID: 899 RVA: 0x00008301 File Offset: 0x00006501
			public override void EndFigure()
			{
				this.both.OnEndFigure();
			}

			// Token: 0x06000384 RID: 900 RVA: 0x0000830E File Offset: 0x0000650E
			public override void EndGeography()
			{
				this.both.OnEndGeography();
			}

			// Token: 0x06000385 RID: 901 RVA: 0x0000831B File Offset: 0x0000651B
			public override void Reset()
			{
				this.both.OnReset();
			}

			// Token: 0x06000386 RID: 902 RVA: 0x00008328 File Offset: 0x00006528
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.both.OnSetCoordinateSystem(coordinateSystem);
			}

			// Token: 0x0400012F RID: 303
			private readonly DrawBoth both;
		}

		// Token: 0x0200008B RID: 139
		private class DrawGeometryInput : GeometryPipeline
		{
			// Token: 0x06000387 RID: 903 RVA: 0x00008337 File Offset: 0x00006537
			public DrawGeometryInput(DrawBoth both)
			{
				this.both = both;
			}

			// Token: 0x06000388 RID: 904 RVA: 0x00008346 File Offset: 0x00006546
			public override void LineTo(GeometryPosition position)
			{
				this.both.OnLineTo(position);
			}

			// Token: 0x06000389 RID: 905 RVA: 0x00008355 File Offset: 0x00006555
			public override void BeginFigure(GeometryPosition position)
			{
				this.both.OnBeginFigure(position);
			}

			// Token: 0x0600038A RID: 906 RVA: 0x00008364 File Offset: 0x00006564
			public override void BeginGeometry(SpatialType type)
			{
				this.both.OnBeginGeometry(type);
			}

			// Token: 0x0600038B RID: 907 RVA: 0x00008373 File Offset: 0x00006573
			public override void EndFigure()
			{
				this.both.OnEndFigure();
			}

			// Token: 0x0600038C RID: 908 RVA: 0x00008380 File Offset: 0x00006580
			public override void EndGeometry()
			{
				this.both.OnEndGeometry();
			}

			// Token: 0x0600038D RID: 909 RVA: 0x0000838D File Offset: 0x0000658D
			public override void Reset()
			{
				this.both.OnReset();
			}

			// Token: 0x0600038E RID: 910 RVA: 0x0000839A File Offset: 0x0000659A
			public override void SetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				this.both.OnSetCoordinateSystem(coordinateSystem);
			}

			// Token: 0x04000130 RID: 304
			private readonly DrawBoth both;
		}
	}
}
