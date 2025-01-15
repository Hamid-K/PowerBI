using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025AB RID: 9643
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstSliceAngle : OpenXmlLeafElement
	{
		// Token: 0x1700570D RID: 22285
		// (get) Token: 0x060120BF RID: 73919 RVA: 0x002F5068 File Offset: 0x002F3268
		public override string LocalName
		{
			get
			{
				return "firstSliceAng";
			}
		}

		// Token: 0x1700570E RID: 22286
		// (get) Token: 0x060120C0 RID: 73920 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700570F RID: 22287
		// (get) Token: 0x060120C1 RID: 73921 RVA: 0x002F506F File Offset: 0x002F326F
		internal override int ElementTypeId
		{
			get
			{
				return 10461;
			}
		}

		// Token: 0x060120C2 RID: 73922 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005710 RID: 22288
		// (get) Token: 0x060120C3 RID: 73923 RVA: 0x002F5076 File Offset: 0x002F3276
		internal override string[] AttributeTagNames
		{
			get
			{
				return FirstSliceAngle.attributeTagNames;
			}
		}

		// Token: 0x17005711 RID: 22289
		// (get) Token: 0x060120C4 RID: 73924 RVA: 0x002F507D File Offset: 0x002F327D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FirstSliceAngle.attributeNamespaceIds;
			}
		}

		// Token: 0x17005712 RID: 22290
		// (get) Token: 0x060120C5 RID: 73925 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x060120C6 RID: 73926 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060120C8 RID: 73928 RVA: 0x002F41C3 File Offset: 0x002F23C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060120C9 RID: 73929 RVA: 0x002F5084 File Offset: 0x002F3284
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstSliceAngle>(deep);
		}

		// Token: 0x060120CA RID: 73930 RVA: 0x002F5090 File Offset: 0x002F3290
		// Note: this type is marked as 'beforefieldinit'.
		static FirstSliceAngle()
		{
			byte[] array = new byte[1];
			FirstSliceAngle.attributeNamespaceIds = array;
		}

		// Token: 0x04007DE6 RID: 32230
		private const string tagName = "firstSliceAng";

		// Token: 0x04007DE7 RID: 32231
		private const byte tagNsId = 11;

		// Token: 0x04007DE8 RID: 32232
		internal const int ElementTypeIdConst = 10461;

		// Token: 0x04007DE9 RID: 32233
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007DEA RID: 32234
		private static byte[] attributeNamespaceIds;
	}
}
