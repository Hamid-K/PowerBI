using System;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B3 RID: 1459
	[Serializable]
	public sealed class DataSourceInfo
	{
		// Token: 0x06005271 RID: 21105 RVA: 0x0015B9E4 File Offset: 0x00159BE4
		public void LinkToStandAlone(DataSourceInfo standAlone, string standAlonePath, Guid standAloneCatalogItemId)
		{
			this.m_name = standAlone.m_name;
			this.m_extension = standAlone.m_extension;
			this.m_connectionStringEncrypted = standAlone.m_connectionStringEncrypted;
			this.m_dataSourceReference = standAlonePath;
			this.m_linkID = standAloneCatalogItemId;
			this.m_secDesc = standAlone.m_secDesc;
			this.m_credentialsRetrieval = standAlone.CredentialsRetrieval;
			this.m_prompt = standAlone.m_prompt;
			this.m_userNameEncrypted = standAlone.m_userNameEncrypted;
			this.m_passwordEncrypted = standAlone.m_passwordEncrypted;
			this.Enabled = standAlone.Enabled;
			this.ImpersonateUser = standAlone.ImpersonateUser;
			this.WindowsCredentials = standAlone.WindowsCredentials;
		}

		// Token: 0x06005272 RID: 21106 RVA: 0x0015BA84 File Offset: 0x00159C84
		public void LinkModelToDataSource(DataSourceInfo standAlone, Guid modelID)
		{
			this.m_DataSourceWithCredentialsId = standAlone.m_DataSourceWithCredentialsId;
			this.m_extension = standAlone.m_extension;
			this.m_connectionStringEncrypted = standAlone.m_connectionStringEncrypted;
			this.m_credentialsRetrieval = standAlone.CredentialsRetrieval;
			this.m_prompt = standAlone.Prompt;
			this.m_userNameEncrypted = standAlone.m_userNameEncrypted;
			this.m_passwordEncrypted = standAlone.m_passwordEncrypted;
			this.Enabled = standAlone.Enabled;
			this.ImpersonateUser = standAlone.ImpersonateUser;
			this.m_flags = standAlone.m_flags;
			this.m_modelID = modelID;
			this.m_isEmbeddedInModel = false;
			this.IsModel = true;
		}

		// Token: 0x06005273 RID: 21107 RVA: 0x0015BB1E File Offset: 0x00159D1E
		public void InitializeAsEmbeddedInModel(Guid modelID)
		{
			this.m_modelID = modelID;
			this.m_isEmbeddedInModel = true;
			this.IsModel = true;
		}

		// Token: 0x06005274 RID: 21108 RVA: 0x0015BB38 File Offset: 0x00159D38
		public void CopyFrom(DataSourceInfo copy, string referencePath, Guid linkToCatalogItemId, bool isEmbeddedInModel)
		{
			this.LinkToStandAlone(copy, referencePath, linkToCatalogItemId);
			this.m_flags = copy.m_flags;
			this.m_modelID = copy.ModelID;
			this.m_modelLastUpdatedTime = copy.ModelLastUpdatedTime;
			this.m_isEmbeddedInModel = isEmbeddedInModel;
			if (isEmbeddedInModel)
			{
				this.IsModel = true;
			}
		}

		// Token: 0x06005275 RID: 21109 RVA: 0x0015BB88 File Offset: 0x00159D88
		public DataSourceInfo(SerializationInfo info, StreamingContext context)
		{
			this.m_id = (Guid)info.GetValue("id", typeof(Guid));
			this.m_DataSourceWithCredentialsId = (Guid)info.GetValue("originalid", typeof(Guid));
			this.m_name = (string)info.GetValue("name", typeof(string));
			this.m_originalName = (string)info.GetValue("originalname", typeof(string));
			this.m_extension = (string)info.GetValue("extension", typeof(string));
			this.m_connectionStringEncrypted = (byte[])info.GetValue("connectionstringencrypted", typeof(byte[]));
			this.m_originalConnectionStringEncrypted = (byte[])info.GetValue("originalconnectionstringencrypted", typeof(byte[]));
			this.m_originalConnectStringExpressionBased = (bool)info.GetValue("originalConnectStringExpressionBased", typeof(bool));
			this.m_dataSourceReference = (string)info.GetValue("datasourcereference", typeof(string));
			this.m_linkID = (Guid)info.GetValue("linkid", typeof(Guid));
			this.m_secDesc = (byte[])info.GetValue("secdesc", typeof(byte[]));
			this.m_credentialsRetrieval = (DataSourceInfo.CredentialsRetrievalOption)info.GetValue("credentialsretrieval", typeof(DataSourceInfo.CredentialsRetrievalOption));
			this.m_prompt = (string)info.GetValue("prompt", typeof(string));
			this.m_userNameEncrypted = (byte[])info.GetValue("usernameencrypted", typeof(byte[]));
			this.m_passwordEncrypted = (byte[])info.GetValue("passwordencrypted", typeof(byte[]));
			this.m_flags = (DataSourceInfo.DataSourceFlags)info.GetValue("datasourceflags", typeof(DataSourceInfo.DataSourceFlags));
			this.m_modelID = (Guid)info.GetValue("modelid", typeof(Guid));
			this.m_modelLastUpdatedTime = (DateTime?)info.GetValue("modellastupdatedtime", typeof(DateTime?));
			this.m_isEmbeddedInModel = (bool)info.GetValue("isembeddedinmodel", typeof(bool));
		}

		// Token: 0x06005276 RID: 21110 RVA: 0x0015BE06 File Offset: 0x0015A006
		public static DataSourceInfo ParseDataSourceNode(XmlNode node, bool clientLoad, IDataProtection dataProtection)
		{
			return DataSourceInfo.ParseDataSourceNode(node, clientLoad, false, dataProtection);
		}

		// Token: 0x06005277 RID: 21111 RVA: 0x0015BE14 File Offset: 0x0015A014
		public static DataSourceInfo ParseDataSourceNode(XmlNode node, bool clientLoad, bool allowNoName, IDataProtection dataProtection)
		{
			if (node.Name != "DataSource")
			{
				throw new InvalidXmlException();
			}
			XmlNode xmlNode = node.SelectSingleNode("Name");
			node.SelectSingleNode("Extension");
			XmlNode xmlNode2 = node.SelectSingleNode("DataSourceDefinition");
			XmlNode xmlNode3 = node.SelectSingleNode("DataSourceReference");
			DataSourceInfo dataSourceInfo = null;
			if ((!allowNoName && xmlNode == null) || xmlNode2 == null == (xmlNode3 == null))
			{
				bool flag = true;
				if (clientLoad && node.SelectSingleNode("InvalidDataSourceReference") != null)
				{
					flag = false;
					dataSourceInfo = new DataSourceInfo((xmlNode == null) ? "" : xmlNode.InnerText);
				}
				if (flag)
				{
					throw new InvalidXmlException();
				}
			}
			string text = ((xmlNode == null) ? "" : xmlNode.InnerText);
			if (xmlNode2 != null)
			{
				dataSourceInfo = new DataSourceInfo(text, text, xmlNode2.OuterXml, dataProtection);
			}
			else if (xmlNode3 != null)
			{
				dataSourceInfo = new DataSourceInfo(text, xmlNode3.InnerText, Guid.Empty);
			}
			return dataSourceInfo;
		}

		// Token: 0x06005278 RID: 21112 RVA: 0x0015BEF0 File Offset: 0x0015A0F0
		public DataSourceInfo(string name, string originalName, string dataSourceDefinition, IDataProtection dataProtection)
			: this(name, originalName)
		{
			XmlDocument xmlDocument = XmlUtil.CreateXmlDocumentWithNullResolver();
			XmlNode xmlNode = null;
			try
			{
				XmlUtil.SafeOpenXmlDocumentString(xmlDocument, dataSourceDefinition);
			}
			catch (XmlException ex)
			{
				throw new MalformedXmlException(ex);
			}
			try
			{
				xmlNode = xmlDocument.SelectSingleNode("/DataSourceDefinition");
				if (xmlNode == null)
				{
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
					xmlNamespaceManager.AddNamespace("rds", DataSourceInfo.GetXmlNamespace());
					xmlNode = xmlDocument.SelectSingleNode("/rds:" + DataSourceInfo.GetDataSourceDefinitionXmlTag(), xmlNamespaceManager);
				}
			}
			catch (XmlException)
			{
				throw new InvalidXmlException();
			}
			this.ParseAndValidate(xmlNode, dataProtection);
		}

		// Token: 0x06005279 RID: 21113 RVA: 0x0015BF90 File Offset: 0x0015A190
		public DataSourceInfo(string name, string originalName, XmlNode root, IDataProtection dataProtection)
			: this(name, originalName)
		{
			this.ParseAndValidate(root, dataProtection);
		}

		// Token: 0x0600527A RID: 21114 RVA: 0x0015BFA4 File Offset: 0x0015A1A4
		public DataSourceInfo(string name, string linkPath, Guid linkId, DataSourceInfo standAloneDatasource)
		{
			this.m_id = Guid.NewGuid();
			this.m_name = name;
			this.m_originalName = name;
			this.m_DataSourceWithCredentialsId = standAloneDatasource.m_DataSourceWithCredentialsId;
			this.InitDefaultsOnCreation();
			this.LinkToStandAlone(standAloneDatasource, linkPath, linkId);
			if (standAloneDatasource.IsModel)
			{
				this.IsModel = true;
				this.m_modelID = standAloneDatasource.ModelID;
			}
		}

		// Token: 0x0600527B RID: 21115 RVA: 0x0015C015 File Offset: 0x0015A215
		public DataSourceInfo(string name, string originalName)
		{
			this.m_id = Guid.NewGuid();
			this.m_name = name;
			this.m_originalName = originalName;
			this.InitDefaultsOnCreation();
		}

		// Token: 0x0600527C RID: 21116 RVA: 0x0015C048 File Offset: 0x0015A248
		public void ValidateDefinition(bool useOriginalConnectString)
		{
			if (this.Extension == null)
			{
				throw new MissingElementException("Extension");
			}
			if (this.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Store && this.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Prompt)
			{
				this.WindowsCredentials = false;
			}
			if (this.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Store)
			{
				if (this.m_userNameEncrypted != null)
				{
					throw new InvalidElementCombinationException("UserName", "CredentialRetrieval");
				}
				if (this.m_passwordEncrypted != null)
				{
					throw new InvalidElementCombinationException("Password", "CredentialRetrieval");
				}
			}
			else if (this.m_userNameEncrypted == null)
			{
				throw new MissingElementException("UserName");
			}
			if (this.ImpersonateUser && this.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Store && this.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.ServiceAccount)
			{
				throw new InvalidElementCombinationException("ImpersonateUser", "CredentialRetrieval");
			}
			if (!useOriginalConnectString && this.ConnectionStringEncrypted == null)
			{
				throw new MissingElementException("ConnectString");
			}
			if (this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Unknown)
			{
				throw new MissingElementException("CredentialRetrieval");
			}
			if (this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.ServiceAccount && !this.ImpersonateUser)
			{
				throw new InvalidElementCombinationException("CredentialRetrieval", "ImpersonateUser");
			}
			if (this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.SecureStore)
			{
				if (this.SecureStoreLookup == null)
				{
					throw new MissingElementException("SecureStoreLookup");
				}
				if (this.SecureStoreLookup.TargetApplicationId == null)
				{
					throw new MissingElementException("TargetApplicationId");
				}
			}
			if (this.SecureStoreLookup != null && this.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.SecureStore)
			{
				throw new InvalidElementCombinationException("SecureStoreLookup", "CredentialRetrieval");
			}
		}

		// Token: 0x0600527D RID: 21117 RVA: 0x0015C198 File Offset: 0x0015A398
		public DataSourceInfo(string originalName, string extension, string connectionString, bool integratedSecurity, string prompt, IDataProtection dataProtection)
		{
			this.m_id = Guid.NewGuid();
			this.m_name = originalName;
			this.m_originalName = originalName;
			this.InitDefaultsOnCreation();
			this.m_prompt = prompt;
			if (integratedSecurity)
			{
				this.m_credentialsRetrieval = DataSourceInfo.CredentialsRetrievalOption.Integrated;
			}
			else
			{
				this.m_credentialsRetrieval = DataSourceInfo.CredentialsRetrievalOption.Prompt;
			}
			this.m_extension = extension;
			this.SetConnectionString(connectionString, dataProtection);
		}

		// Token: 0x0600527E RID: 21118 RVA: 0x0015C204 File Offset: 0x0015A404
		public DataSourceInfo(string originalName, string extension, string connectionString, bool originalConnectStringExpressionBased, bool integratedSecurity, string prompt, IDataProtection dataProtection)
		{
			this.m_id = Guid.NewGuid();
			this.m_name = originalName;
			this.m_originalName = originalName;
			this.InitDefaultsOnCreation();
			this.m_prompt = prompt;
			if (integratedSecurity)
			{
				this.m_credentialsRetrieval = DataSourceInfo.CredentialsRetrievalOption.Integrated;
			}
			else
			{
				this.m_credentialsRetrieval = DataSourceInfo.CredentialsRetrievalOption.Prompt;
			}
			this.m_extension = extension;
			this.SetOriginalConnectionString(connectionString, dataProtection);
			this.m_originalConnectStringExpressionBased = originalConnectStringExpressionBased;
		}

		// Token: 0x0600527F RID: 21119 RVA: 0x0015C278 File Offset: 0x0015A478
		public DataSourceInfo(string originalName, string referenceName, Guid linkID)
		{
			this.m_id = Guid.NewGuid();
			this.m_name = originalName;
			this.m_originalName = originalName;
			this.InitDefaultsOnCreation();
			this.m_credentialsRetrieval = DataSourceInfo.CredentialsRetrievalOption.Prompt;
			this.m_dataSourceReference = referenceName;
			this.m_linkID = linkID;
		}

		// Token: 0x06005280 RID: 21120 RVA: 0x0015C2CA File Offset: 0x0015A4CA
		public DataSourceInfo(string originalName, string referenceName, Guid linkID, bool isEmbeddedInModel)
			: this(originalName, referenceName, linkID)
		{
			this.m_isEmbeddedInModel = isEmbeddedInModel;
			this.IsModel = true;
		}

		// Token: 0x06005281 RID: 21121 RVA: 0x0015C2E4 File Offset: 0x0015A4E4
		public DataSourceInfo(string originalName)
		{
			this.m_id = Guid.NewGuid();
			this.InitDefaultsOnCreation();
			this.OriginalName = originalName;
			this.ReferenceIsValid = false;
		}

		// Token: 0x06005282 RID: 21122 RVA: 0x0015C316 File Offset: 0x0015A516
		public DataSourceInfo(string originalName, bool isEmbeddedInModel)
			: this(originalName)
		{
			this.m_isEmbeddedInModel = isEmbeddedInModel;
			this.IsModel = true;
		}

		// Token: 0x06005283 RID: 21123 RVA: 0x0015C32D File Offset: 0x0015A52D
		public static string GetDataSourceReferenceXmlTag()
		{
			return "DataSourceReference";
		}

		// Token: 0x06005284 RID: 21124 RVA: 0x0015C334 File Offset: 0x0015A534
		public static string GetUserNameXmlTag()
		{
			return "UserName";
		}

		// Token: 0x06005285 RID: 21125 RVA: 0x0015C33B File Offset: 0x0015A53B
		public static string GetDataSourceDefinitionXmlTag()
		{
			return "DataSourceDefinition";
		}

		// Token: 0x06005286 RID: 21126 RVA: 0x0015C342 File Offset: 0x0015A542
		public static string GetXmlNamespace()
		{
			return "http://schemas.microsoft.com/sqlserver/reporting/2006/03/reportdatasource";
		}

		// Token: 0x06005287 RID: 21127 RVA: 0x0015C349 File Offset: 0x0015A549
		public static string GetEnabledXmlTag()
		{
			return "Enabled";
		}

		// Token: 0x17001EAB RID: 7851
		// (get) Token: 0x06005288 RID: 21128 RVA: 0x0015C350 File Offset: 0x0015A550
		// (set) Token: 0x06005289 RID: 21129 RVA: 0x0015C358 File Offset: 0x0015A558
		public Guid ID
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x17001EAC RID: 7852
		// (get) Token: 0x0600528A RID: 21130 RVA: 0x0015C361 File Offset: 0x0015A561
		// (set) Token: 0x0600528B RID: 21131 RVA: 0x0015C369 File Offset: 0x0015A569
		public Guid DataSourceWithCredentialsID
		{
			get
			{
				return this.m_DataSourceWithCredentialsId;
			}
			set
			{
				this.m_DataSourceWithCredentialsId = value;
			}
		}

		// Token: 0x17001EAD RID: 7853
		// (get) Token: 0x0600528C RID: 21132 RVA: 0x0015C372 File Offset: 0x0015A572
		// (set) Token: 0x0600528D RID: 21133 RVA: 0x0015C37A File Offset: 0x0015A57A
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001EAE RID: 7854
		// (get) Token: 0x0600528E RID: 21134 RVA: 0x0015C383 File Offset: 0x0015A583
		public string PromptIdentifier
		{
			get
			{
				return this.OriginalName;
			}
		}

		// Token: 0x17001EAF RID: 7855
		// (get) Token: 0x0600528F RID: 21135 RVA: 0x0015C38B File Offset: 0x0015A58B
		// (set) Token: 0x06005290 RID: 21136 RVA: 0x0015C393 File Offset: 0x0015A593
		public string OriginalName
		{
			get
			{
				return this.m_originalName;
			}
			set
			{
				this.m_originalName = value;
			}
		}

		// Token: 0x17001EB0 RID: 7856
		// (get) Token: 0x06005291 RID: 21137 RVA: 0x0015C39C File Offset: 0x0015A59C
		// (set) Token: 0x06005292 RID: 21138 RVA: 0x0015C3A4 File Offset: 0x0015A5A4
		public string Extension
		{
			get
			{
				return this.m_extension;
			}
			set
			{
				this.m_extension = value;
			}
		}

		// Token: 0x17001EB1 RID: 7857
		// (get) Token: 0x06005293 RID: 21139 RVA: 0x0015C3AD File Offset: 0x0015A5AD
		public byte[] ConnectionStringEncrypted
		{
			get
			{
				return this.m_connectionStringEncrypted;
			}
		}

		// Token: 0x17001EB2 RID: 7858
		// (get) Token: 0x06005294 RID: 21140 RVA: 0x0015C3B5 File Offset: 0x0015A5B5
		public bool UseOriginalConnectionString
		{
			get
			{
				return this.m_connectionStringEncrypted == null;
			}
		}

		// Token: 0x17001EB3 RID: 7859
		// (get) Token: 0x06005295 RID: 21141 RVA: 0x0015C3C0 File Offset: 0x0015A5C0
		public byte[] OriginalConnectionStringEncrypted
		{
			get
			{
				return this.m_originalConnectionStringEncrypted;
			}
		}

		// Token: 0x17001EB4 RID: 7860
		// (get) Token: 0x06005296 RID: 21142 RVA: 0x0015C3C8 File Offset: 0x0015A5C8
		public bool OriginalConnectStringExpressionBased
		{
			get
			{
				return this.m_originalConnectStringExpressionBased;
			}
		}

		// Token: 0x06005297 RID: 21143 RVA: 0x0015C3D0 File Offset: 0x0015A5D0
		public string GetConnectionString(IDataProtection dataProtection)
		{
			return dataProtection.UnprotectDataToString(this.m_connectionStringEncrypted, "ConnectionString");
		}

		// Token: 0x06005298 RID: 21144 RVA: 0x0015C3E3 File Offset: 0x0015A5E3
		public string GetOriginalConnectionString(IDataProtection dataProtection)
		{
			return dataProtection.UnprotectDataToString(this.m_originalConnectionStringEncrypted, "OriginalConnectionString");
		}

		// Token: 0x06005299 RID: 21145 RVA: 0x0015C3F6 File Offset: 0x0015A5F6
		public void SetConnectionString(string connectionString, IDataProtection dataProtection)
		{
			this.SetConnectionStringUseridReference(DataSourceInfo.HasUseridReference(connectionString));
			this.m_connectionStringEncrypted = dataProtection.ProtectData(connectionString, "ConnectionString");
		}

		// Token: 0x0600529A RID: 21146 RVA: 0x0015C416 File Offset: 0x0015A616
		private void SetOriginalConnectionString(string connectionString, IDataProtection dataProtection)
		{
			this.SetConnectionStringUseridReference(DataSourceInfo.HasUseridReference(connectionString));
			this.m_originalConnectionStringEncrypted = dataProtection.ProtectData(connectionString, "OriginalConnectionString");
		}

		// Token: 0x0600529B RID: 21147 RVA: 0x0015C436 File Offset: 0x0015A636
		internal void SetOriginalConnectionString(byte[] connectionStringEncrypted)
		{
			this.m_originalConnectionStringEncrypted = connectionStringEncrypted;
		}

		// Token: 0x0600529C RID: 21148 RVA: 0x0015C43F File Offset: 0x0015A63F
		internal void SetOriginalConnectStringExpressionBased(bool expressionBased)
		{
			this.m_originalConnectStringExpressionBased = expressionBased;
		}

		// Token: 0x17001EB5 RID: 7861
		// (get) Token: 0x0600529D RID: 21149 RVA: 0x0015C448 File Offset: 0x0015A648
		// (set) Token: 0x0600529E RID: 21150 RVA: 0x0015C450 File Offset: 0x0015A650
		internal bool IsExternalDataSource
		{
			get
			{
				return this.m_isExternalDataSource;
			}
			set
			{
				this.m_isExternalDataSource = value;
			}
		}

		// Token: 0x17001EB6 RID: 7862
		// (get) Token: 0x0600529F RID: 21151 RVA: 0x0015C459 File Offset: 0x0015A659
		// (set) Token: 0x060052A0 RID: 21152 RVA: 0x0015C461 File Offset: 0x0015A661
		internal bool IsFullyFormedExternalDataSource
		{
			get
			{
				return this.m_isFullyFormedExternalDataSource;
			}
			set
			{
				this.m_isFullyFormedExternalDataSource = value;
			}
		}

		// Token: 0x17001EB7 RID: 7863
		// (get) Token: 0x060052A1 RID: 21153 RVA: 0x0015C46A File Offset: 0x0015A66A
		// (set) Token: 0x060052A2 RID: 21154 RVA: 0x0015C472 File Offset: 0x0015A672
		internal bool IsMultiDimensional
		{
			get
			{
				return this.m_isMultidimensional;
			}
			set
			{
				this.m_isMultidimensional = value;
			}
		}

		// Token: 0x17001EB8 RID: 7864
		// (get) Token: 0x060052A3 RID: 21155 RVA: 0x0015C47B File Offset: 0x0015A67B
		// (set) Token: 0x060052A4 RID: 21156 RVA: 0x0015C483 File Offset: 0x0015A683
		public string DataSourceReference
		{
			get
			{
				return this.m_dataSourceReference;
			}
			set
			{
				this.m_dataSourceReference = value;
			}
		}

		// Token: 0x17001EB9 RID: 7865
		// (get) Token: 0x060052A5 RID: 21157 RVA: 0x0015C48C File Offset: 0x0015A68C
		public Guid LinkID
		{
			get
			{
				return this.m_linkID;
			}
		}

		// Token: 0x17001EBA RID: 7866
		// (get) Token: 0x060052A6 RID: 21158 RVA: 0x0015C494 File Offset: 0x0015A694
		public bool ReferenceByPath
		{
			get
			{
				return this.DataSourceReference != null && this.LinkID == Guid.Empty && this.ReferenceIsValid;
			}
		}

		// Token: 0x17001EBB RID: 7867
		// (get) Token: 0x060052A7 RID: 21159 RVA: 0x0015C4B8 File Offset: 0x0015A6B8
		public bool IsReference
		{
			get
			{
				return this.DataSourceReference != null || this.LinkID != Guid.Empty || !this.ReferenceIsValid;
			}
		}

		// Token: 0x17001EBC RID: 7868
		// (get) Token: 0x060052A8 RID: 21160 RVA: 0x0015C4DF File Offset: 0x0015A6DF
		public byte[] SecurityDescriptor
		{
			get
			{
				return this.m_secDesc;
			}
		}

		// Token: 0x17001EBD RID: 7869
		// (get) Token: 0x060052A9 RID: 21161 RVA: 0x0015C4E7 File Offset: 0x0015A6E7
		// (set) Token: 0x060052AA RID: 21162 RVA: 0x0015C4EF File Offset: 0x0015A6EF
		public DataSourceInfo.CredentialsRetrievalOption CredentialsRetrieval
		{
			get
			{
				return this.m_credentialsRetrieval;
			}
			set
			{
				this.m_credentialsRetrieval = value;
			}
		}

		// Token: 0x17001EBE RID: 7870
		// (get) Token: 0x060052AB RID: 21163 RVA: 0x0015C4F8 File Offset: 0x0015A6F8
		// (set) Token: 0x060052AC RID: 21164 RVA: 0x0015C505 File Offset: 0x0015A705
		public bool ImpersonateUser
		{
			get
			{
				return (this.m_flags & DataSourceInfo.DataSourceFlags.ImpersonateUser) > (DataSourceInfo.DataSourceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= DataSourceInfo.DataSourceFlags.ImpersonateUser;
					return;
				}
				this.m_flags &= ~DataSourceInfo.DataSourceFlags.ImpersonateUser;
			}
		}

		// Token: 0x17001EBF RID: 7871
		// (get) Token: 0x060052AD RID: 21165 RVA: 0x0015C528 File Offset: 0x0015A728
		// (set) Token: 0x060052AE RID: 21166 RVA: 0x0015C530 File Offset: 0x0015A730
		public string Prompt
		{
			get
			{
				return this.m_prompt;
			}
			set
			{
				this.m_prompt = value;
			}
		}

		// Token: 0x17001EC0 RID: 7872
		// (get) Token: 0x060052AF RID: 21167 RVA: 0x0015C539 File Offset: 0x0015A739
		// (set) Token: 0x060052B0 RID: 21168 RVA: 0x0015C546 File Offset: 0x0015A746
		public bool WindowsCredentials
		{
			get
			{
				return (this.m_flags & DataSourceInfo.DataSourceFlags.WindowsCredentials) > (DataSourceInfo.DataSourceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= DataSourceInfo.DataSourceFlags.WindowsCredentials;
					return;
				}
				this.m_flags &= ~DataSourceInfo.DataSourceFlags.WindowsCredentials;
			}
		}

		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x060052B1 RID: 21169 RVA: 0x0015C569 File Offset: 0x0015A769
		public byte[] UserNameEncrypted
		{
			get
			{
				return this.m_userNameEncrypted;
			}
		}

		// Token: 0x060052B2 RID: 21170 RVA: 0x0015C571 File Offset: 0x0015A771
		public string GetUserName(IDataProtection dataProtection)
		{
			return dataProtection.UnprotectDataToString(this.m_userNameEncrypted, "UserName");
		}

		// Token: 0x060052B3 RID: 21171 RVA: 0x0015C584 File Offset: 0x0015A784
		public void SetUserName(string userName, IDataProtection dataProtection)
		{
			this.m_userNameEncrypted = dataProtection.ProtectData(userName, "UserName");
		}

		// Token: 0x060052B4 RID: 21172 RVA: 0x0015C598 File Offset: 0x0015A798
		public string GetUserNameOnly(IDataProtection dataProtection)
		{
			return DataSourceInfo.GetUserNameOnly(this.GetUserName(dataProtection));
		}

		// Token: 0x060052B5 RID: 21173 RVA: 0x0015C5A8 File Offset: 0x0015A7A8
		public static string GetUserNameOnly(string domainAndUserName)
		{
			if (domainAndUserName == null)
			{
				return null;
			}
			int num = domainAndUserName.IndexOf("\\", StringComparison.Ordinal);
			if (num < 0)
			{
				return domainAndUserName;
			}
			return domainAndUserName.Substring(num + 1);
		}

		// Token: 0x060052B6 RID: 21174 RVA: 0x0015C5D6 File Offset: 0x0015A7D6
		public string GetDomainOnly(IDataProtection dataProtection)
		{
			return DataSourceInfo.GetDomainOnly(this.GetUserName(dataProtection));
		}

		// Token: 0x060052B7 RID: 21175 RVA: 0x0015C5E4 File Offset: 0x0015A7E4
		public static string GetDomainOnly(string domainAndUserName)
		{
			if (domainAndUserName == null)
			{
				return null;
			}
			int num = domainAndUserName.IndexOf("\\", StringComparison.Ordinal);
			if (num < 0)
			{
				return null;
			}
			return domainAndUserName.Substring(0, num);
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x060052B8 RID: 21176 RVA: 0x0015C611 File Offset: 0x0015A811
		public byte[] PasswordEncrypted
		{
			get
			{
				return this.m_passwordEncrypted;
			}
		}

		// Token: 0x060052B9 RID: 21177 RVA: 0x0015C619 File Offset: 0x0015A819
		public SecureStringWrapper GetPassword(IDataProtection dataProtection)
		{
			if (this.m_passwordSecureString == null && this.m_passwordEncrypted != null)
			{
				this.m_passwordSecureString = new SecureStringWrapper(dataProtection.UnprotectDataToString(this.m_passwordEncrypted, "Password"));
			}
			return this.m_passwordSecureString;
		}

		// Token: 0x060052BA RID: 21178 RVA: 0x0015C650 File Offset: 0x0015A850
		public string GetPasswordDecrypted(IDataProtection dataProtection)
		{
			SecureStringWrapper password = this.GetPassword(dataProtection);
			if (password != null)
			{
				return password.ToString();
			}
			return null;
		}

		// Token: 0x060052BB RID: 21179 RVA: 0x0015C670 File Offset: 0x0015A870
		public void SetPassword(string password, IDataProtection dataProtection)
		{
			this.m_passwordEncrypted = dataProtection.ProtectData(password, "Password");
		}

		// Token: 0x060052BC RID: 21180 RVA: 0x0015C684 File Offset: 0x0015A884
		public void SetPassword(SecureString password, IDataProtection dataProtection)
		{
			this.m_passwordSecureString = new SecureStringWrapper(password);
		}

		// Token: 0x060052BD RID: 21181 RVA: 0x0015C692 File Offset: 0x0015A892
		public void SetPasswordFromDataSourceInfo(DataSourceInfo dsInfo)
		{
			this.m_passwordEncrypted = dsInfo.m_passwordEncrypted;
			if (dsInfo.m_passwordSecureString != null)
			{
				this.m_passwordSecureString = new SecureStringWrapper(dsInfo.m_passwordSecureString);
			}
		}

		// Token: 0x060052BE RID: 21182 RVA: 0x0015C6B9 File Offset: 0x0015A8B9
		public void ResetPassword()
		{
			this.m_passwordEncrypted = null;
			this.m_passwordSecureString = null;
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x060052BF RID: 21183 RVA: 0x0015C6C9 File Offset: 0x0015A8C9
		public SecureStoreLookup SecureStoreLookup
		{
			get
			{
				return this.m_secureStoreLookup;
			}
		}

		// Token: 0x060052C0 RID: 21184 RVA: 0x0015C6D1 File Offset: 0x0015A8D1
		public void SetSecureStoreLookupContext(SecureStoreLookup.LookupContextOptions lookupContext, string targetAppId)
		{
			this.m_secureStoreLookup = new SecureStoreLookup(lookupContext, targetAppId);
		}

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x060052C1 RID: 21185 RVA: 0x0015C6E0 File Offset: 0x0015A8E0
		public DataSourceFaultContext DataSourceFaultContext
		{
			get
			{
				return this.m_dataSourceFaultContext;
			}
		}

		// Token: 0x060052C2 RID: 21186 RVA: 0x0015C6E8 File Offset: 0x0015A8E8
		public void SetDataSourceFaultContext(ErrorCode errorCode, string errorString)
		{
			this.m_dataSourceFaultContext = new DataSourceFaultContext(errorCode, errorString);
		}

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x060052C3 RID: 21187 RVA: 0x0015C6F7 File Offset: 0x0015A8F7
		// (set) Token: 0x060052C4 RID: 21188 RVA: 0x0015C6FF File Offset: 0x0015A8FF
		public bool IsCredentialSet { get; set; }

		// Token: 0x17001EC6 RID: 7878
		// (get) Token: 0x060052C5 RID: 21189 RVA: 0x0015C708 File Offset: 0x0015A908
		// (set) Token: 0x060052C6 RID: 21190 RVA: 0x0015C715 File Offset: 0x0015A915
		public bool Enabled
		{
			get
			{
				return (this.m_flags & DataSourceInfo.DataSourceFlags.Enabled) > (DataSourceInfo.DataSourceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= DataSourceInfo.DataSourceFlags.Enabled;
					return;
				}
				this.m_flags &= ~DataSourceInfo.DataSourceFlags.Enabled;
			}
		}

		// Token: 0x17001EC7 RID: 7879
		// (get) Token: 0x060052C7 RID: 21191 RVA: 0x0015C738 File Offset: 0x0015A938
		// (set) Token: 0x060052C8 RID: 21192 RVA: 0x0015C745 File Offset: 0x0015A945
		public bool IsModel
		{
			get
			{
				return DataSourceInfo.StaticIsModel((int)this.m_flags);
			}
			set
			{
				if (value)
				{
					this.m_flags |= DataSourceInfo.DataSourceFlags.IsModel;
					return;
				}
				this.m_flags &= ~DataSourceInfo.DataSourceFlags.IsModel;
			}
		}

		// Token: 0x060052C9 RID: 21193 RVA: 0x0015C769 File Offset: 0x0015A969
		public static bool StaticIsModel(int flags)
		{
			return (flags & 16) != 0;
		}

		// Token: 0x17001EC8 RID: 7880
		// (get) Token: 0x060052CA RID: 21194 RVA: 0x0015C772 File Offset: 0x0015A972
		public bool IsModelSecurityUsed
		{
			get
			{
				return this.m_isModelSecurityUsed;
			}
		}

		// Token: 0x17001EC9 RID: 7881
		// (get) Token: 0x060052CB RID: 21195 RVA: 0x0015C77A File Offset: 0x0015A97A
		// (set) Token: 0x060052CC RID: 21196 RVA: 0x0015C782 File Offset: 0x0015A982
		public IServiceEndpoint ServiceEndpoint
		{
			get
			{
				return this.m_serviceEndpoint;
			}
			set
			{
				this.m_serviceEndpoint = value;
			}
		}

		// Token: 0x17001ECA RID: 7882
		// (get) Token: 0x060052CD RID: 21197 RVA: 0x0015C78B File Offset: 0x0015A98B
		// (set) Token: 0x060052CE RID: 21198 RVA: 0x0015C793 File Offset: 0x0015A993
		public string TenantName
		{
			get
			{
				return this.m_tenantName;
			}
			set
			{
				this.m_tenantName = value;
			}
		}

		// Token: 0x17001ECB RID: 7883
		// (get) Token: 0x060052CF RID: 21199 RVA: 0x0015C79C File Offset: 0x0015A99C
		// (set) Token: 0x060052D0 RID: 21200 RVA: 0x0015C7A4 File Offset: 0x0015A9A4
		public Guid ModelID
		{
			get
			{
				return this.m_modelID;
			}
			set
			{
				this.m_modelID = value;
			}
		}

		// Token: 0x17001ECC RID: 7884
		// (get) Token: 0x060052D1 RID: 21201 RVA: 0x0015C7AD File Offset: 0x0015A9AD
		// (set) Token: 0x060052D2 RID: 21202 RVA: 0x0015C7B5 File Offset: 0x0015A9B5
		public DateTime? ModelLastUpdatedTime
		{
			get
			{
				return this.m_modelLastUpdatedTime;
			}
			set
			{
				this.m_modelLastUpdatedTime = value;
			}
		}

		// Token: 0x17001ECD RID: 7885
		// (get) Token: 0x060052D3 RID: 21203 RVA: 0x0015C7BE File Offset: 0x0015A9BE
		// (set) Token: 0x060052D4 RID: 21204 RVA: 0x0015C7C6 File Offset: 0x0015A9C6
		public string ModelPerspectiveName
		{
			get
			{
				return this.m_modelPerspectiveName;
			}
			set
			{
				this.m_modelPerspectiveName = value;
			}
		}

		// Token: 0x060052D5 RID: 21205 RVA: 0x0015C7D0 File Offset: 0x0015A9D0
		public void ThrowIfNotUsable(ServerDataSourceSettings serverDatasourceSetting)
		{
			if (!this.Enabled)
			{
				throw new DataSourceDisabledException();
			}
			if (!this.ReferenceIsValid)
			{
				throw new InvalidDataSourceReferenceException(this.OriginalName);
			}
			if (!this.GoodForLiveExecution(serverDatasourceSetting.IsSurrogatePresent))
			{
				throw new InvalidDataSourceCredentialSettingException();
			}
			if (this.m_credentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Integrated && !serverDatasourceSetting.AllowIntegratedSecurity)
			{
				throw new WindowsIntegratedSecurityDisabledException();
			}
		}

		// Token: 0x060052D6 RID: 21206 RVA: 0x0015C82A File Offset: 0x0015AA2A
		public bool GoodForLiveExecution(bool isSurrogatePresent)
		{
			return this.ReferenceIsValid && this.Enabled && (isSurrogatePresent || this.CredentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.None);
		}

		// Token: 0x17001ECE RID: 7886
		// (get) Token: 0x060052D7 RID: 21207 RVA: 0x0015C84D File Offset: 0x0015AA4D
		// (set) Token: 0x060052D8 RID: 21208 RVA: 0x0015C85A File Offset: 0x0015AA5A
		public bool ReferenceIsValid
		{
			get
			{
				return (this.m_flags & DataSourceInfo.DataSourceFlags.ReferenceIsValid) > (DataSourceInfo.DataSourceFlags)0;
			}
			set
			{
				if (value)
				{
					this.m_flags |= DataSourceInfo.DataSourceFlags.ReferenceIsValid;
					return;
				}
				this.m_flags &= ~DataSourceInfo.DataSourceFlags.ReferenceIsValid;
			}
		}

		// Token: 0x17001ECF RID: 7887
		// (get) Token: 0x060052D9 RID: 21209 RVA: 0x0015C87D File Offset: 0x0015AA7D
		public bool NeedPrompt
		{
			get
			{
				return this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt && this.m_userNameEncrypted == null;
			}
		}

		// Token: 0x17001ED0 RID: 7888
		// (get) Token: 0x060052DA RID: 21210 RVA: 0x0015C894 File Offset: 0x0015AA94
		public int FlagsForCatalogSerialization
		{
			get
			{
				DataSourceInfo.DataSourceFlags dataSourceFlags = this.m_flags;
				if (!this.m_isEmbeddedInModel)
				{
					dataSourceFlags &= ~DataSourceInfo.DataSourceFlags.IsModel;
				}
				return (int)dataSourceFlags;
			}
		}

		// Token: 0x060052DB RID: 21211 RVA: 0x0015C8B8 File Offset: 0x0015AAB8
		public byte[] GetXmlBytes(IDataProtection dataProtection)
		{
			XmlWriterSettings xmlWriterSettings = new XmlWriterSettings();
			xmlWriterSettings.Indent = true;
			xmlWriterSettings.Encoding = Encoding.UTF8;
			MemoryStream memoryStream = new MemoryStream();
			XmlWriter xmlWriter = XmlWriter.Create(memoryStream, xmlWriterSettings);
			byte[] array;
			using (xmlWriter)
			{
				xmlWriter.WriteStartElement("DataSourceDefinition", "http://schemas.microsoft.com/sqlserver/reporting/2006/03/reportdatasource");
				xmlWriter.WriteElementString("Extension", this.Extension);
				xmlWriter.WriteElementString("ConnectString", this.GetConnectionString(dataProtection));
				xmlWriter.WriteElementString("CredentialRetrieval", this.CredentialsRetrieval.ToString());
				if (this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt || this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Store)
				{
					xmlWriter.WriteElementString("WindowsCredentials", this.WindowsCredentials.ToString());
				}
				if (this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Prompt)
				{
					xmlWriter.WriteElementString("Prompt", string.IsNullOrEmpty(this.Prompt) ? "" : this.Prompt);
				}
				if (this.CredentialsRetrieval == DataSourceInfo.CredentialsRetrievalOption.Store)
				{
					xmlWriter.WriteElementString("ImpersonateUser", this.ImpersonateUser.ToString());
				}
				xmlWriter.WriteElementString("Enabled", this.Enabled.ToString());
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060052DC RID: 21212 RVA: 0x0015CA18 File Offset: 0x0015AC18
		private bool ParseDefinitionXml(XmlNode root, IDataProtection dataProtection)
		{
			bool flag2;
			try
			{
				if (root == null)
				{
					throw new InvalidXmlException();
				}
				string text = null;
				bool flag = false;
				foreach (object obj in root.ChildNodes)
				{
					XmlNode xmlNode = (XmlNode)obj;
					string name = xmlNode.Name;
					string innerText = xmlNode.InnerText;
					if (name != null)
					{
						int length = name.Length;
						switch (length)
						{
						case 6:
							if (!(name == "Prompt"))
							{
								goto IL_0270;
							}
							goto IL_0234;
						case 7:
							if (!(name == "Enabled"))
							{
								goto IL_0270;
							}
							try
							{
								this.Enabled = bool.Parse(innerText);
								continue;
							}
							catch (FormatException)
							{
								throw new ElementTypeMismatchException("Enabled");
							}
							goto IL_0270;
						case 8:
						{
							char c = name[0];
							if (c != 'P')
							{
								if (c != 'U')
								{
									goto IL_0270;
								}
								if (!(name == "UserName"))
								{
									goto IL_0270;
								}
								this.SetUserName(innerText, dataProtection);
								continue;
							}
							else
							{
								if (!(name == "Password"))
								{
									goto IL_0270;
								}
								this.SetPassword(innerText, dataProtection);
								continue;
							}
							break;
						}
						case 9:
							if (!(name == "Extension"))
							{
								goto IL_0270;
							}
							this.Extension = innerText;
							continue;
						case 10:
						case 11:
						case 12:
						case 14:
						case 16:
						case 17:
							goto IL_0270;
						case 13:
							if (!(name == "ConnectString"))
							{
								goto IL_0270;
							}
							text = innerText;
							continue;
						case 15:
							if (!(name == "ImpersonateUser"))
							{
								goto IL_0270;
							}
							goto IL_0218;
						case 18:
							if (!(name == "WindowsCredentials"))
							{
								goto IL_0270;
							}
							goto IL_01FC;
						case 19:
							if (!(name == "CredentialRetrieval"))
							{
								goto IL_0270;
							}
							break;
						default:
							if (length != 24)
							{
								if (length != 36)
								{
									goto IL_0270;
								}
								if (!(name == "OriginalConnectStringExpressionBased"))
								{
									goto IL_0270;
								}
								continue;
							}
							else
							{
								if (!(name == "UseOriginalConnectString"))
								{
									goto IL_0270;
								}
								try
								{
									flag = bool.Parse(innerText);
									continue;
								}
								catch (ArgumentException)
								{
									throw new ElementTypeMismatchException("UseOriginalConnectString");
								}
							}
							break;
						}
						try
						{
							this.m_credentialsRetrieval = (DataSourceInfo.CredentialsRetrievalOption)Enum.Parse(typeof(DataSourceInfo.CredentialsRetrievalOption), innerText, true);
							continue;
						}
						catch (ArgumentException)
						{
							throw new ElementTypeMismatchException("CredentialRetrieval");
						}
						IL_01FC:
						try
						{
							this.WindowsCredentials = bool.Parse(innerText);
							continue;
						}
						catch (Exception)
						{
							throw new ElementTypeMismatchException("WindowsCredentials");
						}
						IL_0218:
						try
						{
							this.ImpersonateUser = bool.Parse(innerText);
							continue;
						}
						catch (FormatException)
						{
							throw new ElementTypeMismatchException("ImpersonateUser");
						}
						IL_0234:
						this.m_prompt = innerText;
						continue;
					}
					IL_0270:
					throw new InvalidXmlException();
				}
				if (flag)
				{
					this.SetConnectionString(null, dataProtection);
				}
				else
				{
					this.SetConnectionString(text, dataProtection);
				}
				flag2 = flag;
			}
			catch (XmlException)
			{
				throw new InvalidXmlException();
			}
			return flag2;
		}

		// Token: 0x060052DD RID: 21213 RVA: 0x0015CD8C File Offset: 0x0015AF8C
		private void ParseAndValidate(XmlNode root, IDataProtection dataProtection)
		{
			bool flag = this.ParseDefinitionXml(root, dataProtection);
			this.ValidateDefinition(flag);
		}

		// Token: 0x17001ED1 RID: 7889
		// (get) Token: 0x060052DE RID: 21214 RVA: 0x0015CDA9 File Offset: 0x0015AFA9
		public bool HasConnectionStringUseridReference
		{
			get
			{
				return (this.m_flags & DataSourceInfo.DataSourceFlags.ConnectionStringUseridReference) > (DataSourceInfo.DataSourceFlags)0;
			}
		}

		// Token: 0x060052DF RID: 21215 RVA: 0x0015CDB7 File Offset: 0x0015AFB7
		private void SetConnectionStringUseridReference(bool hasUseridReference)
		{
			if (hasUseridReference)
			{
				this.m_flags |= DataSourceInfo.DataSourceFlags.ConnectionStringUseridReference;
				return;
			}
			this.m_flags &= ~DataSourceInfo.DataSourceFlags.ConnectionStringUseridReference;
		}

		// Token: 0x060052E0 RID: 21216 RVA: 0x0015CDDB File Offset: 0x0015AFDB
		public static bool HasUseridReference(string connectionString)
		{
			return !string.IsNullOrEmpty(connectionString) && DataSourceInfo.UseridDetectionRegex.Matches(connectionString).Count > 0;
		}

		// Token: 0x060052E1 RID: 21217 RVA: 0x0015CDFA File Offset: 0x0015AFFA
		public static string ReplaceAllUseridReferences(string originalConnectionString, string useridReplacementString)
		{
			if (string.IsNullOrEmpty(originalConnectionString))
			{
				return originalConnectionString;
			}
			return DataSourceInfo.UseridDetectionRegex.Replace(originalConnectionString, useridReplacementString);
		}

		// Token: 0x17001ED2 RID: 7890
		// (get) Token: 0x060052E2 RID: 21218 RVA: 0x0015CE12 File Offset: 0x0015B012
		private static Regex UseridDetectionRegex
		{
			get
			{
				if (DataSourceInfo.m_useridDetectionRegex == null)
				{
					DataSourceInfo.m_useridDetectionRegex = new Regex("{{[\\s]*[uU][sS][eE][rR][iI][dD][\\s]*}}", RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline);
				}
				return DataSourceInfo.m_useridDetectionRegex;
			}
		}

		// Token: 0x060052E3 RID: 21219 RVA: 0x0015CE34 File Offset: 0x0015B034
		private void InitDefaultsOnCreation()
		{
			this.m_extension = null;
			this.m_dataSourceReference = null;
			this.m_linkID = Guid.Empty;
			this.m_credentialsRetrieval = DataSourceInfo.CredentialsRetrievalOption.Unknown;
			this.m_prompt = string.Format(CultureInfo.CurrentCulture, RPRes.rsDataSourcePrompt, Array.Empty<object>());
			this.m_userNameEncrypted = null;
			this.m_passwordEncrypted = null;
			this.m_flags = DataSourceInfo.DataSourceFlags.Enabled | DataSourceInfo.DataSourceFlags.ReferenceIsValid;
			this.m_originalConnectStringExpressionBased = false;
		}

		// Token: 0x060052E4 RID: 21220 RVA: 0x0015CE98 File Offset: 0x0015B098
		public DataSourceInfo(Guid id, Guid originalId, string name, string originalName, string extension, byte[] connectionStringEncrypted, byte[] originalConnectionStringEncypted, bool originalConnectStringExpressionBased, string dataSourceReference, Guid linkID, byte[] secDesc, DataSourceInfo.CredentialsRetrievalOption credentialsRetrieval, string prompt, byte[] userNameEncrypted, byte[] passwordEncrypted, int flags, bool isModelSecurityUsed)
		{
			this.m_id = id;
			this.m_DataSourceWithCredentialsId = originalId;
			this.m_name = name;
			this.m_originalName = originalName;
			this.m_extension = extension;
			this.m_connectionStringEncrypted = connectionStringEncrypted;
			this.m_originalConnectionStringEncrypted = originalConnectionStringEncypted;
			this.m_originalConnectStringExpressionBased = originalConnectStringExpressionBased;
			this.m_dataSourceReference = dataSourceReference;
			this.m_linkID = linkID;
			this.m_secDesc = secDesc;
			this.m_credentialsRetrieval = credentialsRetrieval;
			this.m_prompt = prompt;
			if (this.m_credentialsRetrieval != DataSourceInfo.CredentialsRetrievalOption.Store && (userNameEncrypted != null || passwordEncrypted != null))
			{
				throw new InternalCatalogException("unexpected data source type");
			}
			this.m_userNameEncrypted = userNameEncrypted;
			this.m_passwordEncrypted = passwordEncrypted;
			this.m_flags = (DataSourceInfo.DataSourceFlags)flags;
			this.m_isModelSecurityUsed = isModelSecurityUsed;
		}

		// Token: 0x04002999 RID: 10649
		private const DataSourceInfo.DataSourceFlags DefaultFlags = DataSourceInfo.DataSourceFlags.Enabled | DataSourceInfo.DataSourceFlags.ReferenceIsValid;

		// Token: 0x0400299B RID: 10651
		internal const string ExtensionXmlTag = "Extension";

		// Token: 0x0400299C RID: 10652
		internal const string ConnectionStringXmlTag = "ConnectString";

		// Token: 0x0400299D RID: 10653
		internal const string UseOriginalConnectStringXmlTag = "UseOriginalConnectString";

		// Token: 0x0400299E RID: 10654
		internal const string OriginalConnectStringExpressionBasedXmlTag = "OriginalConnectStringExpressionBased";

		// Token: 0x0400299F RID: 10655
		internal const string CredentialRetrievalXmlTag = "CredentialRetrieval";

		// Token: 0x040029A0 RID: 10656
		internal const string ImpersonateUserXmlTag = "ImpersonateUser";

		// Token: 0x040029A1 RID: 10657
		internal const string PromptXmlTag = "Prompt";

		// Token: 0x040029A2 RID: 10658
		internal const string WindowsCredentialsXmlTag = "WindowsCredentials";

		// Token: 0x040029A3 RID: 10659
		internal const string UserNameXmlTag = "UserName";

		// Token: 0x040029A4 RID: 10660
		internal const string PasswordXmlTag = "Password";

		// Token: 0x040029A5 RID: 10661
		internal const string EnabledXmlTag = "Enabled";

		// Token: 0x040029A6 RID: 10662
		internal const string NameXmlTag = "Name";

		// Token: 0x040029A7 RID: 10663
		internal const string SecureStoreLookupXmlTag = "SecureStoreLookup";

		// Token: 0x040029A8 RID: 10664
		internal const string TargetApplicationIdXmlTag = "TargetApplicationId";

		// Token: 0x040029A9 RID: 10665
		internal const string DataSourcesXmlTag = "DataSources";

		// Token: 0x040029AA RID: 10666
		internal const string DataSourceXmlTag = "DataSource";

		// Token: 0x040029AB RID: 10667
		internal const string DataSourceDefinitionXmlTag = "DataSourceDefinition";

		// Token: 0x040029AC RID: 10668
		internal const string m_dataSourceReferenceXmlTag = "DataSourceReference";

		// Token: 0x040029AD RID: 10669
		internal const string InvalidDataSourceReferenceXmlTag = "InvalidDataSourceReference";

		// Token: 0x040029AE RID: 10670
		internal const string XmlNameSpace = "http://schemas.microsoft.com/sqlserver/reporting/2006/03/reportdatasource";

		// Token: 0x040029AF RID: 10671
		private Guid m_id;

		// Token: 0x040029B0 RID: 10672
		private string m_name;

		// Token: 0x040029B1 RID: 10673
		private string m_originalName;

		// Token: 0x040029B2 RID: 10674
		private string m_extension;

		// Token: 0x040029B3 RID: 10675
		private byte[] m_connectionStringEncrypted;

		// Token: 0x040029B4 RID: 10676
		private byte[] m_originalConnectionStringEncrypted;

		// Token: 0x040029B5 RID: 10677
		private bool m_originalConnectStringExpressionBased;

		// Token: 0x040029B6 RID: 10678
		private string m_dataSourceReference;

		// Token: 0x040029B7 RID: 10679
		private Guid m_linkID;

		// Token: 0x040029B8 RID: 10680
		private Guid m_DataSourceWithCredentialsId;

		// Token: 0x040029B9 RID: 10681
		private byte[] m_secDesc;

		// Token: 0x040029BA RID: 10682
		private DataSourceInfo.CredentialsRetrievalOption m_credentialsRetrieval;

		// Token: 0x040029BB RID: 10683
		private string m_prompt;

		// Token: 0x040029BC RID: 10684
		private byte[] m_userNameEncrypted;

		// Token: 0x040029BD RID: 10685
		private byte[] m_passwordEncrypted;

		// Token: 0x040029BE RID: 10686
		private DataSourceInfo.DataSourceFlags m_flags;

		// Token: 0x040029BF RID: 10687
		private Guid m_modelID = Guid.Empty;

		// Token: 0x040029C0 RID: 10688
		private DateTime? m_modelLastUpdatedTime;

		// Token: 0x040029C1 RID: 10689
		private bool m_isEmbeddedInModel;

		// Token: 0x040029C2 RID: 10690
		private bool m_isModelSecurityUsed;

		// Token: 0x040029C3 RID: 10691
		private string m_tenantName;

		// Token: 0x040029C4 RID: 10692
		[NonSerialized]
		private bool m_isExternalDataSource;

		// Token: 0x040029C5 RID: 10693
		[NonSerialized]
		private bool m_isFullyFormedExternalDataSource;

		// Token: 0x040029C6 RID: 10694
		[NonSerialized]
		private bool m_isMultidimensional;

		// Token: 0x040029C7 RID: 10695
		[NonSerialized]
		private IServiceEndpoint m_serviceEndpoint;

		// Token: 0x040029C8 RID: 10696
		[NonSerialized]
		private SecureStringWrapper m_passwordSecureString;

		// Token: 0x040029C9 RID: 10697
		[NonSerialized]
		private SecureStoreLookup m_secureStoreLookup;

		// Token: 0x040029CA RID: 10698
		[NonSerialized]
		private DataSourceFaultContext m_dataSourceFaultContext;

		// Token: 0x040029CB RID: 10699
		[NonSerialized]
		private string m_modelPerspectiveName;

		// Token: 0x040029CC RID: 10700
		[NonSerialized]
		private static Regex m_useridDetectionRegex;

		// Token: 0x040029CD RID: 10701
		[NonSerialized]
		private const RegexOptions CompiledRegexOptions = RegexOptions.ExplicitCapture | RegexOptions.Compiled | RegexOptions.Singleline;

		// Token: 0x040029CE RID: 10702
		[NonSerialized]
		private const string UseridPattern = "{{[\\s]*[uU][sS][eE][rR][iI][dD][\\s]*}}";

		// Token: 0x02000C07 RID: 3079
		public enum CredentialsRetrievalOption
		{
			// Token: 0x040047F3 RID: 18419
			Unknown,
			// Token: 0x040047F4 RID: 18420
			Prompt,
			// Token: 0x040047F5 RID: 18421
			Store,
			// Token: 0x040047F6 RID: 18422
			Integrated,
			// Token: 0x040047F7 RID: 18423
			None,
			// Token: 0x040047F8 RID: 18424
			ServiceAccount,
			// Token: 0x040047F9 RID: 18425
			SecureStore
		}

		// Token: 0x02000C08 RID: 3080
		[Flags]
		private enum DataSourceFlags
		{
			// Token: 0x040047FB RID: 18427
			Enabled = 1,
			// Token: 0x040047FC RID: 18428
			ReferenceIsValid = 2,
			// Token: 0x040047FD RID: 18429
			ImpersonateUser = 4,
			// Token: 0x040047FE RID: 18430
			WindowsCredentials = 8,
			// Token: 0x040047FF RID: 18431
			IsModel = 16,
			// Token: 0x04004800 RID: 18432
			ConnectionStringUseridReference = 32
		}
	}
}
