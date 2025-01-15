using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C11 RID: 11281
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Color))]
	internal class MruColors : OpenXmlCompositeElement
	{
		// Token: 0x17007FF1 RID: 32753
		// (get) Token: 0x06017C78 RID: 97400 RVA: 0x0033B21A File Offset: 0x0033941A
		public override string LocalName
		{
			get
			{
				return "mruColors";
			}
		}

		// Token: 0x17007FF2 RID: 32754
		// (get) Token: 0x06017C79 RID: 97401 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007FF3 RID: 32755
		// (get) Token: 0x06017C7A RID: 97402 RVA: 0x0033B221 File Offset: 0x00339421
		internal override int ElementTypeId
		{
			get
			{
				return 11262;
			}
		}

		// Token: 0x06017C7B RID: 97403 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017C7C RID: 97404 RVA: 0x00293ECF File Offset: 0x002920CF
		public MruColors()
		{
		}

		// Token: 0x06017C7D RID: 97405 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MruColors(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C7E RID: 97406 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MruColors(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017C7F RID: 97407 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MruColors(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017C80 RID: 97408 RVA: 0x0033A76B File Offset: 0x0033896B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "color" == name)
			{
				return new Color();
			}
			return null;
		}

		// Token: 0x06017C81 RID: 97409 RVA: 0x0033B228 File Offset: 0x00339428
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MruColors>(deep);
		}

		// Token: 0x04009D86 RID: 40326
		private const string tagName = "mruColors";

		// Token: 0x04009D87 RID: 40327
		private const byte tagNsId = 22;

		// Token: 0x04009D88 RID: 40328
		internal const int ElementTypeIdConst = 11262;
	}
}
