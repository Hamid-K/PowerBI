using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B78 RID: 11128
	[ChildElementInfo(typeof(PivotArea))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Format : OpenXmlCompositeElement
	{
		// Token: 0x170079ED RID: 31213
		// (get) Token: 0x06016FA1 RID: 94113 RVA: 0x003314EB File Offset: 0x0032F6EB
		public override string LocalName
		{
			get
			{
				return "format";
			}
		}

		// Token: 0x170079EE RID: 31214
		// (get) Token: 0x06016FA2 RID: 94114 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170079EF RID: 31215
		// (get) Token: 0x06016FA3 RID: 94115 RVA: 0x003314F2 File Offset: 0x0032F6F2
		internal override int ElementTypeId
		{
			get
			{
				return 11108;
			}
		}

		// Token: 0x06016FA4 RID: 94116 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170079F0 RID: 31216
		// (get) Token: 0x06016FA5 RID: 94117 RVA: 0x003314F9 File Offset: 0x0032F6F9
		internal override string[] AttributeTagNames
		{
			get
			{
				return Format.attributeTagNames;
			}
		}

		// Token: 0x170079F1 RID: 31217
		// (get) Token: 0x06016FA6 RID: 94118 RVA: 0x00331500 File Offset: 0x0032F700
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Format.attributeNamespaceIds;
			}
		}

		// Token: 0x170079F2 RID: 31218
		// (get) Token: 0x06016FA7 RID: 94119 RVA: 0x00331507 File Offset: 0x0032F707
		// (set) Token: 0x06016FA8 RID: 94120 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "action")]
		public EnumValue<FormatActionValues> Action
		{
			get
			{
				return (EnumValue<FormatActionValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170079F3 RID: 31219
		// (get) Token: 0x06016FA9 RID: 94121 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06016FAA RID: 94122 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "dxfId")]
		public UInt32Value FormatId
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

		// Token: 0x06016FAB RID: 94123 RVA: 0x00293ECF File Offset: 0x002920CF
		public Format()
		{
		}

		// Token: 0x06016FAC RID: 94124 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Format(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FAD RID: 94125 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Format(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FAE RID: 94126 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Format(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016FAF RID: 94127 RVA: 0x0033048A File Offset: 0x0032E68A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x170079F4 RID: 31220
		// (get) Token: 0x06016FB0 RID: 94128 RVA: 0x00331516 File Offset: 0x0032F716
		internal override string[] ElementTagNames
		{
			get
			{
				return Format.eleTagNames;
			}
		}

		// Token: 0x170079F5 RID: 31221
		// (get) Token: 0x06016FB1 RID: 94129 RVA: 0x0033151D File Offset: 0x0032F71D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Format.eleNamespaceIds;
			}
		}

		// Token: 0x170079F6 RID: 31222
		// (get) Token: 0x06016FB2 RID: 94130 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170079F7 RID: 31223
		// (get) Token: 0x06016FB3 RID: 94131 RVA: 0x003304CB File Offset: 0x0032E6CB
		// (set) Token: 0x06016FB4 RID: 94132 RVA: 0x003304D4 File Offset: 0x0032E6D4
		public PivotArea PivotArea
		{
			get
			{
				return base.GetElement<PivotArea>(0);
			}
			set
			{
				base.SetElement<PivotArea>(0, value);
			}
		}

		// Token: 0x170079F8 RID: 31224
		// (get) Token: 0x06016FB5 RID: 94133 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06016FB6 RID: 94134 RVA: 0x002E96F3 File Offset: 0x002E78F3
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06016FB7 RID: 94135 RVA: 0x00331524 File Offset: 0x0032F724
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "action" == name)
			{
				return new EnumValue<FormatActionValues>();
			}
			if (namespaceId == 0 && "dxfId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016FB8 RID: 94136 RVA: 0x0033155A File Offset: 0x0032F75A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Format>(deep);
		}

		// Token: 0x06016FB9 RID: 94137 RVA: 0x00331564 File Offset: 0x0032F764
		// Note: this type is marked as 'beforefieldinit'.
		static Format()
		{
			byte[] array = new byte[2];
			Format.attributeNamespaceIds = array;
			Format.eleTagNames = new string[] { "pivotArea", "extLst" };
			Format.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009A98 RID: 39576
		private const string tagName = "format";

		// Token: 0x04009A99 RID: 39577
		private const byte tagNsId = 22;

		// Token: 0x04009A9A RID: 39578
		internal const int ElementTypeIdConst = 11108;

		// Token: 0x04009A9B RID: 39579
		private static string[] attributeTagNames = new string[] { "action", "dxfId" };

		// Token: 0x04009A9C RID: 39580
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009A9D RID: 39581
		private static readonly string[] eleTagNames;

		// Token: 0x04009A9E RID: 39582
		private static readonly byte[] eleNamespaceIds;
	}
}
