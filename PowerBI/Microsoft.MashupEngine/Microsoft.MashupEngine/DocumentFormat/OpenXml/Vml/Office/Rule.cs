using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Office
{
	// Token: 0x02002218 RID: 8728
	[ChildElementInfo(typeof(Proxy))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Rule : OpenXmlCompositeElement
	{
		// Token: 0x1700391A RID: 14618
		// (get) Token: 0x0600DFD5 RID: 57301 RVA: 0x002BF737 File Offset: 0x002BD937
		public override string LocalName
		{
			get
			{
				return "r";
			}
		}

		// Token: 0x1700391B RID: 14619
		// (get) Token: 0x0600DFD6 RID: 57302 RVA: 0x0012AF09 File Offset: 0x00129109
		internal override byte NamespaceId
		{
			get
			{
				return 27;
			}
		}

		// Token: 0x1700391C RID: 14620
		// (get) Token: 0x0600DFD7 RID: 57303 RVA: 0x002BF73E File Offset: 0x002BD93E
		internal override int ElementTypeId
		{
			get
			{
				return 12421;
			}
		}

		// Token: 0x0600DFD8 RID: 57304 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700391D RID: 14621
		// (get) Token: 0x0600DFD9 RID: 57305 RVA: 0x002BF745 File Offset: 0x002BD945
		internal override string[] AttributeTagNames
		{
			get
			{
				return Rule.attributeTagNames;
			}
		}

		// Token: 0x1700391E RID: 14622
		// (get) Token: 0x0600DFDA RID: 57306 RVA: 0x002BF74C File Offset: 0x002BD94C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Rule.attributeNamespaceIds;
			}
		}

		// Token: 0x1700391F RID: 14623
		// (get) Token: 0x0600DFDB RID: 57307 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600DFDC RID: 57308 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
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

		// Token: 0x17003920 RID: 14624
		// (get) Token: 0x0600DFDD RID: 57309 RVA: 0x002BF753 File Offset: 0x002BD953
		// (set) Token: 0x0600DFDE RID: 57310 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public EnumValue<RuleValues> Type
		{
			get
			{
				return (EnumValue<RuleValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003921 RID: 14625
		// (get) Token: 0x0600DFDF RID: 57311 RVA: 0x002BF762 File Offset: 0x002BD962
		// (set) Token: 0x0600DFE0 RID: 57312 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "how")]
		public EnumValue<AlignmentValues> How
		{
			get
			{
				return (EnumValue<AlignmentValues>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003922 RID: 14626
		// (get) Token: 0x0600DFE1 RID: 57313 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x0600DFE2 RID: 57314 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "idref")]
		public StringValue ShapeReference
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0600DFE3 RID: 57315 RVA: 0x00293ECF File Offset: 0x002920CF
		public Rule()
		{
		}

		// Token: 0x0600DFE4 RID: 57316 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Rule(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFE5 RID: 57317 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Rule(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DFE6 RID: 57318 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Rule(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DFE7 RID: 57319 RVA: 0x002BF771 File Offset: 0x002BD971
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (27 == namespaceId && "proxy" == name)
			{
				return new Proxy();
			}
			return null;
		}

		// Token: 0x0600DFE8 RID: 57320 RVA: 0x002BF78C File Offset: 0x002BD98C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<RuleValues>();
			}
			if (namespaceId == 0 && "how" == name)
			{
				return new EnumValue<AlignmentValues>();
			}
			if (namespaceId == 0 && "idref" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DFE9 RID: 57321 RVA: 0x002BF7F9 File Offset: 0x002BD9F9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rule>(deep);
		}

		// Token: 0x0600DFEA RID: 57322 RVA: 0x002BF804 File Offset: 0x002BDA04
		// Note: this type is marked as 'beforefieldinit'.
		static Rule()
		{
			byte[] array = new byte[4];
			Rule.attributeNamespaceIds = array;
		}

		// Token: 0x04006DBC RID: 28092
		private const string tagName = "r";

		// Token: 0x04006DBD RID: 28093
		private const byte tagNsId = 27;

		// Token: 0x04006DBE RID: 28094
		internal const int ElementTypeIdConst = 12421;

		// Token: 0x04006DBF RID: 28095
		private static string[] attributeTagNames = new string[] { "id", "type", "how", "idref" };

		// Token: 0x04006DC0 RID: 28096
		private static byte[] attributeNamespaceIds;
	}
}
