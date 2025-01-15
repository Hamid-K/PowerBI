using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB1 RID: 11953
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PreviousRunProperties))]
	internal class RunPropertiesChange : OpenXmlCompositeElement
	{
		// Token: 0x17008BC1 RID: 35777
		// (get) Token: 0x06019675 RID: 104053 RVA: 0x003494AC File Offset: 0x003476AC
		public override string LocalName
		{
			get
			{
				return "rPrChange";
			}
		}

		// Token: 0x17008BC2 RID: 35778
		// (get) Token: 0x06019676 RID: 104054 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008BC3 RID: 35779
		// (get) Token: 0x06019677 RID: 104055 RVA: 0x003494B3 File Offset: 0x003476B3
		internal override int ElementTypeId
		{
			get
			{
				return 11612;
			}
		}

		// Token: 0x06019678 RID: 104056 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008BC4 RID: 35780
		// (get) Token: 0x06019679 RID: 104057 RVA: 0x003494BA File Offset: 0x003476BA
		internal override string[] AttributeTagNames
		{
			get
			{
				return RunPropertiesChange.attributeTagNames;
			}
		}

		// Token: 0x17008BC5 RID: 35781
		// (get) Token: 0x0601967A RID: 104058 RVA: 0x003494C1 File Offset: 0x003476C1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return RunPropertiesChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17008BC6 RID: 35782
		// (get) Token: 0x0601967B RID: 104059 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601967C RID: 104060 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "author")]
		public StringValue Author
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

		// Token: 0x17008BC7 RID: 35783
		// (get) Token: 0x0601967D RID: 104061 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601967E RID: 104062 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "date")]
		public DateTimeValue Date
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

		// Token: 0x17008BC8 RID: 35784
		// (get) Token: 0x0601967F RID: 104063 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019680 RID: 104064 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "id")]
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

		// Token: 0x06019681 RID: 104065 RVA: 0x00293ECF File Offset: 0x002920CF
		public RunPropertiesChange()
		{
		}

		// Token: 0x06019682 RID: 104066 RVA: 0x00293ED7 File Offset: 0x002920D7
		public RunPropertiesChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019683 RID: 104067 RVA: 0x00293EE0 File Offset: 0x002920E0
		public RunPropertiesChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019684 RID: 104068 RVA: 0x00293EE9 File Offset: 0x002920E9
		public RunPropertiesChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019685 RID: 104069 RVA: 0x003494C8 File Offset: 0x003476C8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new PreviousRunProperties();
			}
			return null;
		}

		// Token: 0x17008BC9 RID: 35785
		// (get) Token: 0x06019686 RID: 104070 RVA: 0x003494E3 File Offset: 0x003476E3
		internal override string[] ElementTagNames
		{
			get
			{
				return RunPropertiesChange.eleTagNames;
			}
		}

		// Token: 0x17008BCA RID: 35786
		// (get) Token: 0x06019687 RID: 104071 RVA: 0x003494EA File Offset: 0x003476EA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RunPropertiesChange.eleNamespaceIds;
			}
		}

		// Token: 0x17008BCB RID: 35787
		// (get) Token: 0x06019688 RID: 104072 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008BCC RID: 35788
		// (get) Token: 0x06019689 RID: 104073 RVA: 0x003494F1 File Offset: 0x003476F1
		// (set) Token: 0x0601968A RID: 104074 RVA: 0x003494FA File Offset: 0x003476FA
		public PreviousRunProperties PreviousRunProperties
		{
			get
			{
				return base.GetElement<PreviousRunProperties>(0);
			}
			set
			{
				base.SetElement<PreviousRunProperties>(0, value);
			}
		}

		// Token: 0x0601968B RID: 104075 RVA: 0x00349504 File Offset: 0x00347704
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "author" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "date" == name)
			{
				return new DateTimeValue();
			}
			if (23 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601968C RID: 104076 RVA: 0x00349561 File Offset: 0x00347761
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RunPropertiesChange>(deep);
		}

		// Token: 0x0400A8D0 RID: 43216
		private const string tagName = "rPrChange";

		// Token: 0x0400A8D1 RID: 43217
		private const byte tagNsId = 23;

		// Token: 0x0400A8D2 RID: 43218
		internal const int ElementTypeIdConst = 11612;

		// Token: 0x0400A8D3 RID: 43219
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400A8D4 RID: 43220
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400A8D5 RID: 43221
		private static readonly string[] eleTagNames = new string[] { "rPr" };

		// Token: 0x0400A8D6 RID: 43222
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
