using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B27 RID: 11047
	[ChildElementInfo(typeof(ExternalBook))]
	[ChildElementInfo(typeof(DdeLink))]
	[ChildElementInfo(typeof(OleLink))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExternalLink : OpenXmlPartRootElement
	{
		// Token: 0x170076F7 RID: 30455
		// (get) Token: 0x060168FF RID: 92415 RVA: 0x0032C6F8 File Offset: 0x0032A8F8
		public override string LocalName
		{
			get
			{
				return "externalLink";
			}
		}

		// Token: 0x170076F8 RID: 30456
		// (get) Token: 0x06016900 RID: 92416 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170076F9 RID: 30457
		// (get) Token: 0x06016901 RID: 92417 RVA: 0x0032C6FF File Offset: 0x0032A8FF
		internal override int ElementTypeId
		{
			get
			{
				return 11045;
			}
		}

		// Token: 0x06016902 RID: 92418 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016903 RID: 92419 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal ExternalLink(ExternalWorkbookPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016904 RID: 92420 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(ExternalWorkbookPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170076FA RID: 30458
		// (get) Token: 0x06016905 RID: 92421 RVA: 0x0032C706 File Offset: 0x0032A906
		// (set) Token: 0x06016906 RID: 92422 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public ExternalWorkbookPart ExternalWorkbookPart
		{
			get
			{
				return base.OpenXmlPart as ExternalWorkbookPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016907 RID: 92423 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public ExternalLink(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016908 RID: 92424 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public ExternalLink(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016909 RID: 92425 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public ExternalLink(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601690A RID: 92426 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public ExternalLink()
		{
		}

		// Token: 0x0601690B RID: 92427 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(ExternalWorkbookPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601690C RID: 92428 RVA: 0x0032C714 File Offset: 0x0032A914
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "externalBook" == name)
			{
				return new ExternalBook();
			}
			if (22 == namespaceId && "ddeLink" == name)
			{
				return new DdeLink();
			}
			if (22 == namespaceId && "oleLink" == name)
			{
				return new OleLink();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x0601690D RID: 92429 RVA: 0x0032C782 File Offset: 0x0032A982
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalLink>(deep);
		}

		// Token: 0x04009922 RID: 39202
		private const string tagName = "externalLink";

		// Token: 0x04009923 RID: 39203
		private const byte tagNsId = 22;

		// Token: 0x04009924 RID: 39204
		internal const int ElementTypeIdConst = 11045;
	}
}
