using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;

namespace Microsoft.IdentityModel.Json.Converters
{
	// Token: 0x02000101 RID: 257
	[NullableContext(2)]
	[Nullable(0)]
	internal class XObjectWrapper : IXmlNode
	{
		// Token: 0x06000D1C RID: 3356 RVA: 0x00033BDA File Offset: 0x00031DDA
		public XObjectWrapper(XObject xmlObject)
		{
			this._xmlObject = xmlObject;
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x00033BE9 File Offset: 0x00031DE9
		public object WrappedNode
		{
			get
			{
				return this._xmlObject;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000D1E RID: 3358 RVA: 0x00033BF1 File Offset: 0x00031DF1
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

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000D1F RID: 3359 RVA: 0x00033C04 File Offset: 0x00031E04
		public virtual string LocalName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000D20 RID: 3360 RVA: 0x00033C07 File Offset: 0x00031E07
		[Nullable(1)]
		public virtual List<IXmlNode> ChildNodes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x00033C0E File Offset: 0x00031E0E
		[Nullable(1)]
		public virtual List<IXmlNode> Attributes
		{
			[NullableContext(1)]
			get
			{
				return XmlNodeConverter.EmptyChildNodes;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x00033C15 File Offset: 0x00031E15
		public virtual IXmlNode ParentNode
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000D23 RID: 3363 RVA: 0x00033C18 File Offset: 0x00031E18
		// (set) Token: 0x06000D24 RID: 3364 RVA: 0x00033C1B File Offset: 0x00031E1B
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

		// Token: 0x06000D25 RID: 3365 RVA: 0x00033C22 File Offset: 0x00031E22
		[NullableContext(1)]
		public virtual IXmlNode AppendChild(IXmlNode newChild)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x00033C29 File Offset: 0x00031E29
		public virtual string NamespaceUri
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400040F RID: 1039
		private readonly XObject _xmlObject;
	}
}
