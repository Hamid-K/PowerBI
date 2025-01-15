using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.BinaryFormat
{
	// Token: 0x02000EB3 RID: 3763
	internal static class ByteOrderValue
	{
		// Token: 0x04003690 RID: 13968
		public static readonly IntEnumTypeValue<ByteOrder> Type = new IntEnumTypeValue<ByteOrder>("ByteOrder.Type");

		// Token: 0x04003691 RID: 13969
		public static readonly NumberValue LittleEndian = ByteOrderValue.Type.NewEnumValue("ByteOrder.LittleEndian", 0, ByteOrder.LittleEndian, null);

		// Token: 0x04003692 RID: 13970
		public static readonly NumberValue BigEndian = ByteOrderValue.Type.NewEnumValue("ByteOrder.BigEndian", 1, ByteOrder.BigEndian, null);
	}
}
