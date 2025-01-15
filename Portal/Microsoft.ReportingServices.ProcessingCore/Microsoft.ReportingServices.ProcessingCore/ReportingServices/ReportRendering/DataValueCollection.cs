using System;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200006B RID: 107
	public sealed class DataValueCollection
	{
		// Token: 0x060006EF RID: 1775 RVA: 0x0001AAB4 File Offset: 0x00018CB4
		internal DataValueCollection(DataValueCRIList expressions, DataValueInstanceList instances)
		{
			this.m_expressions = expressions;
			this.m_instances = instances;
			Global.Tracer.Assert(this.m_expressions != null);
			Global.Tracer.Assert(this.m_instances == null || this.m_instances.Count == this.m_expressions.Count);
		}

		// Token: 0x1700052D RID: 1325
		public DataValue this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				string text = null;
				object obj = null;
				if (ExpressionInfo.Types.Constant == this.m_expressions[index].Name.Type)
				{
					text = this.m_expressions[index].Name.Value;
				}
				else if (this.m_instances != null)
				{
					text = this.m_instances[index].Name;
				}
				if (ExpressionInfo.Types.Constant == this.m_expressions[index].Value.Type)
				{
					obj = this.m_expressions[index].Value.Value;
				}
				else if (this.m_instances != null)
				{
					obj = this.m_instances[index].Value;
				}
				return new DataValue(text, obj);
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x0001AC06 File Offset: 0x00018E06
		public int Count
		{
			get
			{
				return this.m_expressions.Count;
			}
		}

		// Token: 0x040001EF RID: 495
		private DataValueInstanceList m_instances;

		// Token: 0x040001F0 RID: 496
		private DataValueCRIList m_expressions;
	}
}
