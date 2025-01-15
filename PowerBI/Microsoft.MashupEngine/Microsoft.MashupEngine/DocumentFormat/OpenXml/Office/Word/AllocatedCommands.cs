using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200247E RID: 9342
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AllocatedCommand))]
	internal class AllocatedCommands : OpenXmlCompositeElement
	{
		// Token: 0x1700513D RID: 20797
		// (get) Token: 0x060113D1 RID: 70609 RVA: 0x002EBF76 File Offset: 0x002EA176
		public override string LocalName
		{
			get
			{
				return "acds";
			}
		}

		// Token: 0x1700513E RID: 20798
		// (get) Token: 0x060113D2 RID: 70610 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700513F RID: 20799
		// (get) Token: 0x060113D3 RID: 70611 RVA: 0x002EBF7D File Offset: 0x002EA17D
		internal override int ElementTypeId
		{
			get
			{
				return 12569;
			}
		}

		// Token: 0x060113D4 RID: 70612 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060113D5 RID: 70613 RVA: 0x00293ECF File Offset: 0x002920CF
		public AllocatedCommands()
		{
		}

		// Token: 0x060113D6 RID: 70614 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AllocatedCommands(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113D7 RID: 70615 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AllocatedCommands(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113D8 RID: 70616 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AllocatedCommands(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060113D9 RID: 70617 RVA: 0x002EBF84 File Offset: 0x002EA184
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "acd" == name)
			{
				return new AllocatedCommand();
			}
			return null;
		}

		// Token: 0x060113DA RID: 70618 RVA: 0x002EBF9F File Offset: 0x002EA19F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AllocatedCommands>(deep);
		}

		// Token: 0x040078CF RID: 30927
		private const string tagName = "acds";

		// Token: 0x040078D0 RID: 30928
		private const byte tagNsId = 33;

		// Token: 0x040078D1 RID: 30929
		internal const int ElementTypeIdConst = 12569;
	}
}
