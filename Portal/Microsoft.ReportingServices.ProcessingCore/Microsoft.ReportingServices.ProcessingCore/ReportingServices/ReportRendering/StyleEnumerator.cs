using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x02000023 RID: 35
	internal sealed class StyleEnumerator : IEnumerator
	{
		// Token: 0x06000414 RID: 1044 RVA: 0x0000BC74 File Offset: 0x00009E74
		internal StyleEnumerator(StyleProperties sharedProps, StyleProperties nonSharedProps)
		{
			this.m_sharedProperties = sharedProps;
			this.m_nonSharedProperties = nonSharedProps;
			this.m_total = 0;
			if (this.m_sharedProperties != null)
			{
				this.m_total += this.m_sharedProperties.Count;
			}
			if (this.m_nonSharedProperties != null)
			{
				this.m_total += this.m_nonSharedProperties.Count;
			}
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000BCE4 File Offset: 0x00009EE4
		public object Current
		{
			get
			{
				if (0 > this.m_current)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				int num = 0;
				if (this.m_sharedProperties != null)
				{
					num = this.m_sharedProperties.Count;
				}
				if (this.m_current < num)
				{
					return this.m_sharedProperties[this.m_current];
				}
				Global.Tracer.Assert(this.m_nonSharedProperties != null);
				return this.m_nonSharedProperties[this.m_current - num];
			}
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public bool MoveNext()
		{
			if (this.m_current < this.m_total - 1)
			{
				this.m_current++;
				return true;
			}
			return false;
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000BD7F File Offset: 0x00009F7F
		public void Reset()
		{
			this.m_current = -1;
		}

		// Token: 0x040000B9 RID: 185
		private StyleProperties m_sharedProperties;

		// Token: 0x040000BA RID: 186
		private StyleProperties m_nonSharedProperties;

		// Token: 0x040000BB RID: 187
		private int m_total;

		// Token: 0x040000BC RID: 188
		private int m_current = -1;
	}
}
