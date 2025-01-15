using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Spatial;

namespace Microsoft.Data.Spatial
{
	// Token: 0x0200006A RID: 106
	internal class SpatialValidatorImplementation : SpatialPipeline
	{
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000797F File Offset: 0x00005B7F
		public override GeographyPipeline GeographyPipeline
		{
			get
			{
				return this.geographyValidatorInstance.GeographyPipeline;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000798C File Offset: 0x00005B8C
		public override GeometryPipeline GeometryPipeline
		{
			get
			{
				return this.geometryValidatorInstance.GeometryPipeline;
			}
		}

		// Token: 0x040000B9 RID: 185
		internal const double MaxLongitude = 15069.0;

		// Token: 0x040000BA RID: 186
		internal const double MaxLatitude = 90.0;

		// Token: 0x040000BB RID: 187
		private readonly SpatialValidatorImplementation.NestedValidator geographyValidatorInstance = new SpatialValidatorImplementation.NestedValidator();

		// Token: 0x040000BC RID: 188
		private readonly SpatialValidatorImplementation.NestedValidator geometryValidatorInstance = new SpatialValidatorImplementation.NestedValidator();

		// Token: 0x0200006B RID: 107
		private class NestedValidator : DrawBoth
		{
			// Token: 0x060002BA RID: 698 RVA: 0x000079B7 File Offset: 0x00005BB7
			public NestedValidator()
			{
				this.InitializeObject();
			}

			// Token: 0x060002BB RID: 699 RVA: 0x000079D4 File Offset: 0x00005BD4
			protected override CoordinateSystem OnSetCoordinateSystem(CoordinateSystem coordinateSystem)
			{
				SpatialValidatorImplementation.NestedValidator.ValidatorState validatorState = this.stack.Peek();
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem);
				if (validatorState == SpatialValidatorImplementation.NestedValidator.CoordinateSystem)
				{
					this.validationCoordinateSystem = coordinateSystem;
				}
				else if (this.validationCoordinateSystem != coordinateSystem)
				{
					throw new FormatException(Strings.Validator_SridMismatch);
				}
				return coordinateSystem;
			}

			// Token: 0x060002BC RID: 700 RVA: 0x00007A1A File Offset: 0x00005C1A
			protected override SpatialType OnBeginGeography(SpatialType shape)
			{
				if (this.depth > 0 && !this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeometry);
				}
				this.processingGeography = true;
				this.BeginShape(shape);
				return shape;
			}

			// Token: 0x060002BD RID: 701 RVA: 0x00007A47 File Offset: 0x00005C47
			protected override void OnEndGeography()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.End);
				if (!this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeometry);
				}
				this.depth--;
			}

			// Token: 0x060002BE RID: 702 RVA: 0x00007A72 File Offset: 0x00005C72
			protected override SpatialType OnBeginGeometry(SpatialType shape)
			{
				if (this.depth > 0 && this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeography);
				}
				this.processingGeography = false;
				this.BeginShape(shape);
				return shape;
			}

			// Token: 0x060002BF RID: 703 RVA: 0x00007A9F File Offset: 0x00005C9F
			protected override void OnEndGeometry()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.End);
				if (this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeography);
				}
				this.depth--;
			}

			// Token: 0x060002C0 RID: 704 RVA: 0x00007ACA File Offset: 0x00005CCA
			protected override GeographyPosition OnBeginFigure(GeographyPosition position)
			{
				this.BeginFigure(new Action<double, double, double?, double?>(SpatialValidatorImplementation.NestedValidator.ValidateGeographyPosition), position.Latitude, position.Longitude, position.Z, position.M);
				return position;
			}

			// Token: 0x060002C1 RID: 705 RVA: 0x00007AF7 File Offset: 0x00005CF7
			protected override GeometryPosition OnBeginFigure(GeometryPosition position)
			{
				this.BeginFigure(new Action<double, double, double?, double?>(SpatialValidatorImplementation.NestedValidator.ValidateGeometryPosition), position.X, position.Y, position.Z, position.M);
				return position;
			}

			// Token: 0x060002C2 RID: 706 RVA: 0x00007B24 File Offset: 0x00005D24
			protected override void OnEndFigure()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure);
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x00007B2E File Offset: 0x00005D2E
			protected override void OnReset()
			{
				this.InitializeObject();
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x00007B38 File Offset: 0x00005D38
			protected override GeographyPosition OnLineTo(GeographyPosition position)
			{
				if (this.processingGeography)
				{
					SpatialValidatorImplementation.NestedValidator.ValidateGeographyPosition(position.Latitude, position.Longitude, position.Z, position.M);
				}
				this.AddControlPoint(position.Latitude, position.Longitude);
				if (!this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeometry);
				}
				return position;
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x00007B90 File Offset: 0x00005D90
			protected override GeometryPosition OnLineTo(GeometryPosition position)
			{
				if (!this.processingGeography)
				{
					SpatialValidatorImplementation.NestedValidator.ValidateGeometryPosition(position.X, position.Y, position.Z, position.M);
				}
				this.AddControlPoint(position.X, position.Y);
				if (this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeography);
				}
				return position;
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x00007BE8 File Offset: 0x00005DE8
			private static bool IsFinite(double value)
			{
				return !double.IsNaN(value) && !double.IsInfinity(value);
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x00007C00 File Offset: 0x00005E00
			[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
			private static bool IsPointValid(double first, double second, double? z, double? m)
			{
				return SpatialValidatorImplementation.NestedValidator.IsFinite(first) && SpatialValidatorImplementation.NestedValidator.IsFinite(second) && (z == null || SpatialValidatorImplementation.NestedValidator.IsFinite(z.Value)) && (m == null || SpatialValidatorImplementation.NestedValidator.IsFinite(m.Value));
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x00007C4D File Offset: 0x00005E4D
			private static void ValidateOnePosition(double first, double second, double? z, double? m)
			{
				if (!SpatialValidatorImplementation.NestedValidator.IsPointValid(first, second, z, m))
				{
					throw new FormatException(Strings.Validator_InvalidPointCoordinate(first, second, z, m));
				}
			}

			// Token: 0x060002C9 RID: 713 RVA: 0x00007C7D File Offset: 0x00005E7D
			private static void ValidateGeographyPosition(double latitude, double longitude, double? z, double? m)
			{
				SpatialValidatorImplementation.NestedValidator.ValidateOnePosition(latitude, longitude, z, m);
				if (!SpatialValidatorImplementation.NestedValidator.IsLatitudeValid(latitude))
				{
					throw new FormatException(Strings.Validator_InvalidLatitudeCoordinate(latitude));
				}
				if (!SpatialValidatorImplementation.NestedValidator.IsLongitudeValid(longitude))
				{
					throw new FormatException(Strings.Validator_InvalidLongitudeCoordinate(longitude));
				}
			}

			// Token: 0x060002CA RID: 714 RVA: 0x00007CBA File Offset: 0x00005EBA
			private static void ValidateGeometryPosition(double x, double y, double? z, double? m)
			{
				SpatialValidatorImplementation.NestedValidator.ValidateOnePosition(x, y, z, m);
			}

			// Token: 0x060002CB RID: 715 RVA: 0x00007CC5 File Offset: 0x00005EC5
			private static bool IsLatitudeValid(double latitude)
			{
				return latitude >= -90.0 && latitude <= 90.0;
			}

			// Token: 0x060002CC RID: 716 RVA: 0x00007CE4 File Offset: 0x00005EE4
			private static bool IsLongitudeValid(double longitude)
			{
				return longitude >= -15069.0 && longitude <= 15069.0;
			}

			// Token: 0x060002CD RID: 717 RVA: 0x00007D03 File Offset: 0x00005F03
			private static void ValidateGeographyPolygon(int numOfPoints, double initialFirstCoordinate, double initialSecondCoordinate, double mostRecentFirstCoordinate, double mostRecentSecondCoordinate)
			{
				if (numOfPoints < 4 || initialFirstCoordinate != mostRecentFirstCoordinate || !SpatialValidatorImplementation.NestedValidator.AreLongitudesEqual(initialSecondCoordinate, mostRecentSecondCoordinate))
				{
					throw new FormatException(Strings.Validator_InvalidPolygonPoints);
				}
			}

			// Token: 0x060002CE RID: 718 RVA: 0x00007D22 File Offset: 0x00005F22
			private static void ValidateGeometryPolygon(int numOfPoints, double initialFirstCoordinate, double initialSecondCoordinate, double mostRecentFirstCoordinate, double mostRecentSecondCoordinate)
			{
				if (numOfPoints < 4 || initialFirstCoordinate != mostRecentFirstCoordinate || initialSecondCoordinate != mostRecentSecondCoordinate)
				{
					throw new FormatException(Strings.Validator_InvalidPolygonPoints);
				}
			}

			// Token: 0x060002CF RID: 719 RVA: 0x00007D3C File Offset: 0x00005F3C
			private static bool AreLongitudesEqual(double left, double right)
			{
				return left == right || (left - right) % 360.0 == 0.0;
			}

			// Token: 0x060002D0 RID: 720 RVA: 0x00007D5C File Offset: 0x00005F5C
			private void BeginFigure(Action<double, double, double?, double?> validate, double x, double y, double? z, double? m)
			{
				validate.Invoke(x, y, z, m);
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure);
				this.pointCount = 0;
				this.TrackPosition(x, y);
			}

			// Token: 0x060002D1 RID: 721 RVA: 0x00007D84 File Offset: 0x00005F84
			private void BeginShape(SpatialType type)
			{
				this.depth++;
				switch (type)
				{
				case SpatialType.Point:
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPoint);
					return;
				case SpatialType.LineString:
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginLineString);
					return;
				case SpatialType.Polygon:
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPolygon);
					return;
				case SpatialType.MultiPoint:
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiPoint);
					return;
				case SpatialType.MultiLineString:
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiLineString);
					return;
				case SpatialType.MultiPolygon:
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiPolygon);
					return;
				case SpatialType.Collection:
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginCollection);
					return;
				case SpatialType.FullGlobe:
					if (!this.processingGeography)
					{
						throw new FormatException(Strings.Validator_InvalidType(type));
					}
					this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFullGlobe);
					return;
				}
				throw new FormatException(Strings.Validator_InvalidType(type));
			}

			// Token: 0x060002D2 RID: 722 RVA: 0x00007E41 File Offset: 0x00006041
			private void AddControlPoint(double first, double second)
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo);
				this.TrackPosition(first, second);
			}

			// Token: 0x060002D3 RID: 723 RVA: 0x00007E53 File Offset: 0x00006053
			private void TrackPosition(double first, double second)
			{
				if (this.pointCount == 0)
				{
					this.initialFirstCoordinate = first;
					this.initialSecondCoordinate = second;
				}
				this.mostRecentFirstCoordinate = first;
				this.mostRecentSecondCoordinate = second;
				this.pointCount++;
			}

			// Token: 0x060002D4 RID: 724 RVA: 0x00007E88 File Offset: 0x00006088
			private void Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall transition)
			{
				SpatialValidatorImplementation.NestedValidator.ValidatorState validatorState = this.stack.Peek();
				validatorState.ValidateTransition(transition, this);
			}

			// Token: 0x060002D5 RID: 725 RVA: 0x00007EAC File Offset: 0x000060AC
			private void InitializeObject()
			{
				this.depth = 0;
				this.initialFirstCoordinate = 0.0;
				this.initialSecondCoordinate = 0.0;
				this.mostRecentFirstCoordinate = 0.0;
				this.mostRecentSecondCoordinate = 0.0;
				this.pointCount = 0;
				this.validationCoordinateSystem = null;
				this.ringCount = 0;
				this.stack.Clear();
				this.stack.Push(SpatialValidatorImplementation.NestedValidator.CoordinateSystem);
			}

			// Token: 0x060002D6 RID: 726 RVA: 0x00007F2C File Offset: 0x0000612C
			private void Call(SpatialValidatorImplementation.NestedValidator.ValidatorState state)
			{
				if (this.stack.Count > 28)
				{
					throw new FormatException(Strings.Validator_NestingOverflow(28));
				}
				this.stack.Push(state);
			}

			// Token: 0x060002D7 RID: 727 RVA: 0x00007F5B File Offset: 0x0000615B
			private void Return()
			{
				this.stack.Pop();
			}

			// Token: 0x060002D8 RID: 728 RVA: 0x00007F69 File Offset: 0x00006169
			private void Jump(SpatialValidatorImplementation.NestedValidator.ValidatorState state)
			{
				this.stack.Pop();
				this.stack.Push(state);
			}

			// Token: 0x040000BD RID: 189
			private const int MaxGeometryCollectionDepth = 28;

			// Token: 0x040000BE RID: 190
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState CoordinateSystem = new SpatialValidatorImplementation.NestedValidator.SetCoordinateSystemState();

			// Token: 0x040000BF RID: 191
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState BeginSpatial = new SpatialValidatorImplementation.NestedValidator.BeginGeoState();

			// Token: 0x040000C0 RID: 192
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointStart = new SpatialValidatorImplementation.NestedValidator.PointStartState();

			// Token: 0x040000C1 RID: 193
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointBuilding = new SpatialValidatorImplementation.NestedValidator.PointBuildingState();

			// Token: 0x040000C2 RID: 194
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointEnd = new SpatialValidatorImplementation.NestedValidator.PointEndState();

			// Token: 0x040000C3 RID: 195
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringStart = new SpatialValidatorImplementation.NestedValidator.LineStringStartState();

			// Token: 0x040000C4 RID: 196
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringBuilding = new SpatialValidatorImplementation.NestedValidator.LineStringBuildingState();

			// Token: 0x040000C5 RID: 197
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringEnd = new SpatialValidatorImplementation.NestedValidator.LineStringEndState();

			// Token: 0x040000C6 RID: 198
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PolygonStart = new SpatialValidatorImplementation.NestedValidator.PolygonStartState();

			// Token: 0x040000C7 RID: 199
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PolygonBuilding = new SpatialValidatorImplementation.NestedValidator.PolygonBuildingState();

			// Token: 0x040000C8 RID: 200
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiPoint = new SpatialValidatorImplementation.NestedValidator.MultiPointState();

			// Token: 0x040000C9 RID: 201
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiLineString = new SpatialValidatorImplementation.NestedValidator.MultiLineStringState();

			// Token: 0x040000CA RID: 202
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiPolygon = new SpatialValidatorImplementation.NestedValidator.MultiPolygonState();

			// Token: 0x040000CB RID: 203
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState Collection = new SpatialValidatorImplementation.NestedValidator.CollectionState();

			// Token: 0x040000CC RID: 204
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState FullGlobe = new SpatialValidatorImplementation.NestedValidator.FullGlobeState();

			// Token: 0x040000CD RID: 205
			private readonly Stack<SpatialValidatorImplementation.NestedValidator.ValidatorState> stack = new Stack<SpatialValidatorImplementation.NestedValidator.ValidatorState>(16);

			// Token: 0x040000CE RID: 206
			private CoordinateSystem validationCoordinateSystem;

			// Token: 0x040000CF RID: 207
			private int ringCount;

			// Token: 0x040000D0 RID: 208
			private double initialFirstCoordinate;

			// Token: 0x040000D1 RID: 209
			private double initialSecondCoordinate;

			// Token: 0x040000D2 RID: 210
			private double mostRecentFirstCoordinate;

			// Token: 0x040000D3 RID: 211
			private double mostRecentSecondCoordinate;

			// Token: 0x040000D4 RID: 212
			private bool processingGeography;

			// Token: 0x040000D5 RID: 213
			private int pointCount;

			// Token: 0x040000D6 RID: 214
			private int depth;

			// Token: 0x0200006C RID: 108
			private enum PipelineCall
			{
				// Token: 0x040000D8 RID: 216
				SetCoordinateSystem,
				// Token: 0x040000D9 RID: 217
				Begin,
				// Token: 0x040000DA RID: 218
				BeginPoint,
				// Token: 0x040000DB RID: 219
				BeginLineString,
				// Token: 0x040000DC RID: 220
				BeginPolygon,
				// Token: 0x040000DD RID: 221
				BeginMultiPoint,
				// Token: 0x040000DE RID: 222
				BeginMultiLineString,
				// Token: 0x040000DF RID: 223
				BeginMultiPolygon,
				// Token: 0x040000E0 RID: 224
				BeginCollection,
				// Token: 0x040000E1 RID: 225
				BeginFullGlobe,
				// Token: 0x040000E2 RID: 226
				BeginFigure,
				// Token: 0x040000E3 RID: 227
				LineTo,
				// Token: 0x040000E4 RID: 228
				EndFigure,
				// Token: 0x040000E5 RID: 229
				End
			}

			// Token: 0x0200006D RID: 109
			private abstract class ValidatorState
			{
				// Token: 0x060002DA RID: 730
				internal abstract void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator);

				// Token: 0x060002DB RID: 731 RVA: 0x00008027 File Offset: 0x00006227
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall(transition, actual));
				}

				// Token: 0x060002DC RID: 732 RVA: 0x0000803F File Offset: 0x0000623F
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition1, SpatialValidatorImplementation.NestedValidator.PipelineCall transition2, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall2(transition1, transition2, actual));
				}

				// Token: 0x060002DD RID: 733 RVA: 0x0000805D File Offset: 0x0000625D
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition1, SpatialValidatorImplementation.NestedValidator.PipelineCall transition2, SpatialValidatorImplementation.NestedValidator.PipelineCall transition3, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall2(transition1 + ", " + transition2, transition3, actual));
				}
			}

			// Token: 0x0200006E RID: 110
			private class SetCoordinateSystemState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002DF RID: 735 RVA: 0x00008094 File Offset: 0x00006294
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem)
					{
						validator.Call(SpatialValidatorImplementation.NestedValidator.BeginSpatial);
						return;
					}
					SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem, transition);
				}
			}

			// Token: 0x0200006F RID: 111
			private class BeginGeoState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002E1 RID: 737 RVA: 0x000080C4 File Offset: 0x000062C4
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					switch (transition)
					{
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPoint:
						validator.Jump(SpatialValidatorImplementation.NestedValidator.PointStart);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginLineString:
						validator.Jump(SpatialValidatorImplementation.NestedValidator.LineStringStart);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPolygon:
						validator.Jump(SpatialValidatorImplementation.NestedValidator.PolygonStart);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiPoint:
						validator.Jump(SpatialValidatorImplementation.NestedValidator.MultiPoint);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiLineString:
						validator.Jump(SpatialValidatorImplementation.NestedValidator.MultiLineString);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiPolygon:
						validator.Jump(SpatialValidatorImplementation.NestedValidator.MultiPolygon);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginCollection:
						validator.Jump(SpatialValidatorImplementation.NestedValidator.Collection);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFullGlobe:
						if (validator.depth != 1)
						{
							throw new FormatException(Strings.Validator_FullGlobeInCollection);
						}
						validator.Jump(SpatialValidatorImplementation.NestedValidator.FullGlobe);
						return;
					default:
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.Begin, transition);
						return;
					}
				}
			}

			// Token: 0x02000070 RID: 112
			private class PointStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002E3 RID: 739 RVA: 0x00008180 File Offset: 0x00006380
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure)
					{
						validator.Jump(SpatialValidatorImplementation.NestedValidator.PointBuilding);
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
						return;
					}
					validator.Return();
				}
			}

			// Token: 0x02000071 RID: 113
			private class PointBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002E5 RID: 741 RVA: 0x000081C0 File Offset: 0x000063C0
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					switch (transition)
					{
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo:
						if (validator.pointCount != 0)
						{
							SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						}
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure:
						if (validator.pointCount == 0)
						{
							SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure, transition);
						}
						validator.Jump(SpatialValidatorImplementation.NestedValidator.PointEnd);
						return;
					default:
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						return;
					}
				}
			}

			// Token: 0x02000072 RID: 114
			private class PointEndState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002E7 RID: 743 RVA: 0x00008220 File Offset: 0x00006420
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						validator.Return();
						return;
					}
					SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
				}
			}

			// Token: 0x02000073 RID: 115
			private class LineStringStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002E9 RID: 745 RVA: 0x0000824C File Offset: 0x0000644C
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure)
					{
						validator.Jump(SpatialValidatorImplementation.NestedValidator.LineStringBuilding);
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
						return;
					}
					validator.Return();
				}
			}

			// Token: 0x02000074 RID: 116
			private class LineStringBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002EB RID: 747 RVA: 0x0000828C File Offset: 0x0000648C
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					switch (transition)
					{
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo:
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure:
						if (validator.pointCount < 2)
						{
							throw new FormatException(Strings.Validator_LineStringNeedsTwoPoints);
						}
						validator.Jump(SpatialValidatorImplementation.NestedValidator.LineStringEnd);
						return;
					default:
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo, SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						return;
					}
				}
			}

			// Token: 0x02000075 RID: 117
			private class LineStringEndState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002ED RID: 749 RVA: 0x000082E4 File Offset: 0x000064E4
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						validator.Return();
						return;
					}
					SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
				}
			}

			// Token: 0x02000076 RID: 118
			private class PolygonStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002EF RID: 751 RVA: 0x00008310 File Offset: 0x00006510
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure)
					{
						validator.Jump(SpatialValidatorImplementation.NestedValidator.PolygonBuilding);
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
						return;
					}
					validator.ringCount = 0;
					validator.Return();
				}
			}

			// Token: 0x02000077 RID: 119
			private class PolygonBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002F1 RID: 753 RVA: 0x00008358 File Offset: 0x00006558
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					switch (transition)
					{
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo:
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure:
						validator.ringCount++;
						if (validator.processingGeography)
						{
							SpatialValidatorImplementation.NestedValidator.ValidateGeographyPolygon(validator.pointCount, validator.initialFirstCoordinate, validator.initialSecondCoordinate, validator.mostRecentFirstCoordinate, validator.mostRecentSecondCoordinate);
						}
						else
						{
							SpatialValidatorImplementation.NestedValidator.ValidateGeometryPolygon(validator.pointCount, validator.initialFirstCoordinate, validator.initialSecondCoordinate, validator.mostRecentFirstCoordinate, validator.mostRecentSecondCoordinate);
						}
						validator.Jump(SpatialValidatorImplementation.NestedValidator.PolygonStart);
						return;
					default:
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo, SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						return;
					}
				}
			}

			// Token: 0x02000078 RID: 120
			private class MultiPointState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002F3 RID: 755 RVA: 0x000083F8 File Offset: 0x000065F8
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					switch (transition)
					{
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem:
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.Begin:
						break;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPoint:
						validator.Call(SpatialValidatorImplementation.NestedValidator.PointStart);
						return;
					default:
						if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
						{
							validator.Return();
							return;
						}
						break;
					}
					SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem, SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPoint, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
				}
			}

			// Token: 0x02000079 RID: 121
			private class MultiLineStringState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002F5 RID: 757 RVA: 0x00008448 File Offset: 0x00006648
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem)
					{
						return;
					}
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginLineString)
					{
						validator.Call(SpatialValidatorImplementation.NestedValidator.LineStringStart);
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem, SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginLineString, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
						return;
					}
					validator.Return();
				}
			}

			// Token: 0x0200007A RID: 122
			private class MultiPolygonState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002F7 RID: 759 RVA: 0x0000848C File Offset: 0x0000668C
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem)
					{
						return;
					}
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPolygon)
					{
						validator.Call(SpatialValidatorImplementation.NestedValidator.PolygonStart);
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem, SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPolygon, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
						return;
					}
					validator.Return();
				}
			}

			// Token: 0x0200007B RID: 123
			private class CollectionState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002F9 RID: 761 RVA: 0x000084D0 File Offset: 0x000066D0
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					switch (transition)
					{
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem:
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPoint:
						validator.Call(SpatialValidatorImplementation.NestedValidator.PointStart);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginLineString:
						validator.Call(SpatialValidatorImplementation.NestedValidator.LineStringStart);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPolygon:
						validator.Call(SpatialValidatorImplementation.NestedValidator.PolygonStart);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiPoint:
						validator.Call(SpatialValidatorImplementation.NestedValidator.MultiPoint);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiLineString:
						validator.Call(SpatialValidatorImplementation.NestedValidator.MultiLineString);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginMultiPolygon:
						validator.Call(SpatialValidatorImplementation.NestedValidator.MultiPolygon);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginCollection:
						validator.Call(SpatialValidatorImplementation.NestedValidator.Collection);
						return;
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFullGlobe:
						throw new FormatException(Strings.Validator_FullGlobeInCollection);
					case SpatialValidatorImplementation.NestedValidator.PipelineCall.End:
						validator.Return();
						return;
					}
					SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem, SpatialValidatorImplementation.NestedValidator.PipelineCall.Begin, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
				}
			}

			// Token: 0x0200007C RID: 124
			private class FullGlobeState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060002FB RID: 763 RVA: 0x00008598 File Offset: 0x00006798
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						validator.Return();
						return;
					}
					throw new FormatException(Strings.Validator_FullGlobeCannotHaveElements);
				}
			}
		}
	}
}
