using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025B6 RID: 9654
	[GeneratedCode("DomGen", "2.0")]
	internal class Orientation : OpenXmlLeafElement
	{
		// Token: 0x17005752 RID: 22354
		// (get) Token: 0x06012151 RID: 74065 RVA: 0x002F55AB File Offset: 0x002F37AB
		public override string LocalName
		{
			get
			{
				return "orientation";
			}
		}

		// Token: 0x17005753 RID: 22355
		// (get) Token: 0x06012152 RID: 74066 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005754 RID: 22356
		// (get) Token: 0x06012153 RID: 74067 RVA: 0x002F55B2 File Offset: 0x002F37B2
		internal override int ElementTypeId
		{
			get
			{
				return 10478;
			}
		}

		// Token: 0x06012154 RID: 74068 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005755 RID: 22357
		// (get) Token: 0x06012155 RID: 74069 RVA: 0x002F55B9 File Offset: 0x002F37B9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Orientation.attributeTagNames;
			}
		}

		// Token: 0x17005756 RID: 22358
		// (get) Token: 0x06012156 RID: 74070 RVA: 0x002F55C0 File Offset: 0x002F37C0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Orientation.attributeNamespaceIds;
			}
		}

		// Token: 0x17005757 RID: 22359
		// (get) Token: 0x06012157 RID: 74071 RVA: 0x002F55C7 File Offset: 0x002F37C7
		// (set) Token: 0x06012158 RID: 74072 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public EnumValue<OrientationValues> Val
		{
			get
			{
				return (EnumValue<OrientationValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601215A RID: 74074 RVA: 0x002F55D6 File Offset: 0x002F37D6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new EnumValue<OrientationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601215B RID: 74075 RVA: 0x002F55F6 File Offset: 0x002F37F6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Orientation>(deep);
		}

		// Token: 0x0601215C RID: 74076 RVA: 0x002F5600 File Offset: 0x002F3800
		// Note: this type is marked as 'beforefieldinit'.
		static Orientation()
		{
			byte[] array = new byte[1];
			Orientation.attributeNamespaceIds = array;
		}

		// Token: 0x04007E1B RID: 32283
		private const string tagName = "orientation";

		// Token: 0x04007E1C RID: 32284
		private const byte tagNsId = 11;

		// Token: 0x04007E1D RID: 32285
		internal const int ElementTypeIdConst = 10478;

		// Token: 0x04007E1E RID: 32286
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007E1F RID: 32287
		private static byte[] attributeNamespaceIds;
	}
}
