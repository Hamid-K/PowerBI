using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x02000062 RID: 98
	public sealed class FolderPermission : ComplexProperty
	{
		// Token: 0x06000463 RID: 1123 RVA: 0x0000FFF0 File Offset: 0x0000EFF0
		private bool IsEqualTo(FolderPermission permission)
		{
			return this.CanCreateItems == permission.CanCreateItems && this.CanCreateSubFolders == permission.CanCreateSubFolders && this.IsFolderContact == permission.IsFolderContact && this.IsFolderVisible == permission.IsFolderVisible && this.IsFolderOwner == permission.IsFolderOwner && this.EditItems == permission.EditItems && this.DeleteItems == permission.DeleteItems && this.ReadItems == permission.ReadItems;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001006F File Offset: 0x0000F06F
		private FolderPermission Clone()
		{
			return (FolderPermission)base.MemberwiseClone();
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001007C File Offset: 0x0000F07C
		private void AdjustPermissionLevel()
		{
			foreach (KeyValuePair<FolderPermissionLevel, FolderPermission> keyValuePair in FolderPermission.defaultPermissions.Member)
			{
				if (this.IsEqualTo(keyValuePair.Value))
				{
					this.permissionLevel = keyValuePair.Key;
					return;
				}
			}
			this.permissionLevel = FolderPermissionLevel.Custom;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000100F4 File Offset: 0x0000F0F4
		private void AssignIndividualPermissions(FolderPermission permission)
		{
			this.canCreateItems = permission.CanCreateItems;
			this.canCreateSubFolders = permission.CanCreateSubFolders;
			this.isFolderContact = permission.IsFolderContact;
			this.isFolderOwner = permission.IsFolderOwner;
			this.isFolderVisible = permission.IsFolderVisible;
			this.editItems = permission.EditItems;
			this.deleteItems = permission.DeleteItems;
			this.readItems = permission.ReadItems;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00010161 File Offset: 0x0000F161
		public FolderPermission()
		{
			this.UserId = new UserId();
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00010174 File Offset: 0x0000F174
		public FolderPermission(UserId userId, FolderPermissionLevel permissionLevel)
		{
			EwsUtilities.ValidateParam(userId, "userId");
			this.userId = userId;
			this.PermissionLevel = permissionLevel;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00010195 File Offset: 0x0000F195
		public FolderPermission(string primarySmtpAddress, FolderPermissionLevel permissionLevel)
		{
			this.userId = new UserId(primarySmtpAddress);
			this.PermissionLevel = permissionLevel;
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x000101B0 File Offset: 0x0000F1B0
		public FolderPermission(StandardUser standardUser, FolderPermissionLevel permissionLevel)
		{
			this.userId = new UserId(standardUser);
			this.PermissionLevel = permissionLevel;
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x000101CC File Offset: 0x0000F1CC
		internal void Validate(bool isCalendarFolder, int permissionIndex)
		{
			if (!this.UserId.IsValid())
			{
				throw new ServiceValidationException(string.Format(Strings.FolderPermissionHasInvalidUserId, permissionIndex));
			}
			if (!isCalendarFolder)
			{
				if (this.readItems == FolderPermissionReadAccess.TimeAndSubjectAndLocation || this.readItems == FolderPermissionReadAccess.TimeOnly)
				{
					throw new ServiceLocalException(string.Format(Strings.ReadAccessInvalidForNonCalendarFolder, this.readItems));
				}
				if (this.permissionLevel == FolderPermissionLevel.FreeBusyTimeAndSubjectAndLocation || this.permissionLevel == FolderPermissionLevel.FreeBusyTimeOnly)
				{
					throw new ServiceLocalException(string.Format(Strings.PermissionLevelInvalidForNonCalendarFolder, this.permissionLevel));
				}
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001026A File Offset: 0x0000F26A
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x00010274 File Offset: 0x0000F274
		public UserId UserId
		{
			get
			{
				return this.userId;
			}
			set
			{
				if (this.userId != null)
				{
					this.userId.OnChange -= this.PropertyChanged;
				}
				this.SetFieldValue<UserId>(ref this.userId, value);
				if (this.userId != null)
				{
					this.userId.OnChange += this.PropertyChanged;
				}
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x000102CC File Offset: 0x0000F2CC
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x000102D4 File Offset: 0x0000F2D4
		public bool CanCreateItems
		{
			get
			{
				return this.canCreateItems;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.canCreateItems, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000102E9 File Offset: 0x0000F2E9
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x000102F1 File Offset: 0x0000F2F1
		public bool CanCreateSubFolders
		{
			get
			{
				return this.canCreateSubFolders;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.canCreateSubFolders, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x00010306 File Offset: 0x0000F306
		// (set) Token: 0x06000473 RID: 1139 RVA: 0x0001030E File Offset: 0x0000F30E
		public bool IsFolderOwner
		{
			get
			{
				return this.isFolderOwner;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isFolderOwner, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00010323 File Offset: 0x0000F323
		// (set) Token: 0x06000475 RID: 1141 RVA: 0x0001032B File Offset: 0x0000F32B
		public bool IsFolderVisible
		{
			get
			{
				return this.isFolderVisible;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isFolderVisible, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x00010340 File Offset: 0x0000F340
		// (set) Token: 0x06000477 RID: 1143 RVA: 0x00010348 File Offset: 0x0000F348
		public bool IsFolderContact
		{
			get
			{
				return this.isFolderContact;
			}
			set
			{
				this.SetFieldValue<bool>(ref this.isFolderContact, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0001035D File Offset: 0x0000F35D
		// (set) Token: 0x06000479 RID: 1145 RVA: 0x00010365 File Offset: 0x0000F365
		public PermissionScope EditItems
		{
			get
			{
				return this.editItems;
			}
			set
			{
				this.SetFieldValue<PermissionScope>(ref this.editItems, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0001037A File Offset: 0x0000F37A
		// (set) Token: 0x0600047B RID: 1147 RVA: 0x00010382 File Offset: 0x0000F382
		public PermissionScope DeleteItems
		{
			get
			{
				return this.deleteItems;
			}
			set
			{
				this.SetFieldValue<PermissionScope>(ref this.deleteItems, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00010397 File Offset: 0x0000F397
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001039F File Offset: 0x0000F39F
		public FolderPermissionReadAccess ReadItems
		{
			get
			{
				return this.readItems;
			}
			set
			{
				this.SetFieldValue<FolderPermissionReadAccess>(ref this.readItems, value);
				this.AdjustPermissionLevel();
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x000103B4 File Offset: 0x0000F3B4
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x000103BC File Offset: 0x0000F3BC
		public FolderPermissionLevel PermissionLevel
		{
			get
			{
				return this.permissionLevel;
			}
			set
			{
				if (this.permissionLevel != value)
				{
					if (value == FolderPermissionLevel.Custom)
					{
						throw new ServiceLocalException(Strings.CannotSetPermissionLevelToCustom);
					}
					this.AssignIndividualPermissions(FolderPermission.defaultPermissions.Member[value]);
					this.SetFieldValue<FolderPermissionLevel>(ref this.permissionLevel, value);
				}
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001040C File Offset: 0x0000F40C
		public FolderPermissionLevel DisplayPermissionLevel
		{
			get
			{
				if (this.permissionLevel == FolderPermissionLevel.Custom)
				{
					foreach (FolderPermission folderPermission in FolderPermission.levelVariants.Member)
					{
						if (this.IsEqualTo(folderPermission))
						{
							return folderPermission.PermissionLevel;
						}
					}
				}
				return this.permissionLevel;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x00010480 File Offset: 0x0000F480
		private void PropertyChanged(ComplexProperty complexProperty)
		{
			this.Changed();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00010488 File Offset: 0x0000F488
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000428-1 == null)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
					dictionary.Add("UserId", 0);
					dictionary.Add("CanCreateItems", 1);
					dictionary.Add("CanCreateSubFolders", 2);
					dictionary.Add("IsFolderOwner", 3);
					dictionary.Add("IsFolderVisible", 4);
					dictionary.Add("IsFolderContact", 5);
					dictionary.Add("EditItems", 6);
					dictionary.Add("DeleteItems", 7);
					dictionary.Add("ReadItems", 8);
					dictionary.Add("PermissionLevel", 9);
					dictionary.Add("CalendarPermissionLevel", 10);
					<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000428-1 = dictionary;
				}
				int num;
				if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000428-1.TryGetValue(localName, ref num))
				{
					switch (num)
					{
					case 0:
						this.UserId = new UserId();
						this.UserId.LoadFromXml(reader, reader.LocalName);
						return true;
					case 1:
						this.canCreateItems = reader.ReadValue<bool>();
						return true;
					case 2:
						this.canCreateSubFolders = reader.ReadValue<bool>();
						return true;
					case 3:
						this.isFolderOwner = reader.ReadValue<bool>();
						return true;
					case 4:
						this.isFolderVisible = reader.ReadValue<bool>();
						return true;
					case 5:
						this.isFolderContact = reader.ReadValue<bool>();
						return true;
					case 6:
						this.editItems = reader.ReadValue<PermissionScope>();
						return true;
					case 7:
						this.deleteItems = reader.ReadValue<PermissionScope>();
						return true;
					case 8:
						this.readItems = reader.ReadValue<FolderPermissionReadAccess>();
						return true;
					case 9:
					case 10:
						this.permissionLevel = reader.ReadValue<FolderPermissionLevel>();
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001062B File Offset: 0x0000F62B
		internal override void LoadFromXml(EwsServiceXmlReader reader, XmlNamespace xmlNamespace, string xmlElementName)
		{
			base.LoadFromXml(reader, xmlNamespace, xmlElementName);
			this.AdjustPermissionLevel();
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001063C File Offset: 0x0000F63C
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			foreach (string text in jsonProperty.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600042a-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(11);
						dictionary.Add("UserId", 0);
						dictionary.Add("CanCreateItems", 1);
						dictionary.Add("CanCreateSubFolders", 2);
						dictionary.Add("IsFolderOwner", 3);
						dictionary.Add("IsFolderVisible", 4);
						dictionary.Add("IsFolderContact", 5);
						dictionary.Add("EditItems", 6);
						dictionary.Add("DeleteItems", 7);
						dictionary.Add("ReadItems", 8);
						dictionary.Add("PermissionLevel", 9);
						dictionary.Add("CalendarPermissionLevel", 10);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600042a-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x600042a-1.TryGetValue(text2, ref num))
					{
						switch (num)
						{
						case 0:
							this.UserId = new UserId();
							this.UserId.LoadFromJson(jsonProperty.ReadAsJsonObject(text), service);
							break;
						case 1:
							this.canCreateItems = jsonProperty.ReadAsBool(text);
							break;
						case 2:
							this.canCreateSubFolders = jsonProperty.ReadAsBool(text);
							break;
						case 3:
							this.isFolderOwner = jsonProperty.ReadAsBool(text);
							break;
						case 4:
							this.isFolderVisible = jsonProperty.ReadAsBool(text);
							break;
						case 5:
							this.isFolderContact = jsonProperty.ReadAsBool(text);
							break;
						case 6:
							this.editItems = jsonProperty.ReadEnumValue<PermissionScope>(text);
							break;
						case 7:
							this.deleteItems = jsonProperty.ReadEnumValue<PermissionScope>(text);
							break;
						case 8:
							this.readItems = jsonProperty.ReadEnumValue<FolderPermissionReadAccess>(text);
							break;
						case 9:
						case 10:
							this.permissionLevel = jsonProperty.ReadEnumValue<FolderPermissionLevel>(text);
							break;
						}
					}
				}
			}
			this.AdjustPermissionLevel();
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001083C File Offset: 0x0000F83C
		internal void WriteElementsToXml(EwsServiceXmlWriter writer, bool isCalendarFolder)
		{
			if (this.UserId != null)
			{
				this.UserId.WriteToXml(writer, "UserId");
			}
			if (this.PermissionLevel == FolderPermissionLevel.Custom)
			{
				writer.WriteElementValue(XmlNamespace.Types, "CanCreateItems", this.CanCreateItems);
				writer.WriteElementValue(XmlNamespace.Types, "CanCreateSubFolders", this.CanCreateSubFolders);
				writer.WriteElementValue(XmlNamespace.Types, "IsFolderOwner", this.IsFolderOwner);
				writer.WriteElementValue(XmlNamespace.Types, "IsFolderVisible", this.IsFolderVisible);
				writer.WriteElementValue(XmlNamespace.Types, "IsFolderContact", this.IsFolderContact);
				writer.WriteElementValue(XmlNamespace.Types, "EditItems", this.EditItems);
				writer.WriteElementValue(XmlNamespace.Types, "DeleteItems", this.DeleteItems);
				writer.WriteElementValue(XmlNamespace.Types, "ReadItems", this.ReadItems);
			}
			writer.WriteElementValue(XmlNamespace.Types, isCalendarFolder ? "CalendarPermissionLevel" : "PermissionLevel", this.PermissionLevel);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00010948 File Offset: 0x0000F948
		internal void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName, bool isCalendarFolder)
		{
			writer.WriteStartElement(base.Namespace, xmlElementName);
			this.WriteAttributesToXml(writer);
			this.WriteElementsToXml(writer, isCalendarFolder);
			writer.WriteEndElement();
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001096C File Offset: 0x0000F96C
		internal object InternalToJson(ExchangeService service, bool isCalendarFolder)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("UserId", this.UserId.InternalToJson(service));
			if (this.PermissionLevel == FolderPermissionLevel.Custom)
			{
				jsonObject.Add("CanCreateItems", this.CanCreateItems);
				jsonObject.Add("CanCreateSubFolders", this.CanCreateSubFolders);
				jsonObject.Add("IsFolderOwner", this.IsFolderOwner);
				jsonObject.Add("IsFolderVisible", this.IsFolderVisible);
				jsonObject.Add("IsFolderContact", this.IsFolderContact);
				jsonObject.Add("EditItems", this.EditItems);
				jsonObject.Add("DeleteItems", this.DeleteItems);
				jsonObject.Add("ReadItems", this.ReadItems);
			}
			jsonObject.Add(isCalendarFolder ? "CalendarPermissionLevel" : "PermissionLevel", this.PermissionLevel);
			jsonObject.AddTypeParameter(isCalendarFolder ? "CalendarPermission" : "Permission");
			return jsonObject;
		}

		// Token: 0x0400019B RID: 411
		private static LazyMember<Dictionary<FolderPermissionLevel, FolderPermission>> defaultPermissions = new LazyMember<Dictionary<FolderPermissionLevel, FolderPermission>>(delegate
		{
			Dictionary<FolderPermissionLevel, FolderPermission> dictionary = new Dictionary<FolderPermissionLevel, FolderPermission>();
			dictionary.Add(FolderPermissionLevel.None, new FolderPermission
			{
				canCreateItems = false,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.None,
				editItems = PermissionScope.None,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = false,
				readItems = FolderPermissionReadAccess.None
			});
			dictionary.Add(FolderPermissionLevel.Contributor, new FolderPermission
			{
				canCreateItems = true,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.None,
				editItems = PermissionScope.None,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.None
			});
			dictionary.Add(FolderPermissionLevel.Reviewer, new FolderPermission
			{
				canCreateItems = false,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.None,
				editItems = PermissionScope.None,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.FullDetails
			});
			dictionary.Add(FolderPermissionLevel.NoneditingAuthor, new FolderPermission
			{
				canCreateItems = true,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.Owned,
				editItems = PermissionScope.None,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.FullDetails
			});
			dictionary.Add(FolderPermissionLevel.Author, new FolderPermission
			{
				canCreateItems = true,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.Owned,
				editItems = PermissionScope.Owned,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.FullDetails
			});
			dictionary.Add(FolderPermissionLevel.PublishingAuthor, new FolderPermission
			{
				canCreateItems = true,
				canCreateSubFolders = true,
				deleteItems = PermissionScope.Owned,
				editItems = PermissionScope.Owned,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.FullDetails
			});
			dictionary.Add(FolderPermissionLevel.Editor, new FolderPermission
			{
				canCreateItems = true,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.All,
				editItems = PermissionScope.All,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.FullDetails
			});
			dictionary.Add(FolderPermissionLevel.PublishingEditor, new FolderPermission
			{
				canCreateItems = true,
				canCreateSubFolders = true,
				deleteItems = PermissionScope.All,
				editItems = PermissionScope.All,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.FullDetails
			});
			dictionary.Add(FolderPermissionLevel.Owner, new FolderPermission
			{
				canCreateItems = true,
				canCreateSubFolders = true,
				deleteItems = PermissionScope.All,
				editItems = PermissionScope.All,
				isFolderContact = true,
				isFolderOwner = true,
				isFolderVisible = true,
				readItems = FolderPermissionReadAccess.FullDetails
			});
			dictionary.Add(FolderPermissionLevel.FreeBusyTimeOnly, new FolderPermission
			{
				canCreateItems = false,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.None,
				editItems = PermissionScope.None,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = false,
				readItems = FolderPermissionReadAccess.TimeOnly
			});
			dictionary.Add(FolderPermissionLevel.FreeBusyTimeAndSubjectAndLocation, new FolderPermission
			{
				canCreateItems = false,
				canCreateSubFolders = false,
				deleteItems = PermissionScope.None,
				editItems = PermissionScope.None,
				isFolderContact = false,
				isFolderOwner = false,
				isFolderVisible = false,
				readItems = FolderPermissionReadAccess.TimeAndSubjectAndLocation
			});
			return dictionary;
		});

		// Token: 0x0400019C RID: 412
		private static LazyMember<List<FolderPermission>> levelVariants = new LazyMember<List<FolderPermission>>(delegate
		{
			List<FolderPermission> list = new List<FolderPermission>();
			FolderPermission folderPermission = FolderPermission.defaultPermissions.Member[FolderPermissionLevel.None];
			FolderPermission folderPermission2 = FolderPermission.defaultPermissions.Member[FolderPermissionLevel.Owner];
			FolderPermission folderPermission3 = folderPermission.Clone();
			folderPermission3.isFolderVisible = true;
			list.Add(folderPermission3);
			folderPermission3 = folderPermission.Clone();
			folderPermission3.isFolderContact = true;
			list.Add(folderPermission3);
			folderPermission3 = folderPermission.Clone();
			folderPermission3.isFolderContact = true;
			folderPermission3.isFolderVisible = true;
			list.Add(folderPermission3);
			folderPermission3 = folderPermission2.Clone();
			folderPermission3.isFolderContact = false;
			list.Add(folderPermission3);
			return list;
		});

		// Token: 0x0400019D RID: 413
		private UserId userId;

		// Token: 0x0400019E RID: 414
		private bool canCreateItems;

		// Token: 0x0400019F RID: 415
		private bool canCreateSubFolders;

		// Token: 0x040001A0 RID: 416
		private bool isFolderOwner;

		// Token: 0x040001A1 RID: 417
		private bool isFolderVisible;

		// Token: 0x040001A2 RID: 418
		private bool isFolderContact;

		// Token: 0x040001A3 RID: 419
		private PermissionScope editItems;

		// Token: 0x040001A4 RID: 420
		private PermissionScope deleteItems;

		// Token: 0x040001A5 RID: 421
		private FolderPermissionReadAccess readItems;

		// Token: 0x040001A6 RID: 422
		private FolderPermissionLevel permissionLevel;
	}
}
