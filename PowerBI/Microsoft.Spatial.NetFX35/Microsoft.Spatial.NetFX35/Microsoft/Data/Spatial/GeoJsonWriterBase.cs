using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000008 RID: 8
	internal abstract class GeoJsonWriterBase : DrawBoth
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00002584 File Offset: 0x00000784
		public GeoJsonWriterBase()
		{
			this.stack = new Stack<SpatialType>();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002597 File Offset: 0x00000797
		private bool ShapeHasObjectScope
		{
			get
			{
				return this.IsTopLevel || this.stack.Peek() == SpatialType.Collection;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000025B1 File Offset: 0x000007B1
		private bool IsTopLevel
		{
			get
			{
				return this.stack.Count == 0;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000025C1 File Offset: 0x000007C1
		private bool FigureHasArrayScope
		{
			get
			{
				return this.stack.Peek() != SpatialType.Point;
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000025D4 File Offset: 0x000007D4
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.WriteControlPoint(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x000025F5 File Offset: 0x000007F5
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.WriteControlPoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002616 File Offset: 0x00000816
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginShape(type, CoordinateSystem.DefaultGeography);
			return type;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002625 File Offset: 0x00000825
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginShape(type, CoordinateSystem.DefaultGeometry);
			return type;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002634 File Offset: 0x00000834
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.BeginFigure();
			this.WriteControlPoint(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000265B File Offset: 0x0000085B
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.BeginFigure();
			this.WriteControlPoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002682 File Offset: 0x00000882
		protected override void OnEndFigure()
		{
			this.EndFigure();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000268A File Offset: 0x0000088A
		protected override void OnEndGeography()
		{
			this.EndShape();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002692 File Offset: 0x00000892
		protected override void OnEndGeometry()
		{
			this.EndShape();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000269A File Offset: 0x0000089A
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.SetCoordinateSystem(coordinateSystem);
			return coordinateSystem;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x000026A4 File Offset: 0x000008A4
		protected override void OnReset()
		{
			this.Reset();
		}

		// Token: 0x06000065 RID: 101
		protected abstract void AddPropertyName(string name);

		// Token: 0x06000066 RID: 102
		protected abstract void AddValue(string value);

		// Token: 0x06000067 RID: 103
		protected abstract void AddValue(double value);

		// Token: 0x06000068 RID: 104
		protected abstract void StartObjectScope();

		// Token: 0x06000069 RID: 105
		protected abstract void StartArrayScope();

		// Token: 0x0600006A RID: 106
		protected abstract void EndObjectScope();

		// Token: 0x0600006B RID: 107
		protected abstract void EndArrayScope();

		// Token: 0x0600006C RID: 108 RVA: 0x000026AC File Offset: 0x000008AC
		protected virtual void Reset()
		{
			this.stack.Clear();
			this.currentCoordinateSystem = null;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000026C0 File Offset: 0x000008C0
		private static string GetSpatialTypeName(SpatialType type)
		{
			switch (type)
			{
			case SpatialType.Point:
				return "Point";
			case SpatialType.LineString:
				return "LineString";
			case SpatialType.Polygon:
				return "Polygon";
			case SpatialType.MultiPoint:
				return "MultiPoint";
			case SpatialType.MultiLineString:
				return "MultiLineString";
			case SpatialType.MultiPolygon:
				return "MultiPolygon";
			case SpatialType.Collection:
				return "GeometryCollection";
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002724 File Offset: 0x00000924
		private static string GetDataName(SpatialType type)
		{
			switch (type)
			{
			case SpatialType.Point:
			case SpatialType.LineString:
			case SpatialType.Polygon:
			case SpatialType.MultiPoint:
			case SpatialType.MultiLineString:
			case SpatialType.MultiPolygon:
				return "coordinates";
			case SpatialType.Collection:
				return "geometries";
			default:
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000276A File Offset: 0x0000096A
		private static bool TypeHasArrayScope(SpatialType type)
		{
			return type != SpatialType.Point && type != SpatialType.LineString;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002779 File Offset: 0x00000979
		private void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.currentCoordinateSystem = coordinateSystem;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00002784 File Offset: 0x00000984
		private void BeginShape(SpatialType type, CoordinateSystem defaultCoordinateSystem)
		{
			if (this.currentCoordinateSystem == null)
			{
				this.currentCoordinateSystem = defaultCoordinateSystem;
			}
			if (this.ShapeHasObjectScope)
			{
				this.WriteShapeHeader(type);
			}
			if (GeoJsonWriterBase.TypeHasArrayScope(type))
			{
				this.StartArrayScope();
			}
			this.stack.Push(type);
			this.figureDrawn = false;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000027D0 File Offset: 0x000009D0
		private void WriteShapeHeader(SpatialType type)
		{
			this.StartObjectScope();
			this.AddPropertyName("type");
			this.AddValue(GeoJsonWriterBase.GetSpatialTypeName(type));
			this.AddPropertyName(GeoJsonWriterBase.GetDataName(type));
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000027FB File Offset: 0x000009FB
		private void BeginFigure()
		{
			if (this.FigureHasArrayScope)
			{
				this.StartArrayScope();
			}
			this.figureDrawn = true;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002814 File Offset: 0x00000A14
		private void WriteControlPoint(double first, double second, double? z, double? m)
		{
			this.StartArrayScope();
			this.AddValue(first);
			this.AddValue(second);
			if (z != null)
			{
				this.AddValue(z.Value);
				if (m != null)
				{
					this.AddValue(m.Value);
				}
			}
			else if (m != null)
			{
				this.AddValue(null);
				this.AddValue(m.Value);
			}
			this.EndArrayScope();
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002886 File Offset: 0x00000A86
		private void EndFigure()
		{
			if (this.FigureHasArrayScope)
			{
				this.EndArrayScope();
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002898 File Offset: 0x00000A98
		private void EndShape()
		{
			SpatialType spatialType = this.stack.Pop();
			if (GeoJsonWriterBase.TypeHasArrayScope(spatialType))
			{
				this.EndArrayScope();
			}
			else if (!this.figureDrawn)
			{
				this.StartArrayScope();
				this.EndArrayScope();
			}
			if (this.IsTopLevel)
			{
				this.WriteCrs();
			}
			if (this.ShapeHasObjectScope)
			{
				this.EndObjectScope();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000028F4 File Offset: 0x00000AF4
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "Microsoft.Data.Spatial.JsonWriter.WriteQuotedString(System.String)", Justification = "Values have no localized content.")]
		private void WriteCrs()
		{
			this.AddPropertyName("crs");
			this.StartObjectScope();
			this.AddPropertyName("type");
			this.AddValue("name");
			this.AddPropertyName("properties");
			this.StartObjectScope();
			this.AddPropertyName("name");
			this.AddValue("EPSG" + ':' + this.currentCoordinateSystem.Id);
			this.EndObjectScope();
			this.EndObjectScope();
		}

		// Token: 0x04000009 RID: 9
		private readonly Stack<SpatialType> stack;

		// Token: 0x0400000A RID: 10
		private CoordinateSystem currentCoordinateSystem;

		// Token: 0x0400000B RID: 11
		private bool figureDrawn;
	}
}
