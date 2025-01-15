using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001610 RID: 5648
	internal static class SerializedTextTypeValue
	{
		// Token: 0x04004D36 RID: 19766
		public static readonly TypeValue SerializedGeographyType = TypeValue.SerializedText.NewMeta(RecordValue.New(Keys.New("Serialization.Format"), new Value[] { TextValue.New("GeographyWKT") })).AsType;

		// Token: 0x04004D37 RID: 19767
		public static readonly TypeValue SerializedGeometryType = TypeValue.SerializedText.NewMeta(RecordValue.New(Keys.New("Serialization.Format"), new Value[] { TextValue.New("GeometryWKT") })).AsType;
	}
}
