using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025FE RID: 9726
	[GeneratedCode("DomGen", "2.0")]
	internal class EditingLanguage : OpenXmlLeafElement
	{
		// Token: 0x17005982 RID: 22914
		// (get) Token: 0x06012621 RID: 75297 RVA: 0x002FA5A7 File Offset: 0x002F87A7
		public override string LocalName
		{
			get
			{
				return "lang";
			}
		}

		// Token: 0x17005983 RID: 22915
		// (get) Token: 0x06012622 RID: 75298 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005984 RID: 22916
		// (get) Token: 0x06012623 RID: 75299 RVA: 0x002FA5AE File Offset: 0x002F87AE
		internal override int ElementTypeId
		{
			get
			{
				return 10572;
			}
		}

		// Token: 0x06012624 RID: 75300 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005985 RID: 22917
		// (get) Token: 0x06012625 RID: 75301 RVA: 0x002FA5B5 File Offset: 0x002F87B5
		internal override string[] AttributeTagNames
		{
			get
			{
				return EditingLanguage.attributeTagNames;
			}
		}

		// Token: 0x17005986 RID: 22918
		// (get) Token: 0x06012626 RID: 75302 RVA: 0x002FA5BC File Offset: 0x002F87BC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return EditingLanguage.attributeNamespaceIds;
			}
		}

		// Token: 0x17005987 RID: 22919
		// (get) Token: 0x06012627 RID: 75303 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012628 RID: 75304 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601262A RID: 75306 RVA: 0x002E6B2F File Offset: 0x002E4D2F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601262B RID: 75307 RVA: 0x002FA5C3 File Offset: 0x002F87C3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EditingLanguage>(deep);
		}

		// Token: 0x0601262C RID: 75308 RVA: 0x002FA5CC File Offset: 0x002F87CC
		// Note: this type is marked as 'beforefieldinit'.
		static EditingLanguage()
		{
			byte[] array = new byte[1];
			EditingLanguage.attributeNamespaceIds = array;
		}

		// Token: 0x04007F5A RID: 32602
		private const string tagName = "lang";

		// Token: 0x04007F5B RID: 32603
		private const byte tagNsId = 11;

		// Token: 0x04007F5C RID: 32604
		internal const int ElementTypeIdConst = 10572;

		// Token: 0x04007F5D RID: 32605
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x04007F5E RID: 32606
		private static byte[] attributeNamespaceIds;
	}
}
