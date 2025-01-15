using System;
using Microsoft.ReportingServices.OData.Json;
using Microsoft.ReportingServices.OData.Json.Extension;

namespace Microsoft.ReportingServices.DataShapeResultRenderer
{
	// Token: 0x02000581 RID: 1409
	internal sealed class JsonDataShapeResultWriter : DataShapeResultWriter, IDisposable
	{
		// Token: 0x06005162 RID: 20834 RVA: 0x00159957 File Offset: 0x00157B57
		public JsonDataShapeResultWriter(JsonWriter writer)
		{
			this.m_writer = writer;
		}

		// Token: 0x06005163 RID: 20835 RVA: 0x00159966 File Offset: 0x00157B66
		public void Dispose()
		{
			if (this.m_writer != null)
			{
				this.m_writer.Close();
				this.m_writer = null;
			}
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x00159982 File Offset: 0x00157B82
		protected override void WriteObjectStart()
		{
			this.m_writer.StartObjectScope();
		}

		// Token: 0x06005165 RID: 20837 RVA: 0x0015998F File Offset: 0x00157B8F
		protected override void WriteObjectEnd()
		{
			this.m_writer.EndObjectScope();
		}

		// Token: 0x06005166 RID: 20838 RVA: 0x0015999C File Offset: 0x00157B9C
		protected override void WriteCollectionStart()
		{
			this.m_writer.StartArrayScope();
		}

		// Token: 0x06005167 RID: 20839 RVA: 0x001599A9 File Offset: 0x00157BA9
		protected override void WriteCollectionEnd()
		{
			this.m_writer.EndArrayScope();
		}

		// Token: 0x06005168 RID: 20840 RVA: 0x001599B6 File Offset: 0x00157BB6
		protected override void WritePropertyName(string name)
		{
			this.m_writer.WriteName(name);
		}

		// Token: 0x06005169 RID: 20841 RVA: 0x001599C4 File Offset: 0x00157BC4
		protected override void WriteValue(bool value)
		{
			this.m_writer.WriteValue(value);
		}

		// Token: 0x0600516A RID: 20842 RVA: 0x001599D2 File Offset: 0x00157BD2
		protected override void WriteValue(string value)
		{
			this.m_writer.WriteValue(value);
		}

		// Token: 0x0600516B RID: 20843 RVA: 0x001599E0 File Offset: 0x00157BE0
		protected override void WriteVariantValue(object value)
		{
			this.m_writer.WriteVariantValue(value);
		}

		// Token: 0x0600516C RID: 20844 RVA: 0x001599EE File Offset: 0x00157BEE
		protected override void WriteRestartFlag(RestartFlag flag)
		{
			this.m_writer.WriteValue((int)flag);
		}

		// Token: 0x04002911 RID: 10513
		private JsonWriter m_writer;
	}
}
