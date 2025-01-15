using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000C1 RID: 193
	public class DataSource : ReportObject, INamedObject
	{
		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x0001C12A File Offset: 0x0001A32A
		// (set) Token: 0x0600081C RID: 2076 RVA: 0x0001C13D File Offset: 0x0001A33D
		[XmlAttribute]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001C14C File Offset: 0x0001A34C
		// (set) Token: 0x0600081E RID: 2078 RVA: 0x0001C15A File Offset: 0x0001A35A
		[DefaultValue(false)]
		public bool Transaction
		{
			get
			{
				return base.PropertyStore.GetBoolean(1);
			}
			set
			{
				base.PropertyStore.SetBoolean(1, value);
			}
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x0600081F RID: 2079 RVA: 0x0001C169 File Offset: 0x0001A369
		// (set) Token: 0x06000820 RID: 2080 RVA: 0x0001C17C File Offset: 0x0001A37C
		public ConnectionProperties ConnectionProperties
		{
			get
			{
				return (ConnectionProperties)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x0001C18B File Offset: 0x0001A38B
		// (set) Token: 0x06000822 RID: 2082 RVA: 0x0001C19E File Offset: 0x0001A39E
		[DefaultValue("")]
		public string DataSourceReference
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001C1AD File Offset: 0x0001A3AD
		public DataSource()
		{
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x0001C1BC File Offset: 0x0001A3BC
		internal DataSource(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x0001C1CC File Offset: 0x0001A3CC
		public override void Initialize()
		{
			base.Initialize();
		}

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000826 RID: 2086 RVA: 0x0001C1D4 File Offset: 0x0001A3D4
		// (set) Token: 0x06000827 RID: 2087 RVA: 0x0001C214 File Offset: 0x0001A414
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		[DefaultValue(SecurityTypeEnum.Unknown)]
		public SecurityTypeEnum SecurityType
		{
			get
			{
				if (this.m_shared)
				{
					return SecurityTypeEnum.Unknown;
				}
				if (this.m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.Integrated)
				{
					return SecurityTypeEnum.Integrated;
				}
				if (this.m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.Prompt || this.m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.Store)
				{
					if (this.m_windowsCredentials)
					{
						return SecurityTypeEnum.Windows;
					}
					return SecurityTypeEnum.DataBase;
				}
				else
				{
					if (this.m_credentialRetrievalEnum == DataSourceCredentialRetrievalEnum.None)
					{
						return SecurityTypeEnum.None;
					}
					return SecurityTypeEnum.Unknown;
				}
			}
			set
			{
				this.m_windowsCredentials = false;
				switch (value)
				{
				case SecurityTypeEnum.None:
					this.m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.None;
					return;
				case SecurityTypeEnum.DataBase:
					this.m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.Prompt;
					return;
				case SecurityTypeEnum.Windows:
					this.m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.Prompt;
					this.m_windowsCredentials = true;
					return;
				case SecurityTypeEnum.Integrated:
					this.m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.Integrated;
					return;
				default:
					return;
				}
			}
		}

		// Token: 0x04000161 RID: 353
		private readonly bool m_shared;

		// Token: 0x04000162 RID: 354
		private DataSourceCredentialRetrievalEnum m_credentialRetrievalEnum = DataSourceCredentialRetrievalEnum.None;

		// Token: 0x04000163 RID: 355
		private bool m_windowsCredentials;

		// Token: 0x0200036D RID: 877
		internal class Definition : DefinitionStore<DataSource, DataSource.Definition.Properties>
		{
			// Token: 0x0200048A RID: 1162
			internal enum Properties
			{
				// Token: 0x04000B23 RID: 2851
				Name,
				// Token: 0x04000B24 RID: 2852
				Transaction,
				// Token: 0x04000B25 RID: 2853
				ConnectionProperties,
				// Token: 0x04000B26 RID: 2854
				DataSourceReference
			}
		}
	}
}
