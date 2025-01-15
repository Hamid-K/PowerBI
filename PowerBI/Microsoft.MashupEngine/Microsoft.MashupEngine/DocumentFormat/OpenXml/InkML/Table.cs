using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003088 RID: 12424
	[GeneratedCode("DomGen", "2.0")]
	internal class Table : OpenXmlLeafTextElement
	{
		// Token: 0x17009760 RID: 38752
		// (get) Token: 0x0601AFDD RID: 110557 RVA: 0x00049581 File Offset: 0x00047781
		public override string LocalName
		{
			get
			{
				return "table";
			}
		}

		// Token: 0x17009761 RID: 38753
		// (get) Token: 0x0601AFDE RID: 110558 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009762 RID: 38754
		// (get) Token: 0x0601AFDF RID: 110559 RVA: 0x0036A6B7 File Offset: 0x003688B7
		internal override int ElementTypeId
		{
			get
			{
				return 12645;
			}
		}

		// Token: 0x0601AFE0 RID: 110560 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009763 RID: 38755
		// (get) Token: 0x0601AFE1 RID: 110561 RVA: 0x0036A6BE File Offset: 0x003688BE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Table.attributeTagNames;
			}
		}

		// Token: 0x17009764 RID: 38756
		// (get) Token: 0x0601AFE2 RID: 110562 RVA: 0x0036A6C5 File Offset: 0x003688C5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Table.attributeNamespaceIds;
			}
		}

		// Token: 0x17009765 RID: 38757
		// (get) Token: 0x0601AFE3 RID: 110563 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AFE4 RID: 110564 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "id")]
		public StringValue Id
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

		// Token: 0x17009766 RID: 38758
		// (get) Token: 0x0601AFE5 RID: 110565 RVA: 0x0036A6CC File Offset: 0x003688CC
		// (set) Token: 0x0601AFE6 RID: 110566 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "apply")]
		public EnumValue<TableApplyValues> Apply
		{
			get
			{
				return (EnumValue<TableApplyValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009767 RID: 38759
		// (get) Token: 0x0601AFE7 RID: 110567 RVA: 0x0036A6DB File Offset: 0x003688DB
		// (set) Token: 0x0601AFE8 RID: 110568 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "interpolation")]
		public EnumValue<TableInterpolationValues> Interpolation
		{
			get
			{
				return (EnumValue<TableInterpolationValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601AFE9 RID: 110569 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Table()
		{
		}

		// Token: 0x0601AFEA RID: 110570 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Table(string text)
			: base(text)
		{
		}

		// Token: 0x0601AFEB RID: 110571 RVA: 0x0036A6EC File Offset: 0x003688EC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601AFEC RID: 110572 RVA: 0x0036A708 File Offset: 0x00368908
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "apply" == name)
			{
				return new EnumValue<TableApplyValues>();
			}
			if (namespaceId == 0 && "interpolation" == name)
			{
				return new EnumValue<TableInterpolationValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AFED RID: 110573 RVA: 0x0036A760 File Offset: 0x00368960
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Table>(deep);
		}

		// Token: 0x0601AFEE RID: 110574 RVA: 0x0036A76C File Offset: 0x0036896C
		// Note: this type is marked as 'beforefieldinit'.
		static Table()
		{
			byte[] array = new byte[3];
			array[0] = 1;
			Table.attributeNamespaceIds = array;
		}

		// Token: 0x0400B277 RID: 45687
		private const string tagName = "table";

		// Token: 0x0400B278 RID: 45688
		private const byte tagNsId = 43;

		// Token: 0x0400B279 RID: 45689
		internal const int ElementTypeIdConst = 12645;

		// Token: 0x0400B27A RID: 45690
		private static string[] attributeTagNames = new string[] { "id", "apply", "interpolation" };

		// Token: 0x0400B27B RID: 45691
		private static byte[] attributeNamespaceIds;
	}
}
