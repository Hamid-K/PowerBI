using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using YamlDotNet.Core;
using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.ObjectGraphVisitors;
using YamlDotNet.Serialization.Utilities;

namespace Microsoft.Lucia.Yaml
{
	// Token: 0x0200001F RID: 31
	public static class YamlCommentExtensions
	{
		// Token: 0x0600006B RID: 107 RVA: 0x00002B50 File Offset: 0x00000D50
		public static T DeserializeWithCommentSupport<T>(this DeserializerBuilder builder, TextReader reader)
		{
			return builder.BuildWithCommentSupport().Deserialize<T>(new Parser(new Scanner(reader, false)));
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002B69 File Offset: 0x00000D69
		public static Deserializer BuildWithCommentSupport(this DeserializerBuilder builder)
		{
			return Deserializer.FromValueDeserializer(new YamlCommentExtensions.CommentValueDeserializer(builder.BuildValueDeserializer()));
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002B7B File Offset: 0x00000D7B
		public static SerializerBuilder WithCommentSupport(this SerializerBuilder builder)
		{
			return builder.WithEmissionPhaseObjectGraphVisitor<YamlCommentExtensions.CommentSerializingObjectGraphVisitor>(([Nullable(1)] EmissionPhaseObjectGraphVisitorArgs a) => new YamlCommentExtensions.CommentSerializingObjectGraphVisitor(a));
		}

		// Token: 0x020001E4 RID: 484
		private sealed class CommentValueDeserializer : IValueDeserializer
		{
			// Token: 0x06000A95 RID: 2709 RVA: 0x0001387F File Offset: 0x00011A7F
			internal CommentValueDeserializer(IValueDeserializer innerDeserializer)
			{
				this._innerDeserializer = innerDeserializer;
				this._nextComments = new List<string>();
			}

			// Token: 0x06000A96 RID: 2710 RVA: 0x0001389C File Offset: 0x00011A9C
			object IValueDeserializer.DeserializeValue(IParser parser, Type expectedType, SerializerState state, IValueDeserializer nestedObjectDeserializer)
			{
				List<string> list = ((this._nextComments.Count > 0) ? this._nextComments.ToList<string>() : null);
				this._nextComments.Clear();
				Comment comment;
				while (ParserExtensions.TryConsume<Comment>(parser, ref comment))
				{
					YamlCommentExtensions.CommentValueDeserializer.AddComment(ref list, comment);
				}
				object obj = this._innerDeserializer.DeserializeValue(parser, expectedType, state, nestedObjectDeserializer);
				Comment comment2;
				while (ParserExtensions.TryConsume<Comment>(parser, ref comment2))
				{
					if (comment2.IsInline)
					{
						YamlCommentExtensions.CommentValueDeserializer.AddComment(ref list, comment2);
					}
					else
					{
						this._nextComments.Add(YamlCommentExtensions.CommentValueDeserializer.CommentToString(comment2));
					}
				}
				if (list != null)
				{
					IPreserveComments preserveComments = obj as IPreserveComments;
					if (preserveComments != null)
					{
						preserveComments.Comments = list;
					}
				}
				return obj;
			}

			// Token: 0x06000A97 RID: 2711 RVA: 0x0001393D File Offset: 0x00011B3D
			private static void AddComment(ref List<string> list, Comment comment)
			{
				if (list == null)
				{
					list = new List<string>();
				}
				list.Add(YamlCommentExtensions.CommentValueDeserializer.CommentToString(comment));
			}

			// Token: 0x06000A98 RID: 2712 RVA: 0x00013958 File Offset: 0x00011B58
			private static string CommentToString(Comment comment)
			{
				int num = comment.End.Column - comment.Start.Column - comment.Value.Length - 2;
				if (num <= 0)
				{
					return comment.Value;
				}
				return new string(' ', num) + comment.Value;
			}

			// Token: 0x040007FD RID: 2045
			private readonly IValueDeserializer _innerDeserializer;

			// Token: 0x040007FE RID: 2046
			private readonly List<string> _nextComments;
		}

		// Token: 0x020001E5 RID: 485
		private sealed class CommentSerializingObjectGraphVisitor : ChainedObjectGraphVisitor
		{
			// Token: 0x06000A99 RID: 2713 RVA: 0x000139A9 File Offset: 0x00011BA9
			internal CommentSerializingObjectGraphVisitor(EmissionPhaseObjectGraphVisitorArgs args)
				: base(args.InnerVisitor)
			{
			}

			// Token: 0x06000A9A RID: 2714 RVA: 0x000139B8 File Offset: 0x00011BB8
			public override bool Enter(IObjectDescriptor value, IEmitter emitter)
			{
				if (base.Enter(value, emitter))
				{
					IPreserveComments preserveComments = value.Value as IPreserveComments;
					if (preserveComments != null && !preserveComments.Comments.IsNullOrEmpty<string>())
					{
						foreach (string text in preserveComments.Comments)
						{
							emitter.Emit(new Comment(text, false));
						}
						if (preserveComments.CommentStyle == CommentStyle.BlockWithLineBreakAfter)
						{
							ITextWriterEmitter textWriterEmitter = emitter as ITextWriterEmitter;
							if (textWriterEmitter != null)
							{
								textWriterEmitter.Output.WriteLine();
							}
						}
					}
					return true;
				}
				return false;
			}
		}
	}
}
