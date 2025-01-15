using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB4 RID: 11956
	[ChildElementInfo(typeof(RunProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class DeletedMathControl : OpenXmlCompositeElement
	{
		// Token: 0x17008C0E RID: 35854
		// (get) Token: 0x06019716 RID: 104214 RVA: 0x00344131 File Offset: 0x00342331
		public override string LocalName
		{
			get
			{
				return "del";
			}
		}

		// Token: 0x17008C0F RID: 35855
		// (get) Token: 0x06019717 RID: 104215 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C10 RID: 35856
		// (get) Token: 0x06019718 RID: 104216 RVA: 0x0034A1DC File Offset: 0x003483DC
		internal override int ElementTypeId
		{
			get
			{
				return 11615;
			}
		}

		// Token: 0x06019719 RID: 104217 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C11 RID: 35857
		// (get) Token: 0x0601971A RID: 104218 RVA: 0x0034A1E3 File Offset: 0x003483E3
		internal override string[] AttributeTagNames
		{
			get
			{
				return DeletedMathControl.attributeTagNames;
			}
		}

		// Token: 0x17008C12 RID: 35858
		// (get) Token: 0x0601971B RID: 104219 RVA: 0x0034A1EA File Offset: 0x003483EA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return DeletedMathControl.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C13 RID: 35859
		// (get) Token: 0x0601971C RID: 104220 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601971D RID: 104221 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008C14 RID: 35860
		// (get) Token: 0x0601971E RID: 104222 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601971F RID: 104223 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008C15 RID: 35861
		// (get) Token: 0x06019720 RID: 104224 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019721 RID: 104225 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06019722 RID: 104226 RVA: 0x00293ECF File Offset: 0x002920CF
		public DeletedMathControl()
		{
		}

		// Token: 0x06019723 RID: 104227 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DeletedMathControl(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019724 RID: 104228 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DeletedMathControl(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019725 RID: 104229 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DeletedMathControl(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019726 RID: 104230 RVA: 0x0034A1F1 File Offset: 0x003483F1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			return null;
		}

		// Token: 0x06019727 RID: 104231 RVA: 0x0034A20C File Offset: 0x0034840C
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

		// Token: 0x06019728 RID: 104232 RVA: 0x0034A269 File Offset: 0x00348469
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DeletedMathControl>(deep);
		}

		// Token: 0x0400A8E1 RID: 43233
		private const string tagName = "del";

		// Token: 0x0400A8E2 RID: 43234
		private const byte tagNsId = 23;

		// Token: 0x0400A8E3 RID: 43235
		internal const int ElementTypeIdConst = 11615;

		// Token: 0x0400A8E4 RID: 43236
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400A8E5 RID: 43237
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
