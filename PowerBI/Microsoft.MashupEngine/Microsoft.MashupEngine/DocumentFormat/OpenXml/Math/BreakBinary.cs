using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029D1 RID: 10705
	[GeneratedCode("DomGen", "2.0")]
	internal class BreakBinary : OpenXmlLeafElement
	{
		// Token: 0x17006E29 RID: 28201
		// (get) Token: 0x0601553E RID: 87358 RVA: 0x0031DFAC File Offset: 0x0031C1AC
		public override string LocalName
		{
			get
			{
				return "brkBin";
			}
		}

		// Token: 0x17006E2A RID: 28202
		// (get) Token: 0x0601553F RID: 87359 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E2B RID: 28203
		// (get) Token: 0x06015540 RID: 87360 RVA: 0x0031DFB3 File Offset: 0x0031C1B3
		internal override int ElementTypeId
		{
			get
			{
				return 10947;
			}
		}

		// Token: 0x06015541 RID: 87361 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E2C RID: 28204
		// (get) Token: 0x06015542 RID: 87362 RVA: 0x0031DFBA File Offset: 0x0031C1BA
		internal override string[] AttributeTagNames
		{
			get
			{
				return BreakBinary.attributeTagNames;
			}
		}

		// Token: 0x17006E2D RID: 28205
		// (get) Token: 0x06015543 RID: 87363 RVA: 0x0031DFC1 File Offset: 0x0031C1C1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BreakBinary.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E2E RID: 28206
		// (get) Token: 0x06015544 RID: 87364 RVA: 0x0031DFC8 File Offset: 0x0031C1C8
		// (set) Token: 0x06015545 RID: 87365 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<BreakBinaryOperatorValues> Val
		{
			get
			{
				return (EnumValue<BreakBinaryOperatorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015547 RID: 87367 RVA: 0x0031DFD7 File Offset: 0x0031C1D7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<BreakBinaryOperatorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015548 RID: 87368 RVA: 0x0031DFF9 File Offset: 0x0031C1F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BreakBinary>(deep);
		}

		// Token: 0x040092B7 RID: 37559
		private const string tagName = "brkBin";

		// Token: 0x040092B8 RID: 37560
		private const byte tagNsId = 21;

		// Token: 0x040092B9 RID: 37561
		internal const int ElementTypeIdConst = 10947;

		// Token: 0x040092BA RID: 37562
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040092BB RID: 37563
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
