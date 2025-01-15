using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D6 RID: 214
	[Guid("E68E368A-5BBB-4b37-922A-03F099FEAA3F")]
	public sealed class RoleCollection : MajorObjectCollection
	{
		// Token: 0x06000DCF RID: 3535 RVA: 0x0006E95B File Offset: 0x0006CB5B
		internal RoleCollection(Server parent)
			: base(parent)
		{
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0006E964 File Offset: 0x0006CB64
		internal RoleCollection(Database parent)
			: base(parent)
		{
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x0006E96D File Offset: 0x0006CB6D
		internal override bool IsSyntacticallyValidName(string name, Type type, ModelType modelType, int compatibilityLevel, out string error)
		{
			return Utils.IsSyntacticallyValidName(name, type, modelType, compatibilityLevel, out error);
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0006E97B File Offset: 0x0006CB7B
		internal override bool IsSyntacticallyValidID(string id, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(id, type, out error);
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x0006E985 File Offset: 0x0006CB85
		internal override string GetSyntacticallyValidName(string namePrefix, Type type, ModelType modelType, int compatibilityLevel)
		{
			return Utils.GetSyntacticallyValidName(namePrefix, type, modelType, compatibilityLevel);
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0006E991 File Offset: 0x0006CB91
		internal override string GetSyntacticallyValidID(string idPrefix, Type type)
		{
			return Utils.GetSyntacticallyValidID(idPrefix, type);
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DD5 RID: 3541 RVA: 0x0006E99A File Offset: 0x0006CB9A
		protected override Type ItemsType
		{
			get
			{
				return typeof(Role);
			}
		}

		// Token: 0x17000376 RID: 886
		public Role this[int index]
		{
			get
			{
				return (Role)base[index];
			}
		}

		// Token: 0x17000377 RID: 887
		public Role this[string id]
		{
			get
			{
				return (Role)base.GetItem(id, true, "ID");
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0006E9C8 File Offset: 0x0006CBC8
		public Role Find(string id)
		{
			return (Role)base.GetItem(id, false, null);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0006E9D8 File Offset: 0x0006CBD8
		public Role FindByName(string name)
		{
			return (Role)base.BaseGetByName(name, false);
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0006E9E7 File Offset: 0x0006CBE7
		public Role GetByName(string name)
		{
			return (Role)base.BaseGetByName(name, true);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0006E9F6 File Offset: 0x0006CBF6
		public int Add(Role item)
		{
			return base.Add(item);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0006EA00 File Offset: 0x0006CC00
		public Role Add(string name, string id)
		{
			Role role = new Role(name, id);
			base.Add(role);
			return role;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0006EA1E File Offset: 0x0006CC1E
		public Role Add(string name)
		{
			return this.Add(name, base.GetNewID(name));
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0006EA2E File Offset: 0x0006CC2E
		public Role Add()
		{
			return this.Add(base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0006EA42 File Offset: 0x0006CC42
		public void Insert(int index, Role item)
		{
			base.Insert(index, item);
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0006EA4C File Offset: 0x0006CC4C
		public Role Insert(int index, string name, string id)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			Role role = new Role(name, id);
			base.Insert(index, role);
			return role;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0006EA8D File Offset: 0x0006CC8D
		public Role Insert(int index, string name)
		{
			return this.Insert(index, name, base.GetNewID(name));
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0006EA9E File Offset: 0x0006CC9E
		public Role Insert(int index)
		{
			return this.Insert(index, base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0006EAB3 File Offset: 0x0006CCB3
		public void Remove(Role item)
		{
			base.Remove(item, true);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x0006EABD File Offset: 0x0006CCBD
		public void Remove(Role item, bool cleanUp)
		{
			base.Remove(item, cleanUp);
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x0006EAC7 File Offset: 0x0006CCC7
		public void Remove(string id)
		{
			base.Remove(id, true);
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0006EAD1 File Offset: 0x0006CCD1
		public new void Remove(string id, bool cleanUp)
		{
			base.Remove(id, cleanUp);
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0006EADB File Offset: 0x0006CCDB
		public new Role Move(int fromIndex, int toIndex)
		{
			return (Role)base.Move(fromIndex, toIndex);
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x0006EAEA File Offset: 0x0006CCEA
		public new Role Move(string id, int toIndex)
		{
			return (Role)base.Move(id, toIndex);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x0006EAF9 File Offset: 0x0006CCF9
		public void Move(Role item, int toIndex)
		{
			base.Move(item, toIndex);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0006EB03 File Offset: 0x0006CD03
		public bool Contains(Role item)
		{
			return base.Contains(item);
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x0006EB0C File Offset: 0x0006CD0C
		public new bool Contains(string id)
		{
			return base.Contains(id);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x0006EB15 File Offset: 0x0006CD15
		public int IndexOf(Role item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0006EB1E File Offset: 0x0006CD1E
		public new int IndexOf(string id)
		{
			return base.IndexOf(id);
		}
	}
}
