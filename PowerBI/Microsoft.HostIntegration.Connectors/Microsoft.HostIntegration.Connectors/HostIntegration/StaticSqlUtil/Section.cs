using System;
using System.Collections.Generic;
using System.Xml;

namespace Microsoft.HostIntegration.StaticSqlUtil
{
	// Token: 0x02000A7B RID: 2683
	public class Section
	{
		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x06005359 RID: 21337 RVA: 0x001531A7 File Offset: 0x001513A7
		// (set) Token: 0x0600535A RID: 21338 RVA: 0x001531AF File Offset: 0x001513AF
		public ResultSet ResultSet
		{
			get
			{
				return this._resultSet;
			}
			set
			{
				this._resultSet = value;
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x0600535B RID: 21339 RVA: 0x001531B8 File Offset: 0x001513B8
		// (set) Token: 0x0600535C RID: 21340 RVA: 0x001531C0 File Offset: 0x001513C0
		public List<Parameter> Parameters
		{
			get
			{
				return this._parameters;
			}
			set
			{
				this._parameters = value;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x0600535D RID: 21341 RVA: 0x001531C9 File Offset: 0x001513C9
		// (set) Token: 0x0600535E RID: 21342 RVA: 0x001531D1 File Offset: 0x001513D1
		public int PackageSectionNumber
		{
			get
			{
				return this._packageSectionNumber;
			}
			set
			{
				this._packageSectionNumber = value;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x0600535F RID: 21343 RVA: 0x001531DA File Offset: 0x001513DA
		// (set) Token: 0x06005360 RID: 21344 RVA: 0x001531E2 File Offset: 0x001513E2
		public string PackageSectionAlias
		{
			get
			{
				return this._packageSectionAlias;
			}
			set
			{
				this._packageSectionAlias = value;
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06005361 RID: 21345 RVA: 0x001531EB File Offset: 0x001513EB
		// (set) Token: 0x06005362 RID: 21346 RVA: 0x001531F3 File Offset: 0x001513F3
		public Statement Statement
		{
			get
			{
				return this._statement;
			}
			set
			{
				this._statement = value;
			}
		}

		// Token: 0x06005363 RID: 21347 RVA: 0x001531FC File Offset: 0x001513FC
		internal void SaveToXml(XmlWriter writer)
		{
			writer.WriteStartElement("section");
			writer.WriteAttributeString("packageSectionNumber", this._packageSectionNumber.ToString());
			writer.WriteAttributeString("alias", this._packageSectionAlias);
			this._statement.SaveToXml(writer);
			writer.WriteStartElement("parameters");
			foreach (Parameter parameter in this._parameters)
			{
				parameter.SaveToXml(writer);
			}
			writer.WriteEndElement();
			this._resultSet.SaveToXml(writer);
			writer.WriteEndElement();
		}

		// Token: 0x06005364 RID: 21348 RVA: 0x001532B0 File Offset: 0x001514B0
		internal void LoadFromXml(XmlElement sectionElement, XmlNamespaceManager nsmgr)
		{
			if (sectionElement.Attributes["packageSectionNumber"] != null)
			{
				int.TryParse(sectionElement.Attributes["packageSectionNumber"].Value, out this._packageSectionNumber);
			}
			if (sectionElement.Attributes["packageSectionAlias"] != null)
			{
				this._packageSectionAlias = sectionElement.Attributes["packageSectionAlias"].Value;
			}
			XmlNode xmlNode = sectionElement.SelectSingleNode("descendant::drdastaticsql:statement", nsmgr);
			if (xmlNode != null)
			{
				this._statement.LoadFromXml((XmlElement)xmlNode, nsmgr);
			}
			foreach (object obj in sectionElement.SelectNodes("descendant::drdastaticsql:parameters/drdastaticsql:parameter", nsmgr))
			{
				XmlElement xmlElement = (XmlElement)obj;
				Parameter parameter = new Parameter();
				parameter.LoadFromXml(xmlElement, nsmgr);
				this._parameters.Add(parameter);
			}
			XmlNode xmlNode2 = sectionElement.SelectSingleNode("descendant::drdastaticsql:resultSet", nsmgr);
			if (xmlNode2 != null)
			{
				this._resultSet.LoadFromXml((XmlElement)xmlNode2, nsmgr);
			}
		}

		// Token: 0x06005365 RID: 21349 RVA: 0x001533CC File Offset: 0x001515CC
		internal void LoadFromXmlV8(XmlElement sectionElement)
		{
			if (sectionElement.Attributes["Number"] != null)
			{
				int.TryParse(sectionElement.Attributes["Number"].Value, out this._packageSectionNumber);
			}
			if (sectionElement.Attributes["Alias"] != null)
			{
				this._packageSectionAlias = sectionElement.Attributes["Alias"].Value;
			}
			XmlNode xmlNode = sectionElement.SelectSingleNode("Statement");
			if (xmlNode != null)
			{
				this._statement.LoadFromXmlV8((XmlElement)xmlNode);
			}
			foreach (object obj in sectionElement.SelectNodes("Parameters/Parameter"))
			{
				XmlElement xmlElement = (XmlElement)obj;
				Parameter parameter = new Parameter();
				parameter.LoadFromXmlV8(xmlElement);
				this._parameters.Add(parameter);
			}
			XmlNode xmlNode2 = sectionElement.SelectSingleNode("ResultSet");
			if (xmlNode2 != null)
			{
				this._resultSet.LoadFromXmlV8((XmlElement)xmlNode2);
			}
		}

		// Token: 0x06005366 RID: 21350 RVA: 0x001534E4 File Offset: 0x001516E4
		internal void SaveToXmlV8(XmlWriter writer)
		{
			writer.WriteStartElement("Section");
			writer.WriteAttributeString("Number", this._packageSectionNumber.ToString());
			writer.WriteAttributeString("Alias", this._packageSectionAlias);
			this._statement.SaveToXmlV8(writer);
			writer.WriteStartElement("Parameters");
			foreach (Parameter parameter in this._parameters)
			{
				parameter.SaveToXmlV8(writer);
			}
			writer.WriteEndElement();
			this._resultSet.SaveToXmlV8(writer);
			writer.WriteEndElement();
		}

		// Token: 0x0400425F RID: 16991
		private int _packageSectionNumber = -1;

		// Token: 0x04004260 RID: 16992
		private string _packageSectionAlias;

		// Token: 0x04004261 RID: 16993
		private Statement _statement = new Statement();

		// Token: 0x04004262 RID: 16994
		private ResultSet _resultSet = new ResultSet();

		// Token: 0x04004263 RID: 16995
		private List<Parameter> _parameters = new List<Parameter>();
	}
}
