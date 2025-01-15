using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB0 RID: 12208
	[GeneratedCode("DomGen", "2.0")]
	internal class AltName : OpenXmlLeafElement
	{
		// Token: 0x1700937D RID: 37757
		// (get) Token: 0x0601A75E RID: 108382 RVA: 0x00362AF2 File Offset: 0x00360CF2
		public override string LocalName
		{
			get
			{
				return "altName";
			}
		}

		// Token: 0x1700937E RID: 37758
		// (get) Token: 0x0601A75F RID: 108383 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700937F RID: 37759
		// (get) Token: 0x0601A760 RID: 108384 RVA: 0x00362AF9 File Offset: 0x00360CF9
		internal override int ElementTypeId
		{
			get
			{
				return 11915;
			}
		}

		// Token: 0x0601A761 RID: 108385 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009380 RID: 37760
		// (get) Token: 0x0601A762 RID: 108386 RVA: 0x00362B00 File Offset: 0x00360D00
		internal override string[] AttributeTagNames
		{
			get
			{
				return AltName.attributeTagNames;
			}
		}

		// Token: 0x17009381 RID: 37761
		// (get) Token: 0x0601A763 RID: 108387 RVA: 0x00362B07 File Offset: 0x00360D07
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AltName.attributeNamespaceIds;
			}
		}

		// Token: 0x17009382 RID: 37762
		// (get) Token: 0x0601A764 RID: 108388 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A765 RID: 108389 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A767 RID: 108391 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A768 RID: 108392 RVA: 0x00362B0E File Offset: 0x00360D0E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AltName>(deep);
		}

		// Token: 0x0400AD05 RID: 44293
		private const string tagName = "altName";

		// Token: 0x0400AD06 RID: 44294
		private const byte tagNsId = 23;

		// Token: 0x0400AD07 RID: 44295
		internal const int ElementTypeIdConst = 11915;

		// Token: 0x0400AD08 RID: 44296
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD09 RID: 44297
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
