using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using SAP.Middleware.Connector;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200001E RID: 30
	internal sealed class BasXmlFunctionWriter : IDisposable
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000064A0 File Offset: 0x000046A0
		static BasXmlFunctionWriter()
		{
			BasXmlFunctionWriter.AddToHeader(new byte[]
			{
				66, 88, 77, 76, 63, 3, 86, 69, 82, 3,
				48, 46, 55, 63, 3, 69, 78, 67, 5, 117,
				116, 102, 45, 56, 43, 3, 97, 115, 120
			});
			BasXmlFunctionWriter.AddToHeader(43);
			BasXmlFunctionWriter.AddToHeader("http://www.sap.com/abapxml");
			BasXmlFunctionWriter.AddToHeader(new byte[]
			{
				58, 2, 3, 43, 4, 97, 98, 97, 112, 60,
				4, 2, 43, 7, 118, 101, 114, 115, 105, 111,
				110, 64, 5, 1, 65, 3, 49, 46, 48, 43,
				7, 97, 115, 120, 104, 105, 110, 116
			});
			BasXmlFunctionWriter.AddToHeader(43);
			BasXmlFunctionWriter.AddToHeader("http://www.sap.com/abapxml/hint");
			BasXmlFunctionWriter.AddToHeader(new byte[]
			{
				58, 6, 7, 42, 3, 43, 6, 118, 97, 108,
				117, 101, 115, 60, 8, 2
			});
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000652C File Offset: 0x0000472C
		public BasXmlFunctionWriter(string filename, IEnumerable<string> skipElements)
		{
			this.markers = new Dictionary<string, int>();
			this.currentMarker = 8;
			this.outputStream = new BasXmlFunctionWriter.BufferedOutputStream(filename);
			this.outputStream.Write(BasXmlFunctionWriter.header, 0, BasXmlFunctionWriter.headerOffset);
			this.skipElements = ((skipElements != null) ? new HashSet<string>(skipElements) : new HashSet<string>());
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00006594 File Offset: 0x00004794
		public void WriteFunction(IRfcFunction function)
		{
			for (int i = 0; i < function.ElementCount; i++)
			{
				string name = function.GetElementMetadata(i).Name;
				if (!this.skipElements.Contains(name))
				{
					this.WriteElement(function, i, name);
				}
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x000065D8 File Offset: 0x000047D8
		public void WriteStructure(IRfcStructure structure)
		{
			for (int i = 0; i < structure.Metadata.FieldCount; i++)
			{
				this.WriteElement(structure, i, structure.Metadata[i].Name);
			}
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00006614 File Offset: 0x00004814
		public void WriteTable(IRfcTable table)
		{
			int currentIndex = table.CurrentIndex;
			RfcStructureMetadata lineType = table.Metadata.LineType;
			if (lineType.FieldCount == 1 && lineType[0].Name.Length == 0)
			{
				for (int i = 0; i < table.RowCount; i++)
				{
					table.CurrentIndex = i;
					this.WriteElement(table, 0, "item");
				}
			}
			else
			{
				for (int j = 0; j < table.RowCount; j++)
				{
					table.CurrentIndex = j;
					this.OpenTag("item");
					for (int k = 0; k < lineType.FieldCount; k++)
					{
						this.WriteElement(table, k, lineType[k].Name);
					}
					this.CloseTag();
				}
			}
			if (currentIndex >= 0)
			{
				table.CurrentIndex = currentIndex;
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000066D8 File Offset: 0x000048D8
		private void WriteElement(IRfcDataContainer dataContainer, int index, string tag)
		{
			this.OpenTag(tag);
			RfcDataType dataType = dataContainer.GetElementMetadata(index).DataType;
			switch (dataType)
			{
			case 1:
				break;
			case 2:
			case 3:
				goto IL_018F;
			case 4:
			case 5:
				this.WriteTextNode(this.encoding.GetBytes(dataContainer.GetCharArray(index)));
				goto IL_01B0;
			case 6:
				this.WriteTextNode(this.encoding.GetBytes(dataContainer.GetString(index).Replace(',', '.') + "Z"));
				goto IL_01B0;
			case 7:
			case 8:
				this.WriteTextNode(this.encoding.GetBytes(dataContainer.GetString(index) + "Z"));
				goto IL_01B0;
			default:
				if (dataType == 14)
				{
					this.WriteTextNode(this.encoding.GetBytes("--" + dataContainer.GetString(index)));
					goto IL_01B0;
				}
				switch (dataType)
				{
				case 23:
					break;
				case 24:
				{
					IRfcStructure structure = dataContainer.GetStructure(index);
					if (structure != null)
					{
						this.WriteStructure(structure);
						goto IL_01B0;
					}
					goto IL_01B0;
				}
				case 25:
				{
					IRfcTable table = dataContainer.GetTable(index);
					if (table != null)
					{
						this.WriteTableRowCount(table.RowCount);
						this.WriteTable(table);
						goto IL_01B0;
					}
					this.WriteTableRowCount(0);
					goto IL_01B0;
				}
				case 26:
					throw new NotSupportedException();
				default:
					goto IL_018F;
				}
				break;
			}
			byte[] array = (byte[])dataContainer.GetObject(index);
			if (array == null && dataType == 1)
			{
				array = dataContainer.GetByteArray(index);
			}
			if (array != null && array.Length != 0)
			{
				this.outputStream.Write(66);
				this.outputStream.Write(array.Length);
				this.outputStream.Write(array);
				goto IL_01B0;
			}
			goto IL_01B0;
			IL_018F:
			if (dataContainer.GetObject(index) != null)
			{
				this.WriteTextNode(this.encoding.GetBytes(dataContainer.GetString(index)));
			}
			IL_01B0:
			this.CloseTag();
		}

		// Token: 0x0600017E RID: 382 RVA: 0x0000689C File Offset: 0x00004A9C
		private int WriteStringPart(string name)
		{
			int num;
			if (!this.markers.TryGetValue(name, out num))
			{
				this.outputStream.Write(43);
				Dictionary<string, int> dictionary = this.markers;
				int num2 = this.currentMarker + 1;
				this.currentMarker = num2;
				dictionary[name] = num2;
				byte[] bytes = this.encoding.GetBytes(Utils.EscapeBasXmlIdentifier(name));
				this.outputStream.Write(bytes.Length);
				this.outputStream.Write(bytes);
				return this.currentMarker;
			}
			return num;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00006917 File Offset: 0x00004B17
		private void WriteTextNode(string text)
		{
			this.WriteTextNode(this.encoding.GetBytes(text));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000692B File Offset: 0x00004B2B
		private void WriteTextNode(byte[] bytes)
		{
			if (bytes.Length != 0)
			{
				this.outputStream.Write(84);
				this.outputStream.Write(bytes.Length);
				this.outputStream.Write(bytes);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00006958 File Offset: 0x00004B58
		private void OpenTag(string elementName)
		{
			this.OpenTag(this.WriteStringPart(elementName));
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00006967 File Offset: 0x00004B67
		private void OpenTag(int id)
		{
			this.outputStream.Write(60);
			this.outputStream.Write(id);
			this.outputStream.Write(1);
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000698E File Offset: 0x00004B8E
		private void CloseTag()
		{
			this.outputStream.Write(62);
		}

		// Token: 0x06000184 RID: 388 RVA: 0x000069A0 File Offset: 0x00004BA0
		private void WriteTableRowCount(int rowCount)
		{
			if (this.linesMarker == 0)
			{
				this.linesMarker = this.WriteStringPart("lines");
			}
			this.outputStream.Write(64);
			this.outputStream.Write(this.linesMarker);
			this.outputStream.Write(3);
			this.outputStream.Write(65);
			byte[] bytes = this.encoding.GetBytes(rowCount.ToString(CultureInfo.InvariantCulture));
			this.outputStream.Write(bytes.Length);
			this.outputStream.Write(bytes);
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00006A30 File Offset: 0x00004C30
		private void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				if (disposing && this.outputStream != null)
				{
					this.outputStream.Write(62);
					this.outputStream.Write(62);
					this.outputStream.Close();
				}
				this.isDisposed = true;
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00006A7C File Offset: 0x00004C7C
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00006A85 File Offset: 0x00004C85
		private static void AddToHeader(byte b)
		{
			BasXmlFunctionWriter.header[BasXmlFunctionWriter.headerOffset++] = b;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00006A9B File Offset: 0x00004C9B
		private static void AddToHeader(params byte[] byteArray)
		{
			Buffer.BlockCopy(byteArray, 0, BasXmlFunctionWriter.header, BasXmlFunctionWriter.headerOffset, byteArray.Length);
			BasXmlFunctionWriter.headerOffset += byteArray.Length;
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00006AC0 File Offset: 0x00004CC0
		private static void AddToHeader(string s)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			BasXmlFunctionWriter.header[BasXmlFunctionWriter.headerOffset++] = Convert.ToByte(bytes.Length);
			BasXmlFunctionWriter.AddToHeader(bytes);
		}

		// Token: 0x04000080 RID: 128
		private static int headerOffset = 0;

		// Token: 0x04000081 RID: 129
		private static readonly byte[] header = new byte[256];

		// Token: 0x04000082 RID: 130
		private readonly BasXmlFunctionWriter.BufferedOutputStream outputStream;

		// Token: 0x04000083 RID: 131
		private readonly Encoding encoding = Encoding.UTF8;

		// Token: 0x04000084 RID: 132
		private readonly Dictionary<string, int> markers;

		// Token: 0x04000085 RID: 133
		private readonly HashSet<string> skipElements;

		// Token: 0x04000086 RID: 134
		private int currentMarker;

		// Token: 0x04000087 RID: 135
		private int linesMarker;

		// Token: 0x04000088 RID: 136
		private bool isDisposed;

		// Token: 0x02000063 RID: 99
		private class BufferedOutputStream
		{
			// Token: 0x060003B0 RID: 944 RVA: 0x0000ED2F File Offset: 0x0000CF2F
			internal BufferedOutputStream(string filename)
			{
				if (filename == null)
				{
					throw new ArgumentNullException("filename");
				}
				this.buffer = new byte[8192];
				this.fileStream = new FileStream(filename, FileMode.Create);
			}

			// Token: 0x060003B1 RID: 945 RVA: 0x0000ED64 File Offset: 0x0000CF64
			public void Write(byte b)
			{
				if (this.position >= 8192)
				{
					this.Flush();
				}
				byte[] array = this.buffer;
				int num = this.position;
				this.position = num + 1;
				array[num] = b;
			}

			// Token: 0x060003B2 RID: 946 RVA: 0x0000ED9D File Offset: 0x0000CF9D
			public void Write(byte[] bytearray)
			{
				if (bytearray != null)
				{
					this.Write(bytearray, 0, bytearray.Length);
				}
			}

			// Token: 0x060003B3 RID: 947 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
			public void Write(byte[] bytearray, int offset, int length)
			{
				if (length > 8192)
				{
					this.Flush();
					this.fileStream.Write(bytearray, offset, length);
					return;
				}
				if (this.position + length > 8192)
				{
					this.Flush();
				}
				Buffer.BlockCopy(bytearray, offset, this.buffer, this.position, length);
				this.position += length;
			}

			// Token: 0x060003B4 RID: 948 RVA: 0x0000EE14 File Offset: 0x0000D014
			internal void Write(int dataLength)
			{
				if (dataLength < 128)
				{
					this.Write(Convert.ToByte(dataLength));
					return;
				}
				int num;
				if (dataLength < 2048)
				{
					if (this.position + 2 > 8192)
					{
						this.Flush();
					}
					byte[] array = this.buffer;
					num = this.position;
					this.position = num + 1;
					array[num] = (byte)((dataLength >> 6) | 192);
					byte[] array2 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array2[num] = (byte)((dataLength & 63) | 128);
					return;
				}
				if (dataLength < 65536)
				{
					if (this.position + 3 > 8192)
					{
						this.Flush();
					}
					byte[] array3 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array3[num] = (byte)((dataLength >> 12) | 224);
					byte[] array4 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array4[num] = (byte)(((dataLength >> 6) & 63) | 128);
					byte[] array5 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array5[num] = (byte)((dataLength & 63) | 128);
					return;
				}
				if (dataLength < 2097152)
				{
					if (this.position + 4 > 8192)
					{
						this.Flush();
					}
					byte[] array6 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array6[num] = (byte)((dataLength >> 18) | 240);
					byte[] array7 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array7[num] = (byte)(((dataLength >> 12) & 63) | 128);
					byte[] array8 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array8[num] = (byte)(((dataLength >> 6) & 63) | 128);
					byte[] array9 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array9[num] = (byte)((dataLength & 63) | 128);
					return;
				}
				if (dataLength < 67108864)
				{
					if (this.position + 5 > 8192)
					{
						this.Flush();
					}
					byte[] array10 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array10[num] = (byte)((dataLength >> 24) | 248);
					byte[] array11 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array11[num] = (byte)(((dataLength >> 18) & 63) | 128);
					byte[] array12 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array12[num] = (byte)(((dataLength >> 12) & 63) | 128);
					byte[] array13 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array13[num] = (byte)(((dataLength >> 6) & 63) | 128);
					byte[] array14 = this.buffer;
					num = this.position;
					this.position = num + 1;
					array14[num] = (byte)((dataLength & 63) | 128);
					return;
				}
				if (this.position + 6 > 8192)
				{
					this.Flush();
				}
				byte[] array15 = this.buffer;
				num = this.position;
				this.position = num + 1;
				array15[num] = (byte)((dataLength >> 30) | 252);
				byte[] array16 = this.buffer;
				num = this.position;
				this.position = num + 1;
				array16[num] = (byte)(((dataLength >> 24) & 63) | 128);
				byte[] array17 = this.buffer;
				num = this.position;
				this.position = num + 1;
				array17[num] = (byte)(((dataLength >> 18) & 63) | 128);
				byte[] array18 = this.buffer;
				num = this.position;
				this.position = num + 1;
				array18[num] = (byte)(((dataLength >> 12) & 63) | 128);
				byte[] array19 = this.buffer;
				num = this.position;
				this.position = num + 1;
				array19[num] = (byte)(((dataLength >> 6) & 63) | 128);
				byte[] array20 = this.buffer;
				num = this.position;
				this.position = num + 1;
				array20[num] = (byte)((dataLength & 63) | 128);
			}

			// Token: 0x060003B5 RID: 949 RVA: 0x0000F1A1 File Offset: 0x0000D3A1
			private void Flush()
			{
				if (this.position > 0)
				{
					this.fileStream.Write(this.buffer, 0, this.position);
					this.position = 0;
				}
			}

			// Token: 0x060003B6 RID: 950 RVA: 0x0000F1CB File Offset: 0x0000D3CB
			internal void Close()
			{
				this.Flush();
				this.fileStream.Dispose();
			}

			// Token: 0x040002C2 RID: 706
			private const int BufferCapacity = 8192;

			// Token: 0x040002C3 RID: 707
			private readonly byte[] buffer;

			// Token: 0x040002C4 RID: 708
			private readonly FileStream fileStream;

			// Token: 0x040002C5 RID: 709
			private int position;
		}
	}
}
