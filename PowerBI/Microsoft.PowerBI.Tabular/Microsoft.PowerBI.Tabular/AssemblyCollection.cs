using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D0 RID: 208
	[Guid("33C44057-2AED-4991-B2F3-A3ED6550C67C")]
	internal sealed class AssemblyCollection : MajorObjectCollection
	{
		// Token: 0x06000D13 RID: 3347 RVA: 0x0006C6B4 File Offset: 0x0006A8B4
		internal AssemblyCollection(Server parent)
			: base(parent)
		{
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0006C6BD File Offset: 0x0006A8BD
		internal AssemblyCollection(Database parent)
			: base(parent)
		{
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0006C6C6 File Offset: 0x0006A8C6
		internal override bool IsSyntacticallyValidName(string name, Type type, ModelType modelType, int compatibilityLevel, out string error)
		{
			return Utils.IsSyntacticallyValidName(name, type, modelType, compatibilityLevel, out error);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0006C6D4 File Offset: 0x0006A8D4
		internal override bool IsSyntacticallyValidID(string id, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(id, type, out error);
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0006C6DE File Offset: 0x0006A8DE
		internal override string GetSyntacticallyValidName(string namePrefix, Type type, ModelType modelType, int compatibilityLevel)
		{
			return Utils.GetSyntacticallyValidName(namePrefix, type, modelType, compatibilityLevel);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0006C6EA File Offset: 0x0006A8EA
		internal override string GetSyntacticallyValidID(string idPrefix, Type type)
		{
			return Utils.GetSyntacticallyValidID(idPrefix, type);
		}

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0006C6F3 File Offset: 0x0006A8F3
		protected override Type ItemsType
		{
			get
			{
				return typeof(Assembly);
			}
		}

		// Token: 0x1700034F RID: 847
		public Assembly this[int index]
		{
			get
			{
				return (Assembly)base[index];
			}
		}

		// Token: 0x17000350 RID: 848
		public Assembly this[string id]
		{
			get
			{
				return (Assembly)base.GetItem(id, true, "ID");
			}
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0006C721 File Offset: 0x0006A921
		public Assembly Find(string id)
		{
			return (Assembly)base.GetItem(id, false, null);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0006C731 File Offset: 0x0006A931
		public Assembly FindByName(string name)
		{
			return (Assembly)base.BaseGetByName(name, false);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0006C740 File Offset: 0x0006A940
		public Assembly GetByName(string name)
		{
			return (Assembly)base.BaseGetByName(name, true);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0006C74F File Offset: 0x0006A94F
		public int Add(Assembly item)
		{
			return base.Add(item);
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0006C758 File Offset: 0x0006A958
		public ClrAssembly Add(string name, string id)
		{
			ClrAssembly clrAssembly = new ClrAssembly(name, id);
			base.Add(clrAssembly);
			return clrAssembly;
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x0006C776 File Offset: 0x0006A976
		public ClrAssembly Add(string name)
		{
			return this.Add(name, base.GetNewID(name));
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x0006C786 File Offset: 0x0006A986
		public ClrAssembly Add()
		{
			return this.Add(base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0006C79A File Offset: 0x0006A99A
		public void Insert(int index, Assembly item)
		{
			base.Insert(index, item);
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x0006C7A4 File Offset: 0x0006A9A4
		public ClrAssembly Insert(int index, string name, string id)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			ClrAssembly clrAssembly = new ClrAssembly(name, id);
			base.Insert(index, clrAssembly);
			return clrAssembly;
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x0006C7E5 File Offset: 0x0006A9E5
		public ClrAssembly Insert(int index, string name)
		{
			return this.Insert(index, name, base.GetNewID(name));
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x0006C7F6 File Offset: 0x0006A9F6
		public ClrAssembly Insert(int index)
		{
			return this.Insert(index, base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x0006C80B File Offset: 0x0006AA0B
		public void Remove(Assembly item)
		{
			base.Remove(item, true);
		}

		// Token: 0x06000D28 RID: 3368 RVA: 0x0006C815 File Offset: 0x0006AA15
		public void Remove(Assembly item, bool cleanUp)
		{
			base.Remove(item, cleanUp);
		}

		// Token: 0x06000D29 RID: 3369 RVA: 0x0006C81F File Offset: 0x0006AA1F
		public void Remove(string id)
		{
			base.Remove(id, true);
		}

		// Token: 0x06000D2A RID: 3370 RVA: 0x0006C829 File Offset: 0x0006AA29
		public new void Remove(string id, bool cleanUp)
		{
			base.Remove(id, cleanUp);
		}

		// Token: 0x06000D2B RID: 3371 RVA: 0x0006C833 File Offset: 0x0006AA33
		public new Assembly Move(int fromIndex, int toIndex)
		{
			return (Assembly)base.Move(fromIndex, toIndex);
		}

		// Token: 0x06000D2C RID: 3372 RVA: 0x0006C842 File Offset: 0x0006AA42
		public new Assembly Move(string id, int toIndex)
		{
			return (Assembly)base.Move(id, toIndex);
		}

		// Token: 0x06000D2D RID: 3373 RVA: 0x0006C851 File Offset: 0x0006AA51
		public void Move(Assembly item, int toIndex)
		{
			base.Move(item, toIndex);
		}

		// Token: 0x06000D2E RID: 3374 RVA: 0x0006C85B File Offset: 0x0006AA5B
		public bool Contains(Assembly item)
		{
			return base.Contains(item);
		}

		// Token: 0x06000D2F RID: 3375 RVA: 0x0006C864 File Offset: 0x0006AA64
		public new bool Contains(string id)
		{
			return base.Contains(id);
		}

		// Token: 0x06000D30 RID: 3376 RVA: 0x0006C86D File Offset: 0x0006AA6D
		public int IndexOf(Assembly item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06000D31 RID: 3377 RVA: 0x0006C876 File Offset: 0x0006AA76
		public new int IndexOf(string id)
		{
			return base.IndexOf(id);
		}
	}
}
