using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200258E RID: 9614
	[GeneratedCode("DomGen", "2.0")]
	internal class HeightPercent : OpenXmlLeafElement
	{
		// Token: 0x1700566C RID: 22124
		// (get) Token: 0x06011F57 RID: 73559 RVA: 0x002F41A7 File Offset: 0x002F23A7
		public override string LocalName
		{
			get
			{
				return "hPercent";
			}
		}

		// Token: 0x1700566D RID: 22125
		// (get) Token: 0x06011F58 RID: 73560 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700566E RID: 22126
		// (get) Token: 0x06011F59 RID: 73561 RVA: 0x002F41AE File Offset: 0x002F23AE
		internal override int ElementTypeId
		{
			get
			{
				return 10418;
			}
		}

		// Token: 0x06011F5A RID: 73562 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700566F RID: 22127
		// (get) Token: 0x06011F5B RID: 73563 RVA: 0x002F41B5 File Offset: 0x002F23B5
		internal override string[] AttributeTagNames
		{
			get
			{
				return HeightPercent.attributeTagNames;
			}
		}

		// Token: 0x17005670 RID: 22128
		// (get) Token: 0x06011F5C RID: 73564 RVA: 0x002F41BC File Offset: 0x002F23BC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return HeightPercent.attributeNamespaceIds;
			}
		}

		// Token: 0x17005671 RID: 22129
		// (get) Token: 0x06011F5D RID: 73565 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x06011F5E RID: 73566 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06011F60 RID: 73568 RVA: 0x002F41C3 File Offset: 0x002F23C3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011F61 RID: 73569 RVA: 0x002F41E3 File Offset: 0x002F23E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HeightPercent>(deep);
		}

		// Token: 0x06011F62 RID: 73570 RVA: 0x002F41EC File Offset: 0x002F23EC
		// Note: this type is marked as 'beforefieldinit'.
		static HeightPercent()
		{
			byte[] array = new byte[1];
			HeightPercent.attributeNamespaceIds = array;
		}

		// Token: 0x04007D70 RID: 32112
		private const string tagName = "hPercent";

		// Token: 0x04007D71 RID: 32113
		private const byte tagNsId = 11;

		// Token: 0x04007D72 RID: 32114
		internal const int ElementTypeIdConst = 10418;

		// Token: 0x04007D73 RID: 32115
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007D74 RID: 32116
		private static byte[] attributeNamespaceIds;
	}
}
