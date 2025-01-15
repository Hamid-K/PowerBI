using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FD2 RID: 12242
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartName : OpenXmlLeafElement
	{
		// Token: 0x17009445 RID: 37957
		// (get) Token: 0x0601A917 RID: 108823 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x17009446 RID: 37958
		// (get) Token: 0x0601A918 RID: 108824 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009447 RID: 37959
		// (get) Token: 0x0601A919 RID: 108825 RVA: 0x00364555 File Offset: 0x00362755
		internal override int ElementTypeId
		{
			get
			{
				return 11948;
			}
		}

		// Token: 0x0601A91A RID: 108826 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009448 RID: 37960
		// (get) Token: 0x0601A91B RID: 108827 RVA: 0x0036455C File Offset: 0x0036275C
		internal override string[] AttributeTagNames
		{
			get
			{
				return DocPartName.attributeTagNames;
			}
		}

		// Token: 0x17009449 RID: 37961
		// (get) Token: 0x0601A91C RID: 108828 RVA: 0x00364563 File Offset: 0x00362763
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DocPartName.attributeNamespaceIds;
			}
		}

		// Token: 0x1700944A RID: 37962
		// (get) Token: 0x0601A91D RID: 108829 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601A91E RID: 108830 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700944B RID: 37963
		// (get) Token: 0x0601A91F RID: 108831 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x0601A920 RID: 108832 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "decorated")]
		public OnOffValue Decorated
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601A922 RID: 108834 RVA: 0x0036456A File Offset: 0x0036276A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "decorated" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A923 RID: 108835 RVA: 0x003645A4 File Offset: 0x003627A4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartName>(deep);
		}

		// Token: 0x0400AD8B RID: 44427
		private const string tagName = "name";

		// Token: 0x0400AD8C RID: 44428
		private const byte tagNsId = 23;

		// Token: 0x0400AD8D RID: 44429
		internal const int ElementTypeIdConst = 11948;

		// Token: 0x0400AD8E RID: 44430
		private static string[] attributeTagNames = new string[] { "val", "decorated" };

		// Token: 0x0400AD8F RID: 44431
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
