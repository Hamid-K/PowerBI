using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026FA RID: 9978
	[GeneratedCode("DomGen", "2.0")]
	internal class FlatText : OpenXmlLeafElement
	{
		// Token: 0x17005E3F RID: 24127
		// (get) Token: 0x060130C7 RID: 78023 RVA: 0x00302F82 File Offset: 0x00301182
		public override string LocalName
		{
			get
			{
				return "flatTx";
			}
		}

		// Token: 0x17005E40 RID: 24128
		// (get) Token: 0x060130C8 RID: 78024 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E41 RID: 24129
		// (get) Token: 0x060130C9 RID: 78025 RVA: 0x00302F89 File Offset: 0x00301189
		internal override int ElementTypeId
		{
			get
			{
				return 10042;
			}
		}

		// Token: 0x060130CA RID: 78026 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E42 RID: 24130
		// (get) Token: 0x060130CB RID: 78027 RVA: 0x00302F90 File Offset: 0x00301190
		internal override string[] AttributeTagNames
		{
			get
			{
				return FlatText.attributeTagNames;
			}
		}

		// Token: 0x17005E43 RID: 24131
		// (get) Token: 0x060130CC RID: 78028 RVA: 0x00302F97 File Offset: 0x00301197
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FlatText.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E44 RID: 24132
		// (get) Token: 0x060130CD RID: 78029 RVA: 0x002E0CB4 File Offset: 0x002DEEB4
		// (set) Token: 0x060130CE RID: 78030 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "z")]
		public Int64Value Z
		{
			get
			{
				return (Int64Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060130D0 RID: 78032 RVA: 0x00302F9E File Offset: 0x0030119E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "z" == name)
			{
				return new Int64Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060130D1 RID: 78033 RVA: 0x00302FBE File Offset: 0x003011BE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FlatText>(deep);
		}

		// Token: 0x060130D2 RID: 78034 RVA: 0x00302FC8 File Offset: 0x003011C8
		// Note: this type is marked as 'beforefieldinit'.
		static FlatText()
		{
			byte[] array = new byte[1];
			FlatText.attributeNamespaceIds = array;
		}

		// Token: 0x0400846A RID: 33898
		private const string tagName = "flatTx";

		// Token: 0x0400846B RID: 33899
		private const byte tagNsId = 10;

		// Token: 0x0400846C RID: 33900
		internal const int ElementTypeIdConst = 10042;

		// Token: 0x0400846D RID: 33901
		private static string[] attributeTagNames = new string[] { "z" };

		// Token: 0x0400846E RID: 33902
		private static byte[] attributeNamespaceIds;
	}
}
