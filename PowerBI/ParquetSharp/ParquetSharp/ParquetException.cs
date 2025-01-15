using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200007B RID: 123
	public class ParquetException : Exception
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0000CA24 File Offset: 0x0000AC24
		[NullableContext(1)]
		public ParquetException(string type, string message)
			: base(type + " (message: '" + message + "')")
		{
		}
	}
}
