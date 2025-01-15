using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C07 RID: 11271
	[ChildElementInfo(typeof(ForegroundColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(BackgroundColor))]
	internal class PatternFill : OpenXmlCompositeElement
	{
		// Token: 0x17007F80 RID: 32640
		// (get) Token: 0x06017B87 RID: 97159 RVA: 0x0033A4EA File Offset: 0x003386EA
		public override string LocalName
		{
			get
			{
				return "patternFill";
			}
		}

		// Token: 0x17007F81 RID: 32641
		// (get) Token: 0x06017B88 RID: 97160 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007F82 RID: 32642
		// (get) Token: 0x06017B89 RID: 97161 RVA: 0x0033A4F1 File Offset: 0x003386F1
		internal override int ElementTypeId
		{
			get
			{
				return 11250;
			}
		}

		// Token: 0x06017B8A RID: 97162 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007F83 RID: 32643
		// (get) Token: 0x06017B8B RID: 97163 RVA: 0x0033A4F8 File Offset: 0x003386F8
		internal override string[] AttributeTagNames
		{
			get
			{
				return PatternFill.attributeTagNames;
			}
		}

		// Token: 0x17007F84 RID: 32644
		// (get) Token: 0x06017B8C RID: 97164 RVA: 0x0033A4FF File Offset: 0x003386FF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PatternFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17007F85 RID: 32645
		// (get) Token: 0x06017B8D RID: 97165 RVA: 0x0033A506 File Offset: 0x00338706
		// (set) Token: 0x06017B8E RID: 97166 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "patternType")]
		public EnumValue<PatternValues> PatternType
		{
			get
			{
				return (EnumValue<PatternValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017B8F RID: 97167 RVA: 0x00293ECF File Offset: 0x002920CF
		public PatternFill()
		{
		}

		// Token: 0x06017B90 RID: 97168 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PatternFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B91 RID: 97169 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PatternFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017B92 RID: 97170 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PatternFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017B93 RID: 97171 RVA: 0x0033A515 File Offset: 0x00338715
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "fgColor" == name)
			{
				return new ForegroundColor();
			}
			if (22 == namespaceId && "bgColor" == name)
			{
				return new BackgroundColor();
			}
			return null;
		}

		// Token: 0x17007F86 RID: 32646
		// (get) Token: 0x06017B94 RID: 97172 RVA: 0x0033A548 File Offset: 0x00338748
		internal override string[] ElementTagNames
		{
			get
			{
				return PatternFill.eleTagNames;
			}
		}

		// Token: 0x17007F87 RID: 32647
		// (get) Token: 0x06017B95 RID: 97173 RVA: 0x0033A54F File Offset: 0x0033874F
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PatternFill.eleNamespaceIds;
			}
		}

		// Token: 0x17007F88 RID: 32648
		// (get) Token: 0x06017B96 RID: 97174 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007F89 RID: 32649
		// (get) Token: 0x06017B97 RID: 97175 RVA: 0x0033A556 File Offset: 0x00338756
		// (set) Token: 0x06017B98 RID: 97176 RVA: 0x0033A55F File Offset: 0x0033875F
		public ForegroundColor ForegroundColor
		{
			get
			{
				return base.GetElement<ForegroundColor>(0);
			}
			set
			{
				base.SetElement<ForegroundColor>(0, value);
			}
		}

		// Token: 0x17007F8A RID: 32650
		// (get) Token: 0x06017B99 RID: 97177 RVA: 0x0033A569 File Offset: 0x00338769
		// (set) Token: 0x06017B9A RID: 97178 RVA: 0x0033A572 File Offset: 0x00338772
		public BackgroundColor BackgroundColor
		{
			get
			{
				return base.GetElement<BackgroundColor>(1);
			}
			set
			{
				base.SetElement<BackgroundColor>(1, value);
			}
		}

		// Token: 0x06017B9B RID: 97179 RVA: 0x0033A57C File Offset: 0x0033877C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "patternType" == name)
			{
				return new EnumValue<PatternValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017B9C RID: 97180 RVA: 0x0033A59C File Offset: 0x0033879C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PatternFill>(deep);
		}

		// Token: 0x06017B9D RID: 97181 RVA: 0x0033A5A8 File Offset: 0x003387A8
		// Note: this type is marked as 'beforefieldinit'.
		static PatternFill()
		{
			byte[] array = new byte[1];
			PatternFill.attributeNamespaceIds = array;
			PatternFill.eleTagNames = new string[] { "fgColor", "bgColor" };
			PatternFill.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009D50 RID: 40272
		private const string tagName = "patternFill";

		// Token: 0x04009D51 RID: 40273
		private const byte tagNsId = 22;

		// Token: 0x04009D52 RID: 40274
		internal const int ElementTypeIdConst = 11250;

		// Token: 0x04009D53 RID: 40275
		private static string[] attributeTagNames = new string[] { "patternType" };

		// Token: 0x04009D54 RID: 40276
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009D55 RID: 40277
		private static readonly string[] eleTagNames;

		// Token: 0x04009D56 RID: 40278
		private static readonly byte[] eleNamespaceIds;
	}
}
