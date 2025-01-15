using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002584 RID: 9604
	[ChildElementInfo(typeof(MultiLevelStringCache))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Formula))]
	internal class MultiLevelStringReference : OpenXmlCompositeElement
	{
		// Token: 0x1700562F RID: 22063
		// (get) Token: 0x06011ED6 RID: 73430 RVA: 0x002F3BB4 File Offset: 0x002F1DB4
		public override string LocalName
		{
			get
			{
				return "multiLvlStrRef";
			}
		}

		// Token: 0x17005630 RID: 22064
		// (get) Token: 0x06011ED7 RID: 73431 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005631 RID: 22065
		// (get) Token: 0x06011ED8 RID: 73432 RVA: 0x002F3BBB File Offset: 0x002F1DBB
		internal override int ElementTypeId
		{
			get
			{
				return 10403;
			}
		}

		// Token: 0x06011ED9 RID: 73433 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011EDA RID: 73434 RVA: 0x00293ECF File Offset: 0x002920CF
		public MultiLevelStringReference()
		{
		}

		// Token: 0x06011EDB RID: 73435 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MultiLevelStringReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EDC RID: 73436 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MultiLevelStringReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EDD RID: 73437 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MultiLevelStringReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011EDE RID: 73438 RVA: 0x002F3BC4 File Offset: 0x002F1DC4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			if (11 == namespaceId && "multiLvlStrCache" == name)
			{
				return new MultiLevelStringCache();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005632 RID: 22066
		// (get) Token: 0x06011EDF RID: 73439 RVA: 0x002F3C1A File Offset: 0x002F1E1A
		internal override string[] ElementTagNames
		{
			get
			{
				return MultiLevelStringReference.eleTagNames;
			}
		}

		// Token: 0x17005633 RID: 22067
		// (get) Token: 0x06011EE0 RID: 73440 RVA: 0x002F3C21 File Offset: 0x002F1E21
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MultiLevelStringReference.eleNamespaceIds;
			}
		}

		// Token: 0x17005634 RID: 22068
		// (get) Token: 0x06011EE1 RID: 73441 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005635 RID: 22069
		// (get) Token: 0x06011EE2 RID: 73442 RVA: 0x002F3878 File Offset: 0x002F1A78
		// (set) Token: 0x06011EE3 RID: 73443 RVA: 0x002F3881 File Offset: 0x002F1A81
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(0);
			}
			set
			{
				base.SetElement<Formula>(0, value);
			}
		}

		// Token: 0x17005636 RID: 22070
		// (get) Token: 0x06011EE4 RID: 73444 RVA: 0x002F3C28 File Offset: 0x002F1E28
		// (set) Token: 0x06011EE5 RID: 73445 RVA: 0x002F3C31 File Offset: 0x002F1E31
		public MultiLevelStringCache MultiLevelStringCache
		{
			get
			{
				return base.GetElement<MultiLevelStringCache>(1);
			}
			set
			{
				base.SetElement<MultiLevelStringCache>(1, value);
			}
		}

		// Token: 0x17005637 RID: 22071
		// (get) Token: 0x06011EE6 RID: 73446 RVA: 0x002F389E File Offset: 0x002F1A9E
		// (set) Token: 0x06011EE7 RID: 73447 RVA: 0x002F38A7 File Offset: 0x002F1AA7
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(2);
			}
			set
			{
				base.SetElement<ExtensionList>(2, value);
			}
		}

		// Token: 0x06011EE8 RID: 73448 RVA: 0x002F3C3B File Offset: 0x002F1E3B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MultiLevelStringReference>(deep);
		}

		// Token: 0x04007D49 RID: 32073
		private const string tagName = "multiLvlStrRef";

		// Token: 0x04007D4A RID: 32074
		private const byte tagNsId = 11;

		// Token: 0x04007D4B RID: 32075
		internal const int ElementTypeIdConst = 10403;

		// Token: 0x04007D4C RID: 32076
		private static readonly string[] eleTagNames = new string[] { "f", "multiLvlStrCache", "extLst" };

		// Token: 0x04007D4D RID: 32077
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11 };
	}
}
