using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CustomDocumentInformationPanel
{
	// Token: 0x020022AD RID: 8877
	[GeneratedCode("DomGen", "2.0")]
	internal class XsnFileLocation : OpenXmlLeafTextElement
	{
		// Token: 0x17004139 RID: 16697
		// (get) Token: 0x0600F0E5 RID: 61669 RVA: 0x002D0F60 File Offset: 0x002CF160
		public override string LocalName
		{
			get
			{
				return "XSNLocation";
			}
		}

		// Token: 0x1700413A RID: 16698
		// (get) Token: 0x0600F0E6 RID: 61670 RVA: 0x002D0E0F File Offset: 0x002CF00F
		internal override byte NamespaceId
		{
			get
			{
				return 37;
			}
		}

		// Token: 0x1700413B RID: 16699
		// (get) Token: 0x0600F0E7 RID: 61671 RVA: 0x002D0F67 File Offset: 0x002CF167
		internal override int ElementTypeId
		{
			get
			{
				return 12630;
			}
		}

		// Token: 0x0600F0E8 RID: 61672 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F0E9 RID: 61673 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public XsnFileLocation()
		{
		}

		// Token: 0x0600F0EA RID: 61674 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public XsnFileLocation(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0EB RID: 61675 RVA: 0x002D0F70 File Offset: 0x002CF170
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0EC RID: 61676 RVA: 0x002D0F8B File Offset: 0x002CF18B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<XsnFileLocation>(deep);
		}

		// Token: 0x0400709F RID: 28831
		private const string tagName = "XSNLocation";

		// Token: 0x040070A0 RID: 28832
		private const byte tagNsId = 37;

		// Token: 0x040070A1 RID: 28833
		internal const int ElementTypeIdConst = 12630;
	}
}
