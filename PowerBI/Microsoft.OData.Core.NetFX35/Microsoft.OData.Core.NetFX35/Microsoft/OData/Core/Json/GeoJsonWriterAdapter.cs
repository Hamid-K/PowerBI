using System;
using Microsoft.Data.Spatial;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x0200010E RID: 270
	internal sealed class GeoJsonWriterAdapter : IGeoJsonWriter
	{
		// Token: 0x06000A2E RID: 2606 RVA: 0x00025EAC File Offset: 0x000240AC
		internal GeoJsonWriterAdapter(IJsonWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00025EBB File Offset: 0x000240BB
		void IGeoJsonWriter.StartObjectScope()
		{
			this.writer.StartObjectScope();
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00025EC8 File Offset: 0x000240C8
		void IGeoJsonWriter.EndObjectScope()
		{
			this.writer.EndObjectScope();
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00025ED5 File Offset: 0x000240D5
		void IGeoJsonWriter.StartArrayScope()
		{
			this.writer.StartArrayScope();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00025EE2 File Offset: 0x000240E2
		void IGeoJsonWriter.EndArrayScope()
		{
			this.writer.EndArrayScope();
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00025EEF File Offset: 0x000240EF
		void IGeoJsonWriter.AddPropertyName(string name)
		{
			this.writer.WriteName(name);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00025EFD File Offset: 0x000240FD
		void IGeoJsonWriter.AddValue(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00025F0B File Offset: 0x0002410B
		void IGeoJsonWriter.AddValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x0400040D RID: 1037
		private readonly IJsonWriter writer;
	}
}
