using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000101 RID: 257
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000D26 RID: 3366 RVA: 0x00033D32 File Offset: 0x00031F32
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D27 RID: 3367 RVA: 0x00033D41 File Offset: 0x00031F41
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D28 RID: 3368 RVA: 0x00033D49 File Offset: 0x00031F49
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

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D29 RID: 3369 RVA: 0x00033D5C File Offset: 0x00031F5C
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D2A RID: 3370 RVA: 0x00033D5F File Offset: 0x00031F5F
		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D2B RID: 3371 RVA: 0x00033D66 File Offset: 0x00031F66
		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x00033D6D File Offset: 0x00031F6D
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x00033D70 File Offset: 0x00031F70
		// (set) Token: 0x06000D2E RID: 3374 RVA: 0x00033D73 File Offset: 0x00031F73
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

		// Token: 0x06000D2F RID: 3375 RVA: 0x00033D7A File Offset: 0x00031F7A
		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x00033D81 File Offset: 0x00031F81
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000410 RID: 1040
		private readonly XObject _xmlObject;
	}
}
