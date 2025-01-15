using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x02000048 RID: 72
	internal class SecurePooledObject<[Nullable(2)] T>
	{
		// Token: 0x06000371 RID: 881 RVA: 0x000092BB File Offset: 0x000074BB
		[NullableContext(1)]
		internal SecurePooledObject(T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			this._value = newValue;
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000092D5 File Offset: 0x000074D5
		// (set) Token: 0x06000373 RID: 883 RVA: 0x000092DD File Offset: 0x000074DD
		internal int Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000092E6 File Offset: 0x000074E6
		[return: Nullable(1)]
		internal T Use<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			if (!this.IsOwned<TCaller>(ref caller))
			{
				Requires.FailObjectDisposed<TCaller>(caller);
			}
			return this._value;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009302 File Offset: 0x00007502
		internal bool TryUse<TCaller>(ref TCaller caller, [Nullable(1)] [MaybeNullWhen(false)] out T value) where TCaller : struct, ISecurePooledObjectUser
		{
			if (this.IsOwned<TCaller>(ref caller))
			{
				value = this._value;
				return true;
			}
			value = default(T);
			return false;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009323 File Offset: 0x00007523
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsOwned<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			return caller.PoolUserId == this._owner;
		}

		// Token: 0x04000042 RID: 66
		private readonly T _value;

		// Token: 0x04000043 RID: 67
		private int _owner;
	}
}
