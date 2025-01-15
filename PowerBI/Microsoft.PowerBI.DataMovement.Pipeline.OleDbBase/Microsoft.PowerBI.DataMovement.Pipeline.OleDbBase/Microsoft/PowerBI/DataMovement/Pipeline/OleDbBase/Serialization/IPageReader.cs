using System;
using System.Data;
using System.Runtime.CompilerServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization
{
	// Token: 0x020000D8 RID: 216
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	public interface IPageReader : IDisposable
	{
		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003F4 RID: 1012
		DataTable SchemaTable { get; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003F5 RID: 1013
		IProgress Progress { get; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003F6 RID: 1014
		// (set) Token: 0x060003F7 RID: 1015
		bool CancelIssued { get; set; }

		// Token: 0x060003F8 RID: 1016
		IPage CreatePage();

		// Token: 0x060003F9 RID: 1017
		void Read(IPage page);
	}
}
