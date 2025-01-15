using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023AC RID: 9132
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BookmarkTarget : OpenXmlLeafElement
	{
		// Token: 0x17004C42 RID: 19522
		// (get) Token: 0x06010884 RID: 67716 RVA: 0x002E476B File Offset: 0x002E296B
		public override string LocalName
		{
			get
			{
				return "bmkTgt";
			}
		}

		// Token: 0x17004C43 RID: 19523
		// (get) Token: 0x06010885 RID: 67717 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C44 RID: 19524
		// (get) Token: 0x06010886 RID: 67718 RVA: 0x002E4772 File Offset: 0x002E2972
		internal override int ElementTypeId
		{
			get
			{
				return 12787;
			}
		}

		// Token: 0x06010887 RID: 67719 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C45 RID: 19525
		// (get) Token: 0x06010888 RID: 67720 RVA: 0x002E4779 File Offset: 0x002E2979
		internal override string[] AttributeTagNames
		{
			get
			{
				return BookmarkTarget.attributeTagNames;
			}
		}

		// Token: 0x17004C46 RID: 19526
		// (get) Token: 0x06010889 RID: 67721 RVA: 0x002E4780 File Offset: 0x002E2980
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BookmarkTarget.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C47 RID: 19527
		// (get) Token: 0x0601088A RID: 67722 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601088B RID: 67723 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public UInt32Value ShapeId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004C48 RID: 19528
		// (get) Token: 0x0601088C RID: 67724 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601088D RID: 67725 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "bmkName")]
		public StringValue BookmarkName
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

		// Token: 0x0601088F RID: 67727 RVA: 0x002E4787 File Offset: 0x002E2987
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "bmkName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010890 RID: 67728 RVA: 0x002E47BD File Offset: 0x002E29BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BookmarkTarget>(deep);
		}

		// Token: 0x06010891 RID: 67729 RVA: 0x002E47C8 File Offset: 0x002E29C8
		// Note: this type is marked as 'beforefieldinit'.
		static BookmarkTarget()
		{
			byte[] array = new byte[2];
			BookmarkTarget.attributeNamespaceIds = array;
		}

		// Token: 0x0400751C RID: 29980
		private const string tagName = "bmkTgt";

		// Token: 0x0400751D RID: 29981
		private const byte tagNsId = 49;

		// Token: 0x0400751E RID: 29982
		internal const int ElementTypeIdConst = 12787;

		// Token: 0x0400751F RID: 29983
		private static string[] attributeTagNames = new string[] { "spid", "bmkName" };

		// Token: 0x04007520 RID: 29984
		private static byte[] attributeNamespaceIds;
	}
}
