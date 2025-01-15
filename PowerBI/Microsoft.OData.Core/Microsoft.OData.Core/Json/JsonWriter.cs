using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Microsoft.OData.Buffers;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x0200021B RID: 539
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This class does not own the underlying stream/writer and thus should never dispose it.")]
	internal sealed class JsonWriter : IJsonStreamWriter, IJsonWriter, IDisposable
	{
		// Token: 0x060017AD RID: 6061 RVA: 0x00043740 File Offset: 0x00041940
		internal JsonWriter(TextWriter writer, bool isIeee754Compatible)
			: this(writer, isIeee754Compatible, ODataStringEscapeOption.EscapeNonAscii)
		{
		}

		// Token: 0x060017AE RID: 6062 RVA: 0x0004374B File Offset: 0x0004194B
		internal JsonWriter(TextWriter writer, bool isIeee754Compatible, ODataStringEscapeOption stringEscapeOption)
		{
			this.writer = new NonIndentedTextWriter(writer);
			this.scopes = new Stack<JsonWriter.Scope>();
			this.isIeee754Compatible = isIeee754Compatible;
			this.stringEscapeOption = stringEscapeOption;
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x060017AF RID: 6063 RVA: 0x00043778 File Offset: 0x00041978
		// (set) Token: 0x060017B0 RID: 6064 RVA: 0x00043780 File Offset: 0x00041980
		public ICharArrayPool ArrayPool { get; set; }

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x00043789 File Offset: 0x00041989
		private bool IsWritingJson
		{
			get
			{
				return string.IsNullOrEmpty(this.currentContentType) || this.currentContentType.StartsWith("application/json", StringComparison.Ordinal);
			}
		}

		// Token: 0x060017B2 RID: 6066 RVA: 0x000437AB File Offset: 0x000419AB
		public void StartPaddingFunctionScope()
		{
			this.StartScope(JsonWriter.ScopeType.Padding);
		}

		// Token: 0x060017B3 RID: 6067 RVA: 0x000437B4 File Offset: 0x000419B4
		public void EndPaddingFunctionScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x000437F4 File Offset: 0x000419F4
		public void StartObjectScope()
		{
			this.StartScope(JsonWriter.ScopeType.Object);
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x00043800 File Offset: 0x00041A00
		public void EndObjectScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x00043840 File Offset: 0x00041A40
		public void StartArrayScope()
		{
			this.StartScope(JsonWriter.ScopeType.Array);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004384C File Offset: 0x00041A4C
		public void EndArrayScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x060017B8 RID: 6072 RVA: 0x0004388C File Offset: 0x00041A8C
		public void WriteName(string name)
		{
			JsonWriter.Scope scope = this.scopes.Peek();
			if (scope.ObjectCount != 0)
			{
				this.writer.Write(",");
			}
			JsonWriter.Scope scope2 = scope;
			int objectCount = scope2.ObjectCount;
			scope2.ObjectCount = objectCount + 1;
			JsonValueUtils.WriteEscapedJsonString(this.writer, name, this.stringEscapeOption, ref this.buffer, null);
			this.writer.Write(":");
		}

		// Token: 0x060017B9 RID: 6073 RVA: 0x000438F6 File Offset: 0x00041AF6
		public void WritePaddingFunctionName(string functionName)
		{
			this.writer.Write(functionName);
		}

		// Token: 0x060017BA RID: 6074 RVA: 0x00043904 File Offset: 0x00041B04
		public void WriteValue(bool value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017BB RID: 6075 RVA: 0x00043918 File Offset: 0x00041B18
		public void WriteValue(int value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x0004392C File Offset: 0x00041B2C
		public void WriteValue(float value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017BD RID: 6077 RVA: 0x00043940 File Offset: 0x00041B40
		public void WriteValue(short value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017BE RID: 6078 RVA: 0x00043954 File Offset: 0x00041B54
		public void WriteValue(long value)
		{
			this.WriteValueSeparator();
			if (this.isIeee754Compatible)
			{
				JsonValueUtils.WriteValue(this.writer, value.ToString(CultureInfo.InvariantCulture), this.stringEscapeOption, ref this.buffer, null);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017BF RID: 6079 RVA: 0x000439A0 File Offset: 0x00041BA0
		public void WriteValue(double value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C0 RID: 6080 RVA: 0x000439B4 File Offset: 0x00041BB4
		public void WriteValue(Guid value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x000439C8 File Offset: 0x00041BC8
		public void WriteValue(decimal value)
		{
			this.WriteValueSeparator();
			if (this.isIeee754Compatible)
			{
				JsonValueUtils.WriteValue(this.writer, value.ToString(CultureInfo.InvariantCulture), this.stringEscapeOption, ref this.buffer, null);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C2 RID: 6082 RVA: 0x00043A14 File Offset: 0x00041C14
		public void WriteValue(DateTimeOffset value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x060017C3 RID: 6083 RVA: 0x00043A29 File Offset: 0x00041C29
		public void WriteValue(TimeSpan value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C4 RID: 6084 RVA: 0x00043A3D File Offset: 0x00041C3D
		public void WriteValue(TimeOfDay value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C5 RID: 6085 RVA: 0x00043A51 File Offset: 0x00041C51
		public void WriteValue(Date value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C6 RID: 6086 RVA: 0x00043A65 File Offset: 0x00041C65
		public void WriteValue(byte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C7 RID: 6087 RVA: 0x00043A79 File Offset: 0x00041C79
		public void WriteValue(sbyte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060017C8 RID: 6088 RVA: 0x00043A8D File Offset: 0x00041C8D
		public void WriteValue(string value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value, this.stringEscapeOption, ref this.buffer, null);
		}

		// Token: 0x060017C9 RID: 6089 RVA: 0x00043AAE File Offset: 0x00041CAE
		public void WriteValue(byte[] value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value, ref this.buffer, this.ArrayPool);
		}

		// Token: 0x060017CA RID: 6090 RVA: 0x00043ACE File Offset: 0x00041CCE
		public void WriteRawValue(string rawValue)
		{
			this.WriteValueSeparator();
			this.writer.Write(rawValue);
		}

		// Token: 0x060017CB RID: 6091 RVA: 0x00043AE2 File Offset: 0x00041CE2
		public void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x060017CC RID: 6092 RVA: 0x00043AF0 File Offset: 0x00041CF0
		public Stream StartStreamValueScope()
		{
			this.WriteValueSeparator();
			this.writer.Write('"');
			this.writer.Flush();
			this.binaryValueStream = new ODataBinaryStreamWriter(this.writer, ref this.buffer, this.ArrayPool);
			return this.binaryValueStream;
		}

		// Token: 0x060017CD RID: 6093 RVA: 0x00043B3E File Offset: 0x00041D3E
		public void EndStreamValueScope()
		{
			this.binaryValueStream.Flush();
			this.binaryValueStream.Dispose();
			this.binaryValueStream = null;
			this.writer.Flush();
			this.writer.Write('"');
		}

		// Token: 0x060017CE RID: 6094 RVA: 0x00043B78 File Offset: 0x00041D78
		public TextWriter StartTextWriterValueScope(string contentType)
		{
			this.WriteValueSeparator();
			this.currentContentType = contentType;
			if (!this.IsWritingJson)
			{
				this.writer.Write('"');
				this.writer.Flush();
				return new ODataJsonTextWriter(this.writer, ref this.buffer, this.ArrayPool);
			}
			this.writer.Flush();
			return this.writer;
		}

		// Token: 0x060017CF RID: 6095 RVA: 0x00043BDB File Offset: 0x00041DDB
		public void EndTextWriterValueScope()
		{
			if (!this.IsWritingJson)
			{
				this.writer.Write('"');
			}
		}

		// Token: 0x060017D0 RID: 6096 RVA: 0x00043BF4 File Offset: 0x00041DF4
		void IDisposable.Dispose()
		{
			if (this.binaryValueStream != null)
			{
				try
				{
					this.binaryValueStream.Dispose();
				}
				finally
				{
					this.binaryValueStream = null;
				}
			}
		}

		// Token: 0x060017D1 RID: 6097 RVA: 0x00043C30 File Offset: 0x00041E30
		public void Dispose()
		{
			if (this.ArrayPool != null && this.buffer != null)
			{
				BufferUtils.ReturnToBuffer(this.ArrayPool, this.buffer);
				this.ArrayPool = null;
				this.buffer = null;
			}
		}

		// Token: 0x060017D2 RID: 6098 RVA: 0x00043C64 File Offset: 0x00041E64
		private void WriteValueSeparator()
		{
			if (this.scopes.Count == 0)
			{
				return;
			}
			JsonWriter.Scope scope = this.scopes.Peek();
			if (scope.Type == JsonWriter.ScopeType.Array)
			{
				if (scope.ObjectCount != 0)
				{
					this.writer.Write(",");
				}
				JsonWriter.Scope scope2 = scope;
				int objectCount = scope2.ObjectCount;
				scope2.ObjectCount = objectCount + 1;
			}
		}

		// Token: 0x060017D3 RID: 6099 RVA: 0x00043CBC File Offset: 0x00041EBC
		private void StartScope(JsonWriter.ScopeType type)
		{
			if (this.scopes.Count != 0 && this.scopes.Peek().Type != JsonWriter.ScopeType.Padding)
			{
				JsonWriter.Scope scope = this.scopes.Peek();
				if (scope.Type == JsonWriter.ScopeType.Array && scope.ObjectCount != 0)
				{
					this.writer.Write(",");
				}
				JsonWriter.Scope scope2 = scope;
				int objectCount = scope2.ObjectCount;
				scope2.ObjectCount = objectCount + 1;
			}
			JsonWriter.Scope scope3 = new JsonWriter.Scope(type);
			this.scopes.Push(scope3);
			this.writer.Write(scope3.StartString);
			this.writer.IncreaseIndentation();
			this.writer.WriteLine();
		}

		// Token: 0x04000AA8 RID: 2728
		private readonly TextWriterWrapper writer;

		// Token: 0x04000AA9 RID: 2729
		private readonly Stack<JsonWriter.Scope> scopes;

		// Token: 0x04000AAA RID: 2730
		private readonly bool isIeee754Compatible;

		// Token: 0x04000AAB RID: 2731
		private readonly ODataStringEscapeOption stringEscapeOption;

		// Token: 0x04000AAC RID: 2732
		private char[] buffer;

		// Token: 0x04000AAD RID: 2733
		private Stream binaryValueStream;

		// Token: 0x04000AAE RID: 2734
		private string currentContentType;

		// Token: 0x020003DE RID: 990
		internal enum ScopeType
		{
			// Token: 0x04000F66 RID: 3942
			Array,
			// Token: 0x04000F67 RID: 3943
			Object,
			// Token: 0x04000F68 RID: 3944
			Padding
		}

		// Token: 0x020003DF RID: 991
		private sealed class Scope
		{
			// Token: 0x060020C0 RID: 8384 RVA: 0x0005C254 File Offset: 0x0005A454
			public Scope(JsonWriter.ScopeType type)
			{
				this.type = type;
				switch (type)
				{
				case JsonWriter.ScopeType.Array:
					this.StartString = "[";
					this.EndString = "]";
					return;
				case JsonWriter.ScopeType.Object:
					this.StartString = "{";
					this.EndString = "}";
					return;
				case JsonWriter.ScopeType.Padding:
					this.StartString = "(";
					this.EndString = ")";
					return;
				default:
					return;
				}
			}

			// Token: 0x17000646 RID: 1606
			// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0005C2C5 File Offset: 0x0005A4C5
			// (set) Token: 0x060020C2 RID: 8386 RVA: 0x0005C2CD File Offset: 0x0005A4CD
			public string StartString { get; private set; }

			// Token: 0x17000647 RID: 1607
			// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0005C2D6 File Offset: 0x0005A4D6
			// (set) Token: 0x060020C4 RID: 8388 RVA: 0x0005C2DE File Offset: 0x0005A4DE
			public string EndString { get; private set; }

			// Token: 0x17000648 RID: 1608
			// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0005C2E7 File Offset: 0x0005A4E7
			// (set) Token: 0x060020C6 RID: 8390 RVA: 0x0005C2EF File Offset: 0x0005A4EF
			public int ObjectCount { get; set; }

			// Token: 0x17000649 RID: 1609
			// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0005C2F8 File Offset: 0x0005A4F8
			public JsonWriter.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000F69 RID: 3945
			private readonly JsonWriter.ScopeType type;
		}
	}
}
