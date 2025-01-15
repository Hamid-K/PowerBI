using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Spatial
{
	// Token: 0x020003F1 RID: 1009
	internal static class SpatialTypeValue
	{
		// Token: 0x04000D89 RID: 3465
		public const string LatitudeField = "Latitude";

		// Token: 0x04000D8A RID: 3466
		public const string LongitudeField = "Longitude";

		// Token: 0x04000D8B RID: 3467
		public const string SRIDField = "SRID";

		// Token: 0x04000D8C RID: 3468
		public const string XField = "X";

		// Token: 0x04000D8D RID: 3469
		public const string YField = "Y";

		// Token: 0x04000D8E RID: 3470
		public const string ZField = "Z";

		// Token: 0x04000D8F RID: 3471
		public const string MField = "M";

		// Token: 0x04000D90 RID: 3472
		public const string PointsField = "Points";

		// Token: 0x04000D91 RID: 3473
		public const string RingsField = "Rings";

		// Token: 0x04000D92 RID: 3474
		public const string ComponentsField = "Components";

		// Token: 0x04000D93 RID: 3475
		public const string KindField = "Kind";

		// Token: 0x04000D94 RID: 3476
		public static readonly Keys GeographyPointKeys = Keys.New("Kind", "Longitude", "Latitude");

		// Token: 0x04000D95 RID: 3477
		public static readonly Keys GeographyPointZKeys = Keys.New("Kind", "Longitude", "Latitude", "Z");

		// Token: 0x04000D96 RID: 3478
		public static readonly Keys GeographyPointMKeys = Keys.New("Kind", "Longitude", "Latitude", "M");

		// Token: 0x04000D97 RID: 3479
		public static readonly Keys GeographyPointZMKeys = Keys.New(new string[] { "Kind", "Longitude", "Latitude", "Z", "M" });

		// Token: 0x04000D98 RID: 3480
		public static readonly Keys GeographyPointSridKeys = Keys.New("Kind", "Longitude", "Latitude", "SRID");

		// Token: 0x04000D99 RID: 3481
		public static readonly Keys GeographyPointZSridKeys = Keys.New(new string[] { "Kind", "Longitude", "Latitude", "Z", "SRID" });

		// Token: 0x04000D9A RID: 3482
		public static readonly Keys GeographyPointMSridKeys = Keys.New(new string[] { "Kind", "Longitude", "Latitude", "M", "SRID" });

		// Token: 0x04000D9B RID: 3483
		public static readonly Keys GeographyPointZMSridKeys = Keys.New(new string[] { "Kind", "Longitude", "Latitude", "Z", "M", "SRID" });

		// Token: 0x04000D9C RID: 3484
		public static readonly Keys GeometryPointKeys = Keys.New("Kind", "X", "Y");

		// Token: 0x04000D9D RID: 3485
		public static readonly Keys GeometryPointZKeys = Keys.New("Kind", "X", "Y", "Z");

		// Token: 0x04000D9E RID: 3486
		public static readonly Keys GeometryPointMKeys = Keys.New("Kind", "X", "Y", "M");

		// Token: 0x04000D9F RID: 3487
		public static readonly Keys GeometryPointZMKeys = Keys.New(new string[] { "Kind", "X", "Y", "Z", "M" });

		// Token: 0x04000DA0 RID: 3488
		public static readonly Keys GeometryPointSridKeys = Keys.New("Kind", "X", "Y", "SRID");

		// Token: 0x04000DA1 RID: 3489
		public static readonly Keys GeometryPointZSridKeys = Keys.New(new string[] { "Kind", "X", "Y", "Z", "SRID" });

		// Token: 0x04000DA2 RID: 3490
		public static readonly Keys GeometryPointMSridKeys = Keys.New(new string[] { "Kind", "X", "Y", "M", "SRID" });

		// Token: 0x04000DA3 RID: 3491
		public static readonly Keys GeometryPointZMSridKeys = Keys.New(new string[] { "Kind", "X", "Y", "Z", "M", "SRID" });

		// Token: 0x04000DA4 RID: 3492
		public static readonly Keys LineKeys = Keys.New("Kind", "Points");

		// Token: 0x04000DA5 RID: 3493
		public static readonly Keys LineWithSRIDKeys = Keys.New("Kind", "Points", "SRID");

		// Token: 0x04000DA6 RID: 3494
		public static readonly Keys PolygonKeys = Keys.New("Kind", "Rings");

		// Token: 0x04000DA7 RID: 3495
		public static readonly Keys PolygonWithSRIDKeys = Keys.New("Kind", "Rings", "SRID");

		// Token: 0x04000DA8 RID: 3496
		public static readonly Keys FullGlobeKeys = Keys.New("Kind");

		// Token: 0x04000DA9 RID: 3497
		public static readonly Keys FullGlobeWithSRIDKeys = Keys.New("Kind", "SRID");

		// Token: 0x04000DAA RID: 3498
		public static readonly Keys CollectionKeys = Keys.New("Kind", "Components");

		// Token: 0x04000DAB RID: 3499
		public static readonly Keys CollectionWithSRIDKeys = Keys.New("Kind", "Components", "SRID");

		// Token: 0x04000DAC RID: 3500
		private static readonly RecordValue RequiredNumber = RecordTypeValue.NewField(TypeValue.Number, null);

		// Token: 0x04000DAD RID: 3501
		private static readonly RecordValue OptionalNumber = RecordTypeValue.NewField(TypeValue.Number, LogicalValue.True);

		// Token: 0x04000DAE RID: 3502
		private static readonly NamedValue KindNamedValue = new NamedValue("Kind", RecordTypeValue.NewField(TypeValue.Text, null));

		// Token: 0x04000DAF RID: 3503
		private static readonly NamedValue SRIDNamedValue = new NamedValue("SRID", SpatialTypeValue.OptionalNumber);

		// Token: 0x04000DB0 RID: 3504
		public static readonly RecordTypeValue Geography = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			SpatialTypeValue.KindNamedValue,
			SpatialTypeValue.SRIDNamedValue
		}), true);

		// Token: 0x04000DB1 RID: 3505
		public static readonly RecordTypeValue GeographyPoint = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			SpatialTypeValue.KindNamedValue,
			new NamedValue("Longitude", SpatialTypeValue.RequiredNumber),
			new NamedValue("Latitude", SpatialTypeValue.RequiredNumber),
			new NamedValue("Z", SpatialTypeValue.OptionalNumber),
			new NamedValue("M", SpatialTypeValue.OptionalNumber),
			SpatialTypeValue.SRIDNamedValue
		}));

		// Token: 0x04000DB2 RID: 3506
		public static readonly RecordTypeValue Geometry = RecordTypeValue.New(RecordValue.New(new NamedValue[] { SpatialTypeValue.KindNamedValue }), true);

		// Token: 0x04000DB3 RID: 3507
		public static readonly RecordTypeValue GeometryPoint = RecordTypeValue.New(RecordValue.New(new NamedValue[]
		{
			SpatialTypeValue.KindNamedValue,
			new NamedValue("X", SpatialTypeValue.RequiredNumber),
			new NamedValue("Y", SpatialTypeValue.RequiredNumber),
			new NamedValue("Z", SpatialTypeValue.OptionalNumber),
			new NamedValue("M", SpatialTypeValue.OptionalNumber),
			SpatialTypeValue.SRIDNamedValue
		}));
	}
}
