using System;
using System.IO;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library
{
	// Token: 0x0200025C RID: 604
	public static class PersistentCacheExtensions
	{
		// Token: 0x060019BE RID: 6590 RVA: 0x00033BA3 File Offset: 0x00031DA3
		public static Stream Add(this IPersistentCache cache, OneOf<string, StructuredCacheKey> key, Stream stream)
		{
			if (!stream.CanRead)
			{
				return stream;
			}
			if (stream.CanSeek && stream.Length > cache.MaxEntryLength)
			{
				return stream;
			}
			return new PersistentCacheExtensions.CachingStream(cache, key, stream, cache.MaxEntryLength);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00033BD8 File Offset: 0x00031DD8
		public static ActionValue ClearDataCache(this ActionValue action, IEngineHost engineHost)
		{
			ICacheSets cacheSets = engineHost.QueryService<ICacheSets>();
			IClearableTransientCache clearableTransientCache = engineHost.QueryService<IClearableTransientCache>();
			return new PersistentCacheExtensions.ClearCacheActionValue(action, cacheSets, clearableTransientCache, false);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00033BFC File Offset: 0x00031DFC
		public static ActionValue ClearCache(this ActionValue action, IEngineHost engineHost)
		{
			ICacheSets cacheSets = engineHost.QueryService<ICacheSets>();
			IClearableTransientCache clearableTransientCache = engineHost.QueryService<IClearableTransientCache>();
			return new PersistentCacheExtensions.ClearCacheActionValue(action, cacheSets, clearableTransientCache, true);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00033C20 File Offset: 0x00031E20
		public static bool TryGetValue(this IPersistentObjectCache cache, OneOf<string, StructuredCacheKey> key, Func<Stream, object> deserializer, out object value)
		{
			if (key.Is<string>())
			{
				return cache.TryGetValue(key.As<string>(), DateTime.MinValue, null, deserializer, out value);
			}
			return cache.TryGetValue(key.As<StructuredCacheKey>(), DateTime.MinValue, null, deserializer, out value);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00033C56 File Offset: 0x00031E56
		public static void CommitValue(this IPersistentObjectCache cache, OneOf<string, StructuredCacheKey> key, Action<Stream, object> serializer, object value)
		{
			if (key.Is<string>())
			{
				cache.CommitValue(key.As<string>(), null, serializer, value);
				return;
			}
			cache.CommitValue(key.As<StructuredCacheKey>(), null, serializer, value);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00033C84 File Offset: 0x00031E84
		public static bool TryGetValue(this IPersistentObjectCache cache, OneOf<string, StructuredCacheKey> key, out TableSchema schema)
		{
			object obj;
			if (key.Is<string>())
			{
				if (cache.TryGetValue(key.As<string>(), DateTime.MinValue, null, (Stream s) => TableSchema.Deserialize(s), out obj))
				{
					goto IL_007E;
				}
			}
			if (key.Is<StructuredCacheKey>())
			{
				if (cache.TryGetValue(key.As<StructuredCacheKey>(), DateTime.MinValue, null, (Stream s) => TableSchema.Deserialize(s), out obj))
				{
					goto IL_007E;
				}
			}
			schema = null;
			return false;
			IL_007E:
			schema = (TableSchema)obj;
			return true;
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00033D20 File Offset: 0x00031F20
		public static void CommitValue(this IPersistentObjectCache cache, OneOf<string, StructuredCacheKey> key, TableSchema schema)
		{
			if (key.Is<string>())
			{
				cache.CommitValue(key.As<string>(), null, delegate(Stream s, object o)
				{
					((TableSchema)o).Serialize(s);
				}, schema);
				return;
			}
			cache.CommitValue(key.As<StructuredCacheKey>(), null, delegate(Stream s, object o)
			{
				((TableSchema)o).Serialize(s);
			}, schema);
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00033D94 File Offset: 0x00031F94
		public static T GetOrCommitValue<T>(this IPersistentObjectCache cache, StructuredCacheKey key, Func<T> ctor, Action<Stream, T> serializer, Func<Stream, T> deserializer)
		{
			object obj;
			if (cache.TryGetValue(key, DateTime.MinValue, null, (Stream s) => deserializer(s), out obj))
			{
				return (T)((object)obj);
			}
			T t = ctor();
			cache.CommitValue(key, null, delegate(Stream s, object v)
			{
				serializer(s, (T)((object)v));
			}, t);
			return t;
		}

		// Token: 0x060019C6 RID: 6598 RVA: 0x00033DFC File Offset: 0x00031FFC
		public static bool TryGetValue(this IObjectCache cache, OneOf<string, StructuredCacheKey> key, out object value)
		{
			if (key.Is<string>())
			{
				return cache.TryGetValue(key.As<string>(), DateTime.MinValue, null, out value);
			}
			return cache.TryGetValue(key.As<StructuredCacheKey>(), DateTime.MinValue, null, out value);
		}

		// Token: 0x060019C7 RID: 6599 RVA: 0x00033E30 File Offset: 0x00032030
		public static void CommitValue(this IObjectCache cache, OneOf<string, StructuredCacheKey> key, int size, object value)
		{
			if (key.Is<string>())
			{
				cache.CommitValue(key.As<string>(), null, size, value);
				return;
			}
			cache.CommitValue(key.As<StructuredCacheKey>(), null, size, value);
		}

		// Token: 0x060019C8 RID: 6600 RVA: 0x00033E5C File Offset: 0x0003205C
		public static IPagedStorage OpenStorage(this IPersistentCache cache, OneOf<string, StructuredCacheKey> key, int pageSize, int maxPageCount)
		{
			if (key.Is<string>())
			{
				return cache.OpenStorage(key.As<string>(), DateTime.MinValue, null, pageSize, maxPageCount);
			}
			return cache.OpenStorage(key.As<StructuredCacheKey>(), DateTime.MinValue, null, pageSize, maxPageCount);
		}

		// Token: 0x0200025D RID: 605
		private class CachingStream : Stream
		{
			// Token: 0x060019C9 RID: 6601 RVA: 0x00033E92 File Offset: 0x00032092
			public CachingStream(IPersistentCache cache, OneOf<string, StructuredCacheKey> key, Stream stream, long maxLength)
			{
				this.cache = cache;
				this.key = key;
				this.stream = stream;
				this.maxLength = maxLength;
				this.cachingStream = cache.BeginAdd();
				this.length = 0L;
			}

			// Token: 0x17000CC1 RID: 3265
			// (get) Token: 0x060019CA RID: 6602 RVA: 0x00033ECB File Offset: 0x000320CB
			public override bool CanRead
			{
				get
				{
					return this.stream.CanRead;
				}
			}

			// Token: 0x17000CC2 RID: 3266
			// (get) Token: 0x060019CB RID: 6603 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000CC3 RID: 3267
			// (get) Token: 0x060019CC RID: 6604 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060019CD RID: 6605 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void Flush()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x17000CC4 RID: 3268
			// (get) Token: 0x060019CE RID: 6606 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override long Length
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000CC5 RID: 3269
			// (get) Token: 0x060019CF RID: 6607 RVA: 0x0000EE09 File Offset: 0x0000D009
			// (set) Token: 0x060019D0 RID: 6608 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override long Position
			{
				get
				{
					throw new InvalidOperationException();
				}
				set
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060019D1 RID: 6609 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060019D2 RID: 6610 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void SetLength(long value)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060019D3 RID: 6611 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x17000CC6 RID: 3270
			// (get) Token: 0x060019D4 RID: 6612 RVA: 0x00033ED8 File Offset: 0x000320D8
			public override int ReadTimeout
			{
				get
				{
					return this.stream.ReadTimeout;
				}
			}

			// Token: 0x060019D5 RID: 6613 RVA: 0x00033EE5 File Offset: 0x000320E5
			private void Commit()
			{
				if (this.cachingStream != null)
				{
					this.cachingStream.Flush();
					this.cache.EndAdd(this.key, this.cachingStream).Close();
					this.cachingStream = null;
				}
			}

			// Token: 0x060019D6 RID: 6614 RVA: 0x00033F1D File Offset: 0x0003211D
			private void Discard()
			{
				if (this.cachingStream != null)
				{
					this.cachingStream.Close();
					this.cachingStream = null;
				}
			}

			// Token: 0x060019D7 RID: 6615 RVA: 0x00033F3C File Offset: 0x0003213C
			public override void Close()
			{
				if (this.stream != null)
				{
					bool flag = false;
					try
					{
						flag = this.stream.ReadByte() == -1;
					}
					catch (Exception ex)
					{
						if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
					}
					if (flag)
					{
						this.Commit();
					}
					else
					{
						this.Discard();
					}
					this.stream.Close();
					this.stream = null;
				}
			}

			// Token: 0x060019D8 RID: 6616 RVA: 0x00033FA4 File Offset: 0x000321A4
			public override int ReadByte()
			{
				if (this.stream == null)
				{
					return -1;
				}
				int num = this.stream.ReadByte();
				if (this.cachingStream != null)
				{
					if (num == -1)
					{
						this.Commit();
					}
					else
					{
						this.length += 1L;
						if (this.length > this.maxLength)
						{
							this.Discard();
						}
						else
						{
							this.cachingStream.WriteByte((byte)num);
						}
					}
				}
				return num;
			}

			// Token: 0x060019D9 RID: 6617 RVA: 0x00034010 File Offset: 0x00032210
			public override int Read(byte[] buffer, int offset, int count)
			{
				if (this.stream == null)
				{
					return 0;
				}
				int num = this.stream.Read(buffer, offset, count);
				if (this.cachingStream != null)
				{
					if (num == 0)
					{
						this.Commit();
					}
					else
					{
						this.length += (long)num;
						if (this.length > this.maxLength)
						{
							this.Discard();
						}
						else
						{
							this.cachingStream.Write(buffer, offset, num);
						}
					}
				}
				return num;
			}

			// Token: 0x0400076D RID: 1901
			private readonly IPersistentCache cache;

			// Token: 0x0400076E RID: 1902
			private readonly OneOf<string, StructuredCacheKey> key;

			// Token: 0x0400076F RID: 1903
			private Stream stream;

			// Token: 0x04000770 RID: 1904
			private readonly long maxLength;

			// Token: 0x04000771 RID: 1905
			private Stream cachingStream;

			// Token: 0x04000772 RID: 1906
			private long length;
		}

		// Token: 0x0200025E RID: 606
		public class WriteOnlyCachingStream : Stream
		{
			// Token: 0x060019DA RID: 6618 RVA: 0x0003407D File Offset: 0x0003227D
			public WriteOnlyCachingStream(IPersistentCache cache, OneOf<string, StructuredCacheKey> key, long maxLength, Action onDiscard = null)
			{
				this.cache = cache;
				this.key = key;
				this.maxLength = maxLength;
				this.onDiscard = onDiscard;
				this.cachingStream = cache.BeginAdd();
				this.length = 0L;
			}

			// Token: 0x17000CC7 RID: 3271
			// (get) Token: 0x060019DB RID: 6619 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000CC8 RID: 3272
			// (get) Token: 0x060019DC RID: 6620 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000CC9 RID: 3273
			// (get) Token: 0x060019DD RID: 6621 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanWrite
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060019DE RID: 6622 RVA: 0x000340B6 File Offset: 0x000322B6
			public override void Flush()
			{
				if (this.cachingStream != null)
				{
					this.cachingStream.Flush();
				}
			}

			// Token: 0x17000CCA RID: 3274
			// (get) Token: 0x060019DF RID: 6623 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override long Length
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x17000CCB RID: 3275
			// (get) Token: 0x060019E0 RID: 6624 RVA: 0x0000EE09 File Offset: 0x0000D009
			// (set) Token: 0x060019E1 RID: 6625 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override long Position
			{
				get
				{
					throw new InvalidOperationException();
				}
				set
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060019E2 RID: 6626 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060019E3 RID: 6627 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override void SetLength(long value)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060019E4 RID: 6628 RVA: 0x000340CB File Offset: 0x000322CB
			public override void Write(byte[] buffer, int offset, int count)
			{
				if (this.cachingStream == null)
				{
					return;
				}
				this.length += (long)count;
				if (this.length > this.maxLength)
				{
					this.Discard();
					return;
				}
				this.cachingStream.Write(buffer, offset, count);
			}

			// Token: 0x17000CCC RID: 3276
			// (get) Token: 0x060019E5 RID: 6629 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override int ReadTimeout
			{
				get
				{
					throw new InvalidOperationException();
				}
			}

			// Token: 0x060019E6 RID: 6630 RVA: 0x00034108 File Offset: 0x00032308
			private void Commit()
			{
				if (this.cachingStream != null)
				{
					this.cachingStream.Flush();
					this.cache.EndAdd(this.key, this.cachingStream).Close();
					this.cachingStream = null;
				}
			}

			// Token: 0x060019E7 RID: 6631 RVA: 0x00034140 File Offset: 0x00032340
			public void Discard()
			{
				if (this.cachingStream != null)
				{
					this.cachingStream.Close();
					this.cachingStream = null;
					if (this.onDiscard != null)
					{
						this.onDiscard();
					}
				}
			}

			// Token: 0x060019E8 RID: 6632 RVA: 0x0003416F File Offset: 0x0003236F
			public override void Close()
			{
				if (this.cachingStream != null)
				{
					this.Commit();
				}
			}

			// Token: 0x060019E9 RID: 6633 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override int ReadByte()
			{
				throw new InvalidOperationException();
			}

			// Token: 0x060019EA RID: 6634 RVA: 0x0000EE09 File Offset: 0x0000D009
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw new InvalidOperationException();
			}

			// Token: 0x04000773 RID: 1907
			private readonly IPersistentCache cache;

			// Token: 0x04000774 RID: 1908
			private readonly OneOf<string, StructuredCacheKey> key;

			// Token: 0x04000775 RID: 1909
			private readonly long maxLength;

			// Token: 0x04000776 RID: 1910
			private readonly Action onDiscard;

			// Token: 0x04000777 RID: 1911
			private Stream cachingStream;

			// Token: 0x04000778 RID: 1912
			private long length;
		}

		// Token: 0x0200025F RID: 607
		private class ClearCacheActionValue : ActionValue
		{
			// Token: 0x060019EB RID: 6635 RVA: 0x0003417F File Offset: 0x0003237F
			public ClearCacheActionValue(ActionValue action, ICacheSets cacheSets, IClearableTransientCache transientCache, bool clearMetadata)
			{
				this.action = action;
				this.cacheSets = cacheSets;
				this.transientCache = transientCache;
				this.clearMetadata = clearMetadata;
			}

			// Token: 0x17000CCD RID: 3277
			// (get) Token: 0x060019EC RID: 6636 RVA: 0x000341A4 File Offset: 0x000323A4
			public override IExpression Expression
			{
				get
				{
					return this.action.Expression;
				}
			}

			// Token: 0x060019ED RID: 6637 RVA: 0x000341B4 File Offset: 0x000323B4
			public override Value Execute()
			{
				Value value;
				try
				{
					value = this.action.Execute();
				}
				finally
				{
					if (this.clearMetadata)
					{
						this.ClearCacheSet(this.cacheSets.Metadata);
					}
					this.ClearCacheSet(this.cacheSets.Data);
					this.transientCache.Clear();
				}
				return value;
			}

			// Token: 0x060019EE RID: 6638 RVA: 0x00034218 File Offset: 0x00032418
			private void ClearCacheSet(ICacheSet cacheSet)
			{
				((IClearablePersistentCache)cacheSet.PersistentCache).Clear();
				((IClearableObjectCache)cacheSet.ObjectCache).Clear();
			}

			// Token: 0x04000779 RID: 1913
			private readonly ActionValue action;

			// Token: 0x0400077A RID: 1914
			private readonly ICacheSets cacheSets;

			// Token: 0x0400077B RID: 1915
			private readonly IClearableTransientCache transientCache;

			// Token: 0x0400077C RID: 1916
			private readonly bool clearMetadata;
		}
	}
}
