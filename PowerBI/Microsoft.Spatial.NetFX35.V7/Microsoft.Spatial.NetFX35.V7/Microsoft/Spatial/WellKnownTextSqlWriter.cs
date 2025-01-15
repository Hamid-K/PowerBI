using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x02000067 RID: 103
	internal sealed class WellKnownTextSqlWriter : DrawBoth
	{
		// Token: 0x0600026B RID: 619 RVA: 0x0000613A File Offset: 0x0000433A
		public WellKnownTextSqlWriter(TextWriter writer)
			: this(writer, false)
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00006144 File Offset: 0x00004344
		public WellKnownTextSqlWriter(TextWriter writer, bool allowOnlyTwoDimensions)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
			this.writer = writer;
			this.parentStack = new Stack<SpatialType>();
			this.Reset();
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000616B File Offset: 0x0000436B
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.AddLineTo(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000618C File Offset: 0x0000438C
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.AddLineTo(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000061AD File Offset: 0x000043AD
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000061AD File Offset: 0x000043AD
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000061B7 File Offset: 0x000043B7
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.WriteFigureScope(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000061D8 File Offset: 0x000043D8
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.WriteFigureScope(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x000061F9 File Offset: 0x000043F9
		protected override void OnEndFigure()
		{
			this.EndFigure();
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00006201 File Offset: 0x00004401
		protected override void OnEndGeography()
		{
			this.EndGeo();
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00006201 File Offset: 0x00004401
		protected override void OnEndGeometry()
		{
			this.EndGeo();
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00006209 File Offset: 0x00004409
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.WriteCoordinateSystem(coordinateSystem);
			return coordinateSystem;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00006213 File Offset: 0x00004413
		protected override void OnReset()
		{
			this.Reset();
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000621C File Offset: 0x0000441C
		private void WriteCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			if (!this.coordinateSystemWritten)
			{
				this.writer.Write("SRID");
				this.writer.Write("=");
				this.writer.Write(coordinateSystem.Id);
				this.writer.Write(";");
				this.coordinateSystemWritten = true;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00006279 File Offset: 0x00004479
		private void Reset()
		{
			this.figureWritten = false;
			this.parentStack.Clear();
			this.shapeWritten = false;
			this.coordinateSystemWritten = false;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000629C File Offset: 0x0000449C
		private void BeginGeo(SpatialType type)
		{
			SpatialType spatialType = ((this.parentStack.Count == 0) ? SpatialType.Unknown : this.parentStack.Peek());
			if (spatialType == SpatialType.MultiPoint || spatialType == SpatialType.MultiLineString || spatialType == SpatialType.MultiPolygon || spatialType == SpatialType.Collection)
			{
				this.writer.Write(this.shapeWritten ? ", " : "(");
			}
			if (spatialType == SpatialType.Unknown || spatialType == SpatialType.Collection)
			{
				this.WriteTaggedText(type);
			}
			this.figureWritten = false;
			this.parentStack.Push(type);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00006315 File Offset: 0x00004515
		private void AddLineTo(double x, double y, double? z, double? m)
		{
			this.writer.Write(", ");
			this.WritePoint(x, y, z, m);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00006332 File Offset: 0x00004532
		private void EndFigure()
		{
			this.writer.Write(")");
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00006344 File Offset: 0x00004544
		private void WriteTaggedText(SpatialType type)
		{
			switch (type)
			{
			case SpatialType.Point:
				this.writer.Write("POINT");
				break;
			case SpatialType.LineString:
				this.writer.Write("LINESTRING");
				break;
			case SpatialType.Polygon:
				this.writer.Write("POLYGON");
				break;
			case SpatialType.MultiPoint:
				this.shapeWritten = false;
				this.writer.Write("MULTIPOINT");
				break;
			case SpatialType.MultiLineString:
				this.shapeWritten = false;
				this.writer.Write("MULTILINESTRING");
				break;
			case SpatialType.MultiPolygon:
				this.shapeWritten = false;
				this.writer.Write("MULTIPOLYGON");
				break;
			case SpatialType.Collection:
				this.shapeWritten = false;
				this.writer.Write("GEOMETRYCOLLECTION");
				break;
			case SpatialType.FullGlobe:
				this.writer.Write("FULLGLOBE");
				break;
			}
			if (type != SpatialType.FullGlobe)
			{
				this.writer.Write(" ");
			}
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00006450 File Offset: 0x00004650
		private void WriteFigureScope(double coordinate1, double coordinate2, double? coordinate3, double? coordinate4)
		{
			if (this.figureWritten)
			{
				this.writer.Write(", ");
			}
			else if (this.parentStack.Peek() == SpatialType.Polygon)
			{
				this.writer.Write("(");
			}
			this.writer.Write("(");
			this.figureWritten = true;
			this.WritePoint(coordinate1, coordinate2, coordinate3, coordinate4);
		}

		// Token: 0x0600027F RID: 639 RVA: 0x000064B8 File Offset: 0x000046B8
		private void EndGeo()
		{
			switch (this.parentStack.Pop())
			{
			case SpatialType.Point:
			case SpatialType.LineString:
				if (!this.figureWritten)
				{
					this.writer.Write("EMPTY");
				}
				break;
			case SpatialType.Polygon:
				this.writer.Write(this.figureWritten ? ")" : "EMPTY");
				break;
			case SpatialType.MultiPoint:
			case SpatialType.MultiLineString:
			case SpatialType.MultiPolygon:
			case SpatialType.Collection:
				this.writer.Write(this.shapeWritten ? ")" : "EMPTY");
				break;
			case SpatialType.FullGlobe:
				this.writer.Write(")");
				break;
			}
			this.shapeWritten = true;
			this.writer.Flush();
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00006588 File Offset: 0x00004788
		[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "x, y, z, m are meaningful")]
		private void WritePoint(double x, double y, double? z, double? m)
		{
			this.writer.WriteRoundtrippable(x);
			this.writer.Write(" ");
			this.writer.WriteRoundtrippable(y);
			if (!this.allowOnlyTwoDimensions && z != null)
			{
				this.writer.Write(" ");
				this.writer.WriteRoundtrippable(z.Value);
				if (!this.allowOnlyTwoDimensions && m != null)
				{
					this.writer.Write(" ");
					this.writer.WriteRoundtrippable(m.Value);
					return;
				}
			}
			else if (!this.allowOnlyTwoDimensions && m != null)
			{
				this.writer.Write(" ");
				this.writer.Write("NULL");
				this.writer.Write(" ");
				this.writer.Write(m.Value);
			}
		}

		// Token: 0x040000AA RID: 170
		private bool allowOnlyTwoDimensions;

		// Token: 0x040000AB RID: 171
		private TextWriter writer;

		// Token: 0x040000AC RID: 172
		private Stack<SpatialType> parentStack;

		// Token: 0x040000AD RID: 173
		private bool coordinateSystemWritten;

		// Token: 0x040000AE RID: 174
		private bool figureWritten;

		// Token: 0x040000AF RID: 175
		private bool shapeWritten;
	}
}
