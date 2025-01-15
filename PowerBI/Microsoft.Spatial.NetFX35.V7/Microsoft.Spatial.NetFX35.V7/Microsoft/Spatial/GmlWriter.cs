using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000043 RID: 67
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the common name for this format")]
	internal sealed class GmlWriter : DrawBoth
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00004701 File Offset: 0x00002901
		public GmlWriter(XmlWriter writer)
		{
			this.writer = writer;
			this.OnReset();
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00004716 File Offset: 0x00002916
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00004720 File Offset: 0x00002920
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.WritePoint(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00004741 File Offset: 0x00002941
		protected override void OnEndGeography()
		{
			this.EndGeo();
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00004716 File Offset: 0x00002916
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00004749 File Offset: 0x00002949
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.WritePoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00004741 File Offset: 0x00002941
		protected override void OnEndGeometry()
		{
			this.EndGeo();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000476A File Offset: 0x0000296A
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.currentCoordinateSystem = coordinateSystem;
			return coordinateSystem;
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00004774 File Offset: 0x00002974
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.BeginFigure(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00004795 File Offset: 0x00002995
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.BeginFigure(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000047B6 File Offset: 0x000029B6
		protected override void OnEndFigure()
		{
			if (this.parentStack.Peek() == SpatialType.Polygon)
			{
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000047DC File Offset: 0x000029DC
		protected override void OnReset()
		{
			this.parentStack = new Stack<SpatialType>();
			this.coordinateSystemWritten = false;
			this.currentCoordinateSystem = null;
			this.figureWritten = false;
			this.shouldWriteContainerWrapper = false;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00004808 File Offset: 0x00002A08
		private void BeginFigure(double x, double y, double? z, double? m)
		{
			if (this.parentStack.Peek() == SpatialType.Polygon)
			{
				this.WriteStartElement(this.figureWritten ? "interior" : "exterior");
				this.WriteStartElement("LinearRing");
			}
			this.figureWritten = true;
			this.WritePoint(x, y, z, m);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000485C File Offset: 0x00002A5C
		private void BeginGeo(SpatialType type)
		{
			if (this.shouldWriteContainerWrapper)
			{
				switch (this.parentStack.Peek())
				{
				case SpatialType.MultiPoint:
					this.WriteStartElement("pointMembers");
					break;
				case SpatialType.MultiLineString:
					this.WriteStartElement("curveMembers");
					break;
				case SpatialType.MultiPolygon:
					this.WriteStartElement("surfaceMembers");
					break;
				case SpatialType.Collection:
					this.WriteStartElement("geometryMembers");
					break;
				}
				this.shouldWriteContainerWrapper = false;
			}
			this.figureWritten = false;
			this.parentStack.Push(type);
			switch (type)
			{
			case SpatialType.Point:
				this.WriteStartElement("Point");
				goto IL_0158;
			case SpatialType.LineString:
				this.WriteStartElement("LineString");
				goto IL_0158;
			case SpatialType.Polygon:
				this.WriteStartElement("Polygon");
				goto IL_0158;
			case SpatialType.MultiPoint:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiPoint");
				goto IL_0158;
			case SpatialType.MultiLineString:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiCurve");
				goto IL_0158;
			case SpatialType.MultiPolygon:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiSurface");
				goto IL_0158;
			case SpatialType.Collection:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiGeometry");
				goto IL_0158;
			case SpatialType.FullGlobe:
				this.writer.WriteStartElement("FullGlobe", "http://schemas.microsoft.com/sqlserver/2011/geography");
				goto IL_0158;
			}
			throw new NotSupportedException(Strings.Validator_InvalidType(type));
			IL_0158:
			this.WriteCoordinateSystem();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000049C7 File Offset: 0x00002BC7
		private void WriteStartElement(string elementName)
		{
			this.writer.WriteStartElement("gml", elementName, "http://www.opengis.net/gml");
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000049E0 File Offset: 0x00002BE0
		private void WriteCoordinateSystem()
		{
			if (!this.coordinateSystemWritten && this.currentCoordinateSystem != null)
			{
				this.coordinateSystemWritten = true;
				string text = "http://www.opengis.net/def/crs/EPSG/0/" + this.currentCoordinateSystem.Id;
				this.writer.WriteAttributeString("gml", "srsName", "http://www.opengis.net/gml", text);
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00004A38 File Offset: 0x00002C38
		private void WritePoint(double x, double y, double? z, double? m)
		{
			this.WriteStartElement("pos");
			this.writer.WriteValue(x);
			this.writer.WriteValue(" ");
			this.writer.WriteValue(y);
			if (z != null)
			{
				this.writer.WriteValue(" ");
				this.writer.WriteValue(z.Value);
				if (m != null)
				{
					this.writer.WriteValue(" ");
					this.writer.WriteValue(m.Value);
				}
			}
			else if (m != null)
			{
				this.writer.WriteValue(" ");
				this.writer.WriteValue(double.NaN);
				this.writer.WriteValue(" ");
				this.writer.WriteValue(m.Value);
			}
			this.writer.WriteEndElement();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00004B2C File Offset: 0x00002D2C
		private void EndGeo()
		{
			switch (this.parentStack.Pop())
			{
			case SpatialType.Point:
				if (!this.figureWritten)
				{
					this.WriteStartElement("pos");
					this.writer.WriteEndElement();
				}
				this.writer.WriteEndElement();
				return;
			case SpatialType.LineString:
				if (!this.figureWritten)
				{
					this.WriteStartElement("posList");
					this.writer.WriteEndElement();
				}
				this.writer.WriteEndElement();
				return;
			case SpatialType.Polygon:
			case SpatialType.FullGlobe:
				this.writer.WriteEndElement();
				break;
			case SpatialType.MultiPoint:
			case SpatialType.MultiLineString:
			case SpatialType.MultiPolygon:
			case SpatialType.Collection:
				if (!this.shouldWriteContainerWrapper)
				{
					this.writer.WriteEndElement();
				}
				this.writer.WriteEndElement();
				this.shouldWriteContainerWrapper = false;
				return;
			case (SpatialType)8:
			case (SpatialType)9:
			case (SpatialType)10:
				break;
			default:
				return;
			}
		}

		// Token: 0x0400003E RID: 62
		private XmlWriter writer;

		// Token: 0x0400003F RID: 63
		private Stack<SpatialType> parentStack;

		// Token: 0x04000040 RID: 64
		private bool coordinateSystemWritten;

		// Token: 0x04000041 RID: 65
		private CoordinateSystem currentCoordinateSystem;

		// Token: 0x04000042 RID: 66
		private bool figureWritten;

		// Token: 0x04000043 RID: 67
		private bool shouldWriteContainerWrapper;
	}
}
