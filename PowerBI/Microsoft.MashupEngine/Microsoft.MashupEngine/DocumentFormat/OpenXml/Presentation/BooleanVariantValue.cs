using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A2B RID: 10795
	[GeneratedCode("DomGen", "2.0")]
	internal class BooleanVariantValue : OpenXmlLeafElement
	{
		// Token: 0x170070AB RID: 28843
		// (get) Token: 0x06015ACD RID: 88781 RVA: 0x00321EC6 File Offset: 0x003200C6
		public override string LocalName
		{
			get
			{
				return "boolVal";
			}
		}

		// Token: 0x170070AC RID: 28844
		// (get) Token: 0x06015ACE RID: 88782 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070AD RID: 28845
		// (get) Token: 0x06015ACF RID: 88783 RVA: 0x00321ECD File Offset: 0x003200CD
		internal override int ElementTypeId
		{
			get
			{
				return 12217;
			}
		}

		// Token: 0x06015AD0 RID: 88784 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070AE RID: 28846
		// (get) Token: 0x06015AD1 RID: 88785 RVA: 0x00321ED4 File Offset: 0x003200D4
		internal override string[] AttributeTagNames
		{
			get
			{
				return BooleanVariantValue.attributeTagNames;
			}
		}

		// Token: 0x170070AF RID: 28847
		// (get) Token: 0x06015AD2 RID: 88786 RVA: 0x00321EDB File Offset: 0x003200DB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BooleanVariantValue.attributeNamespaceIds;
			}
		}

		// Token: 0x170070B0 RID: 28848
		// (get) Token: 0x06015AD3 RID: 88787 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06015AD4 RID: 88788 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public BooleanValue Val
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06015AD6 RID: 88790 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015AD7 RID: 88791 RVA: 0x00321EE2 File Offset: 0x003200E2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BooleanVariantValue>(deep);
		}

		// Token: 0x06015AD8 RID: 88792 RVA: 0x00321EEC File Offset: 0x003200EC
		// Note: this type is marked as 'beforefieldinit'.
		static BooleanVariantValue()
		{
			byte[] array = new byte[1];
			BooleanVariantValue.attributeNamespaceIds = array;
		}

		// Token: 0x04009453 RID: 37971
		private const string tagName = "boolVal";

		// Token: 0x04009454 RID: 37972
		private const byte tagNsId = 24;

		// Token: 0x04009455 RID: 37973
		internal const int ElementTypeIdConst = 12217;

		// Token: 0x04009456 RID: 37974
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04009457 RID: 37975
		private static byte[] attributeNamespaceIds;
	}
}
