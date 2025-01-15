using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x02002424 RID: 9252
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotUserEdit), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(TupleItems), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PivotArea), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	internal class PivotEdit : OpenXmlCompositeElement
	{
		// Token: 0x17004F72 RID: 20338
		// (get) Token: 0x06010FAA RID: 69546 RVA: 0x002E9437 File Offset: 0x002E7637
		public override string LocalName
		{
			get
			{
				return "pivotEdit";
			}
		}

		// Token: 0x17004F73 RID: 20339
		// (get) Token: 0x06010FAB RID: 69547 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F74 RID: 20340
		// (get) Token: 0x06010FAC RID: 69548 RVA: 0x002E943E File Offset: 0x002E763E
		internal override int ElementTypeId
		{
			get
			{
				return 12976;
			}
		}

		// Token: 0x06010FAD RID: 69549 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010FAE RID: 69550 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotEdit()
		{
		}

		// Token: 0x06010FAF RID: 69551 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotEdit(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FB0 RID: 69552 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotEdit(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010FB1 RID: 69553 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotEdit(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010FB2 RID: 69554 RVA: 0x002E9448 File Offset: 0x002E7648
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "userEdit" == name)
			{
				return new PivotUserEdit();
			}
			if (53 == namespaceId && "tupleItems" == name)
			{
				return new TupleItems();
			}
			if (53 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004F75 RID: 20341
		// (get) Token: 0x06010FB3 RID: 69555 RVA: 0x002E94B6 File Offset: 0x002E76B6
		internal override string[] ElementTagNames
		{
			get
			{
				return PivotEdit.eleTagNames;
			}
		}

		// Token: 0x17004F76 RID: 20342
		// (get) Token: 0x06010FB4 RID: 69556 RVA: 0x002E94BD File Offset: 0x002E76BD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PivotEdit.eleNamespaceIds;
			}
		}

		// Token: 0x17004F77 RID: 20343
		// (get) Token: 0x06010FB5 RID: 69557 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004F78 RID: 20344
		// (get) Token: 0x06010FB6 RID: 69558 RVA: 0x002E94C4 File Offset: 0x002E76C4
		// (set) Token: 0x06010FB7 RID: 69559 RVA: 0x002E94CD File Offset: 0x002E76CD
		public PivotUserEdit PivotUserEdit
		{
			get
			{
				return base.GetElement<PivotUserEdit>(0);
			}
			set
			{
				base.SetElement<PivotUserEdit>(0, value);
			}
		}

		// Token: 0x17004F79 RID: 20345
		// (get) Token: 0x06010FB8 RID: 69560 RVA: 0x002E94D7 File Offset: 0x002E76D7
		// (set) Token: 0x06010FB9 RID: 69561 RVA: 0x002E94E0 File Offset: 0x002E76E0
		public TupleItems TupleItems
		{
			get
			{
				return base.GetElement<TupleItems>(1);
			}
			set
			{
				base.SetElement<TupleItems>(1, value);
			}
		}

		// Token: 0x17004F7A RID: 20346
		// (get) Token: 0x06010FBA RID: 69562 RVA: 0x002E94EA File Offset: 0x002E76EA
		// (set) Token: 0x06010FBB RID: 69563 RVA: 0x002E94F3 File Offset: 0x002E76F3
		public PivotArea PivotArea
		{
			get
			{
				return base.GetElement<PivotArea>(2);
			}
			set
			{
				base.SetElement<PivotArea>(2, value);
			}
		}

		// Token: 0x17004F7B RID: 20347
		// (get) Token: 0x06010FBC RID: 69564 RVA: 0x002E94FD File Offset: 0x002E76FD
		// (set) Token: 0x06010FBD RID: 69565 RVA: 0x002E9506 File Offset: 0x002E7706
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(3);
			}
			set
			{
				base.SetElement<ExtensionList>(3, value);
			}
		}

		// Token: 0x06010FBE RID: 69566 RVA: 0x002E9510 File Offset: 0x002E7710
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotEdit>(deep);
		}

		// Token: 0x04007725 RID: 30501
		private const string tagName = "pivotEdit";

		// Token: 0x04007726 RID: 30502
		private const byte tagNsId = 53;

		// Token: 0x04007727 RID: 30503
		internal const int ElementTypeIdConst = 12976;

		// Token: 0x04007728 RID: 30504
		private static readonly string[] eleTagNames = new string[] { "userEdit", "tupleItems", "pivotArea", "extLst" };

		// Token: 0x04007729 RID: 30505
		private static readonly byte[] eleNamespaceIds = new byte[] { 53, 53, 53, 53 };
	}
}
