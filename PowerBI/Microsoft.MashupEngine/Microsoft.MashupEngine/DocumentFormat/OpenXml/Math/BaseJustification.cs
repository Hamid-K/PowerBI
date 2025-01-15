using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A2 RID: 10658
	[GeneratedCode("DomGen", "2.0")]
	internal class BaseJustification : OpenXmlLeafElement
	{
		// Token: 0x17006D28 RID: 27944
		// (get) Token: 0x0601530D RID: 86797 RVA: 0x0031CB1C File Offset: 0x0031AD1C
		public override string LocalName
		{
			get
			{
				return "baseJc";
			}
		}

		// Token: 0x17006D29 RID: 27945
		// (get) Token: 0x0601530E RID: 86798 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D2A RID: 27946
		// (get) Token: 0x0601530F RID: 86799 RVA: 0x0031CB23 File Offset: 0x0031AD23
		internal override int ElementTypeId
		{
			get
			{
				return 10895;
			}
		}

		// Token: 0x06015310 RID: 86800 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006D2B RID: 27947
		// (get) Token: 0x06015311 RID: 86801 RVA: 0x0031CB2A File Offset: 0x0031AD2A
		internal override string[] AttributeTagNames
		{
			get
			{
				return BaseJustification.attributeTagNames;
			}
		}

		// Token: 0x17006D2C RID: 27948
		// (get) Token: 0x06015312 RID: 86802 RVA: 0x0031CB31 File Offset: 0x0031AD31
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BaseJustification.attributeNamespaceIds;
			}
		}

		// Token: 0x17006D2D RID: 27949
		// (get) Token: 0x06015313 RID: 86803 RVA: 0x0031CB38 File Offset: 0x0031AD38
		// (set) Token: 0x06015314 RID: 86804 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "val")]
		public EnumValue<VerticalAlignmentValues> Val
		{
			get
			{
				return (EnumValue<VerticalAlignmentValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015316 RID: 86806 RVA: 0x0031CB47 File Offset: 0x0031AD47
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "val" == name)
			{
				return new EnumValue<VerticalAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015317 RID: 86807 RVA: 0x0031CB69 File Offset: 0x0031AD69
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BaseJustification>(deep);
		}

		// Token: 0x04009201 RID: 37377
		private const string tagName = "baseJc";

		// Token: 0x04009202 RID: 37378
		private const byte tagNsId = 21;

		// Token: 0x04009203 RID: 37379
		internal const int ElementTypeIdConst = 10895;

		// Token: 0x04009204 RID: 37380
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009205 RID: 37381
		private static byte[] attributeNamespaceIds = new byte[] { 21 };
	}
}
