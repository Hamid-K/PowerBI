using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B1 RID: 10673
	[GeneratedCode("DomGen", "2.0")]
	internal class MatrixColumnJustification : OpenXmlLeafElement
	{
		// Token: 0x17006D7F RID: 28031
		// (get) Token: 0x060153C9 RID: 86985 RVA: 0x0031D24C File Offset: 0x0031B44C
		public override string LocalName
		{
			get
			{
				return "mcJc";
			}
		}

		// Token: 0x17006D80 RID: 28032
		// (get) Token: 0x060153CA RID: 86986 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D81 RID: 28033
		// (get) Token: 0x060153CB RID: 86987 RVA: 0x0031D253 File Offset: 0x0031B453
		internal override int ElementTypeId
		{
			get
			{
				return 10913;
			}
		}

		// Token: 0x060153CC RID: 86988 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006D82 RID: 28034
		// (get) Token: 0x060153CD RID: 86989 RVA: 0x0031D25A File Offset: 0x0031B45A
		internal override string[] AttributeTagNames
		{
			get
			{
				return MatrixColumnJustification.attributeTagNames;
			}
		}

		// Token: 0x17006D83 RID: 28035
		// (get) Token: 0x060153CE RID: 86990 RVA: 0x0031D261 File Offset: 0x0031B461
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MatrixColumnJustification.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D84 RID: 28036
		// (get) Token: 0x060153CF RID: 86991 RVA: 0x0031D268 File Offset: 0x0031B468
		// (set) Token: 0x060153D0 RID: 86992 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<HorizontalAlignmentValues> Val
		{
			get
			{
				return (EnumValue<HorizontalAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060153D2 RID: 86994 RVA: 0x0031D277 File Offset: 0x0031B477
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<HorizontalAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060153D3 RID: 86995 RVA: 0x0031D299 File Offset: 0x0031B499
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatrixColumnJustification>(deep);
		}

		// Token: 0x0400923E RID: 37438
		private const string tagName = "mcJc";

		// Token: 0x0400923F RID: 37439
		private const byte tagNsId = 21;

		// Token: 0x04009240 RID: 37440
		internal const int ElementTypeIdConst = 10913;

		// Token: 0x04009241 RID: 37441
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009242 RID: 37442
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
