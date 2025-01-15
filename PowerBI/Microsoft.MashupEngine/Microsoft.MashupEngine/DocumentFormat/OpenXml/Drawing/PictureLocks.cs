using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200283E RID: 10302
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class PictureLocks : OpenXmlCompositeElement
	{
		// Token: 0x17006666 RID: 26214
		// (get) Token: 0x06014372 RID: 82802 RVA: 0x0031088A File Offset: 0x0030EA8A
		public override string LocalName
		{
			get
			{
				return "picLocks";
			}
		}

		// Token: 0x17006667 RID: 26215
		// (get) Token: 0x06014373 RID: 82803 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006668 RID: 26216
		// (get) Token: 0x06014374 RID: 82804 RVA: 0x00310891 File Offset: 0x0030EA91
		internal override int ElementTypeId
		{
			get
			{
				return 10338;
			}
		}

		// Token: 0x06014375 RID: 82805 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006669 RID: 26217
		// (get) Token: 0x06014376 RID: 82806 RVA: 0x00310898 File Offset: 0x0030EA98
		internal override string[] AttributeTagNames
		{
			get
			{
				return PictureLocks.attributeTagNames;
			}
		}

		// Token: 0x1700666A RID: 26218
		// (get) Token: 0x06014377 RID: 82807 RVA: 0x0031089F File Offset: 0x0030EA9F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PictureLocks.attributeNamespaceIds;
			}
		}

		// Token: 0x1700666B RID: 26219
		// (get) Token: 0x06014378 RID: 82808 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06014379 RID: 82809 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "noGrp")]
		public BooleanValue NoGrouping
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

		// Token: 0x1700666C RID: 26220
		// (get) Token: 0x0601437A RID: 82810 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x0601437B RID: 82811 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "noSelect")]
		public BooleanValue NoSelection
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700666D RID: 26221
		// (get) Token: 0x0601437C RID: 82812 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x0601437D RID: 82813 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "noRot")]
		public BooleanValue NoRotation
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700666E RID: 26222
		// (get) Token: 0x0601437E RID: 82814 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601437F RID: 82815 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "noChangeAspect")]
		public BooleanValue NoChangeAspect
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700666F RID: 26223
		// (get) Token: 0x06014380 RID: 82816 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06014381 RID: 82817 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "noMove")]
		public BooleanValue NoMove
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17006670 RID: 26224
		// (get) Token: 0x06014382 RID: 82818 RVA: 0x002D7D70 File Offset: 0x002D5F70
		// (set) Token: 0x06014383 RID: 82819 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "noResize")]
		public BooleanValue NoResize
		{
			get
			{
				return (BooleanValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17006671 RID: 26225
		// (get) Token: 0x06014384 RID: 82820 RVA: 0x002CCC33 File Offset: 0x002CAE33
		// (set) Token: 0x06014385 RID: 82821 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "noEditPoints")]
		public BooleanValue NoEditPoints
		{
			get
			{
				return (BooleanValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17006672 RID: 26226
		// (get) Token: 0x06014386 RID: 82822 RVA: 0x002CA736 File Offset: 0x002C8936
		// (set) Token: 0x06014387 RID: 82823 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "noAdjustHandles")]
		public BooleanValue NoAdjustHandles
		{
			get
			{
				return (BooleanValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17006673 RID: 26227
		// (get) Token: 0x06014388 RID: 82824 RVA: 0x002CB706 File Offset: 0x002C9906
		// (set) Token: 0x06014389 RID: 82825 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "noChangeArrowheads")]
		public BooleanValue NoChangeArrowheads
		{
			get
			{
				return (BooleanValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17006674 RID: 26228
		// (get) Token: 0x0601438A RID: 82826 RVA: 0x002C92EA File Offset: 0x002C74EA
		// (set) Token: 0x0601438B RID: 82827 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "noChangeShapeType")]
		public BooleanValue NoChangeShapeType
		{
			get
			{
				return (BooleanValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17006675 RID: 26229
		// (get) Token: 0x0601438C RID: 82828 RVA: 0x002C8762 File Offset: 0x002C6962
		// (set) Token: 0x0601438D RID: 82829 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "noCrop")]
		public BooleanValue NoCrop
		{
			get
			{
				return (BooleanValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x0601438E RID: 82830 RVA: 0x00293ECF File Offset: 0x002920CF
		public PictureLocks()
		{
		}

		// Token: 0x0601438F RID: 82831 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PictureLocks(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014390 RID: 82832 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PictureLocks(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014391 RID: 82833 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PictureLocks(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014392 RID: 82834 RVA: 0x002FA71E File Offset: 0x002F891E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006676 RID: 26230
		// (get) Token: 0x06014393 RID: 82835 RVA: 0x003108A6 File Offset: 0x0030EAA6
		internal override string[] ElementTagNames
		{
			get
			{
				return PictureLocks.eleTagNames;
			}
		}

		// Token: 0x17006677 RID: 26231
		// (get) Token: 0x06014394 RID: 82836 RVA: 0x003108AD File Offset: 0x0030EAAD
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PictureLocks.eleNamespaceIds;
			}
		}

		// Token: 0x17006678 RID: 26232
		// (get) Token: 0x06014395 RID: 82837 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006679 RID: 26233
		// (get) Token: 0x06014396 RID: 82838 RVA: 0x002FA747 File Offset: 0x002F8947
		// (set) Token: 0x06014397 RID: 82839 RVA: 0x002FA750 File Offset: 0x002F8950
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(0);
			}
			set
			{
				base.SetElement<ExtensionList>(0, value);
			}
		}

		// Token: 0x06014398 RID: 82840 RVA: 0x003108B4 File Offset: 0x0030EAB4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "noGrp" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noSelect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noRot" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeAspect" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noMove" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noResize" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noEditPoints" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noAdjustHandles" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeArrowheads" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noChangeShapeType" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "noCrop" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014399 RID: 82841 RVA: 0x003109BB File Offset: 0x0030EBBB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PictureLocks>(deep);
		}

		// Token: 0x0601439A RID: 82842 RVA: 0x003109C4 File Offset: 0x0030EBC4
		// Note: this type is marked as 'beforefieldinit'.
		static PictureLocks()
		{
			byte[] array = new byte[11];
			PictureLocks.attributeNamespaceIds = array;
			PictureLocks.eleTagNames = new string[] { "extLst" };
			PictureLocks.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x0400898C RID: 35212
		private const string tagName = "picLocks";

		// Token: 0x0400898D RID: 35213
		private const byte tagNsId = 10;

		// Token: 0x0400898E RID: 35214
		internal const int ElementTypeIdConst = 10338;

		// Token: 0x0400898F RID: 35215
		private static string[] attributeTagNames = new string[]
		{
			"noGrp", "noSelect", "noRot", "noChangeAspect", "noMove", "noResize", "noEditPoints", "noAdjustHandles", "noChangeArrowheads", "noChangeShapeType",
			"noCrop"
		};

		// Token: 0x04008990 RID: 35216
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008991 RID: 35217
		private static readonly string[] eleTagNames;

		// Token: 0x04008992 RID: 35218
		private static readonly byte[] eleNamespaceIds;
	}
}
