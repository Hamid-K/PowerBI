using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002ADD RID: 10973
	[GeneratedCode("DomGen", "2.0")]
	internal class ZoomTransition : OpenXmlLeafElement
	{
		// Token: 0x17007597 RID: 30103
		// (get) Token: 0x060165CC RID: 91596 RVA: 0x003294CF File Offset: 0x003276CF
		public override string LocalName
		{
			get
			{
				return "zoom";
			}
		}

		// Token: 0x17007598 RID: 30104
		// (get) Token: 0x060165CD RID: 91597 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007599 RID: 30105
		// (get) Token: 0x060165CE RID: 91598 RVA: 0x003294D6 File Offset: 0x003276D6
		internal override int ElementTypeId
		{
			get
			{
				return 12395;
			}
		}

		// Token: 0x060165CF RID: 91599 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700759A RID: 30106
		// (get) Token: 0x060165D0 RID: 91600 RVA: 0x003294DD File Offset: 0x003276DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return ZoomTransition.attributeTagNames;
			}
		}

		// Token: 0x1700759B RID: 30107
		// (get) Token: 0x060165D1 RID: 91601 RVA: 0x003294E4 File Offset: 0x003276E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ZoomTransition.attributeNamespaceIds;
			}
		}

		// Token: 0x1700759C RID: 30108
		// (get) Token: 0x060165D2 RID: 91602 RVA: 0x002E44A7 File Offset: 0x002E26A7
		// (set) Token: 0x060165D3 RID: 91603 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "dir")]
		public EnumValue<TransitionInOutDirectionValues> Direction
		{
			get
			{
				return (EnumValue<TransitionInOutDirectionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060165D5 RID: 91605 RVA: 0x002E44B6 File Offset: 0x002E26B6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "dir" == name)
			{
				return new EnumValue<TransitionInOutDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060165D6 RID: 91606 RVA: 0x003294EB File Offset: 0x003276EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ZoomTransition>(deep);
		}

		// Token: 0x060165D7 RID: 91607 RVA: 0x003294F4 File Offset: 0x003276F4
		// Note: this type is marked as 'beforefieldinit'.
		static ZoomTransition()
		{
			byte[] array = new byte[1];
			ZoomTransition.attributeNamespaceIds = array;
		}

		// Token: 0x04009774 RID: 38772
		private const string tagName = "zoom";

		// Token: 0x04009775 RID: 38773
		private const byte tagNsId = 24;

		// Token: 0x04009776 RID: 38774
		internal const int ElementTypeIdConst = 12395;

		// Token: 0x04009777 RID: 38775
		private static string[] attributeTagNames = new string[] { "dir" };

		// Token: 0x04009778 RID: 38776
		private static byte[] attributeNamespaceIds;
	}
}
