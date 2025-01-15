using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200035D RID: 861
	internal sealed class SecureStoreLookup : IXmlSerializable
	{
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x00073280 File Offset: 0x00071480
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x00073288 File Offset: 0x00071488
		public SecureStoreLookup.LookupContextOptions LookupContext { get; set; }

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x00073291 File Offset: 0x00071491
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x00073299 File Offset: 0x00071499
		public string TargetApplicationId { get; set; }

		// Token: 0x06001C7F RID: 7295 RVA: 0x0000289C File Offset: 0x00000A9C
		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x000732A4 File Offset: 0x000714A4
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (!reader.IsEmptyElement)
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						string localName = reader.LocalName;
						if (!(localName == "LookupContext"))
						{
							if (localName == "TargetApplicationId")
							{
								reader.Read();
								this.TargetApplicationId = reader.ReadContentAsString();
							}
						}
						else
						{
							reader.Read();
							this.LookupContext = this.ReadLookupContext(reader);
						}
					}
					else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "SecureStoreLookup")
					{
						break;
					}
				}
			}
		}

		// Token: 0x06001C81 RID: 7297 RVA: 0x0007333C File Offset: 0x0007153C
		private SecureStoreLookup.LookupContextOptions ReadLookupContext(XmlReader reader)
		{
			try
			{
				return (SecureStoreLookup.LookupContextOptions)Enum.Parse(typeof(SecureStoreLookup.LookupContextOptions), reader.ReadContentAsString(), false);
			}
			catch (ArgumentException)
			{
				DSD2ErrorHandlingUtils.ThrowIfInavalidElement(reader.LocalName);
			}
			return SecureStoreLookup.LookupContextOptions.AuthenticatedUser;
		}

		// Token: 0x06001C82 RID: 7298 RVA: 0x00073388 File Offset: 0x00071588
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("SecureStoreLookup");
			writer.WriteElementString("LookupContext", this.LookupContext.ToString());
			if (this.TargetApplicationId != null)
			{
				writer.WriteElementString("TargetApplicationId", this.TargetApplicationId);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000BCE RID: 3022
		internal const string SECURESTORELOOKUP = "SecureStoreLookup";

		// Token: 0x04000BCF RID: 3023
		internal const string LOOKUPCONTEXT = "LookupContext";

		// Token: 0x04000BD0 RID: 3024
		internal const string TARGETAPPLICATIONID = "TargetApplicationId";

		// Token: 0x020004F7 RID: 1271
		public enum LookupContextOptions
		{
			// Token: 0x040011B1 RID: 4529
			AuthenticatedUser,
			// Token: 0x040011B2 RID: 4530
			Unattended
		}
	}
}
