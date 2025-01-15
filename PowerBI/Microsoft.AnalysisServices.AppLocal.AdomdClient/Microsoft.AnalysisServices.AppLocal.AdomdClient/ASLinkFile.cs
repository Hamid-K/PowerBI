using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000017 RID: 23
	internal static class ASLinkFile
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000023DC File Offset: 0x000005DC
		public static void LoadFromStream(Stream stream, out string server, out string database, out string description, out bool isDelegationAllowed)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.CloseInput = false;
			xmlReaderSettings.ValidationType = ValidationType.Schema;
			xmlReaderSettings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.MaxCharactersInDocument = 4096L;
			xmlReaderSettings.Schemas.Add("http://schemas.microsoft.com/analysisservices/linkfile", new XmlTextReader(ASLinkFile.Schema.FullSchema, XmlNodeType.Document, null));
			xmlReaderSettings.ValidationEventHandler += delegate(object sender, ValidationEventArgs args)
			{
				throw args.Exception;
			};
			server = null;
			database = null;
			description = null;
			isDelegationAllowed = false;
			using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
			{
				while (xmlReader.Read())
				{
					if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.NamespaceURI.Equals("http://schemas.microsoft.com/analysisservices/linkfile"))
					{
						string localName = xmlReader.LocalName;
						if (!(localName == "ASLinkFile"))
						{
							if (!(localName == "Server"))
							{
								if (!(localName == "Database"))
								{
									if (localName == "Description")
									{
										description = xmlReader.ReadString();
									}
								}
								else
								{
									database = xmlReader.ReadString();
								}
							}
							else
							{
								server = xmlReader.ReadString();
							}
						}
						else if (xmlReader.MoveToAttribute("allowDelegation"))
						{
							isDelegationAllowed = xmlReader.ReadContentAsBoolean();
						}
					}
				}
			}
		}

		// Token: 0x04000092 RID: 146
		public const int MaxLinkFileSize = 4096;

		// Token: 0x02000168 RID: 360
		private static class Schema
		{
			// Token: 0x04000BAD RID: 2989
			public const string Namespace = "http://schemas.microsoft.com/analysisservices/linkfile";

			// Token: 0x04000BAE RID: 2990
			public const string XmlTag_Root = "ASLinkFile";

			// Token: 0x04000BAF RID: 2991
			public const string XmlAttribute_Root_AllowDelegation = "allowDelegation";

			// Token: 0x04000BB0 RID: 2992
			public const string XmlTag_Server = "Server";

			// Token: 0x04000BB1 RID: 2993
			public const string XmlTag_Database = "Database";

			// Token: 0x04000BB2 RID: 2994
			public const string XmlTag_Description = "Description";

			// Token: 0x04000BB3 RID: 2995
			public static readonly string FullSchema = string.Format("<?xml version='1.0' encoding='utf-8'?> \r\n<xs:schema xmlns:xs='http://www.w3.org/2001/XMLSchema' targetNamespace='{0}' elementFormDefault='qualified'>\r\n  <xs:element name='{1}'>\r\n    <xs:complexType>\r\n      <xs:all>\r\n        <xs:element name='{2}' type='xs:string' /> \r\n        <xs:element name='{3}' type='xs:string' />\r\n        <xs:element name='{4}' type='xs:string' minOccurs='0' />\r\n      </xs:all>\r\n      <xs:attribute name='{5}' type='xs:boolean' default='false' />\r\n    </xs:complexType>\r\n  </xs:element>\r\n</xs:schema>", new object[] { "http://schemas.microsoft.com/analysisservices/linkfile", "ASLinkFile", "Server", "Database", "Description", "allowDelegation" });
		}
	}
}
