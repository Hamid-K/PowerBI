using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A7 RID: 679
	public class UserConfiguration : IJsonSerializable
	{
		// Token: 0x06001800 RID: 6144 RVA: 0x000416C4 File Offset: 0x000406C4
		public UserConfiguration(ExchangeService service)
			: this(service, UserConfigurationProperties.Dictionary | UserConfigurationProperties.XmlData | UserConfigurationProperties.BinaryData)
		{
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000416D0 File Offset: 0x000406D0
		private static void WriteByteArrayToXml(EwsServiceXmlWriter writer, byte[] byteArray, string xmlElementName)
		{
			EwsUtilities.Assert(writer != null, "UserConfiguration.WriteByteArrayToXml", "writer is null");
			EwsUtilities.Assert(xmlElementName != null, "UserConfiguration.WriteByteArrayToXml", "xmlElementName is null");
			writer.WriteStartElement(XmlNamespace.Types, xmlElementName);
			if (byteArray != null && byteArray.Length > 0)
			{
				writer.WriteValue(Convert.ToBase64String(byteArray), xmlElementName);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x00041730 File Offset: 0x00040730
		internal static void WriteUserConfigurationNameToXml(EwsServiceXmlWriter writer, XmlNamespace xmlNamespace, string name, FolderId parentFolderId)
		{
			EwsUtilities.Assert(writer != null, "UserConfiguration.WriteUserConfigurationNameToXml", "writer is null");
			EwsUtilities.Assert(name != null, "UserConfiguration.WriteUserConfigurationNameToXml", "name is null");
			EwsUtilities.Assert(parentFolderId != null, "UserConfiguration.WriteUserConfigurationNameToXml", "parentFolderId is null");
			writer.WriteStartElement(xmlNamespace, "UserConfigurationName");
			writer.WriteAttributeValue("Name", name);
			parentFolderId.WriteToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000417A4 File Offset: 0x000407A4
		internal UserConfiguration(ExchangeService service, UserConfigurationProperties requestedProperties)
		{
			EwsUtilities.ValidateParam(service, "service");
			if (service.RequestedServerVersion < ExchangeVersion.Exchange2010)
			{
				throw new ServiceVersionException(string.Format(Strings.ObjectTypeIncompatibleWithRequestVersion, base.GetType().Name, ExchangeVersion.Exchange2010));
			}
			this.service = service;
			this.isNew = true;
			this.InitializeProperties(requestedProperties);
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00041806 File Offset: 0x00040806
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x0004180E File Offset: 0x0004080E
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00041817 File Offset: 0x00040817
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x0004181F File Offset: 0x0004081F
		public FolderId ParentFolderId
		{
			get
			{
				return this.parentFolderId;
			}
			internal set
			{
				this.parentFolderId = value;
			}
		}

		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x06001808 RID: 6152 RVA: 0x00041828 File Offset: 0x00040828
		public ItemId ItemId
		{
			get
			{
				return this.itemId;
			}
		}

		// Token: 0x170005DC RID: 1500
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x00041830 File Offset: 0x00040830
		public UserConfigurationDictionary Dictionary
		{
			get
			{
				return this.dictionary;
			}
		}

		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x0600180A RID: 6154 RVA: 0x00041838 File Offset: 0x00040838
		// (set) Token: 0x0600180B RID: 6155 RVA: 0x00041847 File Offset: 0x00040847
		public byte[] XmlData
		{
			get
			{
				this.ValidatePropertyAccess(UserConfigurationProperties.XmlData);
				return this.xmlData;
			}
			set
			{
				this.xmlData = value;
				this.MarkPropertyForUpdate(UserConfigurationProperties.XmlData);
			}
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x0600180C RID: 6156 RVA: 0x00041857 File Offset: 0x00040857
		// (set) Token: 0x0600180D RID: 6157 RVA: 0x00041866 File Offset: 0x00040866
		public byte[] BinaryData
		{
			get
			{
				this.ValidatePropertyAccess(UserConfigurationProperties.BinaryData);
				return this.binaryData;
			}
			set
			{
				this.binaryData = value;
				this.MarkPropertyForUpdate(UserConfigurationProperties.BinaryData);
			}
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600180E RID: 6158 RVA: 0x00041876 File Offset: 0x00040876
		public bool IsDirty
		{
			get
			{
				return this.updatedProperties != (UserConfigurationProperties)0 || this.dictionary.IsDirty;
			}
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00041890 File Offset: 0x00040890
		public static UserConfiguration Bind(ExchangeService service, string name, FolderId parentFolderId, UserConfigurationProperties properties)
		{
			UserConfiguration userConfiguration = service.GetUserConfiguration(name, parentFolderId, properties);
			userConfiguration.isNew = false;
			return userConfiguration;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x000418AF File Offset: 0x000408AF
		public static UserConfiguration Bind(ExchangeService service, string name, WellKnownFolderName parentFolderName, UserConfigurationProperties properties)
		{
			return UserConfiguration.Bind(service, name, new FolderId(parentFolderName), properties);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x000418C0 File Offset: 0x000408C0
		public void Save(string name, FolderId parentFolderId)
		{
			EwsUtilities.ValidateParam(name, "name");
			EwsUtilities.ValidateParam(parentFolderId, "parentFolderId");
			parentFolderId.Validate(this.service.RequestedServerVersion);
			if (!this.isNew)
			{
				throw new InvalidOperationException(Strings.CannotSaveNotNewUserConfiguration);
			}
			this.parentFolderId = parentFolderId;
			this.name = name;
			this.service.CreateUserConfiguration(this);
			this.isNew = false;
			this.ResetIsDirty();
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00041933 File Offset: 0x00040933
		public void Save(string name, WellKnownFolderName parentFolderName)
		{
			this.Save(name, new FolderId(parentFolderName));
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00041944 File Offset: 0x00040944
		public void Update()
		{
			if (this.isNew)
			{
				throw new InvalidOperationException(Strings.CannotUpdateNewUserConfiguration);
			}
			if (this.IsPropertyUpdated(UserConfigurationProperties.BinaryData) || this.IsPropertyUpdated(UserConfigurationProperties.Dictionary) || this.IsPropertyUpdated(UserConfigurationProperties.XmlData))
			{
				this.service.UpdateUserConfiguration(this);
			}
			this.ResetIsDirty();
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00041996 File Offset: 0x00040996
		public void Delete()
		{
			if (this.isNew)
			{
				throw new InvalidOperationException(Strings.DeleteInvalidForUnsavedUserConfiguration);
			}
			this.service.DeleteUserConfiguration(this.name, this.parentFolderId);
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x000419C7 File Offset: 0x000409C7
		public void Load(UserConfigurationProperties properties)
		{
			this.InitializeProperties(properties);
			this.service.LoadPropertiesForUserConfiguration(this, properties);
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x000419E0 File Offset: 0x000409E0
		internal void WriteToXml(EwsServiceXmlWriter writer, XmlNamespace xmlNamespace, string xmlElementName)
		{
			EwsUtilities.Assert(writer != null, "UserConfiguration.WriteToXml", "writer is null");
			EwsUtilities.Assert(xmlElementName != null, "UserConfiguration.WriteToXml", "xmlElementName is null");
			writer.WriteStartElement(xmlNamespace, xmlElementName);
			UserConfiguration.WriteUserConfigurationNameToXml(writer, XmlNamespace.Types, this.name, this.parentFolderId);
			if (this.IsPropertyUpdated(UserConfigurationProperties.Dictionary))
			{
				this.dictionary.WriteToXml(writer, "Dictionary");
			}
			if (this.IsPropertyUpdated(UserConfigurationProperties.XmlData))
			{
				this.WriteXmlDataToXml(writer);
			}
			if (this.IsPropertyUpdated(UserConfigurationProperties.BinaryData))
			{
				this.WriteBinaryDataToXml(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00041A74 File Offset: 0x00040A74
		object IJsonSerializable.ToJson(ExchangeService service)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("UserConfigurationName", this.GetJsonUserConfigName(service));
			jsonObject.Add("ItemId", this.itemId);
			if (this.IsPropertyUpdated(UserConfigurationProperties.Dictionary))
			{
				jsonObject.Add("Dictionary", ((IJsonSerializable)this.dictionary).ToJson(service));
			}
			if (this.IsPropertyUpdated(UserConfigurationProperties.XmlData))
			{
				jsonObject.Add("XmlData", this.GetBase64PropertyValue(this.XmlData));
			}
			if (this.IsPropertyUpdated(UserConfigurationProperties.BinaryData))
			{
				jsonObject.Add("BinaryData", this.GetBase64PropertyValue(this.BinaryData));
			}
			return jsonObject;
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00041B0C File Offset: 0x00040B0C
		private JsonObject GetJsonUserConfigName(ExchangeService service)
		{
			FolderId folderId = this.parentFolderId;
			string text = this.name;
			return UserConfiguration.GetJsonUserConfigName(service, folderId, text);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00041B30 File Offset: 0x00040B30
		internal static JsonObject GetJsonUserConfigName(ExchangeService service, FolderId parentFolderId, string name)
		{
			JsonObject jsonObject = new JsonObject();
			jsonObject.Add("BaseFolderId", parentFolderId.InternalToJson(service));
			jsonObject.Add("Name", name);
			return jsonObject;
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00041B62 File Offset: 0x00040B62
		private string GetBase64PropertyValue(byte[] bytes)
		{
			if (bytes == null || bytes.Length == 0)
			{
				return string.Empty;
			}
			return Convert.ToBase64String(bytes);
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00041B78 File Offset: 0x00040B78
		private bool IsPropertyUpdated(UserConfigurationProperties property)
		{
			bool flag = false;
			bool flag2 = false;
			switch (property)
			{
			case UserConfigurationProperties.Dictionary:
				flag = this.Dictionary.IsDirty;
				flag2 = this.Dictionary.Count == 0;
				goto IL_00A7;
			case UserConfigurationProperties.Id | UserConfigurationProperties.Dictionary:
				break;
			case UserConfigurationProperties.XmlData:
				flag = (property & this.updatedProperties) == property;
				flag2 = this.xmlData == null || this.xmlData.Length == 0;
				goto IL_00A7;
			default:
				if (property == UserConfigurationProperties.BinaryData)
				{
					flag = (property & this.updatedProperties) == property;
					flag2 = this.binaryData == null || this.binaryData.Length == 0;
					goto IL_00A7;
				}
				break;
			}
			EwsUtilities.Assert(false, "UserConfiguration.IsPropertyUpdated", "property not supported: " + property.ToString());
			IL_00A7:
			return flag && (!flag2 || !this.isNew);
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00041C3F File Offset: 0x00040C3F
		private void WriteXmlDataToXml(EwsServiceXmlWriter writer)
		{
			EwsUtilities.Assert(writer != null, "UserConfiguration.WriteXmlDataToXml", "writer is null");
			UserConfiguration.WriteByteArrayToXml(writer, this.xmlData, "XmlData");
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00041C68 File Offset: 0x00040C68
		private void WriteBinaryDataToXml(EwsServiceXmlWriter writer)
		{
			EwsUtilities.Assert(writer != null, "UserConfiguration.WriteBinaryDataToXml", "writer is null");
			UserConfiguration.WriteByteArrayToXml(writer, this.binaryData, "BinaryData");
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00041C94 File Offset: 0x00040C94
		internal void LoadFromXml(EwsServiceXmlReader reader)
		{
			EwsUtilities.Assert(reader != null, "UserConfiguration.LoadFromXml", "reader is null");
			reader.ReadStartElement(XmlNamespace.Messages, "UserConfiguration");
			reader.Read();
			do
			{
				if (reader.NodeType == 1)
				{
					string localName;
					if ((localName = reader.LocalName) != null)
					{
						if (localName == "UserConfigurationName")
						{
							string text = reader.ReadAttributeValue("Name");
							EwsUtilities.Assert(string.Compare(this.name, text, 4) == 0, "UserConfiguration.LoadFromXml", "UserConfigurationName does not match: Expected: " + this.name + " Name in response: " + text);
							reader.SkipCurrentElement();
							goto IL_0146;
						}
						if (localName == "ItemId")
						{
							this.itemId = new ItemId();
							this.itemId.LoadFromXml(reader, "ItemId");
							goto IL_0146;
						}
						if (localName == "Dictionary")
						{
							this.dictionary.LoadFromXml(reader, "Dictionary");
							goto IL_0146;
						}
						if (localName == "XmlData")
						{
							this.xmlData = Convert.FromBase64String(reader.ReadElementValue());
							goto IL_0146;
						}
						if (localName == "BinaryData")
						{
							this.binaryData = Convert.FromBase64String(reader.ReadElementValue());
							goto IL_0146;
						}
					}
					EwsUtilities.Assert(false, "UserConfiguration.LoadFromXml", "Xml element not supported: " + reader.LocalName);
				}
				IL_0146:
				reader.Read();
			}
			while (!reader.IsEndElement(XmlNamespace.Messages, "UserConfiguration"));
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00041E00 File Offset: 0x00040E00
		internal void LoadFromJson(JsonObject responseObject, ExchangeService service)
		{
			foreach (string text in responseObject.Keys)
			{
				string text2;
				if ((text2 = text) != null)
				{
					if (!(text2 == "UserConfigurationName"))
					{
						if (!(text2 == "ItemId"))
						{
							if (!(text2 == "Dictionary"))
							{
								if (!(text2 == "XmlData"))
								{
									if (text2 == "BinaryData")
									{
										this.binaryData = Convert.FromBase64String(responseObject.ReadAsString(text));
									}
								}
								else
								{
									this.xmlData = Convert.FromBase64String(responseObject.ReadAsString(text));
								}
							}
							else
							{
								((IJsonCollectionDeserializer)this.dictionary).CreateFromJsonCollection(responseObject.ReadAsArray(text), service);
							}
						}
						else
						{
							this.itemId = new ItemId();
							this.itemId.LoadFromJson(responseObject.ReadAsJsonObject(text), service);
						}
					}
					else
					{
						JsonObject jsonObject = responseObject.ReadAsJsonObject(text);
						string text3 = jsonObject.ReadAsString("Name");
						EwsUtilities.Assert(string.Compare(this.name, text3, 4) == 0, "UserConfiguration.LoadFromJson", "UserConfigurationName does not match: Expected: " + this.name + " Name in response: " + text3);
					}
				}
			}
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00041F5C File Offset: 0x00040F5C
		private void InitializeProperties(UserConfigurationProperties requestedProperties)
		{
			this.itemId = null;
			this.dictionary = new UserConfigurationDictionary();
			this.xmlData = null;
			this.binaryData = null;
			this.propertiesAvailableForAccess = requestedProperties;
			this.ResetIsDirty();
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00041F8B File Offset: 0x00040F8B
		private void ResetIsDirty()
		{
			this.updatedProperties = (UserConfigurationProperties)0;
			this.dictionary.IsDirty = false;
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00041FA0 File Offset: 0x00040FA0
		private void ValidatePropertyAccess(UserConfigurationProperties property)
		{
			if ((property & this.propertiesAvailableForAccess) != property)
			{
				throw new PropertyException(Strings.MustLoadOrAssignPropertyBeforeAccess, property.ToString());
			}
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00041FC8 File Offset: 0x00040FC8
		private void MarkPropertyForUpdate(UserConfigurationProperties property)
		{
			this.updatedProperties |= property;
			this.propertiesAvailableForAccess |= property;
		}

		// Token: 0x04001397 RID: 5015
		private const ExchangeVersion ObjectVersion = ExchangeVersion.Exchange2010;

		// Token: 0x04001398 RID: 5016
		private const UserConfigurationProperties PropertiesAvailableForNewObject = UserConfigurationProperties.Dictionary | UserConfigurationProperties.XmlData | UserConfigurationProperties.BinaryData;

		// Token: 0x04001399 RID: 5017
		private const UserConfigurationProperties NoProperties = (UserConfigurationProperties)0;

		// Token: 0x0400139A RID: 5018
		private ExchangeService service;

		// Token: 0x0400139B RID: 5019
		private string name;

		// Token: 0x0400139C RID: 5020
		private FolderId parentFolderId;

		// Token: 0x0400139D RID: 5021
		private ItemId itemId;

		// Token: 0x0400139E RID: 5022
		private UserConfigurationDictionary dictionary;

		// Token: 0x0400139F RID: 5023
		private byte[] xmlData;

		// Token: 0x040013A0 RID: 5024
		private byte[] binaryData;

		// Token: 0x040013A1 RID: 5025
		private UserConfigurationProperties propertiesAvailableForAccess;

		// Token: 0x040013A2 RID: 5026
		private UserConfigurationProperties updatedProperties;

		// Token: 0x040013A3 RID: 5027
		private bool isNew;
	}
}
