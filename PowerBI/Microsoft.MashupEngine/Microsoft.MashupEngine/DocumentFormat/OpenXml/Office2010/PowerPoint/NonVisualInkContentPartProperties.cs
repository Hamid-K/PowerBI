using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Drawing;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023BA RID: 9146
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ContentPartLocks), FileFormatVersions.Office2010)]
	internal class NonVisualInkContentPartProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004C9C RID: 19612
		// (get) Token: 0x0601094A RID: 67914 RVA: 0x002DFE49 File Offset: 0x002DE049
		public override string LocalName
		{
			get
			{
				return "cNvContentPartPr";
			}
		}

		// Token: 0x17004C9D RID: 19613
		// (get) Token: 0x0601094B RID: 67915 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004C9E RID: 19614
		// (get) Token: 0x0601094C RID: 67916 RVA: 0x002E4F92 File Offset: 0x002E3192
		internal override int ElementTypeId
		{
			get
			{
				return 12800;
			}
		}

		// Token: 0x0601094D RID: 67917 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004C9F RID: 19615
		// (get) Token: 0x0601094E RID: 67918 RVA: 0x002E4F99 File Offset: 0x002E3199
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualInkContentPartProperties.attributeTagNames;
			}
		}

		// Token: 0x17004CA0 RID: 19616
		// (get) Token: 0x0601094F RID: 67919 RVA: 0x002E4FA0 File Offset: 0x002E31A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004CA1 RID: 19617
		// (get) Token: 0x06010950 RID: 67920 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06010951 RID: 67921 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "isComment")]
		public BooleanValue IsComment
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06010952 RID: 67922 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualInkContentPartProperties()
		{
		}

		// Token: 0x06010953 RID: 67923 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualInkContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010954 RID: 67924 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualInkContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010955 RID: 67925 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualInkContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010956 RID: 67926 RVA: 0x002DFD24 File Offset: 0x002DDF24
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "cpLocks" == name)
			{
				return new ContentPartLocks();
			}
			if (48 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17004CA2 RID: 19618
		// (get) Token: 0x06010957 RID: 67927 RVA: 0x002E4FA7 File Offset: 0x002E31A7
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualInkContentPartProperties.eleTagNames;
			}
		}

		// Token: 0x17004CA3 RID: 19619
		// (get) Token: 0x06010958 RID: 67928 RVA: 0x002E4FAE File Offset: 0x002E31AE
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004CA4 RID: 19620
		// (get) Token: 0x06010959 RID: 67929 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004CA5 RID: 19621
		// (get) Token: 0x0601095A RID: 67930 RVA: 0x002DFD65 File Offset: 0x002DDF65
		// (set) Token: 0x0601095B RID: 67931 RVA: 0x002DFD6E File Offset: 0x002DDF6E
		public ContentPartLocks ContentPartLocks
		{
			get
			{
				return base.GetElement<ContentPartLocks>(0);
			}
			set
			{
				base.SetElement<ContentPartLocks>(0, value);
			}
		}

		// Token: 0x17004CA6 RID: 19622
		// (get) Token: 0x0601095C RID: 67932 RVA: 0x002DFD78 File Offset: 0x002DDF78
		// (set) Token: 0x0601095D RID: 67933 RVA: 0x002DFD81 File Offset: 0x002DDF81
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(1);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(1, value);
			}
		}

		// Token: 0x0601095E RID: 67934 RVA: 0x002DFD8B File Offset: 0x002DDF8B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "isComment" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601095F RID: 67935 RVA: 0x002E4FB5 File Offset: 0x002E31B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualInkContentPartProperties>(deep);
		}

		// Token: 0x06010960 RID: 67936 RVA: 0x002E4FC0 File Offset: 0x002E31C0
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualInkContentPartProperties()
		{
			byte[] array = new byte[1];
			NonVisualInkContentPartProperties.attributeNamespaceIds = array;
			NonVisualInkContentPartProperties.eleTagNames = new string[] { "cpLocks", "extLst" };
			NonVisualInkContentPartProperties.eleNamespaceIds = new byte[] { 48, 48 };
		}

		// Token: 0x04007557 RID: 30039
		private const string tagName = "cNvContentPartPr";

		// Token: 0x04007558 RID: 30040
		private const byte tagNsId = 49;

		// Token: 0x04007559 RID: 30041
		internal const int ElementTypeIdConst = 12800;

		// Token: 0x0400755A RID: 30042
		private static string[] attributeTagNames = new string[] { "isComment" };

		// Token: 0x0400755B RID: 30043
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400755C RID: 30044
		private static readonly string[] eleTagNames;

		// Token: 0x0400755D RID: 30045
		private static readonly byte[] eleNamespaceIds;
	}
}
