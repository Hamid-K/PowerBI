using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001340 RID: 4928
	public struct Import : IEquatable<Import>
	{
		// Token: 0x060081E6 RID: 33254 RVA: 0x001B91AD File Offset: 0x001B73AD
		public Import(string name)
		{
			this.section = null;
			this.name = name;
		}

		// Token: 0x060081E7 RID: 33255 RVA: 0x001B91BD File Offset: 0x001B73BD
		public Import(string section, string name)
		{
			this.section = section;
			this.name = name;
		}

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x060081E8 RID: 33256 RVA: 0x001B91CD File Offset: 0x001B73CD
		public string Section
		{
			get
			{
				return this.section;
			}
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x060081E9 RID: 33257 RVA: 0x001B91D5 File Offset: 0x001B73D5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x060081EA RID: 33258 RVA: 0x001B91DD File Offset: 0x001B73DD
		public override int GetHashCode()
		{
			return this.name.GetHashCode() ^ ((this.section != null) ? this.section.GetHashCode() : 0);
		}

		// Token: 0x060081EB RID: 33259 RVA: 0x001B9201 File Offset: 0x001B7401
		public override bool Equals(object obj)
		{
			return obj is Import && this.Equals((Import)obj);
		}

		// Token: 0x060081EC RID: 33260 RVA: 0x001B9219 File Offset: 0x001B7419
		public bool Equals(Import other)
		{
			return this.name == other.name && this.section == other.section;
		}

		// Token: 0x060081ED RID: 33261 RVA: 0x001B9241 File Offset: 0x001B7441
		public static bool operator ==(Import left, Import right)
		{
			return left.Equals(right);
		}

		// Token: 0x060081EE RID: 33262 RVA: 0x001B924B File Offset: 0x001B744B
		public static bool operator !=(Import left, Import right)
		{
			return !left.Equals(right);
		}

		// Token: 0x040046A3 RID: 18083
		private string section;

		// Token: 0x040046A4 RID: 18084
		private string name;
	}
}
