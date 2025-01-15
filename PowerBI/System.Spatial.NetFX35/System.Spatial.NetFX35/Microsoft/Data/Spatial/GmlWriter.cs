using System;
using System.Collections.Generic;
using System.Spatial;
using System.Xml;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000046 RID: 70
	internal sealed class GmlWriter : DrawBoth
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x000052A2 File Offset: 0x000034A2
		public GmlWriter(XmlWriter writer)
		{
			this.writer = writer;
			this.OnReset();
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000052B7 File Offset: 0x000034B7
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000052C1 File Offset: 0x000034C1
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.WritePoint(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000052E2 File Offset: 0x000034E2
		protected override void OnEndGeography()
		{
			this.EndGeo();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x000052EA File Offset: 0x000034EA
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000052F4 File Offset: 0x000034F4
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.WritePoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00005315 File Offset: 0x00003515
		protected override void OnEndGeometry()
		{
			this.EndGeo();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000531D File Offset: 0x0000351D
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.currentCoordinateSystem = coordinateSystem;
			return coordinateSystem;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005327 File Offset: 0x00003527
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.BeginFigure(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00005348 File Offset: 0x00003548
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.BeginFigure(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00005369 File Offset: 0x00003569
		protected override void OnEndFigure()
		{
			if (this.parentStack.Peek() == SpatialType.Polygon)
			{
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000538F File Offset: 0x0000358F
		protected override void OnReset()
		{
			this.parentStack = new Stack<SpatialType>();
			this.coordinateSystemWritten = false;
			this.currentCoordinateSystem = null;
			this.figureWritten = false;
			this.shouldWriteContainerWrapper = false;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000053B8 File Offset: 0x000035B8
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

		// Token: 0x060001D5 RID: 469 RVA: 0x0000540C File Offset: 0x0000360C
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
				goto IL_015A;
			case SpatialType.LineString:
				this.WriteStartElement("LineString");
				goto IL_015A;
			case SpatialType.Polygon:
				this.WriteStartElement("Polygon");
				goto IL_015A;
			case SpatialType.MultiPoint:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiPoint");
				goto IL_015A;
			case SpatialType.MultiLineString:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiCurve");
				goto IL_015A;
			case SpatialType.MultiPolygon:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiSurface");
				goto IL_015A;
			case SpatialType.Collection:
				this.shouldWriteContainerWrapper = true;
				this.WriteStartElement("MultiGeometry");
				goto IL_015A;
			case SpatialType.FullGlobe:
				this.writer.WriteStartElement("FullGlobe", "http://schemas.microsoft.com/sqlserver/2011/geography");
				goto IL_015A;
			}
			throw new NotSupportedException(Strings.Validator_InvalidType(type));
			IL_015A:
			this.WriteCoordinateSystem();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x00005579 File Offset: 0x00003779
		private void WriteStartElement(string elementName)
		{
			this.writer.WriteStartElement("gml", elementName, "http://www.opengis.net/gml");
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005594 File Offset: 0x00003794
		private void WriteCoordinateSystem()
		{
			if (!this.coordinateSystemWritten && this.currentCoordinateSystem != null)
			{
				this.coordinateSystemWritten = true;
				string text = "http://www.opengis.net/def/crs/EPSG/0/" + this.currentCoordinateSystem.Id;
				this.writer.WriteAttributeString("gml", "srsName", "http://www.opengis.net/gml", text);
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x000055EC File Offset: 0x000037EC
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

		// Token: 0x060001D9 RID: 473 RVA: 0x000056E0 File Offset: 0x000038E0
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

		// Token: 0x04000042 RID: 66
		private XmlWriter writer;

		// Token: 0x04000043 RID: 67
		private Stack<SpatialType> parentStack;

		// Token: 0x04000044 RID: 68
		private bool coordinateSystemWritten;

		// Token: 0x04000045 RID: 69
		private CoordinateSystem currentCoordinateSystem;

		// Token: 0x04000046 RID: 70
		private bool figureWritten;

		// Token: 0x04000047 RID: 71
		private bool shouldWriteContainerWrapper;
	}
}
