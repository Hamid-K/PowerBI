using System;
using System.Runtime.InteropServices;

namespace AngleSharp.Css
{
	// Token: 0x02000109 RID: 265
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode, Pack = 1)]
	public struct Priority : IEquatable<Priority>, IComparable<Priority>
	{
		// Token: 0x06000871 RID: 2161 RVA: 0x0003BD40 File Offset: 0x00039F40
		public Priority(uint priority)
		{
			this._inlines = (this._ids = (this._classes = (this._tags = 0)));
			this._priority = priority;
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0003BD76 File Offset: 0x00039F76
		public Priority(byte inlines, byte ids, byte classes, byte tags)
		{
			this._priority = 0U;
			this._inlines = inlines;
			this._ids = ids;
			this._classes = classes;
			this._tags = tags;
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x0003BD9C File Offset: 0x00039F9C
		public byte Tags
		{
			get
			{
				return this._tags;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x0003BDA4 File Offset: 0x00039FA4
		public byte Classes
		{
			get
			{
				return this._classes;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x0003BDAC File Offset: 0x00039FAC
		public byte Ids
		{
			get
			{
				return this._ids;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0003BDB4 File Offset: 0x00039FB4
		public byte Inlines
		{
			get
			{
				return this._inlines;
			}
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0003BDBC File Offset: 0x00039FBC
		public static Priority operator +(Priority a, Priority b)
		{
			return new Priority(a._priority + b._priority);
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		public static bool operator ==(Priority a, Priority b)
		{
			return a._priority == b._priority;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0003BDE0 File Offset: 0x00039FE0
		public static bool operator >(Priority a, Priority b)
		{
			return a._priority > b._priority;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0003BDF0 File Offset: 0x00039FF0
		public static bool operator >=(Priority a, Priority b)
		{
			return a._priority >= b._priority;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0003BE03 File Offset: 0x0003A003
		public static bool operator <(Priority a, Priority b)
		{
			return a._priority < b._priority;
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0003BE13 File Offset: 0x0003A013
		public static bool operator <=(Priority a, Priority b)
		{
			return a._priority <= b._priority;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0003BE26 File Offset: 0x0003A026
		public static bool operator !=(Priority a, Priority b)
		{
			return a._priority != b._priority;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0003BDD0 File Offset: 0x00039FD0
		public bool Equals(Priority other)
		{
			return this._priority == other._priority;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x0003BE39 File Offset: 0x0003A039
		public override bool Equals(object obj)
		{
			return obj is Priority && this.Equals((Priority)obj);
		}

		// Token: 0x06000880 RID: 2176 RVA: 0x0003BE51 File Offset: 0x0003A051
		public override int GetHashCode()
		{
			return (int)this._priority;
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0003BE59 File Offset: 0x0003A059
		public int CompareTo(Priority other)
		{
			if (this == other)
			{
				return 0;
			}
			if (!(this > other))
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x0003BE7C File Offset: 0x0003A07C
		public override string ToString()
		{
			return string.Format("({0}, {1}, {2}, {3})", new object[] { this._inlines, this._ids, this._classes, this._tags });
		}

		// Token: 0x04000722 RID: 1826
		[FieldOffset(0)]
		private readonly byte _tags;

		// Token: 0x04000723 RID: 1827
		[FieldOffset(1)]
		private readonly byte _classes;

		// Token: 0x04000724 RID: 1828
		[FieldOffset(2)]
		private readonly byte _ids;

		// Token: 0x04000725 RID: 1829
		[FieldOffset(3)]
		private readonly byte _inlines;

		// Token: 0x04000726 RID: 1830
		[FieldOffset(0)]
		private readonly uint _priority;

		// Token: 0x04000727 RID: 1831
		public static readonly Priority Zero = new Priority(0U);

		// Token: 0x04000728 RID: 1832
		public static readonly Priority OneTag = new Priority(0, 0, 0, 1);

		// Token: 0x04000729 RID: 1833
		public static readonly Priority OneClass = new Priority(0, 0, 1, 0);

		// Token: 0x0400072A RID: 1834
		public static readonly Priority OneId = new Priority(0, 1, 0, 0);

		// Token: 0x0400072B RID: 1835
		public static readonly Priority Inline = new Priority(1, 0, 0, 0);
	}
}
