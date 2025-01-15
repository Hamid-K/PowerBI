using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BE5 RID: 7141
	public abstract class DelegatingPageReader : IPageReader, IDisposable
	{
		// Token: 0x0600B26B RID: 45675 RVA: 0x002456CB File Offset: 0x002438CB
		public DelegatingPageReader(IPageReader pageReader)
		{
			this.pageReader = pageReader;
		}

		// Token: 0x17002CCD RID: 11469
		// (get) Token: 0x0600B26C RID: 45676 RVA: 0x002456DA File Offset: 0x002438DA
		protected IPageReader PageReader
		{
			get
			{
				return this.pageReader;
			}
		}

		// Token: 0x17002CCE RID: 11470
		// (get) Token: 0x0600B26D RID: 45677 RVA: 0x002456E2 File Offset: 0x002438E2
		public virtual TableSchema Schema
		{
			get
			{
				return this.pageReader.Schema;
			}
		}

		// Token: 0x17002CCF RID: 11471
		// (get) Token: 0x0600B26E RID: 45678 RVA: 0x002456EF File Offset: 0x002438EF
		public virtual IProgress Progress
		{
			get
			{
				return this.pageReader.Progress;
			}
		}

		// Token: 0x17002CD0 RID: 11472
		// (get) Token: 0x0600B26F RID: 45679 RVA: 0x002456FC File Offset: 0x002438FC
		public virtual int MaxPageRowCount
		{
			get
			{
				return this.pageReader.MaxPageRowCount;
			}
		}

		// Token: 0x0600B270 RID: 45680 RVA: 0x00245709 File Offset: 0x00243909
		public virtual IPage CreatePage()
		{
			return this.pageReader.CreatePage();
		}

		// Token: 0x0600B271 RID: 45681 RVA: 0x00245716 File Offset: 0x00243916
		public virtual void Read(IPage page)
		{
			this.pageReader.Read(page);
		}

		// Token: 0x0600B272 RID: 45682 RVA: 0x00245724 File Offset: 0x00243924
		public virtual IPageReader NextResult()
		{
			return this.pageReader.NextResult();
		}

		// Token: 0x0600B273 RID: 45683 RVA: 0x00245731 File Offset: 0x00243931
		public virtual void Dispose()
		{
			this.pageReader.Dispose();
		}

		// Token: 0x0600B274 RID: 45684 RVA: 0x00245740 File Offset: 0x00243940
		public static IPageReader Unwrap(IPageReader reader)
		{
			for (;;)
			{
				DelegatingPageReader delegatingPageReader = reader as DelegatingPageReader;
				if (delegatingPageReader == null)
				{
					break;
				}
				reader = delegatingPageReader.PageReader;
			}
			return reader;
		}

		// Token: 0x04005B35 RID: 23349
		private readonly IPageReader pageReader;
	}
}
