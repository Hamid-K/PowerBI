using System;
using Microsoft.Spatial;

namespace Microsoft.OData.Json
{
	// Token: 0x02000212 RID: 530
	internal sealed class GeoJsonWriterAdapter : IGeoJsonWriter
	{
		// Token: 0x06001732 RID: 5938 RVA: 0x00041B45 File Offset: 0x0003FD45
		internal GeoJsonWriterAdapter(IJsonWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x00041B54 File Offset: 0x0003FD54
		void IGeoJsonWriter.StartObjectScope()
		{
			this.writer.StartObjectScope();
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x00041B61 File Offset: 0x0003FD61
		void IGeoJsonWriter.EndObjectScope()
		{
			this.writer.EndObjectScope();
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x00041B6E File Offset: 0x0003FD6E
		void IGeoJsonWriter.StartArrayScope()
		{
			this.writer.StartArrayScope();
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x00041B7B File Offset: 0x0003FD7B
		void IGeoJsonWriter.EndArrayScope()
		{
			this.writer.EndArrayScope();
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x00041B88 File Offset: 0x0003FD88
		void IGeoJsonWriter.AddPropertyName(string name)
		{
			this.writer.WriteName(name);
		}

		// Token: 0x06001738 RID: 5944 RVA: 0x00041B96 File Offset: 0x0003FD96
		void IGeoJsonWriter.AddValue(double value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001739 RID: 5945 RVA: 0x00041BA4 File Offset: 0x0003FDA4
		void IGeoJsonWriter.AddValue(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x04000A67 RID: 2663
		private readonly IJsonWriter writer;
	}
}
