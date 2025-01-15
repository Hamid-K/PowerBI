using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BFB RID: 11259
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MetadataRecord))]
	internal class MetadataBlock : OpenXmlCompositeElement
	{
		// Token: 0x17007F0F RID: 32527
		// (get) Token: 0x06017A93 RID: 96915 RVA: 0x00339A68 File Offset: 0x00337C68
		public override string LocalName
		{
			get
			{
				return "bk";
			}
		}

		// Token: 0x17007F10 RID: 32528
		// (get) Token: 0x06017A94 RID: 96916 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F11 RID: 32529
		// (get) Token: 0x06017A95 RID: 96917 RVA: 0x00339A6F File Offset: 0x00337C6F
		internal override int ElementTypeId
		{
			get
			{
				return 11238;
			}
		}

		// Token: 0x06017A96 RID: 96918 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017A97 RID: 96919 RVA: 0x00293ECF File Offset: 0x002920CF
		public MetadataBlock()
		{
		}

		// Token: 0x06017A98 RID: 96920 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MetadataBlock(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A99 RID: 96921 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MetadataBlock(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017A9A RID: 96922 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MetadataBlock(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017A9B RID: 96923 RVA: 0x00339A76 File Offset: 0x00337C76
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "rc" == name)
			{
				return new MetadataRecord();
			}
			return null;
		}

		// Token: 0x06017A9C RID: 96924 RVA: 0x00339A91 File Offset: 0x00337C91
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MetadataBlock>(deep);
		}

		// Token: 0x04009D0E RID: 40206
		private const string tagName = "bk";

		// Token: 0x04009D0F RID: 40207
		private const byte tagNsId = 22;

		// Token: 0x04009D10 RID: 40208
		internal const int ElementTypeIdConst = 11238;
	}
}
