using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000DB RID: 219
	[Guid("2E262503-1BE3-4b58-B983-9F9108169D42")]
	public sealed class TraceCollection : MajorObjectCollection
	{
		// Token: 0x06000E70 RID: 3696 RVA: 0x00070279 File Offset: 0x0006E479
		internal TraceCollection(Server parent)
			: base(parent)
		{
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x00070282 File Offset: 0x0006E482
		internal override bool IsSyntacticallyValidName(string name, Type type, ModelType modelType, int compatibilityLevel, out string error)
		{
			return Utils.IsSyntacticallyValidName(name, type, modelType, compatibilityLevel, out error);
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x00070290 File Offset: 0x0006E490
		internal override bool IsSyntacticallyValidID(string id, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(id, type, out error);
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0007029A File Offset: 0x0006E49A
		internal override string GetSyntacticallyValidName(string namePrefix, Type type, ModelType modelType, int compatibilityLevel)
		{
			return Utils.GetSyntacticallyValidName(namePrefix, type, modelType, compatibilityLevel);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000702A6 File Offset: 0x0006E4A6
		internal override string GetSyntacticallyValidID(string idPrefix, Type type)
		{
			return Utils.GetSyntacticallyValidID(idPrefix, type);
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x000702AF File Offset: 0x0006E4AF
		protected override Type ItemsType
		{
			get
			{
				return typeof(Trace);
			}
		}

		// Token: 0x1700039A RID: 922
		public Trace this[int index]
		{
			get
			{
				return (Trace)base[index];
			}
		}

		// Token: 0x1700039B RID: 923
		public Trace this[string id]
		{
			get
			{
				return (Trace)base.GetItem(id, true, "ID");
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000702DD File Offset: 0x0006E4DD
		public Trace Find(string id)
		{
			return (Trace)base.GetItem(id, false, null);
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000702ED File Offset: 0x0006E4ED
		public Trace FindByName(string name)
		{
			return (Trace)base.BaseGetByName(name, false);
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x000702FC File Offset: 0x0006E4FC
		public Trace GetByName(string name)
		{
			return (Trace)base.BaseGetByName(name, true);
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x0007030B File Offset: 0x0006E50B
		public int Add(Trace item)
		{
			return base.Add(item);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00070314 File Offset: 0x0006E514
		public Trace Add(string name, string id)
		{
			Trace trace = new Trace(name, id);
			base.Add(trace);
			return trace;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00070332 File Offset: 0x0006E532
		public Trace Add(string name)
		{
			return this.Add(name, base.GetNewID(name));
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00070342 File Offset: 0x0006E542
		public Trace Add()
		{
			return this.Add(base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00070356 File Offset: 0x0006E556
		public void Insert(int index, Trace item)
		{
			base.Insert(index, item);
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00070360 File Offset: 0x0006E560
		public Trace Insert(int index, string name, string id)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			Trace trace = new Trace(name, id);
			base.Insert(index, trace);
			return trace;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000703A1 File Offset: 0x0006E5A1
		public Trace Insert(int index, string name)
		{
			return this.Insert(index, name, base.GetNewID(name));
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000703B2 File Offset: 0x0006E5B2
		public Trace Insert(int index)
		{
			return this.Insert(index, base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000703C7 File Offset: 0x0006E5C7
		public void Remove(Trace item)
		{
			base.Remove(item, true);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x000703D1 File Offset: 0x0006E5D1
		public void Remove(Trace item, bool cleanUp)
		{
			base.Remove(item, cleanUp);
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000703DB File Offset: 0x0006E5DB
		public void Remove(string id)
		{
			base.Remove(id, true);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000703E5 File Offset: 0x0006E5E5
		public new void Remove(string id, bool cleanUp)
		{
			base.Remove(id, cleanUp);
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000703EF File Offset: 0x0006E5EF
		public new Trace Move(int fromIndex, int toIndex)
		{
			return (Trace)base.Move(fromIndex, toIndex);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000703FE File Offset: 0x0006E5FE
		public new Trace Move(string id, int toIndex)
		{
			return (Trace)base.Move(id, toIndex);
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x0007040D File Offset: 0x0006E60D
		public void Move(Trace item, int toIndex)
		{
			base.Move(item, toIndex);
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00070417 File Offset: 0x0006E617
		public bool Contains(Trace item)
		{
			return base.Contains(item);
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00070420 File Offset: 0x0006E620
		public new bool Contains(string id)
		{
			return base.Contains(id);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x00070429 File Offset: 0x0006E629
		public int IndexOf(Trace item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00070432 File Offset: 0x0006E632
		public new int IndexOf(string id)
		{
			return base.IndexOf(id);
		}
	}
}
