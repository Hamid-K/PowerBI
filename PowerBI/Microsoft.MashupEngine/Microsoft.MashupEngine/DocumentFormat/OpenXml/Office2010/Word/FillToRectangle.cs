using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C4 RID: 9412
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class FillToRectangle : OpenXmlLeafElement
	{
		// Token: 0x170052C8 RID: 21192
		// (get) Token: 0x06011735 RID: 71477 RVA: 0x002EE8E4 File Offset: 0x002ECAE4
		public override string LocalName
		{
			get
			{
				return "fillToRect";
			}
		}

		// Token: 0x170052C9 RID: 21193
		// (get) Token: 0x06011736 RID: 71478 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052CA RID: 21194
		// (get) Token: 0x06011737 RID: 71479 RVA: 0x002EE8EB File Offset: 0x002ECAEB
		internal override int ElementTypeId
		{
			get
			{
				return 12884;
			}
		}

		// Token: 0x06011738 RID: 71480 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170052CB RID: 21195
		// (get) Token: 0x06011739 RID: 71481 RVA: 0x002EE8F2 File Offset: 0x002ECAF2
		internal override string[] AttributeTagNames
		{
			get
			{
				return FillToRectangle.attributeTagNames;
			}
		}

		// Token: 0x170052CC RID: 21196
		// (get) Token: 0x0601173A RID: 71482 RVA: 0x002EE8F9 File Offset: 0x002ECAF9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FillToRectangle.attributeNamespaceIds;
			}
		}

		// Token: 0x170052CD RID: 21197
		// (get) Token: 0x0601173B RID: 71483 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601173C RID: 71484 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "l")]
		public Int32Value Left
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

		// Token: 0x170052CE RID: 21198
		// (get) Token: 0x0601173D RID: 71485 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601173E RID: 71486 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "t")]
		public Int32Value Top
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x170052CF RID: 21199
		// (get) Token: 0x0601173F RID: 71487 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06011740 RID: 71488 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(52, "r")]
		public Int32Value Right
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170052D0 RID: 21200
		// (get) Token: 0x06011741 RID: 71489 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06011742 RID: 71490 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(52, "b")]
		public Int32Value Bottom
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06011744 RID: 71492 RVA: 0x002EE900 File Offset: 0x002ECB00
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "l" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "t" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "r" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "b" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011745 RID: 71493 RVA: 0x002EE975 File Offset: 0x002ECB75
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FillToRectangle>(deep);
		}

		// Token: 0x040079E2 RID: 31202
		private const string tagName = "fillToRect";

		// Token: 0x040079E3 RID: 31203
		private const byte tagNsId = 52;

		// Token: 0x040079E4 RID: 31204
		internal const int ElementTypeIdConst = 12884;

		// Token: 0x040079E5 RID: 31205
		private static string[] attributeTagNames = new string[] { "l", "t", "r", "b" };

		// Token: 0x040079E6 RID: 31206
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52, 52, 52 };
	}
}
