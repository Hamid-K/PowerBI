using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.Html
{
	// Token: 0x02000AB9 RID: 2745
	internal sealed class DomNode
	{
		// Token: 0x06004CD1 RID: 19665 RVA: 0x000FD48C File Offset: 0x000FB68C
		private DomNode(string name, DomNodeKind kind, Dictionary<string, object> attributes, List<DomNode> children, string text)
		{
			this.name = name;
			this.kind = kind;
			this.attributes = attributes;
			this.children = children;
			this.text = text;
			this.@class = Lazy.New<string>(new Func<string>(this.GetClass));
			this.classes = Lazy.New<string[]>(new Func<string[]>(this.GetClasses));
			this.id = Lazy.New<string>(new Func<string>(this.GetId));
		}

		// Token: 0x06004CD2 RID: 19666 RVA: 0x000FD509 File Offset: 0x000FB709
		public static DomNode Deserialize(Stream stream)
		{
			return new DomNode.DomDeserializer(stream).Deserialize();
		}

		// Token: 0x17001828 RID: 6184
		// (get) Token: 0x06004CD3 RID: 19667 RVA: 0x000FD516 File Offset: 0x000FB716
		public List<DomNode> Children
		{
			get
			{
				return this.children;
			}
		}

		// Token: 0x17001829 RID: 6185
		// (get) Token: 0x06004CD4 RID: 19668 RVA: 0x000FD51E File Offset: 0x000FB71E
		public string Class
		{
			get
			{
				return this.@class.Value;
			}
		}

		// Token: 0x1700182A RID: 6186
		// (get) Token: 0x06004CD5 RID: 19669 RVA: 0x000FD52B File Offset: 0x000FB72B
		public string[] Classes
		{
			get
			{
				return this.classes.Value;
			}
		}

		// Token: 0x1700182B RID: 6187
		// (get) Token: 0x06004CD6 RID: 19670 RVA: 0x000FD538 File Offset: 0x000FB738
		public string Id
		{
			get
			{
				return this.id.Value;
			}
		}

		// Token: 0x1700182C RID: 6188
		// (get) Token: 0x06004CD7 RID: 19671 RVA: 0x000FD545 File Offset: 0x000FB745
		public DomNodeKind Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x1700182D RID: 6189
		// (get) Token: 0x06004CD8 RID: 19672 RVA: 0x000FD54D File Offset: 0x000FB74D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700182E RID: 6190
		// (get) Token: 0x06004CD9 RID: 19673 RVA: 0x000FD555 File Offset: 0x000FB755
		public string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x06004CDA RID: 19674 RVA: 0x000FD55D File Offset: 0x000FB75D
		public string GetClass()
		{
			return this.GetAttributeValue("class", null);
		}

		// Token: 0x06004CDB RID: 19675 RVA: 0x000FD56B File Offset: 0x000FB76B
		public string[] GetClasses()
		{
			if (this.Class == null)
			{
				return EmptyArray<string>.Instance;
			}
			return DomNode.whiteSpaceRegex.Split(this.Class.Trim());
		}

		// Token: 0x06004CDC RID: 19676 RVA: 0x000FD590 File Offset: 0x000FB790
		public string GetId()
		{
			return this.GetAttributeValue("id", null);
		}

		// Token: 0x06004CDD RID: 19677 RVA: 0x000FD5A0 File Offset: 0x000FB7A0
		public bool GetAttributeValue(string name, bool defaultValue)
		{
			object obj;
			if (this.attributes != null && this.attributes.TryGetValue(name, out obj) && obj is bool)
			{
				return (bool)obj;
			}
			return defaultValue;
		}

		// Token: 0x06004CDE RID: 19678 RVA: 0x000FD5D8 File Offset: 0x000FB7D8
		public int GetAttributeValue(string name, int defaultValue)
		{
			object obj;
			if (this.attributes != null && this.attributes.TryGetValue(name, out obj) && obj is int)
			{
				return (int)obj;
			}
			return defaultValue;
		}

		// Token: 0x06004CDF RID: 19679 RVA: 0x000FD610 File Offset: 0x000FB810
		public string GetAttributeValue(string name, string defaultValue)
		{
			object obj;
			if (this.attributes != null && this.attributes.TryGetValue(name, out obj) && obj is string)
			{
				return (string)obj;
			}
			return defaultValue;
		}

		// Token: 0x040028D5 RID: 10453
		private static readonly Regex whiteSpaceRegex = new Regex("\\s+", RegexOptions.Compiled);

		// Token: 0x040028D6 RID: 10454
		private readonly Dictionary<string, object> attributes;

		// Token: 0x040028D7 RID: 10455
		private readonly List<DomNode> children;

		// Token: 0x040028D8 RID: 10456
		private readonly DomNodeKind kind;

		// Token: 0x040028D9 RID: 10457
		private readonly string name;

		// Token: 0x040028DA RID: 10458
		private readonly string text;

		// Token: 0x040028DB RID: 10459
		private readonly Lazy<string> @class;

		// Token: 0x040028DC RID: 10460
		private readonly Lazy<string[]> classes;

		// Token: 0x040028DD RID: 10461
		private readonly Lazy<string> id;

		// Token: 0x02000ABA RID: 2746
		private class DomDeserializer
		{
			// Token: 0x06004CE1 RID: 19681 RVA: 0x000FD657 File Offset: 0x000FB857
			public DomDeserializer(Stream stream)
			{
				this.reader = new DomNode.BinaryTextReader(stream);
				this.nodes = new List<DomNode>();
			}

			// Token: 0x06004CE2 RID: 19682 RVA: 0x000FD678 File Offset: 0x000FB878
			public DomNode Deserialize()
			{
				DomNode domNode;
				for (;;)
				{
					string text = this.reader.ReadString();
					Dictionary<string, object> dictionary = this.ReadAttributes();
					List<DomNode> list = this.ReadChildren();
					domNode = new DomNode(text, DomNodeKind.Element, dictionary, list, null);
					if (this.reader.EndOfStream)
					{
						break;
					}
					this.nodes.Add(domNode);
				}
				return domNode;
			}

			// Token: 0x06004CE3 RID: 19683 RVA: 0x000FD6C4 File Offset: 0x000FB8C4
			private List<DomNode> ReadChildren()
			{
				int num = this.reader.ReadInt32();
				List<DomNode> list = new List<DomNode>(num);
				for (int i = 0; i < num; i++)
				{
					if (this.reader.ReadInt32() == 1)
					{
						int num2 = this.reader.ReadInt32();
						if (num2 != -1)
						{
							list.Add(this.nodes[num2]);
						}
					}
					else
					{
						string text = this.reader.ReadString();
						list.Add(new DomNode(null, DomNodeKind.Text, new Dictionary<string, object>(0), new List<DomNode>(0), text));
					}
				}
				return list;
			}

			// Token: 0x06004CE4 RID: 19684 RVA: 0x000FD74C File Offset: 0x000FB94C
			private Dictionary<string, object> ReadAttributes()
			{
				int num = this.reader.ReadInt32();
				Dictionary<string, object> dictionary = new Dictionary<string, object>(num);
				for (int i = 0; i < num; i++)
				{
					string text = this.reader.ReadString();
					object obj = this.ReadValue();
					dictionary.Add(text, obj);
				}
				return dictionary;
			}

			// Token: 0x06004CE5 RID: 19685 RVA: 0x000FD798 File Offset: 0x000FB998
			public object ReadValue()
			{
				switch (this.reader.ReadByte())
				{
				case 0:
					return null;
				case 1:
					return this.reader.ReadBoolean();
				case 2:
					return this.reader.ReadInt32();
				case 3:
					return this.reader.ReadString();
				default:
					throw new InvalidOperationException();
				}
			}

			// Token: 0x040028DE RID: 10462
			private readonly DomNode.BinaryTextReader reader;

			// Token: 0x040028DF RID: 10463
			private readonly List<DomNode> nodes;
		}

		// Token: 0x02000ABB RID: 2747
		private class BinaryTextReader
		{
			// Token: 0x06004CE6 RID: 19686 RVA: 0x000FD7FE File Offset: 0x000FB9FE
			public BinaryTextReader(Stream stream)
			{
				this.reader = new StreamReader(stream);
				this.chars = new char[10];
			}

			// Token: 0x1700182F RID: 6191
			// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x000FD81F File Offset: 0x000FBA1F
			public bool EndOfStream
			{
				get
				{
					return this.reader.EndOfStream;
				}
			}

			// Token: 0x06004CE8 RID: 19688 RVA: 0x000FD82C File Offset: 0x000FBA2C
			public int ReadInt32()
			{
				uint num = 0U;
				int num2;
				for (;;)
				{
					num2 = this.reader.Read();
					if (num2 == -1)
					{
						break;
					}
					if (num2 >= 65 && num2 < 75)
					{
						goto Block_3;
					}
					num = num * 10U + (uint)(num2 - 48);
				}
				throw new InvalidOperationException();
				Block_3:
				num = num * 10U + (uint)(num2 - 65);
				return (int)(num - 1U);
			}

			// Token: 0x06004CE9 RID: 19689 RVA: 0x000FD878 File Offset: 0x000FBA78
			public string ReadString()
			{
				if (this.ReadInt32() != 1)
				{
					return null;
				}
				int num = this.ReadInt32();
				if (this.chars.Length < num)
				{
					this.chars = new char[Math.Max(num, this.chars.Length * 2)];
				}
				if (num != this.reader.ReadBlock(this.chars, 0, num))
				{
					throw new InvalidOperationException();
				}
				return new string(this.chars, 0, num);
			}

			// Token: 0x06004CEA RID: 19690 RVA: 0x000FD8E6 File Offset: 0x000FBAE6
			public byte ReadByte()
			{
				return (byte)this.ReadInt32();
			}

			// Token: 0x06004CEB RID: 19691 RVA: 0x000FD8EF File Offset: 0x000FBAEF
			public bool ReadBoolean()
			{
				return this.reader.Read() == 43;
			}

			// Token: 0x040028E0 RID: 10464
			private readonly StreamReader reader;

			// Token: 0x040028E1 RID: 10465
			private char[] chars;
		}

		// Token: 0x02000ABC RID: 2748
		private enum Tag
		{
			// Token: 0x040028E3 RID: 10467
			Null,
			// Token: 0x040028E4 RID: 10468
			Boolean,
			// Token: 0x040028E5 RID: 10469
			Int32,
			// Token: 0x040028E6 RID: 10470
			String
		}
	}
}
