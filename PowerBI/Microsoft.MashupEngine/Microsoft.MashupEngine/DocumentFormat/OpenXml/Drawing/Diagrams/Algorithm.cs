using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002669 RID: 9833
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Parameter))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class Algorithm : OpenXmlCompositeElement
	{
		// Token: 0x17005BF3 RID: 23539
		// (get) Token: 0x06012BB3 RID: 76723 RVA: 0x002FE91F File Offset: 0x002FCB1F
		public override string LocalName
		{
			get
			{
				return "alg";
			}
		}

		// Token: 0x17005BF4 RID: 23540
		// (get) Token: 0x06012BB4 RID: 76724 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005BF5 RID: 23541
		// (get) Token: 0x06012BB5 RID: 76725 RVA: 0x002FE926 File Offset: 0x002FCB26
		internal override int ElementTypeId
		{
			get
			{
				return 10650;
			}
		}

		// Token: 0x06012BB6 RID: 76726 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005BF6 RID: 23542
		// (get) Token: 0x06012BB7 RID: 76727 RVA: 0x002FE92D File Offset: 0x002FCB2D
		internal override string[] AttributeTagNames
		{
			get
			{
				return Algorithm.attributeTagNames;
			}
		}

		// Token: 0x17005BF7 RID: 23543
		// (get) Token: 0x06012BB8 RID: 76728 RVA: 0x002FE934 File Offset: 0x002FCB34
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Algorithm.attributeNamespaceIds;
			}
		}

		// Token: 0x17005BF8 RID: 23544
		// (get) Token: 0x06012BB9 RID: 76729 RVA: 0x002FE93B File Offset: 0x002FCB3B
		// (set) Token: 0x06012BBA RID: 76730 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<AlgorithmValues> Type
		{
			get
			{
				return (EnumValue<AlgorithmValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005BF9 RID: 23545
		// (get) Token: 0x06012BBB RID: 76731 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06012BBC RID: 76732 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "rev")]
		public UInt32Value Revision
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

		// Token: 0x06012BBD RID: 76733 RVA: 0x00293ECF File Offset: 0x002920CF
		public Algorithm()
		{
		}

		// Token: 0x06012BBE RID: 76734 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Algorithm(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BBF RID: 76735 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Algorithm(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012BC0 RID: 76736 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Algorithm(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012BC1 RID: 76737 RVA: 0x002FE94A File Offset: 0x002FCB4A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "param" == name)
			{
				return new Parameter();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06012BC2 RID: 76738 RVA: 0x002FE97D File Offset: 0x002FCB7D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<AlgorithmValues>();
			}
			if (namespaceId == 0 && "rev" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012BC3 RID: 76739 RVA: 0x002FE9B3 File Offset: 0x002FCBB3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Algorithm>(deep);
		}

		// Token: 0x06012BC4 RID: 76740 RVA: 0x002FE9BC File Offset: 0x002FCBBC
		// Note: this type is marked as 'beforefieldinit'.
		static Algorithm()
		{
			byte[] array = new byte[2];
			Algorithm.attributeNamespaceIds = array;
		}

		// Token: 0x04008163 RID: 33123
		private const string tagName = "alg";

		// Token: 0x04008164 RID: 33124
		private const byte tagNsId = 14;

		// Token: 0x04008165 RID: 33125
		internal const int ElementTypeIdConst = 10650;

		// Token: 0x04008166 RID: 33126
		private static string[] attributeTagNames = new string[] { "type", "rev" };

		// Token: 0x04008167 RID: 33127
		private static byte[] attributeNamespaceIds;
	}
}
