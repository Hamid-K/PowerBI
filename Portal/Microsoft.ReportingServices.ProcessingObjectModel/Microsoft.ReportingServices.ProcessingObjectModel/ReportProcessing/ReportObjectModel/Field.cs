using System;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200000F RID: 15
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Field : MarshalByRefObject
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600002C RID: 44
		public abstract object Value { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600002D RID: 45
		public abstract bool IsMissing { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600002E RID: 46
		public abstract string UniqueName { get; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600002F RID: 47
		public abstract string BackgroundColor { get; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000030 RID: 48
		public abstract string Color { get; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000031 RID: 49
		public abstract string FontFamily { get; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000032 RID: 50
		public abstract string FontSize { get; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000033 RID: 51
		public abstract string FontWeight { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000034 RID: 52
		public abstract string FontStyle { get; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000035 RID: 53
		public abstract string TextDecoration { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000036 RID: 54
		public abstract string FormattedValue { get; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000037 RID: 55
		public abstract object Key { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000038 RID: 56
		public abstract int LevelNumber { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000039 RID: 57
		public abstract string ParentUniqueName { get; }

		// Token: 0x17000026 RID: 38
		[IndexerName("Properties")]
		public abstract object this[string key] { get; }
	}
}
