using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB3 RID: 11955
	[ChildElementInfo(typeof(RunProperties))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DeletedMathControl))]
	internal class InsertedMathControl : OpenXmlCompositeElement
	{
		// Token: 0x17008C06 RID: 35846
		// (get) Token: 0x06019702 RID: 104194 RVA: 0x0034411A File Offset: 0x0034231A
		public override string LocalName
		{
			get
			{
				return "ins";
			}
		}

		// Token: 0x17008C07 RID: 35847
		// (get) Token: 0x06019703 RID: 104195 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C08 RID: 35848
		// (get) Token: 0x06019704 RID: 104196 RVA: 0x0034A0E4 File Offset: 0x003482E4
		internal override int ElementTypeId
		{
			get
			{
				return 11614;
			}
		}

		// Token: 0x06019705 RID: 104197 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C09 RID: 35849
		// (get) Token: 0x06019706 RID: 104198 RVA: 0x0034A0EB File Offset: 0x003482EB
		internal override string[] AttributeTagNames
		{
			get
			{
				return InsertedMathControl.attributeTagNames;
			}
		}

		// Token: 0x17008C0A RID: 35850
		// (get) Token: 0x06019707 RID: 104199 RVA: 0x0034A0F2 File Offset: 0x003482F2
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return InsertedMathControl.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C0B RID: 35851
		// (get) Token: 0x06019708 RID: 104200 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019709 RID: 104201 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008C0C RID: 35852
		// (get) Token: 0x0601970A RID: 104202 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601970B RID: 104203 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008C0D RID: 35853
		// (get) Token: 0x0601970C RID: 104204 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601970D RID: 104205 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x0601970E RID: 104206 RVA: 0x00293ECF File Offset: 0x002920CF
		public InsertedMathControl()
		{
		}

		// Token: 0x0601970F RID: 104207 RVA: 0x00293ED7 File Offset: 0x002920D7
		public InsertedMathControl(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019710 RID: 104208 RVA: 0x00293EE0 File Offset: 0x002920E0
		public InsertedMathControl(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019711 RID: 104209 RVA: 0x00293EE9 File Offset: 0x002920E9
		public InsertedMathControl(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019712 RID: 104210 RVA: 0x0034A0F9 File Offset: 0x003482F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (23 == namespaceId && "del" == name)
			{
				return new DeletedMathControl();
			}
			return null;
		}

		// Token: 0x06019713 RID: 104211 RVA: 0x0034A12C File Offset: 0x0034832C
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

		// Token: 0x06019714 RID: 104212 RVA: 0x0034A189 File Offset: 0x00348389
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InsertedMathControl>(deep);
		}

		// Token: 0x0400A8DC RID: 43228
		private const string tagName = "ins";

		// Token: 0x0400A8DD RID: 43229
		private const byte tagNsId = 23;

		// Token: 0x0400A8DE RID: 43230
		internal const int ElementTypeIdConst = 11614;

		// Token: 0x0400A8DF RID: 43231
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400A8E0 RID: 43232
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
