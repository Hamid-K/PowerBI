using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.CoverPageProps
{
	// Token: 0x020022A5 RID: 8869
	[GeneratedCode("DomGen", "2.0")]
	internal class DocumentAbstract : OpenXmlLeafTextElement
	{
		// Token: 0x1700411C RID: 16668
		// (get) Token: 0x0600F09B RID: 61595 RVA: 0x002D0D04 File Offset: 0x002CEF04
		public override string LocalName
		{
			get
			{
				return "Abstract";
			}
		}

		// Token: 0x1700411D RID: 16669
		// (get) Token: 0x0600F09C RID: 61596 RVA: 0x002D0B3B File Offset: 0x002CED3B
		internal override byte NamespaceId
		{
			get
			{
				return 36;
			}
		}

		// Token: 0x1700411E RID: 16670
		// (get) Token: 0x0600F09D RID: 61597 RVA: 0x002D0D0B File Offset: 0x002CEF0B
		internal override int ElementTypeId
		{
			get
			{
				return 12623;
			}
		}

		// Token: 0x0600F09E RID: 61598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600F09F RID: 61599 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public DocumentAbstract()
		{
		}

		// Token: 0x0600F0A0 RID: 61600 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public DocumentAbstract(string text)
			: base(text)
		{
		}

		// Token: 0x0600F0A1 RID: 61601 RVA: 0x002D0D14 File Offset: 0x002CEF14
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600F0A2 RID: 61602 RVA: 0x002D0D2F File Offset: 0x002CEF2F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentAbstract>(deep);
		}

		// Token: 0x04007085 RID: 28805
		private const string tagName = "Abstract";

		// Token: 0x04007086 RID: 28806
		private const byte tagNsId = 36;

		// Token: 0x04007087 RID: 28807
		internal const int ElementTypeIdConst = 12623;
	}
}
