using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeResultWriter
{
	// Token: 0x02000040 RID: 64
	internal sealed class DataLimitWriter : DsrObjectWriterBase
	{
		// Token: 0x0600015F RID: 351 RVA: 0x000047AC File Offset: 0x000029AC
		internal void WriteId(string value)
		{
			base.Writer.WriteProperty(base.DsrNames.Id, value);
		}
	}
}
