using System;
using System.Runtime.Serialization;

namespace <CrtImplementationDetails>
{
	// Token: 0x020000AA RID: 170
	[Serializable]
	internal class ModuleLoadException : Exception
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000B730 File Offset: 0x0000B730
		protected ModuleLoadException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000B714 File Offset: 0x0000B714
		public ModuleLoadException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000B6F8 File Offset: 0x0000B6F8
		public ModuleLoadException(string message)
			: base(message)
		{
		}

		// Token: 0x0400019A RID: 410
		public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";
	}
}
