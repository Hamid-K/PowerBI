using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F85 RID: 12165
	[GeneratedCode("DomGen", "2.0")]
	internal class PixelsPerInch : OpenXmlLeafElement
	{
		// Token: 0x1700919E RID: 37278
		// (get) Token: 0x0601A375 RID: 107381 RVA: 0x0035F310 File Offset: 0x0035D510
		public override string LocalName
		{
			get
			{
				return "pixelsPerInch";
			}
		}

		// Token: 0x1700919F RID: 37279
		// (get) Token: 0x0601A376 RID: 107382 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091A0 RID: 37280
		// (get) Token: 0x0601A377 RID: 107383 RVA: 0x0035F317 File Offset: 0x0035D517
		internal override int ElementTypeId
		{
			get
			{
				return 11845;
			}
		}

		// Token: 0x0601A378 RID: 107384 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170091A1 RID: 37281
		// (get) Token: 0x0601A379 RID: 107385 RVA: 0x0035F31E File Offset: 0x0035D51E
		internal override string[] AttributeTagNames
		{
			get
			{
				return PixelsPerInch.attributeTagNames;
			}
		}

		// Token: 0x170091A2 RID: 37282
		// (get) Token: 0x0601A37A RID: 107386 RVA: 0x0035F325 File Offset: 0x0035D525
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PixelsPerInch.attributeNamespaceIds;
			}
		}

		// Token: 0x170091A3 RID: 37283
		// (get) Token: 0x0601A37B RID: 107387 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601A37C RID: 107388 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int32Value Val
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A37E RID: 107390 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A37F RID: 107391 RVA: 0x0035F32C File Offset: 0x0035D52C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PixelsPerInch>(deep);
		}

		// Token: 0x0400AC3A RID: 44090
		private const string tagName = "pixelsPerInch";

		// Token: 0x0400AC3B RID: 44091
		private const byte tagNsId = 23;

		// Token: 0x0400AC3C RID: 44092
		internal const int ElementTypeIdConst = 11845;

		// Token: 0x0400AC3D RID: 44093
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC3E RID: 44094
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
