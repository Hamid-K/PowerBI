using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace Microsoft.Spatial
{
	// Token: 0x0200006C RID: 108
	internal sealed class WellKnownTextSqlWriter : DrawBoth
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00006E02 File Offset: 0x00005002
		public WellKnownTextSqlWriter(TextWriter writer)
			: this(writer, false)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00006E0C File Offset: 0x0000500C
		public WellKnownTextSqlWriter(TextWriter writer, bool allowOnlyTwoDimensions)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
			this.writer = writer;
			this.parentStack = new Stack<SpatialType>();
			this.Reset();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00006E33 File Offset: 0x00005033
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.AddLineTo(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00006E54 File Offset: 0x00005054
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.AddLineTo(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00006E75 File Offset: 0x00005075
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00006E75 File Offset: 0x00005075
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00006E7F File Offset: 0x0000507F
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.WriteFigureScope(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00006EA0 File Offset: 0x000050A0
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.WriteFigureScope(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00006EC1 File Offset: 0x000050C1
		protected override void OnEndFigure()
		{
			this.EndFigure();
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00006EC9 File Offset: 0x000050C9
		protected override void OnEndGeography()
		{
			this.EndGeo();
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00006EC9 File Offset: 0x000050C9
		protected override void OnEndGeometry()
		{
			this.EndGeo();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00006ED1 File Offset: 0x000050D1
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.WriteCoordinateSystem(coordinateSystem);
			return coordinateSystem;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00006EDB File Offset: 0x000050DB
		protected override void OnReset()
		{
			this.Reset();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00006EE4 File Offset: 0x000050E4
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

		// Token: 0x060002EF RID: 751 RVA: 0x00006F41 File Offset: 0x00005141
		private void Reset()
		{
			this.figureWritten = false;
			this.parentStack.Clear();
			this.shapeWritten = false;
			this.coordinateSystemWritten = false;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00006F64 File Offset: 0x00005164
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

		// Token: 0x060002F1 RID: 753 RVA: 0x00006FDD File Offset: 0x000051DD
		private void AddLineTo(double x, double y, double? z, double? m)
		{
			this.writer.Write(", ");
			this.WritePoint(x, y, z, m);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00006FFA File Offset: 0x000051FA
		private void EndFigure()
		{
			this.writer.Write(")");
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000700C File Offset: 0x0000520C
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

		// Token: 0x060002F4 RID: 756 RVA: 0x00007118 File Offset: 0x00005318
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

		// Token: 0x060002F5 RID: 757 RVA: 0x00007180 File Offset: 0x00005380
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

		// Token: 0x060002F6 RID: 758 RVA: 0x00007250 File Offset: 0x00005450
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

		// Token: 0x040000B7 RID: 183
		private bool allowOnlyTwoDimensions;

		// Token: 0x040000B8 RID: 184
		private TextWriter writer;

		// Token: 0x040000B9 RID: 185
		private Stack<SpatialType> parentStack;

		// Token: 0x040000BA RID: 186
		private bool coordinateSystemWritten;

		// Token: 0x040000BB RID: 187
		private bool figureWritten;

		// Token: 0x040000BC RID: 188
		private bool shapeWritten;
	}
}
