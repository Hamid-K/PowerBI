using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B7 RID: 9655
	[GeneratedCode("DomGen", "2.0")]
	internal class LabelAlignment : OpenXmlLeafElement
	{
		// Token: 0x17005758 RID: 22360
		// (get) Token: 0x0601215D RID: 74077 RVA: 0x002F562F File Offset: 0x002F382F
		public override string LocalName
		{
			get
			{
				return "lblAlgn";
			}
		}

		// Token: 0x17005759 RID: 22361
		// (get) Token: 0x0601215E RID: 74078 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700575A RID: 22362
		// (get) Token: 0x0601215F RID: 74079 RVA: 0x002F5636 File Offset: 0x002F3836
		internal override int ElementTypeId
		{
			get
			{
				return 10482;
			}
		}

		// Token: 0x06012160 RID: 74080 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700575B RID: 22363
		// (get) Token: 0x06012161 RID: 74081 RVA: 0x002F563D File Offset: 0x002F383D
		internal override string[] AttributeTagNames
		{
			get
			{
				return LabelAlignment.attributeTagNames;
			}
		}

		// Token: 0x1700575C RID: 22364
		// (get) Token: 0x06012162 RID: 74082 RVA: 0x002F5644 File Offset: 0x002F3844
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LabelAlignment.attributeNamespaceIds;
			}
		}

		// Token: 0x1700575D RID: 22365
		// (get) Token: 0x06012163 RID: 74083 RVA: 0x002F564B File Offset: 0x002F384B
		// (set) Token: 0x06012164 RID: 74084 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<LabelAlignmentValues> Val
		{
			get
			{
				return (EnumValue<LabelAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012166 RID: 74086 RVA: 0x002F565A File Offset: 0x002F385A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<LabelAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012167 RID: 74087 RVA: 0x002F567A File Offset: 0x002F387A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LabelAlignment>(deep);
		}

		// Token: 0x06012168 RID: 74088 RVA: 0x002F5684 File Offset: 0x002F3884
		// Note: this type is marked as 'beforefieldinit'.
		static LabelAlignment()
		{
			byte[] array = new byte[1];
			LabelAlignment.attributeNamespaceIds = array;
		}

		// Token: 0x04007E20 RID: 32288
		private const string tagName = "lblAlgn";

		// Token: 0x04007E21 RID: 32289
		private const byte tagNsId = 11;

		// Token: 0x04007E22 RID: 32290
		internal const int ElementTypeIdConst = 10482;

		// Token: 0x04007E23 RID: 32291
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E24 RID: 32292
		private static byte[] attributeNamespaceIds;
	}
}
