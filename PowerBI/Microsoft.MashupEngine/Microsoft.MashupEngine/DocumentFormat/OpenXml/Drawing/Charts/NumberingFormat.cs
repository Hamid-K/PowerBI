using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002503 RID: 9475
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingFormat : OpenXmlLeafElement
	{
		// Token: 0x17005412 RID: 21522
		// (get) Token: 0x06011A14 RID: 72212 RVA: 0x002F0D56 File Offset: 0x002EEF56
		public override string LocalName
		{
			get
			{
				return "numFmt";
			}
		}

		// Token: 0x17005413 RID: 21523
		// (get) Token: 0x06011A15 RID: 72213 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005414 RID: 21524
		// (get) Token: 0x06011A16 RID: 72214 RVA: 0x002F0D5D File Offset: 0x002EEF5D
		internal override int ElementTypeId
		{
			get
			{
				return 10342;
			}
		}

		// Token: 0x06011A17 RID: 72215 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005415 RID: 21525
		// (get) Token: 0x06011A18 RID: 72216 RVA: 0x002F0D64 File Offset: 0x002EEF64
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingFormat.attributeTagNames;
			}
		}

		// Token: 0x17005416 RID: 21526
		// (get) Token: 0x06011A19 RID: 72217 RVA: 0x002F0D6B File Offset: 0x002EEF6B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17005417 RID: 21527
		// (get) Token: 0x06011A1A RID: 72218 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06011A1B RID: 72219 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "formatCode")]
		public StringValue FormatCode
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

		// Token: 0x17005418 RID: 21528
		// (get) Token: 0x06011A1C RID: 72220 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06011A1D RID: 72221 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "sourceLinked")]
		public BooleanValue SourceLinked
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06011A1F RID: 72223 RVA: 0x002F0D72 File Offset: 0x002EEF72
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "formatCode" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "sourceLinked" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011A20 RID: 72224 RVA: 0x002F0DA8 File Offset: 0x002EEFA8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingFormat>(deep);
		}

		// Token: 0x06011A21 RID: 72225 RVA: 0x002F0DB4 File Offset: 0x002EEFB4
		// Note: this type is marked as 'beforefieldinit'.
		static NumberingFormat()
		{
			byte[] array = new byte[2];
			NumberingFormat.attributeNamespaceIds = array;
		}

		// Token: 0x04007B97 RID: 31639
		private const string tagName = "numFmt";

		// Token: 0x04007B98 RID: 31640
		private const byte tagNsId = 11;

		// Token: 0x04007B99 RID: 31641
		internal const int ElementTypeIdConst = 10342;

		// Token: 0x04007B9A RID: 31642
		private static string[] attributeTagNames = new string[] { "formatCode", "sourceLinked" };

		// Token: 0x04007B9B RID: 31643
		private static byte[] attributeNamespaceIds;
	}
}
