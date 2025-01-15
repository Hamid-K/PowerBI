using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029D0 RID: 10704
	[GeneratedCode("DomGen", "2.0")]
	internal class MathFont : OpenXmlLeafElement
	{
		// Token: 0x17006E23 RID: 28195
		// (get) Token: 0x06015532 RID: 87346 RVA: 0x0031DF52 File Offset: 0x0031C152
		public override string LocalName
		{
			get
			{
				return "mathFont";
			}
		}

		// Token: 0x17006E24 RID: 28196
		// (get) Token: 0x06015533 RID: 87347 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E25 RID: 28197
		// (get) Token: 0x06015534 RID: 87348 RVA: 0x0031DF59 File Offset: 0x0031C159
		internal override int ElementTypeId
		{
			get
			{
				return 10946;
			}
		}

		// Token: 0x06015535 RID: 87349 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E26 RID: 28198
		// (get) Token: 0x06015536 RID: 87350 RVA: 0x0031DF60 File Offset: 0x0031C160
		internal override string[] AttributeTagNames
		{
			get
			{
				return MathFont.attributeTagNames;
			}
		}

		// Token: 0x17006E27 RID: 28199
		// (get) Token: 0x06015537 RID: 87351 RVA: 0x0031DF67 File Offset: 0x0031C167
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MathFont.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E28 RID: 28200
		// (get) Token: 0x06015538 RID: 87352 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015539 RID: 87353 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public StringValue Val
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

		// Token: 0x0601553B RID: 87355 RVA: 0x0031B905 File Offset: 0x00319B05
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601553C RID: 87356 RVA: 0x0031DF6E File Offset: 0x0031C16E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MathFont>(deep);
		}

		// Token: 0x040092B2 RID: 37554
		private const string tagName = "mathFont";

		// Token: 0x040092B3 RID: 37555
		private const byte tagNsId = 21;

		// Token: 0x040092B4 RID: 37556
		internal const int ElementTypeIdConst = 10946;

		// Token: 0x040092B5 RID: 37557
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040092B6 RID: 37558
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
