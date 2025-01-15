using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000004 RID: 4
	internal sealed class WrappedGeoJsonWriter : GeoJsonWriterBase
	{
		// Token: 0x0600003A RID: 58 RVA: 0x000027BF File Offset: 0x000009BF
		public WrappedGeoJsonWriter(IGeoJsonWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027CE File Offset: 0x000009CE
		protected override void StartObjectScope()
		{
			this.writer.StartObjectScope();
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000027DB File Offset: 0x000009DB
		protected override void StartArrayScope()
		{
			this.writer.StartArrayScope();
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000027E8 File Offset: 0x000009E8
		protected override void AddPropertyName(string name)
		{
			this.writer.AddPropertyName(name);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000027F6 File Offset: 0x000009F6
		protected override void AddValue(string value)
		{
			this.writer.AddValue(value);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002804 File Offset: 0x00000A04
		protected override void AddValue(double value)
		{
			this.writer.AddValue(value);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002812 File Offset: 0x00000A12
		protected override void EndArrayScope()
		{
			this.writer.EndArrayScope();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x0000281F File Offset: 0x00000A1F
		protected override void EndObjectScope()
		{
			this.writer.EndObjectScope();
		}

		// Token: 0x0400000A RID: 10
		private readonly IGeoJsonWriter writer;
	}
}
