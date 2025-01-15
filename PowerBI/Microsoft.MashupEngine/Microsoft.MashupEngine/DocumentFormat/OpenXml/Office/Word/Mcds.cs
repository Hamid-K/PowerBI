using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200245C RID: 9308
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Mcd))]
	internal class Mcds : OpenXmlCompositeElement
	{
		// Token: 0x1700509D RID: 20637
		// (get) Token: 0x0601124F RID: 70223 RVA: 0x002EB05F File Offset: 0x002E925F
		public override string LocalName
		{
			get
			{
				return "mcds";
			}
		}

		// Token: 0x1700509E RID: 20638
		// (get) Token: 0x06011250 RID: 70224 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700509F RID: 20639
		// (get) Token: 0x06011251 RID: 70225 RVA: 0x002EB066 File Offset: 0x002E9266
		internal override int ElementTypeId
		{
			get
			{
				return 12538;
			}
		}

		// Token: 0x06011252 RID: 70226 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011253 RID: 70227 RVA: 0x00293ECF File Offset: 0x002920CF
		public Mcds()
		{
		}

		// Token: 0x06011254 RID: 70228 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Mcds(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011255 RID: 70229 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Mcds(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011256 RID: 70230 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Mcds(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011257 RID: 70231 RVA: 0x002EB06D File Offset: 0x002E926D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "mcd" == name)
			{
				return new Mcd();
			}
			return null;
		}

		// Token: 0x06011258 RID: 70232 RVA: 0x002EB088 File Offset: 0x002E9288
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Mcds>(deep);
		}

		// Token: 0x0400785C RID: 30812
		private const string tagName = "mcds";

		// Token: 0x0400785D RID: 30813
		private const byte tagNsId = 33;

		// Token: 0x0400785E RID: 30814
		internal const int ElementTypeIdConst = 12538;
	}
}
