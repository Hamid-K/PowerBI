using System;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020004CD RID: 1229
	public class ObjectStackEquality : IEqualityComparer<ImmutableStack<object>>
	{
		// Token: 0x06001B62 RID: 7010 RVA: 0x00002130 File Offset: 0x00000330
		private ObjectStackEquality()
		{
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001B63 RID: 7011 RVA: 0x000524F1 File Offset: 0x000506F1
		public static ObjectStackEquality Comparer
		{
			get
			{
				return ObjectStackEquality.Lazy.Value;
			}
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x00052500 File Offset: 0x00050700
		public bool Equals(ImmutableStack<object> x, ImmutableStack<object> y)
		{
			while (!x.IsEmpty && !y.IsEmpty)
			{
				if (!ValueEquality.Comparer.Equals(x.Peek(), y.Peek()))
				{
					return false;
				}
				x = x.Pop();
				y = y.Pop();
			}
			return x.IsEmpty && y.IsEmpty;
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x0005255C File Offset: 0x0005075C
		public int GetHashCode(ImmutableStack<object> obj)
		{
			int num = 0;
			while (!obj.IsEmpty)
			{
				num = (num ^ ValueEquality.Comparer.GetHashCode(obj.Peek())) * 17;
				obj = obj.Pop();
			}
			return num;
		}

		// Token: 0x04000D74 RID: 3444
		private static readonly Lazy<ObjectStackEquality> Lazy = new Lazy<ObjectStackEquality>(() => new ObjectStackEquality());
	}
}
