using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000081 RID: 129
	internal sealed class WellKnownTextSqlWriter : DrawBoth
	{
		// Token: 0x0600031D RID: 797 RVA: 0x00008D88 File Offset: 0x00006F88
		public WellKnownTextSqlWriter(TextWriter writer)
			: this(writer, false)
		{
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00008D92 File Offset: 0x00006F92
		public WellKnownTextSqlWriter(TextWriter writer, bool allowOnlyTwoDimensions)
		{
			this.allowOnlyTwoDimensions = allowOnlyTwoDimensions;
			this.writer = writer;
			this.parentStack = new Stack<SpatialType>();
			this.Reset();
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00008DB9 File Offset: 0x00006FB9
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.AddLineTo(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00008DDA File Offset: 0x00006FDA
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.AddLineTo(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00008DFB File Offset: 0x00006FFB
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00008E05 File Offset: 0x00007005
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00008E0F File Offset: 0x0000700F
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.WriteFigureScope(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00008E30 File Offset: 0x00007030
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.WriteFigureScope(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00008E51 File Offset: 0x00007051
		protected override void OnEndFigure()
		{
			this.EndFigure();
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00008E59 File Offset: 0x00007059
		protected override void OnEndGeography()
		{
			this.EndGeo();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00008E61 File Offset: 0x00007061
		protected override void OnEndGeometry()
		{
			this.EndGeo();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00008E69 File Offset: 0x00007069
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.WriteCoordinateSystem(coordinateSystem);
			return coordinateSystem;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00008E73 File Offset: 0x00007073
		protected override void OnReset()
		{
			this.Reset();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00008E7C File Offset: 0x0000707C
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

		// Token: 0x0600032B RID: 811 RVA: 0x00008ED9 File Offset: 0x000070D9
		private void Reset()
		{
			this.figureWritten = false;
			this.parentStack.Clear();
			this.shapeWritten = false;
			this.coordinateSystemWritten = false;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x00008EFC File Offset: 0x000070FC
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

		// Token: 0x0600032D RID: 813 RVA: 0x00008F75 File Offset: 0x00007175
		private void AddLineTo(double x, double y, double? z, double? m)
		{
			this.writer.Write(", ");
			this.WritePoint(x, y, z, m);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00008F92 File Offset: 0x00007192
		private void EndFigure()
		{
			this.writer.Write(")");
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00008FA4 File Offset: 0x000071A4
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

		// Token: 0x06000330 RID: 816 RVA: 0x000090B4 File Offset: 0x000072B4
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

		// Token: 0x06000331 RID: 817 RVA: 0x0000911C File Offset: 0x0000731C
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

		// Token: 0x06000332 RID: 818 RVA: 0x000091EC File Offset: 0x000073EC
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

		// Token: 0x040000EC RID: 236
		private bool allowOnlyTwoDimensions;

		// Token: 0x040000ED RID: 237
		private TextWriter writer;

		// Token: 0x040000EE RID: 238
		private Stack<SpatialType> parentStack;

		// Token: 0x040000EF RID: 239
		private bool coordinateSystemWritten;

		// Token: 0x040000F0 RID: 240
		private bool figureWritten;

		// Token: 0x040000F1 RID: 241
		private bool shapeWritten;
	}
}
