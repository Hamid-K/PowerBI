using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200268D RID: 9869
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleDefinitionTitle : OpenXmlLeafElement
	{
		// Token: 0x17005D01 RID: 23809
		// (get) Token: 0x06012E12 RID: 77330 RVA: 0x002F2B3B File Offset: 0x002F0D3B
		public override string LocalName
		{
			get
			{
				return "title";
			}
		}

		// Token: 0x17005D02 RID: 23810
		// (get) Token: 0x06012E13 RID: 77331 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D03 RID: 23811
		// (get) Token: 0x06012E14 RID: 77332 RVA: 0x00300565 File Offset: 0x002FE765
		internal override int ElementTypeId
		{
			get
			{
				return 10684;
			}
		}

		// Token: 0x06012E15 RID: 77333 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D04 RID: 23812
		// (get) Token: 0x06012E16 RID: 77334 RVA: 0x0030056C File Offset: 0x002FE76C
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleDefinitionTitle.attributeTagNames;
			}
		}

		// Token: 0x17005D05 RID: 23813
		// (get) Token: 0x06012E17 RID: 77335 RVA: 0x00300573 File Offset: 0x002FE773
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleDefinitionTitle.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D06 RID: 23814
		// (get) Token: 0x06012E18 RID: 77336 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012E19 RID: 77337 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lang")]
		public StringValue Language
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

		// Token: 0x17005D07 RID: 23815
		// (get) Token: 0x06012E1A RID: 77338 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012E1B RID: 77339 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012E1D RID: 77341 RVA: 0x002FDB15 File Offset: 0x002FBD15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lang" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012E1E RID: 77342 RVA: 0x0030057A File Offset: 0x002FE77A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleDefinitionTitle>(deep);
		}

		// Token: 0x06012E1F RID: 77343 RVA: 0x00300584 File Offset: 0x002FE784
		// Note: this type is marked as 'beforefieldinit'.
		static StyleDefinitionTitle()
		{
			byte[] array = new byte[2];
			StyleDefinitionTitle.attributeNamespaceIds = array;
		}

		// Token: 0x04008209 RID: 33289
		private const string tagName = "title";

		// Token: 0x0400820A RID: 33290
		private const byte tagNsId = 14;

		// Token: 0x0400820B RID: 33291
		internal const int ElementTypeIdConst = 10684;

		// Token: 0x0400820C RID: 33292
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x0400820D RID: 33293
		private static byte[] attributeNamespaceIds;
	}
}
