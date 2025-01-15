using System;
using System.Runtime.InteropServices;
using Microsoft.AnalysisServices.Core;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x020000D4 RID: 212
	[Guid("F4C66028-67C0-4a43-9169-1D53381162D6")]
	public sealed class DatabaseCollection : MajorObjectCollection
	{
		// Token: 0x06000DA6 RID: 3494 RVA: 0x0006E6DA File Offset: 0x0006C8DA
		internal DatabaseCollection(Server parent)
			: base(parent)
		{
			this.server = parent;
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x0006E6EA File Offset: 0x0006C8EA
		internal override bool IsSyntacticallyValidName(string name, Type type, ModelType modelType, int compatibilityLevel, out string error)
		{
			return Utils.IsSyntacticallyValidName(name, type, modelType, compatibilityLevel, out error);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0006E6F8 File Offset: 0x0006C8F8
		internal override bool IsSyntacticallyValidID(string id, Type type, out string error)
		{
			return Utils.IsSyntacticallyValidID(id, type, out error);
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x0006E702 File Offset: 0x0006C902
		internal override string GetSyntacticallyValidName(string namePrefix, Type type, ModelType modelType, int compatibilityLevel)
		{
			return Utils.GetSyntacticallyValidName(namePrefix, type, modelType, compatibilityLevel);
		}

		// Token: 0x06000DAA RID: 3498 RVA: 0x0006E70E File Offset: 0x0006C90E
		internal override string GetSyntacticallyValidID(string idPrefix, Type type)
		{
			return Utils.GetSyntacticallyValidID(idPrefix, type);
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x0006E717 File Offset: 0x0006C917
		protected override Type ItemsType
		{
			get
			{
				return typeof(Database);
			}
		}

		// Token: 0x17000371 RID: 881
		public Database this[int index]
		{
			get
			{
				return (Database)base[index];
			}
		}

		// Token: 0x17000372 RID: 882
		public Database this[string id]
		{
			get
			{
				return (Database)base.GetItem(id, true, "ID");
			}
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x0006E745 File Offset: 0x0006C945
		public Database Find(string id)
		{
			return (Database)base.GetItem(id, false, null);
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x0006E755 File Offset: 0x0006C955
		public Database FindByName(string name)
		{
			return (Database)base.BaseGetByName(name, false);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x0006E764 File Offset: 0x0006C964
		public Database GetByName(string name)
		{
			return (Database)base.BaseGetByName(name, true);
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x0006E773 File Offset: 0x0006C973
		public int Add(Database item)
		{
			return base.Add(item);
		}

		// Token: 0x06000DB2 RID: 3506 RVA: 0x0006E77C File Offset: 0x0006C97C
		public Database Add(string name, string id)
		{
			Database database = new Database(name, id);
			base.Add(database);
			this.InitializeNewDatabase(database);
			return database;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x0006E7A1 File Offset: 0x0006C9A1
		public Database Add(string name)
		{
			return this.Add(name, base.GetNewID(name));
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0006E7B1 File Offset: 0x0006C9B1
		public Database Add()
		{
			return this.Add(base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000DB5 RID: 3509 RVA: 0x0006E7C5 File Offset: 0x0006C9C5
		public void Insert(int index, Database item)
		{
			base.Insert(index, item);
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x0006E7D0 File Offset: 0x0006C9D0
		public Database Insert(int index, string name, string id)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("index", index, SR.Collections_IndexOutOfRangeException);
			}
			Database database = new Database(name, id);
			base.Insert(index, database);
			this.InitializeNewDatabase(database);
			return database;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x0006E818 File Offset: 0x0006CA18
		public Database Insert(int index, string name)
		{
			return this.Insert(index, name, base.GetNewID(name));
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0006E829 File Offset: 0x0006CA29
		public Database Insert(int index)
		{
			return this.Insert(index, base.GetNewName(), base.GetNewID());
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x0006E83E File Offset: 0x0006CA3E
		public void Remove(Database item)
		{
			base.Remove(item, true);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0006E848 File Offset: 0x0006CA48
		public void Remove(Database item, bool cleanUp)
		{
			base.Remove(item, cleanUp);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0006E852 File Offset: 0x0006CA52
		public void Remove(string id)
		{
			base.Remove(id, true);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0006E85C File Offset: 0x0006CA5C
		public new void Remove(string id, bool cleanUp)
		{
			base.Remove(id, cleanUp);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0006E866 File Offset: 0x0006CA66
		public new Database Move(int fromIndex, int toIndex)
		{
			return (Database)base.Move(fromIndex, toIndex);
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0006E875 File Offset: 0x0006CA75
		public new Database Move(string id, int toIndex)
		{
			return (Database)base.Move(id, toIndex);
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0006E884 File Offset: 0x0006CA84
		public void Move(Database item, int toIndex)
		{
			base.Move(item, toIndex);
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0006E88E File Offset: 0x0006CA8E
		public bool Contains(Database item)
		{
			return base.Contains(item);
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0006E897 File Offset: 0x0006CA97
		public new bool Contains(string id)
		{
			return base.Contains(id);
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0006E8A0 File Offset: 0x0006CAA0
		public int IndexOf(Database item)
		{
			return base.IndexOf(item);
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0006E8A9 File Offset: 0x0006CAA9
		public new int IndexOf(string id)
		{
			return base.IndexOf(id);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0006E8B2 File Offset: 0x0006CAB2
		internal override void OnItemAdding(IModelComponent item)
		{
			if (base.Parent != null)
			{
				((Server)base.Parent).NotifyDatabaseAdding((Database)item);
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0006E8D2 File Offset: 0x0006CAD2
		internal override void OnItemAdded(IModelComponent item)
		{
			if (base.Parent != null)
			{
				((Server)base.Parent).NotifyDatabaseAdded((Database)item);
			}
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0006E8F4 File Offset: 0x0006CAF4
		private void InitializeNewDatabase(Database item)
		{
			Server server = base.Parent as Server;
			if (server != null)
			{
				if (server.DefaultCompatibilityLevel > 0)
				{
					item.CompatibilityLevel = server.DefaultCompatibilityLevel;
				}
				if (server.ServerMode != ServerMode.Default)
				{
					switch (server.ServerMode)
					{
					case ServerMode.Multidimensional:
						item.StorageEngineUsed = StorageEngineUsed.Traditional;
						return;
					case ServerMode.SharePoint:
					case ServerMode.Tabular:
						item.StorageEngineUsed = StorageEngineUsed.InMemory;
						break;
					case ServerMode.Default:
						break;
					default:
						return;
					}
				}
			}
		}

		// Token: 0x0400019D RID: 413
		private Server server;
	}
}
