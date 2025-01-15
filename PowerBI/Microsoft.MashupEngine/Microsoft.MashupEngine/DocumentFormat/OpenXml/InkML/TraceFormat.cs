using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x0200308E RID: 12430
	[ChildElementInfo(typeof(Channel))]
	[ChildElementInfo(typeof(IntermittentChannels))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TraceFormat : OpenXmlCompositeElement
	{
		// Token: 0x17009790 RID: 38800
		// (get) Token: 0x0601B04D RID: 110669 RVA: 0x0036AC6B File Offset: 0x00368E6B
		public override string LocalName
		{
			get
			{
				return "traceFormat";
			}
		}

		// Token: 0x17009791 RID: 38801
		// (get) Token: 0x0601B04E RID: 110670 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x17009792 RID: 38802
		// (get) Token: 0x0601B04F RID: 110671 RVA: 0x0036AC72 File Offset: 0x00368E72
		internal override int ElementTypeId
		{
			get
			{
				return 12651;
			}
		}

		// Token: 0x0601B050 RID: 110672 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009793 RID: 38803
		// (get) Token: 0x0601B051 RID: 110673 RVA: 0x0036AC79 File Offset: 0x00368E79
		internal override string[] AttributeTagNames
		{
			get
			{
				return TraceFormat.attributeTagNames;
			}
		}

		// Token: 0x17009794 RID: 38804
		// (get) Token: 0x0601B052 RID: 110674 RVA: 0x0036AC80 File Offset: 0x00368E80
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TraceFormat.attributeNamespaceIds;
			}
		}

		// Token: 0x17009795 RID: 38805
		// (get) Token: 0x0601B053 RID: 110675 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601B054 RID: 110676 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601B055 RID: 110677 RVA: 0x00293ECF File Offset: 0x002920CF
		public TraceFormat()
		{
		}

		// Token: 0x0601B056 RID: 110678 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TraceFormat(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B057 RID: 110679 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TraceFormat(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601B058 RID: 110680 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TraceFormat(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601B059 RID: 110681 RVA: 0x0036AC87 File Offset: 0x00368E87
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (43 == namespaceId && "channel" == name)
			{
				return new Channel();
			}
			if (43 == namespaceId && "intermittentChannels" == name)
			{
				return new IntermittentChannels();
			}
			return null;
		}

		// Token: 0x0601B05A RID: 110682 RVA: 0x0036A7E7 File Offset: 0x003689E7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (1 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B05B RID: 110683 RVA: 0x0036ACBA File Offset: 0x00368EBA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TraceFormat>(deep);
		}

		// Token: 0x0400B293 RID: 45715
		private const string tagName = "traceFormat";

		// Token: 0x0400B294 RID: 45716
		private const byte tagNsId = 43;

		// Token: 0x0400B295 RID: 45717
		internal const int ElementTypeIdConst = 12651;

		// Token: 0x0400B296 RID: 45718
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400B297 RID: 45719
		private static byte[] attributeNamespaceIds = new byte[] { 1 };
	}
}
