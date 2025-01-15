using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BEB RID: 11243
	[ChildElementInfo(typeof(ControlProperties), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Control : OpenXmlCompositeElement
	{
		// Token: 0x17007E3A RID: 32314
		// (get) Token: 0x060178C5 RID: 96453 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x17007E3B RID: 32315
		// (get) Token: 0x060178C6 RID: 96454 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007E3C RID: 32316
		// (get) Token: 0x060178C7 RID: 96455 RVA: 0x00338333 File Offset: 0x00336533
		internal override int ElementTypeId
		{
			get
			{
				return 11215;
			}
		}

		// Token: 0x060178C8 RID: 96456 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007E3D RID: 32317
		// (get) Token: 0x060178C9 RID: 96457 RVA: 0x0033833A File Offset: 0x0033653A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Control.attributeTagNames;
			}
		}

		// Token: 0x17007E3E RID: 32318
		// (get) Token: 0x060178CA RID: 96458 RVA: 0x00338341 File Offset: 0x00336541
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Control.attributeNamespaceIds;
			}
		}

		// Token: 0x17007E3F RID: 32319
		// (get) Token: 0x060178CB RID: 96459 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060178CC RID: 96460 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "shapeId")]
		public UInt32Value ShapeId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17007E40 RID: 32320
		// (get) Token: 0x060178CD RID: 96461 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060178CE RID: 96462 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(19, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17007E41 RID: 32321
		// (get) Token: 0x060178CF RID: 96463 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060178D0 RID: 96464 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060178D1 RID: 96465 RVA: 0x00293ECF File Offset: 0x002920CF
		public Control()
		{
		}

		// Token: 0x060178D2 RID: 96466 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Control(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060178D3 RID: 96467 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Control(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060178D4 RID: 96468 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Control(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060178D5 RID: 96469 RVA: 0x00338348 File Offset: 0x00336548
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "controlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17007E42 RID: 32322
		// (get) Token: 0x060178D6 RID: 96470 RVA: 0x00338363 File Offset: 0x00336563
		internal override string[] ElementTagNames
		{
			get
			{
				return Control.eleTagNames;
			}
		}

		// Token: 0x17007E43 RID: 32323
		// (get) Token: 0x060178D7 RID: 96471 RVA: 0x0033836A File Offset: 0x0033656A
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Control.eleNamespaceIds;
			}
		}

		// Token: 0x17007E44 RID: 32324
		// (get) Token: 0x060178D8 RID: 96472 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007E45 RID: 32325
		// (get) Token: 0x060178D9 RID: 96473 RVA: 0x00338371 File Offset: 0x00336571
		// (set) Token: 0x060178DA RID: 96474 RVA: 0x0033837A File Offset: 0x0033657A
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(0);
			}
			set
			{
				base.SetElement<ControlProperties>(0, value);
			}
		}

		// Token: 0x060178DB RID: 96475 RVA: 0x00338384 File Offset: 0x00336584
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "shapeId" == name)
			{
				return new UInt32Value();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060178DC RID: 96476 RVA: 0x003383DD File Offset: 0x003365DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Control>(deep);
		}

		// Token: 0x060178DD RID: 96477 RVA: 0x003383E8 File Offset: 0x003365E8
		// Note: this type is marked as 'beforefieldinit'.
		static Control()
		{
			byte[] array = new byte[3];
			array[1] = 19;
			Control.attributeNamespaceIds = array;
			Control.eleTagNames = new string[] { "controlPr" };
			Control.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009CBB RID: 40123
		private const string tagName = "control";

		// Token: 0x04009CBC RID: 40124
		private const byte tagNsId = 22;

		// Token: 0x04009CBD RID: 40125
		internal const int ElementTypeIdConst = 11215;

		// Token: 0x04009CBE RID: 40126
		private static string[] attributeTagNames = new string[] { "shapeId", "id", "name" };

		// Token: 0x04009CBF RID: 40127
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009CC0 RID: 40128
		private static readonly string[] eleTagNames;

		// Token: 0x04009CC1 RID: 40129
		private static readonly byte[] eleNamespaceIds;
	}
}
