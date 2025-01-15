using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002216 RID: 8726
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Rule))]
	internal class Rules : OpenXmlCompositeElement
	{
		// Token: 0x1700390D RID: 14605
		// (get) Token: 0x0600DFB7 RID: 57271 RVA: 0x002BF610 File Offset: 0x002BD810
		public override string LocalName
		{
			get
			{
				return "rules";
			}
		}

		// Token: 0x1700390E RID: 14606
		// (get) Token: 0x0600DFB8 RID: 57272 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700390F RID: 14607
		// (get) Token: 0x0600DFB9 RID: 57273 RVA: 0x002BF617 File Offset: 0x002BD817
		internal override int ElementTypeId
		{
			get
			{
				return 12419;
			}
		}

		// Token: 0x0600DFBA RID: 57274 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003910 RID: 14608
		// (get) Token: 0x0600DFBB RID: 57275 RVA: 0x002BF61E File Offset: 0x002BD81E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Rules.attributeTagNames;
			}
		}

		// Token: 0x17003911 RID: 14609
		// (get) Token: 0x0600DFBC RID: 57276 RVA: 0x002BF625 File Offset: 0x002BD825
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Rules.attributeNamespaceIds;
			}
		}

		// Token: 0x17003912 RID: 14610
		// (get) Token: 0x0600DFBD RID: 57277 RVA: 0x002BD45C File Offset: 0x002BB65C
		// (set) Token: 0x0600DFBE RID: 57278 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(26, "ext")]
		public EnumValue<ExtensionHandlingBehaviorValues> Extension
		{
			get
			{
				return (EnumValue<ExtensionHandlingBehaviorValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600DFBF RID: 57279 RVA: 0x00293ECF File Offset: 0x002920CF
		public Rules()
		{
		}

		// Token: 0x0600DFC0 RID: 57280 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Rules(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFC1 RID: 57281 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Rules(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFC2 RID: 57282 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Rules(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DFC3 RID: 57283 RVA: 0x002BF62C File Offset: 0x002BD82C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "r" == name)
			{
				return new Rule();
			}
			return null;
		}

		// Token: 0x0600DFC4 RID: 57284 RVA: 0x002BDA15 File Offset: 0x002BBC15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (26 == namespaceId && "ext" == name)
			{
				return new EnumValue<ExtensionHandlingBehaviorValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DFC5 RID: 57285 RVA: 0x002BF647 File Offset: 0x002BD847
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rules>(deep);
		}

		// Token: 0x04006DB2 RID: 28082
		private const string tagName = "rules";

		// Token: 0x04006DB3 RID: 28083
		private const byte tagNsId = 27;

		// Token: 0x04006DB4 RID: 28084
		internal const int ElementTypeIdConst = 12419;

		// Token: 0x04006DB5 RID: 28085
		private static string[] attributeTagNames = new string[] { "ext" };

		// Token: 0x04006DB6 RID: 28086
		private static byte[] attributeNamespaceIds = new byte[] { 26 };
	}
}
