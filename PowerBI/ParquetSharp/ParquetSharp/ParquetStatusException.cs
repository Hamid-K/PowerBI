using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200007F RID: 127
	public sealed class ParquetStatusException : ParquetException
	{
		// Token: 0x06000369 RID: 873 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		[NullableContext(1)]
		public ParquetStatusException(string type, string message, StatusCode statusCode)
			: base(type, message)
		{
			this.StatusCode = statusCode;
		}

		// Token: 0x040000F6 RID: 246
		public readonly StatusCode StatusCode;
	}
}
