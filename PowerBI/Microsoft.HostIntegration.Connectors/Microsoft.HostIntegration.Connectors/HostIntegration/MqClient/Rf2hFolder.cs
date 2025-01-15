using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B38 RID: 2872
	public abstract class Rf2hFolder
	{
		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06005AC0 RID: 23232 RVA: 0x001760B9 File Offset: 0x001742B9
		// (set) Token: 0x06005AC1 RID: 23233 RVA: 0x001760C1 File Offset: 0x001742C1
		protected bool IsDirty { get; set; }

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06005AC2 RID: 23234 RVA: 0x001760CA File Offset: 0x001742CA
		public string CompleteString
		{
			get
			{
				if (this.completeString != null && !this.IsDirty)
				{
					return this.completeString;
				}
				this.completeString = this.GenerateString();
				this.IsDirty = false;
				return this.completeString;
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06005AC3 RID: 23235 RVA: 0x001760FC File Offset: 0x001742FC
		// (set) Token: 0x06005AC4 RID: 23236 RVA: 0x00176104 File Offset: 0x00174304
		public Rf2hFolderType FolderType { get; private set; }

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06005AC5 RID: 23237 RVA: 0x0017610D File Offset: 0x0017430D
		// (set) Token: 0x06005AC6 RID: 23238 RVA: 0x00176115 File Offset: 0x00174315
		internal Rf2hFolderCollection Parent { get; set; }

		// Token: 0x06005AC7 RID: 23239 RVA: 0x0017611E File Offset: 0x0017431E
		protected Rf2hFolder(Rf2hFolderType folderType)
			: this(folderType, null)
		{
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x00176128 File Offset: 0x00174328
		protected Rf2hFolder(Rf2hFolderType folderType, string folderContents)
		{
			if (folderContents != null)
			{
				Rf2hFolderType typeFromContents = Rf2hFolder.GetTypeFromContents(folderContents);
				if (folderType != typeFromContents)
				{
					throw new CustomMqClientException(SR.FolderContentsNotType);
				}
				folderContents = folderContents.Trim();
			}
			this.FolderType = folderType;
			this.completeString = folderContents;
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x0017616A File Offset: 0x0017436A
		protected void EnsureParsed()
		{
			if (this.isParsed)
			{
				return;
			}
			this.isParsed = true;
			if (this.completeString == null)
			{
				return;
			}
			this.ParseCompleteString();
		}

		// Token: 0x06005ACA RID: 23242 RVA: 0x0017618C File Offset: 0x0017438C
		public static Rf2hFolder GetInstance(string folderContents)
		{
			Rf2hFolder rf2hFolder = null;
			switch (Rf2hFolder.GetTypeFromContents(folderContents))
			{
			case Rf2hFolderType.Jms:
				rf2hFolder = new Rf2hJmsFolder(folderContents);
				break;
			case Rf2hFolderType.Mcd:
				rf2hFolder = new Rf2hMcdFolder(folderContents);
				break;
			case Rf2hFolderType.Mq_Usr:
				rf2hFolder = new Rf2hMqUsrFolder(folderContents);
				break;
			case Rf2hFolderType.Usr:
				rf2hFolder = new Rf2hUsrFolder(folderContents);
				break;
			case Rf2hFolderType.Properties:
				rf2hFolder = new Rf2hPropertiesFolder(folderContents);
				break;
			case Rf2hFolderType.Unparsed:
				rf2hFolder = new Rf2hUnparsedFolder(folderContents);
				break;
			}
			return rf2hFolder;
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x001761F8 File Offset: 0x001743F8
		private static Rf2hFolderType GetTypeFromContents(string folderContents)
		{
			int num = folderContents.IndexOf('<');
			if (num == -1)
			{
				throw new CustomMqClientException(SR.FolderContentsNotParseable);
			}
			int num2 = folderContents.IndexOf('>', num + 1);
			if (num2 == -1)
			{
				throw new CustomMqClientException(SR.FolderContentsNotParseable);
			}
			int num3 = folderContents.IndexOf(' ', num + 1);
			if (num3 > num2)
			{
				num3 = -1;
			}
			int num4 = num + 1;
			int num5 = ((num3 == -1) ? num2 : num3);
			string text = folderContents.Substring(num4, num5 - num4);
			Rf2hFolderType rf2hFolderType;
			if (Rf2hFolder.namesToFolderTypes.TryGetValue(text, out rf2hFolderType))
			{
				return rf2hFolderType;
			}
			if (num3 == -1)
			{
				return Rf2hFolderType.Unparsed;
			}
			int num6 = folderContents.IndexOf("content", num3 + 1);
			if (num6 == -1)
			{
				return Rf2hFolderType.Unparsed;
			}
			int num7 = folderContents.IndexOf("properties", num6 + 9);
			if (num7 == -1)
			{
				throw new CustomMqClientException(SR.FolderContentsNotParseable);
			}
			if (num7 > num2)
			{
				throw new CustomMqClientException(SR.FolderContentsNotParseable);
			}
			return Rf2hFolderType.Properties;
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x001762C4 File Offset: 0x001744C4
		internal static string GetFolderName(string folderContents)
		{
			int num = folderContents.IndexOf('<');
			if (num == -1)
			{
				throw new CustomMqClientException(SR.FolderContentsNotParseable);
			}
			int num2 = folderContents.IndexOf('>', num + 1);
			if (num2 == -1)
			{
				throw new CustomMqClientException(SR.FolderContentsNotParseable);
			}
			int num3 = folderContents.IndexOf(' ', num + 1);
			if (num3 > num2)
			{
				num3 = -1;
			}
			int num4 = num + 1;
			int num5 = ((num3 == -1) ? num2 : num3);
			return folderContents.Substring(num4, num5 - num4);
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x0017632F File Offset: 0x0017452F
		internal Rf2hFolder GenerateCopy()
		{
			return Rf2hFolder.GetInstance(this.CompleteString);
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x00003CAB File Offset: 0x00001EAB
		internal virtual string GenerateString()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x000036A9 File Offset: 0x000018A9
		internal virtual void ParseCompleteString()
		{
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x0017633C File Offset: 0x0017453C
		public override string ToString()
		{
			return this.CompleteString;
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x00176344 File Offset: 0x00174544
		protected XmlDocument LoadDocumentFromCompleteString()
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(new NameTable());
			xmlNamespaceManager.AddNamespace("xsi", "http://www.w3.org/2001/XMLSchema-instance");
			XmlParserContext xmlParserContext = new XmlParserContext(null, xmlNamespaceManager, null, XmlSpace.None);
			XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
			xmlReaderSettings.DtdProcessing = DtdProcessing.Prohibit;
			xmlReaderSettings.XmlResolver = null;
			xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
			XmlReader xmlReader = XmlReader.Create(new StringReader(this.completeString), xmlReaderSettings, xmlParserContext);
			xmlDocument.Load(xmlReader);
			return xmlDocument;
		}

		// Token: 0x04004795 RID: 18325
		private string completeString;

		// Token: 0x04004796 RID: 18326
		private bool isParsed;

		// Token: 0x04004798 RID: 18328
		private static Dictionary<string, Rf2hFolderType> namesToFolderTypes = new Dictionary<string, Rf2hFolderType>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"psc",
				Rf2hFolderType.Unparsed
			},
			{
				"pscr",
				Rf2hFolderType.Unparsed
			},
			{
				"jms",
				Rf2hFolderType.Jms
			},
			{
				"mcd",
				Rf2hFolderType.Mcd
			},
			{
				"mq_usr",
				Rf2hFolderType.Mq_Usr
			},
			{
				"sib",
				Rf2hFolderType.Unparsed
			},
			{
				"sib_context",
				Rf2hFolderType.Unparsed
			},
			{
				"sib_usr",
				Rf2hFolderType.Unparsed
			},
			{
				"usr",
				Rf2hFolderType.Usr
			},
			{
				"ibm",
				Rf2hFolderType.Unparsed
			},
			{
				"mq",
				Rf2hFolderType.Unparsed
			},
			{
				"mqema",
				Rf2hFolderType.Unparsed
			},
			{
				"mqext",
				Rf2hFolderType.Unparsed
			},
			{
				"mqps",
				Rf2hFolderType.Unparsed
			},
			{
				"mq_svc",
				Rf2hFolderType.Unparsed
			},
			{
				"mqtt",
				Rf2hFolderType.Unparsed
			}
		};
	}
}
