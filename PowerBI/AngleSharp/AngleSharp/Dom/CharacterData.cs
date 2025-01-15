using System;
using System.IO;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x0200014A RID: 330
	internal abstract class CharacterData : Node, ICharacterData, INode, IEventTarget, IMarkupFormattable, IChildNode, INonDocumentTypeChildNode
	{
		// Token: 0x06000A0B RID: 2571 RVA: 0x00040925 File Offset: 0x0003EB25
		internal CharacterData(Document owner, string name, NodeType type)
			: this(owner, name, type, string.Empty)
		{
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x00040935 File Offset: 0x0003EB35
		internal CharacterData(Document owner, string name, NodeType type, string content)
			: base(owner, name, type, NodeFlags.None)
		{
			this._content = content;
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x0004094C File Offset: 0x0003EB4C
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

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x000409BC File Offset: 0x0003EBBC
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

		// Token: 0x17000192 RID: 402
		internal char this[int index]
		{
			get
			{
				return this._content[index];
			}
			set
			{
				if (index >= 0)
				{
					if (index >= this.Length)
					{
						this._content = this._content.PadRight(index) + value.ToString();
						return;
					}
					char[] array = this._content.ToCharArray();
					array[index] = value;
					this._content = new string(array);
				}
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000A11 RID: 2577 RVA: 0x00040A8D File Offset: 0x0003EC8D
		public int Length
		{
			get
			{
				return this._content.Length;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000A12 RID: 2578 RVA: 0x00040A9A File Offset: 0x0003EC9A
		// (set) Token: 0x06000A13 RID: 2579 RVA: 0x00040AA2 File Offset: 0x0003ECA2
		public sealed override string NodeValue
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000A14 RID: 2580 RVA: 0x00040A9A File Offset: 0x0003EC9A
		// (set) Token: 0x06000A15 RID: 2581 RVA: 0x00040AA2 File Offset: 0x0003ECA2
		public sealed override string TextContent
		{
			get
			{
				return this.Data;
			}
			set
			{
				this.Data = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x00040AAB File Offset: 0x0003ECAB
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x00040AB3 File Offset: 0x0003ECB3
		public string Data
		{
			get
			{
				return this._content;
			}
			set
			{
				this.Replace(0, this.Length, value);
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00040AC4 File Offset: 0x0003ECC4
		public string Substring(int offset, int count)
		{
			int length = this._content.Length;
			if (offset > length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			if (offset + count > length)
			{
				return this._content.Substring(offset);
			}
			return this._content.Substring(offset, count);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x00040B08 File Offset: 0x0003ED08
		public void Append(string value)
		{
			this.Replace(this._content.Length, 0, value);
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x00040B1D File Offset: 0x0003ED1D
		public void Insert(int offset, string data)
		{
			this.Replace(offset, 0, data);
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x00040B28 File Offset: 0x0003ED28
		public void Delete(int offset, int count)
		{
			this.Replace(offset, count, string.Empty);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x00040B38 File Offset: 0x0003ED38
		public void Replace(int offset, int count, string data)
		{
			Document owner = base.Owner;
			int length = this._content.Length;
			if (offset > length)
			{
				throw new DomException(DomError.IndexSizeError);
			}
			if (offset + count > length)
			{
				count = length - offset;
			}
			owner.QueueMutation(MutationRecord.CharacterData(this, this._content));
			int num = offset + data.Length;
			this._content = this._content.Insert(offset, data).Remove(num, count);
			owner.ForEachRange((Range m) => m.Head == this && m.Start > offset && m.Start <= offset + count, delegate(Range m)
			{
				m.StartWith(this, offset);
			});
			owner.ForEachRange((Range m) => m.Tail == this && m.End > offset && m.End <= offset + count, delegate(Range m)
			{
				m.EndWith(this, offset);
			});
			owner.ForEachRange((Range m) => m.Head == this && m.Start > offset + count, delegate(Range m)
			{
				m.StartWith(this, m.Start + data.Length - count);
			});
			owner.ForEachRange((Range m) => m.Tail == this && m.End > offset + count, delegate(Range m)
			{
				m.EndWith(this, m.End + data.Length - count);
			});
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00040C6C File Offset: 0x0003EE6C
		public override void ToHtml(TextWriter writer, IMarkupFormatter formatter)
		{
			writer.Write(formatter.Text(this._content));
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00040C80 File Offset: 0x0003EE80
		public void Before(params INode[] nodes)
		{
			this.InsertBefore(nodes);
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x00040C89 File Offset: 0x0003EE89
		public void After(params INode[] nodes)
		{
			this.InsertAfter(nodes);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00040C92 File Offset: 0x0003EE92
		public void Replace(params INode[] nodes)
		{
			this.ReplaceWith(nodes);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00040C9B File Offset: 0x0003EE9B
		public void Remove()
		{
			this.RemoveFromParent();
		}

		// Token: 0x0400090D RID: 2317
		private string _content;
	}
}
