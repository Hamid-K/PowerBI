using System;
using System.Collections.Generic;

namespace Microsoft.OData.Client
{
	// Token: 0x02000038 RID: 56
	internal class WeakKeyComparer<T> : IEqualityComparer<object> where T : class
	{
		// Token: 0x060001BF RID: 447 RVA: 0x00008492 File Offset: 0x00006692
		public WeakKeyComparer(IEqualityComparer<T> comparer)
		{
			if (comparer == null)
			{
				comparer = EqualityComparer<T>.Default;
			}
			this.comparer = comparer;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x000084AB File Offset: 0x000066AB
		public static WeakKeyComparer<T> Default
		{
			get
			{
				if (WeakKeyComparer<T>.defaultInstance == null)
				{
					WeakKeyComparer<T>.defaultInstance = new WeakKeyComparer<T>(null);
				}
				return WeakKeyComparer<T>.defaultInstance;
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000084C4 File Offset: 0x000066C4
		public virtual int GetHashCode(object obj)
		{
			WeakKeyReference<T> weakKeyReference = obj as WeakKeyReference<T>;
			if (weakKeyReference != null)
			{
				return weakKeyReference.HashCode;
			}
			return this.comparer.GetHashCode((T)((object)obj));
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000084F4 File Offset: 0x000066F4
		public virtual bool Equals(object obj1, object obj2)
		{
			bool flag;
			T target = this.GetTarget(obj1, out flag);
			bool flag2;
			T target2 = this.GetTarget(obj2, out flag2);
			if (flag)
			{
				return flag2 && obj1 == obj2;
			}
			return !flag2 && this.comparer.Equals(target, target2);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00008534 File Offset: 0x00006734
		protected virtual T GetTarget(object obj, out bool isDead)
		{
			WeakKeyReference<T> weakKeyReference = obj as WeakKeyReference<T>;
			T t;
			if (weakKeyReference != null)
			{
				t = weakKeyReference.Target;
				isDead = !weakKeyReference.IsAlive;
			}
			else
			{
				t = (T)((object)obj);
				isDead = false;
			}
			return t;
		}

		// Token: 0x04000094 RID: 148
		protected IEqualityComparer<T> comparer;

		// Token: 0x04000095 RID: 149
		private static WeakKeyComparer<T> defaultInstance;
	}
}
