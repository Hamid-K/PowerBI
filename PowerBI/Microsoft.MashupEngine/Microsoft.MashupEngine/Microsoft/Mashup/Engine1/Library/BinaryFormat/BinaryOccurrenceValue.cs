using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.BinaryFormat
{
	// Token: 0x02000EB2 RID: 3762
	internal static class BinaryOccurrenceValue
	{
		// Token: 0x04003688 RID: 13960
		public static readonly IntEnumTypeValue<BinaryOccurrence> Type = new IntEnumTypeValue<BinaryOccurrence>("BinaryOccurrence.Type");

		// Token: 0x04003689 RID: 13961
		public static readonly NumberValue Optional = BinaryOccurrenceValue.Type.NewEnumValue("BinaryOccurrence.Optional", 0, BinaryOccurrence.Optional, null);

		// Token: 0x0400368A RID: 13962
		public static readonly NumberValue Required = BinaryOccurrenceValue.Type.NewEnumValue("BinaryOccurrence.Required", 1, BinaryOccurrence.Required, null);

		// Token: 0x0400368B RID: 13963
		public static readonly NumberValue Repeating = BinaryOccurrenceValue.Type.NewEnumValue("BinaryOccurrence.Repeating", 2, BinaryOccurrence.Repeating, null);

		// Token: 0x0400368C RID: 13964
		private static readonly IntEnumTypeValue<BinaryOccurrence> AlternateType = new IntEnumTypeValue<BinaryOccurrence>("BinaryOccurrence.Type");

		// Token: 0x0400368D RID: 13965
		public static readonly NumberValue AlternateOptional = BinaryOccurrenceValue.AlternateType.NewEnumValue("Occurrence.Optional", 0, BinaryOccurrence.Optional, null);

		// Token: 0x0400368E RID: 13966
		public static readonly NumberValue AlternateRequired = BinaryOccurrenceValue.AlternateType.NewEnumValue("Occurrence.Required", 1, BinaryOccurrence.Required, null);

		// Token: 0x0400368F RID: 13967
		public static readonly NumberValue AlternateRepeating = BinaryOccurrenceValue.AlternateType.NewEnumValue("Occurrence.Repeating", 2, BinaryOccurrence.Repeating, null);
	}
}
