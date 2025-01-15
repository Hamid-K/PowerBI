using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001B0 RID: 432
	public class NameChanges
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x00023128 File Offset: 0x00021328
		public int Count
		{
			get
			{
				int num = 0;
				foreach (IList<NameChanges.NameChangeEntry> list in this.m_entries.Values)
				{
					num += list.Count;
				}
				return num;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00023188 File Offset: 0x00021388
		public void Add(NameChanges.EntryType type, string oldName, string newName)
		{
			if (string.IsNullOrEmpty(oldName) || string.IsNullOrEmpty(newName))
			{
				throw new ArgumentException();
			}
			if (string.Equals(oldName, newName, StringComparison.Ordinal))
			{
				return;
			}
			if (!this.m_entries.ContainsKey(type))
			{
				this.m_entries.Add(type, new List<NameChanges.NameChangeEntry>());
			}
			bool flag = false;
			foreach (NameChanges.NameChangeEntry nameChangeEntry in this.m_entries[type])
			{
				if (string.Equals(nameChangeEntry.OldName, oldName, StringComparison.Ordinal))
				{
					flag = true;
					nameChangeEntry.NewName = newName;
					break;
				}
			}
			if (!flag)
			{
				this.m_entries[type].Add(new NameChanges.NameChangeEntry(type, oldName, newName));
			}
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0002324C File Offset: 0x0002144C
		public void Add(INamedObject namedObj, string newName)
		{
			if (namedObj == null)
			{
				throw new ArgumentNullException("namedObj");
			}
			if (string.IsNullOrEmpty(newName))
			{
				throw new ArgumentException();
			}
			NameChanges.EntryType entryType;
			if (namedObj is ReportItem)
			{
				entryType = NameChanges.EntryType.ReportItem;
			}
			else if (namedObj is Group)
			{
				entryType = NameChanges.EntryType.Group;
			}
			else if (namedObj is DataSet)
			{
				entryType = NameChanges.EntryType.DataSet;
			}
			else if (namedObj is DataSource)
			{
				entryType = NameChanges.EntryType.DataSource;
			}
			else if (namedObj is ReportParameter)
			{
				entryType = NameChanges.EntryType.ReportParameter;
			}
			else
			{
				if (!(namedObj is EmbeddedImage))
				{
					return;
				}
				entryType = NameChanges.EntryType.EmbeddedImage;
			}
			this.Add(entryType, namedObj.Name, newName);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000232CC File Offset: 0x000214CC
		public bool HasNameChanged(NameChanges.EntryType type, string oldName)
		{
			if (oldName != null && this.m_entries.ContainsKey(type))
			{
				foreach (NameChanges.NameChangeEntry nameChangeEntry in this.m_entries[type])
				{
					if (string.Equals(oldName, nameChangeEntry.OldName, StringComparison.Ordinal))
					{
						return true;
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x00023340 File Offset: 0x00021540
		public string GetNewName(NameChanges.EntryType type, string oldName)
		{
			if (oldName != null && this.m_entries.ContainsKey(type))
			{
				foreach (NameChanges.NameChangeEntry nameChangeEntry in this.m_entries[type])
				{
					if (string.Equals(oldName, nameChangeEntry.OldName, StringComparison.Ordinal))
					{
						return nameChangeEntry.NewName;
					}
				}
				return oldName;
			}
			return oldName;
		}

		// Token: 0x0400053B RID: 1339
		private readonly Dictionary<NameChanges.EntryType, IList<NameChanges.NameChangeEntry>> m_entries = new Dictionary<NameChanges.EntryType, IList<NameChanges.NameChangeEntry>>();

		// Token: 0x020003DE RID: 990
		private class NameChangeEntry
		{
			// Token: 0x06001890 RID: 6288 RVA: 0x0003B913 File Offset: 0x00039B13
			public NameChangeEntry(NameChanges.EntryType type, string oldName, string newName)
			{
				if (oldName == null)
				{
					throw new ArgumentNullException("oldName");
				}
				if (newName == null)
				{
					throw new ArgumentNullException("newName");
				}
				this.m_type = type;
				this.m_oldName = oldName;
				this.m_newName = newName;
			}

			// Token: 0x17000748 RID: 1864
			// (get) Token: 0x06001891 RID: 6289 RVA: 0x0003B94C File Offset: 0x00039B4C
			public NameChanges.EntryType Type
			{
				get
				{
					return this.m_type;
				}
			}

			// Token: 0x17000749 RID: 1865
			// (get) Token: 0x06001892 RID: 6290 RVA: 0x0003B954 File Offset: 0x00039B54
			public string OldName
			{
				get
				{
					return this.m_oldName;
				}
			}

			// Token: 0x1700074A RID: 1866
			// (get) Token: 0x06001893 RID: 6291 RVA: 0x0003B95C File Offset: 0x00039B5C
			// (set) Token: 0x06001894 RID: 6292 RVA: 0x0003B964 File Offset: 0x00039B64
			public string NewName
			{
				get
				{
					return this.m_newName;
				}
				set
				{
					if (value != null && !string.Equals(this.m_newName, value, StringComparison.Ordinal))
					{
						this.m_newName = value;
					}
				}
			}

			// Token: 0x06001895 RID: 6293 RVA: 0x0003B980 File Offset: 0x00039B80
			public override bool Equals(object obj)
			{
				if (obj is NameChanges.NameChangeEntry)
				{
					NameChanges.NameChangeEntry nameChangeEntry = (NameChanges.NameChangeEntry)obj;
					return this.Type == nameChangeEntry.Type && string.Equals(this.OldName, nameChangeEntry.OldName, StringComparison.Ordinal) && string.Equals(this.NewName, nameChangeEntry.NewName, StringComparison.Ordinal);
				}
				return false;
			}

			// Token: 0x06001896 RID: 6294 RVA: 0x0003B9D4 File Offset: 0x00039BD4
			public override int GetHashCode()
			{
				return this.m_oldName.GetHashCode();
			}

			// Token: 0x04000793 RID: 1939
			private readonly NameChanges.EntryType m_type;

			// Token: 0x04000794 RID: 1940
			private readonly string m_oldName;

			// Token: 0x04000795 RID: 1941
			private string m_newName;
		}

		// Token: 0x020003DF RID: 991
		public enum EntryType
		{
			// Token: 0x04000797 RID: 1943
			ReportItem,
			// Token: 0x04000798 RID: 1944
			Group,
			// Token: 0x04000799 RID: 1945
			DataSet,
			// Token: 0x0400079A RID: 1946
			DataSource,
			// Token: 0x0400079B RID: 1947
			ReportParameter,
			// Token: 0x0400079C RID: 1948
			EmbeddedImage,
			// Token: 0x0400079D RID: 1949
			Scope
		}
	}
}
