using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C1D RID: 11293
	[GeneratedCode("DomGen", "2.0")]
	internal class ExternalDefinedName : OpenXmlLeafElement
	{
		// Token: 0x1700804D RID: 32845
		// (get) Token: 0x06017D46 RID: 97606 RVA: 0x002E8690 File Offset: 0x002E6890
		public override string LocalName
		{
			get
			{
				return "definedName";
			}
		}

		// Token: 0x1700804E RID: 32846
		// (get) Token: 0x06017D47 RID: 97607 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700804F RID: 32847
		// (get) Token: 0x06017D48 RID: 97608 RVA: 0x0033B9E6 File Offset: 0x00339BE6
		internal override int ElementTypeId
		{
			get
			{
				return 11274;
			}
		}

		// Token: 0x06017D49 RID: 97609 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008050 RID: 32848
		// (get) Token: 0x06017D4A RID: 97610 RVA: 0x0033B9ED File Offset: 0x00339BED
		internal override string[] AttributeTagNames
		{
			get
			{
				return ExternalDefinedName.attributeTagNames;
			}
		}

		// Token: 0x17008051 RID: 32849
		// (get) Token: 0x06017D4B RID: 97611 RVA: 0x0033B9F4 File Offset: 0x00339BF4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ExternalDefinedName.attributeNamespaceIds;
			}
		}

		// Token: 0x17008052 RID: 32850
		// (get) Token: 0x06017D4C RID: 97612 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017D4D RID: 97613 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008053 RID: 32851
		// (get) Token: 0x06017D4E RID: 97614 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06017D4F RID: 97615 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "refersTo")]
		public StringValue RefersTo
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

		// Token: 0x17008054 RID: 32852
		// (get) Token: 0x06017D50 RID: 97616 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06017D51 RID: 97617 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "sheetId")]
		public UInt32Value SheetId
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06017D53 RID: 97619 RVA: 0x0033B9FC File Offset: 0x00339BFC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "refersTo" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sheetId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017D54 RID: 97620 RVA: 0x0033BA53 File Offset: 0x00339C53
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExternalDefinedName>(deep);
		}

		// Token: 0x06017D55 RID: 97621 RVA: 0x0033BA5C File Offset: 0x00339C5C
		// Note: this type is marked as 'beforefieldinit'.
		static ExternalDefinedName()
		{
			byte[] array = new byte[3];
			ExternalDefinedName.attributeNamespaceIds = array;
		}

		// Token: 0x04009DBE RID: 40382
		private const string tagName = "definedName";

		// Token: 0x04009DBF RID: 40383
		private const byte tagNsId = 22;

		// Token: 0x04009DC0 RID: 40384
		internal const int ElementTypeIdConst = 11274;

		// Token: 0x04009DC1 RID: 40385
		private static string[] attributeTagNames = new string[] { "name", "refersTo", "sheetId" };

		// Token: 0x04009DC2 RID: 40386
		private static byte[] attributeNamespaceIds;
	}
}
