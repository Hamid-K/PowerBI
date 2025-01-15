using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B9 RID: 9401
	[ChildElementInfo(typeof(Checked), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(CheckedState), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(UncheckedState), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SdtContentCheckBox : OpenXmlCompositeElement
	{
		// Token: 0x1700527A RID: 21114
		// (get) Token: 0x0601167F RID: 71295 RVA: 0x002EE29C File Offset: 0x002EC49C
		public override string LocalName
		{
			get
			{
				return "checkbox";
			}
		}

		// Token: 0x1700527B RID: 21115
		// (get) Token: 0x06011680 RID: 71296 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x1700527C RID: 21116
		// (get) Token: 0x06011681 RID: 71297 RVA: 0x002EE2A3 File Offset: 0x002EC4A3
		internal override int ElementTypeId
		{
			get
			{
				return 12875;
			}
		}

		// Token: 0x06011682 RID: 71298 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011683 RID: 71299 RVA: 0x00293ECF File Offset: 0x002920CF
		public SdtContentCheckBox()
		{
		}

		// Token: 0x06011684 RID: 71300 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SdtContentCheckBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011685 RID: 71301 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SdtContentCheckBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011686 RID: 71302 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SdtContentCheckBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011687 RID: 71303 RVA: 0x002EE2AC File Offset: 0x002EC4AC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "checked" == name)
			{
				return new Checked();
			}
			if (52 == namespaceId && "checkedState" == name)
			{
				return new CheckedState();
			}
			if (52 == namespaceId && "uncheckedState" == name)
			{
				return new UncheckedState();
			}
			return null;
		}

		// Token: 0x1700527D RID: 21117
		// (get) Token: 0x06011688 RID: 71304 RVA: 0x002EE302 File Offset: 0x002EC502
		internal override string[] ElementTagNames
		{
			get
			{
				return SdtContentCheckBox.eleTagNames;
			}
		}

		// Token: 0x1700527E RID: 21118
		// (get) Token: 0x06011689 RID: 71305 RVA: 0x002EE309 File Offset: 0x002EC509
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SdtContentCheckBox.eleNamespaceIds;
			}
		}

		// Token: 0x1700527F RID: 21119
		// (get) Token: 0x0601168A RID: 71306 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005280 RID: 21120
		// (get) Token: 0x0601168B RID: 71307 RVA: 0x002EE310 File Offset: 0x002EC510
		// (set) Token: 0x0601168C RID: 71308 RVA: 0x002EE319 File Offset: 0x002EC519
		public Checked Checked
		{
			get
			{
				return base.GetElement<Checked>(0);
			}
			set
			{
				base.SetElement<Checked>(0, value);
			}
		}

		// Token: 0x17005281 RID: 21121
		// (get) Token: 0x0601168D RID: 71309 RVA: 0x002EE323 File Offset: 0x002EC523
		// (set) Token: 0x0601168E RID: 71310 RVA: 0x002EE32C File Offset: 0x002EC52C
		public CheckedState CheckedState
		{
			get
			{
				return base.GetElement<CheckedState>(1);
			}
			set
			{
				base.SetElement<CheckedState>(1, value);
			}
		}

		// Token: 0x17005282 RID: 21122
		// (get) Token: 0x0601168F RID: 71311 RVA: 0x002EE336 File Offset: 0x002EC536
		// (set) Token: 0x06011690 RID: 71312 RVA: 0x002EE33F File Offset: 0x002EC53F
		public UncheckedState UncheckedState
		{
			get
			{
				return base.GetElement<UncheckedState>(2);
			}
			set
			{
				base.SetElement<UncheckedState>(2, value);
			}
		}

		// Token: 0x06011691 RID: 71313 RVA: 0x002EE349 File Offset: 0x002EC549
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentCheckBox>(deep);
		}

		// Token: 0x040079B3 RID: 31155
		private const string tagName = "checkbox";

		// Token: 0x040079B4 RID: 31156
		private const byte tagNsId = 52;

		// Token: 0x040079B5 RID: 31157
		internal const int ElementTypeIdConst = 12875;

		// Token: 0x040079B6 RID: 31158
		private static readonly string[] eleTagNames = new string[] { "checked", "checkedState", "uncheckedState" };

		// Token: 0x040079B7 RID: 31159
		private static readonly byte[] eleNamespaceIds = new byte[] { 52, 52, 52 };
	}
}
