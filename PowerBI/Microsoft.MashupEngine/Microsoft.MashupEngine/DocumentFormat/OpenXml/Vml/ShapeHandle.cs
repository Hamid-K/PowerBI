using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml
{
	// Token: 0x02002257 RID: 8791
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeHandle : OpenXmlLeafElement
	{
		// Token: 0x17003C9E RID: 15518
		// (get) Token: 0x0600E727 RID: 59175 RVA: 0x002C804C File Offset: 0x002C624C
		public override string LocalName
		{
			get
			{
				return "h";
			}
		}

		// Token: 0x17003C9F RID: 15519
		// (get) Token: 0x0600E728 RID: 59176 RVA: 0x00243C87 File Offset: 0x00241E87
		internal override byte NamespaceId
		{
			get
			{
				return 26;
			}
		}

		// Token: 0x17003CA0 RID: 15520
		// (get) Token: 0x0600E729 RID: 59177 RVA: 0x002C8053 File Offset: 0x002C6253
		internal override int ElementTypeId
		{
			get
			{
				return 12527;
			}
		}

		// Token: 0x0600E72A RID: 59178 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003CA1 RID: 15521
		// (get) Token: 0x0600E72B RID: 59179 RVA: 0x002C805A File Offset: 0x002C625A
		internal override string[] AttributeTagNames
		{
			get
			{
				return ShapeHandle.attributeTagNames;
			}
		}

		// Token: 0x17003CA2 RID: 15522
		// (get) Token: 0x0600E72C RID: 59180 RVA: 0x002C8061 File Offset: 0x002C6261
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ShapeHandle.attributeNamespaceIds;
			}
		}

		// Token: 0x17003CA3 RID: 15523
		// (get) Token: 0x0600E72D RID: 59181 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E72E RID: 59182 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "position")]
		public StringValue Position
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

		// Token: 0x17003CA4 RID: 15524
		// (get) Token: 0x0600E72F RID: 59183 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E730 RID: 59184 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "polar")]
		public StringValue Polar
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003CA5 RID: 15525
		// (get) Token: 0x0600E731 RID: 59185 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0600E732 RID: 59186 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "map")]
		public StringValue Map
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003CA6 RID: 15526
		// (get) Token: 0x0600E733 RID: 59187 RVA: 0x002C8068 File Offset: 0x002C6268
		// (set) Token: 0x0600E734 RID: 59188 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "invx")]
		public TrueFalseBlankValue InvertX
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003CA7 RID: 15527
		// (get) Token: 0x0600E735 RID: 59189 RVA: 0x002C8077 File Offset: 0x002C6277
		// (set) Token: 0x0600E736 RID: 59190 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "invy")]
		public TrueFalseBlankValue InvertY
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003CA8 RID: 15528
		// (get) Token: 0x0600E737 RID: 59191 RVA: 0x002C8086 File Offset: 0x002C6286
		// (set) Token: 0x0600E738 RID: 59192 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "switch")]
		public TrueFalseBlankValue Switch
		{
			get
			{
				return (TrueFalseBlankValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003CA9 RID: 15529
		// (get) Token: 0x0600E739 RID: 59193 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E73A RID: 59194 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "xrange")]
		public StringValue XRange
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17003CAA RID: 15530
		// (get) Token: 0x0600E73B RID: 59195 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E73C RID: 59196 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "yrange")]
		public StringValue YRange
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17003CAB RID: 15531
		// (get) Token: 0x0600E73D RID: 59197 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E73E RID: 59198 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "radiusrange")]
		public StringValue RadiusRange
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x0600E740 RID: 59200 RVA: 0x002C8098 File Offset: 0x002C6298
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "position" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "polar" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "map" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "invx" == name)
			{
				return new TrueFalseBlankValue();
			}
			if (namespaceId == 0 && "invy" == name)
			{
				return new TrueFalseBlankValue();
			}
			if (namespaceId == 0 && "switch" == name)
			{
				return new TrueFalseBlankValue();
			}
			if (namespaceId == 0 && "xrange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "yrange" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "radiusrange" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E741 RID: 59201 RVA: 0x002C8173 File Offset: 0x002C6373
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeHandle>(deep);
		}

		// Token: 0x0600E742 RID: 59202 RVA: 0x002C817C File Offset: 0x002C637C
		// Note: this type is marked as 'beforefieldinit'.
		static ShapeHandle()
		{
			byte[] array = new byte[9];
			ShapeHandle.attributeNamespaceIds = array;
		}

		// Token: 0x04006EFB RID: 28411
		private const string tagName = "h";

		// Token: 0x04006EFC RID: 28412
		private const byte tagNsId = 26;

		// Token: 0x04006EFD RID: 28413
		internal const int ElementTypeIdConst = 12527;

		// Token: 0x04006EFE RID: 28414
		private static string[] attributeTagNames = new string[] { "position", "polar", "map", "invx", "invy", "switch", "xrange", "yrange", "radiusrange" };

		// Token: 0x04006EFF RID: 28415
		private static byte[] attributeNamespaceIds;
	}
}
