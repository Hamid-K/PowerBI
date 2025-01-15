using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F14 RID: 12052
	[ChildElementInfo(typeof(Endnote))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Endnotes : OpenXmlPartRootElement
	{
		// Token: 0x17008E48 RID: 36424
		// (get) Token: 0x06019C01 RID: 105473 RVA: 0x002A4F13 File Offset: 0x002A3113
		public override string LocalName
		{
			get
			{
				return "endnotes";
			}
		}

		// Token: 0x17008E49 RID: 36425
		// (get) Token: 0x06019C02 RID: 105474 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E4A RID: 36426
		// (get) Token: 0x06019C03 RID: 105475 RVA: 0x00354BDB File Offset: 0x00352DDB
		internal override int ElementTypeId
		{
			get
			{
				return 11694;
			}
		}

		// Token: 0x06019C04 RID: 105476 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019C05 RID: 105477 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Endnotes(EndnotesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019C06 RID: 105478 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(EndnotesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E4B RID: 36427
		// (get) Token: 0x06019C07 RID: 105479 RVA: 0x00354BE2 File Offset: 0x00352DE2
		// (set) Token: 0x06019C08 RID: 105480 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public EndnotesPart EndnotesPart
		{
			get
			{
				return base.OpenXmlPart as EndnotesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019C09 RID: 105481 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Endnotes(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C0A RID: 105482 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Endnotes(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019C0B RID: 105483 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Endnotes(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019C0C RID: 105484 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Endnotes()
		{
		}

		// Token: 0x06019C0D RID: 105485 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(EndnotesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019C0E RID: 105486 RVA: 0x00354BEF File Offset: 0x00352DEF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "endnote" == name)
			{
				return new Endnote();
			}
			return null;
		}

		// Token: 0x06019C0F RID: 105487 RVA: 0x00354C0A File Offset: 0x00352E0A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Endnotes>(deep);
		}

		// Token: 0x0400AA68 RID: 43624
		private const string tagName = "endnotes";

		// Token: 0x0400AA69 RID: 43625
		private const byte tagNsId = 23;

		// Token: 0x0400AA6A RID: 43626
		internal const int ElementTypeIdConst = 11694;
	}
}
