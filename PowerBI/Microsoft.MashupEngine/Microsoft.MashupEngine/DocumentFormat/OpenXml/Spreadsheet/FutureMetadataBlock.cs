using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BFD RID: 11261
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class FutureMetadataBlock : OpenXmlCompositeElement
	{
		// Token: 0x17007F19 RID: 32537
		// (get) Token: 0x06017AAB RID: 96939 RVA: 0x00339A68 File Offset: 0x00337C68
		public override string LocalName
		{
			get
			{
				return "bk";
			}
		}

		// Token: 0x17007F1A RID: 32538
		// (get) Token: 0x06017AAC RID: 96940 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F1B RID: 32539
		// (get) Token: 0x06017AAD RID: 96941 RVA: 0x00339B2F File Offset: 0x00337D2F
		internal override int ElementTypeId
		{
			get
			{
				return 11240;
			}
		}

		// Token: 0x06017AAE RID: 96942 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017AAF RID: 96943 RVA: 0x00293ECF File Offset: 0x002920CF
		public FutureMetadataBlock()
		{
		}

		// Token: 0x06017AB0 RID: 96944 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FutureMetadataBlock(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017AB1 RID: 96945 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FutureMetadataBlock(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017AB2 RID: 96946 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FutureMetadataBlock(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017AB3 RID: 96947 RVA: 0x003328DF File Offset: 0x00330ADF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007F1C RID: 32540
		// (get) Token: 0x06017AB4 RID: 96948 RVA: 0x00339B36 File Offset: 0x00337D36
		internal override string[] ElementTagNames
		{
			get
			{
				return FutureMetadataBlock.eleTagNames;
			}
		}

		// Token: 0x17007F1D RID: 32541
		// (get) Token: 0x06017AB5 RID: 96949 RVA: 0x00339B3D File Offset: 0x00337D3D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FutureMetadataBlock.eleNamespaceIds;
			}
		}

		// Token: 0x17007F1E RID: 32542
		// (get) Token: 0x06017AB6 RID: 96950 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007F1F RID: 32543
		// (get) Token: 0x06017AB7 RID: 96951 RVA: 0x00332908 File Offset: 0x00330B08
		// (set) Token: 0x06017AB8 RID: 96952 RVA: 0x00332911 File Offset: 0x00330B11
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06017AB9 RID: 96953 RVA: 0x00339B44 File Offset: 0x00337D44
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FutureMetadataBlock>(deep);
		}

		// Token: 0x04009D16 RID: 40214
		private const string tagName = "bk";

		// Token: 0x04009D17 RID: 40215
		private const byte tagNsId = 22;

		// Token: 0x04009D18 RID: 40216
		internal const int ElementTypeIdConst = 11240;

		// Token: 0x04009D19 RID: 40217
		private static readonly string[] eleTagNames = new string[] { "extLst" };

		// Token: 0x04009D1A RID: 40218
		private static readonly byte[] eleNamespaceIds = new byte[] { 22 };
	}
}
