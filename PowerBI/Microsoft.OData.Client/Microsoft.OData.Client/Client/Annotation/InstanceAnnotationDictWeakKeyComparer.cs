using System;
using System.Reflection;

namespace Microsoft.OData.Client.Annotation
{
	// Token: 0x02000113 RID: 275
	internal class InstanceAnnotationDictWeakKeyComparer : WeakKeyComparer<object>
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x0002C72B File Offset: 0x0002A92B
		private InstanceAnnotationDictWeakKeyComparer()
			: base(null)
		{
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0002C734 File Offset: 0x0002A934
		public new static InstanceAnnotationDictWeakKeyComparer Default
		{
			get
			{
				if (InstanceAnnotationDictWeakKeyComparer.defaultInstance == null)
				{
					InstanceAnnotationDictWeakKeyComparer.defaultInstance = new InstanceAnnotationDictWeakKeyComparer();
				}
				return InstanceAnnotationDictWeakKeyComparer.defaultInstance;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0002C74C File Offset: 0x0002A94C
		public override int GetHashCode(object obj)
		{
			WeakKeyReference<object> weakKeyReference = obj as WeakKeyReference<object>;
			if (weakKeyReference != null)
			{
				return weakKeyReference.HashCode;
			}
			Tuple<object, MemberInfo> tuple = obj as Tuple<object, MemberInfo>;
			if (tuple != null)
			{
				return tuple.Item1.GetHashCode() ^ tuple.Item2.GetHashCode();
			}
			Tuple<WeakKeyReference<object>, MemberInfo> tuple2 = obj as Tuple<WeakKeyReference<object>, MemberInfo>;
			if (tuple2 != null)
			{
				return tuple2.Item1.HashCode ^ tuple2.Item2.GetHashCode();
			}
			return this.comparer.GetHashCode(obj);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0002C7BC File Offset: 0x0002A9BC
		public object CreateKey(object obj)
		{
			Tuple<object, MemberInfo> tuple = obj as Tuple<object, MemberInfo>;
			if (tuple != null)
			{
				return new Tuple<WeakKeyReference<object>, MemberInfo>(new WeakKeyReference<object>(tuple.Item1, this), tuple.Item2);
			}
			return new WeakKeyReference<object>(obj, this);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0002C7F4 File Offset: 0x0002A9F4
		public bool RemoveRule(object key)
		{
			Tuple<WeakKeyReference<object>, MemberInfo> tuple = key as Tuple<WeakKeyReference<object>, MemberInfo>;
			return tuple != null && !tuple.Item1.IsAlive;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0002C81C File Offset: 0x0002AA1C
		protected override object GetTarget(object obj, out bool isDead)
		{
			WeakKeyReference<object> weakKeyReference = obj as WeakKeyReference<object>;
			if (weakKeyReference != null)
			{
				isDead = !weakKeyReference.IsAlive;
				return weakKeyReference.Target;
			}
			Tuple<WeakKeyReference<object>, MemberInfo> tuple = obj as Tuple<WeakKeyReference<object>, MemberInfo>;
			if (tuple != null)
			{
				return new Tuple<object, MemberInfo>(this.GetTarget(tuple.Item1, out isDead), tuple.Item2);
			}
			isDead = false;
			return obj;
		}

		// Token: 0x0400064E RID: 1614
		private static InstanceAnnotationDictWeakKeyComparer defaultInstance;
	}
}
