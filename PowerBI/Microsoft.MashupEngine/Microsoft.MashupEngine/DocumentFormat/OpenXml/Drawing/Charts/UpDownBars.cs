using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025AA RID: 9642
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(DownBars))]
	[ChildElementInfo(typeof(GapWidth))]
	[ChildElementInfo(typeof(UpBars))]
	internal class UpDownBars : OpenXmlCompositeElement
	{
		// Token: 0x17005703 RID: 22275
		// (get) Token: 0x060120A9 RID: 73897 RVA: 0x002F4F49 File Offset: 0x002F3149
		public override string LocalName
		{
			get
			{
				return "upDownBars";
			}
		}

		// Token: 0x17005704 RID: 22276
		// (get) Token: 0x060120AA RID: 73898 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005705 RID: 22277
		// (get) Token: 0x060120AB RID: 73899 RVA: 0x002F4F50 File Offset: 0x002F3150
		internal override int ElementTypeId
		{
			get
			{
				return 10457;
			}
		}

		// Token: 0x060120AC RID: 73900 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060120AD RID: 73901 RVA: 0x00293ECF File Offset: 0x002920CF
		public UpDownBars()
		{
		}

		// Token: 0x060120AE RID: 73902 RVA: 0x00293ED7 File Offset: 0x002920D7
		public UpDownBars(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060120AF RID: 73903 RVA: 0x00293EE0 File Offset: 0x002920E0
		public UpDownBars(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060120B0 RID: 73904 RVA: 0x00293EE9 File Offset: 0x002920E9
		public UpDownBars(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060120B1 RID: 73905 RVA: 0x002F4F58 File Offset: 0x002F3158
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "gapWidth" == name)
			{
				return new GapWidth();
			}
			if (11 == namespaceId && "upBars" == name)
			{
				return new UpBars();
			}
			if (11 == namespaceId && "downBars" == name)
			{
				return new DownBars();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005706 RID: 22278
		// (get) Token: 0x060120B2 RID: 73906 RVA: 0x002F4FC6 File Offset: 0x002F31C6
		internal override string[] ElementTagNames
		{
			get
			{
				return UpDownBars.eleTagNames;
			}
		}

		// Token: 0x17005707 RID: 22279
		// (get) Token: 0x060120B3 RID: 73907 RVA: 0x002F4FCD File Offset: 0x002F31CD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return UpDownBars.eleNamespaceIds;
			}
		}

		// Token: 0x17005708 RID: 22280
		// (get) Token: 0x060120B4 RID: 73908 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005709 RID: 22281
		// (get) Token: 0x060120B5 RID: 73909 RVA: 0x002F4FD4 File Offset: 0x002F31D4
		// (set) Token: 0x060120B6 RID: 73910 RVA: 0x002F4FDD File Offset: 0x002F31DD
		public GapWidth GapWidth
		{
			get
			{
				return base.GetElement<GapWidth>(0);
			}
			set
			{
				base.SetElement<GapWidth>(0, value);
			}
		}

		// Token: 0x1700570A RID: 22282
		// (get) Token: 0x060120B7 RID: 73911 RVA: 0x002F4FE7 File Offset: 0x002F31E7
		// (set) Token: 0x060120B8 RID: 73912 RVA: 0x002F4FF0 File Offset: 0x002F31F0
		public UpBars UpBars
		{
			get
			{
				return base.GetElement<UpBars>(1);
			}
			set
			{
				base.SetElement<UpBars>(1, value);
			}
		}

		// Token: 0x1700570B RID: 22283
		// (get) Token: 0x060120B9 RID: 73913 RVA: 0x002F4FFA File Offset: 0x002F31FA
		// (set) Token: 0x060120BA RID: 73914 RVA: 0x002F5003 File Offset: 0x002F3203
		public DownBars DownBars
		{
			get
			{
				return base.GetElement<DownBars>(2);
			}
			set
			{
				base.SetElement<DownBars>(2, value);
			}
		}

		// Token: 0x1700570C RID: 22284
		// (get) Token: 0x060120BB RID: 73915 RVA: 0x002F4721 File Offset: 0x002F2921
		// (set) Token: 0x060120BC RID: 73916 RVA: 0x002F472A File Offset: 0x002F292A
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

		// Token: 0x060120BD RID: 73917 RVA: 0x002F500D File Offset: 0x002F320D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UpDownBars>(deep);
		}

		// Token: 0x04007DE1 RID: 32225
		private const string tagName = "upDownBars";

		// Token: 0x04007DE2 RID: 32226
		private const byte tagNsId = 11;

		// Token: 0x04007DE3 RID: 32227
		internal const int ElementTypeIdConst = 10457;

		// Token: 0x04007DE4 RID: 32228
		private static readonly string[] eleTagNames = new string[] { "gapWidth", "upBars", "downBars", "extLst" };

		// Token: 0x04007DE5 RID: 32229
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
