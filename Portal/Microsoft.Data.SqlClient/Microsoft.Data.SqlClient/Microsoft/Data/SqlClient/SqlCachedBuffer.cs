using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.Data.SqlTypes;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x02000056 RID: 86
	internal sealed class SqlCachedBuffer : INullable
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x000027D1 File Offset: 0x000009D1
		private SqlCachedBuffer()
		{
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x000131F1 File Offset: 0x000113F1
		private SqlCachedBuffer(List<byte[]> cachedBytes)
		{
			this._cachedBytes = cachedBytes;
		}

		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06000850 RID: 2128 RVA: 0x00013200 File Offset: 0x00011400
		internal List<byte[]> CachedBytes
		{
			get
			{
				return this._cachedBytes;
			}
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x00013208 File Offset: 0x00011408
		internal static bool TryCreate(SqlMetaDataPriv metadata, TdsParser parser, TdsParserStateObject stateObj, out SqlCachedBuffer buffer)
		{
			List<byte[]> list = new List<byte[]>();
			buffer = null;
			ulong num;
			if (!parser.TryPlpBytesLeft(stateObj, out num))
			{
				return false;
			}
			while (num != 0UL)
			{
				do
				{
					int num2 = ((num > 2048UL) ? 2048 : ((int)num));
					byte[] array = new byte[num2];
					if (!stateObj.TryReadPlpBytes(ref array, 0, num2, out num2))
					{
						return false;
					}
					if (list.Count == 0)
					{
						SqlCachedBuffer.AddByteOrderMark(array, list);
					}
					list.Add(array);
					num -= (ulong)((long)num2);
				}
				while (num > 0UL);
				if (!parser.TryPlpBytesLeft(stateObj, out num))
				{
					return false;
				}
				if (num <= 0UL)
				{
					break;
				}
				continue;
			}
			buffer = new SqlCachedBuffer(list);
			return true;
		}

		// Token: 0x06000852 RID: 2130 RVA: 0x00013293 File Offset: 0x00011493
		private static void AddByteOrderMark(byte[] byteArr, List<byte[]> cachedBytes)
		{
			if (byteArr.Length < 2 || byteArr[0] != 223 || byteArr[1] != 255)
			{
				cachedBytes.Add(TdsEnums.XMLUNICODEBOMBYTES);
			}
		}

		// Token: 0x06000853 RID: 2131 RVA: 0x000132BA File Offset: 0x000114BA
		internal Stream ToStream()
		{
			return new SqlCachedStream(this);
		}

		// Token: 0x06000854 RID: 2132 RVA: 0x000132C4 File Offset: 0x000114C4
		public override string ToString()
		{
			if (this.IsNull)
			{
				throw new SqlNullValueException();
			}
			if (this._cachedBytes.Count == 0)
			{
				return string.Empty;
			}
			SqlXml sqlXml = new SqlXml(this.ToStream());
			return sqlXml.Value;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x00013304 File Offset: 0x00011504
		internal SqlString ToSqlString()
		{
			if (this.IsNull)
			{
				return SqlString.Null;
			}
			string text = this.ToString();
			return new SqlString(text);
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001332C File Offset: 0x0001152C
		internal SqlXml ToSqlXml()
		{
			return new SqlXml(this.ToStream());
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00013346 File Offset: 0x00011546
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal XmlReader ToXmlReader()
		{
			return SqlTypeWorkarounds.SqlXmlCreateSqlXmlReader(this.ToStream(), false, false);
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00013355 File Offset: 0x00011555
		public bool IsNull
		{
			get
			{
				return this._cachedBytes == null;
			}
		}

		// Token: 0x04000137 RID: 311
		public static readonly SqlCachedBuffer Null = new SqlCachedBuffer();

		// Token: 0x04000138 RID: 312
		private const int MaxChunkSize = 2048;

		// Token: 0x04000139 RID: 313
		private readonly List<byte[]> _cachedBytes;
	}
}
