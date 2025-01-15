using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using Microsoft.SqlServer.XEvent.Linq.Internal;

namespace Microsoft.SqlServer.XEvent.Linq
{
	// Token: 0x020000C1 RID: 193
	public class QueryableXEventData : IQueryable<PublishedEvent>, IEnumerable<PublishedEvent>, IEnumerable, IQueryable, IDisposable
	{
		// Token: 0x0600023B RID: 571 RVA: 0x0001AD54 File Offset: 0x0001AD54
		public QueryableXEventData(string connectionString, string source, EventStreamSourceOptions sourceOption, EventStreamCacheOptions cacheOption)
		{
			if (connectionString == null)
			{
				throw new ArgumentNullException("connectionString");
			}
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			XEventInteropMetadataManager.StaticInit();
			this.m_EventProvider = new XEventSqlStreamProvider<PublishedEvent>(this, connectionString, source, sourceOption, cacheOption);
			this.Expression = Expression.Constant(this);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001ADA8 File Offset: 0x0001ADA8
		public QueryableXEventData(XmlReader inputReader, EventStreamSourceOptions sourceOption, EventStreamCacheOptions cacheOption)
		{
			if (inputReader == null)
			{
				throw new ArgumentNullException("inputReader");
			}
			XEventInteropMetadataManager.StaticInit();
			this.m_EventProvider = new XEventSqlStreamProvider<PublishedEvent>(this, inputReader, sourceOption, cacheOption);
			this.Expression = Expression.Constant(this);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x0001ADEC File Offset: 0x0001ADEC
		public QueryableXEventData(string fileName)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentNullException("fileName");
			}
			string[] array = new string[] { fileName };
			this.m_EventProvider = new XEventFileProvider<PublishedEvent>(this, array, null);
			this.Expression = Expression.Constant(this);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0001AE38 File Offset: 0x0001AE38
		public QueryableXEventData(string[] fileList)
		{
			if (fileList == null || fileList.Length == 0)
			{
				throw new ArgumentNullException("fileList");
			}
			for (int i = 0; i < fileList.Length; i++)
			{
				if (string.IsNullOrEmpty(fileList[i]))
				{
					throw new ArgumentNullException("fileList");
				}
			}
			this.m_EventProvider = new XEventFileProvider<PublishedEvent>(this, fileList, null);
			this.Expression = Expression.Constant(this);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0001AE9C File Offset: 0x0001AE9C
		public QueryableXEventData(string[] fileList, string[] metadataFiles)
		{
			if (fileList == null || fileList.Length == 0)
			{
				throw new ArgumentNullException("fileList");
			}
			for (int i = 0; i < fileList.Length; i++)
			{
				if (string.IsNullOrEmpty(fileList[i]))
				{
					throw new ArgumentNullException("fileList");
				}
			}
			if (metadataFiles == null || metadataFiles.Length == 0)
			{
				throw new ArgumentNullException("metadataFiles");
			}
			for (int j = 0; j < metadataFiles.Length; j++)
			{
				if (string.IsNullOrEmpty(metadataFiles[j]))
				{
					throw new ArgumentNullException("metadataFiles");
				}
			}
			this.m_EventProvider = new XEventFileProvider<PublishedEvent>(this, fileList, metadataFiles);
			this.Expression = Expression.Constant(this);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0001AF34 File Offset: 0x0001AF34
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0001AF50 File Offset: 0x0001AF50
		protected virtual void Dispose(bool disposing)
		{
			this.m_EventProvider.Dispose();
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000242 RID: 578 RVA: 0x0001AF68 File Offset: 0x0001AF68
		public IEventProvider<PublishedEvent> EventProvider
		{
			get
			{
				return this.m_EventProvider;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0001AF7C File Offset: 0x0001AF7C
		public IQueryProvider Provider
		{
			get
			{
				return this.m_EventProvider;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0001AF90 File Offset: 0x0001AF90
		// (set) Token: 0x06000245 RID: 581 RVA: 0x0001AFA4 File Offset: 0x0001AFA4
		public Expression Expression { get; internal set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0001AFB8 File Offset: 0x0001AFB8
		public Type ElementType
		{
			get
			{
				return typeof(PublishedEvent);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0001AFD0 File Offset: 0x0001AFD0
		public IEnumerator<PublishedEvent> GetEnumerator()
		{
			return this.Provider.Execute<IEnumerable<PublishedEvent>>(this.Expression).GetEnumerator();
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0001AFF4 File Offset: 0x0001AFF4
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.Provider.Execute<IEnumerable>(this.Expression).GetEnumerator();
		}

		// Token: 0x04000260 RID: 608
		private IEventProvider<PublishedEvent> m_EventProvider;
	}
}
