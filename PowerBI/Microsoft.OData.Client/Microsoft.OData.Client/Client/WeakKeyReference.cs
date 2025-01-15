using System;

namespace Microsoft.OData.Client
{
	// Token: 0x02000037 RID: 55
	internal sealed class WeakKeyReference<T> : WeakReference where T : class
	{
		// Token: 0x060001BB RID: 443 RVA: 0x00008440 File Offset: 0x00006640
		public WeakKeyReference(T target, WeakKeyComparer<T> comparer)
			: base(target, false)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			this.HashCode = comparer.GetHashCode(target);
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00008474 File Offset: 0x00006674
		// (set) Token: 0x060001BD RID: 445 RVA: 0x0000847C File Offset: 0x0000667C
		public int HashCode { get; private set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00008485 File Offset: 0x00006685
		public new T Target
		{
			get
			{
				return (T)((object)base.Target);
			}
		}
	}
}
