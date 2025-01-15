using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F1B RID: 12059
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AbstractNum))]
	[ChildElementInfo(typeof(NumberingIdMacAtCleanup))]
	[ChildElementInfo(typeof(NumberingPictureBullet))]
	[ChildElementInfo(typeof(NumberingInstance))]
	internal class Numbering : OpenXmlPartRootElement
	{
		// Token: 0x17008E88 RID: 36488
		// (get) Token: 0x06019CAC RID: 105644 RVA: 0x002A6AD3 File Offset: 0x002A4CD3
		public override string LocalName
		{
			get
			{
				return "numbering";
			}
		}

		// Token: 0x17008E89 RID: 36489
		// (get) Token: 0x06019CAD RID: 105645 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E8A RID: 36490
		// (get) Token: 0x06019CAE RID: 105646 RVA: 0x003568FA File Offset: 0x00354AFA
		internal override int ElementTypeId
		{
			get
			{
				return 11700;
			}
		}

		// Token: 0x06019CAF RID: 105647 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019CB0 RID: 105648 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Numbering(NumberingDefinitionsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019CB1 RID: 105649 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(NumberingDefinitionsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17008E8B RID: 36491
		// (get) Token: 0x06019CB2 RID: 105650 RVA: 0x00356901 File Offset: 0x00354B01
		// (set) Token: 0x06019CB3 RID: 105651 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public NumberingDefinitionsPart NumberingDefinitionsPart
		{
			get
			{
				return base.OpenXmlPart as NumberingDefinitionsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06019CB4 RID: 105652 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Numbering(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CB5 RID: 105653 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Numbering(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CB6 RID: 105654 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Numbering(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019CB7 RID: 105655 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Numbering()
		{
		}

		// Token: 0x06019CB8 RID: 105656 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(NumberingDefinitionsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06019CB9 RID: 105657 RVA: 0x00356910 File Offset: 0x00354B10
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "numPicBullet" == name)
			{
				return new NumberingPictureBullet();
			}
			if (23 == namespaceId && "abstractNum" == name)
			{
				return new AbstractNum();
			}
			if (23 == namespaceId && "num" == name)
			{
				return new NumberingInstance();
			}
			if (23 == namespaceId && "numIdMacAtCleanup" == name)
			{
				return new NumberingIdMacAtCleanup();
			}
			return null;
		}

		// Token: 0x06019CBA RID: 105658 RVA: 0x0035697E File Offset: 0x00354B7E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Numbering>(deep);
		}

		// Token: 0x0400AA7E RID: 43646
		private const string tagName = "numbering";

		// Token: 0x0400AA7F RID: 43647
		private const byte tagNsId = 23;

		// Token: 0x0400AA80 RID: 43648
		internal const int ElementTypeIdConst = 11700;
	}
}
