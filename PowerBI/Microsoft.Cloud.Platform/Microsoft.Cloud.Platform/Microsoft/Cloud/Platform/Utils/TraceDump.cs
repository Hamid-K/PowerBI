using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001D2 RID: 466
	public class TraceDump
	{
		// Token: 0x06000BED RID: 3053 RVA: 0x00029B32 File Offset: 0x00027D32
		public TraceDump()
		{
			this.m_lines = new List<string>();
			this.m_indentLevel = 0;
			this.m_indentString = TraceDump.s_indentStrings[0];
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00029B59 File Offset: 0x00027D59
		public TraceDump(string lines)
			: this()
		{
			this.Add(lines);
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x06000BEF RID: 3055 RVA: 0x00029B68 File Offset: 0x00027D68
		// (set) Token: 0x06000BF0 RID: 3056 RVA: 0x00029B70 File Offset: 0x00027D70
		public int Indent
		{
			get
			{
				return this.m_indentLevel;
			}
			set
			{
				if (value != this.m_indentLevel)
				{
					this.m_indentLevel = ((value >= 0) ? value : 0);
					if (this.m_indentLevel < TraceDump.s_indentStrings.Length)
					{
						this.m_indentString = TraceDump.s_indentStrings[this.m_indentLevel];
						return;
					}
					this.m_indentString = "{0," + this.m_indentLevel + "}{1}";
				}
			}
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00029BD6 File Offset: 0x00027DD6
		public void Add(string line)
		{
			this.AddImpl(line);
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00029BDF File Offset: 0x00027DDF
		public void Add(ITraceDumpable obj)
		{
			obj.Dump(this);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00029BE8 File Offset: 0x00027DE8
		public void AddNameValue(string name, Exception value)
		{
			if (value == null)
			{
				this.AddNameValue(name, null);
				return;
			}
			this.AddNameValue(name, value.Message);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00029C04 File Offset: 0x00027E04
		public void AddNameValue(string name, ITraceDumpable value)
		{
			if (value == null)
			{
				this.AddNameValue(name, null);
				return;
			}
			TraceDump traceDump = new TraceDump();
			value.Dump(traceDump);
			this.AddNameValue(name, traceDump);
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00029C34 File Offset: 0x00027E34
		public void AddNameValue(string name, object value)
		{
			ITraceDumpable traceDumpable = value as ITraceDumpable;
			if (traceDumpable != null)
			{
				this.AddNameValue(name, traceDumpable);
				return;
			}
			this.AddNameValueImpl(name, value);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00029C5C File Offset: 0x00027E5C
		private void AddNameValueImpl(string name, object value)
		{
			if (value == null)
			{
				this.AddImpl(name + "=(null)");
				return;
			}
			string[] array;
			if (value is TraceDump)
			{
				array = ((TraceDump)value).m_lines.ToArray();
			}
			else
			{
				array = value.ToString().Split(TraceDump.c_newline, StringSplitOptions.RemoveEmptyEntries);
			}
			if (array == null || array.Length == 0)
			{
				this.AddImpl(name + "=(null)");
				return;
			}
			this.AddImpl(name + "=" + array[0]);
			this.AddImpl(array.Skip(1));
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x00029CE7 File Offset: 0x00027EE7
		public void Add(TraceDump dump)
		{
			this.AddImpl(dump.m_lines);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00029CF5 File Offset: 0x00027EF5
		public void Add(IEnumerable<string> lines)
		{
			this.AddImpl(lines);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00029D00 File Offset: 0x00027F00
		public void WriteConsole()
		{
			foreach (string text in this.m_lines)
			{
				Console.WriteLine(text);
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00029D50 File Offset: 0x00027F50
		public void WriteDebug()
		{
			foreach (string text in this.m_lines)
			{
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x00029D9C File Offset: 0x00027F9C
		public void WriteStream(TextWriter writer)
		{
			foreach (string text in this.m_lines)
			{
				writer.WriteLine(text);
			}
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00029DF0 File Offset: 0x00027FF0
		public void WriteFatal()
		{
			foreach (string text in this.m_lines)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Fatal, "{0}", new object[] { text });
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00029E58 File Offset: 0x00028058
		public void WriteError()
		{
			foreach (string text in this.m_lines)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Error, "{0}", new object[] { text });
			}
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00029EC0 File Offset: 0x000280C0
		public void WriteWarn()
		{
			foreach (string text in this.m_lines)
			{
				TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "{0}", new object[] { text });
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000BFF RID: 3071 RVA: 0x00029F28 File Offset: 0x00028128
		public ICollection<string> Lines
		{
			get
			{
				return this.m_lines;
			}
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00029F30 File Offset: 0x00028130
		public static string Dump(string obj)
		{
			if (obj == null)
			{
				return "(null string)";
			}
			if (obj.Length == 0)
			{
				return "(empty string)";
			}
			return obj;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x00029F4A File Offset: 0x0002814A
		public static string Dump(Delegate obj)
		{
			if (obj == null)
			{
				return "(null delegate)";
			}
			return obj.Method.ToString();
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00029F60 File Offset: 0x00028160
		public override string ToString()
		{
			return this.m_lines.StringJoin(Environment.NewLine);
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x00029F74 File Offset: 0x00028174
		private void AddImpl(string line)
		{
			foreach (string text in line.Split(TraceDump.c_newline, StringSplitOptions.RemoveEmptyEntries))
			{
				if (this.m_lines.Count == 0)
				{
					this.m_lines.Add(text);
				}
				else
				{
					string text2 = string.Format(this.m_indentString, string.Empty, text);
					this.m_lines.Add(text2);
				}
			}
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00029FDC File Offset: 0x000281DC
		private void AddImpl(IEnumerable<string> lines)
		{
			foreach (string text in lines)
			{
				this.AddImpl(text);
			}
		}

		// Token: 0x04000494 RID: 1172
		private List<string> m_lines;

		// Token: 0x04000495 RID: 1173
		private int m_indentLevel;

		// Token: 0x04000496 RID: 1174
		private string m_indentString;

		// Token: 0x04000497 RID: 1175
		private static readonly char[] c_newline = Environment.NewLine.ToCharArray();

		// Token: 0x04000498 RID: 1176
		private static readonly string[] s_indentStrings = new string[] { "{1}", " {1}", "  {1}", "   {1}", "    {1}", "     {1}", "      {1}", "       {1}", "        {1}" };
	}
}
