using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Cryptography
{
	// Token: 0x020000CC RID: 204
	[NullableContext(1)]
	public interface IKeyEncryptionKeyResolver
	{
		// Token: 0x060006D1 RID: 1745
		IKeyEncryptionKey Resolve(string keyId, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060006D2 RID: 1746
		Task<IKeyEncryptionKey> ResolveAsync(string keyId, CancellationToken cancellationToken = default(CancellationToken));
	}
}
