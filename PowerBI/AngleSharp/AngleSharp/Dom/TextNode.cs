using System;
using System.IO;
using System.Text;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000163 RID: 355
	internal sealed class TextNode : CharacterData, IText, ICharacterData, INode, IEventTarget, IMarkupFormattable, IChildNode, INonDocumentTypeChildNode
	{
		// Token: 0x06000CD6 RID: 3286 RVA: 0x00045EC4 File Offset: 0x000440C4
		internal TextNode(Document owner)
			: this(owner, string.Empty)
		{
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x00045ED2 File Offset: 0x000440D2
		internal TextNode(Document owner, string text)
			: base(owner, "#text", NodeType.Text, text)
		{
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000CD8 RID: 3288 RVA: 0x00045EE4 File Offset: 0x000440E4
		internal bool IsEmpty
		{
			get
			{
				for (int i = 0; i < base.Length; i++)
				{
					if (!base[i].IsSpaceCharacter())
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x00045F14 File Offset: 0x00044114
		public string Text
		{
			get
			{
				Node node = base.PreviousSibling;
				TextNode textNode = this;
				StringBuilder stringBuilder = Pool.NewStringBuilder();
				while (node is TextNode)
				{
					textNode = (TextNode)node;
					node = textNode.PreviousSibling;
				}
				do
				{
					stringBuilder.Append(textNode.Data);
					textNode = textNode.NextSibling as TextNode;
				}
				while (textNode != null);
				return stringBuilder.ToPool();
			}
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000CDA RID: 3290 RVA: 0x00045F6C File Offset: 0x0004416C
		public IElement AssignedSlot
		{
			get
			{
				IElement parentElement = base.ParentElement;
				if (parentElement.IsShadow())
				{
					return parentElement.ShadowRoot.GetAssignedSlot(null);
				}
				return null;
			}
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x00045F98 File Offset: 0x00044198
		public override INode Clone(bool deep = true)
		{
			TextNode textNode = new TextNode(base.Owner, base.Data);
			base.CloneNode(textNode, deep);
			return textNode;
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x00045FC0 File Offset: 0x000441C0
		public IText Split(int offset)
		{
			int length = base.Length;
			if (offset > length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			int num = length - offset;
			string text = base.Substring(offset, num);
			TextNode newNode = new TextNode(base.Owner, text);
			Node parent = base.Parent;
			Document owner = base.Owner;
			if (parent != null)
			{
				int index = this.Index();
				parent.InsertBefore(newNode, base.NextSibling);
				owner.ForEachRange((Range m) => m.Head == this && m.Start > offset, delegate(Range m)
				{
					m.StartWith(newNode, m.Start - offset);
				});
				owner.ForEachRange((Range m) => m.Tail == this && m.End > offset, delegate(Range m)
				{
					m.EndWith(newNode, m.End - offset);
				});
				owner.ForEachRange((Range m) => m.Head == parent && m.Start == index + 1, delegate(Range m)
				{
					m.StartWith(parent, m.Start + 1);
				});
				owner.ForEachRange((Range m) => m.Tail == parent && m.End == index + 1, delegate(Range m)
				{
					m.StartWith(parent, m.End + 1);
				});
			}
			base.Replace(offset, num, string.Empty);
			if (parent != null)
			{
				owner.ForEachRange((Range m) => m.Head == this && m.Start > offset, delegate(Range m)
				{
					m.StartWith(this, offset);
				});
				owner.ForEachRange((Range m) => m.Tail == this && m.End > offset, delegate(Range m)
				{
					m.EndWith(this, offset);
				});
			}
			return newNode;
		}

		// Token: 0x06000CDD RID: 3293 RVA: 0x00046180 File Offset: 0x00044380
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			if (base.Parent != null && (base.Parent.Flags & NodeFlags.LiteralText) == NodeFlags.LiteralText)
			{
				writer.Write(base.Data);
				return;
			}
			base.ToHtml(writer, formatter);
		}
	}
}
