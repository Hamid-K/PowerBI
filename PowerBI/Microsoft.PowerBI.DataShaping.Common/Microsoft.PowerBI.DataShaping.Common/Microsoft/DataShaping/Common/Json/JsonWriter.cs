using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.DataShaping.Common.Json
{
	// Token: 0x02000020 RID: 32
	internal sealed class JsonWriter
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000439C File Offset: 0x0000259C
		internal JsonWriter(TextWriter writer, bool indent)
		{
			this._writer = new IndentedTextWriter(writer, indent);
			this._scopes = new Stack<Scope>();
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000043BC File Offset: 0x000025BC
		internal JsonWriter(TextWriter writer, JsonWriterSettings settings)
		{
			this._settings = settings;
			this._writer = new IndentedTextWriter(writer, this._settings.EnableIndentation, this._settings.IndentationString);
			this._scopes = new Stack<Scope>();
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000043F8 File Offset: 0x000025F8
		internal void StartObjectScope()
		{
			this.StartScope(ScopeType.Object);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00004401 File Offset: 0x00002601
		internal void EndObjectScope()
		{
			this.EndScope(ScopeType.Object);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000440A File Offset: 0x0000260A
		internal void StartArrayScope()
		{
			this.StartScope(ScopeType.Array);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00004413 File Offset: 0x00002613
		internal void EndArrayScope()
		{
			this.EndScope(ScopeType.Array);
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000441C File Offset: 0x0000261C
		private void EndScope(ScopeType type)
		{
			if (this._settings != null && ((this._settings.NewLineOnArray && type == ScopeType.Array) || (this._settings.NewLineOnObject && type == ScopeType.Object)))
			{
				this._writer.WriteLine();
			}
			this._writer.DecreaseIndentation();
			this._scopes.Pop();
			if (type != ScopeType.Array)
			{
				if (type != ScopeType.Object)
				{
					Contract.RetailFail("Parameter was not in ScopeType enumerator");
				}
				else
				{
					this._writer.Write("}");
				}
			}
			else
			{
				this._writer.Write("]");
			}
			this._lastWriteWasPropertyName = false;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000044B4 File Offset: 0x000026B4
		internal void Close()
		{
			while (this._scopes.Count > 0)
			{
				ScopeType type = this._scopes.Peek().Type;
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

		// Token: 0x06000115 RID: 277 RVA: 0x000044F8 File Offset: 0x000026F8
		internal void WriteName(string name)
		{
			Scope scope = this._scopes.Peek();
			if (scope.ObjectCount != 0)
			{
				this._writer.Write(",");
			}
			if (this._settings != null && this._settings.NewLineOnProperty)
			{
				this._writer.WriteLine();
			}
			int objectCount = scope.ObjectCount;
			scope.ObjectCount = objectCount + 1;
			scope.PendingValueName = name;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000455E File Offset: 0x0000275E
		private void WriteValueSeparatorWithSettings()
		{
			this.WriteValueSeparator();
			if (this._settings != null && this._settings.NewLineOnArrayPrimitive && this._scopes.Peek().Type == ScopeType.Array)
			{
				this._writer.WriteLine();
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004598 File Offset: 0x00002798
		internal void WriteValue(bool value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteBoolean(this._writer, value);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000045AC File Offset: 0x000027AC
		internal void WriteValue(string value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteString(this._writer, value);
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000045C0 File Offset: 0x000027C0
		internal void WriteJsonEncodedValue(string value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteJsonEncodedString(this._writer, value, false);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000045D5 File Offset: 0x000027D5
		internal void WriteJsonEncodedString(string value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteJsonEncodedString(this._writer, value, true);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000045EA File Offset: 0x000027EA
		internal void WriteValue(double value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteDouble(this._writer, value);
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000045FE File Offset: 0x000027FE
		internal void WriteValue(decimal value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteDecimal(this._writer, value);
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004612 File Offset: 0x00002812
		internal void WriteValue(int value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteInt(this._writer, value);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004626 File Offset: 0x00002826
		internal void WriteValue(long value)
		{
			this.WriteValueSeparatorWithSettings();
			JsonValueUtils.WriteLong(this._writer, value);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000463A File Offset: 0x0000283A
		internal void WriteValue(DateTimeOffset value)
		{
			this.WriteValueSeparatorWithSettings();
			this._writer.Write('"');
			JsonValueUtils.WriteDateTimeOffset(this._writer, value);
			this._writer.Write('"');
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004668 File Offset: 0x00002868
		internal void WriteTypeEncodedValue(object value)
		{
			this.WriteValue(value, true);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004672 File Offset: 0x00002872
		internal void WriteSimpleEncodedValue(object value)
		{
			this.WriteValue(value, false);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000467C File Offset: 0x0000287C
		private void WriteValue(object value, bool isTypeEncoded)
		{
			this.WriteValueSeparatorWithSettings();
			if (this.TryHandleSpecialType(value, isTypeEncoded))
			{
				return;
			}
			string text = (isTypeEncoded ? JsonValueUtils.ToTypeEncodedString(value) : JsonValueUtils.ToSimpleEncodedString(value));
			if (value is string)
			{
				JsonValueUtils.WriteEscapedJsonString(this._writer, text);
				return;
			}
			this.WriteQuoted(text);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x000046C8 File Offset: 0x000028C8
		internal bool TryHandleSpecialType(object value, bool isTypeEncoded)
		{
			if (value == null)
			{
				this._writer.Write("null");
				return true;
			}
			TypeCode typeCode = Type.GetTypeCode(value.GetType());
			if (typeCode == TypeCode.Boolean)
			{
				JsonValueUtils.WriteBoolean(this._writer, (bool)value);
				return true;
			}
			if (typeCode == TypeCode.Int32)
			{
				JsonValueUtils.WriteInt(this._writer, (int)value);
				return true;
			}
			if (typeCode == TypeCode.Object)
			{
				IEnumerable enumerable = value as IEnumerable;
				if (enumerable != null)
				{
					if (enumerable is byte[])
					{
						return false;
					}
					if (isTypeEncoded)
					{
						this.WriteTypeEncodedArray(enumerable);
					}
					else
					{
						this.WriteSimpleEncodedArray(enumerable);
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00004753 File Offset: 0x00002953
		internal void WriteTypeEncodedArray(IEnumerable items)
		{
			this.WriteArray(items, true);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000475D File Offset: 0x0000295D
		internal void WriteSimpleEncodedArray(IEnumerable items)
		{
			this.WriteArray(items, false);
		}

		// Token: 0x06000126 RID: 294 RVA: 0x00004768 File Offset: 0x00002968
		private void WriteArray(IEnumerable items, bool isTypeEncoded)
		{
			if (items == null)
			{
				return;
			}
			this.StartArrayScope();
			foreach (object obj in items)
			{
				if (isTypeEncoded)
				{
					this.WriteTypeEncodedValue(obj);
				}
				else
				{
					this.WriteSimpleEncodedValue(obj);
				}
			}
			this.EndArrayScope();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000047D4 File Offset: 0x000029D4
		private void WriteQuoted(string text)
		{
			this._writer.Write('"');
			this._writer.Write(text);
			this._writer.Write('"');
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000047FC File Offset: 0x000029FC
		internal void WriteRawValue(string value)
		{
			this.WriteValueSeparatorWithSettings();
			if (value == null)
			{
				this._writer.Write("null");
				return;
			}
			this.WriteQuoted(value);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000481F File Offset: 0x00002A1F
		internal void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000482C File Offset: 0x00002A2C
		internal void WriteValueSeparator()
		{
			if (this._scopes.Count == 0)
			{
				return;
			}
			Scope scope = this._scopes.Peek();
			if (scope.Type == ScopeType.Array)
			{
				if (scope.ObjectCount != 0)
				{
					this._writer.Write(",");
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

		// Token: 0x0600012B RID: 299 RVA: 0x00004894 File Offset: 0x00002A94
		private void WritePendingValueName(Scope scope)
		{
			if (scope.PendingValueName != null)
			{
				JsonValueUtils.WriteEscapedJsonString(this._writer, scope.PendingValueName);
				this._writer.Write(":");
				scope.ClearPendingValueName();
				this._lastWriteWasPropertyName = true;
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000048CC File Offset: 0x00002ACC
		private void StartScope(ScopeType type)
		{
			if (this._scopes.Count != 0)
			{
				Scope scope = this._scopes.Peek();
				if (scope.Type == ScopeType.Array && scope.ObjectCount != 0)
				{
					this._writer.Write(",");
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
			this._scopes.Push(scope3);
			if (this._settings != null && this._scopes.Count != 1 && ((type == ScopeType.Array && this._settings.NewLineOnArray) || (type == ScopeType.Object && this._settings.NewLineOnObject)) && (!this._settings.StartingBraceOnParentLine || !this._lastWriteWasPropertyName))
			{
				this._writer.WriteLine();
			}
			this._writer.Write((type == ScopeType.Array) ? "[" : "{");
			if (this._settings != null && ((type == ScopeType.Array && this._settings.NewLineOnArray && !this._settings.NewLineOnObject) || (type == ScopeType.Object && this._settings.NewLineOnObject && !this._settings.NewLineOnArray)))
			{
				this._writer.WriteLine();
			}
			this._writer.IncreaseIndentation();
			this._lastWriteWasPropertyName = false;
		}

		// Token: 0x04000058 RID: 88
		private readonly IndentedTextWriter _writer;

		// Token: 0x04000059 RID: 89
		private readonly Stack<Scope> _scopes;

		// Token: 0x0400005A RID: 90
		private readonly JsonWriterSettings _settings;

		// Token: 0x0400005B RID: 91
		private bool _lastWriteWasPropertyName;
	}
}
