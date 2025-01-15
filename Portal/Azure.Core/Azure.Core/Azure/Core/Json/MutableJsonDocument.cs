using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure.Core.Json
{
	// Token: 0x020000BF RID: 191
	[NullableContext(1)]
	[Nullable(0)]
	[RequiresUnreferencedCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[RequiresDynamicCode("This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.")]
	[JsonConverter(typeof(MutableJsonDocument.MutableJsonDocumentConverter))]
	internal sealed class MutableJsonDocument : IDisposable
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060005D6 RID: 1494 RVA: 0x000121AB File Offset: 0x000103AB
		internal JsonSerializerOptions SerializerOptions
		{
			get
			{
				return this._serializerOptions;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x000121B3 File Offset: 0x000103B3
		internal MutableJsonDocument.ChangeTracker Changes
		{
			get
			{
				if (this._changeTracker == null)
				{
					this._changeTracker = new MutableJsonDocument.ChangeTracker();
				}
				return this._changeTracker;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060005D8 RID: 1496 RVA: 0x000121CE File Offset: 0x000103CE
		public MutableJsonElement RootElement
		{
			get
			{
				return new MutableJsonElement(this, this._originalDocument.RootElement, string.Empty, -1);
			}
		}

		// Token: 0x060005D9 RID: 1497 RVA: 0x000121E8 File Offset: 0x000103E8
		public void WriteTo(Stream stream, [Nullable(2)] string format = null)
		{
			Argument.AssertNotNull<Stream>(stream, "stream");
			if (format == "J" || format == null)
			{
				this.WriteJson(stream);
				return;
			}
			if (!(format == "P"))
			{
				this.AssertInvalidFormat(format);
				return;
			}
			this.WritePatch(stream);
		}

		// Token: 0x060005DA RID: 1498 RVA: 0x00012236 File Offset: 0x00010436
		[NullableContext(2)]
		internal void AssertInvalidFormat(string format)
		{
			throw new FormatException("Unsupported format " + format + ". Supported formats are: \"J\" - JSON, \"P\" - JSON Merge Patch.");
		}

		// Token: 0x060005DB RID: 1499 RVA: 0x00012250 File Offset: 0x00010450
		private void WriteJson(Stream stream)
		{
			if (!this.Changes.HasChanges)
			{
				this.WriteOriginal(stream);
				return;
			}
			using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(stream, default(JsonWriterOptions)))
			{
				this.RootElement.WriteTo(utf8JsonWriter);
			}
		}

		// Token: 0x060005DC RID: 1500 RVA: 0x000122B0 File Offset: 0x000104B0
		private void WriteOriginal(Stream stream)
		{
			if (this._original.Length == 0)
			{
				using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(stream, default(JsonWriterOptions)))
				{
					this._originalDocument.WriteTo(utf8JsonWriter);
					return;
				}
			}
			MutableJsonDocument.Write(stream, this._original.Span);
		}

		// Token: 0x060005DD RID: 1501 RVA: 0x00012314 File Offset: 0x00010514
		private void WritePatch(Stream stream)
		{
			if (!this.Changes.HasChanges)
			{
				return;
			}
			using (Utf8JsonWriter utf8JsonWriter = new Utf8JsonWriter(stream, default(JsonWriterOptions)))
			{
				this.RootElement.WritePatch(utf8JsonWriter);
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001236C File Offset: 0x0001056C
		public void WriteTo(Utf8JsonWriter writer)
		{
			Argument.AssertNotNull<Utf8JsonWriter>(writer, "writer");
			if (!this.Changes.HasChanges)
			{
				this._originalDocument.RootElement.WriteTo(writer);
				return;
			}
			this.RootElement.WriteTo(writer);
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x000123B8 File Offset: 0x000105B8
		[NullableContext(0)]
		private static void Write([Nullable(1)] Stream stream, ReadOnlySpan<byte> buffer)
		{
			byte[] array = ArrayPool<byte>.Shared.Rent(buffer.Length);
			try
			{
				buffer.CopyTo(array);
				stream.Write(array, 0, buffer.Length);
			}
			finally
			{
				ArrayPool<byte>.Shared.Return(array, false);
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00012414 File Offset: 0x00010614
		[NullableContext(0)]
		[return: Nullable(1)]
		public static MutableJsonDocument Parse(ReadOnlyMemory<byte> utf8Json, [Nullable(2)] JsonSerializerOptions serializerOptions = null)
		{
			return new MutableJsonDocument(JsonDocument.Parse(utf8Json, default(JsonDocumentOptions)), utf8Json, serializerOptions);
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00012438 File Offset: 0x00010638
		public static MutableJsonDocument Parse(ref Utf8JsonReader reader, [Nullable(2)] JsonSerializerOptions serializerOptions = null)
		{
			return new MutableJsonDocument(JsonDocument.ParseValue(ref reader), default(ReadOnlyMemory<byte>), serializerOptions);
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x0001245C File Offset: 0x0001065C
		public static MutableJsonDocument Parse(BinaryData utf8Json, [Nullable(2)] JsonSerializerOptions serializerOptions = null)
		{
			return new MutableJsonDocument(JsonDocument.Parse(utf8Json, default(JsonDocumentOptions)), utf8Json.ToMemory(), serializerOptions);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001248C File Offset: 0x0001068C
		public static MutableJsonDocument Parse(string json, [Nullable(2)] JsonSerializerOptions serializerOptions = null)
		{
			Memory<byte> memory = MemoryExtensions.AsMemory<byte>(Encoding.UTF8.GetBytes(json));
			return new MutableJsonDocument(JsonDocument.Parse(memory, default(JsonDocumentOptions)), memory, serializerOptions);
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x000124CA File Offset: 0x000106CA
		public void Dispose()
		{
			this._originalDocument.Dispose();
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x000124D7 File Offset: 0x000106D7
		[NullableContext(0)]
		private MutableJsonDocument([Nullable(1)] JsonDocument document, ReadOnlyMemory<byte> utf8Json, [Nullable(2)] JsonSerializerOptions serializerOptions)
		{
			this._originalDocument = document;
			this._original = utf8Json;
			this._serializerOptions = serializerOptions ?? MutableJsonDocument.DefaultSerializerOptions;
		}

		// Token: 0x04000266 RID: 614
		private static readonly JsonSerializerOptions DefaultSerializerOptions = new JsonSerializerOptions();

		// Token: 0x04000267 RID: 615
		[Nullable(0)]
		private readonly ReadOnlyMemory<byte> _original;

		// Token: 0x04000268 RID: 616
		private readonly JsonDocument _originalDocument;

		// Token: 0x04000269 RID: 617
		private readonly JsonSerializerOptions _serializerOptions;

		// Token: 0x0400026A RID: 618
		internal const string SerializationRequiresUnreferencedCodeClass = "This class utilizes reflection-based JSON serialization and deserialization which is not compatible with trimming.";

		// Token: 0x0400026B RID: 619
		[Nullable(2)]
		private MutableJsonDocument.ChangeTracker _changeTracker;

		// Token: 0x02000143 RID: 323
		[Nullable(0)]
		internal class ChangeTracker
		{
			// Token: 0x170001EA RID: 490
			// (get) Token: 0x0600087D RID: 2173 RVA: 0x00020B03 File Offset: 0x0001ED03
			internal bool HasChanges
			{
				get
				{
					return this._changes != null && this._changes.Count > 0;
				}
			}

			// Token: 0x0600087E RID: 2174 RVA: 0x00020B20 File Offset: 0x0001ED20
			internal bool AncestorChanged(string path, int highWaterMark)
			{
				if (this._changes == null)
				{
					return false;
				}
				bool flag = false;
				while (!flag && path.Length > 0)
				{
					path = MutableJsonDocument.ChangeTracker.PopProperty(path);
					MutableJsonChange mutableJsonChange;
					flag = this.TryGetChange(path, in highWaterMark, out mutableJsonChange);
				}
				return flag;
			}

			// Token: 0x0600087F RID: 2175 RVA: 0x00020B5C File Offset: 0x0001ED5C
			internal bool DescendantChanged(string path, int highWaterMark)
			{
				if (this._changes == null)
				{
					return false;
				}
				bool flag = false;
				for (int i = this._changes.Count - 1; i > highWaterMark; i--)
				{
					if (this._changes[i].IsDescendant(path))
					{
						return true;
					}
				}
				return flag;
			}

			// Token: 0x06000880 RID: 2176 RVA: 0x00020BA7 File Offset: 0x0001EDA7
			internal bool TryGetChange(string path, in int lastAppliedChange, out MutableJsonChange change)
			{
				return this.TryGetChange(MemoryExtensions.AsSpan(path), in lastAppliedChange, out change);
			}

			// Token: 0x06000881 RID: 2177 RVA: 0x00020BB8 File Offset: 0x0001EDB8
			[NullableContext(0)]
			internal bool TryGetChange(ReadOnlySpan<char> path, in int lastAppliedChange, out MutableJsonChange change)
			{
				if (this._changes == null)
				{
					change = default(MutableJsonChange);
					return false;
				}
				for (int i = this._changes.Count - 1; i > lastAppliedChange; i--)
				{
					MutableJsonChange mutableJsonChange = this._changes[i];
					if (MemoryExtensions.SequenceEqual<char>(MemoryExtensions.AsSpan(mutableJsonChange.Path), path))
					{
						change = mutableJsonChange;
						return true;
					}
				}
				change = default(MutableJsonChange);
				return false;
			}

			// Token: 0x06000882 RID: 2178 RVA: 0x00020C24 File Offset: 0x0001EE24
			[NullableContext(2)]
			internal int AddChange([Nullable(1)] string path, object value, MutableJsonChangeKind changeKind = MutableJsonChangeKind.PropertyUpdate, string addedPropertyName = null)
			{
				if (this._changes == null)
				{
					this._changes = new List<MutableJsonChange>();
				}
				int count = this._changes.Count;
				this._changes.Add(new MutableJsonChange(path, count, value, changeKind, addedPropertyName));
				return count;
			}

			// Token: 0x06000883 RID: 2179 RVA: 0x00020C67 File Offset: 0x0001EE67
			internal IEnumerable<MutableJsonChange> GetAddedProperties(string path, int highWaterMark)
			{
				if (this._changes == null)
				{
					yield break;
				}
				int num;
				for (int i = this._changes.Count - 1; i > highWaterMark; i = num - 1)
				{
					MutableJsonChange mutableJsonChange = this._changes[i];
					if (mutableJsonChange.IsDirectDescendant(path) && mutableJsonChange.ChangeKind == MutableJsonChangeKind.PropertyAddition)
					{
						yield return mutableJsonChange;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06000884 RID: 2180 RVA: 0x00020C85 File Offset: 0x0001EE85
			internal IEnumerable<MutableJsonChange> GetRemovedProperties(string path, int highWaterMark)
			{
				if (this._changes == null)
				{
					yield break;
				}
				int num;
				for (int i = this._changes.Count - 1; i > highWaterMark; i = num - 1)
				{
					MutableJsonChange mutableJsonChange = this._changes[i];
					if (mutableJsonChange.IsDirectDescendant(path) && mutableJsonChange.ChangeKind == MutableJsonChangeKind.PropertyRemoval)
					{
						yield return mutableJsonChange;
					}
					num = i;
				}
				yield break;
			}

			// Token: 0x06000885 RID: 2181 RVA: 0x00020CA4 File Offset: 0x0001EEA4
			[NullableContext(0)]
			internal MutableJsonChange? GetFirstMergePatchChange(ReadOnlySpan<char> rootPath, out int maxPathLength)
			{
				maxPathLength = -1;
				if (this._changes == null)
				{
					return null;
				}
				MutableJsonChange? mutableJsonChange = null;
				for (int i = this._changes.Count - 1; i >= 0; i--)
				{
					MutableJsonChange mutableJsonChange2 = this._changes[i];
					if (MemoryExtensions.StartsWith<char>(MemoryExtensions.AsSpan(mutableJsonChange2.Path), rootPath) && (mutableJsonChange == null || mutableJsonChange2.IsLessThan(MemoryExtensions.AsSpan(mutableJsonChange.Value.Path))))
					{
						mutableJsonChange = new MutableJsonChange?(mutableJsonChange2);
					}
					if (mutableJsonChange2.Path.Length > maxPathLength)
					{
						maxPathLength = mutableJsonChange2.Path.Length;
					}
				}
				return mutableJsonChange;
			}

			// Token: 0x06000886 RID: 2182 RVA: 0x00020D58 File Offset: 0x0001EF58
			[NullableContext(0)]
			internal MutableJsonChange? GetNextMergePatchChange(ReadOnlySpan<char> rootPath, ReadOnlySpan<char> lastChangePath)
			{
				if (this._changes == null)
				{
					return null;
				}
				MutableJsonChange? mutableJsonChange = null;
				for (int i = this._changes.Count - 1; i >= 0; i--)
				{
					MutableJsonChange mutableJsonChange2 = this._changes[i];
					if (MemoryExtensions.StartsWith<char>(MemoryExtensions.AsSpan(mutableJsonChange2.Path), rootPath) && mutableJsonChange2.IsGreaterThan(lastChangePath) && (mutableJsonChange == null || mutableJsonChange2.IsLessThan(MemoryExtensions.AsSpan(mutableJsonChange.Value.Path))) && !mutableJsonChange2.IsDescendant(lastChangePath))
					{
						mutableJsonChange = new MutableJsonChange?(mutableJsonChange2);
					}
				}
				return mutableJsonChange;
			}

			// Token: 0x06000887 RID: 2183 RVA: 0x00020DFC File Offset: 0x0001EFFC
			internal bool WasRemoved(string path, int highWaterMark)
			{
				if (this._changes == null)
				{
					return false;
				}
				for (int i = this._changes.Count - 1; i > highWaterMark; i--)
				{
					MutableJsonChange mutableJsonChange = this._changes[i];
					if (mutableJsonChange.Path == path && mutableJsonChange.ChangeKind == MutableJsonChangeKind.PropertyRemoval)
					{
						return true;
					}
				}
				return false;
			}

			// Token: 0x06000888 RID: 2184 RVA: 0x00020E54 File Offset: 0x0001F054
			[NullableContext(0)]
			internal static MutableJsonDocument.ChangeTracker.SegmentEnumerator Split(ReadOnlySpan<char> path)
			{
				return new MutableJsonDocument.ChangeTracker.SegmentEnumerator(path);
			}

			// Token: 0x06000889 RID: 2185 RVA: 0x00020E5C File Offset: 0x0001F05C
			internal static string PushIndex(string path, int index)
			{
				return MutableJsonDocument.ChangeTracker.PushProperty(path, string.Format("{0}", index));
			}

			// Token: 0x0600088A RID: 2186 RVA: 0x00020E74 File Offset: 0x0001F074
			internal static string PopIndex(string path)
			{
				return MutableJsonDocument.ChangeTracker.PopProperty(path);
			}

			// Token: 0x0600088B RID: 2187 RVA: 0x00020E7C File Offset: 0x0001F07C
			internal static string PushProperty(string path, string value)
			{
				if (path.Length == 0)
				{
					return value;
				}
				return path + '\u0001' + value;
			}

			// Token: 0x0600088C RID: 2188 RVA: 0x00020E98 File Offset: 0x0001F098
			[NullableContext(0)]
			internal unsafe static void PushProperty(Span<char> path, ref int pathLength, ReadOnlySpan<char> value)
			{
				if (pathLength == 0)
				{
					value.Slice(0, value.Length).CopyTo(path);
					pathLength = value.Length;
					return;
				}
				*path[pathLength] = '\u0001';
				value.Slice(0, value.Length).CopyTo(path.Slice(pathLength + 1));
				pathLength += value.Length + 1;
			}

			// Token: 0x0600088D RID: 2189 RVA: 0x00020F08 File Offset: 0x0001F108
			internal static string PopProperty(string path)
			{
				int num = path.LastIndexOf('\u0001');
				if (num == -1)
				{
					return string.Empty;
				}
				return path.Substring(0, num);
			}

			// Token: 0x0600088E RID: 2190 RVA: 0x00020F30 File Offset: 0x0001F130
			[NullableContext(0)]
			internal static void PopProperty(Span<char> path, ref int pathLength)
			{
				int num = MemoryExtensions.LastIndexOf<char>(path.Slice(0, pathLength), '\u0001');
				pathLength = ((num == -1) ? 0 : num);
			}

			// Token: 0x040004EC RID: 1260
			[Nullable(2)]
			private List<MutableJsonChange> _changes;

			// Token: 0x040004ED RID: 1261
			internal const char Delimiter = '\u0001';

			// Token: 0x02000176 RID: 374
			[NullableContext(0)]
			internal ref struct SegmentEnumerator
			{
				// Token: 0x0600094C RID: 2380 RVA: 0x00023F92 File Offset: 0x00022192
				public SegmentEnumerator(ReadOnlySpan<char> path)
				{
					this._segmentLength = 0;
					this._current = default(ReadOnlySpan<char>);
					this._start = 0;
					this._path = path;
				}

				// Token: 0x0600094D RID: 2381 RVA: 0x00023FB5 File Offset: 0x000221B5
				public readonly MutableJsonDocument.ChangeTracker.SegmentEnumerator GetEnumerator()
				{
					return this;
				}

				// Token: 0x0600094E RID: 2382 RVA: 0x00023FC0 File Offset: 0x000221C0
				public bool MoveNext()
				{
					if (this._start > this._path.Length)
					{
						return false;
					}
					this._segmentLength = MemoryExtensions.IndexOf<char>(this._path.Slice(this._start), '\u0001');
					if (this._segmentLength == -1)
					{
						this._segmentLength = this._path.Length - this._start;
					}
					this._current = this._path.Slice(this._start, this._segmentLength);
					this._start += this._segmentLength + 1;
					return true;
				}

				// Token: 0x17000202 RID: 514
				// (get) Token: 0x0600094F RID: 2383 RVA: 0x00024053 File Offset: 0x00022253
				public readonly ReadOnlySpan<char> Current
				{
					get
					{
						return this._current;
					}
				}

				// Token: 0x040005B1 RID: 1457
				private readonly ReadOnlySpan<char> _path;

				// Token: 0x040005B2 RID: 1458
				private int _start;

				// Token: 0x040005B3 RID: 1459
				private int _segmentLength;

				// Token: 0x040005B4 RID: 1460
				private ReadOnlySpan<char> _current;
			}
		}

		// Token: 0x02000144 RID: 324
		[Nullable(new byte[] { 0, 1 })]
		[RequiresUnreferencedCode("Using MutableJsonDocument or MutableJsonDocumentConverter is not compatible with trimming due to reflection-based serialization.")]
		[RequiresDynamicCode("Using MutableJsonDocument or MutableJsonDocumentConverter is not compatible with trimming due to reflection-based serialization.")]
		private class MutableJsonDocumentConverter : JsonConverter<MutableJsonDocument>
		{
			// Token: 0x06000890 RID: 2192 RVA: 0x00020F60 File Offset: 0x0001F160
			public override MutableJsonDocument Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				return MutableJsonDocument.Parse(ref reader, null);
			}

			// Token: 0x06000891 RID: 2193 RVA: 0x00020F69 File Offset: 0x0001F169
			public override void Write(Utf8JsonWriter writer, MutableJsonDocument value, JsonSerializerOptions options)
			{
				value.WriteTo(writer);
			}

			// Token: 0x040004EE RID: 1262
			public const string classIsIncompatibleWithTrimming = "Using MutableJsonDocument or MutableJsonDocumentConverter is not compatible with trimming due to reflection-based serialization.";
		}
	}
}
