using System;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D47 RID: 3399
	internal class ScopePath : IEquatable<ScopePath>
	{
		// Token: 0x06005B70 RID: 23408 RVA: 0x0013F095 File Offset: 0x0013D295
		public static ScopePath FromString(string path)
		{
			return new ScopePath(path.Split(new char[] { '.' }));
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x0013F0AD File Offset: 0x0013D2AD
		public ScopePath(string[] path)
		{
			this.Path = path;
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x06005B72 RID: 23410 RVA: 0x0013F0BC File Offset: 0x0013D2BC
		// (set) Token: 0x06005B73 RID: 23411 RVA: 0x0013F0C4 File Offset: 0x0013D2C4
		public string[] Path { get; private set; }

		// Token: 0x06005B74 RID: 23412 RVA: 0x0013F0D0 File Offset: 0x0013D2D0
		public ScopePath NewScope(string scope)
		{
			string[] array = new string[this.Path.Length + 1];
			array[0] = scope;
			for (int i = 0; i < this.Path.Length; i++)
			{
				array[i + 1] = this.Path[i];
			}
			return new ScopePath(array);
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x0013F117 File Offset: 0x0013D317
		public override bool Equals(object other)
		{
			return this.Equals(other as ScopePath);
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x0013F128 File Offset: 0x0013D328
		public bool Equals(ScopePath other)
		{
			bool flag = other != null && this.Path.Length == other.Path.Length;
			int num = 0;
			while (flag && num < this.Path.Length)
			{
				flag &= this.Path[num].Equals(other.Path[num]);
				num++;
			}
			return flag;
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x0013F180 File Offset: 0x0013D380
		public override int GetHashCode()
		{
			int num = this.Path.Length;
			for (int i = 0; i < this.Path.Length; i++)
			{
				num = num * 37 + this.Path[i].GetHashCode();
			}
			return num;
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x0013F1BD File Offset: 0x0013D3BD
		public override string ToString()
		{
			return string.Join(".", this.Path);
		}

		// Token: 0x040032EC RID: 13036
		public static readonly ScopePath Default = new ScopePath(EmptyArray<string>.Instance);
	}
}
