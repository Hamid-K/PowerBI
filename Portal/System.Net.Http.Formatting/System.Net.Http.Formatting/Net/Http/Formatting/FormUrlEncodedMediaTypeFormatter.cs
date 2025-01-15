using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000040 RID: 64
	public class FormUrlEncodedMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x06000266 RID: 614 RVA: 0x0000889C File Offset: 0x00006A9C
		public FormUrlEncodedMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationFormUrlEncodedMediaType);
			this._isDerived = base.GetType() != typeof(FormUrlEncodedMediaTypeFormatter);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000088F0 File Offset: 0x00006AF0
		protected FormUrlEncodedMediaTypeFormatter(FormUrlEncodedMediaTypeFormatter formatter)
			: base(formatter)
		{
			this.MaxDepth = formatter.MaxDepth;
			this.ReadBufferSize = formatter.ReadBufferSize;
			this._isDerived = base.GetType() != typeof(FormUrlEncodedMediaTypeFormatter);
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000268 RID: 616 RVA: 0x0000894D File Offset: 0x00006B4D
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationFormUrlEncodedMediaType;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000269 RID: 617 RVA: 0x00008954 File Offset: 0x00006B54
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000895C File Offset: 0x00006B5C
		public int MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600026B RID: 619 RVA: 0x00008980 File Offset: 0x00006B80
		// (set) Token: 0x0600026C RID: 620 RVA: 0x00008988 File Offset: 0x00006B88
		public int ReadBufferSize
		{
			get
			{
				return this._readBufferSize;
			}
			set
			{
				if (value < 256)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 256);
				}
				this._readBufferSize = value;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000089B4 File Offset: 0x00006BB4
		internal override bool CanWriteAnyTypes
		{
			get
			{
				return this._isDerived;
			}
		}

		// Token: 0x0600026E RID: 622 RVA: 0x000089BC File Offset: 0x00006BBC
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return type == typeof(FormDataCollection) || FormattingUtilities.IsJTokenType(type);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000089EC File Offset: 0x00006BEC
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return false;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00008A04 File Offset: 0x00006C04
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			Task<object> task;
			try
			{
				task = Task.FromResult<object>(this.ReadFromStream(type, readStream));
			}
			catch (Exception ex)
			{
				task = TaskHelpers.FromError<object>(ex);
			}
			return task;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00008A60 File Offset: 0x00006C60
		private object ReadFromStream(Type type, Stream readStream)
		{
			IEnumerable<KeyValuePair<string, string>> enumerable = FormUrlEncodedMediaTypeFormatter.ReadFormUrlEncoded(readStream, this.ReadBufferSize);
			object obj;
			if (type == typeof(FormDataCollection))
			{
				obj = new FormDataCollection(enumerable);
			}
			else
			{
				if (!FormattingUtilities.IsJTokenType(type))
				{
					throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
					{
						base.GetType().Name,
						type.Name
					});
				}
				obj = FormUrlEncodedJson.Parse(enumerable, this._maxDepth);
			}
			return obj;
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00008AD8 File Offset: 0x00006CD8
		private static IEnumerable<KeyValuePair<string, string>> ReadFormUrlEncoded(Stream input, int bufferSize)
		{
			byte[] array = new byte[bufferSize];
			bool flag = false;
			List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
			FormUrlEncodedParser formUrlEncodedParser = new FormUrlEncodedParser(list, long.MaxValue);
			int num2;
			for (;;)
			{
				int num;
				try
				{
					num = input.Read(array, 0, array.Length);
					if (num == 0)
					{
						flag = true;
					}
				}
				catch (Exception ex)
				{
					throw Error.InvalidOperation(ex, Resources.ErrorReadingFormUrlEncodedStream, new object[0]);
				}
				num2 = 0;
				ParserState parserState = formUrlEncodedParser.ParseBuffer(array, num, ref num2, flag);
				if (parserState != ParserState.NeedMoreData && parserState != ParserState.Done)
				{
					break;
				}
				if (flag)
				{
					return list;
				}
			}
			throw Error.InvalidOperation(Resources.FormUrlEncodedParseError, new object[] { num2 });
		}

		// Token: 0x040000AC RID: 172
		private const int MinBufferSize = 256;

		// Token: 0x040000AD RID: 173
		private const int DefaultBufferSize = 32768;

		// Token: 0x040000AE RID: 174
		private int _readBufferSize = 32768;

		// Token: 0x040000AF RID: 175
		private int _maxDepth = 256;

		// Token: 0x040000B0 RID: 176
		private readonly bool _isDerived;
	}
}
