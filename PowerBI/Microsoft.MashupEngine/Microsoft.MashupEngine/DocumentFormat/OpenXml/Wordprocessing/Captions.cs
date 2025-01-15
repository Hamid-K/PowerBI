using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF9 RID: 12281
	[ChildElementInfo(typeof(AutoCaptions))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Caption))]
	internal class Captions : OpenXmlCompositeElement
	{
		// Token: 0x170095BB RID: 38331
		// (get) Token: 0x0601AC2C RID: 109612 RVA: 0x003673F1 File Offset: 0x003655F1
		public override string LocalName
		{
			get
			{
				return "captions";
			}
		}

		// Token: 0x170095BC RID: 38332
		// (get) Token: 0x0601AC2D RID: 109613 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095BD RID: 38333
		// (get) Token: 0x0601AC2E RID: 109614 RVA: 0x003673F8 File Offset: 0x003655F8
		internal override int ElementTypeId
		{
			get
			{
				return 12048;
			}
		}

		// Token: 0x0601AC2F RID: 109615 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AC30 RID: 109616 RVA: 0x00293ECF File Offset: 0x002920CF
		public Captions()
		{
		}

		// Token: 0x0601AC31 RID: 109617 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Captions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AC32 RID: 109618 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Captions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AC33 RID: 109619 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Captions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AC34 RID: 109620 RVA: 0x003673FF File Offset: 0x003655FF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "caption" == name)
			{
				return new Caption();
			}
			if (23 == namespaceId && "autoCaptions" == name)
			{
				return new AutoCaptions();
			}
			return null;
		}

		// Token: 0x0601AC35 RID: 109621 RVA: 0x00367432 File Offset: 0x00365632
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Captions>(deep);
		}

		// Token: 0x0400AE33 RID: 44595
		private const string tagName = "captions";

		// Token: 0x0400AE34 RID: 44596
		private const byte tagNsId = 23;

		// Token: 0x0400AE35 RID: 44597
		internal const int ElementTypeIdConst = 12048;
	}
}
