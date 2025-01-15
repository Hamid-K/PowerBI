using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x0200009B RID: 155
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x0000BA30 File Offset: 0x0000AE30
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060001E7 RID: 487 RVA: 0x0000BA14 File Offset: 0x0000AE14
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x0000B9F8 File Offset: 0x0000ADF8
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x04000226 RID: 550
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
