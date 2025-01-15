using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000B3 RID: 179
	public interface IFieldType
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002F9 RID: 761
		Identifier Name { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002FA RID: 762
		IExpression Type { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002FB RID: 763
		bool Optional { get; }
	}
}
