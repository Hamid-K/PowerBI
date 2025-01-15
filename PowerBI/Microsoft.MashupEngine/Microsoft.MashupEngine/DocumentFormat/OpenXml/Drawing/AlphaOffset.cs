using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026DA RID: 9946
	[GeneratedCode("DomGen", "2.0")]
	internal class AlphaOffset : OpenXmlLeafElement
	{
		// Token: 0x17005DAD RID: 23981
		// (get) Token: 0x06012F84 RID: 77700 RVA: 0x003016A0 File Offset: 0x002FF8A0
		public override string LocalName
		{
			get
			{
				return "alphaOff";
			}
		}

		// Token: 0x17005DAE RID: 23982
		// (get) Token: 0x06012F85 RID: 77701 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005DAF RID: 23983
		// (get) Token: 0x06012F86 RID: 77702 RVA: 0x003016A7 File Offset: 0x002FF8A7
		internal override int ElementTypeId
		{
			get
			{
				return 10012;
			}
		}

		// Token: 0x06012F87 RID: 77703 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005DB0 RID: 23984
		// (get) Token: 0x06012F88 RID: 77704 RVA: 0x003016AE File Offset: 0x002FF8AE
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlphaOffset.attributeTagNames;
			}
		}

		// Token: 0x17005DB1 RID: 23985
		// (get) Token: 0x06012F89 RID: 77705 RVA: 0x003016B5 File Offset: 0x002FF8B5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlphaOffset.attributeNamespaceIds;
			}
		}

		// Token: 0x17005DB2 RID: 23986
		// (get) Token: 0x06012F8A RID: 77706 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012F8B RID: 77707 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public Int32Value Val
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

		// Token: 0x06012F8D RID: 77709 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012F8E RID: 77710 RVA: 0x003016BC File Offset: 0x002FF8BC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlphaOffset>(deep);
		}

		// Token: 0x06012F8F RID: 77711 RVA: 0x003016C8 File Offset: 0x002FF8C8
		// Note: this type is marked as 'beforefieldinit'.
		static AlphaOffset()
		{
			byte[] array = new byte[1];
			AlphaOffset.attributeNamespaceIds = array;
		}

		// Token: 0x040083F4 RID: 33780
		private const string tagName = "alphaOff";

		// Token: 0x040083F5 RID: 33781
		private const byte tagNsId = 10;

		// Token: 0x040083F6 RID: 33782
		internal const int ElementTypeIdConst = 10012;

		// Token: 0x040083F7 RID: 33783
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040083F8 RID: 33784
		private static byte[] attributeNamespaceIds;
	}
}
