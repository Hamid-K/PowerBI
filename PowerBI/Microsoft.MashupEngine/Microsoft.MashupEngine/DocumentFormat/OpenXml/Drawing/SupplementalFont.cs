using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002771 RID: 10097
	[GeneratedCode("DomGen", "2.0")]
	internal class SupplementalFont : OpenXmlLeafElement
	{
		// Token: 0x17006153 RID: 24915
		// (get) Token: 0x0601379C RID: 79772 RVA: 0x002AD88F File Offset: 0x002ABA8F
		public override string LocalName
		{
			get
			{
				return "font";
			}
		}

		// Token: 0x17006154 RID: 24916
		// (get) Token: 0x0601379D RID: 79773 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006155 RID: 24917
		// (get) Token: 0x0601379E RID: 79774 RVA: 0x0030778A File Offset: 0x0030598A
		internal override int ElementTypeId
		{
			get
			{
				return 10134;
			}
		}

		// Token: 0x0601379F RID: 79775 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006156 RID: 24918
		// (get) Token: 0x060137A0 RID: 79776 RVA: 0x00307791 File Offset: 0x00305991
		internal override string[] AttributeTagNames
		{
			get
			{
				return SupplementalFont.attributeTagNames;
			}
		}

		// Token: 0x17006157 RID: 24919
		// (get) Token: 0x060137A1 RID: 79777 RVA: 0x00307798 File Offset: 0x00305998
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SupplementalFont.attributeNamespaceIds;
			}
		}

		// Token: 0x17006158 RID: 24920
		// (get) Token: 0x060137A2 RID: 79778 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060137A3 RID: 79779 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "script")]
		public StringValue Script
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

		// Token: 0x17006159 RID: 24921
		// (get) Token: 0x060137A4 RID: 79780 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060137A5 RID: 79781 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "typeface")]
		public StringValue Typeface
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060137A7 RID: 79783 RVA: 0x0030779F File Offset: 0x0030599F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "script" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "typeface" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060137A8 RID: 79784 RVA: 0x003077D5 File Offset: 0x003059D5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SupplementalFont>(deep);
		}

		// Token: 0x060137A9 RID: 79785 RVA: 0x003077E0 File Offset: 0x003059E0
		// Note: this type is marked as 'beforefieldinit'.
		static SupplementalFont()
		{
			byte[] array = new byte[2];
			SupplementalFont.attributeNamespaceIds = array;
		}

		// Token: 0x04008661 RID: 34401
		private const string tagName = "font";

		// Token: 0x04008662 RID: 34402
		private const byte tagNsId = 10;

		// Token: 0x04008663 RID: 34403
		internal const int ElementTypeIdConst = 10134;

		// Token: 0x04008664 RID: 34404
		private static string[] attributeTagNames = new string[] { "script", "typeface" };

		// Token: 0x04008665 RID: 34405
		private static byte[] attributeNamespaceIds;
	}
}
