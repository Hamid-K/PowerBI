using System;

namespace Microsoft.Spatial
{
	// Token: 0x02000004 RID: 4
	internal sealed class WrappedGeoJsonWriter : GeoJsonWriterBase
	{
		// Token: 0x06000028 RID: 40 RVA: 0x000023D7 File Offset: 0x000005D7
		public WrappedGeoJsonWriter(IGeoJsonWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000023E6 File Offset: 0x000005E6
		protected override void StartObjectScope()
		{
			this.writer.StartObjectScope();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000023F3 File Offset: 0x000005F3
		protected override void StartArrayScope()
		{
			this.writer.StartArrayScope();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002400 File Offset: 0x00000600
		protected override void AddPropertyName(string name)
		{
			this.writer.AddPropertyName(name);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000240E File Offset: 0x0000060E
		protected override void AddValue(string value)
		{
			this.writer.AddValue(value);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000241C File Offset: 0x0000061C
		protected override void AddValue(double value)
		{
			this.writer.AddValue(value);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000242A File Offset: 0x0000062A
		protected override void EndArrayScope()
		{
			this.writer.EndArrayScope();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002437 File Offset: 0x00000637
		protected override void EndObjectScope()
		{
			this.writer.EndObjectScope();
		}

		// Token: 0x04000009 RID: 9
		private readonly IGeoJsonWriter writer;
	}
}
