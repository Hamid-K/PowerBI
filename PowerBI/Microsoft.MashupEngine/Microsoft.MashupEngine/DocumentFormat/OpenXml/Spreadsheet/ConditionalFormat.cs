using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B79 RID: 11129
	[ChildElementInfo(typeof(PivotAreas))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class ConditionalFormat : OpenXmlCompositeElement
	{
		// Token: 0x170079F9 RID: 31225
		// (get) Token: 0x06016FBA RID: 94138 RVA: 0x002E9BF3 File Offset: 0x002E7DF3
		public override string LocalName
		{
			get
			{
				return "conditionalFormat";
			}
		}

		// Token: 0x170079FA RID: 31226
		// (get) Token: 0x06016FBB RID: 94139 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170079FB RID: 31227
		// (get) Token: 0x06016FBC RID: 94140 RVA: 0x003315CF File Offset: 0x0032F7CF
		internal override int ElementTypeId
		{
			get
			{
				return 11109;
			}
		}

		// Token: 0x06016FBD RID: 94141 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170079FC RID: 31228
		// (get) Token: 0x06016FBE RID: 94142 RVA: 0x003315D6 File Offset: 0x0032F7D6
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormat.attributeTagNames;
			}
		}

		// Token: 0x170079FD RID: 31229
		// (get) Token: 0x06016FBF RID: 94143 RVA: 0x003315DD File Offset: 0x0032F7DD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x170079FE RID: 31230
		// (get) Token: 0x06016FC0 RID: 94144 RVA: 0x002E9C0F File Offset: 0x002E7E0F
		// (set) Token: 0x06016FC1 RID: 94145 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "scope")]
		public EnumValue<ScopeValues> Scope
		{
			get
			{
				return (EnumValue<ScopeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170079FF RID: 31231
		// (get) Token: 0x06016FC2 RID: 94146 RVA: 0x002E9C1E File Offset: 0x002E7E1E
		// (set) Token: 0x06016FC3 RID: 94147 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007A00 RID: 31232
		// (get) Token: 0x06016FC4 RID: 94148 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06016FC5 RID: 94149 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "priority")]
		public UInt32Value Priority
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06016FC6 RID: 94150 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormat()
		{
		}

		// Token: 0x06016FC7 RID: 94151 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FC8 RID: 94152 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016FC9 RID: 94153 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016FCA RID: 94154 RVA: 0x003315E4 File Offset: 0x0032F7E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotAreas" == name)
			{
				return new PivotAreas();
			}
			if (22 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17007A01 RID: 31233
		// (get) Token: 0x06016FCB RID: 94155 RVA: 0x00331617 File Offset: 0x0032F817
		internal override string[] ElementTagNames
		{
			get
			{
				return ConditionalFormat.eleTagNames;
			}
		}

		// Token: 0x17007A02 RID: 31234
		// (get) Token: 0x06016FCC RID: 94156 RVA: 0x0033161E File Offset: 0x0032F81E
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConditionalFormat.eleNamespaceIds;
			}
		}

		// Token: 0x17007A03 RID: 31235
		// (get) Token: 0x06016FCD RID: 94157 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007A04 RID: 31236
		// (get) Token: 0x06016FCE RID: 94158 RVA: 0x00331625 File Offset: 0x0032F825
		// (set) Token: 0x06016FCF RID: 94159 RVA: 0x0033162E File Offset: 0x0032F82E
		public PivotAreas PivotAreas
		{
			get
			{
				return base.GetElement<PivotAreas>(0);
			}
			set
			{
				base.SetElement<PivotAreas>(0, value);
			}
		}

		// Token: 0x17007A05 RID: 31237
		// (get) Token: 0x06016FD0 RID: 94160 RVA: 0x002E96EA File Offset: 0x002E78EA
		// (set) Token: 0x06016FD1 RID: 94161 RVA: 0x002E96F3 File Offset: 0x002E78F3
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

		// Token: 0x06016FD2 RID: 94162 RVA: 0x00331638 File Offset: 0x0032F838
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "scope" == name)
			{
				return new EnumValue<ScopeValues>();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<RuleValues>();
			}
			if (namespaceId == 0 && "priority" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016FD3 RID: 94163 RVA: 0x0033168F File Offset: 0x0032F88F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormat>(deep);
		}

		// Token: 0x06016FD4 RID: 94164 RVA: 0x00331698 File Offset: 0x0032F898
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormat()
		{
			byte[] array = new byte[3];
			ConditionalFormat.attributeNamespaceIds = array;
			ConditionalFormat.eleTagNames = new string[] { "pivotAreas", "extLst" };
			ConditionalFormat.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x04009A9F RID: 39583
		private const string tagName = "conditionalFormat";

		// Token: 0x04009AA0 RID: 39584
		private const byte tagNsId = 22;

		// Token: 0x04009AA1 RID: 39585
		internal const int ElementTypeIdConst = 11109;

		// Token: 0x04009AA2 RID: 39586
		private static string[] attributeTagNames = new string[] { "scope", "type", "priority" };

		// Token: 0x04009AA3 RID: 39587
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009AA4 RID: 39588
		private static readonly string[] eleTagNames;

		// Token: 0x04009AA5 RID: 39589
		private static readonly byte[] eleNamespaceIds;
	}
}
