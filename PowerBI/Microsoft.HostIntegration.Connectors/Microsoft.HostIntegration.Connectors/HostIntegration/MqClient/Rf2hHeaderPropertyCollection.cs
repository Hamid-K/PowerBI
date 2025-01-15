using System;
using System.Collections.Generic;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B4C RID: 2892
	internal class Rf2hHeaderPropertyCollection
	{
		// Token: 0x06005B65 RID: 23397 RVA: 0x00178282 File Offset: 0x00176482
		internal Rf2hHeaderPropertyCollection(RulesAndFormattingVersion2Header parentHeader)
		{
			this.header = parentHeader;
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x00178291 File Offset: 0x00176491
		internal Rf2hHeaderPropertyCollectionEnumerator GetEnumerator()
		{
			return new Rf2hHeaderPropertyCollectionEnumerator(this);
		}

		// Token: 0x17001614 RID: 5652
		// (get) Token: 0x06005B67 RID: 23399 RVA: 0x0017829C File Offset: 0x0017649C
		internal int Count
		{
			get
			{
				List<Rf2hFolder> list = new List<Rf2hFolder>();
				List<Rf2hFolder> list2;
				if (this.header.Folders.TypesToFolder.TryGetValue(Rf2hFolderType.Mq_Usr, out list2))
				{
					list.AddRange(list2);
				}
				if (this.header.Folders.TypesToFolder.TryGetValue(Rf2hFolderType.Properties, out list2))
				{
					list.AddRange(list2);
				}
				if (this.header.Folders.TypesToFolder.TryGetValue(Rf2hFolderType.Usr, out list2))
				{
					list.AddRange(list2);
				}
				int num = 0;
				foreach (Rf2hFolder rf2hFolder in list)
				{
					num += (rf2hFolder as Rf2hFolderWithFieldsAndProperties).Properties.Count;
				}
				return num;
			}
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x00178364 File Offset: 0x00176564
		internal void Add(PropertyValueDefinition newPropertyValue)
		{
			if (newPropertyValue == null)
			{
				throw new Exception("BUGBUG: should not call Add with null");
			}
			this.GetPropertiesFolder(newPropertyValue.InsertIntoFolderType, newPropertyValue.InsertIntoPropertiesFolderTag, true).Properties.Add(newPropertyValue);
			this.header.ChangesMade(false);
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0017839E File Offset: 0x0017659E
		private Rf2hFolderWithFieldsAndProperties GetPropertiesFolder(Rf2hFolderType folderType, string folderTag, bool createIfNeeded)
		{
			return this.header.Folders.GetPropertiesFolder(folderType, folderTag, createIfNeeded);
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x001783B4 File Offset: 0x001765B4
		internal bool Remove(PropertyValueDefinition oldPropertyValue)
		{
			if (oldPropertyValue == null)
			{
				throw new Exception("BUGBUG: should not call Remove with null");
			}
			Rf2hFolderWithFieldsAndProperties propertiesFolder = this.GetPropertiesFolder(oldPropertyValue.InsertIntoFolderType, oldPropertyValue.InsertIntoPropertiesFolderTag, false);
			return propertiesFolder != null && propertiesFolder.Properties.Remove(oldPropertyValue);
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x001783F4 File Offset: 0x001765F4
		internal void Clear()
		{
			foreach (KeyValuePair<Rf2hFolderType, List<Rf2hFolder>> keyValuePair in this.header.Folders.TypesToFolder)
			{
				if (keyValuePair.Key != Rf2hFolderType.Jms && keyValuePair.Key != Rf2hFolderType.Mcd && keyValuePair.Key != Rf2hFolderType.Unparsed)
				{
					foreach (Rf2hFolder rf2hFolder in keyValuePair.Value)
					{
						(rf2hFolder as Rf2hFolderWithFieldsAndProperties).Properties.Clear();
					}
				}
			}
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x001784B4 File Offset: 0x001766B4
		internal bool TryGetValue(string propertyName, out PropertyValueDefinition propertyValue)
		{
			if (string.IsNullOrWhiteSpace(propertyName))
			{
				throw new Exception("BUGBUG: should not call TryGetValue with null");
			}
			propertyValue = null;
			Rf2hFolderType rf2hFolderType;
			string text;
			PropertyValueDefinition.GetFolderTypeAndName(ref propertyName, out rf2hFolderType, out text);
			Rf2hFolderWithFieldsAndProperties propertiesFolder = this.GetPropertiesFolder(rf2hFolderType, text, false);
			return propertiesFolder != null && propertiesFolder.Properties.TryGetValue(propertyName, out propertyValue);
		}

		// Token: 0x17001615 RID: 5653
		internal PropertyValueDefinition this[string propertyName]
		{
			get
			{
				if (string.IsNullOrWhiteSpace(propertyName))
				{
					throw new Exception("BUGBUG: should not call Item[] with null");
				}
				Rf2hFolderType rf2hFolderType;
				string text;
				PropertyValueDefinition.GetFolderTypeAndName(ref propertyName, out rf2hFolderType, out text);
				Rf2hFolderWithFieldsAndProperties propertiesFolder = this.GetPropertiesFolder(rf2hFolderType, text, false);
				if (propertiesFolder == null)
				{
					throw new CustomMqClientException(SR.PropertyNotFoundInMessage);
				}
				return propertiesFolder.Properties[propertyName];
			}
			set
			{
				if (string.IsNullOrWhiteSpace(propertyName))
				{
					throw new Exception("BUGBUG: should not call Item[] with null");
				}
				Rf2hFolderType rf2hFolderType;
				string text;
				PropertyValueDefinition.GetFolderTypeAndName(ref propertyName, out rf2hFolderType, out text);
				this.GetPropertiesFolder(rf2hFolderType, text, true).Properties[propertyName] = value;
				this.header.ChangesMade(false);
			}
		}

		// Token: 0x040047F3 RID: 18419
		internal RulesAndFormattingVersion2Header header;
	}
}
