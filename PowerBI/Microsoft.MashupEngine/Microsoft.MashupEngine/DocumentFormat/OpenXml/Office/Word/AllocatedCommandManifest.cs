using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002478 RID: 9336
	[ChildElementInfo(typeof(AllocatedCommandManifestEntry))]
	[GeneratedCode("DomGen", "2.0")]
	internal class AllocatedCommandManifest : OpenXmlCompositeElement
	{
		// Token: 0x1700512B RID: 20779
		// (get) Token: 0x0601139A RID: 70554 RVA: 0x002EBE32 File Offset: 0x002EA032
		public override string LocalName
		{
			get
			{
				return "acdManifest";
			}
		}

		// Token: 0x1700512C RID: 20780
		// (get) Token: 0x0601139B RID: 70555 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x1700512D RID: 20781
		// (get) Token: 0x0601139C RID: 70556 RVA: 0x002EBE39 File Offset: 0x002EA039
		internal override int ElementTypeId
		{
			get
			{
				return 12564;
			}
		}

		// Token: 0x0601139D RID: 70557 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601139E RID: 70558 RVA: 0x00293ECF File Offset: 0x002920CF
		public AllocatedCommandManifest()
		{
		}

		// Token: 0x0601139F RID: 70559 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AllocatedCommandManifest(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113A0 RID: 70560 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AllocatedCommandManifest(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060113A1 RID: 70561 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AllocatedCommandManifest(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060113A2 RID: 70562 RVA: 0x002EBE40 File Offset: 0x002EA040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "acdEntry" == name)
			{
				return new AllocatedCommandManifestEntry();
			}
			return null;
		}

		// Token: 0x060113A3 RID: 70563 RVA: 0x002EBE5B File Offset: 0x002EA05B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AllocatedCommandManifest>(deep);
		}

		// Token: 0x040078BE RID: 30910
		private const string tagName = "acdManifest";

		// Token: 0x040078BF RID: 30911
		private const byte tagNsId = 33;

		// Token: 0x040078C0 RID: 30912
		internal const int ElementTypeIdConst = 12564;
	}
}
