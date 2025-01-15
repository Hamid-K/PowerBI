using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.ReportingServices.OData.Json
{
	// Token: 0x02000016 RID: 22
	internal sealed class JsonWriter : JsonWriterBase
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003786 File Offset: 0x00001986
		internal JsonWriter(TextWriter writer, bool indent)
		{
			DebugUtils.CheckNoExternalCallers();
			this.writer = new IndentedTextWriter(writer, indent);
			this.scopes = new Stack<Scope>();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000037AB File Offset: 0x000019AB
		internal JsonWriter(TextWriter writer, JsonWriterSettings settings)
		{
			DebugUtils.CheckNoExternalCallers();
			this.settings = settings;
			this.writer = new IndentedTextWriter(writer, settings.EnableIndentation, settings.IndentationString);
			this.scopes = new Stack<Scope>();
		}

		// Token: 0x0600008D RID: 141 RVA: 0x000037E2 File Offset: 0x000019E2
		internal override void StartObjectScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.StartScope(ScopeType.Object);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000037F0 File Offset: 0x000019F0
		internal override void EndObjectScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.EndScope(ScopeType.Object);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x000037FE File Offset: 0x000019FE
		internal override void StartArrayScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.StartScope(ScopeType.Array);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000380C File Offset: 0x00001A0C
		internal override void EndArrayScope()
		{
			DebugUtils.CheckNoExternalCallers();
			this.EndScope(ScopeType.Array);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000381C File Offset: 0x00001A1C
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

		// Token: 0x06000092 RID: 146 RVA: 0x000038AC File Offset: 0x00001AAC
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

		// Token: 0x06000093 RID: 147 RVA: 0x000038F4 File Offset: 0x00001AF4
		internal void WriteDataWrapper()
		{
			DebugUtils.CheckNoExternalCallers();
			this.writer.Write("\"d\":");
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000390B File Offset: 0x00001B0B
		internal void WriteDataArrayName()
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteName("results");
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003920 File Offset: 0x00001B20
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

		// Token: 0x06000096 RID: 150 RVA: 0x0000398B File Offset: 0x00001B8B
		private void WriteValueSeparatorWithSettings()
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparator();
			if (this.settings != null && this.settings.NewLineOnArrayPrimitive && this.scopes.Peek().Type == ScopeType.Array)
			{
				this.writer.WriteLine();
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000039CA File Offset: 0x00001BCA
		internal override void WriteValue(bool value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000039E3 File Offset: 0x00001BE3
		internal override void WriteValue(int value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000039FC File Offset: 0x00001BFC
		internal override void WriteValue(float value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003A15 File Offset: 0x00001C15
		internal override void WriteValue(short value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003A2E File Offset: 0x00001C2E
		internal override void WriteValue(long value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003A47 File Offset: 0x00001C47
		internal override void WriteValue(double value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00003A60 File Offset: 0x00001C60
		internal override void WriteValue(Guid value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003A79 File Offset: 0x00001C79
		internal override void WriteValue(decimal value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003A92 File Offset: 0x00001C92
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

		// Token: 0x060000A0 RID: 160 RVA: 0x00003ABE File Offset: 0x00001CBE
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

		// Token: 0x060000A1 RID: 161 RVA: 0x00003AEA File Offset: 0x00001CEA
		internal override void WriteValue(TimeSpan value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003B03 File Offset: 0x00001D03
		internal override void WriteValue(byte value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003B1C File Offset: 0x00001D1C
		internal override void WriteValue(sbyte value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003B35 File Offset: 0x00001D35
		internal override void WriteValue(string value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003B4E File Offset: 0x00001D4E
		internal override void WriteValue(byte[] value)
		{
			DebugUtils.CheckNoExternalCallers();
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00003B68 File Offset: 0x00001D68
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

		// Token: 0x060000A7 RID: 167 RVA: 0x00003BBA File Offset: 0x00001DBA
		internal void Flush()
		{
			DebugUtils.CheckNoExternalCallers();
			this.writer.Flush();
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00003BCC File Offset: 0x00001DCC
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

		// Token: 0x060000A9 RID: 169 RVA: 0x00003C34 File Offset: 0x00001E34
		private void WritePendingValueName(Scope scope)
		{
			if (scope.PendingValueName != null)
			{
				JsonValueUtils.WriteEscapedJsonString(this.writer, scope.PendingValueName);
				this.writer.Write(":");
				scope.ClearPendingValueName();
				this.m_lastWriteWasPropertyName = true;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003C6C File Offset: 0x00001E6C
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

		// Token: 0x0400009E RID: 158
		private readonly IndentedTextWriter writer;

		// Token: 0x0400009F RID: 159
		private readonly JsonWriterSettings settings;

		// Token: 0x040000A0 RID: 160
		private bool m_lastWriteWasPropertyName;

		// Token: 0x040000A1 RID: 161
		private readonly Stack<Scope> scopes;
	}
}
