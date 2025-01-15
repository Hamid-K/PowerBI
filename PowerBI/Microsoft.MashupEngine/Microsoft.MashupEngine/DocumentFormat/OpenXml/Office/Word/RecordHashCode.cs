using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002480 RID: 9344
	[GeneratedCode("DomGen", "2.0")]
	internal class RecordHashCode : OpenXmlLeafElement
	{
		// Token: 0x17005146 RID: 20806
		// (get) Token: 0x060113E7 RID: 70631 RVA: 0x002EC034 File Offset: 0x002EA234
		public override string LocalName
		{
			get
			{
				return "hash";
			}
		}

		// Token: 0x17005147 RID: 20807
		// (get) Token: 0x060113E8 RID: 70632 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x17005148 RID: 20808
		// (get) Token: 0x060113E9 RID: 70633 RVA: 0x002EC03B File Offset: 0x002EA23B
		internal override int ElementTypeId
		{
			get
			{
				return 12571;
			}
		}

		// Token: 0x060113EA RID: 70634 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005149 RID: 20809
		// (get) Token: 0x060113EB RID: 70635 RVA: 0x002EC042 File Offset: 0x002EA242
		internal override string[] AttributeTagNames
		{
			get
			{
				return RecordHashCode.attributeTagNames;
			}
		}

		// Token: 0x1700514A RID: 20810
		// (get) Token: 0x060113EC RID: 70636 RVA: 0x002EC049 File Offset: 0x002EA249
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RecordHashCode.attributeNamespaceIds;
			}
		}

		// Token: 0x1700514B RID: 20811
		// (get) Token: 0x060113ED RID: 70637 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x060113EE RID: 70638 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "val")]
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

		// Token: 0x060113F0 RID: 70640 RVA: 0x002EC05F File Offset: 0x002EA25F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "val" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060113F1 RID: 70641 RVA: 0x002EC081 File Offset: 0x002EA281
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RecordHashCode>(deep);
		}

		// Token: 0x040078D7 RID: 30935
		private const string tagName = "hash";

		// Token: 0x040078D8 RID: 30936
		private const byte tagNsId = 33;

		// Token: 0x040078D9 RID: 30937
		internal const int ElementTypeIdConst = 12571;

		// Token: 0x040078DA RID: 30938
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040078DB RID: 30939
		private static byte[] attributeNamespaceIds = new byte[] { 33 };
	}
}
