using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E8F RID: 11919
	[GeneratedCode("DomGen", "2.0")]
	internal class Spacing : OpenXmlLeafElement
	{
		// Token: 0x17008B27 RID: 35623
		// (get) Token: 0x0601953E RID: 103742 RVA: 0x003461D1 File Offset: 0x003443D1
		public override string LocalName
		{
			get
			{
				return "spacing";
			}
		}

		// Token: 0x17008B28 RID: 35624
		// (get) Token: 0x0601953F RID: 103743 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B29 RID: 35625
		// (get) Token: 0x06019540 RID: 103744 RVA: 0x00348A00 File Offset: 0x00346C00
		internal override int ElementTypeId
		{
			get
			{
				return 11593;
			}
		}

		// Token: 0x06019541 RID: 103745 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B2A RID: 35626
		// (get) Token: 0x06019542 RID: 103746 RVA: 0x00348A07 File Offset: 0x00346C07
		internal override string[] AttributeTagNames
		{
			get
			{
				return Spacing.attributeTagNames;
			}
		}

		// Token: 0x17008B2B RID: 35627
		// (get) Token: 0x06019543 RID: 103747 RVA: 0x00348A0E File Offset: 0x00346C0E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Spacing.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B2C RID: 35628
		// (get) Token: 0x06019544 RID: 103748 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019545 RID: 103749 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06019547 RID: 103751 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019548 RID: 103752 RVA: 0x00348A15 File Offset: 0x00346C15
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Spacing>(deep);
		}

		// Token: 0x0400A857 RID: 43095
		private const string tagName = "spacing";

		// Token: 0x0400A858 RID: 43096
		private const byte tagNsId = 23;

		// Token: 0x0400A859 RID: 43097
		internal const int ElementTypeIdConst = 11593;

		// Token: 0x0400A85A RID: 43098
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A85B RID: 43099
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
