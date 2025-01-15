using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002682 RID: 9858
	[GeneratedCode("DomGen", "2.0")]
	internal class PreferredNumberOfChildren : OpenXmlLeafElement
	{
		// Token: 0x17005CAD RID: 23725
		// (get) Token: 0x06012D65 RID: 77157 RVA: 0x002FFE7B File Offset: 0x002FE07B
		public override string LocalName
		{
			get
			{
				return "chPref";
			}
		}

		// Token: 0x17005CAE RID: 23726
		// (get) Token: 0x06012D66 RID: 77158 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005CAF RID: 23727
		// (get) Token: 0x06012D67 RID: 77159 RVA: 0x002FFE82 File Offset: 0x002FE082
		internal override int ElementTypeId
		{
			get
			{
				return 10673;
			}
		}

		// Token: 0x06012D68 RID: 77160 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005CB0 RID: 23728
		// (get) Token: 0x06012D69 RID: 77161 RVA: 0x002FFE89 File Offset: 0x002FE089
		internal override string[] AttributeTagNames
		{
			get
			{
				return PreferredNumberOfChildren.attributeTagNames;
			}
		}

		// Token: 0x17005CB1 RID: 23729
		// (get) Token: 0x06012D6A RID: 77162 RVA: 0x002FFE90 File Offset: 0x002FE090
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PreferredNumberOfChildren.attributeNamespaceIds;
			}
		}

		// Token: 0x17005CB2 RID: 23730
		// (get) Token: 0x06012D6B RID: 77163 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06012D6C RID: 77164 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06012D6E RID: 77166 RVA: 0x002F5715 File Offset: 0x002F3915
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D6F RID: 77167 RVA: 0x002FFE97 File Offset: 0x002FE097
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PreferredNumberOfChildren>(deep);
		}

		// Token: 0x06012D70 RID: 77168 RVA: 0x002FFEA0 File Offset: 0x002FE0A0
		// Note: this type is marked as 'beforefieldinit'.
		static PreferredNumberOfChildren()
		{
			byte[] array = new byte[1];
			PreferredNumberOfChildren.attributeNamespaceIds = array;
		}

		// Token: 0x040081D0 RID: 33232
		private const string tagName = "chPref";

		// Token: 0x040081D1 RID: 33233
		private const byte tagNsId = 14;

		// Token: 0x040081D2 RID: 33234
		internal const int ElementTypeIdConst = 10673;

		// Token: 0x040081D3 RID: 33235
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x040081D4 RID: 33236
		private static byte[] attributeNamespaceIds;
	}
}
