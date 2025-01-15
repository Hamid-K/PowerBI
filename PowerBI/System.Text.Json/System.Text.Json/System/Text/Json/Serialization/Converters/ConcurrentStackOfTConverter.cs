using System;
using System.Collections.Concurrent;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C6 RID: 198
	internal sealed class ConcurrentStackOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : ConcurrentStack<TElement>
	{
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0002E237 File Offset: 0x0002C437
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0002E23A File Offset: 0x0002C43A
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((TCollection)((object)state.Current.ReturnValue)).Push(value);
		}
	}
}
