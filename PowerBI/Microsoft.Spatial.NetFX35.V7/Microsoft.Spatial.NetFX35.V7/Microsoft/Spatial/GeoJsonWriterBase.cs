using System;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x02000007 RID: 7
	internal abstract class GeoJsonWriterBase : DrawBoth
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000026FA File Offset: 0x000008FA
		public GeoJsonWriterBase()
		{
			this.stack = new Stack<SpatialType>();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000270D File Offset: 0x0000090D
		private bool ShapeHasObjectScope
		{
			get
			{
				return this.IsTopLevel || this.stack.Peek() == SpatialType.Collection;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002727 File Offset: 0x00000927
		private bool IsTopLevel
		{
			get
			{
				return this.stack.Count == 0;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00002737 File Offset: 0x00000937
		private bool FigureHasArrayScope
		{
			get
			{
				return this.stack.Peek() != SpatialType.Point;
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x0000274A File Offset: 0x0000094A
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.WriteControlPoint(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x0000276B File Offset: 0x0000096B
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.WriteControlPoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000278C File Offset: 0x0000098C
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginShape(type, CoordinateSystem.DefaultGeography);
			return type;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x0000279B File Offset: 0x0000099B
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginShape(type, CoordinateSystem.DefaultGeometry);
			return type;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000027AA File Offset: 0x000009AA
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.BeginFigure();
			this.WriteControlPoint(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x000027D1 File Offset: 0x000009D1
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.BeginFigure();
			this.WriteControlPoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000027F8 File Offset: 0x000009F8
		protected override void OnEndFigure()
		{
			this.EndFigure();
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002800 File Offset: 0x00000A00
		protected override void OnEndGeography()
		{
			this.EndShape();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002800 File Offset: 0x00000A00
		protected override void OnEndGeometry()
		{
			this.EndShape();
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002808 File Offset: 0x00000A08
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.SetCoordinateSystem(coordinateSystem);
			return coordinateSystem;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002812 File Offset: 0x00000A12
		protected override void OnReset()
		{
			this.Reset();
		}

		// Token: 0x06000052 RID: 82
		protected abstract void AddPropertyName(string name);

		// Token: 0x06000053 RID: 83
		protected abstract void AddValue(string value);

		// Token: 0x06000054 RID: 84
		protected abstract void AddValue(double value);

		// Token: 0x06000055 RID: 85
		protected abstract void StartObjectScope();

		// Token: 0x06000056 RID: 86
		protected abstract void StartArrayScope();

		// Token: 0x06000057 RID: 87
		protected abstract void EndObjectScope();

		// Token: 0x06000058 RID: 88
		protected abstract void EndArrayScope();

		// Token: 0x06000059 RID: 89 RVA: 0x0000281A File Offset: 0x00000A1A
		protected virtual void Reset()
		{
			this.stack.Clear();
			this.currentCoordinateSystem = null;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002830 File Offset: 0x00000A30
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

		// Token: 0x0600005B RID: 91 RVA: 0x00002892 File Offset: 0x00000A92
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

		// Token: 0x0600005C RID: 92 RVA: 0x000028CB File Offset: 0x00000ACB
		private static bool TypeHasArrayScope(SpatialType type)
		{
			return type != SpatialType.Point && type != SpatialType.LineString;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000028DA File Offset: 0x00000ADA
		private void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.currentCoordinateSystem = coordinateSystem;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000028E4 File Offset: 0x00000AE4
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

		// Token: 0x0600005F RID: 95 RVA: 0x00002930 File Offset: 0x00000B30
		private void WriteShapeHeader(SpatialType type)
		{
			this.StartObjectScope();
			this.AddPropertyName("type");
			this.AddValue(GeoJsonWriterBase.GetSpatialTypeName(type));
			this.AddPropertyName(GeoJsonWriterBase.GetDataName(type));
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000295B File Offset: 0x00000B5B
		private void BeginFigure()
		{
			if (this.FigureHasArrayScope)
			{
				this.StartArrayScope();
			}
			this.figureDrawn = true;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002974 File Offset: 0x00000B74
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

		// Token: 0x06000062 RID: 98 RVA: 0x000029E6 File Offset: 0x00000BE6
		private void EndFigure()
		{
			if (this.FigureHasArrayScope)
			{
				this.EndArrayScope();
			}
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000029F8 File Offset: 0x00000BF8
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

		// Token: 0x06000064 RID: 100 RVA: 0x00002A54 File Offset: 0x00000C54
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

		// Token: 0x04000010 RID: 16
		private readonly Stack<SpatialType> stack;

		// Token: 0x04000011 RID: 17
		private CoordinateSystem currentCoordinateSystem;

		// Token: 0x04000012 RID: 18
		private bool figureDrawn;
	}
}
