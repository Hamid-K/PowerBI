using System;
using System.IO;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;

namespace Microsoft.DataShaping.Engine
{
	// Token: 0x0200001E RID: 30
	public static class StreamingStructureWriterFactory
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x000034AC File Offset: 0x000016AC
		public static IStreamingStructureWriter CreateNewtonsoftJsonWriter(Stream stream, int bufferSizeChars)
		{
			return new NewtonsoftJsonStreamingStructureWriter(stream, bufferSizeChars);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000034B5 File Offset: 0x000016B5
		public static IStreamingStructureEncodedWriter CreateJsonEncodedWriter(Stream stream)
		{
			return new JsonStreamingStructureWriter(stream);
		}
	}
}
