using System;
using System.Runtime.Serialization;

namespace Microsoft.Fabric.Common
{
	// Token: 0x02000400 RID: 1024
	[Serializable]
	internal class ExpressionException : Exception
	{
		// Token: 0x060023D8 RID: 9176 RVA: 0x0001E135 File Offset: 0x0001C335
		public ExpressionException(string message)
			: base(message)
		{
		}

		// Token: 0x060023D9 RID: 9177 RVA: 0x0001E13E File Offset: 0x0001C33E
		public ExpressionException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x060023DA RID: 9178 RVA: 0x0001E148 File Offset: 0x0001C348
		protected ExpressionException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
