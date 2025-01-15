using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001145 RID: 4421
	internal sealed class ThrowingPageReader : IPageReaderWithTableSource, IPageReader, IDisposable
	{
		// Token: 0x060073CD RID: 29645 RVA: 0x0018E3FB File Offset: 0x0018C5FB
		public ThrowingPageReader(IPageReader pageReader)
		{
			this.pageReader = pageReader;
		}

		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x060073CE RID: 29646 RVA: 0x0018E40C File Offset: 0x0018C60C
		public ITableSource TableSource
		{
			get
			{
				IPageReaderWithTableSource pageReaderWithTableSource = this.pageReader as IPageReaderWithTableSource;
				if (pageReaderWithTableSource == null)
				{
					return null;
				}
				return pageReaderWithTableSource.TableSource;
			}
		}

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x060073CF RID: 29647 RVA: 0x0018E430 File Offset: 0x0018C630
		public TableSchema Schema
		{
			get
			{
				return this.pageReader.Schema;
			}
		}

		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x060073D0 RID: 29648 RVA: 0x0018E43D File Offset: 0x0018C63D
		public IProgress Progress
		{
			get
			{
				return this.pageReader.Progress;
			}
		}

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x060073D1 RID: 29649 RVA: 0x0018E44A File Offset: 0x0018C64A
		public int MaxPageRowCount
		{
			get
			{
				return this.pageReader.MaxPageRowCount;
			}
		}

		// Token: 0x060073D2 RID: 29650 RVA: 0x0018E457 File Offset: 0x0018C657
		public IPage CreatePage()
		{
			return this.pageReader.CreatePage();
		}

		// Token: 0x060073D3 RID: 29651 RVA: 0x0018E464 File Offset: 0x0018C664
		public void Read(IPage page)
		{
			this.pageReader.Read(page);
			if (page.ExceptionRows.Count != 0)
			{
				throw PageExceptionSerializer.GetExceptionFromProperties(page.ExceptionRows.OrderBy((KeyValuePair<int, IExceptionRow> er) => er.Key).First<KeyValuePair<int, IExceptionRow>>().Value.Exceptions.OrderBy((KeyValuePair<int, ISerializedException> e) => e.Key).First<KeyValuePair<int, ISerializedException>>().Value);
			}
		}

		// Token: 0x060073D4 RID: 29652 RVA: 0x0018E500 File Offset: 0x0018C700
		public IPageReader NextResult()
		{
			IPageReader pageReader = this.pageReader.NextResult();
			if (pageReader != null)
			{
				pageReader = new ThrowingPageReader(pageReader);
			}
			return pageReader;
		}

		// Token: 0x060073D5 RID: 29653 RVA: 0x0018E524 File Offset: 0x0018C724
		public void Dispose()
		{
			if (this.pageReader != null)
			{
				this.pageReader.Dispose();
				this.pageReader = null;
			}
		}

		// Token: 0x04003FC4 RID: 16324
		private IPageReader pageReader;
	}
}
