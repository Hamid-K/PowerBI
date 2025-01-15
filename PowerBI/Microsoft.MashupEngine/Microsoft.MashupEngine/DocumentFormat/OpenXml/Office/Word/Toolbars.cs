using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x0200247D RID: 9341
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AllocatedCommandManifest))]
	[ChildElementInfo(typeof(ToolbarData))]
	internal class Toolbars : OpenXmlCompositeElement
	{
		// Token: 0x1700513A RID: 20794
		// (get) Token: 0x060113C7 RID: 70599 RVA: 0x002EBF2C File Offset: 0x002EA12C
		public override string LocalName
		{
			get
			{
				return "toolbars";
			}
		}

		// Token: 0x1700513B RID: 20795
		// (get) Token: 0x060113C8 RID: 70600 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700513C RID: 20796
		// (get) Token: 0x060113C9 RID: 70601 RVA: 0x002EBF33 File Offset: 0x002EA133
		internal override int ElementTypeId
		{
			get
			{
				return 12568;
			}
		}

		// Token: 0x060113CA RID: 70602 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060113CB RID: 70603 RVA: 0x00293ECF File Offset: 0x002920CF
		public Toolbars()
		{
		}

		// Token: 0x060113CC RID: 70604 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Toolbars(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113CD RID: 70605 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Toolbars(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113CE RID: 70606 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Toolbars(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060113CF RID: 70607 RVA: 0x002EBF3A File Offset: 0x002EA13A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "acdManifest" == name)
			{
				return new AllocatedCommandManifest();
			}
			if (33 == namespaceId && "toolbarData" == name)
			{
				return new ToolbarData();
			}
			return null;
		}

		// Token: 0x060113D0 RID: 70608 RVA: 0x002EBF6D File Offset: 0x002EA16D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Toolbars>(deep);
		}

		// Token: 0x040078CC RID: 30924
		private const string tagName = "toolbars";

		// Token: 0x040078CD RID: 30925
		private const byte tagNsId = 33;

		// Token: 0x040078CE RID: 30926
		internal const int ElementTypeIdConst = 12568;
	}
}
