using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A32 RID: 10802
	[ChildElementInfo(typeof(VariantValue))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TimeAnimateValue : OpenXmlCompositeElement
	{
		// Token: 0x170070D2 RID: 28882
		// (get) Token: 0x06015B24 RID: 88868 RVA: 0x003221A5 File Offset: 0x003203A5
		public override string LocalName
		{
			get
			{
				return "tav";
			}
		}

		// Token: 0x170070D3 RID: 28883
		// (get) Token: 0x06015B25 RID: 88869 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070D4 RID: 28884
		// (get) Token: 0x06015B26 RID: 88870 RVA: 0x003221AC File Offset: 0x003203AC
		internal override int ElementTypeId
		{
			get
			{
				return 12222;
			}
		}

		// Token: 0x06015B27 RID: 88871 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170070D5 RID: 28885
		// (get) Token: 0x06015B28 RID: 88872 RVA: 0x003221B3 File Offset: 0x003203B3
		internal override string[] AttributeTagNames
		{
			get
			{
				return TimeAnimateValue.attributeTagNames;
			}
		}

		// Token: 0x170070D6 RID: 28886
		// (get) Token: 0x06015B29 RID: 88873 RVA: 0x003221BA File Offset: 0x003203BA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TimeAnimateValue.attributeNamespaceIds;
			}
		}

		// Token: 0x170070D7 RID: 28887
		// (get) Token: 0x06015B2A RID: 88874 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015B2B RID: 88875 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "tm")]
		public StringValue Time
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

		// Token: 0x170070D8 RID: 28888
		// (get) Token: 0x06015B2C RID: 88876 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06015B2D RID: 88877 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "fmla")]
		public StringValue Fomula
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

		// Token: 0x06015B2E RID: 88878 RVA: 0x00293ECF File Offset: 0x002920CF
		public TimeAnimateValue()
		{
		}

		// Token: 0x06015B2F RID: 88879 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TimeAnimateValue(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B30 RID: 88880 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TimeAnimateValue(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B31 RID: 88881 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TimeAnimateValue(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015B32 RID: 88882 RVA: 0x003221C1 File Offset: 0x003203C1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "val" == name)
			{
				return new VariantValue();
			}
			return null;
		}

		// Token: 0x170070D9 RID: 28889
		// (get) Token: 0x06015B33 RID: 88883 RVA: 0x003221DC File Offset: 0x003203DC
		internal override string[] ElementTagNames
		{
			get
			{
				return TimeAnimateValue.eleTagNames;
			}
		}

		// Token: 0x170070DA RID: 28890
		// (get) Token: 0x06015B34 RID: 88884 RVA: 0x003221E3 File Offset: 0x003203E3
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TimeAnimateValue.eleNamespaceIds;
			}
		}

		// Token: 0x170070DB RID: 28891
		// (get) Token: 0x06015B35 RID: 88885 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170070DC RID: 28892
		// (get) Token: 0x06015B36 RID: 88886 RVA: 0x003221EA File Offset: 0x003203EA
		// (set) Token: 0x06015B37 RID: 88887 RVA: 0x003221F3 File Offset: 0x003203F3
		public VariantValue VariantValue
		{
			get
			{
				return base.GetElement<VariantValue>(0);
			}
			set
			{
				base.SetElement<VariantValue>(0, value);
			}
		}

		// Token: 0x06015B38 RID: 88888 RVA: 0x003221FD File Offset: 0x003203FD
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "tm" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fmla" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015B39 RID: 88889 RVA: 0x00322233 File Offset: 0x00320433
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TimeAnimateValue>(deep);
		}

		// Token: 0x06015B3A RID: 88890 RVA: 0x0032223C File Offset: 0x0032043C
		// Note: this type is marked as 'beforefieldinit'.
		static TimeAnimateValue()
		{
			byte[] array = new byte[2];
			TimeAnimateValue.attributeNamespaceIds = array;
			TimeAnimateValue.eleTagNames = new string[] { "val" };
			TimeAnimateValue.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x0400946F RID: 37999
		private const string tagName = "tav";

		// Token: 0x04009470 RID: 38000
		private const byte tagNsId = 24;

		// Token: 0x04009471 RID: 38001
		internal const int ElementTypeIdConst = 12222;

		// Token: 0x04009472 RID: 38002
		private static string[] attributeTagNames = new string[] { "tm", "fmla" };

		// Token: 0x04009473 RID: 38003
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009474 RID: 38004
		private static readonly string[] eleTagNames;

		// Token: 0x04009475 RID: 38005
		private static readonly byte[] eleNamespaceIds;
	}
}
