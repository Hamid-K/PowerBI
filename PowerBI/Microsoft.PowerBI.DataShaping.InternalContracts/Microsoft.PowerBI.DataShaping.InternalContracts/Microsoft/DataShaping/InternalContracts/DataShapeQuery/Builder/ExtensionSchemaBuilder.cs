using System;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Builder
{
	// Token: 0x020000E4 RID: 228
	internal class ExtensionSchemaBuilder : ExtensionSchemaBuilder<object>
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x0000DAC3 File Offset: 0x0000BCC3
		internal ExtensionSchemaBuilder(string name)
			: base(new object(), new ExtensionSchema())
		{
			base.WithName(name);
		}
	}
}
