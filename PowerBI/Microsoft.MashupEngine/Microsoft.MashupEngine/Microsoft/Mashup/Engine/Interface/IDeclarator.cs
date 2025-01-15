using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000C7 RID: 199
	public interface IDeclarator
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000316 RID: 790
		int Count { get; }

		// Token: 0x1700010B RID: 267
		Identifier this[int index] { get; }
	}
}
