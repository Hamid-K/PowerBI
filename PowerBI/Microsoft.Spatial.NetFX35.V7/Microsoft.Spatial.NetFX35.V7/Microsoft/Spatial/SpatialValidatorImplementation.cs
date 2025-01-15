using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Spatial
{
	// Token: 0x02000062 RID: 98
	internal class SpatialValidatorImplementation : SpatialPipeline
	{
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00005E3F File Offset: 0x0000403F
		public override GeographyPipeline GeographyPipeline
		{
			get
			{
				return this.geographyValidatorInstance.GeographyPipeline;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00005E4C File Offset: 0x0000404C
		public override GeometryPipeline GeometryPipeline
		{
			get
			{
				return this.geometryValidatorInstance.GeometryPipeline;
			}
		}

		// Token: 0x040000A0 RID: 160
		internal const double MaxLongitude = 15069.0;

		// Token: 0x040000A1 RID: 161
		internal const double MaxLatitude = 90.0;

		// Token: 0x040000A2 RID: 162
		private readonly SpatialValidatorImplementation.NestedValidator geographyValidatorInstance = new SpatialValidatorImplementation.NestedValidator();

		// Token: 0x040000A3 RID: 163
		private readonly SpatialValidatorImplementation.NestedValidator geometryValidatorInstance = new SpatialValidatorImplementation.NestedValidator();

		// Token: 0x02000085 RID: 133
		private class NestedValidator : DrawBoth
		{
			// Token: 0x0600033D RID: 829 RVA: 0x000084AA File Offset: 0x000066AA
			public NestedValidator()
			{
				this.InitializeObject();
			}

			// Token: 0x0600033E RID: 830 RVA: 0x000084C8 File Offset: 0x000066C8
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

			// Token: 0x0600033F RID: 831 RVA: 0x0000850E File Offset: 0x0000670E
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

			// Token: 0x06000340 RID: 832 RVA: 0x0000853B File Offset: 0x0000673B
			protected override void OnEndGeography()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.End);
				if (!this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeometry);
				}
				this.depth--;
			}

			// Token: 0x06000341 RID: 833 RVA: 0x00008566 File Offset: 0x00006766
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

			// Token: 0x06000342 RID: 834 RVA: 0x00008593 File Offset: 0x00006793
			protected override void OnEndGeometry()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.End);
				if (this.processingGeography)
				{
					throw new FormatException(Strings.Validator_UnexpectedGeography);
				}
				this.depth--;
			}

			// Token: 0x06000343 RID: 835 RVA: 0x000085BE File Offset: 0x000067BE
			protected override GeographyPosition OnBeginFigure(GeographyPosition position)
			{
				this.BeginFigure(new Action<double, double, double?, double?>(SpatialValidatorImplementation.NestedValidator.ValidateGeographyPosition), position.Latitude, position.Longitude, position.Z, position.M);
				return position;
			}

			// Token: 0x06000344 RID: 836 RVA: 0x000085EB File Offset: 0x000067EB
			protected override GeometryPosition OnBeginFigure(GeometryPosition position)
			{
				this.BeginFigure(new Action<double, double, double?, double?>(SpatialValidatorImplementation.NestedValidator.ValidateGeometryPosition), position.X, position.Y, position.Z, position.M);
				return position;
			}

			// Token: 0x06000345 RID: 837 RVA: 0x00008618 File Offset: 0x00006818
			protected override void OnEndFigure()
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.EndFigure);
			}

			// Token: 0x06000346 RID: 838 RVA: 0x00008622 File Offset: 0x00006822
			protected override void OnReset()
			{
				this.InitializeObject();
			}

			// Token: 0x06000347 RID: 839 RVA: 0x0000862C File Offset: 0x0000682C
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

			// Token: 0x06000348 RID: 840 RVA: 0x00008684 File Offset: 0x00006884
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

			// Token: 0x06000349 RID: 841 RVA: 0x000086DC File Offset: 0x000068DC
			private static bool IsFinite(double value)
			{
				return !double.IsNaN(value) && !double.IsInfinity(value);
			}

			// Token: 0x0600034A RID: 842 RVA: 0x000086F4 File Offset: 0x000068F4
			[SuppressMessage("Microsoft.Naming", "CA1704", Justification = "z and m are meaningful")]
			private static bool IsPointValid(double first, double second, double? z, double? m)
			{
				return SpatialValidatorImplementation.NestedValidator.IsFinite(first) && SpatialValidatorImplementation.NestedValidator.IsFinite(second) && (z == null || SpatialValidatorImplementation.NestedValidator.IsFinite(z.Value)) && (m == null || SpatialValidatorImplementation.NestedValidator.IsFinite(m.Value));
			}

			// Token: 0x0600034B RID: 843 RVA: 0x00008741 File Offset: 0x00006941
			private static void ValidateOnePosition(double first, double second, double? z, double? m)
			{
				if (!SpatialValidatorImplementation.NestedValidator.IsPointValid(first, second, z, m))
				{
					throw new FormatException(Strings.Validator_InvalidPointCoordinate(first, second, z, m));
				}
			}

			// Token: 0x0600034C RID: 844 RVA: 0x00008771 File Offset: 0x00006971
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

			// Token: 0x0600034D RID: 845 RVA: 0x000087AE File Offset: 0x000069AE
			private static void ValidateGeometryPosition(double x, double y, double? z, double? m)
			{
				SpatialValidatorImplementation.NestedValidator.ValidateOnePosition(x, y, z, m);
			}

			// Token: 0x0600034E RID: 846 RVA: 0x000087B9 File Offset: 0x000069B9
			private static bool IsLatitudeValid(double latitude)
			{
				return latitude >= -90.0 && latitude <= 90.0;
			}

			// Token: 0x0600034F RID: 847 RVA: 0x000087D8 File Offset: 0x000069D8
			private static bool IsLongitudeValid(double longitude)
			{
				return longitude >= -15069.0 && longitude <= 15069.0;
			}

			// Token: 0x06000350 RID: 848 RVA: 0x000087F7 File Offset: 0x000069F7
			private static void ValidateGeographyPolygon(int numOfPoints, double initialFirstCoordinate, double initialSecondCoordinate, double mostRecentFirstCoordinate, double mostRecentSecondCoordinate)
			{
				if (numOfPoints < 4 || initialFirstCoordinate != mostRecentFirstCoordinate || !SpatialValidatorImplementation.NestedValidator.AreLongitudesEqual(initialSecondCoordinate, mostRecentSecondCoordinate))
				{
					throw new FormatException(Strings.Validator_InvalidPolygonPoints);
				}
			}

			// Token: 0x06000351 RID: 849 RVA: 0x00008816 File Offset: 0x00006A16
			private static void ValidateGeometryPolygon(int numOfPoints, double initialFirstCoordinate, double initialSecondCoordinate, double mostRecentFirstCoordinate, double mostRecentSecondCoordinate)
			{
				if (numOfPoints < 4 || initialFirstCoordinate != mostRecentFirstCoordinate || initialSecondCoordinate != mostRecentSecondCoordinate)
				{
					throw new FormatException(Strings.Validator_InvalidPolygonPoints);
				}
			}

			// Token: 0x06000352 RID: 850 RVA: 0x00008830 File Offset: 0x00006A30
			private static bool AreLongitudesEqual(double left, double right)
			{
				return left == right || (left - right) % 360.0 == 0.0;
			}

			// Token: 0x06000353 RID: 851 RVA: 0x00008850 File Offset: 0x00006A50
			private void BeginFigure(Action<double, double, double?, double?> validate, double x, double y, double? z, double? m)
			{
				validate.Invoke(x, y, z, m);
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.BeginFigure);
				this.pointCount = 0;
				this.TrackPosition(x, y);
			}

			// Token: 0x06000354 RID: 852 RVA: 0x00008878 File Offset: 0x00006A78
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

			// Token: 0x06000355 RID: 853 RVA: 0x00008933 File Offset: 0x00006B33
			private void AddControlPoint(double first, double second)
			{
				this.Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall.LineTo);
				this.TrackPosition(first, second);
			}

			// Token: 0x06000356 RID: 854 RVA: 0x00008945 File Offset: 0x00006B45
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

			// Token: 0x06000357 RID: 855 RVA: 0x0000897C File Offset: 0x00006B7C
			private void Execute(SpatialValidatorImplementation.NestedValidator.PipelineCall transition)
			{
				SpatialValidatorImplementation.NestedValidator.ValidatorState validatorState = this.stack.Peek();
				validatorState.ValidateTransition(transition, this);
			}

			// Token: 0x06000358 RID: 856 RVA: 0x000089A0 File Offset: 0x00006BA0
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

			// Token: 0x06000359 RID: 857 RVA: 0x00008A20 File Offset: 0x00006C20
			private void Call(SpatialValidatorImplementation.NestedValidator.ValidatorState state)
			{
				if (this.stack.Count > 28)
				{
					throw new FormatException(Strings.Validator_NestingOverflow(28));
				}
				this.stack.Push(state);
			}

			// Token: 0x0600035A RID: 858 RVA: 0x00008A4F File Offset: 0x00006C4F
			private void Return()
			{
				this.stack.Pop();
			}

			// Token: 0x0600035B RID: 859 RVA: 0x00008A5D File Offset: 0x00006C5D
			private void Jump(SpatialValidatorImplementation.NestedValidator.ValidatorState state)
			{
				this.stack.Pop();
				this.stack.Push(state);
			}

			// Token: 0x04000126 RID: 294
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState CoordinateSystem = new SpatialValidatorImplementation.NestedValidator.SetCoordinateSystemState();

			// Token: 0x04000127 RID: 295
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState BeginSpatial = new SpatialValidatorImplementation.NestedValidator.BeginGeoState();

			// Token: 0x04000128 RID: 296
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointStart = new SpatialValidatorImplementation.NestedValidator.PointStartState();

			// Token: 0x04000129 RID: 297
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointBuilding = new SpatialValidatorImplementation.NestedValidator.PointBuildingState();

			// Token: 0x0400012A RID: 298
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PointEnd = new SpatialValidatorImplementation.NestedValidator.PointEndState();

			// Token: 0x0400012B RID: 299
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringStart = new SpatialValidatorImplementation.NestedValidator.LineStringStartState();

			// Token: 0x0400012C RID: 300
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringBuilding = new SpatialValidatorImplementation.NestedValidator.LineStringBuildingState();

			// Token: 0x0400012D RID: 301
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState LineStringEnd = new SpatialValidatorImplementation.NestedValidator.LineStringEndState();

			// Token: 0x0400012E RID: 302
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PolygonStart = new SpatialValidatorImplementation.NestedValidator.PolygonStartState();

			// Token: 0x0400012F RID: 303
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState PolygonBuilding = new SpatialValidatorImplementation.NestedValidator.PolygonBuildingState();

			// Token: 0x04000130 RID: 304
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiPoint = new SpatialValidatorImplementation.NestedValidator.MultiPointState();

			// Token: 0x04000131 RID: 305
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiLineString = new SpatialValidatorImplementation.NestedValidator.MultiLineStringState();

			// Token: 0x04000132 RID: 306
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState MultiPolygon = new SpatialValidatorImplementation.NestedValidator.MultiPolygonState();

			// Token: 0x04000133 RID: 307
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState Collection = new SpatialValidatorImplementation.NestedValidator.CollectionState();

			// Token: 0x04000134 RID: 308
			private static readonly SpatialValidatorImplementation.NestedValidator.ValidatorState FullGlobe = new SpatialValidatorImplementation.NestedValidator.FullGlobeState();

			// Token: 0x04000135 RID: 309
			private const int MaxGeometryCollectionDepth = 28;

			// Token: 0x04000136 RID: 310
			private readonly Stack<SpatialValidatorImplementation.NestedValidator.ValidatorState> stack = new Stack<SpatialValidatorImplementation.NestedValidator.ValidatorState>(16);

			// Token: 0x04000137 RID: 311
			private CoordinateSystem validationCoordinateSystem;

			// Token: 0x04000138 RID: 312
			private int ringCount;

			// Token: 0x04000139 RID: 313
			private double initialFirstCoordinate;

			// Token: 0x0400013A RID: 314
			private double initialSecondCoordinate;

			// Token: 0x0400013B RID: 315
			private double mostRecentFirstCoordinate;

			// Token: 0x0400013C RID: 316
			private double mostRecentSecondCoordinate;

			// Token: 0x0400013D RID: 317
			private bool processingGeography;

			// Token: 0x0400013E RID: 318
			private int pointCount;

			// Token: 0x0400013F RID: 319
			private int depth;

			// Token: 0x0200008E RID: 142
			private enum PipelineCall
			{
				// Token: 0x0400014A RID: 330
				SetCoordinateSystem,
				// Token: 0x0400014B RID: 331
				Begin,
				// Token: 0x0400014C RID: 332
				BeginPoint,
				// Token: 0x0400014D RID: 333
				BeginLineString,
				// Token: 0x0400014E RID: 334
				BeginPolygon,
				// Token: 0x0400014F RID: 335
				BeginMultiPoint,
				// Token: 0x04000150 RID: 336
				BeginMultiLineString,
				// Token: 0x04000151 RID: 337
				BeginMultiPolygon,
				// Token: 0x04000152 RID: 338
				BeginCollection,
				// Token: 0x04000153 RID: 339
				BeginFullGlobe,
				// Token: 0x04000154 RID: 340
				BeginFigure,
				// Token: 0x04000155 RID: 341
				LineTo,
				// Token: 0x04000156 RID: 342
				EndFigure,
				// Token: 0x04000157 RID: 343
				End
			}

			// Token: 0x0200008F RID: 143
			private abstract class ValidatorState
			{
				// Token: 0x060003AC RID: 940
				internal abstract void ValidateTransition(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator validator);

				// Token: 0x060003AD RID: 941 RVA: 0x00009673 File Offset: 0x00007873
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall(transition, actual));
				}

				// Token: 0x060003AE RID: 942 RVA: 0x0000968B File Offset: 0x0000788B
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition1, SpatialValidatorImplementation.NestedValidator.PipelineCall transition2, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall2(transition1, transition2, actual));
				}

				// Token: 0x060003AF RID: 943 RVA: 0x000096A9 File Offset: 0x000078A9
				protected static void ThrowExpected(SpatialValidatorImplementation.NestedValidator.PipelineCall transition1, SpatialValidatorImplementation.NestedValidator.PipelineCall transition2, SpatialValidatorImplementation.NestedValidator.PipelineCall transition3, SpatialValidatorImplementation.NestedValidator.PipelineCall actual)
				{
					throw new FormatException(Strings.Validator_UnexpectedCall2(transition1 + ", " + transition2, transition3, actual));
				}
			}

			// Token: 0x02000090 RID: 144
			private class SetCoordinateSystemState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003B1 RID: 945 RVA: 0x000096D7 File Offset: 0x000078D7
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

			// Token: 0x02000091 RID: 145
			private class BeginGeoState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003B3 RID: 947 RVA: 0x000096F8 File Offset: 0x000078F8
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

			// Token: 0x02000092 RID: 146
			private class PointStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003B5 RID: 949 RVA: 0x000097AA File Offset: 0x000079AA
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

			// Token: 0x02000093 RID: 147
			private class PointBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003B7 RID: 951 RVA: 0x000097D8 File Offset: 0x000079D8
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

			// Token: 0x02000094 RID: 148
			private class PointEndState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003B9 RID: 953 RVA: 0x00009826 File Offset: 0x00007A26
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

			// Token: 0x02000095 RID: 149
			private class LineStringStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003BB RID: 955 RVA: 0x0000983C File Offset: 0x00007A3C
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

			// Token: 0x02000096 RID: 150
			private class LineStringBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003BD RID: 957 RVA: 0x00009867 File Offset: 0x00007A67
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

			// Token: 0x02000097 RID: 151
			private class LineStringEndState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003BF RID: 959 RVA: 0x00009826 File Offset: 0x00007A26
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

			// Token: 0x02000098 RID: 152
			private class PolygonStartState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003C1 RID: 961 RVA: 0x000098A0 File Offset: 0x00007AA0
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

			// Token: 0x02000099 RID: 153
			private class PolygonBuildingState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003C3 RID: 963 RVA: 0x000098D4 File Offset: 0x00007AD4
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

			// Token: 0x0200009A RID: 154
			private class MultiPointState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003C5 RID: 965 RVA: 0x00009962 File Offset: 0x00007B62
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

			// Token: 0x0200009B RID: 155
			private class MultiLineStringState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003C7 RID: 967 RVA: 0x00009990 File Offset: 0x00007B90
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

			// Token: 0x0200009C RID: 156
			private class MultiPolygonState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003C9 RID: 969 RVA: 0x000099BE File Offset: 0x00007BBE
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

			// Token: 0x0200009D RID: 157
			private class CollectionState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003CB RID: 971 RVA: 0x000099EC File Offset: 0x00007BEC
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

			// Token: 0x0200009E RID: 158
			private class FullGlobeState : SpatialValidatorImplementation.NestedValidator.ValidatorState
			{
				// Token: 0x060003CD RID: 973 RVA: 0x00009AAA File Offset: 0x00007CAA
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
