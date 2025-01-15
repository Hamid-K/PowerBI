using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000013 RID: 19
	internal sealed class JsonWriter : JsonWriterBase
	{
		// Token: 0x0600008C RID: 140 RVA: 0x0000383E File Offset: 0x00001A3E
		internal JsonWriter(TextWriter writer, bool indent)
		{
			DebugUtils.CheckNoExternalCallers();
			this.writer = new IndentedTextWriter(writer, indent);
			this.scopes = new Stack<Scope>();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003863 File Offset: 0x00001A63
		internal JsonWriter(TextWriter writer, JsonWriterSettings settings)
		{
			DebugUtils.CheckNoExternalCallers();
			this.settings = settings;
			this.writer = new IndentedTextWriter(writer, settings.EnableIndentation, settings.IndentationString);
			this.scopes = new Stack<Scope>();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000389A File Offset: 0x00001A9A
		internal override void StartObjectScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.StartScope(ScopeType.Object);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000038A8 File Offset: 0x00001AA8
		internal override void EndObjectScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.EndScope(ScopeType.Object);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000038B6 File Offset: 0x00001AB6
		internal override void StartArrayScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.StartScope(ScopeType.Array);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000038C4 File Offset: 0x00001AC4
		internal override void EndArrayScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.EndScope(ScopeType.Array);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000038D4 File Offset: 0x00001AD4
		private void EndScope(ScopeType type)
		{
			DebugUtils.CheckNoExternalCallers();
			if (this.settings != null && ((this.settings.NewLineOnArray && type == ScopeType.Array) || (this.settings.NewLineOnObject && type == ScopeType.Object)))
			{
				this.writer.WriteLine();
			}
			this.writer.DecreaseIndentation();
			this.scopes.Pop();
			if (type != ScopeType.Array)
			{
				if (type == ScopeType.Object)
				{
					this.writer.Write("}");
				}
			}
			else
			{
				this.writer.Write("]");
			}
			this.m_lastWriteWasPropertyName = false;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003964 File Offset: 0x00001B64
		internal void Close()
		{
			DebugUtils.CheckNoExternalCallers();
			while (this.scopes.Count > 0)
			{
				ScopeType type = this.scopes.Peek().Type;
				if (type != ScopeType.Array)
				{
					if (type == ScopeType.Object)
					{
						this.EndObjectScope();
					}
				}
				else
				{
					this.EndArrayScope();
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000039AC File Offset: 0x00001BAC
		internal void WriteDataWrapper()
		{
			DebugUtils.CheckNoExternalCallers();
			this.writer.Write("\"d\":");
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000039C3 File Offset: 0x00001BC3
		internal void WriteDataArrayName()
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteName("results");
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000039D8 File Offset: 0x00001BD8
		internal override void WriteName(string name)
		{
			DebugUtils.CheckNoExternalCallers();
			Scope scope = this.scopes.Peek();
			if (scope.ObjectCount != 0)
			{
				this.writer.Write(",");
			}
			if (this.settings != null && this.settings.NewLineOnProperty)
			{
				this.writer.WriteLine();
			}
			int objectCount = scope.ObjectCount;
			scope.ObjectCount = objectCount + 1;
			scope.PendingValueName = name;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00003A43 File Offset: 0x00001C43
		private void WriteValueSeparatorWithSettings()
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparator();
			if (this.settings != null && this.settings.NewLineOnArrayPrimitive && this.scopes.Peek().Type == ScopeType.Array)
			{
				this.writer.WriteLine();
			}
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00003A82 File Offset: 0x00001C82
		internal override void WriteValue(bool value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003A9B File Offset: 0x00001C9B
		internal override void WriteValue(int value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003AB4 File Offset: 0x00001CB4
		internal override void WriteValue(float value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003ACD File Offset: 0x00001CCD
		internal override void WriteValue(short value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003AE6 File Offset: 0x00001CE6
		internal override void WriteValue(long value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003AFF File Offset: 0x00001CFF
		internal override void WriteValue(double value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003B18 File Offset: 0x00001D18
		internal override void WriteValue(Guid value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003B31 File Offset: 0x00001D31
		internal override void WriteValue(decimal value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003B4A File Offset: 0x00001D4A
		internal override void WriteValue(DateTime value, ODataVersion odataVersion)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			if (odataVersion < ODataVersion.V3)
			{
				JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ODataDateTime);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003B76 File Offset: 0x00001D76
		internal override void WriteValue(DateTimeOffset value, ODataVersion odataVersion)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			if (odataVersion < ODataVersion.V3)
			{
				JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ODataDateTime);
				return;
			}
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003BA2 File Offset: 0x00001DA2
		internal override void WriteValue(TimeSpan value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003BBB File Offset: 0x00001DBB
		internal override void WriteValue(byte value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003BD4 File Offset: 0x00001DD4
		internal override void WriteValue(sbyte value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003BED File Offset: 0x00001DED
		internal override void WriteValue(string value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value, this.settings == null || this.settings.EscapeUnicode);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003C1C File Offset: 0x00001E1C
		internal override void WriteValue(byte[] value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00003C38 File Offset: 0x00001E38
		internal override void WriteRawValue(string value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			if (value == null)
			{
				this.writer.Write("null");
				return;
			}
			this.writer.Write('"');
			this.writer.Write(value);
			this.writer.Write('"');
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003C8A File Offset: 0x00001E8A
		internal void Flush()
		{
			DebugUtils.CheckNoExternalCallers();
			this.writer.Flush();
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C9C File Offset: 0x00001E9C
		internal void WriteValueSeparator()
		{
			if (this.scopes.Count == 0)
			{
				return;
			}
			Scope scope = this.scopes.Peek();
			if (scope.Type == ScopeType.Array)
			{
				if (scope.ObjectCount != 0)
				{
					this.writer.Write(",");
				}
				Scope scope2 = scope;
				int objectCount = scope2.ObjectCount;
				scope2.ObjectCount = objectCount + 1;
				return;
			}
			if (scope.Type == ScopeType.Object)
			{
				this.WritePendingValueName(scope);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003D04 File Offset: 0x00001F04
		private void WritePendingValueName(Scope scope)
		{
			if (scope.PendingValueName != null)
			{
				JsonValueUtils.WriteEscapedJsonString(this.writer, scope.PendingValueName, true);
				this.writer.Write(":");
				scope.ClearPendingValueName();
				this.m_lastWriteWasPropertyName = true;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003D40 File Offset: 0x00001F40
		private void StartScope(ScopeType type)
		{
			if (this.scopes.Count != 0)
			{
				Scope scope = this.scopes.Peek();
				if (scope.Type == ScopeType.Array && scope.ObjectCount != 0)
				{
					this.writer.Write(",");
				}
				else if (scope.Type == ScopeType.Object)
				{
					this.WritePendingValueName(scope);
				}
				Scope scope2 = scope;
				int objectCount = scope2.ObjectCount;
				scope2.ObjectCount = objectCount + 1;
			}
			Scope scope3 = new Scope(type);
			this.scopes.Push(scope3);
			if (this.settings != null && this.scopes.Count != 1 && ((type == ScopeType.Array && this.settings.NewLineOnArray) || (type == ScopeType.Object && this.settings.NewLineOnObject)) && (!this.settings.StartingBraceOnParentLine || !this.m_lastWriteWasPropertyName))
			{
				this.writer.WriteLine();
			}
			this.writer.Write((type == ScopeType.Array) ? "[" : "{");
			if (this.settings != null && ((type == ScopeType.Array && this.settings.NewLineOnArray && !this.settings.NewLineOnObject) || (type == ScopeType.Object && this.settings.NewLineOnObject && !this.settings.NewLineOnArray)))
			{
				this.writer.WriteLine();
			}
			this.writer.IncreaseIndentation();
			this.m_lastWriteWasPropertyName = false;
		}

		// Token: 0x0400007A RID: 122
		private readonly IndentedTextWriter writer;

		// Token: 0x0400007B RID: 123
		private readonly JsonWriterSettings settings;

		// Token: 0x0400007C RID: 124
		private bool m_lastWriteWasPropertyName;

		// Token: 0x0400007D RID: 125
		private readonly Stack<Scope> scopes;
	}
}
