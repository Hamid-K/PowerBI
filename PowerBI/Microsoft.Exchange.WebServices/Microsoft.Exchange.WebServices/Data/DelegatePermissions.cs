using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200004C RID: 76
	public sealed class DelegatePermissions : ComplexProperty
	{
		// Token: 0x0600035D RID: 861 RVA: 0x0000CAC4 File Offset: 0x0000BAC4
		internal DelegatePermissions()
		{
			Dictionary<string, DelegatePermissions.DelegateFolderPermission> dictionary = new Dictionary<string, DelegatePermissions.DelegateFolderPermission>();
			dictionary.Add("CalendarFolderPermissionLevel", new DelegatePermissions.DelegateFolderPermission());
			dictionary.Add("TasksFolderPermissionLevel", new DelegatePermissions.DelegateFolderPermission());
			dictionary.Add("InboxFolderPermissionLevel", new DelegatePermissions.DelegateFolderPermission());
			dictionary.Add("ContactsFolderPermissionLevel", new DelegatePermissions.DelegateFolderPermission());
			dictionary.Add("NotesFolderPermissionLevel", new DelegatePermissions.DelegateFolderPermission());
			dictionary.Add("JournalFolderPermissionLevel", new DelegatePermissions.DelegateFolderPermission());
			this.delegateFolderPermissions = dictionary;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x0600035E RID: 862 RVA: 0x0000CB44 File Offset: 0x0000BB44
		// (set) Token: 0x0600035F RID: 863 RVA: 0x0000CB5B File Offset: 0x0000BB5B
		public DelegateFolderPermissionLevel CalendarFolderPermissionLevel
		{
			get
			{
				return this.delegateFolderPermissions["CalendarFolderPermissionLevel"].PermissionLevel;
			}
			set
			{
				this.delegateFolderPermissions["CalendarFolderPermissionLevel"].PermissionLevel = value;
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000360 RID: 864 RVA: 0x0000CB73 File Offset: 0x0000BB73
		// (set) Token: 0x06000361 RID: 865 RVA: 0x0000CB8A File Offset: 0x0000BB8A
		public DelegateFolderPermissionLevel TasksFolderPermissionLevel
		{
			get
			{
				return this.delegateFolderPermissions["TasksFolderPermissionLevel"].PermissionLevel;
			}
			set
			{
				this.delegateFolderPermissions["TasksFolderPermissionLevel"].PermissionLevel = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000362 RID: 866 RVA: 0x0000CBA2 File Offset: 0x0000BBA2
		// (set) Token: 0x06000363 RID: 867 RVA: 0x0000CBB9 File Offset: 0x0000BBB9
		public DelegateFolderPermissionLevel InboxFolderPermissionLevel
		{
			get
			{
				return this.delegateFolderPermissions["InboxFolderPermissionLevel"].PermissionLevel;
			}
			set
			{
				this.delegateFolderPermissions["InboxFolderPermissionLevel"].PermissionLevel = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000364 RID: 868 RVA: 0x0000CBD1 File Offset: 0x0000BBD1
		// (set) Token: 0x06000365 RID: 869 RVA: 0x0000CBE8 File Offset: 0x0000BBE8
		public DelegateFolderPermissionLevel ContactsFolderPermissionLevel
		{
			get
			{
				return this.delegateFolderPermissions["ContactsFolderPermissionLevel"].PermissionLevel;
			}
			set
			{
				this.delegateFolderPermissions["ContactsFolderPermissionLevel"].PermissionLevel = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0000CC00 File Offset: 0x0000BC00
		// (set) Token: 0x06000367 RID: 871 RVA: 0x0000CC17 File Offset: 0x0000BC17
		public DelegateFolderPermissionLevel NotesFolderPermissionLevel
		{
			get
			{
				return this.delegateFolderPermissions["NotesFolderPermissionLevel"].PermissionLevel;
			}
			set
			{
				this.delegateFolderPermissions["NotesFolderPermissionLevel"].PermissionLevel = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0000CC2F File Offset: 0x0000BC2F
		// (set) Token: 0x06000369 RID: 873 RVA: 0x0000CC46 File Offset: 0x0000BC46
		public DelegateFolderPermissionLevel JournalFolderPermissionLevel
		{
			get
			{
				return this.delegateFolderPermissions["JournalFolderPermissionLevel"].PermissionLevel;
			}
			set
			{
				this.delegateFolderPermissions["JournalFolderPermissionLevel"].PermissionLevel = value;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x0000CC60 File Offset: 0x0000BC60
		internal void Reset()
		{
			foreach (DelegatePermissions.DelegateFolderPermission delegateFolderPermission in this.delegateFolderPermissions.Values)
			{
				delegateFolderPermission.Reset();
			}
		}

		// Token: 0x0600036B RID: 875 RVA: 0x0000CCB8 File Offset: 0x0000BCB8
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			DelegatePermissions.DelegateFolderPermission delegateFolderPermission = null;
			if (this.delegateFolderPermissions.TryGetValue(reader.LocalName, ref delegateFolderPermission))
			{
				delegateFolderPermission.Initialize(reader.ReadElementValue<DelegateFolderPermissionLevel>());
			}
			return delegateFolderPermission != null;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0000CCF0 File Offset: 0x0000BCF0
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				DelegatePermissions.DelegateFolderPermission delegateFolderPermission = null;
				if (this.delegateFolderPermissions.TryGetValue(text, ref delegateFolderPermission))
				{
					delegateFolderPermission.Initialize(jsonProperty.ReadEnumValue<DelegateFolderPermissionLevel>(text));
				}
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0000CD5C File Offset: 0x0000BD5C
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			this.WritePermissionToXml(writer, "CalendarFolderPermissionLevel");
			this.WritePermissionToXml(writer, "TasksFolderPermissionLevel");
			this.WritePermissionToXml(writer, "InboxFolderPermissionLevel");
			this.WritePermissionToXml(writer, "ContactsFolderPermissionLevel");
			this.WritePermissionToXml(writer, "NotesFolderPermissionLevel");
			this.WritePermissionToXml(writer, "JournalFolderPermissionLevel");
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0000CDB4 File Offset: 0x0000BDB4
		internal override object InternalToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			this.WritePermissionToJson(jsonObject, "CalendarFolderPermissionLevel");
			this.WritePermissionToJson(jsonObject, "TasksFolderPermissionLevel");
			this.WritePermissionToJson(jsonObject, "InboxFolderPermissionLevel");
			this.WritePermissionToJson(jsonObject, "ContactsFolderPermissionLevel");
			this.WritePermissionToJson(jsonObject, "NotesFolderPermissionLevel");
			this.WritePermissionToJson(jsonObject, "JournalFolderPermissionLevel");
			return jsonObject;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0000CE10 File Offset: 0x0000BE10
		private void WritePermissionToJson(JsonObject jsonProperty, string elementName)
		{
			DelegateFolderPermissionLevel permissionLevel = this.delegateFolderPermissions[elementName].PermissionLevel;
			if (permissionLevel != DelegateFolderPermissionLevel.Custom)
			{
				jsonProperty.Add(elementName, permissionLevel);
			}
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0000CE40 File Offset: 0x0000BE40
		private void WritePermissionToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			DelegateFolderPermissionLevel permissionLevel = this.delegateFolderPermissions[xmlElementName].PermissionLevel;
			if (permissionLevel != DelegateFolderPermissionLevel.Custom)
			{
				writer.WriteElementValue(XmlNamespace.Types, xmlElementName, permissionLevel);
			}
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0000CE82 File Offset: 0x0000BE82
		internal void ValidateAddDelegate()
		{
			if (Enumerable.Any<KeyValuePair<string, DelegatePermissions.DelegateFolderPermission>>(this.delegateFolderPermissions, (KeyValuePair<string, DelegatePermissions.DelegateFolderPermission> kvp) => kvp.Value.PermissionLevel == DelegateFolderPermissionLevel.Custom))
			{
				throw new ServiceValidationException(Strings.CannotSetDelegateFolderPermissionLevelToCustom);
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0000CEE0 File Offset: 0x0000BEE0
		internal void ValidateUpdateDelegate()
		{
			if (Enumerable.Any<KeyValuePair<string, DelegatePermissions.DelegateFolderPermission>>(this.delegateFolderPermissions, (KeyValuePair<string, DelegatePermissions.DelegateFolderPermission> kvp) => kvp.Value.PermissionLevel == DelegateFolderPermissionLevel.Custom && !kvp.Value.IsExistingPermissionLevelCustom))
			{
				throw new ServiceValidationException(Strings.CannotSetDelegateFolderPermissionLevelToCustom);
			}
		}

		// Token: 0x04000170 RID: 368
		private Dictionary<string, DelegatePermissions.DelegateFolderPermission> delegateFolderPermissions;

		// Token: 0x0200004D RID: 77
		private class DelegateFolderPermission
		{
			// Token: 0x06000375 RID: 885 RVA: 0x0000CF1C File Offset: 0x0000BF1C
			internal void Initialize(DelegateFolderPermissionLevel permissionLevel)
			{
				this.PermissionLevel = permissionLevel;
				this.IsExistingPermissionLevelCustom = permissionLevel == DelegateFolderPermissionLevel.Custom;
			}

			// Token: 0x06000376 RID: 886 RVA: 0x0000CF2F File Offset: 0x0000BF2F
			internal void Reset()
			{
				this.Initialize(DelegateFolderPermissionLevel.None);
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x06000377 RID: 887 RVA: 0x0000CF38 File Offset: 0x0000BF38
			// (set) Token: 0x06000378 RID: 888 RVA: 0x0000CF40 File Offset: 0x0000BF40
			internal DelegateFolderPermissionLevel PermissionLevel { get; set; }

			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x06000379 RID: 889 RVA: 0x0000CF49 File Offset: 0x0000BF49
			// (set) Token: 0x0600037A RID: 890 RVA: 0x0000CF51 File Offset: 0x0000BF51
			internal bool IsExistingPermissionLevelCustom { get; private set; }
		}
	}
}
