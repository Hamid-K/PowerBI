using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000047 RID: 71
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the common name for this format")]
	internal sealed class GmlWriter : DrawBoth
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00005212 File Offset: 0x00003412
		public GmlWriter(XmlWriter writer)
		{
			this.writer = writer;
			this.OnReset();
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x00005227 File Offset: 0x00003427
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x00005231 File Offset: 0x00003431
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.WritePoint(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00005252 File Offset: 0x00003452
		protected override void OnEndGeography()
		{
			this.EndGeo();
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000525A File Offset: 0x0000345A
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00005264 File Offset: 0x00003464
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.WritePoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00005285 File Offset: 0x00003485
		protected override void OnEndGeometry()
		{
			this.EndGeo();
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000528D File Offset: 0x0000348D
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.currentCoordinateSystem = coordinateSystem;
			return coordinateSystem;
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00005297 File Offset: 0x00003497
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.BeginFigure(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000052B8 File Offset: 0x000034B8
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.BeginFigure(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x060001DC RID: 476 RVA: 0x000052D9 File Offset: 0x000034D9
		protected override void OnEndFigure()
		{
			if (this.parentStack.Peek() == SpatialType.Polygon)
			{
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000052FF File Offset: 0x000034FF
		protected override void OnReset()
		{
			this.parentStack = new Stack<SpatialType>();
			this.coordinateSystemWritten = false;
			this.currentCoordinateSystem = null;
			this.figureWritten = false;
			this.shouldWriteContainerWrapper = false;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00005328 File Offset: 0x00003528
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

		// Token: 0x060001DF RID: 479 RVA: 0x0000537C File Offset: 0x0000357C
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

		// Token: 0x060001E0 RID: 480 RVA: 0x000054E9 File Offset: 0x000036E9
		private void WriteStartElement(string elementName)
		{
			this.writer.WriteStartElement("gml", elementName, "http://www.opengis.net/gml");
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00005504 File Offset: 0x00003704
		private void WriteCoordinateSystem()
		{
			if (!this.coordinateSystemWritten && this.currentCoordinateSystem != null)
			{
				this.coordinateSystemWritten = true;
				string text = "http://www.opengis.net/def/crs/EPSG/0/" + this.currentCoordinateSystem.Id;
				this.writer.WriteAttributeString("gml", "srsName", "http://www.opengis.net/gml", text);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000555C File Offset: 0x0000375C
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

		// Token: 0x060001E3 RID: 483 RVA: 0x00005650 File Offset: 0x00003850
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

		// Token: 0x04000044 RID: 68
		private XmlWriter writer;

		// Token: 0x04000045 RID: 69
		private Stack<SpatialType> parentStack;

		// Token: 0x04000046 RID: 70
		private bool coordinateSystemWritten;

		// Token: 0x04000047 RID: 71
		private CoordinateSystem currentCoordinateSystem;

		// Token: 0x04000048 RID: 72
		private bool figureWritten;

		// Token: 0x04000049 RID: 73
		private bool shouldWriteContainerWrapper;
	}
}
