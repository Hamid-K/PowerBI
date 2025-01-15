using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Data.OData.Json
{
	// Token: 0x020002A1 RID: 673
	internal sealed class JsonWriter : IJsonWriter
	{
		// Token: 0x06001565 RID: 5477 RVA: 0x0004E276 File Offset: 0x0004C476
		internal JsonWriter(TextWriter writer, bool indent, ODataFormat jsonFormat)
		{
			this.writer = new IndentedTextWriter(writer, indent);
			this.scopes = new Stack<JsonWriter.Scope>();
			this.mustWriteDecimalPointInDoubleValues = jsonFormat == ODataFormat.Json;
		}

		// Token: 0x06001566 RID: 5478 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
		public void StartPaddingFunctionScope()
		{
			this.StartScope(JsonWriter.ScopeType.Padding);
		}

		// Token: 0x06001567 RID: 5479 RVA: 0x0004E2B0 File Offset: 0x0004C4B0
		public void EndPaddingFunctionScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x06001568 RID: 5480 RVA: 0x0004E2F0 File Offset: 0x0004C4F0
		public void StartObjectScope()
		{
			this.StartScope(JsonWriter.ScopeType.Object);
		}

		// Token: 0x06001569 RID: 5481 RVA: 0x0004E2FC File Offset: 0x0004C4FC
		public void EndObjectScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x0600156A RID: 5482 RVA: 0x0004E33C File Offset: 0x0004C53C
		public void StartArrayScope()
		{
			this.StartScope(JsonWriter.ScopeType.Array);
		}

		// Token: 0x0600156B RID: 5483 RVA: 0x0004E348 File Offset: 0x0004C548
		public void EndArrayScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x0004E388 File Offset: 0x0004C588
		public void WriteDataWrapper()
		{
			this.writer.Write("\"d\":");
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x0004E39A File Offset: 0x0004C59A
		public void WriteDataArrayName()
		{
			this.WriteName("results");
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x0004E3A8 File Offset: 0x0004C5A8
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

		// Token: 0x0600156F RID: 5487 RVA: 0x0004E403 File Offset: 0x0004C603
		public void WritePaddingFunctionName(string functionName)
		{
			this.writer.Write(functionName);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0004E411 File Offset: 0x0004C611
		public void WriteValue(bool value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x0004E425 File Offset: 0x0004C625
		public void WriteValue(int value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x0004E439 File Offset: 0x0004C639
		public void WriteValue(float value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x0004E44D File Offset: 0x0004C64D
		public void WriteValue(short value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x0004E461 File Offset: 0x0004C661
		public void WriteValue(long value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x0004E475 File Offset: 0x0004C675
		public void WriteValue(double value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value, this.mustWriteDecimalPointInDoubleValues);
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x0004E48F File Offset: 0x0004C68F
		public void WriteValue(Guid value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x0004E4A3 File Offset: 0x0004C6A3
		public void WriteValue(decimal value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x0004E4B7 File Offset: 0x0004C6B7
		public void WriteValue(DateTime value, ODataVersion odataVersion)
		{
			this.WriteValueSeparator();
			if (odataVersion < ODataVersion.V3)
			{
				JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ODataDateTime);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x0004E4DE File Offset: 0x0004C6DE
		public void WriteValue(DateTimeOffset value, ODataVersion odataVersion)
		{
			this.WriteValueSeparator();
			if (odataVersion < ODataVersion.V3)
			{
				JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ODataDateTime);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x0004E505 File Offset: 0x0004C705
		public void WriteValue(TimeSpan value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x0004E519 File Offset: 0x0004C719
		public void WriteValue(byte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x0004E52D File Offset: 0x0004C72D
		public void WriteValue(sbyte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x0004E541 File Offset: 0x0004C741
		public void WriteValue(string value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0004E555 File Offset: 0x0004C755
		public void WriteRawString(string value)
		{
			this.writer.Write(value);
		}

		// Token: 0x0600157F RID: 5503 RVA: 0x0004E563 File Offset: 0x0004C763
		public void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06001580 RID: 5504 RVA: 0x0004E570 File Offset: 0x0004C770
		public void WriteValueSeparator()
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

		// Token: 0x06001581 RID: 5505 RVA: 0x0004E5C8 File Offset: 0x0004C7C8
		public void StartScope(JsonWriter.ScopeType type)
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

		// Token: 0x04000960 RID: 2400
		private readonly IndentedTextWriter writer;

		// Token: 0x04000961 RID: 2401
		private readonly Stack<JsonWriter.Scope> scopes;

		// Token: 0x04000962 RID: 2402
		private readonly bool mustWriteDecimalPointInDoubleValues;

		// Token: 0x020002A2 RID: 674
		internal enum ScopeType
		{
			// Token: 0x04000964 RID: 2404
			Array,
			// Token: 0x04000965 RID: 2405
			Object,
			// Token: 0x04000966 RID: 2406
			Padding
		}

		// Token: 0x020002A3 RID: 675
		private sealed class Scope
		{
			// Token: 0x06001582 RID: 5506 RVA: 0x0004E66C File Offset: 0x0004C86C
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

			// Token: 0x1700048A RID: 1162
			// (get) Token: 0x06001583 RID: 5507 RVA: 0x0004E6DF File Offset: 0x0004C8DF
			// (set) Token: 0x06001584 RID: 5508 RVA: 0x0004E6E7 File Offset: 0x0004C8E7
			public string StartString { get; private set; }

			// Token: 0x1700048B RID: 1163
			// (get) Token: 0x06001585 RID: 5509 RVA: 0x0004E6F0 File Offset: 0x0004C8F0
			// (set) Token: 0x06001586 RID: 5510 RVA: 0x0004E6F8 File Offset: 0x0004C8F8
			public string EndString { get; private set; }

			// Token: 0x1700048C RID: 1164
			// (get) Token: 0x06001587 RID: 5511 RVA: 0x0004E701 File Offset: 0x0004C901
			// (set) Token: 0x06001588 RID: 5512 RVA: 0x0004E709 File Offset: 0x0004C909
			public int ObjectCount { get; set; }

			// Token: 0x1700048D RID: 1165
			// (get) Token: 0x06001589 RID: 5513 RVA: 0x0004E712 File Offset: 0x0004C912
			public JsonWriter.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000967 RID: 2407
			private readonly JsonWriter.ScopeType type;
		}
	}
}
