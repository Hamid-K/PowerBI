using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002987 RID: 10631
	[GeneratedCode("DomGen", "2.0")]
	internal class Break : OpenXmlLeafElement
	{
		// Token: 0x17006CA0 RID: 27808
		// (get) Token: 0x060151CE RID: 86478 RVA: 0x0031B686 File Offset: 0x00319886
		public override string LocalName
		{
			get
			{
				return "brk";
			}
		}

		// Token: 0x17006CA1 RID: 27809
		// (get) Token: 0x060151CF RID: 86479 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CA2 RID: 27810
		// (get) Token: 0x060151D0 RID: 86480 RVA: 0x0031B68D File Offset: 0x0031988D
		internal override int ElementTypeId
		{
			get
			{
				return 10866;
			}
		}

		// Token: 0x060151D1 RID: 86481 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006CA3 RID: 27811
		// (get) Token: 0x060151D2 RID: 86482 RVA: 0x0031B694 File Offset: 0x00319894
		internal override string[] AttributeTagNames
		{
			get
			{
				return Break.attributeTagNames;
			}
		}

		// Token: 0x17006CA4 RID: 27812
		// (get) Token: 0x060151D3 RID: 86483 RVA: 0x0031B69B File Offset: 0x0031989B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Break.attributeNamespaceIds;
			}
		}

		// Token: 0x17006CA5 RID: 27813
		// (get) Token: 0x060151D4 RID: 86484 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x060151D5 RID: 86485 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(21, "alnAt")]
		public IntegerValue AlignAt
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

		// Token: 0x17006CA6 RID: 27814
		// (get) Token: 0x060151D6 RID: 86486 RVA: 0x002BD46B File Offset: 0x002BB66B
		// (set) Token: 0x060151D7 RID: 86487 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(21, "val")]
		public IntegerValue Val
		{
			get
			{
				return (IntegerValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060151D9 RID: 86489 RVA: 0x0031B6A2 File Offset: 0x003198A2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "alnAt" == name)
			{
				return new IntegerValue();
			}
			if (21 == namespaceId && "val" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060151DA RID: 86490 RVA: 0x0031B6DC File Offset: 0x003198DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Break>(deep);
		}

		// Token: 0x040091A1 RID: 37281
		private const string tagName = "brk";

		// Token: 0x040091A2 RID: 37282
		private const byte tagNsId = 21;

		// Token: 0x040091A3 RID: 37283
		internal const int ElementTypeIdConst = 10866;

		// Token: 0x040091A4 RID: 37284
		private static string[] attributeTagNames = new string[] { "alnAt", "val" };

		// Token: 0x040091A5 RID: 37285
		private static byte[] attributeNamespaceIds = new byte[] { 21, 21 };
	}
}
