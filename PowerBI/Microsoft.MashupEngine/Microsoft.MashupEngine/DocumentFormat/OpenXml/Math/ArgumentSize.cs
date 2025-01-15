using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029CB RID: 10699
	[GeneratedCode("DomGen", "2.0")]
	internal class ArgumentSize : OpenXmlLeafElement
	{
		// Token: 0x17006E0D RID: 28173
		// (get) Token: 0x06015503 RID: 87299 RVA: 0x0031DDC4 File Offset: 0x0031BFC4
		public override string LocalName
		{
			get
			{
				return "argSz";
			}
		}

		// Token: 0x17006E0E RID: 28174
		// (get) Token: 0x06015504 RID: 87300 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E0F RID: 28175
		// (get) Token: 0x06015505 RID: 87301 RVA: 0x0031DDCB File Offset: 0x0031BFCB
		internal override int ElementTypeId
		{
			get
			{
				return 10943;
			}
		}

		// Token: 0x06015506 RID: 87302 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E10 RID: 28176
		// (get) Token: 0x06015507 RID: 87303 RVA: 0x0031DDD2 File Offset: 0x0031BFD2
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArgumentSize.attributeTagNames;
			}
		}

		// Token: 0x17006E11 RID: 28177
		// (get) Token: 0x06015508 RID: 87304 RVA: 0x0031DDD9 File Offset: 0x0031BFD9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArgumentSize.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E12 RID: 28178
		// (get) Token: 0x06015509 RID: 87305 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x0601550A RID: 87306 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601550C RID: 87308 RVA: 0x0031CBB6 File Offset: 0x0031ADB6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601550D RID: 87309 RVA: 0x0031DDE0 File Offset: 0x0031BFE0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArgumentSize>(deep);
		}

		// Token: 0x040092A0 RID: 37536
		private const string tagName = "argSz";

		// Token: 0x040092A1 RID: 37537
		private const byte tagNsId = 21;

		// Token: 0x040092A2 RID: 37538
		internal const int ElementTypeIdConst = 10943;

		// Token: 0x040092A3 RID: 37539
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040092A4 RID: 37540
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
