using System;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Data.Mashup
{
	// Token: 0x0200001F RID: 31
	internal class ErrorTranslatingPageReader : DelegatingPageReader
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00006832 File Offset: 0x00004A32
		public ErrorTranslatingPageReader(IPageReader pageReader, Func<Exception, Exception> translateException)
			: base(pageReader)
		{
			this.translateException = translateException;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006844 File Offset: 0x00004A44
		public override TableSchema Schema
		{
			get
			{
				TableSchema schema;
				try
				{
					schema = base.Schema;
				}
				catch (Exception ex)
				{
					this.HandleException(ex);
					throw;
				}
				return schema;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006878 File Offset: 0x00004A78
		public override IProgress Progress
		{
			get
			{
				IProgress progress;
				try
				{
					progress = base.Progress;
				}
				catch (Exception ex)
				{
					this.HandleException(ex);
					throw;
				}
				return progress;
			}
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000068AC File Offset: 0x00004AAC
		public override IPage CreatePage()
		{
			IPage page;
			try
			{
				page = base.CreatePage();
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
				throw;
			}
			return page;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000068E0 File Offset: 0x00004AE0
		public override void Read(IPage page)
		{
			try
			{
				base.Read(page);
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
				throw;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00006910 File Offset: 0x00004B10
		public override void Dispose()
		{
			try
			{
				base.Dispose();
			}
			catch (Exception ex)
			{
				this.HandleException(ex);
				throw;
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00006940 File Offset: 0x00004B40
		private void HandleException(Exception e)
		{
			Exception ex = this.translateException(e);
			if (ex != e)
			{
				throw ex;
			}
		}

		// Token: 0x040000A8 RID: 168
		private readonly Func<Exception, Exception> translateException;
	}
}
