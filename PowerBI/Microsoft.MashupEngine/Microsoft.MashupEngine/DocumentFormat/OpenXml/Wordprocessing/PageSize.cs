using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E43 RID: 11843
	[GeneratedCode("DomGen", "2.0")]
	internal class PageSize : OpenXmlLeafElement
	{
		// Token: 0x170089D0 RID: 35280
		// (get) Token: 0x0601926F RID: 103023 RVA: 0x00346DB0 File Offset: 0x00344FB0
		public override string LocalName
		{
			get
			{
				return "pgSz";
			}
		}

		// Token: 0x170089D1 RID: 35281
		// (get) Token: 0x06019270 RID: 103024 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170089D2 RID: 35282
		// (get) Token: 0x06019271 RID: 103025 RVA: 0x00346DB7 File Offset: 0x00344FB7
		internal override int ElementTypeId
		{
			get
			{
				return 11529;
			}
		}

		// Token: 0x06019272 RID: 103026 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170089D3 RID: 35283
		// (get) Token: 0x06019273 RID: 103027 RVA: 0x00346DBE File Offset: 0x00344FBE
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageSize.attributeTagNames;
			}
		}

		// Token: 0x170089D4 RID: 35284
		// (get) Token: 0x06019274 RID: 103028 RVA: 0x00346DC5 File Offset: 0x00344FC5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageSize.attributeNamespaceIds;
			}
		}

		// Token: 0x170089D5 RID: 35285
		// (get) Token: 0x06019275 RID: 103029 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06019276 RID: 103030 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "w")]
		public UInt32Value Width
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170089D6 RID: 35286
		// (get) Token: 0x06019277 RID: 103031 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06019278 RID: 103032 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "h")]
		public UInt32Value Height
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170089D7 RID: 35287
		// (get) Token: 0x06019279 RID: 103033 RVA: 0x00346DCC File Offset: 0x00344FCC
		// (set) Token: 0x0601927A RID: 103034 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "orient")]
		public EnumValue<PageOrientationValues> Orient
		{
			get
			{
				return (EnumValue<PageOrientationValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170089D8 RID: 35288
		// (get) Token: 0x0601927B RID: 103035 RVA: 0x00343A0A File Offset: 0x00341C0A
		// (set) Token: 0x0601927C RID: 103036 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "code")]
		public UInt16Value Code
		{
			get
			{
				return (UInt16Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601927E RID: 103038 RVA: 0x00346DDC File Offset: 0x00344FDC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "w" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "h" == name)
			{
				return new UInt32Value();
			}
			if (23 == namespaceId && "orient" == name)
			{
				return new EnumValue<PageOrientationValues>();
			}
			if (23 == namespaceId && "code" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601927F RID: 103039 RVA: 0x00346E51 File Offset: 0x00345051
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageSize>(deep);
		}

		// Token: 0x0400A750 RID: 42832
		private const string tagName = "pgSz";

		// Token: 0x0400A751 RID: 42833
		private const byte tagNsId = 23;

		// Token: 0x0400A752 RID: 42834
		internal const int ElementTypeIdConst = 11529;

		// Token: 0x0400A753 RID: 42835
		private static string[] attributeTagNames = new string[] { "w", "h", "orient", "code" };

		// Token: 0x0400A754 RID: 42836
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
