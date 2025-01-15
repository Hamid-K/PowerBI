using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002608 RID: 9736
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BarSerExtension))]
	internal class BarSerExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170059E4 RID: 23012
		// (get) Token: 0x060126F7 RID: 75511 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170059E5 RID: 23013
		// (get) Token: 0x060126F8 RID: 75512 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170059E6 RID: 23014
		// (get) Token: 0x060126F9 RID: 75513 RVA: 0x002FB0D2 File Offset: 0x002F92D2
		internal override int ElementTypeId
		{
			get
			{
				return 10585;
			}
		}

		// Token: 0x060126FA RID: 75514 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060126FB RID: 75515 RVA: 0x00293ECF File Offset: 0x002920CF
		public BarSerExtensionList()
		{
		}

		// Token: 0x060126FC RID: 75516 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BarSerExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126FD RID: 75517 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BarSerExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060126FE RID: 75518 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BarSerExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060126FF RID: 75519 RVA: 0x002FB0D9 File Offset: 0x002F92D9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "ext" == name)
			{
				return new BarSerExtension();
			}
			return null;
		}

		// Token: 0x06012700 RID: 75520 RVA: 0x002FB0F4 File Offset: 0x002F92F4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarSerExtensionList>(deep);
		}

		// Token: 0x04007F8C RID: 32652
		private const string tagName = "extLst";

		// Token: 0x04007F8D RID: 32653
		private const byte tagNsId = 11;

		// Token: 0x04007F8E RID: 32654
		internal const int ElementTypeIdConst = 10585;
	}
}
