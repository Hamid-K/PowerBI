using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Immutable
{
	// Token: 0x020020CA RID: 8394
	internal class SecurePooledObject<[Nullable(2)] T>
	{
		// Token: 0x060119C9 RID: 72137 RVA: 0x003C364F File Offset: 0x003C184F
		[NullableContext(1)]
		internal SecurePooledObject(T newValue)
		{
			Requires.NotNullAllowStructs<T>(newValue, "newValue");
			this._value = newValue;
		}

		// Token: 0x17002F41 RID: 12097
		// (get) Token: 0x060119CA RID: 72138 RVA: 0x003C3669 File Offset: 0x003C1869
		// (set) Token: 0x060119CB RID: 72139 RVA: 0x003C3671 File Offset: 0x003C1871
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

		// Token: 0x060119CC RID: 72140 RVA: 0x003C367A File Offset: 0x003C187A
		[return: Nullable(1)]
		internal T Use<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			if (!this.IsOwned<TCaller>(ref caller))
			{
				Requires.FailObjectDisposed<TCaller>(caller);
			}
			return this._value;
		}

		// Token: 0x060119CD RID: 72141 RVA: 0x003C3696 File Offset: 0x003C1896
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

		// Token: 0x060119CE RID: 72142 RVA: 0x003C36B7 File Offset: 0x003C18B7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsOwned<TCaller>(ref TCaller caller) where TCaller : struct, ISecurePooledObjectUser
		{
			return caller.PoolUserId == this._owner;
		}

		// Token: 0x0400699C RID: 27036
		private readonly T _value;

		// Token: 0x0400699D RID: 27037
		private int _owner;
	}
}
