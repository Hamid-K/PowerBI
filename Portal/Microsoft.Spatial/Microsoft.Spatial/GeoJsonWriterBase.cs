using System;
using System.Collections.Generic;

namespace Microsoft.Spatial
{
	// Token: 0x02000007 RID: 7
	internal abstract class GeoJsonWriterBase : DrawBoth
	{
		// Token: 0x06000055 RID: 85 RVA: 0x00002AE2 File Offset: 0x00000CE2
		public GeoJsonWriterBase()
		{
			this.stack = new Stack<SpatialType>();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002AF5 File Offset: 0x00000CF5
		private bool ShapeHasObjectScope
		{
			get
			{
				return this.IsTopLevel || this.stack.Peek() == SpatialType.Collection;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00002B0F File Offset: 0x00000D0F
		private bool IsTopLevel
		{
			get
			{
				return this.stack.Count == 0;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002B1F File Offset: 0x00000D1F
		private bool FigureHasArrayScope
		{
			get
			{
				return this.stack.Peek() != SpatialType.Point;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00002B32 File Offset: 0x00000D32
		protected override GeographyPosition OnLineTo(GeographyPosition position)
		{
			this.WriteControlPoint(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00002B53 File Offset: 0x00000D53
		protected override GeometryPosition OnLineTo(GeometryPosition position)
		{
			this.WriteControlPoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002B74 File Offset: 0x00000D74
		protected override SpatialType OnBeginGeography(SpatialType type)
		{
			this.BeginShape(type, CoordinateSystem.DefaultGeography);
			return type;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002B83 File Offset: 0x00000D83
		protected override SpatialType OnBeginGeometry(SpatialType type)
		{
			this.BeginShape(type, CoordinateSystem.DefaultGeometry);
			return type;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002B92 File Offset: 0x00000D92
		protected override GeographyPosition OnBeginFigure(GeographyPosition position)
		{
			this.BeginFigure();
			this.WriteControlPoint(position.Longitude, position.Latitude, position.Z, position.M);
			return position;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002BB9 File Offset: 0x00000DB9
		protected override GeometryPosition OnBeginFigure(GeometryPosition position)
		{
			this.BeginFigure();
			this.WriteControlPoint(position.X, position.Y, position.Z, position.M);
			return position;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002BE0 File Offset: 0x00000DE0
		protected override void OnEndFigure()
		{
			this.EndFigure();
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002BE8 File Offset: 0x00000DE8
		protected override void OnEndGeography()
		{
			this.EndShape();
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002BE8 File Offset: 0x00000DE8
		protected override void OnEndGeometry()
		{
			this.EndShape();
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00002BF0 File Offset: 0x00000DF0
		protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.SetCoordinateSystem(coordinateSystem);
			return coordinateSystem;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00002BFA File Offset: 0x00000DFA
		protected override void OnReset()
		{
			this.Reset();
		}

		// Token: 0x06000064 RID: 100
		protected abstract void AddPropertyName(string name);

		// Token: 0x06000065 RID: 101
		protected abstract void AddValue(string value);

		// Token: 0x06000066 RID: 102
		protected abstract void AddValue(double value);

		// Token: 0x06000067 RID: 103
		protected abstract void StartObjectScope();

		// Token: 0x06000068 RID: 104
		protected abstract void StartArrayScope();

		// Token: 0x06000069 RID: 105
		protected abstract void EndObjectScope();

		// Token: 0x0600006A RID: 106
		protected abstract void EndArrayScope();

		// Token: 0x0600006B RID: 107 RVA: 0x00002C02 File Offset: 0x00000E02
		protected virtual void Reset()
		{
			this.stack.Clear();
			this.currentCoordinateSystem = null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002C18 File Offset: 0x00000E18
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

		// Token: 0x0600006D RID: 109 RVA: 0x00002C7A File Offset: 0x00000E7A
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

		// Token: 0x0600006E RID: 110 RVA: 0x00002CB3 File Offset: 0x00000EB3
		private static bool TypeHasArrayScope(SpatialType type)
		{
			return type != SpatialType.Point && type != SpatialType.LineString;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00002CC2 File Offset: 0x00000EC2
		private void SetCoordinateSystem(CoordinateSystem coordinateSystem)
		{
			this.currentCoordinateSystem = coordinateSystem;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002CCC File Offset: 0x00000ECC
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

		// Token: 0x06000071 RID: 113 RVA: 0x00002D18 File Offset: 0x00000F18
		private void WriteShapeHeader(SpatialType type)
		{
			this.StartObjectScope();
			this.AddPropertyName("type");
			this.AddValue(GeoJsonWriterBase.GetSpatialTypeName(type));
			this.AddPropertyName(GeoJsonWriterBase.GetDataName(type));
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002D43 File Offset: 0x00000F43
		private void BeginFigure()
		{
			if (this.FigureHasArrayScope)
			{
				this.StartArrayScope();
			}
			this.figureDrawn = true;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002D5C File Offset: 0x00000F5C
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

		// Token: 0x06000074 RID: 116 RVA: 0x00002DCE File Offset: 0x00000FCE
		private void EndFigure()
		{
			if (this.FigureHasArrayScope)
			{
				this.EndArrayScope();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002DE0 File Offset: 0x00000FE0
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

		// Token: 0x06000076 RID: 118 RVA: 0x00002E3C File Offset: 0x0000103C
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

		// Token: 0x04000011 RID: 17
		private readonly Stack<SpatialType> stack;

		// Token: 0x04000012 RID: 18
		private CoordinateSystem currentCoordinateSystem;

		// Token: 0x04000013 RID: 19
		private bool figureDrawn;
	}
}
