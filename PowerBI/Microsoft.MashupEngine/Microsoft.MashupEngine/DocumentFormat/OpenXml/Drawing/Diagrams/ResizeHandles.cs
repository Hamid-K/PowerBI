using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002688 RID: 9864
	[GeneratedCode("DomGen", "2.0")]
	internal class ResizeHandles : OpenXmlLeafElement
	{
		// Token: 0x17005CD1 RID: 23761
		// (get) Token: 0x06012DAD RID: 77229 RVA: 0x00300133 File Offset: 0x002FE333
		public override string LocalName
		{
			get
			{
				return "resizeHandles";
			}
		}

		// Token: 0x17005CD2 RID: 23762
		// (get) Token: 0x06012DAE RID: 77230 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CD3 RID: 23763
		// (get) Token: 0x06012DAF RID: 77231 RVA: 0x0030013A File Offset: 0x002FE33A
		internal override int ElementTypeId
		{
			get
			{
				return 10679;
			}
		}

		// Token: 0x06012DB0 RID: 77232 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CD4 RID: 23764
		// (get) Token: 0x06012DB1 RID: 77233 RVA: 0x00300141 File Offset: 0x002FE341
		internal override string[] AttributeTagNames
		{
			get
			{
				return ResizeHandles.attributeTagNames;
			}
		}

		// Token: 0x17005CD5 RID: 23765
		// (get) Token: 0x06012DB2 RID: 77234 RVA: 0x00300148 File Offset: 0x002FE348
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ResizeHandles.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CD6 RID: 23766
		// (get) Token: 0x06012DB3 RID: 77235 RVA: 0x0030014F File Offset: 0x002FE34F
		// (set) Token: 0x06012DB4 RID: 77236 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<ResizeHandlesStringValues> Val
		{
			get
			{
				return (EnumValue<ResizeHandlesStringValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012DB6 RID: 77238 RVA: 0x0030015E File Offset: 0x002FE35E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<ResizeHandlesStringValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012DB7 RID: 77239 RVA: 0x0030017E File Offset: 0x002FE37E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ResizeHandles>(deep);
		}

		// Token: 0x06012DB8 RID: 77240 RVA: 0x00300188 File Offset: 0x002FE388
		// Note: this type is marked as 'beforefieldinit'.
		static ResizeHandles()
		{
			byte[] array = new byte[1];
			ResizeHandles.attributeNamespaceIds = array;
		}

		// Token: 0x040081EE RID: 33262
		private const string tagName = "resizeHandles";

		// Token: 0x040081EF RID: 33263
		private const byte tagNsId = 14;

		// Token: 0x040081F0 RID: 33264
		internal const int ElementTypeIdConst = 10679;

		// Token: 0x040081F1 RID: 33265
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081F2 RID: 33266
		private static byte[] attributeNamespaceIds;
	}
}
