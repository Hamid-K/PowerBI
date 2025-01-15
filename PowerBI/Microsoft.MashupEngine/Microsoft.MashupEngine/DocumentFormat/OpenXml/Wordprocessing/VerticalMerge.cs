using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EE7 RID: 12007
	[GeneratedCode("DomGen", "2.0")]
	internal class VerticalMerge : OpenXmlLeafElement
	{
		// Token: 0x17008D59 RID: 36185
		// (get) Token: 0x060199FF RID: 104959 RVA: 0x0035354C File Offset: 0x0035174C
		public override string LocalName
		{
			get
			{
				return "vMerge";
			}
		}

		// Token: 0x17008D5A RID: 36186
		// (get) Token: 0x06019A00 RID: 104960 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D5B RID: 36187
		// (get) Token: 0x06019A01 RID: 104961 RVA: 0x00353553 File Offset: 0x00351753
		internal override int ElementTypeId
		{
			get
			{
				return 11653;
			}
		}

		// Token: 0x06019A02 RID: 104962 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008D5C RID: 36188
		// (get) Token: 0x06019A03 RID: 104963 RVA: 0x0035355A File Offset: 0x0035175A
		internal override string[] AttributeTagNames
		{
			get
			{
				return VerticalMerge.attributeTagNames;
			}
		}

		// Token: 0x17008D5D RID: 36189
		// (get) Token: 0x06019A04 RID: 104964 RVA: 0x00353561 File Offset: 0x00351761
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VerticalMerge.attributeNamespaceIds;
			}
		}

		// Token: 0x17008D5E RID: 36190
		// (get) Token: 0x06019A05 RID: 104965 RVA: 0x003534DC File Offset: 0x003516DC
		// (set) Token: 0x06019A06 RID: 104966 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<MergedCellValues> Val
		{
			get
			{
				return (EnumValue<MergedCellValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019A08 RID: 104968 RVA: 0x003534EB File Offset: 0x003516EB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<MergedCellValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019A09 RID: 104969 RVA: 0x00353568 File Offset: 0x00351768
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VerticalMerge>(deep);
		}

		// Token: 0x0400A9BF RID: 43455
		private const string tagName = "vMerge";

		// Token: 0x0400A9C0 RID: 43456
		private const byte tagNsId = 23;

		// Token: 0x0400A9C1 RID: 43457
		internal const int ElementTypeIdConst = 11653;

		// Token: 0x0400A9C2 RID: 43458
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A9C3 RID: 43459
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
