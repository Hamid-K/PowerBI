using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.PowerPoint;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A90 RID: 10896
	[ChildElementInfo(typeof(ShowEventRecordList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LaserTraceList), FileFormatVersions.Office2010)]
	internal class SlideExtension : OpenXmlCompositeElement
	{
		// Token: 0x170073CC RID: 29644
		// (get) Token: 0x060161AD RID: 90541 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170073CD RID: 29645
		// (get) Token: 0x060161AE RID: 90542 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170073CE RID: 29646
		// (get) Token: 0x060161AF RID: 90543 RVA: 0x00326917 File Offset: 0x00324B17
		internal override int ElementTypeId
		{
			get
			{
				return 12309;
			}
		}

		// Token: 0x060161B0 RID: 90544 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170073CF RID: 29647
		// (get) Token: 0x060161B1 RID: 90545 RVA: 0x0032691E File Offset: 0x00324B1E
		internal override string[] AttributeTagNames
		{
			get
			{
				return SlideExtension.attributeTagNames;
			}
		}

		// Token: 0x170073D0 RID: 29648
		// (get) Token: 0x060161B2 RID: 90546 RVA: 0x00326925 File Offset: 0x00324B25
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SlideExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170073D1 RID: 29649
		// (get) Token: 0x060161B3 RID: 90547 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060161B4 RID: 90548 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x060161B5 RID: 90549 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlideExtension()
		{
		}

		// Token: 0x060161B6 RID: 90550 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlideExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161B7 RID: 90551 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlideExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060161B8 RID: 90552 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlideExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060161B9 RID: 90553 RVA: 0x0032692C File Offset: 0x00324B2C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "laserTraceLst" == name)
			{
				return new LaserTraceList();
			}
			if (49 == namespaceId && "showEvtLst" == name)
			{
				return new ShowEventRecordList();
			}
			return null;
		}

		// Token: 0x060161BA RID: 90554 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060161BB RID: 90555 RVA: 0x0032695F File Offset: 0x00324B5F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlideExtension>(deep);
		}

		// Token: 0x060161BC RID: 90556 RVA: 0x00326968 File Offset: 0x00324B68
		// Note: this type is marked as 'beforefieldinit'.
		static SlideExtension()
		{
			byte[] array = new byte[1];
			SlideExtension.attributeNamespaceIds = array;
		}

		// Token: 0x0400963B RID: 38459
		private const string tagName = "ext";

		// Token: 0x0400963C RID: 38460
		private const byte tagNsId = 24;

		// Token: 0x0400963D RID: 38461
		internal const int ElementTypeIdConst = 12309;

		// Token: 0x0400963E RID: 38462
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x0400963F RID: 38463
		private static byte[] attributeNamespaceIds;
	}
}
