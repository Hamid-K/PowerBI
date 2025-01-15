using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002594 RID: 9620
	[GeneratedCode("DomGen", "2.0")]
	internal class Size : OpenXmlLeafElement
	{
		// Token: 0x17005691 RID: 22161
		// (get) Token: 0x06011FA3 RID: 73635 RVA: 0x002F460F File Offset: 0x002F280F
		public override string LocalName
		{
			get
			{
				return "size";
			}
		}

		// Token: 0x17005692 RID: 22162
		// (get) Token: 0x06011FA4 RID: 73636 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005693 RID: 22163
		// (get) Token: 0x06011FA5 RID: 73637 RVA: 0x002F4616 File Offset: 0x002F2816
		internal override int ElementTypeId
		{
			get
			{
				return 10430;
			}
		}

		// Token: 0x06011FA6 RID: 73638 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005694 RID: 22164
		// (get) Token: 0x06011FA7 RID: 73639 RVA: 0x002F461D File Offset: 0x002F281D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Size.attributeTagNames;
			}
		}

		// Token: 0x17005695 RID: 22165
		// (get) Token: 0x06011FA8 RID: 73640 RVA: 0x002F4624 File Offset: 0x002F2824
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Size.attributeNamespaceIds;
			}
		}

		// Token: 0x17005696 RID: 22166
		// (get) Token: 0x06011FA9 RID: 73641 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x06011FAA RID: 73642 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public ByteValue Val
		{
			get
			{
				return (ByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011FAC RID: 73644 RVA: 0x002DE397 File Offset: 0x002DC597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new ByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011FAD RID: 73645 RVA: 0x002F462B File Offset: 0x002F282B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Size>(deep);
		}

		// Token: 0x06011FAE RID: 73646 RVA: 0x002F4634 File Offset: 0x002F2834
		// Note: this type is marked as 'beforefieldinit'.
		static Size()
		{
			byte[] array = new byte[1];
			Size.attributeNamespaceIds = array;
		}

		// Token: 0x04007D8E RID: 32142
		private const string tagName = "size";

		// Token: 0x04007D8F RID: 32143
		private const byte tagNsId = 11;

		// Token: 0x04007D90 RID: 32144
		internal const int ElementTypeIdConst = 10430;

		// Token: 0x04007D91 RID: 32145
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D92 RID: 32146
		private static byte[] attributeNamespaceIds;
	}
}
