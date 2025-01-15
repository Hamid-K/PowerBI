using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200001F RID: 31
	internal class BasXmlReader : IDisposable
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00006AFC File Offset: 0x00004CFC
		protected BasXmlReader()
		{
			this.depth = 0;
			this.remainingBufferLength = 0;
			this.identifiers = new List<string>
			{
				string.Empty,
				string.Empty
			};
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00006B4C File Offset: 0x00004D4C
		public BasXmlReader(ColumnProvider columnProvider)
			: this()
		{
			if (columnProvider == null || columnProvider.ColumnCount <= 0)
			{
				throw new ArgumentNullException("columnProvider");
			}
			this.fieldCount = columnProvider.ColumnCount;
			this.columnProvider = columnProvider;
			this.identifierIndexToColumnProviderIndex = new List<int> { -1, -1 };
			this.row = null;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00006BA9 File Offset: 0x00004DA9
		protected virtual string TagOfInterest
		{
			get
			{
				return "item";
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00006BB0 File Offset: 0x00004DB0
		private object[] Row
		{
			get
			{
				if (this.row == null)
				{
					this.row = ((this.columnProvider != null) ? this.columnProvider.StartBuildingRecord() : new object[this.fieldCount]);
				}
				return this.row;
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00006BE6 File Offset: 0x00004DE6
		public IEnumerator<object[]> Read(Stream basXmlStream)
		{
			this.stream = basXmlStream;
			using (basXmlStream)
			{
				this.ParseHeader();
				foreach (object[] array in this.Read())
				{
					yield return array;
				}
				IEnumerator<object[]> enumerator = null;
			}
			Stream stream = null;
			this.stream = null;
			yield break;
			yield break;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00006BFC File Offset: 0x00004DFC
		protected virtual IEnumerable<object[]> Read()
		{
			while (this.ReadNext())
			{
				int num = this.depth;
				int? num2 = this.itemDepth - 1;
				if (((num == num2.GetValueOrDefault()) & (num2 != null)) && this.row != null)
				{
					if (this.columnProvider != null)
					{
						this.columnProvider.FinishBuildingRecord(this.row);
					}
					yield return this.row;
					this.row = null;
				}
			}
			yield break;
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006C0C File Offset: 0x00004E0C
		protected bool ReadNext()
		{
			int num;
			while (this.TryReadByte(out num))
			{
				if (num <= 43)
				{
					if (num != 42)
					{
						if (num == 43)
						{
							this.ReadValue(BasXmlReader.TokenKind.Identifier);
						}
					}
					else
					{
						this.SkipTokens(1);
					}
				}
				else
				{
					switch (num)
					{
					case 58:
						this.SkipTokens(2);
						break;
					case 59:
					case 61:
					case 63:
						break;
					case 60:
						this.ReadToken(BasXmlReader.TokenKind.Tag);
						break;
					case 62:
						this.EndTag();
						return true;
					case 64:
						this.ReadToken(BasXmlReader.TokenKind.Attribute);
						break;
					case 65:
						this.ReadValue(BasXmlReader.TokenKind.AttributeText);
						break;
					default:
						if (num == 84)
						{
							this.ReadValue(BasXmlReader.TokenKind.Text);
						}
						break;
					}
				}
			}
			return false;
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00006CB4 File Offset: 0x00004EB4
		protected void ParseHeader()
		{
			if (!this.CheckSequence(new byte[] { 66, 88, 77, 76 }))
			{
				throw new SapBwException(Resources.InvalidBxmlHeader);
			}
			if (!this.CheckSequence(new byte[] { 63, 3, 86, 69, 82 }))
			{
				throw new SapBwException(Resources.InvalidBxmlVersion);
			}
			this.ReadString();
			if (this.CheckSequence(new byte[] { 63, 3, 69, 78, 67 }))
			{
				this.encoding = Encoding.GetEncoding(this.ReadString());
				return;
			}
			throw new Exception(Resources.InvalidBxmlEncoding);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00006D58 File Offset: 0x00004F58
		private bool CheckSequence(params byte[] characters)
		{
			foreach (byte b in characters)
			{
				if ((byte)this.stream.ReadByte() != b)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00006D8C File Offset: 0x00004F8C
		private void SkipTokens(int tokenCount)
		{
			for (int i = 0; i < tokenCount; i++)
			{
				int num;
				this.TryExtractDataLength(out num);
			}
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00006DB0 File Offset: 0x00004FB0
		private void SkipBytes(int byteCount)
		{
			for (int i = 0; i < byteCount; i++)
			{
				if (this.stream.ReadByte() == -1)
				{
					throw new SapBwException(Resources.UnexpectedEOF);
				}
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00006DE8 File Offset: 0x00004FE8
		private void ReadToken(BasXmlReader.TokenKind tokenKind)
		{
			int num;
			if (!this.TryExtractDataLength(out num))
			{
				return;
			}
			if (tokenKind == BasXmlReader.TokenKind.Tag)
			{
				this.fieldName = this.identifiers[num];
				this.StartTag(num);
				this.SkipTokens(1);
				return;
			}
			if (tokenKind != BasXmlReader.TokenKind.Attribute)
			{
				throw new SapBwException(Resources.InvalidBxmlTokenKind(tokenKind));
			}
			this.SkipTokens(1);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00006E48 File Offset: 0x00005048
		protected virtual void StartTag(int identifierIndex)
		{
			this.depth++;
			if (this.itemDepth == null && this.fieldName == this.TagOfInterest)
			{
				this.itemDepth = new int?(this.depth);
			}
			if (this.itemDepth != null)
			{
				int num = this.depth;
				int? num2 = this.itemDepth;
				if ((num > num2.GetValueOrDefault()) & (num2 != null))
				{
					this.fieldIndex = this.identifierIndexToColumnProviderIndex[identifierIndex];
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00006ED3 File Offset: 0x000050D3
		protected virtual void EndTag()
		{
			this.depth--;
			this.fieldName = null;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00006EEC File Offset: 0x000050EC
		private string ReadString()
		{
			int num;
			if (!this.TryExtractDataLength(out num))
			{
				throw new SapBwException(Resources.UnexpectedStringLength);
			}
			return this.ConvertToString(num);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006F1C File Offset: 0x0000511C
		protected string ConvertToString(int dataLength)
		{
			byte[] array = new byte[dataLength];
			this.stream.Read(array, 0, dataLength);
			return this.encoding.GetString(array, 0, dataLength);
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006F50 File Offset: 0x00005150
		private void ReadValue(BasXmlReader.TokenKind tokenKind)
		{
			int num;
			if (this.TryExtractDataLength(out num))
			{
				switch (tokenKind)
				{
				case BasXmlReader.TokenKind.Identifier:
					this.AddIdentifier(num);
					return;
				case BasXmlReader.TokenKind.Text:
					this.SaveValue(num);
					return;
				case BasXmlReader.TokenKind.AttributeText:
					this.SkipBytes(num);
					return;
				}
				throw new SapBwException(Resources.InvalidBxmlTokenKind(tokenKind));
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00006FB4 File Offset: 0x000051B4
		protected virtual void SaveValue(int length)
		{
			if (this.fieldName == null)
			{
				throw new SapBwException(Resources.NullFieldName);
			}
			if (this.fieldIndex != -1)
			{
				string text = this.ConvertToString(length);
				MdxColumn mdxColumn = this.columnProvider[this.fieldIndex];
				if (mdxColumn.Length > 0 && mdxColumn.Length < text.Length && mdxColumn.DataType == SapBwDataType.Char)
				{
					text = text.Substring(0, mdxColumn.Length);
				}
				object obj;
				if (mdxColumn.TryExtractValue(text, out obj))
				{
					this.Row[this.fieldIndex] = obj;
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00007044 File Offset: 0x00005244
		protected void AddIdentifier(int length)
		{
			string text = this.ConvertToString(length);
			if (Utils.IsEscaped(text))
			{
				text = Utils.UnescapeBasXmlIdentifier(text);
			}
			if (this.columnProvider != null)
			{
				int num;
				this.identifierIndexToColumnProviderIndex.Add(this.columnProvider.TryGetFieldIndex(text, out num) ? num : (-1));
			}
			this.identifiers.Add(text);
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000709B File Offset: 0x0000529B
		private bool TryReadByte(out int singlebyte)
		{
			singlebyte = this.stream.ReadByte();
			return singlebyte != -1;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000070B4 File Offset: 0x000052B4
		private bool TryExtractDataLength(out int dataLength)
		{
			dataLength = -1;
			int num;
			if (!this.TryReadByte(out num))
			{
				return false;
			}
			if ((num & 128) == 0)
			{
				dataLength = num;
			}
			else
			{
				int num2 = num & 240;
				int num3;
				int num4;
				if (num2 <= 208)
				{
					if (num2 == 192 || num2 == 208)
					{
						if (this.TryReadByte(out num3) && (num3 & 192) == 128)
						{
							dataLength = ((num & 31) << 6) | (num3 & 63);
						}
					}
				}
				else if (num2 != 224)
				{
					if (num2 == 240)
					{
						int num5;
						int num6;
						int num7;
						if ((num & 8) == 0)
						{
							if (this.TryReadByte(out num3) && this.TryReadByte(out num4) && this.TryReadByte(out num5) && (num3 & 192) == 128 && (num4 & 192) == 128 && (num5 & 192) == 128)
							{
								dataLength = ((((((num & 7) << 6) | (num3 & 63)) << 6) | (num4 & 63)) << 6) | (num5 & 63);
							}
						}
						else if ((num & 4) == 0)
						{
							if (this.TryReadByte(out num3) && this.TryReadByte(out num4) && this.TryReadByte(out num5) && this.TryReadByte(out num6) && (num3 & 192) == 128 && (num4 & 192) == 128 && (num5 & 192) == 128 && (num6 & 192) == 128)
							{
								dataLength = ((((((((num & 3) << 6) | (num3 & 63)) << 6) | (num4 & 63)) << 6) | (num5 & 63)) << 6) | (num6 & 63);
							}
						}
						else if ((num & 2) == 0 && this.TryReadByte(out num3) && this.TryReadByte(out num4) && this.TryReadByte(out num5) && this.TryReadByte(out num6) && this.TryReadByte(out num7) && (num3 & 192) == 128 && (num4 & 192) == 128 && (num5 & 192) == 128 && (num6 & 192) == 128 && (num7 & 192) == 128)
						{
							dataLength = ((((((((((num & 1) << 6) | (num3 & 63)) << 6) | (num4 & 63)) << 6) | (num5 & 63)) << 6) | (num6 & 63)) << 6) | (num7 & 63);
						}
					}
				}
				else if (this.TryReadByte(out num3) && this.TryReadByte(out num4) && (num3 & 192) == 128 && (num4 & 192) == 128)
				{
					dataLength = ((((num & 15) << 6) | (num3 & 63)) << 6) | (num4 & 63);
				}
				if (dataLength < 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000738C File Offset: 0x0000558C
		public virtual void Dispose()
		{
			if (this.stream != null)
			{
				this.stream.Dispose();
				this.stream = null;
			}
		}

		// Token: 0x04000089 RID: 137
		protected const int DefaultBaseBufferCapacity = 16384;

		// Token: 0x0400008A RID: 138
		private readonly ColumnProvider columnProvider;

		// Token: 0x0400008B RID: 139
		private readonly List<int> identifierIndexToColumnProviderIndex;

		// Token: 0x0400008C RID: 140
		private readonly List<string> identifiers;

		// Token: 0x0400008D RID: 141
		protected Stream stream;

		// Token: 0x0400008E RID: 142
		protected Encoding encoding = Encoding.UTF8;

		// Token: 0x0400008F RID: 143
		protected int depth;

		// Token: 0x04000090 RID: 144
		protected int remainingBufferLength;

		// Token: 0x04000091 RID: 145
		protected object[] row;

		// Token: 0x04000092 RID: 146
		protected int fieldCount;

		// Token: 0x04000093 RID: 147
		protected int fieldIndex;

		// Token: 0x04000094 RID: 148
		protected string fieldName;

		// Token: 0x04000095 RID: 149
		protected int? itemDepth;

		// Token: 0x04000096 RID: 150
		protected bool parseDocumentHeader;

		// Token: 0x02000064 RID: 100
		protected enum TokenKind
		{
			// Token: 0x040002C7 RID: 711
			Identifier,
			// Token: 0x040002C8 RID: 712
			Tag,
			// Token: 0x040002C9 RID: 713
			Text,
			// Token: 0x040002CA RID: 714
			Attribute,
			// Token: 0x040002CB RID: 715
			AttributeText
		}
	}
}
