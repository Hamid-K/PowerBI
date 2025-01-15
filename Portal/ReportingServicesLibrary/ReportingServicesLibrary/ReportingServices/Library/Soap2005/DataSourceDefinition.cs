using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library.Soap2005
{
	// Token: 0x02000311 RID: 785
	public class DataSourceDefinition : DataSourceDefinitionOrReference
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x0006E314 File Offset: 0x0006C514
		internal static DataSourceInfo ThisToDataSourceInfo(string name, string originalName, DataSourceDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			DataSourceInfo dataSourceInfo = new DataSourceInfo(name, originalName);
			dataSourceInfo.Extension = definition.Extension;
			if (definition.UseOriginalConnectString)
			{
				dataSourceInfo.SetConnectionString(null, DataProtection.Instance);
			}
			else
			{
				dataSourceInfo.SetConnectionString(definition.ConnectString, DataProtection.Instance);
			}
			dataSourceInfo.CredentialsRetrieval = (DataSourceInfo.CredentialsRetrievalOption)Enum.Parse(typeof(DataSourceInfo.CredentialsRetrievalOption), definition.CredentialRetrieval.ToString());
			dataSourceInfo.WindowsCredentials = definition.WindowsCredentials;
			if (definition.ImpersonateUserSpecified)
			{
				dataSourceInfo.ImpersonateUser = definition.ImpersonateUser;
			}
			if (definition.Prompt != null)
			{
				dataSourceInfo.Prompt = definition.Prompt;
			}
			if (definition.UserName != null)
			{
				dataSourceInfo.SetUserName(definition.UserName, DataProtection.Instance);
			}
			if (definition.Password != null)
			{
				dataSourceInfo.SetPassword(definition.Password, DataProtection.Instance);
			}
			if (definition.EnabledSpecified)
			{
				dataSourceInfo.Enabled = definition.Enabled;
			}
			dataSourceInfo.ValidateDefinition(definition.UseOriginalConnectString);
			return dataSourceInfo;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x0006E414 File Offset: 0x0006C614
		internal static DataSourceDefinition DataSourceInfoToThis(DataSourceInfo dsi, bool getPassword, bool encrypted = false)
		{
			DataSourceDefinition dataSourceDefinition = new DataSourceDefinition();
			dataSourceDefinition.Extension = dsi.Extension;
			dataSourceDefinition.UseOriginalConnectString = dsi.UseOriginalConnectionString;
			if (dataSourceDefinition.UseOriginalConnectString)
			{
				dataSourceDefinition.ConnectString = dsi.GetOriginalConnectionString(DataProtection.Instance);
			}
			else
			{
				dataSourceDefinition.ConnectString = dsi.GetConnectionString(DataProtection.Instance);
			}
			dataSourceDefinition.OriginalConnectStringExpressionBased = dsi.OriginalConnectStringExpressionBased;
			try
			{
				dataSourceDefinition.CredentialRetrieval = (CredentialRetrievalEnum)Enum.Parse(typeof(CredentialRetrievalEnum), dsi.CredentialsRetrieval.ToString());
			}
			catch (ArgumentException)
			{
				throw new InvalidParameterException("CredentialRetrieval");
			}
			dataSourceDefinition.WindowsCredentials = dsi.WindowsCredentials;
			dataSourceDefinition.ImpersonateUser = dsi.ImpersonateUser;
			dataSourceDefinition.ImpersonateUserSpecified = true;
			dataSourceDefinition.Prompt = dsi.Prompt;
			dataSourceDefinition.UserName = dsi.GetUserName(DataProtection.Instance);
			if (getPassword)
			{
				if (encrypted)
				{
					dataSourceDefinition.Password = ((dsi.PasswordEncrypted == null) ? null : Convert.ToBase64String(dsi.PasswordEncrypted));
				}
				else
				{
					dataSourceDefinition.Password = dsi.GetPasswordDecrypted(DataProtection.Instance);
				}
			}
			else
			{
				dataSourceDefinition.Password = null;
			}
			dataSourceDefinition.Enabled = dsi.Enabled;
			dataSourceDefinition.EnabledSpecified = true;
			return dataSourceDefinition;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x0006E554 File Offset: 0x0006C754
		internal static DataSourceDefinition XmlToThis(string definitionXml)
		{
			if (definitionXml == null)
			{
				return null;
			}
			return DataSourceDefinition.DataSourceInfoToThis(new DataSourceInfo("", "", definitionXml, DataProtection.Instance), true, false);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x0006E578 File Offset: 0x0006C778
		internal static string ThisToXml(DataSourceDefinition definition)
		{
			if (definition == null)
			{
				return null;
			}
			StringWriter stringWriter = new StringWriter(Localization.CatalogCulture);
			XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
			DataSourceDefinition.WriteToXml(definition, xmlTextWriter);
			return stringWriter.ToString();
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x0006E5A8 File Offset: 0x0006C7A8
		internal static void WriteToXml(DataSourceDefinition definition, XmlTextWriter writer)
		{
			writer.WriteStartElement("DataSourceDefinition");
			if (definition.Extension != null)
			{
				writer.WriteElementString("Extension", definition.Extension);
			}
			if (definition.ConnectString != null)
			{
				writer.WriteElementString("ConnectString", definition.ConnectString);
			}
			writer.WriteElementString("UseOriginalConnectString", definition.UseOriginalConnectString ? "True" : "False");
			writer.WriteElementString("OriginalConnectStringExpressionBased", definition.OriginalConnectStringExpressionBased ? "True" : "False");
			writer.WriteElementString("CredentialRetrieval", definition.CredentialRetrieval.ToString());
			if (definition.CredentialRetrieval == CredentialRetrievalEnum.Prompt || definition.CredentialRetrieval == CredentialRetrievalEnum.Store)
			{
				writer.WriteElementString("WindowsCredentials", definition.WindowsCredentials.ToString());
			}
			if (definition.Prompt != null && definition.CredentialRetrieval == CredentialRetrievalEnum.Prompt)
			{
				writer.WriteElementString("Prompt", definition.Prompt);
			}
			if (definition.CredentialRetrieval == CredentialRetrievalEnum.Store)
			{
				if (definition.ImpersonateUserSpecified)
				{
					writer.WriteElementString("ImpersonateUser", definition.ImpersonateUser.ToString());
				}
				if (definition.UserName != null)
				{
					writer.WriteElementString("UserName", definition.UserName);
				}
				if (definition.Password != null)
				{
					writer.WriteElementString("Password", definition.Password);
				}
			}
			if (definition.EnabledSpecified)
			{
				writer.WriteElementString("Enabled", definition.Enabled.ToString());
			}
			writer.WriteEndElement();
		}

		// Token: 0x04000A90 RID: 2704
		public string Extension;

		// Token: 0x04000A91 RID: 2705
		public string ConnectString;

		// Token: 0x04000A92 RID: 2706
		public bool UseOriginalConnectString;

		// Token: 0x04000A93 RID: 2707
		public bool OriginalConnectStringExpressionBased;

		// Token: 0x04000A94 RID: 2708
		public CredentialRetrievalEnum CredentialRetrieval;

		// Token: 0x04000A95 RID: 2709
		public bool WindowsCredentials;

		// Token: 0x04000A96 RID: 2710
		public bool ImpersonateUser;

		// Token: 0x04000A97 RID: 2711
		[XmlIgnore]
		public bool ImpersonateUserSpecified;

		// Token: 0x04000A98 RID: 2712
		public string Prompt;

		// Token: 0x04000A99 RID: 2713
		public string UserName;

		// Token: 0x04000A9A RID: 2714
		public string Password;

		// Token: 0x04000A9B RID: 2715
		public bool Enabled;

		// Token: 0x04000A9C RID: 2716
		[XmlIgnore]
		public bool EnabledSpecified;
	}
}
