using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000051 RID: 81
	internal class FormUrlEncodedParser
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000A960 File Offset: 0x00008B60
		public FormUrlEncodedParser(ICollection<KeyValuePair<string, string>> nameValuePairs, long maxMessageSize)
		{
			if (maxMessageSize < 1L)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxMessageSize", maxMessageSize, 1);
			}
			if (nameValuePairs == null)
			{
				throw Error.ArgumentNull("nameValuePairs");
			}
			this._nameValuePairs = nameValuePairs;
			this._maxMessageSize = maxMessageSize;
			this._currentNameValuePair = new FormUrlEncodedParser.CurrentNameValuePair();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0000A9B8 File Offset: 0x00008BB8
		public ParserState ParseBuffer(byte[] buffer, int bytesReady, ref int bytesConsumed, bool isFinal)
		{
			if (buffer == null)
			{
				throw Error.ArgumentNull("buffer");
			}
			ParserState parserState = ParserState.NeedMoreData;
			if (bytesConsumed >= bytesReady)
			{
				if (isFinal)
				{
					parserState = this.CopyCurrent(parserState);
				}
				return parserState;
			}
			try
			{
				parserState = FormUrlEncodedParser.ParseNameValuePairs(buffer, bytesReady, ref bytesConsumed, ref this._nameValueState, this._maxMessageSize, ref this._totalBytesConsumed, this._currentNameValuePair, this._nameValuePairs);
				if (isFinal)
				{
					parserState = this.CopyCurrent(parserState);
				}
			}
			catch (Exception)
			{
				parserState = ParserState.Invalid;
			}
			return parserState;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0000AA34 File Offset: 0x00008C34
		private static ParserState ParseNameValuePairs(byte[] buffer, int bytesReady, ref int bytesConsumed, ref FormUrlEncodedParser.NameValueState nameValueState, long maximumLength, ref long totalBytesConsumed, FormUrlEncodedParser.CurrentNameValuePair currentNameValuePair, ICollection<KeyValuePair<string, string>> nameValuePairs)
		{
			int num = bytesConsumed;
			ParserState parserState = ParserState.DataTooBig;
			long num2 = ((maximumLength <= 0L) ? long.MaxValue : (maximumLength - totalBytesConsumed + (long)num));
			if ((long)bytesReady < num2)
			{
				parserState = ParserState.NeedMoreData;
				num2 = (long)bytesReady;
			}
			FormUrlEncodedParser.NameValueState nameValueState2 = nameValueState;
			if (nameValueState2 != FormUrlEncodedParser.NameValueState.Name)
			{
				if (nameValueState2 != FormUrlEncodedParser.NameValueState.Value)
				{
					goto IL_0176;
				}
				goto IL_00F1;
			}
			int num3;
			int num4;
			for (;;)
			{
				IL_003F:
				num3 = bytesConsumed;
				while (buffer[bytesConsumed] != 61 && buffer[bytesConsumed] != 38)
				{
					num4 = bytesConsumed + 1;
					bytesConsumed = num4;
					if ((long)num4 == num2)
					{
						goto Block_5;
					}
				}
				if (bytesConsumed > num3)
				{
					string @string = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentNameValuePair.Name.Append(@string);
				}
				if (buffer[bytesConsumed] == 61)
				{
					goto Block_8;
				}
				currentNameValuePair.CopyNameOnlyTo(nameValuePairs);
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if ((long)num4 == num2)
				{
					goto Block_10;
				}
			}
			Block_5:
			string string2 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
			currentNameValuePair.Name.Append(string2);
			goto IL_0176;
			Block_8:
			nameValueState = FormUrlEncodedParser.NameValueState.Value;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if ((long)num4 != num2)
			{
				goto IL_00F1;
			}
			Block_10:
			goto IL_0176;
			IL_00F1:
			num3 = bytesConsumed;
			while (buffer[bytesConsumed] != 38)
			{
				num4 = bytesConsumed + 1;
				bytesConsumed = num4;
				if ((long)num4 == num2)
				{
					string string3 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
					currentNameValuePair.Value.Append(string3);
					goto IL_0176;
				}
			}
			if (bytesConsumed > num3)
			{
				string string4 = Encoding.UTF8.GetString(buffer, num3, bytesConsumed - num3);
				currentNameValuePair.Value.Append(string4);
			}
			currentNameValuePair.CopyTo(nameValuePairs);
			nameValueState = FormUrlEncodedParser.NameValueState.Name;
			num4 = bytesConsumed + 1;
			bytesConsumed = num4;
			if ((long)num4 != num2)
			{
				goto IL_003F;
			}
			IL_0176:
			totalBytesConsumed += (long)(bytesConsumed - num);
			return parserState;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0000ABC4 File Offset: 0x00008DC4
		private ParserState CopyCurrent(ParserState parseState)
		{
			if (this._nameValueState == FormUrlEncodedParser.NameValueState.Name)
			{
				if (this._totalBytesConsumed > 0L)
				{
					this._currentNameValuePair.CopyNameOnlyTo(this._nameValuePairs);
				}
			}
			else
			{
				this._currentNameValuePair.CopyTo(this._nameValuePairs);
			}
			if (parseState != ParserState.NeedMoreData)
			{
				return parseState;
			}
			return ParserState.Done;
		}

		// Token: 0x040000ED RID: 237
		private const int MinMessageSize = 1;

		// Token: 0x040000EE RID: 238
		private long _totalBytesConsumed;

		// Token: 0x040000EF RID: 239
		private long _maxMessageSize;

		// Token: 0x040000F0 RID: 240
		private FormUrlEncodedParser.NameValueState _nameValueState;

		// Token: 0x040000F1 RID: 241
		private ICollection<KeyValuePair<string, string>> _nameValuePairs;

		// Token: 0x040000F2 RID: 242
		private readonly FormUrlEncodedParser.CurrentNameValuePair _currentNameValuePair;

		// Token: 0x02000081 RID: 129
		private enum NameValueState
		{
			// Token: 0x040001CB RID: 459
			Name,
			// Token: 0x040001CC RID: 460
			Value
		}

		// Token: 0x02000082 RID: 130
		private class CurrentNameValuePair
		{
			// Token: 0x170000E2 RID: 226
			// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000E8CF File Offset: 0x0000CACF
			public StringBuilder Name
			{
				get
				{
					return this._name;
				}
			}

			// Token: 0x170000E3 RID: 227
			// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000E8D7 File Offset: 0x0000CAD7
			public StringBuilder Value
			{
				get
				{
					return this._value;
				}
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
			public void CopyTo(ICollection<KeyValuePair<string, string>> nameValuePairs)
			{
				string text = UriQueryUtility.UrlDecode(this._name.ToString());
				string text2 = UriQueryUtility.UrlDecode(this._value.ToString());
				nameValuePairs.Add(new KeyValuePair<string, string>(text, text2));
				this.Clear();
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x0000E924 File Offset: 0x0000CB24
			public void CopyNameOnlyTo(ICollection<KeyValuePair<string, string>> nameValuePairs)
			{
				string text = UriQueryUtility.UrlDecode(this._name.ToString());
				string empty = string.Empty;
				nameValuePairs.Add(new KeyValuePair<string, string>(text, empty));
				this.Clear();
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x0000E95B File Offset: 0x0000CB5B
			private void Clear()
			{
				this._name.Clear();
				this._value.Clear();
			}

			// Token: 0x040001CD RID: 461
			private const int DefaultNameAllocation = 128;

			// Token: 0x040001CE RID: 462
			private const int DefaultValueAllocation = 2048;

			// Token: 0x040001CF RID: 463
			private readonly StringBuilder _name = new StringBuilder(128);

			// Token: 0x040001D0 RID: 464
			private readonly StringBuilder _value = new StringBuilder(2048);
		}
	}
}
