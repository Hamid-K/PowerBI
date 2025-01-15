using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200258F RID: 9615
	[GeneratedCode("DomGen", "2.0")]
	internal class RotateY : OpenXmlLeafElement
	{
		// Token: 0x17005672 RID: 22130
		// (get) Token: 0x06011F63 RID: 73571 RVA: 0x002F421B File Offset: 0x002F241B
		public override string LocalName
		{
			get
			{
				return "rotY";
			}
		}

		// Token: 0x17005673 RID: 22131
		// (get) Token: 0x06011F64 RID: 73572 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005674 RID: 22132
		// (get) Token: 0x06011F65 RID: 73573 RVA: 0x002F4222 File Offset: 0x002F2422
		internal override int ElementTypeId
		{
			get
			{
				return 10419;
			}
		}

		// Token: 0x06011F66 RID: 73574 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005675 RID: 22133
		// (get) Token: 0x06011F67 RID: 73575 RVA: 0x002F4229 File Offset: 0x002F2429
		internal override string[] AttributeTagNames
		{
			get
			{
				return RotateY.attributeTagNames;
			}
		}

		// Token: 0x17005676 RID: 22134
		// (get) Token: 0x06011F68 RID: 73576 RVA: 0x002F4230 File Offset: 0x002F2430
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RotateY.attributeNamespaceIds;
			}
		}

		// Token: 0x17005677 RID: 22135
		// (get) Token: 0x06011F69 RID: 73577 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x06011F6A RID: 73578 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011F6C RID: 73580 RVA: 0x002F41C3 File Offset: 0x002F23C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011F6D RID: 73581 RVA: 0x002F4237 File Offset: 0x002F2437
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RotateY>(deep);
		}

		// Token: 0x06011F6E RID: 73582 RVA: 0x002F4240 File Offset: 0x002F2440
		// Note: this type is marked as 'beforefieldinit'.
		static RotateY()
		{
			byte[] array = new byte[1];
			RotateY.attributeNamespaceIds = array;
		}

		// Token: 0x04007D75 RID: 32117
		private const string tagName = "rotY";

		// Token: 0x04007D76 RID: 32118
		private const byte tagNsId = 11;

		// Token: 0x04007D77 RID: 32119
		internal const int ElementTypeIdConst = 10419;

		// Token: 0x04007D78 RID: 32120
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D79 RID: 32121
		private static byte[] attributeNamespaceIds;
	}
}
