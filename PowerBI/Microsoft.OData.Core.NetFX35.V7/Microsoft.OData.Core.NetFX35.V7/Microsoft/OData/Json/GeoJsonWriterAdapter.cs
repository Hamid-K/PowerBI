using System;
using Microsoft.Spatial;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E0 RID: 480
	internal sealed class GeoJsonWriterAdapter : IGeoJsonWriter
	{
		// Token: 0x060012CB RID: 4811 RVA: 0x000363EC File Offset: 0x000345EC
		internal GeoJsonWriterAdapter(IJsonWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000363FB File Offset: 0x000345FB
		void IGeoJsonWriter.StartObjectScope()
		{
			this.writer.StartObjectScope();
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x00036408 File Offset: 0x00034608
		void IGeoJsonWriter.EndObjectScope()
		{
			this.writer.EndObjectScope();
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00036415 File Offset: 0x00034615
		void IGeoJsonWriter.StartArrayScope()
		{
			this.writer.StartArrayScope();
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00036422 File Offset: 0x00034622
		void IGeoJsonWriter.EndArrayScope()
		{
			this.writer.EndArrayScope();
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003642F File Offset: 0x0003462F
		void IGeoJsonWriter.AddPropertyName(string name)
		{
			this.writer.WriteName(name);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x0003643D File Offset: 0x0003463D
		void IGeoJsonWriter.AddValue(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x0003644B File Offset: 0x0003464B
		void IGeoJsonWriter.AddValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x04000987 RID: 2439
		private readonly IJsonWriter writer;
	}
}
