using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003089 RID: 12425
	[GeneratedCode("DomGen", "2.0")]
	internal class Matrix : OpenXmlLeafTextElement
	{
		// Token: 0x17009768 RID: 38760
		// (get) Token: 0x0601AFEF RID: 110575 RVA: 0x0036A7AF File Offset: 0x003689AF
		public override string LocalName
		{
			get
			{
				return "matrix";
			}
		}

		// Token: 0x17009769 RID: 38761
		// (get) Token: 0x0601AFF0 RID: 110576 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x1700976A RID: 38762
		// (get) Token: 0x0601AFF1 RID: 110577 RVA: 0x0036A7B6 File Offset: 0x003689B6
		internal override int ElementTypeId
		{
			get
			{
				return 12646;
			}
		}

		// Token: 0x0601AFF2 RID: 110578 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700976B RID: 38763
		// (get) Token: 0x0601AFF3 RID: 110579 RVA: 0x0036A7BD File Offset: 0x003689BD
		internal override string[] AttributeTagNames
		{
			get
			{
				return Matrix.attributeTagNames;
			}
		}

		// Token: 0x1700976C RID: 38764
		// (get) Token: 0x0601AFF4 RID: 110580 RVA: 0x0036A7C4 File Offset: 0x003689C4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Matrix.attributeNamespaceIds;
			}
		}

		// Token: 0x1700976D RID: 38765
		// (get) Token: 0x0601AFF5 RID: 110581 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601AFF6 RID: 110582 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(1, "id")]
		public StringValue Id
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

		// Token: 0x0601AFF7 RID: 110583 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Matrix()
		{
		}

		// Token: 0x0601AFF8 RID: 110584 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Matrix(string text)
			: base(text)
		{
		}

		// Token: 0x0601AFF9 RID: 110585 RVA: 0x0036A7CC File Offset: 0x003689CC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601AFFA RID: 110586 RVA: 0x0036A7E7 File Offset: 0x003689E7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AFFB RID: 110587 RVA: 0x0036A808 File Offset: 0x00368A08
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Matrix>(deep);
		}

		// Token: 0x0400B27C RID: 45692
		private const string tagName = "matrix";

		// Token: 0x0400B27D RID: 45693
		private const byte tagNsId = 43;

		// Token: 0x0400B27E RID: 45694
		internal const int ElementTypeIdConst = 12646;

		// Token: 0x0400B27F RID: 45695
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400B280 RID: 45696
		private static byte[] attributeNamespaceIds = new byte[] { 1 };
	}
}
