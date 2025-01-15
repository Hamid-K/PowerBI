using System;
using System.Collections.Concurrent;

namespace System.Text.Json.Serialization.Converters
{
	// Token: 0x020000C5 RID: 197
	internal sealed class ConcurrentQueueOfTConverter<TCollection, TElement> : IEnumerableDefaultConverter<TCollection, TElement> where TCollection : ConcurrentQueue<TElement>
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0002E20A File Offset: 0x0002C40A
		internal override bool CanPopulate
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002E20D File Offset: 0x0002C40D
		protected override void Add(in TElement value, ref ReadStack state)
		{
			((TCollection)((object)state.Current.ReturnValue)).Enqueue(value);
		}
	}
}
