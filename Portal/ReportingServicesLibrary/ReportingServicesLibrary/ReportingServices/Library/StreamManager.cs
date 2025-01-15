using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.ReportingServices.Common;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000245 RID: 581
	internal sealed class StreamManager
	{
		// Token: 0x06001524 RID: 5412 RVA: 0x00054128 File Offset: 0x00052328
		public StreamManager(StreamFactoryBase factory)
		{
			this.m_streamFactory = factory;
		}

		// Token: 0x1700060D RID: 1549
		// (set) Token: 0x06001525 RID: 5413 RVA: 0x00054162 File Offset: 0x00052362
		public bool NeedCacheableStreams
		{
			set
			{
				this.m_needCacheableStreams = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06001526 RID: 5414 RVA: 0x0005416B File Offset: 0x0005236B
		public bool HasSecondaryStreams
		{
			get
			{
				return this.m_secondaryStreamNames.Count > 0;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x0005417B File Offset: 0x0005237B
		internal StreamFactoryBase StreamFactory
		{
			[DebuggerStepThrough]
			get
			{
				return this.m_streamFactory;
			}
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x00054184 File Offset: 0x00052384
		public Stream GetNewStream(string name, string extension, Encoding encoding, string mimeType, bool willSeek, StreamOper operation)
		{
			if (operation == StreamOper.RegisterOnly)
			{
				if (!this.m_secondaryStreamNames.Contains(name))
				{
					this.m_secondaryStreamNames.Add(name);
				}
				return null;
			}
			if (operation == StreamOper.CreateOnly)
			{
				return new RSStream(this.m_streamFactory.CreateStream(false, willSeek, false), false);
			}
			if (this.ArePersistingStreams)
			{
				willSeek = true;
			}
			RSStream rsstream;
			if (this.m_primaryStream == null)
			{
				rsstream = new RSStream(this.m_streamFactory.CreateStream(true, willSeek, this.m_needCacheableStreams), true, this.ArePersistingStreams);
				this.m_streamFactory.SetResult(rsstream);
			}
			else
			{
				if (operation == StreamOper.CreateForPersistedStreams && !this.ArePersistingStreams)
				{
					return Stream.Null;
				}
				rsstream = new RSStream(this.m_streamFactory.CreateStream(false, willSeek, true), false, this.ArePersistingStreams);
				if (operation != StreamOper.CreateForPersistedStreams)
				{
					if (this.m_secondaryStreamNames.Contains(name))
					{
						if (this.m_streams.ContainsKey(name))
						{
							throw new InternalCatalogException("Expected unique stream names: " + name);
						}
					}
					else
					{
						this.m_secondaryStreamNames.Add(name);
					}
				}
			}
			if (this.ArePersistingStreams)
			{
				this.m_persistedStreamManager.AddNextStream(rsstream);
			}
			rsstream.Name = name;
			rsstream.Extension = extension;
			rsstream.MimeType = mimeType;
			rsstream.Encoding = encoding;
			if (this.m_primaryStream == null)
			{
				this.m_primaryStream = rsstream;
			}
			else
			{
				this.m_streams.Add(name, rsstream);
				if (rsstream.IsCacheable && operation != StreamOper.CreateForPersistedStreams)
				{
					this.m_cacheableSecondaryStreams.Add(name, rsstream);
				}
			}
			return rsstream;
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x000542E8 File Offset: 0x000524E8
		public RSStream PrimaryStream
		{
			get
			{
				return this.m_primaryStream;
			}
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x000542F0 File Offset: 0x000524F0
		internal void SetCachedPrimaryStream(CachedData.CacheStream cachedStream)
		{
			this.m_primaryStream = cachedStream;
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x000542FC File Offset: 0x000524FC
		internal RSStream GetStream(string streamName)
		{
			RSStream rsstream = null;
			if (this.PrimaryStream != null && StreamManager.StreamNameComparer.Equals(this.PrimaryStream.Name, streamName))
			{
				rsstream = this.PrimaryStream;
			}
			else if (!this.m_streams.TryGetValue(streamName, out rsstream))
			{
				rsstream = null;
			}
			return rsstream;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x000542F0 File Offset: 0x000524F0
		public void SetPrimaryStream(RSStream stream)
		{
			this.m_primaryStream = stream;
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x00054347 File Offset: 0x00052547
		public void AddSecondaryStream(RSStream stream)
		{
			this.m_streams.Add(stream.Name, stream);
			if (stream.IsCacheable && !this.ArePersistingStreams)
			{
				this.m_cacheableSecondaryStreams.Add(stream.Name, stream);
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x0600152E RID: 5422 RVA: 0x0005437D File Offset: 0x0005257D
		internal RSStream[] SecondaryCacheableStreams
		{
			get
			{
				return StreamManager.CopyOutCollection<RSStream>(this.m_cacheableSecondaryStreams.Values);
			}
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x0005438F File Offset: 0x0005258F
		internal string[] SecondaryStreamNames
		{
			get
			{
				return StreamManager.CopyOutCollection<string>(this.m_secondaryStreamNames);
			}
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x0005439C File Offset: 0x0005259C
		internal void SetSecondaryStreamNames(string[] streamNames)
		{
			foreach (string text in streamNames)
			{
				if (!this.m_secondaryStreamNames.Contains(text))
				{
					this.m_secondaryStreamNames.Add(text);
				}
			}
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x000543D8 File Offset: 0x000525D8
		public void ClearSecondaryStreams()
		{
			this.m_secondaryStreamNames.Clear();
			StreamFactoryBase.CloseAllStreams(Microsoft.ReportingServices.Common.EnumeratorMapping.Map<RSStream, Stream>(this.m_streams.Values, (RSStream s) => s));
			this.m_streams.Clear();
			this.m_cacheableSecondaryStreams.Clear();
		}

		// Token: 0x17000613 RID: 1555
		// (get) Token: 0x06001532 RID: 5426 RVA: 0x0005443A File Offset: 0x0005263A
		public string[] SecondaryCacheableStreamNames
		{
			get
			{
				return StreamManager.CopyOutCollection<string>(this.m_cacheableSecondaryStreams.Keys);
			}
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x0005444C File Offset: 0x0005264C
		private static T[] CopyOutCollection<T>(ICollection<T> collection)
		{
			T[] array = new T[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x17000614 RID: 1556
		// (get) Token: 0x06001534 RID: 5428 RVA: 0x00054470 File Offset: 0x00052670
		internal bool IsPrimaryStreamCacheable
		{
			get
			{
				bool flag = false;
				if (this.m_primaryStream != null && this.m_primaryStream.Length > 0L)
				{
					ICacheable primaryStream = this.m_primaryStream;
					if (primaryStream != null)
					{
						flag = primaryStream.IsCacheable;
					}
				}
				return flag;
			}
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000544A8 File Offset: 0x000526A8
		public byte[] GetResourceFromPrimaryStream(out string mimeType)
		{
			RSStream primaryStream = this.PrimaryStream;
			byte[] array;
			if (primaryStream != null)
			{
				primaryStream.Seek(0L, SeekOrigin.Begin);
				array = StreamSupport.ReadToEndUsingLength(primaryStream);
				mimeType = primaryStream.MimeType;
			}
			else
			{
				array = new byte[0];
				mimeType = "";
			}
			return array;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000544EB File Offset: 0x000526EB
		public void SetPersistStreams(string sessionID, bool clearOldStreams)
		{
			this.m_persistedStreamManager = new SQLPersistedStreams(sessionID);
			if (clearOldStreams)
			{
				this.m_persistedStreamManager.ClearStreams();
			}
		}

		// Token: 0x17000615 RID: 1557
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x00054507 File Offset: 0x00052707
		public PersistedStreamManagement PersistedStreamManager
		{
			get
			{
				return this.m_persistedStreamManager;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x0005450F File Offset: 0x0005270F
		private bool ArePersistingStreams
		{
			get
			{
				return this.m_persistedStreamManager != null;
			}
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x0005451C File Offset: 0x0005271C
		public long TotalStreamLength
		{
			get
			{
				long num = 0L;
				if (this.m_primaryStream != null)
				{
					num += this.m_primaryStream.Length;
				}
				foreach (RSStream rsstream in this.m_streams.Values)
				{
					if (rsstream != null)
					{
						num += rsstream.Length;
					}
				}
				return num;
			}
		}

		// Token: 0x040007B5 RID: 1973
		private static readonly StringComparer StreamNameComparer = StringComparer.OrdinalIgnoreCase;

		// Token: 0x040007B6 RID: 1974
		private StreamFactoryBase m_streamFactory;

		// Token: 0x040007B7 RID: 1975
		private Dictionary<string, RSStream> m_streams = new Dictionary<string, RSStream>(StreamManager.StreamNameComparer);

		// Token: 0x040007B8 RID: 1976
		private Dictionary<string, RSStream> m_cacheableSecondaryStreams = new Dictionary<string, RSStream>(StreamManager.StreamNameComparer);

		// Token: 0x040007B9 RID: 1977
		private List<string> m_secondaryStreamNames = new List<string>();

		// Token: 0x040007BA RID: 1978
		private RSStream m_primaryStream;

		// Token: 0x040007BB RID: 1979
		private bool m_needCacheableStreams;

		// Token: 0x040007BC RID: 1980
		private PersistedStreamManagement m_persistedStreamManager;
	}
}
