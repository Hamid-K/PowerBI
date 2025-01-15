using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025DC RID: 9692
	[GeneratedCode("DomGen", "2.0")]
	internal class SizeRepresents : OpenXmlLeafElement
	{
		// Token: 0x17005865 RID: 22629
		// (get) Token: 0x060123AC RID: 74668 RVA: 0x002F7A0F File Offset: 0x002F5C0F
		public override string LocalName
		{
			get
			{
				return "sizeRepresents";
			}
		}

		// Token: 0x17005866 RID: 22630
		// (get) Token: 0x060123AD RID: 74669 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005867 RID: 22631
		// (get) Token: 0x060123AE RID: 74670 RVA: 0x002F7A16 File Offset: 0x002F5C16
		internal override int ElementTypeId
		{
			get
			{
				return 10535;
			}
		}

		// Token: 0x060123AF RID: 74671 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005868 RID: 22632
		// (get) Token: 0x060123B0 RID: 74672 RVA: 0x002F7A1D File Offset: 0x002F5C1D
		internal override string[] AttributeTagNames
		{
			get
			{
				return SizeRepresents.attributeTagNames;
			}
		}

		// Token: 0x17005869 RID: 22633
		// (get) Token: 0x060123B1 RID: 74673 RVA: 0x002F7A24 File Offset: 0x002F5C24
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SizeRepresents.attributeNamespaceIds;
			}
		}

		// Token: 0x1700586A RID: 22634
		// (get) Token: 0x060123B2 RID: 74674 RVA: 0x002F7A2B File Offset: 0x002F5C2B
		// (set) Token: 0x060123B3 RID: 74675 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<SizeRepresentsValues> Val
		{
			get
			{
				return (EnumValue<SizeRepresentsValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060123B5 RID: 74677 RVA: 0x002F7A3A File Offset: 0x002F5C3A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<SizeRepresentsValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060123B6 RID: 74678 RVA: 0x002F7A5A File Offset: 0x002F5C5A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SizeRepresents>(deep);
		}

		// Token: 0x060123B7 RID: 74679 RVA: 0x002F7A64 File Offset: 0x002F5C64
		// Note: this type is marked as 'beforefieldinit'.
		static SizeRepresents()
		{
			byte[] array = new byte[1];
			SizeRepresents.attributeNamespaceIds = array;
		}

		// Token: 0x04007EBB RID: 32443
		private const string tagName = "sizeRepresents";

		// Token: 0x04007EBC RID: 32444
		private const byte tagNsId = 11;

		// Token: 0x04007EBD RID: 32445
		internal const int ElementTypeIdConst = 10535;

		// Token: 0x04007EBE RID: 32446
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007EBF RID: 32447
		private static byte[] attributeNamespaceIds;
	}
}
