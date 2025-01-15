using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002721 RID: 10017
	[GeneratedCode("DomGen", "2.0")]
	internal class RelativeOffset : OpenXmlLeafElement
	{
		// Token: 0x17005F96 RID: 24470
		// (get) Token: 0x0601339B RID: 78747 RVA: 0x0030518A File Offset: 0x0030338A
		public override string LocalName
		{
			get
			{
				return "relOff";
			}
		}

		// Token: 0x17005F97 RID: 24471
		// (get) Token: 0x0601339C RID: 78748 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005F98 RID: 24472
		// (get) Token: 0x0601339D RID: 78749 RVA: 0x00305191 File Offset: 0x00303391
		internal override int ElementTypeId
		{
			get
			{
				return 10079;
			}
		}

		// Token: 0x0601339E RID: 78750 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005F99 RID: 24473
		// (get) Token: 0x0601339F RID: 78751 RVA: 0x00305198 File Offset: 0x00303398
		internal override string[] AttributeTagNames
		{
			get
			{
				return RelativeOffset.attributeTagNames;
			}
		}

		// Token: 0x17005F9A RID: 24474
		// (get) Token: 0x060133A0 RID: 78752 RVA: 0x0030519F File Offset: 0x0030339F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RelativeOffset.attributeNamespaceIds;
			}
		}

		// Token: 0x17005F9B RID: 24475
		// (get) Token: 0x060133A1 RID: 78753 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060133A2 RID: 78754 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "tx")]
		public Int32Value OffsetX
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

		// Token: 0x17005F9C RID: 24476
		// (get) Token: 0x060133A3 RID: 78755 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060133A4 RID: 78756 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "ty")]
		public Int32Value OffsetY
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060133A6 RID: 78758 RVA: 0x003051A6 File Offset: 0x003033A6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "tx" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "ty" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060133A7 RID: 78759 RVA: 0x003051DC File Offset: 0x003033DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RelativeOffset>(deep);
		}

		// Token: 0x060133A8 RID: 78760 RVA: 0x003051E8 File Offset: 0x003033E8
		// Note: this type is marked as 'beforefieldinit'.
		static RelativeOffset()
		{
			byte[] array = new byte[2];
			RelativeOffset.attributeNamespaceIds = array;
		}

		// Token: 0x04008530 RID: 34096
		private const string tagName = "relOff";

		// Token: 0x04008531 RID: 34097
		private const byte tagNsId = 10;

		// Token: 0x04008532 RID: 34098
		internal const int ElementTypeIdConst = 10079;

		// Token: 0x04008533 RID: 34099
		private static string[] attributeTagNames = new string[] { "tx", "ty" };

		// Token: 0x04008534 RID: 34100
		private static byte[] attributeNamespaceIds;
	}
}
