using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B44 RID: 2884
	public abstract class Rf2hFolderWithFieldsAndProperties : Rf2hFolderWithFields
	{
		// Token: 0x17001607 RID: 5639
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x001776B0 File Offset: 0x001758B0
		// (set) Token: 0x06005B31 RID: 23345 RVA: 0x001776B8 File Offset: 0x001758B8
		public string FolderTag { get; private set; }

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x001776C1 File Offset: 0x001758C1
		// (set) Token: 0x06005B33 RID: 23347 RVA: 0x001776C9 File Offset: 0x001758C9
		public Rf2hFolderPropertyCollection Properties { get; private set; }

		// Token: 0x06005B34 RID: 23348 RVA: 0x001776D2 File Offset: 0x001758D2
		protected Rf2hFolderWithFieldsAndProperties(Rf2hFolderType folderType, string folderTag, Dictionary<string, FieldTypeAndIndex> tagsToFieldInfo, string folderContents)
			: base(folderType, tagsToFieldInfo, folderContents)
		{
			if (folderContents != null && Rf2hFolder.GetFolderName(folderContents) != folderTag)
			{
				throw new CustomMqClientException(SR.FolderContentsNotTag);
			}
			this.FolderTag = folderTag;
			this.Properties = new Rf2hFolderPropertyCollection(this);
		}

		// Token: 0x06005B35 RID: 23349 RVA: 0x00177710 File Offset: 0x00175910
		internal override void ParseCompleteString()
		{
			XmlDocument xmlDocument = base.LoadDocumentFromCompleteString();
			foreach (XmlNode xmlNode in base.ParseDefinedFields(xmlDocument, true))
			{
				this.ParsePropertyNode(xmlNode, xmlDocument.DocumentElement.Name);
			}
		}

		// Token: 0x06005B36 RID: 23350 RVA: 0x00177778 File Offset: 0x00175978
		internal override string GenerateString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (base.FolderType == Rf2hFolderType.Properties)
			{
				stringBuilder.Append("<" + this.FolderTag + " content='properties'>");
			}
			else
			{
				stringBuilder.Append("<" + this.FolderTag + ">");
			}
			base.AddFields(stringBuilder);
			PropertyNode propertyNode = new PropertyNode("root", null);
			foreach (PropertyValueDefinition propertyValueDefinition in this.Properties)
			{
				propertyNode.AddProperty(propertyValueDefinition);
			}
			propertyNode.AddToXml(stringBuilder);
			stringBuilder.Append("</" + this.FolderTag + ">");
			return stringBuilder.ToString();
		}

		// Token: 0x06005B37 RID: 23351 RVA: 0x0017784C File Offset: 0x00175A4C
		private void ParsePropertyNode(XmlNode node, string currentFullName)
		{
			if (node.NodeType == XmlNodeType.Text)
			{
				this.AddSingleValue(node, currentFullName);
				return;
			}
			if (currentFullName.Length != 0)
			{
				currentFullName += ".";
			}
			currentFullName += node.Name;
			XmlNodeList childNodes = node.ChildNodes;
			if (childNodes.Count == 0)
			{
				this.AddSingleValue(node, currentFullName);
				return;
			}
			foreach (object obj in childNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				this.ParsePropertyNode(xmlNode, currentFullName);
			}
		}

		// Token: 0x06005B38 RID: 23352 RVA: 0x001778F0 File Offset: 0x00175AF0
		private void AddSingleValue(XmlNode valueNode, string currentFullName)
		{
			PropertyValueDefinition propertyValueDefinition = new PropertyValueDefinition(this, valueNode, currentFullName);
			this.Properties.Add(propertyValueDefinition);
		}

		// Token: 0x06005B39 RID: 23353 RVA: 0x00177912 File Offset: 0x00175B12
		internal void PropertyCollectionUpdated()
		{
			base.IsDirty = true;
		}

		// Token: 0x06005B3A RID: 23354 RVA: 0x0017791B File Offset: 0x00175B1B
		internal void EnsurePropertyCollectionExists()
		{
			base.EnsureParsed();
		}
	}
}
