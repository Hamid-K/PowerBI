using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B15 RID: 11029
	[ChildElementInfo(typeof(CalculationCell))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CalculationChain : OpenXmlPartRootElement
	{
		// Token: 0x170075B6 RID: 30134
		// (get) Token: 0x06016614 RID: 91668 RVA: 0x002A75BF File Offset: 0x002A57BF
		public override string LocalName
		{
			get
			{
				return "calcChain";
			}
		}

		// Token: 0x170075B7 RID: 30135
		// (get) Token: 0x06016615 RID: 91669 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075B8 RID: 30136
		// (get) Token: 0x06016616 RID: 91670 RVA: 0x00329733 File Offset: 0x00327933
		internal override int ElementTypeId
		{
			get
			{
				return 11027;
			}
		}

		// Token: 0x06016617 RID: 91671 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016618 RID: 91672 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal CalculationChain(CalculationChainPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016619 RID: 91673 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CalculationChainPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170075B9 RID: 30137
		// (get) Token: 0x0601661A RID: 91674 RVA: 0x0032973A File Offset: 0x0032793A
		// (set) Token: 0x0601661B RID: 91675 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CalculationChainPart CalculationChainPart
		{
			get
			{
				return base.OpenXmlPart as CalculationChainPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0601661C RID: 91676 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public CalculationChain(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601661D RID: 91677 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public CalculationChain(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601661E RID: 91678 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public CalculationChain(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601661F RID: 91679 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public CalculationChain()
		{
		}

		// Token: 0x06016620 RID: 91680 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CalculationChainPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06016621 RID: 91681 RVA: 0x00329747 File Offset: 0x00327947
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "c" == name)
			{
				return new CalculationCell();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06016622 RID: 91682 RVA: 0x0032977A File Offset: 0x0032797A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculationChain>(deep);
		}

		// Token: 0x040098CA RID: 39114
		private const string tagName = "calcChain";

		// Token: 0x040098CB RID: 39115
		private const byte tagNsId = 22;

		// Token: 0x040098CC RID: 39116
		internal const int ElementTypeIdConst = 11027;
	}
}
