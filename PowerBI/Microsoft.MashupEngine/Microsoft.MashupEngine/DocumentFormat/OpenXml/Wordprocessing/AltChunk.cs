using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F0E RID: 12046
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AltChunkProperties))]
	internal class AltChunk : OpenXmlCompositeElement
	{
		// Token: 0x17008E24 RID: 36388
		// (get) Token: 0x06019B9F RID: 105375 RVA: 0x003545C4 File Offset: 0x003527C4
		public override string LocalName
		{
			get
			{
				return "altChunk";
			}
		}

		// Token: 0x17008E25 RID: 36389
		// (get) Token: 0x06019BA0 RID: 105376 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E26 RID: 36390
		// (get) Token: 0x06019BA1 RID: 105377 RVA: 0x003545CB File Offset: 0x003527CB
		internal override int ElementTypeId
		{
			get
			{
				return 11684;
			}
		}

		// Token: 0x06019BA2 RID: 105378 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008E27 RID: 36391
		// (get) Token: 0x06019BA3 RID: 105379 RVA: 0x003545D2 File Offset: 0x003527D2
		internal override string[] AttributeTagNames
		{
			get
			{
				return AltChunk.attributeTagNames;
			}
		}

		// Token: 0x17008E28 RID: 36392
		// (get) Token: 0x06019BA4 RID: 105380 RVA: 0x003545D9 File Offset: 0x003527D9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AltChunk.attributeNamespaceIds;
			}
		}

		// Token: 0x17008E29 RID: 36393
		// (get) Token: 0x06019BA5 RID: 105381 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019BA6 RID: 105382 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
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

		// Token: 0x06019BA7 RID: 105383 RVA: 0x00293ECF File Offset: 0x002920CF
		public AltChunk()
		{
		}

		// Token: 0x06019BA8 RID: 105384 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AltChunk(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BA9 RID: 105385 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AltChunk(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019BAA RID: 105386 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AltChunk(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019BAB RID: 105387 RVA: 0x003545E0 File Offset: 0x003527E0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "altChunkPr" == name)
			{
				return new AltChunkProperties();
			}
			return null;
		}

		// Token: 0x17008E2A RID: 36394
		// (get) Token: 0x06019BAC RID: 105388 RVA: 0x003545FB File Offset: 0x003527FB
		internal override string[] ElementTagNames
		{
			get
			{
				return AltChunk.eleTagNames;
			}
		}

		// Token: 0x17008E2B RID: 36395
		// (get) Token: 0x06019BAD RID: 105389 RVA: 0x00354602 File Offset: 0x00352802
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AltChunk.eleNamespaceIds;
			}
		}

		// Token: 0x17008E2C RID: 36396
		// (get) Token: 0x06019BAE RID: 105390 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008E2D RID: 36397
		// (get) Token: 0x06019BAF RID: 105391 RVA: 0x00354609 File Offset: 0x00352809
		// (set) Token: 0x06019BB0 RID: 105392 RVA: 0x00354612 File Offset: 0x00352812
		public AltChunkProperties AltChunkProperties
		{
			get
			{
				return base.GetElement<AltChunkProperties>(0);
			}
			set
			{
				base.SetElement<AltChunkProperties>(0, value);
			}
		}

		// Token: 0x06019BB1 RID: 105393 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019BB2 RID: 105394 RVA: 0x0035461C File Offset: 0x0035281C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AltChunk>(deep);
		}

		// Token: 0x0400AA50 RID: 43600
		private const string tagName = "altChunk";

		// Token: 0x0400AA51 RID: 43601
		private const byte tagNsId = 23;

		// Token: 0x0400AA52 RID: 43602
		internal const int ElementTypeIdConst = 11684;

		// Token: 0x0400AA53 RID: 43603
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x0400AA54 RID: 43604
		private static byte[] attributeNamespaceIds = new byte[] { 19 };

		// Token: 0x0400AA55 RID: 43605
		private static readonly string[] eleTagNames = new string[] { "altChunkPr" };

		// Token: 0x0400AA56 RID: 43606
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
