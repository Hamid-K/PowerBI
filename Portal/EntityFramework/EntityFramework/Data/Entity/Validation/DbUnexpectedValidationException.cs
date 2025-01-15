using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace System.Data.Entity.Validation
{
	// Token: 0x02000070 RID: 112
	[Serializable]
	public class DbUnexpectedValidationException : DataException
	{
		// Token: 0x06000424 RID: 1060 RVA: 0x0000F75D File Offset: 0x0000D95D
		public DbUnexpectedValidationException()
		{
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000F765 File Offset: 0x0000D965
		public DbUnexpectedValidationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0000F76E File Offset: 0x0000D96E
		public DbUnexpectedValidationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0000F778 File Offset: 0x0000D978
		[ExcludeFromCodeCoverage]
		protected DbUnexpectedValidationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
