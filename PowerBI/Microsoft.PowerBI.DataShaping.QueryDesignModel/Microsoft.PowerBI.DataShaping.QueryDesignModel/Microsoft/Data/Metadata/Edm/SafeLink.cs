using System;
using System.Collections.Generic;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000AF RID: 175
	internal class SafeLink<TParent> where TParent : class
	{
		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000B7C RID: 2940 RVA: 0x0001D69E File Offset: 0x0001B89E
		public TParent Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x0001D6A8 File Offset: 0x0001B8A8
		internal static IEnumerable<TChild> BindChildren<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, IEnumerable<TChild> children)
		{
			foreach (TChild tchild in children)
			{
				SafeLink<TParent>.BindChild<TChild>(parent, getLink, tchild);
			}
			return children;
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0001D6F4 File Offset: 0x0001B8F4
		internal static TChild BindChild<TChild>(TParent parent, Func<TChild, SafeLink<TParent>> getLink, TChild child)
		{
			getLink(child)._value = parent;
			return child;
		}

		// Token: 0x040008AF RID: 2223
		private TParent _value;
	}
}
