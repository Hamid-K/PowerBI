using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.Common
{
	// Token: 0x0200035A RID: 858
	internal sealed class DataSourceDefinition2 : IXmlSerializable
	{
		// Token: 0x17000803 RID: 2051
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x00072A74 File Offset: 0x00070C74
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x00072A7C File Offset: 0x00070C7C
		public string ConnectString { get; set; }

		// Token: 0x17000804 RID: 2052
		// (get) Token: 0x06001C51 RID: 7249 RVA: 0x00072A85 File Offset: 0x00070C85
		// (set) Token: 0x06001C52 RID: 7250 RVA: 0x00072A8D File Offset: 0x00070C8D
		public DataSourceDefinition2.CredentialRetrievalTypes CredentialRetrieval { get; set; }

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06001C53 RID: 7251 RVA: 0x00072A96 File Offset: 0x00070C96
		// (set) Token: 0x06001C54 RID: 7252 RVA: 0x00072A9E File Offset: 0x00070C9E
		public bool WindowsCredentials { get; set; }

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06001C55 RID: 7253 RVA: 0x00072AA7 File Offset: 0x00070CA7
		// (set) Token: 0x06001C56 RID: 7254 RVA: 0x00072AAF File Offset: 0x00070CAF
		public bool ImpersonateUser { get; set; }

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00072AB8 File Offset: 0x00070CB8
		// (set) Token: 0x06001C58 RID: 7256 RVA: 0x00072AC0 File Offset: 0x00070CC0
		public SecureStoreLookup SecureStoreLookup { get; set; }

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00072AC9 File Offset: 0x00070CC9
		// (set) Token: 0x06001C5A RID: 7258 RVA: 0x00072AD1 File Offset: 0x00070CD1
		public string PerspectiveName { get; set; }

		// Token: 0x17000809 RID: 2057
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00072ADA File Offset: 0x00070CDA
		// (set) Token: 0x06001C5C RID: 7260 RVA: 0x00072AE2 File Offset: 0x00070CE2
		public string OnPremiseModelIdentityClaims { get; set; }

		// Token: 0x1700080A RID: 2058
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00072AEB File Offset: 0x00070CEB
		// (set) Token: 0x06001C5E RID: 7262 RVA: 0x00072AF3 File Offset: 0x00070CF3
		public long PowerBIModelId { get; set; }

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00072AFC File Offset: 0x00070CFC
		// (set) Token: 0x06001C60 RID: 7264 RVA: 0x00072B04 File Offset: 0x00070D04
		public DateTime? PowerBIModelLastUpdatedTime { get; set; }

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00072B0D File Offset: 0x00070D0D
		// (set) Token: 0x06001C62 RID: 7266 RVA: 0x00072B15 File Offset: 0x00070D15
		public bool IsMultiDimensional { get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00072B1E File Offset: 0x00070D1E
		// (set) Token: 0x06001C64 RID: 7268 RVA: 0x00072B26 File Offset: 0x00070D26
		public bool IsFullyFormedExternalConnectionString { get; set; }

		// Token: 0x06001C65 RID: 7269 RVA: 0x00072B30 File Offset: 0x00070D30
		internal static DataSourceDefinition2 ReadFromString(string dataSourceDefinition)
		{
			DataSourceDefinition2 dataSourceDefinition2 = new DataSourceDefinition2();
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.IgnoreWhitespace = true;
			xmlReaderSettings.ProhibitDtd = true;
			xmlReaderSettings.XmlResolver = null;
			using (StringReader stringReader = new StringReader(dataSourceDefinition))
			{
				using (XmlReader xmlReader = XmlReader.Create(stringReader, xmlReaderSettings))
				{
					xmlReader.Read();
					dataSourceDefinition2.ReadXml(xmlReader);
				}
			}
			return dataSourceDefinition2;
		}

		// Token: 0x06001C66 RID: 7270 RVA: 0x0000289C File Offset: 0x00000A9C
		public XmlSchema GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001C67 RID: 7271 RVA: 0x00072BB0 File Offset: 0x00070DB0
		public void WriteXml(XmlWriter writer)
		{
			writer.WriteStartElement("DataSourceDefinition2");
			if (this.ConnectString != null)
			{
				writer.WriteElementString("ConnectString", this.ConnectString);
			}
			if (this.CredentialRetrieval != DataSourceDefinition2.CredentialRetrievalTypes.None)
			{
				writer.WriteElementString("CredentialRetrieval", this.CredentialRetrieval.ToString());
			}
			writer.WriteElementString("WindowsCredentials", XmlConvert.ToString(this.WindowsCredentials));
			writer.WriteElementString("ImpersonateUser", XmlConvert.ToString(this.ImpersonateUser));
			if (this.SecureStoreLookup != null)
			{
				this.SecureStoreLookup.WriteXml(writer);
			}
			if (this.PerspectiveName != null)
			{
				writer.WriteElementString("PerspectiveName", this.PerspectiveName);
			}
			if (this.OnPremiseModelIdentityClaims != null)
			{
				writer.WriteElementString("OnPremiseIdentity", this.OnPremiseModelIdentityClaims);
			}
			if (this.PowerBIModelId != 0L)
			{
				writer.WriteElementString("PowerBiModelId", this.PowerBIModelId.ToString());
			}
			if (this.PowerBIModelLastUpdatedTime != null)
			{
				writer.WriteStartElement("PowerBiModelLastUpdatedTime");
				writer.WriteValue(this.PowerBIModelLastUpdatedTime.Value);
				writer.WriteEndElement();
			}
			if (this.IsMultiDimensional)
			{
				writer.WriteElementString("IsMultiDimensional", XmlConvert.ToString(this.IsMultiDimensional));
			}
			if (this.IsFullyFormedExternalConnectionString)
			{
				writer.WriteElementString("IsFullyFormedExternalConnectionString", XmlConvert.ToString(this.IsFullyFormedExternalConnectionString));
			}
			writer.WriteEndElement();
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06001C68 RID: 7272 RVA: 0x00072D12 File Offset: 0x00070F12
		public bool IsOnPremiseModel
		{
			get
			{
				return !string.IsNullOrEmpty(this.OnPremiseModelIdentityClaims);
			}
		}

		// Token: 0x06001C69 RID: 7273 RVA: 0x00072D24 File Offset: 0x00070F24
		public void ReadXml(XmlReader reader)
		{
			while (reader.Read())
			{
				if (!reader.IsEmptyElement)
				{
					if (reader.NodeType == XmlNodeType.Element)
					{
						string localName = reader.LocalName;
						uint num = global::<PrivateImplementationDetails>.ComputeStringHash(localName);
						if (num <= 591018678U)
						{
							if (num <= 349881781U)
							{
								if (num != 67574007U)
								{
									if (num == 349881781U)
									{
										if (localName == "ImpersonateUser")
										{
											reader.Read();
											this.ImpersonateUser = reader.ReadContentAsBoolean();
											continue;
										}
									}
								}
								else if (localName == "SecureStoreLookup")
								{
									this.SecureStoreLookup = new SecureStoreLookup();
									this.SecureStoreLookup.ReadXml(reader);
									continue;
								}
							}
							else if (num != 481858228U)
							{
								if (num != 545517886U)
								{
									if (num == 591018678U)
									{
										if (localName == "PerspectiveName")
										{
											reader.Read();
											this.PerspectiveName = reader.ReadContentAsString();
											continue;
										}
									}
								}
								else if (localName == "IsFullyFormedExternalConnectionString")
								{
									reader.Read();
									this.IsFullyFormedExternalConnectionString = reader.ReadContentAsBoolean();
									continue;
								}
							}
							else if (localName == "ConnectString")
							{
								reader.Read();
								this.ConnectString = reader.ReadContentAsString();
								continue;
							}
						}
						else if (num <= 3333713617U)
						{
							if (num != 1505415362U)
							{
								if (num != 3051021484U)
								{
									if (num == 3333713617U)
									{
										if (localName == "PowerBiModelId")
										{
											reader.Read();
											this.PowerBIModelId = reader.ReadContentAsLong();
											continue;
										}
									}
								}
								else if (localName == "WindowsCredentials")
								{
									reader.Read();
									this.WindowsCredentials = reader.ReadContentAsBoolean();
									continue;
								}
							}
							else if (localName == "PowerBiModelLastUpdatedTime")
							{
								reader.Read();
								this.PowerBIModelLastUpdatedTime = new DateTime?(reader.ReadContentAsDateTime());
								continue;
							}
						}
						else if (num != 3512981405U)
						{
							if (num != 4117838964U)
							{
								if (num == 4223616647U)
								{
									if (localName == "IsMultiDimensional")
									{
										reader.Read();
										this.IsMultiDimensional = reader.ReadContentAsBoolean();
										continue;
									}
								}
							}
							else if (localName == "CredentialRetrieval")
							{
								reader.Read();
								this.CredentialRetrieval = this.ReadCredentialRetrieval(reader);
								continue;
							}
						}
						else if (localName == "OnPremiseIdentity")
						{
							reader.Read();
							this.OnPremiseModelIdentityClaims = reader.ReadContentAsString();
							continue;
						}
						DSD2ErrorHandlingUtils.ThrowIfUnrecognizedElement(reader.LocalName);
					}
					else if (reader.NodeType == XmlNodeType.EndElement && reader.LocalName == "DataSourceDefinition2")
					{
						break;
					}
				}
			}
		}

		// Token: 0x06001C6A RID: 7274 RVA: 0x00073018 File Offset: 0x00071218
		private DataSourceDefinition2.CredentialRetrievalTypes ReadCredentialRetrieval(XmlReader reader)
		{
			try
			{
				return (DataSourceDefinition2.CredentialRetrievalTypes)Enum.Parse(typeof(DataSourceDefinition2.CredentialRetrievalTypes), reader.ReadContentAsString(), false);
			}
			catch (ArgumentException)
			{
				DSD2ErrorHandlingUtils.ThrowIfInavalidElement(reader.LocalName);
			}
			return DataSourceDefinition2.CredentialRetrievalTypes.None;
		}

		// Token: 0x04000BB9 RID: 3001
		internal const string DATASOURCEDEFINITION2 = "DataSourceDefinition2";

		// Token: 0x04000BBA RID: 3002
		internal const string CONNECTSTRING = "ConnectString";

		// Token: 0x04000BBB RID: 3003
		internal const string CREDENTIALRETRIEVAL = "CredentialRetrieval";

		// Token: 0x04000BBC RID: 3004
		internal const string WINDOWSCREDENTIALS = "WindowsCredentials";

		// Token: 0x04000BBD RID: 3005
		internal const string IMPERSONATEUSER = "ImpersonateUser";

		// Token: 0x04000BBE RID: 3006
		internal const string SECURESTORELOOKUP = "SecureStoreLookup";

		// Token: 0x04000BBF RID: 3007
		internal const string PERSPECTIVENAME = "PerspectiveName";

		// Token: 0x04000BC0 RID: 3008
		internal const string ONPREMISEIDENTITY = "OnPremiseIdentity";

		// Token: 0x04000BC1 RID: 3009
		internal const string POWERBIMODELID = "PowerBiModelId";

		// Token: 0x04000BC2 RID: 3010
		internal const string POWERBIMODELLASTUPDATEDTIME = "PowerBiModelLastUpdatedTime";

		// Token: 0x04000BC3 RID: 3011
		internal const string ISMULTIDIMENSIONAL = "IsMultiDimensional";

		// Token: 0x04000BC4 RID: 3012
		internal const string ISFULLYFORMEDEXTERNALCONNECIONSTRING = "IsFullyFormedExternalConnectionString";

		// Token: 0x020004F6 RID: 1270
		public enum CredentialRetrievalTypes
		{
			// Token: 0x040011AA RID: 4522
			None,
			// Token: 0x040011AB RID: 4523
			Prompt,
			// Token: 0x040011AC RID: 4524
			Store,
			// Token: 0x040011AD RID: 4525
			Integrated,
			// Token: 0x040011AE RID: 4526
			ServiceAccount,
			// Token: 0x040011AF RID: 4527
			SecureStore
		}
	}
}
