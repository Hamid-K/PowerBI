using System;
using System.Collections.Generic;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200001D RID: 29
	public sealed class DocumentSharingLocation
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006CF3 File Offset: 0x00005CF3
		// (set) Token: 0x0600013C RID: 316 RVA: 0x00006CFB File Offset: 0x00005CFB
		public string ServiceUrl
		{
			get
			{
				return this.serviceUrl;
			}
			private set
			{
				this.serviceUrl = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006D04 File Offset: 0x00005D04
		// (set) Token: 0x0600013E RID: 318 RVA: 0x00006D0C File Offset: 0x00005D0C
		public string LocationUrl
		{
			get
			{
				return this.locationUrl;
			}
			private set
			{
				this.locationUrl = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006D15 File Offset: 0x00005D15
		// (set) Token: 0x06000140 RID: 320 RVA: 0x00006D1D File Offset: 0x00005D1D
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			private set
			{
				this.displayName = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006D26 File Offset: 0x00005D26
		// (set) Token: 0x06000142 RID: 322 RVA: 0x00006D2E File Offset: 0x00005D2E
		public IEnumerable<string> SupportedFileExtensions
		{
			get
			{
				return this.supportedFileExtensions;
			}
			private set
			{
				this.supportedFileExtensions = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000143 RID: 323 RVA: 0x00006D37 File Offset: 0x00005D37
		// (set) Token: 0x06000144 RID: 324 RVA: 0x00006D3F File Offset: 0x00005D3F
		public bool ExternalAccessAllowed
		{
			get
			{
				return this.externalAccessAllowed;
			}
			private set
			{
				this.externalAccessAllowed = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00006D48 File Offset: 0x00005D48
		// (set) Token: 0x06000146 RID: 326 RVA: 0x00006D50 File Offset: 0x00005D50
		public bool AnonymousAccessAllowed
		{
			get
			{
				return this.anonymousAccessAllowed;
			}
			private set
			{
				this.anonymousAccessAllowed = value;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00006D59 File Offset: 0x00005D59
		// (set) Token: 0x06000148 RID: 328 RVA: 0x00006D61 File Offset: 0x00005D61
		public bool CanModifyPermissions
		{
			get
			{
				return this.canModifyPermissions;
			}
			private set
			{
				this.canModifyPermissions = value;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00006D6A File Offset: 0x00005D6A
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00006D72 File Offset: 0x00005D72
		public bool IsDefault
		{
			get
			{
				return this.isDefault;
			}
			private set
			{
				this.isDefault = value;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00006D7B File Offset: 0x00005D7B
		private DocumentSharingLocation()
		{
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00006D84 File Offset: 0x00005D84
		internal static DocumentSharingLocation LoadFromXml(EwsXmlReader reader)
		{
			DocumentSharingLocation documentSharingLocation = new DocumentSharingLocation();
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000149-1 == null)
					{
						Dictionary<string, int> dictionary = new Dictionary<string, int>(8);
						dictionary.Add("ServiceUrl", 0);
						dictionary.Add("LocationUrl", 1);
						dictionary.Add("DisplayName", 2);
						dictionary.Add("SupportedFileExtensions", 3);
						dictionary.Add("ExternalAccessAllowed", 4);
						dictionary.Add("AnonymousAccessAllowed", 5);
						dictionary.Add("CanModifyPermissions", 6);
						dictionary.Add("IsDefault", 7);
						<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000149-1 = dictionary;
					}
					int num;
					if (<PrivateImplementationDetails>{70549B87-FCC0-4468-A58C-F62EC848C70D}.$$method0x6000149-1.TryGetValue(localName, ref num))
					{
						switch (num)
						{
						case 0:
							documentSharingLocation.ServiceUrl = reader.ReadElementValue<string>();
							break;
						case 1:
							documentSharingLocation.LocationUrl = reader.ReadElementValue<string>();
							break;
						case 2:
							documentSharingLocation.DisplayName = reader.ReadElementValue<string>();
							break;
						case 3:
						{
							List<string> list = new List<string>();
							reader.Read();
							while (reader.IsStartElement(XmlNamespace.Autodiscover, "FileExtension"))
							{
								string text = reader.ReadElementValue<string>();
								list.Add(text);
								reader.Read();
							}
							documentSharingLocation.SupportedFileExtensions = list;
							break;
						}
						case 4:
							documentSharingLocation.ExternalAccessAllowed = reader.ReadElementValue<bool>();
							break;
						case 5:
							documentSharingLocation.AnonymousAccessAllowed = reader.ReadElementValue<bool>();
							break;
						case 6:
							documentSharingLocation.CanModifyPermissions = reader.ReadElementValue<bool>();
							break;
						case 7:
							documentSharingLocation.IsDefault = reader.ReadElementValue<bool>();
							break;
						}
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "DocumentSharingLocation"));
			return documentSharingLocation;
		}

		// Token: 0x04000070 RID: 112
		private string serviceUrl;

		// Token: 0x04000071 RID: 113
		private string locationUrl;

		// Token: 0x04000072 RID: 114
		private string displayName;

		// Token: 0x04000073 RID: 115
		private IEnumerable<string> supportedFileExtensions;

		// Token: 0x04000074 RID: 116
		private bool externalAccessAllowed;

		// Token: 0x04000075 RID: 117
		private bool anonymousAccessAllowed;

		// Token: 0x04000076 RID: 118
		private bool canModifyPermissions;

		// Token: 0x04000077 RID: 119
		private bool isDefault;
	}
}
