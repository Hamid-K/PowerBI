using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x02000021 RID: 33
	internal sealed class YamlJsonReader : JsonReader, IJsonLineInfo, IParsingEventVisitor
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002F1B File Offset: 0x0000111B
		internal YamlJsonReader(TextReader reader)
			: this(new Parser(reader))
		{
			this._underlyingReader = reader;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002F30 File Offset: 0x00001130
		internal YamlJsonReader(IParser parser)
		{
			this._parser = parser;
			this._containerStack = new Stack<JsonReader.State>();
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002F4A File Offset: 0x0000114A
		public int LineNumber
		{
			get
			{
				return this._lineInfo.Line;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002F57 File Offset: 0x00001157
		public int LinePosition
		{
			get
			{
				return this._lineInfo.Column;
			}
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002F64 File Offset: 0x00001164
		public bool HasLineInfo()
		{
			return this._lineInfo != null;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002F6F File Offset: 0x0000116F
		public override void Close()
		{
			base.Close();
			if (base.CloseInput && this._underlyingReader != null)
			{
				this._underlyingReader.Dispose();
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002F94 File Offset: 0x00001194
		public override bool Read()
		{
			if (this._containerStack.Count == 0)
			{
				if (this._parser.Current == null)
				{
					this._parser.MoveNext();
				}
				this._containerStack.Push(JsonReader.State.Start);
			}
			if (this._parser.Current != null)
			{
				do
				{
					this._skip = false;
					this._parser.Current.Accept(this);
					this._lineInfo = this._parser.Current.Start;
				}
				while (this._parser.MoveNext() && this._skip);
				return !this._skip;
			}
			return false;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000302E File Offset: 0x0000122E
		void IParsingEventVisitor.Visit(AnchorAlias e)
		{
			throw new YamlException(this._parser.Current.Start, this._parser.Current.End, "Anchor aliases are not supported in this context.");
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000305A File Offset: 0x0000125A
		void IParsingEventVisitor.Visit(StreamStart e)
		{
			this._skip = true;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003063 File Offset: 0x00001263
		void IParsingEventVisitor.Visit(StreamEnd e)
		{
			this._skip = true;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000306C File Offset: 0x0000126C
		void IParsingEventVisitor.Visit(DocumentStart e)
		{
			this._skip = true;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003075 File Offset: 0x00001275
		void IParsingEventVisitor.Visit(DocumentEnd e)
		{
			this._skip = true;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003080 File Offset: 0x00001280
		void IParsingEventVisitor.Visit(Scalar e)
		{
			if (this._containerStack.Peek() == JsonReader.State.Object && base.CurrentState != JsonReader.State.Property)
			{
				base.SetToken(JsonToken.PropertyName, e.Value);
				return;
			}
			global::System.ValueTuple<JsonToken, object> valueTuple = YamlCoreSchema.ParseValueToJson(e);
			base.SetToken(valueTuple.Item1, valueTuple.Item2);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000030CB File Offset: 0x000012CB
		void IParsingEventVisitor.Visit(SequenceStart e)
		{
			base.SetToken(JsonToken.StartArray);
			this._containerStack.Push(JsonReader.State.Array);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000030E0 File Offset: 0x000012E0
		void IParsingEventVisitor.Visit(SequenceEnd e)
		{
			base.SetToken(JsonToken.EndArray);
			this._containerStack.Pop();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000030F6 File Offset: 0x000012F6
		void IParsingEventVisitor.Visit(MappingStart e)
		{
			base.SetToken(JsonToken.StartObject);
			this._containerStack.Push(JsonReader.State.Object);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000310B File Offset: 0x0000130B
		void IParsingEventVisitor.Visit(MappingEnd e)
		{
			base.SetToken(JsonToken.EndObject);
			this._containerStack.Pop();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003121 File Offset: 0x00001321
		void IParsingEventVisitor.Visit(Comment e)
		{
			base.SetToken(JsonToken.Comment, e.Value);
		}

		// Token: 0x04000048 RID: 72
		private readonly IDisposable _underlyingReader;

		// Token: 0x04000049 RID: 73
		private readonly IParser _parser;

		// Token: 0x0400004A RID: 74
		private readonly Stack<JsonReader.State> _containerStack;

		// Token: 0x0400004B RID: 75
		private Mark _lineInfo;

		// Token: 0x0400004C RID: 76
		private bool _skip;
	}
}
