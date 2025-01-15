using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A05 RID: 10757
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class SlideSyncProperties : OpenXmlPartRootElement
	{
		// Token: 0x17006F5C RID: 28508
		// (get) Token: 0x060157E9 RID: 88041 RVA: 0x0031FD6A File Offset: 0x0031DF6A
		public override string LocalName
		{
			get
			{
				return "sldSyncPr";
			}
		}

		// Token: 0x17006F5D RID: 28509
		// (get) Token: 0x060157EA RID: 88042 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006F5E RID: 28510
		// (get) Token: 0x060157EB RID: 88043 RVA: 0x0031FD71 File Offset: 0x0031DF71
		internal override int ElementTypeId
		{
			get
			{
				return 12184;
			}
		}

		// Token: 0x060157EC RID: 88044 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006F5F RID: 28511
		// (get) Token: 0x060157ED RID: 88045 RVA: 0x0031FD78 File Offset: 0x0031DF78
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideSyncProperties.attributeTagNames;
			}
		}

		// Token: 0x17006F60 RID: 28512
		// (get) Token: 0x060157EE RID: 88046 RVA: 0x0031FD7F File Offset: 0x0031DF7F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideSyncProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17006F61 RID: 28513
		// (get) Token: 0x060157EF RID: 88047 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060157F0 RID: 88048 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "serverSldId")]
		public StringValue ServerSlideId
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

		// Token: 0x17006F62 RID: 28514
		// (get) Token: 0x060157F1 RID: 88049 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x060157F2 RID: 88050 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "serverSldModifiedTime")]
		public DateTimeValue ServerSlideModifiedTime
		{
			get
			{
				return (DateTimeValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006F63 RID: 28515
		// (get) Token: 0x060157F3 RID: 88051 RVA: 0x0031FD86 File Offset: 0x0031DF86
		// (set) Token: 0x060157F4 RID: 88052 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "clientInsertedTime")]
		public DateTimeValue ClientInsertedTime
		{
			get
			{
				return (DateTimeValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060157F5 RID: 88053 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal SlideSyncProperties(SlideSyncDataPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060157F6 RID: 88054 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(SlideSyncDataPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17006F64 RID: 28516
		// (get) Token: 0x060157F7 RID: 88055 RVA: 0x0031FD95 File Offset: 0x0031DF95
		// (set) Token: 0x060157F8 RID: 88056 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public SlideSyncDataPart SlideSyncDataPart
		{
			get
			{
				return base.OpenXmlPart as SlideSyncDataPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060157F9 RID: 88057 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public SlideSyncProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060157FA RID: 88058 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public SlideSyncProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060157FB RID: 88059 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public SlideSyncProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060157FC RID: 88060 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public SlideSyncProperties()
		{
		}

		// Token: 0x060157FD RID: 88061 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(SlideSyncDataPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060157FE RID: 88062 RVA: 0x0031FDA2 File Offset: 0x0031DFA2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17006F65 RID: 28517
		// (get) Token: 0x060157FF RID: 88063 RVA: 0x0031FDBD File Offset: 0x0031DFBD
		internal override string[] ElementTagNames
		{
			get
			{
				return SlideSyncProperties.eleTagNames;
			}
		}

		// Token: 0x17006F66 RID: 28518
		// (get) Token: 0x06015800 RID: 88064 RVA: 0x0031FDC4 File Offset: 0x0031DFC4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SlideSyncProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006F67 RID: 28519
		// (get) Token: 0x06015801 RID: 88065 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006F68 RID: 28520
		// (get) Token: 0x06015802 RID: 88066 RVA: 0x0031FDCB File Offset: 0x0031DFCB
		// (set) Token: 0x06015803 RID: 88067 RVA: 0x0031FDD4 File Offset: 0x0031DFD4
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

		// Token: 0x06015804 RID: 88068 RVA: 0x0031FDE0 File Offset: 0x0031DFE0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "serverSldId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "serverSldModifiedTime" == name)
			{
				return new DateTimeValue();
			}
			if (namespaceId == 0 && "clientInsertedTime" == name)
			{
				return new DateTimeValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015805 RID: 88069 RVA: 0x0031FE37 File Offset: 0x0031E037
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideSyncProperties>(deep);
		}

		// Token: 0x06015806 RID: 88070 RVA: 0x0031FE40 File Offset: 0x0031E040
		// Note: this type is marked as 'beforefieldinit'.
		static SlideSyncProperties()
		{
			byte[] array = new byte[3];
			SlideSyncProperties.attributeNamespaceIds = array;
			SlideSyncProperties.eleTagNames = new string[] { "extLst" };
			SlideSyncProperties.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x04009398 RID: 37784
		private const string tagName = "sldSyncPr";

		// Token: 0x04009399 RID: 37785
		private const byte tagNsId = 24;

		// Token: 0x0400939A RID: 37786
		internal const int ElementTypeIdConst = 12184;

		// Token: 0x0400939B RID: 37787
		private static string[] attributeTagNames = new string[] { "serverSldId", "serverSldModifiedTime", "clientInsertedTime" };

		// Token: 0x0400939C RID: 37788
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400939D RID: 37789
		private static readonly string[] eleTagNames;

		// Token: 0x0400939E RID: 37790
		private static readonly byte[] eleNamespaceIds;
	}
}
