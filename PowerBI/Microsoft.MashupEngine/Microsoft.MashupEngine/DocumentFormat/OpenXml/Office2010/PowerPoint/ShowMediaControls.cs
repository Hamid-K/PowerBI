using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023B3 RID: 9139
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ShowMediaControls : OpenXmlLeafElement
	{
		// Token: 0x17004C6D RID: 19565
		// (get) Token: 0x060108E4 RID: 67812 RVA: 0x002E4B3B File Offset: 0x002E2D3B
		public override string LocalName
		{
			get
			{
				return "showMediaCtrls";
			}
		}

		// Token: 0x17004C6E RID: 19566
		// (get) Token: 0x060108E5 RID: 67813 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C6F RID: 19567
		// (get) Token: 0x060108E6 RID: 67814 RVA: 0x002E4B42 File Offset: 0x002E2D42
		internal override int ElementTypeId
		{
			get
			{
				return 12794;
			}
		}

		// Token: 0x060108E7 RID: 67815 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C70 RID: 19568
		// (get) Token: 0x060108E8 RID: 67816 RVA: 0x002E4B49 File Offset: 0x002E2D49
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShowMediaControls.attributeTagNames;
			}
		}

		// Token: 0x17004C71 RID: 19569
		// (get) Token: 0x060108E9 RID: 67817 RVA: 0x002E4B50 File Offset: 0x002E2D50
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShowMediaControls.attributeNamespaceIds;
			}
		}

		// Token: 0x17004C72 RID: 19570
		// (get) Token: 0x060108EA RID: 67818 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060108EB RID: 67819 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x060108ED RID: 67821 RVA: 0x002DE6BC File Offset: 0x002DC8BC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060108EE RID: 67822 RVA: 0x002E4B57 File Offset: 0x002E2D57
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowMediaControls>(deep);
		}

		// Token: 0x060108EF RID: 67823 RVA: 0x002E4B60 File Offset: 0x002E2D60
		// Note: this type is marked as 'beforefieldinit'.
		static ShowMediaControls()
		{
			byte[] array = new byte[1];
			ShowMediaControls.attributeNamespaceIds = array;
		}

		// Token: 0x0400753B RID: 30011
		private const string tagName = "showMediaCtrls";

		// Token: 0x0400753C RID: 30012
		private const byte tagNsId = 49;

		// Token: 0x0400753D RID: 30013
		internal const int ElementTypeIdConst = 12794;

		// Token: 0x0400753E RID: 30014
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400753F RID: 30015
		private static byte[] attributeNamespaceIds;
	}
}
