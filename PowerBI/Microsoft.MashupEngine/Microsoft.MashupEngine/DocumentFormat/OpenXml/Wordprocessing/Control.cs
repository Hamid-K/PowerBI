using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x0200300E RID: 12302
	[GeneratedCode("DomGen", "2.0")]
	internal class Control : OpenXmlLeafElement
	{
		// Token: 0x1700966A RID: 38506
		// (get) Token: 0x0601ADAC RID: 109996 RVA: 0x002AD773 File Offset: 0x002AB973
		public override string LocalName
		{
			get
			{
				return "control";
			}
		}

		// Token: 0x1700966B RID: 38507
		// (get) Token: 0x0601ADAD RID: 109997 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700966C RID: 38508
		// (get) Token: 0x0601ADAE RID: 109998 RVA: 0x00368750 File Offset: 0x00366950
		internal override int ElementTypeId
		{
			get
			{
				return 12160;
			}
		}

		// Token: 0x0601ADAF RID: 109999 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700966D RID: 38509
		// (get) Token: 0x0601ADB0 RID: 110000 RVA: 0x00368757 File Offset: 0x00366957
		internal override string[] AttributeTagNames
		{
			get
			{
				return Control.attributeTagNames;
			}
		}

		// Token: 0x1700966E RID: 38510
		// (get) Token: 0x0601ADB1 RID: 110001 RVA: 0x0036875E File Offset: 0x0036695E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Control.attributeNamespaceIds;
			}
		}

		// Token: 0x1700966F RID: 38511
		// (get) Token: 0x0601ADB2 RID: 110002 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601ADB3 RID: 110003 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "name")]
		public StringValue Name
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

		// Token: 0x17009670 RID: 38512
		// (get) Token: 0x0601ADB4 RID: 110004 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601ADB5 RID: 110005 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "shapeid")]
		public StringValue ShapeId
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

		// Token: 0x17009671 RID: 38513
		// (get) Token: 0x0601ADB6 RID: 110006 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601ADB7 RID: 110007 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x0601ADB9 RID: 110009 RVA: 0x00368768 File Offset: 0x00366968
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "shapeid" == name)
			{
				return new StringValue();
			}
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601ADBA RID: 110010 RVA: 0x003687C5 File Offset: 0x003669C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Control>(deep);
		}

		// Token: 0x0400AE8E RID: 44686
		private const string tagName = "control";

		// Token: 0x0400AE8F RID: 44687
		private const byte tagNsId = 23;

		// Token: 0x0400AE90 RID: 44688
		internal const int ElementTypeIdConst = 12160;

		// Token: 0x0400AE91 RID: 44689
		private static string[] attributeTagNames = new string[] { "name", "shapeid", "id" };

		// Token: 0x0400AE92 RID: 44690
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 19 };
	}
}
