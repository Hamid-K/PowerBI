using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021EC RID: 8684
	[GeneratedCode("DomGen", "2.0")]
	internal class AcceleratorPrimary : OpenXmlLeafTextElement
	{
		// Token: 0x170037D1 RID: 14289
		// (get) Token: 0x0600DD12 RID: 56594 RVA: 0x002BD098 File Offset: 0x002BB298
		public override string LocalName
		{
			get
			{
				return "Accel";
			}
		}

		// Token: 0x170037D2 RID: 14290
		// (get) Token: 0x0600DD13 RID: 56595 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x170037D3 RID: 14291
		// (get) Token: 0x0600DD14 RID: 56596 RVA: 0x002BD09F File Offset: 0x002BB29F
		internal override int ElementTypeId
		{
			get
			{
				return 12457;
			}
		}

		// Token: 0x0600DD15 RID: 56597 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600DD16 RID: 56598 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public AcceleratorPrimary()
		{
		}

		// Token: 0x0600DD17 RID: 56599 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public AcceleratorPrimary(string text)
			: base(text)
		{
		}

		// Token: 0x0600DD18 RID: 56600 RVA: 0x002BD0A8 File Offset: 0x002BB2A8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new ByteValue
			{
				InnerText = text
			};
		}

		// Token: 0x0600DD19 RID: 56601 RVA: 0x002BD0C3 File Offset: 0x002BB2C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AcceleratorPrimary>(deep);
		}

		// Token: 0x04006CF8 RID: 27896
		private const string tagName = "Accel";

		// Token: 0x04006CF9 RID: 27897
		private const byte tagNsId = 29;

		// Token: 0x04006CFA RID: 27898
		internal const int ElementTypeIdConst = 12457;
	}
}
