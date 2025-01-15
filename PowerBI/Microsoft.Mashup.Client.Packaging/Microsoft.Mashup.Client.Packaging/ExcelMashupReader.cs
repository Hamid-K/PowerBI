using System;
using System.IO;
using System.IO.Packaging;
using System.Security;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Mashup.Client.Packaging
{
	// Token: 0x0200000A RID: 10
	public class ExcelMashupReader : MashupReader
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002360 File Offset: 0x00000560
		public override bool TryGetMashupBytes(Package excelPackage, out byte[] mashupBytes)
		{
			PackagePartCollection parts = excelPackage.GetParts();
			foreach (PackagePart packagePart in parts)
			{
				XDocument xdocument;
				if (ExcelMashupReader.TryGetMashupPart(packagePart, out xdocument))
				{
					mashupBytes = Convert.FromBase64String(xdocument.Root.Value);
					return mashupBytes.Length != 0;
				}
			}
			mashupBytes = null;
			return false;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023D8 File Offset: 0x000005D8
		private static bool TryGetMashupPart(PackagePart part, out XDocument xdoc)
		{
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.XmlResolver = null;
			xmlReaderSettings.ProhibitDtd = true;
			try
			{
				if (!part.Uri.OriginalString.StartsWith("/customXml/", 5))
				{
					xdoc = null;
					return false;
				}
				using (Stream stream = part.GetStream(3, 1))
				{
					using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
					{
						xdoc = XDocument.Load(xmlReader);
						string namespaceName = xdoc.Root.Name.NamespaceName;
						string localName = xdoc.Root.Name.LocalName;
						if (((namespaceName == "http://schemas.microsoft.com/DataMashup" || namespaceName == "http://schemas.microsoft.com/DataMashup/Temp") && localName == "DataMashup") || ((namespaceName == "http://schemas.microsoft.com/DataExplorer" || namespaceName == "http://schemas.microsoft.com/DataExplorer/Temp") && localName == "DataExplorer"))
						{
							return true;
						}
					}
				}
			}
			catch (ArgumentException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (FileNotFoundException)
			{
			}
			catch (XmlException)
			{
			}
			xdoc = null;
			return false;
		}

		// Token: 0x04000037 RID: 55
		private const string NamespaceUri = "http://schemas.microsoft.com/DataMashup";

		// Token: 0x04000038 RID: 56
		private const string NamespaceUriTemp = "http://schemas.microsoft.com/DataMashup/Temp";

		// Token: 0x04000039 RID: 57
		private const string OldNamespaceUri = "http://schemas.microsoft.com/DataExplorer";

		// Token: 0x0400003A RID: 58
		private const string OldNamespaceUriTemp = "http://schemas.microsoft.com/DataExplorer/Temp";

		// Token: 0x0400003B RID: 59
		private const string RootNodeName = "DataMashup";

		// Token: 0x0400003C RID: 60
		private const string OldRootNodeName = "DataExplorer";
	}
}
