using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002591 RID: 9617
	[GeneratedCode("DomGen", "2.0")]
	internal class Perspective : OpenXmlLeafElement
	{
		// Token: 0x1700567E RID: 22142
		// (get) Token: 0x06011F7B RID: 73595 RVA: 0x002F42C3 File Offset: 0x002F24C3
		public override string LocalName
		{
			get
			{
				return "perspective";
			}
		}

		// Token: 0x1700567F RID: 22143
		// (get) Token: 0x06011F7C RID: 73596 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005680 RID: 22144
		// (get) Token: 0x06011F7D RID: 73597 RVA: 0x002F42CA File Offset: 0x002F24CA
		internal override int ElementTypeId
		{
			get
			{
				return 10422;
			}
		}

		// Token: 0x06011F7E RID: 73598 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005681 RID: 22145
		// (get) Token: 0x06011F7F RID: 73599 RVA: 0x002F42D1 File Offset: 0x002F24D1
		internal override string[] AttributeTagNames
		{
			get
			{
				return Perspective.attributeTagNames;
			}
		}

		// Token: 0x17005682 RID: 22146
		// (get) Token: 0x06011F80 RID: 73600 RVA: 0x002F42D8 File Offset: 0x002F24D8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Perspective.attributeNamespaceIds;
			}
		}

		// Token: 0x17005683 RID: 22147
		// (get) Token: 0x06011F81 RID: 73601 RVA: 0x002DE388 File Offset: 0x002DC588
		// (set) Token: 0x06011F82 RID: 73602 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011F84 RID: 73604 RVA: 0x002DE397 File Offset: 0x002DC597
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new ByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011F85 RID: 73605 RVA: 0x002F42DF File Offset: 0x002F24DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Perspective>(deep);
		}

		// Token: 0x06011F86 RID: 73606 RVA: 0x002F42E8 File Offset: 0x002F24E8
		// Note: this type is marked as 'beforefieldinit'.
		static Perspective()
		{
			byte[] array = new byte[1];
			Perspective.attributeNamespaceIds = array;
		}

		// Token: 0x04007D7F RID: 32127
		private const string tagName = "perspective";

		// Token: 0x04007D80 RID: 32128
		private const byte tagNsId = 11;

		// Token: 0x04007D81 RID: 32129
		internal const int ElementTypeIdConst = 10422;

		// Token: 0x04007D82 RID: 32130
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D83 RID: 32131
		private static byte[] attributeNamespaceIds;
	}
}
