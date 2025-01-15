using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BAE RID: 11182
	[ChildElementInfo(typeof(Text))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PhoneticRun : OpenXmlCompositeElement
	{
		// Token: 0x17007B7E RID: 31614
		// (get) Token: 0x06017302 RID: 94978 RVA: 0x00333A59 File Offset: 0x00331C59
		public override string LocalName
		{
			get
			{
				return "rPh";
			}
		}

		// Token: 0x17007B7F RID: 31615
		// (get) Token: 0x06017303 RID: 94979 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B80 RID: 31616
		// (get) Token: 0x06017304 RID: 94980 RVA: 0x00333A60 File Offset: 0x00331C60
		internal override int ElementTypeId
		{
			get
			{
				return 11153;
			}
		}

		// Token: 0x06017305 RID: 94981 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007B81 RID: 31617
		// (get) Token: 0x06017306 RID: 94982 RVA: 0x00333A67 File Offset: 0x00331C67
		internal override string[] AttributeTagNames
		{
			get
			{
				return PhoneticRun.attributeTagNames;
			}
		}

		// Token: 0x17007B82 RID: 31618
		// (get) Token: 0x06017307 RID: 94983 RVA: 0x00333A6E File Offset: 0x00331C6E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PhoneticRun.attributeNamespaceIds;
			}
		}

		// Token: 0x17007B83 RID: 31619
		// (get) Token: 0x06017308 RID: 94984 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06017309 RID: 94985 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "sb")]
		public UInt32Value BaseTextStartIndex
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

		// Token: 0x17007B84 RID: 31620
		// (get) Token: 0x0601730A RID: 94986 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601730B RID: 94987 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "eb")]
		public UInt32Value EndingBaseIndex
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

		// Token: 0x0601730C RID: 94988 RVA: 0x00293ECF File Offset: 0x002920CF
		public PhoneticRun()
		{
		}

		// Token: 0x0601730D RID: 94989 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PhoneticRun(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601730E RID: 94990 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PhoneticRun(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601730F RID: 94991 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PhoneticRun(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017310 RID: 94992 RVA: 0x00333A75 File Offset: 0x00331C75
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "t" == name)
			{
				return new Text();
			}
			return null;
		}

		// Token: 0x17007B85 RID: 31621
		// (get) Token: 0x06017311 RID: 94993 RVA: 0x00333A90 File Offset: 0x00331C90
		internal override string[] ElementTagNames
		{
			get
			{
				return PhoneticRun.eleTagNames;
			}
		}

		// Token: 0x17007B86 RID: 31622
		// (get) Token: 0x06017312 RID: 94994 RVA: 0x00333A97 File Offset: 0x00331C97
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PhoneticRun.eleNamespaceIds;
			}
		}

		// Token: 0x17007B87 RID: 31623
		// (get) Token: 0x06017313 RID: 94995 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007B88 RID: 31624
		// (get) Token: 0x06017314 RID: 94996 RVA: 0x00333290 File Offset: 0x00331490
		// (set) Token: 0x06017315 RID: 94997 RVA: 0x00333299 File Offset: 0x00331499
		public Text Text
		{
			get
			{
				return base.GetElement<Text>(0);
			}
			set
			{
				base.SetElement<Text>(0, value);
			}
		}

		// Token: 0x06017316 RID: 94998 RVA: 0x00333A9E File Offset: 0x00331C9E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "sb" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "eb" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017317 RID: 94999 RVA: 0x00333AD4 File Offset: 0x00331CD4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PhoneticRun>(deep);
		}

		// Token: 0x06017318 RID: 95000 RVA: 0x00333AE0 File Offset: 0x00331CE0
		// Note: this type is marked as 'beforefieldinit'.
		static PhoneticRun()
		{
			byte[] array = new byte[2];
			PhoneticRun.attributeNamespaceIds = array;
			PhoneticRun.eleTagNames = new string[] { "t" };
			PhoneticRun.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009B83 RID: 39811
		private const string tagName = "rPh";

		// Token: 0x04009B84 RID: 39812
		private const byte tagNsId = 22;

		// Token: 0x04009B85 RID: 39813
		internal const int ElementTypeIdConst = 11153;

		// Token: 0x04009B86 RID: 39814
		private static string[] attributeTagNames = new string[] { "sb", "eb" };

		// Token: 0x04009B87 RID: 39815
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009B88 RID: 39816
		private static readonly string[] eleTagNames;

		// Token: 0x04009B89 RID: 39817
		private static readonly byte[] eleNamespaceIds;
	}
}
