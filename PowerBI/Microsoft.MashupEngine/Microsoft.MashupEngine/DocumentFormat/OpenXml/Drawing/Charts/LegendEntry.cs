using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025C3 RID: 9667
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(Delete))]
	[ChildElementInfo(typeof(Index))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LegendEntry : OpenXmlCompositeElement
	{
		// Token: 0x17005790 RID: 22416
		// (get) Token: 0x060121D5 RID: 74197 RVA: 0x002F5AE3 File Offset: 0x002F3CE3
		public override string LocalName
		{
			get
			{
				return "legendEntry";
			}
		}

		// Token: 0x17005791 RID: 22417
		// (get) Token: 0x060121D6 RID: 74198 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005792 RID: 22418
		// (get) Token: 0x060121D7 RID: 74199 RVA: 0x002F5AEA File Offset: 0x002F3CEA
		internal override int ElementTypeId
		{
			get
			{
				return 10493;
			}
		}

		// Token: 0x060121D8 RID: 74200 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060121D9 RID: 74201 RVA: 0x00293ECF File Offset: 0x002920CF
		public LegendEntry()
		{
		}

		// Token: 0x060121DA RID: 74202 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LegendEntry(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121DB RID: 74203 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LegendEntry(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060121DC RID: 74204 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LegendEntry(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060121DD RID: 74205 RVA: 0x002F5AF4 File Offset: 0x002F3CF4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "idx" == name)
			{
				return new Index();
			}
			if (11 == namespaceId && "delete" == name)
			{
				return new Delete();
			}
			if (11 == namespaceId && "txPr" == name)
			{
				return new TextProperties();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005793 RID: 22419
		// (get) Token: 0x060121DE RID: 74206 RVA: 0x002F5B62 File Offset: 0x002F3D62
		internal override string[] ElementTagNames
		{
			get
			{
				return LegendEntry.eleTagNames;
			}
		}

		// Token: 0x17005794 RID: 22420
		// (get) Token: 0x060121DF RID: 74207 RVA: 0x002F5B69 File Offset: 0x002F3D69
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LegendEntry.eleNamespaceIds;
			}
		}

		// Token: 0x17005795 RID: 22421
		// (get) Token: 0x060121E0 RID: 74208 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005796 RID: 22422
		// (get) Token: 0x060121E1 RID: 74209 RVA: 0x002F1CB8 File Offset: 0x002EFEB8
		// (set) Token: 0x060121E2 RID: 74210 RVA: 0x002F1CC1 File Offset: 0x002EFEC1
		public Index Index
		{
			get
			{
				return base.GetElement<Index>(0);
			}
			set
			{
				base.SetElement<Index>(0, value);
			}
		}

		// Token: 0x060121E3 RID: 74211 RVA: 0x002F5B70 File Offset: 0x002F3D70
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LegendEntry>(deep);
		}

		// Token: 0x04007E4C RID: 32332
		private const string tagName = "legendEntry";

		// Token: 0x04007E4D RID: 32333
		private const byte tagNsId = 11;

		// Token: 0x04007E4E RID: 32334
		internal const int ElementTypeIdConst = 10493;

		// Token: 0x04007E4F RID: 32335
		private static readonly string[] eleTagNames = new string[] { "idx", "delete", "txPr", "extLst" };

		// Token: 0x04007E50 RID: 32336
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11 };
	}
}
