using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B3A RID: 2874
	public abstract class Rf2hFolderWithFields : Rf2hFolder
	{
		// Token: 0x06005AD8 RID: 23256 RVA: 0x001764CC File Offset: 0x001746CC
		public static Dictionary<string, FieldTypeAndIndex> GenerateFieldInfo(FolderFieldType[] fieldTypes, string[] fieldTags)
		{
			Dictionary<string, FieldTypeAndIndex> dictionary = new Dictionary<string, FieldTypeAndIndex>(fieldTypes.Length);
			for (int i = 0; i < fieldTypes.Length; i++)
			{
				dictionary.Add(fieldTags[i], new FieldTypeAndIndex(fieldTypes[i], i));
			}
			return dictionary;
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x00176503 File Offset: 0x00174703
		protected Rf2hFolderWithFields(Rf2hFolderType folderType, Dictionary<string, FieldTypeAndIndex> tagsToFieldInfo, string folderContents)
			: base(folderType, folderContents)
		{
			this.tagsToFieldTypeAndIndex = tagsToFieldInfo;
			this.folderFieldValues = new object[(tagsToFieldInfo == null) ? 0 : tagsToFieldInfo.Count];
		}

		// Token: 0x06005ADA RID: 23258 RVA: 0x0017652C File Offset: 0x0017472C
		protected T GetProperty<T>(int propertyIndex)
		{
			base.EnsureParsed();
			object obj;
			if ((obj = this.folderFieldValues[propertyIndex]) == null)
			{
				obj = default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x06005ADB RID: 23259 RVA: 0x0017655E File Offset: 0x0017475E
		protected void SetProperty<T>(int propertyIndex, T value)
		{
			base.EnsureParsed();
			this.folderFieldValues[propertyIndex] = value;
			base.IsDirty = true;
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x0017657C File Offset: 0x0017477C
		internal override void ParseCompleteString()
		{
			XmlDocument xmlDocument = base.LoadDocumentFromCompleteString();
			this.ParseDefinedFields(xmlDocument, false);
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x0017659C File Offset: 0x0017479C
		protected List<XmlNode> ParseDefinedFields(XmlDocument document, bool allowUnknownChildren)
		{
			List<XmlNode> list = null;
			if (allowUnknownChildren)
			{
				list = new List<XmlNode>(document.DocumentElement.ChildNodes.Count);
			}
			foreach (object obj in document.DocumentElement.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				FieldTypeAndIndex fieldTypeAndIndex;
				if (this.tagsToFieldTypeAndIndex == null)
				{
					list.Add(xmlNode);
				}
				else if (!this.tagsToFieldTypeAndIndex.TryGetValue(xmlNode.Name, out fieldTypeAndIndex))
				{
					if (!allowUnknownChildren)
					{
						throw new CustomMqClientException(SR.FolderUnknownProperty(base.FolderType.ToString().ToLowerInvariant(), xmlNode.Name));
					}
					list.Add(xmlNode);
				}
				else
				{
					string innerText = xmlNode.InnerText;
					if (innerText.Length != 0 && (fieldTypeAndIndex.FolderFieldType == FolderFieldType.String || !string.IsNullOrWhiteSpace(innerText)))
					{
						switch (fieldTypeAndIndex.FolderFieldType)
						{
						case FolderFieldType.I4:
						{
							int num;
							if (!int.TryParse(innerText, out num))
							{
								throw new CustomMqClientException(SR.UnparseableIntProperty(base.FolderType.ToString().ToLowerInvariant(), xmlNode.Name, innerText));
							}
							this.folderFieldValues[fieldTypeAndIndex.FolderFieldIndex] = num;
							break;
						}
						case FolderFieldType.I8:
						{
							long num2;
							if (!long.TryParse(innerText, out num2))
							{
								throw new CustomMqClientException(SR.UnparseableLongProperty(base.FolderType.ToString().ToLowerInvariant(), xmlNode.Name, innerText));
							}
							this.folderFieldValues[fieldTypeAndIndex.FolderFieldIndex] = num2;
							break;
						}
						case FolderFieldType.String:
							this.folderFieldValues[fieldTypeAndIndex.FolderFieldIndex] = innerText;
							break;
						default:
							throw new InvalidOperationException("BUGBUG: Invalid Field Type");
						}
					}
				}
			}
			return list;
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x00176788 File Offset: 0x00174988
		internal override string GenerateString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("<" + base.FolderType.ToString().ToLowerInvariant() + ">");
			this.AddFields(stringBuilder);
			stringBuilder.Append("</" + base.FolderType.ToString().ToLowerInvariant() + ">");
			return stringBuilder.ToString();
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x00176808 File Offset: 0x00174A08
		protected void AddFields(StringBuilder sb)
		{
			if (this.tagsToFieldTypeAndIndex == null)
			{
				return;
			}
			foreach (KeyValuePair<string, FieldTypeAndIndex> keyValuePair in this.tagsToFieldTypeAndIndex)
			{
				if (this.folderFieldValues[keyValuePair.Value.FolderFieldIndex] != null)
				{
					sb.Append("<" + keyValuePair.Key + ">");
					sb.Append(this.folderFieldValues[keyValuePair.Value.FolderFieldIndex].ToString());
					sb.Append("</" + keyValuePair.Key + ">");
				}
			}
		}

		// Token: 0x0400479C RID: 18332
		private Dictionary<string, FieldTypeAndIndex> tagsToFieldTypeAndIndex;

		// Token: 0x0400479D RID: 18333
		private object[] folderFieldValues;
	}
}
