using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x0200230D RID: 8973
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ContextMenu), FileFormatVersions.Office2010)]
	internal class ContextMenus : OpenXmlCompositeElement
	{
		// Token: 0x17004823 RID: 18467
		// (get) Token: 0x0600FF8A RID: 65418 RVA: 0x002DE08B File Offset: 0x002DC28B
		public override string LocalName
		{
			get
			{
				return "contextMenus";
			}
		}

		// Token: 0x17004824 RID: 18468
		// (get) Token: 0x0600FF8B RID: 65419 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x17004825 RID: 18469
		// (get) Token: 0x0600FF8C RID: 65420 RVA: 0x002DE092 File Offset: 0x002DC292
		internal override int ElementTypeId
		{
			get
			{
				return 13115;
			}
		}

		// Token: 0x0600FF8D RID: 65421 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FF8E RID: 65422 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContextMenus()
		{
		}

		// Token: 0x0600FF8F RID: 65423 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContextMenus(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF90 RID: 65424 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContextMenus(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FF91 RID: 65425 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContextMenus(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FF92 RID: 65426 RVA: 0x002DE099 File Offset: 0x002DC299
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (57 == namespaceId && "contextMenu" == name)
			{
				return new ContextMenu();
			}
			return null;
		}

		// Token: 0x0600FF93 RID: 65427 RVA: 0x002DE0B4 File Offset: 0x002DC2B4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextMenus>(deep);
		}

		// Token: 0x04007251 RID: 29265
		private const string tagName = "contextMenus";

		// Token: 0x04007252 RID: 29266
		private const byte tagNsId = 57;

		// Token: 0x04007253 RID: 29267
		internal const int ElementTypeIdConst = 13115;
	}
}
