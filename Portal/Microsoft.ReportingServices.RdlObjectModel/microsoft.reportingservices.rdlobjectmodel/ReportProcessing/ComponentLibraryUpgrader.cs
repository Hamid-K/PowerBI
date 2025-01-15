using System;
using System.IO;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000077 RID: 119
	internal sealed class ComponentLibraryUpgrader
	{
		// Token: 0x0600043A RID: 1082 RVA: 0x00017016 File Offset: 0x00015216
		public static Stream UpgradeToCurrent(Stream stream, ref Stream outStream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return new ComponentLibraryUpgrader.ReportPartsUpgrader().UpgradeToCurrent(stream, ref outStream);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x00017034 File Offset: 0x00015234
		public static byte[] UpgradeToCurrent(byte[] definition)
		{
			Stream stream = new MemoryStream(definition);
			Stream stream2 = new MemoryStream();
			ComponentLibraryUpgrader.UpgradeToCurrent(stream, ref stream2);
			byte[] array = new byte[stream2.Length];
			stream2.Position = 0L;
			stream2.Read(array, 0, (int)stream2.Length);
			stream2.Close();
			return array;
		}

		// Token: 0x0200032E RID: 814
		private class ReportPartsUpgrader
		{
			// Token: 0x06001762 RID: 5986 RVA: 0x000376E0 File Offset: 0x000358E0
			internal Stream UpgradeToCurrent(Stream stream, ref Stream outStream)
			{
				XmlElement documentElement = this.LoadDefinition(stream).DocumentElement;
				this.ScanRdlNamespace(documentElement);
				if (this.NeedsUpgrade)
				{
					string oldRdlNamespace = this.OldRdlNamespace;
					if (!(oldRdlNamespace == "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition"))
					{
						if (!(oldRdlNamespace == "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition"))
						{
							throw new RDLUpgradeException(RDLUpgradeStringsWrapper.rdlInvalidTargetNamespace(this.OldRdlNamespace));
						}
					}
					else
					{
						this.UpdateNamespaceURI("http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition");
					}
				}
				return this.SaveDefinition(ref outStream);
			}

			// Token: 0x17000730 RID: 1840
			// (get) Token: 0x06001763 RID: 5987 RVA: 0x00037756 File Offset: 0x00035956
			private bool NeedsUpgrade
			{
				get
				{
					return this.m_needsUpgrade;
				}
			}

			// Token: 0x17000731 RID: 1841
			// (get) Token: 0x06001764 RID: 5988 RVA: 0x0003775E File Offset: 0x0003595E
			private string OldRdlNamespace
			{
				get
				{
					return this.m_oldNamespace;
				}
			}

			// Token: 0x17000732 RID: 1842
			// (get) Token: 0x06001765 RID: 5989 RVA: 0x00037766 File Offset: 0x00035966
			private string OldRdlNSPrefix
			{
				get
				{
					return this.m_oldNsPrefix;
				}
			}

			// Token: 0x06001766 RID: 5990 RVA: 0x00037770 File Offset: 0x00035970
			private void ScanRdlNamespace(XmlElement root)
			{
				string text = string.Empty;
				foreach (string text2 in this.m_knowNamespaces)
				{
					text = root.GetPrefixOfNamespace(text2);
					if (!string.IsNullOrEmpty(text))
					{
						this.m_oldNamespace = text2;
						this.m_needsUpgrade = true;
						this.m_oldNsPrefix = text;
						return;
					}
				}
			}

			// Token: 0x06001767 RID: 5991 RVA: 0x000377C2 File Offset: 0x000359C2
			private XmlDocument LoadDefinition(Stream stream)
			{
				this.m_document = XmlUtils.CreateXmlDocumentWithNullResolver();
				this.m_document.PreserveWhitespace = true;
				this.m_document.LoadWithNullResolver(stream);
				return this.m_document;
			}

			// Token: 0x06001768 RID: 5992 RVA: 0x000377ED File Offset: 0x000359ED
			private Stream SaveDefinition(ref Stream outStream)
			{
				this.m_document.Save(outStream);
				this.m_document = null;
				outStream.Seek(0L, SeekOrigin.Begin);
				return outStream;
			}

			// Token: 0x06001769 RID: 5993 RVA: 0x00037810 File Offset: 0x00035A10
			private void UpdateNamespaceURI(string oldNamespaceURI, string newNamespaceURI)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(this.m_document.OuterXml);
				stringBuilder.Replace(oldNamespaceURI, newNamespaceURI);
				this.m_document.LoadXmlWithNullResolver(stringBuilder.ToString());
			}

			// Token: 0x0400074B RID: 1867
			private bool m_needsUpgrade;

			// Token: 0x0400074C RID: 1868
			private string m_oldNamespace = string.Empty;

			// Token: 0x0400074D RID: 1869
			private string m_oldNsPrefix = string.Empty;

			// Token: 0x0400074E RID: 1870
			private XmlDocument m_document;

			// Token: 0x0400074F RID: 1871
			private readonly string[] m_knowNamespaces = new string[] { "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition", "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition" };

			// Token: 0x04000750 RID: 1872
			private const string NamespaceURI201001 = "http://schemas.microsoft.com/sqlserver/reporting/2010/01/reportdefinition";

			// Token: 0x04000751 RID: 1873
			private const string NamespaceURI201601 = "http://schemas.microsoft.com/sqlserver/reporting/2016/01/reportdefinition";
		}
	}
}
