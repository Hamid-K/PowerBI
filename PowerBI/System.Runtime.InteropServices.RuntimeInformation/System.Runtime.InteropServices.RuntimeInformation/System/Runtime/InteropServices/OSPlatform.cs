using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000007 RID: 7
	public struct OSPlatform : IEquatable<OSPlatform>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000023BA File Offset: 0x000005BA
		public static OSPlatform Linux { get; } = new OSPlatform("LINUX");

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000023C1 File Offset: 0x000005C1
		public static OSPlatform OSX { get; } = new OSPlatform("OSX");

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000023C8 File Offset: 0x000005C8
		public static OSPlatform Windows { get; } = new OSPlatform("WINDOWS");

		// Token: 0x06000013 RID: 19 RVA: 0x000023CF File Offset: 0x000005CF
		private OSPlatform(string osPlatform)
		{
			if (osPlatform == null)
			{
				throw new ArgumentNullException("osPlatform");
			}
			if (osPlatform.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyValue, "osPlatform");
			}
			this._osPlatform = osPlatform;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023FE File Offset: 0x000005FE
		public static OSPlatform Create(string osPlatform)
		{
			return new OSPlatform(osPlatform);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002406 File Offset: 0x00000606
		public bool Equals(OSPlatform other)
		{
			return this.Equals(other._osPlatform);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002414 File Offset: 0x00000614
		internal bool Equals(string other)
		{
			return string.Equals(this._osPlatform, other, StringComparison.Ordinal);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002423 File Offset: 0x00000623
		public override bool Equals(object obj)
		{
			return obj is OSPlatform && this.Equals((OSPlatform)obj);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000243B File Offset: 0x0000063B
		public override int GetHashCode()
		{
			if (this._osPlatform != null)
			{
				return this._osPlatform.GetHashCode();
			}
			return 0;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002452 File Offset: 0x00000652
		public override string ToString()
		{
			return this._osPlatform ?? string.Empty;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002463 File Offset: 0x00000663
		public static bool operator ==(OSPlatform left, OSPlatform right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000246D File Offset: 0x0000066D
		public static bool operator !=(OSPlatform left, OSPlatform right)
		{
			return !(left == right);
		}

		// Token: 0x0400000F RID: 15
		private readonly string _osPlatform;
	}
}
