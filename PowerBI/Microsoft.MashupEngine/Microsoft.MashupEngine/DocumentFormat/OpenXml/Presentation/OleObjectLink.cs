using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A97 RID: 10903
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class OleObjectLink : OpenXmlCompositeElement
	{
		// Token: 0x170073FF RID: 29695
		// (get) Token: 0x0601622A RID: 90666 RVA: 0x00326DCE File Offset: 0x00324FCE
		public override string LocalName
		{
			get
			{
				return "link";
			}
		}

		// Token: 0x17007400 RID: 29696
		// (get) Token: 0x0601622B RID: 90667 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007401 RID: 29697
		// (get) Token: 0x0601622C RID: 90668 RVA: 0x00326DD5 File Offset: 0x00324FD5
		internal override int ElementTypeId
		{
			get
			{
				return 12318;
			}
		}

		// Token: 0x0601622D RID: 90669 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007402 RID: 29698
		// (get) Token: 0x0601622E RID: 90670 RVA: 0x00326DDC File Offset: 0x00324FDC
		internal override string[] AttributeTagNames
		{
			get
			{
				return OleObjectLink.attributeTagNames;
			}
		}

		// Token: 0x17007403 RID: 29699
		// (get) Token: 0x0601622F RID: 90671 RVA: 0x00326DE3 File Offset: 0x00324FE3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return OleObjectLink.attributeNamespaceIds;
			}
		}

		// Token: 0x17007404 RID: 29700
		// (get) Token: 0x06016230 RID: 90672 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06016231 RID: 90673 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "updateAutomatic")]
		public BooleanValue AutoUpdate
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06016232 RID: 90674 RVA: 0x00293ECF File Offset: 0x002920CF
		public OleObjectLink()
		{
		}

		// Token: 0x06016233 RID: 90675 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OleObjectLink(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016234 RID: 90676 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OleObjectLink(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016235 RID: 90677 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OleObjectLink(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016236 RID: 90678 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007405 RID: 29701
		// (get) Token: 0x06016237 RID: 90679 RVA: 0x00326DEA File Offset: 0x00324FEA
		internal override string[] ElementTagNames
		{
			get
			{
				return OleObjectLink.eleTagNames;
			}
		}

		// Token: 0x17007406 RID: 29702
		// (get) Token: 0x06016238 RID: 90680 RVA: 0x00326DF1 File Offset: 0x00324FF1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return OleObjectLink.eleNamespaceIds;
			}
		}

		// Token: 0x17007407 RID: 29703
		// (get) Token: 0x06016239 RID: 90681 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007408 RID: 29704
		// (get) Token: 0x0601623A RID: 90682 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x0601623B RID: 90683 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x0601623C RID: 90684 RVA: 0x00326DF8 File Offset: 0x00324FF8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "updateAutomatic" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601623D RID: 90685 RVA: 0x00326E18 File Offset: 0x00325018
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OleObjectLink>(deep);
		}

		// Token: 0x0601623E RID: 90686 RVA: 0x00326E24 File Offset: 0x00325024
		// Note: this type is marked as 'beforefieldinit'.
		static OleObjectLink()
		{
			byte[] array = new byte[1];
			OleObjectLink.attributeNamespaceIds = array;
			OleObjectLink.eleTagNames = new string[] { "extLst" };
			OleObjectLink.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x04009660 RID: 38496
		private const string tagName = "link";

		// Token: 0x04009661 RID: 38497
		private const byte tagNsId = 24;

		// Token: 0x04009662 RID: 38498
		internal const int ElementTypeIdConst = 12318;

		// Token: 0x04009663 RID: 38499
		private static string[] attributeTagNames = new string[] { "updateAutomatic" };

		// Token: 0x04009664 RID: 38500
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009665 RID: 38501
		private static readonly string[] eleTagNames;

		// Token: 0x04009666 RID: 38502
		private static readonly byte[] eleNamespaceIds;
	}
}
