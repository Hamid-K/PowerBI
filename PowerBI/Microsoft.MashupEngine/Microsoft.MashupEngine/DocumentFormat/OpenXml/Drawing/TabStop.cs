using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002823 RID: 10275
	[GeneratedCode("DomGen", "2.0")]
	internal class TabStop : OpenXmlLeafElement
	{
		// Token: 0x170065C2 RID: 26050
		// (get) Token: 0x060141E3 RID: 82403 RVA: 0x002D001B File Offset: 0x002CE21B
		public override string LocalName
		{
			get
			{
				return "tab";
			}
		}

		// Token: 0x170065C3 RID: 26051
		// (get) Token: 0x060141E4 RID: 82404 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170065C4 RID: 26052
		// (get) Token: 0x060141E5 RID: 82405 RVA: 0x0030F8A4 File Offset: 0x0030DAA4
		internal override int ElementTypeId
		{
			get
			{
				return 10307;
			}
		}

		// Token: 0x060141E6 RID: 82406 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170065C5 RID: 26053
		// (get) Token: 0x060141E7 RID: 82407 RVA: 0x0030F8AB File Offset: 0x0030DAAB
		internal override string[] AttributeTagNames
		{
			get
			{
				return TabStop.attributeTagNames;
			}
		}

		// Token: 0x170065C6 RID: 26054
		// (get) Token: 0x060141E8 RID: 82408 RVA: 0x0030F8B2 File Offset: 0x0030DAB2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TabStop.attributeNamespaceIds;
			}
		}

		// Token: 0x170065C7 RID: 26055
		// (get) Token: 0x060141E9 RID: 82409 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060141EA RID: 82410 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "pos")]
		public Int32Value Position
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

		// Token: 0x170065C8 RID: 26056
		// (get) Token: 0x060141EB RID: 82411 RVA: 0x0030F8B9 File Offset: 0x0030DAB9
		// (set) Token: 0x060141EC RID: 82412 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "algn")]
		public EnumValue<TextTabAlignmentValues> Alignment
		{
			get
			{
				return (EnumValue<TextTabAlignmentValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060141EE RID: 82414 RVA: 0x0030F8C8 File Offset: 0x0030DAC8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "pos" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "algn" == name)
			{
				return new EnumValue<TextTabAlignmentValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060141EF RID: 82415 RVA: 0x0030F8FE File Offset: 0x0030DAFE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TabStop>(deep);
		}

		// Token: 0x060141F0 RID: 82416 RVA: 0x0030F908 File Offset: 0x0030DB08
		// Note: this type is marked as 'beforefieldinit'.
		static TabStop()
		{
			byte[] array = new byte[2];
			TabStop.attributeNamespaceIds = array;
		}

		// Token: 0x04008916 RID: 35094
		private const string tagName = "tab";

		// Token: 0x04008917 RID: 35095
		private const byte tagNsId = 10;

		// Token: 0x04008918 RID: 35096
		internal const int ElementTypeIdConst = 10307;

		// Token: 0x04008919 RID: 35097
		private static string[] attributeTagNames = new string[] { "pos", "algn" };

		// Token: 0x0400891A RID: 35098
		private static byte[] attributeNamespaceIds;
	}
}
