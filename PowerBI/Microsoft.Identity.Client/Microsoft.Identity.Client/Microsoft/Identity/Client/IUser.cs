using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Microsoft.Identity.Client
{
	// Token: 0x0200015F RID: 351
	[EditorBrowsable(EditorBrowsableState.Never)]
	[Obsolete("Use IAccount instead (See https://aka.ms/msal-net-2-released)", true)]
	public interface IUser
	{
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06001135 RID: 4405
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use IAccount.Username instead (See https://aka.ms/msal-net-2-released)", true)]
		string DisplayableId { get; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001136 RID: 4406
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use IAccount.Username instead (See https://aka.ms/msal-net-2-released)", true)]
		string Name { get; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06001137 RID: 4407
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use IAccount.Environment instead to get the Identity Provider host (See https://aka.ms/msal-net-2-released)", true)]
		string IdentityProvider { get; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06001138 RID: 4408
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[Obsolete("Use IAccount.HomeAccountId.Identifier instead to get the user identifier (See https://aka.ms/msal-net-2-released)", true)]
		string Identifier { get; }
	}
}
