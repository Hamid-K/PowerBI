using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F1A RID: 12058
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Font))]
	internal class Fonts : OpenXmlPartRootElement
	{
		// Token: 0x17008E84 RID: 36484
		// (get) Token: 0x06019C9D RID: 105629 RVA: 0x002AD888 File Offset: 0x002ABA88
		public override string LocalName
		{
			get
			{
				return "fonts";
			}
		}

		// Token: 0x17008E85 RID: 36485
		// (get) Token: 0x06019C9E RID: 105630 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E86 RID: 36486
		// (get) Token: 0x06019C9F RID: 105631 RVA: 0x003568C2 File Offset: 0x00354AC2
		internal override int ElementTypeId
		{
			get
			{
				return 11699;
			}
		}

		// Token: 0x06019CA0 RID: 105632 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019CA1 RID: 105633 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Fonts(FontTablePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019CA2 RID: 105634 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(FontTablePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E87 RID: 36487
		// (get) Token: 0x06019CA3 RID: 105635 RVA: 0x003568C9 File Offset: 0x00354AC9
		// (set) Token: 0x06019CA4 RID: 105636 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public FontTablePart FontTablePart
		{
			get
			{
				return base.OpenXmlPart as FontTablePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019CA5 RID: 105637 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Fonts(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CA6 RID: 105638 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Fonts(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CA7 RID: 105639 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Fonts(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019CA8 RID: 105640 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Fonts()
		{
		}

		// Token: 0x06019CA9 RID: 105641 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(FontTablePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019CAA RID: 105642 RVA: 0x003568D6 File Offset: 0x00354AD6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "font" == name)
			{
				return new Font();
			}
			return null;
		}

		// Token: 0x06019CAB RID: 105643 RVA: 0x003568F1 File Offset: 0x00354AF1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fonts>(deep);
		}

		// Token: 0x0400AA7B RID: 43643
		private const string tagName = "fonts";

		// Token: 0x0400AA7C RID: 43644
		private const byte tagNsId = 23;

		// Token: 0x0400AA7D RID: 43645
		internal const int ElementTypeIdConst = 11699;
	}
}
