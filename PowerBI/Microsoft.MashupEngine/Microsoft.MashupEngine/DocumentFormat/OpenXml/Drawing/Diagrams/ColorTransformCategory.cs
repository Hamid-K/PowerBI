using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002651 RID: 9809
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorTransformCategory : OpenXmlLeafElement
	{
		// Token: 0x17005B4C RID: 23372
		// (get) Token: 0x06012A2F RID: 76335 RVA: 0x002F7174 File Offset: 0x002F5374
		public override string LocalName
		{
			get
			{
				return "cat";
			}
		}

		// Token: 0x17005B4D RID: 23373
		// (get) Token: 0x06012A30 RID: 76336 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B4E RID: 23374
		// (get) Token: 0x06012A31 RID: 76337 RVA: 0x002FD87D File Offset: 0x002FBA7D
		internal override int ElementTypeId
		{
			get
			{
				return 10627;
			}
		}

		// Token: 0x06012A32 RID: 76338 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B4F RID: 23375
		// (get) Token: 0x06012A33 RID: 76339 RVA: 0x002FD884 File Offset: 0x002FBA84
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorTransformCategory.attributeTagNames;
			}
		}

		// Token: 0x17005B50 RID: 23376
		// (get) Token: 0x06012A34 RID: 76340 RVA: 0x002FD88B File Offset: 0x002FBA8B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorTransformCategory.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B51 RID: 23377
		// (get) Token: 0x06012A35 RID: 76341 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012A36 RID: 76342 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public StringValue Type
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

		// Token: 0x17005B52 RID: 23378
		// (get) Token: 0x06012A37 RID: 76343 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06012A38 RID: 76344 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pri")]
		public UInt32Value Priority
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012A3A RID: 76346 RVA: 0x002FD892 File Offset: 0x002FBA92
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "pri" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012A3B RID: 76347 RVA: 0x002FD8C8 File Offset: 0x002FBAC8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorTransformCategory>(deep);
		}

		// Token: 0x06012A3C RID: 76348 RVA: 0x002FD8D4 File Offset: 0x002FBAD4
		// Note: this type is marked as 'beforefieldinit'.
		static ColorTransformCategory()
		{
			byte[] array = new byte[2];
			ColorTransformCategory.attributeNamespaceIds = array;
		}

		// Token: 0x040080FC RID: 33020
		private const string tagName = "cat";

		// Token: 0x040080FD RID: 33021
		private const byte tagNsId = 14;

		// Token: 0x040080FE RID: 33022
		internal const int ElementTypeIdConst = 10627;

		// Token: 0x040080FF RID: 33023
		private static string[] attributeTagNames = new string[] { "type", "pri" };

		// Token: 0x04008100 RID: 33024
		private static byte[] attributeNamespaceIds;
	}
}
