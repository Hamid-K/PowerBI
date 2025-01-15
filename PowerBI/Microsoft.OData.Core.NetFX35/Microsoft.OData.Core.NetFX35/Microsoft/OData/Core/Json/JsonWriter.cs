using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Microsoft.OData.Edm.Library;

namespace Microsoft.OData.Core.Json
{
	// Token: 0x02000118 RID: 280
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This class does not own the underlying stream/writer and thus should never dispose it.")]
	internal sealed class JsonWriter : IJsonWriter
	{
		// Token: 0x06000A8F RID: 2703 RVA: 0x0002697E File Offset: 0x00024B7E
		internal JsonWriter(TextWriter writer, bool indent, ODataFormat jsonFormat, bool isIeee754Compatible)
		{
			if (indent)
			{
				this.writer = new IndentedTextWriter(writer);
			}
			else
			{
				this.writer = new NonIndentedTextWriter(writer);
			}
			this.scopes = new Stack<JsonWriter.Scope>();
			this.isIeee754Compatible = isIeee754Compatible;
		}

		// Token: 0x06000A90 RID: 2704 RVA: 0x000269B6 File Offset: 0x00024BB6
		public void StartPaddingFunctionScope()
		{
			this.StartScope(JsonWriter.ScopeType.Padding);
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x000269C0 File Offset: 0x00024BC0
		public void EndPaddingFunctionScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00026A00 File Offset: 0x00024C00
		public void StartObjectScope()
		{
			this.StartScope(JsonWriter.ScopeType.Object);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x00026A0C File Offset: 0x00024C0C
		public void EndObjectScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00026A4C File Offset: 0x00024C4C
		public void StartArrayScope()
		{
			this.StartScope(JsonWriter.ScopeType.Array);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00026A58 File Offset: 0x00024C58
		public void EndArrayScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00026A98 File Offset: 0x00024C98
		public void WriteName(string name)
		{
			JsonWriter.Scope scope = this.scopes.Peek();
			if (scope.ObjectCount != 0)
			{
				this.writer.Write(",");
			}
			scope.ObjectCount++;
			JsonValueUtils.WriteEscapedJsonString(this.writer, name);
			this.writer.Write(":");
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00026AF3 File Offset: 0x00024CF3
		public void WritePaddingFunctionName(string functionName)
		{
			this.writer.Write(functionName);
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x00026B01 File Offset: 0x00024D01
		public void WriteValue(bool value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x00026B15 File Offset: 0x00024D15
		public void WriteValue(int value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00026B29 File Offset: 0x00024D29
		public void WriteValue(float value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x00026B3D File Offset: 0x00024D3D
		public void WriteValue(short value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00026B51 File Offset: 0x00024D51
		public void WriteValue(long value)
		{
			this.WriteValueSeparator();
			if (this.isIeee754Compatible)
			{
				JsonValueUtils.WriteValue(this.writer, value.ToString(CultureInfo.InvariantCulture));
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x00026B85 File Offset: 0x00024D85
		public void WriteValue(double value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00026B99 File Offset: 0x00024D99
		public void WriteValue(Guid value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00026BAD File Offset: 0x00024DAD
		public void WriteValue(decimal value)
		{
			this.WriteValueSeparator();
			if (this.isIeee754Compatible)
			{
				JsonValueUtils.WriteValue(this.writer, value.ToString(CultureInfo.InvariantCulture));
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00026BE1 File Offset: 0x00024DE1
		public void WriteValue(DateTimeOffset value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00026BF6 File Offset: 0x00024DF6
		public void WriteValue(TimeSpan value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x00026C0A File Offset: 0x00024E0A
		public void WriteValue(TimeOfDay value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x00026C1E File Offset: 0x00024E1E
		public void WriteValue(Date value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00026C32 File Offset: 0x00024E32
		public void WriteValue(byte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x00026C46 File Offset: 0x00024E46
		public void WriteValue(sbyte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00026C5A File Offset: 0x00024E5A
		public void WriteValue(string value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00026C6E File Offset: 0x00024E6E
		public void WriteValue(byte[] value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00026C82 File Offset: 0x00024E82
		public void WriteRawValue(string rawValue)
		{
			this.WriteValueSeparator();
			this.writer.Write(rawValue);
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00026C96 File Offset: 0x00024E96
		public void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00026CA4 File Offset: 0x00024EA4
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
				scope.ObjectCount++;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00026CFC File Offset: 0x00024EFC
		private void StartScope(JsonWriter.ScopeType type)
		{
			if (this.scopes.Count != 0 && this.scopes.Peek().Type != JsonWriter.ScopeType.Padding)
			{
				JsonWriter.Scope scope = this.scopes.Peek();
				if (scope.Type == JsonWriter.ScopeType.Array && scope.ObjectCount != 0)
				{
					this.writer.Write(",");
				}
				scope.ObjectCount++;
			}
			JsonWriter.Scope scope2 = new JsonWriter.Scope(type);
			this.scopes.Push(scope2);
			this.writer.Write(scope2.StartString);
			this.writer.IncreaseIndentation();
			this.writer.WriteLine();
		}

		// Token: 0x04000446 RID: 1094
		private readonly TextWriterWrapper writer;

		// Token: 0x04000447 RID: 1095
		private readonly Stack<JsonWriter.Scope> scopes;

		// Token: 0x04000448 RID: 1096
		private readonly bool isIeee754Compatible;

		// Token: 0x02000119 RID: 281
		internal enum ScopeType
		{
			// Token: 0x0400044A RID: 1098
			Array,
			// Token: 0x0400044B RID: 1099
			Object,
			// Token: 0x0400044C RID: 1100
			Padding
		}

		// Token: 0x0200011A RID: 282
		private sealed class Scope
		{
			// Token: 0x06000AAC RID: 2732 RVA: 0x00026DA0 File Offset: 0x00024FA0
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

			// Token: 0x17000234 RID: 564
			// (get) Token: 0x06000AAD RID: 2733 RVA: 0x00026E13 File Offset: 0x00025013
			// (set) Token: 0x06000AAE RID: 2734 RVA: 0x00026E1B File Offset: 0x0002501B
			public string StartString { get; private set; }

			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06000AAF RID: 2735 RVA: 0x00026E24 File Offset: 0x00025024
			// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x00026E2C File Offset: 0x0002502C
			public string EndString { get; private set; }

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06000AB1 RID: 2737 RVA: 0x00026E35 File Offset: 0x00025035
			// (set) Token: 0x06000AB2 RID: 2738 RVA: 0x00026E3D File Offset: 0x0002503D
			public int ObjectCount { get; set; }

			// Token: 0x17000237 RID: 567
			// (get) Token: 0x06000AB3 RID: 2739 RVA: 0x00026E46 File Offset: 0x00025046
			public JsonWriter.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x0400044D RID: 1101
			private readonly JsonWriter.ScopeType type;
		}
	}
}
