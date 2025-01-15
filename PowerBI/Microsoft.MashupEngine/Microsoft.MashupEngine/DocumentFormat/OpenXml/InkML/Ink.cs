using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003086 RID: 12422
	[ChildElementInfo(typeof(Context))]
	[ChildElementInfo(typeof(Trace))]
	[ChildElementInfo(typeof(Annotation))]
	[ChildElementInfo(typeof(AnnotationXml))]
	[ChildElementInfo(typeof(Definitions))]
	[ChildElementInfo(typeof(TraceView))]
	[ChildElementInfo(typeof(TraceGroup))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Ink : OpenXmlPartRootElement
	{
		// Token: 0x17009751 RID: 38737
		// (get) Token: 0x0601AFB8 RID: 110520 RVA: 0x002BDD6E File Offset: 0x002BBF6E
		public override string LocalName
		{
			get
			{
				return "ink";
			}
		}

		// Token: 0x17009752 RID: 38738
		// (get) Token: 0x0601AFB9 RID: 110521 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009753 RID: 38739
		// (get) Token: 0x0601AFBA RID: 110522 RVA: 0x0036A4B7 File Offset: 0x003686B7
		internal override int ElementTypeId
		{
			get
			{
				return 12643;
			}
		}

		// Token: 0x0601AFBB RID: 110523 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009754 RID: 38740
		// (get) Token: 0x0601AFBC RID: 110524 RVA: 0x0036A4BE File Offset: 0x003686BE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Ink.attributeTagNames;
			}
		}

		// Token: 0x17009755 RID: 38741
		// (get) Token: 0x0601AFBD RID: 110525 RVA: 0x0036A4C5 File Offset: 0x003686C5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Ink.attributeNamespaceIds;
			}
		}

		// Token: 0x17009756 RID: 38742
		// (get) Token: 0x0601AFBE RID: 110526 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AFBF RID: 110527 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "documentID")]
		public StringValue DocumentId
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

		// Token: 0x0601AFC0 RID: 110528 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Ink()
		{
		}

		// Token: 0x0601AFC1 RID: 110529 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Ink(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AFC2 RID: 110530 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Ink(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AFC3 RID: 110531 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Ink(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AFC4 RID: 110532 RVA: 0x0036A4CC File Offset: 0x003686CC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "annotation" == name)
			{
				return new Annotation();
			}
			if (43 == namespaceId && "annotationXML" == name)
			{
				return new AnnotationXml();
			}
			if (43 == namespaceId && "definitions" == name)
			{
				return new Definitions();
			}
			if (43 == namespaceId && "context" == name)
			{
				return new Context();
			}
			if (43 == namespaceId && "trace" == name)
			{
				return new Trace();
			}
			if (43 == namespaceId && "traceGroup" == name)
			{
				return new TraceGroup();
			}
			if (43 == namespaceId && "traceView" == name)
			{
				return new TraceView();
			}
			return null;
		}

		// Token: 0x0601AFC5 RID: 110533 RVA: 0x0036A582 File Offset: 0x00368782
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "documentID" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AFC6 RID: 110534 RVA: 0x0036A5A2 File Offset: 0x003687A2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Ink>(deep);
		}

		// Token: 0x0601AFC7 RID: 110535 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Ink(CustomXmlPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601AFC8 RID: 110536 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomXmlPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x0601AFC9 RID: 110537 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomXmlPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601AFCA RID: 110538 RVA: 0x0036A5AC File Offset: 0x003687AC
		// Note: this type is marked as 'beforefieldinit'.
		static Ink()
		{
			byte[] array = new byte[1];
			Ink.attributeNamespaceIds = array;
		}

		// Token: 0x0400B26D RID: 45677
		private const string tagName = "ink";

		// Token: 0x0400B26E RID: 45678
		private const byte tagNsId = 43;

		// Token: 0x0400B26F RID: 45679
		internal const int ElementTypeIdConst = 12643;

		// Token: 0x0400B270 RID: 45680
		private static string[] attributeTagNames = new string[] { "documentID" };

		// Token: 0x0400B271 RID: 45681
		private static byte[] attributeNamespaceIds;
	}
}
