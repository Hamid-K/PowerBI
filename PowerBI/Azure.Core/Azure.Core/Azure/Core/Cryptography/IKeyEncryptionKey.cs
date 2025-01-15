using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Azure.Core.Cryptography
{
	// Token: 0x020000CB RID: 203
	[NullableContext(1)]
	public interface IKeyEncryptionKey
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060006CC RID: 1740
		string KeyId { get; }

		// Token: 0x060006CD RID: 1741
		byte[] WrapKey(string algorithm, [Nullable(0)] ReadOnlyMemory<byte> key, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060006CE RID: 1742
		Task<byte[]> WrapKeyAsync(string algorithm, [Nullable(0)] ReadOnlyMemory<byte> key, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060006CF RID: 1743
		byte[] UnwrapKey(string algorithm, [Nullable(0)] ReadOnlyMemory<byte> encryptedKey, CancellationToken cancellationToken = default(CancellationToken));

		// Token: 0x060006D0 RID: 1744
		Task<byte[]> UnwrapKeyAsync(string algorithm, [Nullable(0)] ReadOnlyMemory<byte> encryptedKey, CancellationToken cancellationToken = default(CancellationToken));
	}
}
