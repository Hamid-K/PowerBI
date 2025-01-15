using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml;

namespace Microsoft.Spatial
{
	// Token: 0x02000048 RID: 72
	[SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Gml", Justification = "Gml is the common name for this format")]
	internal sealed class GmlWriter : DrawBoth
	{
		// Token: 0x06000216 RID: 534 RVA: 0x000053D5 File Offset: 0x000035D5
		public GmlWriter(XmlWriter writer)
		{
			this.writer = writer;
			this.OnReset();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000053EA File Offset: 0x000035EA
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000053F4 File Offset: 0x000035F4
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.WritePoint(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00005415 File Offset: 0x00003615
		protected override void OnEndGeography()
		{
			this.EndGeo();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000053EA File Offset: 0x000035EA
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginGeo(type);
			return type;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000541D File Offset: 0x0000361D
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.WritePoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00005415 File Offset: 0x00003615
		protected override void OnEndGeometry()
		{
			this.EndGeo();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000543E File Offset: 0x0000363E
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.currentCoordinateSystem = coordinateSystem;
			return coordinateSystem;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00005448 File Offset: 0x00003648
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.BeginFigure(position.Latitude, position.Longitude, position.Z, position.M);
			return position;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00005469 File Offset: 0x00003669
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.BeginFigure(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000548A File Offset: 0x0000368A
		protected override void OnEndFigure()
		{
			if (this.parentStack.Peek() == SpatialType.Polygon)
			{
				this.writer.WriteEndElement();
				this.writer.WriteEndElement();
			}
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000054B0 File Offset: 0x000036B0
		protected override void OnReset()
		{
			this.parentStack = new Stack<SpatialType>();
			this.coordinateSystemWritten = false;
			this.currentCoordinateSystem = null;
			this.figureWritten = false;
			this.shouldWriteContainerWrapper = false;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000054DC File Offset: 0x000036DC
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

		// Token: 0x06000223 RID: 547 RVA: 0x00005530 File Offset: 0x00003730
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

		// Token: 0x06000224 RID: 548 RVA: 0x0000569B File Offset: 0x0000389B
		private void WriteStartElement(string elementName)
		{
			this.writer.WriteStartElement("gml", elementName, "http://www.opengis.net/gml");
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000056B4 File Offset: 0x000038B4
		private void WriteCoordinateSystem()
		{
			if (!this.coordinateSystemWritten && this.currentCoordinateSystem != null)
			{
				this.coordinateSystemWritten = true;
				string text = "http://www.opengis.net/def/crs/EPSG/0/" + this.currentCoordinateSystem.Id;
				this.writer.WriteAttributeString("srsName", text);
			}
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00005700 File Offset: 0x00003900
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

		// Token: 0x06000227 RID: 551 RVA: 0x000057F4 File Offset: 0x000039F4
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

		// Token: 0x0400004B RID: 75
		private XmlWriter writer;

		// Token: 0x0400004C RID: 76
		private Stack<SpatialType> parentStack;

		// Token: 0x0400004D RID: 77
		private bool coordinateSystemWritten;

		// Token: 0x0400004E RID: 78
		private CoordinateSystem currentCoordinateSystem;

		// Token: 0x0400004F RID: 79
		private bool figureWritten;

		// Token: 0x04000050 RID: 80
		private bool shouldWriteContainerWrapper;
	}
}
