using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.AnalysisServices.Tabular.Extensions;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices.Tabular.Json
{
	// Token: 0x020001BA RID: 442
	internal sealed class JsonSchemaBuilder : Disposable
	{
		// Token: 0x06001B30 RID: 6960 RVA: 0x000B89F6 File Offset: 0x000B6BF6
		public JsonSchemaBuilder(JsonWriter writer, bool keepWriterOpen = false)
		{
			this.writer = writer;
			this.keepWriterOpen = keepWriterOpen;
			this.scopes = new Stack<JsonSchemaScopeType>();
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000B8A17 File Offset: 0x000B6C17
		public void WriteMetadataEnum<TEnum>(string description, IEnumerable<TEnum> values)
		{
			this.WriteMetadataEnum(typeof(TEnum), description, (IEnumerable<object>)values);
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000B8A30 File Offset: 0x000B6C30
		public void WriteMetadataEnum(Type enumType, string description, IEnumerable<object> values)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteMetadataEnumStartImpl(enumType, description);
			this.WriteMetadataEnumValuesImpl(enumType, values);
			this.WriteMetadataEnumEndImpl();
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000B8A4E File Offset: 0x000B6C4E
		public void WriteBooleanProperty(string propertyName)
		{
			base.ThrowIfAlreadyDisposed();
			this.WritePropertyImpl(propertyName, "boolean", null);
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000B8A63 File Offset: 0x000B6C63
		public void WriteIntegerProperty(string propertyName)
		{
			base.ThrowIfAlreadyDisposed();
			this.WritePropertyImpl(propertyName, "integer", null);
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000B8A78 File Offset: 0x000B6C78
		public void WriteFloatProperty(string propertyName)
		{
			base.ThrowIfAlreadyDisposed();
			this.WritePropertyImpl(propertyName, "number", null);
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000B8A8D File Offset: 0x000B6C8D
		public void WriteStringProperty(string propertyName, string propertyFormat = null)
		{
			base.ThrowIfAlreadyDisposed();
			this.WritePropertyImpl(propertyName, "string", propertyFormat);
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000B8AA2 File Offset: 0x000B6CA2
		public void WriteMetadataElementProperty(string propertyName, string elementName)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(propertyName);
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			this.WriteMetadataElementReferenceImpl(elementName);
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000B8ACC File Offset: 0x000B6CCC
		public void WriteMetadataElementArrayProperty(string propertyName, string elementName)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(propertyName);
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			this.WriteRawEntryImpl("type", "array");
			this.WriteRawNameImpl("items");
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			this.WriteMetadataElementReferenceImpl(elementName);
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000B8B2E File Offset: 0x000B6D2E
		public void WriteMetadataEnumStart(Type enumType, string description)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteMetadataEnumStartImpl(enumType, description);
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000B8B3E File Offset: 0x000B6D3E
		public void WriteMetadataEnumValues(Type enumType, IEnumerable<object> values)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteMetadataEnumValuesImpl(enumType, values);
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000B8B4E File Offset: 0x000B6D4E
		public void WriteMetadataEnumEnd()
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteMetadataEnumEndImpl();
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000B8B5C File Offset: 0x000B6D5C
		public void WriteMetadataObjectStart(string objectName, string description)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteElementStartImpl(objectName, "object", description);
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000B8B74 File Offset: 0x000B6D74
		public void WriteMetadataObjectEnd(IEnumerable<string> requiredProperties, bool additionalProperties)
		{
			base.ThrowIfAlreadyDisposed();
			if (requiredProperties != null)
			{
				this.WriteRawNameImpl("required");
				this.OpenScopeImpl(JsonSchemaScopeType.Array);
				foreach (string text in requiredProperties)
				{
					this.WriteRawValueImpl(text);
				}
				this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Array));
			}
			this.WriteElementEndImpl(new bool?(additionalProperties));
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000B8BF0 File Offset: 0x000B6DF0
		public void WriteMetadataObjectArrayStart(string objectName, string description)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(objectName);
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			if (!string.IsNullOrEmpty(description))
			{
				this.WriteRawEntryImpl("description", description);
			}
			this.WriteRawEntryImpl("type", "array");
			this.WriteRawNameImpl("items");
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			this.WriteRawEntryImpl("type", "object");
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000B8C58 File Offset: 0x000B6E58
		public void WriteMetadataObjectArrayEnd(IEnumerable<string> requiredProperties, bool additionalProperties)
		{
			base.ThrowIfAlreadyDisposed();
			if (requiredProperties != null)
			{
				this.WriteRawNameImpl("required");
				this.OpenScopeImpl(JsonSchemaScopeType.Array);
				foreach (string text in requiredProperties)
				{
					this.WriteRawValueImpl(text);
				}
				this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Array));
			}
			this.WriteRawEntryImpl("additionalProperties", additionalProperties);
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000B8CEC File Offset: 0x000B6EEC
		public void WriteElementStart(string elementName, string elementType, string description)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteElementStartImpl(elementName, elementType, description);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000B8CFD File Offset: 0x000B6EFD
		public void WriteElementEnd(bool? additionalProperties)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteElementEndImpl(additionalProperties);
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000B8D0C File Offset: 0x000B6F0C
		public void WritePropertyStart(string propertyName, string propertyType)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(propertyName);
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			this.WriteRawEntryImpl("type", propertyType);
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000B8D2E File Offset: 0x000B6F2E
		public void WritePropertyEnd()
		{
			base.ThrowIfAlreadyDisposed();
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000B8D42 File Offset: 0x000B6F42
		public void WriteExtensionStart(string extension)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(extension);
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000B8D58 File Offset: 0x000B6F58
		public void WriteExtensionEnd()
		{
			base.ThrowIfAlreadyDisposed();
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000B8D6C File Offset: 0x000B6F6C
		public void WriteMetadataElementReference(string elementName)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteMetadataElementReferenceImpl(elementName);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000B8D7C File Offset: 0x000B6F7C
		public void WriteRawArray(string array, IEnumerable<object> values, bool oneValuePerLine = false)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(array);
			this.OpenScopeImpl(JsonSchemaScopeType.Array);
			if (values != null)
			{
				if (oneValuePerLine)
				{
					this.writer.Formatting = 1;
				}
				foreach (object obj in values)
				{
					this.writer.WriteValue(obj);
				}
			}
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Array));
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000B8DFC File Offset: 0x000B6FFC
		public void WriteRawEntry(string name, string value)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawEntryImpl(name, value);
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000B8E0C File Offset: 0x000B700C
		public void WriteRawEntry(string name, int value)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawEntryImpl(name, value);
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000B8E1C File Offset: 0x000B701C
		public void WriteRawEntry(string name, bool value)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawEntryImpl(name, value);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000B8E2C File Offset: 0x000B702C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteRawName(string name)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(name);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000B8E3B File Offset: 0x000B703B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteRawValue(string value)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawValueImpl(value);
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000B8E4A File Offset: 0x000B704A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteRawValue(int value)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawValueImpl(value);
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000B8E59 File Offset: 0x000B7059
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void WriteRawValue(bool value)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawValueImpl(value);
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000B8E68 File Offset: 0x000B7068
		public void OpenScope(JsonSchemaScopeType scope)
		{
			base.ThrowIfAlreadyDisposed();
			this.OpenScopeImpl(scope);
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000B8E78 File Offset: 0x000B7078
		public void CloseScope()
		{
			base.ThrowIfAlreadyDisposed();
			this.CloseScopeImpl(null);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000B8E9C File Offset: 0x000B709C
		public void WriteSeperationLine()
		{
			base.ThrowIfAlreadyDisposed();
			JsonTextWriter jsonTextWriter = this.writer as JsonTextWriter;
			if (jsonTextWriter != null)
			{
				jsonTextWriter.RequestSeparationLine(1);
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000B8EC8 File Offset: 0x000B70C8
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && !this.keepWriterOpen)
				{
					this.writer.Close();
				}
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000B8F08 File Offset: 0x000B7108
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteMetadataEnumStartImpl(Type enumType, string description)
		{
			this.WriteElementStartImpl(enumType.Name.ToJsonCase(), null, description);
			this.WriteRawNameImpl("enum");
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000B8F28 File Offset: 0x000B7128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteMetadataEnumValuesImpl(Type enumType, IEnumerable<object> values)
		{
			this.OpenScopeImpl(JsonSchemaScopeType.Array);
			if (values == null)
			{
				using (IEnumerator enumerator = Enum.GetValues(enumType).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						this.WriteRawValueImpl(enumType.GetEnumName(obj).ToJsonCase());
					}
					goto IL_0084;
				}
			}
			foreach (object obj2 in values)
			{
				this.WriteRawValueImpl(enumType.GetEnumName(obj2).ToJsonCase());
			}
			IL_0084:
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Array));
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000B8FE4 File Offset: 0x000B71E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteMetadataEnumEndImpl()
		{
			this.WriteElementEndImpl(null);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000B9000 File Offset: 0x000B7200
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteElementStartImpl(string elementName, string elementType, string description)
		{
			this.WriteRawNameImpl(elementName);
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			if (!string.IsNullOrEmpty(description))
			{
				this.WriteRawEntryImpl("description", description);
			}
			if (!string.IsNullOrEmpty(elementType))
			{
				this.WriteRawEntryImpl("type", elementType);
			}
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000B9038 File Offset: 0x000B7238
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteElementEndImpl(bool? additionalProperties)
		{
			if (additionalProperties != null)
			{
				this.WriteRawEntryImpl("additionalProperties", additionalProperties.Value);
			}
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000B9061 File Offset: 0x000B7261
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteMetadataElementReferenceImpl(string elementName)
		{
			this.WriteRawEntryImpl("$ref", string.Format("#/{0}/{1}", "$defs", elementName));
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000B9080 File Offset: 0x000B7280
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WritePropertyImpl(string propertyName, string propertyType, string propertyFormat = null)
		{
			base.ThrowIfAlreadyDisposed();
			this.WriteRawNameImpl(propertyName);
			this.OpenScopeImpl(JsonSchemaScopeType.Object);
			this.WriteRawEntryImpl("type", propertyType);
			if (!string.IsNullOrEmpty(propertyFormat))
			{
				this.WriteRawEntryImpl("format", propertyFormat);
			}
			this.CloseScopeImpl(new JsonSchemaScopeType?(JsonSchemaScopeType.Object));
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000B90CD File Offset: 0x000B72CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteRawEntryImpl(string name, string value)
		{
			this.WriteRawNameImpl(name);
			this.WriteRawValueImpl(value);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000B90DD File Offset: 0x000B72DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteRawEntryImpl(string name, int value)
		{
			this.WriteRawNameImpl(name);
			this.WriteRawValueImpl(value);
		}

		// Token: 0x06001B5C RID: 7004 RVA: 0x000B90ED File Offset: 0x000B72ED
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteRawEntryImpl(string name, bool value)
		{
			this.WriteRawNameImpl(name);
			this.WriteRawValueImpl(value);
		}

		// Token: 0x06001B5D RID: 7005 RVA: 0x000B90FD File Offset: 0x000B72FD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteRawNameImpl(string name)
		{
			this.writer.WritePropertyName(name);
		}

		// Token: 0x06001B5E RID: 7006 RVA: 0x000B910B File Offset: 0x000B730B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteRawValueImpl(string value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000B9119 File Offset: 0x000B7319
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteRawValueImpl(int value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000B9127 File Offset: 0x000B7327
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void WriteRawValueImpl(bool value)
		{
			this.writer.WriteValue(value);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x000B9138 File Offset: 0x000B7338
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void OpenScopeImpl(JsonSchemaScopeType scope)
		{
			switch (scope)
			{
			case JsonSchemaScopeType.Global:
				this.writer.WriteStartObject();
				break;
			case JsonSchemaScopeType.Definitions:
				this.writer.WritePropertyName("$defs");
				this.writer.WriteStartObject();
				break;
			case JsonSchemaScopeType.Choice:
				this.writer.WritePropertyName("anyOf");
				this.writer.WriteStartArray();
				break;
			case JsonSchemaScopeType.Object:
				this.writer.WriteStartObject();
				break;
			case JsonSchemaScopeType.Array:
				this.writer.WriteStartArray();
				break;
			default:
				throw new TomInternalException("Invalid JsonSchemaScopeType");
			}
			this.scopes.Push(scope);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x000B91DC File Offset: 0x000B73DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void CloseScopeImpl(JsonSchemaScopeType? currentScope)
		{
			JsonSchemaScopeType jsonSchemaScopeType = this.scopes.Pop();
			if (currentScope != null)
			{
				JsonSchemaScopeType? jsonSchemaScopeType2 = currentScope;
				JsonSchemaScopeType jsonSchemaScopeType3 = jsonSchemaScopeType;
				if (!((jsonSchemaScopeType2.GetValueOrDefault() == jsonSchemaScopeType3) & (jsonSchemaScopeType2 != null)))
				{
					throw TomInternalException.Create("Expected scope: {0} doesn't match current scope: {1}", new object[] { currentScope, jsonSchemaScopeType });
				}
			}
			switch (jsonSchemaScopeType)
			{
			case JsonSchemaScopeType.Global:
				this.writer.WriteEndObject();
				return;
			case JsonSchemaScopeType.Definitions:
				this.writer.WriteEndObject();
				return;
			case JsonSchemaScopeType.Choice:
				this.writer.WriteEndArray();
				return;
			case JsonSchemaScopeType.Object:
				this.writer.WriteEndObject();
				return;
			case JsonSchemaScopeType.Array:
				this.writer.WriteEndArray();
				return;
			default:
				throw new TomInternalException("Invalid JsonSchemaScopeType");
			}
		}

		// Token: 0x04000534 RID: 1332
		private readonly JsonWriter writer;

		// Token: 0x04000535 RID: 1333
		private readonly bool keepWriterOpen;

		// Token: 0x04000536 RID: 1334
		private Stack<JsonSchemaScopeType> scopes;
	}
}
