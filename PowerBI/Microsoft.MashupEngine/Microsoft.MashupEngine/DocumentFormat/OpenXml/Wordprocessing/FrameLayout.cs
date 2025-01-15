using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F93 RID: 12179
	[GeneratedCode("DomGen", "2.0")]
	internal class FrameLayout : OpenXmlLeafElement
	{
		// Token: 0x170091D8 RID: 37336
		// (get) Token: 0x0601A3ED RID: 107501 RVA: 0x0035F700 File Offset: 0x0035D900
		public override string LocalName
		{
			get
			{
				return "frameLayout";
			}
		}

		// Token: 0x170091D9 RID: 37337
		// (get) Token: 0x0601A3EE RID: 107502 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170091DA RID: 37338
		// (get) Token: 0x0601A3EF RID: 107503 RVA: 0x0035F707 File Offset: 0x0035D907
		internal override int ElementTypeId
		{
			get
			{
				return 11860;
			}
		}

		// Token: 0x0601A3F0 RID: 107504 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170091DB RID: 37339
		// (get) Token: 0x0601A3F1 RID: 107505 RVA: 0x0035F70E File Offset: 0x0035D90E
		internal override string[] AttributeTagNames
		{
			get
			{
				return FrameLayout.attributeTagNames;
			}
		}

		// Token: 0x170091DC RID: 37340
		// (get) Token: 0x0601A3F2 RID: 107506 RVA: 0x0035F715 File Offset: 0x0035D915
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FrameLayout.attributeNamespaceIds;
			}
		}

		// Token: 0x170091DD RID: 37341
		// (get) Token: 0x0601A3F3 RID: 107507 RVA: 0x0035F71C File Offset: 0x0035D91C
		// (set) Token: 0x0601A3F4 RID: 107508 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<FrameLayoutValues> Val
		{
			get
			{
				return (EnumValue<FrameLayoutValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A3F6 RID: 107510 RVA: 0x0035F72B File Offset: 0x0035D92B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<FrameLayoutValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A3F7 RID: 107511 RVA: 0x0035F74D File Offset: 0x0035D94D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FrameLayout>(deep);
		}

		// Token: 0x0400AC6A RID: 44138
		private const string tagName = "frameLayout";

		// Token: 0x0400AC6B RID: 44139
		private const byte tagNsId = 23;

		// Token: 0x0400AC6C RID: 44140
		internal const int ElementTypeIdConst = 11860;

		// Token: 0x0400AC6D RID: 44141
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AC6E RID: 44142
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
