using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B52 RID: 11090
	[GeneratedCode("DomGen", "2.0")]
	internal class PageItem : OpenXmlLeafElement
	{
		// Token: 0x17007850 RID: 30800
		// (get) Token: 0x06016C1A RID: 93210 RVA: 0x0032EB43 File Offset: 0x0032CD43
		public override string LocalName
		{
			get
			{
				return "pageItem";
			}
		}

		// Token: 0x17007851 RID: 30801
		// (get) Token: 0x06016C1B RID: 93211 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007852 RID: 30802
		// (get) Token: 0x06016C1C RID: 93212 RVA: 0x0032EB4A File Offset: 0x0032CD4A
		internal override int ElementTypeId
		{
			get
			{
				return 11073;
			}
		}

		// Token: 0x06016C1D RID: 93213 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007853 RID: 30803
		// (get) Token: 0x06016C1E RID: 93214 RVA: 0x0032EB51 File Offset: 0x0032CD51
		internal override string[] AttributeTagNames
		{
			get
			{
				return PageItem.attributeTagNames;
			}
		}

		// Token: 0x17007854 RID: 30804
		// (get) Token: 0x06016C1F RID: 93215 RVA: 0x0032EB58 File Offset: 0x0032CD58
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PageItem.attributeNamespaceIds;
			}
		}

		// Token: 0x17007855 RID: 30805
		// (get) Token: 0x06016C20 RID: 93216 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016C21 RID: 93217 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x06016C23 RID: 93219 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016C24 RID: 93220 RVA: 0x0032EB5F File Offset: 0x0032CD5F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PageItem>(deep);
		}

		// Token: 0x06016C25 RID: 93221 RVA: 0x0032EB68 File Offset: 0x0032CD68
		// Note: this type is marked as 'beforefieldinit'.
		static PageItem()
		{
			byte[] array = new byte[1];
			PageItem.attributeNamespaceIds = array;
		}

		// Token: 0x040099D8 RID: 39384
		private const string tagName = "pageItem";

		// Token: 0x040099D9 RID: 39385
		private const byte tagNsId = 22;

		// Token: 0x040099DA RID: 39386
		internal const int ElementTypeIdConst = 11073;

		// Token: 0x040099DB RID: 39387
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x040099DC RID: 39388
		private static byte[] attributeNamespaceIds;
	}
}
