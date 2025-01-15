using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000077 RID: 119
	public interface IUnknownIdentifierError : IError
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060001C2 RID: 450
		string Section { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060001C3 RID: 451
		string Name { get; }
	}
}
