using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200272B RID: 10027
	[GeneratedCode("DomGen", "2.0")]
	internal class Miter : OpenXmlLeafElement
	{
		// Token: 0x17005FE9 RID: 24553
		// (get) Token: 0x06013447 RID: 78919 RVA: 0x002ED220 File Offset: 0x002EB420
		public override string LocalName
		{
			get
			{
				return "miter";
			}
		}

		// Token: 0x17005FEA RID: 24554
		// (get) Token: 0x06013448 RID: 78920 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FEB RID: 24555
		// (get) Token: 0x06013449 RID: 78921 RVA: 0x00305966 File Offset: 0x00303B66
		internal override int ElementTypeId
		{
			get
			{
				return 10090;
			}
		}

		// Token: 0x0601344A RID: 78922 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005FEC RID: 24556
		// (get) Token: 0x0601344B RID: 78923 RVA: 0x0030596D File Offset: 0x00303B6D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Miter.attributeTagNames;
			}
		}

		// Token: 0x17005FED RID: 24557
		// (get) Token: 0x0601344C RID: 78924 RVA: 0x00305974 File Offset: 0x00303B74
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Miter.attributeNamespaceIds;
			}
		}

		// Token: 0x17005FEE RID: 24558
		// (get) Token: 0x0601344D RID: 78925 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601344E RID: 78926 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lim")]
		public Int32Value Limit
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

		// Token: 0x06013450 RID: 78928 RVA: 0x0030597B File Offset: 0x00303B7B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lim" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013451 RID: 78929 RVA: 0x0030599B File Offset: 0x00303B9B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Miter>(deep);
		}

		// Token: 0x06013452 RID: 78930 RVA: 0x003059A4 File Offset: 0x00303BA4
		// Note: this type is marked as 'beforefieldinit'.
		static Miter()
		{
			byte[] array = new byte[1];
			Miter.attributeNamespaceIds = array;
		}

		// Token: 0x04008562 RID: 34146
		private const string tagName = "miter";

		// Token: 0x04008563 RID: 34147
		private const byte tagNsId = 10;

		// Token: 0x04008564 RID: 34148
		internal const int ElementTypeIdConst = 10090;

		// Token: 0x04008565 RID: 34149
		private static string[] attributeTagNames = new string[] { "lim" };

		// Token: 0x04008566 RID: 34150
		private static byte[] attributeNamespaceIds;
	}
}
