using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.Identity.Json.Converters
{
	// Token: 0x02000100 RID: 256
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000D0C RID: 3340 RVA: 0x0003340E File Offset: 0x0003160E
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x0003341D File Offset: 0x0003161D
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00033425 File Offset: 0x00031625
		public virtual XmlNodeType NodeType
		{
			get
			{
				XObject xmlObject = this._xmlObject;
				if (xmlObject == null)
				{
					return XmlNodeType.None;
				}
				return xmlObject.NodeType;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00033438 File Offset: 0x00031638
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x0003343B File Offset: 0x0003163B
		[Nullable(0)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(0)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00033442 File Offset: 0x00031642
		[Nullable(0)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(0)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00033449 File Offset: 0x00031649
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0003344C File Offset: 0x0003164C
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x0003344F File Offset: 0x0003164F
		public virtual string Value
		{
			get
			{
				return null;
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x00033456 File Offset: 0x00031656
		[NullableContext(0)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x0003345D File Offset: 0x0003165D
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040003F2 RID: 1010
		private readonly XObject _xmlObject;
	}
}
