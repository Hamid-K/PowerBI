using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200258D RID: 9613
	[GeneratedCode("DomGen", "2.0")]
	internal class RotateX : OpenXmlLeafElement
	{
		// Token: 0x17005666 RID: 22118
		// (get) Token: 0x06011F4B RID: 73547 RVA: 0x002F4123 File Offset: 0x002F2323
		public override string LocalName
		{
			get
			{
				return "rotX";
			}
		}

		// Token: 0x17005667 RID: 22119
		// (get) Token: 0x06011F4C RID: 73548 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005668 RID: 22120
		// (get) Token: 0x06011F4D RID: 73549 RVA: 0x002F412A File Offset: 0x002F232A
		internal override int ElementTypeId
		{
			get
			{
				return 10417;
			}
		}

		// Token: 0x06011F4E RID: 73550 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005669 RID: 22121
		// (get) Token: 0x06011F4F RID: 73551 RVA: 0x002F4131 File Offset: 0x002F2331
		internal override string[] AttributeTagNames
		{
			get
			{
				return RotateX.attributeTagNames;
			}
		}

		// Token: 0x1700566A RID: 22122
		// (get) Token: 0x06011F50 RID: 73552 RVA: 0x002F4138 File Offset: 0x002F2338
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RotateX.attributeNamespaceIds;
			}
		}

		// Token: 0x1700566B RID: 22123
		// (get) Token: 0x06011F51 RID: 73553 RVA: 0x002F413F File Offset: 0x002F233F
		// (set) Token: 0x06011F52 RID: 73554 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public SByteValue Val
		{
			get
			{
				return (SByteValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011F54 RID: 73556 RVA: 0x002F414E File Offset: 0x002F234E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new SByteValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011F55 RID: 73557 RVA: 0x002F416E File Offset: 0x002F236E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RotateX>(deep);
		}

		// Token: 0x06011F56 RID: 73558 RVA: 0x002F4178 File Offset: 0x002F2378
		// Note: this type is marked as 'beforefieldinit'.
		static RotateX()
		{
			byte[] array = new byte[1];
			RotateX.attributeNamespaceIds = array;
		}

		// Token: 0x04007D6B RID: 32107
		private const string tagName = "rotX";

		// Token: 0x04007D6C RID: 32108
		private const byte tagNsId = 11;

		// Token: 0x04007D6D RID: 32109
		internal const int ElementTypeIdConst = 10417;

		// Token: 0x04007D6E RID: 32110
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D6F RID: 32111
		private static byte[] attributeNamespaceIds;
	}
}
