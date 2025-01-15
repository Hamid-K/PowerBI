using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028FB RID: 10491
	[GeneratedCode("DomGen", "2.0")]
	internal class ProducerName : NameType
	{
		// Token: 0x1700699A RID: 27034
		// (get) Token: 0x06014ADC RID: 84700 RVA: 0x003154E8 File Offset: 0x003136E8
		public override string LocalName
		{
			get
			{
				return "ProducerName";
			}
		}

		// Token: 0x1700699B RID: 27035
		// (get) Token: 0x06014ADD RID: 84701 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700699C RID: 27036
		// (get) Token: 0x06014ADE RID: 84702 RVA: 0x003154EF File Offset: 0x003136EF
		internal override int ElementTypeId
		{
			get
			{
				return 10778;
			}
		}

		// Token: 0x06014ADF RID: 84703 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AE0 RID: 84704 RVA: 0x003153D6 File Offset: 0x003135D6
		public ProducerName()
		{
		}

		// Token: 0x06014AE1 RID: 84705 RVA: 0x003153DE File Offset: 0x003135DE
		public ProducerName(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AE2 RID: 84706 RVA: 0x003153E7 File Offset: 0x003135E7
		public ProducerName(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AE3 RID: 84707 RVA: 0x003153F0 File Offset: 0x003135F0
		public ProducerName(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014AE4 RID: 84708 RVA: 0x003154F6 File Offset: 0x003136F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProducerName>(deep);
		}

		// Token: 0x04008F7C RID: 36732
		private const string tagName = "ProducerName";

		// Token: 0x04008F7D RID: 36733
		private const byte tagNsId = 9;

		// Token: 0x04008F7E RID: 36734
		internal const int ElementTypeIdConst = 10778;
	}
}
