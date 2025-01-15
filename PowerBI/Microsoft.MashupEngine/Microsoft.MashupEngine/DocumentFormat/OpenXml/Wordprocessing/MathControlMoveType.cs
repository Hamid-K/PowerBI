using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EB5 RID: 11957
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunProperties))]
	[ChildElementInfo(typeof(InsertedMathControl))]
	[ChildElementInfo(typeof(DeletedMathControl))]
	internal abstract class MathControlMoveType : OpenXmlCompositeElement
	{
		// Token: 0x17008C16 RID: 35862
		// (get) Token: 0x0601972A RID: 104234 RVA: 0x0034A2BC File Offset: 0x003484BC
		internal override string[] AttributeTagNames
		{
			get
			{
				return MathControlMoveType.attributeTagNames;
			}
		}

		// Token: 0x17008C17 RID: 35863
		// (get) Token: 0x0601972B RID: 104235 RVA: 0x0034A2C3 File Offset: 0x003484C3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MathControlMoveType.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C18 RID: 35864
		// (get) Token: 0x0601972C RID: 104236 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601972D RID: 104237 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17008C19 RID: 35865
		// (get) Token: 0x0601972E RID: 104238 RVA: 0x002EC18F File Offset: 0x002EA38F
		// (set) Token: 0x0601972F RID: 104239 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17008C1A RID: 35866
		// (get) Token: 0x06019730 RID: 104240 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06019731 RID: 104241 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06019732 RID: 104242 RVA: 0x0034A2CC File Offset: 0x003484CC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPr" == name)
			{
				return new RunProperties();
			}
			if (23 == namespaceId && "ins" == name)
			{
				return new InsertedMathControl();
			}
			if (23 == namespaceId && "del" == name)
			{
				return new DeletedMathControl();
			}
			return null;
		}

		// Token: 0x06019733 RID: 104243 RVA: 0x0034A324 File Offset: 0x00348524
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

		// Token: 0x06019734 RID: 104244 RVA: 0x00293ECF File Offset: 0x002920CF
		protected MathControlMoveType()
		{
		}

		// Token: 0x06019735 RID: 104245 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected MathControlMoveType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019736 RID: 104246 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected MathControlMoveType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019737 RID: 104247 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected MathControlMoveType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400A8E6 RID: 43238
		private static string[] attributeTagNames = new string[] { "author", "date", "id" };

		// Token: 0x0400A8E7 RID: 43239
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
