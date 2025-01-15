using System;
using System.Collections;
using Microsoft.DataShaping.Common.Json;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000051 RID: 81
	internal sealed class FlagSequenceWriter : InlinedDsrStructureWriterBase
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x0000534C File Offset: 0x0000354C
		internal void WriteProperty(string name, BitArray flags)
		{
			long num = FlagSequenceConverter.ConvertToNumber(flags);
			base.Writer.WriteJsonEncodedProperty(name, JsonValueUtils.ToString(num));
		}

		// Token: 0x040000D8 RID: 216
		internal static int MaxFlagSequenceSize = FlagSequenceConverter.MaxEncodingBits;
	}
}
