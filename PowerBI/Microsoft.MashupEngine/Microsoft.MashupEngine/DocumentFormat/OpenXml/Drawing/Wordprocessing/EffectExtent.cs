using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A7 RID: 10407
	[GeneratedCode("DomGen", "2.0")]
	internal class EffectExtent : OpenXmlLeafElement
	{
		// Token: 0x17006872 RID: 26738
		// (get) Token: 0x060147E1 RID: 83937 RVA: 0x00313F74 File Offset: 0x00312174
		public override string LocalName
		{
			get
			{
				return "effectExtent";
			}
		}

		// Token: 0x17006873 RID: 26739
		// (get) Token: 0x060147E2 RID: 83938 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006874 RID: 26740
		// (get) Token: 0x060147E3 RID: 83939 RVA: 0x00313F7B File Offset: 0x0031217B
		internal override int ElementTypeId
		{
			get
			{
				return 10703;
			}
		}

		// Token: 0x060147E4 RID: 83940 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006875 RID: 26741
		// (get) Token: 0x060147E5 RID: 83941 RVA: 0x00313F82 File Offset: 0x00312182
		internal override string[] AttributeTagNames
		{
			get
			{
				return EffectExtent.attributeTagNames;
			}
		}

		// Token: 0x17006876 RID: 26742
		// (get) Token: 0x060147E6 RID: 83942 RVA: 0x00313F89 File Offset: 0x00312189
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EffectExtent.attributeNamespaceIds;
			}
		}

		// Token: 0x17006877 RID: 26743
		// (get) Token: 0x060147E7 RID: 83943 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060147E8 RID: 83944 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "l")]
		public Int64Value LeftEdge
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006878 RID: 26744
		// (get) Token: 0x060147E9 RID: 83945 RVA: 0x002E0CC3 File Offset: 0x002DEEC3
		// (set) Token: 0x060147EA RID: 83946 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "t")]
		public Int64Value TopEdge
		{
			get
			{
				return (Int64Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006879 RID: 26745
		// (get) Token: 0x060147EB RID: 83947 RVA: 0x002E0CD2 File Offset: 0x002DEED2
		// (set) Token: 0x060147EC RID: 83948 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "r")]
		public Int64Value RightEdge
		{
			get
			{
				return (Int64Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700687A RID: 26746
		// (get) Token: 0x060147ED RID: 83949 RVA: 0x00313F90 File Offset: 0x00312190
		// (set) Token: 0x060147EE RID: 83950 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "b")]
		public Int64Value BottomEdge
		{
			get
			{
				return (Int64Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060147F0 RID: 83952 RVA: 0x00313FA0 File Offset: 0x003121A0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "l" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "t" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new Int64Value();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060147F1 RID: 83953 RVA: 0x0031400D File Offset: 0x0031220D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EffectExtent>(deep);
		}

		// Token: 0x060147F2 RID: 83954 RVA: 0x00314018 File Offset: 0x00312218
		// Note: this type is marked as 'beforefieldinit'.
		static EffectExtent()
		{
			byte[] array = new byte[4];
			EffectExtent.attributeNamespaceIds = array;
		}

		// Token: 0x04008E55 RID: 36437
		private const string tagName = "effectExtent";

		// Token: 0x04008E56 RID: 36438
		private const byte tagNsId = 16;

		// Token: 0x04008E57 RID: 36439
		internal const int ElementTypeIdConst = 10703;

		// Token: 0x04008E58 RID: 36440
		private static string[] attributeTagNames = new string[] { "l", "t", "r", "b" };

		// Token: 0x04008E59 RID: 36441
		private static byte[] attributeNamespaceIds;
	}
}
