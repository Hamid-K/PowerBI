using System;

namespace Microsoft.Data.Spatial
{
	// Token: 0x02000009 RID: 9
	internal sealed class WrappedGeoJsonWriter : GeoJsonWriterBase
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00002972 File Offset: 0x00000B72
		public WrappedGeoJsonWriter(IGeoJsonWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002981 File Offset: 0x00000B81
		protected override void StartObjectScope()
		{
			this.writer.StartObjectScope();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000298E File Offset: 0x00000B8E
		protected override void StartArrayScope()
		{
			this.writer.StartArrayScope();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000299B File Offset: 0x00000B9B
		protected override void AddPropertyName(string name)
		{
			this.writer.AddPropertyName(name);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000029A9 File Offset: 0x00000BA9
		protected override void AddValue(string value)
		{
			this.writer.AddValue(value);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000029B7 File Offset: 0x00000BB7
		protected override void AddValue(double value)
		{
			this.writer.AddValue(value);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000029C5 File Offset: 0x00000BC5
		protected override void EndArrayScope()
		{
			this.writer.EndArrayScope();
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000029D2 File Offset: 0x00000BD2
		protected override void EndObjectScope()
		{
			this.writer.EndObjectScope();
		}

		// Token: 0x0400000C RID: 12
		private readonly IGeoJsonWriter writer;
	}
}
