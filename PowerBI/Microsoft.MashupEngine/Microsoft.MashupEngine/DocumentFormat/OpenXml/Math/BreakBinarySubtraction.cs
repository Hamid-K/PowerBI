using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029D2 RID: 10706
	[GeneratedCode("DomGen", "2.0")]
	internal class BreakBinarySubtraction : OpenXmlLeafElement
	{
		// Token: 0x17006E2F RID: 28207
		// (get) Token: 0x0601554A RID: 87370 RVA: 0x0031E038 File Offset: 0x0031C238
		public override string LocalName
		{
			get
			{
				return "brkBinSub";
			}
		}

		// Token: 0x17006E30 RID: 28208
		// (get) Token: 0x0601554B RID: 87371 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006E31 RID: 28209
		// (get) Token: 0x0601554C RID: 87372 RVA: 0x0031E03F File Offset: 0x0031C23F
		internal override int ElementTypeId
		{
			get
			{
				return 10948;
			}
		}

		// Token: 0x0601554D RID: 87373 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006E32 RID: 28210
		// (get) Token: 0x0601554E RID: 87374 RVA: 0x0031E046 File Offset: 0x0031C246
		internal override string[] AttributeTagNames
		{
			get
			{
				return BreakBinarySubtraction.attributeTagNames;
			}
		}

		// Token: 0x17006E33 RID: 28211
		// (get) Token: 0x0601554F RID: 87375 RVA: 0x0031E04D File Offset: 0x0031C24D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BreakBinarySubtraction.attributeNamespaceIds;
			}
		}

		// Token: 0x17006E34 RID: 28212
		// (get) Token: 0x06015550 RID: 87376 RVA: 0x0031E054 File Offset: 0x0031C254
		// (set) Token: 0x06015551 RID: 87377 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<BreakBinarySubtractionValues> Val
		{
			get
			{
				return (EnumValue<BreakBinarySubtractionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015553 RID: 87379 RVA: 0x0031E063 File Offset: 0x0031C263
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<BreakBinarySubtractionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015554 RID: 87380 RVA: 0x0031E085 File Offset: 0x0031C285
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BreakBinarySubtraction>(deep);
		}

		// Token: 0x040092BC RID: 37564
		private const string tagName = "brkBinSub";

		// Token: 0x040092BD RID: 37565
		private const byte tagNsId = 21;

		// Token: 0x040092BE RID: 37566
		internal const int ElementTypeIdConst = 10948;

		// Token: 0x040092BF RID: 37567
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040092C0 RID: 37568
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
