using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FDA RID: 12250
	[GeneratedCode("DomGen", "2.0")]
	internal class View : OpenXmlLeafElement
	{
		// Token: 0x1700948E RID: 38030
		// (get) Token: 0x0601A9B7 RID: 108983 RVA: 0x00364D56 File Offset: 0x00362F56
		public override string LocalName
		{
			get
			{
				return "view";
			}
		}

		// Token: 0x1700948F RID: 38031
		// (get) Token: 0x0601A9B8 RID: 108984 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009490 RID: 38032
		// (get) Token: 0x0601A9B9 RID: 108985 RVA: 0x00364D5D File Offset: 0x00362F5D
		internal override int ElementTypeId
		{
			get
			{
				return 11959;
			}
		}

		// Token: 0x0601A9BA RID: 108986 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009491 RID: 38033
		// (get) Token: 0x0601A9BB RID: 108987 RVA: 0x00364D64 File Offset: 0x00362F64
		internal override string[] AttributeTagNames
		{
			get
			{
				return View.attributeTagNames;
			}
		}

		// Token: 0x17009492 RID: 38034
		// (get) Token: 0x0601A9BC RID: 108988 RVA: 0x00364D6B File Offset: 0x00362F6B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return View.attributeNamespaceIds;
			}
		}

		// Token: 0x17009493 RID: 38035
		// (get) Token: 0x0601A9BD RID: 108989 RVA: 0x00364D72 File Offset: 0x00362F72
		// (set) Token: 0x0601A9BE RID: 108990 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<ViewValues> Val
		{
			get
			{
				return (EnumValue<ViewValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A9C0 RID: 108992 RVA: 0x00364D81 File Offset: 0x00362F81
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<ViewValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A9C1 RID: 108993 RVA: 0x00364DA3 File Offset: 0x00362FA3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<View>(deep);
		}

		// Token: 0x0400ADB1 RID: 44465
		private const string tagName = "view";

		// Token: 0x0400ADB2 RID: 44466
		private const byte tagNsId = 23;

		// Token: 0x0400ADB3 RID: 44467
		internal const int ElementTypeIdConst = 11959;

		// Token: 0x0400ADB4 RID: 44468
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADB5 RID: 44469
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
