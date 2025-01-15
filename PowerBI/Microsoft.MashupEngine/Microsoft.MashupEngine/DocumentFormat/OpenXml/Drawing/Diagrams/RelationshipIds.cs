using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200264D RID: 9805
	[GeneratedCode("DomGen", "2.0")]
	internal class RelationshipIds : OpenXmlLeafElement
	{
		// Token: 0x17005B30 RID: 23344
		// (get) Token: 0x060129E8 RID: 76264 RVA: 0x002FD525 File Offset: 0x002FB725
		public override string LocalName
		{
			get
			{
				return "relIds";
			}
		}

		// Token: 0x17005B31 RID: 23345
		// (get) Token: 0x060129E9 RID: 76265 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B32 RID: 23346
		// (get) Token: 0x060129EA RID: 76266 RVA: 0x002FD52C File Offset: 0x002FB72C
		internal override int ElementTypeId
		{
			get
			{
				return 10623;
			}
		}

		// Token: 0x060129EB RID: 76267 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B33 RID: 23347
		// (get) Token: 0x060129EC RID: 76268 RVA: 0x002FD533 File Offset: 0x002FB733
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelationshipIds.attributeTagNames;
			}
		}

		// Token: 0x17005B34 RID: 23348
		// (get) Token: 0x060129ED RID: 76269 RVA: 0x002FD53A File Offset: 0x002FB73A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelationshipIds.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B35 RID: 23349
		// (get) Token: 0x060129EE RID: 76270 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060129EF RID: 76271 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "dm")]
		public StringValue DataPart
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

		// Token: 0x17005B36 RID: 23350
		// (get) Token: 0x060129F0 RID: 76272 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060129F1 RID: 76273 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "lo")]
		public StringValue LayoutPart
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

		// Token: 0x17005B37 RID: 23351
		// (get) Token: 0x060129F2 RID: 76274 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060129F3 RID: 76275 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(19, "qs")]
		public StringValue StylePart
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17005B38 RID: 23352
		// (get) Token: 0x060129F4 RID: 76276 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060129F5 RID: 76277 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(19, "cs")]
		public StringValue ColorPart
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060129F7 RID: 76279 RVA: 0x002FD544 File Offset: 0x002FB744
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "dm" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "lo" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "qs" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "cs" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060129F8 RID: 76280 RVA: 0x002FD5B9 File Offset: 0x002FB7B9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RelationshipIds>(deep);
		}

		// Token: 0x040080EA RID: 33002
		private const string tagName = "relIds";

		// Token: 0x040080EB RID: 33003
		private const byte tagNsId = 14;

		// Token: 0x040080EC RID: 33004
		internal const int ElementTypeIdConst = 10623;

		// Token: 0x040080ED RID: 33005
		private static string[] attributeTagNames = new string[] { "dm", "lo", "qs", "cs" };

		// Token: 0x040080EE RID: 33006
		private static byte[] attributeNamespaceIds = new byte[] { 19, 19, 19, 19 };
	}
}
