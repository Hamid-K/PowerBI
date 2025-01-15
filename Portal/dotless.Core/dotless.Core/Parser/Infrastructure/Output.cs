using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x0200005B RID: 91
	public class Output
	{
		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00014AB7 File Offset: 0x00012CB7
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00014ABF File Offset: 0x00012CBF
		private Env Env { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00014AC8 File Offset: 0x00012CC8
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00014AD0 File Offset: 0x00012CD0
		private StringBuilder Builder { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00014AD9 File Offset: 0x00012CD9
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00014AE1 File Offset: 0x00012CE1
		private Stack<StringBuilder> BuilderStack { get; set; }

		// Token: 0x060003EA RID: 1002 RVA: 0x00014AEA File Offset: 0x00012CEA
		public Output(Env env)
		{
			this.Env = env;
			this.BuilderStack = new Stack<StringBuilder>();
			this.Push();
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00014B0B File Offset: 0x00012D0B
		public Output Push()
		{
			this.Builder = new StringBuilder();
			this.BuilderStack.Push(this.Builder);
			return this;
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00014B2A File Offset: 0x00012D2A
		public StringBuilder Pop()
		{
			if (this.BuilderStack.Count == 1)
			{
				throw new InvalidOperationException();
			}
			StringBuilder stringBuilder = this.BuilderStack.Pop();
			this.Builder = this.BuilderStack.Peek();
			return stringBuilder;
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00014B5C File Offset: 0x00012D5C
		public void Reset(string s)
		{
			this.Builder = new StringBuilder(s);
			this.BuilderStack.Pop();
			this.BuilderStack.Push(this.Builder);
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x00014B87 File Offset: 0x00012D87
		public Output PopAndAppend()
		{
			return this.Append(this.Pop());
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x00014B98 File Offset: 0x00012D98
		public Output Append(Node node)
		{
			if (node != null)
			{
				if (node.PreComments)
				{
					node.PreComments.AppendCSS(this.Env);
				}
				node.AppendCSS(this.Env);
				if (node.PostComments)
				{
					node.PostComments.AppendCSS(this.Env);
				}
			}
			return this;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x00014BF1 File Offset: 0x00012DF1
		public Output Append(string s)
		{
			this.Builder.Append(s);
			return this;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00014C01 File Offset: 0x00012E01
		public Output Append(char? s)
		{
			this.Builder.Append(s);
			return this;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00014C16 File Offset: 0x00012E16
		public Output Append(StringBuilder sb)
		{
			this.Builder.Append(sb);
			return this;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x00014C26 File Offset: 0x00012E26
		public Output AppendMany<TNode>(IEnumerable<TNode> nodes) where TNode : Node
		{
			return this.AppendMany<TNode>(nodes, null);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x00014C30 File Offset: 0x00012E30
		public Output AppendMany<TNode>(IEnumerable<TNode> nodes, string join) where TNode : Node
		{
			return this.AppendMany<TNode>(nodes, delegate(TNode n)
			{
				this.Env.Output.Append(n);
			}, join);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00014C46 File Offset: 0x00012E46
		public Output AppendMany(IEnumerable<string> list, string join)
		{
			return this.AppendMany<string>(list, delegate(string item, StringBuilder sb)
			{
				sb.Append(item);
			}, join);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00014C70 File Offset: 0x00012E70
		public Output AppendMany<T>(IEnumerable<T> list, Func<T, string> toString, string join)
		{
			return this.AppendMany<T>(list, delegate(T item, StringBuilder sb)
			{
				sb.Append(toString(item));
			}, join);
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00014CA0 File Offset: 0x00012EA0
		public Output AppendMany<T>(IEnumerable<T> list, Action<T> toString, string join)
		{
			return this.AppendMany<T>(list, delegate(T item, StringBuilder sb)
			{
				toString(item);
			}, join);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00014CD0 File Offset: 0x00012ED0
		public Output AppendMany<T>(IEnumerable<T> list, Action<T, StringBuilder> toString, string join)
		{
			bool flag = true;
			bool flag2 = !string.IsNullOrEmpty(join);
			foreach (T t in list)
			{
				if (!flag && flag2)
				{
					this.Builder.Append(join);
				}
				flag = false;
				toString(t, this.Builder);
			}
			return this;
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00014D44 File Offset: 0x00012F44
		public Output AppendMany(IEnumerable<StringBuilder> buildersToAppend)
		{
			return this.AppendMany(buildersToAppend, null);
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00014D4E File Offset: 0x00012F4E
		public Output AppendMany(IEnumerable<StringBuilder> buildersToAppend, string join)
		{
			return this.AppendMany<StringBuilder>(buildersToAppend, delegate(StringBuilder b, StringBuilder output)
			{
				output.Append(b);
			}, join);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00014D77 File Offset: 0x00012F77
		public Output AppendFormat(string format, params object[] values)
		{
			return this.AppendFormat(CultureInfo.InvariantCulture, format, values);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00014D86 File Offset: 0x00012F86
		public Output AppendFormat(IFormatProvider formatProvider, string format, params object[] values)
		{
			this.Builder.AppendFormat(formatProvider, format, values);
			return this;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00014D98 File Offset: 0x00012F98
		public Output Indent(int amount)
		{
			if (amount > 0)
			{
				string text = new string(' ', amount);
				this.Builder.Replace("\n", "\n" + text);
				this.Builder.Insert(0, text);
			}
			return this;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00014DE0 File Offset: 0x00012FE0
		public Output Trim()
		{
			return this.TrimLeft(null).TrimRight(null);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00014E0C File Offset: 0x0001300C
		public Output TrimLeft(char? c)
		{
			int num = 0;
			int length = this.Builder.Length;
			if (length == 0)
			{
				return this;
			}
			while (num < length && ((c != null) ? (this.Builder[num] == c.Value) : char.IsWhiteSpace(this.Builder[num])))
			{
				num++;
			}
			if (num > 0)
			{
				this.Builder.Remove(0, num);
			}
			return this;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00014E7C File Offset: 0x0001307C
		public Output TrimRight(char? c)
		{
			int num = 0;
			int length = this.Builder.Length;
			if (length == 0)
			{
				return this;
			}
			while (num < length && ((c != null) ? (this.Builder[length - (num + 1)] == c.Value) : char.IsWhiteSpace(this.Builder[length - (num + 1)])))
			{
				num++;
			}
			if (num > 0)
			{
				this.Builder.Remove(length - num, num);
			}
			return this;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00014EF4 File Offset: 0x000130F4
		public override string ToString()
		{
			return this.Builder.ToString();
		}
	}
}
