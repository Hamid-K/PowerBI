using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Spatial
{
	// Token: 0x020003E8 RID: 1000
	internal sealed class SpatialModule : Module
	{
		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x06002284 RID: 8836 RVA: 0x0005FCA0 File Offset: 0x0005DEA0
		public override string Name
		{
			get
			{
				return "Spatial";
			}
		}

		// Token: 0x17000E85 RID: 3717
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x0005FCA7 File Offset: 0x0005DEA7
		public override Keys ExportKeys
		{
			get
			{
				if (this.exportKeys == null)
				{
					this.exportKeys = Keys.New(6, delegate(int index)
					{
						switch (index)
						{
						case 0:
							return "Geography.FromWellKnownText";
						case 1:
							return "Geography.ToWellKnownText";
						case 2:
							return "GeographyPoint.From";
						case 3:
							return "Geometry.FromWellKnownText";
						case 4:
							return "Geometry.ToWellKnownText";
						case 5:
							return "GeometryPoint.From";
						default:
							throw new InvalidOperationException();
						}
					});
				}
				return this.exportKeys;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x0005FCE2 File Offset: 0x0005DEE2
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				switch (index)
				{
				case 0:
					return SpatialModule.GeographyFromWellKnownText;
				case 1:
					return SpatialModule.GeographyToWellKnownText;
				case 2:
					return SpatialModule.GeographyPointFrom;
				case 3:
					return SpatialModule.GeometryFromWellKnownText;
				case 4:
					return SpatialModule.GeometryToWellKnownText;
				case 5:
					return SpatialModule.GeometryPointFrom;
				default:
					throw new InvalidOperationException();
				}
			});
		}

		// Token: 0x04000D77 RID: 3447
		public static readonly FunctionValue GeographyPointFrom = new SpatialModule.GeographyPointFromFunctionValue();

		// Token: 0x04000D78 RID: 3448
		private static readonly FunctionValue GeographyFromWellKnownText = new SpatialModule.GeographyFromWellKnownTextFunctionValue();

		// Token: 0x04000D79 RID: 3449
		private static readonly FunctionValue GeographyToWellKnownText = new SpatialModule.GeographyToWellKnownTextFunctionValue();

		// Token: 0x04000D7A RID: 3450
		public static readonly FunctionValue GeometryPointFrom = new SpatialModule.GeometryPointFromFunctionValue();

		// Token: 0x04000D7B RID: 3451
		private static readonly FunctionValue GeometryFromWellKnownText = new SpatialModule.GeometryFromWellKnownTextFunctionValue();

		// Token: 0x04000D7C RID: 3452
		private static readonly FunctionValue GeometryToWellKnownText = new SpatialModule.GeometryToWellKnownTextFunctionValue();

		// Token: 0x04000D7D RID: 3453
		private Keys exportKeys;

		// Token: 0x020003E9 RID: 1001
		private enum Exports
		{
			// Token: 0x04000D7F RID: 3455
			Geography_FromWellKnownText,
			// Token: 0x04000D80 RID: 3456
			Geography_ToWellKnownText,
			// Token: 0x04000D81 RID: 3457
			GeographyPoint_From,
			// Token: 0x04000D82 RID: 3458
			Geometry_FromWellKnownText,
			// Token: 0x04000D83 RID: 3459
			Geometry_ToWellKnownText,
			// Token: 0x04000D84 RID: 3460
			GeometryPoint_From,
			// Token: 0x04000D85 RID: 3461
			Count
		}

		// Token: 0x020003EA RID: 1002
		private sealed class GeographyPointFromFunctionValue : NativeFunctionValue5<RecordValue, NumberValue, NumberValue, Value, Value, Value>
		{
			// Token: 0x06002289 RID: 8841 RVA: 0x0005FD4C File Offset: 0x0005DF4C
			public GeographyPointFromFunctionValue()
				: base(SpatialTypeValue.GeographyPoint, 2, "longitude", TypeValue.Double, "latitude", TypeValue.Double, "z", NullableTypeValue.Number, "m", NullableTypeValue.Number, "srid", NullableTypeValue.Int32)
			{
			}

			// Token: 0x0600228A RID: 8842 RVA: 0x0005FD98 File Offset: 0x0005DF98
			public override RecordValue TypedInvoke(NumberValue longitude, NumberValue latitude, Value z, Value m, Value srid)
			{
				return SpatialUtilities.CreateGeographyPoint(longitude.AsDouble, latitude.AsDouble, z.IsNull ? null : new double?(z.AsNumber.AsDouble), m.IsNull ? null : new double?(m.AsNumber.AsDouble), srid.IsNull ? null : new int?(srid.AsNumber.AsInteger32));
			}
		}

		// Token: 0x020003EB RID: 1003
		private sealed class GeographyFromWellKnownTextFunctionValue : NativeFunctionValue1<Value, Value>
		{
			// Token: 0x0600228B RID: 8843 RVA: 0x0005FE23 File Offset: 0x0005E023
			public GeographyFromWellKnownTextFunctionValue()
				: base(SpatialTypeValue.Geography.Nullable, "input", NullableTypeValue.Text)
			{
			}

			// Token: 0x0600228C RID: 8844 RVA: 0x0005FE3F File Offset: 0x0005E03F
			public override Value TypedInvoke(Value input)
			{
				if (input.IsNull)
				{
					return Value.Null;
				}
				return SpatialUtilities.ToGeographyFromWellKnownText(input.AsText.AsString);
			}
		}

		// Token: 0x020003EC RID: 1004
		private sealed class GeographyToWellKnownTextFunctionValue : NativeFunctionValue2<Value, Value, Value>
		{
			// Token: 0x0600228D RID: 8845 RVA: 0x0005FE5F File Offset: 0x0005E05F
			public GeographyToWellKnownTextFunctionValue()
				: base(SerializedTextTypeValue.SerializedGeographyType.Nullable, 1, "input", SpatialTypeValue.Geography.Nullable, "omitSRID", NullableTypeValue.Logical)
			{
			}

			// Token: 0x0600228E RID: 8846 RVA: 0x0005FE8C File Offset: 0x0005E08C
			public override Value TypedInvoke(Value input, Value omitSrid)
			{
				if (input.IsNull)
				{
					return Value.Null;
				}
				bool flag = !omitSrid.IsNull && omitSrid.AsLogical.AsBoolean;
				return TextValue.New(SpatialUtilities.ToWellKnownText(input.AsRecord, true, flag));
			}
		}

		// Token: 0x020003ED RID: 1005
		private sealed class GeometryPointFromFunctionValue : NativeFunctionValue5<RecordValue, NumberValue, NumberValue, Value, Value, Value>
		{
			// Token: 0x0600228F RID: 8847 RVA: 0x0005FED0 File Offset: 0x0005E0D0
			public GeometryPointFromFunctionValue()
				: base(SpatialTypeValue.GeometryPoint, 2, "x", TypeValue.Double, "y", TypeValue.Double, "z", NullableTypeValue.Number, "m", NullableTypeValue.Number, "srid", NullableTypeValue.Int32)
			{
			}

			// Token: 0x06002290 RID: 8848 RVA: 0x0005FF1C File Offset: 0x0005E11C
			public override RecordValue TypedInvoke(NumberValue x, NumberValue y, Value z, Value m, Value srid)
			{
				return SpatialUtilities.CreateGeometryPointRecord(x.AsDouble, y.AsDouble, z.IsNull ? null : new double?(z.AsNumber.AsDouble), m.IsNull ? null : new double?(m.AsNumber.AsDouble), srid.IsNull ? null : new int?(srid.AsNumber.AsInteger32));
			}
		}

		// Token: 0x020003EE RID: 1006
		private sealed class GeometryFromWellKnownTextFunctionValue : NativeFunctionValue1<Value, Value>
		{
			// Token: 0x06002291 RID: 8849 RVA: 0x0005FFA7 File Offset: 0x0005E1A7
			public GeometryFromWellKnownTextFunctionValue()
				: base(SpatialTypeValue.Geometry.Nullable, "input", NullableTypeValue.Text)
			{
			}

			// Token: 0x06002292 RID: 8850 RVA: 0x0005FFC3 File Offset: 0x0005E1C3
			public override Value TypedInvoke(Value input)
			{
				if (input.IsNull)
				{
					return Value.Null;
				}
				return SpatialUtilities.ToGeometryFromWellKnownText(input.AsText.AsString);
			}
		}

		// Token: 0x020003EF RID: 1007
		private sealed class GeometryToWellKnownTextFunctionValue : NativeFunctionValue2<Value, Value, Value>
		{
			// Token: 0x06002293 RID: 8851 RVA: 0x0005FFE3 File Offset: 0x0005E1E3
			public GeometryToWellKnownTextFunctionValue()
				: base(SerializedTextTypeValue.SerializedGeometryType.Nullable, 1, "input", SpatialTypeValue.Geometry.Nullable, "omitSRID", NullableTypeValue.Logical)
			{
			}

			// Token: 0x06002294 RID: 8852 RVA: 0x00060010 File Offset: 0x0005E210
			public override Value TypedInvoke(Value input, Value omitSrid)
			{
				if (input.IsNull)
				{
					return Value.Null;
				}
				bool flag = !omitSrid.IsNull && omitSrid.AsLogical.AsBoolean;
				return TextValue.New(SpatialUtilities.ToWellKnownText(input.AsRecord, false, flag));
			}
		}
	}
}
