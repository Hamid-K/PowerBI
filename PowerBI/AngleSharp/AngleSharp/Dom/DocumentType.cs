using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x0200014E RID: 334
	internal sealed class DocumentType : Node, IDocumentType, INode, IEventTarget, IMarkupFormattable, IChildNode
	{
		// Token: 0x06000B3B RID: 2875 RVA: 0x00042A62 File Offset: 0x00040C62
		internal DocumentType(Document owner, string name)
			: base(owner, name, NodeType.DocumentType, NodeFlags.None)
		{
			this.PublicIdentifier = string.Empty;
			this.SystemIdentifier = string.Empty;
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00042A88 File Offset: 0x00040C88
		public IElement PreviousElementSibling
		{
			get
			{
				Node parent = base.Parent;
				if (parent != null)
				{
					bool flag = false;
					for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
					{
						if (parent.ChildNodes[i] == this)
						{
							flag = true;
						}
						else if (flag && parent.ChildNodes[i] is IElement)
						{
							return (IElement)parent.ChildNodes[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000B3D RID: 2877 RVA: 0x00042AF8 File Offset: 0x00040CF8
		public IElement NextElementSibling
		{
			get
			{
				Node parent = base.Parent;
				if (parent != null)
				{
					int length = parent.ChildNodes.Length;
					bool flag = false;
					for (int i = 0; i < length; i++)
					{
						if (parent.ChildNodes[i] == this)
						{
							flag = true;
						}
						else if (flag && parent.ChildNodes[i] is IElement)
						{
							return (IElement)parent.ChildNodes[i];
						}
					}
				}
				return null;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00042B65 File Offset: 0x00040D65
		public IEnumerable<Entity> Entities
		{
			get
			{
				return Enumerable.Empty<Entity>();
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000B3F RID: 2879 RVA: 0x00042B6C File Offset: 0x00040D6C
		public IEnumerable<Notation> Notations
		{
			get
			{
				return Enumerable.Empty<Notation>();
			}
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000B40 RID: 2880 RVA: 0x00042B73 File Offset: 0x00040D73
		public string Name
		{
			get
			{
				return base.NodeName;
			}
		}

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x00042B7B File Offset: 0x00040D7B
		// (set) Token: 0x06000B42 RID: 2882 RVA: 0x00042B83 File Offset: 0x00040D83
		public string PublicIdentifier { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000B43 RID: 2883 RVA: 0x00042B8C File Offset: 0x00040D8C
		// (set) Token: 0x06000B44 RID: 2884 RVA: 0x00042B94 File Offset: 0x00040D94
		public string SystemIdentifier { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000B45 RID: 2885 RVA: 0x00042B9D File Offset: 0x00040D9D
		// (set) Token: 0x06000B46 RID: 2886 RVA: 0x00042BA5 File Offset: 0x00040DA5
		public string InternalSubset { get; set; }

		// Token: 0x06000B47 RID: 2887 RVA: 0x00042BB0 File Offset: 0x00040DB0
		public override INode Clone(bool deep = true)
		{
			DocumentType documentType = new DocumentType(base.Owner, this.Name)
			{
				PublicIdentifier = this.PublicIdentifier,
				SystemIdentifier = this.SystemIdentifier,
				InternalSubset = this.InternalSubset
			};
			base.CloneNode(documentType, deep);
			return documentType;
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x00040C80 File Offset: 0x0003EE80
		public void Before(params INode[] nodes)
		{
			this.InsertBefore(nodes);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x00040C89 File Offset: 0x0003EE89
		public void After(params INode[] nodes)
		{
			this.InsertAfter(nodes);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x00040C92 File Offset: 0x0003EE92
		public void Replace(params INode[] nodes)
		{
			this.ReplaceWith(nodes);
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x00040C9B File Offset: 0x0003EE9B
		public void Remove()
		{
			this.RemoveFromParent();
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x00042BFC File Offset: 0x00040DFC
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			writer.Write(formatter.Doctype(this));
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0000C295 File Offset: 0x0000A495
		protected override string LocateNamespace(string prefix)
		{
			return null;
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0000C295 File Offset: 0x0000A495
		protected override string LocatePrefix(string namespaceUri)
		{
			return null;
		}
	}
}
