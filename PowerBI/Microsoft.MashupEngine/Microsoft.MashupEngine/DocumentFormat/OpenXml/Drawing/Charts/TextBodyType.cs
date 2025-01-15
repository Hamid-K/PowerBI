using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002505 RID: 9477
	[ChildElementInfo(typeof(BodyProperties))]
	[ChildElementInfo(typeof(ListStyle))]
	[ChildElementInfo(typeof(Paragraph))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TextBodyType : OpenXmlCompositeElement
	{
		// Token: 0x06011A37 RID: 72247 RVA: 0x002F1044 File Offset: 0x002EF244
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "bodyPr" == name)
			{
				return new BodyProperties();
			}
			if (10 == namespaceId && "lstStyle" == name)
			{
				return new ListStyle();
			}
			if (10 == namespaceId && "p" == name)
			{
				return new Paragraph();
			}
			return null;
		}

		// Token: 0x17005423 RID: 21539
		// (get) Token: 0x06011A38 RID: 72248 RVA: 0x002F109A File Offset: 0x002EF29A
		internal override string[] ElementTagNames
		{
			get
			{
				return TextBodyType.eleTagNames;
			}
		}

		// Token: 0x17005424 RID: 21540
		// (get) Token: 0x06011A39 RID: 72249 RVA: 0x002F10A1 File Offset: 0x002EF2A1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextBodyType.eleNamespaceIds;
			}
		}

		// Token: 0x17005425 RID: 21541
		// (get) Token: 0x06011A3A RID: 72250 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005426 RID: 21542
		// (get) Token: 0x06011A3B RID: 72251 RVA: 0x002DF0E8 File Offset: 0x002DD2E8
		// (set) Token: 0x06011A3C RID: 72252 RVA: 0x002DF0F1 File Offset: 0x002DD2F1
		public BodyProperties BodyProperties
		{
			get
			{
				return base.GetElement<BodyProperties>(0);
			}
			set
			{
				base.SetElement<BodyProperties>(0, value);
			}
		}

		// Token: 0x17005427 RID: 21543
		// (get) Token: 0x06011A3D RID: 72253 RVA: 0x002DF0FB File Offset: 0x002DD2FB
		// (set) Token: 0x06011A3E RID: 72254 RVA: 0x002DF104 File Offset: 0x002DD304
		public ListStyle ListStyle
		{
			get
			{
				return base.GetElement<ListStyle>(1);
			}
			set
			{
				base.SetElement<ListStyle>(1, value);
			}
		}

		// Token: 0x06011A3F RID: 72255 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TextBodyType()
		{
		}

		// Token: 0x06011A40 RID: 72256 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TextBodyType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A41 RID: 72257 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TextBodyType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011A42 RID: 72258 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TextBodyType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007BA3 RID: 31651
		private static readonly string[] eleTagNames = new string[] { "bodyPr", "lstStyle", "p" };

		// Token: 0x04007BA4 RID: 31652
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10 };
	}
}
