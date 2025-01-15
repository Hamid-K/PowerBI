using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000067 RID: 103
	internal class SpatialValidatorImplementation : SpatialPipeline
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002CB RID: 715 RVA: 0x00006B07 File Offset: 0x00004D07
		public override GeographyPipeline GeographyPipeline
		{
			get
			{
				return this.geographyValidatorInstance.GeographyPipeline;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00006B14 File Offset: 0x00004D14
		public override GeometryPipeline GeometryPipeline
		{
			get
			{
				return this.geometryValidatorInstance.GeometryPipeline;
			}
		}

		// Token: 0x040000AD RID: 173
		internal const double MaxLongitude = 15069.0;

		// Token: 0x040000AE RID: 174
		internal const double MaxLatitude = 90.0;

		// Token: 0x040000AF RID: 175
		private readonly SpatialValidatorImplementation.NestedValidator geographyValidatorInstance = new SpatialValidatorImplementation.NestedValidator();

		// Token: 0x040000B0 RID: 176
		private readonly SpatialValidatorImplementation.NestedValidator geometryValidatorInstance = new SpatialValidatorImplementation.NestedValidator();

		// Token: 0x02000091 RID: 145
		private class NestedValidator : DrawBoth
		{
			// Token: 0x060003C5 RID: 965 RVA: 0x0000920E File Offset: 0x0000740E
			public NestedValidator()
			{
				this.InitializeObject();
			}

			// Token: 0x060003C6 RID: 966 RVA: 0x0000922C File Offset: 0x0000742C
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

			// Token: 0x060003C7 RID: 967 RVA: 0x00009272 File Offset: 0x00007472
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

			// Token: 0x060003C8 RID: 968 RVA: 0x0000929F File Offset: 0x0000749F
			protected override void OnEndGeography()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.End);
				if (!this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeometry);
				}
				this.depth--;
			}

			// Token: 0x060003C9 RID: 969 RVA: 0x000092CA File Offset: 0x000074CA
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

			// Token: 0x060003CA RID: 970 RVA: 0x000092F7 File Offset: 0x000074F7
			protected override void OnEndGeometry()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.End);
				if (this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeography);
				}
				this.depth--;
			}

			// Token: 0x060003CB RID: 971 RVA: 0x00009322 File Offset: 0x00007522
			protected override GeographyPosition OnBeginFigure(GeographyPosition position)
			{
				this.BeginFigure(new Action<double, double, double?, double?>(SpatialValidatorImplementation.NestedValidator.ValidateGeographyPosition), position.Latitude, position.Longitude, position.Z, position.M);
				return position;
			}

			// Token: 0x060003CC RID: 972 RVA: 0x0000934F File Offset: 0x0000754F
			protected override GeometryPosition OnBeginFigure(GeometryPosition position)
			{
				this.BeginFigure(new Action<double, double, double?, double?>(SpatialValidatorImplementation.NestedValidator.ValidateGeometryPosition), position.X, position.Y, position.Z, position.M);
				return position;
			}

			// Token: 0x060003CD RID: 973 RVA: 0x0000937C File Offset: 0x0000757C
			protected override void OnEndFigure()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure);
			}

			// Token: 0x060003CE RID: 974 RVA: 0x00009386 File Offset: 0x00007586
			protected override void OnReset()
			{
				this.InitializeObject();
			}

			// Token: 0x060003CF RID: 975 RVA: 0x00009390 File Offset: 0x00007590
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

			// Token: 0x060003D0 RID: 976 RVA: 0x000093E8 File Offset: 0x000075E8
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

			// Token: 0x060003D1 RID: 977 RVA: 0x00009440 File Offset: 0x00007640
			private static bool IsFinite(double value)
			{
				return !double.IsNaN(value) && !double.IsInfinity(value);
			}

			// Token: 0x060003D2 RID: 978 RVA: 0x00009458 File Offset: 0x00007658
			[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
			private static bool IsPointValid(double first, double second, double? z, double? m)
			{
				return SpatialValidatorImplementation.NestedValidator.IsFinite(first) && SpatialValidatorImplementation.NestedValidator.IsFinite(second) && (z == null || SpatialValidatorImplementation.NestedValidator.IsFinite(z.Value)) && (m == null || SpatialValidatorImplementation.NestedValidator.IsFinite(m.Value));
			}

			// Token: 0x060003D3 RID: 979 RVA: 0x000094A5 File Offset: 0x000076A5
			private static void ValidateOnePosition(double first, double second, double? z, double? m)
			{
				if (!SpatialValidatorImplementation.NestedValidator.IsPointValid(first, second, z, m))
				{
					throw new FormatException(Strings.Validator_InvalidPointCoordinate(first, second, z, m));
				}
			}

			// Token: 0x060003D4 RID: 980 RVA: 0x000094D5 File Offset: 0x000076D5
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

			// Token: 0x060003D5 RID: 981 RVA: 0x00009512 File Offset: 0x00007712
			private static void ValidateGeometryPosition(double x, double y, double? z, double? m)
			{
				SpatialValidatorImplementation.NestedValidator.ValidateOnePosition(x, y, z, m);
			}

			// Token: 0x060003D6 RID: 982 RVA: 0x0000951D File Offset: 0x0000771D
			private static bool IsLatitudeValid(double latitude)
			{
				return latitude >= -90.0 && latitude <= 90.0;
			}

			// Token: 0x060003D7 RID: 983 RVA: 0x0000953C File Offset: 0x0000773C
			private static bool IsLongitudeValid(double longitude)
			{
				return longitude >= -15069.0 && longitude <= 15069.0;
			}

			// Token: 0x060003D8 RID: 984 RVA: 0x0000955B File Offset: 0x0000775B
			private static void ValidateGeographyPolygon(int numOfPoints, double initialFirstCoordinate, double initialSecondCoordinate, double mostRecentFirstCoordinate, double mostRecentSecondCoordinate)
			{
				if (numOfPoints < 4 || initialFirstCoordinate != mostRecentFirstCoordinate || !SpatialValidatorImplementation.NestedValidator.AreLongitudesEqual(initialSecondCoordinate, mostRecentSecondCoordinate))
				{
					throw new FormatException(Strings.Validator_InvalidPolygonPoints);
				}
			}

			// Token: 0x060003D9 RID: 985 RVA: 0x0000957A File Offset: 0x0000777A
			private static void ValidateGeometryPolygon(int numOfPoints, double initialFirstCoordinate, double initialSecondCoordinate, double mostRecentFirstCoordinate, double mostRecentSecondCoordinate)
			{
				if (numOfPoints < 4 || initialFirstCoordinate != mostRecentFirstCoordinate || initialSecondCoordinate != mostRecentSecondCoordinate)
				{
					throw new FormatException(Strings.Validator_InvalidPolygonPoints);
				}
			}

			// Token: 0x060003DA RID: 986 RVA: 0x00009594 File Offset: 0x00007794
			private static bool AreLongitudesEqual(double left, double right)
			{
				return left == right || (left - right) % 360.0 == 0.0;
			}

			// Token: 0x060003DB RID: 987 RVA: 0x000095B4 File Offset: 0x000077B4
			private void BeginFigure(Action<double, double, double?, double?> validate, double x, double y, double? z, double? m)
			{
				validate(x, y, z, m);
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure);
				this.pointCount = 0;
				this.TrackPosition(x, y);
			}

			// Token: 0x060003DC RID: 988 RVA: 0x000095DC File Offset: 0x000077DC
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

			// Token: 0x060003DD RID: 989 RVA: 0x00009697 File Offset: 0x00007897
			private void AddControlPoint(double first, double second)
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo);
				this.TrackPosition(first, second);
			}

			// Token: 0x060003DE RID: 990 RVA: 0x000096A9 File Offset: 0x000078A9
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

			// Token: 0x060003DF RID: 991 RVA: 0x000096E0 File Offset: 0x000078E0
			private void Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall transition)
			{
				SpatialValidatorImplementation.NestedValidator.ValidatorState validatorState = this.stack.Peek();
				validatorState.ValidateTransition(transition, this);
			}

			// Token: 0x060003E0 RID: 992 RVA: 0x00009704 File Offset: 0x00007904
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

			// Token: 0x060003E1 RID: 993 RVA: 0x00009784 File Offset: 0x00007984
			private void Call(SpatialValidatorImplementation.NestedValidator.ValidatorState state)
			{
				if (this.stack.Count > 28)
				{
					throw new FormatException(Strings.Validator_NestingOverflow(28));
				}
				this.stack.Push(state);
			}

			// Token: 0x060003E2 RID: 994 RVA: 0x000097B3 File Offset: 0x000079B3
			private void Return()
			{
				this.stack.Pop();
			}

			// Token: 0x060003E3 RID: 995 RVA: 0x000097C1 File Offset: 0x000079C1
			private void Jump(SpatialValidatorImplementation.NestedValidator.ValidatorState state)
			{
				this.stack.Pop();
				this.stack.Push(state);
			}

			// Token: 0x04000142 RID: 322
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState CoordinateSystem = new SpatialValidatorImplementation.NestedValidator.SetCoordinateSystemState();

			// Token: 0x04000143 RID: 323
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState BeginSpatial = new SpatialValidatorImplementation.NestedValidator.BeginGeoState();

			// Token: 0x04000144 RID: 324
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointStart = new SpatialValidatorImplementation.NestedValidator.PointStartState();

			// Token: 0x04000145 RID: 325
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointBuilding = new SpatialValidatorImplementation.NestedValidator.PointBuildingState();

			// Token: 0x04000146 RID: 326
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointEnd = new SpatialValidatorImplementation.NestedValidator.PointEndState();

			// Token: 0x04000147 RID: 327
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringStart = new SpatialValidatorImplementation.NestedValidator.LineStringStartState();

			// Token: 0x04000148 RID: 328
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringBuilding = new SpatialValidatorImplementation.NestedValidator.LineStringBuildingState();

			// Token: 0x04000149 RID: 329
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringEnd = new SpatialValidatorImplementation.NestedValidator.LineStringEndState();

			// Token: 0x0400014A RID: 330
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PolygonStart = new SpatialValidatorImplementation.NestedValidator.PolygonStartState();

			// Token: 0x0400014B RID: 331
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PolygonBuilding = new SpatialValidatorImplementation.NestedValidator.PolygonBuildingState();

			// Token: 0x0400014C RID: 332
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiPoint = new SpatialValidatorImplementation.NestedValidator.MultiPointState();

			// Token: 0x0400014D RID: 333
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiLineString = new SpatialValidatorImplementation.NestedValidator.MultiLineStringState();

			// Token: 0x0400014E RID: 334
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiPolygon = new SpatialValidatorImplementation.NestedValidator.MultiPolygonState();

			// Token: 0x0400014F RID: 335
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState Collection = new SpatialValidatorImplementation.NestedValidator.CollectionState();

			// Token: 0x04000150 RID: 336
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState FullGlobe = new SpatialValidatorImplementation.NestedValidator.FullGlobeState();

			// Token: 0x04000151 RID: 337
			private const int MaxGeometryCollectionDepth = 28;

			// Token: 0x04000152 RID: 338
			private readonly Stack<SpatialValidatorImplementation.NestedValidator.ValidatorState> stack = new Stack<SpatialValidatorImplementation.NestedValidator.ValidatorState>(16);

			// Token: 0x04000153 RID: 339
			private CoordinateSystem validationCoordinateSystem;

			// Token: 0x04000154 RID: 340
			private int ringCount;

			// Token: 0x04000155 RID: 341
			private double initialFirstCoordinate;

			// Token: 0x04000156 RID: 342
			private double initialSecondCoordinate;

			// Token: 0x04000157 RID: 343
			private double mostRecentFirstCoordinate;

			// Token: 0x04000158 RID: 344
			private double mostRecentSecondCoordinate;

			// Token: 0x04000159 RID: 345
			private bool processingGeography;

			// Token: 0x0400015A RID: 346
			private int pointCount;

			// Token: 0x0400015B RID: 347
			private int depth;

			// Token: 0x0200009A RID: 154
			private enum PipelineCall
			{
				// Token: 0x04000166 RID: 358
				SetCoordinateSystem,
				// Token: 0x04000167 RID: 359
				Begin,
				// Token: 0x04000168 RID: 360
				BeginPoint,
				// Token: 0x04000169 RID: 361
				BeginLineString,
				// Token: 0x0400016A RID: 362
				BeginPolygon,
				// Token: 0x0400016B RID: 363
				BeginMultiPoint,
				// Token: 0x0400016C RID: 364
				BeginMultiLineString,
				// Token: 0x0400016D RID: 365
				BeginMultiPolygon,
				// Token: 0x0400016E RID: 366
				BeginCollection,
				// Token: 0x0400016F RID: 367
				BeginFullGlobe,
				// Token: 0x04000170 RID: 368
				BeginFigure,
				// Token: 0x04000171 RID: 369
				LineTo,
				// Token: 0x04000172 RID: 370
				EndFigure,
				// Token: 0x04000173 RID: 371
				End
			}

			// Token: 0x0200009B RID: 155
			private abstract class ValidatorState
			{
				// Token: 0x06000434 RID: 1076
				internal abstract void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator);

				// Token: 0x06000435 RID: 1077 RVA: 0x0000A3D7 File Offset: 0x000085D7
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall(transition, actual));
				}

				// Token: 0x06000436 RID: 1078 RVA: 0x0000A3EF File Offset: 0x000085EF
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition1, SpatialValidatorImplementation.NestedValidator.PipelineCall transition2, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall2(transition1, transition2, actual));
				}

				// Token: 0x06000437 RID: 1079 RVA: 0x0000A40D File Offset: 0x0000860D
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition1, SpatialValidatorImplementation.NestedValidator.PipelineCall transition2, SpatialValidatorImplementation.NestedValidator.PipelineCall transition3, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall2(transition1 + ", " + transition2, transition3, actual));
				}
			}

			// Token: 0x0200009C RID: 156
			private class SetCoordinateSystemState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000439 RID: 1081 RVA: 0x0000A43B File Offset: 0x0000863B
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

			// Token: 0x0200009D RID: 157
			private class BeginGeoState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x0600043B RID: 1083 RVA: 0x0000A45C File Offset: 0x0000865C
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

			// Token: 0x0200009E RID: 158
			private class PointStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x0600043D RID: 1085 RVA: 0x0000A50E File Offset: 0x0000870E
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

			// Token: 0x0200009F RID: 159
			private class PointBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x0600043F RID: 1087 RVA: 0x0000A53C File Offset: 0x0000873C
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo)
					{
						if (validator.pointCount != 0)
						{
							SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						}
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						return;
					}
					if (validator.pointCount == 0)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure, transition);
					}
					validator.Jump(SpatialValidatorImplementation.NestedValidator.PointEnd);
				}
			}

			// Token: 0x020000A0 RID: 160
			private class PointEndState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000441 RID: 1089 RVA: 0x0000A58A File Offset: 0x0000878A
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

			// Token: 0x020000A1 RID: 161
			private class LineStringStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000443 RID: 1091 RVA: 0x0000A5A0 File Offset: 0x000087A0
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

			// Token: 0x020000A2 RID: 162
			private class LineStringBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000445 RID: 1093 RVA: 0x0000A5CB File Offset: 0x000087CB
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo)
					{
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo, SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						return;
					}
					if (validator.pointCount < 2)
					{
						throw new FormatException(Strings.Validator_LineStringNeedsTwoPoints);
					}
					validator.Jump(SpatialValidatorImplementation.NestedValidator.LineStringEnd);
				}
			}

			// Token: 0x020000A3 RID: 163
			private class LineStringEndState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000447 RID: 1095 RVA: 0x0000A58A File Offset: 0x0000878A
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

			// Token: 0x020000A4 RID: 164
			private class PolygonStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000449 RID: 1097 RVA: 0x0000A604 File Offset: 0x00008804
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

			// Token: 0x020000A5 RID: 165
			private class PolygonBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x0600044B RID: 1099 RVA: 0x0000A638 File Offset: 0x00008838
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo)
					{
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo, SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure, transition);
						return;
					}
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
				}
			}

			// Token: 0x020000A6 RID: 166
			private class MultiPointState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x0600044D RID: 1101 RVA: 0x0000A6C6 File Offset: 0x000088C6
				internal override void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator)
				{
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem)
					{
						return;
					}
					if (transition == SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPoint)
					{
						validator.Call(SpatialValidatorImplementation.NestedValidator.PointStart);
						return;
					}
					if (transition != SpatialValidatorImplementation.NestedValidator.PipelineCall.End)
					{
						SpatialValidatorImplementation.NestedValidator.ValidatorState.ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall.SetCoordinateSystem, SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginPoint, SpatialValidatorImplementation.NestedValidator.PipelineCall.End, transition);
						return;
					}
					validator.Return();
				}
			}

			// Token: 0x020000A7 RID: 167
			private class MultiLineStringState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x0600044F RID: 1103 RVA: 0x0000A6F4 File Offset: 0x000088F4
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

			// Token: 0x020000A8 RID: 168
			private class MultiPolygonState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000451 RID: 1105 RVA: 0x0000A722 File Offset: 0x00008922
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

			// Token: 0x020000A9 RID: 169
			private class CollectionState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000453 RID: 1107 RVA: 0x0000A750 File Offset: 0x00008950
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

			// Token: 0x020000AA RID: 170
			private class FullGlobeState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x06000455 RID: 1109 RVA: 0x0000A80E File Offset: 0x00008A0E
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
