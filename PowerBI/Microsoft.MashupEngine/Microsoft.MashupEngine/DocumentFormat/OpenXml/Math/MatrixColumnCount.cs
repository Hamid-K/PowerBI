using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B0 RID: 10672
	[GeneratedCode("DomGen", "2.0")]
	internal class MatrixColumnCount : OpenXmlLeafElement
	{
		// Token: 0x17006D79 RID: 28025
		// (get) Token: 0x060153BD RID: 86973 RVA: 0x00149599 File Offset: 0x00147799
		public override string LocalName
		{
			get
			{
				return "count";
			}
		}

		// Token: 0x17006D7A RID: 28026
		// (get) Token: 0x060153BE RID: 86974 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D7B RID: 28027
		// (get) Token: 0x060153BF RID: 86975 RVA: 0x0031D1F8 File Offset: 0x0031B3F8
		internal override int ElementTypeId
		{
			get
			{
				return 10912;
			}
		}

		// Token: 0x060153C0 RID: 86976 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006D7C RID: 28028
		// (get) Token: 0x060153C1 RID: 86977 RVA: 0x0031D1FF File Offset: 0x0031B3FF
		internal override string[] AttributeTagNames
		{
			get
			{
				return MatrixColumnCount.attributeTagNames;
			}
		}

		// Token: 0x17006D7D RID: 28029
		// (get) Token: 0x060153C2 RID: 86978 RVA: 0x0031D206 File Offset: 0x0031B406
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MatrixColumnCount.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D7E RID: 28030
		// (get) Token: 0x060153C3 RID: 86979 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x060153C4 RID: 86980 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public IntegerValue Val
		{
			get
			{
				return (IntegerValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060153C6 RID: 86982 RVA: 0x0031CBB6 File Offset: 0x0031ADB6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060153C7 RID: 86983 RVA: 0x0031D20D File Offset: 0x0031B40D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatrixColumnCount>(deep);
		}

		// Token: 0x04009239 RID: 37433
		private const string tagName = "count";

		// Token: 0x0400923A RID: 37434
		private const byte tagNsId = 21;

		// Token: 0x0400923B RID: 37435
		internal const int ElementTypeIdConst = 10912;

		// Token: 0x0400923C RID: 37436
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400923D RID: 37437
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
