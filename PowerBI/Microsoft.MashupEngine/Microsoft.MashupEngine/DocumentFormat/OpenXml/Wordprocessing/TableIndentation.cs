using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F08 RID: 12040
	[GeneratedCode("DomGen", "2.0")]
	internal class TableIndentation : OpenXmlLeafElement
	{
		// Token: 0x17008DF1 RID: 36337
		// (get) Token: 0x06019B35 RID: 105269 RVA: 0x003540DA File Offset: 0x003522DA
		public override string LocalName
		{
			get
			{
				return "tblInd";
			}
		}

		// Token: 0x17008DF2 RID: 36338
		// (get) Token: 0x06019B36 RID: 105270 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DF3 RID: 36339
		// (get) Token: 0x06019B37 RID: 105271 RVA: 0x003540E1 File Offset: 0x003522E1
		internal override int ElementTypeId
		{
			get
			{
				return 11678;
			}
		}

		// Token: 0x06019B38 RID: 105272 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008DF4 RID: 36340
		// (get) Token: 0x06019B39 RID: 105273 RVA: 0x003540E8 File Offset: 0x003522E8
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableIndentation.attributeTagNames;
			}
		}

		// Token: 0x17008DF5 RID: 36341
		// (get) Token: 0x06019B3A RID: 105274 RVA: 0x003540EF File Offset: 0x003522EF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableIndentation.attributeNamespaceIds;
			}
		}

		// Token: 0x17008DF6 RID: 36342
		// (get) Token: 0x06019B3B RID: 105275 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019B3C RID: 105276 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "w")]
		public Int32Value Width
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

		// Token: 0x17008DF7 RID: 36343
		// (get) Token: 0x06019B3D RID: 105277 RVA: 0x00353358 File Offset: 0x00351558
		// (set) Token: 0x06019B3E RID: 105278 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "type")]
		public EnumValue<TableWidthUnitValues> Type
		{
			get
			{
				return (EnumValue<TableWidthUnitValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06019B40 RID: 105280 RVA: 0x003540F6 File Offset: 0x003522F6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "w" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "type" == name)
			{
				return new EnumValue<TableWidthUnitValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019B41 RID: 105281 RVA: 0x00354130 File Offset: 0x00352330
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableIndentation>(deep);
		}

		// Token: 0x0400AA32 RID: 43570
		private const string tagName = "tblInd";

		// Token: 0x0400AA33 RID: 43571
		private const byte tagNsId = 23;

		// Token: 0x0400AA34 RID: 43572
		internal const int ElementTypeIdConst = 11678;

		// Token: 0x0400AA35 RID: 43573
		private static string[] attributeTagNames = new string[] { "w", "type" };

		// Token: 0x0400AA36 RID: 43574
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
