using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA3 RID: 11427
	[ChildElementInfo(typeof(Control))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Controls : OpenXmlCompositeElement
	{
		// Token: 0x17008442 RID: 33858
		// (get) Token: 0x06018681 RID: 99969 RVA: 0x00327339 File Offset: 0x00325539
		public override string LocalName
		{
			get
			{
				return "controls";
			}
		}

		// Token: 0x17008443 RID: 33859
		// (get) Token: 0x06018682 RID: 99970 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008444 RID: 33860
		// (get) Token: 0x06018683 RID: 99971 RVA: 0x003414E1 File Offset: 0x0033F6E1
		internal override int ElementTypeId
		{
			get
			{
				return 11407;
			}
		}

		// Token: 0x06018684 RID: 99972 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018685 RID: 99973 RVA: 0x00293ECF File Offset: 0x002920CF
		public Controls()
		{
		}

		// Token: 0x06018686 RID: 99974 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Controls(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018687 RID: 99975 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Controls(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018688 RID: 99976 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Controls(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018689 RID: 99977 RVA: 0x003414E8 File Offset: 0x0033F6E8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "control" == name)
			{
				return new Control();
			}
			return null;
		}

		// Token: 0x0601868A RID: 99978 RVA: 0x00341503 File Offset: 0x0033F703
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Controls>(deep);
		}

		// Token: 0x0400A01E RID: 40990
		private const string tagName = "controls";

		// Token: 0x0400A01F RID: 40991
		private const byte tagNsId = 22;

		// Token: 0x0400A020 RID: 40992
		internal const int ElementTypeIdConst = 11407;
	}
}
