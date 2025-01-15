using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025AF RID: 9647
	[GeneratedCode("DomGen", "2.0")]
	internal class SecondPieSize : OpenXmlLeafElement
	{
		// Token: 0x17005722 RID: 22306
		// (get) Token: 0x060120ED RID: 73965 RVA: 0x002F51F9 File Offset: 0x002F33F9
		public override string LocalName
		{
			get
			{
				return "secondPieSize";
			}
		}

		// Token: 0x17005723 RID: 22307
		// (get) Token: 0x060120EE RID: 73966 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005724 RID: 22308
		// (get) Token: 0x060120EF RID: 73967 RVA: 0x002F5200 File Offset: 0x002F3400
		internal override int ElementTypeId
		{
			get
			{
				return 10466;
			}
		}

		// Token: 0x060120F0 RID: 73968 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005725 RID: 22309
		// (get) Token: 0x060120F1 RID: 73969 RVA: 0x002F5207 File Offset: 0x002F3407
		internal override string[] AttributeTagNames
		{
			get
			{
				return SecondPieSize.attributeTagNames;
			}
		}

		// Token: 0x17005726 RID: 22310
		// (get) Token: 0x060120F2 RID: 73970 RVA: 0x002F520E File Offset: 0x002F340E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SecondPieSize.attributeNamespaceIds;
			}
		}

		// Token: 0x17005727 RID: 22311
		// (get) Token: 0x060120F3 RID: 73971 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x060120F4 RID: 73972 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public UInt16Value Val
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060120F6 RID: 73974 RVA: 0x002F41C3 File Offset: 0x002F23C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060120F7 RID: 73975 RVA: 0x002F5215 File Offset: 0x002F3415
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SecondPieSize>(deep);
		}

		// Token: 0x060120F8 RID: 73976 RVA: 0x002F5220 File Offset: 0x002F3420
		// Note: this type is marked as 'beforefieldinit'.
		static SecondPieSize()
		{
			byte[] array = new byte[1];
			SecondPieSize.attributeNamespaceIds = array;
		}

		// Token: 0x04007DF8 RID: 32248
		private const string tagName = "secondPieSize";

		// Token: 0x04007DF9 RID: 32249
		private const byte tagNsId = 11;

		// Token: 0x04007DFA RID: 32250
		internal const int ElementTypeIdConst = 10466;

		// Token: 0x04007DFB RID: 32251
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DFC RID: 32252
		private static byte[] attributeNamespaceIds;
	}
}
