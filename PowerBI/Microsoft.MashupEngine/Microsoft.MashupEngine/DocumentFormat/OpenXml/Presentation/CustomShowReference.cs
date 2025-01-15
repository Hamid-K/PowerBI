using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F2 RID: 10738
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomShowReference : OpenXmlLeafElement
	{
		// Token: 0x17006E70 RID: 28272
		// (get) Token: 0x060155D0 RID: 87504 RVA: 0x0031E33C File Offset: 0x0031C53C
		public override string LocalName
		{
			get
			{
				return "custShow";
			}
		}

		// Token: 0x17006E71 RID: 28273
		// (get) Token: 0x060155D1 RID: 87505 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006E72 RID: 28274
		// (get) Token: 0x060155D2 RID: 87506 RVA: 0x0031E343 File Offset: 0x0031C543
		internal override int ElementTypeId
		{
			get
			{
				return 12164;
			}
		}

		// Token: 0x060155D3 RID: 87507 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E73 RID: 28275
		// (get) Token: 0x060155D4 RID: 87508 RVA: 0x0031E34A File Offset: 0x0031C54A
		internal override string[] AttributeTagNames
		{
			get
			{
				return CustomShowReference.attributeTagNames;
			}
		}

		// Token: 0x17006E74 RID: 28276
		// (get) Token: 0x060155D5 RID: 87509 RVA: 0x0031E351 File Offset: 0x0031C551
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CustomShowReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E75 RID: 28277
		// (get) Token: 0x060155D6 RID: 87510 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060155D7 RID: 87511 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x060155D9 RID: 87513 RVA: 0x002E554A File Offset: 0x002E374A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060155DA RID: 87514 RVA: 0x0031E358 File Offset: 0x0031C558
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomShowReference>(deep);
		}

		// Token: 0x060155DB RID: 87515 RVA: 0x0031E364 File Offset: 0x0031C564
		// Note: this type is marked as 'beforefieldinit'.
		static CustomShowReference()
		{
			byte[] array = new byte[1];
			CustomShowReference.attributeNamespaceIds = array;
		}

		// Token: 0x0400932F RID: 37679
		private const string tagName = "custShow";

		// Token: 0x04009330 RID: 37680
		private const byte tagNsId = 24;

		// Token: 0x04009331 RID: 37681
		internal const int ElementTypeIdConst = 12164;

		// Token: 0x04009332 RID: 37682
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x04009333 RID: 37683
		private static byte[] attributeNamespaceIds;
	}
}
