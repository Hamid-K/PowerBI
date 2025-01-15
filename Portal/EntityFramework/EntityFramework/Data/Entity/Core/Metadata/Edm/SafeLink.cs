using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F7 RID: 1271
	internal class SafeLink<TParent> where TParent : class
	{
		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06003EED RID: 16109 RVA: 0x000D1411 File Offset: 0x000CF611
		public TParent Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06003EEE RID: 16110 RVA: 0x000D141C File Offset: 0x000CF61C
		internal static IEnumerable<TChild> BindChildren<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, IEnumerable<TChild> children)
		{
			foreach (TChild tchild in children)
			{
				SafeLink<TParent>.BindChild<TChild>(parent, getLink, tchild);
			}
			return children;
		}

		// Token: 0x06003EEF RID: 16111 RVA: 0x000D1468 File Offset: 0x000CF668
		internal static TChild BindChild<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, TChild child)
		{
			getLink(child)._value = parent;
			return child;
		}

		// Token: 0x0400157A RID: 5498
		private TParent _value;
	}
}
