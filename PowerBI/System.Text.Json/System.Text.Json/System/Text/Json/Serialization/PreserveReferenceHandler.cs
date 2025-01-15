using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000094 RID: 148
	internal sealed class PreserveReferenceHandler : ReferenceHandler
	{
		// Token: 0x060008F9 RID: 2297 RVA: 0x00026F95 File Offset: 0x00025195
		public override ReferenceResolver CreateResolver()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060008FA RID: 2298 RVA: 0x00026F9C File Offset: 0x0002519C
		internal override ReferenceResolver CreateResolver(bool writing)
		{
			return new PreserveReferenceResolver(writing);
		}
	}
}
