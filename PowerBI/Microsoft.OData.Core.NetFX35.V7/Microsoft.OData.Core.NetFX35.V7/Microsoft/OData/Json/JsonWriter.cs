using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Json
{
	// Token: 0x020001E9 RID: 489
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable", Justification = "This class does not own the underlying stream/writer and thus should never dispose it.")]
	internal sealed class JsonWriter : IJsonWriter
	{
		// Token: 0x06001335 RID: 4917 RVA: 0x000376A8 File Offset: 0x000358A8
		internal JsonWriter(TextWriter writer, bool isIeee754Compatible)
		{
			this.writer = new NonIndentedTextWriter(writer);
			this.scopes = new Stack<JsonWriter.Scope>();
			this.isIeee754Compatible = isIeee754Compatible;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x000376CE File Offset: 0x000358CE
		public void StartPaddingFunctionScope()
		{
			this.StartScope(JsonWriter.ScopeType.Padding);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x000376D8 File Offset: 0x000358D8
		public void EndPaddingFunctionScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x00037718 File Offset: 0x00035918
		public void StartObjectScope()
		{
			this.StartScope(JsonWriter.ScopeType.Object);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x00037724 File Offset: 0x00035924
		public void EndObjectScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x00037764 File Offset: 0x00035964
		public void StartArrayScope()
		{
			this.StartScope(JsonWriter.ScopeType.Array);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x00037770 File Offset: 0x00035970
		public void EndArrayScope()
		{
			this.writer.WriteLine();
			this.writer.DecreaseIndentation();
			JsonWriter.Scope scope = this.scopes.Pop();
			this.writer.Write(scope.EndString);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x000377B0 File Offset: 0x000359B0
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
			JsonValueUtils.WriteEscapedJsonString(this.writer, name);
			this.writer.Write(":");
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0003780D File Offset: 0x00035A0D
		public void WritePaddingFunctionName(string functionName)
		{
			this.writer.Write(functionName);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x0003781B File Offset: 0x00035A1B
		public void WriteValue(bool value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0003782F File Offset: 0x00035A2F
		public void WriteValue(int value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x00037843 File Offset: 0x00035A43
		public void WriteValue(float value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x00037857 File Offset: 0x00035A57
		public void WriteValue(short value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0003786B File Offset: 0x00035A6B
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

		// Token: 0x06001343 RID: 4931 RVA: 0x0003789F File Offset: 0x00035A9F
		public void WriteValue(double value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x000378B3 File Offset: 0x00035AB3
		public void WriteValue(Guid value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000378C7 File Offset: 0x00035AC7
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

		// Token: 0x06001346 RID: 4934 RVA: 0x000378FB File Offset: 0x00035AFB
		public void WriteValue(DateTimeOffset value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value, ODataJsonDateTimeFormat.ISO8601DateTime);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x00037910 File Offset: 0x00035B10
		public void WriteValue(TimeSpan value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00037924 File Offset: 0x00035B24
		public void WriteValue(TimeOfDay value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x00037938 File Offset: 0x00035B38
		public void WriteValue(Date value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0003794C File Offset: 0x00035B4C
		public void WriteValue(byte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x00037960 File Offset: 0x00035B60
		public void WriteValue(sbyte value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00037974 File Offset: 0x00035B74
		public void WriteValue(string value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x00037988 File Offset: 0x00035B88
		public void WriteValue(byte[] value)
		{
			this.WriteValueSeparator();
			JsonValueUtils.WriteValue(this.writer, value);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x0003799C File Offset: 0x00035B9C
		public void WriteRawValue(string rawValue)
		{
			this.WriteValueSeparator();
			this.writer.Write(rawValue);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000379B0 File Offset: 0x00035BB0
		public void Flush()
		{
			this.writer.Flush();
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x000379C0 File Offset: 0x00035BC0
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

		// Token: 0x06001351 RID: 4945 RVA: 0x00037A18 File Offset: 0x00035C18
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

		// Token: 0x040009C4 RID: 2500
		private readonly TextWriterWrapper writer;

		// Token: 0x040009C5 RID: 2501
		private readonly Stack<JsonWriter.Scope> scopes;

		// Token: 0x040009C6 RID: 2502
		private readonly bool isIeee754Compatible;

		// Token: 0x02000316 RID: 790
		internal enum ScopeType
		{
			// Token: 0x04000CE3 RID: 3299
			Array,
			// Token: 0x04000CE4 RID: 3300
			Object,
			// Token: 0x04000CE5 RID: 3301
			Padding
		}

		// Token: 0x02000317 RID: 791
		private sealed class Scope
		{
			// Token: 0x06001A23 RID: 6691 RVA: 0x0004AF34 File Offset: 0x00049134
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

			// Token: 0x170005A0 RID: 1440
			// (get) Token: 0x06001A24 RID: 6692 RVA: 0x0004AFA5 File Offset: 0x000491A5
			// (set) Token: 0x06001A25 RID: 6693 RVA: 0x0004AFAD File Offset: 0x000491AD
			public string StartString { get; private set; }

			// Token: 0x170005A1 RID: 1441
			// (get) Token: 0x06001A26 RID: 6694 RVA: 0x0004AFB6 File Offset: 0x000491B6
			// (set) Token: 0x06001A27 RID: 6695 RVA: 0x0004AFBE File Offset: 0x000491BE
			public string EndString { get; private set; }

			// Token: 0x170005A2 RID: 1442
			// (get) Token: 0x06001A28 RID: 6696 RVA: 0x0004AFC7 File Offset: 0x000491C7
			// (set) Token: 0x06001A29 RID: 6697 RVA: 0x0004AFCF File Offset: 0x000491CF
			public int ObjectCount { get; set; }

			// Token: 0x170005A3 RID: 1443
			// (get) Token: 0x06001A2A RID: 6698 RVA: 0x0004AFD8 File Offset: 0x000491D8
			public JsonWriter.ScopeType Type
			{
				get
				{
					return this.type;
				}
			}

			// Token: 0x04000CE6 RID: 3302
			private readonly JsonWriter.ScopeType type;
		}
	}
}
