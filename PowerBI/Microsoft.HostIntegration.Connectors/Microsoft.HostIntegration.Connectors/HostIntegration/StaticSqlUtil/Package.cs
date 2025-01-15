using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A78 RID: 2680
	public class Package
	{
		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x0600532E RID: 21294 RVA: 0x00152387 File Offset: 0x00150587
		// (set) Token: 0x0600532F RID: 21295 RVA: 0x0015238F File Offset: 0x0015058F
		public string Title
		{
			get
			{
				return this._title;
			}
			set
			{
				this._title = value;
			}
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06005330 RID: 21296 RVA: 0x00152398 File Offset: 0x00150598
		// (set) Token: 0x06005331 RID: 21297 RVA: 0x001523A0 File Offset: 0x001505A0
		public string ConsistencyToken
		{
			get
			{
				return this._consistencyToken;
			}
			set
			{
				this._consistencyToken = value;
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06005332 RID: 21298 RVA: 0x001523A9 File Offset: 0x001505A9
		// (set) Token: 0x06005333 RID: 21299 RVA: 0x001523B1 File Offset: 0x001505B1
		public string VersionName
		{
			get
			{
				return this._versionName;
			}
			set
			{
				this._versionName = value;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06005334 RID: 21300 RVA: 0x001523BA File Offset: 0x001505BA
		// (set) Token: 0x06005335 RID: 21301 RVA: 0x001523C2 File Offset: 0x001505C2
		public string CollectionIdentifier
		{
			get
			{
				return this._collectionIdentifier;
			}
			set
			{
				this._collectionIdentifier = value;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x001523CB File Offset: 0x001505CB
		// (set) Token: 0x06005337 RID: 21303 RVA: 0x001523D3 File Offset: 0x001505D3
		public string PackageIdentifier
		{
			get
			{
				return this._packageIdentifier;
			}
			set
			{
				this._packageIdentifier = value;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06005338 RID: 21304 RVA: 0x001523DC File Offset: 0x001505DC
		public List<Section> Sections
		{
			get
			{
				return this._sections;
			}
		}

		// Token: 0x06005339 RID: 21305 RVA: 0x001523E4 File Offset: 0x001505E4
		internal void SaveToXml(XmlWriter writer)
		{
			writer.WriteStartElement("package");
			writer.WriteAttributeString("consistencyToken", this._consistencyToken);
			writer.WriteAttributeString("versionName", this._versionName);
			writer.WriteAttributeString("collectionIdentifier", this._collectionIdentifier);
			writer.WriteAttributeString("packageIdentifier", this._packageIdentifier);
			writer.WriteAttributeString("title", this._title);
			writer.WriteStartElement("sections");
			foreach (Section section in this._sections)
			{
				section.SaveToXml(writer);
			}
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x0600533A RID: 21306 RVA: 0x001524AC File Offset: 0x001506AC
		internal void LoadFromXml(XmlElement packageElement, XmlNamespaceManager nsmgr)
		{
			if (packageElement.Attributes["consistencyToken"] != null)
			{
				this._consistencyToken = packageElement.Attributes["consistencyToken"].Value;
			}
			if (packageElement.Attributes["versionName"] != null)
			{
				this._versionName = packageElement.Attributes["versionName"].Value;
			}
			if (packageElement.Attributes["collectionIdentifier"] != null)
			{
				this._collectionIdentifier = packageElement.Attributes["collectionIdentifier"].Value;
			}
			if (packageElement.Attributes["packageIdentifier"] != null)
			{
				this._packageIdentifier = packageElement.Attributes["packageIdentifier"].Value;
			}
			if (packageElement.Attributes["title"] != null)
			{
				this._title = packageElement.Attributes["title"].Value;
			}
			foreach (object obj in packageElement.SelectNodes("descendant::drdastaticsql:sections/drdastaticsql:section", nsmgr))
			{
				XmlElement xmlElement = (XmlElement)obj;
				Section section = new Section();
				section.LoadFromXml(xmlElement, nsmgr);
				this._sections.Add(section);
			}
		}

		// Token: 0x0600533B RID: 21307 RVA: 0x00152600 File Offset: 0x00150800
		internal void LoadFromXmlV8(XmlElement packageElement)
		{
			if (packageElement.Attributes["Token"] != null)
			{
				this._consistencyToken = packageElement.Attributes["Token"].Value;
			}
			if (packageElement.Attributes["Version"] != null)
			{
				this._versionName = packageElement.Attributes["Version"].Value;
			}
			if (packageElement.Attributes["Collection"] != null)
			{
				this._collectionIdentifier = packageElement.Attributes["Collection"].Value;
			}
			if (packageElement.Attributes["Id"] != null)
			{
				this._packageIdentifier = packageElement.Attributes["Id"].Value;
			}
			if (packageElement.Attributes["Title"] != null)
			{
				this._title = packageElement.Attributes["Title"].Value;
			}
			if (packageElement.Attributes["IsolationLevel"] != null)
			{
				this._isolationLevel = packageElement.Attributes["IsolationLevel"].Value;
			}
			XmlNodeList xmlNodeList = packageElement.SelectNodes("Section");
			Section[] array = new Section[xmlNodeList.Count];
			int num = 0;
			foreach (object obj in xmlNodeList)
			{
				XmlElement xmlElement = (XmlElement)obj;
				Section section = new Section();
				section.LoadFromXmlV8(xmlElement);
				array[num] = section;
				num++;
			}
			Section section2 = new Section();
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = i + 1; j < array.Length; j++)
				{
					if (array[i].PackageSectionNumber > array[j].PackageSectionNumber)
					{
						section2 = array[i];
						array[i] = array[j];
						array[j] = section2;
					}
				}
			}
			for (int k = 0; k < xmlNodeList.Count; k++)
			{
				this._sections.Add(array[k]);
			}
			array = null;
		}

		// Token: 0x0600533C RID: 21308 RVA: 0x00152810 File Offset: 0x00150A10
		internal void SaveToXmlV8(XmlWriter writer)
		{
			writer.WriteStartElement("Package");
			writer.WriteAttributeString("Token", this._consistencyToken);
			writer.WriteAttributeString("Version", this._versionName);
			writer.WriteAttributeString("Collection", this._collectionIdentifier);
			writer.WriteAttributeString("Id", this._packageIdentifier);
			writer.WriteAttributeString("Title", this._title);
			writer.WriteAttributeString("IsolationLevel", this._isolationLevel);
			foreach (Section section in this._sections)
			{
				section.SaveToXmlV8(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x0400424E RID: 16974
		private string _title;

		// Token: 0x0400424F RID: 16975
		private string _consistencyToken;

		// Token: 0x04004250 RID: 16976
		private string _versionName;

		// Token: 0x04004251 RID: 16977
		private string _collectionIdentifier;

		// Token: 0x04004252 RID: 16978
		private string _packageIdentifier;

		// Token: 0x04004253 RID: 16979
		private List<Section> _sections = new List<Section>();

		// Token: 0x04004254 RID: 16980
		internal string _isolationLevel = string.Empty;
	}
}
