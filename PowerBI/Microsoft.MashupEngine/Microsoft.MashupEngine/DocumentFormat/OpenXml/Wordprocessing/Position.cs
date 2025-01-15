using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E92 RID: 11922
	[GeneratedCode("DomGen", "2.0")]
	internal class Position : OpenXmlLeafElement
	{
		// Token: 0x17008B39 RID: 35641
		// (get) Token: 0x06019562 RID: 103778 RVA: 0x00348B44 File Offset: 0x00346D44
		public override string LocalName
		{
			get
			{
				return "position";
			}
		}

		// Token: 0x17008B3A RID: 35642
		// (get) Token: 0x06019563 RID: 103779 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B3B RID: 35643
		// (get) Token: 0x06019564 RID: 103780 RVA: 0x00348B4B File Offset: 0x00346D4B
		internal override int ElementTypeId
		{
			get
			{
				return 11596;
			}
		}

		// Token: 0x06019565 RID: 103781 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B3C RID: 35644
		// (get) Token: 0x06019566 RID: 103782 RVA: 0x00348B52 File Offset: 0x00346D52
		internal override string[] AttributeTagNames
		{
			get
			{
				return Position.attributeTagNames;
			}
		}

		// Token: 0x17008B3D RID: 35645
		// (get) Token: 0x06019567 RID: 103783 RVA: 0x00348B59 File Offset: 0x00346D59
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Position.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B3E RID: 35646
		// (get) Token: 0x06019568 RID: 103784 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019569 RID: 103785 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601956B RID: 103787 RVA: 0x00344715 File Offset: 0x00342915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601956C RID: 103788 RVA: 0x00348B60 File Offset: 0x00346D60
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Position>(deep);
		}

		// Token: 0x0400A866 RID: 43110
		private const string tagName = "position";

		// Token: 0x0400A867 RID: 43111
		private const byte tagNsId = 23;

		// Token: 0x0400A868 RID: 43112
		internal const int ElementTypeIdConst = 11596;

		// Token: 0x0400A869 RID: 43113
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A86A RID: 43114
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
