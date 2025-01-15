using System;
using System.Collections.Generic;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010F5 RID: 4341
	internal abstract class OleDbClient : IDisposable
	{
		// Token: 0x06007186 RID: 29062
		public abstract IPageReader ExecuteCommand(IList<Type> types, string text);

		// Token: 0x06007187 RID: 29063
		public abstract void Dispose();
	}
}
