using System;
using System.Runtime.CompilerServices;

namespace ParquetSharp
{
	// Token: 0x0200000D RID: 13
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ApplicationVersion
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002378 File Offset: 0x00000578
		internal ApplicationVersion(ApplicationVersion.CStruct cstruct)
		{
			this.Application = StringUtil.PtrToStringUtf8(cstruct.Application);
			this.Build = StringUtil.PtrToStringUtf8(cstruct.Build);
			this.Major = cstruct.Major;
			this.Minor = cstruct.Minor;
			this.Patch = cstruct.Patch;
			this.Unknown = StringUtil.PtrToStringUtf8(cstruct.Unknown);
			this.PreRelease = StringUtil.PtrToStringUtf8(cstruct.PreRelease);
			this.BuildInfo = StringUtil.PtrToStringUtf8(cstruct.BuildInfo);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002408 File Offset: 0x00000608
		public override string ToString()
		{
			return string.Format("{0} version {1}.{2}.{3}", new object[] { this.Application, this.Major, this.Minor, this.Patch });
		}

		// Token: 0x0400000B RID: 11
		public readonly string Application;

		// Token: 0x0400000C RID: 12
		public readonly string Build;

		// Token: 0x0400000D RID: 13
		public readonly int Major;

		// Token: 0x0400000E RID: 14
		public readonly int Minor;

		// Token: 0x0400000F RID: 15
		public readonly int Patch;

		// Token: 0x04000010 RID: 16
		public readonly string Unknown;

		// Token: 0x04000011 RID: 17
		public readonly string PreRelease;

		// Token: 0x04000012 RID: 18
		public readonly string BuildInfo;

		// Token: 0x020000F9 RID: 249
		[NullableContext(0)]
		internal readonly struct CStruct
		{
			// Token: 0x040002B7 RID: 695
			public readonly IntPtr Application;

			// Token: 0x040002B8 RID: 696
			public readonly IntPtr Build;

			// Token: 0x040002B9 RID: 697
			public readonly int Major;

			// Token: 0x040002BA RID: 698
			public readonly int Minor;

			// Token: 0x040002BB RID: 699
			public readonly int Patch;

			// Token: 0x040002BC RID: 700
			public readonly IntPtr Unknown;

			// Token: 0x040002BD RID: 701
			public readonly IntPtr PreRelease;

			// Token: 0x040002BE RID: 702
			public readonly IntPtr BuildInfo;
		}
	}
}
