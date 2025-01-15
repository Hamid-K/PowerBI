using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B1 RID: 9649
	[GeneratedCode("DomGen", "2.0")]
	internal class PictureFormat : OpenXmlLeafElement
	{
		// Token: 0x17005730 RID: 22320
		// (get) Token: 0x0601210B RID: 73995 RVA: 0x002F52FD File Offset: 0x002F34FD
		public override string LocalName
		{
			get
			{
				return "pictureFormat";
			}
		}

		// Token: 0x17005731 RID: 22321
		// (get) Token: 0x0601210C RID: 73996 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005732 RID: 22322
		// (get) Token: 0x0601210D RID: 73997 RVA: 0x002F5304 File Offset: 0x002F3504
		internal override int ElementTypeId
		{
			get
			{
				return 10472;
			}
		}

		// Token: 0x0601210E RID: 73998 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005733 RID: 22323
		// (get) Token: 0x0601210F RID: 73999 RVA: 0x002F530B File Offset: 0x002F350B
		internal override string[] AttributeTagNames
		{
			get
			{
				return PictureFormat.attributeTagNames;
			}
		}

		// Token: 0x17005734 RID: 22324
		// (get) Token: 0x06012110 RID: 74000 RVA: 0x002F5312 File Offset: 0x002F3512
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PictureFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17005735 RID: 22325
		// (get) Token: 0x06012111 RID: 74001 RVA: 0x002F5319 File Offset: 0x002F3519
		// (set) Token: 0x06012112 RID: 74002 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<PictureFormatValues> Val
		{
			get
			{
				return (EnumValue<PictureFormatValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012114 RID: 74004 RVA: 0x002F5328 File Offset: 0x002F3528
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<PictureFormatValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012115 RID: 74005 RVA: 0x002F5348 File Offset: 0x002F3548
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PictureFormat>(deep);
		}

		// Token: 0x06012116 RID: 74006 RVA: 0x002F5354 File Offset: 0x002F3554
		// Note: this type is marked as 'beforefieldinit'.
		static PictureFormat()
		{
			byte[] array = new byte[1];
			PictureFormat.attributeNamespaceIds = array;
		}

		// Token: 0x04007E02 RID: 32258
		private const string tagName = "pictureFormat";

		// Token: 0x04007E03 RID: 32259
		private const byte tagNsId = 11;

		// Token: 0x04007E04 RID: 32260
		internal const int ElementTypeIdConst = 10472;

		// Token: 0x04007E05 RID: 32261
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E06 RID: 32262
		private static byte[] attributeNamespaceIds;
	}
}
