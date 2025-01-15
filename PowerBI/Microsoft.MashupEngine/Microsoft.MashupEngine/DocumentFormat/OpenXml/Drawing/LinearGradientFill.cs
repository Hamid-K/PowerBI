using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026FB RID: 9979
	[GeneratedCode("DomGen", "2.0")]
	internal class LinearGradientFill : OpenXmlLeafElement
	{
		// Token: 0x17005E45 RID: 24133
		// (get) Token: 0x060130D3 RID: 78035 RVA: 0x002ECE00 File Offset: 0x002EB000
		public override string LocalName
		{
			get
			{
				return "lin";
			}
		}

		// Token: 0x17005E46 RID: 24134
		// (get) Token: 0x060130D4 RID: 78036 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E47 RID: 24135
		// (get) Token: 0x060130D5 RID: 78037 RVA: 0x00302FF7 File Offset: 0x003011F7
		internal override int ElementTypeId
		{
			get
			{
				return 10043;
			}
		}

		// Token: 0x060130D6 RID: 78038 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E48 RID: 24136
		// (get) Token: 0x060130D7 RID: 78039 RVA: 0x00302FFE File Offset: 0x003011FE
		internal override string[] AttributeTagNames
		{
			get
			{
				return LinearGradientFill.attributeTagNames;
			}
		}

		// Token: 0x17005E49 RID: 24137
		// (get) Token: 0x060130D8 RID: 78040 RVA: 0x00303005 File Offset: 0x00301205
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LinearGradientFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E4A RID: 24138
		// (get) Token: 0x060130D9 RID: 78041 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060130DA RID: 78042 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ang")]
		public Int32Value Angle
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005E4B RID: 24139
		// (get) Token: 0x060130DB RID: 78043 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060130DC RID: 78044 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "scaled")]
		public BooleanValue Scaled
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060130DE RID: 78046 RVA: 0x0030300C File Offset: 0x0030120C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ang" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "scaled" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060130DF RID: 78047 RVA: 0x00303042 File Offset: 0x00301242
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LinearGradientFill>(deep);
		}

		// Token: 0x060130E0 RID: 78048 RVA: 0x0030304C File Offset: 0x0030124C
		// Note: this type is marked as 'beforefieldinit'.
		static LinearGradientFill()
		{
			byte[] array = new byte[2];
			LinearGradientFill.attributeNamespaceIds = array;
		}

		// Token: 0x0400846F RID: 33903
		private const string tagName = "lin";

		// Token: 0x04008470 RID: 33904
		private const byte tagNsId = 10;

		// Token: 0x04008471 RID: 33905
		internal const int ElementTypeIdConst = 10043;

		// Token: 0x04008472 RID: 33906
		private static string[] attributeTagNames = new string[] { "ang", "scaled" };

		// Token: 0x04008473 RID: 33907
		private static byte[] attributeNamespaceIds;
	}
}
