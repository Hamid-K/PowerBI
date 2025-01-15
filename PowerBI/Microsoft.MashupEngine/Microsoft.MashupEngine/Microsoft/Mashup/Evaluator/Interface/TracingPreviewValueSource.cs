using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E4B RID: 7755
	public class TracingPreviewValueSource : IPreviewValueSource, IDisposable
	{
		// Token: 0x0600BE8F RID: 48783 RVA: 0x00268877 File Offset: 0x00266A77
		public TracingPreviewValueSource(IPreviewValueSource previewValueSource, IHostTrace trace)
		{
			this.previewValueSource = previewValueSource;
			this.trace = trace;
		}

		// Token: 0x17002EDA RID: 11994
		// (get) Token: 0x0600BE90 RID: 48784 RVA: 0x0026888D File Offset: 0x00266A8D
		public bool IsComplete
		{
			get
			{
				return this.previewValueSource.IsComplete;
			}
		}

		// Token: 0x17002EDB RID: 11995
		// (get) Token: 0x0600BE91 RID: 48785 RVA: 0x0026889C File Offset: 0x00266A9C
		public ITableSource TableSource
		{
			get
			{
				ITableSource tableSource;
				using (this.trace.NewTimedScope())
				{
					tableSource = this.previewValueSource.TableSource;
				}
				return tableSource;
			}
		}

		// Token: 0x17002EDC RID: 11996
		// (get) Token: 0x0600BE92 RID: 48786 RVA: 0x002688E4 File Offset: 0x00266AE4
		public string SmallValue
		{
			get
			{
				string smallValue;
				using (this.trace.NewTimedScope())
				{
					smallValue = this.previewValueSource.SmallValue;
				}
				return smallValue;
			}
		}

		// Token: 0x17002EDD RID: 11997
		// (get) Token: 0x0600BE93 RID: 48787 RVA: 0x0026892C File Offset: 0x00266B2C
		public string Value
		{
			get
			{
				string value;
				using (this.trace.NewTimedScope())
				{
					value = this.previewValueSource.Value;
				}
				return value;
			}
		}

		// Token: 0x0600BE94 RID: 48788 RVA: 0x00268974 File Offset: 0x00266B74
		public void Dispose()
		{
			using (this.trace.NewTimedScope())
			{
				this.previewValueSource.Dispose();
			}
		}

		// Token: 0x0400610C RID: 24844
		private readonly IPreviewValueSource previewValueSource;

		// Token: 0x0400610D RID: 24845
		private readonly IHostTrace trace;
	}
}
